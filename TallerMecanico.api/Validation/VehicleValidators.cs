using FluentValidation;
using TallerMecanico.Core.Dtos;

namespace TallerMecanico.Api.Validation;

public class VehicleCreateValidator : AbstractValidator<CreateVehicleRequest>
{
    public VehicleCreateValidator()
    {
        RuleFor(x => x.IdClient).GreaterThan(0);
        RuleFor(x => x.Brand).MaximumLength(50).When(x => x.Brand != null);
        RuleFor(x => x.Model).MaximumLength(50).When(x => x.Model != null);
        RuleFor(x => x.Plate).MaximumLength(20).When(x => x.Plate != null);
    }
}
