using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XyzUniversityAPP
{
    public partial class StudentEntryUI : Form
    {
        public StudentEntryUI()
        {
            InitializeComponent();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
           Student aStudent=new Student();
            aStudent.Name = nameTextBox.Text;
            aStudent.Email = emailTextBox.Text;
            aStudent.Address = addressTextBox.Text;


            string conn = @"server=SHIBLY-PC; database=XyzUniversity; integrated security=true";

            SqlConnection Connection = new SqlConnection();

            Connection.ConnectionString = conn;
            Connection.Open();

            string query = string.Format("INSERT INTO t_Student VALUES ('{0}','{1}','{2}')", aStudent.Name, aStudent.Email, aStudent.Address);

            SqlCommand command = new SqlCommand(query, Connection);
            int affectedrow = command.ExecuteNonQuery();
            if (affectedrow > 0)

                MessageBox.Show("insert success");

            else

                MessageBox.Show("problem");

        }

        private void showButton_Click(object sender, EventArgs e)
        {

            string conn = @"server=SHIBLY-PC; database=XyzUniversity; integrated security=true";

            SqlConnection Connection = new SqlConnection();

            Connection.ConnectionString = conn;
            Connection.Open();

            string query = string.Format("SELECT * FROM t_student");

            SqlCommand command = new SqlCommand(query, Connection);

            SqlDataReader aReader = command.ExecuteReader(); 

            List<Student> students=new List<Student>();
            if(aReader.HasRows)
            {
                while (aReader.Read())
                {
                    Student aStudent=new Student();
                    aStudent.StudentId = (int) aReader[0];
                    aStudent.Name = (string) aReader[1];
                    aStudent.Email = aReader[2].ToString();
                    aStudent.Address = aReader[3].ToString();
                    students.Add(aStudent);

                }
                
            }
            Connection.Close();
            studentGridView.DataSource = students;
        }
    }
}
