using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using TomaFoodRestaurant.Model;

namespace TomaFoodRestaurant.BLL
{
    public class CustomizeMessageSentToEmail
    {

      
        public void SentMail(string ex)
        {
            var resInformation = GlobalSetting.RestaurantInformation;
            MailMessage mail = new MailMessage();
            mail.Subject = resInformation.RestaurantName;
            mail.Body = ex;
            Mail(mail);

        }

        private void Mail(MailMessage mail)
        {
            try
            {

                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("demo@gmail.com");
                mail.To.Add("demo.iiuc2017@gmail.com");

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("demo.iiuc2017@gmail.com", "samsung123");
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);

            }
            catch (Exception ex)
            {

            }
        }
    }
}