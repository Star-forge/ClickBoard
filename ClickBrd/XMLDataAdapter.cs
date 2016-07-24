using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

namespace ClickBrd
{
    //Класс описывающий запись данных вкладки в xml файл и чтение в обратном порядке.
    class XMLDataAdapter
    {
        //********************************************************************************
        /////*********************  Конструктор по-умолчанию  ****************************
        //********************************************************************************
        public XMLDataAdapter(){}

        //********************************************************************************
        /////*********** Преобразование цвета в понятный программе вид *******************
        //********************************************************************************
        public String получить_текущую_дату_и_время_в_виде_строки()
        {
            return DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + "_" + DateTime.Now.Millisecond.ToString();
        }

        //********************************************************************************
        /////*********** Преобразование цвета в понятный программе вид *******************
        //********************************************************************************
        public Color getColor(string sColor)
        {
            // По-умолчанию черный цвет
            Color TextColor = Color.Black;

            // Если это именованный цвет, то мы его воспринимаем как есть и 
            // Цвет может быть представлен в виде 0xRRGGBB
            // (может остаться в файле с предыдущих версий программы. 
            // В данный момент эта нотация цвета не используется)
            TextColor = Color.FromName(sColor);
            
            // Поскольку языком при записи неименованных цветов записывается ARGB в имя (для удобочитаемости), 
            // но при этом само поле ARGB обнуляется, и в таком случае имя обратно уже не конвертировать, 
            // то имеет место проверка на 0 и если истина, то цвет восстанавливается из имени
            if (TextColor.ToArgb().Equals(0))
            {
                int R = 0, G = 0, B = 0;
                R = Convert.ToInt32(sColor.Substring(2, 2), 16);
                G = Convert.ToInt32(sColor.Substring(4, 2), 16);
                B = Convert.ToInt32(sColor.Substring(6, 2), 16);
                TextColor = Color.FromArgb(R, G, B);
            }

            return TextColor;
        }

        //********************************************************************************
        /////******************  Чтение данных из файла вкладки  *************************
        //********************************************************************************
        public BrdData ReadXMLTreeData(string XMLTreeDataFile)
        {
            BrdData td = new BrdData();

            // Именем вкладки всегда будет имя файла с данными. Это сделано для наглядности.
            td.BrdName = XMLTreeDataFile;   
            try
            {
                // Чтение документа
                XDocument doc = XDocument.Load(XMLTreeDataFile); 
                // и сохранение прочитанного как копию с расширением bak.
                doc.Save(td.BrdName + получить_текущую_дату_и_время_в_виде_строки() + ".bak");

                // разбор конфигурации дерева - настройки вкладки и окон
                Boolean.TryParse(doc.Element("tree").Attribute("TopMost").Value, out td.topMost);
                Boolean.TryParse(doc.Element("tree").Attribute("Sound").Value, out td.sound);
                td.SoundFile = doc.Element("tree").Attribute("SoundFile").Value;
                Boolean.TryParse(doc.Element("tree").Attribute("DoReaction").Value, out td.doReaction);
                td.treeBackColor = getColor(doc.Element("tree").Attribute("TreeBackColor").Value);
                int.TryParse(doc.Element("tree").Attribute("FormWidth").Value, out td.formWidth);
                int.TryParse(doc.Element("tree").Attribute("FormHeight").Value, out td.formHeight);
                int.TryParse(doc.Element("tree").Attribute("FormX").Value, out td.formX);
                int.TryParse(doc.Element("tree").Attribute("FormY").Value, out td.formY);

                // разбор строк дерева. Строки упаковываются в структуру для последующей обработки.
                var strings = from u in doc.Descendants("string")
                    select new
                    {
                        Text4view = (string)u.Attribute("Text4view").Value,
                        Text4copy = (string)u.Attribute("Text4copy").Value,
                        TextColor = (string)u.Attribute("TextColor").Value,
                        //NodeType = (string)u.Attribute("Type").Value,
                        Option = (string)u.Attribute("Option").Value
                    };
                
                // Обработка структуры строк
                foreach (var s in strings)
                {
                    td.addNode(new treeNodeString(
                        s.Text4view.ToString(), 
                        s.Text4copy.ToString(), 
                        getColor(s.TextColor), 
                        s.Option.ToString()));
                }
            }
            catch (Exception ex) 
            {
                /* LogNDebug.SendEmailReport(ex.ToString() + "\n\n\nПожалуйста, добавьте свой текст комментария: опишите действия приведшие к ошибке и отправьте письмо."); */ LogNDebug.SendEmailReport(ex.ToString()); 
                //MessageBox.Show(ex.ToString(), XMLTreeDataFile + "- Ошибка");
            }

            return td;
        }

        //********************************************************************************
        /////****************** Запись данных из вкладки в файл **************************
        //********************************************************************************
        public void WriteXMLTreeData(BrdData td)
        {
            try
            {
                XDocument doc = new XDocument();
                // Формируем корневой элемент и записываем его отрибуты
	            XElement tree = new XElement("tree", 
                    new XAttribute("TopMost", td.topMost),
                    new XAttribute("Sound", td.sound),
                    new XAttribute("SoundFile", td.SoundFile),
                    new XAttribute("DoReaction", td.doReaction),
                    new XAttribute("TreeBackColor", td.treeBackColor.Name),
                    new XAttribute("FormWidth", td.formWidth),
                    new XAttribute("FormHeight", td.formHeight),
                    new XAttribute("FormX", td.formX),
                    new XAttribute("FormY", td.formY)
                    );

                for (int i = 0; i < td.tree.Count; i++) // Перебираем все узлы-ветви
                {
                    tree.Add(new XElement("string",
                        new XAttribute("Text4view", td.tree[i][0].text4view),

                        new XAttribute("Text4copy", td.tree[i][0].text4copy[0]),
                        new XAttribute("TextColor", td.tree[i][0].textColor.Name),
                        // Если пустые опции  
                        new XAttribute("Option", (td.tree[i][0].option.Count.Equals(0) ? "" : td.tree[i][0].option[0]))));
                    for (int j = 1; j < td.tree[i].Count; j++)  // Далее в цикле добавление узлов-листьев
                    {
                        tree.Add(new XElement("string",
                            new XAttribute("Text4view", td.tree[i][j].text4view),
                            new XAttribute("Text4copy", td.tree[i][j].text4copy[0]),
                            new XAttribute("TextColor", td.tree[i][j].textColor.Name),
                            new XAttribute("Option", (td.tree[i][j].option.Count.Equals(0) ? "" : td.tree[i][j].option[0]))));
                    }
                }
                // Формируем документ и записываем его в виде файла.
                doc.Add(tree);
                doc.Save(td.BrdName);
            }
            catch (Exception ex)
            {
                /* LogNDebug.SendEmailReport(ex.ToString() + "\n\n\nПожалуйста, добавьте свой текст комментария: опишите действия приведшие к ошибке и отправьте письмо."); */ LogNDebug.SendEmailReport(ex.ToString()); 
            }
            
        }


    }

    
}
