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
                    dataGridView1.Columns[0].HeaderText = "ID";
                    dataGridView1.Columns[1].HeaderText = "Door ID";
                    dataGridView1.Columns[2].HeaderText = "Part Complete Date";
                    dataGridView1.Columns[3].HeaderText = "Time for Part";
                    dataGridView1.Columns[4].HeaderText = "Operation";
                    dataGridView1.Columns[5].HeaderText = "Name";
                    dataGridView1.Columns[6].HeaderText = "Part % completed";
                    conn.Close();
                }
            }
        }
    }
}
