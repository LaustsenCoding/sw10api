using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Runtime.Serialization;

namespace sw10api.Models {
    [DataContract]
    public class Test {

        public Test() { }

        public Test(int id) {
            this.GetOrSetId = id;
        }

        [DataMember(Name = "id")]
        public int GetOrSetId {
            get {
                return _accountId;
            }
            set {
                _accountId = value;
            }
        }
        private int _accountId;
    }
}
