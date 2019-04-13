using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GiftShopServiceDAL.BindingModel;
using GiftShopServiceDAL.Interfaces;
using GiftShopServiceDAL.ViewModel;
using GiftShopServiceImplementList.Implementations;

namespace GiftShopWeb
{
    public partial class FormSet : System.Web.UI.Page
    {
        private readonly ISetService service = new SetServiceList();

        private int id;

        private List<SetPartViewModel> SetPart;

        private SetPartViewModel model;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Int32.TryParse((string)Session["id"], out id))
            {
                try
                {
                    SetViewModel view = service.GetElement(id);
                    if (view != null)
                    {
                        textBoxName.Text = view.SetName;
                        textBoxPrice.Text = view.Price.ToString();
                        this.SetPart = view.SetParts;
                        LoadData();
                    }
                }
                catch (Exception ex)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('" + ex.Message + "');</script>");
                }
            }
            else
            {
                if (service.GetList().Count == 0 || service.GetList().Last().SetName != null)
                {
                    this.SetPart = new List<SetPartViewModel>();
                    LoadData();
                }
                else
                {
                    this.SetPart = service.GetList().Last().SetParts;
                    LoadData();
                }
            }
            if (Session["SEId"] != null)
            {
                model = new SetPartViewModel
                {
                    Id = (int)Session["SEId"],
                    SetId = (int)Session["SESetId"],
                    PartId = (int)Session["SEPartId"],
                    PartName = (string)Session["SEPartName"],
                    Count = (int)Session["SECount"]
                };
                if (Session["SEIs"] != null)
                {
                    this.SetPart[(int)Session["SEIs"]] = model;
                }
                else
                {
                    this.SetPart.Add(model);
                }
            }
            List<SetPartBindingModel> commodityPart = new List<SetPartBindingModel>();
            for (int i = 0; i < this.SetPart.Count; ++i)
            {
                commodityPart.Add(new SetPartBindingModel
                {
                    Id = this.SetPart[i].Id,
                    SetId = this.SetPart[i].SetId,
                    PartId = this.SetPart[i].PartId,
                    Count = this.SetPart[i].Count
                });
            }
            if (commodityPart.Count != 0)
            {
                if (service.GetList().Count == 0 || service.GetList().Last().SetName != null)
                {
                    service.AddElement(new SetBindingModel
                    {
                        SetName = null,
                        Price = -1,
                        SetParts = commodityPart
                    });
                }
                else
                {
                    service.UpdElement(new SetBindingModel
                    {
                        Id = service.GetList().Last().Id,
                        SetName = null,
                        Price = -1,
                        SetParts = commodityPart
                    });
                }

            }
            try
            {
                if (this.SetPart != null)
                {
                    dataGridView.DataBind();
                    dataGridView.DataSource = this.SetPart;
                    dataGridView.DataBind();
                    dataGridView.ShowHeaderWhenEmpty = true;
                    dataGridView.SelectedRowStyle.BackColor = Color.Silver;
                    dataGridView.Columns[1].Visible = false;
                    dataGridView.Columns[2].Visible = false;
                    dataGridView.Columns[3].Visible = false;
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('" + ex.Message + "');</script>");
            }
            Session["SEId"] = null;
            Session["SESetId"] = null;
            Session["SEPartId"] = null;
            Session["SEPartName"] = null;
            Session["SECount"] = null;
            Session["SEIs"] = null;
        }

        private void LoadData()
        {
            try
            {
                if (SetPart != null)
                {
                    dataGridView.DataBind();
                    dataGridView.DataSource = SetPart;
                    dataGridView.DataBind();
                    dataGridView.ShowHeaderWhenEmpty = true;
                    dataGridView.SelectedRowStyle.BackColor = Color.Silver;
                    dataGridView.Columns[1].Visible = false;
                    dataGridView.Columns[2].Visible = false;
                    dataGridView.Columns[3].Visible = false;
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('" + ex.Message + "');</script>");
            }
        }

        protected void ButtonAdd_Click(object sender, EventArgs e)
        {
            Server.Transfer("FormSetPart.aspx");
        }

        protected void ButtonChange_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedIndex >= 0)
            {
                Session["SEId"] = model.Id;
                Session["SESetId"] = model.SetId;
                Session["SEPartId"] = model.PartId;
                Session["SEPartName"] = model.PartName;
                Session["SECount"] = model.Count;
                Session["SEIs"] = dataGridView.SelectedIndex;
                Server.Transfer("FormSetPart.aspx");
            }
        }

        protected void ButtonDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedIndex >= 0)
            {
                try
                {
                    SetPart.RemoveAt(dataGridView.SelectedIndex);
                }
                catch (Exception ex)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('" + ex.Message + "');</script>");
                }
                LoadData();
            }
        }

        protected void ButtonUpd_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Заполните название');</script>");
                return;
            }
            if (string.IsNullOrEmpty(textBoxPrice.Text))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Заполните цену');</script>");
                return;
            }
            if (SetPart == null || SetPart.Count == 0)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Заполните компоненты');</script>");
                return;
            }
            try
            {
                List<SetPartBindingModel> commodityPartBM = new List<SetPartBindingModel>();
                for (int i = 0; i < SetPart.Count; ++i)
                {
                    commodityPartBM.Add(new SetPartBindingModel
                    {
                        Id = SetPart[i].Id,
                        SetId = SetPart[i].SetId,
                        PartId = SetPart[i].PartId,
                        Count = SetPart[i].Count
                    });
                }
                service.DelElement(service.GetList().Last().Id);
                if (Int32.TryParse((string)Session["id"], out id))
                {
                    service.UpdElement(new SetBindingModel
                    {
                        Id = id,
                        SetName = textBoxName.Text,
                        Price = Convert.ToInt32(textBoxPrice.Text),
                        SetParts = commodityPartBM
                    });
                }
                else
                {
                    service.AddElement(new SetBindingModel
                    {
                        SetName = textBoxName.Text,
                        Price = Convert.ToInt32(textBoxPrice.Text),
                        SetParts = commodityPartBM
                    });
                }
                Session["id"] = null;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Сохранение прошло успешно');</script>");
                Server.Transfer("FormSets.aspx");
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('" + ex.Message + "');</script>");
            }
        }

        protected void ButtonCancel_Click(object sender, EventArgs e)
        {
            if (service.GetList().Count != 0 && service.GetList().Last().SetName == null)
            {
                service.DelElement(service.GetList().Last().Id);
            }
            Session["id"] = null;
            Server.Transfer("FormSets.aspx");
        }

        protected void dataGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[2].Visible = false;
            e.Row.Cells[3].Visible = false;
        }
    }
}