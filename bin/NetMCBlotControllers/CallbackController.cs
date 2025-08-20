using NETMVCBlot.Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NETMVCBlot.Controllers
{
    public class CallbackController : BaseController
    {
        protected override void Utility()
        {

        }

        [HttpPost]
        public ActionResult Download(string fileName)
        {
            if (!IBValidator.IsValidFileName(fileName))
                return new HttpNotFoundResult();

            // CTSECISSUE:DirectoryTraversal
            return new FilePathResult(@"D:\wwwroot\reports\" + fileName, "application/pdf");
        }

        public String DownloadAsString(string fileName)
        {
            // Validate file name to prevent directory traversal
            if (!IBValidator.IsValidFileName(fileName))
                return null;

            string basePath = @"D:\wwwroot\reports\";
            string fullPath = System.IO.Path.GetFullPath(System.IO.Path.Combine(basePath, fileName));

            // Ensure the resulting path is within the intended directory
            if (!fullPath.StartsWith(basePath, StringComparison.OrdinalIgnoreCase))
                return null;

            return System.IO.File.ReadAllText(fullPath);
        }

        public JsonResult ExecuteProcess(string argument)
        {
            // Validate that the argument is a valid hostname or IP address (basic check)
            if (string.IsNullOrWhiteSpace(argument) || 
                !System.Text.RegularExpressions.Regex.IsMatch(argument, @"^[a-zA-Z0-9\.\-]+$"))
            {
                return Json(new { error = "Invalid argument." }, JsonRequestBehavior.AllowGet);
            }

            // Use ProcessStartInfo to avoid shell injection
            var psi = new ProcessStartInfo
            {
                FileName = "ping.exe",
                Arguments = argument,
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (var process = Process.Start(psi))
            {
                string output = process.StandardOutput.ReadToEnd();
                process.WaitForExit();
                return Json(new { result = output }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}