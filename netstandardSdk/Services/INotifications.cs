using System.Threading.Tasks;
using Paydock_dotnet_sdk.Models;

namespace Paydock_dotnet_sdk.Services
{
	public interface INotifications
	{
		Task<NotificationTemplateResponse> AddTemplate(NotificationTemplateRequest request);
		Task<NotificationTriggerResponse> AddTrigger(NotificationTriggerRequest request);
		Task<NotificationLogResponse> DeleteLog(string notificationLogId);
		Task<NotificationTemplateResponse> DeleteTemplate(string notificationTemplateId);
		Task<NotificationTriggerResponse> DeleteTrigger(string notificationTriggerId);
		Task<NotificationLogsResponse> GetLogs(NotificationLogRequest request);
		Task<NotificationTriggerResponse> GetTrigger(string notificationTriggerId);
		Task<NotificationTriggerItemsResponse> GetTriggers();
		Task<NotificationTemplateResponse> UpdateTemplate(NotificationTemplateUpdateRequest request);
        Task<NotificationLogResponse> ResendNotification(string notificationLogId);
	}
}