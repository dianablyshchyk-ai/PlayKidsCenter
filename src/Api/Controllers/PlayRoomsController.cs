using Api.Dtos;
using Application.PlayRooms.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("playrooms")]
[ApiController]
public class PlayRoomsController(IPlayRoomService playRoomService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<PlayRoomDto>>> GetPlayRooms(CancellationToken cancellationToken)
    {
        var playRooms = await playRoomService.GetPlayRooms(cancellationToken);
        var dtos = playRooms.Select(PlayRoomDto.FromDomainModel).ToList();
        return Ok(dtos);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<PlayRoomDto>> GetPlayRoom(Guid id, CancellationToken cancellationToken)
    {
        var playRoom = await playRoomService.GetPlayRoom(id, cancellationToken);
        if (playRoom is null)
        {
            return NotFound();
        }

        return PlayRoomDto.FromDomainModel(playRoom);
    }

    [HttpPost]
    public async Task<ActionResult<PlayRoomDto>> CreatePlayRoom(
        [FromBody] CreatePlayRoomDto request,
        CancellationToken cancellationToken)
    {
        try
        {
            var playRoom = await playRoomService.Add(
                request.RoomNumber,
                request.Name,
                request.PricePerHour,
                request.Capacity,
                request.HasAnimator,
                request.Theme,
                cancellationToken);

            var dto = PlayRoomDto.FromDomainModel(playRoom);
            return CreatedAtAction(nameof(GetPlayRoom), new { id = dto.Id }, dto);
        }
        catch (ArgumentException exception)
        {
            return Conflict(exception.Message);
        }
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<PlayRoomDto>> UpdatePlayRoom(
        Guid id,
        [FromBody] UpdatePlayRoomDto request,
        CancellationToken cancellationToken)
    {
        try
        {
            var playRoom = await playRoomService.Update(
                id,
                request.RoomNumber,
                request.Name,
                request.PricePerHour,
                request.Capacity,
                request.HasAnimator,
                request.Theme,
                cancellationToken);

            if (playRoom is null)
            {
                return NotFound();
            }

            return PlayRoomDto.FromDomainModel(playRoom);
        }
        catch (ArgumentException exception)
        {
            return Conflict(exception.Message);
        }
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeletePlayRoom(Guid id, CancellationToken cancellationToken)
    {
        var isDeleted = await playRoomService.Delete(id, cancellationToken);
        if (!isDeleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}