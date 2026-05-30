using Bansi.Application.Examenes.DTOs;

namespace Bansi.Application.Examenes
{
    /// <summary>
    /// Interfaz de los métodos expuestos para el modelo de Examen
    /// </summary>
    public interface IExamenService
    {
        /// <summary>
        /// Determina la tecnología de guardado el cual puede ser por EF o Procedimiento Almacenado
        /// </summary>
        bool UsarStoredProcedures { get; set; }
        /// <summary>
        /// Servicio para crear un item de examen
        /// </summary>
        /// <param name="examenDto"></param>
        /// <returns></returns>
        Task<bool> AgregarAsync(ExamenInputDto examenDto);
        /// <summary>
        /// Servicio para actualizar un examen existente
        /// </summary>
        /// <param name="examenDto"></param>
        /// <returns></returns>
        Task<bool> ActualizarAsync(ExamenDto examenDto);
        /// <summary>
        /// Servicio para eliminar un item de examen
        /// </summary>
        /// <param name="examenDto"></param>
        /// <returns></returns>
        Task<bool> EliminarAsync(int idExamen);
        /// <summary>
        /// Servicio para obtener el listado completo de examenes
        /// </summary>
        /// <param name="examenDto"></param>
        /// <returns></returns>
        Task<IEnumerable<ExamenDto>> ConsultarAsync();
    }
}
