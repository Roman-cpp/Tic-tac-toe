using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace wcf_matchmaking
{
    [ServiceBehavior(
  ConcurrencyMode = ConcurrencyMode.Single,
  InstanceContextMode = InstanceContextMode.PerSession,
  ReleaseServiceInstanceOnTransactionComplete = true
)]
    public class Lobby
    {
        public Lobby(int ID, Player player)
        {
            this.ID = ID;
            players = new List<Player>() { player };
        }

        public int ID { get; set; }
        

        public List<Player> players { get; set; }
    }
}
