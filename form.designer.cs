namespace osusb1 {
	partial class form {
	/// <summary>
	/// Required designer variable.
	/// </summary>
	private System.ComponentModel.IContainer components = null;

	/// <summary>
	/// Clean up any resources being used.
	/// </summary>
	/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
	protected override void Dispose(bool disposing) {
		if (disposing && (components != null)) {
			components.Dispose();
		}
		base.Dispose(disposing);
	}

	#region Windows Form Designer generated code

	/// <summary>
	/// Required method for Designer support - do not modify
	/// the contents of this method with the code editor.
	/// </summary>
	private void InitializeComponent() {
		this.components = new System.ComponentModel.Container();
		this.panel1 = new System.Windows.Forms.PictureBox();
		this.nuptime = new System.Windows.Forms.NumericUpDown();
		this.button1 = new System.Windows.Forms.Button();
		this.timer1 = new System.Windows.Forms.Timer(this.components);
		this.btnexport = new System.Windows.Forms.Button();
		this.trackBar1 = new System.Windows.Forms.TrackBar();
		this.trackBar2 = new System.Windows.Forms.TrackBar();
		this.trackBar3 = new System.Windows.Forms.TrackBar();
		this.trackBar4 = new System.Windows.Forms.TrackBar();
		this.trackBar5 = new System.Windows.Forms.TrackBar();
		this.trackBar6 = new System.Windows.Forms.TrackBar();
		this.trackBar7 = new System.Windows.Forms.TrackBar();
		this.udata0 = new System.Windows.Forms.Label();
		this.udata1 = new System.Windows.Forms.Label();
		this.udata2 = new System.Windows.Forms.Label();
		this.udata3 = new System.Windows.Forms.Label();
		this.udata4 = new System.Windows.Forms.Label();
		this.udata5 = new System.Windows.Forms.Label();
		this.udata6 = new System.Windows.Forms.Label();
		this.chkwidescreen = new System.Windows.Forms.CheckBox();
		this.chkComments = new System.Windows.Forms.CheckBox();
		this.chkPhantom = new System.Windows.Forms.CheckBox();
		this.button2 = new System.Windows.Forms.Button();
		this.listBox1 = new System.Windows.Forms.ListBox();
		this.listBox2 = new System.Windows.Forms.ListBox();
		this.listBox3 = new System.Windows.Forms.ListBox();
		this.textBox1 = new System.Windows.Forms.TextBox();
		this.textBox2 = new System.Windows.Forms.TextBox();
		this.textBox3 = new System.Windows.Forms.TextBox();
		this.abutton1 = new System.Windows.Forms.Button();
		this.abutton2 = new System.Windows.Forms.Button();
		this.abutton3 = new System.Windows.Forms.Button();
		this.abuttonsave = new System.Windows.Forms.Button();
		this.pnlBezs = new System.Windows.Forms.Panel();
		this.comboBox1 = new System.Windows.Forms.ComboBox();
		this.label1 = new System.Windows.Forms.Label();
		this.button3 = new System.Windows.Forms.Button();
		((System.ComponentModel.ISupportInitialize)(this.panel1)).BeginInit();
		((System.ComponentModel.ISupportInitialize)(this.nuptime)).BeginInit();
		((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
		((System.ComponentModel.ISupportInitialize)(this.trackBar2)).BeginInit();
		((System.ComponentModel.ISupportInitialize)(this.trackBar3)).BeginInit();
		((System.ComponentModel.ISupportInitialize)(this.trackBar4)).BeginInit();
		((System.ComponentModel.ISupportInitialize)(this.trackBar5)).BeginInit();
		((System.ComponentModel.ISupportInitialize)(this.trackBar6)).BeginInit();
		((System.ComponentModel.ISupportInitialize)(this.trackBar7)).BeginInit();
		this.SuspendLayout();
		// 
		// panel1
		// 
		this.panel1.Location = new System.Drawing.Point(12, 12);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(854, 480);
		this.panel1.TabIndex = 0;
		this.panel1.TabStop = false;
		this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseMove);
		this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
		this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
		this.panel1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseUp);
		// 
		// nuptime
		// 
		this.nuptime.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
		this.nuptime.Location = new System.Drawing.Point(12, 472);
		this.nuptime.Maximum = new decimal(new int[] {
            500000,
            0,
            0,
            0});
		this.nuptime.Name = "nuptime";
		this.nuptime.Size = new System.Drawing.Size(105, 20);
		this.nuptime.TabIndex = 1;
		this.nuptime.Value = new decimal(new int[] {
            121000,
            0,
            0,
            0});
		this.nuptime.ValueChanged += new System.EventHandler(this.nuptime_ValueChanged);
		// 
		// button1
		// 
		this.button1.Location = new System.Drawing.Point(577, 469);
		this.button1.Name = "button1";
		this.button1.Size = new System.Drawing.Size(75, 23);
		this.button1.TabIndex = 2;
		this.button1.Text = "auto";
		this.button1.UseVisualStyleBackColor = true;
		this.button1.Click += new System.EventHandler(this.button1_Click);
		// 
		// timer1
		// 
		this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
		// 
		// btnexport
		// 
		this.btnexport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
		this.btnexport.Location = new System.Drawing.Point(1424, 85);
		this.btnexport.Name = "btnexport";
		this.btnexport.Size = new System.Drawing.Size(120, 23);
		this.btnexport.TabIndex = 10;
		this.btnexport.Text = "export";
		this.btnexport.UseVisualStyleBackColor = true;
		this.btnexport.Click += new System.EventHandler(this.UI_ExportRequest);
		// 
		// trackBar1
		// 
		this.trackBar1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
		this.trackBar1.Location = new System.Drawing.Point(1363, 125);
		this.trackBar1.Maximum = 20;
		this.trackBar1.Minimum = -20;
		this.trackBar1.Name = "trackBar1";
		this.trackBar1.Size = new System.Drawing.Size(181, 45);
		this.trackBar1.TabIndex = 14;
		this.trackBar1.Tag = "0";
		// 
		// trackBar2
		// 
		this.trackBar2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
		this.trackBar2.Location = new System.Drawing.Point(1363, 173);
		this.trackBar2.Maximum = 20;
		this.trackBar2.Minimum = -20;
		this.trackBar2.Name = "trackBar2";
		this.trackBar2.Size = new System.Drawing.Size(181, 45);
		this.trackBar2.TabIndex = 15;
		this.trackBar2.Tag = "1";
		// 
		// trackBar3
		// 
		this.trackBar3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
		this.trackBar3.Location = new System.Drawing.Point(1363, 221);
		this.trackBar3.Maximum = 20;
		this.trackBar3.Minimum = -20;
		this.trackBar3.Name = "trackBar3";
		this.trackBar3.Size = new System.Drawing.Size(181, 45);
		this.trackBar3.TabIndex = 16;
		this.trackBar3.Tag = "2";
		// 
		// trackBar4
		// 
		this.trackBar4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
		this.trackBar4.Location = new System.Drawing.Point(1363, 269);
		this.trackBar4.Maximum = 20;
		this.trackBar4.Minimum = -20;
		this.trackBar4.Name = "trackBar4";
		this.trackBar4.Size = new System.Drawing.Size(181, 45);
		this.trackBar4.TabIndex = 17;
		this.trackBar4.Tag = "3";
		// 
		// trackBar5
		// 
		this.trackBar5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
		this.trackBar5.Location = new System.Drawing.Point(1363, 317);
		this.trackBar5.Maximum = 20;
		this.trackBar5.Minimum = -20;
		this.trackBar5.Name = "trackBar5";
		this.trackBar5.Size = new System.Drawing.Size(181, 45);
		this.trackBar5.TabIndex = 18;
		this.trackBar5.Tag = "4";
		// 
		// trackBar6
		// 
		this.trackBar6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
		this.trackBar6.Location = new System.Drawing.Point(1363, 365);
		this.trackBar6.Maximum = 20;
		this.trackBar6.Minimum = -20;
		this.trackBar6.Name = "trackBar6";
		this.trackBar6.Size = new System.Drawing.Size(181, 45);
		this.trackBar6.TabIndex = 19;
		this.trackBar6.Tag = "5";
		// 
		// trackBar7
		// 
		this.trackBar7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
		this.trackBar7.Location = new System.Drawing.Point(1363, 413);
		this.trackBar7.Maximum = 20;
		this.trackBar7.Minimum = -20;
		this.trackBar7.Name = "trackBar7";
		this.trackBar7.Size = new System.Drawing.Size(181, 45);
		this.trackBar7.TabIndex = 20;
		this.trackBar7.Tag = "6";
		// 
		// udata0
		// 
		this.udata0.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
		this.udata0.AutoSize = true;
		this.udata0.Location = new System.Drawing.Point(1520, 154);
		this.udata0.Name = "udata0";
		this.udata0.Size = new System.Drawing.Size(13, 13);
		this.udata0.TabIndex = 22;
		this.udata0.Text = "0";
		this.udata0.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		// 
		// udata1
		// 
		this.udata1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
		this.udata1.AutoSize = true;
		this.udata1.Location = new System.Drawing.Point(1520, 205);
		this.udata1.Name = "udata1";
		this.udata1.Size = new System.Drawing.Size(13, 13);
		this.udata1.TabIndex = 23;
		this.udata1.Text = "0";
		this.udata1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		// 
		// udata2
		// 
		this.udata2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
		this.udata2.AutoSize = true;
		this.udata2.Location = new System.Drawing.Point(1520, 250);
		this.udata2.Name = "udata2";
		this.udata2.Size = new System.Drawing.Size(13, 13);
		this.udata2.TabIndex = 24;
		this.udata2.Text = "0";
		this.udata2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		// 
		// udata3
		// 
		this.udata3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
		this.udata3.AutoSize = true;
		this.udata3.Location = new System.Drawing.Point(1520, 298);
		this.udata3.Name = "udata3";
		this.udata3.Size = new System.Drawing.Size(13, 13);
		this.udata3.TabIndex = 25;
		this.udata3.Text = "0";
		this.udata3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		// 
		// udata4
		// 
		this.udata4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
		this.udata4.AutoSize = true;
		this.udata4.Location = new System.Drawing.Point(1520, 346);
		this.udata4.Name = "udata4";
		this.udata4.Size = new System.Drawing.Size(13, 13);
		this.udata4.TabIndex = 26;
		this.udata4.Text = "0";
		this.udata4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		// 
		// udata5
		// 
		this.udata5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
		this.udata5.AutoSize = true;
		this.udata5.Location = new System.Drawing.Point(1520, 394);
		this.udata5.Name = "udata5";
		this.udata5.Size = new System.Drawing.Size(13, 13);
		this.udata5.TabIndex = 27;
		this.udata5.Text = "0";
		this.udata5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		// 
		// udata6
		// 
		this.udata6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
		this.udata6.AutoSize = true;
		this.udata6.Location = new System.Drawing.Point(1520, 442);
		this.udata6.Name = "udata6";
		this.udata6.Size = new System.Drawing.Size(13, 13);
		this.udata6.TabIndex = 28;
		this.udata6.Text = "0";
		this.udata6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		// 
		// chkwidescreen
		// 
		this.chkwidescreen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
		this.chkwidescreen.AutoSize = true;
		this.chkwidescreen.Checked = true;
		this.chkwidescreen.CheckState = System.Windows.Forms.CheckState.Checked;
		this.chkwidescreen.Location = new System.Drawing.Point(1461, 36);
		this.chkwidescreen.Name = "chkwidescreen";
		this.chkwidescreen.Size = new System.Drawing.Size(83, 17);
		this.chkwidescreen.TabIndex = 30;
		this.chkwidescreen.Text = "Widescreen";
		this.chkwidescreen.UseVisualStyleBackColor = true;
		this.chkwidescreen.CheckedChanged += new System.EventHandler(this.chkwidescreen_CheckedChanged);
		// 
		// chkComments
		// 
		this.chkComments.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
		this.chkComments.AutoSize = true;
		this.chkComments.Location = new System.Drawing.Point(1363, 36);
		this.chkComments.Name = "chkComments";
		this.chkComments.Size = new System.Drawing.Size(75, 17);
		this.chkComments.TabIndex = 31;
		this.chkComments.Text = "Comments";
		this.chkComments.UseVisualStyleBackColor = true;
		// 
		// chkPhantom
		// 
		this.chkPhantom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
		this.chkPhantom.AutoSize = true;
		this.chkPhantom.Checked = true;
		this.chkPhantom.CheckState = System.Windows.Forms.CheckState.Checked;
		this.chkPhantom.Location = new System.Drawing.Point(1461, 13);
		this.chkPhantom.Name = "chkPhantom";
		this.chkPhantom.Size = new System.Drawing.Size(68, 17);
		this.chkPhantom.TabIndex = 32;
		this.chkPhantom.Text = "Phantom";
		this.chkPhantom.UseVisualStyleBackColor = true;
		// 
		// button2
		// 
		this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
		this.button2.Location = new System.Drawing.Point(1363, 85);
		this.button2.Name = "button2";
		this.button2.Size = new System.Drawing.Size(55, 23);
		this.button2.TabIndex = 34;
		this.button2.Text = "min";
		this.button2.UseVisualStyleBackColor = true;
		this.button2.Click += new System.EventHandler(this.button2_Click);
		// 
		// listBox1
		// 
		this.listBox1.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
		this.listBox1.FormattingEnabled = true;
		this.listBox1.ItemHeight = 16;
		this.listBox1.Location = new System.Drawing.Point(872, 12);
		this.listBox1.Name = "listBox1";
		this.listBox1.Size = new System.Drawing.Size(155, 420);
		this.listBox1.TabIndex = 35;
		// 
		// listBox2
		// 
		this.listBox2.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
		this.listBox2.FormattingEnabled = true;
		this.listBox2.ItemHeight = 16;
		this.listBox2.Location = new System.Drawing.Point(1033, 13);
		this.listBox2.Name = "listBox2";
		this.listBox2.Size = new System.Drawing.Size(155, 420);
		this.listBox2.TabIndex = 36;
		// 
		// listBox3
		// 
		this.listBox3.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
		this.listBox3.FormattingEnabled = true;
		this.listBox3.ItemHeight = 16;
		this.listBox3.Location = new System.Drawing.Point(1194, 12);
		this.listBox3.Name = "listBox3";
		this.listBox3.Size = new System.Drawing.Size(155, 420);
		this.listBox3.TabIndex = 37;
		// 
		// textBox1
		// 
		this.textBox1.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
		this.textBox1.Location = new System.Drawing.Point(872, 443);
		this.textBox1.Name = "textBox1";
		this.textBox1.Size = new System.Drawing.Size(155, 22);
		this.textBox1.TabIndex = 38;
		// 
		// textBox2
		// 
		this.textBox2.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
		this.textBox2.Location = new System.Drawing.Point(1033, 443);
		this.textBox2.Name = "textBox2";
		this.textBox2.Size = new System.Drawing.Size(155, 22);
		this.textBox2.TabIndex = 39;
		// 
		// textBox3
		// 
		this.textBox3.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
		this.textBox3.Location = new System.Drawing.Point(1194, 443);
		this.textBox3.Name = "textBox3";
		this.textBox3.Size = new System.Drawing.Size(155, 22);
		this.textBox3.TabIndex = 40;
		// 
		// abutton1
		// 
		this.abutton1.Location = new System.Drawing.Point(872, 469);
		this.abutton1.Name = "abutton1";
		this.abutton1.Size = new System.Drawing.Size(75, 23);
		this.abutton1.TabIndex = 41;
		this.abutton1.Text = "Add";
		this.abutton1.UseVisualStyleBackColor = true;
		// 
		// abutton2
		// 
		this.abutton2.Location = new System.Drawing.Point(1033, 469);
		this.abutton2.Name = "abutton2";
		this.abutton2.Size = new System.Drawing.Size(75, 23);
		this.abutton2.TabIndex = 42;
		this.abutton2.Text = "Add";
		this.abutton2.UseVisualStyleBackColor = true;
		// 
		// abutton3
		// 
		this.abutton3.Location = new System.Drawing.Point(1194, 469);
		this.abutton3.Name = "abutton3";
		this.abutton3.Size = new System.Drawing.Size(75, 23);
		this.abutton3.TabIndex = 43;
		this.abutton3.Text = "Add";
		this.abutton3.UseVisualStyleBackColor = true;
		// 
		// abuttonsave
		// 
		this.abuttonsave.Location = new System.Drawing.Point(1274, 469);
		this.abuttonsave.Name = "abuttonsave";
		this.abuttonsave.Size = new System.Drawing.Size(75, 23);
		this.abuttonsave.TabIndex = 44;
		this.abuttonsave.Text = "SAVE!";
		this.abuttonsave.UseVisualStyleBackColor = true;
		// 
		// pnlBezs
		// 
		this.pnlBezs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
			    | System.Windows.Forms.AnchorStyles.Left)
			    | System.Windows.Forms.AnchorStyles.Right)));
		this.pnlBezs.AutoScroll = true;
		this.pnlBezs.Location = new System.Drawing.Point(1, 498);
		this.pnlBezs.Name = "pnlBezs";
		this.pnlBezs.Size = new System.Drawing.Size(1556, 381);
		this.pnlBezs.TabIndex = 45;
		// 
		// comboBox1
		// 
		this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.comboBox1.FormattingEnabled = true;
		this.comboBox1.Items.AddRange(new object[] {
            "A",
            "B",
            "C"});
		this.comboBox1.Location = new System.Drawing.Point(4, 498);
		this.comboBox1.Name = "comboBox1";
		this.comboBox1.Size = new System.Drawing.Size(62, 21);
		this.comboBox1.TabIndex = 0;
		this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
		// 
		// label1
		// 
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(4, 522);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(89, 52);
		this.label1.TabIndex = 1;
		this.label1.Text = "yes yes these are\r\nnot actually\r\nbeziers, don\'t\r\nkill me";
		// 
		// button3
		// 
		this.button3.Location = new System.Drawing.Point(4, 577);
		this.button3.Name = "button3";
		this.button3.Size = new System.Drawing.Size(53, 23);
		this.button3.TabIndex = 2;
		this.button3.Text = "save";
		this.button3.UseVisualStyleBackColor = true;
		this.button3.Click += new System.EventHandler(this.button3_Click);
		// 
		// form
		// 
		this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
		this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.ClientSize = new System.Drawing.Size(1556, 880);
		this.Controls.Add(this.comboBox1);
		this.Controls.Add(this.label1);
		this.Controls.Add(this.button3);
		this.Controls.Add(this.pnlBezs);
		this.Controls.Add(this.abuttonsave);
		this.Controls.Add(this.abutton3);
		this.Controls.Add(this.abutton2);
		this.Controls.Add(this.abutton1);
		this.Controls.Add(this.textBox3);
		this.Controls.Add(this.textBox2);
		this.Controls.Add(this.textBox1);
		this.Controls.Add(this.listBox3);
		this.Controls.Add(this.listBox2);
		this.Controls.Add(this.listBox1);
		this.Controls.Add(this.button2);
		this.Controls.Add(this.chkPhantom);
		this.Controls.Add(this.chkComments);
		this.Controls.Add(this.chkwidescreen);
		this.Controls.Add(this.udata6);
		this.Controls.Add(this.udata5);
		this.Controls.Add(this.udata4);
		this.Controls.Add(this.udata3);
		this.Controls.Add(this.udata2);
		this.Controls.Add(this.udata1);
		this.Controls.Add(this.udata0);
		this.Controls.Add(this.trackBar7);
		this.Controls.Add(this.trackBar6);
		this.Controls.Add(this.trackBar5);
		this.Controls.Add(this.trackBar4);
		this.Controls.Add(this.trackBar3);
		this.Controls.Add(this.trackBar2);
		this.Controls.Add(this.trackBar1);
		this.Controls.Add(this.btnexport);
		this.Controls.Add(this.button1);
		this.Controls.Add(this.nuptime);
		this.Controls.Add(this.panel1);
		this.DoubleBuffered = true;
		this.Name = "form";
		this.Text = "Form1";
		((System.ComponentModel.ISupportInitialize)(this.panel1)).EndInit();
		((System.ComponentModel.ISupportInitialize)(this.nuptime)).EndInit();
		((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
		((System.ComponentModel.ISupportInitialize)(this.trackBar2)).EndInit();
		((System.ComponentModel.ISupportInitialize)(this.trackBar3)).EndInit();
		((System.ComponentModel.ISupportInitialize)(this.trackBar4)).EndInit();
		((System.ComponentModel.ISupportInitialize)(this.trackBar5)).EndInit();
		((System.ComponentModel.ISupportInitialize)(this.trackBar6)).EndInit();
		((System.ComponentModel.ISupportInitialize)(this.trackBar7)).EndInit();
		this.ResumeLayout(false);
		this.PerformLayout();

	}

	#endregion

	private System.Windows.Forms.NumericUpDown nuptime;
	private System.Windows.Forms.Button button1;
	private System.Windows.Forms.Timer timer1;
	private System.Windows.Forms.Button btnexport;
	private System.Windows.Forms.TrackBar trackBar1;
	private System.Windows.Forms.TrackBar trackBar2;
	private System.Windows.Forms.TrackBar trackBar3;
	private System.Windows.Forms.TrackBar trackBar4;
	private System.Windows.Forms.TrackBar trackBar5;
	private System.Windows.Forms.TrackBar trackBar6;
	private System.Windows.Forms.TrackBar trackBar7;
	private System.Windows.Forms.Label udata0;
	private System.Windows.Forms.Label udata1;
	private System.Windows.Forms.Label udata2;
	private System.Windows.Forms.Label udata3;
	private System.Windows.Forms.Label udata4;
	private System.Windows.Forms.Label udata5;
	private System.Windows.Forms.Label udata6;
	private System.Windows.Forms.CheckBox chkwidescreen;
	private System.Windows.Forms.CheckBox chkComments;
		private System.Windows.Forms.CheckBox chkPhantom;
	private System.Windows.Forms.Button button2;
		private System.Windows.Forms.ListBox listBox1;
		private System.Windows.Forms.ListBox listBox2;
		private System.Windows.Forms.ListBox listBox3;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.TextBox textBox2;
		private System.Windows.Forms.TextBox textBox3;
		private System.Windows.Forms.Button abutton1;
		private System.Windows.Forms.Button abutton2;
		private System.Windows.Forms.Button abutton3;
		private System.Windows.Forms.Button abuttonsave;
		private System.Windows.Forms.Panel pnlBezs;
		private System.Windows.Forms.PictureBox panel1;
		private System.Windows.Forms.ComboBox comboBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button button3;
}
}

