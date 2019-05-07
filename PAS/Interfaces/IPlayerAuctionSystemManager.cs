using PAS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAS.Interfaces
{
    public interface IPlayerAuctionSystemManager
    {
        int AddPlayer(string teamName,Player player);

        List<Player> DisplayPlayer(string teamName);
    }
}
