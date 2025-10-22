using FluentValidation;
using TallerMecanico.Core.Dtos;

namespace TallerMecanico.Api.Validation;

public class ClientCreateValidator : AbstractValidator<CreateClientRequest>
{
    public ClientCreateValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name es obligatorio")
            .MaximumLength(100);
    }
}

public class ClientUpdateValidator : AbstractValidator<UpdateClientRequest>
{
    public ClientUpdateValidator()
    {
        RuleFor(x => x.IdClient).GreaterThan(0);
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name es obligatorio")
            .MaximumLength(100);
    }
}
