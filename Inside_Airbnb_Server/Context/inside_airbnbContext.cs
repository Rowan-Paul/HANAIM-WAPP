using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Inside_Airbnb_Server
{
    public partial class inside_airbnbContext : DbContext
    {
        public inside_airbnbContext()
        {
        }

        public inside_airbnbContext(DbContextOptions<inside_airbnbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Listing> Listings { get; set; } = null!;
        public virtual DbSet<Neighbourhood> Neighbourhoods { get; set; } = null!;
        public virtual DbSet<Review> Reviews { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=localhost;Database=inside_airbnb;User Id=sa;password=$3RBgNBfJ8C6SrAJ;Trusted_Connection=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Listing>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("listings");

                entity.Property(e => e.Availability365).HasColumnName("availability_365");

                entity.Property(e => e.CalculatedHostListingsCount).HasColumnName("calculated_host_listings_count");

                entity.Property(e => e.HostId).HasColumnName("host_id");

                entity.Property(e => e.HostName)
                    .HasColumnType("text")
                    .HasColumnName("host_name");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.LastReview)
                    .HasColumnType("text")
                    .HasColumnName("last_review");

                entity.Property(e => e.Latitude).HasColumnName("latitude");

                entity.Property(e => e.License)
                    .HasColumnType("text")
                    .HasColumnName("license");

                entity.Property(e => e.Longitude).HasColumnName("longitude");

                entity.Property(e => e.MinimumNights).HasColumnName("minimum_nights");

                entity.Property(e => e.Name)
                    .HasColumnType("text")
                    .HasColumnName("name");

                entity.Property(e => e.Neighbourhood)
                    .IsUnicode(false)
                    .HasColumnName("neighbourhood");

                entity.Property(e => e.NeighbourhoodGroup)
                    .HasColumnType("text")
                    .HasColumnName("neighbourhood_group");

                entity.Property(e => e.NumberOfReviews).HasColumnName("number_of_reviews");

                entity.Property(e => e.NumberOfReviewsLtm).HasColumnName("number_of_reviews_ltm");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.ReviewsPerMonth).HasColumnName("reviews_per_month");

                entity.Property(e => e.RoomType)
                    .HasColumnType("text")
                    .HasColumnName("room_type");
            });

            modelBuilder.Entity<Neighbourhood>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("neighbourhoods");

                entity.Property(e => e.Neighbourhood1)
                    .HasColumnType("text")
                    .HasColumnName("neighbourhood");

                entity.Property(e => e.NeighbourhoodGroup)
                    .HasColumnType("text")
                    .HasColumnName("neighbourhood_group");
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("reviews");

                entity.Property(e => e.Date)
                    .HasColumnType("text")
                    .HasColumnName("date");

                entity.Property(e => e.ListingId).HasColumnName("listing_id");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
