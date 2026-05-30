using Bansi.Domain.Entities;
using Bansi.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Bansi.Infrastructure.Repositories
{
    public class ExamenEfRepository : IExamenRepository
    {
        private readonly BansiContext _context;
        public ExamenEfRepository(BansiContext context)
        {
            _context = context;
        }
        public async Task<bool> AgregarAsync(Examen examen)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                await _context.Examenes.AddAsync(examen);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                return false;
            }
        }

        public async Task<bool> ActualizarAsync(Examen examen)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                _context.Examenes.Update(examen);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                return false;
            }
        }

        public async Task<bool> EliminarAsync(int idExamen)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var examen = await _context.Examenes.FindAsync(idExamen);
                if (examen == null) return false;

                _context.Examenes.Remove(examen);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                return false;
            }
        }

        public async Task<IEnumerable<Examen>> ConsultarAsync()
        {
            return await _context.Examenes.ToListAsync();
        }
    }
}
