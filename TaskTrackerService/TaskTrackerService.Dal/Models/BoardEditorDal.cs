namespace TaskTrackerService.Dal.Models;

public class BoardEditorDal : BaseDalModel<Guid>
{
    public BoardEditorDal()
    {
        Id = Guid.NewGuid();
    }
    
    public required Guid UserId { get; init; }
    
    private string _firstName;

    public required string FirstName
    {
        get => _firstName;
        set => _firstName = value ?? throw new ArgumentNullException(nameof(value), $"Попытка установить пустое значение");
    }

    private string _lastName; 
    public required string LastName
    {
        get => _lastName;
        set => _lastName = value ?? throw new ArgumentNullException(nameof(value), $"Попытка установить пустое значение");
    }

    public Guid BoardId { get; set; }
    
    private readonly BoardDal _boardDal;

    public BoardDal Board
    {
        get => _boardDal;
        internal init => _boardDal = value; //TODO: поработать над целостностью
    }
}