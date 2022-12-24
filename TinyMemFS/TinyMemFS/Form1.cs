using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TinyMemFS
{
    public partial class Form1 : Form
    {
        TinyMemFS tinyMemFS;
        public Form1()
        {
            InitializeComponent();
            tinyMemFS = new TinyMemFS();
            //dataGridView1.Columns.Add("File Number", "File Number");
            dataGridView1.Columns.Add("File Name", "File Name");
            dataGridView1.Columns.Add("File Size", "File Size");
            dataGridView1.Columns.Add("File date&time", "File date&time");

            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void addFileButton_Click(object sender, EventArgs e)
        { 
            string fileName = fileNameAddText.Text;
            string filePath = filePathTextAdd.Text;

            if (!(tinyMemFS.add(fileName, filePath)))
            {
                fileNameAddText.Clear();
                filePathTextAdd.Clear();
                MessageBox.Show("The operation 'add file' has failed, please check your arguments", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                List<String> files = tinyMemFS.listFiles();
                String cur = files[files.Count - 1];
                String[] final = cur.Split(',');
                dataGridView1.Rows.Add(final[0], final[1], final[2]);
                fileNameAddText.Clear();
                filePathTextAdd.Clear();
                changeSize();
                //MessageBox.Show("The operation 'add file' succeeded", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void removeFileButton_Click(object sender, EventArgs e)
        {
            //String fileName = fileNameRemoveText.Text;
            String fileNametmp = dataGridView1.SelectedCells[0].Value.ToString();
            int index = dataGridView1.SelectedCells[0].RowIndex;
            

            if (!(tinyMemFS.remove(fileNametmp)))
            {
                MessageBox.Show("The operation 'delete file' has failed, please check your file name", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                dataGridView1.Rows.RemoveAt(index);
                changeSize();
                //MessageBox.Show("The operation 'delete file' succeeded", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void saveFileButton_Click(object sender, EventArgs e)
        {
            String fileName = fileNameSave.Text;
            String filePath = filePathSave.Text;

            if (!(tinyMemFS.save(fileName, filePath)))
            {
                fileNameSave.Clear();
                filePathSave.Clear();
                MessageBox.Show("The operation 'save file' has failed, please check your arguments", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                fileNameSave.Clear();
                filePathSave.Clear();
                MessageBox.Show("The operation 'save file' succeeded", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void encyrptButton_Click(object sender, EventArgs e)
        {
            String key = encryptKeyText.Text;

            if (!(tinyMemFS.encrypt(key)))
            {
                encryptKeyText.Clear();
                MessageBox.Show("The operation 'Encryption' has failed, key is not valid", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                encryptKeyText.Clear();
                MessageBox.Show("The operation 'Encryption' succeeded", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void decryptButton_Click(object sender, EventArgs e)
        {
            String key = decryptKeyText.Text;

            if (!(tinyMemFS.decrypt(key)))
            {
                decryptKeyText.Clear();
                MessageBox.Show("The operation 'Decryption' has failed, key is not valid", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                decryptKeyText.Clear();
                MessageBox.Show("The operation 'Decryption' succeeded", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void reanameFileButton_Click(object sender, EventArgs e)
        {
            String curName = reanameFileNameText.Text;
            String newName = reanameToText.Text;
            reanameFileNameText.Clear();
            reanameToText.Clear();
            bool found = false;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if ((String)row.Cells["File Name"].Value == curName)
                {
                    if (tinyMemFS.rename(curName, newName))
                    {
                        row.SetValues(newName);
                        found = true;
                    }
                }
            }
            if (!found)
            {
                MessageBox.Show("The operation 'Reaname' has failed, please choose valid arguments", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String curName = createCopyOfText.Text;
            String newName = createCopyToText.Text;
            createCopyToText.Clear();
            createCopyOfText.Clear();
            int index = 0;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if ((String)row.Cells["File Name"].Value == curName)
                {
                    if (tinyMemFS.copy(curName, newName))
                    {
                        //row.SetValues(newName);

                        List<String> files = tinyMemFS.listFiles();
                        String cur = files[files.Count - 1];
                        String[] final = cur.Split(',');
                        dataGridView1.Rows.Add(final[0], final[1], final[2]);
                        changeSize();
                    }
                }
                index++;
            }
        }

        private void presentSizeText_TextChanged(object sender, EventArgs e)
        {

        }

        private void changeSize()
        {
            long size = tinyMemFS.getSize();
            presentSizeText.Text = size.ToString();
        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void loadToDiskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (Form2 form = new Form2())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    TextBox tb = form.getText();
                    String fileName = tb.Text;
                    if (tinyMemFS.saveToDisk(fileName))
                    {

                    }
                    else
                    {
                        MessageBox.Show("The operation save to disk has failed", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
            }
        }

        private void loadFromDiskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            {
                using (Form3 form = new Form3())
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        TextBox tb = form.getText();
                        String fileName = tb.Text;

                        if (tinyMemFS.loadFromDisk(fileName))
                        {
                            dataGridView1.Rows.Clear();
                            List<String> files = tinyMemFS.listFiles();
                            for (int i = 0; i < tinyMemFS.filesList.Count; i++)
                            {
                                String cur = files[i];
                                String[] final = cur.Split(',');
                                dataGridView1.Rows.Add(final[0], final[1], final[2]);
                                fileNameAddText.Clear();
                                filePathTextAdd.Clear();
                                changeSize();
                            }
                        }
                        else
                        {
                            MessageBox.Show("The operation load to disk has failed", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
        }

        private void saveFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (Form4 form = new Form4())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    TextBox fileNameText = form.getFileName();
                    TextBox filePathText = form.getFilePath();
                    String fileName = fileNameText.Text;
                    String filePath = filePathText.Text;

                    if (tinyMemFS.save(fileName, filePath))
                    {

                    }
                    else
                    {
                        MessageBox.Show("The operation load to disk has failed", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void byNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tinyMemFS.sortByName();
            {
                dataGridView1.Rows.Clear();
                List<String> files = tinyMemFS.listFiles();
                for (int i = 0; i < tinyMemFS.filesList.Count; i++)
                {
                    String cur = files[i];
                    String[] final = cur.Split(',');
                    dataGridView1.Rows.Add(final[0], final[1], final[2]);
                    fileNameAddText.Clear();
                    filePathTextAdd.Clear();
                    changeSize();
                }
            }
        }

        private void bySizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tinyMemFS.sortBySize();
            {
                dataGridView1.Rows.Clear();
                List<String> files = tinyMemFS.listFiles();
                for (int i = 0; i < tinyMemFS.filesList.Count; i++)
                {
                    String cur = files[i];
                    String[] final = cur.Split(',');
                    dataGridView1.Rows.Add(final[0], final[1], final[2]);
                    fileNameAddText.Clear();
                    filePathTextAdd.Clear();
                    changeSize();
                }
            }
        }

        private void byDateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tinyMemFS.sortByDate();
            {
                dataGridView1.Rows.Clear();
                List<String> files = tinyMemFS.listFiles();
                for (int i = 0; i < tinyMemFS.filesList.Count; i++)
                {
                    String cur = files[i];
                    String[] final = cur.Split(',');
                    dataGridView1.Rows.Add(final[0], final[1], final[2]);
                    fileNameAddText.Clear();
                    filePathTextAdd.Clear();
                    changeSize();
                }
            }
        }
    }
}

