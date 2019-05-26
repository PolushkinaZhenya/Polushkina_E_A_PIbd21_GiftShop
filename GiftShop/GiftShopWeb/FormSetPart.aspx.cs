using System;
using System.Collections.Generic;
using System.Web.UI;
using GiftShopServiceDAL.ViewModel;

namespace GiftShopWeb
{
    public partial class FormSetPart : System.Web.UI.Page
    {
        private SetPartViewModel model;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    List<PartViewModel> list = APIClient.GetRequest<List<PartViewModel>>("api/Part/GetList");
                    if (list != null)
                    {
                        DropDownListPart.DataSource = list;
                        DropDownListPart.DataValueField = "Id";
                        DropDownListPart.DataTextField = "PartName";
                        DropDownListPart.SelectedIndex = -1;
                        Page.DataBind();
                    }

                }
                catch (Exception ex)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('" + ex.Message + "');</script>");
                }
            }
            if (Session["SEId"] != null)
            {
                model = new SetPartViewModel
                {
                    PartId = Convert.ToInt32(Session["SEPartId"]),
                    PartName = Session["SEPartName"].ToString(),
                    Count = Convert.ToInt32(Session["SECount"].ToString())
                };
                DropDownListPart.Enabled = false;
                DropDownListPart.SelectedValue = Session["SEPartId"].ToString();
            }

            if ((Session["SEId"] != null) && (!Page.IsPostBack))
            {
                TextBoxCount.Text = Session["SECount"].ToString();
            }

        }

        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TextBoxCount.Text))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Заполните поле Количество');</script>");
                return;
            }
            if (DropDownListPart.SelectedValue == null)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Выберите компонент');</script>");
                return;
            }
            try
            {
                if (Session["SEId"] == null)
                {
                    model = new SetPartViewModel
                    {
                        PartId = Convert.ToInt32(DropDownListPart.SelectedValue),
                        PartName = DropDownListPart.SelectedItem.Text,
                        Count = Convert.ToInt32(TextBoxCount.Text)
                    };
                    Session["SEId"] = model.Id;
                    Session["SESetId"] = model.SetId;
                    Session["SEPartId"] = model.PartId;
                    Session["SEPartName"] = model.PartName;
                    Session["SECount"] = model.Count;
                }
                else
                {
                    model.Count = Convert.ToInt32(TextBoxCount.Text);
                    Session["SEId"] = model.Id;
                    Session["SEServiceId"] = model.SetId;
                    Session["SEPartId"] = model.PartId;
                    Session["SEPartName"] = model.PartName;
                    Session["SECount"] = model.Count;
                    Session["Change"] = "1";
                }
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Сохранение прошло успешно');</script>");
                Server.Transfer("FormSet.aspx");
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('" + ex.Message + "');</script>");
            }
        }

        protected void ButtonCancel_Click(object sender, EventArgs e)
        {
            Server.Transfer("FormSet.aspx");
        }
    }
}