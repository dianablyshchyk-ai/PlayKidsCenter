using FluentValidation;

namespace Application.PlayRooms.Commands;

public class CreatePlayRoomCommandValidator : AbstractValidator<CreatePlayRoomCommand>
{
    public CreatePlayRoomCommandValidator()
    {
        RuleFor(x => x.RoomNumber)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.Name)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(255);

        RuleFor(x => x.Description)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(500);

        RuleFor(x => x.PricePerHour)
            .InclusiveBetween(0.01m, 1_000_000m);

        RuleFor(x => x.Capacity)
            .InclusiveBetween(1, 100);

        RuleFor(x => x.Theme)
            .IsInEnum();

        When(x => x.Attributes is not null, () =>
        {
            RuleFor(x => x.Attributes!.AreaSqMeters)
                .GreaterThan(0)
                .When(x => x.Attributes!.AreaSqMeters.HasValue);

            RuleFor(x => x.Attributes!.MinRecommendedAge)
                .InclusiveBetween(0, 18)
                .When(x => x.Attributes!.MinRecommendedAge.HasValue);

            RuleFor(x => x.Attributes!.MaxRecommendedAge)
                .InclusiveBetween(0, 18)
                .When(x => x.Attributes!.MaxRecommendedAge.HasValue);

            RuleFor(x => x.Attributes!.MaxRecommendedAge)
                .GreaterThanOrEqualTo(x => x.Attributes!.MinRecommendedAge!.Value)
                .When(x => x.Attributes!.MinRecommendedAge.HasValue && x.Attributes!.MaxRecommendedAge.HasValue);

            RuleFor(x => x.Attributes!.ParentNotes)
                .MaximumLength(500);
        });
    }
}