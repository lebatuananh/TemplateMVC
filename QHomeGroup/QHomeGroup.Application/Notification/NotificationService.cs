using FirebaseAdmin.Messaging;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QHomeGroup.Application.Notification
{
    public class NotificationService : INotificationService
    {
        private readonly FirebaseMessaging _messaging;
        private readonly IConfiguration _configuration;

        public NotificationService(FirebaseMessaging messaging, IConfiguration configuration)
        {
            _messaging = messaging;
            _configuration = configuration;
        }

        public async Task<bool> SendToDevice(string token, string title, string body, string contactId)
        {
            try
            {
                if (!string.IsNullOrEmpty(token))
                {
                    var result = await _messaging.SendAsync(CreateNotification(title, body, token, contactId));
                    return true;
                }
            }
            catch (FirebaseMessagingException e)
            {
                return false;
            }
            return true;
        }

        private Message CreateNotification(string title, string body, string token, string contactId)
        {
            return new Message()
            {
                Token = token ?? string.Empty,
                Notification = new FirebaseAdmin.Messaging.Notification()
                {
                    Body = body ?? string.Empty,
                    Title = title ?? string.Empty,
                },
                Webpush = new WebpushConfig()
                {
                    FcmOptions = new WebpushFcmOptions()
                    {
                        Link = $"{_configuration.GetValue<string>("AdminHost")}/contact/detail/{contactId}"
                    },
                    Notification = new WebpushNotification()
                    {
                        Icon = "https://qhomegroup.com/images/logo.png"
                    },
                    Data = new Dictionary<string, string> { { "contactId", contactId } }
                }
            };
        }
    }
}
