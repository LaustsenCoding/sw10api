using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

using CarDataProject;

using sw10api.Models;

namespace sw10api.Interfaces {
    [ServiceContract]
    public interface IFact {
        [OperationContract]
        Fact GetFact(Int64 tripid);
        [OperationContract]
        List<Fact> GetFacts(Int16 carid, Int64 tripid);
        [OperationContract]
        Test GetMyTest();
    }
}
