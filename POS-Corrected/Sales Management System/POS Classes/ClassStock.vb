Imports MySql.Data
Imports MySql.Data.MySqlClient

Public Class ClassStock
    Public Sub insertStock(ByVal Barcode As String, _
                           ByVal Description As String, _
                           ByVal InStock As Double, _
                           ByVal NewStock As Double, _
                           ByVal fullnames As String, _
                           ByVal Narration As String, _
                           ByVal Selector As Integer)

        Dim strsql As String
        Dim dtaName As New MySqlClient.MySqlDataAdapter
        Try
            dtaName.InsertCommand = New MySqlClient.MySqlCommand
            With dtaName.InsertCommand
                .CommandTimeout = 60
                .Connection = Conn()
                .CommandType = CommandType.StoredProcedure
                strsql = "sp_stock"
                .CommandText = strsql
                .Parameters.AddWithValue("@inselector", Selector)
                .Parameters.AddWithValue("@inbarcode", Barcode)
                .Parameters.AddWithValue("@indesc", Description)
                .Parameters.AddWithValue("@inoldstock", InStock)
                .Parameters.AddWithValue("@innewstock", NewStock)
                .Parameters.AddWithValue("@innarration", Narration) '
                .Parameters.AddWithValue("@inuser", fullname)
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
