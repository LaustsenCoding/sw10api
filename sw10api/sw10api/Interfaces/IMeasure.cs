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
    public interface IMeasure {
        [OperationContract]
        string AddAndUpdateFacts(Stream facts);
    }
}
