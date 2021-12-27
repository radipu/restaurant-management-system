using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TomaFoodRestaurant.BLL;
using TomaFoodRestaurant.Model;
using TomaFoodRestaurant.OtherForm;
using TomaFoodRestaurant.DAL.DAO;
using Mailjet.Client;
using Mailjet.Client.Resources;
using Mailjet.Client.TransactionalEmails;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace TomaFoodRestaurant.DAL.CommonMethod
{
    public class SendEmail
    {

        /// <summary>
        /// Send email based on the restaurant setting
        /// </summary>
        public static async Task send(EmailSettings emailSettings)
        {
            try
            {
                MysqlEmailModuleDAO mysqlEmailModuleDAO = new MysqlEmailModuleDAO();
                EmailModule emailModule = mysqlEmailModuleDAO.GetEmailModule();

                switch (emailModule.Slug)
                {
                    case "mailjetmail":
                        await sendEmailByMailjet(emailModule, emailSettings);
                        break;
                    case "sendgridmail":
                        await sendEmailBySendgrid(emailModule, emailSettings);
                        break;
                    default:
                        //get mailjet info

                        break;
                }
            }

            catch (Exception exception)
            {
                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(exception.ToString());
            }
        }

        private static async Task sendEmailByMailjet(EmailModule emailModule, EmailSettings emailSetting)
        {
            try
            {
                MailjetClient client = new MailjetClient(emailModule.ApiKey, emailModule.ApiSecret);
                MailjetRequest request = new MailjetRequest
                {
                    Resource = Send.Resource,
                };

                // construct email with builder
                var email = new TransactionalEmailBuilder()
                .WithFrom(new SendContact(emailSetting.sender_email, emailSetting.sender_name))
                .WithSubject(emailSetting.subject)
                .WithHtmlPart(emailSetting.msg)
                .WithTo(new SendContact(emailSetting.to_email, emailSetting.to_name))
                .Build();

                // invoke API to send email
                var response = await client.SendTransactionalEmailAsync(email);

            }
            catch (Exception ex)
            {
                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(ex.ToString());
            }
        }

        private static async Task sendEmailBySendgrid(EmailModule emailModule, EmailSettings emailSetting)
        {
            try
            {
                var client = new SendGridClient(emailModule.ApiKey);
                var from = new EmailAddress(emailSetting.sender_email, emailSetting.sender_name);
                var to = new EmailAddress(emailSetting.to_email, emailSetting.to_name);
                var msg = MailHelper.CreateSingleEmail(from, to, emailSetting.subject, "", emailSetting.msg);
                var response = await client.SendEmailAsync(msg).ConfigureAwait(false);
            }
            catch (Exception ex)
            {

            }
            
        }

    }
}
