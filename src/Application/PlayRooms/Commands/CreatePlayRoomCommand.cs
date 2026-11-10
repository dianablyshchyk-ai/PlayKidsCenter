using Application.Common.Interfaces.Repositories;
using Application.PlayRooms.Exceptions;
using Domain.PlayRooms;
using LanguageExt;
using MediatR;

namespace Application.PlayRooms.Commands;

public record CreatePlayRoomCommand : IRequest<Either<PlayRoomException, PlayRoom>>
{
    public required string RoomNumber { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required decimal PricePerHour { get; init; }
    public required int Capacity { get; init; }
    public required bool HasAnimator { get; init; }
    public required PlayRoomTheme Theme { get; init; }
    public PlayRoomAttributes? Attributes { get; init; }
}

public class CreatePlayRoomCommandHandler(IPlayRoomRepository playRoomRepository) : IRequestHandler<CreatePlayRoomCommand, Either<PlayRoomException, PlayRoom>>
{
    public async Task<Either<PlayRoomException, PlayRoom>> Handle(
        CreatePlayRoomCommand request,
        CancellationToken cancellationToken)
    {
        var existingByRoomNumber = await playRoomRepository.GetByRoomNumber(
            request.RoomNumber,
            cancellationToken);

        return await existingByRoomNumber.MatchAsync(
            p => new PlayRoomAlreadyExistsException(p.Id),
            () => CheckNameAndCreateEntity(request, cancellationToken));
    }

    private async Task<Either<PlayRoomException, PlayRoom>> CheckNameAndCreateEntity(
        CreatePlayRoomCommand request,
        CancellationToken cancellationToken)
    {
        var existingByName = await playRoomRepository.GetByName(
            request.Name,
            cancellationToken);

        return await existingByName.MatchAsync(
            p => new PlayRoomAlreadyExistsException(p.Id),
            () => CreateEntity(request, cancellationToken));
    }

    private async Task<Either<PlayRoomException, PlayRoom>> CreateEntity(
        CreatePlayRoomCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            var playRoom = PlayRoom.New(
                PlayRoomId.New(),
                request.RoomNumber,
                request.Name,
                request.Description,
                request.PricePerHour,
                request.Capacity,
                request.HasAnimator,
                request.Theme,
                request.Attributes);

            return await playRoomRepository.Add(playRoom, cancellationToken);
        }
        catch (Exception exception)
        {
            return new PlayRoomUnhandledException(PlayRoomId.Empty(), exception);
        }
    }
}