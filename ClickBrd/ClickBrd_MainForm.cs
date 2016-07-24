using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
// DLL import
using System.Runtime.InteropServices;

namespace ClickBrd
{
    public partial class Form1 : Form
    {
        Data data = new Data();         //*** Класс данных - все данные приложения

        List<BrdData> TabDataList = new List<BrdData>();
        TreeView tv;
        TreeNode hitNode = null;        // Активная строка
        TreeNode hitpNode = null;       // Родительский элемент активной строки
        Profile activeProfile = null;   // Активный профиль


        // Флаги синхронизации размера и/или месторасположения окна при переключении вкладок
        // true - значит, что если переключается вкладка то окно остается на месте и/или с теми же размерами
        // false - окно становится в определенное место и/или изменяет размер, характерный для каждой вкладки 
        bool isTabsSizeSync = true;
        bool isTabsLocatonSync = true;

        // Выбранная строка дерева (лист или ветвь)
        public Object hitTNS = null;

        // Меню программы:  nodeMenu - заголовка, 
        //                  childMenu - строки, 
        //                  treeViewMenu - вкладки(но по сути это вписанный во вкладку treeView)
        ContextMenuStrip nodeMenu = new ContextMenuStrip();     // Меню для заголовка
        ContextMenuStrip childMenuStrip = new ContextMenuStrip();    // меню для строки
        ContextMenuStrip treeViewMenu = new ContextMenuStrip(); // Меню для вкладки

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
//#if RELEASE
            try{
//#endif
            //********************************************************************************
            /////************************* Чтение конфигов ***********************************
            //********************************************************************************
            ReadMainConfig(); // Чтение конфига, а именно чтение названия приложения, версии и профилей.
            
            // Для каждого профиля читаются его вкладки(brd)
            List<Profile> dbProfile = new List<Profile>();          // Создается переменная для проверки записей
            dbProfile.AddRange(data.Profiles);                       // Записываются значения профилей из постоянного хранилища

            foreach (Profile profile in dbProfile)                  // Перебираются все профили
            {
                foreach (string ClkBrdName in profile.ClkBrdNames)  // Перебираются все вкладки (имена файлов)
                {
                    // Если возвращается пустая вкладка
                    if (new XMLDataAdapter().ReadXMLTreeData(ClkBrdName) == null)
                    {                                               // Удаляется запись о вкладке из постоянного хранилища
                        int idx = dbProfile.IndexOf(profile);
                        data.Profiles[idx].ClkBrdNames.Remove(ClkBrdName);
                        MessageBox.Show("ОШИБКА чтения файла '" + ClkBrdName + "' с настройками вкладок для профиля '" + profile.ProfileName + "' - ФАЙЛ НЕ НАЙДЕН!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                if (profile.ClkBrdNames.Count == 0)
                    data.Profiles.Remove(profile);
            }

            //********************************************************************************
            /////*********************** Настройка интерфейса ********************************
            //********************************************************************************
            // Наименование главного окна
            this.Text = data.WindowCaption + " " + data.Version;
            
            // Добавляется в выпадающий список профили
            foreach (Profile profile in data.Profiles)
                ProfileList.DropDownItems.Add(new ToolStripMenuItem(profile.ProfileName));

            // Открытие профиля по-умолчанию
            ProfileList.Text = "default";

            OpenProfileBrds("default");

            setUpMenu(); // Заполнение меню пунктами и привязка обработчика событий

//#if RELEASE
            }
            catch (Exception ex) 
            { 
                // MessageBox.Show(ex.ToString(), XMLTreeDataFile + "- Ошибка"); 
                /* LogNDebug.SendEmailReport(ex.ToString() + "\n\n\nПожалуйста, добавьте свой текст комментария: опишите действия приведшие к ошибке и отправьте письмо."); */ LogNDebug.SendEmailReport(ex.ToString()); 
            }
//#endif
        }

        //********************************************************************************
        /////****************** Чтение Файлов вкладок с данными и ************************
        /////****************** формирование вкладок с деревьями  ************************
        //********************************************************************************
        private void OpenProfileBrds(string profileName)
        {
            // При открытии профиля обнуляем данные по вкладкам
            TabDataList.Clear();

            // Перебор всех профилей - поиск необходимого для открытия
            foreach (Profile profile in data.Profiles)
                if(profile.ProfileName.Equals(profileName))
                {
                    // Устанавливаем что этот профиль теперь активен
                    activeProfile = profile;

                    // Перебираем вкладки профиля и формируем их в GUI
                    foreach (string ClkBrdName in profile.ClkBrdNames)
                    {
                        // Дерево
                        tv = new TreeView();           // DOTNET CLR должен будет удалить неиспользуемый объект сам... по идее)
                        tv.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top);

                        Cursor.Current = Cursors.WaitCursor;     // Показывает курсор ожидания пока формируется дерево
                        tv.BeginUpdate();                        //Отключает перерисовку дерева
                        tv.Nodes.Clear();                        // Очищает дерево перед заполнением

                        // Чтение данных из файла
                        TabDataList.Add(new XMLDataAdapter().ReadXMLTreeData(ClkBrdName));
                        // Добавление эл-тов дерева из структуры данных.
                        List<List<treeNodeString>> _tree = TabDataList[TabDataList.Count - 1].tree;

                        for (int i = 0; i < _tree.Count; i++)
                        {
                            TreeNode tmpTreeNode = new TreeNode(_tree[i][0].text4view);     // Добавление заголовка
                            tmpTreeNode.ToolTipText = _tree[i][0].text4copy[0];             // и подсказки для неё
                            if (_tree[i][0].option[0].Equals("-"))                          // Если включена опция развернуть
                                tmpTreeNode.Toggle();                                       // Разворачивается ветвь
                            tmpTreeNode.ForeColor = _tree[i][0].textColor;                  // Задается цвет заголовка
                            //tmpTreeNode.NodeFont = new Font("Arial", 20);

                            // Set the ContextMenuStrip property to the ContextMenuStrip.
                            //tmpTreeNode.ContextMenuStrip = nodeMenu;
                            tmpTreeNode.ContextMenuStrip = parentMenu;
                            
                            for (int j = 1; j < _tree[i].Count; j++)            // Далее в цикле добавление листьев
                            {
                                tmpTreeNode.Nodes.Add(_tree[i][j].text4view);                   // Добавление строки
                                tmpTreeNode.LastNode.ToolTipText = _tree[i][j].text4copy[0];    // и подсказки для неё
                                tmpTreeNode.LastNode.ForeColor = _tree[i][j].textColor;         // Задается цвет строки
                                // Set the ContextMenuStrip property to the ContextMenuStrip.
                                //tmpTreeNode.LastNode.ContextMenuStrip = childMenuStrip;
                                tmpTreeNode.LastNode.ContextMenuStrip = childMenu;
                            }
                            tv.Nodes.Add(tmpTreeNode);
                            tmpTreeNode = null;
                        }
                        //Конец добавления.

                        Cursor.Current = Cursors.Default;    // Возвращает обычный курсор
                        tv.EndUpdate();                      // разрешает отрисовку дерева

                        // Настройка дерева
                        tv.AllowDrop = true;
                        tv.ShowNodeToolTips = true;         // Разрешение показывать подсказки       
                        tv.BackColor = TabDataList[TabDataList.Count - 1].treeBackColor;                                 // Задается цвет фона
                        tv.Font = new Font(FontFamily.GenericMonospace, 10);

                        // Регистрация событий клика по узлу дерева
                        

                        tv.MouseDown += new System.Windows.Forms.MouseEventHandler(tv_MouseDown);
                        tv.NodeMouseClick += new TreeNodeMouseClickEventHandler(tv_MouseClick);
                        tv.DragOver += new System.Windows.Forms.DragEventHandler(tv_DragOver);
                        tv.DragDrop += new System.Windows.Forms.DragEventHandler(tv_DragDrop);

                        // Настройка вкладок
                        tabControl1.ShowToolTips = true;    // Разрешаем подсказки для них
                        TabPage tp = new TabPage(ClkBrdName);

                        tp.Controls.Add(tv);
                        tabControl1.TabPages.Add(tp);

                        // Настраиваем меню и пораметры дерева
                        //tv.ContextMenuStrip = treeViewMenu;
                        TopMost_ToolStripMenuItem.CheckOnClick = true;
                        Sound_ToolStripMenuItem.CheckOnClick = true;
                        Auto_ToolStripMenuItem.CheckOnClick = true;
                        SyncTabPos_ToolStripMenuItem.CheckOnClick = true;
                        SyncTabSize_ToolStripMenuItem.CheckOnClick = true;

                        tv.ContextMenuStrip = treeMenu;
                        tv.SetBounds(tv.Bounds.X, tv.Bounds.Y, tp.Bounds.Width, tv.Bounds.Height + 3);
                        //this.Size = new Size(td.formWidth, td.formHeight);

                        // Настройка окна программы
                        this.Size = new Size(TabDataList[tabControl1.SelectedIndex].formWidth, TabDataList[tabControl1.SelectedIndex].formHeight);
                        this.TopMost = TabDataList[tabControl1.SelectedIndex].topMost;
                        this.Location = new Point(TabDataList[tabControl1.SelectedIndex].formX, TabDataList[tabControl1.SelectedIndex].formY);

                        // Настройка строки статуса
                        AddTabStatus();
                    }
                }
        }

        // При выборе объекта мышью
        private void tv_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (!((MouseEventArgs)e).Button.Equals(MouseButtons.Right) & ((Control.ModifierKeys & Keys.Shift) == Keys.Shift)) // Нажатие ПКМ с зажатым Shift
            {
                // Показываем уведомление
                TrayIcon.ShowBalloonTip(2000, "Вы нажали Shift и выбрали элемент дерева строк", "Режим перетаскивания элементов активирован!\nВы можете перетаскивать строки в дереве.\n\nДля работы вставки отпустите Shift.", ToolTipIcon.Info);

                // Получаем дерево.
                TreeView tree = (TreeView)sender;

                // Получаем выбранный узел.
                TreeNode node = tree.GetNodeAt(e.X, e.Y);
                tree.SelectedNode = node;

                // Если узел получен, то обрабатываем drag-and-drop с клонированной копией узла.
                if (node != null)
                {
                    // 
                    tree.DoDragDrop(node, DragDropEffects.Move);
                }
            }
        }

