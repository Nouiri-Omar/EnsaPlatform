using EnsaPlatform.Models;
using Microsoft.EntityFrameworkCore;

namespace EnsaPlatform.Data
{
    public class EnsaContext : DbContext
    {
        public EnsaContext(DbContextOptions<EnsaContext> options)
            : base(options)
        {
        }

        public DbSet<EnsaPlatform.Models.Departement> Departements { get; set; }

        public DbSet<EnsaPlatform.Models.Professeur> Professeurs { get; set; }

        public DbSet<EnsaPlatform.Models.Matiere> Matieres { get; set; }

        public DbSet<EnsaPlatform.Models.Module> Modules { get; set; }

        public DbSet<EnsaPlatform.Models.Niveau> Niveaux { get; set; }

        public DbSet<EnsaPlatform.Models.Etudiant> Etudiants { get; set; }

        public DbSet<EnsaPlatform.Models.Sceance> Sceances { get; set; }

        public DbSet<EnsaPlatform.Models.Note> Notes { get; set; }

        public DbSet<EnsaPlatform.Models.Absence> Absences { get; set; }

        public DbSet<EnsaPlatform.Models.Administration> Administrations { get; set; }

        public DbSet<EnsaPlatform.Models.Module_Matiere> Module_Matieres { get; set; }
        public DbSet<EnsaPlatform.Models.Module_Niveau> Module_Niveaus { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Departement>().ToTable("Departement");
            modelBuilder.Entity<Professeur>().ToTable("Professeur");
            modelBuilder.Entity<Matiere>().ToTable("Matiere");

            modelBuilder.Entity<Module>().ToTable("Module");
            modelBuilder.Entity<Niveau>().ToTable("Niveau");
            modelBuilder.Entity<Etudiant>().ToTable("Etudiant");

            modelBuilder.Entity<Sceance>().ToTable("Sceance");
            modelBuilder.Entity<Note>().ToTable("Note");
            modelBuilder.Entity<Absence>().ToTable("Absence");

            modelBuilder.Entity<Administration>().ToTable("Administration");
            //modelBuilder.Entity<Module_Matiere>().ToTable("Module_Matiere");
            //modelBuilder.Entity<Module_Niveau>().ToTable("Module_Niveau");

            /*
                        modelBuilder.Entity<ApplicationUserCompany>()
           .HasKey(t => new { t.ApplicationUserID, t.CompanyID });

            modelBuilder.Entity<ApplicationUserCompany>()
                .HasOne(ac => ac.ApplicationUserFields)
                .WithMany(a => a.ApplicationUserCompanies)
                .HasForeignKey(ac => ac.ApplicationUserID);

            modelBuilder.Entity<ApplicationUserCompany>()
                .HasOne(ac => ac.CompanyFields)
                .WithMany(c => c.ApplicationUserCompanies)
                .HasForeignKey(ac => ac.CompanyID);
             */

        }

    }
}
