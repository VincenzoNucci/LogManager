namespace LogManagerBenchmark
{
    partial class Form1
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
            this.gboxMongoDB = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbxDatabase = new System.Windows.Forms.ComboBox();
            this.btnConnection = new System.Windows.Forms.Button();
            this.txtConnectionString = new System.Windows.Forms.TextBox();
            this.lblCollection = new System.Windows.Forms.Label();
            this.lblDatabase = new System.Windows.Forms.Label();
            this.cBoxCollection = new System.Windows.Forms.ComboBox();
            this.cBoxDatabase = new System.Windows.Forms.ComboBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.cartesianChart1 = new LiveCharts.WinForms.CartesianChart();
            this.nudThreads = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnExecute = new System.Windows.Forms.Button();
            this.cbxTraceClass = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.nudBufferSize = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.nudBuffers = new System.Windows.Forms.NumericUpDown();
            this.nudLogs = new System.Windows.Forms.NumericUpDown();
            this.console = new System.Windows.Forms.TextBox();
            this.gboxMongoDB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudThreads)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudBufferSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBuffers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLogs)).BeginInit();
            this.SuspendLayout();
            // 
            // gboxMongoDB
            // 
            this.gboxMongoDB.Controls.Add(this.label4);
            this.gboxMongoDB.Controls.Add(this.cbxDatabase);
            this.gboxMongoDB.Controls.Add(this.btnConnection);
            this.gboxMongoDB.Controls.Add(this.txtConnectionString);
            this.gboxMongoDB.Controls.Add(this.lblCollection);
            this.gboxMongoDB.Controls.Add(this.lblDatabase);
            this.gboxMongoDB.Controls.Add(this.cBoxCollection);
            this.gboxMongoDB.Controls.Add(this.cBoxDatabase);
            this.gboxMongoDB.Controls.Add(this.lblStatus);
            this.gboxMongoDB.Location = new System.Drawing.Point(578, 12);
            this.gboxMongoDB.Name = "gboxMongoDB";
            this.gboxMongoDB.Size = new System.Drawing.Size(210, 220);
            this.gboxMongoDB.TabIndex = 0;
            this.gboxMongoDB.TabStop = false;
            this.gboxMongoDB.Text = "DataBase Connection";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 68);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Database";
            // 
            // cbxDatabase
            // 
            this.cbxDatabase.FormattingEnabled = true;
            this.cbxDatabase.Items.AddRange(new object[] {
            "MongoDB",
            "Internal LiteDb"});
            this.cbxDatabase.Location = new System.Drawing.Point(94, 65);
            this.cbxDatabase.Name = "cbxDatabase";
            this.cbxDatabase.Size = new System.Drawing.Size(100, 21);
            this.cbxDatabase.TabIndex = 8;
            // 
            // btnConnection
            // 
            this.btnConnection.Location = new System.Drawing.Point(9, 99);
            this.btnConnection.Name = "btnConnection";
            this.btnConnection.Size = new System.Drawing.Size(75, 23);
            this.btnConnection.TabIndex = 7;
            this.btnConnection.Text = "Connection";
            this.btnConnection.UseVisualStyleBackColor = true;
            this.btnConnection.Click += new System.EventHandler(this.btnConnection_Click);
            // 
            // txtConnectionString
            // 
            this.txtConnectionString.Location = new System.Drawing.Point(94, 102);
            this.txtConnectionString.Name = "txtConnectionString";
            this.txtConnectionString.Size = new System.Drawing.Size(100, 20);
            this.txtConnectionString.TabIndex = 6;
            // 
            // lblCollection
            // 
            this.lblCollection.AutoSize = true;
            this.lblCollection.Location = new System.Drawing.Point(14, 176);
            this.lblCollection.Name = "lblCollection";
            this.lblCollection.Size = new System.Drawing.Size(56, 13);
            this.lblCollection.TabIndex = 4;
            this.lblCollection.Text = "Collection:";
            this.lblCollection.Visible = false;
            // 
            // lblDatabase
            // 
            this.lblDatabase.AutoSize = true;
            this.lblDatabase.Location = new System.Drawing.Point(11, 138);
            this.lblDatabase.Name = "lblDatabase";
            this.lblDatabase.Size = new System.Drawing.Size(56, 13);
            this.lblDatabase.TabIndex = 3;
            this.lblDatabase.Text = "Database:";
            this.lblDatabase.Visible = false;
            // 
            // cBoxCollection
            // 
            this.cBoxCollection.FormattingEnabled = true;
            this.cBoxCollection.Location = new System.Drawing.Point(73, 176);
            this.cBoxCollection.Name = "cBoxCollection";
            this.cBoxCollection.Size = new System.Drawing.Size(121, 21);
            this.cBoxCollection.TabIndex = 2;
            this.cBoxCollection.Visible = false;
            // 
            // cBoxDatabase
            // 
            this.cBoxDatabase.FormattingEnabled = true;
            this.cBoxDatabase.Location = new System.Drawing.Point(73, 138);
            this.cBoxDatabase.Name = "cBoxDatabase";
            this.cBoxDatabase.Size = new System.Drawing.Size(121, 21);
            this.cBoxDatabase.TabIndex = 1;
            this.cBoxDatabase.Visible = false;
            this.cBoxDatabase.SelectedIndexChanged += new System.EventHandler(this.cBoxDatabase_SelectedIndexChanged);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(6, 26);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(40, 13);
            this.lblStatus.TabIndex = 0;
            this.lblStatus.Text = "Status:";
            // 
            // cartesianChart1
            // 
            this.cartesianChart1.Location = new System.Drawing.Point(13, 13);
            this.cartesianChart1.Name = "cartesianChart1";
            this.cartesianChart1.Size = new System.Drawing.Size(559, 319);
            this.cartesianChart1.TabIndex = 1;
            this.cartesianChart1.Text = "cartesianChart1";
            // 
            // nudThreads
            // 
            this.nudThreads.Location = new System.Drawing.Point(103, 52);
            this.nudThreads.Name = "nudThreads";
            this.nudThreads.Size = new System.Drawing.Size(67, 20);
            this.nudThreads.TabIndex = 2;
            this.nudThreads.Value = new decimal(new int[] {
            64,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "N. Threads";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 89);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "N. Logs";
            // 
            // btnExecute
            // 
            this.btnExecute.Location = new System.Drawing.Point(636, 415);
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.Size = new System.Drawing.Size(75, 23);
            this.btnExecute.TabIndex = 6;
            this.btnExecute.Text = "Execute";
            this.btnExecute.UseVisualStyleBackColor = true;
            this.btnExecute.Click += new System.EventHandler(this.btnExecute_Click);
            // 
            // cbxTraceClass
            // 
            this.cbxTraceClass.FormattingEnabled = true;
            this.cbxTraceClass.Items.AddRange(new object[] {
            "Trace",
            "ConcurrentTrace",
            "TimedConcurrentTrace"});
            this.cbxTraceClass.Location = new System.Drawing.Point(77, 18);
            this.cbxTraceClass.Name = "cbxTraceClass";
            this.cbxTraceClass.Size = new System.Drawing.Size(121, 21);
            this.cbxTraceClass.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Trace Class";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.nudBufferSize);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.nudBuffers);
            this.groupBox1.Controls.Add(this.nudLogs);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cbxTraceClass);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.nudThreads);
            this.groupBox1.Location = new System.Drawing.Point(579, 238);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(209, 171);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Settings";
            // 
            // nudBufferSize
            // 
            this.nudBufferSize.Location = new System.Drawing.Point(103, 145);
            this.nudBufferSize.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudBufferSize.Name = "nudBufferSize";
            this.nudBufferSize.Size = new System.Drawing.Size(67, 20);
            this.nudBufferSize.TabIndex = 13;
            this.nudBufferSize.Value = new decimal(new int[] {
            200,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(23, 147);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Buffer size";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(23, 112);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(54, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "N. Buffers";
            // 
            // nudBuffers
            // 
            this.nudBuffers.Location = new System.Drawing.Point(103, 110);
            this.nudBuffers.Name = "nudBuffers";
            this.nudBuffers.Size = new System.Drawing.Size(67, 20);
            this.nudBuffers.TabIndex = 10;
            this.nudBuffers.Value = new decimal(new int[] {
            64,
            0,
            0,
            0});
            // 
            // nudLogs
            // 
            this.nudLogs.Location = new System.Drawing.Point(103, 87);
            this.nudLogs.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudLogs.Name = "nudLogs";
            this.nudLogs.Size = new System.Drawing.Size(67, 20);
            this.nudLogs.TabIndex = 9;
            this.nudLogs.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // console
            // 
            this.console.Location = new System.Drawing.Point(13, 338);
            this.console.Multiline = true;
            this.console.Name = "console";
            this.console.Size = new System.Drawing.Size(559, 100);
            this.console.TabIndex = 10;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.console);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnExecute);
            this.Controls.Add(this.cartesianChart1);
            this.Controls.Add(this.gboxMongoDB);
            this.Name = "Form1";
            this.Text = "Form1";
            this.gboxMongoDB.ResumeLayout(false);
            this.gboxMongoDB.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudThreads)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudBufferSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBuffers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLogs)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gboxMongoDB;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblCollection;
        private System.Windows.Forms.Label lblDatabase;
        private System.Windows.Forms.ComboBox cBoxCollection;
        private System.Windows.Forms.ComboBox cBoxDatabase;
        private System.Windows.Forms.TextBox txtConnectionString;
        private System.Windows.Forms.Button btnConnection;
        private LiveCharts.WinForms.CartesianChart cartesianChart1;
        private System.Windows.Forms.NumericUpDown nudThreads;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnExecute;
        private System.Windows.Forms.ComboBox cbxTraceClass;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown nudLogs;
        private System.Windows.Forms.TextBox console;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbxDatabase;
        private System.Windows.Forms.NumericUpDown nudBufferSize;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown nudBuffers;
    }
}