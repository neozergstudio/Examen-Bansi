using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bansi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class BansiDBInitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblExamen",
                columns: table => new
                {
                    IdExamen = table.Column<int>(type: "int", nullable: false, comment: "Identificador único de la tabla tblExamen")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, comment: "Nombre o título del examen"),
                    Descripcion = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, comment: "Descripción detallada del examen")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblExamen", x => x.IdExamen);
                });

            //Generación de SP
            //Create
            migrationBuilder.Sql(@"
                CREATE PROCEDURE spAgregar
                    @Nombre VARCHAR(255),
                    @Descripcion VARCHAR(255),
                    @CodigoRetorno INT OUTPUT,
                    @DescRetorno VARCHAR(255) OUTPUT
                AS
                BEGIN
                    BEGIN TRY
                        INSERT INTO tblExamen (Nombre, Descripcion) VALUES (@Nombre, @Descripcion);
                        SET @CodigoRetorno = 0;
                        SET @DescRetorno = 'Registro insertado satisfactoriamente';
                    END TRY
                    BEGIN CATCH
                        SET @CodigoRetorno = ERROR_NUMBER();
                        SET @DescRetorno = ERROR_MESSAGE();
                    END CATCH
                END
            ");

            //Update
            migrationBuilder.Sql(@"
                CREATE PROCEDURE spActualizar
                    @IdExamen INT,
                    @Nombre VARCHAR(255),
                    @Descripcion VARCHAR(255),
                    @CodigoRetorno INT OUTPUT,
                    @DescRetorno VARCHAR(255) OUTPUT
                AS
                BEGIN
                    BEGIN TRY
                        UPDATE tblExamen 
                        SET Nombre = @Nombre, 
                            Descripcion = @Descripcion 
                        WHERE IdExamen = @IdExamen;

                        IF @@ROWCOUNT > 0
                        BEGIN
                            SET @CodigoRetorno = 0;
                            SET @DescRetorno = 'Registro actualizado satisfactoriamente';
                        END
                        ELSE
                        BEGIN
                            SET @CodigoRetorno = 1;
                            SET @DescRetorno = 'No se encontró el registro a actualizar';
                        END
                    END TRY
                    BEGIN CATCH
                        SET @CodigoRetorno = ERROR_NUMBER();
                        SET @DescRetorno = ERROR_MESSAGE();
                    END CATCH
                END
            ");

            //Delete
            migrationBuilder.Sql(@"
                CREATE PROCEDURE spEliminar
                    @IdExamen INT,
                    @CodigoRetorno INT OUTPUT,
                    @DescRetorno VARCHAR(255) OUTPUT
                AS
                BEGIN
                    BEGIN TRY
                        DELETE FROM tblExamen 
                        WHERE IdExamen = @IdExamen;

                        IF @@ROWCOUNT > 0
                        BEGIN
                            SET @CodigoRetorno = 0;
                            SET @DescRetorno = 'Registro eliminado satisfactoriamente';
                        END
                        ELSE
                        BEGIN
                            SET @CodigoRetorno = 1;
                            SET @DescRetorno = 'No se encontró el registro a eliminar';
                        END
                    END TRY
                    BEGIN CATCH
                        SET @CodigoRetorno = ERROR_NUMBER();
                        SET @DescRetorno = ERROR_MESSAGE();
                    END CATCH
                END
            ");
            //GetAll
            migrationBuilder.Sql(@"
                CREATE PROCEDURE spConsultar
                AS
                BEGIN
                    SELECT 
                        IdExamen, 
                        Nombre, 
                        Descripcion 
                    FROM tblExamen;
                END
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblExamen");
            //SP
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS spAgregar");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS spActualizar");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS spEliminar");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS spConsultar");
        }
    }
}
