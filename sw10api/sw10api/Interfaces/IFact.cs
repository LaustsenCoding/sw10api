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
        Fact GetFact(int tripid);
        [OperationContract]
        Test GetMyTest();
    }
}
