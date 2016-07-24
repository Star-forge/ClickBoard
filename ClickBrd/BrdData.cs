using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClickBrd
{
    class BrdData
    {
        public List<List<treeNodeString>> tree = new List<List<treeNodeString>>();  // дерево
        public Boolean сloseToTray = false;                 // При закрытии прятать в трей
        public Boolean topMost = false;                     // Поверх всех окон
        public Boolean sound = false;                       // Вкл звук при реакции
        public String SoundFile = "";                       // Файл звука при реакции
        public Boolean doReaction = false;                  // Делать специальную реакцю
        public Color treeBackColor = Color.LightGray;       // Цвет по умолчанию чёрный
        public string BrdName = "";
        public int formWidth = 600;
        public int formHeight = 600;
        public int formX = 100;                             // Координата расположения формы X
        public int formY = 100;                             // Координата расположения формы Y             

        public BrdData() { }

        public Boolean addNode(treeNodeString tns)
        {
            try
            {
                if (tns.option[0].Equals("-") || tns.option[0].Equals("+")) //Если включены опции свернуть ветвь (+) или развернуть ветвь (-) значит это ветвь, а не лись.
                {
                    tree.Add(new List<treeNodeString> { tns });
                }
                else
                { // иначе это лист
                    tree[tree.Count - 1].Add(tns);
                }
                return true;
            }
            catch (Exception e)
            {
                LogNDebug.SendEmailReport(e.ToString());
                return false;
            }
        }

        public Boolean addNode(int pos, treeNodeString tns)
        {
            try
            {
                tree[pos].Add(tns);
                return true;
            }
            catch (Exception e)
            {
                LogNDebug.SendEmailReport(e.ToString());
                return false;
            }
        }

        public Boolean delNodes(int pos)
        {
            try
            {
                tree.RemoveAt(pos);
                return true;
            }
            catch (Exception e)
            {
                LogNDebug.SendEmailReport(e.ToString());
                return false;
            }
        }

        public Boolean delNode(int ppos, int pos)
        {
            try
            {
                tree[ppos].RemoveAt(pos+1);
                return true;
            }
            catch (Exception e)
            {
                LogNDebug.SendEmailReport(e.ToString());
                return false;
            }
        }

        public List<string> getChildText4copy(string parent, string text4view)
        {
            for (int i = 0; i < this.tree.Count; i++)
            {
                if (this.tree[i][0].text4view.Equals(parent))
                    for (int j = 1; j < this.tree[i].Count; j++)
                    {
                        if (this.tree[i][j].text4view.Equals(text4view))
                            return this.tree[i][j].text4copy;
                    }
            }
            return null;
        }

        public List<string> getParentText4copy(string parent)
        {
            for (int i = 0; i < this.tree.Count; i++)
            {
                if (this.tree[i][0].text4view.Equals(parent))
                    return this.tree[i][0].text4copy;
            }
            return null;
        }

        public List<string> getChildOption(string parent, string text4view)
        {
            for (int i = 0; i < this.tree.Count; i++)
            {
                if (this.tree[i][0].text4view.Equals(parent))
                    for (int j = 1; j < this.tree[i].Count; j++)
                    {
                        if (this.tree[i][j].text4view.Equals(text4view))
                            return this.tree[i][j].option;
                    }
            }
            return null;
        }

        public List<string> getParentOption(string parent)
        {
            for (int i = 0; i < this.tree.Count; i++)
            {
                if (this.tree[i][0].text4view.Equals(parent))
                    return this.tree[i][0].option;
            }
            return null;
        }

        public treeNodeString getTreeNodeString(string parent, string text4view)
        {
            for (int i = 0; i < this.tree.Count; i++)
            {
                if (this.tree[i][0].text4view.Equals(parent))
                    for (int j = 1; j < this.tree[i].Count; j++)
                    {
                        if (this.tree[i][j].text4view.Equals(text4view))
                            return this.tree[i][j];
                    }
            }
            return null;
        }

        public bool setChildTreeNodeString(string parent, string text4view, treeNodeString tns)
        {
            for (int i = 0; i < this.tree.Count; i++)
            {
                if (this.tree[i][0].text4view.Equals(parent))
                    for (int j = 1; j < this.tree[i].Count; j++)
                    {
                        if (this.tree[i][j].text4view.Equals(text4view))
                        {
                            this.tree[i][j] = tns;
                            return true;
                        }
                    }
            }
            return false;
        }

        public bool setParentTreeNodeString(string parent, treeNodeString tns)
        {
            for (int i = 0; i < this.tree.Count; i++)
            {
                if (this.tree[i][0].text4view.Equals(parent))
                {
                    this.tree[i][0] = tns;
                    return true;
                }
            }
            return false;
        }
    }
}
