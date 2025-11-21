
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SizeFintech.Domain.Models.Entities;

namespace SizeFintech.Infra.Mapping
{
    public class BaseMap<T> : IEntityTypeConfiguration<T> where T : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey("Id");

            builder.Property(b => b.Ativo)
                .HasColumnType("bit")
                .IsRequired();
            builder.Property(b => b.DataCriacao)
                .HasColumnType("datetime")
                .IsRequired();
            builder.Property(b => b.DataExclusao)
                .HasColumnType("datetime")
                .IsRequired(false);
        }
    }
}
