using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Starter1C
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //printAllPaths();
            var userFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string basesFilePath = userFolder + $"\\AppData\\Roaming\\1C\\1CEStart\\ibases.v8i";
            ReadBasesList(basesFilePath);
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.FromArgb(254, 230, 144);
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.Black;
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.None;
            //dataGridView1.DefaultCellStyle.SelectionBorderColor = Color.Red;
            dataGridView1.CellPainting += dataGridView1_CellPainting;

            //button1.BorderRadius = 10;



        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && dataGridView1.Rows[e.RowIndex].Selected)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.Border);

                Rectangle rectBorder = dataGridView1.GetRowDisplayRectangle(e.RowIndex, true);
                rectBorder.Inflate(-2, -2); // Уменьшение размера строки, чтобы создать эффект бордера
                using (Pen penBorder = new Pen(Color.FromArgb(241, 204, 53), 2)) // Задание цвета и толщины бордера
                {
                    e.Graphics.DrawRectangle(penBorder, rectBorder);
                }

                e.Handled = true;
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public List<string> GetListOfBases(string basesFilePath)
        {
            List<string> basesList = new List<string>();

            if (!File.Exists(basesFilePath))
            {
                return basesList;
            }

            using (StreamReader sr = new StreamReader(basesFilePath))
            {
                while (!sr.EndOfStream)
                {
                    string str = sr.ReadLine();
                    if (str.StartsWith("[") && str.EndsWith("]"))
                    {
                        string baseName = str.Trim('[', ']');
                        basesList.Add(baseName);
                    }
                }
            }

            return basesList;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //if (listBox1.SelectedItem != null)
            //{
            //    string selectedTextItem = listBox1.SelectedItem.ToString();
            //    for (int i = 0; i < listBase.Count; i++)
            //    {
            //        if (listBase[i].name == selectedTextItem)
            //        {
            //            label1.Text = listBase[i].path;
            //        }
            //    }
            //}
        }
        //private void printAllPaths()
        //{
        //    var userFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        //    string basesFilePath = userFolder + $"\\AppData\\Roaming\\1C\\1CEStart\\ibases.v8i";
        //
        //    List<string> items = GetListOfBases(basesFilePath);
        //    for (int i = 0; i < items.Count; i++)
        //    {
        //        GroupBox textBlock = new GroupBox();
        //
        //        Label label = new Label();
        //
        //        label.Text = items[i]; // Установка текстового свойства для Label
        //
        //        textBlock.Controls.Add(label);
        //        //textBlock.Controls.Add(items[i]);
        //        textBlock.BackColor = Color.Blue;
        //
        //
        //        listBox1.Items.Add(textBlock);
        //    }
        //}
        /// <summary>
        /// статусы базы
        /// </summary>
        public enum BasesStatus
        {
            OK, // можно копировать
            NotFound, // файл не найден
            Server // серверная версия БД
        }

        /// <summary>
        /// описание класса базы
        /// </summary>
        public class Bases
        {
            public string name;
            public string dateDel;
            public string path; // путь к файлу БД
            public BasesStatus copied; // копируемая база?
        }

        private List<Bases> listBase = new List<Bases>();
        public bool isDel = false;

        private void ReadBasesList(string path)
        {
            if (!File.Exists(path)) // проверка наличия файла списка
            {
                return;
            }

            StreamReader sr_BasesList = new StreamReader(path); // чтение файла списка

            // очистка листбокса и списка
            //listBox1.Items.Clear();
            listBase.Clear();

            string namebase, pathbase, copiedbase, datedel = "";

            // парсинг файла списка
            // ТУТ ОПИШИ НОВЫЙ АЛГОРИТМ ПАРСИНГА КТОРОЫЙ СОБИРАЕТ ПАЧКАМИ СТРОКИ И ОБЪЕДИНЯЕТ ПО ПАЧКЕ ЕСЛИ ВСТРЕЧАЕТ "[БД]". ТАК ЖЕ НЕОБХОДИМО ПРОПУСКАТЬ ПУСТЫЕ СТРОКИ
            /*
             ПЕРЕБОР ВСЕГО ТЕКСТА ДЛЯ ГРУППИРОВКИ ДАННЫХ  О КАЖДОЙ БАЗЕ
             ЗАПОЛНЕНИЕ КЛАССА БАЗА НА ОСНОВЕ ПОЛУЧЕННЫХ ПАЧЕК БД
             ПРОВЕРКА НАЛИЧИЯ ИСПОЛНЯЕМОГО ФАЙЛА
             
            using System;
            using System.Collections.Generic;

            class Program
            {
                static void Main(string[] args)
                {
                    Dictionary<string, List<string>> dictionary = new Dictionary<string, List<string>>();
        
                    // Добавление пары ключ-значение со строковым ключом и списком строк в качестве значения
                    string key = "имя";
                    List<string> value = new List<string>() { "строка1", "строка2", "строка3" };
                    dictionary.Add(key, value);
        
                    Console.WriteLine("Ключ: " + key);
                    Console.WriteLine("Значение: ");
                    foreach (string item in dictionary[key])
                    {
                        Console.WriteLine(item);
                    }
        
                    Console.ReadLine();
                }
            }

             */
            Dictionary<string, List<string>> basesData = new Dictionary<string, List<string>>();
            string baseName = "";
            List<string> strings = new List<string>();

            while (!sr_BasesList.EndOfStream)
            {
                Dictionary<string, List<string>> t = basesData;
                string str = sr_BasesList.ReadLine();
                // заполнение списка
                if (str.Length != 0) 
                {
                    if (str[0] == '[') // если строка начинается с [, то в ней содержится название базы
                    {
                        str = str.Remove(0, 1);
                        str = str.Remove(str.Length - 1, 1);
                        baseName = str;
                        strings = new List<string>();
                    } else if (str != null && str != "" && baseName != "")
                    {
                        strings.Add(str);
                        basesData[baseName] = strings;
                    }
                }
            }
            string q = "";
            if (basesData.Count!=0) 
            {
                foreach (string base_name in basesData.Keys)
                {
                    //q += base_name + "\n";

                    string name = base_name;
                    string pathExe = "";
                    string dateDel = "";
                    
                    List<string> lines = basesData[base_name];
                    foreach (string base_line in lines)
                    {
                        if (base_line.Contains("DateDel="))
                        {
                            int index = base_line.IndexOf("DateDel=");
                            string substr = base_line.Substring(index + 8);

                            if (substr.Length >= 10 && Regex.IsMatch(substr, @"\d{2}\.\d{2}\.\d{4}"))
                            {
                                dateDel = substr;
                                if (!isDel) { isDel = true; }
                            }
                        }
                    }
                    if (File.Exists(Path.Combine(path, "1cv8.1cd"))) // файл существует, можно копировать
                    {
                        listBase.Add(new Bases { name = name, path = path, copied = BasesStatus.OK, dateDel = dateDel });
                    }
                    else // файла не существует
                    {
                        listBase.Add(new Bases { name = name, path = "", copied = BasesStatus.NotFound, dateDel = dateDel });
                    }
                    //listBase.Add(new Bases { name = name, path = path, dateDel = dateDel });
                    //q += "\n\n";
                    //Dictionary<string, List<string>> w = basesData;
                }
            }
            //Console.WriteLine(q);

            //for

            //if (basesData.Length) 
            //{
            //    
            //}

            //str = sr_BasesList.ReadLine();
            //if (str[0] == '[') continue;
            //
            //if (str.Contains("Connect=")) // если строка содержит "Connect=", то в ней содержится путь к базе
            //{
            //    if (str.Contains("DateDel")) // если строка содержит "File", то база файловая и ее можно копировать
            //    {
            //        str = str.Remove(0, 9);
            //        datedel = str;
            //    }
            //    if (str.Contains("File")) // если строка содержит "File", то база файловая и ее можно копировать
            //    {
            //        // обработка пути к файлу БД
            //        str = str.Remove(0, 14);
            //        str = str.Remove(str.Length - 2, 2);
            //        pathbase = str;
            //
            //
            //        if (File.Exists(Path.Combine(pathbase, "1cv8.1cd"))) // файл существует, можно копировать
            //        {
            //            listBase.Add(new Bases { name = namebase, path = pathbase, copied = BasesStatus.OK, dateDel = datedel });
            //        }
            //        else // файла не существует
            //        {
            //            listBase.Add(new Bases { name = namebase, path = "", copied = BasesStatus.NotFound, dateDel = datedel });
            //        }
            //    }
            //    else // если строка с "Connect=" не содержит "File", то база клиент-серверная или веб-Серверная. База не копируется
            //    {
            //        listBase.Add(new Bases { name = namebase, path = "", copied = BasesStatus.Server, dateDel = datedel });
            //    }
            //}

            // закрыть файл списка баз
            sr_BasesList.Close();

            // Заполнение листбокса
            foreach (Bases bases in listBase)
            {
                switch (bases.copied)
                {
                    case BasesStatus.OK:
                        copiedbase = "";
                        break;
                    case BasesStatus.NotFound:
                        copiedbase = "[Файл БД не найден] ";
                        break;
                    case BasesStatus.Server:
                        copiedbase = "[Серверная БД] ";
                        break;
                    default:
                        break;
                }
                //listBox1.Items.Add(bases.name);
                int rowIndex = dataGridView1.Rows.Add();
                dataGridView1.Rows[rowIndex].Cells[0].Value = bases.name;
                dataGridView1.Rows[rowIndex].Cells[1].Value = bases.dateDel;

            }
            if (!isDel) { dataGridView1.ColumnHeadersVisible = false; }
            //dataGridView1.ColumnHeadersVisible = false;

            //dataGridView1.Columns.Add("Column1", "Имя колонки 1");
            dataGridView1.ReadOnly = true;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dataGridView1.RowHeadersVisible = false;
            //dataGridView1.ColumnHeadersVisible = true;

            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form2 form = new Form2();
            form.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }
    }


}
