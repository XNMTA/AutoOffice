using autoOffice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace autoOffice.Common
{
    public static class CommonUser
    {
        public static string pureName(string longName)
        {
            int index = longName.IndexOf("\\");
            return longName.Substring(index+1);
        }
        
    }
}