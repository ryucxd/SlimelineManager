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
        public int staff_id { get; set; }
        public decimal part_validation { get; set; }
        public frm_edit(string passed_id, decimal part_percent_)
        {
            InitializeComponent();
            _id = passed_id;
            part_validation = part_percent_;
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
            if (loop == 1)
                return;
            //old time
            txt_old_time.Text = Convert.ToString(Math.Round((Convert.ToDecimal(dataGridView1.Rows[0].Cells[3].Value) / Convert.ToDecimal(dataGridView1.Rows[0].Cells[6].Value)), 2));
            //find the time for part if the percent complete was 1
            //then use that new % to find out what the new total time should be 
            //old time was 10 and the new time is 20 then the total time should effectively double
            //also need validation like the other function
            decimal percentage;
            decimal time;

            if (string.IsNullOrEmpty(txt_part_percent_complete.Text))
                percentage = Convert.ToDecimal(dataGridView1.Rows[0].Cells[6].Value);
            else
                percentage = Convert.ToDecimal(txt_part_percent_complete.Text);

            if (string.IsNullOrEmpty(txt_time_for_part.Text))
                time = Convert.ToDecimal(dataGridView1.Rows[0].Cells[3].Value);
            else
                time = Convert.ToDecimal(txt_time_for_part.Text);
            decimal one_hundread_percent = Convert.ToDecimal(time) / Convert.ToDecimal(percentage);
            lbl_time.Visible = true;
            txt_total_time.Visible = true;
            lbl_old.Visible = true;
            txt_old_time.Visible = true;
            if (one_hundread_percent < 0)
                one_hundread_percent = one_hundread_percent * -1;
            txt_total_time.Text = Math.Round(one_hundread_percent, 2).ToString();
        }

        //maths for changing part % complete#
        private void maths_ppc()
        {//time for part / new % number
            //stop this from looping forever
            if (loop == 2)
                return;
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
            if (loop == 2) //stop it from looping FOREVER
            {
                loop = 0;
                return;
            }

            //if last character = "." then halt
            if (txt_part_percent_complete.Text.EndsWith("."))
                return;
            if (txt_part_percent_complete.Text == "-")
                txt_part_percent_complete.Text = "";
            if (txt_part_percent_complete.Text == "" || txt_part_percent_complete.Text == " ")
                return;
            if (((part_validation - Convert.ToDecimal(dataGridView1.Rows[0].Cells[6].Value.ToString())) + Convert.ToDecimal(txt_part_percent_complete.Text)) > 1)
            {
                MessageBox.Show("You can't surpass 100% on a job.", "Validation", MessageBoxButtons.OK);
                if (part_validation > 1)
                    txt_part_percent_complete.Text = "0";
                else
                    txt_part_percent_complete.Text = dataGridView1.Rows[0].Cells[6].Value.ToString();
            }
            else if (txt_part_percent_complete.Text == "0")
                return;

            if (txt_part_percent_complete.Text != dataGridView1.Rows[0].Cells[6].Value.ToString())
            {
                loop = 1;
                maths_ppc();
                txt_time_for_part.Enabled = false;
                return;
            }

            else if (txt_part_percent_complete.Text == dataGridView1.Rows[0].Cells[6].Value.ToString())
            {
                txt_time_for_part.Text = dataGridView1.Rows[0].Cells[3].Value.ToString();
            }

        }

        private void Txt_time_for_part_TextChanged(object sender, EventArgs e)
        {
            if (loop == 1)
            {
                loop = 0;
                return;
            }
            if (txt_time_for_part.Text != dataGridView1.Rows[0].Cells[3].Value.ToString())
            {
                loop = 2;
                maths_tfp();
                txt_part_percent_complete.Enabled = false;

                return;
            }


        }

        private void Btn_confirm_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to update this record?", "UPDATE", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                //MessageBox.Show("yeet");
                string date = (dte_date.Value.ToString().Substring(0, 10) + dte_time.Value.ToString().Substring(10, 9));
                DateTime part_date = Convert.ToDateTime(date);

                get_user();
                update(part_date);
                complete();

                //daily goals needs to be updated last.
                daily_goals(part_date);
                if (txt_total_time.Visible == true)
                {
                    using (SqlConnection conn = new SqlConnection(CONNECT.ConnectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand("usp_slimline_output_configurator_time_for_part", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            // MessageBox.Show(dataGridView1.Rows[0].Cells[0].Value.ToString());
                            // int _id = Convert.ToInt32(dataGridView1.Rows[0].Cells[0].Value);
                            //        int _door_id = Convert.ToInt32(dataGridView1.Rows[0].Cells[1].Value.ToString());


                            cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = Convert.ToInt32(_id);
                            cmd.Parameters.Add("@door_id", SqlDbType.VarChar).Value = dataGridView1.Rows[0].Cells[1].Value.ToString();
                            cmd.Parameters.Add("@total_time", SqlDbType.VarChar).Value = txt_total_time.Text;
                            cmd.Parameters.Add("@operation", SqlDbType.VarChar).Value = cmb_operation.SelectedItem.ToString();




                            conn.Open();

                            cmd.ExecuteNonQuery();
                            conn.Close();
                        }
                    }
                }
            }
            else
                return;
        }

        private void get_user()
        {
            using (SqlConnection conn = new SqlConnection(CONNECT.ConnectionStringUser))
            {
                using (SqlCommand cmd = new SqlCommand("select id from dbo.[user] where forename + ' ' + surname = '" + cmb_name.Text + "'", conn))
                {
                    conn.Open();
                    staff_id = Convert.ToInt32(cmd.ExecuteScalar());
                   // MessageBox.Show("Staff ID: " + staff_id.ToString());
                }
            }
        }
        private void update(DateTime passed_date)
        {
            using (SqlConnection conn = new SqlConnection(CONNECT.ConnectionString))
            {
                string sql = "update dbo.door_part_completion_log SET ";
                sql = sql + "part_complete_date = '" + passed_date.ToString("yyyy-MM-dd HH:mm:ss") + "',";
                sql = sql + "time_for_part = " + txt_time_for_part.Text + ",";
                sql = sql + "op = '" + cmb_operation.Text + "',";
                sql = sql + "staff_id = " + staff_id.ToString() + ",";
                sql = sql + "part_percent_complete = " + txt_part_percent_complete.Text + ",";
                sql = sql.Remove(sql.Length - 1);
                sql = sql + " where id  = " + dataGridView1.Rows[0].Cells[0].Value.ToString();
               // MessageBox.Show(sql);

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    //MessageBox.Show(sql);
                    conn.Close();
                }
            }
        }
        private void complete()
        {
            //this updates the X_complete area ONLY if part percent is 1
            //maybe ask tom if this needs to add all the parts from the list rather than just 
            if ((part_validation - Convert.ToDecimal(dataGridView1.Rows[0].Cells[6].Value.ToString())) + Convert.ToDecimal(txt_part_percent_complete.Text) == 1)
            {
                string sql;
                string operation = dataGridView1.Rows[0].Cells[4].Value.ToString();
                sql = "update dbo.door SET ";
                //all of the SL variants 
                if (cmb_operation.Text == "SL_Buff")
                    sql = sql + "date_SL_buff_complete = GETDATE(), complete_SL_buff = 1 ";
                if (cmb_operation.Text == "SL_Pack")
                    sql = sql + "date_pack_complete = GETDATE(), complete_pack = 1  ";
                if (cmb_operation.Text == "SL_Stores")
                    sql = sql + "date_sl_stores_complete = GETDATE(), complete_SL_stores = 1 ";
                //normal variants 
                if (cmb_operation.Text == "Assembly")
                    sql = sql + "date_assembly_complete = GETDATE(), complete_assembly = 1 ";
                if (cmb_operation.Text == "Cutting")
                    sql = sql + "date_cutting_complete = GETDATE(), complete_cutting = 1 ";
                if (cmb_operation.Text == "Prepping")
                    sql = sql + "date_prepping_complete = GETDATE(), complete_prep = 1 ";

                sql = sql + "WHERE id = " + dataGridView1.Rows[0].Cells[1].Value.ToString();
                using (SqlConnection conn = new SqlConnection(CONNECT.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        //MessageBox.Show(sql);
                        conn.Open();
                        //MessageBox.Show(sql);
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }

            }
        }
        private void daily_goals(DateTime passed_date)
        {
            //get date time fixed
            DateTime date_SOD = passed_date; //this is for the first minute of the day
            //MessageBox.Show(date_SOD.ToString());
            date_SOD = date_SOD.AddHours(-date_SOD.Hour).AddMinutes(-date_SOD.Minute).AddSeconds(-date_SOD.Second);
            //MessageBox.Show(date_SOD.ToString());
            DateTime date_EOD; //this is for the final minute of the day -- for daily goals calculation
            date_EOD = date_SOD.AddHours(23).AddMinutes(59).AddSeconds(59);
           // MessageBox.Show(date_EOD.ToString());


            string sql = "select COALESCE(sum(time_for_part / 60),-1) as time from dbo.door_part_completion_log a " +
           "LEFT JOIN [user_info].dbo.[user] b on a.staff_id = b.id " +
           "LEFT JOIN  dbo.door c ON a.door_id = c.id " +
           "LEFT JOIN dbo.door_type d on c.door_type_id = d.id where a.part_percent_complete is not null and part_complete_date >= '" + date_SOD.ToString("yyyy-MM-dd HH:mm:ss") + "' " +
           "AND part_complete_date <= '" + date_EOD.ToString("yyyy-MM-dd HH:mm:ss") + "'";
            decimal actual_hours = 0;

            using (SqlConnection conn = new SqlConnection(CONNECT.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    actual_hours = Convert.ToDecimal(cmd.ExecuteScalar());
                  //  MessageBox.Show("Actual Hours: " + actual_hours.ToString());
                    conn.Close();
                    if (actual_hours == -1)
                    {
                        MessageBox.Show("Please inform IT before trying again.", "ERROR", MessageBoxButtons.OK);
                    }
                }
                sql = "UPDATE dbo.daily_department_goal " +
                "SET actual_hours_slimline = " + actual_hours.ToString() +
                " WHERE date_goal = '" + date_SOD.ToString("yyyy-MM-dd HH:mm:ss") + "'";
               // MessageBox.Show(sql);

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    //MessageBox.Show(sql);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }

        }
    }
}
