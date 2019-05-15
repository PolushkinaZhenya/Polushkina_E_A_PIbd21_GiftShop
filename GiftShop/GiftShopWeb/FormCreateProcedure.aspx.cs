﻿using System;
using System.Collections.Generic;
using System.Web.UI;
using GiftShopServiceDAL.BindingModel;
using GiftShopServiceDAL.Interfaces;
using GiftShopServiceDAL.ViewModel;
using GiftShopServiceImplementDataBase.Implementations;
using Unity;

namespace GiftShopWeb
{
    public partial class FormCreateProcedure : System.Web.UI.Page
    {
        private readonly ICustomerService serviceC = UnityConfig.Container.Resolve<CustomerServiceDB>();

        private readonly ISetService serviceS = UnityConfig.Container.Resolve<SetServiceDB>();

        private readonly IMainService serviceM = UnityConfig.Container.Resolve<MainServiceDB>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    List<CustomerViewModel> listC = serviceC.GetList();
                    if (listC != null)
                    {
                        DropDownListCustomer.DataSource = listC;
                        DropDownListCustomer.DataBind();
                        DropDownListCustomer.DataTextField = "CustomerFIO";
                        DropDownListCustomer.DataValueField = "Id";
                    }
                    List<SetViewModel> listP = serviceS.GetList();
                    if (listP != null)
                    {
                        DropDownListSet.DataSource = listP;
                        DropDownListSet.DataBind();
                        DropDownListSet.DataTextField = "SetName";
                        DropDownListSet.DataValueField = "Id";
                    }
                    Page.DataBind();

                }
                catch (Exception ex)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('" + ex.Message + "');</script>");
                }
            }
        }

        private void CalcSum()
        {

            if (DropDownListSet.SelectedValue != null && !string.IsNullOrEmpty(TextBoxCount.Text))
            {
                try
                {
                    int id = Convert.ToInt32(DropDownListSet.SelectedValue);
                    SetViewModel product = serviceS.GetElement(id);
                    int count = Convert.ToInt32(TextBoxCount.Text);
                    TextBoxSum.Text = (count * product.Price).ToString();
                }
                catch (Exception ex)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('" + ex.Message + "');</script>");
                }
            }
        }

        protected void TextBoxCount_TextChanged(object sender, EventArgs e)
        {
            CalcSum();
        }

        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TextBoxCount.Text))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Заполните поле Количество');</script>");
                return;
            }
            if (DropDownListCustomer.SelectedValue == null)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Выберите клиента');</script>");
                return;
            }
            if (DropDownListSet.SelectedValue == null)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Выберите изделие');</script>");
                return;
            }
            try
            {
                serviceM.CreateProcedure(new ProcedureBindingModel
                {
                    CustomerId = Convert.ToInt32(DropDownListCustomer.SelectedValue),
                    SetId = Convert.ToInt32(DropDownListSet.SelectedValue),
                    Count = Convert.ToInt32(TextBoxCount.Text),
                    Sum = Convert.ToDecimal(TextBoxSum.Text)
                });
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Сохранение прошло успешно');</script>");
                Server.Transfer("FormMain.aspx");
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('" + ex.Message + "');</script>");
            }
        }

        protected void ButtonCancel_Click(object sender, EventArgs e)
        {
            Server.Transfer("FormMain.aspx");
        }
    }
}
