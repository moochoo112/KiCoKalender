﻿using Domain;
using Attributes;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Service.Interfaces;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace KiCoKalender_v2
{
    public class UserController
    {
		ILogger Logger { get; }
		IUserService UserService { get; }

		public UserController(ILogger<UserController> Logger, IUserService UserService)
		{
			this.Logger = Logger;
			this.UserService = UserService;
		}

		[Function(nameof(UserController.FindUserById))]
		//[UserAuth]
		[OpenApiOperation(operationId: "FindUserById", tags: new[] { "user" }, Summary = "Find user by ID", Description = "Returns a user by ID.", Visibility = OpenApiVisibilityType.Important)]
		//[OpenApiSecurity("petstore_auth", SecuritySchemeType.Http, In = OpenApiSecurityLocationType.Header, Scheme = OpenApiSecuritySchemeType.Bearer, BearerFormat = "JWT")]
		[OpenApiParameter(name: "userId", In = ParameterLocation.Path, Required = true, Type = typeof(long?), Summary = "ID of user to return", Description = "ID of user to return", Visibility = OpenApiVisibilityType.Important)]
		[OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(User), Summary = "successful operation", Description = "successful operation", Example = typeof(DummyUserExample))]
		[OpenApiResponseWithoutBody(statusCode: HttpStatusCode.BadRequest, Summary = "Invalid ID supplied", Description = "Invalid ID supplied")]
		[OpenApiResponseWithoutBody(statusCode: HttpStatusCode.NotFound, Summary = "User not found", Description = "User not found")]
		//[UnauthorizedResponse]
		//[ForbiddenResponse]
		public async Task<HttpResponseData> FindUserById(
			[HttpTrigger(AuthorizationLevel.Function, "GET", Route = "user/{userId}")]
			HttpRequestData req,
			long? userId,
			FunctionContext executionContext)
		{
			// Generate output
			HttpResponseData response = req.CreateResponse(HttpStatusCode.OK);

			if (!userId.HasValue)
			{
				response = req.CreateResponse(HttpStatusCode.BadRequest);
			}
			else
			{
			await response.WriteAsJsonAsync(UserService.FindUserById(userId));
			}

			return response;
		}


		[Function(nameof(UserController.FindByName))]
		[OpenApiOperation(operationId: "FindByName", tags: new[] { "user" }, Summary = "Finds User by name", Description = "Returns a user by name.", Visibility = OpenApiVisibilityType.Important)]
		//[OpenApiSecurity("petstore_auth", SecuritySchemeType.Http, In = OpenApiSecurityLocationType.Header, Scheme = OpenApiSecuritySchemeType.Bearer, BearerFormat = "JWT")]
		[OpenApiParameter(name: "userName", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "Name of user to return", Description = "Name of user to return", Visibility = OpenApiVisibilityType.Important)]
		[OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(User), Summary = "successful operation", Description = "successful operation", Example = typeof(DummyUserExample))]
		[OpenApiResponseWithoutBody(statusCode: HttpStatusCode.BadRequest, Summary = "Invalid name value", Description = "Invalid name value")]
		[OpenApiResponseWithoutBody(statusCode: HttpStatusCode.NotFound, Summary = "User not found", Description = "User not found")]
		[OpenApiResponseWithoutBody(statusCode: HttpStatusCode.MethodNotAllowed, Summary = "Validation exception", Description = "Validation exception")]
		public async Task<HttpResponseData> FindByName(
			[HttpTrigger(AuthorizationLevel.Function, "GET", Route = "user/FindByName")]
			HttpRequestData req,
			string userName,
			FunctionContext executionContext)
		{
			// Generate output
			HttpResponseData response = req.CreateResponse(HttpStatusCode.OK);

			if (string.IsNullOrEmpty(userName))
			{
				response = req.CreateResponse(HttpStatusCode.BadRequest);
			}
			else
			{
				await response.WriteAsJsonAsync(UserService.FindUserByName(userName));
			}

			return response;
		}

		[Function(nameof(UserController.AddUser))]
		[OpenApiOperation(operationId: "addUser", tags: new[] { "user" }, Summary = "Add a user to the KiCoKalender", Description = "This adds a new user to the KiCoKalender.", Visibility = OpenApiVisibilityType.Important)]
		//[OpenApiSecurity("petstore_auth", SecuritySchemeType.Http, In = OpenApiSecurityLocationType.Header, Scheme = OpenApiSecuritySchemeType.Bearer, BearerFormat = "JWT")]
		[OpenApiRequestBody(contentType: "application/json", bodyType: typeof(User), Required = true, Description = "User object that needs to be added to the KiCoKalender")]
		[OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(User), Summary = "New user added", Description = "New user added", Example = typeof(DummyUserExample))]
		[OpenApiResponseWithoutBody(statusCode: HttpStatusCode.BadRequest, Summary = "Invalid input", Description = "Invalid input")]
		[OpenApiResponseWithoutBody(statusCode: HttpStatusCode.MethodNotAllowed, Summary = "Validation exception", Description = "Validation exception")]
		public async Task<HttpResponseData> AddUser(
			[HttpTrigger(AuthorizationLevel.Function, "POST", Route = "user")]
			HttpRequestData req,
			FunctionContext executionContext)
		{
			// Parse input
			string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
			User user = JsonConvert.DeserializeObject<User>(requestBody);

			// Generate output
			HttpResponseData response = req.CreateResponse(HttpStatusCode.OK);

			if (user is null)
			{
				response = req.CreateResponse(HttpStatusCode.BadRequest);
			}
			else
			{
				UserService.AddUser(user);
			}

			await response.WriteAsJsonAsync(user);

			return response;
		}

		[Function(nameof(UserController.UpdateUser))]
		[OpenApiOperation(operationId: "updateUser", tags: new[] { "user" }, Summary = "Update an existing user", Description = "This updates an existing user.", Visibility = OpenApiVisibilityType.Important)]
		//[OpenApiSecurity("petstore_auth", SecuritySchemeType.Http, In = OpenApiSecurityLocationType.Header, Scheme = OpenApiSecuritySchemeType.Bearer, BearerFormat = "JWT")]
		[OpenApiRequestBody(contentType: "application/json", bodyType: typeof(User), Required = true, Description = "User object that needs to be updated")]
		[OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(User), Summary = "User details updated", Description = "User details updated", Example = typeof(DummyUserExample))]
		[OpenApiResponseWithoutBody(statusCode: HttpStatusCode.BadRequest, Summary = "Invalid ID supplied", Description = "Invalid ID supplied")]
		[OpenApiResponseWithoutBody(statusCode: HttpStatusCode.NotFound, Summary = "User not found", Description = "User not found")]
		[OpenApiResponseWithoutBody(statusCode: HttpStatusCode.MethodNotAllowed, Summary = "Validation exception", Description = "Validation exception")]
		public async Task<HttpResponseData> UpdateUser(
			[HttpTrigger(AuthorizationLevel.Function, "PUT", Route = "user")]
			HttpRequestData req,
			FunctionContext executionContext)
		{
			// Parse input
			string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
			User user = JsonConvert.DeserializeObject<User>(requestBody);

			// Generate output
			HttpResponseData response = req.CreateResponse(HttpStatusCode.OK);

			if (user is null)
			{
				response = req.CreateResponse(HttpStatusCode.BadRequest);
			}
			else
			{
				UserService.UpdateUser(user);
			}

			await response.WriteAsJsonAsync(user);

			return response;
		}

		[Function(nameof(UserController.DeleteUser))]
		[OpenApiOperation(operationId: "deleteUser", tags: new[] { "user" }, Summary = "Deletes a user from the KiCoKalender", Description = "Deletes a user from the KiCoKalender.", Visibility = OpenApiVisibilityType.Important)]
		[OpenApiParameter(name: "userId", In = ParameterLocation.Path, Required = true, Type = typeof(long?), Summary = "ID of user to delete", Description = "ID of user to delete", Visibility = OpenApiVisibilityType.Important)]
		[OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(User), Summary = "New user Delete", Description = "user deleted")]
		[OpenApiResponseWithoutBody(statusCode: HttpStatusCode.BadRequest, Summary = "Invalid input", Description = "Invalid input")]
		[OpenApiResponseWithoutBody(statusCode: HttpStatusCode.NotFound, Summary = "User not found", Description = "User not found")]
		[OpenApiResponseWithoutBody(statusCode: HttpStatusCode.MethodNotAllowed, Summary = "Validation exception", Description = "Validation exception")]
		public async Task<HttpResponseData> DeleteUser(
			[HttpTrigger(AuthorizationLevel.Function,
			"DELETE", Route = "user/{userId}")]
			HttpRequestData req,
			long? userId,
			FunctionContext executionContext)
		{

			// Generate output
			HttpResponseData response = req.CreateResponse(HttpStatusCode.OK);

			if (!userId.HasValue)
			{
				response = req.CreateResponse(HttpStatusCode.BadRequest);
			}
			else
			{
				UserService.DeleteUser(userId);
			}

			return response;
		}
	}
}
