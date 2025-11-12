using FluentValidation;
using TallerMecanico.Core.Dtos;
using System;

namespace TallerMecanico.Api.Validation;

public class ServiceCreateValidator : AbstractValidator<CreateServiceRequest>
{
    public ServiceCreateValidator()
    {
        RuleFor(x => x.IdVehicle)
            .GreaterThan(0)
            .WithMessage("Debe indicar un IdVehicle válido");

        RuleFor(x => x.Description)
            .MaximumLength(200)
            .When(x => !string.IsNullOrWhiteSpace(x.Description))
            .WithMessage("La descripción no puede exceder 200 caracteres");

        RuleFor(x => x.DateService)
     .Must(date =>
     {
         if (!date.HasValue) return true; // Permitir nulo

         // Conversión manual segura a DateTime
         var serviceDate = new DateTime(date.Value.Year, date.Value.Month, date.Value.Day);
         var today = DateTime.UtcNow.Date;

         return serviceDate <= today; // Comparación segura
     })
     .WithMessage("La fecha no puede ser futura");





    }
}
