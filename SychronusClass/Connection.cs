using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace SychronusClass
{
    public class Connection
    {
        public string connectionClass()
        {
            return ConfigurationManager.ConnectionStrings["SynchronusSoftware"].ConnectionString;
        }
    }
}
