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


namespace KavoshFrameWorkWebApplication.Controllers
{

    public class CompanyShareController : BaseController
    {
        IMapper _mapper;
        
        IGenericRepository<CompanyShare> _companyShareRepository;
        IGenericRepository<CompanyShareholder> _companyShareholderRepository;
        IGenericRepository<Portfo> _portfoRepository;
        private readonly IEntityService entityService;

        public CompanyShareController(IGenericRepository<CompanyShare> companyShareRepository,
            IGenericRepository<CompanyShareholder> companyShareholderRepository,
             IGenericRepository<Portfo> portfoRepository, IMapper mapper, UserManager<ApplicationUser> userManager,                     IGenericRepository<CompanyUser> companyUserRepository, IEntityService _entityService)
        {
            _companyShareRepository = companyShareRepository;
            _mapper = mapper;
            this._userManager = userManager;
            _companyShareholderRepository = companyShareholderRepository;
            _portfoRepository = portfoRepository;
            this._companyUserRepository = companyUserRepository;
            entityService = _entityService;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.SystemAction = await entityService.GetUserAllowedActionFor(User.Identity.Name, typeof(Company));
            if ((((SystemAction)ViewBag.SystemAction) & SystemAction.Update) == 0)
                return Forbid();

            List<CompanyShare> items = _companyShareRepository.GetAsQueryable(includeProperties: "Company").ToList();

            if (User.IsInRole("Admin"))
                items = _companyShareRepository.GetAsQueryable(includeProperties: "Company").ToList();
            else
                items = _companyShareRepository.GetAsQueryable(x => CompanyIds.Contains(x.Id), includeProperties: "Company").ToList(); 


            var model = _mapper.Map<IEnumerable<CompanyShare>, IEnumerable<CompanyShareViewModel>>(items);
            return View(model);
        }
        
        [HttpGet]
        public IActionResult Create()
        {
            return View(new CompanyShareViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CompanyShareViewModel model)
        {
            model.SystemUserId = UserId;

            if (!HasAccess(model.CompanyId))
                return View("Error");
            if (ModelState.IsValid)
            {
                model.Date = model.JalaliDate.ToDateTime();
                if (model.Date > DateTime.Now)
                {
                    ModelState.AddModelError("", Resources.Messages.InvalidDate);
                    return View(model);
                }

                var capitalChangePercentage = Math.Round(100 * ((model.CurrentCapital - model.PreviousCapital) / (double)model.PreviousCapital), 2);
                var sumOfCapitalIncrease = model.CapitalIncreaseFromAccumulatedProfits + model.CapitalIncreaseFromAssetsRevaluation +
                    model.CapitalIncreaseFromCash + model.CapitalIncreaseFromShareholderReceivables;

                //if (Math.Round((Math.Round(100 * Math.Abs(capitalChangePercentage), 2) / 100), 2) != (Math.Round(100 * Math.Abs(sumOfCapitalIncrease), 2) / 100))
                //{
                //    ModelState.AddModelError("", $"جمع آورده ها با درصد تغییرات سرمایه محاسبه شده یعنی {Math.Round((Math.Round(100 * Math.Abs(capitalChangePercentage), 2) / 100), 2) } باید یکی باشد");
                //    return View(model);
                //}

                //await ReCalculatePortfo();

                var item = _mapper.Map<CompanyShareViewModel, CompanyShare>(model);
                await _companyShareRepository.InsertAsync(item);

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
            var item = await _companyShareRepository.GetByIDAsync(id);


            if (!HasAccess(item.CompanyId))
                return View("Error");
            var model = _mapper.Map<CompanyShare, CompanyShareViewModel>(item);

            model.JalaliDate = model.Date.ToShamsi();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CompanyShareViewModel model)
        {
            model.SystemUserId = UserId;

            if (!HasAccess(model.CompanyId))
                return View("Error");

            if (ModelState.IsValid)
            {
                model.Date = model.JalaliDate.ToDateTime();
                if (model.Date > DateTime.Now)
                {
                    ModelState.AddModelError("", Resources.Messages.InvalidDate);
                    return View(model);
                }

                var capitalChangePercentage =Math.Round( 100 * ((model.CurrentCapital - model.PreviousCapital) / (double)model.PreviousCapital),2);
                var sumOfCapitalIncrease = model.CapitalIncreaseFromAccumulatedProfits + model.CapitalIncreaseFromAssetsRevaluation +
                    model.CapitalIncreaseFromCash + model.CapitalIncreaseFromShareholderReceivables;

               

                //if (Math.Round((Math.Round(100 * Math.Abs(capitalChangePercentage), 2) / 100), 2) != (Math.Round(100 * Math.Abs(sumOfCapitalIncrease), 2) / 100))
                //{
                //    ModelState.AddModelError("", $"جمع آورده ها با درصد تغییرات سرمایه محاسبه شده یعنی {Math.Round((Math.Round(100 * Math.Abs(capitalChangePercentage), 2) / 100), 2) } باید یکی باشد");
                //    return View(model);
                //}

                var item = _mapper.Map<CompanyShareViewModel, CompanyShare>(model);
                var result = await _companyShareRepository.UpdateAsync(item);

                //await ReCalculatePortfo();

                ErrorMessage = Resources.Messages.ChangesSavedSuccessfully;

                if (Request.Form.Keys.Contains("SaveAndReturn"))
                    return RedirectToAction("Index");
                else
                    return RedirectToAction("Edit", new { id = item.Id });
            }
            return View(model);
        }
        public bool NearlyEqual(double a, double b, double epsilon)
        {
            const double MinNormal = 2.2250738585072014E-308d;
            double absA = Math.Abs(a);
            double absB = Math.Abs(b);
            double diff = Math.Abs(a - b);

            if (a.Equals(b))
            { // shortcut, handles infinities
                return true;
            }
            else if (a == 0 || b == 0 || absA + absB < MinNormal)
            {
                // a or b is zero or both are extremely close to it
                // relative error is less meaningful here
                return diff < (epsilon * MinNormal);
            }
            else
            { // use relative error
                return diff / (absA + absB) < epsilon;
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            var item = await _companyShareRepository.GetByIDAsync(id);

            if (!HasAccess(item.CompanyId))
                return View("Error");

            var result = await _companyShareRepository.DeleteAsync(id);
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
                var result = await _portfoRepository.UpdateAsync(portfo);
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

    }
}
