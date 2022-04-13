
namespace RepairView
{
    partial class FormImplementers
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
            this.ButtonUpd = new System.Windows.Forms.Button();
            this.ButtonDel = new System.Windows.Forms.Button();
            this.ButtonRef = new System.Windows.Forms.Button();
            this.ButtonAdd = new System.Windows.Forms.Button();
            this.dataGridViewImplementer = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewImplementer)).BeginInit();
            this.SuspendLayout();
            // 
            // ButtonUpd
            // 
            this.ButtonUpd.Location = new System.Drawing.Point(664, 327);
            this.ButtonUpd.Name = "ButtonUpd";
            this.ButtonUpd.Size = new System.Drawing.Size(111, 65);
            this.ButtonUpd.TabIndex = 7;
            this.ButtonUpd.Text = "Обновить";
            this.ButtonUpd.UseVisualStyleBackColor = true;
            this.ButtonUpd.Click += new System.EventHandler(this.buttonUpdate_Click);
            // 
            // ButtonDel
            // 
            this.ButtonDel.Location = new System.Drawing.Point(664, 226);
            this.ButtonDel.Name = "ButtonDel";
            this.ButtonDel.Size = new System.Drawing.Size(111, 67);
            this.ButtonDel.TabIndex = 6;
            this.ButtonDel.Text = "Удалить";
            this.ButtonDel.UseVisualStyleBackColor = true;
            this.ButtonDel.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // ButtonRef
            // 
            this.ButtonRef.Location = new System.Drawing.Point(664, 130);
            this.ButtonRef.Name = "ButtonRef";
            this.ButtonRef.Size = new System.Drawing.Size(111, 69);
            this.ButtonRef.TabIndex = 5;
            this.ButtonRef.Text = "Изменить";
            this.ButtonRef.UseVisualStyleBackColor = true;
            this.ButtonRef.Click += new System.EventHandler(this.buttonChange_Click);
            // 
            // ButtonAdd
            // 
            this.ButtonAdd.Location = new System.Drawing.Point(664, 32);
            this.ButtonAdd.Name = "ButtonAdd";
            this.ButtonAdd.Size = new System.Drawing.Size(111, 60);
            this.ButtonAdd.TabIndex = 4;
            this.ButtonAdd.Text = "Добавить";
            this.ButtonAdd.UseVisualStyleBackColor = true;
            this.ButtonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // dataGridViewImplementer
            // 
            this.dataGridViewImplementer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewImplementer.Location = new System.Drawing.Point(12, 12);
            this.dataGridViewImplementer.Name = "dataGridViewImplementer";
            this.dataGridViewImplementer.RowTemplate.Height = 25;
            this.dataGridViewImplementer.Size = new System.Drawing.Size(622, 426);
            this.dataGridViewImplementer.TabIndex = 8;
            // 
            // FormImplementers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dataGridViewImplementer);
            this.Controls.Add(this.ButtonUpd);
            this.Controls.Add(this.ButtonDel);
            this.Controls.Add(this.ButtonRef);
            this.Controls.Add(this.ButtonAdd);
            this.Name = "FormImplementers";
            this.Text = "Исполнители";
            this.Load += new System.EventHandler(this.FormImplementers_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewImplementer)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button ButtonUpd;
        private System.Windows.Forms.Button ButtonDel;
        private System.Windows.Forms.Button ButtonRef;
        private System.Windows.Forms.Button ButtonAdd;
        private System.Windows.Forms.DataGridView dataGridViewImplementer;
    }
}