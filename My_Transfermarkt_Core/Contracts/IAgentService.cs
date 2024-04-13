namespace My_Transfermarkt_Core.Contracts
{
    public interface IAgentService
    {
        Task SignFootballerToMe(string userId, int footballerId);
    }
}
