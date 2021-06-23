using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CodeVault_DotNet
{
    class EmailDemo
    {
       public async Task  SendEmail()
        {
            try
            {
                var apiKey = Environment.GetEnvironmentVariable("EMAIL_API");
                var client = new SendGridClient(apiKey);
                var from = new EmailAddress("jdinith74@gmail.com", "dinith2");
                var subject = "Test message";
                var to = new EmailAddress("dj1234@yopmail.com", "test user");
               
                var htmlContent = "<strong>and easy to do anywhere, even with C# attmpt 2</strong>";
                var msg = MailHelper.CreateSingleEmail(from, to, subject, string.Empty , htmlContent);
                var response = await client.SendEmailAsync(msg);
            }
            catch (Exception ex )
            {


                Console.WriteLine(ex);
            }
        }
    }
}
