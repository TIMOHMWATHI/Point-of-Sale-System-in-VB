Imports MySql.Data
Imports MySql.Data.MySqlClient
Public Class frmSettings
    Dim yes As Integer, no As Integer
    Dim backup As New ClassBackup

    Private Sub btnSettingSave_Click(sender As Object, e As EventArgs) Handles btnSettingSave.Click
        Dim setValue As Integer
        If rdoNo.Checked = True Then
            setValue = 0
        Else
            setValue = 1
        End If
        Try
            Dim dtaName As New MySqlClient.MySqlDataAdapter
            dtaName.InsertCommand = New MySqlClient.MySqlCommand
            With dtaName.InsertCommand
                .CommandTimeout = 60
                .Connection = Conn()
                .CommandType = CommandType.StoredProcedure
                .CommandText = "sp_savesetting"
                .Parameters.AddWithValue("@inname", "outofstock")
                .Parameters.AddWithValue("@invalue", setValue)
                .ExecuteNonQuery()
            End With
            MessageBox.Show("Successfully Saved to database", "Product Color", MessageBoxButtons.OK, MessageBoxIcon.Information)
            closeconn()

        Catch ex As Exception
            If Conn.State = ConnectionState.Open Then Conn.Close()
            MessageBox.Show("An error occured while try to save data. If error persists, inform your administrator for more action", "Add color", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
            closeconn()
        End Try
    End Sub

    Private Sub btnBackupSave_Click(sender As Object, e As EventArgs) Handles btnBackupSave.Click
        Dim LocalDisk As String, DBName As String

        If txtLocalDisk.Text.ToString = String.Empty Then
            MessageBox.Show("local Disk with Backup folder missing", "Strawberry POS", MessageBoxButtons.OK)
            txtLocalDisk.Focus()
            Exit Sub
        Else
            LocalDisk = txtLocalDisk.Text.Trim.ToUpper
        End If

        'validate db name
        If txtDBName.Text.ToString = String.Empty Then
            MessageBox.Show("DB name input is missing", "Strawberry POS", MessageBoxButtons.OK)
            txtDBName.Focus()
            Exit Sub
        Else
            DBName = txtDBName.Text.Trim.ToLower
        End If

        'save to db
        backup.Set_Backup_Directory(LocalDisk, DBName, Today(), 1)

        'clear controls
        txtLocalDisk.Clear()
        txtDBName.Clear()
    End Sub
End Class