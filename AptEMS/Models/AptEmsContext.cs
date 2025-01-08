

using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace AptEMS.Models
{
    public class AptEmsContext : DbContext
    {
        public AptEmsContext() : base("cs")
        {
        }

        // Define DbSets for entities
        
        public DbSet<JobApplication> JobApplications { get; set; }

        public DbSet<UserAccount> UserAccounts { get; set; }

        public DbSet<PricingPackage> PricingPackages { get; set; }

        public DbSet<Enrollment> Enrollments { get; set; }

    

        public DbSet<AssignedEnrollment> AssignedEnrollments { get; set; }

        public DbSet<Statistic> Statistics { get; set; }

        public DbSet<ClientReview> ClientReviews { get; set; }
        public DbSet<Partnership> Partnerships { get; set; }

        public DbSet<JobApplicationForm> JobApplicationForms { get; set; }
        public DbSet<Qualification> Qualifications { get; set; }

        public DbSet<JobPost> JobPosts { get; set; }

        public DbSet<UploadedImages> UploadedImages { get; set; }
        public DbSet<Strength> Strengths { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Location> Locations { get; set; }


        public DbSet<ClientAgreements> ClientAgreements { get; set; }

        public DbSet<Payment> Payments { get; set; }



        /*protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Payment>()
                .Property(p => p.AmountPaid)
                .HasColumnType("decimal")
                .HasPrecision(18, 2); // Define precision explicitly

            base.OnModelCreating(modelBuilder);
        }*/














        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Map JobApplicationForm to the correct table name (singular)
            modelBuilder.Entity<JobApplicationForm>().ToTable("JobApplicationForm");

            modelBuilder.Entity<Payment>()
                .Property(p => p.AmountPaid)
                .HasColumnType("decimal")
                .HasPrecision(18, 2);

            base.OnModelCreating(modelBuilder);
        }

    }


}
