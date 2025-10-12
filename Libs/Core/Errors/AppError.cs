using FluentResults;

namespace Core.Errors;

public class AppError : Error
{
    public string Code { get; }

    public AppError(string code, string message) : base(message)
    {
        Code = code;
        Metadata["Error"] = this;
    }
    
    public AppError WithMessage(string message) => new AppError(this.Code, message);

    public static readonly AppError Unauthorized = new("Unauthorized", "Аутентификация не удалась");
    public static readonly AppError Validation = new("Validation", "Ошибка валидации данных");
    public static readonly AppError Conflict   = new("Conflict", "Объект уже существует");
    public static readonly AppError NotFound   = new("NotFound", "Объект не найден");
}