using Microsoft.Extensions.DependencyInjection;
using Bansi.Application.Examenes.DTOs;
using Bansi.Application.Examenes;
using Bansi.Domain.Entities;
using Bansi.Domain.Interfaces;
using FluentValidation;

namespace Bansi.Application.Examenes.Services
{
    public class clsExamen : IExamenService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IValidator<ExamenDto> _validator;
        private readonly IValidator<ExamenInputDto> _validatorInput;
        public bool UsarStoredProcedures { get; set; } = false;
        public clsExamen(IServiceProvider serviceProvider, IValidator<ExamenDto> validator, IValidator<ExamenInputDto> validatorInput)
        {
            _serviceProvider = serviceProvider;
            _validator = validator;
            _validatorInput = validatorInput;
        }

        private IExamenRepository ObtenerRepositorio()
        {
            string key = UsarStoredProcedures ? "SP" : "EF";
            return _serviceProvider.GetRequiredKeyedService<IExamenRepository>(key);
        }

        public async Task<bool> AgregarAsync(ExamenInputDto examenDto)
        {
            var validationResult = await _validatorInput.ValidateAsync(examenDto);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var entidad = new Examen
            {
                Nombre = examenDto.Nombre,
                Descripcion = examenDto.Descripcion
            };

            var repository = ObtenerRepositorio();
            return await repository.AgregarAsync(entidad);
        }

        public async Task<bool> ActualizarAsync(ExamenDto examenDto)
        {
            var validationResult = await _validator.ValidateAsync(examenDto);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var entidad = new Examen
            {
                IdExamen = examenDto.IdExamen,
                Nombre = examenDto.Nombre,
                Descripcion = examenDto.Descripcion
            };

            var repository = ObtenerRepositorio();
            return await repository.ActualizarAsync(entidad);
        }

        public async Task<bool> EliminarAsync(int idExamen)
        {
            var repository = ObtenerRepositorio();
            return await repository.EliminarAsync(idExamen);
        }

        public async Task<IEnumerable<ExamenDto>> ConsultarAsync()
        {
            var repository = ObtenerRepositorio();
            var entidades = await repository.ConsultarAsync();

            return entidades.Select(e => new ExamenDto
            {
                IdExamen = e.IdExamen,
                Nombre = e.Nombre,
                Descripcion = e.Descripcion
            }).ToList();
        }
    }
}
