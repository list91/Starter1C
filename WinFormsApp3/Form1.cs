using System.Text.RegularExpressions;
using System.Windows.Forms;
using static Starter1C.Form1;

namespace Starter1C
{
    public partial class Form1 : Form
    {
        public int SelectedRowIndex = -1;
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
            button1.Image = new Bitmap(button1.Image, 20, 20);
            if (SelectedRowIndex == -1) 
            {
                button1.Enabled = false;
            }
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
                SelectedRowIndex = e.RowIndex;
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
            int rowIndex = dataGridView1.Rows.Add();
            dataGridView1.Rows[rowIndex].Cells[0].Value = SelectedRowIndex;
        }
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
                    }
                    else if (str != null && str != "" && baseName != "")
                    {
                        strings.Add(str);
                        basesData[baseName] = strings;
                    }
                }
            }
            string q = "";
            if (basesData.Count != 0)
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
