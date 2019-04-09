using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace LoginWeb.Model
{
    [DataContract]
    public class ValidateUser
    {
        [DataMember]
        public string NetworkName { get; set; }
        [DataMember]
        public int? NextPasswordChange { get; set; }
        [DataMember]
        public int? Result { get; set; }
        [DataMember]
        public string SessionID { get; set; }
        [DataMember]
        public int? UserID { get; set; }
        [DataMember]
        public string UserName { get; set; }
    }
    public class RootObject
    {
        public ValidateUser ValidateUser { get; set; }
    }

}
