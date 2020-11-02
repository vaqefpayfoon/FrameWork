using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Reflection;
using AutoMapper;
using System.Net.Http;
using KavoshFrameWorkData.Repositories.Generic;
using KavoshFrameWorkCore.Models;
using KavoshFrameWorkWebApplication.Models;
using KavoshFrameWorkCommon.Extensions;
using KavoshFrameWorkCore;
using KavoshFrameWorkCommon.Helpers;
using System.Security.Claims;
using NPOI.SS.UserModel;
using System.IO;
using NPOI.XSSF.UserModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using FormStatus = KavoshFrameWorkWebApplication.Models.FormStatus;
using Kendo.Mvc.UI;
using Serilog;
using Kendo.Mvc.Extensions;

namespace KavoshFrameWorkWebApplication.Controllers
{

    public class CompanyShareholderController : BaseController
    {
        IMapper _mapper;
        private readonly IGenericRepository<CompanyShare> companyShareRepository;
        IGenericRepository<CompanyShareholder> _companyShareholderRepository;
        IGenericRepository<CompanyShareholderArchive> _companyShareholderArchiveRepository;
        IGenericRepository<Portfo> _portfoRepository;
        IGenericRepository<CompanyShare> _companyShareRepository;
        IGenericRepository<Shareholder> _shareholderRepository;
        public CompanyShareholderController(IGenericRepository<CompanyShareholder> companyShareholderRepository, IMapper mapper, UserManager<ApplicationUser> userManager, IGenericRepository<CompanyShareholderArchive> companyShareholderArchiveRepository,
             IGenericRepository<CompanyShare> companyShareRepository, IGenericRepository<Portfo> portfoRepository,
                     IGenericRepository<CompanyUser> companyUserRepository, IGenericRepository<Shareholder> shareholderRepository
                     )
        {
            _companyShareholderRepository = companyShareholderRepository;
            _mapper = mapper;
            this.companyShareRepository = companyShareRepository;
            this._userManager = userManager;
            this._companyUserRepository = companyUserRepository;
            _companyShareholderArchiveRepository = companyShareholderArchiveRepository;
            _companyShareRepository = companyShareRepository;
            _portfoRepository = portfoRepository;
            _shareholderRepository = shareholderRepository;
        }

