using System;
using System.IO;
using System.Web;

namespace Projeto.Helper
{
    public class FilesHelper
    {
        public static bool UploadPhoto(HttpPostedFileBase file, string folder, string name)
        {
            string path = string.Empty;

            if (file == null || string.IsNullOrEmpty(folder) || string.IsNullOrEmpty(name))
            {
                return false;
            }
            try
            {
                if (file != null)
                {
                    path = Path.Combine(HttpContext.Current.Server.MapPath(folder), name);
                    file.SaveAs(path);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        file.InputStream.CopyTo(ms);
                        byte[] array = ms.GetBuffer();
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
    }
}