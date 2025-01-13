namespace YurtKayitSistemi
{
    partial class FrmYemekListesi
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmYemekListesi));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.yemekİdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.anaYemekDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ikinciAnaYemekDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.çorbaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tatlıDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.yanÜrünDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.yemekBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.yurtOtomasyonuDataSet7 = new YurtKayitSistemi.YurtOtomasyonuDataSet7();
            this.yemekTableAdapter = new YurtKayitSistemi.YurtOtomasyonuDataSet7TableAdapters.YemekTableAdapter();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.yemekBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.yurtOtomasyonuDataSet7)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.yemekİdDataGridViewTextBoxColumn,
            this.anaYemekDataGridViewTextBoxColumn,
            this.ikinciAnaYemekDataGridViewTextBoxColumn,
            this.çorbaDataGridViewTextBoxColumn,
            this.tatlıDataGridViewTextBoxColumn,
            this.yanÜrünDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.yemekBindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(-7, 62);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(828, 389);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // yemekİdDataGridViewTextBoxColumn
            // 
            this.yemekİdDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.yemekİdDataGridViewTextBoxColumn.DataPropertyName = "Yemekİd";
            this.yemekİdDataGridViewTextBoxColumn.HeaderText = "Yemekİd";
            this.yemekİdDataGridViewTextBoxColumn.Name = "yemekİdDataGridViewTextBoxColumn";
            this.yemekİdDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // anaYemekDataGridViewTextBoxColumn
            // 
            this.anaYemekDataGridViewTextBoxColumn.DataPropertyName = "AnaYemek";
            this.anaYemekDataGridViewTextBoxColumn.HeaderText = "AnaYemek";
            this.anaYemekDataGridViewTextBoxColumn.Name = "anaYemekDataGridViewTextBoxColumn";
            // 
            // ikinciAnaYemekDataGridViewTextBoxColumn
            // 
            this.ikinciAnaYemekDataGridViewTextBoxColumn.DataPropertyName = "İkinciAnaYemek";
            this.ikinciAnaYemekDataGridViewTextBoxColumn.HeaderText = "İkinciAnaYemek";
            this.ikinciAnaYemekDataGridViewTextBoxColumn.Name = "ikinciAnaYemekDataGridViewTextBoxColumn";
            // 
            // çorbaDataGridViewTextBoxColumn
            // 
            this.çorbaDataGridViewTextBoxColumn.DataPropertyName = "Çorba";
            this.çorbaDataGridViewTextBoxColumn.HeaderText = "Çorba";
            this.çorbaDataGridViewTextBoxColumn.Name = "çorbaDataGridViewTextBoxColumn";
            // 
            // tatlıDataGridViewTextBoxColumn
            // 
            this.tatlıDataGridViewTextBoxColumn.DataPropertyName = "Tatlı";
            this.tatlıDataGridViewTextBoxColumn.HeaderText = "Tatlı";
            this.tatlıDataGridViewTextBoxColumn.Name = "tatlıDataGridViewTextBoxColumn";
            // 
            // yanÜrünDataGridViewTextBoxColumn
            // 
            this.yanÜrünDataGridViewTextBoxColumn.DataPropertyName = "YanÜrün";
            this.yanÜrünDataGridViewTextBoxColumn.HeaderText = "YanÜrün";
            this.yanÜrünDataGridViewTextBoxColumn.Name = "yanÜrünDataGridViewTextBoxColumn";
            // 
            // yemekBindingSource
            // 
            this.yemekBindingSource.DataMember = "Yemek";
            this.yemekBindingSource.DataSource = this.yurtOtomasyonuDataSet7;
            // 
            // yurtOtomasyonuDataSet7
            // 
            this.yurtOtomasyonuDataSet7.DataSetName = "YurtOtomasyonuDataSet7";
            this.yurtOtomasyonuDataSet7.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // yemekTableAdapter
            // 
            this.yemekTableAdapter.ClearBeforeFill = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Bodoni MT Condensed", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(237, 40);
            this.label1.TabIndex = 1;
            this.label1.Text = "Aylık Yemek Listesi ";
            // 
            // FrmYemekListesi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(1033, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FrmYemekListesi";
            this.Text = "Yemek Listesi ";
            this.Load += new System.EventHandler(this.FrmYemekListesi_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.yemekBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.yurtOtomasyonuDataSet7)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private YurtOtomasyonuDataSet7 yurtOtomasyonuDataSet7;
        private System.Windows.Forms.BindingSource yemekBindingSource;
        private YurtOtomasyonuDataSet7TableAdapters.YemekTableAdapter yemekTableAdapter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn yemekİdDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn anaYemekDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ikinciAnaYemekDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn çorbaDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tatlıDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn yanÜrünDataGridViewTextBoxColumn;
    }
}