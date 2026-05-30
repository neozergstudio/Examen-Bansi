using Bansi.Domain.Entities;
using Bansi.Domain.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Bansi.Infrastructure.Repositories
{
    public class ExamenSpRepository : IExamenRepository
    {
        private readonly BansiContext _context;

        public ExamenSpRepository(BansiContext context)
        {
            _context = context;
        }
        public async Task<bool> AgregarAsync(Examen examen)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var pNombre = new SqlParameter("@Nombre", examen.Nombre);
                var pDesc = new SqlParameter("@Descripcion", examen.Descripcion);
                var pCodRetorno = new SqlParameter("@CodigoRetorno", SqlDbType.Int) { Direction = ParameterDirection.Output };
                var pDescRetorno = new SqlParameter("@DescRetorno", SqlDbType.VarChar, 255) { Direction = ParameterDirection.Output };

                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC spAgregar @Nombre, @Descripcion, @CodigoRetorno OUTPUT, @DescRetorno OUTPUT",
                    pNombre, pDesc, pCodRetorno, pDescRetorno);

                await transaction.CommitAsync();
                return (int)pCodRetorno.Value == 0;
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
                var pId = new SqlParameter("@IdExamen", examen.IdExamen);
                var pNombre = new SqlParameter("@Nombre", examen.Nombre);
                var pDesc = new SqlParameter("@Descripcion", examen.Descripcion);
                var pCodRetorno = new SqlParameter("@CodigoRetorno", SqlDbType.Int) { Direction = ParameterDirection.Output };
                var pDescRetorno = new SqlParameter("@DescRetorno", SqlDbType.VarChar, 255) { Direction = ParameterDirection.Output };

                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC spActualizar @IdExamen, @Nombre, @Descripcion, @CodigoRetorno OUTPUT, @DescRetorno OUTPUT",
                    pId, pNombre, pDesc, pCodRetorno, pDescRetorno);

                await transaction.CommitAsync();
                return (int)pCodRetorno.Value == 0;
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
                var pId = new SqlParameter("@IdExamen", idExamen);
                var pCodRetorno = new SqlParameter("@CodigoRetorno", SqlDbType.Int) { Direction = ParameterDirection.Output };
                var pDescRetorno = new SqlParameter("@DescRetorno", SqlDbType.VarChar, 255) { Direction = ParameterDirection.Output };

                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC spEliminar @IdExamen, @CodigoRetorno OUTPUT, @DescRetorno OUTPUT",
                    pId, pCodRetorno, pDescRetorno);

                await transaction.CommitAsync();
                return (int)pCodRetorno.Value == 0;
            }
            catch
            {
                await transaction.RollbackAsync();
                return false;
            }
        }

        public async Task<IEnumerable<Examen>> ConsultarAsync()
        {
            return await _context.Examenes.FromSqlRaw("EXEC spConsultar").ToListAsync();
        }
    }
}
