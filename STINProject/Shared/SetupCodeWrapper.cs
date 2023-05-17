using Google.Authenticator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STINProject.Shared
{
    public class SetupCodeWrapper
    {
        public string QRCode { get; set; }
        public string ManualCode { get; set; }
        public SetupCodeWrapper(string qRCode, string manualCode)
        {
            QRCode = qRCode;
            ManualCode = manualCode;
        }

        public SetupCodeWrapper()
        {
            QRCode = "";
            ManualCode = "";
        }
    }
}
