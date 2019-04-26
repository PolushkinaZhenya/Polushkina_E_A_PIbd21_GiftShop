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
using GiftShopServiceDAL.ViewModel;
using GiftShopServiceDAL.Interfaces;

namespace GiftShopView
{
    public partial class FormStoragesLoad : Form
    {
        public FormStoragesLoad()
        {
            InitializeComponent();
        }

        private void FormStoragesLoad_Load(object sender, EventArgs e)
        {
            try
            {
                var dict = APIClient.GetRequest<List<StoragesLoadViewModel>>("api/Record/GetStoragesLoad");
                if (dict != null)
                {
                    dataGridView.Rows.Clear();
                    foreach (var elem in dict)
                    {
                        dataGridView.Rows.Add(new object[] { elem.StorageName, "", "" });
                        foreach (var listElem in elem.Parts)
                        {
                            dataGridView.Rows.Add(new object[] { "", listElem.Item1,
                                listElem.Item2 });
                        }
                        dataGridView.Rows.Add(new object[] { "Итого", "", elem.TotalCount
                        });
                        dataGridView.Rows.Add(new object[] { });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void buttonSaveToExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "xls|*.xls|xlsx|*.xlsx"
            };
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    APIClient.PostRequest<RecordBindingModel,
                    bool>("api/Record/SaveStoragesLoad", new RecordBindingModel
                    {
                        FileName = sfd.FileName
                    });
                    MessageBox.Show("Выполнено", "Успех", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }
    }
}
