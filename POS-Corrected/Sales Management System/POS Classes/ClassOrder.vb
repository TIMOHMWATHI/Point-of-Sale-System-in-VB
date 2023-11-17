Imports MySql.Data
Imports MySql.Data.MySqlClient

Public Class ClassOrder

    Public Sub insertOrderMaster(ByVal supplierid As Integer, _
                                 ByVal dateordered As Date, _
                                 ByVal orderamount As Double)
        Dim strsql As String
        Dim dtaName As New MySqlClient.MySqlDataAdapter
        Try
            dtaName.InsertCommand = New MySqlClient.MySqlCommand
            With dtaName.InsertCommand
                .CommandTimeout = 60
                .Connection = Conn()
                .CommandType = CommandType.Text
                strsql = "insert into ordermaster(supplierid,dateordered,orderamount,placedbyid)" _
                               & " values(" & supplierid & ", '" & dateordered & "' ," & orderamount & " ,'" & userid & "')"
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

  

    Public Sub insertOrderDetails(ByVal orderno As Integer, _
                                 ByVal itemid As String, _
                                 ByVal quantityordered As Double, _
                                 ByVal unitprice As Double, _
                                 ByVal totalprice As Double)

        Dim strsql As String
        Dim dtaName As New MySqlClient.MySqlDataAdapter
        Try
            dtaName.InsertCommand = New MySqlClient.MySqlCommand
            With dtaName.InsertCommand
                .CommandTimeout = 60
                .Connection = Conn()

                .CommandType = CommandType.Text
                strsql = "insert into orderdetails(orderno,itemid,quantityordered,unitprice,totalprice)" _
                               & " values(" & orderno & ", '" & itemid & "' ," & quantityordered & " ," & unitprice & "," & totalprice & ")"
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

    Public Function getNextOrderNo() As Integer
        Dim datareader As MySqlDataReader
        Dim strsql As String = "select max(orderno) from ordermaster"
        Dim orderno As Integer = 0
        Try

            Dim cmd As New MySqlCommand(strsql, Conn)
            cmd.CommandType = CommandType.Text
            datareader = cmd.ExecuteReader
            While datareader.Read
                orderno = Integer.Parse(datareader(0).ToString)
            End While
            closeconn()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return orderno
    End Function
End Class
