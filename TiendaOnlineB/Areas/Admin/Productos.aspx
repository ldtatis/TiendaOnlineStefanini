<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Areas/Admin/Maestra.Master" CodeBehind="Productos.aspx.vb" Inherits="TiendaOnlineB.Productos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="CPHM" runat="server">
    <div class="row justify-content-center">
        <h2>Productos</h2>
    </div>
    <div class="row float-right">
        <p>
            <asp:Button Text="Nuevo" runat="server" ID="btnNuevo" CssClass="btn btn-primary plus" OnClick="btnNuevo_Click" />

        </p>
    </div>
    <asp:GridView ID="gvListar" runat="server" AllowSorting="True" AutoGenerateColumns="True" SkinID="gvGeneral">
    </asp:GridView>


</asp:Content>


