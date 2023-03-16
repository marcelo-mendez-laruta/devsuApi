using System;
using devsuApi.Context;
namespace devsuApi.Modules.Movements
{
	public interface Contracts
	{
		public Task<Movement> GetMovementAsync(int id);
		public Task<List<Movement>> GetMovementsAsync();
		public Task<Movement> CreateMovementAsync(NewMovementModel request);
		public Task<Movement> UpdateMovementAsync(int id, NewMovementModel request);
		public Task<bool> DeleteMovementAsync(int id);
	}
}

