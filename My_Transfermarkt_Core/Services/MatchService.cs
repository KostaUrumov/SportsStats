using My_Transfermarkt.Data;
using My_Transfermarkt_Core.Contracts;

namespace My_Transfermarkt_Core.Services
{
    public class MatchService : IMatchService
    {
        private readonly ApplicationDbContext data;

        public MatchService(ApplicationDbContext _data)
        {
            data = _data;
        }
    }
}
