using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MinhaCarteira.Comum.Definicao.Entidade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinhaCarteira.Servidor.Modelo.Maps
{
    public class CentroClassificacaoMap : IEntityTypeConfiguration<CentroClassificacao>
    {
        public void Configure(EntityTypeBuilder<CentroClassificacao> builder)
        {
            builder.ToTable(nameof(CentroClassificacao));

            builder.HasKey(k => k.Id);
            builder.Property(p => p.Nome).HasMaxLength(200);
        }
    }
}
