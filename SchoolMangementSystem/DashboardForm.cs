using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Data.SqlClient;

namespace SchoolMangementSystem
{
    public partial class DashboardForm : UserControl
    {
        SqlConnection connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Laptop Code\Documents\school3.mdf;Integrated Security=True;Connect Timeout=30");
        public DashboardForm()
        {
            InitializeComponent();

            displayTotalES();
            displayTotalTT();
            displayTotalGS();

            displayEnrolledStudentToday();
        }


        //Display Enrolled Student
        public void displayTotalES()
        {
            if(connect.State != ConnectionState.Open)
            {
                try
                {
                    connect.Open();
                    string selectData = "SELECT COUNT(id) FROM students WHERE student_status = 'Enrolled' AND date_delete IS NULL";

                    using (SqlCommand cmd = new SqlCommand(selectData, connect))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        int tempES = 0;
                        if (reader.Read())
                        {
                            tempES = Convert.ToInt32(reader[0]);

                            total_ES.Text = tempES.ToString();
                        }
                    }

                }
                catch(Exception ex)
                {
                    MessageBox.Show("Error to connect Database: " + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                finally
                {
                    connect.Close();
                }
            }
        }


        //Display Total Teachers
        public void displayTotalTT()
        {
            if (connect.State != ConnectionState.Open)
            {
                try
                {
                    connect.Open();
                    string selectData = "SELECT COUNT(id) FROM teachers WHERE teacher_status = 'Active' AND  date_delete IS NULL";

                    using (SqlCommand cmd = new SqlCommand(selectData, connect))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        int tempTT = 0;
                        if (reader.Read())
                        {
                            tempTT = Convert.ToInt32(reader[0]);

                            total_TT.Text = tempTT.ToString();
                        }
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error to connect Database: " + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                finally
                {
                    connect.Close();
                }
            }
        }

        //Display Graduated Students+
        public void displayTotalGS()
        {
            if (connect.State != ConnectionState.Open)
            {
                try
                {
                    connect.Open();
                    string selectData = "SELECT COUNT(id) FROM students WHERE student_status = 'Graduated' AND date_delete IS NULL";

                    using (SqlCommand cmd = new SqlCommand(selectData, connect))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        int tempGS = 0;
                        if (reader.Read())
                        {
                            tempGS = Convert.ToInt32(reader[0]);

                            total_GS.Text = tempGS.ToString();
                        }
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error to connect Database: " + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                finally
                {
                    connect.Close();
                }
            }
        }

        public void displayEnrolledStudentToday()
        {
            AddStudentData asData = new AddStudentData();

            dataGridView1.DataSource = asData.dashboardStudentData();
        }

        private void DashboardForm_Load(object sender, EventArgs e)
        {

        }

        private void chart_dash_Click(object sender, EventArgs e)
        {



            chart_dash.ChartAreas[0].AxisY.Interval = 1;
            chart_dash.Enabled = false;
            chart_dash.Series["Data"].Points.AddXY("Enrolled Students", total_ES.Text);
            chart_dash.Series["Data"].Points.AddXY("Graduated Students", total_GS.Text);
            chart_dash.Series["Data"].Points.AddXY("Active Teacher", total_TT.Text);




        }

        private void total_TT_Click(object sender, EventArgs e)
        {

        }
    }
}
