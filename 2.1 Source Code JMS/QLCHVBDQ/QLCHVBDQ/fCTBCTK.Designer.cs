namespace QLCHVBDQ
{
    partial class fCTBCTK
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.line1 = new System.Windows.Forms.Label();
            this.text1 = new System.Windows.Forms.Label();
            this.textThemCTPDV = new System.Windows.Forms.Label();
            this.dtgvChiTietBCTK = new System.Windows.Forms.DataGridView();
            this.textThang = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textNam = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvChiTietBCTK)).BeginInit();
            this.SuspendLayout();
            // 
            // line1
            // 
            this.line1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.line1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.line1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.line1.Location = new System.Drawing.Point(-39, 92);
            this.line1.Margin = new System.Windows.Forms.Padding(0);
            this.line1.Name = "line1";
            this.line1.Size = new System.Drawing.Size(1493, 2);
            this.line1.TabIndex = 100;
            // 
            // text1
            // 
            this.text1.AutoSize = true;
            this.text1.Font = new System.Drawing.Font("Montserrat", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.text1.ForeColor = System.Drawing.Color.Black;
            this.text1.Location = new System.Drawing.Point(592, 122);
            this.text1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.text1.Name = "text1";
            this.text1.Size = new System.Drawing.Size(122, 41);
            this.text1.TabIndex = 99;
            this.text1.Text = "Tháng ";
            this.text1.Click += new System.EventHandler(this.text1_Click);
            // 
            // textThemCTPDV
            // 
            this.textThemCTPDV.AutoSize = true;
            this.textThemCTPDV.Font = new System.Drawing.Font("Montserrat Black", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textThemCTPDV.Location = new System.Drawing.Point(595, 31);
            this.textThemCTPDV.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.textThemCTPDV.Name = "textThemCTPDV";
            this.textThemCTPDV.Size = new System.Drawing.Size(378, 55);
            this.textThemCTPDV.TabIndex = 97;
            this.textThemCTPDV.Text = "Báo cáo tồn kho";
            this.textThemCTPDV.Click += new System.EventHandler(this.textThemCTPDV_Click);
            // 
            // dtgvChiTietBCTK
            // 
            this.dtgvChiTietBCTK.AllowUserToAddRows = false;
            this.dtgvChiTietBCTK.AllowUserToDeleteRows = false;
            this.dtgvChiTietBCTK.AllowUserToOrderColumns = true;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dtgvChiTietBCTK.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dtgvChiTietBCTK.BackgroundColor = System.Drawing.Color.White;
            this.dtgvChiTietBCTK.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dtgvChiTietBCTK.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dtgvChiTietBCTK.ColumnHeadersHeight = 36;
            this.dtgvChiTietBCTK.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dtgvChiTietBCTK.DefaultCellStyle = dataGridViewCellStyle3;
            this.dtgvChiTietBCTK.Location = new System.Drawing.Point(16, 204);
            this.dtgvChiTietBCTK.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dtgvChiTietBCTK.Name = "dtgvChiTietBCTK";
            this.dtgvChiTietBCTK.ReadOnly = true;
            this.dtgvChiTietBCTK.RowHeadersVisible = false;
            this.dtgvChiTietBCTK.RowHeadersWidth = 51;
            this.dtgvChiTietBCTK.RowTemplate.Height = 36;
            this.dtgvChiTietBCTK.Size = new System.Drawing.Size(1392, 667);
            this.dtgvChiTietBCTK.TabIndex = 109;
            this.dtgvChiTietBCTK.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgvChiTietBCTK_CellContentClick);
            // 
            // textThang
            // 
            this.textThang.AutoSize = true;
            this.textThang.Font = new System.Drawing.Font("Montserrat", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textThang.ForeColor = System.Drawing.Color.Black;
            this.textThang.Location = new System.Drawing.Point(711, 122);
            this.textThang.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.textThang.Name = "textThang";
            this.textThang.Size = new System.Drawing.Size(29, 41);
            this.textThang.TabIndex = 110;
            this.textThang.Text = "1";
            this.textThang.Click += new System.EventHandler(this.textThang_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Montserrat", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(768, 122);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 41);
            this.label1.TabIndex = 111;
            this.label1.Text = "Năm";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // textNam
            // 
            this.textNam.AutoSize = true;
            this.textNam.Font = new System.Drawing.Font("Montserrat", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textNam.ForeColor = System.Drawing.Color.Black;
            this.textNam.Location = new System.Drawing.Point(861, 122);
            this.textNam.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.textNam.Name = "textNam";
            this.textNam.Size = new System.Drawing.Size(89, 41);
            this.textNam.TabIndex = 112;
            this.textNam.Text = "2023";
            // 
            // fCTBCTK
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1423, 886);
            this.Controls.Add(this.textNam);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textThang);
            this.Controls.Add(this.dtgvChiTietBCTK);
            this.Controls.Add(this.line1);
            this.Controls.Add(this.text1);
            this.Controls.Add(this.textThemCTPDV);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "fCTBCTK";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chi tiết phiếu bán hàng";
            ((System.ComponentModel.ISupportInitialize)(this.dtgvChiTietBCTK)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label line1;
        private System.Windows.Forms.Label text1;
        private System.Windows.Forms.Label textThemCTPDV;
        private System.Windows.Forms.DataGridView dtgvChiTietBCTK;
        private System.Windows.Forms.Label textThang;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label textNam;
    }
}