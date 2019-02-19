using LivrariaOnline.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LivrariaOnline.Infra.Data.Mapping
{
    public class BookMap : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.Ignore(b => b.Valid);
            builder.Ignore(b => b.ValidationResult);
            builder.Ignore(b => b.Invalid);

            builder.HasKey(b => b.Id);            
            builder.Property(b => b.Autor).HasMaxLength(100).IsRequired();            
            builder.Property(b => b.Nome).HasMaxLength(100).IsRequired();
            builder.Property(b => b.PathImage).IsRequired();
            builder.Property(b => b.Isbn).IsRequired();
            builder.Property(b => b.Preco).IsRequired();
            builder.Property(b => b.DataPublicacao).IsRequired();
        }
    }
}
