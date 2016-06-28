using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace VeraitoServices
{
     [DataContract]
    public class ErrorMessages
    {
       
      
            public ErrorMessages(string errorInfo, string errorDetails)
            {
                ErrorInfo = errorInfo;
                ErrorDetails = errorDetails;
            }

            [DataMember]
            public string ErrorInfo { get; private set; }

            [DataMember]
            public string ErrorDetails { get; private set; }
        }
    }
