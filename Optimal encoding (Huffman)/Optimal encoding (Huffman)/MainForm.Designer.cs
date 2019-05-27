namespace Optimal_encoding__Huffman_
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.label1 = new System.Windows.Forms.Label();
            this.encodingTable = new System.Windows.Forms.DataGridView();
            this.button_LoadFile = new System.Windows.Forms.Button();
            this.button_EncodeFile = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.label_fileName = new System.Windows.Forms.Label();
            this.label_EncodedFile = new System.Windows.Forms.Label();
            this.button_decodeFile = new System.Windows.Forms.Button();
            this.symbolNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.symbol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.probability = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.codeWord = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.codeWordLength = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.encodingTable)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(88, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(630, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "Оптимальное кодирование по алгоритму Хаффмана";
            // 
            // encodingTable
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.encodingTable.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.encodingTable.BackgroundColor = System.Drawing.SystemColors.Control;
            this.encodingTable.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.ControlDark;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.encodingTable.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.encodingTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.encodingTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.symbolNum,
            this.symbol,
            this.probability,
            this.codeWord,
            this.codeWordLength});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.encodingTable.DefaultCellStyle = dataGridViewCellStyle5;
            this.encodingTable.Location = new System.Drawing.Point(12, 54);
            this.encodingTable.Name = "encodingTable";
            this.encodingTable.ReadOnly = true;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.encodingTable.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.encodingTable.RowHeadersVisible = false;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.encodingTable.RowsDefaultCellStyle = dataGridViewCellStyle7;
            this.encodingTable.RowTemplate.Height = 30;
            this.encodingTable.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.encodingTable.Size = new System.Drawing.Size(819, 363);
            this.encodingTable.TabIndex = 2;
            // 
            // button_LoadFile
            // 
            this.button_LoadFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button_LoadFile.Location = new System.Drawing.Point(12, 439);
            this.button_LoadFile.Name = "button_LoadFile";
            this.button_LoadFile.Size = new System.Drawing.Size(242, 46);
            this.button_LoadFile.TabIndex = 3;
            this.button_LoadFile.Text = "Загрузить текст из файла";
            this.button_LoadFile.UseVisualStyleBackColor = true;
            this.button_LoadFile.Click += new System.EventHandler(this.button_LoadFile_Click);
            // 
            // button_EncodeFile
            // 
            this.button_EncodeFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button_EncodeFile.Location = new System.Drawing.Point(284, 439);
            this.button_EncodeFile.Name = "button_EncodeFile";
            this.button_EncodeFile.Size = new System.Drawing.Size(239, 46);
            this.button_EncodeFile.TabIndex = 4;
            this.button_EncodeFile.Text = "Выполнить кодирование";
            this.button_EncodeFile.UseVisualStyleBackColor = true;
            this.button_EncodeFile.Click += new System.EventHandler(this.button_EncodeFile_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            this.openFileDialog.Filter = "Текстовые файлы|*.txt";
            // 
            // label_fileName
            // 
            this.label_fileName.AutoSize = true;
            this.label_fileName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_fileName.Location = new System.Drawing.Point(12, 514);
            this.label_fileName.Name = "label_fileName";
            this.label_fileName.Size = new System.Drawing.Size(274, 25);
            this.label_fileName.TabIndex = 5;
            this.label_fileName.Text = "Файл с исходным текстом:";
            // 
            // label_EncodedFile
            // 
            this.label_EncodedFile.AutoSize = true;
            this.label_EncodedFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_EncodedFile.Location = new System.Drawing.Point(12, 563);
            this.label_EncodedFile.Name = "label_EncodedFile";
            this.label_EncodedFile.Size = new System.Drawing.Size(210, 25);
            this.label_EncodedFile.TabIndex = 6;
            this.label_EncodedFile.Text = "Кодированный файл:";
            // 
            // button_decodeFile
            // 
            this.button_decodeFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button_decodeFile.Location = new System.Drawing.Point(545, 439);
            this.button_decodeFile.Name = "button_decodeFile";
            this.button_decodeFile.Size = new System.Drawing.Size(254, 46);
            this.button_decodeFile.TabIndex = 7;
            this.button_decodeFile.Text = "Выполнить декодирование";
            this.button_decodeFile.UseVisualStyleBackColor = true;
            this.button_decodeFile.Click += new System.EventHandler(this.button_decodeFile_Click);
            // 
            // symbolNum
            // 
            this.symbolNum.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.symbolNum.DefaultCellStyle = dataGridViewCellStyle3;
            this.symbolNum.Frozen = true;
            this.symbolNum.HeaderText = "№";
            this.symbolNum.Name = "symbolNum";
            this.symbolNum.ReadOnly = true;
            this.symbolNum.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.symbolNum.Width = 54;
            // 
            // symbol
            // 
            this.symbol.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.symbol.Frozen = true;
            this.symbol.HeaderText = "Символ текста";
            this.symbol.Name = "symbol";
            this.symbol.ReadOnly = true;
            this.symbol.Width = 151;
            // 
            // probability
            // 
            this.probability.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.probability.Frozen = true;
            this.probability.HeaderText = "Вероятность появления";
            this.probability.Name = "probability";
            this.probability.ReadOnly = true;
            this.probability.Width = 222;
            // 
            // codeWord
            // 
            this.codeWord.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.codeWord.Frozen = true;
            this.codeWord.HeaderText = "Кодовое слово";
            this.codeWord.Name = "codeWord";
            this.codeWord.ReadOnly = true;
            this.codeWord.Width = 151;
            // 
            // codeWordLength
            // 
            this.codeWordLength.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.codeWordLength.DefaultCellStyle = dataGridViewCellStyle4;
            this.codeWordLength.Frozen = true;
            this.codeWordLength.HeaderText = "Длина кодового слова";
            this.codeWordLength.Name = "codeWordLength";
            this.codeWordLength.ReadOnly = true;
            this.codeWordLength.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.codeWordLength.Width = 208;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(833, 620);
            this.Controls.Add(this.button_decodeFile);
            this.Controls.Add(this.label_EncodedFile);
            this.Controls.Add(this.label_fileName);
            this.Controls.Add(this.button_EncodeFile);
            this.Controls.Add(this.button_LoadFile);
            this.Controls.Add(this.encodingTable);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "Huffman encoding";
            ((System.ComponentModel.ISupportInitialize)(this.encodingTable)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView encodingTable;
        private System.Windows.Forms.Button button_LoadFile;
        private System.Windows.Forms.Button button_EncodeFile;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Label label_fileName;
        private System.Windows.Forms.Label label_EncodedFile;
        private System.Windows.Forms.Button button_decodeFile;
        private System.Windows.Forms.DataGridViewTextBoxColumn symbolNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn symbol;
        private System.Windows.Forms.DataGridViewTextBoxColumn probability;
        private System.Windows.Forms.DataGridViewTextBoxColumn codeWord;
        private System.Windows.Forms.DataGridViewTextBoxColumn codeWordLength;
    }
}

