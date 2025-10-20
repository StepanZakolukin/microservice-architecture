using System.Security.Cryptography;
using UserService.Application.Auth.Interfaces;
using UserService.Domain.Entities;
using UserService.Domain.Interfaces;

namespace UserService.Application.Auth;

internal class RefreshTokenGenerator : IRefreshTokenGenerator
{
    private readonly IRefreshTokenRepository _repository;
    
    public RefreshTokenGenerator(IRefreshTokenRepository repository)
    {
        _repository = repository;
    }

    public async Task<RefreshToken> GenerateAsync(Guid userId, CancellationToken cancellationToken)
    {
        var token = new RefreshToken
        {
            Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
            UserId = userId,
            Expires = DateTime.UtcNow.AddDays(7)
        };

        await _repository.AddAsync(token,cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);
        return token;
    }
}