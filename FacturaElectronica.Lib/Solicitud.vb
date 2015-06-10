Imports Microsoft.VisualBasic.FileIO.FileSystem
Imports System.Xml
Imports System.Xml.Serialization
Imports System.IO

Public Class Solicitud

    '<FeCabReq> 
    '    <CantReg>int</CantReg>
    '    <PtoVta>int</PtoVta> 
    '    <CbteTipo>int</CbteTipo> 
    '</FeCabReq>
    Private _cuit As String
    Private _puntoDeVenta As Integer
    Private _tipoComprobante As Integer 'enum ??
    Private _listDetalles As New List(Of SolicitudDetalle)
    Private _token As String
    Private _firma As String

    Public ReadOnly Property PuntoDeVenta() As Integer
        Get
            Return _puntoDeVenta
        End Get
    End Property

    Public ReadOnly Property TipoComprobante() As Integer
        Get
            Return _tipoComprobante
        End Get
    End Property

    Public ReadOnly Property ListaDetalles() As List(Of SolicitudDetalle)
        Get
            Return _listDetalles
        End Get
    End Property

    Friend Sub New(ByVal pCuit As String, _
                    ByVal pPuntoDeVenta As Integer, _
                    ByVal pTipoComprobante As Integer, _
                    ByVal pToken As String, _
                    ByVal pFirma As String)
        _cuit = pCuit
        _puntoDeVenta = pPuntoDeVenta
        _tipoComprobante = pTipoComprobante
        _token = pToken
        _firma = pFirma
    End Sub

    'Public Shared Function CrearSolicitud(ByVal pPuntoDeVenta As Integer, _
    '                                      ByVal pTipoComprobante As Integer) As Solicitud
    '    Return New Solicitud(pPuntoDeVenta, pTipoComprobante)
    'End Function

    'Public Shared Function CrearSolicitud(ByVal pCuit As String, _
    '                                      ByVal pPuntoDeVenta As Integer, _
    '                                      ByVal pTipoComprobante As Integer, _
    '                                      ByVal pToken As String, _
    '                                      ByVal pFirma As String) As Solicitud
    '    Return New Solicitud(pCuit, pPuntoDeVenta, pTipoComprobante, pToken, pFirma)
    'End Function

    Public Function CrearDetalle() As SolicitudDetalle
        Return New SolicitudDetalle()
    End Function

    Public Function Presentar() As String
        Dim objWSFEV1 As New wsfev1.Service()
        objWSFEV1.Url = Cfg.UrlServicio()
        Dim FEAuthRequest As New wsfev1.FEAuthRequest

        FEAuthRequest.Token = _token ' "PD94bWwgdmVyc2lvbj0iMS4wIiBlbmNvZGluZz0iVVRGLTgiIHN0YW5kYWxvbmU9InllcyI/Pgo8c3NvIHZlcnNpb249IjIuMCI+CiAgICA8aWQgdW5pcXVlX2lkPSI0MjM1MDE0NzI3IiBzcmM9IkNOPXdzYWFob21vLCBPPUFGSVAsIEM9QVIsIFNFUklBTE5VTUJFUj1DVUlUIDMzNjkzNDUwMjM5IiBnZW5fdGltZT0iMTQzMzY5MTAzNiIgZXhwX3RpbWU9IjE0MzM3MzQyOTYiIGRzdD0iQ049d3NmZSwgTz1BRklQLCBDPUFSIi8+CiAgICA8b3BlcmF0aW9uIHZhbHVlPSJncmFudGVkIiB0eXBlPSJsb2dpbiI+CiAgICAgICAgPGxvZ2luIHVpZD0iQz1hciwgTz1tYXJpYW5vIHZlbmVydXMsIFNFUklBTE5VTUJFUj1DVUlUIDIwMjUzMTQwMjg0LCBDTj1hZmlwLXdzIiBzZXJ2aWNlPSJ3c2ZlIiByZWdtZXRob2Q9IjIyIiBlbnRpdHk9IjMzNjkzNDUwMjM5IiBhdXRobWV0aG9kPSJjbXMiPgogICAgICAgICAgICA8cmVsYXRpb25zPgogICAgICAgICAgICAgICAgPHJlbGF0aW9uIHJlbHR5cGU9IjQiIGtleT0iMjAyNTMxNDAyODQiLz4KICAgICAgICAgICAgPC9yZWxhdGlvbnM+CiAgICAgICAgPC9sb2dpbj4KICAgIDwvb3BlcmF0aW9uPgo8L3Nzbz4KCg=="
        FEAuthRequest.Sign = _firma ' "LCGD/l71xfK04qXhillVc9yelz0sFg8ixsQodoBcbK8InvKlkD6Qda7Jr09ng/l7F+ND7rrWilzzCGHYBJrYrn2tOAZWzLUyuBamN8Hj5T+iTGWfj4AqloecbRjxKfIrU3tGMi1PrZmYPEPvppJIHkmt+d2Xg9yOnhqdp06W+1U="
        FEAuthRequest.Cuit = _cuit

        Dim objFECAECabRequest As New wsfev1.FECAECabRequest
        Dim objFECAERequest As New wsfev1.FECAERequest
        Dim objFECAEResponse As New wsfev1.FECAEResponse
        Dim arrayFECAEDetRequest(_listDetalles.Count - 1) As wsfev1.FECAEDetRequest
        Dim _contItems As Integer = 0

        objFECAECabRequest.CantReg = _listDetalles.Count
        objFECAECabRequest.CbteTipo = _tipoComprobante
        objFECAECabRequest.PtoVta = _puntoDeVenta

        For Each it As SolicitudDetalle In _listDetalles
            Dim objFECAEDetRequest As New wsfev1.FECAEDetRequest
            With objFECAEDetRequest
                .Concepto = it.Concepto
                .DocTipo = it.DocumentoTipo
                .DocNro = it.DocumentoNro
                .CbteDesde = it.ComprobanteDesde
                .CbteHasta = it.ComprobanteHasta
                .CbteFch = it.ComprobanteFecha
                .ImpTotal = it.ImporteTotal
                .ImpTotConc = it.ImporteTotalConc
                .ImpNeto = it.ImporteNeto
                .ImpOpEx = it.ImporteOpEx
                .ImpTrib = it.ImporteTributos()
                .ImpIVA = it.ImporteIva
                .FchServDesde = it.FechaServicioDesde
                .FchServHasta = it.FechaServicioHasta
                .FchVtoPago = it.FechaServicioVtoPago
                .MonId = it.MonedaID
                .MonCotiz = it.MonedaCotizacion
            End With
            arrayFECAEDetRequest(_contItems) = objFECAEDetRequest

            _contItems += 1
        Next

        With objFECAERequest
            .FeCabReq = objFECAECabRequest
            .FeDetReq = arrayFECAEDetRequest
        End With

        ' Invoco al método FECAESolicitar
        Try
            objFECAEResponse = objWSFEV1.FECAESolicitar(FEAuthRequest, objFECAERequest)
            If objFECAEResponse IsNot Nothing Then
                'Serialize object to a text file.
                Dim sw As New StringWriter()
                Dim x As New XmlSerializer(objFECAEResponse.GetType)
                x.Serialize(sw, objFECAEResponse)
                sw.Close()

                Return sw.ToString()
            End If
            If objFECAEResponse.Errors IsNot Nothing Then
                For i = 0 To objFECAEResponse.Errors.Length - 1
                    'MessageBox.Show("objFECAEResponse.Errors(i).Code: " + objFECAEResponse.Errors(i).Code.ToString + vbCrLf +
                    '"objFECAEResponse.Errors(i).Msg: " + objFECAEResponse.Errors(i).Msg)
                Next
            End If
            If objFECAEResponse.Events IsNot Nothing Then
                For i = 0 To objFECAEResponse.Events.Length - 1
                    'MessageBox.Show("objFECAEResponse.Events(i).Code: " + objFECAEResponse.Events(i).Code.ToString + vbCrLf +
                    '"objFECAEResponse.Events(i).Msg: " + objFECAEResponse.Events(i).Msg)
                Next
            End If
        Catch ex As Exception
        End Try
    End Function



End Class
