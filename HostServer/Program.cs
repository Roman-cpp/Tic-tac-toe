using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace HostServer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (var home = new ServiceHost(typeof(wcf_matchmaking.ServiceMatchmaking)))
            {
                home.Open();
                Console.WriteLine("Start");
                Console.ReadKey();
            }
        }
    }
}
