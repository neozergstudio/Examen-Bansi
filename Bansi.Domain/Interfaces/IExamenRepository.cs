using Bansi.Domain.Entities;

namespace Bansi.Domain.Interfaces
{
    public interface IExamenRepository
    {
        Task<bool> AgregarAsync(Examen examen);

        Task<bool> ActualizarAsync(Examen examen);

        Task<bool> EliminarAsync(int idExamen);

        Task<IEnumerable<Examen>> ConsultarAsync();
    }
}
