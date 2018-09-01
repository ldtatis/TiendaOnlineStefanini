<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Areas/Admin/Maestra.Master" CodeBehind="ProductosNuevo.aspx.vb" Inherits="TiendaOnlineB.ProductosNuevo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHM" runat="server">

    <div id="AlertSuc" class="alert alert-success" runat="server" visible="false" role="alert">
        <h4>Éxito</h4>
        <p>
            <asp:Label ID="lblMensaje" Text="" runat="server" />
        </p>
    </div>
    <div id="AlextFile" class="alert alert-danger" runat="server" visible="false" role="alert">
        <h4>Error!</h4>
        <p>
            <asp:Label ID="lblMensajDan" Text="" runat="server" />
        </p>
    </div>
    <div class="row justify-content-center top">
        <div class="col-md-8 col-12">
            <div class="card">
                <div class="card-header bg-primary">
                    <h4>Registrar nuevo producto</h4>
                </div>
                <div class="row">
                    <div class="col-md-6  col-12">
                        <div class="form-group">
                            <asp:Label ID="label" AssociatedControlID="txtTitulo" Text="Titulo" CssClass="control-label col-md-8" runat="server" />
                            <div class="col-md-10">
                                <asp:TextBox runat="server" class="form-control" type="text" CssClass="form-control" ID="txtTitulo" />
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6  col-12">
                        <div class="form-group">
                            <asp:Label ID="label1" AssociatedControlID="txtNumeroProducto" Text="NumeroProducto" CssClass="control-label col-md-8" runat="server" />
                            <div class="col-md-10">
                                <asp:TextBox runat="server" class="form-control" type="text" CssClass="form-control" ID="txtNumeroProducto" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6  col-12">
                        <div class="form-group">
                            <asp:Label ID="label2" AssociatedControlID="txtPrecio" Text="Precio" CssClass="control-label col-md-8" runat="server" />
                            <div class="col-md-10">
                                <asp:TextBox runat="server" class="form-control" type="text" CssClass="form-control" ID="txtPrecio" />
                            </div>
                        </div>
                    </div>
                </div>
                <asp:GridView ID="Categorias" runat="server" AllowSorting="True" AutoGenerateColumns="True" SkinID="gvGeneral">
                </asp:GridView>

                <div class="carg-footer form-group">
                    <div class="row justify-content-center">
                        <div class="col-auto">
                            <asp:Button Text="Crear" runat="server" ID="btnCrear" CssClass="btn btn-success plus" OnClick="btnCrear_Click" />
                            <asp:Button Text="Volver a la lista" runat="server" ID="btnVolver" CssClass="btn btn-info plus" OnClick="btnVolver_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
