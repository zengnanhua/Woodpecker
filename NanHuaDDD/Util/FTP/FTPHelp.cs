using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Web.Common
{
  public static class FTPHelp
    {
      private static MyFTP ftp = new MyFTP();

      public static string GetBaseHttpPath() {
          return ftp.HttpUrl;
      }

      /// <summary>
      /// 上传到附件服务器
      /// </summary>
      /// <param name="filePath">文件名</param>
      /// <param name="fileData">文件数据</param>
      /// <returns>返回文件路径</returns>
       public static string UploadFile(string filePath, byte[] fileData)
       {
           string url = "";
           try
           {
               url=ftp.UploadFile(filePath, fileData);
           }
           catch {
               return null;
           }
           return url;
       }
       /// <summary>
       /// 上传到附件服务器
       /// </summary>
       /// <param name="filePath">文件名</param>
       /// <param name="fileData">HttpPostedFileBase 文件</param>
       /// <returns>返回文件路径</returns>
       public static string UploadFile(string filePath,HttpPostedFileBase singleFile)
       {
            string url = "";
            try
            {
                byte[] bytes = new byte[singleFile.InputStream.Length];
                singleFile.InputStream.Position = 0;
                singleFile.InputStream.Read(bytes, 0, (int)singleFile.InputStream.Length);
                url=ftp.UploadFile(filePath, bytes);
            }
            catch {
                return null;
            }
            return url;
       }
    }
}
