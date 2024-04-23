using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using My_Transfermarkt_Infastructure.DataModels;

namespace My_Transfermarkt_Infastructure.Configuration
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
