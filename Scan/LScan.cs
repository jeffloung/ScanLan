using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Scan
{
    public enum ScanNetMode
    {
        IpAddr,NetAdapter
    }
    class LScan
    {
        public bool allowdebug = false;
        public ScanNetMode mode = ScanNetMode.IpAddr;


        public void Init()
        {
            switch (mode)
            {
                case ScanNetMode.IpAddr:
                    InitByIpAddr();
                    break;
                case ScanNetMode.NetAdapter:
                    InitByNetAdapter();
                    break;
            }
        }

        NetworkInterface[] nics;
        public void InitByNetAdapter()
        {
            nics = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface adapter in nics)
            {
                Debug("type:" + adapter.NetworkInterfaceType);

                //if (adapter.NetworkInterfaceType == NetworkInterfaceType.Wireless80211
                //    || adapter.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
                //{
                //    Debug("name:" + adapter.Name);
                //    IPInterfaceProperties ipIp = adapter.GetIPProperties();
                //    Debug("ipIp:" + ipIp.UnicastAddresses[0].Address.ToString());
                //    Debug("ipmask:" + ipIp.UnicastAddresses[0].IPv4Mask.ToString());

                //}
            }
        }

        #region "InitByIpAddr"
        string hostName;
        IPHostEntry localHost;
        IPAddress[] ipAddrAll;
        List<IPAddress> ipAddrV4;


        public void InitByIpAddr()
        {
            ipAddrV4 = new List<IPAddress>();
            hostName = Dns.GetHostName();
            localHost = Dns.GetHostEntry(hostName);
            ipAddrAll = localHost.AddressList;
            Debug("hostname:" + hostName);
            for (int i = 0; i < ipAddrAll.Length; i++)
            {
                Debug(i + ":ipaddrall:" + ipAddrAll[i]);
                if (ipAddrAll[i].AddressFamily == AddressFamily.InterNetwork)
                {
                    Debug(i + ":ipaddrv4:" + ipAddrAll[i]);
                    ipAddrV4.Add(ipAddrAll[i]);
                }
            }
        }
        #endregion

        public void DoScan()
        {

        }

        void Debug(string debuginfo)
        {
            if (allowdebug)
            {
                Console.WriteLine(debuginfo);
            }
        }
    }
}
