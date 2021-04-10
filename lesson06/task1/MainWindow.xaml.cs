using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace task1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        public string InfoText
        {
            get { return (string)GetValue(InfoTextProperty); }
            set { SetValue(InfoTextProperty, value); }
        }
        public static readonly DependencyProperty InfoTextProperty =
            DependencyProperty.Register("InfoText", typeof(string), typeof(MainWindow), new PropertyMetadata(string.Empty));

        private void Open_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var dlg = new OpenFileDialog();

            if (dlg.ShowDialog() == true)
            {
                Assembly assembly = null;
                var path = dlg.FileName;

                try
                {
                    assembly = Assembly.LoadFile(path);
                    InfoText += $"Сборка {path} успешно загружена" + Environment.NewLine + Environment.NewLine;
                }
                catch (FileNotFoundException ex)
                {
                    InfoText += ex.Message;
                }

                if (assembly != null)
                {
                    InfoText += GetAssemblyInfo(assembly);
                }
            }
        }

        private string GetAssemblyInfo(Assembly assembly)
        {
            var infoBuilder = new StringBuilder();
            infoBuilder.Append("Список всех типов в сборке: ").Append(assembly.FullName).Append(Environment.NewLine).Append(Environment.NewLine);

            var types = assembly.GetTypes();
            int typesCounter = 0;

            foreach (Type type in types)
            {
                typesCounter += 1;
                infoBuilder.Append(Environment.NewLine).Append("Тип ").Append(typesCounter).Append(": ").Append(type).Append(Environment.NewLine);

                var methods = type.GetMethods();
                foreach (MethodInfo method in methods)
                {
                    var methStr = "Метод: " + method.Name + "\n";
                    var methodBody = method.GetMethodBody();
                    if (methodBody != null)
                    {
                        var byteArray = methodBody.GetILAsByteArray();

                        foreach (var b in byteArray)
                        {
                            methStr += b + ":";
                        }
                    }
                    infoBuilder.Append(methStr).Append(Environment.NewLine);
                }


                var properties = type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic);
                foreach (PropertyInfo property in properties)
                {
                    infoBuilder.Append("Свойство: ").Append(property.Name).Append(Environment.NewLine).Append(Environment.NewLine);
                }


                var fields = type.GetFields(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly | BindingFlags.NonPublic);
                foreach (FieldInfo field in fields)
                {
                    infoBuilder.Append("Поле: ").Append(field.Name).Append(field.IsPrivate ? " is private" : " is not private").Append(Environment.NewLine).Append(Environment.NewLine);
                }
            }

            infoBuilder.Append(new string('-', 60)).Append(Environment.NewLine).Append(Environment.NewLine);

            return infoBuilder.ToString();
        }

        private void Close_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Close();
        }
    }
}
