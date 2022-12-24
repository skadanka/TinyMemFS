using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TinyMemFS
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        public TextBox getFileName()
        {
            return fileName;
        }

        public TextBox getFilePath()
        {
            return filePath;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
