Public Class Solicitud

    '<FeCabReq> 
    '    <CantReg>int</CantReg>
    '    <PtoVta>int</PtoVta> 
    '    <CbteTipo>int</CbteTipo> 
    '</FeCabReq>
    Private _puntoDeVenta As Integer
    Private _tipoComprobante As Integer 'enum ??
    Private _listDetalles As New List(Of SolicitudDetalle)

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

    Private Sub New(ByVal pPuntoDeVenta As Integer, ByVal pTipoComprobante As Integer)
        _puntoDeVenta = pPuntoDeVenta
        _tipoComprobante = pTipoComprobante
    End Sub

    Public Shared Function CrearSolicitud(ByVal pPuntoDeVenta As Integer, ByVal pTipoComprobante As Integer) As Solicitud
        Return New Solicitud(pPuntoDeVenta, pTipoComprobante)
    End Function

    Public Function CrearDetalle() As SolicitudDetalle
        Return New SolicitudDetalle()
    End Function

    Public Sub Presentar()
        Dim objWSFEV1 As New wsfev1.ServiceSoapClient
        Dim FEAuthRequest As New wsfev1.FEAuthRequest

        FEAuthRequest.Token = ""
        FEAuthRequest.Sign = ""
        FEAuthRequest.Cuit = ""

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
                'Dim objStreamWriter As New StreamWriter("C:\WSFEV1_objFECAEResponse.xml")
                'Dim x As New XmlSerializer(objFECAEResponse.GetType)
                'x.Serialize(objStreamWriter, objFECAEResponse)
                'objStreamWriter.Close()
                'MessageBox.Show("Se generó el archivo C:\WSFEV1_objFECAEResponse.xml")
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
    End Sub



End Class
