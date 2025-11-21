
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SizeFintech.Domain.Models.Entities;

namespace SizeFintech.Infra.Mapping
{
    public class CompanyMap : BaseMap<CompanyEntity>
    {
        public override void Configure(EntityTypeBuilder<CompanyEntity> builder)
        {
            base.Configure(builder);

            builder.ToTable("Company");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Nome)
                .HasColumnType("varchar(200)")
                .IsRequired();

            builder.Property(e => e.Cnpj)
                .HasColumnType("varchar(14)")
                .IsRequired();

            builder.Property(e => e.Faturamento)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(e => e.LimiteCredito)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(e => e.RamoId)
                .IsRequired();

            builder.HasMany(e => e.NotasFiscais)
                .WithOne(e => e.Empresa)
                .HasForeignKey(e => e.EmpresaId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.Carrinho)
               .WithOne(e => e.Empresa)
               .HasForeignKey<CartEntity>(e => e.EmpresaId)
               .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
