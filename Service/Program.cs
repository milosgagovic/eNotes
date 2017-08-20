using Common;
using Common.Entities;
using Service.Access;
using System;
using System.Data.Entity;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace Service
{
    public class Program
    {
        private static ServiceHost host;
        private static string baseAddress;

        #region Properties
        public static string BaseAddress
        {
            get
            {
                return baseAddress;
            }

            set
            {
                baseAddress = value;
            }
        }
        #endregion

        static void Main(string[] args)
        {
            Start();
            Console.ReadKey(true);
        }

        private static void Start()
        {
            string executable = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string path = System.IO.Path.GetDirectoryName(executable);
            path = path.Substring(0, path.LastIndexOf("bin")) + "DB";
            AppDomain.CurrentDomain.SetData("DataDirectory", path);
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<AccessDB, Configuration>());

            Group g1 = new Group();
            g1.Name = "Crtanje";

            Group g = new Group();
            g.Name = "Programiranje";

            EBeleznikDB.Instance.AddGroup(g1);
            EBeleznikDB.Instance.AddGroup(g);

            User admin = new User("admin", "admin");
            admin.Name = "admin";
            admin.Surname = "admin";
            admin.Group.Add(g);
            admin.Group.Add(g1);
            EBeleznikDB.Instance.AddUser(admin);

            User u1 = new User("pero", "pero");
            u1.Name = "Pero";
            u1.Surname = "Nikic";
            u1.Group.Add(g);
            EBeleznikDB.Instance.AddUser(u1);



          


            host = new ServiceHost(typeof(EBeleznikSevice));
            host.AddServiceEndpoint(typeof(IServiceConract),
                new NetTcpBinding(),
                new Uri("net.tcp://localhost:4000/IServiceContract"));
            host.Description.Behaviors.Remove(typeof(ServiceDebugBehavior));
            host.Description.Behaviors.Add(new ServiceDebugBehavior() { IncludeExceptionDetailInFaults = true });
            host.Open();


            Console.WriteLine("Host opened");
            Console.WriteLine("Server is ready ! ");
        }
    }
}
