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
    public partial class frm_main : Form
    {
        public string _id { get; set; }
        public decimal total_part_completed { get; set; }
        public frm_main()
        {
            InitializeComponent();
        }

        private void format()
        {
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[1].HeaderText = "Door ID";
            dataGridView1.Columns[2].HeaderText = "Part Complete Date";
            dataGridView1.Columns[3].HeaderText = "Time for Part";
            dataGridView1.Columns[4].HeaderText = "Operation";
            dataGridView1.Columns[5].HeaderText = "Name";
            dataGridView1.Columns[6].HeaderText = "Part % completed";
        }

        private void Txt_door_number_TextChanged(object sender, EventArgs e)
        {
            //update door number on each key entered
            string sql = "SELECT a.id,a.door_id,a.part_complete_date,a.time_for_part,a.op, forename + ' ' + surname as [name],a.part_percent_complete" +
                " from dbo.door_part_completion_log a LEFT JOIN [user_info].[dbo].[user] b on staff_id = b.id WHERE door_id = " + txt_door_number.Text + " ORDER BY a.id DESC";

            //MessageBox.Show(sql);

            //clean up dgv 
            if (dataGridView1.DataSource != null)
                dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            if (txt_door_number.TextLength == 0)
                return;

            //sql
            using(SqlConnection conn = new SqlConnection(CONNECT.ConnectionString))
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
            format();
        }

        private void DataGridView1_DoubleClick(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Empty;
            }
            int row = Convert.ToInt32(dataGridView1.CurrentCell.RowIndex);
            dataGridView1.Rows[row].DefaultCellStyle.BackColor = Color.CornflowerBlue;

            if (btn_edit.Visible == false)
                btn_edit.Visible = true;
        }

        private void Btn_edit_Click(object sender, EventArgs e)
        {
            //find the operation string and use that in another loop to count all the part % complete where operation = X
            string operation = "";
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].DefaultCellStyle.BackColor == Color.CornflowerBlue)
                {
                    operation = dataGridView1.Rows[i].Cells[4].Value.ToString();
                }
            }
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Cells[4].Value.ToString() == operation)
                {
                    total_part_completed  = total_part_completed + Convert.ToDecimal(dataGridView1.Rows[i].Cells[6].Value.ToString());
                }
            }
            MessageBox.Show(total_part_completed.ToString());
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].DefaultCellStyle.BackColor == Color.CornflowerBlue)
                {
                    _id = dataGridView1.Rows[i].Cells[0].Value.ToString();

                    frm_edit frm = new frm_edit(_id,total_part_completed);
                    this.Hide();
                    frm.Show();
                }
            }
        }
    }
}
