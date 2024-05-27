using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportsStats_Infastructure.DataModels;

namespace SportsStats_Infastructure.Configuration
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
