using TaskTrackerService.Dal.Models;
using TaskTrackerService.Logic.Board.Models.Response;

namespace TaskTrackerService.Logic;

public static class ColumnDalExtension
{
    public static ColumnResponse ConvertToColumnResponse(this ColumnDal column)
    {
        return new ColumnResponse
        {
            Id = column.Id,
            Title = column.Title,
            Tasks = column.Tasks.Select(task => task.ConvertToTaskResult()).ToList()
        };
    }
}