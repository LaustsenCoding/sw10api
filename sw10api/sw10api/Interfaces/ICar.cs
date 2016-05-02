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
    public interface ICar {
        [OperationContract]
        string GetCar(Int16 carid);
        [OperationContract]
        string GetOrCreateCar(long imei);
        [OperationContract]
        void UpdateCarWithUsername(Int16 carid, string username);
    }
}
