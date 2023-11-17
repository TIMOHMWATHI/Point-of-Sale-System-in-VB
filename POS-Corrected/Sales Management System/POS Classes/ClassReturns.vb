Imports MySql.Data
Imports MySql.Data.MySqlClient

Public Class ClassReturns
    Public Sub updateSalesMaster(ByVal receiptNo As Integer, _
                               ByVal total As Double)
        Dim strsql As String
        Dim dtaName As New MySqlClient.MySqlDataAdapter
        Try
            dtaName.UpdateCommand = New MySqlClient.MySqlCommand
            With dtaName.UpdateCommand
                .CommandTimeout = 60
                .Connection = Conn()

                .CommandType = CommandType.Text

                strsql = ""

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

    Public Sub UpdateSalesDetails(ByVal receiptNo As Integer, _
                             ByVal total As Double)
        Dim strsql As String
        Dim dtaName As New MySqlClient.MySqlDataAdapter
        Try
            dtaName.UpdateCommand = New MySqlClient.MySqlCommand
            With dtaName.UpdateCommand
                .CommandTimeout = 60
                .Connection = Conn()
                .CommandType = CommandType.Text

                strsql = ""

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

    Public Sub UpdateCreditDetails(ByVal receiptNo As Integer, _
                             ByVal total As Double)
        Dim strsql As String
        Dim dtaName As New MySqlClient.MySqlDataAdapter
        Try
            dtaName.UpdateCommand = New MySqlClient.MySqlCommand
            With dtaName.UpdateCommand
                .CommandTimeout = 60
                .Connection = Conn()

                .CommandType = CommandType.Text
                strsql = ""

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
