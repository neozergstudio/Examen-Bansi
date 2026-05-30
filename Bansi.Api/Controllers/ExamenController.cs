using Bansi.Application.Examenes;
using Bansi.Application.Examenes.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Bansi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamenController : ControllerBase
    {
        private readonly IExamenService _examenService;
        public ExamenController(IExamenService examenService)
        {
            _examenService = examenService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExamenDto>>> Consultar([FromQuery] bool usarSp = false)
        {
            _examenService.UsarStoredProcedures = usarSp;

            var resultado = await _examenService.ConsultarAsync();
            return Ok(resultado);
        }

        [HttpPost]
        public async Task<IActionResult> Agregar([FromBody] ExamenInputDto examenDto, [FromQuery] bool usarSp = false)
        {
            try
            {
                _examenService.UsarStoredProcedures = usarSp;
                var exito = await _examenService.AgregarAsync(examenDto);

                if (exito)
                    return Ok(new { Mensaje = "Registro insertado satisfactoriamente" });

                return BadRequest("Ocurrió un error al guardar el registro.");
            }
            catch (FluentValidation.ValidationException ex)
            {
                return BadRequest(new { Errores = ex.Errors });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] ExamenDto examenDto, [FromQuery] bool usarSp = false)
        {
            if (id != examenDto.IdExamen)
            {
                return BadRequest("El ID no coincide.");
            }

            try
            {
                _examenService.UsarStoredProcedures = usarSp;
                var exito = await _examenService.ActualizarAsync(examenDto);

                if (exito)
                    return Ok(new { Mensaje = "Registro actualizado satisfactoriamente" });

                return NotFound("No se encontró el registro a actualizar.");
            }
            catch (FluentValidation.ValidationException ex)
            {
                return BadRequest(new { Errores = ex.Errors });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id, [FromQuery] bool usarSp = false)
        {
            _examenService.UsarStoredProcedures = usarSp;
            var exito = await _examenService.EliminarAsync(id);

            if (exito)
                return Ok(new { Mensaje = "Registro eliminado satisfactoriamente" });

            return NotFound("No se encontró el registro a eliminar.");
        }
    }
}
