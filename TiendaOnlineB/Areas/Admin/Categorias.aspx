<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Categorias.aspx.vb" Inherits="TiendaOnlineB.Categorias" %>

<%@ MasterType VirtualPath="~/Areas/Admin/Maestra.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="table-responsive">
        <asp:GridView ID="gv" runat="server" AllowSorting="True" AutoGenerateColumns="True" SkinID="gvGeneral" >           
        </asp:GridView>
    </div>
</asp:Content>
