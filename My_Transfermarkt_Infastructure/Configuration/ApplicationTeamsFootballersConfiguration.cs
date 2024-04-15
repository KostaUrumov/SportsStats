using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using My_Transfermarkt_Infastructure.DataModels;

namespace My_Transfermarkt_Infastructure.Configuration
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
