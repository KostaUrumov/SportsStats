using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportsStats_Infastructure.DataModels;

namespace SportsStats_Infastructure.Configuration
{
    internal class ApplicationRefereesTournamentsConfiguration : IEntityTypeConfiguration<RefereesTournaments>
    {
        public void Configure(EntityTypeBuilder<RefereesTournaments> builder)
        {

            builder
                .HasKey(x => new { x.TournamentId, x.RefereeId });
        }
    }
}
