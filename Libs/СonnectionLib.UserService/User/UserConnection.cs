using Core.HttpLogic.Dto;
using Core.HttpLogic.Services.Interfaces;
using FluentResults;
using Microsoft.AspNetCore.Http;

namespace СonnectionLib.UserService.User;

internal class UserConnection : IUserConnection
{
    private const string BaseUrl = "https://localhost:7005";
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IHttpRequestService _httpRequestService;

    public UserConnection(IHttpRequestService httpRequestService, IHttpContextAccessor httpContextAccessor)
    {
        _httpRequestService = httpRequestService;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<Result<UserDto>> GetUserAsync(Guid userId, CancellationToken cancellationToken)
    {
        var requestData = new HttpRequestData
        {
            Method = HttpMethod.Get,
            Uri = CombineUri(BaseUrl, $"api/users/{userId}")
        };
        requestData.HeaderDictionary.Add("Authorization", $"{GetJwtToken()}");

        var connectionData = new HttpConnectionData
        {
            ClientName = "TaskTracker",
            CancellationToken = cancellationToken
        };

        var response = await _httpRequestService.SendRequestAsync<UserDto>(requestData, connectionData);
        
        return response.IsSuccessStatusCode 
            ? Result.Ok(response.Body!)
            : Result.Fail("При обращении в микросервис управления пользователями произошла ошибка.");
    }

    private string? GetJwtToken()
    {
        return _httpContextAccessor.HttpContext!.Request.Headers.Authorization.FirstOrDefault();
    }
    
    private string CombineUri(string baseUri, string relativeUri)
    {
        return new Uri(new Uri(baseUri), relativeUri).ToString();
    }
}