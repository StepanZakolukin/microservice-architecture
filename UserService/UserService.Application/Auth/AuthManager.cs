using Core.Errors;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using UserService.Application.Auth.Command;
using UserService.Application.Auth.Dto;
using UserService.Application.Auth.Interfaces;
using UserService.Application.InterfaceRepositories;

namespace UserService.Application.Auth;

internal class AuthManager : IAuthManager
{
    private readonly UserManager<Domain.Entities.User> _userManager;
    private readonly SignInManager<Domain.Entities.User> _signInManager;
    private readonly IAccessTokenGenerator _accessTokenGenerator;
    private readonly IRefreshTokenGenerator _refreshTokenGenerator;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    
    public AuthManager(UserManager<Domain.Entities.User> userManager,
        IRefreshTokenGenerator refreshTokenGenerator,
        IAccessTokenGenerator accessTokenGenerator,
        SignInManager<Domain.Entities.User> signInManager,
        IRefreshTokenRepository refreshTokenRepository)
    {
        _userManager = userManager;
        _refreshTokenGenerator =  refreshTokenGenerator;
        _accessTokenGenerator = accessTokenGenerator;
        _signInManager = signInManager;
        _refreshTokenRepository = refreshTokenRepository;
    }

    public async Task<Result<RegisterInfoResponse>> RegisterAsync(RegisterCommand info, CancellationToken cancellationToken)
    {
        var user = new Domain.Entities.User
        {
            Email = info.Email, FirstName = info.FirstName, LastName = info.LastName, UserName = info.Email
        };
        
        var result = await _userManager.CreateAsync(user, info.Password);
        if (!result.Succeeded)
            return Result.Fail(AppError.Validation(result.Errors.First().Description));
        
        var refreshToken = await _refreshTokenGenerator.GenerateAsync(user.Id, cancellationToken);
        var claims = await _signInManager.CreateUserPrincipalAsync(user);
        var accessToken = _accessTokenGenerator.Generate(claims.Claims);

        return Result.Ok(new RegisterInfoResponse
        {
            UserId = user.Id, RefreshToken = refreshToken.Token, AccessToken = accessToken
        });
    }

    public async Task<Result> LogoutAsync(string refreshToken, CancellationToken cancellationToken)
    {
        var token = await _refreshTokenRepository.GetAsync(refreshToken, cancellationToken);

        if (token is not null)
        {
            token.IsRevoked = true;
            await _refreshTokenRepository.SaveChangesAsync(cancellationToken);
        }
        
        return Result.Ok();
    }

    public async Task<Result<LoginInfoResponse>> LoginAsync(string email, string password, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null || !await _userManager.CheckPasswordAsync(user, password))
            return Result.Fail(AppError.Unauthorized("Неверный email или password"));
        
        var accessToken = await GenerateAccessToken(user);
        var refreshToken = await _refreshTokenGenerator.GenerateAsync(user.Id, cancellationToken);

        return Result.Ok(new LoginInfoResponse
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken.Token,
        });
    }

    private async Task<string> GenerateAccessToken(Domain.Entities.User user)
    {
        var principal = await _signInManager.CreateUserPrincipalAsync(user);
        return _accessTokenGenerator.Generate(principal.Claims);
    }

    public async Task<Result<string>> RefreshTokenAsync(string refreshToken, CancellationToken cancellationToken)
    {
        var token = await _refreshTokenRepository.GetAsync(refreshToken, cancellationToken);

        if (token is null || !token.IsActive)
            return Result.Fail(AppError.Unauthorized("Неверный или отозванный refresh token."));

        var user = await _userManager.FindByIdAsync($"{token.UserId}");
        var accessToken = await GenerateAccessToken(user);
        
        return Result.Ok(accessToken);
    }
}