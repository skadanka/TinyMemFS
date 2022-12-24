using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace ThrashingGenerator
{
    public partial class Form1 : Form
    {
        private static bool running = false;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 10; i++)
            {
                Thread trasher = new Thread(() => thrashProc());
                trasher.Name = i.ToString();
                if (!running)
                    trasher.Start();
            }
        }

        private static void thrashProc()
        {
            string imagePath = System.Environment.CurrentDirectory + @"\Cow.JPG";
            running = true;
            Random rand = new Random();
            while (running)
            {
                Thread.Sleep(20);
                Image img = Image.FromFile(imagePath);
                using (Graphics g = Graphics.FromImage(img))
                    g.DrawLine(Pens.Black, rand.Next(0, img.Width-1), 10, 20, 20);
                img.Save(imagePath + "Cpy" + Thread.CurrentThread.Name);
                img.Dispose();
            }
            File.Delete(imagePath + "Cpy" + Thread.CurrentThread.Name);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            running = false;
            //Environment.Exit(0);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
