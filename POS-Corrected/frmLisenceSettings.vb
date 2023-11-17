Imports MySql.Data
Imports MySql.Data.MySqlClient

Public Class frmLisenceSettings

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        lblSystemTime.Text = Date.Today
    End Sub

    Private Sub frmLisenceSettings_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Timer1.Start()

        'SHOW TIMER CONTROLS
        If License_Is_valid() <= 0 Then
            'show below controls
            lblSystemTime.Enabled = False
            dtpExpiryDate.Enabled = False
            btnActivate.Enabled = False
            lblSystemTime.Enabled = False
            lblExpiryDate.Visible = False
            dtpExpiryDate.Visible = False
            btnActivate.Visible = False
        Else
            'show below controls
            lblSystemTime.Enabled = True
            dtpExpiryDate.Enabled = True
            btnActivate.Enabled = True
            lblSystemTime.Enabled = True
            lblExpiryDate.Visible = True
            dtpExpiryDate.Visible = True
            btnActivate.Visible = True
        End If
    End Sub

    Private Sub btnActivate_Click(sender As Object, e As EventArgs) Handles btnActivate.Click
        ' what code is here? i wanted to set the expiry date from this button
        Dim dtaName As New MySqlClient.MySqlDataAdapter
        Try
            dtaName.UpdateCommand = New MySqlClient.MySqlCommand
            With dtaName.UpdateCommand
                .CommandTimeout = 60
                .Connection = Conn()
                .CommandType = CommandType.Text

                .CommandText = "UPDATE organisationsettings SET is_validated=0, licensekey='', " _
                               & " sysdate='" & dtpExpiryDate.Value & "' WHERE fieldname='LICENSE'"
                .ExecuteNonQuery()
                MessageBox.Show("Success", "Strawberry POS", MessageBoxButtons.OK, MessageBoxIcon.Information)

                ' Then Call this
                Check_License(mdiSales.MenuStrip1, mdiSales.MenuStrip)

                'show below controls
                mdiSales.ToolStripLabel4.Visible = True
                mdiSales.ToolStripLabel4.Text = "Expiry days remaining:" & License_Expiry_Days()
                lblSystemTime.Enabled = False
                lblExpiryDate.Visible = False
                dtpExpiryDate.Visible = False
                btnActivate.Visible = False
                Me.Hide()
            End With
            dtaName.Dispose()
            closeconn()
        Catch ex As Exception
            If Conn.State = ConnectionState.Open Then closeconn()
            MsgBox(ex.Message & " Error occured while saving data.If error persists, Inform your administrator for further action", MsgBoxStyle.Critical)
            Exit Sub
        End Try
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Dim Valid_Key As String = txtResetPassword.Text.Trim

        'validate textbox
        If (txtResetPassword.Text = String.Empty) OrElse (txtResetPassword.Text <> "c4ca4238a0b9hgvb820d234") Then
            MessageBox.Show("Invalid license", "Strawberry POS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            txtResetPassword.Focus()
            Exit Sub
        Else
            ' Update the Is_Validated Column Dim Valid_Key As String = "c4ca4238a0b9hgvb820d23409"
            Dim strsql As String
            Dim dtaName As New MySqlClient.MySqlDataAdapter
            Try
                dtaName.UpdateCommand = New MySqlClient.MySqlCommand
                With dtaName.UpdateCommand
                    .CommandTimeout = 60
                    .Connection = Conn()
                    .CommandType = CommandType.Text

                    strsql = "UPDATE organisationsettings SET is_validated=1, licensekey='" & Valid_Key & "' WHERE fieldname='LICENSE'"

                    .CommandText = strsql
                    .ExecuteNonQuery()
                End With
                MessageBox.Show("Activation successful", "Strawberry POS", MessageBoxButtons.OK, MessageBoxIcon.Information)
                'clear text box
                txtResetPassword.Clear()
                ' Then Call this
                Check_License(mdiSales.MenuStrip1, mdiSales.MenuStrip)
                'close form
                Me.Hide()

                dtaName.Dispose()
                closeconn()

            Catch ex As Exception
                If Conn.State = ConnectionState.Open Then closeconn()
                MsgBox(ex.Message & " Error occured while saving data.If error persists, Inform your administrator for further action", MsgBoxStyle.Critical)
                Exit Sub
            End Try
        End If
    End Sub
End Class