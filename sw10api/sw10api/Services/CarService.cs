using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel.Web;
using System.IO;

using Newtonsoft.Json.Linq;
using Newtonsoft.Json;


using sw10api.Interfaces;
using CarDataProject;

namespace sw10api.Services {
    public partial class RestService : ICar {
        [WebInvoke(Method = "GET", UriTemplate = "GetCar?carid={carid}")]
        public string GetCar(Int16 carid) {
            try {
                throw new Exception("something broke");
            } catch (Exception e) {
                Console.WriteLine(e.ToString());
                DBController dbc = new DBController();
                dbc.AddLog("GetCar?carid={carid}", carid, null, null, e.ToString().Substring(0, Math.Min(e.ToString().Count(), 254)), "");
                dbc.Close();
            }
            return "";
        }

        [WebInvoke(Method = "GET", UriTemplate = "GetOrCreateCar?imei={imei}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json) ]
        public string GetOrCreateCar(Int64 imei) {
            try {
                DBController dbc = new DBController();
                Car car = dbc.GetCarByIMEI(imei);
                if (car == null) {
                    dbc.AddNewCar(imei);
                    Car newCar = dbc.GetCarByIMEI(imei);
                    return JsonConvert.SerializeObject(newCar);
                }

                dbc.Close();

                return JsonConvert.SerializeObject(car);
            } catch (Exception e) {
                Console.WriteLine(e.ToString());
                DBController dbc = new DBController();
                dbc.AddLog("GetOrCreateCar?imei={imei}", null, null, null, e.ToString().Substring(0, Math.Min(e.ToString().Count(), 254)), imei.ToString());
                dbc.Close();
            }
            return "";
        }

        [WebInvoke(Method = "GET", UriTemplate = "UpdateCarWithUsername?carid={carid}&username={username}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public void UpdateCarWithUsername(Int16 carid, string username) {
            try {
                DBController dbc = new DBController();
                dbc.UpdateCarWithUsername(carid, username);
                dbc.Close();
            } catch(Exception e) {
                Console.WriteLine(e.ToString());
                DBController dbc = new DBController();
                dbc.AddLog("UpdateCarWithUsername?carid={carid}&username={username}", carid, null, null, e.ToString().Substring(0, Math.Min(e.ToString().Count(), 254)), username);
                dbc.Close();
            }

            return;
        }
    }
}
