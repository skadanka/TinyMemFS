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
    public partial class Form2 : Form
    {
        //private String fileNameToSave;
        //private String fileNameToLoad;
        public Form2()
        {
            InitializeComponent();
        }

        //public String getFileNameToSave()
        //{
        //    return this.fileNameToSave;
        //}

        //public String getFileNameToLoad()
        //{
        //    return this.fileNameToLoad;
        //}

        public TextBox getText()
        {
            return fileNameToLoadText;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            //this.fileNameToSave = fileNameToLoadText.Text;
        }
    }
}
