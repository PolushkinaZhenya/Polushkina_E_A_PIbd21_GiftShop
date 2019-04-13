using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GiftShopServiceDAL.BindingModel;
using GiftShopServiceDAL.Interfaces;
using GiftShopServiceDAL.ViewModel;

namespace GiftShopView
{
    public partial class FormSet : Form
    {
        public int Id { set { id = value; } }

        private int? id;

        private List<SetPartViewModel> SetParts;

        public FormSet()
        {
            InitializeComponent();
        }

        private void FormSet_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    SetViewModel set = APICustomer.GetRequest<SetViewModel>("api/Set/Get/" + id.Value);
                    textBoxName.Text = set.SetName;
                    textBoxPrice.Text = set.Price.ToString();
                    SetParts = set.SetParts;
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                }
            }
            else
            {
                SetParts = new List<SetPartViewModel>();
            }

        }

        private void LoadData()
        {
            try
            {
                if (SetParts != null)
                {
                    dataGridView.DataSource = null;
                    dataGridView.DataSource = SetParts;
                    dataGridView.Columns[0].Visible = false;
                    dataGridView.Columns[1].Visible = false;
                    dataGridView.Columns[2].Visible = false;
                    dataGridView.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var form = new FormSetPart();
            if (form.ShowDialog() == DialogResult.OK)
            {
                if (form.Model != null)
                {
                    if (id.HasValue)
                    {
                        form.Model.SetId = id.Value;
                    }
                    SetParts.Add(form.Model);
                }
                LoadData();
            }
        }

        private void buttonUpd_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                var form = new FormSetPart();
                form.Model = SetParts[dataGridView.SelectedRows[0].Cells[0].RowIndex];
                if (form.ShowDialog() == DialogResult.OK)
                {
                    SetParts[dataGridView.SelectedRows[0].Cells[0].RowIndex] = form.Model;
                    LoadData();
                }
            }
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                if (MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        SetParts.RemoveAt(dataGridView.SelectedRows[0].Cells[0].RowIndex);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    LoadData();
                }
            }
        }

        private void buttonRef_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxPrice.Text))
            {
                MessageBox.Show("Заполните цену", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (SetParts == null || SetParts.Count == 0)
            {
                MessageBox.Show("Заполните компоненты", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                List<SetPartBindingModel> setComponentBM = new List<SetPartBindingModel>();
                for (int i = 0; i < SetParts.Count; ++i)
                {
                    setComponentBM.Add(new SetPartBindingModel
                    {
                        Id = SetParts[i].Id,
                        SetId = SetParts[i].SetId,
                        PartId = SetParts[i].PartId,
                        Count = SetParts[i].Count
                    });
                }
                if (id.HasValue)
                {
                    APICustomer.PostRequest<SetBindingModel,
                        bool>("api/Set/UpdElement", new SetBindingModel
                        {
                        Id = id.Value,
                        SetName = textBoxName.Text,
                        Price = Convert.ToInt32(textBoxPrice.Text),
                        SetParts = setComponentBM
                    });
                }
                else
                {
                    APICustomer.PostRequest<SetBindingModel,
                        bool>("api/Set/UpdElement", new SetBindingModel
                        {
                        SetName = textBoxName.Text,
                        Price = Convert.ToInt32(textBoxPrice.Text),
                        SetParts = setComponentBM
                    });
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
            DialogResult = DialogResult.Cancel; Close();
        }
    }
}
