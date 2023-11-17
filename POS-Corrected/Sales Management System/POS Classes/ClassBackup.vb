Imports MySql.Data
Imports MySql.Data.MySqlClient

Public Class ClassBackup
    Public Sub Set_Backup_Directory(ByVal LocalDisk As String, _
                                    ByVal DBName As String, _
                                    ByVal DateSet As Date, _
                                    ByVal status As Integer)
        Dim strsql As String
        Dim dtaName As New MySqlClient.MySqlDataAdapter
        Try
            dtaName.InsertCommand = New MySqlClient.MySqlCommand
            With dtaName.InsertCommand
                .CommandTimeout = 60
                .Connection = Conn()
                .CommandType = CommandType.Text

                strsql = "UPDATE backupdirectory SET status =0 WHERE status=1"

                .CommandText = strsql
                .ExecuteNonQuery()

                strsql = "INSERT INTO backupdirectory (localdisk,databasename,dateset,status)" _
                         & " VALUES('" & LocalDisk & "','" & DBName & "','" & DateSet & "'," & status & ")"

                .CommandText = strsql
                .ExecuteNonQuery()
                MessageBox.Show("Success", "Strawberry POS", MessageBoxButtons.OK, MessageBoxIcon.Information)
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
