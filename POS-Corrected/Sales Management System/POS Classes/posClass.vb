Imports MySql.Data
Imports MySql.Data.MySqlClient
Public Class posClass
    Public Sub comitSales(ByVal TotalSale As Double, _
                          ByVal AmountPaid As Double, _
                          ByVal CustomerChange As Double, _
                          ByVal PurchaseMode As String, _
                          ByVal paydet As String, _
                          ByVal VAT As Double, _
                          ByVal Purmode As String, _
                          ByVal fullypaid As Integer, _
                          ByVal Barcode As String, _
                          ByVal BP As Double, _
                          ByVal SP As Double, _
                          ByVal Qty As Double, _
                          ByVal Disc As Double, _
                          ByVal SalesMode As String, _
                          ByVal PayMode As String, _
                          ByVal TransactionNo As String, _
                          ByVal Item_VAT As Double, _
                          ByVal SaleReturns As Double, _
                          ByVal selector As Integer)

        Dim strsql As String
        Dim dtaName As New MySqlClient.MySqlDataAdapter
        Try
            dtaName.InsertCommand = New MySqlClient.MySqlCommand
            With dtaName.InsertCommand
                .CommandTimeout = 60
                .Connection = Conn()
                .CommandType = CommandType.StoredProcedure
                strsql = "sp_managepos"
                .CommandText = strsql
                .Parameters.AddWithValue("@inselector", selector)
                .Parameters.AddWithValue("@intotalsale", TotalSale)
                .Parameters.AddWithValue("@inamountpaid", AmountPaid)
                .Parameters.AddWithValue("@inamountreturned", CustomerChange)
                .Parameters.AddWithValue("@inpurchasemode", PurchaseMode)
                .Parameters.AddWithValue("@inpaydet", paydet)
                .Parameters.AddWithValue("@inuser", userid)
                .Parameters.AddWithValue("@invat", VAT)
                .Parameters.AddWithValue("@inpurmode", Purmode)
                .Parameters.AddWithValue("@infullypaiid", fullypaid)
                .Parameters.AddWithValue("@inbarcode", Barcode)
                .Parameters.AddWithValue("@inbp", BP)
                .Parameters.AddWithValue("@insp", SP)
                .Parameters.AddWithValue("@inqty", Qty)
                .Parameters.AddWithValue("@inrecno", 0)
                .Parameters.AddWithValue("@insalesmode", SalesMode)
                .Parameters.AddWithValue("@indisc", Disc)
                .Parameters.AddWithValue("@inpaymode", PayMode)
                .Parameters.AddWithValue("@initemvat", Item_VAT)
                .Parameters.AddWithValue("@insalereturns", SaleReturns)
                .Parameters.AddWithValue("@intransactionno", TransactionNo)
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

    Public Sub creditPayment(ByVal amountpaid As Double, _
                             ByVal paymode As String, _
                             ByVal paydet As String, _
                             ByVal purmode As String, _
                             ByVal fullypaid As Integer, _
                             ByVal selector As Integer, _
                             ByVal recno As Integer, _
                             ByVal fileNo As Integer, _
                             ByVal disc As Double, _
                             ByVal salesMode As String)

        Dim strsql As String
        Dim dtaName As New MySqlClient.MySqlDataAdapter
        Try
            dtaName.InsertCommand = New MySqlClient.MySqlCommand
            With dtaName.InsertCommand
                .CommandTimeout = 60
                .Connection = Conn()
                .CommandType = CommandType.StoredProcedure
                strsql = "sp_managepos"
                .CommandText = strsql
                .Parameters.AddWithValue("@inselector", selector)
                .Parameters.AddWithValue("@intotalsale", 0)
                .Parameters.AddWithValue("@inamountpaid", amountpaid)
                .Parameters.AddWithValue("@inamountreturned", 0)
                .Parameters.AddWithValue("@inpurchasemode", "")
                .Parameters.AddWithValue("@inpaymode", paymode)
                .Parameters.AddWithValue("@inpaydet", paydet)
                .Parameters.AddWithValue("@inuser", userid)
                .Parameters.AddWithValue("@invat", 0)
                .Parameters.AddWithValue("@inpurmode", purmode)
                .Parameters.AddWithValue("@infullypaiid", fullypaid)
                .Parameters.AddWithValue("@inbarcode", "")
                .Parameters.AddWithValue("@inbp", 0)
                .Parameters.AddWithValue("@insp", fileNo)
                .Parameters.AddWithValue("@inqty", 0)
                .Parameters.AddWithValue("@inrecno", recno)
                .Parameters.AddWithValue("@indisc", disc)
                .Parameters.AddWithValue("@intransactionno", "")
                .Parameters.AddWithValue("@initemvat", 0)
                .Parameters.AddWithValue("@insalereturns", 0)
                .Parameters.AddWithValue("@insalesmode", salesMode)
                .ExecuteNonQuery()
            End With
            dtaName.Dispose()
            closeconn()
        Catch ex As Exception
            If Conn.State = ConnectionState.Open Then Conn.Close()
            MsgBox(ex.Message & " Error occured while saving data.If error persists," _
                   & " Inform your administrator for further action", MsgBoxStyle.Critical)
        End Try
    End Sub

    Public Function setSalesReturns(ByVal valueReturned As Double, _
                                    ByVal qtyReturned As Double, _
                                    ByVal sp As Double, _
                                    ByVal disc As Double, _
                                    ByVal vat As Double, _
                                    ByVal barcode As String, _
                                    ByVal recNo As String, _
                                    ByVal salesMode As String, _
                                    ByVal selector As Integer) As Integer
        Dim strsql As String
        Dim dtaName As New MySqlClient.MySqlDataAdapter
        Dim success As Integer = 0
        Try
            dtaName.InsertCommand = New MySqlClient.MySqlCommand
            With dtaName.InsertCommand
                .CommandTimeout = 60
                .Connection = Conn()
                .CommandType = CommandType.StoredProcedure
                strsql = "sp_managepos"
                .CommandText = strsql
                .Parameters.AddWithValue("@inselector", selector)
                .Parameters.AddWithValue("@inpurchasemode", "")
                .Parameters.AddWithValue("@intotalsale", 0)
                .Parameters.AddWithValue("@inamountpaid", valueReturned)
                .Parameters.AddWithValue("@inamountreturned", 0)
                .Parameters.AddWithValue("@inpaymode", "")
                .Parameters.AddWithValue("@inpaydet", "")
                .Parameters.AddWithValue("@inuser", userid)
                .Parameters.AddWithValue("@indisc", disc)
                .Parameters.AddWithValue("@invat", vat)
                .Parameters.AddWithValue("@inpurmode", "")
                .Parameters.AddWithValue("@infullypaiid", 0)
                .Parameters.AddWithValue("@inbarcode", barcode)
                .Parameters.AddWithValue("@inbp", 0)
                .Parameters.AddWithValue("@insp", sp)
                .Parameters.AddWithValue("@inqty", qtyReturned)
                .Parameters.AddWithValue("@inrecno", recNo)
                .Parameters.AddWithValue("@intransactionno", "")
                .Parameters.AddWithValue("@initemvat", 0)
                .Parameters.AddWithValue("@insalereturns", valueReturned)
                .Parameters.AddWithValue("@insalesmode", "")
                success = .ExecuteNonQuery()
            End With
            dtaName.Dispose()
            closeconn()
        Catch ex As Exception
            If Conn.State = ConnectionState.Open Then Conn.Close()
            MsgBox(ex.Message & " Error occured while saving data.If error persists, Inform your administrator for further action", MsgBoxStyle.Critical)
        End Try
        Return success
    End Function

    Public Sub Insert_To_SalesPaymode(ByVal receiptNo As Integer, _
                                    ByVal Paymode As String, _
                                    ByVal TransactionNo As String, _
                                    ByVal AmountPaid As Double)

        Dim strsql As String
        Dim dtaName As New MySqlClient.MySqlDataAdapter
        Try
            dtaName.InsertCommand = New MySqlClient.MySqlCommand
            With dtaName.InsertCommand
                .CommandTimeout = 60
                .Connection = Conn()
                .CommandType = CommandType.Text
                strsql = " INSERT INTO salespaymode (receiptno,paymode,transactionno,amountpaid)" _
                         & " VALUES(" & receiptNo & ",'" & Paymode & "','" & TransactionNo & "'," & AmountPaid & ") "

                .CommandText = strsql
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

    Public Sub creditupdationdetails(ByVal receiptNo As Integer, _
                                     ByVal CustomerNo As String, _
                                     ByVal updatecredit As Double, _
                                     ByVal datepaid As Date)

        Dim strsql As String
        Dim dtaName As New MySqlClient.MySqlDataAdapter
        Try
            dtaName.InsertCommand = New MySqlClient.MySqlCommand
            With dtaName.InsertCommand
                .CommandTimeout = 60
                .Connection = Conn()
                .CommandType = CommandType.Text
                strsql = " INSERT INTO creditpaynarration (receiptno,customerno,amountpaid,datepaid)" _
                         & " VALUES (" & receiptNo & ",'" & CustomerNo & "'," & updatecredit & ", NOW()) "

                .CommandText = strsql
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
End Class
