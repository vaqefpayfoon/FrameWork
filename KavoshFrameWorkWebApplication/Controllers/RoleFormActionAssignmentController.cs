using AutoMapper;
using KavoshFrameWorkCore;
using KavoshFrameWorkCore.Models;
using KavoshFrameWorkData.Repositories.Generic;
using KavoshFrameWorkWebApplication.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace KavoshFrameWorkWebApplication.Controllers
{
    public class RoleFormActionAssignmentController : BaseController
    {
        IMapper _mapper;
        RoleManager<ApplicationRole> _roleManager;
        private readonly KavoshFrameWorkContext _context;
        IGenericRepository<RoleFormActionAssignment> _roleFormActionAssignmentRepository;
        IGenericRepository<SystemForm> _systemFormRepository;
        
        private readonly IEntityService entityService;

        public RoleFormActionAssignmentController(
             IGenericRepository<RoleFormActionAssignment> roleFormActionAssignmentRepository,
             IGenericRepository<SystemForm> systemFormRepository,
             IEntityService entityService, UserManager<ApplicationUser> userManager,
             RoleManager<ApplicationRole> roleManager,
             KavoshFrameWorkContext context, IMapper mapper)
        {
            _userManager = userManager;
            this._roleFormActionAssignmentRepository = roleFormActionAssignmentRepository;
            this._systemFormRepository = systemFormRepository;
            this.entityService = entityService;
            this._context = context;
            this._mapper = mapper;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            try
            {                
                IEnumerable<IGrouping<object, RoleFormActionAssignment>> model = _roleFormActionAssignmentRepository.GetAsQueryableGroupBy(x => x.RoleId, includeProperties: "Role");

                IEnumerable<KeyTextViewModel> result = model.Select(woak => new KeyTextViewModel
                {
                    Text = woak.FirstOrDefault().Role.Name,
                    Id = woak.Key.ToString()
                });
                return View(result);
            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                return View("~/Views/Shared/Error.cshtml", new ErrorViewModel { RequestId = e.Message });
            }
        }

        [HttpGet]
        //[Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            try
            {
                var model = new RoleFormActionAssignmentsViewModel();
                ViewBag.Lists = GetItems();
                model.Items = _systemFormRepository.GetAsQueryable()
                    .Select(x => new RoleFormActionAssignmentViewModel
                    {
                        SystemFormId = x.Id,
                        SystemFormTitle = x.Title
                    }).ToList();

                return View(model);
            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                return View("~/Views/Shared/Error.cshtml", new ErrorViewModel { RequestId = e.Message });
            }
        }

        [HttpPost]
        // [ValidateAntiForgeryToken]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(RoleFormActionAssignmentsViewModel model)
        {
            try
            {
                ViewBag.Lists = GetItems();
                if (ModelState.IsValid)
                {
                    var roleActionAssignemtList = new List<RoleFormActionAssignment>();
                    foreach (var item in model.Items)
                    {
                        var systemAction = SystemAction.None;

                        if (item.CanCreate)
                            systemAction = systemAction | SystemAction.Create;
                        if (item.CanRead)
                            systemAction = systemAction | SystemAction.Read;
                        if (item.CanUpdate)
                            systemAction = systemAction | SystemAction.Update;
                        if (item.CanDelete)
                            systemAction = systemAction | SystemAction.Delete;

                        if (systemAction != SystemAction.None)
                        {
                            var assignment = new RoleFormActionAssignment
                            {
                                SystemFormId = item.SystemFormId,
                                SystemAction = systemAction,
                                RoleId = model.RoleId
                            };
                            roleActionAssignemtList.Add(assignment);
                        }
                    }
                    await _roleFormActionAssignmentRepository.InsertAllAsync(roleActionAssignemtList);

                    if (Request.Form.Keys.Contains("SaveAndReturn"))
                        return RedirectToAction("Index");
                    else
                        return RedirectToAction("Edit", new { id = model.RoleId });
                }
                return View(model);
            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                return View("~/Views/Shared/Error.cshtml", new ErrorViewModel { RequestId = e.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            try
            {
                //var model = new RoleFormActionAssignmentsViewModel();
                //model.RoleId = id;
                //var currRoles = _roleFormActionAssignmentRepository.GetAsQueryable(x => x.RoleId == id).Join(_systemFormRepository.GetAsQueryable(), a => a.SystemFormId, b => b.Id, (a, b) => new { allRoles = a, systemForms = b });

                //model.Items = currRoles.Select(x => new RoleFormActionAssignmentViewModel
                //{
                //    SystemFormId = x.systemForms.Id,
                //    SystemFormTitle = x.systemForms.Title,
                //    CanCreate = (x.allRoles.SystemAction & SystemAction.Create) != 0,
                //    CanRead = (x.allRoles.SystemAction & SystemAction.Read) != 0,
                //    CanUpdate = (x.allRoles.SystemAction & SystemAction.Update) != 0,
                //    CanDelete = (x.allRoles.SystemAction & SystemAction.Delete) != 0,
                //    SystemAction = x.allRoles.SystemAction
                //}).ToList();
                //return View(model);


                //var model = new RoleFormActionAssignmentsViewModel();
                //model.RoleId = id;
                //var currRoles = _systemFormRepository.GetAsQueryable().GroupJoin(_roleFormActionAssignmentRepository.GetAsQueryable(), a => a.Id, b => b.SystemFormId, (a, b) => new { systemForms= a, allRoles = b }).SelectMany(
                //  sysForm => sysForm.allRoles.DefaultIfEmpty(),
                //  (x, y) => new { Forms= x.systemForms, rols = y }); ;

                //model.Items = currRoles.Select(x => new RoleFormActionAssignmentViewModel
                //{
                //    SystemFormId = x.Forms.Id,
                //    SystemFormTitle = x.Forms.Title,
                //    CanCreate = (x.rols.SystemAction & SystemAction.Create) != 0,
                //    CanRead = (x.rols.SystemAction & SystemAction.Read) != 0,
                //    CanUpdate = (x.rols.SystemAction & SystemAction.Update) != 0,
                //    CanDelete = (x.rols.SystemAction & SystemAction.Delete) != 0,
                //    SystemAction = x.rols.SystemAction
                //}).ToList(); ;
                //return View(model);

                var resultFormActionAssignments = await _roleFormActionAssignmentRepository.GetAsync(x => x.RoleId == id);
               

                var roleFormActionAssignments = resultFormActionAssignments.Select(x => new
                {
                    x.SystemFormId,
                    x.SystemAction
                }).ToList();

                var model = new RoleFormActionAssignmentsViewModel();
                model.RoleId = id;
                var sysFormList = await _systemFormRepository.GetAsync();
                model.Items = sysFormList
                    .Select(x => new RoleFormActionAssignmentViewModel
                    {
                        SystemFormId = x.Id,
                        SystemFormTitle = x.Title,
                        CanCreate = (roleFormActionAssignments.Where(r => r.SystemFormId == x.Id).Select(r => r.SystemAction).FirstOrDefault() & SystemAction.Create) != 0,
                        CanRead = (roleFormActionAssignments.Where(r => r.SystemFormId == x.Id).Select(r => r.SystemAction).FirstOrDefault() & SystemAction.Read) != 0,
                        CanUpdate = (roleFormActionAssignments.Where(r => r.SystemFormId == x.Id).Select(r => r.SystemAction).FirstOrDefault() & SystemAction.Update) != 0,
                        CanDelete = (roleFormActionAssignments.Where(r => r.SystemFormId == x.Id).Select(r => r.SystemAction).FirstOrDefault() & SystemAction.Delete) != 0
                    }).ToList();

                // return RedirectToAction("Index");

                return View(model);

            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                return View("~/Views/Shared/Error.cshtml", new ErrorViewModel { RequestId = e.Message });
            }
        }

        [HttpPost]
        // [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(RoleFormActionAssignmentsViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _roleFormActionAssignmentRepository.DeleteAllAsync(x => x.RoleId == model.RoleId);
                    var roleActionAssignemtList = new List<RoleFormActionAssignment>();
                    foreach (var item in model.Items)
                    {
                        var systemAction = SystemAction.None;

                        if (item.CanCreate)
                            systemAction = systemAction | SystemAction.Create;
                        if (item.CanRead)
                            systemAction = systemAction | SystemAction.Read;
                        if (item.CanUpdate)
                            systemAction = systemAction | SystemAction.Update;
                        if (item.CanDelete)
                            systemAction = systemAction | SystemAction.Delete;

                        if (systemAction != SystemAction.None)
                        {
                            var assignment = new RoleFormActionAssignment
                            {
                                SystemFormId = item.SystemFormId,
                                SystemAction = systemAction,
                                RoleId = model.RoleId
                            };
                            roleActionAssignemtList.Add(assignment);
                        }
                    }
                    await _roleFormActionAssignmentRepository.InsertAllAsync(roleActionAssignemtList);

                    if (Request.Form.Keys.Contains("SaveAndReturn"))
                        return RedirectToAction("Index");
                    else

                        return RedirectToAction("Edit", new { id = model.RoleId });
                }
                return View(model);
            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                return View("~/Views/Shared/Error.cshtml", new ErrorViewModel { RequestId = e.Message });
            }
        }
        public ActionResult TagHelper_Virtualization_Read([DataSourceRequest] DataSourceRequest request)
        {
            try
            {
                List<ApplicationRole> roles = _roleManager.Roles.ToList();
                return Json(roles.ToDataSourceResult(request));
            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                return View("~/Views/Shared/Error.cshtml", new ErrorViewModel { RequestId = e.Message });
            }
        }

        public IActionResult TagHelper_Orders_ValueMapper(string[] values)
        {
            try
            {
                var indices = new List<int>();

                if (values != null && values.Any())
                {
                    var index = 0;
                    List<ApplicationRole> roles = _roleManager.Roles.ToList();
                    foreach (var item in roles)
                    {
                        if (values.Contains(item.Id))
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
        public List<SelectListItem> GetItems()
        {
            try
            {
                List<SelectListItem> lst = _roleManager.Roles.Select(woak => new SelectListItem
                {
                    Text = woak.Name,
                    Value = woak.Id.ToString()
                }).ToList();
                return lst;
            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                return null;
            }
        }

        //public ActionResult ApplicationRole_Read([DataSourceRequest] Kendo.Mvc.UI.DataSourceRequest request)
        //{
        //    try
        //    {
        //        return Json(_roleManager.Roles.ToList().ToDataSourceResult(request));

        //    }
        //    catch (Exception e)
        //    {
        //        Log.Error(e, e.Message);
        //        return View("~/Views/Shared/Error.cshtml", new ErrorViewModel { RequestId = e.Message });
        //    }
        //}


        public ActionResult ApplicationRole_Read([DataSourceRequest] Kendo.Mvc.UI.DataSourceRequest request ,string id)
        {
            try
            {
                var hiddenRoles = new List<string>();
                if (string.IsNullOrEmpty(id))
                    hiddenRoles = _roleFormActionAssignmentRepository.GetAll().Select(x => x.RoleId).ToList();


                return Json(_roleManager.Roles.Where(x => !hiddenRoles.Contains(x.Id))
                    .ToList().ToDataSourceResult(request));

            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                return View("~/Views/Shared/Error.cshtml", new ErrorViewModel { RequestId = e.Message });
            }
        }
        public ActionResult ApplicationRole_ValueMapper(string[] values)
        {
            try
            {
                var indices = new List<int>();

                if (values != null && values.Any())
                {
                    var index = 0;

                    foreach (var woak in TagHelper_ApplicationRoles())
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
        public IEnumerable<ApplicationRole> TagHelper_ApplicationRoles()
        {
            try
            {
                return _roleManager.Roles.ToList();
            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                return null;
            }
        }

    }
}