using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel.Web;
using System.IO;

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



        public string GetAllCompetitions() {
            throw new NotImplementedException();
        }

        [WebInvoke(Method = "GET", UriTemplate = "GetCompetitionById?competitionid={competitionid}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public string GetCompetitionById(Int16 competitionid) {
            

            return "";
        }

        public string GetCompetitionRank(Int16 carid, Int16 competitionid) {
            throw new NotImplementedException();
        }

        public string GetCompetitionsByCarId(Int16 carid) {
            throw new NotImplementedException();
        }
    }
}
