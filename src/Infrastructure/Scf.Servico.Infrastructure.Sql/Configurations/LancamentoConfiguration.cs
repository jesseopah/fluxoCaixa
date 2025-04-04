using Scf.Servico.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;

namespace Scf.Servico.Sql.Configurations
{
    [ExcludeFromCodeCoverageAttribute]
    public class LancamentoConfiguration : IEntityTypeConfiguration<Tlancamentos>
    {
        public void Configure(EntityTypeBuilder<Tlancamentos> builder)
        {
            builder.ToTable("TLancamentos");

            builder.HasKey(x => x.Codigo);

            builder.Property(x => x.Codigo)
               .HasColumnName("Codigo")
               .HasColumnType("int")
               .HasMaxLength(10);

            builder.Property(x => x.DataDoLancamento)
                .HasColumnName("DataDoLancamento")
                .HasColumnType("DateTime")
                .HasMaxLength(10);

            builder.Property(x => x.DescricaoLancamento)
                .HasColumnName("DescricaoLancamento")
                .HasColumnType("varchar")
                .HasMaxLength(50);

            builder.Property(x => x.TipoLancamento)
               .HasColumnName("TipoLancamento")
               .HasColumnType("int")
               .HasMaxLength(1);

            builder.Property(x => x.ValorLancamento)
               .HasColumnName("ValorLancamento")
               .HasColumnType("decimal")
               .HasMaxLength(12);


        }
    }
}
