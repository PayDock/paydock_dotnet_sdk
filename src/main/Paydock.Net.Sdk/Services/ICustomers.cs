using Paydock.Net.Sdk.Models;

namespace Paydock.Net.Sdk.Services
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