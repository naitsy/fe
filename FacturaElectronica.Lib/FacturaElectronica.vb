Public Class FacturaElectronica

    Private _cuit As String

    Public Sub New(ByVal pCuit As String)
        _cuit = pCuit
    End Sub

    Public Function ObtenerMonedas() As List(Of Moneda)
        '<Errors> <Err> <Code>int</Code> <Msg>string</Msg> </Err> <Err> <Code>int</Code> <Msg>string</Msg> </Err> </Errors> <Events> <Evt> <Code>int</Code> <Msg>string</Msg> </Evt> <Evt> <Code>int</Code> <Msg>string</Msg> </Evt> </Events>
        Dim objWSFEV1 As New wsfev1.Service()
        objWSFEV1.Url = Cfg.UrlServicioMonedas()
        Dim FEAuthRequest As New wsfev1.FEAuthRequest

        FEAuthRequest.Token = "PD94bWwgdmVyc2lvbj0iMS4wIiBlbmNvZGluZz0iVVRGLTgiIHN0YW5kYWxvbmU9InllcyI/Pgo8c3NvIHZlcnNpb249IjIuMCI+CiAgICA8aWQgdW5pcXVlX2lkPSIzMDQ5OTI3MTUwIiBzcmM9IkNOPXdzYWFob21vLCBPPUFGSVAsIEM9QVIsIFNFUklBTE5VTUJFUj1DVUlUIDMzNjkzNDUwMjM5IiBnZW5fdGltZT0iMTQzMzkwMTE2NiIgZXhwX3RpbWU9IjE0MzM5NDQ0MjYiIGRzdD0iQ049d3NmZSwgTz1BRklQLCBDPUFSIi8+CiAgICA8b3BlcmF0aW9uIHZhbHVlPSJncmFudGVkIiB0eXBlPSJsb2dpbiI+CiAgICAgICAgPGxvZ2luIHVpZD0iQz1hciwgTz1tYXJpYW5vIHZlbmVydXMsIFNFUklBTE5VTUJFUj1DVUlUIDIwMjUzMTQwMjg0LCBDTj1hZmlwLXdzIiBzZXJ2aWNlPSJ3c2ZlIiByZWdtZXRob2Q9IjIyIiBlbnRpdHk9IjMzNjkzNDUwMjM5IiBhdXRobWV0aG9kPSJjbXMiPgogICAgICAgICAgICA8cmVsYXRpb25zPgogICAgICAgICAgICAgICAgPHJlbGF0aW9uIHJlbHR5cGU9IjQiIGtleT0iMjAyNTMxNDAyODQiLz4KICAgICAgICAgICAgPC9yZWxhdGlvbnM+CiAgICAgICAgPC9sb2dpbj4KICAgIDwvb3BlcmF0aW9uPgo8L3Nzbz4KCg=="
        FEAuthRequest.Sign = "iMua9P90bvpWZAAOxXatjWZ62gfbXHr8vKrDXKgZTxCWpkLYoYF29dKDM4VwhwEDauR9E7ymfg67Z6BXrJNio4nrlKAk77ac2tlVi5QDvYB8t9tihoAcjbde2lsg7APCmlHYONKM01B9UKYf5O+aSpb51ucN8phNVl2a6nlyi20="
        FEAuthRequest.Cuit = _cuit

        Dim objMoneda As wsfev1.MonedaResponse
        Try
            objMoneda = objWSFEV1.FEParamGetTiposMonedas(FEAuthRequest)
            Dim _lstMonedas As List(Of Moneda) = Nothing
            If objMoneda.ResultGet.Length > 0 Then _lstMonedas = New List(Of Moneda)
            For i As Integer = 0 To objMoneda.ResultGet.Length - 1
                _lstMonedas.Add(New Moneda(objMoneda.ResultGet.GetValue(i).Id.ToString(), _
                                           objMoneda.ResultGet.GetValue(i).Desc.ToString(), _
                                           objMoneda.ResultGet.GetValue(i).FchDesde.ToString(), _
                                           objMoneda.ResultGet.GetValue(i).FchHasta.ToString()))
            Next
            Return _lstMonedas

            'If objMoneda.Errors IsNot Nothing Then
            '    For i = 0 To objMoneda.Errors.Length - 1
            '        MessageBox.Show("objMoneda.Errors(i).Code: " + objMoneda.Errors(i).Code.ToString + vbCrLf +
            '        "objMoneda.Errors(i).Msg: " + objMoneda.Errors(i).Msg)
            '    Next
            'End If
            'If objMoneda.Events IsNot Nothing Then
            '    For i = 0 To objMoneda.Events.Length - 1
            '        MessageBox.Show("objMoneda.Events(i).Code: " + objMoneda.Events(i).Code.ToString + vbCrLf +
            '        "objMoneda.Events(i).Msg: " + objMoneda.Errors(i).Msg)
            '    Next
            'End If
        Catch ex As Exception
            'MessageBox.Show(ex.Message)
        End Try
    End Function

    Public Function CrearSolicitud(ByVal pPuntoVenta As Integer,
                                   ByVal pTipoComprobante As Integer) As Solicitud
        Dim _solictud As New Solicitud(_cuit, _
                                       pPuntoVenta, _
                                       pTipoComprobante, "", "")
        Return _solictud
    End Function

End Class
