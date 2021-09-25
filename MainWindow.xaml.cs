using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace CheckInSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<string> names = new List<string>();
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtName.Text))
            {
                if (listNames.Items.Contains(txtName.Text))
                {
                    MessageBox.Show("你已经签过到了！");
                    return;
                }
                using (StreamWriter Writer = new StreamWriter("data", true))
                {
                    listNames.Items.Add(txtName.Text);
                    names.Add(txtName.Text);
                    Writer.WriteLine(txtName.Text);
                    txtName.Clear();
                    Writer.Close();
                    MessageBox.Show("签到成功！");
                }
            }
            else
            {
                MessageBox.Show("请输入姓名！");
            }
        }

        private void selectLuckyStudent_Click(object sender, RoutedEventArgs e)
        {
            var randomNumber = new Random(Guid.NewGuid().GetHashCode()).Next(0, names.Count);
            if (names.Count != 0)
            {
                MessageBox.Show(names.ElementAt(randomNumber));
            }
            else
            {
                MessageBox.Show("现在没有任何人签到！");
            }
        }

        private void visitExistedNames_Click(object sender, RoutedEventArgs e)
        {
            if (File.Exists("data"))
            {
                StreamReader Reader = new StreamReader("data");
                while (!Reader.EndOfStream)
                {
                    string CurrentName = Reader.ReadLine();
                    if (!listNames.Items.Contains(CurrentName))
                    {
                        listNames.Items.Add(CurrentName);
                        names.Add(CurrentName);
                    }
                }
                Reader.Close();
                MessageBox.Show("加载成功！");
            }
            else
            {
                MessageBox.Show("现在没有任何人签到！");
            }
        }

    }
}
