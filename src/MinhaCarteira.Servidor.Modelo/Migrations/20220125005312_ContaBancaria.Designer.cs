﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MinhaCarteira.Servidor.Modelo.Data;

namespace MinhaCarteira.Servidor.Modelo.Migrations
{
    [DbContext(typeof(MinhaCarteiraContext))]
    [Migration("20220125005312_ContaBancaria")]
    partial class ContaBancaria
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.13")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MinhaCarteira.Comum.Definicao.Entidade.ContaBancaria", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Agencia")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Conta")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("IdInstituicaoFinanceira")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.HasIndex("IdInstituicaoFinanceira");

                    b.ToTable("ContaBancaria");
                });

            modelBuilder.Entity("MinhaCarteira.Comum.Definicao.Entidade.InstituicaoFinanceira", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nome")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.ToTable("InstituicaoFinanceira");
                });

            modelBuilder.Entity("MinhaCarteira.Comum.Definicao.Entidade.Pessoa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("EhCliente")
                        .HasColumnType("bit");

                    b.Property<bool>("EhFornecedor")
                        .HasColumnType("bit");

                    b.Property<int?>("IdAuxiliar")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.ToTable("Pessoa");
                });

            modelBuilder.Entity("MinhaCarteira.Comum.Definicao.Entidade.ContaBancaria", b =>
                {
                    b.HasOne("MinhaCarteira.Comum.Definicao.Entidade.InstituicaoFinanceira", "InstituicaoFinanceira")
                        .WithMany()
                        .HasForeignKey("IdInstituicaoFinanceira")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("InstituicaoFinanceira");
                });
#pragma warning restore 612, 618
        }
    }
}
