using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using My_Transfermarkt_Infastructure.DataModels;

namespace My_Transfermarkt_Infastructure.Configuration
{
    public class ApplicationFootballerConfiguration : IEntityTypeConfiguration<Footballer>
    {
        public void Configure(EntityTypeBuilder<Footballer> builder)
        {
            builder
              .HasData
              (
                  new Footballer
                  {
                      Id = 1,
                      CountryId = 1,
                      FirstName = "Tsanko",
                      LastName = "Tsvetanov",
                      BirthDay = DateTime.Parse("2000/03/10"),
                      Position = My_Transfermarkt_Infastructure.Enums.Position.Defender,
                      PreferedFoot = My_Transfermarkt_Infastructure.Enums.Foot.Right,
                      InternationalCaps = 15,
                      CurrentMarketValue = 150000,
                      HighestValue = 150000,
                      HishestValueDate = DateTime.Now,
                      IsRetired = false
                  },

                   new Footballer
                   {
                       Id = 2,
                       CountryId = 1,
                       FirstName = "Milen",
                       LastName = "Gamakov",
                       BirthDay = DateTime.Parse("1997/03/10"),
                       Position = My_Transfermarkt_Infastructure.Enums.Position.Midfielder,
                       PreferedFoot = My_Transfermarkt_Infastructure.Enums.Foot.Right,
                       InternationalCaps = 3,
                       CurrentMarketValue = decimal.Parse("170000,23"),
                       HighestValue = decimal.Parse("170000,23"),
                       HishestValueDate = DateTime.Now,
                       IsRetired = false
                   },

                    new Footballer
                    {
                        Id = 3,
                        CountryId = 1,
                        FirstName = "Ivan",
                        LastName = "Petkov",
                        BirthDay = DateTime.Parse("1997/03/10"),
                        Position = My_Transfermarkt_Infastructure.Enums.Position.Forward,
                        PreferedFoot = My_Transfermarkt_Infastructure.Enums.Foot.Right,
                        InternationalCaps = 11,
                        CurrentMarketValue = decimal.Parse("200000,23"),
                        HighestValue = decimal.Parse("200000,23"),
                        HishestValueDate = DateTime.Now,
                        IsRetired = false
                    }
             );
            builder.Property(p => p.HighestValue)
                .HasPrecision(18, 2);

            builder.Property(p => p.CurrentMarketValue)
                .HasPrecision(18, 2);
        }
    }
}
