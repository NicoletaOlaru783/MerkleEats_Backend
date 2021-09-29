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
using System.Text.Json;
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
            var linkConfirm = _appSettings.LinkConfirm + UID;
            var linkCancel = _appSettings.LinkCancel + UID;
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
            request.AddHeader("authorization", "Bearer " + _appSettings.ApiKey);
            request.AddParameter("application/json", "{\"from\":{\"email\":\"Merkle@kitchen.merkleinc.agency\",\"name\":\"Merkle Kitchen DK\"},\"reply_to\":{\"email\":\"dk.canteen@emea.merkleinc.com\",\"name\":\"Merkle Kitchen DK\"},\"personalizations\":[{\"to\":[{\"email\":  \"" + email + "\" }],\"dynamic_template_data\":{\"items\":\"" + htmlFinal + "\",\"firstName\":\"" + firstName + "\",\"linkConfirm\":\"" + linkConfirm + "\",\"linkCancel\":\"" + linkCancel + "\", ,\"orderType\":\"" + orderType.ToLower() + "\"}}],\"template_id\":\"d-9899597d84d54dcfb35dc99a189b5af0\"}", ParameterType.RequestBody);
            client.Execute(request);

            return true;
        }

        public bool SendCustomEmail(Email email)
        {
            int index = email.Recipient.IndexOf('.');
            var firstName = email.Recipient.Substring(0, index).ToUpper();

            var client = new RestClient("https://api.sendgrid.com/v3/mail/send");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/json");
            request.AddHeader("authorization", "Bearer " + _appSettings.ApiKey);
            request.AddParameter("application/json", "{\"from\":{\"email\":\"Merkle@kitchen.merkleinc.agency\",\"name\":\"Merkle Kitchen DK\"}, \"reply_to\":{\"email\":\"dk.canteen@emea.merkleinc.com\",\"name\":\"Merkle Kitchen DK\"},\"personalizations\":[{\"to\":[{\"email\":  \"" + email.Recipient + "\" }],\"dynamic_template_data\":{\"firstName\":\"" + firstName + "\",\"subjectLine\":\"" + email.Subject + "\", \"text\":\"" + email.Text + "\"}}],\"template_id\":\"d-01f7475fb35244078094ac9d8a74dc49\"}", ParameterType.RequestBody);
            client.Execute(request);
            return true;
        }

        public bool SendOrderEmail(List<EmailMultipleRecipientsDto> recipients, int type)
        {
            string id = "d-734381b76c654b3ebdfddf5d3897bd03";
            if (type == 1)
            {
                id = "d-a1a8e7b79afb4078911ff48da26486dd";
            }

            foreach (var obj in recipients)
            {
                int index = obj.Recipient.IndexOf('.');
                var firstName = obj.Recipient.Substring(0, index).ToUpper();

                var client = new RestClient("https://api.sendgrid.com/v3/mail/send");
                var request = new RestRequest(Method.POST);
                request.AddHeader("content-type", "application/json");
                request.AddHeader("authorization", "Bearer " + _appSettings.ApiKey);
                request.AddParameter("application/json", "{\"from\":{\"email\":\"Merkle@kitchen.merkleinc.agency\",\"name\":\"Merkle Kitchen DK\"}, \"reply_to\":{\"email\":\"dk.canteen@emea.merkleinc.com\",\"name\":\"Merkle Kitchen DK\"},\"personalizations\":[{\"to\":[{\"email\":  \"" + obj.Recipient + "\" }],\"dynamic_template_data\":{\"firstName\":\"" + firstName + "\"}}],\"template_id\":\"" + id + "\"}", ParameterType.RequestBody);
                client.Execute(request);
            }
            return true;
        }
    }
}
