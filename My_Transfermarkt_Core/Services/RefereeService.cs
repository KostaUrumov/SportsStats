using My_Transfermarkt.Data;
using My_Transfermarkt_Core.Contracts;

namespace My_Transfermarkt_Core.Services
{
    public class RefereeService : IRefereeService
    {
        private readonly ApplicationDbContext data;

        public RefereeService(ApplicationDbContext _data)
        {
            data = _data;
        }
    }
}
