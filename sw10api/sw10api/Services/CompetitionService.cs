using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel.Web;
using System.IO;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using sw10api.Interfaces;
using CarDataProject;


namespace sw10api.Services {
    public partial class RestService : ICompetition {

        public string AddTripToCompetition(Int16 competitionid) {
            throw new NotImplementedException();
        }

        [WebInvoke(Method = "GET", UriTemplate = "CompetitionSignUp?carid={carid}&competitionid={competitionid}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public void CompetitionSignUp(Int16 carid, Int16 competitionid) {
            DBController dbc = new DBController();
            dbc.CompetitionSignUp(carid, competitionid);
            dbc.Close();

            return;
        }

        [WebInvoke(Method = "GET", UriTemplate = "CompetitionSignDown?carid={carid}&competitionid={competitionid}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public void CompetitionSignDown(Int16 carid, Int16 competitionid) {
            DBController dbc = new DBController();
            dbc.CompetitionSignDown(carid, competitionid);
            dbc.Close();

            return;
        }

        [WebInvoke(Method = "GET", UriTemplate = "GetAllCompetitions", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public string GetAllCompetitions() {
            DBController dbc = new DBController();
            List<Competition> allCompetitions = dbc.GetAllCompetitions();
            dbc.Close();

            return JsonConvert.SerializeObject(allCompetitions);
        }

        [WebInvoke(Method = "GET", UriTemplate = "GetCompetitionById?competitionid={competitionid}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public string GetCompetitionById(Int16 competitionid) {
            DBController dbc = new DBController();
            Competition competition = dbc.GetCompetitionByCompetitionId(competitionid);
            dbc.Close();

            return JsonConvert.SerializeObject(competition);
        }
        /*
        public string GetCompetitionRank(Int16 carid, Int16 competitionid, Int64 tripid) {
            throw new NotImplementedException();
        }
        */
        [WebInvoke(Method = "GET", UriTemplate = "GetCompetitionsByCarId?carid={carid}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public string GetCompetitionsByCarId(Int16 carid) {
            DBController dbc = new DBController();
            List<Competition> carCompetitions = dbc.GetCompetitionByCarId(carid);
            dbc.Close();

            return JsonConvert.SerializeObject(carCompetitions);
        }

        public string GetCompetitionLeaderboard(short competitionid) {
            throw new NotImplementedException();
        }
    }
}
