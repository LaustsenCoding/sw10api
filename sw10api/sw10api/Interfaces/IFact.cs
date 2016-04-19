using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.IO;

using CarDataProject;

using sw10api.Models;

namespace sw10api.Interfaces {
    [ServiceContract]
    public interface IFact {
        [OperationContract]
        void GetFacts(Int16 carid, Int64 tripid);
        [OperationContract]
        void GetFactsForMap(Int16 carid, Int64 tripid);
        [OperationContract]
        string GetMyTest();
        //Test GetMyTest();

        [OperationContract]
        void AddFacts(Stream facts);
    }
}
