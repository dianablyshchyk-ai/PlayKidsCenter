namespace Domain.PlayRooms;

public class PlayRoom
{
    public PlayRoomId Id { get; }
    public string RoomNumber { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public decimal PricePerHour { get; private set; }
    public int Capacity { get; private set; }
    public bool HasAnimator { get; private set; }
    public PlayRoomTheme Theme { get; private set; }
    public PlayRoomAttributes Attributes { get; private set; }
    public DateTime CreatedAt { get; }
    public DateTime? UpdatedAt { get; private set; }

    public ICollection<PlayRoomAmenity> Amenities { get; } = [];
    public ICollection<PlayRoomImage> Images { get; } = [];

    private PlayRoom()
    {
        Id = null!;
        RoomNumber = null!;
        Name = null!;
        Description = null!;
        Attributes = PlayRoomAttributes.Empty();
    }

    private PlayRoom(
        PlayRoomId id,
        string roomNumber,
        string name,
        string description,
        decimal pricePerHour,
        int capacity,
        bool hasAnimator,
        PlayRoomTheme theme,
        PlayRoomAttributes attributes,
        DateTime createdAt)
    {
        Id = id;
        RoomNumber = roomNumber;
        Name = name;
        Description = description;
        PricePerHour = pricePerHour;
        Capacity = capacity;
        HasAnimator = hasAnimator;
        Theme = theme;
        Attributes = attributes;
        CreatedAt = createdAt;
    }

    public static PlayRoom New(
        PlayRoomId id,
        string roomNumber,
        string name,
        string description,
        decimal pricePerHour,
        int capacity,
        bool hasAnimator,
        PlayRoomTheme theme,
        PlayRoomAttributes? attributes = null) => new(
            id,
            roomNumber,
            name,
            description,
            pricePerHour,
            capacity,
            hasAnimator,
            theme,
            attributes ?? PlayRoomAttributes.Empty(),
            DateTime.UtcNow);

    public void UpdateDetails(
        string roomNumber,
        string name,
        string description,
        decimal pricePerHour,
        int capacity,
        bool hasAnimator,
        PlayRoomTheme theme,
        PlayRoomAttributes? attributes = null)
    {
        RoomNumber = roomNumber;
        Name = name;
        Description = description;
        PricePerHour = pricePerHour;
        Capacity = capacity;
        HasAnimator = hasAnimator;
        Theme = theme;
        Attributes = attributes ?? PlayRoomAttributes.Empty();
        UpdatedAt = DateTime.UtcNow;
    }
}