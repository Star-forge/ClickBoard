using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClickBrd
{
    class Data
    {
        // Данные с Основного конфига
        public List<Profile> Profiles = new List<Profile>();     // Список профилей
        public String Version = "";                             // Версия программы
        public String WindowCaption = "";                       // Название программы

        // Данные вкладок
        public List<BrdData> Brds = new List<BrdData>(); // Список всех доступных вкладок 
    }
}
