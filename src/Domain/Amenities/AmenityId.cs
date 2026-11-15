namespace Domain.Amenities;

public record AmenityId(Guid Value)
{
    public static AmenityId Empty() => new(Guid.Empty);
    public static AmenityId New() => new(Guid.NewGuid());
    public override string ToString() => Value.ToString();
}