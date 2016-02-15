using System.ServiceModel;
using sw10api.Interfaces;
using CarDataProject;

namespace sw10api.Services {
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, AddressFilterMode = AddressFilterMode.Any)]
    public partial class RestService : ITest, IFact, ITrip {

    }
}
