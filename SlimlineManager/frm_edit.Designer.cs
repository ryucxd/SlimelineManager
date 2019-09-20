namespace SlimlineManager
{
    partial class frm_edit
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_edit));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.locked_part_complete_date = new System.Windows.Forms.TextBox();
            this.lbl_title = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.locked_time_for_part = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.locked_operation = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.locked_name = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.locked_part_percent_complete = new System.Windows.Forms.TextBox();
            this.txt_part_percent_complete = new System.Windows.Forms.TextBox();
            this.txt_name = new System.Windows.Forms.TextBox();
            this.txt_operation = new System.Windows.Forms.TextBox();
            this.txt_time_for_part = new System.Windows.Forms.TextBox();
            this.txt_part_complete_date = new System.Windows.Forms.TextBox();
            this.btn_close = new System.Windows.Forms.Button();
            this.dte_date = new System.Windows.Forms.DateTimePicker();
            this.dte_time = new System.Windows.Forms.DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 7);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(0, 0);
            this.dataGridView1.TabIndex = 3;
            // 
            // locked_part_complete_date
            // 
            this.locked_part_complete_date.Location = new System.Drawing.Point(30, 59);
            this.locked_part_complete_date.Name = "locked_part_complete_date";
            this.locked_part_complete_date.ReadOnly = true;
            this.locked_part_complete_date.Size = new System.Drawing.Size(120, 20);
            this.locked_part_complete_date.TabIndex = 4;
            this.locked_part_complete_date.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lbl_title
            // 
            this.lbl_title.AutoSize = true;
            this.lbl_title.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_title.Location = new System.Drawing.Point(275, 9);
            this.lbl_title.Name = "lbl_title";
            this.lbl_title.Size = new System.Drawing.Size(106, 19);
            this.lbl_title.TabIndex = 5;
            this.lbl_title.Text = "Door ID: 00000";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(24, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(136, 19);
            this.label1.TabIndex = 6;
            this.label1.Text = "Part Complete Date";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(173, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 19);
            this.label2.TabIndex = 8;
            this.label2.Text = "Time for Part";
            // 
            // locked_time_for_part
            // 
            this.locked_time_for_part.Location = new System.Drawing.Point(169, 59);
            this.locked_time_for_part.Name = "locked_time_for_part";
            this.locked_time_for_part.ReadOnly = true;
            this.locked_time_for_part.Size = new System.Drawing.Size(100, 20);
            this.locked_time_for_part.TabIndex = 7;
            this.locked_time_for_part.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(298, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 19);
            this.label3.TabIndex = 10;
            this.label3.Text = "Operation";
            // 
            // locked_operation
            // 
            this.locked_operation.Location = new System.Drawing.Point(285, 59);
            this.locked_operation.Name = "locked_operation";
            this.locked_operation.ReadOnly = true;
            this.locked_operation.Size = new System.Drawing.Size(100, 20);
            this.locked_operation.TabIndex = 9;
            this.locked_operation.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(430, 37);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 19);
            this.label4.TabIndex = 12;
            this.label4.Text = "Name";
            // 
            // locked_name
            // 
            this.locked_name.Location = new System.Drawing.Point(403, 59);
            this.locked_name.Name = "locked_name";
            this.locked_name.ReadOnly = true;
            this.locked_name.Size = new System.Drawing.Size(100, 20);
            this.locked_name.TabIndex = 11;
            this.locked_name.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(510, 37);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(122, 19);
            this.label5.TabIndex = 14;
            this.label5.Text = "Part % completed";
            // 
            // locked_part_percent_complete
            // 
            this.locked_part_percent_complete.Location = new System.Drawing.Point(533, 59);
            this.locked_part_percent_complete.Name = "locked_part_percent_complete";
            this.locked_part_percent_complete.ReadOnly = true;
            this.locked_part_percent_complete.Size = new System.Drawing.Size(78, 20);
            this.locked_part_percent_complete.TabIndex = 13;
            this.locked_part_percent_complete.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txt_part_percent_complete
            // 
            this.txt_part_percent_complete.Location = new System.Drawing.Point(533, 107);
            this.txt_part_percent_complete.Name = "txt_part_percent_complete";
            this.txt_part_percent_complete.Size = new System.Drawing.Size(78, 20);
            this.txt_part_percent_complete.TabIndex = 19;
            // 
            // txt_name
            // 
            this.txt_name.Location = new System.Drawing.Point(403, 107);
            this.txt_name.Name = "txt_name";
            this.txt_name.Size = new System.Drawing.Size(100, 20);
            this.txt_name.TabIndex = 18;
            // 
            // txt_operation
            // 
            this.txt_operation.Location = new System.Drawing.Point(285, 107);
            this.txt_operation.Name = "txt_operation";
            this.txt_operation.Size = new System.Drawing.Size(100, 20);
            this.txt_operation.TabIndex = 17;
            // 
            // txt_time_for_part
            // 
            this.txt_time_for_part.Location = new System.Drawing.Point(169, 107);
            this.txt_time_for_part.Name = "txt_time_for_part";
            this.txt_time_for_part.Size = new System.Drawing.Size(100, 20);
            this.txt_time_for_part.TabIndex = 16;
            // 
            // txt_part_complete_date
            // 
            this.txt_part_complete_date.Location = new System.Drawing.Point(12, 148);
            this.txt_part_complete_date.Name = "txt_part_complete_date";
            this.txt_part_complete_date.Size = new System.Drawing.Size(120, 20);
            this.txt_part_complete_date.TabIndex = 15;
            // 
            // btn_close
            // 
            this.btn_close.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_close.Location = new System.Drawing.Point(578, 162);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(67, 27);
            this.btn_close.TabIndex = 20;
            this.btn_close.Text = "Close";
            this.btn_close.UseVisualStyleBackColor = true;
            this.btn_close.Click += new System.EventHandler(this.Btn_close_Click);
            // 
            // dte_date
            // 
            this.dte_date.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dte_date.Location = new System.Drawing.Point(12, 107);
            this.dte_date.Name = "dte_date";
            this.dte_date.Size = new System.Drawing.Size(79, 20);
            this.dte_date.TabIndex = 21;
            // 
            // dte_time
            // 
            this.dte_time.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dte_time.Location = new System.Drawing.Point(93, 107);
            this.dte_time.Name = "dte_time";
            this.dte_time.Size = new System.Drawing.Size(70, 20);
            this.dte_time.TabIndex = 22;
            // 
            // frm_edit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(657, 201);
            this.ControlBox = false;
            this.Controls.Add(this.dte_time);
            this.Controls.Add(this.dte_date);
            this.Controls.Add(this.btn_close);
            this.Controls.Add(this.txt_part_percent_complete);
            this.Controls.Add(this.txt_name);
            this.Controls.Add(this.txt_operation);
            this.Controls.Add(this.txt_time_for_part);
            this.Controls.Add(this.txt_part_complete_date);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.locked_part_percent_complete);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.locked_name);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.locked_operation);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.locked_time_for_part);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbl_title);
            this.Controls.Add(this.locked_part_complete_date);
            this.Controls.Add(this.dataGridView1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frm_edit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Slimline Manager";
            this.Load += new System.EventHandler(this.Frm_edit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox locked_part_complete_date;
        private System.Windows.Forms.Label lbl_title;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox locked_time_for_part;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox locked_operation;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox locked_name;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox locked_part_percent_complete;
        private System.Windows.Forms.TextBox txt_part_percent_complete;
        private System.Windows.Forms.TextBox txt_name;
        private System.Windows.Forms.TextBox txt_operation;
        private System.Windows.Forms.TextBox txt_time_for_part;
        private System.Windows.Forms.TextBox txt_part_complete_date;
        private System.Windows.Forms.Button btn_close;
        private System.Windows.Forms.DateTimePicker dte_date;
        private System.Windows.Forms.DateTimePicker dte_time;
    }
}