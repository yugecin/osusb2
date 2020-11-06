using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace osusb1 {
partial class form : Form {

	public form() {
		InitializeComponent();
		trackBar1.ValueChanged += udata_ValueChanged;
		trackBar2.ValueChanged += udata_ValueChanged;
		trackBar3.ValueChanged += udata_ValueChanged;
		trackBar4.ValueChanged += udata_ValueChanged;
		trackBar5.ValueChanged += udata_ValueChanged;
		trackBar6.ValueChanged += udata_ValueChanged;
		trackBar7.ValueChanged += udata_ValueChanged;
		this.Text = all.osb;
		all.Widescreen = chkwidescreen.Checked;
		refreshLists();
		nuptime.Value = 71208;
		nuptime.KeyDown += new KeyEventHandler(nuptime_KeyDown);
		listBox1.SelectedIndexChanged += new EventHandler(listBox_SelectedIndexChanged);
		listBox2.SelectedIndexChanged += new EventHandler(listBox_SelectedIndexChanged);
		listBox3.SelectedIndexChanged += new EventHandler(listBox_SelectedIndexChanged);
		listBox1.KeyDown += new KeyEventHandler(nuptime_KeyDown);
		listBox2.KeyDown += new KeyEventHandler(nuptime_KeyDown);
		listBox3.KeyDown += new KeyEventHandler(listbox3keydown);
		textBox1.TextChanged += new EventHandler(textBox1_TextChanged);
		textBox2.TextChanged += new EventHandler(textBox1_TextChanged);
		textBox3.TextChanged += new EventHandler(textBox1_TextChanged);
		abutton1.Click += new EventHandler(abutton_Click);
		abutton2.Click += new EventHandler(abutton_Click);
		abutton3.Click += new EventHandler(abutton_Click);
		abuttonsave.Click += new EventHandler(abuttonsave_Click);
	}

	void abuttonsave_Click(object sender, EventArgs e)
	{
		harr.write();
	}

	void abutton_Click(object sender, EventArgs e)
	{
		if (sender == abutton1) {
			all.vec3[] pts = new all.vec3[harr.points.Length + 1];
			int i;
			for (i = 0; i < harr.points.Length; i++) {
				pts[i] = all.v3(harr.points[i]);
			}
			pts[harr.points.Length] = all.v3(harr.points[i - 1]);
			harr.points = pts;
			refreshLists();
			listBox1.SelectedIndex = i;
		} else if (sender == abutton2) {
			int[][] lines = new int[harr.lines.Length + 1][];
			int i;
			for (i = 0; i < harr.lines.Length; i++) {
				lines[i] = new int[] { harr.lines[i][0], harr.lines[i][1] };
			}
			lines[i] = new int[] { harr.lines[i - 1][0], harr.lines[i - 1][1] };
			harr.lines = lines;
			refreshLists();
			listBox2.SelectedIndex = i;
		} else if (sender == abutton3) {
			int[][] tris = new int[harr.tris.Length + 1][];
			int i;
			for (i = 0; i < harr.tris.Length; i++) {
				tris[i] = new int[] { harr.tris[i][0], harr.tris[i][1],
				harr.tris[i][2], harr.tris[i][3]};
			}
			tris[i] = new int[] { harr.tris[i - 1][0], harr.tris[i - 1][1],
				harr.tris[i - 1][2], harr.tris[i - 1][3]};
			harr.tris = tris;
			refreshLists();
			listBox3.SelectedIndex = i;
		}
	}

	void textBox1_TextChanged(object sender, EventArgs e)
	{
		try {
			if (sender == textBox1) {
				string[] u = textBox1.Text.Split(' ');
				if (u.Length == 3) {
					float x = float.Parse(u[0]);
					float y = float.Parse(u[1]);
					float z = float.Parse(u[2]);
					all.harrpoint = all.v3(x, y, z);
					harr.points[listBox1.SelectedIndex].x = x;
					harr.points[listBox1.SelectedIndex].y = y;
					harr.points[listBox1.SelectedIndex].z = z;
					all.haha();
				}
			} else if (sender == textBox2) {
				string[] z = textBox2.Text.Split(' ');
				if (z.Length == 2) {
					int a = int.Parse(z[0]);
					int b = int.Parse(z[1]);
					harr.lines[listBox2.SelectedIndex][0] = a;
					harr.lines[listBox2.SelectedIndex][1] = b;
					all.haha();
				}
			} else if (sender == textBox3) {
				string[] z = textBox3.Text.Split(' ');
				if (z.Length == 4) {
					int a = int.Parse(z[0]);
					int b = int.Parse(z[1]);
					int c = int.Parse(z[2]);
					int d = int.Parse(z[3]);
					harr.tris[listBox3.SelectedIndex][0] = a;
					harr.tris[listBox3.SelectedIndex][1] = b;
					harr.tris[listBox3.SelectedIndex][2] = c;
					harr.tris[listBox3.SelectedIndex][3] = d;
					all.haha();
				}
			}
			panel1.Refresh();
		} catch (Exception) { }
	}

	void listBox_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (sender == listBox1) {
			all.vec3 p = harr.points[listBox1.SelectedIndex];
			all.harrpoint = all.v3(p);
			textBox1.Text = p.x + " " + p.y + " " + p.z;
		} else if (sender == listBox2) {
			int[] l = harr.lines[listBox2.SelectedIndex];
			all.harrpoint = all.v3(
				(harr.points[l[0]].x + harr.points[l[1]].x) / 2,
				(harr.points[l[0]].y + harr.points[l[1]].y) / 2,
				(harr.points[l[0]].z + harr.points[l[1]].z) / 2
				);
			textBox2.Text = l[0] + " " + l[1];
		} else if (sender == listBox3) {
			int[] t = harr.tris[listBox3.SelectedIndex];
			all.harrpoint = all.v3(
				(harr.points[t[1]].x + harr.points[t[2]].x + harr.points[t[3]].x) / 3,
				(harr.points[t[1]].y + harr.points[t[2]].y + harr.points[t[3]].y) / 3,
				(harr.points[t[1]].z + harr.points[t[2]].z + harr.points[t[3]].z) / 3
				);
			textBox3.Text = t[0] + " " + t[1] + " " + t[2] + " " + t[3];
		}
		panel1.Refresh();
	}

	void refreshLists()
	{
		listBox1.BeginUpdate();
		listBox2.BeginUpdate();
		listBox3.BeginUpdate();

		listBox1.Items.Clear();
		listBox2.Items.Clear();
		listBox3.Items.Clear();

		for (int i = 0; i < harr.points.Length; i++) {
			listBox1.Items.Add(i + ": " + harr.points[i]);
		}

		for (int i = 0; i < harr.lines.Length; i++) {
			listBox2.Items.Add(i + ": " + harr.lines[i][0] + " " + harr.lines[i][1]);
		}

		for (int i = 0; i < harr.tris.Length; i++) {
			listBox3.Items.Add(i + ": " + harr.tris[i][0] + " " + harr.tris[i][1] + " " + harr.tris[i][2] + " " + harr.tris[i][3]);
		}

		listBox3.EndUpdate();
		listBox2.EndUpdate();
		listBox1.EndUpdate();
	}

	private int last_selected_triangle_index = -1;

	void listbox3keydown(object sender, KeyEventArgs e)
	{
		e.Handled = true;
		e.SuppressKeyPress = true;
		int sel = listBox3.SelectedIndex;
		if (e.KeyCode == Keys.Up) {
			if (sel > 0) {
				int[] tmp = harr.tris[sel];
				harr.tris[sel] = harr.tris[sel - 1];
				harr.tris[sel - 1] = tmp;
				all.haha();
				refreshLists();
				listBox3.SelectedIndex = sel - 1;
				panel1.Refresh();
			}
			return;
		}
		if (e.KeyCode == Keys.Down) {
			if (sel < listBox3.Items.Count - 1) {
				int[] tmp = harr.tris[sel];
				harr.tris[sel] = harr.tris[sel + 1];
				harr.tris[sel + 1] = tmp;
				all.haha();
				refreshLists();
				listBox3.SelectedIndex = sel + 1;
				panel1.Refresh();
			}
			return;
		}
		if (e.KeyCode == Keys.A) {
			if (listBox3.SelectedIndex < 0) {
				return;
			}
			if (last_selected_triangle_index == -1) {
				last_selected_triangle_index = listBox3.SelectedIndex;
				return;
			}
			int[] tmp = harr.tris[listBox3.SelectedIndex];
			harr.tris[listBox3.SelectedIndex] = harr.tris[last_selected_triangle_index];
			harr.tris[last_selected_triangle_index] = tmp;
			last_selected_triangle_index = -1;
			all.haha();
			refreshLists();
			panel1.Refresh();
			return;
		}
		nuptime_KeyDown(sender, e);
	}

	void nuptime_KeyDown(object sender, KeyEventArgs e)
	{
		e.Handled = true;
		e.SuppressKeyPress = true;
		all.debugmove(e.KeyCode);
		panel1.Refresh();
	}

	void udata_ValueChanged(object sender, EventArgs e) {
		string num = (sender as Control).Tag.ToString();
		int val = (sender as TrackBar).Value * 5;
		(Controls.Find("udata" + num, false)[0] as Label).Text = val.ToString();
		all.udata[int.Parse(num)] = val;
		panel1.Invalidate();
	}

	void panel1_Paint(object sender, PaintEventArgs e) {
		/*
		Bitmap bm = new Bitmap(panel1.Width, panel1.Height);
		all.render((int) nuptime.Value, Graphics.FromImage(bm));
		e.Graphics.DrawImage(bm, 0, 0);
		*/
		all.render((int) nuptime.Value, e.Graphics);
	}

	void nuptime_ValueChanged(object sender, EventArgs e) {
		panel1.Invalidate();
	}

	private void chkwidescreen_CheckedChanged(object sender, EventArgs e) {
		all.Widescreen = chkwidescreen.Checked;
		panel1.Invalidate();
	}

	void button1_Click(object sender, EventArgs e) {
		timer1.Enabled = !timer1.Enabled;
	}

	void timer1_Tick(object sender, EventArgs e) {
		nuptime.Value = (int) nuptime.Value + timer1.Interval;
	}

	void UI_ExportRequest(object sender, EventArgs e) {
		((Control) sender).Enabled = false;
		all.Widescreen = chkwidescreen.Checked;
		all.processPhantom = chkPhantom.Checked;
		all.export(chkComments.Checked);
		((Control) sender).Enabled = true;
	}

	bool pmousedown = false;
	Point plastval;
	Point pmouse;
	private void panel1_MouseDown(object sender, MouseEventArgs e) {
		pmousedown = true;
		pmouse = new Point(e.Location.X, e.Location.Y);
	}

	private void panel1_MouseMove(object sender, MouseEventArgs e) {
		if (pmousedown) {
			all.mouse.x = e.Location.X - pmouse.X + plastval.X;
			all.mouse.y = -(e.Location.Y - pmouse.Y) + plastval.Y;
			panel1.Refresh();
		}
	}

	private void panel1_MouseUp(object sender, MouseEventArgs e) {
		pmousedown = false;
		if (e.Button == System.Windows.Forms.MouseButtons.Right) {
			all.mouse.x = all.mouse.y = 0;
		}
		plastval = all.mouse.point();
		panel1.Refresh();
	}

	private void button2_Click(object sender, EventArgs e) {
		button2.Enabled = false;
		all.dovariablething();
		button2.Enabled = true;
	}
}

