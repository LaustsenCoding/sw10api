using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using CarDataProject;

namespace sw10api.Models {
    [DataContract]
    public class CompetitionView {
        [DataMember(Name = "competitionid")]
        public Int16 CompetitionId { get; set; }
        [DataMember(Name = "competitionname")]
        public string CompetitionName { get; set; }
        [DataMember(Name = "competitiondescription")]
        public string CompetitionDescription { get; set; }
        [DataMember(Name = "starttemporal")]
        public TemporalInformation StartTemporal { get; set; }
        [DataMember(Name = "stoptemporal")]
        public TemporalInformation StopTemporal { get; set; }
        [DataMember(Name = "participantcount")]
        public int ParticipantCount { get; set; }
        [DataMember(Name = "isparticipating")]
        public bool IsParticipating{ get; set; }
        [DataMember(Name = "rank")]
        public int Rank { get; set; }
        [DataMember(Name = "attemptcount")]
        public int AttemptCount { get; set; }

        public CompetitionView (Competition com) {
            this.CompetitionId = com.CompetitionId;
            this.CompetitionName = com.CompetitionName;
            this.CompetitionDescription = com.CompetitionDescription;
            this.StartTemporal = com.StartTemporal;
            this.StopTemporal = com.StopTemporal;
        }

    }
}
