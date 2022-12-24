namespace TinyMemFS
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.fileNameAddText = new System.Windows.Forms.TextBox();
            this.filePathTextAdd = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.fileNameSave = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.filePathSave = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.addFileButton = new System.Windows.Forms.Button();
            this.removeFileButton = new System.Windows.Forms.Button();
            this.saveFileButton = new System.Windows.Forms.Button();
            this.encyrptButton = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.encryptKeyText = new System.Windows.Forms.TextBox();
            this.decryptKeyText = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.decryptButton = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.reanameFileNameText = new System.Windows.Forms.TextBox();
            this.reanameToText = new System.Windows.Forms.TextBox();
            this.reanameFileButton = new System.Windows.Forms.Button();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.createCopyOfText = new System.Windows.Forms.TextBox();
            this.createCopyToText = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label21 = new System.Windows.Forms.Label();
            this.presentSizeText = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToDiskToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadFromDiskToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sortDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.byNameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bySizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.byDateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(475, 40);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(500, 289);
            this.dataGridView1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Calibri", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label1.Location = new System.Drawing.Point(12, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(214, 21);
            this.label1.TabIndex = 1;
            this.label1.Text = "Welcome to our file system! ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Calibri", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label2.Location = new System.Drawing.Point(12, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(406, 21);
            this.label2.TabIndex = 2;
            this.label2.Text = "Please choose operatios to perform from the list below:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Calibri", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label3.Location = new System.Drawing.Point(12, 97);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 21);
            this.label3.TabIndex = 3;
            this.label3.Text = "Add File";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label4.Location = new System.Drawing.Point(12, 122);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 18);
            this.label4.TabIndex = 4;
            this.label4.Text = "File Name: ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label5.Location = new System.Drawing.Point(206, 122);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 18);
            this.label5.TabIndex = 5;
            this.label5.Text = "File Path:";
            // 
            // fileNameAddText
            // 
            this.fileNameAddText.Font = new System.Drawing.Font("Calibri", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.fileNameAddText.Location = new System.Drawing.Point(97, 122);
            this.fileNameAddText.Name = "fileNameAddText";
            this.fileNameAddText.Size = new System.Drawing.Size(100, 23);
            this.fileNameAddText.TabIndex = 6;
            // 
            // filePathTextAdd
            // 
            this.filePathTextAdd.Font = new System.Drawing.Font("Calibri", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.filePathTextAdd.Location = new System.Drawing.Point(282, 122);
            this.filePathTextAdd.Name = "filePathTextAdd";
            this.filePathTextAdd.Size = new System.Drawing.Size(100, 23);
            this.filePathTextAdd.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Calibri", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label6.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label6.Location = new System.Drawing.Point(12, 193);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(98, 21);
            this.label6.TabIndex = 8;
            this.label6.Text = "Remove File";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Calibri", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label7.Location = new System.Drawing.Point(12, 314);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(94, 21);
            this.label7.TabIndex = 9;
            this.label7.Text = "Save to disk";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label9.Location = new System.Drawing.Point(12, 346);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(78, 18);
            this.label9.TabIndex = 12;
            this.label9.Text = "File Name: ";
            // 
            // fileNameSave
            // 
            this.fileNameSave.Font = new System.Drawing.Font("Calibri", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.fileNameSave.Location = new System.Drawing.Point(99, 345);
            this.fileNameSave.Name = "fileNameSave";
            this.fileNameSave.Size = new System.Drawing.Size(100, 23);
            this.fileNameSave.TabIndex = 13;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label10.Location = new System.Drawing.Point(209, 347);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(88, 18);
            this.label10.TabIndex = 14;
            this.label10.Text = "Save to path:";
            // 
            // filePathSave
            // 
            this.filePathSave.Font = new System.Drawing.Font("Calibri", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.filePathSave.Location = new System.Drawing.Point(315, 346);
            this.filePathSave.Name = "filePathSave";
            this.filePathSave.Size = new System.Drawing.Size(100, 23);
            this.filePathSave.TabIndex = 15;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Font = new System.Drawing.Font("Calibri", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label11.Location = new System.Drawing.Point(12, 426);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(186, 21);
            this.label11.TabIndex = 16;
            this.label11.Text = "Encrypt all files using key";
            // 
            // addFileButton
            // 
            this.addFileButton.Font = new System.Drawing.Font("Calibri", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.addFileButton.Location = new System.Drawing.Point(12, 154);
            this.addFileButton.Name = "addFileButton";
            this.addFileButton.Size = new System.Drawing.Size(406, 28);
            this.addFileButton.TabIndex = 17;
            this.addFileButton.Text = "Add file";
            this.addFileButton.UseVisualStyleBackColor = true;
            this.addFileButton.Click += new System.EventHandler(this.addFileButton_Click);
            // 
            // removeFileButton
            // 
            this.removeFileButton.Font = new System.Drawing.Font("Calibri", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.removeFileButton.Location = new System.Drawing.Point(12, 264);
            this.removeFileButton.Name = "removeFileButton";
            this.removeFileButton.Size = new System.Drawing.Size(406, 30);
            this.removeFileButton.TabIndex = 18;
            this.removeFileButton.Text = "Remove file";
            this.removeFileButton.UseVisualStyleBackColor = true;
            this.removeFileButton.Click += new System.EventHandler(this.removeFileButton_Click);
            // 
            // saveFileButton
            // 
            this.saveFileButton.Font = new System.Drawing.Font("Calibri", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.saveFileButton.Location = new System.Drawing.Point(12, 374);
            this.saveFileButton.Name = "saveFileButton";
            this.saveFileButton.Size = new System.Drawing.Size(406, 29);
            this.saveFileButton.TabIndex = 19;
            this.saveFileButton.Text = "Save file";
            this.saveFileButton.UseVisualStyleBackColor = true;
            this.saveFileButton.Click += new System.EventHandler(this.saveFileButton_Click);
            // 
            // encyrptButton
            // 
            this.encyrptButton.Font = new System.Drawing.Font("Calibri", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.encyrptButton.Location = new System.Drawing.Point(12, 484);
            this.encyrptButton.Name = "encyrptButton";
            this.encyrptButton.Size = new System.Drawing.Size(406, 27);
            this.encyrptButton.TabIndex = 20;
            this.encyrptButton.Text = "Encrypt all files";
            this.encyrptButton.UseVisualStyleBackColor = true;
            this.encyrptButton.Click += new System.EventHandler(this.encyrptButton_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label12.Location = new System.Drawing.Point(12, 454);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(35, 18);
            this.label12.TabIndex = 21;
            this.label12.Text = "Key:";
            // 
            // encryptKeyText
            // 
            this.encryptKeyText.Font = new System.Drawing.Font("Calibri", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.encryptKeyText.Location = new System.Drawing.Point(54, 454);
            this.encryptKeyText.Name = "encryptKeyText";
            this.encryptKeyText.Size = new System.Drawing.Size(134, 23);
            this.encryptKeyText.TabIndex = 22;
            // 
            // decryptKeyText
            // 
            this.decryptKeyText.Font = new System.Drawing.Font("Calibri", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.decryptKeyText.Location = new System.Drawing.Point(54, 560);
            this.decryptKeyText.Name = "decryptKeyText";
            this.decryptKeyText.Size = new System.Drawing.Size(134, 23);
            this.decryptKeyText.TabIndex = 26;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label13.Location = new System.Drawing.Point(12, 560);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(35, 18);
            this.label13.TabIndex = 25;
            this.label13.Text = "Key:";
            // 
            // decryptButton
            // 
            this.decryptButton.Font = new System.Drawing.Font("Calibri", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.decryptButton.Location = new System.Drawing.Point(12, 590);
            this.decryptButton.Name = "decryptButton";
            this.decryptButton.Size = new System.Drawing.Size(406, 27);
            this.decryptButton.TabIndex = 24;
            this.decryptButton.Text = "Decrypt all files";
            this.decryptButton.UseVisualStyleBackColor = true;
            this.decryptButton.Click += new System.EventHandler(this.decryptButton_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.Transparent;
            this.label14.Font = new System.Drawing.Font("Calibri", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label14.Location = new System.Drawing.Point(12, 532);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(189, 21);
            this.label14.TabIndex = 23;
            this.label14.Text = "Decrypt all files using key";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Calibri", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label8.Location = new System.Drawing.Point(12, 215);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(385, 21);
            this.label8.TabIndex = 27;
            this.label8.Text = "Please choose the row of the file you want to delete,";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.Transparent;
            this.label15.Font = new System.Drawing.Font("Calibri", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label15.Location = new System.Drawing.Point(471, 416);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(98, 21);
            this.label15.TabIndex = 28;
            this.label15.Text = "Rename File";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.BackColor = System.Drawing.Color.Transparent;
            this.label16.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label16.Location = new System.Drawing.Point(471, 454);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(119, 18);
            this.label16.TabIndex = 29;
            this.label16.Text = "Change file name:";
            this.label16.Click += new System.EventHandler(this.label16_Click);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.BackColor = System.Drawing.Color.Transparent;
            this.label17.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label17.Location = new System.Drawing.Point(764, 451);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(25, 18);
            this.label17.TabIndex = 30;
            this.label17.Text = "to:";
            // 
            // reanameFileNameText
            // 
            this.reanameFileNameText.Font = new System.Drawing.Font("Calibri", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.reanameFileNameText.Location = new System.Drawing.Point(622, 452);
            this.reanameFileNameText.Name = "reanameFileNameText";
            this.reanameFileNameText.Size = new System.Drawing.Size(120, 23);
            this.reanameFileNameText.TabIndex = 31;
            // 
            // reanameToText
            // 
            this.reanameToText.Font = new System.Drawing.Font("Calibri", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.reanameToText.Location = new System.Drawing.Point(808, 452);
            this.reanameToText.Name = "reanameToText";
            this.reanameToText.Size = new System.Drawing.Size(120, 23);
            this.reanameToText.TabIndex = 32;
            // 
            // reanameFileButton
            // 
            this.reanameFileButton.Font = new System.Drawing.Font("Calibri", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.reanameFileButton.Location = new System.Drawing.Point(474, 484);
            this.reanameFileButton.Name = "reanameFileButton";
            this.reanameFileButton.Size = new System.Drawing.Size(538, 27);
            this.reanameFileButton.TabIndex = 33;
            this.reanameFileButton.Text = "Rename File";
            this.reanameFileButton.UseVisualStyleBackColor = true;
            this.reanameFileButton.Click += new System.EventHandler(this.reanameFileButton_Click);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.BackColor = System.Drawing.Color.Transparent;
            this.label18.Font = new System.Drawing.Font("Calibri", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label18.Location = new System.Drawing.Point(471, 532);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(150, 21);
            this.label18.TabIndex = 34;
            this.label18.Text = "Create copy of a file";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.BackColor = System.Drawing.Color.Transparent;
            this.label19.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label19.Location = new System.Drawing.Point(471, 556);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(121, 18);
            this.label19.TabIndex = 35;
            this.label19.Text = "File name to copy:";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.BackColor = System.Drawing.Color.Transparent;
            this.label20.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label20.Location = new System.Drawing.Point(746, 555);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(124, 18);
            this.label20.TabIndex = 36;
            this.label20.Text = "Name of copy fiile:";
            // 
            // createCopyOfText
            // 
            this.createCopyOfText.Font = new System.Drawing.Font("Calibri", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.createCopyOfText.Location = new System.Drawing.Point(619, 553);
            this.createCopyOfText.Name = "createCopyOfText";
            this.createCopyOfText.Size = new System.Drawing.Size(120, 23);
            this.createCopyOfText.TabIndex = 37;
            // 
            // createCopyToText
            // 
            this.createCopyToText.Font = new System.Drawing.Font("Calibri", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.createCopyToText.Location = new System.Drawing.Point(892, 554);
            this.createCopyToText.Name = "createCopyToText";
            this.createCopyToText.Size = new System.Drawing.Size(120, 23);
            this.createCopyToText.TabIndex = 38;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Calibri", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.button1.Location = new System.Drawing.Point(474, 590);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(538, 27);
            this.button1.TabIndex = 39;
            this.button1.Text = "Create Copy";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.BackColor = System.Drawing.Color.Transparent;
            this.label21.Font = new System.Drawing.Font("Calibri", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label21.Location = new System.Drawing.Point(668, 351);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(117, 21);
            this.label21.TabIndex = 40;
            this.label21.Text = "Total files size: ";
            // 
            // presentSizeText
            // 
            this.presentSizeText.Font = new System.Drawing.Font("Calibri", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.presentSizeText.Location = new System.Drawing.Point(791, 349);
            this.presentSizeText.Name = "presentSizeText";
            this.presentSizeText.Size = new System.Drawing.Size(100, 23);
            this.presentSizeText.TabIndex = 41;
            this.presentSizeText.TextChanged += new System.EventHandler(this.presentSizeText_TextChanged);
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.BackColor = System.Drawing.Color.Transparent;
            this.label22.Font = new System.Drawing.Font("Calibri", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label22.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label22.Location = new System.Drawing.Point(12, 236);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(182, 21);
            this.label22.TabIndex = 42;
            this.label22.Text = "finally, click \'remove file\'";
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1063, 28);
            this.menuStrip1.TabIndex = 43;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadToDiskToolStripMenuItem,
            this.loadFromDiskToolStripMenuItem,
            this.sortDataToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(46, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // loadToDiskToolStripMenuItem
            // 
            this.loadToDiskToolStripMenuItem.Name = "loadToDiskToolStripMenuItem";
            this.loadToDiskToolStripMenuItem.Size = new System.Drawing.Size(240, 26);
            this.loadToDiskToolStripMenuItem.Text = "Load system to disk";
            this.loadToDiskToolStripMenuItem.Click += new System.EventHandler(this.loadToDiskToolStripMenuItem_Click);
            // 
            // loadFromDiskToolStripMenuItem
            // 
            this.loadFromDiskToolStripMenuItem.Name = "loadFromDiskToolStripMenuItem";
            this.loadFromDiskToolStripMenuItem.Size = new System.Drawing.Size(240, 26);
            this.loadFromDiskToolStripMenuItem.Text = "Load system from disk";
            this.loadFromDiskToolStripMenuItem.Click += new System.EventHandler(this.loadFromDiskToolStripMenuItem_Click);
            // 
            // sortDataToolStripMenuItem
            // 
            this.sortDataToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.byNameToolStripMenuItem,
            this.bySizeToolStripMenuItem,
            this.byDateToolStripMenuItem});
            this.sortDataToolStripMenuItem.Name = "sortDataToolStripMenuItem";
            this.sortDataToolStripMenuItem.Size = new System.Drawing.Size(240, 26);
            this.sortDataToolStripMenuItem.Text = "Sort Data";
            // 
            // byNameToolStripMenuItem
            // 
            this.byNameToolStripMenuItem.Name = "byNameToolStripMenuItem";
            this.byNameToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.byNameToolStripMenuItem.Text = "By Name";
            this.byNameToolStripMenuItem.Click += new System.EventHandler(this.byNameToolStripMenuItem_Click);
            // 
            // bySizeToolStripMenuItem
            // 
            this.bySizeToolStripMenuItem.Name = "bySizeToolStripMenuItem";
            this.bySizeToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.bySizeToolStripMenuItem.Text = "By Size";
            this.bySizeToolStripMenuItem.Click += new System.EventHandler(this.bySizeToolStripMenuItem_Click);
            // 
            // byDateToolStripMenuItem
            // 
            this.byDateToolStripMenuItem.Name = "byDateToolStripMenuItem";
            this.byDateToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.byDateToolStripMenuItem.Text = "By Date";
            this.byDateToolStripMenuItem.Click += new System.EventHandler(this.byDateToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1063, 669);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.presentSizeText);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.createCopyToText);
            this.Controls.Add(this.createCopyOfText);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.reanameFileButton);
            this.Controls.Add(this.reanameToText);
            this.Controls.Add(this.reanameFileNameText);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.decryptKeyText);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.decryptButton);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.encryptKeyText);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.encyrptButton);
            this.Controls.Add(this.saveFileButton);
            this.Controls.Add(this.removeFileButton);
            this.Controls.Add(this.addFileButton);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.filePathSave);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.fileNameSave);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.filePathTextAdd);
            this.Controls.Add(this.fileNameAddText);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "TinyMemFS";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox fileNameAddText;
        private System.Windows.Forms.TextBox filePathTextAdd;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox fileNameSave;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox filePathSave;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button addFileButton;
        private System.Windows.Forms.Button removeFileButton;
        private System.Windows.Forms.Button saveFileButton;
        private System.Windows.Forms.Button encyrptButton;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox encryptKeyText;
        private System.Windows.Forms.TextBox decryptKeyText;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button decryptButton;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox reanameFileNameText;
        private System.Windows.Forms.TextBox reanameToText;
        private System.Windows.Forms.Button reanameFileButton;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox createCopyOfText;
        private System.Windows.Forms.TextBox createCopyToText;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox presentSizeText;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToDiskToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadFromDiskToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sortDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem byNameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bySizeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem byDateToolStripMenuItem;
    }
}

