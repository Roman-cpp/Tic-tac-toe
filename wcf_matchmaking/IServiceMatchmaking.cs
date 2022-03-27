using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace wcf_matchmaking
{
    // ПРИМЕЧАНИЕ. Можно использовать команду "Переименовать" в меню "Рефакторинг", чтобы изменить имя интерфейса "IServiceMatchmaking" в коде и файле конфигурации.
    [ServiceContract(CallbackContract =typeof(IserviceCallback))]
    public interface IServiceMatchmaking
    {
        [OperationContract]
        Tuple<int, int, bool> Connect(string nickname);

        [OperationContract]
        void Disconnect(int ID);
        [OperationContract]
        void SendMove(int move, int lobbyID, int senderID);
    }
    public interface IserviceCallback
    {
        [OperationContract(IsOneWay = true)]
        void StartGame();

        [OperationContract(IsOneWay = true)]
        void MoveCallback(int move);
    }
}
