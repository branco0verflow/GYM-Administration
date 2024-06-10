using Microsoft.EntityFrameworkCore;
using PruebaGym2.Models;

namespace PruebaGym2.Datos
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> opciones) : base(opciones)
        {

        }

        public DbSet<TipoMaquina> TipoMaquinas { get; set; }
        public DbSet<Local> Locales { get; set; }
        public DbSet<Maquina> Maquinas { get; set; }
        public DbSet<Responsable> Responsables { get; set; }
        public DbSet<Socio> Socios { get; set; }
        public DbSet<Rutina> Rutinas { get; set; }
        public DbSet<Ejercicio> Ejercicios { get; set; }
        public DbSet<SocioRutina> SociosRutinas { get; set; }
        public DbSet<RutinaEjercicio> RutinaEjercicios { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SocioRutina>().HasKey(sr => new { sr.IdSocio, sr.IdRutina });

            modelBuilder.Entity<SocioRutina>()
                .HasOne(sr => sr.Socio)
                .WithMany(s => s.SocioRutinas)
                .HasForeignKey(sr => sr.IdSocio);

            modelBuilder.Entity<SocioRutina>()
                .HasOne(sr => sr.Rutina)
                .WithMany(r => r.SocioRutinas)
                .HasForeignKey(sr => sr.IdRutina);

            modelBuilder.Entity<Maquina>()
            .Property(m => m.PrecioCompra)
            .HasColumnType("int");

            modelBuilder.Entity<RutinaEjercicio>().HasKey(re => new { re.IdRutina, re.IdEjercicio });

            modelBuilder.Entity<RutinaEjercicio>()
                .HasOne(re => re.Rutina)
                .WithMany(r => r.RutinaEjercicios)
                .HasForeignKey(re => re.IdRutina);

            modelBuilder.Entity<RutinaEjercicio>()
                .HasOne(re => re.Ejercicio)
                .WithMany()
                .HasForeignKey(re => re.IdEjercicio);


        }

    }
}
