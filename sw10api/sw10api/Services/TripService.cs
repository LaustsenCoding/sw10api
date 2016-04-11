using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel.Web;
using sw10api.Interfaces;
using sw10api.Models;
using System.Data;


using CarDataProject;


namespace sw10api.Services {
    public partial class RestService : ITrip  {

        [WebInvoke(Method = "GET", UriTemplate = "GetTrip?carid={carid}&tripid={tripid}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public Trip GetTrip(Int16 carid, Int64 tripid) {

            DBController dbc = new DBController();
            DataRow row = dbc.GetTripViewByCarIdAndTripId(carid, tripid);
            dbc.Close();
            TripView trip = new TripView(row);

            return trip;
        }

        [WebInvoke(Method = "GET", UriTemplate = "GetTrips?carid={carid}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public List<Trip> GetTrips(Int16 carid) {

            DBController dbc = new DBController();

            List<Trip> trips = dbc.GetTripsByCarId(carid);
            dbc.Close();
            
            return trips;
        }

        [WebInvoke(Method = "GET", UriTemplate = "GetTripsForList?carid={carid}&offset={offset}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public List<Trip> GetTripsForTripList(Int16 carid, int offset) {

            DBController dbc = new DBController();

            List<Trip> trips = dbc.GetTripsForListByCarId(carid, offset);

            dbc.Close();

            return trips;
        }

    }
}
