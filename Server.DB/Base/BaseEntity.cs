namespace MyChat.Server.DB.Base;

/// <summary>
/// base class for all database entities
/// </summary>
public class BaseEntity
{
    public int Id { get; set; }

    public DateTime CreationDate { get; set; }
    
    public DateTime? EditionDate { get; set; }

    public bool IsDeleted { get; set; }
    
    public DateTime? DeletedDate { get; set; }
}