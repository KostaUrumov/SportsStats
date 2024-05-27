using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportsStats_Infastructure.DataModels;

namespace SportsStats_Infastructure.Configuration
{
    public class ApplicationTeamsFootballersConfiguration : IEntityTypeConfiguration<TeamsFootballers>
    {
        public void Configure(EntityTypeBuilder<TeamsFootballers> builder)
        {
            builder
                .HasKey(k => new { k.TeamId, k.FootballerId });
        }
    }
}
