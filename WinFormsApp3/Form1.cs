using System.Diagnostics;
using System.Drawing.Drawing2D;
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
            CheckSelected();

            //ControlBox = false;
            //menuStrip1.BackColor = Color.Blue;

            this.Text = "";

            setStyleBtn(button1);
            setStyleBtn(button2);
            setStyleBtn(button3);





        }
        public void setStyleBtn(Button button)
        {
            button.FlatStyle = FlatStyle.Flat;
            int borderRadius = 3; // Радиус скругления (можно изменить на нужное значение)
            int borderWidth = 3; // Толщина контура линии (можно изменить на нужное значение)

            System.Drawing.Pen borderPen = new System.Drawing.Pen(System.Drawing.Color.Black, borderWidth);

            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            int arcSize = borderRadius * 2;

            // Верхний левый угол
            path.AddArc(new System.Drawing.Rectangle(0, 0, arcSize, arcSize), 180, 90);
            // Верхняя граница
            path.AddLine(borderRadius, 0, button.Width - borderRadius, 0);
            // Верхний правый угол
            path.AddArc(new System.Drawing.Rectangle(button.Width - arcSize, 0, arcSize, arcSize), 270, 90);
            // Правая граница
            path.AddLine(button.Width, borderRadius, button.Width, button.Height - borderRadius);
            // Нижний правый угол
            path.AddArc(new System.Drawing.Rectangle(button.Width - arcSize, button.Height - arcSize, arcSize, arcSize), 0, 90);
            // Нижняя граница
            path.AddLine(button.Width - borderRadius, button.Height, borderRadius, button.Height);
            // Нижний левый угол
            path.AddArc(new System.Drawing.Rectangle(0, button.Height - arcSize, arcSize, arcSize), 90, 90);
            // Левая граница
            path.AddLine(0, button.Height - borderRadius, 0, borderRadius);

            button.Region = new System.Drawing.Region(path);
            button.Paint += (sender, e) =>
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                e.Graphics.DrawPath(borderPen, path);
            };
        }


        public void CheckSelected()
        {
            if (SelectedRowIndex == -1)
            {
                yt_Button1.HideButton();
                button1.Enabled = false;
            }
            else
            {
                yt_Button1.ShowButton();
                button1.Enabled = true;
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
                CheckSelected();
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
            //dataGridView1.Rows[rowIndex].Cells[0].Value = SelectedRowIndex;
            string nameDB = dataGridView1.Rows[SelectedRowIndex].Cells[0].Value.ToString();
            //dataGridView1.Rows[rowIndex].Cells[0].Value = nameDB;
            string pathExe = null;
            foreach (Bases bases in listBase)
            {
                if (bases.name == nameDB)
                {
                    pathExe = bases.path;
                }
            }
            if (pathExe != null)
            {
                try
                {
                    //Process.Start("C:\\Users\\Sibgroup-PC2\\Desktop\\javafx config.txt");
                    Process.Start(new ProcessStartInfo { FileName = pathExe, UseShellExecute = true });
                    Console.WriteLine("Файл успешно запущен.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при запуске файла: {ex.Message}");
                }
            }
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
                        if (base_line.Contains("Connect=File="))
                        {
                            string input = base_line;

                            // Проверяем наличие подстроки "Connect=File="
                            int startIndex = input.IndexOf("Connect=File=");

                            if (startIndex != -1)
                            {
                                // Получаем оставшуюся часть строки
                                input = input.Substring(startIndex + "Connect=File=".Length);

                                int startIndexPoint = input.IndexOf('"');
                                int endIndexPoint = input.LastIndexOf('"');

                                if (startIndexPoint >= 0 && startIndexPoint != endIndexPoint)
                                {
                                    string findFolder = input.Substring(startIndexPoint + 1, endIndexPoint - startIndexPoint - 1);
                                    string V8iFile = FindV8iFile(findFolder);
                                    if (V8iFile != null) { pathExe = findFolder + "\\" + V8iFile; }
                                }

                                //pathExe = string.Empty;
                            }
                        }
                    }
                    //Console.WriteLine(pathExe);
                    // если есть файл БД и её лаунчер
                    //if (File.Exists(Path.Combine(path, "1cv8.1cd")) && Directory.GetFiles(path, "*.v8i").Length > 0)
                    if (pathExe != "")
                    {
                        // файл существует, можно копировать

                        listBase.Add(new Bases { name = name, path = pathExe, copied = BasesStatus.OK, dateDel = dateDel });
                    }

                    //else // файла не существует
                    //{
                    //    listBase.Add(new Bases { name = name, path = pathExe, copied = BasesStatus.NotFound, dateDel = dateDel });
                    //}
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
        static string FindV8iFile(string folderPath)
        {
            if (Directory.Exists(folderPath))
            {
                string[] files = Directory.GetFiles(folderPath, "*.v8i");
                if (files.Length > 0)
                {
                    string fileName = Path.GetFileName(files[0]);
                    return fileName;
                }
            }

            return null;
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

        private void yt_Button2_Click(object sender, EventArgs e)
        {
            this.Close();
            //yt_Button2.HideButton();
        }

        private void yt_Button4_Click(object sender, EventArgs e)
        {
            Form2 form = new Form2();
            form.Show();
        }

        private void yt_Button1_Click(object sender, EventArgs e)
        {
            if (yt_Button1.Visible())
            {
                int rowIndex = dataGridView1.Rows.Add();
                //dataGridView1.Rows[rowIndex].Cells[0].Value = SelectedRowIndex;
                string nameDB = dataGridView1.Rows[SelectedRowIndex].Cells[0].Value.ToString();
                //dataGridView1.Rows[rowIndex].Cells[0].Value = nameDB;
                string pathExe = null;
                foreach (Bases bases in listBase)
                {
                    if (bases.name == nameDB)
                    {
                        pathExe = bases.path;
                    }
                }
                if (pathExe != null)
                {
                    try
                    {
                        //Process.Start("C:\\Users\\Sibgroup-PC2\\Desktop\\javafx config.txt");
                        Process.Start(new ProcessStartInfo { FileName = pathExe, UseShellExecute = true });
                        Console.WriteLine("Файл успешно запущен.");
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Ошибка при запуске файла: {ex.Message}");
                    }
                }
            }
        }
    }


}
