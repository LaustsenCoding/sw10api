using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel.Web;

using sw10api.Interfaces;

namespace sw10api.Services {
    public partial class RestService : ITest  {

        [WebInvoke(Method = "POST", UriTemplate = "AddTest", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public void AddTest() {
            
        }

        [WebInvoke(Method = "GET", UriTemplate = "GetTest", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public String GetTest() {
            return "Jamen Andreas, Der er hul igennem";
        }

    }
}
