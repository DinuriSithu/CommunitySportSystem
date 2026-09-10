using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace CommunitySportSystem.Models
{
    public class CommunitySportsDbContext : DbContext
    {
        public CommunitySportsDbContext()
            : base("name=CommunitySportsDB")
        {
        }

        public DbSet<Member> Members { get; set; }
        public DbSet<Sport> Sports { get; set; }
        public DbSet<Member_Sport> MemberSports { get; set; }
        public DbSet<Facility_Type> FacilityTypes { get; set; }
        public DbSet<Facility> Facilities { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Inquiry> Inquiries { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Member>()
                .ToTable("MEMBER");

            
            modelBuilder.Entity<Sport>()
                .ToTable("SPORT");

           
            modelBuilder.Entity<Member_Sport>()
                .ToTable("MEMBER_SPORT");

           
            modelBuilder.Entity<Member_Sport>()
                .HasKey(ms => new
                {
                    ms.Member_ID,
                    ms.Sport_ID
                });

            
            modelBuilder.Entity<Facility_Type>()
                .ToTable("FACILITY_TYPE");

            
            modelBuilder.Entity<Facility>()
                .ToTable("FACILITY");

            
            modelBuilder.Entity<Booking>()
                .ToTable("BOOKING");

           
            modelBuilder.Entity<Review>()
                .ToTable("REVIEW");

            
            modelBuilder.Entity<Inquiry>()
                .ToTable("INQUIRY");

            base.OnModelCreating(modelBuilder);
        }
    }
}