using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using KavoshFrameWorkData.Repositories.Generic;
using KavoshFrameWorkCore.Models;
using KavoshFrameWorkWebApplication.Models;
using KavoshFrameWorkCommon.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Globalization;

namespace KavoshFrameWorkWebApplication.Controllers
{
    public class ReportController : BaseController
    {
        IMapper _mapper;
        private readonly IGenericRepository<Company> company;
        IGenericRepository<CompanyBoardMember> _companyBoardMemberRepository;
        IGenericRepository<CompanyShare> _companyShareRepository;
        IGenericRepository<BoardofDirectorsLegalMember> _boardofDirectorsRepository;
        IGenericRepository<Agent> _agentRepository;
        IGenericRepository<CompanyShareholder> _companyShareholderRepository;
        IGenericRepository<CompanyAnnualMeeting> _companyAnnualRepository;
        private readonly KavoshFrameWorkCore.KavoshFrameWorkContext _context;
        public ReportController(IGenericRepository<CompanyBoardMember> companyBoardMemberRepository, IMapper mapper,
             UserManager<ApplicationUser> userManager,
        IGenericRepository<Company> company, IGenericRepository<CompanyShare> companyShareRepository,
        IGenericRepository<BoardofDirectorsLegalMember> boardofDirectorsRepository,
        IGenericRepository<CompanyUser> companyUserRepository, IGenericRepository<Agent> agentRepository,
        IGenericRepository<CompanyShareholder> companyShareholderRepository, IGenericRepository<CompanyAnnualMeeting> companyAnnualRepository, KavoshFrameWorkCore.KavoshFrameWorkContext context, IConfiguration configuration)
        {
            _companyBoardMemberRepository = companyBoardMemberRepository;
            _mapper = mapper;
            this.company = company;
            this._userManager = userManager;
            this._companyUserRepository = companyUserRepository;
            _companyShareRepository = companyShareRepository;
            _boardofDirectorsRepository = boardofDirectorsRepository;
            _agentRepository = agentRepository;
            _companyShareholderRepository = companyShareholderRepository;
            _companyAnnualRepository = companyAnnualRepository;

            this._context = context;
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        //CompanyBoardMember
        [HttpGet(Name = "CompanyBoardMember")]
        public IActionResult CompanyBoardMember()
        {
            ViewBag.JalaliFromDate = "0";
            ViewBag.JalaliToDate = "0";
            ViewBag.FromDate = null;
            ViewBag.ToDate = null;
            var items = _companyBoardMemberRepository.GetAsQueryable(x => HasAccess(x.CompanyId), includeProperties: "BoardofDirectorsLegalMember,AgentCompany,LegalMemberType,OrganizationalPosition,Company,Agent").ToList();
            var model = _mapper.Map<IEnumerable<CompanyBoardMember>, IEnumerable<CompanyBoardMemberViewModel>>(items);
            return View(model);
        }
        [HttpPost(Name = "CompanyBoardMember")]
        public IActionResult CompanyBoardMember(CompanyBoardMemberViewModel model)
        {
            if (!string.IsNullOrEmpty(model.JalaliFromDate) && !string.IsNullOrEmpty(model.JalaliToDate))
            {
                DateTime? fromDate = model.JalaliFromDate.ToDateTime();
                DateTime? toDate = model.JalaliToDate.ToDateTime();

                ViewBag.JalaliFromDate = model.JalaliFromDate;
                ViewBag.JalaliToDate = model.JalaliToDate;
                ViewBag.FromDate = model.JalaliFromDate.ToDateTime();
                ViewBag.ToDate = model.JalaliToDate.ToDateTime();

                var items = _companyBoardMemberRepository.GetAsQueryable(x => HasAccess(x.CompanyId) && (x.AppointmentDate >= fromDate && x.AppointmentDate <= toDate), includeProperties: "BoardofDirectorsLegalMember,AgentCompany,LegalMemberType,OrganizationalPosition,Company,Agent").ToList();
                var res = _mapper.Map<IEnumerable<CompanyBoardMember>, IEnumerable<CompanyBoardMemberViewModel>>(items);
                return View(res);
            }
            return RedirectToAction("CompanyBoardMember");
        }
  

        [HttpGet(Name = "CompanyBoardMemberDays")]
        public IActionResult CompanyBoardMemberDays()
        {
            ViewBag.JalaliFromDate = "0";
            ViewBag.JalaliToDate = "0";
            ViewBag.FromDate = null;
            ViewBag.ToDate = null;
            var items = _companyBoardMemberRepository.GetAsQueryable(x => HasAccess(x.CompanyId), includeProperties: "BoardofDirectorsLegalMember,AgentCompany,LegalMemberType,OrganizationalPosition,Company,Agent").ToList();
            var model = _mapper.Map<IEnumerable<CompanyBoardMember>, IEnumerable<CompanyBoardMemberViewModel>>(items);
            return View(model);
        }
        [HttpPost(Name = "CompanyBoardMemberDays")]
        public IActionResult CompanyBoardMemberDays(CompanyBoardMemberViewModel model)
        {
            if (!string.IsNullOrEmpty(model.JalaliFromDate) && !string.IsNullOrEmpty(model.JalaliToDate))
            {
                DateTime? fromDate = model.JalaliFromDate.ToDateTime();
                DateTime? toDate = model.JalaliToDate.ToDateTime();

                ViewBag.JalaliFromDate = model.JalaliFromDate;
                ViewBag.JalaliToDate = model.JalaliToDate;
                ViewBag.FromDate = model.JalaliFromDate.ToDateTime();
                ViewBag.ToDate = model.JalaliToDate.ToDateTime();

                var items = _companyBoardMemberRepository.GetAsQueryable(x => HasAccess(x.CompanyId) && (x.AppointmentDate >= fromDate && x.AppointmentDate <= toDate), includeProperties: "BoardofDirectorsLegalMember,AgentCompany,LegalMemberType,OrganizationalPosition,Company,Agent").ToList();
                var res = _mapper.Map<IEnumerable<CompanyBoardMember>, IEnumerable<CompanyBoardMemberViewModel>>(items);
                return View(res);
            }
            return RedirectToAction("CompanyBoardMemberDays");
        }

        [HttpGet(Name = "CompanyShare")]
        public IActionResult CompanyShare()
        {
            ViewBag.JalaliFromDate = "0";
            ViewBag.JalaliToDate = "0";
            ViewBag.FromDate = null;
            ViewBag.ToDate = null;
            var items = _companyShareRepository.GetAsQueryable(x => HasAccess(x.CompanyId) && (x.CurrentCapital != x.PreviousCapital), includeProperties: "Company").ToList();

            var model = _mapper.Map<IEnumerable<CompanyShare>, IEnumerable<CompanyShareViewModel>>(items);

            return View(model);
        }
        [HttpPost(Name = "CompanyShare")]
        public IActionResult CompanyShare(CompanyShareViewModel model)
        {
            if (!string.IsNullOrEmpty(model.JalaliFromDate) && !string.IsNullOrEmpty(model.JalaliToDate))
            {
                DateTime? fromDate = model.JalaliFromDate.ToDateTime();
                DateTime? toDate = model.JalaliToDate.ToDateTime();

                ViewBag.JalaliFromDate = model.JalaliFromDate;
                ViewBag.JalaliToDate = model.JalaliToDate;
                ViewBag.FromDate = model.JalaliFromDate.ToDateTime();
                ViewBag.ToDate = model.JalaliToDate.ToDateTime();

                var items = _companyShareRepository.GetAsQueryable(x => HasAccess(x.CompanyId) && (x.CurrentCapital != x.PreviousCapital) && (x.Date >= fromDate && x.Date <= toDate), includeProperties: "Company").ToList();

                var res = _mapper.Map<IEnumerable<CompanyShare>, IEnumerable<CompanyShareViewModel>>(items);
                return View(res);
            }
            return RedirectToAction("CompanyShare");
        }



        [HttpGet(Name = "CompanyBoardMemberEx")]
        public IActionResult CompanyBoardMemberEx()
        {
            ViewBag.JalaliFromDate = "0";
            ViewBag.JalaliToDate = "0";
            ViewBag.FromDate = null;
            ViewBag.ToDate = null;
            var itemsBoardMember = _companyBoardMemberRepository.GetAsQueryable(x => HasAccess(x.CompanyId), includeProperties: "BoardofDirectorsLegalMember,Company,Agent").Join(_boardofDirectorsRepository.GetAsQueryable(), a => a.BoardofDirectorsLegalMemberId, b => b.Id, (a, b) => new { boardMembers = a, boardofDirectors = b }).Where(x => x.boardMembers.IsActive == false).ToList();

            var itemsAgent = _companyBoardMemberRepository.GetAsQueryable(x => HasAccess(x.CompanyId)).Join(_agentRepository.GetAsQueryable(), a => a.AgentId, b => b.Id, (a, b) => new { boardMembers = a, agent = b }).Where(x => x.boardMembers.IsActive == false).ToList();

            var items = _companyBoardMemberRepository.GetAsQueryable(x => HasAccess(x.CompanyId) && x.IsDeleted == false && x.FormStatus == KavoshFrameWorkCore.FormStatus.Verified).Join(itemsBoardMember, a => new { a.CompanyId, a.OrganizationalPositionId }, b => new { b.boardMembers.CompanyId, b.boardMembers.OrganizationalPositionId }, (a, b) => new { boardMembers = a, boardMembersDirect = b }).Join(itemsAgent, a => new { a.boardMembers.CompanyId, a.boardMembers.OrganizationalPositionId }, b => new { b.boardMembers.CompanyId, b.boardMembers.OrganizationalPositionId }, (a, b) => new { boardMembers2 = a, boardMembersAgent = b });

            IEnumerable<CompanyBoardMemberExViewModel> result = items.Select(woak => new CompanyBoardMemberExViewModel
            {
                CompanyTitle = woak.boardMembers2.boardMembers.Company.Title,
                AgentTitleOld = woak.boardMembersAgent.agent.Title,
                BoardofDirectorsLegalMemberTitleOld = woak.boardMembers2.boardMembersDirect.boardofDirectors.Title,
                AgentTitle = woak.boardMembers2.boardMembers.Agent.Title,
                BoardofDirectorsLegalMemberTitle = woak.boardMembers2.boardMembers.BoardofDirectorsLegalMember.Title,
                AppointmentDate = woak.boardMembers2.boardMembers.AppointmentDate
            });
            return View(result);
        }
        [HttpPost(Name = "CompanyBoardMemberEx")]
        public IActionResult CompanyBoardMemberEx(CompanyBoardMemberViewModel model)
        {
            if (!string.IsNullOrEmpty(model.JalaliFromDate) && !string.IsNullOrEmpty(model.JalaliToDate))
            {
                DateTime? fromDate = model.JalaliFromDate.ToDateTime();
                DateTime? toDate = model.JalaliToDate.ToDateTime();

                ViewBag.JalaliFromDate = model.JalaliFromDate;
                ViewBag.JalaliToDate = model.JalaliToDate;
                ViewBag.FromDate = model.JalaliFromDate.ToDateTime();
                ViewBag.ToDate = model.JalaliToDate.ToDateTime();

                var itemsBoardMember = _companyBoardMemberRepository.GetAsQueryable(x => HasAccess(x.CompanyId), includeProperties: "BoardofDirectorsLegalMember,Company,Agent").Join(_boardofDirectorsRepository.GetAsQueryable(), a => a.BoardofDirectorsLegalMemberId, b => b.Id, (a, b) => new { boardMembers = a, boardofDirectors = b }).Where(x => x.boardMembers.IsActive == false).ToList();

                var itemsAgent = _companyBoardMemberRepository.GetAsQueryable(x => HasAccess(x.CompanyId)).Join(_agentRepository.GetAsQueryable(), a => a.AgentId, b => b.Id, (a, b) => new { boardMembers = a, agent = b }).Where(x => x.boardMembers.IsActive == false).ToList();

                var items = _companyBoardMemberRepository.GetAsQueryable(x => HasAccess(x.CompanyId) && x.IsDeleted == false && x.FormStatus == KavoshFrameWorkCore.FormStatus.Verified && (x.AppointmentDate >= fromDate && x.AppointmentDate <= toDate)).Join(itemsBoardMember, a => new { a.CompanyId, a.OrganizationalPositionId }, b => new { b.boardMembers.CompanyId, b.boardMembers.OrganizationalPositionId }, (a, b) => new { boardMembers = a, boardMembersDirect = b }).Join(itemsAgent, a => new { a.boardMembers.CompanyId, a.boardMembers.OrganizationalPositionId }, b => new { b.boardMembers.CompanyId, b.boardMembers.OrganizationalPositionId }, (a, b) => new { boardMembers2 = a, boardMembersAgent = b });

                IEnumerable<CompanyBoardMemberExViewModel> result = items.Select(woak => new CompanyBoardMemberExViewModel
                {
                    CompanyTitle = woak.boardMembers2.boardMembers.Company.Title,
                    AgentTitleOld = woak.boardMembersAgent.agent.Title,
                    BoardofDirectorsLegalMemberTitleOld = woak.boardMembers2.boardMembersDirect.boardofDirectors.Title,
                    AgentTitle = woak.boardMembers2.boardMembers.Agent.Title,
                    BoardofDirectorsLegalMemberTitle = woak.boardMembers2.boardMembers.BoardofDirectorsLegalMember.Title,
                    AppointmentDate = woak.boardMembers2.boardMembers.AppointmentDate

                });
                return View(result);
            }
            return RedirectToAction("CompanyBoardMemberEx");
        }

        [HttpGet(Name = "CompanyShareHolder")]
        public IActionResult CompanyShareHolder()
        {

            ViewBag.MonthList = new List<SelectListItem>()
            {
                new SelectListItem { Value = "1", Text = "فروردین" },
                new SelectListItem { Value = "2", Text = "اردیبهشت" },
                new SelectListItem { Value = "3", Text = "خرداد" },
                new SelectListItem { Value = "4", Text = "تیر" },
                new SelectListItem { Value = "5", Text = "مرداد" },
                new SelectListItem { Value = "6", Text = "شهریور" },
                new SelectListItem { Value = "7", Text = "مهر" },
                new SelectListItem { Value = "8", Text = "آبان" },
                new SelectListItem { Value = "9", Text = "آذر" },
                new SelectListItem { Value = "10", Text = "دی" },
                new SelectListItem { Value = "11", Text = "بهمن" },
                new SelectListItem { Value = "12", Text = "اسفند" },
            };
            ViewBag.Year = 0;

            var items = _companyShareholderRepository.GetAsQueryable(x => HasAccess(x.CompanyId) && x.ShareholderId == 47, includeProperties: "Shareholder,Company").OrderByDescending(w => w.Year).ThenByDescending(x => x.Month).OrderByDescending(x => x.Year).ThenByDescending(x => x.Month).GroupBy(x => new { x.CompanyId, x.ShareholderId }).ToList();

            List<CompanyShareholder> result = new List<CompanyShareholder>();
            foreach (var woak in items)
            {
                foreach (var pick in woak)
                {
                    result.Add(pick);
                    break;
                }
            }
            var model = _mapper.Map<IEnumerable<CompanyShareholder>, IEnumerable<CompanyShareholderViewModel>>(result);

            var companyShares = _companyShareRepository.GetAsQueryable().Select(x => new CompanyShareViewModel
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
        [HttpPost(Name = "CompanyShareHolder")]
        public IActionResult CompanyShareHolder(ReportViewModel model)
        {
            ViewBag.MonthList = new List<SelectListItem>()
            {
                new SelectListItem { Value = "1", Text = "فروردین" },
                new SelectListItem { Value = "2", Text = "اردیبهشت" },
                new SelectListItem { Value = "3", Text = "خرداد" },
                new SelectListItem { Value = "4", Text = "تیر" },
                new SelectListItem { Value = "5", Text = "مرداد" },
                new SelectListItem { Value = "6", Text = "شهریور" },
                new SelectListItem { Value = "7", Text = "مهر" },
                new SelectListItem { Value = "8", Text = "آبان" },
                new SelectListItem { Value = "9", Text = "آذر" },
                new SelectListItem { Value = "10", Text = "دی" },
                new SelectListItem { Value = "11", Text = "بهمن" },
                new SelectListItem { Value = "12", Text = "اسفند" },
            };
            ViewBag.Year = model.Year;
            if (!string.IsNullOrEmpty(model.Month) && model.Year > 0)
            {
                int month = Convert.ToInt32(model.Month);
                int year = model.Year;

                var items = _companyShareholderRepository.GetAsQueryable(x => HasAccess(x.CompanyId) && x.ShareholderId == 47 && (x.Month <= month && x.Year <= year), includeProperties: "Shareholder,Company").OrderByDescending(w => w.Year).ThenByDescending(x => x.Month).OrderByDescending(x => x.Year).ThenByDescending(x => x.Month).GroupBy(x => new { x.CompanyId, x.ShareholderId }).ToList();

                List<CompanyShareholder> result = new List<CompanyShareholder>();
                foreach (var woak in items)
                {
                    foreach (var pick in woak)
                    {
                        result.Add(pick);
                        break;
                    }
                }

                var models = _mapper.Map<IEnumerable<CompanyShareholder>, IEnumerable<CompanyShareholderViewModel>>(result);

                var companyShares = _companyShareRepository.GetAsQueryable().Select(x => new CompanyShareViewModel
                {
                    CompanyId = x.CompanyId,
                    PreviousShares = x.PreviousShares,
                    PreviousCapital = x.PreviousCapital,
                    CurrentCapital = x.CurrentCapital,
                    FormStatusFilter = x.FormStatus,
                    Date = x.Date
                }).ToList();

                foreach (var item in models)
                {
                    var companyShare = companyShares.Where(x => x.CompanyId == item.CompanyId && x.FormStatusFilter == KavoshFrameWorkCore.FormStatus.Verified).OrderByDescending(x => x.Date).FirstOrDefault();
                    if (companyShare != null)
                    {
                        var capitalChangePercentage = 100 * ((companyShare.CurrentCapital - companyShare.PreviousCapital) / (double)companyShare.PreviousCapital);
                        var totalShare = companyShare.PreviousShares * (1 + (capitalChangePercentage) / 100);
                        item.OwnershipPercentage = (item.Shares / totalShare) * 100;
                    }
                }

                return View(models);
            }
            return RedirectToAction("CompanyShareHolder");
        }


        [HttpGet(Name = "CompanyAnnualMeeting")]
        public IActionResult CompanyAnnualMeeting()
        {
            ViewBag.JalaliFromDate = "0";
            ViewBag.JalaliToDate = "0";
            ViewBag.FromDate = null;
            ViewBag.ToDate = null;
            var items = _companyAnnualRepository.GetAsQueryable(x => HasAccess(x.CompanyId), includeProperties: "Company").Join(_companyShareRepository.GetAsQueryable(), a => a.CompanyId, b => b.CompanyId, (a, b) => new { meeting = a, companyShare = b }).ToList();

            IEnumerable<CompanyAnnualMeetingViewModel> models = items.Select(woak => new CompanyAnnualMeetingViewModel
            {
                CompanyTitle = woak.meeting.Company.Title,
                CurrentCapital = woak.companyShare.CurrentShares,
                HoldingShares = woak.meeting.HoldingShares,
                DividedProfit = woak.meeting.DividedProfit,
                TotalProfit = woak.meeting.HoldingShares * woak.meeting.DividedProfit,
                Date = woak.meeting.Date
            });
            return View(models);
        }
        [HttpPost(Name = "CompanyAnnualMeeting")]
        public IActionResult CompanyAnnualMeeting(CompanyShareViewModel model)
        {
            if (!string.IsNullOrEmpty(model.JalaliFromDate) && !string.IsNullOrEmpty(model.JalaliToDate))
            {
                DateTime? fromDate = model.JalaliFromDate.ToDateTime();
                DateTime? toDate = model.JalaliToDate.ToDateTime();

                ViewBag.JalaliFromDate = model.JalaliFromDate;
                ViewBag.JalaliToDate = model.JalaliToDate;
                ViewBag.FromDate = model.JalaliFromDate.ToDateTime();
                ViewBag.ToDate = model.JalaliToDate.ToDateTime();

                var items = _companyAnnualRepository.GetAsQueryable(x => HasAccess(x.CompanyId) && (x.Date >= fromDate && x.Date <= toDate), includeProperties: "Company").Join(_companyShareRepository.GetAsQueryable(), a => a.CompanyId, b => b.CompanyId, (a, b) => new { meeting = a, companyShare = b }).ToList();

                IEnumerable<CompanyAnnualMeetingViewModel> models = items.Select(woak => new CompanyAnnualMeetingViewModel
                {
                    CompanyTitle = woak.meeting.Company.Title,
                    CurrentCapital = woak.companyShare.CurrentShares,
                    HoldingShares = woak.meeting.HoldingShares,
                    DividedProfit = woak.meeting.DividedProfit,
                    TotalProfit = woak.meeting.HoldingShares * woak.meeting.DividedProfit,
                    Date = woak.meeting.Date
                });
                return View(models);
            }
            return RedirectToAction("CompanyAnnualMeeting");
        }


        [HttpGet(Name = "AnnualMeetingPerCompany")]
        public IActionResult AnnualMeetingPerCompany()
        {
            ViewBag.JalaliFromDate = "0";
            ViewBag.JalaliToDate = "0";
            ViewBag.FromDate = null;
            ViewBag.ToDate = null;
            var items = _companyAnnualRepository.GetAsQueryable(x => HasAccess(x.CompanyId), includeProperties: "Company,PrimaryAuditor,SecondaryAuditor").ToList();

            IEnumerable<CompanyAnnualMeetingViewModel> models = items.Select(woak => new CompanyAnnualMeetingViewModel
            {
                CompanyTitle = woak.Company.Title,
                Date = woak.Date,
                HoldingShares = woak.HoldingShares,
                DividedProfit = woak.DividedProfit,
                BoardBonus = woak.BoardBonus,
                PrimaryAuditorTitle = woak.PrimaryAuditor.Title,
                SecondaryAuditorTitle = woak.SecondaryAuditor.Title,
                BoardRight = woak.BoardRight,
                SocialResponsibility = (double)woak.SocialResponsibility
            });
            return View(models);
        }
        [HttpPost(Name = "AnnualMeetingPerCompany")]
        public IActionResult AnnualMeetingPerCompany(CompanyShareViewModel model)
        {
            if (!string.IsNullOrEmpty(model.JalaliFromDate) && !string.IsNullOrEmpty(model.JalaliToDate))
            {
                DateTime? fromDate = model.JalaliFromDate.ToDateTime();
                DateTime? toDate = model.JalaliToDate.ToDateTime();

                ViewBag.JalaliFromDate = model.JalaliFromDate;
                ViewBag.JalaliToDate = model.JalaliToDate;
                ViewBag.FromDate = model.JalaliFromDate.ToDateTime();
                ViewBag.ToDate = model.JalaliToDate.ToDateTime();

                var items = _companyAnnualRepository.GetAsQueryable(x => HasAccess(x.CompanyId) && (x.Date >= fromDate && x.Date <= toDate), includeProperties: "Company,PrimaryAuditor,SecondaryAuditor").ToList();

                IEnumerable<CompanyAnnualMeetingViewModel> models = items.Select(woak => new CompanyAnnualMeetingViewModel
                {
                    CompanyTitle = woak.Company.Title,
                    Date = woak.Date,
                    HoldingShares = woak.HoldingShares,
                    DividedProfit = woak.DividedProfit,
                    BoardBonus = woak.BoardBonus,
                    PrimaryAuditorTitle = woak.PrimaryAuditor.Title,
                    SecondaryAuditorTitle = woak.SecondaryAuditor.Title,
                    BoardRight = woak.BoardRight,
                    SocialResponsibility = (double)woak.SocialResponsibility
                });

                return View(models);
            }
            return RedirectToAction("AnnualMeetingPerCompany");
        }


        [HttpGet(Name = "AnnualMeetingOrdinary")]
        public IActionResult AnnualMeetingOrdinary()
        {
            ViewBag.JalaliFromDate = "0";
            ViewBag.JalaliToDate = "0";
            ViewBag.FromDate = null;
            ViewBag.ToDate = null;
            var items = _companyAnnualRepository.GetAsQueryable(x => HasAccess(x.CompanyId), includeProperties: "Company,PrimaryAuditor,SecondaryAuditor,Newspaper").ToList();

            IEnumerable<CompanyAnnualMeetingViewModel> models = items.Select(woak => new CompanyAnnualMeetingViewModel
            {
                CompanyTitle = woak.Company.Title,
                AccountingRight = (double)woak.AccountingRight,
                SocialResponsibility = (double)woak.SocialResponsibility,
                HoldingShares = woak.HoldingShares,
                DividedProfit = woak.DividedProfit,
                BoardBonus = woak.BoardBonus,
                PrimaryAuditorTitle = woak.PrimaryAuditor.Title,
                SecondaryAuditorTitle = woak.SecondaryAuditor.Title,
                BoardRight = woak.BoardRight,
                NewspaperTitle = woak.Newspaper.Title,
            });
            return View(models);
        }
        [HttpPost(Name = "AnnualMeetingOrdinary")]
        public IActionResult AnnualMeetingOrdinary(CompanyShareViewModel model)
        {
            if (!string.IsNullOrEmpty(model.JalaliFromDate) && !string.IsNullOrEmpty(model.JalaliToDate))
            {
                DateTime? fromDate = model.JalaliFromDate.ToDateTime();
                DateTime? toDate = model.JalaliToDate.ToDateTime();

                ViewBag.JalaliFromDate = model.JalaliFromDate;
                ViewBag.JalaliToDate = model.JalaliToDate;
                ViewBag.FromDate = model.JalaliFromDate.ToDateTime();
                ViewBag.ToDate = model.JalaliToDate.ToDateTime();

                var items = _companyAnnualRepository.GetAsQueryable(x => HasAccess(x.CompanyId) && (x.Date >= fromDate && x.Date <= toDate), includeProperties: "Company,PrimaryAuditor,SecondaryAuditor,Newspaper").ToList();

                IEnumerable<CompanyAnnualMeetingViewModel> models = items.Select(woak => new CompanyAnnualMeetingViewModel
                {
                    CompanyTitle = woak.Company.Title,
                    AccountingRight = (double)woak.AccountingRight,
                    SocialResponsibility = (double)woak.SocialResponsibility,
                    HoldingShares = woak.HoldingShares,
                    DividedProfit = woak.DividedProfit,
                    BoardBonus = woak.BoardBonus,
                    PrimaryAuditorTitle = woak.PrimaryAuditor.Title,
                    SecondaryAuditorTitle = woak.SecondaryAuditor.Title,
                    BoardRight = woak.BoardRight,
                    NewspaperTitle = woak.Newspaper.Title,
                });

                return View(models);
            }
            return RedirectToAction("AnnualMeetingOrdinary");
        }


        [HttpGet(Name = "CompanyShareEx")]
        public IActionResult CompanyShareEx()
        {
            ViewBag.JalaliFromDate = "0";
            ViewBag.JalaliToDate = "0";
            ViewBag.FromDate = null;
            ViewBag.ToDate = null;
            //&& x.FormStatus == KavoshFrameWorkCore.FormStatus.Verified
            //IEnumerable<IGrouping<int, CompanyShare>> items = _companyShareRepository.GetAsQueryable(x => HasAccess(x.CompanyId) && (x.CurrentShares != x.PreviousShares), includeProperties: "Company").OrderByDescending(x => x.Date).GroupBy(x => x.CompanyId).ToList();
            string query = string.Empty;
            if (User.IsInRole("Admin"))
            {
                query = @"with cte as (
            SELECT CompanyID,ShareholderId 
            ,(Select Title from [dbo].[Companies] c where c.id=CompanyID) CompanyTitle
            ,(Select Title from [dbo].Shareholders c where c.id=ShareholderId) ShareHolderTitle
            ,shares
            ,ROW_NUMBER() over( partition by CompanyID,ShareholderId order by year,month ) RankShare,
            year,month
              FROM [KavoshFrameWorkWebApplication].[dbo].[CompanyShareholders] P
              where p.IsDeleted=0
            ), cte2 as (
	            Select CompanyID,ShareholderId,CompanyTitle,ShareHolderTitle ,	
	            (Select Year from cte c where c.RankShare=(Select Max(RankShare) from cte cc where cc.CompanyId=cte.CompanyId and cc.ShareholderId=cte.ShareholderId) and c.CompanyId=cte.CompanyId and c.ShareholderId=cte.ShareholderId) as NewYear, 
		            (Select Month from cte c where c.RankShare=(Select Max(RankShare) from cte cc where cc.CompanyId=cte.CompanyId and cc.ShareholderId=cte.ShareholderId) and c.CompanyId=cte.CompanyId and c.ShareholderId=cte.ShareholderId) as NewMonth, 
	            (Select shares from cte c where c.RankShare=(Select Max(RankShare) from cte cc where cc.CompanyId=cte.CompanyId and cc.ShareholderId=cte.ShareholderId) and c.CompanyId=cte.CompanyId and c.ShareholderId=cte.ShareholderId) as NewShare,
	             (Select Year from cte c where c.RankShare=(Select Max(RankShare) from cte cc where cc.CompanyId=cte.CompanyId and cc.ShareholderId=cte.ShareholderId)-1 and c.CompanyId=cte.CompanyId and c.ShareholderId=cte.ShareholderId) as OldYear,
	               (Select Month from cte c where c.RankShare=(Select Max(RankShare) from cte cc where cc.CompanyId=cte.CompanyId and cc.ShareholderId=cte.ShareholderId)-1 and c.CompanyId=cte.CompanyId and c.ShareholderId=cte.ShareholderId) as OldMonth,
	             (Select shares from cte c where c.RankShare=(Select Max(RankShare) from cte cc where cc.CompanyId=cte.CompanyId and cc.ShareholderId=cte.ShareholderId)-1 and c.CompanyId=cte.CompanyId and c.ShareholderId=cte.ShareholderId) as OldShare
	            from cte
		 group by CompanyID,ShareholderId ,CompanyTitle,ShareHolderTitle)
		 Select * from cte2
		  where  
		 OldShare is not null";
            }
            else
            {
                if (!(CompanyIds.Count > 0))
                    return View("Error");
                query = @"with cte as (
            SELECT CompanyID,ShareholderId 
            ,(Select Title from [dbo].[Companies] c where c.id=CompanyID) CompanyTitle
            ,(Select Title from [dbo].Shareholders c where c.id=ShareholderId) ShareHolderTitle
            ,shares
            ,ROW_NUMBER() over( partition by CompanyID,ShareholderId order by year,month ) RankShare,
            year,month
              FROM [KavoshFrameWorkWebApplication].[dbo].[CompanyShareholders] P
              where p.IsDeleted=0
            ), cte2 as (
	            Select CompanyID,ShareholderId,CompanyTitle,ShareHolderTitle ,	
	            (Select Year from cte c where c.RankShare=(Select Max(RankShare) from cte cc where cc.CompanyId=cte.CompanyId and cc.ShareholderId=cte.ShareholderId) and c.CompanyId=cte.CompanyId and c.ShareholderId=cte.ShareholderId) as NewYear, 
		            (Select Month from cte c where c.RankShare=(Select Max(RankShare) from cte cc where cc.CompanyId=cte.CompanyId and cc.ShareholderId=cte.ShareholderId) and c.CompanyId=cte.CompanyId and c.ShareholderId=cte.ShareholderId) as NewMonth, 
	            (Select shares from cte c where c.RankShare=(Select Max(RankShare) from cte cc where cc.CompanyId=cte.CompanyId and cc.ShareholderId=cte.ShareholderId) and c.CompanyId=cte.CompanyId and c.ShareholderId=cte.ShareholderId) as NewShare,
	             (Select Year from cte c where c.RankShare=(Select Max(RankShare) from cte cc where cc.CompanyId=cte.CompanyId and cc.ShareholderId=cte.ShareholderId)-1 and c.CompanyId=cte.CompanyId and c.ShareholderId=cte.ShareholderId) as OldYear,
	               (Select Month from cte c where c.RankShare=(Select Max(RankShare) from cte cc where cc.CompanyId=cte.CompanyId and cc.ShareholderId=cte.ShareholderId)-1 and c.CompanyId=cte.CompanyId and c.ShareholderId=cte.ShareholderId) as OldMonth,
	             (Select shares from cte c where c.RankShare=(Select Max(RankShare) from cte cc where cc.CompanyId=cte.CompanyId and cc.ShareholderId=cte.ShareholderId)-1 and c.CompanyId=cte.CompanyId and c.ShareholderId=cte.ShareholderId) as OldShare
	            from cte
		 group by CompanyID,ShareholderId ,CompanyTitle,ShareHolderTitle)
		 Select * from cte2
		  where  
		 OldShare is not null";
                query += " And CompanyId In(";
                foreach (int id in CompanyIds)
                {
                    query += id + " ,";
                }
                query = query.Remove(query.Length - 1);
                query += ")";
            }

            string strCnn = Configuration.GetConnectionString("ConnectionString");
            System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(query, strCnn);
            DataTable dt = new DataTable();
            da.Fill(dt);

            List<CompanyShareholderReport> models = new List<CompanyShareholderReport>();
            foreach (DataRow woak in dt.Rows)
            {
                models.Add(new CompanyShareholderReport()
                {
                    CompanyTitle = woak["CompanyTitle"].ToString(),
                    ShareholderTitle = woak["ShareHolderTitle"].ToString(),
                    OldShares = Convert.ToInt64(woak["OldShare"]),
                    Shares = Convert.ToInt64(woak["NewShare"]),
                    Year = Convert.ToInt32(woak["NewYear"] == null ? "0" : woak["NewYear"]),
                    Month = Convert.ToInt32(woak["NewMonth"] == null ? "0" : woak["NewMonth"]),
                    YearOld = Convert.ToInt32(woak["OldYear"] == null ? "0" : woak["OldYear"]),
                    MonthOld = Convert.ToInt32(woak["OldMonth"] == null ? "0" : woak["OldMonth"]),
                });
            }
            return View(models);
        }
        [HttpPost(Name = "CompanyShareEx")]
        public IActionResult CompanyShareEx(CompanyShareViewModel model)
        {
            if (!string.IsNullOrEmpty(model.JalaliFromDate) && !string.IsNullOrEmpty(model.JalaliToDate))
            {
                DateTime? fromDate = model.JalaliFromDate.ToDateTime();
                DateTime? toDate = model.JalaliToDate.ToDateTime();

                ViewBag.JalaliFromDate = model.JalaliFromDate;
                ViewBag.JalaliToDate = model.JalaliToDate;
                ViewBag.FromDate = model.JalaliFromDate.ToDateTime();
                ViewBag.ToDate = model.JalaliToDate.ToDateTime();

                PersianCalendar pc = new PersianCalendar();

                int yearFrom = pc.GetYear((DateTime)fromDate);
                int yearTo = pc.GetYear((DateTime)toDate);

                int monthFrom = pc.GetMonth((DateTime)fromDate);
                int monthTo = pc.GetMonth((DateTime)toDate);
                string query = "";


                query = String.Format(@" with cte as (
            SELECT CompanyID,ShareholderId 
            ,(Select Title from [dbo].[Companies] c where c.id=CompanyID) CompanyTitle
            ,(Select Title from [dbo].Shareholders c where c.id=ShareholderId) ShareHolderTitle
            ,shares
            ,ROW_NUMBER() over( partition by CompanyID,ShareholderId order by year,month ) RankShare,
            year,month
              FROM [KavoshFrameWorkWebApplication].[dbo].[CompanyShareholders] P
              where p.IsDeleted=0
            ), cte2 as (
	            Select CompanyID,ShareholderId,CompanyTitle,ShareHolderTitle ,	
	            (Select Year from cte c where c.RankShare=(Select Max(RankShare) from cte cc where cc.CompanyId=cte.CompanyId and cc.ShareholderId=cte.ShareholderId) and c.CompanyId=cte.CompanyId and c.ShareholderId=cte.ShareholderId) as NewYear, 
		            (Select Month from cte c where c.RankShare=(Select Max(RankShare) from cte cc where cc.CompanyId=cte.CompanyId and cc.ShareholderId=cte.ShareholderId) and c.CompanyId=cte.CompanyId and c.ShareholderId=cte.ShareholderId) as NewMonth, 
	            (Select shares from cte c where c.RankShare=(Select Max(RankShare) from cte cc where cc.CompanyId=cte.CompanyId and cc.ShareholderId=cte.ShareholderId) and c.CompanyId=cte.CompanyId and c.ShareholderId=cte.ShareholderId) as NewShare,
	             (Select Year from cte c where c.RankShare=(Select Max(RankShare) from cte cc where cc.CompanyId=cte.CompanyId and cc.ShareholderId=cte.ShareholderId)-1 and c.CompanyId=cte.CompanyId and c.ShareholderId=cte.ShareholderId) as OldYear,
	               (Select Month from cte c where c.RankShare=(Select Max(RankShare) from cte cc where cc.CompanyId=cte.CompanyId and cc.ShareholderId=cte.ShareholderId)-1 and c.CompanyId=cte.CompanyId and c.ShareholderId=cte.ShareholderId) as OldMonth,
	             (Select shares from cte c where c.RankShare=(Select Max(RankShare) from cte cc where cc.CompanyId=cte.CompanyId and cc.ShareholderId=cte.ShareholderId)-1 and c.CompanyId=cte.CompanyId and c.ShareholderId=cte.ShareholderId) as OldShare
	            from cte
		 group by CompanyID,ShareholderId ,CompanyTitle,ShareHolderTitle)
		 Select * from cte2		  
		 where NewYear>={0}  and NewYear<={0} 
		 and NewMonth>={1}  and NewMonth<={1}
		  and OldShare is not null", yearTo, monthTo);

                if (User.IsInRole("Admin"))
                {

                }
                else
                {
                    if(CompanyIds.Count==0)
                    {
                        return View("Error");
                    }

                    query += " And CompanyId In(";
                    foreach (int id in CompanyIds)
                    {
                        query += id + " ,";
                    }
                    query = query.Remove(query.Length - 1);
                    query += ")";
                }



                string strCnn = Configuration.GetConnectionString("ConnectionString");
                System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(query, strCnn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                List<CompanyShareholderReport> models = new List<CompanyShareholderReport>();
                foreach (DataRow woak in dt.Rows)
                {
                    models.Add(new CompanyShareholderReport()
                    {
                        CompanyTitle = woak["CompanyTitle"].ToString(),
                        ShareholderTitle = woak["ShareHolderTitle"].ToString(),
                        OldShares = Convert.ToInt64(woak["OldShare"]),
                        Shares = Convert.ToInt64(woak["NewShare"]),
                        Year = Convert.ToInt32(woak["NewYear"] == null ? "0" : woak["NewYear"]),
                        Month = Convert.ToInt32(woak["NewMonth"] == null ? "0" : woak["NewMonth"]),
                        YearOld = Convert.ToInt32(woak["OldYear"] == null ? "0" : woak["OldYear"]),
                        MonthOld = Convert.ToInt32(woak["OldMonth"] == null ? "0" : woak["OldMonth"]),
                    });
                }
                return View(models);
            }
            return RedirectToAction("CompanyShareEx");
        }

    }
}