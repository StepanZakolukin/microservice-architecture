using Core.Errors;
using FluentResults;
using IdentityService.Api.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using UserService.Application.Command;
using UserService.Application.Dto;
using UserService.Application.Interfaces;
using UserService.Domain.Interfaces;
using UserService.Domain.Models;

namespace UserService.Application.Services;

internal class AuthService : IAuthService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IAccessTokenGenerator _accessTokenGenerator;
    private readonly IRefreshTokenGenerator _refreshTokenGenerator;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    
    public AuthService(UserManager<User> userManager, IRefreshTokenGenerator refreshTokenGenerator, IAccessTokenGenerator accessTokenGenerator, SignInManager<User> signInManager, IRefreshTokenRepository refreshTokenRepository)
    {
        _userManager = userManager;
        _refreshTokenGenerator =  refreshTokenGenerator;
        _accessTokenGenerator = accessTokenGenerator;
        _signInManager = signInManager;
        _refreshTokenRepository = refreshTokenRepository;
    }

    public async Task<Result<RegisterInfoResponse>> RegisterAsync(RegisterCommand info, CancellationToken cancellationToken)
    {
        var user = new User
        {
            Email = info.Email, FirstName = info.FirstName, LastName = info.LastName, UserName = info.Email
        };
        
        var result = await _userManager.CreateAsync(user, info.Password);
        if (!result.Succeeded)
            return Result.Fail<RegisterInfoResponse>(AppError.Validation(result.Errors.First().Description));
        
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

    private async Task<string> GenerateAccessToken(User user)
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