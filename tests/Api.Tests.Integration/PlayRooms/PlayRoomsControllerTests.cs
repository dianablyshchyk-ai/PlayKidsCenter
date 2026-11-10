using System.Net.Http.Json;
using Api.Dtos;
using Domain.PlayRooms;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Tests.Common;
using Xunit;

namespace Api.Tests.Integration.PlayRooms;

public class PlayRoomsControllerTests : BaseIntegrationTest
{
    public PlayRoomsControllerTests(IntegrationTestWebFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task ShouldCreatePlayRoom()
    {
        // Arrange
        var request = new CreatePlayRoomDto(
            RoomNumber: "R-101",
            Name: "Test PlayRoom",
            Description: "Cozy room for kids",
            PricePerHour: 250m,
            Capacity: 10,
            HasAnimator: true,
            Theme: (PlayRoomTheme)1,
            Attributes: new PlayRoomAttributesDto(45.5m, 3, 8, "No shoes"));

        // Act
        var response = await Client.PostAsJsonAsync("playrooms", request);

        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();

        var responseDto = await response.Content.ReadFromJsonAsync<PlayRoomDto>();
        responseDto.Should().NotBeNull();

        var dbPlayRoom = await Context.PlayRooms.FirstOrDefaultAsync(x => x.Id == new PlayRoomId(responseDto!.Id));
        dbPlayRoom.Should().NotBeNull();
        dbPlayRoom!.RoomNumber.Should().Be(request.RoomNumber);
        dbPlayRoom.Name.Should().Be(request.Name);
        dbPlayRoom.Description.Should().Be(request.Description);
        dbPlayRoom.PricePerHour.Should().Be(request.PricePerHour);
        dbPlayRoom.Capacity.Should().Be(request.Capacity);
        dbPlayRoom.HasAnimator.Should().Be(request.HasAnimator);
    }
}