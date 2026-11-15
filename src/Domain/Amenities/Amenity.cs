namespace Domain.Amenities;

public class Amenity
{
    public AmenityId Id { get; }
    public string Name { get; private set; }
    public DateTime CreatedAt { get; }

    private Amenity()
    {
        Id = null!;
        Name = null!;
    }

    private Amenity(AmenityId id, string name, DateTime createdAt)
    {
        Id = id;
        Name = name;
        CreatedAt = createdAt;
    }

    public static Amenity New(AmenityId id, string name) => new(id, name, DateTime.UtcNow);
}