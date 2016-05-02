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
using sw10api.Models;
using CarDataProject;


namespace sw10api.Services {
    public partial class RestService : ICompetition {

        public string AddTripToCompetition(Int16 competitionid, Int16 carid, Int64 tripid) {
            try {
                DBController dbc = new DBController();
                dbc.UpdateWithCompetitionAttempt(competitionid, carid, tripid);
                dbc.Close();
            } catch (Exception e) {
                Console.WriteLine(e.ToString());
                DBController dbc = new DBController();
                dbc.AddLog("AddTripToCompetition", carid, tripid, competitionid, e.ToString().Substring(0, Math.Min(e.ToString().Count(), 254)), "");
                dbc.Close();
            }

            return "succes";
        }

        [WebInvoke(Method = "GET", UriTemplate = "CompetitionSignUp?carid={carid}&competitionid={competitionid}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public void CompetitionSignUp(Int16 carid, Int16 competitionid) {
            try {
                DBController dbc = new DBController();
                dbc.CompetitionSignUp(carid, competitionid);
                dbc.Close();
            } catch (Exception e) {
                Console.WriteLine(e.ToString());
                DBController dbc = new DBController();
                dbc.AddLog("CompetitionSignUp?carid={carid}&competitionid={competitionid}", carid, null, competitionid, e.ToString().Substring(0, Math.Min(e.ToString().Count(), 254)), "");
                dbc.Close();
            }
            return;
        }

        [WebInvoke(Method = "GET", UriTemplate = "CompetitionSignDown?carid={carid}&competitionid={competitionid}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public void CompetitionSignDown(Int16 carid, Int16 competitionid) {
            try {
                DBController dbc = new DBController();
                dbc.CompetitionSignDown(carid, competitionid);
                dbc.Close();
            } catch (Exception e) {
                Console.WriteLine(e.ToString());
                DBController dbc = new DBController();
                dbc.AddLog("CompetitionSignDown?carid={carid}&competitionid={competitionid}", carid, null, competitionid, e.ToString().Substring(0, Math.Min(e.ToString().Count(), 254)), "");
                dbc.Close();
            }

            return;
        }

        [WebInvoke(Method = "GET", UriTemplate = "GetAllCompetitions", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public string GetAllCompetitions() {
            try {
                DBController dbc = new DBController();
                List<Competition> allCompetitions = dbc.GetAllCompetitions();
                dbc.Close();

                return JsonConvert.SerializeObject(allCompetitions);
            } catch (Exception e) {
                Console.WriteLine(e.ToString());
                DBController dbc = new DBController();
                dbc.AddLog("GetAllCompetitions", null, null, null, e.ToString().Substring(0, Math.Min(e.ToString().Count(), 254)), "");
                dbc.Close();
            }

            return "";
        }

        [WebInvoke(Method = "GET", UriTemplate = "GetCompetitionById?competitionid={competitionid}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public string GetCompetitionById(Int16 competitionid) {
            try {
                DBController dbc = new DBController();
                Competition competition = dbc.GetCompetitionByCompetitionId(competitionid);
                dbc.Close();

                return JsonConvert.SerializeObject(competition);
            } catch (Exception e) {
                Console.WriteLine(e.ToString());
                DBController dbc = new DBController();
                dbc.AddLog("GetCompetitionById?competitionid={competitionid}", null, null, competitionid, e.ToString().Substring(0, Math.Min(e.ToString().Count(), 254)), "");
                dbc.Close();
            }

            return "";
        }
        /*
        public string GetCompetitionRank(Int16 carid, Int16 competitionid, Int64 tripid) {
            throw new NotImplementedException();
        }
        */
        [WebInvoke(Method = "GET", UriTemplate = "GetCompetitionsByCarId?carid={carid}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public string GetCompetitionsByCarId(Int16 carid) {
            try {
                DBController dbc = new DBController();
                List<Competition> carCompetitions = dbc.GetCompetitionByCarId(carid);
                dbc.Close();

                return JsonConvert.SerializeObject(carCompetitions);
            } catch (Exception e) {
                Console.WriteLine(e.ToString());
                DBController dbc = new DBController();
                dbc.AddLog("GetCompetitionsByCarId?carid={carid}", carid, null, null, e.ToString().Substring(0, Math.Min(e.ToString().Count(), 254)), "");
                dbc.Close();
            }

            return "";
        }

        public string GetCompetitionLeaderboard(short competitionid) {
            throw new NotImplementedException();
        }

        [WebInvoke(Method = "GET", UriTemplate = "GetCompetitionsForListView?carid={carid}&offset={offset}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public string GetCompetitionsForListView(Int16 carid, int offset) {
            try {
                DBController dbc = new DBController();
                Car car = dbc.GetCarByCarId(carid);
                List<Competition> competitions = dbc.GetAllCompetitionsWithOffset(offset);
                List<Int16> CarCompetitions = dbc.GetCompetitionIdByCarId(carid);
                dbc.Close();

                if (!car.Username.ToLower().StartsWith("lb")) {
                    competitions.Remove(competitions.Single(p => p.CompetitionId == 1));
                }
                List<CompetitionView> competitionsForListView = new List<CompetitionView>();

                foreach (Competition com in competitions) {
                    CompetitionView comView = new CompetitionView(com);

                    if (CarCompetitions.Contains(com.CompetitionId)) {
                        comView.IsParticipating = true;
                    }

                    //When we know how to log it...
                    comView.ParticipantCount = 0;
                    comView.Rank = 0;
                    comView.AttemptCount = 0;

                    competitionsForListView.Add(comView);
                }

                return JsonConvert.SerializeObject(competitionsForListView);

            } catch (Exception e) {
                Console.WriteLine(e.ToString());
                DBController dbc = new DBController();
                dbc.AddLog("GetCompetitionsForListView?carid={carid}&offset={offset}", carid, null, null, e.ToString().Substring(0, Math.Min(e.ToString().Count(), 254)), offset.ToString());
                dbc.Close();
            }

            return "";
        }
    }
}
