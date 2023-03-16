using System;
namespace devsuApi.Modules.Movements
{
	public static class Routes
	{
		public static void MapMovementsEndpoint(this IEndpointRouteBuilder routes)
		{
			routes.MapGet("/movements", (Contracts movementServices) =>
			{
				return Results.Ok(movementServices.GetMovementsAsync());
			});
			routes.MapGet("/movements/{id}", (int id, Contracts movementServices) =>
			{
				return Results.Ok(movementServices.GetMovementAsync(id));
			});
			routes.MapPost("/movements/new", (NewMovementModel request, Contracts movementServices) =>
			{
				return Results.Ok(movementServices.CreateMovementAsync(request));
			});
			routes.MapPut("/movements/{id}", (int id, NewMovementModel request, Contracts movementServices) =>
			{
				return Results.Ok(movementServices.UpdateMovementAsync(id, request));
			});
			routes.MapDelete("/movements/{id}", (int id, Contracts movementServices) =>
			{
				return Results.Ok(movementServices.DeleteMovementAsync(id));
			});
		}
	}
}

