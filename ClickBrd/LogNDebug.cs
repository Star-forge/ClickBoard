using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Mail;
using System.Net;
using System.Net.Sockets;
using System.Net.Mime;
using System.IO;
using System.Net.NetworkInformation;

namespace ClickBrd
{
    class LogNDebug
    {
        public int formWidth = 600;
        public int formHeight = 600;

        public LogNDebug() { }

        public static void ViewInfo(string message)
        {
            MessageBox.Show(message, "Поймано исключение", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void ToLog(string message)
        {
            //MessageBox.Show(message, "Поймано исключение", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static bool TestIP(string ip)
        {
            Ping pingSender = new Ping ();
            PingOptions options = new PingOptions ();

            // Use the default Ttl value which is 128,
            // but change the fragmentation behavior.
            options.DontFragment = true;

            // Create a buffer of 32 bytes of data to be transmitted.
            string data = "0";
            byte[] buffer = Encoding.ASCII.GetBytes (data);
            int timeout = 120;
            PingReply reply = pingSender.Send (IPAddress.Parse(ip), timeout, buffer, options);
            if (reply.Status == IPStatus.Success)
                return true;
            else
                return false;
        }

        public static void SendLoMailReport(string smtpServer, string port, string userName, string password, string mailSender,
            string mailSubject, string mailBody, string mailReciver, bool useSsl = true, string attachmentName = "")
        {
            var client = new SmtpClient(smtpServer, Convert.ToInt32(port))
                {
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(userName, password),
                    EnableSsl = useSsl
                };
            var message = new MailMessage();
            message.From = new MailAddress(mailSender);
            message.Subject = mailSubject;
            message.Body = mailBody;
            if (mailBody.Contains("<html>") || mailBody.Contains("<tr") || mailBody.Contains("<table"))
                message.IsBodyHtml = true;
            message.To.Add(mailReciver);
            if (!string.IsNullOrEmpty(attachmentName))
            {
                string[] attachmentArr = attachmentName.Split(new[] {";"}, StringSplitOptions.RemoveEmptyEntries);
                foreach (var attach in attachmentArr)
                {
                    var data = new Attachment(attach, MediaTypeNames.Application.Octet);
                    ContentDisposition disposition = data.ContentDisposition;
                    disposition.CreationDate = File.GetCreationTime(attach);
                    disposition.ModificationDate = File.GetLastWriteTime(attach);
                    disposition.ReadDate = File.GetLastAccessTime(attach);
                    message.Attachments.Add(data);
                }
            }
                
            client.Send(message);
            message.Dispose();

        } 

        public static void SendEmailReport(string message)
        {
            try
            {
                if (!TestIP("10.32.0.229"))
                {
                    MessageBox.Show("Ошибка:\n" + message + "\n\nНевозможно отправить письмо с сообщением об ошибке стандартным способом!\nСообщение будет отправлено через gmail, если получится.", "Произошла ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    if (TestIP("87.245.198.35"))
                        //SendLoMailReport(SMTP сервер     ,  port, userName       , password           , mailSender               , mailSubject           , mailBody, mailReciver              , useSsl)
                        SendLoMailReport("smtp.gmail.com", "587", "AndreyDemakov", "C5-F1-BC-70-88-89", "AndreyDemakov@gmail.com", "Отладка папки обмена", message, "AndreyDemakov@gmail.com", true, null);
                }     
                else 
                    if (MessageBox.Show("Ошибка:\n" + message + "\n\nОтправить письмо с сообщением об ошибке?", "Произошла ошибка!", MessageBoxButtons.YesNo, MessageBoxIcon.Error).Equals(DialogResult.Yes))
                        SendLoMailReport("10.32.0.229", "25", "itru_demakov", "F1ashback", "itru_demakov@spb.orw.rzd", "Отладка папки обмена", message, "itru_demakov@spb.orw.rzd", false, null);
                    //else
                    //    SendLoMailReport("smtp.gmail.com", "465", "AndreyDemakov", "C5-F1-BC-70-88-89", "AndreyDemakov@gmail.com", "Отладка папки обмена", message, "AndreyDemakov@gmail.com", true, null);
            }
            catch (WebException we)
            {
                MessageBox.Show(we.ToString(), "Ошибка");
            }
        }
    }
}
