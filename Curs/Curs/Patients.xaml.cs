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

namespace Curs
{
    /// <summary>
    /// Логика взаимодействия для Patients.xaml
    /// </summary>
    public partial class Patients : Window
    {
        static private SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["Curs"].ConnectionString);
        static private SqlDataAdapter Adapter;
        static private SqlCommand Com;
        public Patients()
        {
            InitializeComponent();
        }

        private void Data_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateTable();
        }
        private void UpdateTable()
        {
            sqlConn.Open();
            if (sqlConn.State == ConnectionState.Open)
            {
                DataTable dtPatients = new DataTable();
                DataTable dtMedicine = new DataTable();
                DataTable dtExamination = new DataTable();
                Adapter = new SqlDataAdapter("SELECT * FROM Patients", sqlConn);
                Adapter.Fill(dtPatients);
                Adapter = new SqlDataAdapter("SELECT * FROM Medicine", sqlConn);
                Adapter.Fill(dtMedicine);
                Adapter = new SqlDataAdapter("SELECT * FROM Examination", sqlConn);
                Adapter.Fill(dtExamination);
                TablePatients.ItemsSource = dtPatients.DefaultView;
                TableMedicine.ItemsSource = dtMedicine.DefaultView;
                TableExamination.ItemsSource = dtExamination.DefaultView;
                //TablePatients.Columns[2].Width = 30;
                //TableMedicine.Columns[2].Width = 30;
                //TableExamination.Columns[0].Width = 30;
            }
            else { MessageBox.Show("No connection to DB"); }
            sqlConn.Close();
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            MainWindow MW = new MainWindow();
            MW.Show();
            Data.Close();
        }
    }
}
