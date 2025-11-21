
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SizeFintech.Domain.Models.Entities;

namespace SizeFintech.Infra.Mapping
{
    public class CartMap : BaseMap<CartEntity>
    {
        public override void Configure(EntityTypeBuilder<CartEntity> builder)
        {
            base.Configure(builder);

            builder.ToTable("Cart");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.TotalBruto)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(e => e.TotalLiquido)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.HasOne(e => e.Empresa)
                .WithMany(c => c.Carrinhos)
                .HasForeignKey(e => e.EmpresaId)
                .IsRequired();
        }
    }
}
