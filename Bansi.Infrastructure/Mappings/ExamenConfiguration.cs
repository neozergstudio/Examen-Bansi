using Bansi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bansi.Infrastructure.Mappings
{
    public class ExamenConfiguration : IEntityTypeConfiguration<Examen>
    {
        public void Configure(EntityTypeBuilder<Examen> builder)
        {
            builder.ToTable("tblExamen");
            builder.HasKey(e => e.IdExamen);
            builder.Property(e => e.IdExamen).UseIdentityColumn().HasColumnName("IdExamen").HasComment("Identificador único de la tabla tblExamen");
            builder.Property(e => e.Nombre).IsRequired().HasMaxLength(255).HasColumnType("varchar(255)").HasComment("Nombre o título del examen");
            builder.Property(e => e.Descripcion).IsRequired(false).HasMaxLength(255).HasColumnType("varchar(255)").HasComment("Descripción detallada del examen");
        }
    }
}
