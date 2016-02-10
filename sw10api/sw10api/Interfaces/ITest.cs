using System.ServiceModel;

namespace sw10api.Interfaces {
    [ServiceContract]
    public interface ITest {
        [OperationContract]
        void AddTest();
        [OperationContract]
        string GetTest();
    }
}
