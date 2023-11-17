Imports MySql.Data
Imports MySql.Data.MySqlClient
Public Class frmChangePassword
    Dim username As String, passwad As String, strsql As String, staffid As Integer
    Dim dgv As DataGridView
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnResetPassword_Click(sender As Object, e As EventArgs) Handles btnResetPassword.Click
        If NEWSYSTEMUSER = True Then
            If txtnewpassword.Text.Trim.Length > 4 Then
                If (txtnewpassword.Text.Trim = txtconfirmpassword.Text.Trim) Then
                    passwad = txtnewpassword.Text.Trim
                    strsql = "UPDATE staff SET paswad=MD5('" & passwad & "'),newuser =0 WHERE staffid=" & userid & ""
                    Dim dtaName As New MySqlClient.MySqlDataAdapter
                    dtaName.UpdateCommand = New MySqlClient.MySqlCommand
                    With dtaName.UpdateCommand
                        .Connection = Conn()
                        .CommandTimeout = 60
                        .CommandType = CommandType.Text
                        .CommandText = strsql
                        .ExecuteNonQuery()
                    End With
                    MessageBox.Show("Password changed successfully")
                    closeconn()
                    'clear data
                    clearinputs()
                    Me.Hide()
                Else
                    MessageBox.Show("Input matching password", "Change Password- Smartec POS", MessageBoxButtons.OK)
                    txtconfirmpassword.Clear()
                    txtconfirmpassword.Focus()
                    Exit Sub
                End If
            End If
        Else
            staffid = Integer.Parse(frmStaff.dgvStaff.CurrentRow.Cells(0).Value.ToString)
            If (txtnewpassword.Text.Trim = txtconfirmpassword.Text.Trim) Then
                passwad = txtnewpassword.Text.Trim
                strsql = "UPDATE staff SET paswad='" & passwad & "',newuser =1  WHERE staffid=" & staffid & ""
                Dim dtaName As New MySqlClient.MySqlDataAdapter
                dtaName.UpdateCommand = New MySqlClient.MySqlCommand
                With dtaName.UpdateCommand
                    .Connection = Conn()
                    .CommandTimeout = 60
                    .CommandType = CommandType.Text
                    .CommandText = strsql
                    .ExecuteNonQuery()
                End With
                MessageBox.Show("Password changed successfully")
                closeconn()

                'reload data
                'frmStaff.Loadstaffdatafromdatabase()

                'clear data
                clearinputs()
            End If
        End If
    End Sub

    'clear data
    Sub clearinputs()
        txtusername.Clear()
        txtnewpassword.Clear()
        txtconfirmpassword.Clear()
    End Sub

    Private Sub frmChangePassword_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim username As String = frmLogin.txtUsername.Text
        If NEWSYSTEMUSER = True Then
            txtusername.Text = username
        End If
    End Sub
End Class