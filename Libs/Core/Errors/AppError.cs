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

    public static AppError Forbidden(string message = "Доступ запрещен") => new("Forbidden", message);
    public static AppError Unauthorized(string message = "Аутентификация не удалась") => new("Unauthorized", message);
    public static AppError Validation(string message = "Ошибка валидации данных") => new("Validation", message);
    public static AppError Conflict(string message = "Объект уже существует") => new("Conflict", message);
    public static AppError NotFound(string message = "Объект не найден") => new("NotFound", message);
}