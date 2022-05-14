using RepairContracts.BindingModels;
using RepairContracts.BusinessLogicsContracts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity;

namespace RepairView
{
    public partial class FormRepairs : Form
    {
        private readonly IRepairLogic _logic;

        public FormRepairs(IRepairLogic logic)
        {
            InitializeComponent();
            _logic = logic;
        }

        private void FormRepairs_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        private void LoadData()
        {
            try
            {
                Program.ConfigGrid(_logic.Read(null), dataGridViewRepairs);
                /*
                var list = _logic.Read(null);
                if (list != null)
                {
                    dataGridViewRepairs.DataSource = list;
                    dataGridViewRepairs.Columns[0].Visible = false;
                    dataGridViewRepairs.Columns[3].Visible = false;
                    dataGridViewRepairs.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
                */
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var form = Program.Container.Resolve<FormRepair>();
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridViewRepairs.SelectedRows.Count == 1)
            {
                var form = Program.Container.Resolve<FormRepair>();
                form.Id = Convert.ToInt32(dataGridViewRepairs.SelectedRows[0].Cells[0].Value);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                }
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewRepairs.SelectedRows.Count == 1)
            {
                if (MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int id = Convert.ToInt32(dataGridViewRepairs.SelectedRows[0].Cells[0].Value);
                    try
                    {
                        _logic.Delete(new RepairBindingModel { Id = id });
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    LoadData();
                }
            }
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
