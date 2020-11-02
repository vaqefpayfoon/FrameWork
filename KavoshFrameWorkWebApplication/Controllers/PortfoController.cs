using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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
using System.Globalization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Internal;
using System.Data.SqlClient;
using System.Data;

namespace KavoshFrameWorkWebApplication.Controllers
{
    public class PortfoController : BaseController
    {
        IMapper _mapper;
        readonly IHostingEnvironment hosting;
        IGenericRepository<Portfo> _portfoRepository;
        IGenericRepository<PortfoShareholderPair> _portfoShareholderPairRepository;
        IGenericRepository<CompanyShareholder> _companyShareholderRepository;
        IGenericRepository<CompanyShare> _companyShareRepository;
        IGenericRepository<Company> _companyRepository;
        IGenericRepository<Shareholder> _shareholderRepository;
        IConfiguration Configuration;
        public PortfoController(IGenericRepository<Portfo> portfoRepository, IMapper mapper,
             IHostingEnvironment hosting,
             UserManager<ApplicationUser> userManager,
             IGenericRepository<PortfoShareholderPair> portfoShareholderPairRepository,
             IGenericRepository<Company> companyRepository,
             IGenericRepository<CompanyShareholder> companyShareholderRepository,
        IGenericRepository<CompanyShare> companyShareRepository,
        IGenericRepository<CompanyUser> companyUserRepository,
        IGenericRepository<Shareholder> shareholderRepository, IConfiguration configuration)
        {
            _portfoRepository = portfoRepository;
            _mapper = mapper;
            this.hosting = hosting;
            this._userManager = userManager;
            this._companyUserRepository = companyUserRepository;
            _companyShareRepository = companyShareRepository;
            _companyShareholderRepository = companyShareholderRepository;
            _companyRepository = companyRepository;
            _portfoShareholderPairRepository = portfoShareholderPairRepository;
            _shareholderRepository = shareholderRepository;
            Configuration = configuration;
        }

