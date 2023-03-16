using System;
namespace devsuApi.Modules.Clients
{
	public static class Routes
	{
		public static void MapClientEndpoints(this IEndpointRouteBuilder routes)
		{
			var clientGroup = routes.MapGroup("/clients");
			clientGroup.MapGet("", (Contracts clientServices) =>
			{
				return Results.Ok(clientServices.GetClientsAsync());
			});
			clientGroup.MapGet("/{id}", (int id, Contracts clientServices) =>
			{
				return Results.Ok(clientServices.GetClientAsync(id));
			});
			clientGroup.MapPost("/new", (newClientModel request, Contracts clientServices) =>
			{
				return Results.Ok(clientServices.CreateClientAsync(request));
			});
			clientGroup.MapPut("/{id}", (int id, newClientModel request, Contracts clientServices) =>
			{
				return Results.Ok(clientServices.UpdateAccountAsync(id, request));
			});
			clientGroup.MapDelete("/{id}", (int id, Contracts clientServices) =>
			{
				return clientServices.DeleteAccountAsync(id).Result?Results.Ok():Results.NotFound();
			});
		}
	}
}

