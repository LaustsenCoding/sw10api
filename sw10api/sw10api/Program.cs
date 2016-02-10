﻿using System;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.ServiceModel.Description;
using System.Net;
using System.Net.Sockets;

using sw10api.Services;
using sw10api.Interfaces;


namespace sw10api {
    class Program {
        static void Main(string[] args) {
            startRestService();
        }



        static void startRestService() {
            string localIP = "?";
            IPHostEntry myHost = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in myHost.AddressList) {
                if (ip.AddressFamily == AddressFamily.InterNetwork) {
                    localIP = ip.ToString();
                }
            }

            //Uri uri = new Uri("http://192.168.1.206:8000/RestService");
            //Uri uri = new Uri("http://localhost:8000/RestService");
            Uri uri = new Uri("http://" + localIP + ":8000/RestService");
            WebServiceHost host = new WebServiceHost(typeof(RestService), uri);
            host.AddServiceEndpoint(typeof(ITest), new WebHttpBinding(), new Uri(uri + "/ITest"));

            host.Open();

            foreach (ServiceEndpoint se in host.Description.Endpoints) {
                Console.WriteLine(string.Format("Binding name:{0}, Address:{1}, Contract:{2}", se.Binding.Name, se.Address.ToString(), se.Contract.Name));
            }
            Console.ReadLine();
            host.Close();
        }

    }
}
