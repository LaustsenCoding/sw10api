using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.ServiceModel.Description;
using System.Net;
using System.Net.Sockets;

using sw10api.Services;
using sw10api.Interfaces;

using CarDataProject;


namespace sw10api {
    class Program {
        static void Main(string[] args) {
            /*
            DBController dbc = new DBController();
            List<Trip> trips = dbc.GetTripsByCarId(1);
            dbc.Close();
            foreach(Trip trip in trips) {

                Console.WriteLine(trip.ToString());
            }
            Console.ReadLine();
            */

            

            
            startRestService();
        }

        static void startRestService() {
            /*
            string localIP = "?";
            IPHostEntry myHost = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in myHost.AddressList) {
                if (ip.AddressFamily == AddressFamily.InterNetwork) {
                    localIP = ip.ToString();
                }
            }
            */

            //Uri uri = new Uri("http://192.168.1.206:8000/RestService");



            //Uri uri = new Uri("http://localhost:8000/RestService");
            Uri uri = new Uri("http://172.19.1.124:9220/RestService");
            WebServiceHost host = new WebServiceHost(typeof(RestService));

            ServiceEndpoint endpoint3 = host.AddServiceEndpoint(typeof(IFact), new WebHttpBinding(), new Uri(uri + "/Fact"));

            ServiceEndpoint endpoint1 = host.AddServiceEndpoint(typeof(ITest), new WebHttpBinding(), new Uri(uri + "/Test"));
            //ServiceEndpoint endpoint1 = host.AddServiceEndpoint(typeof(ITest), new WebHttpBinding(), "");

            ServiceEndpoint endpoint2 = host.AddServiceEndpoint(typeof(ITrip), new WebHttpBinding(), new Uri(uri + "/Trip"));
            //endpoint2.EndpointBehaviors.Add(new WebHttpBehavior());
            ServiceEndpoint endpoint4 = host.AddServiceEndpoint(typeof(ICar), new WebHttpBinding(), new Uri(uri + "/Car"));

            ServiceEndpoint endpoint5 = host.AddServiceEndpoint(typeof(ICompetition), new WebHttpBinding(), new Uri(uri + "/Competition"));

            //WebHttpBinding binding = new WebHttpBinding();
            //binding.MaxBufferSize = int.MaxValue;
            //binding.MaxReceivedMessageSize = int.MaxValue;

            //endpoint3.EndpointBehaviors.Add(new WebHttpBehavior());

            host.Open();

            foreach (ServiceEndpoint se in host.Description.Endpoints) {
                Console.WriteLine(string.Format("Binding name:{0}, Address:{1}, Contract:{2}", se.Binding.Name, se.Address.ToString(), se.Contract.Name));
            }
            Console.ReadLine();

        }

    }
}


/*
        var binding = new WebHttpBinding()
        {
            Name = "JSON",
            MaxReceivedMessageSize = 1000000,
            MaxBufferSize = 1000000,
            ReaderQuotas = { MaxStringContentLength = 1000000 },
            HostNameComparisonMode = HostNameComparisonMode.StrongWildcard,
            Security = { Mode = WebHttpSecurityMode.None }
        };

*/
