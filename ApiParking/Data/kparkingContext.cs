using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ApiParking.Models;

namespace ApiParking.Data
{
    public partial class kparkingContext : DbContext
    {
     
        public kparkingContext(DbContextOptions<kparkingContext> options)
            : base(options)
        {
        }

        public virtual DbSet<MdKategoriArea> MdKategoriArea { get; set; }
        public virtual DbSet<MdParkingFees> MdParkingFees { get; set; }
        public virtual DbSet<MgIncome> MgIncome { get; set; }
        public virtual DbSet<MgParkHistory> MgParkHistory { get; set; }
        public virtual DbSet<MgParkingArea> MgParkingArea { get; set; }
        public virtual DbSet<MgParkingSlot> MgParkingSlot { get; set; }
        public virtual DbSet<MgParkingUserCar> MgParkingUserCar { get; set; }
        public virtual DbSet<MgUserParking> MgUserParking { get; set; }
        public virtual DbSet<ParkingOtp> ParkingOtp { get; set; }
    
        public DbSet<IncoemModels> Incoems { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MdKategoriArea>(entity =>
            {
                entity.HasKey(e => e.KatiAreaId)
                    .HasName("PRIMARY");

                entity.ToTable("md_kategori_area");

                entity.Property(e => e.KatiAreaId)
                    .HasColumnName("kati_area_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.KatAreaName)
                    .IsRequired()
                    .HasColumnName("kat_area_name")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.KatNumber)
                  .IsRequired()
                  .HasColumnName("kat_number")
                  .HasColumnType("int(5)")
                  .HasCharSet("latin1")
                  .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.KatAreaSts)
                    .HasColumnName("kat_area_sts")
                    .HasColumnType("int(2)");
            });

            modelBuilder.Entity<ParkingOtp>(entity =>
            {
                entity.HasKey(e => e.OtpId)
                    .HasName("PRIMARY");

                entity.ToTable("praking_otp");

                entity.Property(e => e.OtpId)
                   .HasColumnName("otp_id")
                   .HasColumnType("int(11)");

                entity.Property(e => e.OtpKode)
                    .HasColumnName("otp_kode")
                    .HasColumnType("int(7)");

                entity.Property(e => e.OtpUserId)
                    .HasColumnName("otp_user_id")
                    .HasColumnType("int(4)");

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.OtpSts)
                    .HasColumnName("otp_sts")
                    .HasColumnType("int(2)");
            });

