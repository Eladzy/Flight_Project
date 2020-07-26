//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using SendGrid;
//using SendGrid.Helpers.Mail;
//using System.Threading.Tasks;

//namespace SendGridLibrary
//{
//    class SendGridClient
//    {
//        private static void Main()
//        {
//            Execute().Wait();
//        }

//        static async Task Execute()
//        {
//            var apiKey = Environment.GetEnvironmentVariable("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");
//            var client = new SendGridClient(apiKey);
//            var from = new EmailAddress("test@example.com", "Example User");
//            var subject = "Sending with SendGrid is Fun";
//            var to = new EmailAddress("test@example.com", "Example User");
//            var plainTextContent = "and easy to do anywhere, even with C#";
//            var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
//            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
//            var response = await client.SendEmailAsync(msg);
//        }
//    }
//}
