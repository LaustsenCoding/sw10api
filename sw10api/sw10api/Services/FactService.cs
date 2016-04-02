using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.ServiceModel.Web;
using System.IO;

using Newtonsoft.Json.Linq;

using sw10api.Interfaces;
using CarDataProject;

using sw10api.Models;


namespace sw10api.Services {
    public partial class RestService : IFact  {

        [WebInvoke(Method = "GET", UriTemplate = "GetFact?tripid={tripid}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public Fact GetFact(Int64 tripid) {

            DBController dbc = new DBController();
            List<Fact> facts = dbc.GetFactsByTripId(tripid);
            dbc.Close();

            Console.WriteLine(facts[1].ToString());

            return facts[1];
        }

        [WebInvoke(Method = "GET", UriTemplate = "GetFacts?carid={carid}&tripid={tripid}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public List<Fact> GetFacts(Int16 carid, Int64 tripid) {

            DBController dbc = new DBController();
            List<Fact> facts = dbc.GetFactsByCarIdAndTripId(carid, tripid);
            dbc.Close();

            Console.WriteLine(facts[1].ToString());
            try {
                return facts;
            } catch(Exception e) {
                Console.WriteLine(e.ToString());
            }
            return null;
        }

        [WebInvoke(Method = "GET", UriTemplate = "GetFactsForMap?carid={carid}&tripid={tripid}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public List<Fact> GetFactsForMap(Int16 carid, Int64 tripid) {

            DBController dbc = new DBController();
            List<Fact> facts = dbc.GetFactsForMapByCarIdAndTripId(carid, tripid);
            dbc.Close();

            try {
                return facts;
            } catch (Exception e) {
                Console.WriteLine(e.ToString());
            }
            return null;
        }



        /*
        [WebInvoke(Method = "GET", UriTemplate = "GetMyTest", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public Test GetMyTest() {
            Test t = new Test(1234567);

            return t;
        }
        */
        [WebInvoke(Method = "POST", UriTemplate = "AddFacts", ResponseFormat = WebMessageFormat.Json)]
        public void AddFacts(Stream facts) {
            //Reading the stream of data sent from client
            Console.WriteLine("Message Received");
            var sr = new StreamReader(facts);
            string text = sr.ReadToEnd();
            Console.WriteLine("It was: " + text);
            
            //Parsing the nested JSON objects to a JArray
            JArray allfacts = JArray.Parse(text) as JArray;
            dynamic allthefacts = allfacts;

            //Assigning a tripid
            DBController dbc = new DBController();
            Int16 carId = (Int16)allthefacts[0].carid;
            Int64 assignedTripId = dbc.AddTripInformation(carId);
            dbc.Close();

            Console.WriteLine("Assigned TripId: " + assignedTripId);

            //Converting each JSON object to a Fact object
            List<Fact> factObjs = new List<Fact>();

            foreach (dynamic fact in allthefacts) {
                fact.tripid = assignedTripId;
                factObjs.Add(new Fact(fact));
            }


            //Add Facts to DB
            dbc = new DBController();

            foreach (Fact f in factObjs) {
                dbc.AddFact(f);
            }

            dbc.Close();


            Console.WriteLine("Number of facts: " + factObjs.Count());
            TripFactUpdater.UpdateTrip(carId, assignedTripId);

            return;
        }
        
        

    }
}
