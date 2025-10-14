namespace TaskTrackerService.Dal.Models;

public class ColumnDal : BaseDalModel<Guid>
{
    public required string Title { get; set; }
    public required int Number { get; set; }
    public required Guid BoardId { get; init; }
    private List<TaskDal> _tasks;
    public IEnumerable<TaskDal> Tasks => _tasks;
    
    public int Count => _tasks.Count;

    public void AddTask(TaskDal task, int position = 0)
    {
        _tasks = _tasks.OrderBy(currentTask => currentTask.Number).ToList();
        _tasks.Insert(position, task);
        RestoreTaskNumbering();
    }

    public void RemoveTask(TaskDal task)
    {
        _tasks.Remove(task);
        RestoreTaskNumbering();
    }

    private void RestoreTaskNumbering()
    {
        for (var i = 0; i < _tasks.Count; i++)
            _tasks[i].Number = i;
    }
    
    public ColumnDal()
    {
        Id = Guid.NewGuid();
    }
}