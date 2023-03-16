using System;
using devsuApi.Context;

namespace devsuApi.Modules.Accounts
{
	public static class Routes
	{
		public static void MapAccountsEndpoints(this IEndpointRouteBuilder routes)
		{
			routes.MapGet("/accounts", (Contracts accountServices) =>
			{
				return Results.Ok(accountServices.GetAccountsAsync());
			});
			routes.MapGet("/accounts/{id}", (int id, Contracts accountServices) =>
			{
				return Results.Ok(accountServices.GetAccountAsync(id));
			});
			routes.MapPost("/accounts/new", ( NewAccountModel request ,Contracts accountServices) =>
			{

				return Results.Ok(accountServices.CreateAccountAsync(request));
			});
			routes.MapPut("/accounts/{id}", (int id, NewAccountModel request, Contracts accountServices) =>
			{
				return Results.Ok(accountServices.UpdateAccountAsync(id, request));
			});
			routes.MapDelete("/accounts/{id}", (int id, Contracts accountServices) =>
			{
				return Results.Ok(accountServices.DeleteAccountAsync(id));
			});
		}
	}
}

