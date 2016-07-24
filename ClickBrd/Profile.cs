using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClickBrd
{
    class Profile
    {
        public string ProfileName;
        public List<String> ClkBrdNames;
        // Флаги синхронизации размера и/или месторасположения окна при переключении вкладок
        // true - значит, что если переключается вкладка то окно остается на месте и/или с теми же размерами
        // false - окно становится в определенное место и/или изменяет размер, характерный для каждой вкладки 
        public bool isTabsSizeSync = true;
        public bool isTabsLocatonSync = true;  

        public Profile()
        {
            this.ProfileName = "";
            this.ClkBrdNames = new List<String>();
        }

        public Profile(string profile)
        {
            this.ProfileName = profile;
            this.ClkBrdNames = new List<String>();
        }

        public Profile(string profile, List<String> ClkBrds)
        {
            this.ProfileName = profile;
            this.ClkBrdNames = ClkBrds;
        }
    }
}
