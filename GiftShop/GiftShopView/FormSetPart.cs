using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GiftShopServiceDAL.Interfaces;
using GiftShopServiceDAL.ViewModel;

namespace GiftShopView
{
    public partial class FormSetPart : Form
    {
        public SetPartViewModel Model
        {
            set
            {
                model = value;
            }
            get
            {
                return model;
            }
        }

        private SetPartViewModel model;

        public FormSetPart()
        {
            InitializeComponent();
        }

        private void FormSetPart_Load(object sender, EventArgs e)
        {
            try
            {
                List<PartViewModel> list = APICustomer.GetRequest<List<PartViewModel>>("api/Part/GetList");
                if (list != null)
                {
                    comboBoxPart.DisplayMember = "PartName";
                    comboBoxPart.ValueMember = "Id";
                    comboBoxPart.DataSource = list;
                    comboBoxPart.SelectedItem = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (model != null)
            {
                comboBoxPart.Enabled = false;
                comboBoxPart.SelectedValue = model.PartId;
                textBoxCount.Text = model.Count.ToString();
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show("Заполните поле Количество", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxPart.SelectedValue == null)
            {
                MessageBox.Show("Выберите компонент", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                if (model == null)
                {
                    model = new SetPartViewModel
                    {
                        PartId = Convert.ToInt32(comboBoxPart.SelectedValue),
                        PartName = comboBoxPart.Text,
                        Count = Convert.ToInt32(textBoxCount.Text)
                    };
                }
                else
                {
                    model.Count = Convert.ToInt32(textBoxCount.Text);
                }
                MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
