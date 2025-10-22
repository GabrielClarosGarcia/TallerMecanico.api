using FluentValidation;
using TallerMecanico.Core.Dtos;

namespace TallerMecanico.Api.Validation;

public class ServiceCreateValidator : AbstractValidator<CreateServiceRequest>
{
    public ServiceCreateValidator()
    {
        RuleFor(x => x.IdVehicle).GreaterThan(0);

        RuleFor(x => x.Description)
            .MaximumLength(200)
            .When(x => !string.IsNullOrWhiteSpace(x.Description));

        RuleFor(x => x.DateService)
            .Must(d => d == null || d.Value <= DateOnly.FromDateTime(DateTime.UtcNow.Date))
            .WithMessage("DateService no puede ser futura");
    }
}
