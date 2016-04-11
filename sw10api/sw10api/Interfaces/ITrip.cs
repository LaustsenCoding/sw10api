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
        Trip GetTrip(Int16 carid, Int64 tripid);
        [OperationContract]
        List<Trip> GetTrips(Int16 carid);
        [OperationContract]
        List<Trip> GetTripsForList(Int16 carid, int offset);
    }
}
