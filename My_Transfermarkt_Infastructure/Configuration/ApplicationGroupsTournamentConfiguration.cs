using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using My_Transfermarkt_Infastructure.DataModels;

namespace My_Transfermarkt_Infastructure.Configuration
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
