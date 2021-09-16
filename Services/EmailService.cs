using MerkleKitchenApp_V2.Model;
using MerkleKitchenApp_V2.Model.Dtos;
using MerkleKitchenApp_V2.Services.IServices;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RestSharp;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MerkleKitchenApp_V2.Services
{
    public class EmailService : IEmailService
    {
        private readonly AppSettings _appSettings;
        public EmailService(IOptions<AppSettings> appsettings)
        {
            _appSettings = appsettings.Value;
        }

        public bool SendConfirmOrderEmail(string email, string UID)
        {
            var key = new SendGridClient(_appSettings.ApiKey);
            var linkConfirm = "http://localhost:8080/orderConfirmed/o/" + UID;
            var linkCancel = "http://localhost:8080/OrderCancelled/o/" + UID;
            int index = email.IndexOf('.');
            var firstName = email.Substring(0, index).ToUpper();
            firstName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(firstName.ToLower());

            var client = new RestClient("https://api.sendgrid.com/v3/mail/send");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/json");
            request.AddHeader("authorization", "Bearer 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");
            request.AddParameter("application/json", "{\"from\":{\"email\":\"Merkle@kitchen.merkleinc.agency\",\"name\":\"Merkle Kitchen DK\"},\"personalizations\":[{\"to\":[{\"email\":  \"" + email + "\" }],\"dynamic_template_data\":{\"firstName\":\"" + firstName + "\",\"linkConfirm\":\"" + linkConfirm + "\",\"linkCancel\":\"" + linkCancel + "\"}}],\"template_id\":\"d-9899597d84d54dcfb35dc99a189b5af0\"}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            
            //var from = new EmailAddress("merkle@em3726.kitchen.merkleinc.agency", "Merkle Kitchen");
            //var subject = "Please confirm your order";
            
            //var text = "Hi, you are almost there, we just need you to confirm your order by clicking the button below.  " + linkConfirm 
            //            + " . If you changed your mind, you can still cancel your order after you confirmed if it's before 13:00 o'clock, by clicking the button below. " + linkCancel;
            //var html = "";

            //var msg = MailHelper.CreateSingleEmail(from, new EmailAddress(email), subject, text, html);
            //await client.SendEmailAsync(msg);

            return true;
        }

        public async Task SendCustomEmail(Email email)
        {
            var client = new SendGridClient(_appSettings.ApiKey);
            var from = new EmailAddress("merkle@em3726.kitchen.merkleinc.agency", "Merkle Kitchen");

            var msg = MailHelper.CreateSingleEmail(from, new EmailAddress(email.Recipient), email.Subject, email.Text, email.Html);
            await client.SendEmailAsync(msg);
            return;
        }

        public async Task SendOrderEmail(List<EmailMultipleRecipientsDto> recipients, int type)
        {
            var toEmails = new List<EmailAddress>();
            var client = new SendGridClient(_appSettings.ApiKey);
            var from = new EmailAddress("merkle@em3726.kitchen.merkleinc.agency", "Merkle Kitchen");
            var subject = "Information about your order";
            var text = "Order cancelled";
            var html = "";

            foreach (var obj in recipients)
            {
                var newObject = new EmailAddress(obj.Recipient);
                toEmails.Add(newObject);
            }
            if(type == 0)
            {
                text = "Order accepted";
            }

            var msg = MailHelper.CreateSingleEmailToMultipleRecipients(from, toEmails, subject, text, html);
            await client.SendEmailAsync(msg);

            return;
        }
    }
}
