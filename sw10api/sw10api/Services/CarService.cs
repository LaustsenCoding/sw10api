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

            return "";
        }

        [WebInvoke(Method = "GET", UriTemplate = "GetOrCreateCar?imei={imei}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json) ]
        public string GetOrCreateCar(Int64 imei) {

            //return JsonConvert.SerializeObject(facts);
            // IF EXIST GET
            // OR CREATE
            DBController dbc = new DBController();
            Car car = null;
            try {
                car = dbc.GetCarByIMEI(imei);
            } catch (Exception e) {
            }
            if (car == null) {
                dbc.AddNewCar(imei);
                Car newCar = dbc.GetCarByIMEI(imei);
                return JsonConvert.SerializeObject(newCar);
            } 

            dbc.Close();

            return JsonConvert.SerializeObject(car);
        }

        [WebInvoke(Method = "POST", UriTemplate = "UpdateCarWithUsername?imei={imei}&username={username}")]
        public void UpdateCarWithUsername(Int64 imei, string username) {
            
            DBController dbc = new DBController();
            dbc.UpdateCarWithUsername(imei, username);
            dbc.Close();

            return;
        }
    }
}
