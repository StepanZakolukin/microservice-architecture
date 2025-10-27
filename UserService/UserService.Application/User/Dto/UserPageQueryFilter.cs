namespace UserService.Application.User.Dto;

public record UserPageQueryFilter
{
    public string? Email { get; init; }
    
    public required int Page { get; init; }
    
    public required int PageSize { get; init; }
}