using System;
using HGSSSARAssistant.Core;

namespace HGSSSARAssistant.Web.Services
{
    public interface IActionNotifier
    {
        void SendNotification(User recipient, string message);
    }
}
