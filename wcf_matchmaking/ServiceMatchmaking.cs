using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;

using System.Text;

namespace wcf_matchmaking
{
    //ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "ServiceMatchmaking" в коде и файле конфигурации.
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class ServiceMatchmaking : IServiceMatchmaking
    {
        List<Lobby> lobbys = new List<Lobby>();

        int nextIDp = 1;
        int nextIDl = 1;
        public Tuple<int, int, bool> Connect(string nickname)
        {
            int lobbyID;
            bool isGame = false;

            Player player = new Player()
            {
                ID = nextIDp,
                Name = nickname,
                operationContext = OperationContext.Current
            };

            Lobby freeLobby = null;
            foreach (var Lobby in lobbys)
            {
                if(Lobby.players.Count == 1)
                {
                    freeLobby = Lobby;
                    break;
                }
            }

            if(freeLobby == null)
            {
                Lobby lobby = new Lobby(nextIDl, player);
                lobbyID = lobby.ID;
                lobbys.Add(lobby);     
            }
            else
            {
                foreach (var opponent in freeLobby.players)
                    opponent.operationContext.GetCallbackChannel<IserviceCallback>().StartGame();
                freeLobby.players.Add(player);
                lobbyID = freeLobby.ID;
                isGame = true;    
            }
            

            nextIDp++;
            nextIDl++;

            return new Tuple<int, int, bool>(lobbyID, player.ID, isGame);
        }

        public void Disconnect(int ID)
        {
            var lobby = lobbys.FirstOrDefault(lobby_ => lobby_.ID == ID);
            if (lobby != null)
                lobbys.Remove(lobby);
        }

        public void SendMove(int move, int lobbyID, int senderID)
        {
            var Lobby = lobbys.FirstOrDefault(lobby_ => lobby_.ID == lobbyID);
            var opponent = Lobby.players.FirstOrDefault(player_ => player_.ID != senderID);
            if (opponent != null)
                opponent.operationContext.GetCallbackChannel<IserviceCallback>().MoveCallback(move);
        }
    }
}
