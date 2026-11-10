namespace Domain.Amenities;

public class Amenity
{
    public Guid Id { get; }
    public string Name { get; private set; }
    public DateTime CreatedAt { get; }

    private Amenity(Guid id, string name, DateTime createdAt)
    {
        Id = id;
        Name = name;
        CreatedAt = createdAt;
    }

    public static Amenity New(Guid id, string name) => new(id, name, DateTime.UtcNow);
}