partial class all {

	[STAThread]
	static void Main() {
		all.Widescreen = true;
		CultureInfo customCulture = (CultureInfo) Thread.CurrentThread.CurrentCulture.Clone();
		customCulture.NumberFormat.NumberDecimalSeparator = ".";
		Thread.CurrentThread.CurrentCulture = customCulture;
		harr.read();
		fft = new FFT();
		zs = new List<Z>();
		font = new Font();
		eq_init();
		init();
		Application.EnableVisualStyles();
		Application.SetCompatibleTextRenderingDefault(false);
		Application.Run(new form());
	}

	public static string path = @"S:\games\osu!\Songs\beatmap-637369189818030795-bensound-dubstep";
	public static string osb = path + @"\bensound - dubstep (yugecin).osb";
	public static string osbx = path + @"\bensound - dubstep (yugecin))-.osb";

	static List<Z> zs;
	static FFT fft;
	static Font font;

	static bool rendering;
	static bool isPhantomFrame;
	public static bool processPhantom;

	public static vec2 mouse = v2(0f);

	public static int[] udata = new int[8];

	public static int DECIMALS_PRECISE = 7;

	static void init() {
		zs.Clear();
		zs.Add(new Zcamera(00000, 124000));
		//zs.Add(new Zairport2(00000, 71208));
		//zs.Add(new Zltext(00000, 71208, "robin_be presents", v3(0f, 250f, -20f)));
		//zs.Add(new Zltext(00000, 71208, "osu! storyboard demo number two", v3(0f, 150f, -20f)));
		zs.Add(new Zharrier(00000, 71208));
		zs.Add(new Zharrierbreakdown(71208, 104085));
		foreach (Z z in zs) {
			z.framedelta = 1000 / 20;
		}
		foreach (Z z in zs) {
			if (z.framedelta == 0) {
				throw new Exception("framedelta for " + z.GetType().Name);
			}
			if (z.phantomframedelta == 0) {
				z.phantomframedelta = z.framedelta;
			}
		}
	}

