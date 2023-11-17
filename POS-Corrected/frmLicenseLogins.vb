Imports MySql.Data
Imports MySql.Data.MySqlClient

Public Class frmLicenseLogins

    Dim Valid_Password As String = "@madeinmanhattan"

    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        'if passwords dont match exit
        If (txtPassword.Text.Trim <> Valid_Password) OrElse (txtPassword.Text.Trim <> txtConfirmPassword.Text.Trim) Then
            MessageBox.Show("Sorry, password is incorrect or not matching password", "Strawberry POS", MessageBoxButtons.OK)
            txtPassword.Clear()
            txtConfirmPassword.Clear()
            txtPassword.Focus()
            Exit Sub
        Else
            frmLisenceSettings.Show()
            txtPassword.Clear()
            txtConfirmPassword.Clear()
            Me.Hide()
        End If
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class