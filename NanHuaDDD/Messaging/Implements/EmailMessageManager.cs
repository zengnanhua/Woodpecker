/************************************************************************

*创建标记：啄木鸟

*创建时间：2016/5/11 19:46:20

*创建人：曾南华 

*版本号： V1.0.0.0

*描述：DDD
*************************************************************************/


using NanHuaDDD.ConfigConstants;
using NanHuaDDD.Singleton;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NanHuaDDD.Messaging.Implements
{
    internal class EmailMessageManager : Singleton<EmailMessageManager>, IMessageManager
    {
        //static string email_Address=

        private string email_Address = FrameConfigManager.Config.Email_Address;
        private string email_DisplayName = FrameConfigManager.Config.Email_DisplayName;
        private string email_Host = FrameConfigManager.Config.Email_Host;
        private string email_Password = FrameConfigManager.Config.Email_Password;
        private int email_Port = FrameConfigManager.Config.Email_Port;
        private string email_UserName = FrameConfigManager.Config.Email_UserName;

        private EmailMessageManager()
        { }


        public void Send(string recipient, string subject, string body, string serverVirtualPath = null)
        {
            Send(new List<string> { recipient }, subject, body);
        }

        public void Send(IEnumerable<string> recipients, string subject, string body, string serverVirtualPath = null)
        {
            Send(recipients, subject, body, false);
        }

        public void Send(IEnumerable<string> recipients, string subject, string body, bool isAsync, string serverVirtualPath = null)
        {
            try
            {
                if (recipients != null && recipients.Any())
                {
                    using (SmtpClient client = new SmtpClient()
                    {
                        Host = email_Host,
                        Port = email_Port,
                        Credentials = new NetworkCredential(email_UserName, email_Password),
                        EnableSsl = true,//设置为true会出现"服务器不支持安全连接的错误"
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                    })
                    {
                        #region 发送电子邮件
                        var mail = new MailMessage()
                        {
                            From = new MailAddress(email_Address,email_DisplayName),
                            Subject=subject,
                            Body=body,
                            IsBodyHtml=true
                        };

                        MailAddressCollection mailAddressCollection = new MailAddressCollection();
                        recipients.ToList().ForEach(i => {
                            //email有效性验证
                            if (new Regex(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$").IsMatch(i))
                                mail.To.Add(i);
                        });
                        if (isAsync)
                        {
                            client.SendCompleted += new SendCompletedEventHandler(client_SendCompleted);
                            client.SendAsync(mail, recipients);
                        }
                        else 
                        {
                            client.Send(mail);
                        }
                        #endregion
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
                Logger.LoggerFactory.Instance.Logger_Info(ex.Message);
            }
        }

        void client_SendCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            string arr = null;
            (e.UserState as List<string>).ToList().ForEach(i => { arr += i; });
        }
    }
}
