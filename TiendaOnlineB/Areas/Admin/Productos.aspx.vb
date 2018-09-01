Imports Negocio.DTO

Public Class Productos
    Inherits BaseController

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim List As New List(Of ProductosDTO)
        List = ProducRN.GetProductos()

        gvListar.DataSource = List
        gvListar.DataBind()

        If gvListar.Columns.Count > 0 Then
            gvListar.Columns(2).Visible = True
        Else
            gvListar.HeaderRow.Cells(0).Visible = False
            For Each gvr As GridViewRow In gvListar.Rows
                gvr.Cells(0).Visible = False
            Next
        End If

    End Sub

    Protected Sub btnNuevo_Click(sender As Object, e As EventArgs)
        Response.Redirect("ProductosNuevo.aspx")
    End Sub
End Class