using System;
using System.Collections.Generic;
using System.Web.UI;
using GiftShopServiceDAL.BindingModel;
using GiftShopServiceDAL.Interfaces;
using GiftShopServiceDAL.ViewModel;
using GiftShopServiceImplementDataBase.Implementations;
using Unity;

namespace GiftShopWeb
{
    public partial class FormMain : System.Web.UI.Page
    {
        private readonly IMainService service = UnityConfig.Container.Resolve<MainServiceDB>();

        List<ProcedureViewModel> list;

        protected void Page_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                list = service.GetList();
                dataGridView1.Columns[0].Visible = false;
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('" + ex.Message + "');</script>");
            }
        }

        protected void ButtonCreateProcedure_Click(object sender, EventArgs e)
        {
            Server.Transfer("FormCreateProcedure.aspx");
        }

        protected void ButtonTakeProcedureInWork_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedIndex >= 0)
            {
                try
                {
                    int id = list[dataGridView1.SelectedIndex].Id;
                    service.TakeProcedureInWork(new ProcedureBindingModel { Id = id });
                    LoadData();
                    Server.Transfer("FormMain.aspx");
                }
                catch (Exception ex)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('" + ex.Message + "');</script>");
                }
            }
        }

        protected void ButtonFinishProcedure_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedIndex >= 0)
            {
                int id = list[dataGridView1.SelectedIndex].Id;
                try
                {
                    service.FinishProcedure(new ProcedureBindingModel { Id = id });
                    LoadData();
                    Server.Transfer("FormMain.aspx");
                }
                catch (Exception ex)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('" + ex.Message + "');</script>");
                }
            }
        }

        protected void ButtonProcedurePayed_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedIndex >= 0)
            {
                int id = list[dataGridView1.SelectedIndex].Id;
                try
                {
                    service.PayProcedure(new ProcedureBindingModel { Id = id });
                    LoadData();
                    Server.Transfer("FormMain.aspx");
                }
                catch (Exception ex)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('" + ex.Message + "');</script>");
                }
            }
        }

        protected void ButtonUpd_Click(object sender, EventArgs e)
        {
            LoadData();
            Server.Transfer("FormMain.aspx");
        }
    }
}