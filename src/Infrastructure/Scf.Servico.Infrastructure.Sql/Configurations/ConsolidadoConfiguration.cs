using Scf.Servico.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;

namespace Scf.Servico.Sql.Configurations
{
    [ExcludeFromCodeCoverageAttribute]
    public class ConsolidadoConfiguration : IEntityTypeConfiguration<TConsolidados>
    {
        public void Configure(EntityTypeBuilder<TConsolidados> builder)
        {
            builder.ToTable("TConsolidados");

            builder.HasKey(x => x.Codigo);

            builder.Property(x => x.Codigo)
               .HasColumnName("Codigo")
               .HasColumnType("int")
               .HasMaxLength(12);

            builder.Property(x => x.DataConsolidado)
               .HasColumnName("DataConsolidado")
               .HasColumnType("DateTime")
               .HasMaxLength(10);            

            builder.Property(x => x.TotalEntrada)
               .HasColumnName("TotalEntrada")
               .HasColumnType("decimal")
               .HasMaxLength(12);

            builder.Property(x => x.TotalSaida)
               .HasColumnName("TotalSaida")
               .HasColumnType("decimal")
               .HasMaxLength(12);

            builder.Property(x => x.ValorConsolidado)
               .HasColumnName("ValorConsolidado")
               .HasColumnType("decimal")
               .HasMaxLength(12);
        }
    }
}
