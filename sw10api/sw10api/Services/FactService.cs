using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel.Web;

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
            List<Fact> facts = dbc.GetFactsByCarIdANDTripId(carid, tripid);
            dbc.Close();

            Console.WriteLine(facts[1].ToString());

            return facts;
        }

        [WebInvoke(Method = "GET", UriTemplate = "GetMyTest", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public Test GetMyTest() {
            Test t = new Test(1234567);

            return t;
        }

        


    }
}
