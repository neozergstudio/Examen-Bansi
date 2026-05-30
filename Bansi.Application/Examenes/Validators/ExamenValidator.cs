using Bansi.Application.Examenes.DTOs;
using FluentValidation;

namespace Bansi.Application.Examenes.Validators
{
    public class ExamenValidator : AbstractValidator<ExamenDto>
    {
        public ExamenValidator()
        {
            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El nombre es obligatorio.")
                .MaximumLength(255).WithMessage("El nombre no puede exceder los 255 caracteres.");

            RuleFor(x => x.Descripcion)
                .MaximumLength(255).WithMessage("La descripción no puede exceder los 255 caracteres.");
        }
    }
}
