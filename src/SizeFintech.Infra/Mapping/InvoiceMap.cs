
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SizeFintech.Domain.Models.Entities;

namespace SizeFintech.Infra.Mapping
{
    public class InvoiceMap : BaseMap<InvoiceEntity>
    {
        public override void Configure(EntityTypeBuilder<InvoiceEntity> builder)
        {
            base.Configure(builder);

            builder.ToTable("Invoice");

            builder.Property(x => x.ValorBruto)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(x => x.ValorLiquido)
                .HasColumnType("decimal(18,2)")
                .IsRequired(false);

            builder.Property(x => x.DataVencimento)
                .HasColumnType("datetime")
                .IsRequired();

            builder.Property(x => x.EmpresaId)
                .IsRequired();

            builder.Property(x => x.CarrinhoId)
                .IsRequired(false);

            builder.HasOne(x => x.Empresa)
                .WithMany(x => x.NotasFiscais)
                .HasForeignKey(x => x.EmpresaId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Carrinho)
             .WithMany(x => x.NotasFiscais)
             .HasForeignKey(x => x.CarrinhoId)
             .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
