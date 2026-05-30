using Bansi.Application.Examenes.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bansi.Application.Examenes.Validators
{
    public class ExamenInputValidator : AbstractValidator<ExamenInputDto>
    {
        public ExamenInputValidator()
        {
            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El nombre es obligatorio.")
                .MaximumLength(255).WithMessage("El nombre no puede exceder los 255 caracteres.");

            RuleFor(x => x.Descripcion)
                .MaximumLength(255).WithMessage("La descripción no puede exceder los 255 caracteres.");
        }
    }
}
