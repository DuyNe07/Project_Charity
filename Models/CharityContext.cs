using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace project_charity.Models;

public partial class CharityContext : DbContext
{
    public CharityContext()
    {
    }

    public CharityContext(DbContextOptions<CharityContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Campaign> Campaigns { get; set; }

    public virtual DbSet<Charity> Charities { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Donate> Donates { get; set; }

    public virtual DbSet<Donor> Donors { get; set; }

    public virtual DbSet<Image> Images { get; set; }

    public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }

    public virtual DbSet<Tag> Tags { get; set; }

    public virtual DbSet<Volunteer> Volunteers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:Charity");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.ToTable("Account");

            entity.Property(e => e.AccountId).HasColumnName("AccountID");
            entity.Property(e => e.DonorId).HasColumnName("DonorID");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Donor).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.DonorId)
                .HasConstraintName("FK_Account_Donor");
        });

        modelBuilder.Entity<Campaign>(entity =>
        {
            entity.HasKey(e => e.CampainId);

            entity.ToTable("Campaign");

            entity.Property(e => e.CampainId).HasColumnName("CampainID");
            entity.Property(e => e.Describe).IsUnicode(false);
            entity.Property(e => e.Location).IsUnicode(false);
            entity.Property(e => e.Name).IsUnicode(false);
            entity.Property(e => e.TagLine)
                .HasMaxLength(2000)
                .IsUnicode(false);

            entity.HasMany(d => d.Tags).WithMany(p => p.Campains)
                .UsingEntity<Dictionary<string, object>>(
                    "CampainTag",
                    r => r.HasOne<Tag>().WithMany()
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_CampainTag_Tag"),
                    l => l.HasOne<Campaign>().WithMany()
                        .HasForeignKey("CampainId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_CampainTag_Campaign"),
                    j =>
                    {
                        j.HasKey("CampainId", "TagId");
                        j.ToTable("CampainTag");
                        j.IndexerProperty<int>("CampainId").HasColumnName("CampainID");
                        j.IndexerProperty<int>("TagId")
                            .ValueGeneratedOnAdd()
                            .HasColumnName("TagID");
                    });
        });

        modelBuilder.Entity<Charity>(entity =>
        {
            entity.HasKey(e => e.CharitiesId);

            entity.Property(e => e.CharitiesId).HasColumnName("CharitiesID");
            entity.Property(e => e.AccountId).HasColumnName("AccountID");
            entity.Property(e => e.Address).IsUnicode(false);
            entity.Property(e => e.Bio).IsUnicode(false);
            entity.Property(e => e.Experience).IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(1000)
                .IsUnicode(false);
            entity.Property(e => e.Number)
                .HasMaxLength(11)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.Account).WithMany(p => p.Charities)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("FK_Charities_Account");

            entity.HasMany(d => d.Campains).WithMany(p => p.Charities)
                .UsingEntity<Dictionary<string, object>>(
                    "Organize",
                    r => r.HasOne<Campaign>().WithMany()
                        .HasForeignKey("CampainId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Organize_Campaign"),
                    l => l.HasOne<Charity>().WithMany()
                        .HasForeignKey("CharitiesId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Organize_Charities"),
                    j =>
                    {
                        j.HasKey("CharitiesId", "CampainId");
                        j.ToTable("Organize");
                        j.IndexerProperty<int>("CharitiesId").HasColumnName("CharitiesID");
                        j.IndexerProperty<int>("CampainId").HasColumnName("CampainID");
                    });
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.ToTable("Comment");

            entity.Property(e => e.CommentId).HasColumnName("CommentID");
            entity.Property(e => e.CampainId).HasColumnName("CampainID");
            entity.Property(e => e.DonorId).HasColumnName("DonorID");

            entity.HasOne(d => d.Campain).WithMany(p => p.Comments)
                .HasForeignKey(d => d.CampainId)
                .HasConstraintName("FK_Comment_Campain");

            entity.HasOne(d => d.Donor).WithMany(p => p.Comments)
                .HasForeignKey(d => d.DonorId)
                .HasConstraintName("FK_Comment_Donor");
        });

        modelBuilder.Entity<Donate>(entity =>
        {
            entity.HasKey(e => new { e.DonorId, e.CampainId });

            entity.ToTable("Donate");

            entity.Property(e => e.DonorId).HasColumnName("DonorID");
            entity.Property(e => e.CampainId).HasColumnName("CampainID");
            entity.Property(e => e.PaymentMethod)
                .HasMaxLength(1000)
                .IsUnicode(false);

            entity.HasOne(d => d.Campain).WithMany(p => p.Donates)
                .HasForeignKey(d => d.CampainId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Donate_Campaign");

            entity.HasOne(d => d.Donor).WithMany(p => p.Donates)
                .HasForeignKey(d => d.DonorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Donate_Donor");
        });

        modelBuilder.Entity<Donor>(entity =>
        {
            entity.ToTable("Donor");

            entity.Property(e => e.DonorId).HasColumnName("DonorID");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Bio)
                .HasMaxLength(1000)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Number)
                .HasMaxLength(11)
                .IsUnicode(false)
                .IsFixedLength();
        });

        modelBuilder.Entity<Image>(entity =>
        {
            entity.ToTable("Image");

            entity.Property(e => e.ImageId).HasColumnName("ImageID");
            entity.Property(e => e.CampainId).HasColumnName("CampainID");

            entity.HasOne(d => d.Campain).WithMany(p => p.Images)
                .HasForeignKey(d => d.CampainId)
                .HasConstraintName("FK_Image_Campain");
        });

        modelBuilder.Entity<PaymentMethod>(entity =>
        {
            entity.ToTable("PaymentMethod");

            entity.Property(e => e.PaymentMethodId).HasColumnName("PaymentMethodID");
            entity.Property(e => e.AccountNumber)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.DonorId).HasColumnName("DonorID");
            entity.Property(e => e.Name)
                .HasMaxLength(1000)
                .IsUnicode(false);
            entity.Property(e => e.Providder)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Donor).WithMany(p => p.PaymentMethods)
                .HasForeignKey(d => d.DonorId)
                .HasConstraintName("FK_PaymentMethod_Donor");
        });

        modelBuilder.Entity<Tag>(entity =>
        {
            entity.ToTable("Tag");

            entity.Property(e => e.TagId).HasColumnName("TagID");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .IsFixedLength();
        });

        modelBuilder.Entity<Volunteer>(entity =>
        {
            entity.ToTable("Volunteer");

            entity.Property(e => e.VolunteerId).HasColumnName("VolunteerID");
            entity.Property(e => e.DonorId).HasColumnName("DonorID");
            entity.Property(e => e.Education).IsUnicode(false);
            entity.Property(e => e.Experience).IsUnicode(false);
            entity.Property(e => e.Job)
                .HasMaxLength(1000)
                .IsUnicode(false);

            entity.HasOne(d => d.Donor).WithMany(p => p.Volunteers)
                .HasForeignKey(d => d.DonorId)
                .HasConstraintName("FK_Volunteer_Donor");

            entity.HasMany(d => d.Campains).WithMany(p => p.Volunteers)
                .UsingEntity<Dictionary<string, object>>(
                    "Participate",
                    r => r.HasOne<Campaign>().WithMany()
                        .HasForeignKey("CampainId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Participate_Campaign"),
                    l => l.HasOne<Volunteer>().WithMany()
                        .HasForeignKey("VolunteerId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Participate_Volunteer"),
                    j =>
                    {
                        j.HasKey("VolunteerId", "CampainId");
                        j.ToTable("Participate");
                        j.IndexerProperty<int>("VolunteerId").HasColumnName("VolunteerID");
                        j.IndexerProperty<int>("CampainId").HasColumnName("CampainID");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
