Imports MySql.Data
Imports MySql.Data.MySqlClient

Public Class ClassSupplierPayment
    Public Sub UpdatePaymentMaster(ByVal supplierid As Integer, _
                                   ByVal TotalAmnt As Double, _
                                   ByVal Amountpaid As Double, _
                                   ByVal TransactionNo As String, _
                                   ByVal MasterNo As Integer, _
                                   ByVal Paystatus As Integer)

        Dim strsql As String
        Dim dtaName As New MySqlClient.MySqlDataAdapter
        Try
            dtaName.InsertCommand = New MySqlClient.MySqlCommand
            With dtaName.InsertCommand
                .CommandTimeout = 60
                .Connection = Conn()
                .CommandType = CommandType.Text

                If Amountpaid > 0 Then

                    strsql = "UPDATE supplierpayments SET amountpaid = amountpaid + " & Amountpaid & "," _
                             & " paystatus=" & Paystatus & " WHERE transactionno='" & TransactionNo & "' AND " _
                             & " suppliedby=" & supplierid & " AND masterno=" & MasterNo & " AND paystatus!=1"

                    .CommandText = strsql
                End If
                .ExecuteNonQuery()
            End With
            dtaName.Dispose()
            closeconn()
        Catch ex As Exception
            If Conn.State = ConnectionState.Open Then closeconn()
            MsgBox(ex.Message & " Error occured while saving data.If error persists, Inform your administrator for further action", MsgBoxStyle.Critical)
            Exit Sub
        End Try
    End Sub

    Public Sub insertPayNarration(ByVal MasterNo As Integer, _
                                  ByVal Amountpaid As Double, _
                                  ByVal Paymode As String, _
                                  ByVal Bank As String, _
                                  ByVal TransactionCode As String, _
                                  ByVal PayDate As Date, _
                                  ByVal Othernotes As String, _
                                  ByVal Inuser As Integer)
        Dim strsql As String
        Dim dtaName As New MySqlClient.MySqlDataAdapter
        Try
            dtaName.InsertCommand = New MySqlClient.MySqlCommand
            With dtaName.InsertCommand
                .CommandTimeout = 60
                .Connection = Conn()
                .CommandType = CommandType.Text

                strsql = "INSERT INTO supplierpaynarration(masterno,amountpaid,paymode," _
                         & " bank,transactioncode,paydate,othernotes,servedby)" _
                         & " VALUES (" & MasterNo & "," & Amountpaid & "," _
                         & " '" & Paymode & "','" & Bank & "','" & TransactionCode & "'," _
                         & " '" & PayDate & "','" & Othernotes & "'," & Inuser & ")"

                .CommandText = strsql
                .ExecuteNonQuery()
            End With
            dtaName.Dispose()
            closeconn()
        Catch ex As Exception
            If Conn.State = ConnectionState.Open Then closeconn()
            MsgBox(ex.Message & " Error occured while saving data.If error persists, Inform your administrator for further action", MsgBoxStyle.Critical)
            Exit Sub
        End Try
    End Sub
End Class
