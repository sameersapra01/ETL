namespace ETLServer
{
    partial class Server
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
            this.panel3 = new System.Windows.Forms.Panel();
            this.checkDestination = new System.Windows.Forms.Button();
            this.portToConnect = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.startServer = new System.Windows.Forms.Button();
            this.destinationPassword = new System.Windows.Forms.TextBox();
            this.destinationUname = new System.Windows.Forms.TextBox();
            this.destinationDbName = new System.Windows.Forms.TextBox();
            this.destServer = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.serverDataTB = new System.Windows.Forms.RichTextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.MistyRose;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.checkDestination);
            this.panel3.Controls.Add(this.portToConnect);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.startServer);
            this.panel3.Controls.Add(this.destinationPassword);
            this.panel3.Controls.Add(this.destinationUname);
            this.panel3.Controls.Add(this.destinationDbName);
            this.panel3.Controls.Add(this.destServer);
            this.panel3.Controls.Add(this.label12);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.label9);
            this.panel3.Controls.Add(this.label11);
            this.panel3.Controls.Add(this.label10);
            this.panel3.Location = new System.Drawing.Point(-3, 1);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(370, 438);
            this.panel3.TabIndex = 8;
            // 
            // checkDestination
            // 
            this.checkDestination.Location = new System.Drawing.Point(128, 240);
            this.checkDestination.Name = "checkDestination";
            this.checkDestination.Size = new System.Drawing.Size(102, 34);
            this.checkDestination.TabIndex = 10;
            this.checkDestination.Text = "Check";
            this.checkDestination.UseVisualStyleBackColor = true;
            this.checkDestination.Click += new System.EventHandler(this.checkDestination_Click);
            // 
            // portToConnect
            // 
            this.portToConnect.Location = new System.Drawing.Point(204, 316);
            this.portToConnect.Name = "portToConnect";
            this.portToConnect.Size = new System.Drawing.Size(100, 20);
            this.portToConnect.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(76, 316);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 16);
            this.label3.TabIndex = 8;
            this.label3.Text = "Port";
            // 
            // startServer
            // 
            this.startServer.Location = new System.Drawing.Point(128, 376);
            this.startServer.Name = "startServer";
            this.startServer.Size = new System.Drawing.Size(102, 35);
            this.startServer.TabIndex = 0;
            this.startServer.Text = "Start Server";
            this.startServer.UseVisualStyleBackColor = true;
            this.startServer.Click += new System.EventHandler(this.startServer_Click);
            // 
            // destinationPassword
            // 
            this.destinationPassword.Location = new System.Drawing.Point(204, 192);
            this.destinationPassword.Name = "destinationPassword";
            this.destinationPassword.Size = new System.Drawing.Size(100, 20);
            this.destinationPassword.TabIndex = 5;
            // 
            // destinationUname
            // 
            this.destinationUname.Location = new System.Drawing.Point(204, 147);
            this.destinationUname.Name = "destinationUname";
            this.destinationUname.Size = new System.Drawing.Size(100, 20);
            this.destinationUname.TabIndex = 5;
            // 
            // destinationDbName
            // 
            this.destinationDbName.Location = new System.Drawing.Point(204, 103);
            this.destinationDbName.Name = "destinationDbName";
            this.destinationDbName.Size = new System.Drawing.Size(100, 20);
            this.destinationDbName.TabIndex = 5;
            // 
            // destServer
            // 
            this.destServer.Location = new System.Drawing.Point(204, 67);
            this.destServer.Name = "destServer";
            this.destServer.Size = new System.Drawing.Size(100, 20);
            this.destServer.TabIndex = 5;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(76, 67);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(48, 16);
            this.label12.TabIndex = 7;
            this.label12.Text = "Server";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(131, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(138, 16);
            this.label2.TabIndex = 0;
            this.label2.Text = "Destination ( MSSQL )";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(76, 107);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(105, 16);
            this.label9.TabIndex = 5;
            this.label9.Text = "DatabaseName";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(76, 151);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(71, 16);
            this.label11.TabIndex = 5;
            this.label11.Text = "Username";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(76, 196);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(68, 16);
            this.label10.TabIndex = 5;
            this.label10.Text = "Password";
            // 
            // serverDataTB
            // 
            this.serverDataTB.Location = new System.Drawing.Point(3, 68);
            this.serverDataTB.Name = "serverDataTB";
            this.serverDataTB.Size = new System.Drawing.Size(360, 358);
            this.serverDataTB.TabIndex = 1;
            this.serverDataTB.Text = "";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.serverDataTB);
            this.panel2.Location = new System.Drawing.Point(370, 1);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(370, 438);
            this.panel2.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(158, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Operations";
            // 
            // Server
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(740, 439);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Name = "Server";
            this.Text = "Form1";
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RichTextBox serverDataTB;
        private System.Windows.Forms.Button startServer;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox destinationPassword;
        private System.Windows.Forms.TextBox destinationUname;
        private System.Windows.Forms.TextBox destinationDbName;
        private System.Windows.Forms.TextBox destServer;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox portToConnect;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button checkDestination;
    }
}

