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
using Microsoft.Reporting.WinForms;

namespace GiftShopView
{
    public partial class FormCustomerProcedures : Form
    {
        public FormCustomerProcedures()
        {
            InitializeComponent();
        }

        private void buttonMake_Click(object sender, EventArgs e)
        {
            if (dateTimePickerFrom.Value.Date >= dateTimePickerTo.Value.Date)
            {
                MessageBox.Show("Дата начала должна быть меньше даты окончания",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                ReportParameter parameter = new ReportParameter("RecordParameterPeriod",
                    "c " +
                    dateTimePickerFrom.Value.ToShortDateString() +
                    " по " +
                    dateTimePickerTo.Value.ToShortDateString());
                recordViewer.LocalReport.SetParameters(parameter);

                List<CustomerProceduresModel> response =
                    APICustomer.PostRequest<RecordBindingModel,
                    List<CustomerProceduresModel>>("api/Record/GetCustomerProcedures", new RecordBindingModel
                    {
                        DateFrom = dateTimePickerFrom.Value,
                        DateTo = dateTimePickerTo.Value
                    });

                ReportDataSource source = new ReportDataSource("DataSetProcedures", response);
                recordViewer.LocalReport.DataSources.Add(source);
                recordViewer.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonToPdf_Click(object sender, EventArgs e)
        {
            if (dateTimePickerFrom.Value.Date >= dateTimePickerTo.Value.Date)
            {
                MessageBox.Show("Дата начала должна быть меньше даты окончания",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "pdf|*.pdf"
            };
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    APICustomer.PostRequest<RecordBindingModel,
                        bool>("api/Record/SaveCustomerProcedures", new RecordBindingModel
                        {
                            FileName = sfd.FileName,
                            DateFrom = dateTimePickerFrom.Value,
                            DateTo = dateTimePickerTo.Value
                        });
                    MessageBox.Show("Выполнено", "Успех",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