            modelBuilder.Entity<MdParkingFees>(entity =>
            {
                entity.HasKey(e => e.ParkFeesId)
                    .HasName("PRIMARY");

                entity.ToTable("md_parking_fees");

                entity.Property(e => e.ParkFeesId)
                    .HasColumnName("park_fees_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.ParkFeesSts)
                    .HasColumnName("park_fees_sts")
                    .HasColumnType("int(3)");

                entity.Property(e => e.ParkFeesValue)
                    .HasColumnName("park_fees_value")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<MgIncome>(entity =>
            {
                entity.HasKey(e => e.IncomeId)
                    .HasName("PRIMARY");

                entity.ToTable("mg_income");

                entity.Property(e => e.IncomeId)
                    .HasColumnName("income_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.HistId)
                    .HasColumnName("hist_id")
                    .HasColumnType("int(3)");

                entity.Property(e => e.IncomeCreatedAt)
                    .HasColumnName("income_created_at")
                    .HasColumnType("int(2)");

                entity.Property(e => e.IncomeSts)
                    .HasColumnName("income_sts")
                    .HasColumnType("int(2)");

                entity.Property(e => e.IncomeValue).HasColumnName("income_value");
            });

            modelBuilder.Entity<MgParkHistory>(entity =>
            {
                entity.HasKey(e => e.HistId)
                    .HasName("PRIMARY");

                entity.ToTable("mg_park_history");

                entity.Property(e => e.HistId)
                    .HasColumnName("hist_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.HistAreaId)
                    .IsRequired()
                    .HasColumnName("hist_area_id")
                    .HasColumnType("varchar(12)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.HistCreatedAtd)
                    .HasColumnName("hist_created_atd")
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.HistIn)
                    .HasColumnName("hist_in")
                    .HasColumnType("datetime");

                entity.Property(e => e.HistOut)
                    .HasColumnName("hist_out")
                    .HasColumnType("datetime");

                entity.Property(e => e.HistoryKode)
                    .HasColumnName("hist_kode")
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.HistSts)
                    .HasColumnName("hist_sts")
                    .HasColumnType("int(2)");

                entity.Property(e => e.ParkUserId)
                    .IsRequired()
                    .HasColumnName("park_user_id")
                    .HasColumnType("varchar(2)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");
            });

            modelBuilder.Entity<MgParkingArea>(entity =>
            {
                entity.HasKey(e => e.AreaId)
                    .HasName("PRIMARY");

                entity.ToTable("mg_parking_area");

                entity.Property(e => e.AreaId)
                    .HasColumnName("area_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.AreaCreatedAt)
                    .HasColumnName("area_created_at")
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.AreaKategoriId)
                    .IsRequired()
                    .HasColumnName("area_kategori_id")
                    .HasColumnType("varchar(3)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.AreaNumber)
                    .HasColumnName("area_number")
                    .HasColumnType("int(5)");

                entity.Property(e => e.AreaParkingFeesId)
                    .IsRequired()
                    .HasColumnName("area_parking_fees_id")
                    .HasColumnType("varchar(3)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.AreaSts)
                    .HasColumnName("area_sts")
                    .HasColumnType("int(2)");
            });

            modelBuilder.Entity<MgParkingSlot>(entity =>
            {
                entity.HasKey(e => e.ParSlotId)
                    .HasName("PRIMARY");

                entity.ToTable("mg_parking_slot");

                entity.Property(e => e.ParSlotId)
                    .HasColumnName("par_slot_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ParAreaId)
                    .IsRequired()
                    .HasColumnName("par_area_id")
                    .HasColumnType("int(10)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ParkSlotCreated)
                    .HasColumnName("park_slot_created")
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.ParkSlotStatus)
                    .IsRequired()
                    .HasColumnName("park_slot_status")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ParkSlotSts)
                    .HasColumnName("park_slot_sts")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ParkSlotUserId)
                    .IsRequired()
                    .HasColumnName("park_slot_user_id")
                    .HasColumnType("varchar(10)")
                    .HasDefaultValue(null)
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");
            });

            modelBuilder.Entity<MgParkingUserCar>(entity =>
            {
                entity.HasKey(e => e.ParkCarId)
                    .HasName("PRIMARY");

                entity.ToTable("mg_parking_user_car");

                entity.Property(e => e.ParkCarId)
                    .HasColumnName("park_car_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ParkCarCreatedAt).HasColumnName("park_car_created_at");

                entity.Property(e => e.ParkCarImage)
                    .IsRequired()
                    .HasColumnName("park_car_image")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ParkCarLicence)
                    .IsRequired()
                    .HasColumnName("park_car_licence")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ParkCarSts)
                    .HasColumnName("park_car_sts")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ParkCarUserId)
                    .IsRequired()
                    .HasColumnName("park_car_user_id")
                    .HasColumnType("varchar(3)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");
            });

            modelBuilder.Entity<MgUserParking>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PRIMARY");

                entity.ToTable("mg_user_parking");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.UserCraetedAt)
                    .HasColumnName("user_craeted_at")
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.UserFullname)
                    .IsRequired()
                    .HasColumnName("user_fullname")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.UserPassword)
                    .IsRequired()
                    .HasColumnName("user_password")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.UserRole)
                    .IsRequired()
                    .HasColumnName("user_role")
                    .HasColumnType("enum('1','2')")
                    .HasComment("1 admin ,2 pengunjung")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.UserUsername)
                    .IsRequired()
                    .HasColumnName("user_username")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.UsersSts)
                    .HasColumnName("users_sts")
                    .HasColumnType("int(3)");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
