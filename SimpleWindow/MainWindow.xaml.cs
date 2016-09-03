using System;
using System.Collections.Generic;
using System.Linq;
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

namespace SimpleWindow
{
    public class Item
    {
        public Item(string name, int value)
        {
            Name = name;
            ID = value;
        }
        
        public string Name { get; set; }
        public int ID { get; set; }
    }

    public class Asset
    {
        public Asset (int id)
        {
            ID = id;
        }

        // Contains a bunch of stuff, not show here
        public int ID { get; set; }
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public SimpleClass data;
        public MainWindow()
        {
            data = new SimpleClass();
            data.DecimalValue = 2.1m;
            data.Value1 = "temp string";
            data.IntValue = 12;

            InitializeComponent();

            textBox.DataContext = data;

            List<SimpleWindow.Item> elements = new List<SimpleWindow.Item>();
            elements.Add(new SimpleWindow.Item("one", 1));
            elements.Add(new SimpleWindow.Item("two", 2));
            elements.Add(new SimpleWindow.Item("three",3));
            Asset asset = new SimpleWindow.Asset(1);

            comboBox.ItemsSource = elements;
            comboBox.DisplayMemberPath = "Name";
            comboBox.SelectedValuePath = "ID";
            comboBox.SelectedValue = asset.ID;
            comboBox.Tag = asset;
            comboBox.SelectionChanged += OnSelectionChanged;
        }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            Asset asset = cb.Tag as Asset;
            Item selected = cb.SelectedItem as Item;
            asset.ID = selected.ID;
        }
    }
}
