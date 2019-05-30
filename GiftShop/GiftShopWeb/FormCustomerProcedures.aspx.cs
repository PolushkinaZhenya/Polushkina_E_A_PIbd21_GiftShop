using System;
using Microsoft.Reporting.WebForms;
using System.Web.UI;
using Unity;
using GiftShopServiceDAL.Interfaces;
using GiftShopServiceImplementDataBase.Implementations;
using GiftShopServiceDAL.BindingModel;

namespace GiftShopWeb
{
    public partial class FormCustomerProcedures : System.Web.UI.Page
    {
        private readonly IRecordService service = UnityConfig.Container.Resolve<RecordServiceDB>();

        protected void ButtonMake_Click(object sender, EventArgs e)
        {
            if (Calendar1.SelectedDate >= Calendar2.SelectedDate)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "ScriptAllertDate", "<script>alert('Дата начала должна быть меньше даты окончания');</script>");
                return;
            }
            try
            {
                var dataSource = service.GetCustomerProcedures(new RecordBindingModel
                {
                    DateFrom = Calendar1.SelectedDate,
                    DateTo = Calendar2.SelectedDate
                });
                ReportDataSource source = new ReportDataSource("DataSetProcedures", dataSource);
                ReportViewer1.LocalReport.DataSources.Add(source);
                ReportViewer1.DataBind();
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "ScriptAllert", "<script>alert('" + ex.Message + "');</script>");
            }
        }

        protected void ButtonToPdf_Click(object sender, EventArgs e)
        {
            string path = "C:\\Users\\Евгения\\Desktop\\CustomerProcedures.pdf";
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("Content-Disposition", "filename=CustomerProcedures.pdf");
            Response.ContentType = "application/vnd.ms-word";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            try
            {
                service.SaveCustomerProcedures(new RecordBindingModel
                {
                    FileName = path,
                    DateFrom = Calendar1.SelectedDate,
                    DateTo = Calendar2.SelectedDate
                });
                Response.WriteFile(path);
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "ScriptAllert", "<script>alert('" + ex.Message + "');</script>");
            }
            Response.End();
        }

        protected void ButtonBack_Click(object sender, EventArgs e)
        {
            Server.Transfer("FormMain.aspx");
        }
    }
}