using System;
using System.Collections.Generic;
using System.Data;
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

namespace Triggers
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            // TextBlockTriggers();
            GridBehaviour();
        }

        private void TextBlockTriggers()
        {
            Type type = typeof(TextBlock);
            Style style = new Style(type);
            Brush brush = new SolidColorBrush(Colors.Blue);
            Brush brushChanged = new SolidColorBrush(Colors.Red);

            Setter newSet = new Setter(TextBlock.ForegroundProperty, brush);
            SetterBaseCollection colSett = style.Setters;
            colSett.Add(newSet);

            // setter 1
            Setter setter1 = new Setter(TextBlock.ForegroundProperty, brushChanged);
            // setter 2
            TextDecoration decdor = new TextDecoration(TextDecorationLocation.Underline, new Pen(brushChanged, 2), 0, TextDecorationUnit.FontRecommended, TextDecorationUnit.FontRecommended);
            TextDecorationCollection col = new TextDecorationCollection();
            col.Add(decdor);
            Setter setter2 = new Setter(TextBlock.TextDecorationsProperty, col);


            Trigger trigger = new Trigger();
            trigger.Property = TextBlock.IsMouseOverProperty;
            trigger.Value = true;

            trigger.Setters.Add(setter1);
            trigger.Setters.Add(setter2);


            style.Triggers.Add(trigger);

            txt.Style = style;
        }

        private void DoSmth(object sender, EventArgs e)
        {
            Close();
        }


        void GridBehaviour()
        {
            Type grType = typeof(DataGridCell);
            Style chekBoxStyle = new Style(grType);



            DataGridCheckBoxColumn column = new DataGridCheckBoxColumn();
            column.Binding = new Binding { Mode = BindingMode.TwoWay,
                                           Path = new PropertyPath("Value")
                                         };
            column.HeaderStringFormat = "Header";
            CheckBox chBox = new CheckBox();
            chBox.Checked += ChBox_Checked1;
            chBox.Unchecked += ChBox_Unchecked;


            DataTemplate dtTempl = new DataTemplate();
            FrameworkElementFactory factory = new FrameworkElementFactory(typeof(CheckBox));
            dtTempl.VisualTree = factory;
            factory.SetBinding(CheckBox.IsCheckedProperty, new Binding("Value"));
            factory.AddHandler(CheckBox.CheckedEvent, new EventHandler(ChBox_Checked1));


            return;
            ///////
            DataTemplate dtTempl = new DataTemplate(typeof(CheckBox));
            dtTempl.RegisterName("temp", "chBox");
          

            column.HeaderTemplate = dtTempl;

            dataGrid.Columns.Add(column);




            Setter setRow = new Setter();
            //DependencyProperty dep = DependencyProperty.Register("Name", typeof(String), typeof(DataColumn)) ;
            setRow.Property = DataGridRow.BackgroundProperty;
            setRow.Value = new SolidColorBrush(Colors.MediumAquamarine);
            // new SolidColorBrush(Colors.Azure);



            Style st = new Style(typeof(CheckBox));
            Setter setCheched = new Setter(CheckBox.IsCheckedProperty, true);
            st.Setters.Add(setCheched);

            // триггер сработает, если стиль чекбокса будет, как st
            Trigger trigger = new Trigger();
            trigger.Property = DataGridCheckBoxColumn.CellStyleProperty;
            trigger.Value = st;

            Setter whatToDo = new Setter(CheckBox.ForegroundProperty, new SolidColorBrush(Colors.Violet));
            trigger.Setters.Add(whatToDo);


            trigger.Setters.Add(setRow);
            trigger.Setters.Add(whatToDo);

            chekBoxStyle.Triggers.Add(trigger);
            column.CellStyle = chekBoxStyle;



            // данные
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("Name"));
            dt.Columns.Add(new DataColumn("Value"));

            dt.Rows.Add(dt.NewRow());
            dt.Rows.Add(dt.NewRow());
            dt.Rows.Add(dt.NewRow());
            dt.Rows[0].SetField(0, "Peter");
            dt.Rows[0].SetField(1, true);
            dt.Rows[1].SetField(0, "James");
            dt.Rows[1].SetField(1, false);
            dataGrid.ItemsSource = dt.AsDataView();
        }

        private void ChBox_Unchecked(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Unchecked");
        }

        private void ChBox_Checked1(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Checked");
        }

    }
}