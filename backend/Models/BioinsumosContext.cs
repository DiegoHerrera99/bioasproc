using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace bioinsumos_asproc_backend.Models;

public partial class BioinsumosContext : DbContext
{
    public BioinsumosContext()
    {
    }

    public BioinsumosContext(DbContextOptions<BioinsumosContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CertifionInformation> CertifionInformations { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Faq> Faqs { get; set; }

    public virtual DbSet<Handbook> Handbooks { get; set; }

    public virtual DbSet<News> News { get; set; }

    public virtual DbSet<Pdf> Pdfs { get; set; }

    public virtual DbSet<Price> Prices { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Video> Videos { get; set; }

    public virtual DbSet<WheaterAlert> WheaterAlerts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_uca1400_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<CertifionInformation>(entity =>
        {
            entity.HasKey(e => e.CertificationInformationId).HasName("PRIMARY");

            entity.ToTable("Certifion_Information");

            entity.Property(e => e.CertificationInformationId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("certificationInformationId");
            entity.Property(e => e.Body)
                .HasMaxLength(255)
                .HasColumnName("body");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp")
                .HasColumnName("createdAt");
            entity.Property(e => e.ModifiedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp")
                .HasColumnName("modifiedAt");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("'1'")
                .HasColumnName("status");
            entity.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(128)
                .HasColumnName("title");
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.CourseId).HasName("PRIMARY");

            entity.Property(e => e.CourseId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("courseId");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp")
                .HasColumnName("createdAt");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.ImgPath)
                .IsRequired()
                .HasMaxLength(128)
                .HasDefaultValueSql("''")
                .HasColumnName("imgPath");
            entity.Property(e => e.ModifiedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp")
                .HasColumnName("modifiedAt");
            entity.Property(e => e.Name)
                .HasMaxLength(128)
                .HasColumnName("name");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("'1'")
                .HasColumnName("status");
        });

        modelBuilder.Entity<Faq>(entity =>
        {
            entity.HasKey(e => e.FaqId).HasName("PRIMARY");

            entity.Property(e => e.FaqId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("faqId");
            entity.Property(e => e.Answer)
                .HasMaxLength(255)
                .HasColumnName("answer");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp")
                .HasColumnName("createdAt");
            entity.Property(e => e.ModifiedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp")
                .HasColumnName("modifiedAt");
            entity.Property(e => e.Question)
                .IsRequired()
                .HasMaxLength(128)
                .HasColumnName("question");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("'1'")
                .HasColumnName("status");
        });

        modelBuilder.Entity<Handbook>(entity =>
        {
            entity.HasKey(e => e.HandbookId).HasName("PRIMARY");

            entity.Property(e => e.HandbookId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("handbookId");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp")
                .HasColumnName("createdAt");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.ModifiedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp")
                .HasColumnName("modifiedAt");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(128)
                .HasColumnName("name");
            entity.Property(e => e.Path)
                .IsRequired()
                .HasMaxLength(128)
                .HasColumnName("path");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("'1'")
                .HasColumnName("status");
        });

        modelBuilder.Entity<News>(entity =>
        {
            entity.HasKey(e => e.NewId).HasName("PRIMARY");

            entity.Property(e => e.NewId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("newId");
            entity.Property(e => e.Body)
                .HasColumnType("text")
                .HasColumnName("body");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp")
                .HasColumnName("createdAt");
            entity.Property(e => e.ImgPath)
                .IsRequired()
                .HasMaxLength(128)
                .HasColumnName("imgPath");
            entity.Property(e => e.ModifiedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp")
                .HasColumnName("modifiedAt");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("'1'")
                .HasColumnName("status");
            entity.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(128)
                .HasColumnName("title");
            entity.Property(e => e.Url)
                .HasMaxLength(255)
                .HasColumnName("url");
        });

        modelBuilder.Entity<Pdf>(entity =>
        {
            entity.HasKey(e => e.PdfId).HasName("PRIMARY");

            entity.HasIndex(e => e.CourseId, "courseId");

            entity.Property(e => e.PdfId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("pdfId");
            entity.Property(e => e.CourseId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("courseId");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp")
                .HasColumnName("createdAt");
            entity.Property(e => e.ModifiedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp")
                .HasColumnName("modifiedAt");
            entity.Property(e => e.Path)
                .HasMaxLength(128)
                .HasColumnName("path");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("'1'")
                .HasColumnName("status");

            // Configuración de la clave foránea sin la propiedad de navegación
            entity.HasOne<Course>()
                .WithMany(p => p.Pdfs)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Pdfs_ibfk_1");
        });

        modelBuilder.Entity<Price>(entity =>
        {
            entity.HasKey(e => e.PriceId).HasName("PRIMARY");

            entity.Property(e => e.PriceId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("priceId");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp")
                .HasColumnName("createdAt");
            entity.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(128)
                .HasColumnName("description");
            entity.Property(e => e.ImgPath)
                .IsRequired()
                .HasMaxLength(128)
                .HasColumnName("imgPath");
            entity.Property(e => e.ModifiedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp")
                .HasColumnName("modifiedAt");
            entity.Property(e => e.Price1)
                .HasDefaultValueSql("'0'")
                .HasColumnType("smallint(5) unsigned")
                .HasColumnName("price");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("'1'")
                .HasColumnName("status");
            entity.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(128)
                .HasColumnName("title");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PRIMARY");

            entity.Property(e => e.ProductId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("productId");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp")
                .HasColumnName("createdAt");
            entity.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(300)
                .HasColumnName("description");
            entity.Property(e => e.ImgPath)
                .IsRequired()
                .HasMaxLength(128)
                .HasColumnName("imgPath");
            entity.Property(e => e.ModifiedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp")
                .HasColumnName("modifiedAt");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("'1'")
                .HasColumnName("status");
            entity.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(128)
                .HasColumnName("title");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.ReviewId).HasName("PRIMARY");

            entity.HasIndex(e => e.CourseId, "courseId");

            entity.Property(e => e.ReviewId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("reviewId");
            entity.Property(e => e.CourseId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("courseId");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp")
                .HasColumnName("createdAt");
            entity.Property(e => e.Email)
                .HasMaxLength(64)
                .HasColumnName("email");
            entity.Property(e => e.ModifiedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp")
                .HasColumnName("modifiedAt");
            entity.Property(e => e.Name)
                .HasMaxLength(64)
                .HasColumnName("name");
            entity.Property(e => e.Phone)
                .HasMaxLength(16)
                .HasColumnName("phone");
            entity.Property(e => e.Response)
                .HasMaxLength(255)
                .HasColumnName("response");
            entity.Property(e => e.Review1)
                .HasMaxLength(255)
                .HasColumnName("review");
            entity.Property(e => e.Score)
                .HasPrecision(2, 1)
                .HasColumnName("score");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("'1'")
                .HasColumnName("status");

            // Configuración de la clave foránea sin la propiedad de navegación
            entity.HasOne<Course>()
                .WithMany(c => c.Reviews)
                .HasForeignKey(r => r.CourseId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Reviews_ibfk_1");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PRIMARY");

            entity.HasIndex(e => e.Email, "email").IsUnique();

            entity.Property(e => e.UserId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("userId");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp")
                .HasColumnName("createdAt");
            entity.Property(e => e.Email)
                .HasMaxLength(64)
                .HasColumnName("email");
            entity.Property(e => e.ModifiedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp")
                .HasColumnName("modifiedAt");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("'1'")
                .HasColumnName("status");
        });

        modelBuilder.Entity<Video>(entity =>
        {
            entity.HasKey(e => e.VideoId).HasName("PRIMARY");

            entity.HasIndex(e => e.CourseId, "courseId");

            entity.Property(e => e.VideoId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("videoId");
            entity.Property(e => e.CourseId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("courseId");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp")
                .HasColumnName("createdAt");
            entity.Property(e => e.ModifiedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp")
                .HasColumnName("modifiedAt");
            entity.Property(e => e.Path)
                .HasMaxLength(128)
                .HasColumnName("path");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("'1'")
                .HasColumnName("status");

            entity.HasOne<Course>().WithMany(p => p.Videos)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Videos_ibfk_1");
        });

        modelBuilder.Entity<WheaterAlert>(entity =>
        {
            entity.HasKey(e => e.WeatherAlertId).HasName("PRIMARY");

            entity.ToTable("Wheater_Alerts");

            entity.Property(e => e.WeatherAlertId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("weatherAlertId");
            entity.Property(e => e.Body)
                .HasColumnType("text")
                .HasColumnName("body");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp")
                .HasColumnName("createdAt");
            entity.Property(e => e.ImgPath)
                .IsRequired()
                .HasMaxLength(128)
                .HasColumnName("imgPath");
            entity.Property(e => e.ModifiedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp")
                .HasColumnName("modifiedAt");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("'1'")
                .HasColumnName("status");
            entity.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(128)
                .HasColumnName("title");
            entity.Property(e => e.Url)
                .HasMaxLength(128)
                .HasColumnName("url");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
