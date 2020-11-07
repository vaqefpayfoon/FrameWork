using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ExcelReader;
using KavoshFrameWorkWebApplication.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace KavoshFrameWorkWebApplication.Controllers
{
    public class ExcelReaderController : Controller
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        public ExcelReaderController(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        public IActionResult Index()
        {
            try
            {
                return View(new MartisViewModel());

            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                return View("~/Views/Shared/Error.cshtml", new ErrorViewModel { RequestId = e.Message });
            }

        }
        [HttpPost]
        public IActionResult Index(MartisViewModel mat)
        {
            string webRootPath = _hostingEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;
            List<MatrisDetailViewModel> lst = new List<MatrisDetailViewModel>();
            if (files.Count > 0)
            {
                //files has been uploaded
                var fileAddress = Path.Combine(webRootPath + "\\", "files" + "\\" + files[0].FileName);
                //var extension = Path.GetExtension(files[0].FileName);
                //string fileAddress = Path.Combine(uploads, extension);1, 1, 4, 2
                using (var filesStream = new FileStream(fileAddress, FileMode.Create))
                {
                    files[0].CopyTo(filesStream);
                }
                Book obj = new Book(fileAddress);
                Sheet[] sheets = obj.getSheets();

                var items = sheets[mat.SheetNo - 1].getRange(mat.FromRow, mat.FromColumn, mat.ToRow, mat.ToColumn);
                for (int i = mat.FromRow - 1; i <= mat.ToRow - 1; i++)
                {
                    for (int j = mat.FromColumn - 1; j <= mat.ToColumn - 1; j++)
                    {
                        MatrisDetailViewModel matris = new MatrisDetailViewModel();
                        matris.Text = items[i, j];
                        matris.RowNum = i;
                        lst.Add(matris);
                    }
                }

                obj.CloseFile();
            }
            else
            {

            }
            mat.MatrisDetails = lst;

            return View(mat);
        }
    }
}
