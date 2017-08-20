using Common;
using Common.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Windows;

namespace eBeleznik
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public readonly static string HostAddress = "net.tcp://localhost:4000/IServiceContract";
        private static IServiceConract proxy;
        private static User loggedUser;


        public static User LoggedUser
        {
            get { return loggedUser; }
            set { loggedUser = value; }
        }

        public static IServiceConract Proxy
        {
            get
            {
                return proxy;
            }

            set
            {
                proxy = value;
            }
        }

     
        static App()
        {
            Proxy = new ClientProxy(new NetTcpBinding(), HostAddress);
        }

        public App()
        {

        }
        
      
    }
}
