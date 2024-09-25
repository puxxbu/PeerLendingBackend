﻿// <auto-generated />
using System;
using DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DAL.Migrations
{
    [DbContext(typeof(PeerlendingContext))]
    partial class PeerlendingContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("DAL.Models.MstLoans", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric")
                        .HasColumnName("amount");

                    b.Property<string>("BorrowerId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("borrower_id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<int>("Duration")
                        .HasColumnType("integer")
                        .HasColumnName("duration_month");

                    b.Property<decimal>("InterestRate")
                        .HasColumnType("numeric")
                        .HasColumnName("interest_rate");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("status");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.HasKey("Id");

                    b.HasIndex("BorrowerId");

                    b.ToTable("mst_loans");
                });

            modelBuilder.Entity("DAL.Models.MstUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text")
                        .HasColumnName("id");

                    b.Property<decimal?>("Balance")
                        .HasPrecision(18, 2)
                        .HasColumnType("numeric(18,2)")
                        .HasColumnName("balance");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("email");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)")
                        .HasColumnName("name");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("password");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)")
                        .HasColumnName("role");

                    b.HasKey("Id")
                        .HasName("mst_user_pkey");

                    b.ToTable("mst_user", (string)null);
                });

            modelBuilder.Entity("DAL.Models.TrnFunding", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric")
                        .HasColumnName("amount");

                    b.Property<DateTime>("FundedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("funded_at");

                    b.Property<string>("LenderId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("lender_id");

                    b.Property<string>("LoanId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("loan_id");

                    b.HasKey("Id");

                    b.HasIndex("LenderId");

                    b.HasIndex("LoanId");

                    b.ToTable("trn_funding");
                });

            modelBuilder.Entity("DAL.Models.MstLoans", b =>
                {
                    b.HasOne("DAL.Models.MstUser", "User")
                        .WithMany("MstLoans")
                        .HasForeignKey("BorrowerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("DAL.Models.TrnFunding", b =>
                {
                    b.HasOne("DAL.Models.MstUser", "User")
                        .WithMany()
                        .HasForeignKey("LenderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DAL.Models.MstLoans", "Loan")
                        .WithMany()
                        .HasForeignKey("LoanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Loan");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DAL.Models.MstUser", b =>
                {
                    b.Navigation("MstLoans");
                });
#pragma warning restore 612, 618
        }
    }
}
