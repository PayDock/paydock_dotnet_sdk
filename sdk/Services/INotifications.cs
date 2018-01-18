using Paydock_dotnet_sdk.Models;

namespace Paydock_dotnet_sdk.Services
{
    public interface INotifications
    {
        NotificationTemplateResponse AddTemplate(NotificationTemplateRequest request);
        NotificationTriggerResponse AddTrigger(NotificationTriggerRequest request);
        NotificationLogResponse DeleteLog(string notificationLogId);
		NotificationLogResponse ResendNotification(string notificationLogId);
		NotificationTemplateResponse DeleteTemplate(string notificationTemplateId);
        NotificationTriggerResponse DeleteTrigger(string notificationTriggerId);
        NotificationLogsResponse GetLogs(NotificationLogRequest request);
        NotificationTriggerResponse GetTrigger(string notificationTriggerId);
        NotificationTriggerItemsResponse GetTriggers();
        NotificationTemplateResponse UpdateTemplate(NotificationTemplateUpdateRequest request);
    }
}