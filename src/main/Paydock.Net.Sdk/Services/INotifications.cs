using Paydock.Net.Sdk.Models;

namespace Paydock.Net.Sdk.Services
{
    public interface INotifications
    {
        NotificationTemplateResponse AddTemplate(NotificationTemplateRequest request);
        NotificationTriggerResponse AddTrigger(NotificationTriggerRequest request);
        NotificationTriggerResponse DeleteLog(string notificationLogId);
        NotificationTemplateResponse DeleteTemplate(string notificationTemplateId);
        NotificationTriggerResponse DeleteTrigger(string notificationTriggerId);
        NotificationLogsResponse GetLogs(NotificationLogRequest request);
        NotificationTriggerResponse GetTrigger(string notificationTriggerId);
        NotificationTriggerItemsResponse GetTriggers();
        NotificationTemplateResponse UpdateTemplate(NotificationTemplateUpdateRequest request);
    }
}