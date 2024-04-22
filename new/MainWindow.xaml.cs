using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using System.Configuration;

namespace PR17
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SqlDataAdapter adapter;
        DataTable AeroportTable;
        SqlConnection connection = null;
        string ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void con_btn_Click(object sender, RoutedEventArgs e)
        {
            string sql = "SELECT * FROM Airplanes";
            AeroportTable = new DataTable();

            try
            {
                connection = new SqlConnection(ConnectionString);
                SqlCommand command = new SqlCommand(sql, connection);
                adapter = new SqlDataAdapter(command);

                // установка команды на добавление для вызова хранимой процедуры
                adapter.InsertCommand = new SqlCommand("sp_InsertPhone", connection);
                adapter.InsertCommand.CommandType = CommandType.StoredProcedure;
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@airplane_id", SqlDbType.Int, 0, "airplane_id"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@airplane_name", SqlDbType.NVarChar, 100, "airplane_name"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@date_of_issue", SqlDbType.Date, 0, "date_of_issue"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@capacity", SqlDbType.Int, 0, "capacity"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@last_tech_service", SqlDbType.Date, 0, "last_tech_service"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@flight_hours", SqlDbType.Int, 0, "flight_hours"));

                connection.Open();
                adapter.Fill(AeroportTable);
                MainDG.ItemsSource = AeroportTable.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }
        }

        private void add_btn_Click(object sender, RoutedEventArgs e)
        {
            Window window = new CRUD(1);
            window.Show();
        }

        private void del_btn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void upd_btn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
