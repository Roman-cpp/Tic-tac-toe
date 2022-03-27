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
    public class Player
    {
       
        public int ID { get; set; }
       
        public string Name { get; set; }
       
        public OperationContext operationContext { get; set; }
    }
}
