using devsuApi.Context;
namespace devsuApi.Modules.Clients
{
	public interface Contracts
	{
		public Task<Client> GetClientAsync(int id);
		public Task<List<Client>> GetClientsAsync();
		public Task<Client> CreateClientAsync(newClientModel request);
		public Task<Client> UpdateAccountAsync(int id, newClientModel request);
		public Task<bool> DeleteAccountAsync(int id);
	}
}