        public IActionResult Index()
        {
            //var items = _portfoRepository.GetAsQueryable(x => HasAccess(x.CompanyId), includeProperties: "Company").ToList();
            RunStoreProcedure();            
            var items = _portfoRepository.GetAsQueryable(includeProperties: "Company").ToList();
            foreach (Portfo woak in items)
            {
                getPortfo2(woak.Id);
            }
            var model = _mapper.Map<IEnumerable<Portfo>, IEnumerable<PortfoViewModel>>(items);
            return View(model.Select(woak => new PortfoViewModel {Id = woak.Id, CompanyTitle = woak.CompanyTitle, AggregatePercentage = Math.Round((woak.AggregatePercentage * 100), 2) }));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new PortfoViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PortfoViewModel model)
        {
            model.SystemUserId = UserId;

            if (ModelState.IsValid)
            {
                var item = _mapper.Map<PortfoViewModel, Portfo>(model);
                await _portfoRepository.InsertAsync(item);

                ErrorMessage = Resources.Messages.ChangesSavedSuccessfully;
                if (Request.Form.Keys.Contains("SaveAndReturn"))
                    return RedirectToAction("Index");
                else
                    return RedirectToAction("Edit", new { id = item.Id });
            }
            return View(model);
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            HttpContext.Session.SetString("portfoId", id.ToString());
            PortfoViewModel model = getPortfo(id);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PortfoViewModel model)
        {
            model.SystemUserId = UserId;
            if (ModelState.IsValid)
            {
                var item = _mapper.Map<PortfoViewModel, Portfo>(model);
                var result = await _portfoRepository.UpdateAsync(item);

                ErrorMessage = Resources.Messages.ChangesSavedSuccessfully;

                if (Request.Form.Keys.Contains("SaveAndReturn"))
                    return RedirectToAction("Index");
                else
                    return RedirectToAction("Edit", new { id = item.Id });
            }
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ShareholderPairs(PortfoViewModel model)
        {
            var portfo = await _portfoRepository.GetByIDAsync(model.Id);
            portfo.AggregatePercentage = model.AggregatePercentage;
            await _portfoRepository.UpdateAsync(portfo);
            var result = await _portfoShareholderPairRepository.DeleteAllAsync(x => x.PortfoId == model.Id);

            var items = model.PortfoShareholderPairs.Where(x => x.Pair != null).Select(x => new PortfoShareholderPair
            {
                PortfoId = model.Id,
                AddedDate = DateTime.Now,
                CompanyId = int.Parse(x.Pair.Split('-')[0]),
                ShareholderId = int.Parse(x.Pair.Split('-')[1]),
                Row = x.Row,
                Column = 0,
            }).ToList();

            await _portfoShareholderPairRepository.InsertAllAsync(items);

            ErrorMessage = Resources.Messages.ChangesSavedSuccessfully;

            if (Request.Form.Keys.Contains("SaveAndReturn"))
                return RedirectToAction("Index");
            else
                return RedirectToAction("Edit", new { id = model.Id });
        }

        public async Task<IActionResult> Delete(int id)
        {
            var result = await _portfoRepository.DeleteAsync(id);
            if (result < 1)
            {
                if (TempData["Error"] != null)
                    TempData.Remove("Error");
                TempData.Add("Error", Resources.Messages.ProblemDeletingItem);
            }
            return RedirectToAction("Index");
        }

        public IActionResult Excel()
        {
            //RunStoreProcedure();

            var portfos = _portfoRepository.GetAsQueryable(woak => !woak.IsDeleted,includeProperties: "Company.CompanyType,PortfoShareholderPairs.Shareholder").ToList();
            //foreach (Portfo woak in portfos)
            //{
            //    getPortfo2(woak.Id);
            //}
            portfos = portfos.OrderByDescending(x => x.AggregatePercentage).ToList();
            List<PortfoViewModel> portfosViewModel = _mapper.Map<List<Portfo>, List<PortfoViewModel>>(portfos);


            List<PortfoShareholderViewModel> mustDelete = new List<PortfoShareholderViewModel>();

            foreach (var portfo in portfos)
            {
                var portfoViewModel = portfosViewModel.FirstOrDefault(x => x.Id == portfo.Id);
                portfoViewModel.CompanyTypeTitle = portfo.Company.CompanyType.Title;
            }
            foreach (var portfo in portfosViewModel)
            {
                var total = portfo.AggregatePercentage;
                IEnumerable<IGrouping<int, CompanyShareholder>> shareholder = _companyShareholderRepository.GetAsQueryable(x => x.CompanyId == portfo.CompanyId, includeProperties: "Shareholder").OrderByDescending(x => x.IsMainShareholder).ThenByDescending(x => x.Id).GroupBy(x => x.ShareholderId);
                List<PortfoShareholderViewModel> lstPortfiModel = new List<PortfoShareholderViewModel>();
                foreach (IGrouping<int, CompanyShareholder> woak in shareholder)
                {
                    foreach(CompanyShareholder pick in woak)
                    {
                        PortfoShareholderViewModel portfoView = new PortfoShareholderViewModel();
                        if (shareholder == null) continue;
                        var companyShare = _companyShareRepository.GetAsQueryable(x => x.CompanyId == pick.CompanyId && x.FormStatus == KavoshFrameWorkCore.FormStatus.Verified).OrderByDescending(x => x.Date).FirstOrDefault();                      
                        
                        if (companyShare != null)
                        {
                            portfo.CurrentCapital = companyShare.CurrentCapital;
                            var capitalChangePercentage = 100 * ((companyShare.CurrentCapital - companyShare.PreviousCapital) / (double)companyShare.PreviousCapital);
                            var totalShare = companyShare.PreviousShares * (1 + (capitalChangePercentage) / 100);
                            var ownershipPercentage = (pick.Shares / totalShare) * 100;

                            portfoView.Value = Math.Round(ownershipPercentage, 2);
                            portfoView.ShareholderId = pick.ShareholderId;
                            portfoView.CompanyId = pick.CompanyId;

                            if (portfoView.Value == 0)
                            {
                                if (!mustDelete.Any(push => push.ShareholderId == portfoView.ShareholderId && push.CompanyId == portfoView.CompanyId))
                                    mustDelete.Add(new PortfoShareholderViewModel { CompanyId = portfoView.CompanyId, ShareholderId = portfoView.ShareholderId });
                            }
                            portfo.CompanyShareholders.Add(new CompanyShareholderViewModel { CompanyId = portfoView.CompanyId, Shares = pick.Shares, OwnershipPercentage = ownershipPercentage, ShareholderTitle = portfoView.ShareholderCompanyTitle, IsMainShareholder = pick.IsMainShareholder });

                            portfoView.Column = total;
                            portfoView.IsMain = pick.IsMainShareholder;
                            portfoView.Title = pick.Shareholder.Title;
                            if (pick.IsMainShareholder)
                                portfo.Shares = pick.Shares;
                        }
                        lstPortfiModel.Add(portfoView);
                        break;
                    }

                }
                portfo.PortfoShareholderViewModels = lstPortfiModel;
                portfo.AggregatePercentage = CalcAggregatePercentage(portfo.PortfoShareholderPairs);
            }

            //portfosViewModel.Select(woak => woak.PortfoShareholderViewModels.RemoveAll(push => dicIds.ContainsKey(push.ShareholderId)));

            foreach (PortfoShareholderViewModel item in mustDelete)
            {
                foreach (var pick in portfosViewModel)
                {
                    foreach (var pick2 in pick.PortfoShareholderViewModels)
                    {
                        if (pick2.ShareholderId == item.ShareholderId && pick2.CompanyId == item.CompanyId)
                        {
                            pick2.show = false;
                        }
                    }
                }
            }
            return Export(portfosViewModel);
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
        double CalcAggregatePercentage(List<PortfoShareholderPairViewModel> portfoShareholderPairs, List<CompanyShareholder> companyShares)
        {
            double total = 0.0;
            foreach (IGrouping<int, PortfoShareholderPairViewModel> group in portfoShareholderPairs.GroupBy(x => x.Row))
            {
                double subtotal = 0.0;
                foreach (var item in group)
                {
                    if (subtotal == 0.0)
                        subtotal = 1;
                    var value = companyShares.FirstOrDefault(woak => woak.CompanyId == item.CompanyId && woak.Shareholder.Id == item.ShareholderId).OwnershipPercentage;
                    subtotal = subtotal * (value) / 100;
                }
                total += subtotal;
            }
            return (total * 100);
        }
        IActionResult Export(List<PortfoViewModel> model)
        {
            var filename = Guid.NewGuid().ToString() + ".xlsx";
            using (FileStream stream = new FileStream(hosting.WebRootPath + filename, FileMode.Create, FileAccess.Write))
            {
                IWorkbook wb = new XSSFWorkbook();
                XSSFSheet sheet = (XSSFSheet)wb.CreateSheet("Sheet1");

                sheet.RightToLeft = true;
                IRow header_row = sheet.CreateRow(0);
                header_row.CreateCell(0).SetCellValue("ردیف");
                header_row.CreateCell(1).SetCellValue("سهامدار");
                header_row.CreateCell(2).SetCellValue("نام شرکت");
                header_row.CreateCell(3).SetCellValue("تعداد سهام");
                header_row.CreateCell(4).SetCellValue("درصد سهام");
                header_row.CreateCell(5).SetCellValue("درصد تجمعی گروه");
                header_row.CreateCell(6).SetCellValue("ارتباط با هلدینگ");
                header_row.CreateCell(7).SetCellValue("ارتباط با هلدینگ");
                header_row.CreateCell(8).SetCellValue("نوع سرمایه‌گذاری");
                header_row.CreateCell(9).SetCellValue("سرمایه ثبت شده(میلیون ریال)");
                header_row.CreateCell(10).SetCellValue("تعداد نماینده");
                header_row.CreateCell(11).SetCellValue("سایر سهامداران اصلی");
                header_row.CreateCell(12).SetCellValue("درصد مالکیت");

                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 6, 7));

                header_row = sheet.CreateRow(1);
                header_row.CreateCell(0).SetCellValue("ردیف");
                header_row.CreateCell(1).SetCellValue("سهامدار");
                header_row.CreateCell(2).SetCellValue("نام شرکت");
                header_row.CreateCell(3).SetCellValue("تعداد سهام");
                header_row.CreateCell(4).SetCellValue("درصد سهام");
                header_row.CreateCell(5).SetCellValue("درصد تجمعی گروه");
                header_row.CreateCell(6).SetCellValue("سهامدار");
                header_row.CreateCell(7).SetCellValue(" تحت مدیریت");
                header_row.CreateCell(8).SetCellValue("نوع سرمایه‌گذاری");
                header_row.CreateCell(9).SetCellValue("سرمایه ثبت شده(میلیون ریال)");
                header_row.CreateCell(10).SetCellValue("تعداد نماینده");
                header_row.CreateCell(11).SetCellValue("سایر سهامداران اصلی");
                header_row.CreateCell(12).SetCellValue("درصد مالکیت");

                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 1, 0, 0));
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 1, 1, 1));
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 1, 2, 2));
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 1, 3, 3));
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 1, 4, 4));
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 1, 5, 5));
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 1, 8, 8));
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 1, 9, 9));
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 1, 10, 10));
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 1, 11, 11));
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 1, 12, 12));

                int k = 2;
                int currGrp = 0;
                int prevGrp = 0;
                int keepRow = 0;
                for (int i = 0; i < model.Count; i++)
                {
                    
                    var modelRow = model.ElementAt(i);
                    string companyName = modelRow.CompanyTitle;

                    var res = modelRow.PortfoShareholderViewModels.Where(woak => !woak.IsDeleted && woak.show).OrderByDescending(woak => woak.IsMain);
                    currGrp = res.Where(woak => !woak.IsDeleted && !woak.IsMain && woak.show).Count();
                    if (res == null || res.Count() == 0)
                        continue;

                    //int shareHolderId = res.Where(e => e.IsMain).FirstOrDefault().ShareholderId;
                    if (i > 0)
                    {
                        int res_int = model.ElementAt(i - 1).PortfoShareholderViewModels.Where(woak => !woak.IsDeleted && !woak.IsMain && woak.show).Count();
                        if (res_int > 0)
                        {
                            if (res_int == 0) 
                                ++res_int;
                            prevGrp += res_int;
                        }                            
                        else
                            prevGrp++;
                    }

                    string subshareholders = string.Empty;
                    string values = string.Empty;
                    int rowNum = 0;
                    keepRow++;
                    foreach (var item in res)
                    {
                        IRow row = sheet.CreateRow(k);
                        if (!item.IsMain)
                        {
                            k++;
                            rowNum++;
                        }
                        DataFormatter da = new DataFormatter();
                        ICell ic; ICellStyle st;



                        string titleMain = res.Where(e => e.IsMain == true).FirstOrDefault() == null ? "-" : res.Where(e => e.IsMain == true).FirstOrDefault().Title;

                        row.CreateCell(0).SetCellValue(keepRow);
                        //if (shareholder != null)
                        row.CreateCell(1).SetCellValue(titleMain);
                        row.CreateCell(2).SetCellValue(modelRow.CompanyTitle == string.Empty ? "-" : modelRow.CompanyTitle);
                        var mainObject = res.Where(e => e.IsMain == true).FirstOrDefault();
                        double mainShare = res.Where(e => e.IsMain == true).FirstOrDefault() == null ? 0 : res.Where(e => e.IsMain == true).FirstOrDefault().Value;
                        if (mainObject == null)
                        {
                            if (!string.IsNullOrEmpty(item.Title))
                                row.CreateCell(11).SetCellValue(item.Title);

                            row.CreateCell(12).SetCellValue(item.Value);
                            continue;
                        }
                        row.CreateCell(3).SetCellValue(String.Format("{0:n0}", modelRow.Shares));

                        row.CreateCell(4).SetCellValue(mainShare);

                        //aggregate percentage
                        row.CreateCell(5).SetCellValue(String.Format("{0:.##}", item.Column * 100));//item.Column

                        //Communication
                        row.CreateCell(6).SetCellValue(modelRow.ShareholderHoldingCommunication ? "*" : "");
                        row.CreateCell(7).SetCellValue(modelRow.UnderManagementHoldingCommunication ? "*" : "");

                        //Invest type
                        row.CreateCell(8).SetCellValue(modelRow.CompanyTypeTitle);

                        //CurrentCapital
                        row.CreateCell(9).SetCellValue(String.Format("{0:n0}", modelRow.CurrentCapital / 1000000d));

                        //AgentsNumber
                        row.CreateCell(10).SetCellValue(modelRow.AgentsNumber);
                        if (item.IsMain)
                        {
                            row.CreateCell(11).SetCellValue("-");
                            row.CreateCell(12).SetCellValue("-");
                        }
                        else
                        {
                            row.CreateCell(11).SetCellValue(item.Title);
                            row.CreateCell(12).SetCellValue(item.Value);
                        }

                    }

                    for (int j = 0; j < 11; j++)
                    {
                        int fr = 0;
                        int lr = 0;
                        if (i == 0)
                        {
                            if (rowNum == 0)
                            {
                                rowNum++;
                                k++;
                            }
                            if (currGrp == 0) currGrp+=1;
                            fr = 2;
                            lr = currGrp + 1;
                        }
                        else
                        {
                            if (rowNum == 0)
                            {
                                rowNum++;
                                k++;
                            }
                            if (currGrp == 0)
                            { 
                                currGrp++;
                            }
                            fr = prevGrp + 2;
                            lr = fr + currGrp - 1;
                        }

                        if (lr < fr)
                            break;
                        var cra = new NPOI.SS.Util.CellRangeAddress(fr, lr, j, j);
                        sheet.AddMergedRegion(cra);
                    }

                }


                wb.Write(stream);

                var bytes = System.IO.File.ReadAllBytes(hosting.WebRootPath + filename);
                System.IO.File.Delete(hosting.WebRootPath + filename);
                var fileResult = File(bytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"portfo.xlsx");
                return fileResult;

            }
        }

        public async Task ReCalculatePortfo()
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
                await _portfoRepository.UpdateAsync(portfo);
            }

        }
        [HttpPost(Name = "savePortfo")]
        public async Task<IActionResult> SavePortfo(PortfoValues tag)
        {
            int insert = 9;
            if (tag.Sel1 == null || tag.Group < 1) return BadRequest();
            if (tag.Sel1 != null)
            {
                var item1 = new PortfoShareholderPair
                {
                    PortfoId = tag.Id,
                    AddedDate = DateTime.Now,
                    CompanyId = int.Parse(tag.Sel1.Split('-')[0]),
                    ShareholderId = int.Parse(tag.Sel1.Split('-')[1]),
                    Row = tag.Group,
                    Column = 0,
                };
                insert = await _portfoShareholderPairRepository.InsertAsync(item1);
                if (insert != 1)
                    return Ok("E");
            }
            return Ok(getPortfo2(tag.Id));
        }
        [HttpGet(Name = "getPortfo")]
        public IActionResult GetPortfo(Tag tag)
        {
            return Ok(getPortfo2(tag.Id));
        }
        private PortfoViewModel getPortfo(int id)
        {
            var item = _portfoRepository.GetAsQueryable(x => x.Id == id, includeProperties: "Company.CompanyType,PortfoShareholderPairs").FirstOrDefault();
            PortfoViewModel model = _mapper.Map<Portfo, PortfoViewModel>(item);

            model.CurrentCapital = _companyShareRepository.GetAsQueryable(x => x.CompanyId == item.CompanyId)
                .OrderByDescending(x => x.Id).Select(x => x.CurrentCapital).FirstOrDefault();
            model.CompanyTypeTitle = item.Company.CompanyType.Title;

            //&& x.ShareholderId == 47 && x.CompanyId == 19
            //var shareholderList = _companyShareholderRepository.GetAsQueryable(x => x.IncludeInFormula).GroupBy(woak => new { woak.CompanyId, woak.ShareholderId }).Select(g => g.OrderByDescending(c => c.Id).FirstOrDefault()).ToList();

            var shareholderList = _companyShareholderRepository.GetAsQueryableGroupBy(woak => new { woak.CompanyId, woak.ShareholderId }, x => x.IncludeInFormula).Select(g => g.OrderByDescending(c => c.Id).FirstOrDefault()).ToList();

            var allPossibleShareholders = shareholderList.Join(_companyRepository.GetAll(), a => a.CompanyId, b => b.Id, (a, b) => new { companiesShare = a, companies = b }).Join(_shareholderRepository.GetAll(), a => a.companiesShare.ShareholderId, b => b.Id, (a, b) => new { companiesShare2 = a, shares = b }).ToList();


            List<CompanyShareholderViewModel> fakes = new List<CompanyShareholderViewModel>();
            foreach (var woak in allPossibleShareholders)
            {
                //x => x.CompanyId == item.CompanyId && x.FormStatusFilter == KavoshFrameWorkCore.FormStatus.Verified).OrderByDescending(x => x.Date)
                var companyShare = _companyShareRepository
                    .GetAsQueryable(x => x.CompanyId == woak.companiesShare2.companiesShare.CompanyId && x.FormStatus == KavoshFrameWorkCore.FormStatus.Verified).OrderByDescending(x => x.Date).FirstOrDefault();
                if (companyShare != null)
                {
                    var capitalChangePercentage = 100 * ((companyShare.CurrentCapital - companyShare.PreviousCapital) / (double)companyShare.PreviousCapital);
                    var totalShare = companyShare.PreviousShares * (1 + (capitalChangePercentage) / 100);
                    var ownershipPercentage = (woak.companiesShare2.companiesShare.Shares / totalShare) * 100;
                    var sh = new CompanyShareholderViewModel
                    {
                        CompanyId = woak.companiesShare2.companiesShare.CompanyId,
                        CompanyTitle = woak.companiesShare2.companies.Title,
                        ShareholderTitle = woak.shares.Title,
                        IsMainShareholder = woak.companiesShare2.companiesShare.IsMainShareholder,
                        ShareholderId = woak.companiesShare2.companiesShare.ShareholderId,
                        OwnershipPercentage = ownershipPercentage,
                    };
                    fakes.Add(sh);
                }
            }

            var items = new List<KeyValueViewModel>();

            foreach (var fake in fakes)
            {
                items.Add(new KeyValueViewModel { Key = fake.CompanyId + "-" + fake.ShareholderId, Value = fake.ShareholderTitle + " در " + fake.CompanyTitle + " (" + Math.Round((fake.OwnershipPercentage / 100), 2) + ")" });
            }

            model.ValidShareholderTreeNodes = items;
            return model;
        }
        private List<PortfoResult> getPortfo2(int id)
        {
            var item = _portfoRepository.GetAsQueryable(x => x.Id == id, includeProperties: "Company.CompanyType,PortfoShareholderPairs").FirstOrDefault();
            PortfoViewModel model = _mapper.Map<Portfo, PortfoViewModel>(item);

            model.CurrentCapital = _companyShareRepository.GetAsQueryable(x => x.CompanyId == item.CompanyId)
                .OrderByDescending(x => x.Id).Select(x => x.CurrentCapital).FirstOrDefault();
            model.CompanyTypeTitle = item.Company.CompanyType.Title;


            //var shareholderList = _companyShareholderRepository.GetAsQueryable(x => x.IncludeInFormula).GroupBy(woak => new { woak.CompanyId, woak.ShareholderId }).Select(g => g.OrderByDescending(c => c.Id).FirstOrDefault()).ToList();

            var shareholderList = _companyShareholderRepository.GetAsQueryableGroupBy(woak => new { woak.CompanyId, woak.ShareholderId }, x => x.IncludeInFormula).Select(g => g.OrderByDescending(c => c.Id).FirstOrDefault()).ToList();


            var companies = _companyRepository.GetAll();

            var shareholders = _shareholderRepository.GetAll();

            var portfoShareholders = _portfoShareholderPairRepository.GetAsQueryable(woak => woak.PortfoId == id).OrderBy(p => p.Row).ToList();


            var allPossibleShareholders = portfoShareholders.Join(shareholderList, a => new { a.CompanyId, a.ShareholderId }, b => new { b.CompanyId, b.ShareholderId }, (a, b) => new { portfo = a, companiesShare = b }).Join(companies, a => a.companiesShare.CompanyId, b => b.Id, (a, b) => new { companiesShare = a, companies = b }).Join(shareholders, a => a.companiesShare.companiesShare.ShareholderId, b => b.Id, (a, b) => new { companiesShare2 = a, shares = b }).ToList();




            List<CompanyShareholderViewModel> fakes = new List<CompanyShareholderViewModel>();
            foreach (var woak in allPossibleShareholders)
            {
                var companyShare = _companyShareRepository
                    .GetAsQueryable(x => x.CompanyId == woak.companiesShare2.companiesShare.companiesShare.CompanyId && x.FormStatus == KavoshFrameWorkCore.FormStatus.Verified).OrderByDescending(x => x.Date).FirstOrDefault();
                if (companyShare != null)
                {
                    var capitalChangePercentage = 100 * ((companyShare.CurrentCapital - companyShare.PreviousCapital) / (double)companyShare.PreviousCapital);
                    var totalShare = companyShare.PreviousShares * (1 + (capitalChangePercentage) / 100);
                    var ownershipPercentage = (woak.companiesShare2.companiesShare.companiesShare.Shares / totalShare);
                    var sh = new CompanyShareholderViewModel
                    {
                        Id = woak.companiesShare2.companiesShare.portfo.Id,
                        CompanyId = woak.companiesShare2.companiesShare.companiesShare.CompanyId,
                        CompanyTitle = woak.companiesShare2.companies.Title,
                        ShareholderTitle = woak.shares.Title,
                        IsMainShareholder = woak.companiesShare2.companiesShare.companiesShare.IsMainShareholder,
                        ShareholderId = woak.companiesShare2.companiesShare.companiesShare.ShareholderId,
                        OwnershipPercentage = ownershipPercentage,
                        Row = woak.companiesShare2.companiesShare.portfo.Row
                    };
                    fakes.Add(sh);
                }
            }

            List<PortfoResult> bitPush = new List<PortfoResult>();

            double total = 0;
            IEnumerable<IGrouping<int, CompanyShareholderViewModel>> group = fakes.GroupBy(woak => woak.Row);
            foreach (IGrouping<int, CompanyShareholderViewModel> woak in group)
            {
                int row = woak.Key;
                string id1 = string.Empty;
                string title = string.Empty;
                string value = string.Empty;
                double sum = 1;

                foreach (var put in woak)
                {
                    id1 += put.Id;
                    string strVal = String.Format("{0:.##}", (put.OwnershipPercentage * 100));
                    title += put.ShareholderTitle + " در " + put.CompanyTitle + "(" + strVal + ")>> " + "<a class='btn btn-sm btn-danger' onclick='deletePortfo2(" + put.Id + ")'>x</a>" + "  ";
                    value += Math.Round((put.OwnershipPercentage / 100), 2).ToString() + "-";
                    sum *= (put.OwnershipPercentage);

                }
                id1 = id1.Remove(id1.Length - 1);
                //title = title.Remove(title.Length - 1);2345
                value = value.Remove(value.Length - 1);

                total += sum;
                string strSum = String.Format("{0:.##}", sum * 100);
                string strTotla = String.Format("{0:.##}", total);
                bitPush.Add(new PortfoResult { Id = id1, Title = title, Value = value, Sum = strSum == "" ? "0" : strSum, Total = strTotla, Row = row });
            }
            item.AggregatePercentage = total;
            int update = _portfoRepository.Update(item);
            return bitPush;
        }

        [HttpPost]
        public async Task<IActionResult> DeletePortfo(Tag tag)
        {
            var portfoId = HttpContext.Session.GetString("portfoId");
            IEnumerable<PortfoShareholderPair> items = await _portfoShareholderPairRepository.GetAsync(woak => woak.Row == tag.Id && woak.PortfoId == Convert.ToInt32(portfoId));
            foreach (PortfoShareholderPair woak in items)
                await _portfoShareholderPairRepository.RemoveAsync(woak.Id);
            ErrorMessage = Resources.Messages.ChangesSavedSuccessfully;
            return Ok(getPortfo2(tag.EnactmentId));
        }
        [HttpPost]
        public async Task<IActionResult> DeletePortfo2(Tag tag)
        {
            await _portfoShareholderPairRepository.RemoveAsync(tag.Id);
            ErrorMessage = Resources.Messages.ChangesSavedSuccessfully;
            return Ok(getPortfo2(tag.EnactmentId));
        }

        private void RunStoreProcedure()
        {
            var companies = _companyShareholderRepository.GetAsQueryable(woak => !woak.IsDeleted && woak.IncludeInFormula).Select(woak => woak.CompanyId).Distinct();
            string strCnn = Configuration.GetConnectionString("DefaultConnection");
            SqlCommand cmd;
            SqlConnection sqlConn = new SqlConnection(strCnn);

            foreach (var item in companies)
            {
                cmd = new SqlCommand();
                cmd.Connection = sqlConn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_CalculatePortfo";
                cmd.Parameters.Add("@CompanyID", System.Data.SqlDbType.Int).Value = item;
                if (sqlConn.State == ConnectionState.Closed)
                    sqlConn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
    public class PortfoValues
    {
        public int Id { get; set; }
        public string Sel1 { get; set; }
        public int Group { get; set; }
    }
    public class PortfoResult
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Value { get; set; }
        public int Row { get; set; }
        public string Sum { get; set; }
        public string Total { get; set; }
    }
    public class Tag2
    {
        public string Id { get; set; }
        public int EnactmentId { get; set; }
    }
}
