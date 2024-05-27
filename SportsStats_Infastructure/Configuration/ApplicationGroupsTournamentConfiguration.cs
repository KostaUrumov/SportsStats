using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportsStats_Infastructure.DataModels;

namespace SportsStats_Infastructure.Configuration
{
    public class ApplicationGroupsTournamentConfiguration : IEntityTypeConfiguration<GroupsTournament>
    {
        public void Configure(EntityTypeBuilder<GroupsTournament> builder)
        {
            builder
                .HasKey(k => new { k.TournamenId, k.GroupId });
        }
    }
}
