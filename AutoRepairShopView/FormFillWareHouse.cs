using RepairBusinessLogic.BusinessLogics;
using RepairContracts.BindingModels;
using RepairContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Unity;

namespace RepairView
{
    public partial class FormFillWareHouse : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly WareHouseLogic WareHouseLogic;
        private readonly ComponentLogic componentLogic;
        public int WareHouseId { get { return Convert.ToInt32(comboBoxWareHouse.SelectedValue); } set { comboBoxWareHouse.SelectedValue = value; } }
        public int ComponentId { get { return Convert.ToInt32(comboBoxComponent.SelectedValue); } set { comboBoxComponent.SelectedValue = value; } }
        public int Count { get { return Convert.ToInt32(textBoxCount.Text); } set { textBoxCount.Text = value.ToString(); } }
        public FormFillWareHouse(WareHouseLogic logicWareHouse, ComponentLogic logicComponent)
        {
            InitializeComponent();
            WareHouseLogic = logicWareHouse;
            List<WareHouseViewModel> listWareHouse = logicWareHouse.Read(null);
            if (listWareHouse != null)
            {
                comboBoxWareHouse.DisplayMember = "WareHouseName";
                comboBoxWareHouse.ValueMember = "Id";
                comboBoxWareHouse.DataSource = listWareHouse;
                comboBoxWareHouse.SelectedItem = null;
            }
            List<ComponentViewModel> listComponent = logicComponent.Read(null);
            if (listComponent != null)
            {
                comboBoxComponent.DisplayMember = "ComponentName";
                comboBoxComponent.ValueMember = "Id";
                comboBoxComponent.DataSource = listComponent;
                comboBoxComponent.SelectedItem = null;
            }
        }
        private void ButtonFill_Click(object sender, EventArgs e)
        {
            if (textBoxCount.Text.Length == 0)
            {
                MessageBox.Show("Пустое поле Количество", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (comboBoxWareHouse.SelectedValue == null)
            {
                MessageBox.Show("Вы не выбрали склад", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (comboBoxComponent.SelectedValue == null)
            {
                MessageBox.Show("Вы не выбрали компонент", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                int count = Convert.ToInt32(textBoxCount.Text);
                if (count < 1)
                {
                    throw new Exception("Надо пополнять, а не уменьшать");
                }
                WareHouseLogic.Fill(new WareHouseBindingModel
                {
                    Id = Convert.ToInt32(comboBoxWareHouse.SelectedValue)
                }, Convert.ToInt32(comboBoxComponent.SelectedValue), Convert.ToInt32(textBoxCount.Text));
                MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}