	internal
	static void render(int time, Graphics g) {
		if (g != null) {
			g.TranslateTransform(107f, 0f);
			g.FillRectangle(new SolidBrush(Color.Black), LOWERBOUND, 0, UPPERBOUND - LOWERBOUND * 2, 480);
		}

		fft.Update(time);

		foreach (Z z in zs) {
			if (z.start <= time && time < z.stop) {
				isPhantomFrame = false;
				if (rendering && time % z.framedelta != 0) {
					if (!processPhantom || time % z.phantomframedelta != 0) {
						continue;
					}
					isPhantomFrame = true;
				}
				int reltime = time - z.start;
				z.draw(new SCENE(z.start, z.stop, time, g));
			}
		}

		if (g != null && !Widescreen) {
			Widescreen = true;
			Brush b = new SolidBrush(SystemColors.Control);
			g.FillRectangle(b, LOWERBOUND, 0, -LOWERBOUND, 480);
			g.FillRectangle(b, UPPERBOUND + LOWERBOUND, 0, -LOWERBOUND, 480);
			Widescreen = false;
		}
	}

	internal
	static void export(bool comments) {
		mouse.x = 0;
		mouse.y = 0;
		int mintime = int.MaxValue;
		int maxtime = int.MinValue;
		foreach (Z z in zs) {
			if (z.start < mintime) {
				mintime = z.start;
			}
			if (z.stop > maxtime) {
				maxtime = z.stop;
			}
		}
		int nextprogress = 0;
		rendering = true;
		for (int i = mintime; i < maxtime; i += 5) {
			int progress = (i - mintime) * 100 / (maxtime - mintime);
			if (progress >= nextprogress) {
				Console.Write("{0}% ", progress);
				nextprogress += 5;
			}
			render(i, null);
		}
		rendering = false;
		isPhantomFrame = false;
		Console.WriteLine("\nWriting...");
		using (StreamWriter w = new StreamWriter(osb)) {
			w.Write("[32]\n");
			w.Write("4,0,1,b.png,0,0\n");
			Writer writer = new Writer(w, comments);
			fin(writer);
		}
		foreach (string sprite in Sprite.usagedata.Keys) {
			Console.WriteLine("sprite '{0}': {1}", sprite, Sprite.usagedata[sprite]);
		}
		Console.WriteLine(
			"easing results: {0} success {1} failure {2} commands saved",
			Sprite.easeResultSuccess,
			Sprite.easeResultFailed,
			Sprite.easeCommandsSaved
		);
		Console.WriteLine(
			"{0}KB / {1}KiB",
			Sprite.easeResultBytesSaved / 1000f,
			Sprite.easeResultBytesSaved / 1024f
		);
		Console.WriteLine("Done");
	}

	static void fin(Writer w) {
		int totalbytes = 0;
		foreach (Z z in zs) {
			Sprite.framedelta = z.framedelta;
			w.byteswritten = 0;
			w.comment(z.GetType().Name);
			z.fin(w);
			Console.WriteLine(
				"scene '{0}' @ {1}fps ({2}): {3}KB",
				z.GetType().Name,
				1000 / z.framedelta,
				1000 / z.phantomframedelta,
				w.byteswritten / 1000f
			);
			totalbytes += w.byteswritten;
		}
		Console.WriteLine("{0}KB / {1}KiB", totalbytes / 1000f, totalbytes / 1024f);
		//w.ln("4,3,1,,NaN,-∞");
		w.ln("");
	}
}
}
