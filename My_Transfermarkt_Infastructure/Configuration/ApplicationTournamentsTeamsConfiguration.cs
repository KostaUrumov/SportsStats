using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using My_Transfermarkt_Infastructure.DataModels;

namespace My_Transfermarkt_Infastructure.Configuration
{
    public class ApplicationTournamentsTeamsConfiguration : IEntityTypeConfiguration<TournamentsTeams>
    {
        public void Configure(EntityTypeBuilder<TournamentsTeams> builder)
        {
            builder
                .HasKey(x => new { x.TournamentId, x.TeamId });
        }
    }
}
