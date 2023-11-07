using System.Windows.Forms;

namespace Starter1C
{
    internal static class Program
    {
        /// <summary>
        /// Точка входа в приложение.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Console.WriteLine("Привет, мир!");  // Добавляем сообщение в консоль
            // Чтобы настроить параметры приложения, такие как настройка высокого разрешения или шрифта по умолчанию, 
            // см. https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());

        }
    }
}

//public partial class Form1 : Form
//{
//    public Form1()
//    {
//        InitializeComponent();
//        //printAllPaths();
//        var userFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
//        string basesFilePath = userFolder + $"\\AppData\\Roaming\\1C\\1CEStart\\ibases.v8i";
//        ReadBasesList(basesFilePath);
//    }
//
//    private void button2_Click(object sender, EventArgs e)
//    {
//        this.Close();
//    }
//
//    private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
//    {
//
//    }
//
//    public List<string> GetListOfBases(string basesFilePath)
//    {
//        List<string> basesList = new List<string>();
//
//        if (!File.Exists(basesFilePath))
//        {
//            return basesList;
//        }
//
//        using (StreamReader sr = new StreamReader(basesFilePath))
//        {
//            while (!sr.EndOfStream)
//            {
//                string str = sr.ReadLine();
//                if (str.StartsWith("[") && str.EndsWith("]"))
//                {
//                    string baseName = str.Trim('[', ']');
//                    basesList.Add(baseName);
//                }
//            }
//        }
//
//        return basesList;
//    }
//
//    private void button1_Click(object sender, EventArgs e)
//    {
//        if (listBox1.SelectedItem != null)
//        {
//            string selectedTextItem = listBox1.SelectedItem.ToString();
//            for (int i = 0; i < listBase.Count; i++)
//            {
//                if (listBase[i].name == selectedTextItem)
//                {
//                    label1.Text = listBase[i].path;
//                }
//            }
//        }
//    }
//
//    public enum BasesStatus
//    {
//        OK, // можно копировать
//        NotFound, // файл не найден
//        Server // серверная версия БД
//    }
//
//    public class Bases
//    {
//        public string name;
//        public string path;
//        public BasesStatus copied;
//    }
//
//    private List<Bases> listBase = new List<Bases>();
//
//    private void ReadBasesList(string path)
//    {
//        if (!File.Exists(path))
//        {
//            return;
//        }
//
//        StreamReader sr_BasesList = new StreamReader(path);
//        listBox1.Items.Clear();
//        listBase.Clear();
//
//        string namebase, pathbase, copiedbase = "";
//
//        while (!sr_BasesList.EndOfStream)
//        {
//            string str = sr_BasesList.ReadLine();
//
//            if (str[0] == '[')
//            {
//                str = str.Remove(0, 1);
//                str = str.Remove(str.Length - 1, 1);
//                namebase = str;
//
//                str = sr_BasesList.ReadLine();
//                if (str[0] == '[') continue;
//
//                if (str.Contains("Connect="))
//                {
//                    if (str.Contains("File"))
//                    {
//                        str = str.Remove(0, 14);
//                        str = str.Remove(str.Length - 2, 2);
//                        pathbase = str;
//                        if (File.Exists(Path.Combine(pathbase, "1cv8.1cd")))
//                        {
//                            listBase.Add(new Bases { name = namebase, path = pathbase, copied = BasesStatus.OK });
//                        }
//                        else
//                        {
//                            listBase.Add(new Bases { name = namebase, path = "", copied = BasesStatus.NotFound });
//                        }
//                    }
//                    else
//                    {
//                        listBase.Add(new Bases { name = namebase, path = "", copied = BasesStatus.Server });
//                    }
//                }
//            }
//        }
//
//        sr_BasesList.Close();
//
//        foreach (Bases bases in listBase)
//        {
//            switch (bases.copied)
//            {
//                case BasesStatus.OK:
//                    copiedbase = "";
//                    break;
//                case BasesStatus.NotFound:
//                    copiedbase = "[Файл БД не найден] ";
//                    break;
//                case BasesStatus.Server:
//                    copiedbase = "[Серверная БД] ";
//                    break;
//                default:
//                    break;
//            }
//
//            listBox1.Items.Add(bases.name);
//        }
//    }
//
//    private void label1_Click(object sender, EventArgs e)
//    {
//
//    }
//}