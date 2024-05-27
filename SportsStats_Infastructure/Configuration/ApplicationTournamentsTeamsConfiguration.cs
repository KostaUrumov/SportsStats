using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportsStats_Infastructure.DataModels;

namespace SportsStats_Infastructure.Configuration
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
