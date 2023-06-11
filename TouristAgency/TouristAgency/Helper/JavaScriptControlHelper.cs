using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;

namespace TouristAgency.Helper
{
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    [ComVisible(true)]
    public class JavaScriptControlHelper
    {
        UserWindow prozor;
        public JavaScriptControlHelper(UserWindow w)
        {
            prozor = w;
        }

        public void RunFromJavascript(string param)
        {
            prozor.doThings(param);
        }
    }
}