        // Узел перетаскивается с помощью мыши в клиентскую область другого элемента
        private void tv_DragOver(object sender, System.Windows.Forms.DragEventArgs e)
        {
            // Получаем дерево.
            TreeView tree = (TreeView)sender;

            // Drag and drop запрещен по-умолчанию.
            e.Effect = DragDropEffects.None;

            // Проверяем тип объекта и если это узел дерева
            if (e.Data.GetData(typeof(TreeNode)) != null)
            {
                // Получаем кооринаты куа перетащили
                Point pt = new Point(e.X, e.Y);

                // Конвертируем их в кооринаты соответствующие TreeView.
                pt = tree.PointToClient(pt);

                // Проверяем подхоитли узел для операции?
                TreeNode node = tree.GetNodeAt(pt);
                if (node != null)
                {
                    if (node.Level.Equals(0))
                    {
                        // Если это ветвь - разрешаем вставку
                        e.Effect = DragDropEffects.Move;
                        tree.SelectedNode = node;
                    }
                }
            }
        }

        // объект перетаскивается за пределы элемента управления.
        private void tv_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
        {
             // Получаем дерево.
            TreeView tree = (TreeView)sender;
            
            // Получаем кооринаты куа перетащили
            Point pt = new Point(e.X, e.Y);

            // Конвертируем их в кооринаты соответствующие TreeView.
            pt = tree.PointToClient(pt);

            // Проверяем подхоитли узел для операции?
            TreeNode node = tree.GetNodeAt(pt);

            // Добавляем узел.
            if (node.Level.Equals(0))
            {
                TreeNode tnn = (TreeNode)e.Data.GetData(typeof(TreeNode));
                if (!tnn.Nodes.Count.Equals(0)) //Если вставляем не строку, а
                    node.TreeView.Nodes.Add(tnn);
                else // Иначе вставляем
                    node.Nodes.Add(tnn);
            }
            // Show the newly added node if it is not already visible.
            //node.Expand();
        }

        private void ReadMainConfig() // Чтение основного конфига
        {
            // Если нет параметра название главного окна
            if (!System.Configuration.ConfigurationManager.AppSettings.AllKeys.Contains("WindowCaption"))
            {
                // открываем текущий конфиг специальным обьектом
                System.Configuration.Configuration currentConfig = System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None);
                // добавляем позицию в раздел AppSettings
                currentConfig.AppSettings.Settings.Add("WindowCaption", "Папка обмена.");
                //сохраняем
                currentConfig.Save(System.Configuration.ConfigurationSaveMode.Full);
                //принудительно перезагружаем соотвествующую секцию
                System.Configuration.ConfigurationManager.RefreshSection("appSettings");
            }
            // Читается название главного окна
            data.WindowCaption = System.Configuration.ConfigurationManager.AppSettings["WindowCaption"];

