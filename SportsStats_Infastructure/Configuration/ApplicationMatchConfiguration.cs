using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportsStats_Infastructure.DataModels;

namespace SportsStats_Infastructure.Configuration
{
    public class ApplicationMatchConfiguration : IEntityTypeConfiguration<Match>
    {
        public void Configure(EntityTypeBuilder<Match> builder)
        {
            builder
                .HasOne(t => t.HomeTeam)
                .WithMany(x => x.HomeGames)
                .OnDelete(DeleteBehavior.Restrict);
            builder
                 .HasOne(p => p.AwayTeam)
                 .WithMany(s => s.AwayGames)
                 .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
