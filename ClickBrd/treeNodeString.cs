using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClickBrd
{
    class treeNodeString                        // Класс строки - ветви или листа дерева
    {
        public string text4view = "";                           // Текст отображаемый в дереве
        public List<string> text4copy = new List<string>();     // Текст который будет обработан по реакции (вставлен в буфер обмена, например)
        public List<string> option = new List<string>();        // Пустой текст в этом поле - ветвь которая НЕ реагирует на нажатие ЛКМ
        //public int type = ""
        public Color textColor = Color.Black;                   // Цвет по умолчанию чёрный

        public treeNodeString() { }              // Контруктор по-умолчанию

        public string text4copyAsString()
        {
            string strText4copy = "";
            foreach (string line in this.text4copy)
                strText4copy = strText4copy + line + "\n";
            return strText4copy;
        }
        
        
        public treeNodeString(string _text4view, string _text4copy, Color _textColor, string _option)
        {                                       // Конструктор со всеми параметрами для новых ветвей (фунциональные листья)
            this.text4view = _text4view;
            this.text4copy.Add(_text4copy);
            this.textColor = _textColor;
            this.option.Add(_option);
        }
        /*
        public treeNodeString(string _text4view, string _text4copy, Color _textColor)
        {                                       // Конструктор со всеми параметрами для новых ветвей (НЕфункциональные заголовки) 
            this.text4view = _text4view;
            this.text4copy.Add(_text4copy);
            this.textColor = _textColor;
        }
        public treeNodeString(string _text4view, string _text4copy)
        {                                       // Конструктор со всеми параметрами для новых ветвей (аналогично предыдущему но без цвета) 
            this.text4view = _text4view;
            this.text4copy.Add(_text4copy);
        }
        public treeNodeString(string _text4view)
        {                                       // Конструктор для которого характерно единство отображаемого на экране и обрабатываемого текста
            this.text4view = _text4view;
            this.text4copy.Add(_text4view);
        }
        
        */
    }
}
