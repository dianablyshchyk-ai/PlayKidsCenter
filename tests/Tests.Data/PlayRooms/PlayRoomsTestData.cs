using Domain.PlayRooms;

namespace Tests.Data.PlayRooms;
public static class PlayRoomsTestData
{
    public static PlayRoom FirstTestPlayRoom() => PlayRoom.New(
        id: PlayRoomId.New(),
        roomNumber: "R-101",
        name: "First test playroom",
        description: "Cozy room for kids",
        pricePerHour: 100m,
        capacity: 10,
        hasAnimator: true,
        theme: (PlayRoomTheme)1,
        attributes: new PlayRoomAttributes(40m, 3, 8, "No shoes"));

    public static PlayRoom SecondTestPlayRoom() => PlayRoom.New(
        id: PlayRoomId.New(),
        roomNumber: "R-102",
        name: "Second test playroom",
        description: "Adventure room with slide",
        pricePerHour: 200m,
        capacity: 15,
        hasAnimator: false,
        theme: (PlayRoomTheme)1,
        attributes: new PlayRoomAttributes(55m, 4, 12, "Socks required"));

    public static PlayRoom ThirdTestPlayRoom() => PlayRoom.New(
        id: PlayRoomId.New(),
        roomNumber: "R-103",
        name: "Third test playroom",
        description: "VIP party room",
        pricePerHour: 300m,
        capacity: 20,
        hasAnimator: true,
        theme: (PlayRoomTheme)1,
        attributes: new PlayRoomAttributes(75m, 5, 14, "Cake allowed"));
}