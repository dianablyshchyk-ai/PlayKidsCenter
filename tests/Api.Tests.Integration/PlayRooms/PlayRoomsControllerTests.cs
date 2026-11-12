using System.Net;
using System.Net.Http.Json;
using Api.Dtos;
using Domain.PlayRooms;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Tests.Common;
using Tests.Data.PlayRooms;
using Xunit;

namespace Api.Tests.Integration.PlayRooms;

public class PlayRoomsControllerTests : BaseIntegrationTest, IAsyncLifetime
{
    private readonly PlayRoom _firstTestPlayRoom;
    private readonly PlayRoom _secondTestPlayRoom;
    private readonly PlayRoom _thirdTestPlayRoom;

    private readonly string _createPlayRoomRoute;
    private readonly string _updatePlayRoomRoute;

    public PlayRoomsControllerTests(IntegrationTestWebFactory factory) : base(factory)
    {
        _firstTestPlayRoom = PlayRoomsTestData.FirstTestPlayRoom();
        _secondTestPlayRoom = PlayRoomsTestData.SecondTestPlayRoom();
        _thirdTestPlayRoom = PlayRoomsTestData.ThirdTestPlayRoom();

        _createPlayRoomRoute = "playrooms";
        _updatePlayRoomRoute = $"playrooms/{_secondTestPlayRoom.Id}";
    }

    [Fact]
    public async Task ShouldCreatePlayRoom()
    {
        // Arrange
        var request = new CreatePlayRoomDto(
            RoomNumber: _firstTestPlayRoom.RoomNumber,
            Name: _firstTestPlayRoom.Name,
            Description: _firstTestPlayRoom.Description,
            PricePerHour: _firstTestPlayRoom.PricePerHour,
            Capacity: _firstTestPlayRoom.Capacity,
            HasAnimator: _firstTestPlayRoom.HasAnimator,
            Theme: _firstTestPlayRoom.Theme,
            Attributes: new PlayRoomAttributesDto(
                _firstTestPlayRoom.Attributes.AreaSqMeters,
                _firstTestPlayRoom.Attributes.MinRecommendedAge,
                _firstTestPlayRoom.Attributes.MaxRecommendedAge,
                _firstTestPlayRoom.Attributes.ParentNotes));

        // Act
        var response = await Client.PostAsJsonAsync(_createPlayRoomRoute, request);

        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();

        var responseDto = await response.Content.ReadFromJsonAsync<PlayRoomDto>();
        responseDto.Should().NotBeNull();

        var dbPlayRoom = await Context.PlayRooms.FirstOrDefaultAsync(x => x.Id == new PlayRoomId(responseDto!.Id));
        dbPlayRoom.Should().NotBeNull();
        dbPlayRoom!.RoomNumber.Should().Be(_firstTestPlayRoom.RoomNumber);
        dbPlayRoom.Name.Should().Be(_firstTestPlayRoom.Name);
        dbPlayRoom.Description.Should().Be(_firstTestPlayRoom.Description);
        dbPlayRoom.PricePerHour.Should().Be(_firstTestPlayRoom.PricePerHour);
        dbPlayRoom.Capacity.Should().Be(_firstTestPlayRoom.Capacity);
        dbPlayRoom.HasAnimator.Should().Be(_firstTestPlayRoom.HasAnimator);
    }

    [Fact]
    public async Task ShouldNotCreatePlayRoomBecauseNameAlreadyTaken()
    {
        // Arrange
        var request = new CreatePlayRoomDto(
            RoomNumber: _secondTestPlayRoom.RoomNumber,
            Name: _secondTestPlayRoom.Name,
            Description: _secondTestPlayRoom.Description,
            PricePerHour: _secondTestPlayRoom.PricePerHour,
            Capacity: _secondTestPlayRoom.Capacity,
            HasAnimator: _secondTestPlayRoom.HasAnimator,
            Theme: _secondTestPlayRoom.Theme,
            Attributes: new PlayRoomAttributesDto(
                _secondTestPlayRoom.Attributes.AreaSqMeters,
                _secondTestPlayRoom.Attributes.MinRecommendedAge,
                _secondTestPlayRoom.Attributes.MaxRecommendedAge,
                _secondTestPlayRoom.Attributes.ParentNotes));

        // Act
        var response = await Client.PostAsJsonAsync(_createPlayRoomRoute, request);

        // Assert
        response.IsSuccessStatusCode.Should().BeFalse();
        response.StatusCode.Should().Be(HttpStatusCode.Conflict);

        var dbPlayRoom = await Context.PlayRooms.SingleAsync(x => x.Name == _secondTestPlayRoom.Name);
        dbPlayRoom.Id.Should().Be(_secondTestPlayRoom.Id);
    }

    [Fact]
    public async Task ShouldNotCreatePlayRoomBecauseRequestIsInvalid()
    {
        // Arrange
        var request = new CreatePlayRoomDto(
            RoomNumber: string.Empty,
            Name: string.Empty,
            Description: string.Empty,
            PricePerHour: -10m,
            Capacity: 0,
            HasAnimator: false,
            Theme: (PlayRoomTheme)1,
            Attributes: null);

        // Act
        var response = await Client.PostAsJsonAsync(_createPlayRoomRoute, request);

        // Assert
        response.IsSuccessStatusCode.Should().BeFalse();
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var count = await Context.PlayRooms.CountAsync();
        count.Should().Be(1);
    }

