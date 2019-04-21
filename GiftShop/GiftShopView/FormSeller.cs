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
    public partial class FormSeller : Form
    {
        public int Id { set { id = value; } }

        private int? id;

        public FormSeller()
        {
            InitializeComponent();
        }
        private void FormSeller_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    SellerViewModel seller = APICustomer.GetRequest<SellerViewModel>("api/Seller/Get/" + id.Value);
                    textBoxFIO.Text = seller.SellerFIO;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxFIO.Text))
            {
                MessageBox.Show("Заполните ФИО", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                if (id.HasValue)
                {
                    APICustomer.PostRequest<SellerBindingModel,
                        bool>("api/Seller/UpdElement", new SellerBindingModel
                        {
                            Id = id.Value,
                            SellerFIO = textBoxFIO.Text
                        });
                }
                else
                {
                    APICustomer.PostRequest<SellerBindingModel,
                        bool>("api/Seller/AddElement", new SellerBindingModel
                        {
                            SellerFIO = textBoxFIO.Text
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
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