            // Если нет параметра версия приложения
            if (!System.Configuration.ConfigurationManager.AppSettings.AllKeys.Contains("Version"))
            {
                // открываем текущий конфиг специальным обьектом
                System.Configuration.Configuration currentConfig = System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None);
                // добавляем позицию в раздел AppSettings
                currentConfig.AppSettings.Settings.Add("Version", "Unknown Version");
                //сохраняем
                currentConfig.Save(System.Configuration.ConfigurationSaveMode.Full);
                //принудительно перезагружаем соотвествующую секцию
                System.Configuration.ConfigurationManager.RefreshSection("appSettings");
            }
            // Читается версия приложения
            data.Version = System.Configuration.ConfigurationManager.AppSettings["Version"];

            // Если нет параметра имена профилей
            if (!System.Configuration.ConfigurationManager.AppSettings.AllKeys.Contains("ProfileNames"))
            {
                // открываем текущий конфиг специальным обьектом
                System.Configuration.Configuration currentConfig = System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None);
                // добавляем позицию в раздел AppSettings
                currentConfig.AppSettings.Settings.Add("ProfileNames", "default");
                //сохраняем
                currentConfig.Save(System.Configuration.ConfigurationSaveMode.Full);
                //принудительно перезагружаем соотвествующую секцию
                System.Configuration.ConfigurationManager.RefreshSection("appSettings");
                //ошибка, надо добавить код на генерацию конфига, существующий недостаточен.

            }
            // Читается имена профилей
            string[] sProfileNames = System.Configuration.ConfigurationManager.AppSettings["ProfileNames"].Split(',');
                
            // Для всех считанных имен профилей создаем объект профиля
            foreach (string ProfileName in sProfileNames)
            {
                // Если нет параметра с таким именем профиля
                if (System.Configuration.ConfigurationManager.AppSettings.AllKeys.Contains(ProfileName))
                {
                    // Читаем списки вкладок(Brd) и добавляем в список профилей
                    List<String> ClkBrdNames = new List<string>();
                    ClkBrdNames.AddRange(System.Configuration.ConfigurationManager.AppSettings[ProfileName].Split(','));
                    // Проверка наличия файлов вкладок
                    for (int i = 0; i < ClkBrdNames.Count; i++)
                    {
                        // Если файла не существует, то 
                        if (!System.IO.File.Exists(ClkBrdNames[i]))
                        {
                            // Удаляется запись о вкладке
                            MessageBox.Show("ОШИБКА чтения файла '" + ClkBrdNames[i] + "' с настройками вкладок для профиля '" + ProfileName + "' - ФАЙЛ НЕ НАЙДЕН!\nДанная вкладка будет не доступна.\n\nДанная оибка будет появляться каждый раз при запуске до тех пор, пока не будет исправлен файл конфигурации.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            ClkBrdNames.RemoveAt(i); 
                            i--;
                        }  
                    }

                    if (ClkBrdNames.Count == 0)
                    {
                        if (MessageBox.Show("ОШИБКА чтения профиля '" + ProfileName + "' - Отсутствуют файлы вкладок.\nДобавить вкладку по-умолчанию в конфигурационный файл?\n\nПри нажатии \"Нет\" профиль будет недоступен!", "Пустой профиль", MessageBoxButtons.YesNo, MessageBoxIcon.Question).Equals(DialogResult.Yes))
                        {
                            // Изменение конфига
                            System.Configuration.Configuration currentConfig = System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None);
                            currentConfig.AppSettings.Settings[ProfileName].Value = "default.txt";
                            currentConfig.Save(System.Configuration.ConfigurationSaveMode.Modified);
                            System.Configuration.ConfigurationManager.RefreshSection("appSettings");
                            // Добавление в коллекцию
                            ClkBrdNames.Add("default.txt");
                            
                            // Добавление в хранилище
                            data.Profiles.Add(new Profile(ProfileName, ClkBrdNames));
                        }
                    }
                    else // Добавление в хранилище
                        data.Profiles.Add(new Profile(ProfileName, ClkBrdNames));
                }
                else
                {
                    if (MessageBox.Show("ОШИБКА чтения профиля '" + ProfileName + "' - Отсутствуют файлы вкладок.\nДобавить вкладку по-умолчанию в конфигурационный файл?\n\nПри нажатии \"Нет\" профиль будет недоступен!", "Пустой профиль", MessageBoxButtons.YesNo, MessageBoxIcon.Question).Equals(DialogResult.Yes))
                    {
                        // Изменение конфига - добавление данных вкладки по-умолчанию
                        System.Configuration.Configuration currentConfig = System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None);
                        // добавляем позицию в раздел AppSettings
                        currentConfig.AppSettings.Settings.Add(ProfileName, "default.txt");
                        //сохраняем
                        currentConfig.Save(System.Configuration.ConfigurationSaveMode.Full);
                        //принудительно перезагружаем соотвествующую секцию
                        System.Configuration.ConfigurationManager.RefreshSection("appSettings");
                        // Добавление в коллекцию
                        List<String> ClkBrdNames = new List<string>();
                        ClkBrdNames.Add("default.txt");
                        // Добавление в хранилище
                        data.Profiles.Add(new Profile(ProfileName, ClkBrdNames));
                    }
                }
            }
        }

        private void AddTabStatus() // Добавляет статус в название вкладки
        {
            // Выставление статусов как ВСЁ ВЫКЛ.
            StatusLabelOnTop.Image = StatusLabelSound.Image = StatusLabelAuto.Image = global::ClickBrd.Properties.Resources._113_;

            TopMost_ToolStripMenuItem.Checked = TabDataList[tabControl1.SelectedIndex].topMost;
            Sound_ToolStripMenuItem.Checked = TabDataList[tabControl1.SelectedIndex].sound;
            Auto_ToolStripMenuItem.Checked = TabDataList[tabControl1.SelectedIndex].doReaction;
            SyncTabPos_ToolStripMenuItem.Checked = isTabsLocatonSync;
            SyncTabSize_ToolStripMenuItem.Checked = isTabsSizeSync;

            // Анализ параметров и выставление статуса вкладки
            if (TabDataList[tabControl1.SelectedIndex].topMost)
            {
                StatusLabelOnTop.Image = global::ClickBrd.Properties.Resources.ontop_on;
            }
            else
            {
                StatusLabelOnTop.Image = global::ClickBrd.Properties.Resources.ontop_off;
            }
            if (TabDataList[tabControl1.SelectedIndex].sound)
            {
                StatusLabelSound.Image = global::ClickBrd.Properties.Resources.sound_on;
            }
            else
            {
                StatusLabelSound.Image = global::ClickBrd.Properties.Resources.sound_off;
            }
            if (TabDataList[tabControl1.SelectedIndex].doReaction)
            {
                StatusLabelAuto.Image = global::ClickBrd.Properties.Resources.auto_on;
            }
            else
            {
                StatusLabelAuto.Image = global::ClickBrd.Properties.Resources.auto_off;
            }
            
            //Анализ и выставление статуса Окна
            if (isTabsLocatonSync)
                StatusLabelTabsLocatonSync.ForeColor = Color.Green;
            else
                StatusLabelTabsLocatonSync.ForeColor = Color.Red;
            if (isTabsSizeSync)
                StatusLabelTabsSizeSync.ForeColor = Color.Green;
            else
                StatusLabelTabsSizeSync.ForeColor = Color.Red;
        }

        private void setUpMenu() // Заполнение меню пунктами и привязка обработчика событий
        {
            // Добавление пунктов меню
            //nodeMenu.Items.Add(new ToolStripMenuItem("Изменить режим поверх всех окон"));
            //nodeMenu.Items.Add(new ToolStripMenuItem("Вкл/Выкл звук")); 
            //nodeMenu.Items.Add(new ToolStripMenuItem("Вкл/Выкл автоматизацию"));

            //nodeMenu.Items.Add(new ToolStripMenuItem("Синхронизация размера окна для вкладок"));
            //nodeMenu.Items.Add(new ToolStripMenuItem("Синхронизация позиции окна для вкладок"));

            //nodeMenu.Items.Add(new ToolStripMenuItem("Изменить цвет фона вкладки"));
            //nodeMenu.Items.Add(new ToolStripSeparator());
            nodeMenu.Items.Add(new ToolStripMenuItem("Сменить цвет для всех записей внутри"));
            nodeMenu.Items.Add(new ToolStripMenuItem("Сменить цвет")); 
            //nodeMenu.Items.Add(new ToolStripMenuItem("Добавить..."));  
            //nodeMenu.Items.Add(new ToolStripMenuItem("Изменить..."),); 
            //nodeMenu.Items.Add(new ToolStripMenuItem("Удалить это и всё внутри")); 
            nodeMenu.Items.Add(new ToolStripSeparator());
            nodeMenu.Items.Add(new ToolStripMenuItem("Свернуть/Развернуть всё"));
            // Привязка обработчика событий
            nodeMenu.ItemClicked += new ToolStripItemClickedEventHandler(ToolStripMenuItem_Click);
            nodeMenu.Opening += new CancelEventHandler(ToolStripMenuItem_Opening);

            // Добавление пунктов меню
            //childMenu.Items.Add(new ToolStripMenuItem("Изменить режим поверх всех окон"));
            //childMenu.Items.Add(new ToolStripMenuItem("Вкл/Выкл звук"));
            //childMenu.Items.Add(new ToolStripMenuItem("Вкл/Выкл автоматизацию"));

            //childMenu.Items.Add(new ToolStripMenuItem("Синхронизация настройки размера окна для вкладок"));
            //childMenu.Items.Add(new ToolStripMenuItem("Синхронизация позиции окна для вкладок"));
            //childMenu.Items.Add(new ToolStripMenuItem("Изменить цвет фона вкладки"));
            //childMenu.Items.Add(new ToolStripSeparator()); 
            childMenuStrip.Items.Add(new ToolStripMenuItem("Сменить цвет")); 
            //childMenu.Items.Add(new ToolStripMenuItem("Изменить...")); 
            //childMenu.Items.Add(new ToolStripMenuItem("Удалить"));
            childMenuStrip.Items.Add(new ToolStripSeparator());
            childMenuStrip.Items.Add(new ToolStripMenuItem("Свернуть/Развернуть всё"));
            // Привязка обработчика событий
            childMenuStrip.ItemClicked += new ToolStripItemClickedEventHandler(ToolStripMenuItem_Click);
            childMenuStrip.Opening += new CancelEventHandler(ToolStripMenuItem_Opening);

            // Добавление пунктов меню
            treeViewMenu.Items.Add(new ToolStripMenuItem("Поверх всех окон"));
            treeViewMenu.Items.Add(new ToolStripMenuItem("Звук"));
            treeViewMenu.Items.Add(new ToolStripMenuItem("Автоматизация"));

            treeViewMenu.Items.Add(new ToolStripMenuItem("Синхронизация размера окна для вкладок"));
            treeViewMenu.Items.Add(new ToolStripMenuItem("Синхронизация позиции окна для вкладок"));
            //treeViewMenu.Items.Add(new ToolStripMenuItem("Изменить цвет фона вкладки"));
            treeViewMenu.Items.Add(new ToolStripMenuItem("Свернуть/Развернуть всё"));
            // Привязка обработчика событий
            treeViewMenu.ItemClicked += new ToolStripItemClickedEventHandler(ToolStripMenuItem_Click);
            treeViewMenu.Opening += new CancelEventHandler(ToolStripMenuItem_Opening);
        }

        private void ToolStripMenuItem_Click(System.Object sender, System.EventArgs e)
        {
            if (((ToolStripItemClickedEventArgs)e).ClickedItem.ToString().Equals("Изменить режим поверх всех окон"))
            {
                TabDataList[tabControl1.SelectedIndex].topMost = !TabDataList[tabControl1.SelectedIndex].topMost;
                this.TopMost = !this.TopMost;
                AddTabStatus();
            }
            if (((ToolStripItemClickedEventArgs)e).ClickedItem.ToString().Equals("Вкл/Выкл звук"))
            {
                TabDataList[tabControl1.SelectedIndex].sound = !TabDataList[tabControl1.SelectedIndex].sound;
                AddTabStatus();
            }
            if (((ToolStripItemClickedEventArgs)e).ClickedItem.ToString().Equals("Вкл/Выкл автоматизацию"))
            {
                TabDataList[tabControl1.SelectedIndex].doReaction = !TabDataList[tabControl1.SelectedIndex].doReaction;
                AddTabStatus();
            }
            if (((ToolStripItemClickedEventArgs)e).ClickedItem.ToString().Equals("Разные настройки размера окна для вкладок"))
            {
                isTabsSizeSync = !isTabsSizeSync;
            }
            if (((ToolStripItemClickedEventArgs)e).ClickedItem.ToString().Equals("Разные настройки позиции окна для вкладок"))
            {
                isTabsLocatonSync = !isTabsLocatonSync;
            }
            if (((ToolStripItemClickedEventArgs)e).ClickedItem.ToString().Equals("Изменить цвет фона вкладки"))
            {
                ColorDialog clrdlg = new ColorDialog();
                if (clrdlg.ShowDialog() == DialogResult.OK)
                {
                    hitNode.TreeView.BackColor = clrdlg.Color;
                }
            }
            if (((ToolStripItemClickedEventArgs)e).ClickedItem.ToString().Equals("Свернуть/Развернуть всё"))
            {
                if (!hitNode.Level.Equals(0))
                {
                    if (hitNode.Parent.IsExpanded)
                        hitNode.TreeView.CollapseAll();
                    else
                        hitNode.TreeView.ExpandAll();
                }
                else
                {
                    if (hitNode.IsExpanded)
                        hitNode.TreeView.CollapseAll();
                    else
                        hitNode.TreeView.ExpandAll();
                }
            }
            if (((ToolStripItemClickedEventArgs)e).ClickedItem.ToString().Equals("Удалить это и всё внутри"))
            {
                if (hitNode.Level.Equals(0))
                {
                    hitNode.Remove();
                }
            }
            if (((ToolStripItemClickedEventArgs)e).ClickedItem.ToString().Equals("Удалить"))
            {
                if (!hitNode.Level.Equals(0))
                {
                    hitNode.Remove();
                }
            }
            if (((ToolStripItemClickedEventArgs)e).ClickedItem.ToString().Equals("Добавить..."))
            {
                this.TopMost = false;
                DataModForm dmf = new DataModForm("Добавление в " + hitNode.Text, false);

                dmf.Owner = this;
                dmf.ShowDialog();
                treeNodeString newTNS = hitTNS as treeNodeString;
                if (newTNS != null)
                {
                    TabDataList[tabControl1.SelectedIndex].addNode(hitNode.Index, newTNS);
                    TreeNode newNode = new TreeNode(newTNS.text4view);
                    newNode.ToolTipText = newTNS.text4copyAsString();
                    newNode.ForeColor = newTNS.textColor;
                    newNode.ContextMenuStrip = childMenuStrip;
                    hitNode.Nodes.Add(newNode);
                }
                this.TopMost = TabDataList[tabControl1.SelectedIndex].topMost;
            }
            if (((ToolStripItemClickedEventArgs)e).ClickedItem.ToString().Equals("Сменить цвет для всех записей внутри"))
            {
                ColorDialog clrdlg = new ColorDialog();
                if (clrdlg.ShowDialog() == DialogResult.OK)
                {
                    foreach (TreeNode childNode in hitNode.Nodes)
                        childNode.ForeColor = clrdlg.Color;
                }
            }
            if (((ToolStripItemClickedEventArgs)e).ClickedItem.ToString().Equals("Сменить цвет"))
            {
                ColorDialog clrdlg = new ColorDialog();
                if (clrdlg.ShowDialog() == DialogResult.OK)
                {
                    hitNode.ForeColor = clrdlg.Color;
                }
            }
            if (((ToolStripItemClickedEventArgs)e).ClickedItem.ToString().Equals("Изменить..."))
            {
                this.TopMost = false;
                if (!hitNode.Level.Equals(0))
                {
                    DataModForm dmf = new DataModForm("Изменение " + hitNode.Text, false);

                    dmf.setText4view(hitNode.Text);
                    dmf.setText4copy(TabDataList[tabControl1.SelectedIndex].getChildText4copy(hitNode.Parent.Text, hitNode.Text));
                    dmf.setOptions(TabDataList[tabControl1.SelectedIndex].getChildOption(hitNode.Parent.Text, hitNode.Text));
                    dmf.setColor(hitNode.ForeColor);

                    dmf.Owner = this;
                    dmf.ShowDialog();
                    treeNodeString newTNS = hitTNS as treeNodeString;
                    if (newTNS != null)
                        if (TabDataList[tabControl1.SelectedIndex].setChildTreeNodeString(hitNode.Parent.Text, hitNode.Text, newTNS))
                        {
                            hitNode.ForeColor = newTNS.textColor;
                            hitNode.Text = newTNS.text4view;
                            hitNode.ToolTipText = newTNS.text4copyAsString();
                        }

                }
                else
                {
                    if (hitNode.Level.Equals(0))
                    {
                        DataModForm dmf = new DataModForm("Изменение " + hitNode.Text, true);

                        dmf.setText4view(hitNode.Text);
                        dmf.setText4copy(TabDataList[tabControl1.SelectedIndex].getParentText4copy(hitNode.Text));
                        dmf.setOptions(TabDataList[tabControl1.SelectedIndex].getParentOption(hitNode.Text));
                        dmf.setColor(hitNode.ForeColor);

                        dmf.Owner = this;
                        dmf.ShowDialog();
                        treeNodeString newTNS = hitTNS as treeNodeString;
                        if (newTNS != null)
                            if (TabDataList[tabControl1.SelectedIndex].setParentTreeNodeString(hitNode.Text, newTNS))
                            {
                                hitNode.ForeColor = newTNS.textColor;
                                hitNode.Text = newTNS.text4view;
                                hitNode.ToolTipText = newTNS.text4copyAsString();
                            }
                    }
                }
                this.TopMost = TabDataList[tabControl1.SelectedIndex].topMost;
            }
            //tv.SelectedNode.Remove();
            //MessageBox.Show(((ToolStripItemClickedEventArgs)e).ClickedItem + " на " + hitNode, "Клик ПКМ на ноде", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        private void ToolStripMenuItem_Opening(System.Object sender, System.EventArgs e)
        {
            foreach (Object menuItem in ((ContextMenuStrip)sender).Items)
            {
                bool bmenuitem = menuItem is ToolStripMenuItem; // Если объект меню это пункт меню, а не разделитель
                if (bmenuitem)
                { //выставляются флажки вкл/выкл для пунктов меню, согласно настройкам для текущей вкладки
                    if (((ToolStripMenuItem)menuItem).Text.Equals("Изменить режим поверх всех окон"))
                        ((ToolStripMenuItem)menuItem).Checked = TabDataList[tabControl1.SelectedIndex].topMost;
                    if (((ToolStripMenuItem)menuItem).Text.Equals("Вкл/Выкл звук"))
                        ((ToolStripMenuItem)menuItem).Checked = TabDataList[tabControl1.SelectedIndex].sound;
                    if (((ToolStripMenuItem)menuItem).Text.Equals("Вкл/Выкл автоматизацию"))
                        ((ToolStripMenuItem)menuItem).Checked = TabDataList[tabControl1.SelectedIndex].doReaction;
                    if (((ToolStripMenuItem)menuItem).Text.Equals("Разные настройки размера окна для вкладок"))
                        ((ToolStripMenuItem)menuItem).Checked = isTabsSizeSync;
                    if (((ToolStripMenuItem)menuItem).Text.Equals("Разные настройки позиции окна для вкладок"))
                        ((ToolStripMenuItem)menuItem).Checked = isTabsLocatonSync;
                }
            }
        }

        // Импорт библиотеки для реализации win32 
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);

        // Коды клавиш для реализации win32
        private const byte VK_MENU = 0x12;
        private const byte VK_TAB = 0x09;
        private const byte VK_SHIFT = 0x10;
        private const byte VK_INSERT = 0x2D; 
        private const int KEYEVENTF_EXTENDEDKEY = 0x01;
        private const int KEYEVENTF_KEYUP = 0x02;

        Boolean debugMode = false;

        private void tv_MouseClick(object sender, EventArgs e)
        {
#if RELEASE
            try { 
#endif

            if (((TreeNodeMouseClickEventArgs)e).Button.Equals(MouseButtons.Right)) // Нажатие ПКМ - открытие меню
            {
                //Запоминаем выбранную ветвь дерева чтобы связать с выбранным в меню действием
                hitNode = ((TreeNodeMouseClickEventArgs)e).Node;
                hitpNode = ((TreeNodeMouseClickEventArgs)e).Node.Parent;
            }
            else
            {
                if (((TreeNodeMouseClickEventArgs)e).Button.Equals(MouseButtons.Left) & (!((Control.ModifierKeys & Keys.Shift) == Keys.Shift))) // Нажатие ЛКМ - работа с буфером обмена
                {
                    if (((TreeNodeMouseClickEventArgs)e).Node.Level != 0)
                    {
                        //TODO: Событие клика на текст.
                        List<string> _text4copy = TabDataList[tabControl1.SelectedIndex].getChildText4copy(((TreeNodeMouseClickEventArgs)e).Node.Parent.Text, ((TreeNodeMouseClickEventArgs)e).Node.Text);
                        if (!_text4copy.Equals(null))
                        {
                            if (!TabDataList[tabControl1.SelectedIndex].doReaction)
                            {
                                //Clipboard.SetText(_text4copy[0]); //Не разрешены автодействия, поэтому просто вставка
                                //Clipboard.SetDataObject(_text4copy[0], true, 5, 10); реализация не привязанная к формату текста, есть подозрение на ошибки в работе.
                                Clipboard.SetText(_text4copy[0]);
                                TrayIcon.ShowBalloonTip(500, "В буфер обмена помещен текст",  _text4copy[0], ToolTipIcon.Info);
                            }
                            else
                            {
                                this.TopMost = false;

                                Clipboard.SetText(_text4copy[0]);
                                //Clipboard.SetDataObject(_text4copy[0], true, 5, 10); реализация не привязанная к формату текста, есть подозрение на ошибки в работе.
                                //TrayIcon.ShowBalloonTip(10, "В буфер обмена помещен текст" , _text4copy[0], ToolTipIcon.Info);


                                //this.WindowState = FormWindowState.Minimized;

                                // ALT-TAB + SHIFT-INS Реализация Win32 т.к. только такая работает на всех необходимых ОС
                                keybd_event(VK_MENU,    0, KEYEVENTF_EXTENDEDKEY | 0, 0);
                                keybd_event(VK_TAB,     0, KEYEVENTF_EXTENDEDKEY | 0, 0);
                                System.Threading.Thread.Sleep(100);
                                keybd_event(VK_TAB,     0, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP, 0);
                                keybd_event(VK_MENU,    0, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP, 0);

                                // Ожидание пока завершится ALT-TAB
                                System.Threading.Thread.Sleep(200);

                                keybd_event(VK_SHIFT,    0, KEYEVENTF_EXTENDEDKEY | 0, 0);
                                keybd_event(VK_INSERT,     0, KEYEVENTF_EXTENDEDKEY | 0, 0);
                                System.Threading.Thread.Sleep(10);
                                keybd_event(VK_INSERT, 0, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP, 0);
                                keybd_event(VK_SHIFT, 0, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP, 0);

                                //this.WindowState = FormWindowState.Normal;

                                TrayIcon.ShowBalloonTip(100, "Автовствка текста", _text4copy[0], ToolTipIcon.Info);

                                /*
                                // ALT-TAB + SHIFT-INS Реализация DDN
                                SendKeys.SendWait("%{TAB}");
                                 
                                System.Threading.Thread.Sleep(100);
                                 
                                SendKeys.SendWait("+{INS}");
                                SendKeys.Flush();
                                */

                                if (TabDataList[tabControl1.SelectedIndex].topMost)
                                    this.Activate();
                                this.TopMost = TabDataList[tabControl1.SelectedIndex].topMost;

                                //    // TODO автодействия
                                //    System.Diagnostics.Process[] p = System.Diagnostics.Process.GetProcessesByName("iexplore");

                                //    // Activate the first application we find with this name
                                //    if (p.Count() > 0)
                                //        SetForegroundWindow(p[0].MainWindowHandle);

                                //    System.Threading.Thread.Sleep(200);

                                //    List<string> _option = treeDatas[tabControl1.SelectedIndex].getOption(((TreeNodeMouseClickEventArgs)e).Node.Parent.Text, ((TreeNodeMouseClickEventArgs)e).Node.Text);
                            }
                            if (TabDataList[tabControl1.SelectedIndex].sound) // Если воспроизводить звук, то
                                Console.Beep(40, 100); //Звук beep при клике
                        }
                    }
                }
            }
#if RELEASE
            } catch (Exception ex) { /* LogNDebug.SendEmailReport(ex.ToString() + "\n\n\nПожалуйста, добавьте свой текст комментария: опишите действия приведшие к ошибке и отправьте письмо."); */ LogNDebug.SendEmailReport(ex.ToString()); }
#endif
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
#if RELEASE
            try{
#endif
            //if (this.WindowState == FormWindowState.Minimized)
            //{
            //    this.ShowInTaskbar = false;
            //    this.Visible = false;
            //}
            //else
            {
                if (tabControl1.TabCount > 0) // Если была загружена хоть одна вкладка
                {
                    TabDataList[tabControl1.SelectedIndex].formWidth = this.Width;
                    TabDataList[tabControl1.SelectedIndex].formHeight = this.Height;
                }

                StatusLabelAuto.DisplayStyle = StatusLabelSound.DisplayStyle = StatusLabelOnTop.DisplayStyle = ProfileList.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;

                if (20 + StatusLabelAuto.Size.Width + StatusLabelSound.Size.Width + StatusLabelOnTop.Size.Width + ProfileList.Size.Width > statusPane.Size.Width)
                {
                    StatusLabelAuto.DisplayStyle = ToolStripItemDisplayStyle.Image;
                    StatusLabelSound.DisplayStyle = ToolStripItemDisplayStyle.Image;
                    StatusLabelOnTop.DisplayStyle = ToolStripItemDisplayStyle.Image;
                    if (20 + StatusLabelAuto.Size.Width + StatusLabelSound.Size.Width + StatusLabelOnTop.Size.Width + ProfileList.Size.Width > statusPane.Size.Width)
                    {
                        ProfileList.DisplayStyle = ToolStripItemDisplayStyle.Image;
                    }
                }
            }

#if RELEASE
            }catch (Exception ex) { /* LogNDebug.SendEmailReport(ex.ToString() + "\n\n\nПожалуйста, добавьте свой текст комментария: опишите действия приведшие к ошибке и отправьте письмо."); */ LogNDebug.SendEmailReport(ex.ToString()); }
#endif
        }

        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
