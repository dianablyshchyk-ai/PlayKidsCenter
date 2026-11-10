using Api.Dtos;
using Api.Modules.Errors;
using Application.Common.Interfaces.Queries;
using Application.PlayRooms.Commands;
using Domain.PlayRooms;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("playrooms")]
[ApiController]
public class PlayRoomsController(
    ISender sender,
    IPlayRoomQueries playRoomQueries) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<PlayRoomDto>>> GetPlayRooms(CancellationToken cancellationToken)
    {
        var playRooms = await playRoomQueries.GetAll(cancellationToken);
        var dtos = playRooms.Select(PlayRoomDto.FromDomainModel).ToList();
        return Ok(dtos);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<PlayRoomDto>> GetPlayRoom(Guid id, CancellationToken cancellationToken)
    {
        var playRoom = await playRoomQueries.GetById(new PlayRoomId(id), cancellationToken);

        return playRoom.Match<ActionResult<PlayRoomDto>>(
            p => PlayRoomDto.FromDomainModel(p),
            () => NotFound());
    }

    [HttpPost]
    public async Task<ActionResult<PlayRoomDto>> CreatePlayRoom(
        [FromBody] CreatePlayRoomDto request,
        CancellationToken cancellationToken)
    {
        var input = new CreatePlayRoomCommand
        {
            RoomNumber = request.RoomNumber,
            Name = request.Name,
            Description = request.Description,
            PricePerHour = request.PricePerHour,
            Capacity = request.Capacity,
            HasAnimator = request.HasAnimator,
            Theme = request.Theme,
            Attributes = request.Attributes?.ToDomainModel()
        };

        var result = await sender.Send(input, cancellationToken);

        return result.Match<ActionResult<PlayRoomDto>>(
            p => CreatedAtAction(nameof(GetPlayRoom), new { id = p.Id.Value }, PlayRoomDto.FromDomainModel(p)),
            e => e.ToObjectResult());
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<PlayRoomDto>> UpdatePlayRoom(
        Guid id,
        [FromBody] UpdatePlayRoomDto request,
        CancellationToken cancellationToken)
    {
        var input = new UpdatePlayRoomCommand
        {
            PlayRoomId = id,
            RoomNumber = request.RoomNumber,
            Name = request.Name,
            Description = request.Description,
            PricePerHour = request.PricePerHour,
            Capacity = request.Capacity,
            HasAnimator = request.HasAnimator,
            Theme = request.Theme,
            Attributes = request.Attributes?.ToDomainModel()
        };

        var result = await sender.Send(input, cancellationToken);

        return result.Match<ActionResult<PlayRoomDto>>(
            p => PlayRoomDto.FromDomainModel(p),
            e => e.ToObjectResult());
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeletePlayRoom(Guid id, CancellationToken cancellationToken)
    {
        var input = new DeletePlayRoomCommand
        {
            PlayRoomId = id
        };

        var result = await sender.Send(input, cancellationToken);

        return result.Match<IActionResult>(
            _ => NoContent(),
            e => e.ToObjectResult());
    }
}