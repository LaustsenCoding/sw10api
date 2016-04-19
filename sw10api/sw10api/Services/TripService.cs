using System;
using System.Collections.Generic;
using System.ServiceModel.Web;

using Newtonsoft.Json;

using sw10api.Interfaces;
using CarDataProject;


namespace sw10api.Services {
    public partial class RestService : ITrip  {
        /*
        [WebInvoke(Method = "GET", UriTemplate = "GetTrip?carid={carid}&tripid={tripid}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public Trip GetTrip(Int16 carid, Int64 tripid) {

            DBController dbc = new DBController();
            DataRow row = dbc.GetTripViewByCarIdAndTripId(carid, tripid);
            dbc.Close();
            TripView trip = new TripView(row);

            return trip;
        }
        */

        [WebInvoke(Method = "GET", UriTemplate = "GetTrips?carid={carid}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public string GetTrips(Int16 carid) {

            DBController dbc = new DBController();

            List<Trip> trips = dbc.GetTripsByCarId(carid);
            dbc.Close();

            return JsonConvert.SerializeObject(trips);
        }

        [WebInvoke(Method = "GET", UriTemplate = "GetTripsForList?carid={carid}&offset={offset}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public string GetTripsForList(Int16 carid, int offset) {

            DBController dbc = new DBController();

            List<Trip> trips = dbc.GetTripsForListByCarId(carid, offset);

            dbc.Close();

            return JsonConvert.SerializeObject(trips);
        }

    }
}
