using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Cinema.Models
{
    public partial class CinemaContext : IdentityDbContext<Users>
    {
        public CinemaContext()
        {
        }

        public CinemaContext(DbContextOptions<CinemaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Users> AspNetUsers { get; set; }
        public virtual DbSet<Film> Film { get; set; }
        public virtual DbSet<Genres> Genres { get; set; }
        public virtual DbSet<Hall> Hall { get; set; }
        public virtual DbSet<MovieGenre> MovieGenre { get; set; }
        public virtual DbSet<Session> Session { get; set; }
        public virtual DbSet<Ticket> Ticket { get; set; }
        public virtual DbSet<UserOrders> UserOrders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Users>(entity =>
            {
                entity.Property(e => e.ConcurrencyStamp).HasMaxLength(256);

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.PhoneNumber).IsRequired();


                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<Film>(entity =>
            {
                entity.Property(e => e.FilmId).ValueGeneratedNever();

                entity.Property(e => e.FilmDate).HasColumnType("date");

                entity.Property(e => e.FilmDirector)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FilmName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Genres>(entity =>
            {
                entity.HasKey(e => e.GenreId);

                entity.Property(e => e.GenreId)
                    .HasColumnName("GenreID")
                    .ValueGeneratedNever();

                entity.Property(e => e.GenreName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Hall>(entity =>
            {
                entity.Property(e => e.HallId).ValueGeneratedNever();

                entity.Property(e => e.HallName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.HallType)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<MovieGenre>(entity =>
            {
                entity.HasKey(e => new { e.MovieId, e.GenreId });

                entity.Property(e => e.MovieId).HasColumnName("MovieID");

                entity.Property(e => e.GenreId).HasColumnName("GenreID");

                entity.HasOne(d => d.Genre)
                    .WithMany(p => p.MovieGenre)
                    .HasForeignKey(d => d.GenreId)
                    .HasConstraintName("FK_MovieGenre_Genres");

                entity.HasOne(d => d.Movie)
                    .WithMany(p => p.MovieGenre)
                    .HasForeignKey(d => d.MovieId)
                    .HasConstraintName("FK_MovieGenre_Film");
            });

            

            modelBuilder.Entity<Session>(entity =>
            {
                entity.Property(e => e.SessionId).ValueGeneratedNever();

                entity.Property(e => e.SessionDateTime).HasColumnType("datetime");

                entity.HasOne(d => d.Film)
                    .WithMany(p => p.Session)
                    .HasForeignKey(d => d.FilmId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Session_Film");

                entity.HasOne(d => d.Hall)
                    .WithMany(p => p.Session)
                    .HasForeignKey(d => d.HallId)
                    .HasConstraintName("FK_Session_Hall");
            });

            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.Property(e => e.TicketId)
                    .HasColumnName("TicketID")
                    .ValueGeneratedNever();

                entity.HasOne(d => d.Session)
                    .WithMany(p => p.Ticket)
                    .HasForeignKey(d => d.SessionId)
                    .HasConstraintName("FK_SessionSeat_Session");
                entity.HasOne(d => d.UserOrders)
                .WithMany(p => p.Tickets)
                .HasForeignKey(d => d.OrderID)
                .HasConstraintName("FK_Ticket_UserOrders");
            });

            modelBuilder.Entity<UserOrders>(entity =>
            {
                entity.HasKey(e => e.OrderId);

                entity.Property(e => e.OrderId)
                    .HasColumnName("OrderID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.OrderDate).HasColumnType("datetime");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.Use)
                    .WithMany(p => p.UserOrders)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_UserOrders_User");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
