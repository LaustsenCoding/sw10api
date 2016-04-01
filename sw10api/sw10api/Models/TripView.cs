using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Runtime.Serialization;

using CarDataProject;

namespace sw10api.Models {
    [DataContract]
    public class TripView : Trip {

        [DataMember(Name = "roadtypemajority")]
        public int RoadtypeMajority;
        [DataMember(Name = "timeperiodmajority")]
        public int TimePeriodMajority;

        [DataMember]
        public double SpeedingScore;
        [DataMember]
        public double AccelerationScore;
        [DataMember]
        public double BrakingScore;
        [DataMember]
        public double JerkingScore;
        [DataMember]
        public double RoadtypeScore;
        [DataMember]
        public double TimePeriodScore;

        public TripView(DataRow row) : base(row) {

        }


    }
}
