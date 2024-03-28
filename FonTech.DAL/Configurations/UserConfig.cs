using FonTech.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FonTech.DAL.Configurations
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(x=>x.Id).ValueGeneratedOnAdd();
            builder.Property(x=>x.Login).IsRequired().HasAnnotation("MinLength", 3).HasMaxLength(100);
            builder.Property(x => x.Password).IsRequired().HasAnnotation("MinLength", 8);

            builder.HasMany<Report>(x => x.Reports)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId)
                .HasPrincipalKey(x => x.Id);
        }
    }
}
