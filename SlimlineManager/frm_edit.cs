using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using System.Data.SqlClient;

namespace SlimlineManager
{
    public partial class frm_edit : Form
    {
        public int loop { get; set; }
        public string _id { get; set; }
        public frm_edit(string passed_id)
        {
            InitializeComponent();
            _id = passed_id;
        }

        private void Frm_edit_Load(object sender, EventArgs e)
        {

            int name_validation = 0;
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
            string test = dataGridView1.Rows[0].Cells[2].Value.ToString();
            string date = test.Substring(0, 10);
            test = test.Remove(0, 11);
            string time = test;
            //MessageBox.Show(test);
            //MessageBox.Show(time);
            dte_date.Value = Convert.ToDateTime(date);
            dte_time.Value = Convert.ToDateTime(time);


            //combo box for gettting all [current] staff for slimline
            //   txt_name.Text = dataGridView1.Rows[0].Cells[5].Value.ToString();
            int validation = 0;
            using (SqlConnection conn = new SqlConnection(CONNECT.ConnectionStringUser))
            {
                using (SqlCommand cmd = new SqlCommand("Select forename + ' ' + surname as [name] FROM dbo.[user] where slimline = -1 and [current] = 1", conn))
                {
                    conn.Open();
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    int total_names = 0; //these two are for passing a string to  combo where the name isnt current etc
                    int not_current = 0; //anthony wells is not current but he appears on recent names so it solves that issue of updating these.
                    foreach (DataRow row in dt.Rows)
                    {
                        total_names++;
                        string nameforcombo = dataGridView1.Rows[0].Cells[5].Value.ToString();
                        if (row["name"].ToString() == nameforcombo)
                        {
                            cmb_name.Items.Insert(0, row["name"].ToString());
                            validation = 1;
                        }
                        else
                        {
                            cmb_name.Items.Add(row["name"].ToString());
                            not_current++;
                        }
                    }
                }
                conn.Close();

                //set the combo box to default to whoever is currently assigned
                if (validation == 1)
                    cmb_name.SelectedIndex = 0;
                else
                {
                    cmb_name.Items.Insert(0, dataGridView1.Rows[0].Cells[5].Value.ToString());
                    cmb_name.SelectedIndex = 0;
                }

            } //end of sql connection
            using (SqlConnection conn = new SqlConnection(CONNECT.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("Select DISTINCT a.op from dbo.door_part_completion_log a " +
                "LEFT JOIN [user_info].dbo.[user] b on a.staff_id = b.id " +
                "LEFT JOIN  dbo.door c ON a.door_id = c.id " +
                "LEFT JOIN dbo.door_type d on c.door_type_id = d.id " +
                "WHERE d.slimline_y_n = -1 AND  part_percent_complete is not null and part_percent_complete <> 0 and op <> '' " +
                "and a.op <> 'pack' and a.op <> 'Painting' and a.op <> 'Prep' ORDER BY op ASC", conn))
                {
                    conn.Open();
                    DataTable td = new DataTable();
                    SqlDataAdapter ad = new SqlDataAdapter(cmd);
                    conn.Close();
                    ad.Fill(td);
                    foreach (DataRow row in td.Rows)
                    {
                        CultureInfo culture = CultureInfo.InvariantCulture;
                        string nameforcombo = dataGridView1.Rows[0].Cells[4].Value.ToString();
                        if (culture.CompareInfo.IndexOf(row["op"].ToString(), nameforcombo, CompareOptions.IgnoreCase) >= 0)
                        {
                            cmb_operation.Items.Insert(0, row["op"].ToString());
                            name_validation = 1;
                        }
                        else
                            cmb_operation.Items.Add(row["op"].ToString());
                    }
                }
            }
            if (name_validation == 1)
                cmb_operation.SelectedIndex = 0;
            //copy others 1:1
            txt_time_for_part.Text = dataGridView1.Rows[0].Cells[3].Value.ToString();
            txt_part_percent_complete.Text = dataGridView1.Rows[0].Cells[6].Value.ToString();
        }

        private void Btn_close_Click(object sender, EventArgs e)
        {
            //this needs to go back to the main form but also select the correct number entered to have the end user start exactly where it transistioned from
            Application.Restart();
        }

        //maths for changing time for part
        private void maths_tfp()
        {
            //find the time for part if the percent complete was 1
            //then use that new % to find out what the new total time should be 
            //old time was 10 and the new time is 20 then the total time should effectively double
            //also need validation like the other function
        

        }

        //maths for changing part % complete#
        private void maths_ppc()
        {//time for part / new % number
            //variables to work this out
            string first_number = "0";
            string second_number = "0";
            decimal rounded = 0;
            decimal time_for_part = Convert.ToDecimal(dataGridView1.Rows[0].Cells[3].Value.ToString());
            string old_value = dataGridView1.Rows[0].Cells[6].Value.ToString(); // if its not a whole number 
            string has_decimal = old_value.Substring(old_value.IndexOf(".") + 1);
            int length = has_decimal.Length;
            decimal final_number = 0;
            // split the strings apart so we can check if there is a whole number easier and make the process a bit cleaner by handling this sooner rather than later.
            if (length > 1)
            {
                first_number = has_decimal.Substring(0, 1);
                second_number = has_decimal.Substring(1, 1);
            }
            else if ((dataGridView1.Rows[0].Cells[6].Value.ToString().Length) < 1)
                return; //if its blank then stop any more code from executing
            else
                first_number = has_decimal.Substring(0, 1);
            //start stitching the strings back together for the maths
            //if its a whole number then....
            if (Convert.ToDecimal(dataGridView1.Rows[0].Cells[6].Value) >= 1)
            {
                final_number = time_for_part;
                final_number = final_number / Convert.ToDecimal(txt_part_percent_complete);
                txt_part_percent_complete.Text = final_number.ToString();
            }
            else //if its not a whole number it has to be a decimal 
            {
                first_number = "0." + first_number + second_number;
                rounded = Convert.ToDecimal(first_number);
                final_number = (time_for_part / Convert.ToDecimal(old_value));
                final_number = final_number * Convert.ToDecimal(txt_part_percent_complete.Text);
                if (final_number < 0)
                    final_number = final_number * -1;
                txt_time_for_part.Text = Math.Round(final_number, 2).ToString();
            }
        }

        private void Txt_part_percent_complete_TextChanged(object sender, EventArgs e)
        {
            //if (loop == 1) //stop it from looping FOREVER
            //{
            //    loop = 0;
            //    return;
            //}
            //if last character = "." then halt
            if (txt_part_percent_complete.Text.EndsWith("."))
                return;
            if (txt_part_percent_complete.Text == "-")
                txt_part_percent_complete.Text = "";
            if (txt_part_percent_complete.Text == "" || txt_part_percent_complete.Text == " ")
                return;

            else if (txt_part_percent_complete.Text == "0")
                return;

            if (txt_part_percent_complete.Text != dataGridView1.Rows[0].Cells[6].Value.ToString())
            {
                maths_ppc();
                txt_time_for_part.Enabled = false;
                return;
            }

            else if (txt_part_percent_complete.Text == dataGridView1.Rows[0].Cells[6].Value.ToString())
            {
                txt_time_for_part.Text = dataGridView1.Rows[0].Cells[3].Value.ToString();

            }

        }
    }
}
