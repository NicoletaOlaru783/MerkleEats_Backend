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

        public bool SendConfirmOrderEmail(string email, string UID, string orderType, List<OrderItemCreateDto> orderItem)
        {
            var key = new SendGridClient(_appSettings.ApiKey);
            var linkConfirm = "http://localhost:8080/orderConfirmed/o/" + UID;
            var linkCancel = "http://localhost:8080/OrderCancelled/o/" + UID;
            int index = email.IndexOf('.');
            var firstName = email.Substring(0, index).ToUpper();
            //firstName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(firstName.ToLower());
            var htmlStart = "<table><tr><th> Product</th><th>Quantity</th></tr>";
            var htmlMiddle = "";
            var htmlEnd = "</table>";
            foreach (var obj in orderItem)
            {
                htmlMiddle = htmlMiddle + "<tr><td> " + obj.Name + ": " + obj.Description + "<br><br></td><td>" + obj.Quantity + "<br><br></td></tr>";
            }
            var htmlFinal = htmlStart + htmlMiddle + htmlEnd;        

            var client = new RestClient("https://api.sendgrid.com/v3/mail/send");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/json");
            request.AddHeader("authorization", "Bearer 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");
            request.AddParameter("application/json", "{\"from\":{\"email\":\"Merkle@kitchen.merkleinc.agency\",\"name\":\"Merkle Kitchen DK\"},\"personalizations\":[{\"to\":[{\"email\":  \"" + email + "\" }],\"dynamic_template_data\":{\"items\":\"" + htmlFinal + "\",\"firstName\":\"" + firstName + "\",\"linkConfirm\":\"" + linkConfirm + "\",\"linkCancel\":\"" + linkCancel + "\", ,\"orderType\":\"" + orderType.ToLower() + "\"}}],\"template_id\":\"d-9899597d84d54dcfb35dc99a189b5af0\"}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

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
            var from = new EmailAddress("Merkle@kitchen.merkleinc.agency", "Merkle Kitchen DK");
            var subject = "Here's the deal with your meal";
            var text = "Order cancelled";
            var html = "<div> Hi there, <br><br> We write to let you know that your order has been accepted. <br><br> You can pick up your order from the kitchen area any time after 14 o'clock. <br><br> <b>Best regards</b>,<br>The kitchen staff </div>";

            foreach (var obj in recipients)
            {
                var newObject = new EmailAddress(obj.Recipient);
                toEmails.Add(newObject);
            }
            if(type == 0)
            {
               html = "Hi there, <br><br> We write to let you know that your order has been accepted. <br><br> You can pick up your order from the kitchen area any time after 14 o'clock. <br><br> <b>Best regards</b>,<br>The kitchen staff";
            }

            var msg = MailHelper.CreateSingleEmailToMultipleRecipients(from, toEmails, subject, text, html);
            await client.SendEmailAsync(msg);

            return;
        }
    }
}
