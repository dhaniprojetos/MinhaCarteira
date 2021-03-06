// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MinhaCarteira.Servidor.Modelo.Data;

namespace MinhaCarteira.Servidor.Modelo.Migrations
{
    [DbContext(typeof(MinhaCarteiraContext))]
    partial class MinhaCarteiraContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.13")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.HasSequence<int>("PapelIds")
                .StartsAt(100L);

            modelBuilder.HasSequence<int>("UsuarioIds")
                .StartsAt(100L);

            modelBuilder.Entity("MinhaCarteira.Comum.Definicao.Entidade.Agendamento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CategoriaId")
                        .HasColumnType("int");

                    b.Property<int>("CentroClassificacaoId")
                        .HasColumnType("int");

                    b.Property<int?>("ContaBancariaId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DataInicial")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descricao")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int?>("IdAuxiliar")
                        .HasColumnType("int");

                    b.Property<int>("Parcelas")
                        .HasColumnType("int");

                    b.Property<int?>("PessoaId")
                        .HasColumnType("int");

                    b.Property<string>("RegraRecorrencia")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Tipo")
                        .HasColumnType("int");

                    b.Property<int>("TipoParcelas")
                        .HasColumnType("int");

                    b.Property<int>("TipoRecorrencia")
                        .HasColumnType("int");

                    b.Property<decimal>("Valor")
                        .HasPrecision(18, 6)
                        .HasColumnType("decimal(18,6)");

                    b.HasKey("Id");

                    b.HasIndex("CategoriaId");

                    b.HasIndex("CentroClassificacaoId");

                    b.HasIndex("ContaBancariaId");

                    b.HasIndex("PessoaId");

