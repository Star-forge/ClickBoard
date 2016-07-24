namespace ClickBrd
{
    partial class DataModForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.text4viewField = new System.Windows.Forms.TextBox();
            this.text4viewLabel = new System.Windows.Forms.Label();
            this.colorButton = new System.Windows.Forms.Button();
            this.text4copyLabel = new System.Windows.Forms.Label();
            this.text4copyField = new System.Windows.Forms.TextBox();
            this.optionsField = new System.Windows.Forms.TextBox();
            this.optionsLabel = new System.Windows.Forms.Label();
            this.doButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // text4viewField
            // 
            this.text4viewField.Location = new System.Drawing.Point(15, 28);
            this.text4viewField.Name = "text4viewField";
            this.text4viewField.Size = new System.Drawing.Size(257, 20);
            this.text4viewField.TabIndex = 0;
            this.text4viewField.TextChanged += new System.EventHandler(this.text4viewField_TextChanged);
            // 
            // text4viewLabel
            // 
            this.text4viewLabel.AutoSize = true;
            this.text4viewLabel.Location = new System.Drawing.Point(12, 12);
            this.text4viewLabel.Name = "text4viewLabel";
            this.text4viewLabel.Size = new System.Drawing.Size(91, 13);
            this.text4viewLabel.TabIndex = 1;
            this.text4viewLabel.Text = "Видимый текст: ";
            // 
            // colorButton
            // 
            this.colorButton.Location = new System.Drawing.Point(15, 212);
            this.colorButton.Name = "colorButton";
            this.colorButton.Size = new System.Drawing.Size(257, 23);
            this.colorButton.TabIndex = 2;
            this.colorButton.Text = "Цвет";
            this.colorButton.UseVisualStyleBackColor = true;
            this.colorButton.Click += new System.EventHandler(this.colorButton_Click);
            // 
            // text4copyLabel
            // 
            this.text4copyLabel.AutoSize = true;
            this.text4copyLabel.Location = new System.Drawing.Point(12, 51);
            this.text4copyLabel.Name = "text4copyLabel";
            this.text4copyLabel.Size = new System.Drawing.Size(108, 13);
            this.text4copyLabel.TabIndex = 3;
            this.text4copyLabel.Text = "Текст для вставки: ";
            // 
            // text4copyField
            // 
            this.text4copyField.Location = new System.Drawing.Point(15, 67);
            this.text4copyField.Multiline = true;
            this.text4copyField.Name = "text4copyField";
            this.text4copyField.Size = new System.Drawing.Size(257, 60);
            this.text4copyField.TabIndex = 4;
            // 
            // optionsField
            // 
            this.optionsField.Location = new System.Drawing.Point(15, 146);
            this.optionsField.Multiline = true;
            this.optionsField.Name = "optionsField";
            this.optionsField.Size = new System.Drawing.Size(257, 60);
            this.optionsField.TabIndex = 6;
            // 
            // optionsLabel
            // 
            this.optionsLabel.AutoSize = true;
            this.optionsLabel.Location = new System.Drawing.Point(12, 130);
            this.optionsLabel.Name = "optionsLabel";
            this.optionsLabel.Size = new System.Drawing.Size(45, 13);
            this.optionsLabel.TabIndex = 5;
            this.optionsLabel.Text = "Опции: ";
            // 
            // doButton
            // 
            this.doButton.Location = new System.Drawing.Point(197, 241);
            this.doButton.Name = "doButton";
            this.doButton.Size = new System.Drawing.Size(75, 23);
            this.doButton.TabIndex = 7;
            this.doButton.Text = "Завершить";
            this.doButton.UseVisualStyleBackColor = true;
            this.doButton.Click += new System.EventHandler(this.doButton_Click);
            // 
            // DataModForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 271);
            this.Controls.Add(this.doButton);
            this.Controls.Add(this.optionsField);
            this.Controls.Add(this.optionsLabel);
            this.Controls.Add(this.text4copyField);
            this.Controls.Add(this.text4copyLabel);
            this.Controls.Add(this.colorButton);
            this.Controls.Add(this.text4viewLabel);
            this.Controls.Add(this.text4viewField);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "DataModForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox text4viewField;
        private System.Windows.Forms.Label text4viewLabel;
        private System.Windows.Forms.Button colorButton;
        private System.Windows.Forms.Label text4copyLabel;
        private System.Windows.Forms.TextBox text4copyField;
        private System.Windows.Forms.TextBox optionsField;
        private System.Windows.Forms.Label optionsLabel;
        private System.Windows.Forms.Button doButton;
    }
}