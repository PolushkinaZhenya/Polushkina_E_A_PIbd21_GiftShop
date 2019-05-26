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
using GiftShopServiceImplementDataBase.Implementations;

namespace GiftShopWeb
{
    public partial class FormSet : System.Web.UI.Page
    {
        private int id;

        private List<SetPartViewModel> setParts;

        private SetPartViewModel model;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Int32.TryParse((string)Session["id"], out id))
            {
                try
                {
                    SetViewModel view = APIClient.GetRequest<SetViewModel>("api/Set/Get/" + id);
                    if (view != null)
                    {
                        if (!Page.IsPostBack)
                        {
                            textBoxName.Text = view.SetName;
                            textBoxPrice.Text = view.Price.ToString();
                        }
                        this.setParts = view.SetParts;
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
                this.setParts = new List<SetPartViewModel>();
            }
            if (Session["SEId"] != null)
            {
                if ((Session["SEIs"] != null) && (Session["Change"].ToString() != "0"))
                {
                    model = new SetPartViewModel
                    {
                        Id = (int)Session["SEId"],
                        SetId = (int)Session["SESetId"],
                        PartId = (int)Session["SEPartId"],
                        PartName = (string)Session["SEPartName"],
                        Count = (int)Session["SECount"]
                    };

                    this.setParts[(int)Session["SEIs"]] = model;
                    Session["Change"] = "0";
                }
                else
                {
                    model = new SetPartViewModel
                    {
                        SetId = (int)Session["SESetId"],
                        PartId = (int)Session["SEPartId"],
                        PartName = (string)Session["SEPartName"],
                        Count = (int)Session["SECount"]
                    };
                    this.setParts.Add(model);
                }
                Session["SEId"] = null;
                Session["SESetId"] = null;
                Session["SEPartId"] = null;
                Session["SEPartName"] = null;
                Session["SECount"] = null;
                Session["SEIs"] = null;
            }
            List<SetPartBindingModel> setPartBM = new List<SetPartBindingModel>();
            for (int i = 0; i < this.setParts.Count; ++i)
            {
                setPartBM.Add(new SetPartBindingModel
                {
                    Id = this.setParts[i].Id,
                    SetId = this.setParts[i].SetId,
                    PartId = this.setParts[i].PartId,
                    Count = this.setParts[i].Count
                });
            }
            if (setPartBM.Count != 0)
            {
                if (Int32.TryParse((string)Session["id"], out id))
                {
                    APIClient.PostRequest<SetBindingModel, bool>("api/Set/UpdElement", new SetBindingModel
                    {
                        Id = id,
                        SetName = "Введите название",
                        Price = 0,
                        SetParts = setPartBM
                    });
                }
                else
                {
                    APIClient.PostRequest<SetBindingModel, bool>("api/Set/AddElement", new SetBindingModel
                    {
                        SetName = "Введите название",
                        Price = 0,
                        SetParts = setPartBM
                    });
                    Session["id"] = APIClient.GetRequest<List<SetViewModel>>("api/Set/GetList").Last().Id.ToString();
                    Session["Change"] = "0";
                }
            }
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                if (setParts != null)
                {
                    dataGridView.DataBind();
                    dataGridView.DataSource = setParts;
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
                model = APIClient.GetRequest<SetViewModel>("api/Set/Get/" + id).SetParts[dataGridView.SelectedIndex];
                Session["SEId"] = model.Id;
                Session["SESetId"] = model.SetId;
                Session["SEPartId"] = model.PartId;
                Session["SEPartName"] = model.PartName;
                Session["SECount"] = model.Count;
                Session["SEIs"] = dataGridView.SelectedIndex;
                Session["Change"] = "0";
                Server.Transfer("FormSetPart.aspx");
            }
        }

        protected void ButtonDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedIndex >= 0)
            {
                try
                {
                    setParts.RemoveAt(dataGridView.SelectedIndex);
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
            if (setParts == null || setParts.Count == 0)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Заполните компоненты');</script>");
                return;
            }
            try
            {
                List<SetPartBindingModel> setPartBM = new List<SetPartBindingModel>();
                for (int i = 0; i < setParts.Count; ++i)
                {
                    setPartBM.Add(new SetPartBindingModel
                    {
                        Id = setParts[i].Id,
                        SetId = setParts[i].SetId,
                        PartId = setParts[i].PartId,
                        Count = setParts[i].Count
                    });
                }
                if (Int32.TryParse((string)Session["id"], out id))
                {
                    APIClient.PostRequest<SetBindingModel, bool>("api/Set/UpdElement", new SetBindingModel
                    {
                        Id = id,
                        SetName = textBoxName.Text,
                        Price = Convert.ToInt32(textBoxPrice.Text),
                        SetParts = setPartBM
                    });
                }
                else
                {
                    APIClient.PostRequest<SetBindingModel, bool>("api/Set/AddElement", new SetBindingModel
                    {
                        SetName = textBoxName.Text,
                        Price = Convert.ToInt32(textBoxPrice.Text),
                        SetParts = setPartBM
                    });
                }
                Session["id"] = null;
                Session["Change"] = null;
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
            if (APIClient.GetRequest<List<SetViewModel>>("api/Set/GetList").Count != 0 && APIClient.GetRequest<List<SetViewModel>>("api/Set/GetList").Last().SetName == null)
            {
                APIClient.PostRequest<SetBindingModel, bool>("api/Set/DelElement", new SetBindingModel { Id = APIClient.GetRequest<List<SetViewModel>>("api/Set/GetList").Last().Id });
            }
            if (!String.Equals(Session["Change"], null))
            {
                APIClient.PostRequest<SetBindingModel, bool>("api/Set/DelElement", new SetBindingModel { Id = id });
            }
            Session["id"] = null;
            Session["Change"] = null;
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