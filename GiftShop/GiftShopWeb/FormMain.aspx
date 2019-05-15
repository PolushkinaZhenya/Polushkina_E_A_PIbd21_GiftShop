<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormMain.aspx.cs" Inherits="GiftShopWeb.FormMain" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style type="text/css">
        #form1 {
            height: 666px;
            width: 1067px;
        }
    </style>
    <script src="https://unpkg.com/sweetalert/dist/sweetalert.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Menu ID="Menu" runat="server" BackColor="White" ForeColor="Black" Height="150px">
            <Items>
                <asp:MenuItem Text="Справочники" Value="Справочники">
                    <asp:MenuItem Text="Клиенты" Value="Клиенты" NavigateUrl="~/FormCustomers.aspx"></asp:MenuItem>
                    <asp:MenuItem Text="Компоненты" Value="Компоненты" NavigateUrl="~/FormParts.aspx"></asp:MenuItem>
                    <asp:MenuItem Text="Изделия" Value="Изделия" NavigateUrl="~/FormSets.aspx"></asp:MenuItem>
                    <asp:MenuItem Text="Склады" Value="Склады" NavigateUrl="~/FormStorages.aspx"></asp:MenuItem>
                </asp:MenuItem>
                <asp:MenuItem Text="Пополнить склад" Value="Пополнить склад" NavigateUrl="~/FormPutOnStorage.aspx"></asp:MenuItem>
            </Items>
        </asp:Menu>
        <asp:Button ID="ButtonCreateProcedure" runat="server" Text="Создать заказ" OnClick="ButtonCreateProcedure_Click" />
        <asp:Button ID="ButtonTakeProcedureInWork" runat="server" Text="Отдать на выполнение" OnClick="ButtonTakeProcedureInWork_Click" />
        <asp:Button ID="ButtonFinishProcedure" runat="server" Text="Заказ готов" OnClick="ButtonFinishProcedure_Click" />
        <asp:Button ID="ButtonProcedurePayed" runat="server" Text="Заказ оплачен" OnClick="ButtonProcedurePayed_Click" />
        <asp:Button ID="ButtonUpd" runat="server" Text="Обновить список" OnClick="ButtonUpd_Click" />
        <asp:GridView ID="dataGridView1" runat="server" AutoGenerateColumns="False" DataSourceID="ObjectDataSource1" ShowHeaderWhenEmpty="True">
            <Columns>
                <asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id" />
                <asp:CommandField ShowSelectButton="true" SelectText=">>" />
                <asp:BoundField DataField="CustomerFIO" HeaderText="CustomerFIO" SortExpression="CustomerFIO" />
                <asp:BoundField DataField="SetName" HeaderText="SetName" SortExpression="SetName" />
                <asp:BoundField DataField="Count" HeaderText="Count" SortExpression="Count" />
                <asp:BoundField DataField="Sum" HeaderText="Sum" SortExpression="Sum" />
                <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" />
                <asp:BoundField DataField="DateCreate" HeaderText="DateCreate" SortExpression="DateCreate" />
                <asp:BoundField DataField="DateImplement" HeaderText="DateImplement" SortExpression="DateImplement" />
            </Columns>
            <SelectedRowStyle BackColor="#CCCCCC" />
        </asp:GridView>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DeleteMethod="PayProcedure" InsertMethod="CreateProcedure" SelectMethod="GetList" TypeName="GiftShopServiceImplementDataBase.Implementations.MainServiceDB" UpdateMethod="TakeProcedureInWork">
            <DeleteParameters>
                <asp:Parameter Name="id" Type="Int32" />
            </DeleteParameters>
        </asp:ObjectDataSource>
    </form>
</body>
</html>
