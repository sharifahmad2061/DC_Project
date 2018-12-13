using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace Client
{
    class AdapterHandling
    {
        public static IPAddress AdaptersAddress(NetworkInterfaceType networkInterfaceType)
        {
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface adapter in nics)
            {
                if (adapter.NetworkInterfaceType != networkInterfaceType)
                    continue;
                if (adapter.NetworkInterfaceType == NetworkInterfaceType.Loopback)
                    continue;
                if (adapter.NetworkInterfaceType == NetworkInterfaceType.Tunnel)
                    continue;
                if (!adapter.SupportsMulticast)
                    continue; // multicast is meaningless for this type of connection
                if (adapter.OperationalStatus != OperationalStatus.Up)
                    continue; // this adapter is off or not connected
                IPv4InterfaceProperties p = adapter.GetIPProperties().GetIPv4Properties();
                if (p == null)
                    continue; // IPv4 is not configured on this adapter
                foreach (UnicastIPAddressInformation ip in adapter.GetIPProperties().UnicastAddresses)
                {
                    if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                    {
                        return ip.Address;
                    }
                }
            }
            return null;
        }
    }
}
