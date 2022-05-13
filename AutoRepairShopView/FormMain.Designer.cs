
using System.Windows.Forms;

namespace RepairView
{
    partial class FormMain
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.справочникиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.компонентToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.продуктToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.СкладыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.пополнениеСкладаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.клиентыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.отчетыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.списокКомпонентовToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.компонентыПоРемонтамToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.списокЗаказовToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.buttonCreateOrder = new System.Windows.Forms.Button();
            this.buttonTakeOrderInWork = new System.Windows.Forms.Button();
            this.buttonOrderReady = new System.Windows.Forms.Button();
            this.buttonPayOrder = new System.Windows.Forms.Button();
            this.buttonRef = new System.Windows.Forms.Button();
            this.пополнкниеСкладаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.отчетыToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.списокРасходниковToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.расходникиПоРемонтуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.списокЗаказовToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.списокСкладовToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.компонентыПоСкладамToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.информацияОЗаказахToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.справочникиToolStripMenuItem,
            this.пополнкниеСкладаToolStripMenuItem,
            this.отчетыToolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(804, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // справочникиToolStripMenuItem
            // 
            this.справочникиToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.компонентToolStripMenuItem,
            this.продуктToolStripMenuItem,
            this.СкладыToolStripMenuItem});
            this.продуктToolStripMenuItem,
            this.клиентыToolStripMenuItem});
            this.справочникиToolStripMenuItem.Name = "справочникиToolStripMenuItem";
            this.справочникиToolStripMenuItem.Size = new System.Drawing.Size(94, 20);
            this.справочникиToolStripMenuItem.Text = "Справочники";
            // 
            // компонентToolStripMenuItem
            // 
            this.компонентToolStripMenuItem.Name = "компонентToolStripMenuItem";
            this.компонентToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.компонентToolStripMenuItem.Text = "Расходники";
            this.компонентToolStripMenuItem.Click += new System.EventHandler(this.КомпонентыToolStripMenuItem_Click);
            // 
            // продуктToolStripMenuItem
            // 
            this.продуктToolStripMenuItem.Name = "продуктToolStripMenuItem";
            this.продуктToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.продуктToolStripMenuItem.Text = "Ремонт";
            this.продуктToolStripMenuItem.Click += new System.EventHandler(this.ИзделияToolStripMenuItem_Click);
            // 
            // СкладыToolStripMenuItem
            // 
            this.СкладыToolStripMenuItem.Name = "СкладыToolStripMenuItem";
            this.СкладыToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.СкладыToolStripMenuItem.Text = "Склады";
            this.СкладыToolStripMenuItem.Click += new System.EventHandler(this.СкладыToolStripMenuItem_Click);
            // 
            // пополнениеСкладаToolStripMenuItem
            // 
            this.пополнениеСкладаToolStripMenuItem.Name = "пополнениеСкладаToolStripMenuItem";
            this.пополнениеСкладаToolStripMenuItem.Size = new System.Drawing.Size(129, 20);
            this.пополнениеСкладаToolStripMenuItem.Text = "Пополнение склада";
            this.пополнениеСкладаToolStripMenuItem.Click += new System.EventHandler(this.ПополнениеСкладаToolStripMenuItem_Click);
            // 
            // клиентыToolStripMenuItem
            // 
            this.клиентыToolStripMenuItem.Name = "клиентыToolStripMenuItem";
            this.клиентыToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.клиентыToolStripMenuItem.Text = "Клиенты";
            this.клиентыToolStripMenuItem.Click += new System.EventHandler(this.КлиентыToolStripMenuItem_Click);
            // 
            // отчетыToolStripMenuItem
            // 
            this.отчетыToolStripMenuItem.Name = "отчетыToolStripMenuItem";
            this.отчетыToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // списокКомпонентовToolStripMenuItem
            // 
            this.списокКомпонентовToolStripMenuItem.Name = "списокКомпонентовToolStripMenuItem";
            this.списокКомпонентовToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // компонентыПоРемонтамToolStripMenuItem
            // 
            this.компонентыПоРемонтамToolStripMenuItem.Name = "компонентыПоРемонтамToolStripMenuItem";
            this.компонентыПоРемонтамToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // списокЗаказовToolStripMenuItem
            // 
            this.списокЗаказовToolStripMenuItem.Name = "списокЗаказовToolStripMenuItem";
            this.списокЗаказовToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // dataGridView
            // 
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Location = new System.Drawing.Point(13, 28);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowTemplate.Height = 25;
            this.dataGridView.Size = new System.Drawing.Size(663, 453);
            this.dataGridView.TabIndex = 7;
            // 
            // buttonCreateOrder
            // 
            this.buttonCreateOrder.Location = new System.Drawing.Point(682, 43);
            this.buttonCreateOrder.Name = "buttonCreateOrder";
            this.buttonCreateOrder.Size = new System.Drawing.Size(100, 45);
            this.buttonCreateOrder.TabIndex = 2;
            this.buttonCreateOrder.Text = "Создать заказ";
            this.buttonCreateOrder.UseVisualStyleBackColor = true;
            this.buttonCreateOrder.Click += new System.EventHandler(this.ButtonCreateOrder_Click);
            // 
            // buttonTakeOrderInWork
            // 
            this.buttonTakeOrderInWork.Location = new System.Drawing.Point(682, 111);
            this.buttonTakeOrderInWork.Name = "buttonTakeOrderInWork";
            this.buttonTakeOrderInWork.Size = new System.Drawing.Size(100, 52);
            this.buttonTakeOrderInWork.TabIndex = 3;
            this.buttonTakeOrderInWork.Text = "Отдать на выполнение";
            this.buttonTakeOrderInWork.UseVisualStyleBackColor = true;
            this.buttonTakeOrderInWork.Click += new System.EventHandler(this.ButtonTakeOrderInWork_Click);
            // 
            // buttonOrderReady
            // 
            this.buttonOrderReady.Location = new System.Drawing.Point(682, 189);
            this.buttonOrderReady.Name = "buttonOrderReady";
            this.buttonOrderReady.Size = new System.Drawing.Size(100, 49);
            this.buttonOrderReady.TabIndex = 4;
            this.buttonOrderReady.Text = "Заказ готов";
            this.buttonOrderReady.UseVisualStyleBackColor = true;
            this.buttonOrderReady.Click += new System.EventHandler(this.ButtonOrderReady_Click);
            // 
            // buttonPayOrder
            // 
            this.buttonPayOrder.Location = new System.Drawing.Point(682, 259);
            this.buttonPayOrder.Name = "buttonPayOrder";
            this.buttonPayOrder.Size = new System.Drawing.Size(100, 49);
            this.buttonPayOrder.TabIndex = 5;
            this.buttonPayOrder.Text = "Заказ оплачен";
            this.buttonPayOrder.UseVisualStyleBackColor = true;
            this.buttonPayOrder.Click += new System.EventHandler(this.ButtonIssuedOrder_Click);
            // 
            // buttonRef
            // 
            this.buttonRef.Location = new System.Drawing.Point(682, 331);
            this.buttonRef.Name = "buttonRef";
            this.buttonRef.Size = new System.Drawing.Size(100, 46);
            this.buttonRef.TabIndex = 6;
            this.buttonRef.Text = "Обновить";
            this.buttonRef.UseVisualStyleBackColor = true;
            this.buttonRef.Click += new System.EventHandler(this.ButtonRef_Click);
            // 
            // пополнкниеСкладаToolStripMenuItem
            // 
            this.пополнкниеСкладаToolStripMenuItem.Name = "пополнкниеСкладаToolStripMenuItem";
            this.пополнкниеСкладаToolStripMenuItem.Size = new System.Drawing.Size(129, 20);
            this.пополнкниеСкладаToolStripMenuItem.Text = "Пополнкние склада";
            this.пополнкниеСкладаToolStripMenuItem.Click += new System.EventHandler(this.ПополнениеСкладаToolStripMenuItem_Click);
            // 
            // отчетыToolStripMenuItem1
            // 
            this.отчетыToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.списокРасходниковToolStripMenuItem,
            this.расходникиПоРемонтуToolStripMenuItem,
            this.списокЗаказовToolStripMenuItem1,
            this.списокСкладовToolStripMenuItem,
            this.компонентыПоСкладамToolStripMenuItem,
            this.информацияОЗаказахToolStripMenuItem});
            this.отчетыToolStripMenuItem1.Name = "отчетыToolStripMenuItem1";
            this.отчетыToolStripMenuItem1.Size = new System.Drawing.Size(60, 20);
            this.отчетыToolStripMenuItem1.Text = "Отчеты";
            // 
            // списокРасходниковToolStripMenuItem
            // 
            this.списокРасходниковToolStripMenuItem.Name = "списокРасходниковToolStripMenuItem";
            this.списокРасходниковToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
            this.списокРасходниковToolStripMenuItem.Text = "Список Расходников";
            this.списокРасходниковToolStripMenuItem.Click += new System.EventHandler(this.ComponentsToolStripMenuItem_Click);
            // 
            // расходникиПоРемонтуToolStripMenuItem
            // 
            this.расходникиПоРемонтуToolStripMenuItem.Name = "расходникиПоРемонтуToolStripMenuItem";
            this.расходникиПоРемонтуToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
            this.расходникиПоРемонтуToolStripMenuItem.Text = "Расходники по ремонту";
            this.расходникиПоРемонтуToolStripMenuItem.Click += new System.EventHandler(this.ComponentRepairsToolStripMenuItem_Click);
            // 
            // списокЗаказовToolStripMenuItem1
            // 
            this.списокЗаказовToolStripMenuItem1.Name = "списокЗаказовToolStripMenuItem1";
            this.списокЗаказовToolStripMenuItem1.Size = new System.Drawing.Size(211, 22);
            this.списокЗаказовToolStripMenuItem1.Text = "Список заказов";
            this.списокЗаказовToolStripMenuItem1.Click += new System.EventHandler(this.OrdersToolStripMenuItem_Click);
            // 
            // списокСкладовToolStripMenuItem
            // 
            this.списокСкладовToolStripMenuItem.Name = "списокСкладовToolStripMenuItem";
            this.списокСкладовToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
            this.списокСкладовToolStripMenuItem.Text = "Список складов";
            this.списокСкладовToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItemWareHouseList_Click);
            // 
            // компонентыПоСкладамToolStripMenuItem
            // 
            this.компонентыПоСкладамToolStripMenuItem.Name = "компонентыПоСкладамToolStripMenuItem";
            this.компонентыПоСкладамToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
            this.компонентыПоСкладамToolStripMenuItem.Text = "Компоненты по складам";
            this.компонентыПоСкладамToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItemWareHouseIngredients_Click);
            // 
            // информацияОЗаказахToolStripMenuItem
            // 
            this.информацияОЗаказахToolStripMenuItem.Name = "информацияОЗаказахToolStripMenuItem";
            this.информацияОЗаказахToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
            this.информацияОЗаказахToolStripMenuItem.Text = "Информация о заказах";
            this.информацияОЗаказахToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItemOrdersInfo_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(804, 505);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.buttonRef);
            this.Controls.Add(this.buttonPayOrder);
            this.Controls.Add(this.buttonOrderReady);
            this.Controls.Add(this.buttonTakeOrderInWork);
            this.Controls.Add(this.buttonCreateOrder);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormMain";
            this.Text = "Автомастерская";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem справочникиToolStripMenuItem;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Button buttonCreateOrder;
        private System.Windows.Forms.Button buttonTakeOrderInWork;
        private System.Windows.Forms.Button buttonOrderReady;
        private System.Windows.Forms.Button buttonPayOrder;
        private System.Windows.Forms.Button buttonRef;
        private System.Windows.Forms.ToolStripMenuItem компонентToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem продуктToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem СкладыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem пополнениеСкладаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem отчетыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem списокКомпонентовToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem компонентыПоРемонтамToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem списокЗаказовToolStripMenuItem;
        private ToolStripMenuItem пополнкниеСкладаToolStripMenuItem;
        private ToolStripMenuItem отчетыToolStripMenuItem1;
        private ToolStripMenuItem списокРасходниковToolStripMenuItem;
        private ToolStripMenuItem расходникиПоРемонтуToolStripMenuItem;
        private ToolStripMenuItem списокЗаказовToolStripMenuItem1;
        private ToolStripMenuItem списокСкладовToolStripMenuItem;
        private ToolStripMenuItem компонентыПоСкладамToolStripMenuItem;
        private ToolStripMenuItem информацияОЗаказахToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem клиентыToolStripMenuItem;
    }
}