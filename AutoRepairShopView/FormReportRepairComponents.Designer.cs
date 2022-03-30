
namespace RepairView
{
    partial class FormReportRepairComponents
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
            this.ButtonSaveToExcel = new System.Windows.Forms.Button();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.Component = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Repair = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Count = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // ButtonSaveToExcel
            // 
            this.ButtonSaveToExcel.Location = new System.Drawing.Point(195, 12);
            this.ButtonSaveToExcel.Name = "ButtonSaveToExcel";
            this.ButtonSaveToExcel.Size = new System.Drawing.Size(127, 25);
            this.ButtonSaveToExcel.TabIndex = 0;
            this.ButtonSaveToExcel.Text = "Сохранить в Excel";
            this.ButtonSaveToExcel.UseVisualStyleBackColor = true;
            this.ButtonSaveToExcel.Click += new System.EventHandler(this.ButtonSaveToExcel_Click);
            // 
            // dataGridView
            // 
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Component,
            this.Repair,
            this.Count});
            this.dataGridView.Location = new System.Drawing.Point(1, 43);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowTemplate.Height = 25;
            this.dataGridView.Size = new System.Drawing.Size(519, 489);
            this.dataGridView.TabIndex = 1;
            // 
            // Component
            // 
            this.Component.HeaderText = "Ремонт";
            this.Component.Name = "Component";
            // 
            // Repair
            // 
            this.Repair.HeaderText = "Расходник";
            this.Repair.Name = "Repair";
            // 
            // Count
            // 
            this.Count.HeaderText = "Количество";
            this.Count.Name = "Count";
            // 
            // FormReportRepairComponents
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(517, 533);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.ButtonSaveToExcel);
            this.Name = "FormReportRepairComponents";
            this.Text = "Расходники по ремонту";
            this.Load += new System.EventHandler(this.FormReportRepairComponents_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button ButtonSaveToExcel;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn Component;
        private System.Windows.Forms.DataGridViewTextBoxColumn Repair;
        private System.Windows.Forms.DataGridViewTextBoxColumn Count;
    }
}