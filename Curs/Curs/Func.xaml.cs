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
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Globalization;
namespace Curs
{
    /// <summary>
    /// Логика взаимодействия для Func.xaml
    /// </summary>
    public partial class Func : Window
    {
        static private SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["Curs"].ConnectionString);
        static private SqlDataAdapter Adapter;
        static private SqlCommand Com;
        public Func()
        {
            InitializeComponent();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            MainWindow MW = new MainWindow ();
            MW.Show();
            Func1.Close();
        }

        private void Calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            Date.Text = Calendar.SelectedDate.Value.ToString("yyyy-MM-dd");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string str= "select DoctorName, Address, Date from Examination where Date = '" + Date.Text + "'";
            sqlConn.Open();
            Adapter = new SqlDataAdapter(str,sqlConn);
            DataTable dt = new DataTable();
            Adapter.Fill(dt);
            ExGrid.ItemsSource = dt.DefaultView;
            sqlConn.Close();
            CountEx.Text = dt.Rows.Count.ToString();
        }
        private void ShowPat_Click(object sender, RoutedEventArgs e)
        {
            string str = "select Name as 'Ім''я', Date as 'Дата' from Examination inner join Patients ON Examination.IDPatient=Patients.IDPatient where Diagnosis = '"+ illness.Text +"';";
            sqlConn.Open();
            Adapter = new SqlDataAdapter(str, sqlConn);
            DataTable dt = new DataTable();
            Adapter.Fill(dt);
            IllGrid.ItemsSource = dt.DefaultView;
            sqlConn.Close();
            CountIll.Text = dt.Rows.Count.ToString();
        }

        private void ShowSide_Click(object sender, RoutedEventArgs e)
        {
            string str = "select Name, SideEffects from Medicine where Name='" + Med.Text + "';";
            sqlConn.Open();
            Adapter = new SqlDataAdapter(str, sqlConn);
            DataTable dt = new DataTable();
            Adapter.Fill(dt);
            SideGrid.ItemsSource = dt.DefaultView;
            sqlConn.Close();
        }

        private void AddMedicine_Click(object sender, RoutedEventArgs e)
        {          
            string str2 = "'" + MedName.Text + "', " + "'" + MedAdministering.Text + "', " + "'" + MedAction.Text + "', " + "'" + MedSideEffects.Text + "'";
            string str = "insert into Medicine (Name, Administering, Action, SideEffects) values (" + str2 + ")";
            sqlConn.Open();
            Com = new SqlCommand(str, sqlConn);
            Com.ExecuteNonQuery();
            MessageBox.Show("Ліки успішно додано!");
            sqlConn.Close();
        }
    }
}
