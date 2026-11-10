using Application.Common.Interfaces.Repositories;
using Application.PlayRooms.Exceptions;
using Domain.PlayRooms;
using LanguageExt;
using MediatR;
using Unit = LanguageExt.Unit;

namespace Application.PlayRooms.Commands;

public record UpdatePlayRoomCommand : IRequest<Either<PlayRoomException, PlayRoom>>
{
    public required Guid PlayRoomId { get; init; }
    public required string RoomNumber { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required decimal PricePerHour { get; init; }
    public required int Capacity { get; init; }
    public required bool HasAnimator { get; init; }
    public required PlayRoomTheme Theme { get; init; }
    public PlayRoomAttributes? Attributes { get; init; }
}

public class UpdatePlayRoomCommandHandler(IPlayRoomRepository playRoomRepository) : IRequestHandler<UpdatePlayRoomCommand, Either<PlayRoomException, PlayRoom>>
{
    public async Task<Either<PlayRoomException, PlayRoom>> Handle(
        UpdatePlayRoomCommand request,
        CancellationToken cancellationToken)
    {
        var playRoomId = new PlayRoomId(request.PlayRoomId);
        var playRoom = await playRoomRepository.GetById(playRoomId, cancellationToken);

        return await playRoom.MatchAsync(
            p => CheckDuplicates(p.Id, request.RoomNumber, request.Name, cancellationToken)
                .BindAsync(_ => UpdateEntity(p, request, cancellationToken)),
            () => new PlayRoomNotFoundException(playRoomId));
    }

    private async Task<Either<PlayRoomException, Unit>> CheckDuplicates(
        PlayRoomId currentPlayRoomId,
        string roomNumber,
        string name,
        CancellationToken cancellationToken)
    {
        var byRoomNumber = await playRoomRepository.GetByRoomNumber(roomNumber, cancellationToken);
        var roomNumberCheck = byRoomNumber.Match<Either<PlayRoomException, Unit>>(
            p => p.Id == currentPlayRoomId ? Unit.Default : new PlayRoomAlreadyExistsException(p.Id),
            () => Unit.Default);

        if (roomNumberCheck.IsLeft)
        {
            return roomNumberCheck;
        }

        var byName = await playRoomRepository.GetByName(name, cancellationToken);
        return byName.Match<Either<PlayRoomException, Unit>>(
            p => p.Id == currentPlayRoomId ? Unit.Default : new PlayRoomAlreadyExistsException(p.Id),
            () => Unit.Default);
    }

    private async Task<Either<PlayRoomException, PlayRoom>> UpdateEntity(
        PlayRoom playRoom,
        UpdatePlayRoomCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            playRoom.UpdateDetails(
                request.RoomNumber,
                request.Name,
                request.Description,
                request.PricePerHour,
                request.Capacity,
                request.HasAnimator,
                request.Theme,
                request.Attributes);

            return await playRoomRepository.Update(playRoom, cancellationToken);
        }
        catch (Exception exception)
        {
            return new PlayRoomUnhandledException(playRoom.Id, exception);
        }
    }
}