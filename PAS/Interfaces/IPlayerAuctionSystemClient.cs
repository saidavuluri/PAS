using PAS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAS.Interfaces
{
    public interface IPlayerAuctionSystemClient
    {
        int AddPlayer(TeamPlayerObject teamPlayerObject);

        List<Player> DisplayPlayer(string TeamName);
    }
}
