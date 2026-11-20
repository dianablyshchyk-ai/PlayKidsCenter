using Application.Amenities.Exceptions;
using Application.Common.Interfaces.Repositories;
using Domain.Amenities;
using LanguageExt;
using MediatR;

namespace Application.Amenities.Commands;

public record CreateAmenityCommand : IRequest<Either<AmenityException, Amenity>>
{
    public required string Name { get; init; }
}

public class CreateAmenityCommandHandler(IAmenityRepository amenityRepository)
    : IRequestHandler<CreateAmenityCommand, Either<AmenityException, Amenity>>
{
    public async Task<Either<AmenityException, Amenity>> Handle(
        CreateAmenityCommand request,
        CancellationToken cancellationToken)
    {
        var existingByName = await amenityRepository.GetByName(
            request.Name,
            cancellationToken);

        return await existingByName.MatchAsync(
            a => new AmenityAlreadyExistsException(a.Id),
            () => CreateEntity(request, cancellationToken));
    }

    private async Task<Either<AmenityException, Amenity>> CreateEntity(
        CreateAmenityCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            var amenity = Amenity.New(
                AmenityId.New(),
                request.Name);

            return await amenityRepository.Add(amenity, cancellationToken);
        }
        catch (Exception exception)
        {
            return new AmenityUnhandledException(AmenityId.Empty(), exception);
        }
    }
}