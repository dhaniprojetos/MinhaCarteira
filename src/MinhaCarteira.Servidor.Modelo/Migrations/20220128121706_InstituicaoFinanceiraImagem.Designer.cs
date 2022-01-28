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
    [Migration("20220128121706_InstituicaoFinanceiraImagem")]
    partial class InstituicaoFinanceiraImagem
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.13")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MinhaCarteira.Comum.Definicao.Entidade.Categoria", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("IdAuxiliar")
                        .HasColumnType("int");

                    b.Property<int?>("IdCategoriaPai")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.HasIndex("IdCategoriaPai");

                    b.ToTable("Categoria");
                });

            modelBuilder.Entity("MinhaCarteira.Comum.Definicao.Entidade.CentroClassificacao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("EhDespesa")
                        .HasColumnType("bit");

                    b.Property<bool>("EhReceita")
                        .HasColumnType("bit");

                    b.Property<int?>("IdAuxiliar")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.ToTable("CentroClassificacao");
                });

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

                    b.Property<string>("Icone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.ToTable("InstituicaoFinanceira");
                });

            modelBuilder.Entity("MinhaCarteira.Comum.Definicao.Entidade.MovimentoBancario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CategoriaId")
                        .HasColumnType("int");

                    b.Property<int>("CentroClassificacaoId")
                        .HasColumnType("int");

                    b.Property<int>("ContaBancariaId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DataMovimento")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descricao")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int?>("IdAuxiliar")
                        .HasColumnType("int");

                    b.Property<int?>("PessoaId")
                        .HasColumnType("int");

                    b.Property<int>("TipoMovimento")
                        .HasColumnType("int");

                    b.Property<decimal>("Valor")
                        .HasPrecision(18, 6)
                        .HasColumnType("decimal(18,6)");

                    b.HasKey("Id");

                    b.HasIndex("CategoriaId");

                    b.HasIndex("CentroClassificacaoId");

                    b.HasIndex("ContaBancariaId");

                    b.HasIndex("PessoaId");

                    b.ToTable("MovimentoBancario");
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

            modelBuilder.Entity("MinhaCarteira.Comum.Definicao.Entidade.Categoria", b =>
                {
                    b.HasOne("MinhaCarteira.Comum.Definicao.Entidade.Categoria", "CategoriaPai")
                        .WithMany("SubCategoria")
                        .HasForeignKey("IdCategoriaPai");

                    b.Navigation("CategoriaPai");
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

            modelBuilder.Entity("MinhaCarteira.Comum.Definicao.Entidade.MovimentoBancario", b =>
                {
                    b.HasOne("MinhaCarteira.Comum.Definicao.Entidade.Categoria", "Categoria")
                        .WithMany()
                        .HasForeignKey("CategoriaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MinhaCarteira.Comum.Definicao.Entidade.CentroClassificacao", "CentroClassificacao")
                        .WithMany()
                        .HasForeignKey("CentroClassificacaoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MinhaCarteira.Comum.Definicao.Entidade.ContaBancaria", "ContaBancaria")
                        .WithMany()
                        .HasForeignKey("ContaBancariaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MinhaCarteira.Comum.Definicao.Entidade.Pessoa", "Pessoa")
                        .WithMany()
                        .HasForeignKey("PessoaId");

                    b.Navigation("Categoria");

                    b.Navigation("CentroClassificacao");

                    b.Navigation("ContaBancaria");

                    b.Navigation("Pessoa");
                });

            modelBuilder.Entity("MinhaCarteira.Comum.Definicao.Entidade.Categoria", b =>
                {
                    b.Navigation("SubCategoria");
                });
#pragma warning restore 612, 618
        }
    }
}
