using Microsoft.AspNetCore.Http;
using Serilog;
using System;

namespace KavoshFrameWorkCommon.Extensions
{
    public static class HttpPostedFileBaseExtensions
    {
        public const int ImageMinimumBytes = 512;

        public static bool IsImage(this IFormFile postedFile)
        {
            try
            {
                //-------------------------------------------
                //  Check the image mime types
                //-------------------------------------------

              if (!string.Equals(postedFile.ContentType, "image/jpg", StringComparison.OrdinalIgnoreCase) &&
              !string.Equals(postedFile.ContentType, "image/jpeg", StringComparison.OrdinalIgnoreCase) &&
              !string.Equals(postedFile.ContentType, "image/pjpeg", StringComparison.OrdinalIgnoreCase) &&
              !string.Equals(postedFile.ContentType, "image/gif", StringComparison.OrdinalIgnoreCase) &&
              !string.Equals(postedFile.ContentType, "image/x-png", StringComparison.OrdinalIgnoreCase) &&
              !string.Equals(postedFile.ContentType, "image/png", StringComparison.OrdinalIgnoreCase))
                {
                    return false;
                }
                return true;
            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                return false;
            }
           
        }
    }
}
