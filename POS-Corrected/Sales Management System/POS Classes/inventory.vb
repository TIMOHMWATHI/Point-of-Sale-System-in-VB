Imports MySql.Data
Imports MySql.Data.MySqlClient
Public Class inventory
    Public Sub stockmanagement()
        Dim strsql As String
        Dim dtaName As New MySqlClient.MySqlDataAdapter
        Try
            dtaName.InsertCommand = New MySqlClient.MySqlCommand
            With dtaName.InsertCommand
                .CommandTimeout = 60
                .Connection = Conn()
                .CommandType = CommandType.StoredProcedure
                strsql = "sp_managestock"
                .CommandText = strsql
                .Parameters.AddWithValue("@indetermine", 0)
                .Parameters.AddWithValue("@inbarcode", "")
                .Parameters.AddWithValue("@inqtydelivered", 0)
                .Parameters.AddWithValue("@incost", 0)
                .Parameters.AddWithValue("@inuser", userid)
                .Parameters.AddWithValue("@innarration", "")
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

    Public Sub alterStock(ByVal barcode As String, _
                          ByVal stockqty As Double, _
                          ByVal determiner As Integer)

        Dim strsql As String
        Dim dtaName As New MySqlClient.MySqlDataAdapter
        Try
            dtaName.InsertCommand = New MySqlClient.MySqlCommand
            With dtaName.InsertCommand
                .CommandTimeout = 60
                .Connection = Conn()
                .CommandType = CommandType.StoredProcedure
                strsql = "sp_managestock"
                .CommandText = strsql
                .Parameters.AddWithValue("@indetermine", determiner)
                .Parameters.AddWithValue("@inbarcode", barcode)
                .Parameters.AddWithValue("@inqtydelivered", stockqty)
                .Parameters.AddWithValue("@incost", 0)
                .Parameters.AddWithValue("@inuser", userid)
                .Parameters.AddWithValue("@innarration", "")
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
    Public Sub wasteRegister(ByVal barcode As String, ByVal stockqty As Double, _
                             ByVal cost As Double, ByVal narration As String, _
                            ByVal determiner As Integer)
        Dim strsql As String
        Dim dtaName As New MySqlClient.MySqlDataAdapter
        Try
            dtaName.InsertCommand = New MySqlClient.MySqlCommand
            With dtaName.InsertCommand
                .CommandTimeout = 60
                .Connection = Conn()
                .CommandType = CommandType.StoredProcedure
                strsql = "sp_managestock"
                .CommandText = strsql
                .Parameters.AddWithValue("@indetermine", determiner)
                .Parameters.AddWithValue("@inbarcode", barcode)
                .Parameters.AddWithValue("@inqtydelivered", stockqty)
                .Parameters.AddWithValue("@incost", cost)
                .Parameters.AddWithValue("@inuser", userid)
                .Parameters.AddWithValue("@innarration", narration)
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
