using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SUMMERA.Domain.Entities;
using SUMMERA.Infrastructure.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUMMERA.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSets — كل واحد يمثل جدول في قاعدة البيانات
        public DbSet<City> Cities { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<HotelImage> HotelImages { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomImage> RoomImages { get; set; }
        public DbSet<Amenity> Amenities { get; set; }
        public DbSet<HotelAmenity> HotelAmenities { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<ContactMessage> ContactMessages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ============================================
            // 1. المفتاح المركب لجدول HotelAmenities
            // ============================================
            modelBuilder.Entity<HotelAmenity>()
                .HasKey(ha => new { ha.HotelId, ha.AmenityId });

            // ============================================
            // 2. الفهارس (Indexes)
            // ============================================
            modelBuilder.Entity<City>().HasIndex(c => c.Name);
            modelBuilder.Entity<Hotel>().HasIndex(h => h.Name);
            modelBuilder.Entity<Hotel>().HasIndex(h => h.CityId);
            modelBuilder.Entity<Hotel>().HasIndex(h => h.StarRating);
            modelBuilder.Entity<Room>().HasIndex(r => r.HotelId);
            modelBuilder.Entity<Room>().HasIndex(r => r.IsAvailable);
            modelBuilder.Entity<Booking>().HasIndex(b => b.BookingReference).IsUnique();
            modelBuilder.Entity<Booking>().HasIndex(b => b.UserId);
            modelBuilder.Entity<Booking>().HasIndex(b => b.Status);
            modelBuilder.Entity<Booking>().HasIndex(b => b.CheckInDate);
            modelBuilder.Entity<Review>().HasIndex(r => r.HotelId);
            modelBuilder.Entity<Review>().HasIndex(r => r.UserId);
            modelBuilder.Entity<Amenity>().HasIndex(a => a.Name).IsUnique();

            // ============================================
            // 3. قواعد الحذف (OnDelete Behaviors)
            // ============================================
            modelBuilder.Entity<Hotel>()
                .HasOne(h => h.City)
                .WithMany(c => c.Hotels)
                .HasForeignKey(h => h.CityId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Room>()
                .HasOne(r => r.Hotel)
                .WithMany(h => h.Rooms)
                .HasForeignKey(r => r.HotelId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<HotelImage>()
                .HasOne(hi => hi.Hotel)
                .WithMany(h => h.Images)
                .HasForeignKey(hi => hi.HotelId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RoomImage>()
                .HasOne(ri => ri.Room)
                .WithMany(r => r.Images)
                .HasForeignKey(ri => ri.RoomId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Room)
                .WithMany(r => r.Bookings)
                .HasForeignKey(b => b.RoomId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.Hotel)
                .WithMany(h => h.Reviews)
                .HasForeignKey(r => r.HotelId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<HotelAmenity>()
                .HasOne(ha => ha.Hotel)
                .WithMany(h => h.HotelAmenities)
                .HasForeignKey(ha => ha.HotelId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<HotelAmenity>()
                .HasOne(ha => ha.Amenity)
                .WithMany(a => a.HotelAmenities)
                .HasForeignKey(ha => ha.AmenityId)
                .OnDelete(DeleteBehavior.Cascade);

            // ============================================
            // 4. فلتر الحذف الناعم (Soft Delete Filter)
            // ============================================
            modelBuilder.Entity<City>().HasQueryFilter(c => !c.IsDeleted);
            modelBuilder.Entity<Hotel>().HasQueryFilter(h => !h.IsDeleted);
            modelBuilder.Entity<Room>().HasQueryFilter(r => !r.IsDeleted);
            modelBuilder.Entity<Booking>().HasQueryFilter(b => !b.IsDeleted);
            modelBuilder.Entity<Review>().HasQueryFilter(r => !r.IsDeleted);
            modelBuilder.Entity<Amenity>().HasQueryFilter(a => !a.IsDeleted);
            modelBuilder.Entity<ContactMessage>().HasQueryFilter(c => !c.IsDeleted);
        }
    }
}