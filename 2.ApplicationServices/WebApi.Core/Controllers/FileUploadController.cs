using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace WebApi.Core.Controllers
{
    public class FileUploadController : BaseController
    {
        [HttpPost()]
        public string UploadFiles()
        {
            int iUploadedCnt = 0;

            // DEFINE THE PATH WHERE WE WANT TO SAVE THE FILES.
            string sPath = "";
            sPath = new HostingEnvironment().WebRootPath + "\\locker\\";

            var hfc = HttpContext.Request.Form.Files;

            // CHECK THE FILE COUNT.
            for (int iCnt = 0; iCnt <= hfc.Count - 1; iCnt++)
            {
                IFormFile hpf = hfc[iCnt];


                if (hpf.Length > 0)
                {
                    // CHECK IF THE SELECTED FILE(S) ALREADY EXISTS IN FOLDER. (AVOID DUPLICATE)
                    if (!System.IO.File.Exists(sPath + Path.GetFileName(hpf.FileName)))
                    {
                        // SAVE THE FILES IN THE FOLDER.
                        using (FileStream output = System.IO.File.Create(sPath))
                        {
                            hpf.CopyTo(output);
                            iUploadedCnt = iUploadedCnt + 1;
                        }
                    }
                }
            }

            // RETURN A MESSAGE (OPTIONAL).
            if (iUploadedCnt > 0)
            {
                return iUploadedCnt + " Files Uploaded Successfully";
            }
            else {
                return "Upload Failed";
            }
        }
    }
}