        public IActionResult Index()
        {
            var items = _companyShareholderRepository.GetAsQueryable(includeProperties: "Shareholder,Company").ToList();

            var model = _mapper.Map<IEnumerable<CompanyShareholder>, IEnumerable<CompanyShareholderViewModel>>(items);

            var companyShares = companyShareRepository.GetAsQueryable().Select(x => new CompanyShareViewModel
            {
                CompanyId = x.CompanyId,
                PreviousShares = x.PreviousShares,
                PreviousCapital = x.PreviousCapital,
                CurrentCapital = x.CurrentCapital,
                FormStatusFilter = x.FormStatus,
                Date = x.Date
            }).ToList();

            foreach (var item in model)
            {
                var companyShare = companyShares.Where(x => x.CompanyId == item.CompanyId && x.FormStatusFilter == KavoshFrameWorkCore.FormStatus.Verified).OrderByDescending(x => x.Date).FirstOrDefault();
                if (companyShare != null)
                {
                    var capitalChangePercentage = 100 * ((companyShare.CurrentCapital - companyShare.PreviousCapital) / (double)companyShare.PreviousCapital);
                    var totalShare = companyShare.PreviousShares * (1 + (capitalChangePercentage) / 100);
                    item.OwnershipPercentage = (item.Shares / totalShare) * 100;
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new CompanyShareholderViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CompanyShareholderViewModel model)
        {
            model.SystemUserId = UserId;

            if (!HasAccess(model.CompanyId))
                return View("Error");

            if (ModelState.IsValid)
            {
                if (model.IsMainShareholder)
                {
                    IEnumerable<CompanyShareholder> finds = await _companyShareholderRepository.GetAsync(woak => woak.CompanyId == model.CompanyId);

                    foreach (CompanyShareholder woak in finds)
                    {
                        woak.IsMainShareholder = false;
                    }
                }

                var item = _mapper.Map<CompanyShareholderViewModel, CompanyShareholder>(model);
                await _companyShareholderRepository.InsertAsync(item);

                var archive_item = _mapper.Map<CompanyShareholderViewModel, CompanyShareholderArchive>(model);

                await _companyShareholderArchiveRepository.InsertAsync(archive_item);

                ErrorMessage = Resources.Messages.ChangesSavedSuccessfully;
                if (Request.Form.Keys.Contains("SaveAndReturn"))
                    return RedirectToAction("Index");
                else
                    return RedirectToAction("Edit", new { id = item.Id });
            }
            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var companyShareHolder = await _companyShareholderRepository.GetByIDAsync(id);

            var companyShare = _companyShareRepository.GetAsQueryable(woak => woak.CompanyId == companyShareHolder.CompanyId && woak.FormStatus == KavoshFrameWorkCore.FormStatus.Verified).OrderByDescending(x => x.Date).FirstOrDefault();



            double ownershipPercentage = 0;
            if (companyShare != null)
            {
                var capitalChangePercentage = 100 * ((companyShare.CurrentCapital - companyShare.PreviousCapital) / (double)companyShare.PreviousCapital);
                var totalShare = companyShare.PreviousShares * (1 + (capitalChangePercentage) / 100);
                ownershipPercentage = ((companyShareHolder.Shares / totalShare) * 100);
            }
            string strShare = String.Format("{0:.##}", ownershipPercentage);

            if (!HasAccess(companyShareHolder.CompanyId))
                return View("Error");
            var model = _mapper.Map<CompanyShareholder, CompanyShareholderViewModel>(companyShareHolder);
            if(!string.IsNullOrEmpty(strShare))
                model.OwnershipPercentage = Convert.ToDouble(strShare);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CompanyShareholderViewModel model)
        {
            model.SystemUserId = UserId;

            if (!HasAccess(model.CompanyId))
                return View("Error");


            if (ModelState.IsValid)
            {
                if(model.IsMainShareholder)
                {
                    //var dup = await _companyShareholderRepository.GetAsync(woak => woak.IsMainShareholder && woak.CompanyId == model.CompanyId && !woak.IsDeleted);
                    //if (dup.Any())
                    //    return View("Error", new ErrorViewModel { RequestId = "این شرکت سهامدار اصلی دارد" });

                    IEnumerable<CompanyShareholder> finds = await _companyShareholderRepository.GetAsync(woak => woak.CompanyId == model.CompanyId);

                    foreach (CompanyShareholder woak in finds)
                    {
                        if(woak.Id != model.Id)
                            woak.IsMainShareholder = false;
                    }
                }

                var item = _mapper.Map<CompanyShareholderViewModel, CompanyShareholder>(model);
                var result = await _companyShareholderRepository.UpdateAsync(item);

                var archive_item = _mapper.Map<CompanyShareholderViewModel, CompanyShareholderArchive>(model);
                archive_item.Id = 0;
                await _companyShareholderArchiveRepository.InsertAsync(archive_item);

                ErrorMessage = Resources.Messages.ChangesSavedSuccessfully;

               
                //await ReCalculatePortfo();

                if (Request.Form.Keys.Contains("SaveAndReturn"))
                    return RedirectToAction("Index");
                else
                    return RedirectToAction("Edit", new { id = item.Id });
            }
            return View(model);
        }


        public async Task<IActionResult> Delete(int id)
        {
            var item = await _companyShareholderRepository.GetByIDAsync(id);

            if (!HasAccess(item.CompanyId))
                return View("Error");

            var result = await _companyShareholderRepository.DeleteAsync(id);
            if (result < 1)
            {
                if (TempData["Error"] != null)
                    TempData.Remove("Error");
                TempData.Add("Error", Resources.Messages.ProblemDeletingItem);
            }
            return RedirectToAction("Index");
        }

        public async Task ReCalculatePortfo12()
        {

            var portfos = _portfoRepository.GetAsQueryable(includeProperties: "Company.CompanyType,PortfoShareholderPairs.ShareholderCompany").ToList();
            var portfosViewModel = _mapper.Map<List<Portfo>, List<PortfoViewModel>>(portfos);

            foreach (var portfo in portfos)
            {
                var portfoViewModel = portfosViewModel.FirstOrDefault(x => x.Id == portfo.Id);
                portfoViewModel.CompanyTypeTitle = portfo.Company.CompanyType.Title;
            }
            foreach (var portfo in portfosViewModel)
            {
                foreach (var pair in portfo.PortfoShareholderPairs)
                {
                    var shareholder = _companyShareholderRepository.GetAsQueryable(x => x.CompanyId == pair.CompanyId && x.ShareholderId == pair.ShareholderCompanyShareholderId, includeProperties: "Shareholder").FirstOrDefault();

                    var companyShare = _companyShareRepository
                        .GetAsQueryable(x => x.CompanyId == pair.CompanyId).OrderByDescending(x => x.Id).FirstOrDefault();
                    if (pair.CompanyId == portfo.CompanyId)
                    {
                        portfo.CurrentCapital = companyShare.CurrentCapital;
                    }
                    if (companyShare != null)
                    {
                        var capitalChangePercentage = 100 * ((companyShare.CurrentCapital - companyShare.PreviousCapital) / (double)companyShare.PreviousCapital);
                        var totalShare = companyShare.PreviousShares * (1 + (capitalChangePercentage) / 100);

                        var ownershipPercentage = (shareholder.Shares / totalShare) * 100;
                        pair.Value = ownershipPercentage;

                        portfo.CompanyShareholders.Add(new CompanyShareholderViewModel { CompanyId = pair.CompanyId, Shares = shareholder.Shares, OwnershipPercentage = ownershipPercentage, ShareholderTitle = pair.ShareholderCompanyTitle, IsMainShareholder = shareholder.IsMainShareholder });
                    }
                }

                portfo.AggregatePercentage = CalcAggregatePercentage(portfo.PortfoShareholderPairs);
            }

            foreach (var portfo in portfos)
            {
                portfo.AggregatePercentage = portfosViewModel.Where(x => x.Id == portfo.Id).FirstOrDefault().AggregatePercentage;
                var result =await _portfoRepository.UpdateAsync(portfo);
            }

        }

        double CalcAggregatePercentage(List<PortfoShareholderPairViewModel> portfoShareholderPairs)
        {
            double total = 0.0;
            foreach (var group in portfoShareholderPairs.GroupBy(x => x.Row))
            {
                double subtotal = 0.0;
                foreach (var item in group)
                {
                    if (subtotal == 0.0)
                        subtotal = 1;
                    subtotal = subtotal * (item.Value) / 100;
                }
                total += subtotal;
            }
            return total * 100;
        }
        [HttpPost(Name = "ReloadShare")]
        public IActionResult ReloadShare(Tag tag)
        {
            var companyShare = _companyShareRepository.GetAsQueryable(woak => woak.CompanyId == tag.Id && woak.FormStatus == KavoshFrameWorkCore.FormStatus.Verified).OrderByDescending(x => x.Date).FirstOrDefault();

            var companyShareHolder = _companyShareholderRepository.GetAsQueryable(woak => woak.CompanyId == tag.Id && woak.ShareholderId == tag.EnactmentId).FirstOrDefault();            

            double ownershipPercentage = 0;
            if (companyShare != null && companyShareHolder != null)
            {
                var capitalChangePercentage = 100 * ((companyShare.CurrentCapital - companyShare.PreviousCapital) / (double)companyShare.PreviousCapital);
                var totalShare = companyShare.PreviousShares * (1 + (capitalChangePercentage) / 100);
                ownershipPercentage = ((companyShareHolder.Shares / totalShare) * 100);
            }
            else
            {
                return View("Error", new ErrorViewModel{ RequestId = "اطلاعات سهم شرکت نامعتبر" });
            }
            string strShare = String.Format("{0:.##}", ownershipPercentage);
            if (strShare == string.Empty)
                return Ok();
            return Ok(new { shares = strShare });
        }

        public ActionResult Shareholder_Read([DataSourceRequest] DataSourceRequest request)
        {
            try
            {
                return Json(_shareholderRepository.GetAll().ToList().ToDataSourceResult(request));
            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                return View("~/Views/Shared/Error.cshtml", new ErrorViewModel { RequestId = e.Message });
            }
        }
        public ActionResult Shareholder_Read_ValueMapper(int[] values)
        {
            try
            {
                var indices = new List<int>();

                if (values != null && values.Any())
                {
                    var index = 0;

                    foreach (var woak in TagHelper_Shareholder())
                    {
                        if (values.Contains(woak.Id))
                        {
                            indices.Add(index);
                        }

                        index += 1;
                    }
                }

                return Json(indices);
            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                return View("~/Views/Shared/Error.cshtml", new ErrorViewModel { RequestId = e.Message });
            }
        }
        public IEnumerable<Shareholder> TagHelper_Shareholder()
        {
            try
            {
                return _shareholderRepository.GetAll().ToList();

            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                return null;
            }
        }

    }
}