#if RELEASE
            try{
#endif
            if (e.TabPageIndex != -1)
            {
                // Получение выбранной вкладки и параметров для формы.
                //treeData td = ReadTree(((TabControl)sender).SelectedTab.Text);
                // Выставляем параметры формыe
                if (!isTabsSizeSync)
                    this.Size = new Size(TabDataList[tabControl1.SelectedIndex].formWidth, TabDataList[tabControl1.SelectedIndex].formHeight);

                this.TopMost = TabDataList[tabControl1.SelectedIndex].topMost;

                if (!isTabsLocatonSync)
                    this.Location = new Point(TabDataList[tabControl1.SelectedIndex].formX, TabDataList[tabControl1.SelectedIndex].formY);

                AddTabStatus();
            }
#if RELEASE
            } catch (Exception ex) { /* LogNDebug.SendEmailReport(ex.ToString() + "\n\n\nПожалуйста, добавьте свой текст комментария: опишите действия приведшие к ошибке и отправьте письмо."); */ LogNDebug.SendEmailReport(ex.ToString()); }
#endif
        }

        private void tabControl1_Click(object sender, EventArgs e)
        {
                //hitNode = 
        }

        // Перемещение окна.
        private void Form1_LocationChanged(object sender, EventArgs e)
        {
#if RELEASE
            try{
#endif
            
            Point pt = this.Location;
            //MessageBox.Show(TabDataList.Count+"|" + selectedTabndex + "|-|" + pt.X + "|-|" + pt.Y + "|");
            if (TabDataList.Count != 0)
            {
                TabDataList[tabControl1.SelectedIndex].formX = pt.X;
                TabDataList[tabControl1.SelectedIndex].formY = pt.Y;
            }
#if RELEASE
            }catch (Exception ex) { /* LogNDebug.SendEmailReport(ex.ToString() + "\n\n\nПожалуйста, добавьте свой текст комментария: опишите действия приведшие к ошибке и отправьте письмо."); */ LogNDebug.SendEmailReport(ex.ToString()); }
#endif
        }

        private void ProfileList_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            
            tabControl1.TabPages.Clear();
            //TabDataList.Clear();

            String ProfileName = e.ClickedItem.Text;
            if (!ProfileName.Equals("Новый профиль..."))
            {
                ProfileList.Text = ProfileName;
                OpenProfileBrds(ProfileName);

                this.Text = data.WindowCaption + " " + data.Version + " - " + ProfileName;        
            }
        }

        private void StatusLabelOnTop_Click(object sender, EventArgs e)
        {
            if (tabControl1.TabCount > 0)
            {
                TabDataList[tabControl1.SelectedIndex].topMost = !TabDataList[tabControl1.SelectedIndex].topMost;
                this.TopMost = !this.TopMost;
                AddTabStatus();
            }
        }

        private void StatusLabelSound_Click(object sender, EventArgs e)
        {
            if (tabControl1.TabCount > 0)
            {
                TabDataList[tabControl1.SelectedIndex].sound = !TabDataList[tabControl1.SelectedIndex].sound;
                AddTabStatus();
            }
        }

        private void StatusLabelAuto_Click(object sender, EventArgs e)
        {
            if (tabControl1.TabCount > 0)
            {
                TabDataList[tabControl1.SelectedIndex].doReaction = !TabDataList[tabControl1.SelectedIndex].doReaction;
                AddTabStatus();
            }
        }

        private void StatusLabelSizeSync_Click(object sender, EventArgs e)
        {
            isTabsSizeSync = !isTabsSizeSync;
            AddTabStatus();
        }

        private void StatusLabelTabsLocatonSync_Click(object sender, EventArgs e)
        {
            isTabsLocatonSync = !isTabsLocatonSync;
            AddTabStatus();
        }

        private void changeColor_Click(object sender, EventArgs e)
        {
            ColorDialog clrdlg = new ColorDialog();
            if (clrdlg.ShowDialog() == DialogResult.OK)
            {
                hitNode.ForeColor = clrdlg.Color;
            }
        }

        private void parentMenu_edit_add_Click(object sender, EventArgs e)
        {
            this.TopMost = false;
            DataModForm dmf = new DataModForm("Добавление в " + hitNode.Text, false);

            dmf.Owner = this;
            dmf.ShowDialog();
            treeNodeString newTNS = hitTNS as treeNodeString;
            if (newTNS != null)
            {
                TabDataList[tabControl1.SelectedIndex].addNode(hitNode.Index, newTNS);  // Добавление строки
                TreeNode newNode = new TreeNode(newTNS.text4view);
                newNode.ToolTipText = newTNS.text4copyAsString();                       // и подсказки для неё
                newNode.ForeColor = newTNS.textColor;                                   // Задается цвет строки
                newNode.ContextMenuStrip = childMenu;                                   // Меню 
                hitNode.Nodes.Add(newNode);
            }
            this.TopMost = TabDataList[tabControl1.SelectedIndex].topMost;
        }

        private void parentMenu_edit_edt_Click(object sender, EventArgs e)
        {
            this.TopMost = false;
            if (hitNode.Level.Equals(0))
            {
                //DataModForm dmf = new DataModForm("Изменение " + hitNode.Text, true);
                DataModForm dmf = new DataModForm(hitNode, true);

                dmf.setText4view(hitNode.Text);
                dmf.setText4copy(TabDataList[tabControl1.SelectedIndex].getParentText4copy(hitNode.Text));
                dmf.setOptions(TabDataList[tabControl1.SelectedIndex].getParentOption(hitNode.Text));
                dmf.setColor(hitNode.ForeColor);

                dmf.Owner = this;
                dmf.ShowDialog();
                treeNodeString newTNS = hitTNS as treeNodeString;
                if (newTNS != null)
                    if (TabDataList[tabControl1.SelectedIndex].setParentTreeNodeString(hitNode.Text, newTNS))
                    {
                        hitNode.ForeColor = newTNS.textColor;
                        hitNode.Text = newTNS.text4view;
                        hitNode.ToolTipText = newTNS.text4copyAsString();
                    }
            }
            else
            {
                DataModForm dmf = new DataModForm(hitNode, false);

                dmf.setText4view(hitNode.Text);
                dmf.setText4copy(TabDataList[tabControl1.SelectedIndex].getChildText4copy(hitNode.Parent.Text, hitNode.Text));
                dmf.setOptions(TabDataList[tabControl1.SelectedIndex].getChildOption(hitNode.Parent.Text, hitNode.Text));
                dmf.setColor(hitNode.ForeColor);

                dmf.Owner = this;
                dmf.ShowDialog();
                treeNodeString newTNS = hitTNS as treeNodeString;
                if (newTNS != null)
                    if (TabDataList[tabControl1.SelectedIndex].setChildTreeNodeString(hitNode.Parent.Text, hitNode.Text, newTNS))
                    {
                        hitNode.ForeColor = newTNS.textColor;
                        hitNode.Text = newTNS.text4view;
                        hitNode.ToolTipText = newTNS.text4copyAsString();
                    }
            }
            this.TopMost = TabDataList[tabControl1.SelectedIndex].topMost;
        }

        private void parentMenu_edit_del_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Вы действительно хотите удалить все эти элементы?", "Подтвердите удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question).Equals(DialogResult.Yes))
                if (hitNode.Level.Equals(0))
                { 
                    if(TabDataList[tabControl1.SelectedIndex].delNodes(hitNode.Index))
                        hitNode.Remove();
                }
        }

        private void parentMenu_changeColorAll_Click(object sender, EventArgs e)
        {
            ColorDialog clrdlg = new ColorDialog();
            if (clrdlg.ShowDialog() == DialogResult.OK)
            {
                foreach (TreeNode childNode in hitNode.Nodes)
                    childNode.ForeColor = clrdlg.Color;
            }
        }

        private void parentMenu_AllCollapse_Click(object sender, EventArgs e)
        {
            if (!hitNode.Level.Equals(0))
            {
                if (hitNode.Parent.IsExpanded)
                    hitNode.TreeView.CollapseAll();
                else
                    hitNode.TreeView.ExpandAll();
            }
            else
            {
                if (hitNode.IsExpanded)
                    hitNode.TreeView.CollapseAll();
                else
                    hitNode.TreeView.ExpandAll();
            }
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите удалить " + hitNode.Text+ "?", "Подтвердите удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question).Equals(DialogResult.Yes))
            {
                if(TabDataList[tabControl1.SelectedIndex].delNode(hitpNode.Index, hitNode.Index))
                    hitNode.Remove();
            }    
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.F11))
                debugMode = !debugMode;
            //MessageBox.Show("Включен = " + debugMode, "Режим настройки");
        }

        private void saveProfileButton_click(object sender, EventArgs e)
        {
            foreach (BrdData brd in TabDataList)
            {
                new XMLDataAdapter().WriteXMLTreeData(brd);
            }
            // TODO: Сохранение профиля
        }

        private void newProfileMenuItem_Click(object sender, EventArgs e)
        {
            // TODO: создание нового профиля
        }

        private void TrayIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Activate();
            if (this.WindowState == FormWindowState.Minimized)
            {
                //this.Visible = true;
                //this.ShowInTaskbar = true;
                this.WindowState = FormWindowState.Normal;
            }
 
            //else
            //{
            //    //this.Visible = false;
            //    //this.ShowInTaskbar = true;
            //    this.WindowState = FormWindowState.Minimized;
            //}
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            TrayIcon.Visible = false;
            TrayIcon.Dispose();
        }

        private void addNewNode_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.TopMost = false;
            DataModForm dmf = new DataModForm("Добавление строки", true);

            dmf.Owner = this;
            dmf.ShowDialog();
            treeNodeString newTNS = hitTNS as treeNodeString;
            newTNS.option.Add("-");
            if (newTNS != null)
            {
                // Добавляем в базу
                TabDataList[tabControl1.SelectedIndex].addNode(newTNS);
                // Добавляем в дерево
                TreeNode newNode = new TreeNode(newTNS.text4view);
                newNode.ToolTipText = newTNS.text4copyAsString();
                newNode.ForeColor = newTNS.textColor;
                newNode.ContextMenuStrip = parentMenu;
                tv.Nodes.Add(newNode);
            }
            this.TopMost = TabDataList[tabControl1.SelectedIndex].topMost;
        }

        private void ToggleAll_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!hitNode.Level.Equals(0))
            {
                if (hitNode.Parent.IsExpanded)
                    hitNode.TreeView.CollapseAll();
                else
                    hitNode.TreeView.ExpandAll();
            }
            else
            {
                if (hitNode.IsExpanded)
                    hitNode.TreeView.CollapseAll();
                else
                    hitNode.TreeView.ExpandAll();
            }
        }

        private void SyncTabPos_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            isTabsLocatonSync = !isTabsLocatonSync;
            AddTabStatus();
        }

        private void SyncTabSize_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            isTabsSizeSync = !isTabsSizeSync;
            AddTabStatus();
        }

        private void Auto_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TabDataList[tabControl1.SelectedIndex].doReaction = !TabDataList[tabControl1.SelectedIndex].doReaction;
            AddTabStatus();
        }

        private void Sound_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TabDataList[tabControl1.SelectedIndex].sound = !TabDataList[tabControl1.SelectedIndex].sound;
            AddTabStatus();
        }

        private void TopMost_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TabDataList[tabControl1.SelectedIndex].topMost = !TabDataList[tabControl1.SelectedIndex].topMost;
            this.TopMost = !this.TopMost;
            AddTabStatus();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (TabDataList[tabControl1.SelectedIndex].сloseToTray)
                if(e.CloseReason == CloseReason.UserClosing)
                {
                    this.WindowState = FormWindowState.Minimized;
                    e.Cancel = true;
                }   
        }
    }
}
