using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using System.ServiceModel;

using CarDataProject;

namespace sw10api.Interfaces {
    [ServiceContract]
    public interface ICompetition {
        [OperationContract]
        string GetCompetitionById(Int16 competitionid);
        [OperationContract]
        string GetAllCompetitions();
        [OperationContract]
        string GetCompetitionsByCarId(Int16 carid);
        [OperationContract]
        void CompetitionSignUp(Int16 carid, Int16 competitionid);
        [OperationContract]
        void CompetitionSignDown(Int16 carid, Int16 competitionid);
        [OperationContract]
        string AddTripToCompetition(Int16 competitionid);
        //[OperationContract]
        //string GetCompetitionRank(Int16 carid, Int16 competitionid, Int64 tripid);
        [OperationContract]
        string GetCompetitionLeaderboard(Int16 competitionid);
    }
}
