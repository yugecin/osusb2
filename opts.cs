using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace osusb1
{
public partial class opts : Form
{
	public static bool export;
	public static bool[] enabled = new bool[7];
	public static int[] c1idx = new int[6];
	public static int[] c2idx = new int[6];
	public static int c1pxs, c2pxs;
	public static int[] fps = new int[7];
	public static string txt1, txt2;

	public opts()
	{
		InitializeComponent();

		c1right.SelectedIndex = 0;
		c1front.SelectedIndex = 1;
		c1back.SelectedIndex = 2;
		c1top.SelectedIndex = 3;
		c1bot.SelectedIndex = 4;
		c1left.SelectedIndex = 0;
		c1ps.SelectedIndex = 4;

		c2right.SelectedIndex = 5;
		c2front.SelectedIndex = 6;
		c2back.SelectedIndex = 5;
		c2top.SelectedIndex = 5;
		c2bot.SelectedIndex = 5;
		c2left.SelectedIndex = 5;
		c2ps.SelectedIndex = 4;

		setvals();
	}

	private void setvals()
	{
		c1idx[0] = c1right.SelectedIndex;
		c1idx[1] = c1front.SelectedIndex;
		c1idx[2] = c1back.SelectedIndex;
		c1idx[3] = c1top.SelectedIndex;
		c1idx[4] = c1bot.SelectedIndex;
		c1idx[5] = c1left.SelectedIndex;
		c1pxs = c1ps.SelectedIndex + 1;

		c2idx[0] = c2right.SelectedIndex;
		c2idx[1] = c2front.SelectedIndex;
		c2idx[2] = c2back.SelectedIndex;
		c2idx[3] = c2top.SelectedIndex;
		c2idx[4] = c2bot.SelectedIndex;
		c2idx[5] = c2left.SelectedIndex;
		c2pxs = c2ps.SelectedIndex + 1;

		enabled[0] = checkBox1.Checked;
		enabled[1] = checkBox2.Checked;
		enabled[2] = checkBox3.Checked;
		enabled[3] = checkBox4.Checked;
		enabled[4] = checkBox5.Checked;
		enabled[5] = checkBox6.Checked;
		enabled[6] = checkBox7.Checked;

		fps[0] = (int)numericUpDown1.Value;
		fps[1] = (int)numericUpDown2.Value;
		fps[2] = (int)numericUpDown3.Value;
		fps[3] = (int)numericUpDown4.Value;
		fps[4] = (int)numericUpDown5.Value;
		fps[5] = (int)numericUpDown6.Value;
		fps[6] = (int)numericUpDown7.Value;

		txt1 = textBox1.Text;
		txt2 = textBox2.Text;

		all.init();
	}

	private void button1_Click(object sender, EventArgs e)
	{
		export = true;
		setvals();
		this.Close();
	}

	private void button2_Click(object sender, EventArgs e)
	{
		new form().ShowDialog();
	}
}
}