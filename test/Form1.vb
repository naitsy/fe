Imports FacturaElectronica.Lib

Public Class Form1


    Private Sub btnTest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTest.Click
        Dim oSolicitud As Solicitud = Solicitud.CrearSolicitud(txtCUIT.Text.Trim(), 1, 1, txtToken.Text.Trim(), txtFirma.Text.Trim())
        Dim oDetalle As SolicitudDetalle = oSolicitud.CrearDetalle()

        With oDetalle
            .Concepto = 1
            .DocumentoTipo = 1
            .DocumentoNro = 123456789
            .ComprobanteDesde = 10
            .ComprobanteHasta = 10
            .ComprobanteFecha = "06/06/2015"
            .ImporteTotal = 1500.0
            .ImporteTotalConc = 1500.0
            .ImporteNeto = 1200.0
            .ImporteOpEx = 0
            .FechaServicioDesde = "01/06/2015" 'opcional
            .FechaServicioHasta = "05/06/2015" 'opcional
            .FechaServicioVtoPago = "06/06/2015" 'opcional
            .MonedaID = "PES"
            .MonedaCotizacion = 1

            .AgregarItemComprobantes(1, 1, 10)
            .AgregarItemIva(1, 10.5, 1500)
            .AgregarItemTributo(1, "Tributo", 10.5, 1500, 200)
        End With
        oSolicitud.ListaDetalles.Add(oDetalle)
        txtResponse.Text = oSolicitud.Presentar()


    End Sub
End Class
