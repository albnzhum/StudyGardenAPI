using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudyGarden.Core.Models;

namespace StudyGarden.DataAccess.Configuration;

public class PlantTypeConfiguration : IEntityTypeConfiguration<PlantType>
{
    public void Configure(EntityTypeBuilder<PlantType> builder)
    {
        builder.HasKey(pt => pt.ID);
        
        builder.Property(pt => pt.Name)
            .IsRequired();
    }
}