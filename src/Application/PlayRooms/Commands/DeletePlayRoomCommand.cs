using Application.Common.Interfaces.Repositories;
using Application.PlayRooms.Exceptions;
using Domain.PlayRooms;
using LanguageExt;
using MediatR;

namespace Application.PlayRooms.Commands;

public record DeletePlayRoomCommand : IRequest<Either<PlayRoomException, PlayRoom>>
{
    public required Guid PlayRoomId { get; init; }
}

public class DeletePlayRoomCommandHandler(IPlayRoomRepository playRoomRepository) : IRequestHandler<DeletePlayRoomCommand, Either<PlayRoomException, PlayRoom>>
{
    public async Task<Either<PlayRoomException, PlayRoom>> Handle(
        DeletePlayRoomCommand request,
        CancellationToken cancellationToken)
    {
        var playRoomId = new PlayRoomId(request.PlayRoomId);
        var playRoom = await playRoomRepository.GetById(playRoomId, cancellationToken);

        return await playRoom.MatchAsync(
            p => DeleteEntity(p, cancellationToken),
            () => new PlayRoomNotFoundException(playRoomId));
    }

    private async Task<Either<PlayRoomException, PlayRoom>> DeleteEntity(
        PlayRoom playRoom,
        CancellationToken cancellationToken)
    {
        try
        {
            await playRoomRepository.Delete(playRoom, cancellationToken);
            return playRoom;
        }
        catch (Exception exception)
        {
            return new PlayRoomUnhandledException(playRoom.Id, exception);
        }
    }
}