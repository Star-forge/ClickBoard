using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ClickBrd
{
    public class Configuration
    {
        string _trees;

        public Configuration()
        {
            _trees = System.Configuration.ConfigurationManager.AppSettings["Trees"];
        }

        public string getTrees()
        {
            return _trees;
        }
    }
}
