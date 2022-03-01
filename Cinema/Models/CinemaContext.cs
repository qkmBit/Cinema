using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Cinema.Models
{
    public partial class CinemaContext : DbContext
    {
        public CinemaContext()
        {
        }

        public CinemaContext(DbContextOptions<CinemaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Film> Film { get; set; }
        public virtual DbSet<Genres> Genres { get; set; }
        public virtual DbSet<Hall> Hall { get; set; }
        public virtual DbSet<MovieGenre> MovieGenre { get; set; }
        public virtual DbSet<OrderTickets> OrderTickets { get; set; }
        public virtual DbSet<Session> Session { get; set; }
        public virtual DbSet<Ticket> Ticket { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserConfirm> UserConfirm { get; set; }
        public virtual DbSet<UserOrders> UserOrders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=LAPTOP-BRLITGLL;Database=Cinema;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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

            modelBuilder.Entity<OrderTickets>(entity =>
            {
                entity.HasKey(e => new { e.OrderId, e.TicketId });

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.TicketId).HasColumnName("TicketID");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderTickets)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderTickets_UserOrders");

                entity.HasOne(d => d.Ticket)
                    .WithMany(p => p.OrderTickets)
                    .HasForeignKey(d => d.TicketId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderTickets_Ticket");
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
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserId)
                    .HasColumnName("UserID")
                    .ValueGeneratedNever();

                entity.Property(e => e.HashPassword)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.Property(e => e.RegistrationDate).HasColumnType("date");

                entity.Property(e => e.UserEmail)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.UserPhone)
                    .IsRequired()
                    .HasMaxLength(11);
            });

            modelBuilder.Entity<UserConfirm>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.Property(e => e.UserId)
                    .HasColumnName("UserID")
                    .ValueGeneratedNever();

                entity.Property(e => e.RegistrationDate).HasColumnType("date");

                entity.Property(e => e.Token).HasMaxLength(50);

                entity.Property(e => e.UserEmail)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.HasOne(d => d.User)
                    .WithOne(p => p.UserConfirm)
                    .HasForeignKey<UserConfirm>(d => d.UserId)
                    .HasConstraintName("FK_UserConfirm_User");
            });

            modelBuilder.Entity<UserOrders>(entity =>
            {
                entity.HasKey(e => e.OrderId);

                entity.Property(e => e.OrderId)
                    .HasColumnName("OrderID")
                    .ValueGeneratedNever();

                entity.Property(e => e.OrderDate).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserOrders)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_UserOrders_User");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
