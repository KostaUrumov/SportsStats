using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using My_Transfermarkt_Infastructure.DataModels;

namespace My_Transfermarkt_Infastructure.Configuration
{
    internal class ApplicationAgentsFootballersConfiguration : IEntityTypeConfiguration<AgentsFootballers>
    {
        public void Configure(EntityTypeBuilder<AgentsFootballers> builder)
        {
            builder
               .HasKey(k => new { k.AgentId, k.FootballerId });

        }
    }
}
