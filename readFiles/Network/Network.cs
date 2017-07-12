using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace readFiles {
    class Network {
        public static void Connect() {
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            IPEndPoint ipPoint = new IPEndPoint(ip, 80);
            IPHostEntry entry = Dns.GetHostEntry("www.baidu.com");
            foreach (var item in entry.AddressList) {
                Console.WriteLine(item.Address.ToString());
            }

        }
    }
}
