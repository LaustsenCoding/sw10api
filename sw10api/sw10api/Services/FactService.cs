using System;
using System.Collections.Generic;
using System.ServiceModel.Web;
using System.Linq;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using sw10api.Interfaces;
using CarDataProject;


namespace sw10api.Services {
    public partial class RestService : IFact {
        /*
        Husk at fix i IFact hvis du vil bruge denne igen
        [WebInvoke(Method = "GET", UriTemplate = "GetFact?tripid={tripid}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public string GetFact(Int64 tripid) {

            DBController dbc = new DBController();
            List<Fact> facts = dbc.GetFactsByTripId(tripid);
            dbc.Close();

            Console.WriteLine(facts[1].ToString());

            return JsonConvert.SerializeObject(facts[1]);
        }
        */

        [WebInvoke(Method = "GET", UriTemplate = "GetFacts?carid={carid}&tripid={tripid}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public string GetFacts(Int16 carid, Int64 tripid) {
            try {
                DBController dbc = new DBController();
                List<Fact> facts = dbc.GetFactsByCarIdAndTripIdNoQuality(carid, tripid);
                dbc.Close();

                return JsonConvert.SerializeObject(facts);
            } catch (Exception e) {
                Console.WriteLine(e.ToString());
                DBController dbc = new DBController();
                dbc.AddLog("GetFacts?carid={carid}&tripid={tripid}", carid, tripid, null, e.ToString().Substring(0, Math.Min(e.ToString().Count(), 254)), "");
                dbc.Close();
            }

            return "";
        }

        [WebInvoke(Method = "GET", UriTemplate = "GetFactsForMap?carid={carid}&tripid={tripid}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public string GetFactsForMap(Int16 carid, Int64 tripid) {
            try {
                DBController dbc = new DBController();
                List<Fact> facts = dbc.GetFactsForMapByCarIdAndTripId(carid, tripid);
                dbc.Close();

                return JsonConvert.SerializeObject(facts);
            } catch (Exception e) {
                Console.WriteLine(e.ToString());
                DBController dbc = new DBController();
                dbc.AddLog("GetFactsForMap?carid={carid}&tripid={tripid}", carid, tripid, null, e.ToString().Substring(0, Math.Min(e.ToString().Count(), 254)), "");
                dbc.Close();
            }

            return "";

        }

        [WebInvoke(Method = "GET", UriTemplate = "GetMyTest", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public string GetMyTest() {
            return "Jamen Andreas, Der er hul igennem";
        }

        /*
        [WebInvoke(Method = "GET", UriTemplate = "GetMyTest", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public Test GetMyTest() {
            Test t = new Test(1234567);

            return t;
        }
        */
        [WebInvoke(Method = "POST", UriTemplate = "AddFacts")]
        public string AddFacts(Stream facts) {
            int carIdLog = -1;
            Int64 tripIdLog = -1;

            try {
                //Reading the stream of data sent from client
                Console.WriteLine("Message Received");
                StreamReader sr = new StreamReader(facts);
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

                carIdLog = carId;
                tripIdLog = assignedTripId;

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
                    dbc.AddRawFact(f);
                }
                dbc.Close();

                Mapmatch.MatchTrip(carId, assignedTripId);
                Console.WriteLine("Trip " + assignedTripId + " was map-matched for CarId " + carId + ".");

                Console.WriteLine("Updating Facts for trip " + assignedTripId + ".");
                GPSFactUpdater.UpdateRawGPS(carId, assignedTripId);
                Console.WriteLine("Number of facts: " + factObjs.Count);

                Console.WriteLine("Updating trip " + assignedTripId + ".");
                TripFactUpdater.UpdateTrip(carId, assignedTripId);
                Console.WriteLine("Update on trip " + assignedTripId + " completed.");

                Car car = dbc.GetCarByCarId(carId);
                if (car.Username.ToLower().StartsWith("lb")) {
                    Console.WriteLine("User is a member of the LB competition");
                    Console.WriteLine("Updating this tripscore in the competition");
                    AddTripToCompetition(1, carId, assignedTripId);
                    Console.WriteLine("Update complete.");
                }

            } catch (Exception e) {
                Console.WriteLine(e.ToString());
                DBController dbc = new DBController();
                dbc.AddLog("AddFacts", (Int16)carIdLog, tripIdLog, null, e.ToString().Substring(0, Math.Min(e.ToString().Count(), 254)), facts.ToString().Substring(0, Math.Min(e.ToString().Count(), 499)));
                dbc.Close();
                return "failed";
            }

            return "succes";
        }
    }
}
