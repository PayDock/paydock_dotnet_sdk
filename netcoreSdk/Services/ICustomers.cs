using System.Threading.Tasks;
using Paydock_dotnet_sdk.Models;

namespace Paydock_dotnet_sdk.Services
{
	public interface ICustomers
	{
		Task<CustomerResponse> Add(CustomerRequest request);
		Task<CustomerItemResponse> Delete(string customerId);
		Task<CustomerItemsResponse> Get();
		Task<CustomerItemsResponse> Get(CustomerSearchRequest request);
		Task<CustomerItemResponse> Get(string customerId);
		Task<CustomerItemResponse> Update(CustomerUpdateRequest request);
	}
}