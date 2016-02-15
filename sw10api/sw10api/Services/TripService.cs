using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel.Web;
using sw10api.Interfaces;
using sw10api.Models;

using CarDataProject;


namespace sw10api.Services {
    public partial class RestService : ITrip  {

        [WebInvoke(Method = "GET", UriTemplate = "GetTrip?tripid={tripid}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public Trip GetTrip(int tripid) {
            
            DBController dbc = new DBController();

            Trip trip = dbc.GetTripByTripId(tripid);
            dbc.Close();

            return trip;
        }

        [WebInvoke(Method = "GET", UriTemplate = "GetTrips?carid={carid}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public List<Trip> GetTrips(Int16 carid) {

            DBController dbc = new DBController();

            List<Trip> trips = dbc.GetTripsByCarId(carid);
            dbc.Close();
            
            return trips;
        }   

    }
}
