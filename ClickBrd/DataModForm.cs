using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ClickBrd
{
    public partial class DataModForm : Form
    {
        bool bParent = false;
        public DataModForm()
        {
            InitializeComponent();
        }

        public DataModForm(string title, bool bParent)
        {
            this.Text = title;
            this.bParent = bParent;
            InitializeComponent();

            optionsField.ReadOnly = bParent;
        }

        public DataModForm(TreeNode tn, bool bParent)
        {
            this.Text = tn.Text;
            this.bParent = bParent;
            InitializeComponent();
            //text4copyField.ReadOnly = bParent;
            optionsField.ReadOnly = bParent;
        }


        public void setText4view(string text4view)
        {
            text4viewField.Text = text4view;
        }

        public void setText4copy(List<string> text4copy)
        {
            foreach (string text in text4copy)
                text4copyField.Text = text4copyField.Text + text + "\n";
        }

        public void setOptions(List<string> options)
        {
            foreach (string text in options)
                optionsField.Text = optionsField.Text + text + "\n";
        }

        public void setColor(Color textColor)
        {
            colorButton.ForeColor = textColor;
            //colorButton.BackColor = getContrastColor(textColor);
            //colorButton.BackColor = 
        }

        private void colorButton_Click(object sender, EventArgs e)
        {
            ColorDialog clrdlg = new ColorDialog();
            if (clrdlg.ShowDialog() == DialogResult.OK)
            {
                setColor(clrdlg.Color);
            }
        }

        private Color getContrastColor(Color _color) // получает контрастный цвет
        {
            int R = _color.R;
            int G = _color.G;
            int B = _color.B;

            if (R < 128)
                R = 255;
            else
                R = 0;
            if (G < 128)
                G = 255;
            else
                G = 0;
            if (B < 128)
                B = 255;
            else
                B = 0;

            return Color.FromArgb(_color.A, R, G, B);
        }

        private void doButton_Click(object sender, EventArgs e)
        {
            Form1 main = this.Owner as Form1;
            if (main != null)
            {
                treeNodeString tns = new treeNodeString();
                tns.text4view = text4viewField.Text;
                foreach (string line in text4copyField.Lines)
                    if (!line.Equals(""))
                        tns.text4copy.Add(line);
                foreach (string line in optionsField.Lines)
                    if (!line.Equals(""))
                        tns.option.Add(line);
                tns.textColor = colorButton.ForeColor;
                main.hitTNS = tns;

            }
            this.Close();
        }

        private void text4viewField_TextChanged(object sender, EventArgs e)
        {
            //if (bParent)
            //    text4copyField.Text = text4viewField.Text;
        }

    }
}
