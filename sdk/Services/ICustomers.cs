using Paydock_dotnet_sdk.Models;

namespace Paydock_dotnet_sdk.Services
{
    public interface ICustomers
    {
        CustomerResponse Add(CustomerRequest request);
        CustomerItemResponse Delete(string customerId);
        CustomerItemsResponse Get();
        CustomerItemResponse Get(string customerId);
        CustomerItemsResponse Get(CustomerSearchRequest request);
        CustomerItemResponse Update(CustomerUpdateRequest request);
    }
}