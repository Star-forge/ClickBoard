namespace ClickBrd
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.TrayIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.statusPane = new System.Windows.Forms.StatusStrip();
            this.toolStripSplitButton1 = new System.Windows.Forms.ToolStripSplitButton();
            this.ProfileList = new System.Windows.Forms.ToolStripDropDownButton();
            this.новыйПрофильToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.StatusLabelOnTop = new System.Windows.Forms.ToolStripSplitButton();
            this.StatusLabelSound = new System.Windows.Forms.ToolStripSplitButton();
            this.StatusLabelAuto = new System.Windows.Forms.ToolStripSplitButton();
            this.StatusLabelTabsSizeSync = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusLabelTabsLocatonSync = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusText = new System.Windows.Forms.ToolStripStatusLabel();
            this.parentMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.parentMenu_changeColor = new System.Windows.Forms.ToolStripMenuItem();
            this.parentMenu_edit = new System.Windows.Forms.ToolStripMenuItem();
            this.parentMenu_edit_add = new System.Windows.Forms.ToolStripMenuItem();
            this.parentMenu_edit_edt = new System.Windows.Forms.ToolStripMenuItem();
            this.parentMenu_edit_del = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.parentMenu_changeColorAll = new System.Windows.Forms.ToolStripMenuItem();
            this.parentMenu_AllCollapse = new System.Windows.Forms.ToolStripMenuItem();
            this.childMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.сменитьЦветToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.правкаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.изменитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.удалитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.treeMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addNewNode_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.TopMost_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Sound_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Auto_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SyncTabSize_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SyncTabPos_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToggleAll_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusPane.SuspendLayout();
            this.parentMenu.SuspendLayout();
            this.childMenu.SuspendLayout();
            this.treeMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(748, 549);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tabControl1_Selecting);
            this.tabControl1.Click += new System.EventHandler(this.tabControl1_Click);
            // 
            // TrayIcon
            // 
            this.TrayIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("TrayIcon.Icon")));
            this.TrayIcon.Text = "Двойной клик - раскрыть/скрыть окно.";
            this.TrayIcon.Visible = true;
            this.TrayIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.TrayIcon_MouseDoubleClick);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "t.ico");
            this.imageList1.Images.SetKeyName(1, "s.ico");
            this.imageList1.Images.SetKeyName(2, "a.ico");
            this.imageList1.Images.SetKeyName(3, "ts.ico");
            this.imageList1.Images.SetKeyName(4, "sa.ico");
            this.imageList1.Images.SetKeyName(5, "ta.ico");
            this.imageList1.Images.SetKeyName(6, "tsa.ico");
            // 
            // statusPane
            // 
            this.statusPane.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSplitButton1,
            this.ProfileList,
            this.StatusLabelOnTop,
            this.StatusLabelSound,
            this.StatusLabelAuto,
            this.StatusLabelTabsSizeSync,
            this.StatusLabelTabsLocatonSync,
            this.StatusText});
            this.statusPane.Location = new System.Drawing.Point(0, 527);
            this.statusPane.Name = "statusPane";
            this.statusPane.Size = new System.Drawing.Size(748, 22);
            this.statusPane.TabIndex = 1;
            this.statusPane.Text = "StatusPane";
            // 
            // toolStripSplitButton1
            // 
            this.toolStripSplitButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripSplitButton1.DropDownButtonWidth = 0;
            this.toolStripSplitButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripSplitButton1.Image")));
            this.toolStripSplitButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitButton1.Name = "toolStripSplitButton1";
            this.toolStripSplitButton1.Size = new System.Drawing.Size(21, 20);
            this.toolStripSplitButton1.Text = "toolStripSplitButton1";
            this.toolStripSplitButton1.ToolTipText = "Сохранение профиля";
            this.toolStripSplitButton1.Click += new System.EventHandler(this.saveProfileButton_click);
            // 
            // ProfileList
            // 
            this.ProfileList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.ProfileList.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.новыйПрофильToolStripMenuItem});
            this.ProfileList.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.ProfileList.Image = global::ClickBrd.Properties.Resources.profile;
            this.ProfileList.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ProfileList.Margin = new System.Windows.Forms.Padding(0);
            this.ProfileList.Name = "ProfileList";
            this.ProfileList.Size = new System.Drawing.Size(88, 22);
            this.ProfileList.Text = "Профиль";
            this.ProfileList.ToolTipText = "Выбрать свой профиль";
            this.ProfileList.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ProfileList_DropDownItemClicked);
            // 
            // новыйПрофильToolStripMenuItem
            // 
            this.новыйПрофильToolStripMenuItem.Name = "новыйПрофильToolStripMenuItem";
            this.новыйПрофильToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.новыйПрофильToolStripMenuItem.Text = "Новый профиль...";
            this.новыйПрофильToolStripMenuItem.Click += new System.EventHandler(this.newProfileMenuItem_Click);
            // 
            // StatusLabelOnTop
            // 
            this.StatusLabelOnTop.AutoToolTip = false;
            this.StatusLabelOnTop.DropDownButtonWidth = 0;
            this.StatusLabelOnTop.Image = global::ClickBrd.Properties.Resources.ontop_off;
            this.StatusLabelOnTop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.StatusLabelOnTop.Margin = new System.Windows.Forms.Padding(0);
            this.StatusLabelOnTop.Name = "StatusLabelOnTop";
            this.StatusLabelOnTop.Size = new System.Drawing.Size(124, 22);
            this.StatusLabelOnTop.Tag = "";
            this.StatusLabelOnTop.Text = "Поверх всех окон";
            this.StatusLabelOnTop.ToolTipText = "Включение режима \"Поверх всех окон\"";
            this.StatusLabelOnTop.Click += new System.EventHandler(this.StatusLabelOnTop_Click);
            // 
            // StatusLabelSound
            // 
            this.StatusLabelSound.DropDownButtonWidth = 0;
            this.StatusLabelSound.Image = global::ClickBrd.Properties.Resources.sound_off;
            this.StatusLabelSound.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.StatusLabelSound.Margin = new System.Windows.Forms.Padding(0);
            this.StatusLabelSound.Name = "StatusLabelSound";
            this.StatusLabelSound.Size = new System.Drawing.Size(53, 22);
            this.StatusLabelSound.Text = "Звук";
            this.StatusLabelSound.ToolTipText = "Включение звука";
            this.StatusLabelSound.Click += new System.EventHandler(this.StatusLabelSound_Click);
            // 
            // StatusLabelAuto
            // 
            this.StatusLabelAuto.DropDownButtonWidth = 0;
            this.StatusLabelAuto.Image = global::ClickBrd.Properties.Resources.auto_off;
            this.StatusLabelAuto.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.StatusLabelAuto.Margin = new System.Windows.Forms.Padding(0);
            this.StatusLabelAuto.Name = "StatusLabelAuto";
            this.StatusLabelAuto.Size = new System.Drawing.Size(112, 22);
            this.StatusLabelAuto.Text = "Автоматизация";
            this.StatusLabelAuto.ToolTipText = "Включение автоматизации (Автовставки)";
            this.StatusLabelAuto.Click += new System.EventHandler(this.StatusLabelAuto_Click);
            // 
            // StatusLabelTabsSizeSync
            // 
            this.StatusLabelTabsSizeSync.AutoToolTip = true;
            this.StatusLabelTabsSizeSync.Name = "StatusLabelTabsSizeSync";
            this.StatusLabelTabsSizeSync.Size = new System.Drawing.Size(53, 17);
            this.StatusLabelTabsSizeSync.Text = "РАЗМЕР";
            this.StatusLabelTabsSizeSync.ToolTipText = "Выкл. индивидуальные настройки размера окна при переключении вкладок";
            this.StatusLabelTabsSizeSync.Click += new System.EventHandler(this.StatusLabelSizeSync_Click);
            // 
            // StatusLabelTabsLocatonSync
            // 
            this.StatusLabelTabsLocatonSync.AutoToolTip = true;
            this.StatusLabelTabsLocatonSync.Name = "StatusLabelTabsLocatonSync";
            this.StatusLabelTabsLocatonSync.Size = new System.Drawing.Size(66, 17);
            this.StatusLabelTabsLocatonSync.Text = "ПОЗИЦИЯ";
            this.StatusLabelTabsLocatonSync.ToolTipText = "Выкл. индивидуальные настройки расположения окна при переключении вкладок";
            this.StatusLabelTabsLocatonSync.Click += new System.EventHandler(this.StatusLabelTabsLocatonSync_Click);
            // 
            // StatusText
            // 
            this.StatusText.BackColor = System.Drawing.Color.Transparent;
            this.StatusText.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.StatusText.Name = "StatusText";
            this.StatusText.Size = new System.Drawing.Size(89, 17);
            this.StatusText.Text = "Строка статуса";
            this.StatusText.Visible = false;
            // 
            // parentMenu
            // 
            this.parentMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.parentMenu_changeColor,
            this.parentMenu_edit,
            this.toolStripSeparator1,
            this.parentMenu_changeColorAll,
            this.parentMenu_AllCollapse});
            this.parentMenu.Name = "contextMenuStrip1";
            this.parentMenu.ShowImageMargin = false;
            this.parentMenu.Size = new System.Drawing.Size(257, 98);
            this.parentMenu.Text = "Меню ветви";
            // 
            // parentMenu_changeColor
            // 
            this.parentMenu_changeColor.Name = "parentMenu_changeColor";
            this.parentMenu_changeColor.Size = new System.Drawing.Size(256, 22);
            this.parentMenu_changeColor.Text = "Сменить цвет...";
            this.parentMenu_changeColor.Click += new System.EventHandler(this.changeColor_Click);
            // 
            // parentMenu_edit
            // 
            this.parentMenu_edit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.parentMenu_edit_add,
            this.parentMenu_edit_edt,
            this.parentMenu_edit_del});
            this.parentMenu_edit.Name = "parentMenu_edit";
            this.parentMenu_edit.Size = new System.Drawing.Size(256, 22);
            this.parentMenu_edit.Text = "Правка ";
            // 
            // parentMenu_edit_add
            // 
            this.parentMenu_edit_add.Name = "parentMenu_edit_add";
            this.parentMenu_edit_add.Size = new System.Drawing.Size(213, 22);
            this.parentMenu_edit_add.Text = "Добавить строку...";
            this.parentMenu_edit_add.Click += new System.EventHandler(this.parentMenu_edit_add_Click);
            // 
            // parentMenu_edit_edt
            // 
            this.parentMenu_edit_edt.Name = "parentMenu_edit_edt";
            this.parentMenu_edit_edt.Size = new System.Drawing.Size(213, 22);
            this.parentMenu_edit_edt.Text = "Изменить заголовок...";
            this.parentMenu_edit_edt.Click += new System.EventHandler(this.parentMenu_edit_edt_Click);
            // 
            // parentMenu_edit_del
            // 
            this.parentMenu_edit_del.Name = "parentMenu_edit_del";
            this.parentMenu_edit_del.Size = new System.Drawing.Size(213, 22);
            this.parentMenu_edit_del.Text = "Удалить (вкл. все строки)";
            this.parentMenu_edit_del.Click += new System.EventHandler(this.parentMenu_edit_del_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(253, 6);
            // 
            // parentMenu_changeColorAll
            // 
            this.parentMenu_changeColorAll.Name = "parentMenu_changeColorAll";
            this.parentMenu_changeColorAll.Size = new System.Drawing.Size(256, 22);
            this.parentMenu_changeColorAll.Text = "Сменить цвет для всех строк внутри...";
            this.parentMenu_changeColorAll.Click += new System.EventHandler(this.parentMenu_changeColorAll_Click);
            // 
            // parentMenu_AllCollapse
            // 
            this.parentMenu_AllCollapse.Name = "parentMenu_AllCollapse";
            this.parentMenu_AllCollapse.Size = new System.Drawing.Size(256, 22);
            this.parentMenu_AllCollapse.Text = "Свернуть/Развернуть всё";
            this.parentMenu_AllCollapse.Click += new System.EventHandler(this.parentMenu_AllCollapse_Click);
            // 
            // childMenu
            // 
            this.childMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.сменитьЦветToolStripMenuItem,
            this.правкаToolStripMenuItem});
            this.childMenu.Name = "contextMenuStrip1";
            this.childMenu.ShowImageMargin = false;
            this.childMenu.Size = new System.Drawing.Size(134, 48);
            this.childMenu.Text = "Меню ";
            // 
            // сменитьЦветToolStripMenuItem
            // 
            this.сменитьЦветToolStripMenuItem.Name = "сменитьЦветToolStripMenuItem";
            this.сменитьЦветToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.сменитьЦветToolStripMenuItem.Text = "Сменить цвет...";
            this.сменитьЦветToolStripMenuItem.Click += new System.EventHandler(this.changeColor_Click);
            // 
            // правкаToolStripMenuItem
            // 
            this.правкаToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.изменитьToolStripMenuItem,
            this.удалитьToolStripMenuItem});
            this.правкаToolStripMenuItem.Name = "правкаToolStripMenuItem";
            this.правкаToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.правкаToolStripMenuItem.Text = "Правка";
            // 
            // изменитьToolStripMenuItem
            // 
            this.изменитьToolStripMenuItem.Name = "изменитьToolStripMenuItem";
            this.изменитьToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.изменитьToolStripMenuItem.Text = "Изменить...";
            this.изменитьToolStripMenuItem.Click += new System.EventHandler(this.parentMenu_edit_edt_Click);
            // 
            // удалитьToolStripMenuItem
            // 
            this.удалитьToolStripMenuItem.Name = "удалитьToolStripMenuItem";
            this.удалитьToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.удалитьToolStripMenuItem.Text = "Удалить";
            this.удалитьToolStripMenuItem.Click += new System.EventHandler(this.удалитьToolStripMenuItem_Click);
            // 
            // treeMenu
            // 
            this.treeMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addNewNode_ToolStripMenuItem,
            this.toolStripSeparator2,
            this.TopMost_ToolStripMenuItem,
            this.Sound_ToolStripMenuItem,
            this.Auto_ToolStripMenuItem,
            this.SyncTabSize_ToolStripMenuItem,
            this.SyncTabPos_ToolStripMenuItem,
            this.ToggleAll_ToolStripMenuItem});
            this.treeMenu.Name = "treeMenu";
            this.treeMenu.ShowCheckMargin = true;
            this.treeMenu.ShowImageMargin = false;
            this.treeMenu.Size = new System.Drawing.Size(309, 164);
            // 
            // addNewNode_ToolStripMenuItem
            // 
            this.addNewNode_ToolStripMenuItem.Name = "addNewNode_ToolStripMenuItem";
            this.addNewNode_ToolStripMenuItem.Size = new System.Drawing.Size(308, 22);
            this.addNewNode_ToolStripMenuItem.Text = "Добавить заголовок...";
            this.addNewNode_ToolStripMenuItem.Click += new System.EventHandler(this.addNewNode_ToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(305, 6);
            // 
            // TopMost_ToolStripMenuItem
            // 
            this.TopMost_ToolStripMenuItem.Name = "TopMost_ToolStripMenuItem";
            this.TopMost_ToolStripMenuItem.Size = new System.Drawing.Size(308, 22);
            this.TopMost_ToolStripMenuItem.Text = "Поверх всех окон";
            this.TopMost_ToolStripMenuItem.Click += new System.EventHandler(this.TopMost_ToolStripMenuItem_Click);
            // 
            // Sound_ToolStripMenuItem
            // 
            this.Sound_ToolStripMenuItem.Name = "Sound_ToolStripMenuItem";
            this.Sound_ToolStripMenuItem.Size = new System.Drawing.Size(308, 22);
            this.Sound_ToolStripMenuItem.Text = "Звук";
            this.Sound_ToolStripMenuItem.Click += new System.EventHandler(this.Sound_ToolStripMenuItem_Click);
            // 
            // Auto_ToolStripMenuItem
            // 
            this.Auto_ToolStripMenuItem.Name = "Auto_ToolStripMenuItem";
            this.Auto_ToolStripMenuItem.Size = new System.Drawing.Size(308, 22);
            this.Auto_ToolStripMenuItem.Text = "Автоматизация";
            this.Auto_ToolStripMenuItem.Click += new System.EventHandler(this.Auto_ToolStripMenuItem_Click);
            // 
            // SyncTabSize_ToolStripMenuItem
            // 
            this.SyncTabSize_ToolStripMenuItem.Name = "SyncTabSize_ToolStripMenuItem";
            this.SyncTabSize_ToolStripMenuItem.Size = new System.Drawing.Size(308, 22);
            this.SyncTabSize_ToolStripMenuItem.Text = "Синхронизация размера окна для вкладок";
            this.SyncTabSize_ToolStripMenuItem.Click += new System.EventHandler(this.SyncTabSize_ToolStripMenuItem_Click);
            // 
            // SyncTabPos_ToolStripMenuItem
            // 
            this.SyncTabPos_ToolStripMenuItem.Name = "SyncTabPos_ToolStripMenuItem";
            this.SyncTabPos_ToolStripMenuItem.Size = new System.Drawing.Size(308, 22);
            this.SyncTabPos_ToolStripMenuItem.Text = "Синхронизация позиции окна для вкладок";
            this.SyncTabPos_ToolStripMenuItem.Click += new System.EventHandler(this.SyncTabPos_ToolStripMenuItem_Click);
            // 
            // ToggleAll_ToolStripMenuItem
            // 
            this.ToggleAll_ToolStripMenuItem.Name = "ToggleAll_ToolStripMenuItem";
            this.ToggleAll_ToolStripMenuItem.Size = new System.Drawing.Size(308, 22);
            this.ToggleAll_ToolStripMenuItem.Text = "Свернуть/Развернуть всё";
            this.ToggleAll_ToolStripMenuItem.Click += new System.EventHandler(this.ToggleAll_ToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(748, 549);
            this.Controls.Add(this.statusPane);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.LocationChanged += new System.EventHandler(this.Form1_LocationChanged);
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.statusPane.ResumeLayout(false);
            this.statusPane.PerformLayout();
            this.parentMenu.ResumeLayout(false);
            this.childMenu.ResumeLayout(false);
            this.treeMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.NotifyIcon TrayIcon;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.StatusStrip statusPane;
        private System.Windows.Forms.ToolStripStatusLabel StatusText;
        private System.Windows.Forms.ToolStripSplitButton StatusLabelOnTop;
        private System.Windows.Forms.ToolStripSplitButton StatusLabelSound;
        private System.Windows.Forms.ToolStripSplitButton StatusLabelAuto;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabelTabsSizeSync;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabelTabsLocatonSync;
        private System.Windows.Forms.ContextMenuStrip parentMenu;
        private System.Windows.Forms.ToolStripMenuItem parentMenu_changeColorAll;
        private System.Windows.Forms.ToolStripMenuItem parentMenu_changeColor;
        private System.Windows.Forms.ToolStripMenuItem parentMenu_AllCollapse;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem parentMenu_edit;
        private System.Windows.Forms.ToolStripMenuItem parentMenu_edit_add;
        private System.Windows.Forms.ToolStripMenuItem parentMenu_edit_edt;
        private System.Windows.Forms.ToolStripMenuItem parentMenu_edit_del;
        private System.Windows.Forms.ToolStripDropDownButton ProfileList;
        private System.Windows.Forms.ContextMenuStrip childMenu;
        private System.Windows.Forms.ToolStripMenuItem правкаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem изменитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem удалитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem сменитьЦветToolStripMenuItem;
        private System.Windows.Forms.ToolStripSplitButton toolStripSplitButton1;
        private System.Windows.Forms.ToolStripMenuItem новыйПрофильToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip treeMenu;
        private System.Windows.Forms.ToolStripMenuItem addNewNode_ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem TopMost_ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Sound_ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Auto_ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SyncTabSize_ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SyncTabPos_ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ToggleAll_ToolStripMenuItem;

    }
}

