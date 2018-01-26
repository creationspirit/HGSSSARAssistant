using System;
using System.Collections.Generic;
using HGSSSARAssistant.Core;
using Newtonsoft.Json.Linq;
using PushSharp.Common;
using PushSharp.Google;

namespace HGSSSARAssistant.Web.Services
{
    public class ActionPushNotifier : IActionNotifier, IDisposable
    {
        private GcmServiceBroker _broker;

        public ActionPushNotifier()
        {
            var config = new GcmConfiguration("", "", null);
            this._broker = new GcmServiceBroker(config);
            this.SetupBroker();
        }

        private void SetupBroker()
        {
            this._broker.OnNotificationFailed += (notification, aggregateEx) =>
            {

                aggregateEx.Handle(ex =>
                {

                    // See what kind of exception it was to further diagnose
                    if (ex is GcmNotificationException)
                    {
                        var notificationException = (GcmNotificationException)ex;

                        // Deal with the failed notification
                        var gcmNotification = notificationException.Notification;
                        var description = notificationException.Description;

                        Console.WriteLine($"GCM Notification Failed: ID={gcmNotification.MessageId}, Desc={description}");
                    }
                    else if (ex is GcmMulticastResultException)
                    {
                        var multicastException = (GcmMulticastResultException)ex;

                        foreach (var succeededNotification in multicastException.Succeeded)
                        {
                            Console.WriteLine($"GCM Notification Succeeded: ID={succeededNotification.MessageId}");
                        }

                        foreach (var failedKvp in multicastException.Failed)
                        {
                            var n = failedKvp.Key;
                            var e = failedKvp.Value;

                            Console.WriteLine($"GCM Notification Failed: ID={n.MessageId}");
                        }

                    }
                    else if (ex is DeviceSubscriptionExpiredException)
                    {
                        var expiredException = (DeviceSubscriptionExpiredException)ex;

                        var oldId = expiredException.OldSubscriptionId;
                        var newId = expiredException.NewSubscriptionId;

                        Console.WriteLine($"Device RegistrationId Expired: {oldId}");

                        if (!string.IsNullOrWhiteSpace(newId))
                        {
                            // If this value isn't null, our subscription changed and we should update our database
                            Console.WriteLine($"Device RegistrationId Changed To: {newId}");
                        }
                    }
                    else if (ex is RetryAfterException)
                    {
                        var retryException = (RetryAfterException)ex;
                        // If you get rate limited, you should stop sending messages until after the RetryAfterUtc date
                        Console.WriteLine($"GCM Rate Limited, don't send more until after {retryException.RetryAfterUtc}");
                    }
                    else
                    {
                        Console.WriteLine("GCM Notification Failed for some unknown reason");
                    }

                    // Mark it as handled
                    return true;
                });
            };

            this._broker.OnNotificationSucceeded += (notification) =>
            {
                Console.WriteLine("GCM Notification Sent!");
            };

            // Start the broker
            this._broker.Start();
        }

        public void SendNotification(User recipient, string message)
        {

            // Queue a notification to send
            this._broker.QueueNotification(new GcmNotification
            {
                RegistrationIds = new List<string> {
                    recipient.AndroidPushId
                },
                Data = JObject.Parse("{ \"message\" : \"" + message + "\" }")
            });
        }

        public void Dispose()
        {
            this._broker.Stop();
        }
    }
}
