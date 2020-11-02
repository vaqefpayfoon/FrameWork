using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using KavoshFrameWorkCommon.Helpers;
using KavoshFrameWorkCore;
using KavoshFrameWorkCore.Models;
using KavoshFrameWorkData.Repositories.Generic;
using KavoshFrameWorkWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KavoshFrameWorkWebApplication.Controllers
{
    public class EnactmentSearchController : BaseController
    {
        IMapper _mapper;
        IEnactmentRepository _EnactmentRepository;

        public EnactmentSearchController(IEnactmentRepository EnactmentRepository, IMapper mapper, UserManager<ApplicationUser> userManager,
                     IGenericRepository<CompanyUser> companyUserRepository)
        {
            _mapper = mapper;
            this._userManager = userManager;
            this._companyUserRepository = companyUserRepository;
            this._EnactmentRepository = EnactmentRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> LoadData()
        {
            IEnumerable<EnactmentDetail> items;
            if(User.IsInRole("Admin"))
                items = await _EnactmentRepository.EnactmentDetails();
            else
                items = await _EnactmentRepository.EnactmentDetails(x => HasAccess(x.Enactment.CompanyId));

            EnactmentSearchViewModel model = new EnactmentSearchViewModel();
            model.EnactmentDetails = items.ToList();
            model.Search = string.Empty;

            try
            {
                var draw = Request.Form["draw"][0];
                var start = Request.Form["start"][0];
                var length = Request.Form["length"][0];
                var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                var sortColumnDir = Request.Form["order[0][dir]"].FirstOrDefault();
                var searchValue = Request.Form["search[value]"].FirstOrDefault();

    
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;
    
                var enactmentData = (from enactment in items.ToList()
                                    select new { enactment.Enactment.Title, enactment.EnactmentTitle, enactment.Enactment.Id });

                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
                {
                    enactmentData = enactmentData.ToList().OrderBy(woak => woak.EnactmentTitle);
                }
                if (!string.IsNullOrEmpty(searchValue))
                {
                    enactmentData = enactmentData.Where(m => m.EnactmentTitle.Contains(searchValue));
                }
 
                recordsTotal = enactmentData.Count();
                var data = enactmentData.Skip(skip).Take(pageSize).ToList();
                    
                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });

            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        public ActionResult Edit(int? ID)
        {
            try
            {
                return RedirectToAction("Edit","Enactment", new { id = ID });
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}