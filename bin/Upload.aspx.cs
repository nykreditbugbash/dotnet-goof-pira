using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NETWebFormsBlot
{
    public partial class Upload : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            // CTSECISSUE:DirectoryTraversal
            string originalFileName = Path.GetFileName(uploadFile.PostedFile.FileName);
            if (string.IsNullOrEmpty(originalFileName) || originalFileName.Contains("..") || originalFileName.Contains("/") || originalFileName.Contains("\\"))
            {
                throw new InvalidOperationException("Invalid file name.");
            }
            FileInfo fi = new FileInfo(originalFileName);

            Stream stream = uploadFile.FileContent;
            if (stream != null)
            {
                // nearly all File.Write* and related methods are sinks

                // CTSECISSUE:PossibleInsecureFileUpload
                string safeFileName = Path.GetFileName(uploadFile.PostedFile.FileName);
                string uploadPath = Path.Combine(@"C:\uploaded_files\", safeFileName);
                File.WriteAllBytes(uploadPath, uploadFile.FileBytes);

                // CTSECISSUE:PossibleInsecureFileUpload
                string sanitizedFileName = Path.GetFileName(uploadFile.PostedFile.FileName);
                string safePath = Path.Combine(@"C:\uploaded_files\", sanitizedFileName);
                File.WriteAllText(safePath, "");

                // Always use a fixed upload directory and sanitized file name
                string uploadDirectory = Server.MapPath("~/uploaded_files/");
                string sanitizedFileName2 = Path.GetFileName(uploadFile.PostedFile.FileName);
                string safeAbsolutePath = Path.Combine(uploadDirectory, sanitizedFileName2);
                File.WriteAllText(safeAbsolutePath, "");

                // CTSECISSUE:PossibleInsecureFileUpload
                byte [] buffer = ReadFully(stream);
                string converted = Encoding.UTF8.GetString(buffer, 0, buffer.Length);

                // Use sanitized file name and fixed upload directory to prevent path traversal
                string safeUploadDirectory = Server.MapPath("~/uploaded_files/");
                string safeSanitizedFileName = Path.GetFileName(uploadFile.PostedFile.FileName);
                string safeFullPath = Path.Combine(safeUploadDirectory, safeSanitizedFileName);
                File.WriteAllText(safeFullPath, converted);
            }
        }

        private static byte[] ReadFully(Stream input)
        {
            byte[] buffer = new byte[input.Length];
            //byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
    }
}