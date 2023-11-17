Imports MySql.Data
Imports MySql.Data.MySqlClient

'connects to the ReceivedStock
Public Class ClassProductsDelivery
    'INSERTS DATA TO THE DATABASE TABLE deliverymaster
    Public Sub insertDeliveryMaster(ByVal InvoiceNo As String, ByVal ReceiptNo As String, _
                                    ByVal DateReceived As Date, ByVal DeliveredBy As String, _
                                    ByVal TotalAmnt As Double, ByVal OtherNotes As String, _
                                    ByVal OderNo As Integer, ByVal MasterNo As Integer, _
                                    ByVal Selector As Integer)
        MasterNo = getMasterNo()
        Dim strsql As String
        Dim dtaName As New MySqlClient.MySqlDataAdapter
        Try
            dtaName.InsertCommand = New MySqlClient.MySqlCommand
            With dtaName.InsertCommand
                .CommandTimeout = 60
                .Connection = Conn()
                .CommandType = CommandType.StoredProcedure
                strsql = "sp_deliverymaster"
                .CommandText = strsql
                .Parameters.AddWithValue("@inselector", Selector)
                .Parameters.AddWithValue("@ininvoiceno", InvoiceNo)
                .Parameters.AddWithValue("@inreceptno", ReceiptNo)
                .Parameters.AddWithValue("@indatereceived", DateReceived)
                .Parameters.AddWithValue("@indeliveredby", DeliveredBy)
                .Parameters.AddWithValue("@intotalamnt", TotalAmnt)
                .Parameters.AddWithValue("@inothernotes", OtherNotes)
                .Parameters.AddWithValue("@inorderno", OderNo)
                .Parameters.AddWithValue("@inmasterno", MasterNo)
                .ExecuteNonQuery()
            End With
            dtaName.Dispose()
            closeconn()
        Catch ex As Exception
            If Conn.State = ConnectionState.Open Then Conn.Close()
            MsgBox(ex.Message & " Error occured while saving data.If error persists, Inform your administrator for further action", MsgBoxStyle.Critical)
            Exit Sub
        End Try
    End Sub

    'INSERTS DATA TO THE DATABASE TABLE deliverymaster
    Public Sub insertDeliverydetails(ByVal Barcode As String, ByVal TransactionNo As String, _
                                     ByVal QtyDelivered As Integer, ByVal UnitPricee As Double, _
                                     ByVal TotalPrice As Double, ByVal ItemBP As Double, _
                                     ByVal ItemsPerPack As Integer, ByVal ItemQtyTotals As Integer, _
                                     ByVal QtyVariance As Integer, ByVal PriceVariance As Double, _
                                     ByVal BatchNo As String, ByVal ExpiryDate As Date, _
                                     ByVal MadeBy As String, ByVal MadeDate As Date, _
                                     ByVal MasterNo As Integer, ByVal Selector As Integer)

        MasterNo = getMasterNo()

        Dim strsql As String
        Dim dtaName As New MySqlClient.MySqlDataAdapter
        Try
            dtaName.InsertCommand = New MySqlClient.MySqlCommand
            With dtaName.InsertCommand
                .CommandTimeout = 60
                .Connection = Conn()
                .CommandType = CommandType.StoredProcedure
                strsql = "sp_deliverydetails"
                .CommandText = strsql
                .Parameters.AddWithValue("@inbarcode", Barcode)
                .Parameters.AddWithValue("@indeliveryno", TransactionNo)
                .Parameters.AddWithValue("@inqtydelivered", QtyDelivered)
                .Parameters.AddWithValue("@inunitprice", UnitPricee)
                .Parameters.AddWithValue("@intotalprice", TotalPrice)
                .Parameters.AddWithValue("@initembp", ItemBP)
                .Parameters.AddWithValue("@initemsperpack", ItemsPerPack)
                .Parameters.AddWithValue("@inqtydeliveredtotals", ItemQtyTotals)
                .Parameters.AddWithValue("@inqtyvariance", QtyVariance)
                .Parameters.AddWithValue("@inpricevariance", PriceVariance)
                .Parameters.AddWithValue("@inbatchno", BatchNo)
                .Parameters.AddWithValue("@inexpirydate", ExpiryDate)
                .Parameters.AddWithValue("@inmadeby", MadeBy)
                .Parameters.AddWithValue("@inmakedate", MadeDate)
                .Parameters.AddWithValue("@inmasterno", MasterNo)
                .Parameters.AddWithValue("@inselector", Selector)
                .ExecuteNonQuery()
            End With
            dtaName.Dispose()
            closeconn()
        Catch ex As Exception
            If Conn.State = ConnectionState.Open Then Conn.Close()
            MsgBox(ex.Message & " Error occured while saving data.If error persists, Inform your administrator for further action", MsgBoxStyle.Critical)
            Exit Sub
        End Try
    End Sub

    'INSERTS DATA TO THE DATABASE TABLE deliverymaster
    Public Sub insertSupplierPayment(ByVal DeliveredBy As String, _
                                     ByVal Paymode As String, ByVal TransactionNo As String, _
                                     ByVal TotalAmnt As Double, ByVal AmntPaid As Double, _
                                     ByVal MasterNo As Integer, ByVal OtherNotes As String, _
                                     ByVal PayStatus As Integer, ByVal Selector As Integer)
        MasterNo = getMasterNo()
        Dim strsql As String
        Dim dtaName As New MySqlClient.MySqlDataAdapter
        Try
            dtaName.InsertCommand = New MySqlClient.MySqlCommand
            With dtaName.InsertCommand
                .CommandTimeout = 60
                .Connection = Conn()
                .CommandType = CommandType.StoredProcedure
                strsql = "sp_supplierpayment"
                .CommandText = strsql
                .Parameters.AddWithValue("@indeliveredby", DeliveredBy)
                .Parameters.AddWithValue("@inpaymode", Paymode)
                .Parameters.AddWithValue("@intransno", TransactionNo)
                .Parameters.AddWithValue("@intotalamnt", TotalAmnt)
                .Parameters.AddWithValue("@inamntpaid", AmntPaid)
                .Parameters.AddWithValue("@inmasterno", MasterNo)
                .Parameters.AddWithValue("@inothernotes", OtherNotes)
                .Parameters.AddWithValue("@inpaystatus", PayStatus)
                .Parameters.AddWithValue("@inselector", Selector)
                .ExecuteNonQuery()
            End With
            dtaName.Dispose()
            closeconn()
        Catch ex As Exception
            If Conn.State = ConnectionState.Open Then Conn.Close()
            MsgBox(ex.Message & " Error occured while saving data.If error persists, Inform your administrator for further action", MsgBoxStyle.Critical)
            Exit Sub
        End Try
    End Sub

    'Loads database table to Datagridview
    Public Sub SelectDeliveryMaster(ByVal datereceived As Date, ByVal gview As DataGridView)

        Dim strsql As String = "SELECT deliveryid,deliverynotenumber,invoicenumber,datereceived," _
                               & " deliveredby,totalamnt,vehicleregno,othernotes FROM deliverymaster" _
                               & " INNER JOIN orderdetails ON deliverymaster.orderno=orderdetails.orderno"

        Dim cmd As New MySqlCommand(strsql, Conn)
        cmd.CommandType = CommandType.Text
        Dim adapter As New MySqlDataAdapter(cmd)
        Dim table As New DataTable

        adapter.Fill(table)
        'Display the data.
        gview.DataSource = table
        adapter.Dispose()
        closeconn()
    End Sub

    'get maximum receipt number
    Public Function getMasterNo() As Integer
        Dim MasterNo As Integer = 0
        Dim datareader As MySqlDataReader
        Dim strsql As String = "SELECT MAX(deliveryid) FROM deliverymaster"
        Try
            Dim cmd As New MySqlCommand(strsql, Conn)
            cmd.CommandType = CommandType.Text
            datareader = cmd.ExecuteReader
            While datareader.Read
                MasterNo = Integer.Parse(datareader(0).ToString)
            End While
            datareader.Dispose()
            closeconn()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return MasterNo
    End Function
End Class
