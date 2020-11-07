using EllipticCurve.Utils;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using String = System.String;

namespace FIT5032_Assignment.Utils
{
    public class EmailSender
    {
        // Please use your API KEY here.
        private const String API_KEY = "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";

        public void Send(String toEmail, String subject, String contents)
        {
            //var file = new FormFileCollection();
            var client = new SendGridClient(API_KEY);
            var from = new EmailAddress("lihaoxing30@vip.qq.com", "FIT5032 Assignment User");
            var to = new EmailAddress(toEmail, "");
            var plainTextContent = contents;
            var htmlContent = "<p>" + contents + "</p>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

            var response = client.SendEmailAsync(msg);
        }

        //Bulk email
        public void SendBulk(List<string> toEmails, String subject, String contents)
        {
            var client = new SendGridClient(API_KEY);
            var from = new EmailAddress("lihaoxing30@vip.qq.com", "FIT5032 Assignment User");
            List<EmailAddress> to = new List<EmailAddress>();
            for (int i = 0; i < toEmails.Count; i++)
            {
                to.Add(new EmailAddress(toEmails[i], ""));
            }
            var plainTextContent = contents;
            var htmlContent = "<p>" + contents + "</p>";
            var msg = MailHelper.CreateSingleEmailToMultipleRecipients(from, to, subject, plainTextContent, htmlContent);
            string path = HttpContext.Current.Server.MapPath("~/Resources/dontForget.PNG");
            var bytes = System.IO.File.ReadAllBytes(path);
            var attachFile = Convert.ToBase64String(bytes);
            msg.AddAttachment("dontForget.PNG", attachFile);
            var response = client.SendEmailAsync(msg);
        }
    }
}