using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel.Web;
using System.IO;
using CarDataProject;
using sw10api.Interfaces;

namespace sw10api.Services {
    public partial class RestService : ITest  {

        [WebInvoke(Method = "POST", UriTemplate = "AddTest")]
        public void AddTest(Stream facts) {
            try {
                Console.WriteLine("Message Received");
                StreamReader sr = new StreamReader(facts);
                string text = sr.ReadToEnd();
                Console.WriteLine("It was: " + text);
            } catch (Exception e) {
                Console.WriteLine(e.ToString());
                DBController dbc = new DBController();
                dbc.AddLog("AddTest", null, null, null, e.ToString().Substring(0, 254), e.ToString().Substring(0, Math.Min(e.ToString().Count(), 499)));
                dbc.Close();
                return;
            }
            return;
        }

        [WebInvoke(Method = "GET", UriTemplate = "GetTest", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public String GetTest() {
            return "Jamen Andreas, Der er hul igennem";
        }
    }
}
