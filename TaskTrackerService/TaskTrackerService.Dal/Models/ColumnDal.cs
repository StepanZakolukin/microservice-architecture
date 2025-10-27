namespace TaskTrackerService.Dal.Models;

public class ColumnDal : BaseDalModel<Guid>
{
    public ColumnDal()
    {
        Id = Guid.NewGuid();
    }
    
    public required string Title { get; set; }
    public int Number { get; internal set; }
    
    private readonly List<TaskDal> _tasks;

    public IEnumerable<TaskDal> Tasks
    {
        get => _tasks;
        init
        {
            _tasks = value.OrderBy(task => task.Number).ToList();
        }
    }
    
    public int TaskCount => _tasks.Count;

    public Guid BoardId { get; set; }

    private readonly BoardDal _board;
    public BoardDal Board
    {
        get => _board;
        init => _board = value ??  throw new ArgumentException("Попытка присвоить пустое значение", nameof(value));
    }

    public void AddTask(TaskDal task, int position = 0)
    {
        if (position < 0) throw new ArgumentException("Должен быть больше 0", nameof(position));
        
        task.Column = this;
        _tasks.Insert(position, task);
        RestoreTaskNumbering();
    }

    public void RemoveTask(TaskDal task)
    {
        task.Column = null!;
        _tasks.Remove(task);
        RestoreTaskNumbering();
    }

    private void RestoreTaskNumbering()
    {
        for (var i = 0; i < _tasks.Count; i++)
            _tasks[i].Number = i;
    }
}