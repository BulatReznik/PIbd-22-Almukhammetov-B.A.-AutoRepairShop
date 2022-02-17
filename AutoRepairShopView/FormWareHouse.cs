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
using RepairBusinessLogic.BusinessLogics;
using RepairContracts.BindingModels;
using RepairContracts.ViewModels;

namespace RepairView
{
    public partial class FormWareHouse : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        public int Id { set { id = value; } }
        private readonly WareHouseLogic logic;
        private int? id;
        private Dictionary<int, (string, int)> wareHouseComponents;
        public FormWareHouse(WareHouseLogic service)
        {
            InitializeComponent();
            logic = service;
        }

        private void FormWareHouse_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    WareHouseViewModel view = logic.Read(new WareHouseBindingModel { Id = id.Value })?[0];
                    if (view != null)
                    {
                        textBoxName.Text = view.WareHouseName;
                        textBoxResponsibleName.Text = view.ResponsibleName.ToString();
                        dateTimePickerDateCreation.Value = view.DateCreation;
                        wareHouseComponents = view.WareHouseComponents ?? new Dictionary<int, (string, int)>();
                        LoadData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                wareHouseComponents = new Dictionary<int, (string, int)>();
            }
        }
        private void LoadData()
        {
            try
            {
                if (wareHouseComponents != null)
                {
                    dataGridView.Rows.Clear();
                    foreach (var pc in wareHouseComponents)
                    {
                        dataGridView.Rows.Add(new object[] { pc.Value.Item1, pc.Value.Item2 });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ButtonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxResponsibleName.Text))
            {
                MessageBox.Show("Заполните ФИО ответственного", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                logic.CreateOrUpdate(new WareHouseBindingModel
                {
                    Id = id,
                    WareHouseName = textBoxName.Text,
                    ResponsibleName = textBoxResponsibleName.Text,
                    WareHouseComponents = wareHouseComponents ?? new Dictionary<int, (string, int)>(),
                    DateCreate = dateTimePickerDateCreation.Value
                });
                MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}