    [Fact]
    public async Task ShouldUpdatePlayRoom()
    {
        // Arrange
        var request = new UpdatePlayRoomDto(
            RoomNumber: _thirdTestPlayRoom.RoomNumber,
            Name: _thirdTestPlayRoom.Name,
            Description: _thirdTestPlayRoom.Description,
            PricePerHour: _thirdTestPlayRoom.PricePerHour,
            Capacity: _thirdTestPlayRoom.Capacity,
            HasAnimator: _thirdTestPlayRoom.HasAnimator,
            Theme: _thirdTestPlayRoom.Theme,
            Attributes: new PlayRoomAttributesDto(
                _thirdTestPlayRoom.Attributes.AreaSqMeters,
                _thirdTestPlayRoom.Attributes.MinRecommendedAge,
                _thirdTestPlayRoom.Attributes.MaxRecommendedAge,
                _thirdTestPlayRoom.Attributes.ParentNotes));

        // Act
        var response = await Client.PutAsJsonAsync(_updatePlayRoomRoute, request);

        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();

        var responseDto = await response.Content.ReadFromJsonAsync<PlayRoomDto>();
        responseDto.Should().NotBeNull();
        responseDto!.Id.Should().Be(_secondTestPlayRoom.Id.Value);

        var dbPlayRoom = await Context.PlayRooms.FirstOrDefaultAsync(x => x.Id == _secondTestPlayRoom.Id);
        dbPlayRoom.Should().NotBeNull();
        dbPlayRoom!.RoomNumber.Should().Be(_thirdTestPlayRoom.RoomNumber);
        dbPlayRoom.Name.Should().Be(_thirdTestPlayRoom.Name);
        dbPlayRoom.Description.Should().Be(_thirdTestPlayRoom.Description);
        dbPlayRoom.PricePerHour.Should().Be(_thirdTestPlayRoom.PricePerHour);
        dbPlayRoom.Capacity.Should().Be(_thirdTestPlayRoom.Capacity);
        dbPlayRoom.HasAnimator.Should().Be(_thirdTestPlayRoom.HasAnimator);
    }

    [Fact]
    public async Task ShouldNotUpdatePlayRoomBecauseNameAlreadyTaken()
    {
        // Arrange
        await Context.PlayRooms.AddAsync(_firstTestPlayRoom);
        await Context.SaveChangesAsync();
        Context.ChangeTracker.Clear();

        var request = new UpdatePlayRoomDto(
            RoomNumber: _firstTestPlayRoom.RoomNumber,
            Name: _firstTestPlayRoom.Name,
            Description: _thirdTestPlayRoom.Description,
            PricePerHour: _thirdTestPlayRoom.PricePerHour,
            Capacity: _thirdTestPlayRoom.Capacity,
            HasAnimator: _thirdTestPlayRoom.HasAnimator,
            Theme: _thirdTestPlayRoom.Theme,
            Attributes: null);

        // Act
        var response = await Client.PutAsJsonAsync(_updatePlayRoomRoute, request);

        // Assert
        response.IsSuccessStatusCode.Should().BeFalse();
        response.StatusCode.Should().Be(HttpStatusCode.Conflict);

        var firstDbPlayRoom = await Context.PlayRooms.SingleAsync(x => x.Id == _firstTestPlayRoom.Id);
        firstDbPlayRoom.Name.Should().Be(_firstTestPlayRoom.Name);

        var secondDbPlayRoom = await Context.PlayRooms.SingleAsync(x => x.Id == _secondTestPlayRoom.Id);
        secondDbPlayRoom.Name.Should().Be(_secondTestPlayRoom.Name);
    }

    [Fact]
    public async Task ShouldNotUpdatePlayRoomBecausePlayRoomNotFound()
    {
        // Arrange
        var nonExistentRoute = $"playrooms/{Guid.NewGuid()}";
        var request = new UpdatePlayRoomDto(
            RoomNumber: _thirdTestPlayRoom.RoomNumber,
            Name: _thirdTestPlayRoom.Name,
            Description: _thirdTestPlayRoom.Description,
            PricePerHour: _thirdTestPlayRoom.PricePerHour,
            Capacity: _thirdTestPlayRoom.Capacity,
            HasAnimator: _thirdTestPlayRoom.HasAnimator,
            Theme: _thirdTestPlayRoom.Theme,
            Attributes: null);

        // Act
        var response = await Client.PutAsJsonAsync(nonExistentRoute, request);

        // Assert
        response.IsSuccessStatusCode.Should().BeFalse();
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task ShouldDeletePlayRoom()
    {
        // Act
        var response = await Client.DeleteAsync(_updatePlayRoomRoute);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);

        var dbPlayRoom = await Context.PlayRooms.FirstOrDefaultAsync(x => x.Id == _secondTestPlayRoom.Id);
        dbPlayRoom.Should().BeNull();
    }

    [Fact]
    public async Task ShouldNotDeletePlayRoomBecausePlayRoomNotFound()
    {
        // Arrange
        var nonExistentRoute = $"playrooms/{Guid.NewGuid()}";

        // Act
        var response = await Client.DeleteAsync(nonExistentRoute);

        // Assert
        response.IsSuccessStatusCode.Should().BeFalse();
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);

        var dbPlayRoom = await Context.PlayRooms.FirstOrDefaultAsync(x => x.Id == _secondTestPlayRoom.Id);
        dbPlayRoom.Should().NotBeNull();
    }

    public async Task InitializeAsync()
    {
        await Context.PlayRooms.AddAsync(_secondTestPlayRoom);

        await Context.SaveChangesAsync();
        Context.ChangeTracker.Clear();
    }

    public async Task DisposeAsync()
    {
        Context.PlayRooms.RemoveRange(Context.PlayRooms);

        await Context.SaveChangesAsync();
        Context.ChangeTracker.Clear();
    }
}