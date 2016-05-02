using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel.Web;
using System.IO;

using Newtonsoft.Json.Linq;
using Newtonsoft.Json;


using sw10api.Interfaces;
using CarDataProject;

namespace sw10api.Services {
    public partial class RestService : IMeasure {
        [WebInvoke(Method = "POST", UriTemplate = "AddAndUpdateFacts")]
        public string AddAndUpdateFacts(Stream facts) {
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
                Console.WriteLine("Trip " + assignedTripId + " was map-matched.");

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
                dbc.AddLog("AddAndUpdateFacts", (Int16)carIdLog, tripIdLog, null, e.ToString().Substring(0, Math.Min(e.ToString().Count(), 254)), facts.ToString().Substring(0, Math.Min(e.ToString().Count(), 499)));
                dbc.Close();
                return "failed";
            }

            return "succes";
        }

    }
}
