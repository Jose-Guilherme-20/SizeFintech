
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

            builder.Property<bool>("Ativo")
                .IsRequired();
            builder.Property<DateTime>("DataCriacao")
                .HasColumnType("datetime")
                .IsRequired();
            builder.Property<DateTime?>("DataExclusao")
                .HasColumnType("datetime")
                .IsRequired(false);
        }
    }
}
