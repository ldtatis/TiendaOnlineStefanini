Imports Negocio.DTO

Public Class ProductosNuevo
    Inherits BaseController

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim model As New ProductoXCategorias()
        model = ProducRN.ObtenerProductoXCategorias()

        Categorias.DataSource = model.Categorias.Select(Function(x) x.Categoria).ToList()
        Categorias.DataBind()

        If Categorias.Columns.Count > 0 Then
            Categorias.Columns(0).Visible = True
        Else
            Categorias.HeaderRow.Cells(0).Visible = False
            For Each gvr As GridViewRow In Categorias.Rows
                gvr.Cells(0).Visible = False
            Next
        End If

    End Sub

    Protected Sub btnCrear_Click(sender As Object, e As EventArgs)
        Try

            ProducRN.InsertarProductoXCategorias(Llenar())
            MensajeSuccess("Registro guardado.")
        Catch ex As Exception
            MensajeFile(ex.Message)
        End Try
    End Sub

    Protected Sub btnVolver_Click(sender As Object, e As EventArgs)
        Response.Redirect("Productos.aspx")
    End Sub

#Region "Métodos"
    Private Sub MensajeSuccess(ByVal sMensaje As String)
        AlertSuc.Visible = True
        lblMensaje.Text = sMensaje
        AlextFile.Visible = False
    End Sub

    Private Sub MensajeFile(ByVal sMensaje As String)
        AlextFile.Visible = True
        lblMensajDan.Text = sMensaje
        AlertSuc.Visible = False
    End Sub
    Private Function Llenar() As ProductoXCategorias
        Dim producCategoDTO As New ProductoXCategorias()

        producCategoDTO.Producto.NumeroProducto = txtNumeroProducto.Text
        producCategoDTO.Producto.Precio = txtPrecio.Text
        producCategoDTO.Producto.Titulo = txtTitulo.Text

        Return producCategoDTO
    End Function

#End Region
End Class