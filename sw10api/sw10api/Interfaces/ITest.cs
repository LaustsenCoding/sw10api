using System.ServiceModel;
using System.IO;

namespace sw10api.Interfaces {
    [ServiceContract]
    public interface ITest {
        [OperationContract]
        void AddTest(Stream facts);
        [OperationContract]
        string GetTest();
    }
}
