using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DiamondAssessmentSystem.Models;

public partial class DiamondAssessmentSystemContext : DbContext
{
    public DiamondAssessmentSystemContext()
    {
    }

    public DiamondAssessmentSystemContext(DbContextOptions<DiamondAssessmentSystemContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Blog> Blogs { get; set; }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<BookingDetail> BookingDetails { get; set; }

    public virtual DbSet<Certificate> Certificates { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Form> Forms { get; set; }

    public virtual DbSet<Result> Results { get; set; }

    public virtual DbSet<ServicePrice> ServicePrices { get; set; }

    public virtual DbSet<Staff> Staff { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server =(local); database = DiamondAssessmentSystem;uid=sa; pwd=12345; TrustServerCertificate=True; Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.AccId).HasName("PK__Account__9A20D554F670DC22");

            entity.ToTable("Account");

            entity.Property(e => e.AccId).HasColumnName("acc_id");
            entity.Property(e => e.Password)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Role).HasColumnName("role");
            entity.Property(e => e.Username)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("username");
        });

        modelBuilder.Entity<Blog>(entity =>
        {
            entity.HasKey(e => e.BlogId).HasName("PK__Blog__2975AA28D7A92340");

            entity.ToTable("Blog");

            entity.Property(e => e.BlogId).HasColumnName("blog_id");
            entity.Property(e => e.BlogDate).HasColumnName("blog_date");
            entity.Property(e => e.Context)
                .IsUnicode(false)
                .HasColumnName("context");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.Image)
                .IsUnicode(false)
                .HasColumnName("image");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("title");
        });

        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.BookingId).HasName("PK__Booking__5DE3A5B1FBEEC01C");

            entity.ToTable("Booking");

            entity.Property(e => e.BookingId).HasColumnName("booking_id");
            entity.Property(e => e.BookingDate).HasColumnName("booking_date");
            entity.Property(e => e.BookingDetailId).HasColumnName("booking_detail_id");
            entity.Property(e => e.CommitmentId).HasColumnName("commitment_id");
            entity.Property(e => e.ConsultantId).HasColumnName("consultant_id");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.ReceiptId).HasColumnName("receipt_id");
            entity.Property(e => e.SealingId).HasColumnName("sealing_id");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("status");

            entity.HasOne(d => d.Commitment).WithMany(p => p.BookingCommitments)
                .HasForeignKey(d => d.CommitmentId)
                .HasConstraintName("FK__Booking__commitm__534D60F1");

            entity.HasOne(d => d.Consultant).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.ConsultantId)
                .HasConstraintName("FK__Booking__consult__5070F446");

            entity.HasOne(d => d.Customer).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Booking__custome__4F7CD00D");

            entity.HasOne(d => d.Receipt).WithMany(p => p.BookingReceipts)
                .HasForeignKey(d => d.ReceiptId)
                .HasConstraintName("FK__Booking__receipt__5165187F");

            entity.HasOne(d => d.Sealing).WithMany(p => p.BookingSealings)
                .HasForeignKey(d => d.SealingId)
                .HasConstraintName("FK__Booking__sealing__52593CB8");
        });

        modelBuilder.Entity<BookingDetail>(entity =>
        {
            entity.HasKey(e => e.BookingDetailId).HasName("PK__Booking___647E5673F77B1AA5");

            entity.ToTable("Booking_detail");

            entity.Property(e => e.BookingDetailId).HasColumnName("booking_detail_id");
            entity.Property(e => e.IsAccepted).HasColumnName("is_accepted");
            entity.Property(e => e.ResultId).HasColumnName("result_id");
            entity.Property(e => e.ServicePriceId).HasColumnName("service_price_id");

            entity.HasOne(d => d.Result).WithMany(p => p.BookingDetails)
                .HasForeignKey(d => d.ResultId)
                .HasConstraintName("FK__Booking_d__resul__48CFD27E");

            entity.HasOne(d => d.ServicePrice).WithMany(p => p.BookingDetails)
                .HasForeignKey(d => d.ServicePriceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Booking_d__servi__49C3F6B7");
        });

        modelBuilder.Entity<Certificate>(entity =>
        {
            entity.HasKey(e => e.CertId).HasName("PK__Certific__024B46EC1D9B9E7C");

            entity.ToTable("Certificate");

            entity.Property(e => e.CertId).HasColumnName("cert_id");
            entity.Property(e => e.IssueDate).HasColumnName("issue_date");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__Customer__CD65CB859EFE657D");

            entity.ToTable("Customer");

            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.AccId).HasColumnName("acc_id");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("address");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.IdCard)
                .HasMaxLength(16)
                .IsUnicode(false)
                .HasColumnName("id_card");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Phone)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("phone");
            entity.Property(e => e.TaxCode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("tax_code");
            entity.Property(e => e.UnitName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("unit_name");

            entity.HasOne(d => d.Acc).WithMany(p => p.Customers)
                .HasForeignKey(d => d.AccId)
                .HasConstraintName("FK__Customer__acc_id__3E52440B");
        });

        modelBuilder.Entity<Form>(entity =>
        {
            entity.HasKey(e => e.FormId).HasName("PK__Form__190E16C936A5AB6B");

            entity.ToTable("Form");

            entity.Property(e => e.FormId).HasColumnName("form_id");
            entity.Property(e => e.CreateDate).HasColumnName("create_date");
            entity.Property(e => e.FormType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("form_type");
        });

        modelBuilder.Entity<Result>(entity =>
        {
            entity.HasKey(e => e.ResultId).HasName("PK__Result__AFB3C31677AE6F71");

            entity.ToTable("Result");

            entity.Property(e => e.ResultId).HasColumnName("result_id");
            entity.Property(e => e.AssessmentNote)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("assessment_note");
            entity.Property(e => e.AssessmentStaff).HasColumnName("assessment_staff");
            entity.Property(e => e.CaratWeight)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("carat_weight");
            entity.Property(e => e.CertId).HasColumnName("cert_id");
            entity.Property(e => e.Clarity)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("clarity");
            entity.Property(e => e.Color)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("color");
            entity.Property(e => e.Cut)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("cut");
            entity.Property(e => e.Fluorescence)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("fluorescence");
            entity.Property(e => e.IsAccepted).HasColumnName("is_accepted");
            entity.Property(e => e.ManagerNote)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("manager_note");
            entity.Property(e => e.Measurement)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("measurement");
            entity.Property(e => e.Origin)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("origin");
            entity.Property(e => e.Polish)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("polish");
            entity.Property(e => e.Proportion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("proportion");
            entity.Property(e => e.Shape)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("shape");
            entity.Property(e => e.Symmetry)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("symmetry");

            entity.HasOne(d => d.AssessmentStaffNavigation).WithMany(p => p.Results)
                .HasForeignKey(d => d.AssessmentStaff)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Result__assessme__440B1D61");

            entity.HasOne(d => d.Cert).WithMany(p => p.Results)
                .HasForeignKey(d => d.CertId)
                .HasConstraintName("FK__Result__cert_id__4316F928");
        });

        modelBuilder.Entity<ServicePrice>(entity =>
        {
            entity.HasKey(e => e.ServicePriceId).HasName("PK__Service___B715FBB7E9F8300C");

            entity.ToTable("Service_price");

            entity.Property(e => e.ServicePriceId).HasColumnName("service_price_id");
            entity.Property(e => e.Description)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.Duration)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("duration");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("price");
            entity.Property(e => e.ServiceType)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("service_type");
        });

        modelBuilder.Entity<Staff>(entity =>
        {
            entity.HasKey(e => e.StaffId).HasName("PK__Staff__1963DD9CF0C1B5B8");

            entity.Property(e => e.StaffId).HasColumnName("staff_id");
            entity.Property(e => e.AccId).HasColumnName("acc_id");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Phone)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("phone");

            entity.HasOne(d => d.Acc).WithMany(p => p.Staff)
                .HasForeignKey(d => d.AccId)
                .HasConstraintName("FK__Staff__acc_id__3B75D760");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
