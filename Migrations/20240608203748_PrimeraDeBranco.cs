using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PruebaGym2.Migrations
{
    /// <inheritdoc />
    public partial class PrimeraDeBranco : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Responsables",
                columns: table => new
                {
                    idResponsable = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Telefono = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Responsables", x => x.idResponsable);
                });

            migrationBuilder.CreateTable(
                name: "Rutinas",
                columns: table => new
                {
                    IdRutina = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoRutina = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CalificacionPromedio = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rutinas", x => x.IdRutina);
                });

            migrationBuilder.CreateTable(
                name: "TipoMaquinas",
                columns: table => new
                {
                    IdTipoMaquina = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreTipo = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoMaquinas", x => x.IdTipoMaquina);
                });

            migrationBuilder.CreateTable(
                name: "Locales",
                columns: table => new
                {
                    IdLocal = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Ciudad = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Telefono = table.Column<int>(type: "int", nullable: false),
                    IdResponsable = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locales", x => x.IdLocal);
                    table.ForeignKey(
                        name: "FK_Locales_Responsables_IdResponsable",
                        column: x => x.IdResponsable,
                        principalTable: "Responsables",
                        principalColumn: "idResponsable",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ejercicios",
                columns: table => new
                {
                    IdEjercicio = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreEjercicio = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DescripcionEjercicio = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    TipoMaquinaId = table.Column<int>(type: "int", nullable: true),
                    RutinaIdRutina = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ejercicios", x => x.IdEjercicio);
                    table.ForeignKey(
                        name: "FK_Ejercicios_Rutinas_RutinaIdRutina",
                        column: x => x.RutinaIdRutina,
                        principalTable: "Rutinas",
                        principalColumn: "IdRutina");
                    table.ForeignKey(
                        name: "FK_Ejercicios_TipoMaquinas_TipoMaquinaId",
                        column: x => x.TipoMaquinaId,
                        principalTable: "TipoMaquinas",
                        principalColumn: "IdTipoMaquina");
                });

            migrationBuilder.CreateTable(
                name: "Maquinas",
                columns: table => new
                {
                    IdMaquina = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaCompra = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PrecioCompra = table.Column<int>(type: "int", nullable: false),
                    VidaUtil = table.Column<int>(type: "int", nullable: false),
                    Disponible = table.Column<bool>(type: "bit", nullable: false),
                    IdLocal = table.Column<int>(type: "int", nullable: false),
                    IdTipoMaquina = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Maquinas", x => x.IdMaquina);
                    table.ForeignKey(
                        name: "FK_Maquinas_Locales_IdLocal",
                        column: x => x.IdLocal,
                        principalTable: "Locales",
                        principalColumn: "IdLocal",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Maquinas_TipoMaquinas_IdTipoMaquina",
                        column: x => x.IdTipoMaquina,
                        principalTable: "TipoMaquinas",
                        principalColumn: "IdTipoMaquina",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Socios",
                columns: table => new
                {
                    IdSocio = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreSocio = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoSocio = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefono = table.Column<int>(type: "int", nullable: false),
                    Mail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdLocal = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Socios", x => x.IdSocio);
                    table.ForeignKey(
                        name: "FK_Socios_Locales_IdLocal",
                        column: x => x.IdLocal,
                        principalTable: "Locales",
                        principalColumn: "IdLocal",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RutinaEjercicios",
                columns: table => new
                {
                    IdRutina = table.Column<int>(type: "int", nullable: false),
                    IdEjercicio = table.Column<int>(type: "int", nullable: false),
                    Sets = table.Column<int>(type: "int", nullable: false),
                    Repeticiones = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RutinaEjercicios", x => new { x.IdRutina, x.IdEjercicio });
                    table.ForeignKey(
                        name: "FK_RutinaEjercicios_Ejercicios_IdEjercicio",
                        column: x => x.IdEjercicio,
                        principalTable: "Ejercicios",
                        principalColumn: "IdEjercicio",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RutinaEjercicios_Rutinas_IdRutina",
                        column: x => x.IdRutina,
                        principalTable: "Rutinas",
                        principalColumn: "IdRutina",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SociosRutinas",
                columns: table => new
                {
                    IdSocio = table.Column<int>(type: "int", nullable: false),
                    IdRutina = table.Column<int>(type: "int", nullable: false),
                    Calificacion = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SociosRutinas", x => new { x.IdSocio, x.IdRutina });
                    table.ForeignKey(
                        name: "FK_SociosRutinas_Rutinas_IdRutina",
                        column: x => x.IdRutina,
                        principalTable: "Rutinas",
                        principalColumn: "IdRutina",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SociosRutinas_Socios_IdSocio",
                        column: x => x.IdSocio,
                        principalTable: "Socios",
                        principalColumn: "IdSocio",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ejercicios_RutinaIdRutina",
                table: "Ejercicios",
                column: "RutinaIdRutina");

            migrationBuilder.CreateIndex(
                name: "IX_Ejercicios_TipoMaquinaId",
                table: "Ejercicios",
                column: "TipoMaquinaId");

            migrationBuilder.CreateIndex(
                name: "IX_Locales_IdResponsable",
                table: "Locales",
                column: "IdResponsable");

            migrationBuilder.CreateIndex(
                name: "IX_Maquinas_IdLocal",
                table: "Maquinas",
                column: "IdLocal");

            migrationBuilder.CreateIndex(
                name: "IX_Maquinas_IdTipoMaquina",
                table: "Maquinas",
                column: "IdTipoMaquina");

            migrationBuilder.CreateIndex(
                name: "IX_RutinaEjercicios_IdEjercicio",
                table: "RutinaEjercicios",
                column: "IdEjercicio");

            migrationBuilder.CreateIndex(
                name: "IX_Socios_IdLocal",
                table: "Socios",
                column: "IdLocal");

            migrationBuilder.CreateIndex(
                name: "IX_SociosRutinas_IdRutina",
                table: "SociosRutinas",
                column: "IdRutina");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Maquinas");

            migrationBuilder.DropTable(
                name: "RutinaEjercicios");

            migrationBuilder.DropTable(
                name: "SociosRutinas");

            migrationBuilder.DropTable(
                name: "Ejercicios");

            migrationBuilder.DropTable(
                name: "Socios");

            migrationBuilder.DropTable(
                name: "Rutinas");

            migrationBuilder.DropTable(
                name: "TipoMaquinas");

            migrationBuilder.DropTable(
                name: "Locales");

            migrationBuilder.DropTable(
                name: "Responsables");
        }
    }
}
