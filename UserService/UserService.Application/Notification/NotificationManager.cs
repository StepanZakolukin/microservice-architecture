using ConnectionLib.TaskTrackerService.Board;
using Core.Errors;
using FluentResults;
using UserService.Application.InterfaceRepositories;

namespace UserService.Application.Notification;

internal class NotificationManager : INotificationManager
{
    private readonly IUserRepository _userRepository;
    private readonly IBoardConnection _boardConnection;
    
    public NotificationManager(IUserRepository userRepository, IBoardConnection boardConnection)
    {
        _userRepository = userRepository;
        _boardConnection = boardConnection;
    }

    public async Task<Result<Guid>> CreateNotificationAsync(
        string text,
        Guid userId,
        Guid authenticatedUserId,
        CancellationToken cancellationToken)
    {
        if (userId != authenticatedUserId)
        {
            var checkForSharedBoardsResult = await _boardConnection.CheckForSharedBoards(
                userId,
                authenticatedUserId,
                cancellationToken);
            if (checkForSharedBoardsResult.IsFailed)
                return Result.Fail("Что то пошло не так, попробуйте повторить попытку");
            if (!checkForSharedBoardsResult.Value)
                return Result.Fail(AppError.Forbidden());
        }
        
        var user = await _userRepository.GetUserAsync(userId, cancellationToken);
        if (user is null)
            return Result.Fail(AppError.NotFound("Пользователь не найден."));
        
        var notification = user.AddNotification(text);
        await _userRepository.SaveChangesAsync(cancellationToken);
        
        return Result.Ok(notification.Id);
    }

    public async Task<Result> MarkAsReadAsync(Guid notificationId, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserAsync(notificationId, cancellationToken);
        if (user is null)
            return Result.Fail(AppError.NotFound("Пользователь не найден."));
        
        var notification = user.Notifications.FirstOrDefault(notification => notification.Id == notificationId);
        if (notification == null)
            return Result.Fail(AppError.NotFound("Уведомление не найдено"));
        
        notification.MarkAsRead();
        await _userRepository.SaveChangesAsync(cancellationToken);
        
        return Result.Ok();
    }

    public async Task<Result<IEnumerable<Domain.Entities.Notification>>> GetNotificationListAsync(Guid userId, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserAsync(userId, cancellationToken);
        return user is null
            ? Result.Fail(AppError.NotFound("Пользователь не найден."))
            : Result.Ok(user.Notifications);
    }

    public async Task<Result<IEnumerable<Domain.Entities.Notification>>> GetUnreadNotificationListAsync(Guid userId, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserAsync(
            userId,
            cancellationToken);

        if (user is null)
            return Result.Fail(AppError.NotFound("Пользователь не найден"));
        
        var result = user.Notifications
            .Where(notification => !notification.ReadIt)
            .OrderBy(notification => notification.Created)
            .AsEnumerable();
        
        return Result.Ok(result);
    }
}