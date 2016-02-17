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

        public Test(String text) {
            this.GetOrSetText = text;
        }

        [DataMember]
        public String GetOrSetText {
            get {
                return _text;
            }
            set {
                _text = value;
            }
        }
        private String _text;
    }
}
