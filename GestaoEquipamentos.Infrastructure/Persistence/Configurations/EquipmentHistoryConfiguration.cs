using GestaoEquipamentos.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GestaoEquipamentos.Infrastructure.Persistence.Configurations;

public class EquipmentHistoryConfiguration : IEntityTypeConfiguration<EquipmentHistory>
{
    public void Configure(EntityTypeBuilder<EquipmentHistory> builder)
    {
        builder.Property(h => h.Action)
            .IsRequired()
            .HasMaxLength(200);
        
        builder.HasOne<Equipment>()
            .WithMany()
            .HasForeignKey(h => h.EquipmentId);

    }
    
}
