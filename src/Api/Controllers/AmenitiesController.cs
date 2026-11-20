using Api.Dtos;
using Api.Modules.Errors;
using Application.Amenities.Commands;
using Application.Common.Interfaces.Queries;
using Domain.Amenities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("amenities")]
[ApiController]
public class AmenitiesController(
    ISender sender,
    IAmenityQueries amenityQueries) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<AmenityDto>>> GetAmenities(CancellationToken cancellationToken)
    {
        var amenities = await amenityQueries.GetAll(cancellationToken);
        var dtos = amenities.Select(AmenityDto.FromDomainModel).ToList();
        return Ok(dtos);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<AmenityDto>> GetAmenity(Guid id, CancellationToken cancellationToken)
    {
        var amenity = await amenityQueries.GetById(new AmenityId(id), cancellationToken);

        return amenity.Match<ActionResult<AmenityDto>>(
            a => AmenityDto.FromDomainModel(a),
            () => NotFound());
    }

    [HttpPost]
    public async Task<ActionResult<AmenityDto>> CreateAmenity(
        [FromBody] CreateAmenityDto request,
        CancellationToken cancellationToken)
    {
        var input = new CreateAmenityCommand
        {
            Name = request.Name
        };

        var result = await sender.Send(input, cancellationToken);

        return result.Match<ActionResult<AmenityDto>>(
            a => CreatedAtAction(nameof(GetAmenity), new { id = a.Id.Value }, AmenityDto.FromDomainModel(a)),
            e => e.ToObjectResult());
    }
}