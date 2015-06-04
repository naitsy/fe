Imports FacturaElectronica.Lib

Public Class Form1


    Private Sub btnTest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTest.Click
        Dim ff As New FacturaFactory(1, 2)
        Dim factura As Factura = ff.CrearFactura()

    End Sub
End Class
