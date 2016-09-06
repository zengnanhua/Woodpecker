using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Web.Common
{
    class MyFTP
    {
        private string FtpUrl = string.Empty;
        private string FtpUserName = string.Empty;
        private string FtpPwd = string.Empty;
        private string httpUrl = string.Empty;
        public string HttpUrl {
            get {
                if (string.IsNullOrEmpty(httpUrl))
                {
                    if (string.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["httpServer"]))
                    {
                        throw new Exception("httpServer 没有配置");
                    }
                    this.httpUrl = System.Configuration.ConfigurationManager.AppSettings["httpServer"];
                }
                return httpUrl;
            }
            set { httpUrl = value; }
        }
        private FtpWebRequest FtpWebRequest;
        private FtpWebResponse FtpWebResponse;

        public MyFTP(string ftpUrl, string ftpUserName, string ftpPwd)
        {
            this.FtpUrl = ftpUrl;
            this.FtpUserName = ftpUserName;
            this.FtpPwd = ftpPwd;
        }

        /// <summary>
        /// 读取配置文件
        /// </summary>
        public MyFTP()
        {
            if (string.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["FtpServer"]))
            {
                throw new Exception("FtpServer 没有配置");
            }
            string[] FtpServer = System.Configuration.ConfigurationManager.AppSettings["FtpServer"].Split(',');
          
            this.FtpUrl = FtpServer[0];
            this.FtpUserName = FtpServer[1];
            this.FtpPwd = FtpServer[2];
        }

        #region 这是Ftp的链接
        private void Conn(string filePath)
        {
            try
            {
                FtpWebRequest = (FtpWebRequest)WebRequest.Create(filePath);
                FtpWebRequest.Credentials = new NetworkCredential(FtpUserName, FtpPwd);
                FtpWebRequest.UseBinary = true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public void test()
        {
            int i = "/lksdfj".IndexOf('/');

            Console.WriteLine(i);
        }
        #endregion

        #region 前面是否加斜杠
        private string stringHandle(string filePath)
        {
            if (filePath.IndexOf('/') != 0 && filePath.IndexOf('\\') == -1)  //这一句 判断了前面是否有 / \的字符
            {
                filePath = "/" + filePath;
            }
            return filePath;
        }
        #endregion

        #region 删除文件
        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="fileName">文件名+路径，(要加后缀,从根目录算起)</param>
        /// <returns></returns>
        public MyFTP DeleteFileName(string fileName)
        {
            string url = FtpUrl + stringHandle(fileName);
            Conn(url);      //创建这个图片的访问链接
            try
            {
                FtpWebRequest.Method = WebRequestMethods.Ftp.DeleteFile;
                FtpWebResponse = (FtpWebResponse)FtpWebRequest.GetResponse();
                FtpWebResponse.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return this;
        }
        #endregion

        #region 上传文件
        public string UploadFile(string filePath, byte[] fileData)
        {
            string url = FtpUrl + stringHandle(filePath);
         //  string httpUrl = this.HttpUrl + stringHandle(filePath);
            Conn(url);
            try
            {
                FtpWebRequest.Method = WebRequestMethods.Ftp.UploadFile;
                FtpWebRequest.ContentLength = fileData.Length;
                using (Stream ftpStream = FtpWebRequest.GetRequestStream())
                {
                    ftpStream.Write(fileData, 0, fileData.Length);
                }
                FtpWebResponse = (FtpWebResponse)FtpWebRequest.GetResponse();
                string msg = FtpWebResponse.StatusDescription;
                FtpWebResponse.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return filePath;
        }
        #endregion

        #region 下载文件
        public string DownloadFile(string filepath, out byte[] outbytes)
        {
         

          
                string url = FtpUrl + stringHandle(filepath);
                string httpUrl = this.HttpUrl + stringHandle(filepath);
                try
                {
                Conn(url);
          
                 FtpWebRequest.Method = WebRequestMethods.Ftp.DownloadFile;
            
                using (FtpWebResponse = (FtpWebResponse)FtpWebRequest.GetResponse())
                {
                    using (Stream ftpStream = FtpWebResponse.GetResponseStream())
                    {
                        long bufferSize = ftpStream.Length;
                    
                        outbytes = new byte[bufferSize];
                       ftpStream.Read(outbytes, 0, (int)bufferSize);
                    }
                }
            }
            catch (Exception ex)
            {
                outbytes = null;
                throw new Exception(ex.Message);
            }
                return httpUrl;

        }
        #endregion

        #region 遍历文件
        #endregion
    }
}