                    b.ToTable("Agendamento");
                });

            modelBuilder.Entity("MinhaCarteira.Comum.Definicao.Entidade.AgendamentoItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AgendamentoId")
                        .HasColumnType("int");

                    b.Property<int?>("ContaBancariaId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Data")
                        .HasPrecision(0)
                        .HasColumnType("datetime2(0)");

                    b.Property<DateTime?>("DataPagamento")
                        .HasPrecision(0)
                        .HasColumnType("datetime2(0)");

                    b.Property<bool>("EstahConciliada")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<bool>("EstahPaga")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<int?>("PessoaId")
                        .HasColumnType("int");

                    b.Property<decimal>("Valor")
                        .HasPrecision(18, 6)
                        .HasColumnType("decimal(18,6)");

                    b.Property<decimal?>("ValorPago")
                        .HasPrecision(18, 6)
                        .HasColumnType("decimal(18,6)");

                    b.HasKey("Id");

                    b.HasIndex("AgendamentoId");

                    b.HasIndex("ContaBancariaId");

                    b.HasIndex("PessoaId");

                    b.ToTable("AgendamentoItem");
                });

            modelBuilder.Entity("MinhaCarteira.Comum.Definicao.Entidade.Categoria", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Icone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("IdAuxiliar")
                        .HasColumnType("int");

                    b.Property<int?>("IdCategoriaPai")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("NomeArquivo")
                        .HasColumnType("nvarchar(max)");

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

                    b.Property<DateTime>("DataSaldoInicial")
                        .HasColumnType("datetime2");

                    b.Property<int>("InstituicaoFinanceiraId")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<decimal>("ValorSaldoAtual")
                        .HasPrecision(18, 6)
                        .HasColumnType("decimal(18,6)");

                    b.Property<decimal>("ValorSaldoInicial")
                        .HasPrecision(18, 6)
                        .HasColumnType("decimal(18,6)");

                    b.HasKey("Id");

                    b.HasIndex("InstituicaoFinanceiraId");

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

                    b.Property<string>("NomeArquivo")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("InstituicaoFinanceira");
                });

            modelBuilder.Entity("MinhaCarteira.Comum.Definicao.Entidade.MovimentoBancario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AgendamentoItemId")
                        .HasColumnType("int");

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

                    b.HasIndex("AgendamentoItemId");

                    b.HasIndex("CategoriaId");

                    b.HasIndex("CentroClassificacaoId");

                    b.HasIndex("ContaBancariaId");

                    b.HasIndex("PessoaId");

                    b.ToTable("MovimentoBancario");
                });

            modelBuilder.Entity("MinhaCarteira.Comum.Definicao.Entidade.Papel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValueSql("NEXT VALUE FOR PapelIds");

                    b.Property<string>("Nome")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.ToTable("Papel");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Nome = "Admin"
                        },
                        new
                        {
                            Id = 2,
                            Nome = "SuperAdmin"
                        });
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

            modelBuilder.Entity("MinhaCarteira.Comum.Definicao.Entidade.Relatorio.ExtratoDiario", b =>
                {
                    b.Property<int>("ContaBancariaId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Data")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descricao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Idx")
                        .HasColumnType("int");

                    b.Property<decimal>("Saldo")
                        .HasPrecision(18, 6)
                        .HasColumnType("decimal(18,6)");

                    b.Property<decimal>("Valor")
                        .HasPrecision(18, 6)
                        .HasColumnType("decimal(18,6)");
                });

            modelBuilder.Entity("MinhaCarteira.Comum.Definicao.Entidade.Relatorio.ExtratoMensal", b =>
                {
                    b.Property<int>("ContaBancariaId")
                        .HasColumnType("int");

                    b.Property<string>("Descricao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Idx")
                        .HasColumnType("int");

                    b.Property<string>("MesAno")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Saldo")
                        .HasPrecision(18, 6)
                        .HasColumnType("decimal(18,6)");

                    b.Property<decimal>("Valor")
                        .HasPrecision(18, 6)
                        .HasColumnType("decimal(18,6)");
                });

            modelBuilder.Entity("MinhaCarteira.Comum.Definicao.Entidade.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValueSql("NEXT VALUE FOR UsuarioIds");

                    b.Property<string>("Email")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Nome")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("PasswordHash")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Sobrenome")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Username")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Usuario");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "administrador@dhaniprojetos.com",
                            Nome = "Administrador",
                            PasswordHash = "AB4+r9Q4eYueiDzAeZloJYLY4OtcX5Zp52zjvkOtrZn7bX210wvRb1/+QJFIe9FzQg==",
                            Sobrenome = "do Sistema",
                            Username = "admin"
                        });
                });

            modelBuilder.Entity("MinhaCarteira.Comum.Definicao.Entidade.UsuarioPapel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("PapelId")
                        .HasColumnType("int");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PapelId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("UsuarioPapel");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            PapelId = 1,
                            UsuarioId = 1
                        },
                        new
                        {
                            Id = 2,
                            PapelId = 2,
                            UsuarioId = 1
                        });
                });

            modelBuilder.Entity("MinhaCarteira.Comum.Definicao.Entidade.Agendamento", b =>
                {
                    b.HasOne("MinhaCarteira.Comum.Definicao.Entidade.Categoria", "Categoria")
                        .WithMany()
                        .HasForeignKey("CategoriaId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("MinhaCarteira.Comum.Definicao.Entidade.CentroClassificacao", "CentroClassificacao")
                        .WithMany()
                        .HasForeignKey("CentroClassificacaoId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("MinhaCarteira.Comum.Definicao.Entidade.ContaBancaria", "ContaBancaria")
                        .WithMany()
                        .HasForeignKey("ContaBancariaId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("MinhaCarteira.Comum.Definicao.Entidade.Pessoa", "Pessoa")
                        .WithMany()
                        .HasForeignKey("PessoaId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Categoria");

                    b.Navigation("CentroClassificacao");

                    b.Navigation("ContaBancaria");

                    b.Navigation("Pessoa");
                });

            modelBuilder.Entity("MinhaCarteira.Comum.Definicao.Entidade.AgendamentoItem", b =>
                {
                    b.HasOne("MinhaCarteira.Comum.Definicao.Entidade.Agendamento", "Agendamento")
                        .WithMany("Items")
                        .HasForeignKey("AgendamentoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MinhaCarteira.Comum.Definicao.Entidade.ContaBancaria", "ContaBancaria")
                        .WithMany()
                        .HasForeignKey("ContaBancariaId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("MinhaCarteira.Comum.Definicao.Entidade.Pessoa", "Pessoa")
                        .WithMany()
                        .HasForeignKey("PessoaId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Agendamento");

                    b.Navigation("ContaBancaria");

                    b.Navigation("Pessoa");
                });

            modelBuilder.Entity("MinhaCarteira.Comum.Definicao.Entidade.Categoria", b =>
                {
                    b.HasOne("MinhaCarteira.Comum.Definicao.Entidade.Categoria", "CategoriaPai")
                        .WithMany("SubCategoria")
                        .HasForeignKey("IdCategoriaPai")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("CategoriaPai");
                });

            modelBuilder.Entity("MinhaCarteira.Comum.Definicao.Entidade.ContaBancaria", b =>
                {
                    b.HasOne("MinhaCarteira.Comum.Definicao.Entidade.InstituicaoFinanceira", "InstituicaoFinanceira")
                        .WithMany()
                        .HasForeignKey("InstituicaoFinanceiraId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("InstituicaoFinanceira");
                });

            modelBuilder.Entity("MinhaCarteira.Comum.Definicao.Entidade.MovimentoBancario", b =>
                {
                    b.HasOne("MinhaCarteira.Comum.Definicao.Entidade.AgendamentoItem", "AgendamentoItem")
                        .WithMany("Movimentos")
                        .HasForeignKey("AgendamentoItemId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("MinhaCarteira.Comum.Definicao.Entidade.Categoria", "Categoria")
                        .WithMany()
                        .HasForeignKey("CategoriaId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("MinhaCarteira.Comum.Definicao.Entidade.CentroClassificacao", "CentroClassificacao")
                        .WithMany()
                        .HasForeignKey("CentroClassificacaoId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("MinhaCarteira.Comum.Definicao.Entidade.ContaBancaria", "ContaBancaria")
                        .WithMany()
                        .HasForeignKey("ContaBancariaId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("MinhaCarteira.Comum.Definicao.Entidade.Pessoa", "Pessoa")
                        .WithMany()
                        .HasForeignKey("PessoaId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("AgendamentoItem");

                    b.Navigation("Categoria");

                    b.Navigation("CentroClassificacao");

                    b.Navigation("ContaBancaria");

                    b.Navigation("Pessoa");
                });

            modelBuilder.Entity("MinhaCarteira.Comum.Definicao.Entidade.UsuarioPapel", b =>
                {
                    b.HasOne("MinhaCarteira.Comum.Definicao.Entidade.Papel", "Papel")
                        .WithMany("Usuarios")
                        .HasForeignKey("PapelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MinhaCarteira.Comum.Definicao.Entidade.Usuario", "Usuario")
                        .WithMany("Papeis")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Papel");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("MinhaCarteira.Comum.Definicao.Entidade.Agendamento", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("MinhaCarteira.Comum.Definicao.Entidade.AgendamentoItem", b =>
                {
                    b.Navigation("Movimentos");
                });

            modelBuilder.Entity("MinhaCarteira.Comum.Definicao.Entidade.Categoria", b =>
                {
                    b.Navigation("SubCategoria");
                });

            modelBuilder.Entity("MinhaCarteira.Comum.Definicao.Entidade.Papel", b =>
                {
                    b.Navigation("Usuarios");
                });

            modelBuilder.Entity("MinhaCarteira.Comum.Definicao.Entidade.Usuario", b =>
                {
                    b.Navigation("Papeis");
                });
#pragma warning restore 612, 618
        }
    }
}
