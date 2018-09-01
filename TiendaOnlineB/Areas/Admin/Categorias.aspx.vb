Imports Negocio.DTO

Public Class Categorias
    Inherits BaseController

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim List As New List(Of CategoriasDTO)
        List = CategoRN.GetCategorias()

        gv.DataSource = List
        gv.DataBind()

    End Sub

End Class