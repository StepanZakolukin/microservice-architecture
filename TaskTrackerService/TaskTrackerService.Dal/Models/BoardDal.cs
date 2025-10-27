namespace TaskTrackerService.Dal.Models;

public class BoardDal : BaseDalModel<Guid>
{
    public BoardDal()
    {
        Id = Guid.NewGuid();
    }
    
    public required string Name { get; set; }
    
    private readonly List<BoardEditorDal> _editors = [];

    public IEnumerable<BoardEditorDal> Editors
    {
        get => _editors;
        init => _editors = value.ToList();
    }
    
    public readonly List<ColumnDal> _columns = [];

    public IEnumerable<ColumnDal> Columns
    {
        get => _columns;
        init
        {
            _columns = value.OrderBy(column => column.Number).ToList();
        }
    }
    
    public int ColumnCount => _columns.Count;
    
    public bool CheckEditorExists(Guid userId)
    {
        return Editors.Any(editor => editor.UserId == userId);
    }

    public bool TryMoveTask(TaskDal task, ColumnDal newColumn, int newNumber)
    {
        if (Columns.FirstOrDefault(column => column == newColumn) is null)
            throw new ArgumentException("Задачи можно перемещать только в рамках одной доски", nameof(newColumn));
        if (newNumber < 0) return false;
        
        var oldColumn = task.Column;
        if (oldColumn != newColumn)
        {
            if (newNumber > newColumn.TaskCount)
            {
                return false;
            }
        }
        else if (oldColumn.TaskCount >= newNumber)
        {
            return false;
        }
        oldColumn.RemoveTask(task);
        newColumn.AddTask(task, newNumber);
        
        return true;
    }

    public bool TryMoveColumn(Guid columnId, int newNumber)
    {
        if (newNumber < 0 || newNumber >= ColumnCount) return false;
        var column = _columns.FirstOrDefault(column => column.Id == columnId);
        if (!RemoveColumn(columnId)) return false;

        _columns.Insert(newNumber, column!);
        return true;
    }

    public BoardEditorDal AddEditor(Guid userId, string firstName, string lastName)
    {
        var editor = new BoardEditorDal
        {
            UserId = userId,
            FirstName = firstName,
            LastName = lastName,
            Board = this
        };
        _editors.Add(editor);
        
        return editor;
    }

    public bool RemoveEditor(Guid editorId)
    {
        var editorIndex = _editors.FindIndex(editor => editor.Id == editorId);
        
        if (editorIndex == -1)
            return false;
        
        _editors.RemoveAt(editorIndex);
        
        return true;
    }
    
    public ColumnDal AddColumn(string title)
    {
        var column = new ColumnDal
        {
            Title = title,
            Number = ColumnCount
        };
        _columns.Add(column);
        
        return column;
    }

    public bool RemoveColumn(Guid columnId)
    {
        var columnIndex = _columns.FindIndex(column => column.Id == columnId);
        
        if (columnIndex == -1)
            return false;
        
        _columns.RemoveAt(columnIndex);
        RestoreColumnNumbering();
        
        return true;
    }

    public ColumnDal? GetColumn(Guid columnId)
    {
        return _columns.FirstOrDefault(column => column.Id == columnId);
    }

    private void RestoreColumnNumbering()
    {
        for (var i = 0; i < ColumnCount; i++)
            _columns[i].Number = i;
    }
}