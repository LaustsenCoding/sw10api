using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ServiceModel;

using CarDataProject;

namespace sw10api.Interfaces {
    [ServiceContract]
    public interface ITrip {
        [OperationContract]
        string GetTrip(Int16 carid, Int64 tripid);
        [OperationContract]
        string GetTrips(Int16 carid);
        [OperationContract]
        string GetTripsForList(Int16 carid, int offset);
    }
}
