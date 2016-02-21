namespace ETL
{
    partial class Client
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.portTB = new System.Windows.Forms.TextBox();
            this.IPaddressTB = new System.Windows.Forms.TextBox();
            this.transferData = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.sourcePassword = new System.Windows.Forms.TextBox();
            this.sourceUsername = new System.Windows.Forms.TextBox();
            this.sourceDbName = new System.Windows.Forms.TextBox();
            this.sourceServer = new System.Windows.Forms.TextBox();
            this.testSource = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.destinationPassword = new System.Windows.Forms.TextBox();
            this.destinationUname = new System.Windows.Forms.TextBox();
            this.destinationDbName = new System.Windows.Forms.TextBox();
            this.destServer = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.testDestination = new System.Windows.Forms.Button();
            this.dataTB = new System.Windows.Forms.RichTextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(61, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Source ( MySQL )";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(64, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(138, 16);
            this.label2.TabIndex = 0;
            this.label2.Text = "Destination ( MSSQL )";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(65, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 16);
            this.label3.TabIndex = 1;
            this.label3.Text = "Connection";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(9, 88);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 16);
            this.label4.TabIndex = 2;
            this.label4.Text = "IPAddress";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(11, 147);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 16);
            this.label5.TabIndex = 2;
            this.label5.Text = "Port";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DarkTurquoise;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.portTB);
            this.panel1.Controls.Add(this.IPaddressTB);
            this.panel1.Controls.Add(this.transferData);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Location = new System.Drawing.Point(1, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(243, 331);
            this.panel1.TabIndex = 4;
            // 
            // portTB
            // 
            this.portTB.Location = new System.Drawing.Point(105, 146);
            this.portTB.Name = "portTB";
            this.portTB.Size = new System.Drawing.Size(100, 20);
            this.portTB.TabIndex = 5;
            // 
            // IPaddressTB
            // 
            this.IPaddressTB.Location = new System.Drawing.Point(105, 84);
            this.IPaddressTB.Name = "IPaddressTB";
            this.IPaddressTB.Size = new System.Drawing.Size(100, 20);
            this.IPaddressTB.TabIndex = 5;
            // 
            // transferData
            // 
            this.transferData.Location = new System.Drawing.Point(86, 250);
            this.transferData.Name = "transferData";
            this.transferData.Size = new System.Drawing.Size(79, 25);
            this.transferData.TabIndex = 4;
            this.transferData.Text = "Transfer";
            this.transferData.UseVisualStyleBackColor = true;
            this.transferData.Click += new System.EventHandler(this.transferData_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(3, 122);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(105, 16);
            this.label6.TabIndex = 5;
            this.label6.Text = "DatabaseName";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(7, 212);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(68, 16);
            this.label7.TabIndex = 5;
            this.label7.Text = "Password";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(8, 168);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(71, 16);
            this.label8.TabIndex = 5;
            this.label8.Text = "Username";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(9, 126);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(105, 16);
            this.label9.TabIndex = 5;
            this.label9.Text = "DatabaseName";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(9, 212);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(68, 16);
            this.label10.TabIndex = 5;
            this.label10.Text = "Password";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(9, 171);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(71, 16);
            this.label11.TabIndex = 5;
            this.label11.Text = "Username";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.sourcePassword);
            this.panel2.Controls.Add(this.sourceUsername);
            this.panel2.Controls.Add(this.sourceDbName);
            this.panel2.Controls.Add(this.sourceServer);
            this.panel2.Controls.Add(this.testSource);
            this.panel2.Controls.Add(this.label13);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Location = new System.Drawing.Point(241, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(253, 331);
            this.panel2.TabIndex = 6;
            // 
            // sourcePassword
            // 
            this.sourcePassword.Location = new System.Drawing.Point(129, 208);
            this.sourcePassword.Name = "sourcePassword";
            this.sourcePassword.Size = new System.Drawing.Size(100, 20);
            this.sourcePassword.TabIndex = 5;
            // 
            // sourceUsername
            // 
            this.sourceUsername.Location = new System.Drawing.Point(129, 164);
            this.sourceUsername.Name = "sourceUsername";
            this.sourceUsername.Size = new System.Drawing.Size(100, 20);
            this.sourceUsername.TabIndex = 5;
            // 
            // sourceDbName
            // 
            this.sourceDbName.Location = new System.Drawing.Point(129, 122);
            this.sourceDbName.Name = "sourceDbName";
            this.sourceDbName.Size = new System.Drawing.Size(100, 20);
            this.sourceDbName.TabIndex = 5;
            // 
            // sourceServer
            // 
            this.sourceServer.Location = new System.Drawing.Point(129, 80);
            this.sourceServer.Name = "sourceServer";
            this.sourceServer.Size = new System.Drawing.Size(100, 20);
            this.sourceServer.TabIndex = 5;
            // 
            // testSource
            // 
            this.testSource.Location = new System.Drawing.Point(92, 250);
            this.testSource.Name = "testSource";
            this.testSource.Size = new System.Drawing.Size(90, 25);
            this.testSource.TabIndex = 6;
            this.testSource.Text = "Check";
            this.testSource.UseVisualStyleBackColor = true;
            this.testSource.Click += new System.EventHandler(this.testSource_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(7, 84);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(48, 16);
            this.label13.TabIndex = 7;
            this.label13.Text = "Server";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.MistyRose;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.destinationPassword);
            this.panel3.Controls.Add(this.destinationUname);
            this.panel3.Controls.Add(this.destinationDbName);
            this.panel3.Controls.Add(this.destServer);
            this.panel3.Controls.Add(this.label12);
            this.panel3.Controls.Add(this.testDestination);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.label9);
            this.panel3.Controls.Add(this.label11);
            this.panel3.Controls.Add(this.label10);
            this.panel3.Location = new System.Drawing.Point(490, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(260, 331);
            this.panel3.TabIndex = 7;
            // 
            // destinationPassword
            // 
            this.destinationPassword.Location = new System.Drawing.Point(137, 211);
            this.destinationPassword.Name = "destinationPassword";
            this.destinationPassword.Size = new System.Drawing.Size(100, 20);
            this.destinationPassword.TabIndex = 5;
            // 
            // destinationUname
            // 
            this.destinationUname.Location = new System.Drawing.Point(137, 171);
            this.destinationUname.Name = "destinationUname";
            this.destinationUname.Size = new System.Drawing.Size(100, 20);
            this.destinationUname.TabIndex = 5;
            // 
            // destinationDbName
            // 
            this.destinationDbName.Location = new System.Drawing.Point(137, 126);
            this.destinationDbName.Name = "destinationDbName";
            this.destinationDbName.Size = new System.Drawing.Size(100, 20);
            this.destinationDbName.TabIndex = 5;
            // 
            // destServer
            // 
            this.destServer.Location = new System.Drawing.Point(137, 80);
            this.destServer.Name = "destServer";
            this.destServer.Size = new System.Drawing.Size(100, 20);
            this.destServer.TabIndex = 5;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(9, 87);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(48, 16);
            this.label12.TabIndex = 7;
            this.label12.Text = "Server";
            // 
            // testDestination
            // 
            this.testDestination.Location = new System.Drawing.Point(101, 254);
            this.testDestination.Name = "testDestination";
            this.testDestination.Size = new System.Drawing.Size(84, 21);
            this.testDestination.TabIndex = 6;
            this.testDestination.Text = "Check";
            this.testDestination.UseVisualStyleBackColor = true;
            this.testDestination.Click += new System.EventHandler(this.testDestination_Click);
            // 
            // dataTB
            // 
            this.dataTB.Location = new System.Drawing.Point(30, 391);
            this.dataTB.Name = "dataTB";
            this.dataTB.Size = new System.Drawing.Size(708, 129);
            this.dataTB.TabIndex = 8;
            this.dataTB.Text = "";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.Blue;
            this.label14.Location = new System.Drawing.Point(294, 354);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(130, 20);
            this.label14.TabIndex = 9;
            this.label14.Text = "Actions going On";
            // 
            // Client
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(750, 532);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.dataTB);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "Client";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Client_FormClosed);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button transferData;
        private System.Windows.Forms.Button testSource;
        private System.Windows.Forms.Button testDestination;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.RichTextBox dataTB;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox portTB;
        private System.Windows.Forms.TextBox IPaddressTB;
        private System.Windows.Forms.TextBox sourcePassword;
        private System.Windows.Forms.TextBox sourceUsername;
        private System.Windows.Forms.TextBox sourceDbName;
        private System.Windows.Forms.TextBox sourceServer;
        private System.Windows.Forms.TextBox destinationPassword;
        private System.Windows.Forms.TextBox destinationUname;
        private System.Windows.Forms.TextBox destinationDbName;
        private System.Windows.Forms.TextBox destServer;
    }
}

