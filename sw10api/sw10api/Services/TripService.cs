using System;
using System.Collections.Generic;
using System.ServiceModel.Web;
using System.Linq;

using Newtonsoft.Json;

using sw10api.Interfaces;
using CarDataProject;


namespace sw10api.Services {
    public partial class RestService : ITrip  {
        
        [WebInvoke(Method = "GET", UriTemplate = "GetTrip?carid={carid}&tripid={tripid}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public string GetTrip(Int16 carid, Int64 tripid) {
            try {
                DBController dbc = new DBController();
                Trip trip = dbc.GetTripByCarIdAndTripId(carid, tripid);
                dbc.Close();

                return JsonConvert.SerializeObject(trip);
            } catch (Exception e) {
                Console.WriteLine(e.ToString());
                DBController dbc = new DBController();
                dbc.AddLog("GetTrip?carid={carid}&tripid={tripid}", carid, tripid, null, e.ToString().Substring(0, Math.Min(e.ToString().Count(), 254)), "");
                dbc.Close();
            }

            return "";
        }

        [WebInvoke(Method = "GET", UriTemplate = "GetTrips?carid={carid}", ResponseFormat = WebMessageFormat.Json)]
        public string GetTrips(Int16 carid) {
            try {
                DBController dbc = new DBController();
                List<Trip> trips = dbc.GetTripsByCarId(carid);
                dbc.Close();

                return JsonConvert.SerializeObject(trips);
            } catch (Exception e) {
                Console.WriteLine(e.ToString());
                DBController dbc = new DBController();
                dbc.AddLog("GetTrips?carid={carid}", carid, null, null, e.ToString().Substring(0, Math.Min(e.ToString().Count(), 254)), "");
                dbc.Close();
            }

            return "";
        }

        [WebInvoke(Method = "GET", UriTemplate = "GetTripsForList?carid={carid}&offset={offset}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public string GetTripsForList(Int16 carid, int offset) {
            try {
                DBController dbc = new DBController();
                List<Trip> trips = dbc.GetTripsForListByCarId(carid, offset);
                dbc.Close();

                return JsonConvert.SerializeObject(trips);
            } catch (Exception e) {
                Console.WriteLine(e.ToString());
                DBController dbc = new DBController();
                dbc.AddLog("GetTripsForList?carid={carid}&offset={offset}", carid, null, null, e.ToString().Substring(0, Math.Min(e.ToString().Count(), 254)), offset.ToString());
                dbc.Close();
            }

            return "";
        }

    }
}
