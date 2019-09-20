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

namespace SlimlineManager
{
    public partial class frm_edit : Form
    {
        public string _id { get; set; }
        public frm_edit(string passed_id)
        {
            InitializeComponent();
            _id = passed_id;
        }

        private void Frm_edit_Load(object sender, EventArgs e)
        {
            string sql = "select a.id,a.door_id,a.part_complete_date,a.time_for_part,a.op,b.forename + ' ' + b.surname as [name],a.part_percent_complete " +
                "from dbo.door_part_completion_log a Left join[user_info].dbo.[user] b on a.staff_id = b.id WHERE a.id = " + _id;
            using (SqlConnection conn = new SqlConnection(CONNECT.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                    conn.Close();
                }
            }
            //load the DGV into each text boxes. trying a new layout than last time.
            lbl_title.Text = "Door ID = " + dataGridView1.Rows[0].Cells[1].Value.ToString();
            locked_part_complete_date.Text = dataGridView1.Rows[0].Cells[2].Value.ToString();
            locked_time_for_part.Text = dataGridView1.Rows[0].Cells[3].Value.ToString();
            locked_operation.Text = dataGridView1.Rows[0].Cells[4].Value.ToString();
            locked_name.Text = dataGridView1.Rows[0].Cells[5].Value.ToString();
            locked_part_percent_complete.Text = dataGridView1.Rows[0].Cells[6].Value.ToString();

            //load them into the editable textboxes too
            //split the date and time apart to allow for a kinder interface

            //dte_time.Value = Convert.ToDateTime(time)
            string test = dataGridView1.Rows[0].Cells[2].Value.ToString();
            string date = test.Substring(0,10);
             test = test.Remove(0,11);
            string time = test;
            MessageBox.Show(test);
            MessageBox.Show(time);
            dte_date.Value = Convert.ToDateTime(date);
            dte_time.Value = Convert.ToDateTime(time);
            txt_part_complete_date.Text = dataGridView1.Rows[0].Cells[2].Value.ToString();
            //copy others 1:1
            txt_time_for_part.Text = dataGridView1.Rows[0].Cells[3].Value.ToString();
            txt_operation.Text = dataGridView1.Rows[0].Cells[4].Value.ToString();
            txt_name.Text = dataGridView1.Rows[0].Cells[5].Value.ToString();
            txt_part_percent_complete.Text = dataGridView1.Rows[0].Cells[6].Value.ToString();
        }

        private void Btn_close_Click(object sender, EventArgs e)
        {
            //this needs to go back to the main form but also select the correct number entered to have the end user start exactly where it transistioned from
            Application.Restart();
        }
    }
}
