using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using My_Transfermarkt_Infastructure.DataModels;

namespace My_Transfermarkt_Infastructure.Configuration
{
    public class ApplicationGroupTeamsConfiguration : IEntityTypeConfiguration<GroupTeams>
    {
        public void Configure(EntityTypeBuilder<GroupTeams> builder)
        {
            builder
                .HasKey(k => new { k.GroupId, k.TeamId });
        }
    }
}
