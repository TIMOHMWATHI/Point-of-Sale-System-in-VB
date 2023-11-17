Imports MySql.Data
Imports MySql.Data.MySqlClient

Public Class frmLogin
    Dim strsql As String, userFound As Boolean, staffid As Integer
    Dim password As String, newUser As Integer

    Private Sub btn_login_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        'username input validation
        If txtUsername.Text.Trim = String.Empty Then
            MessageBox.Show("Username input missing", "Strawberry POS", MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtUsername.Focus()
            Exit Sub
        Else
            username = txtUsername.Text.Trim
        End If

        'password input validation
        If txtPassword.Text.Trim = String.Empty Then
            MessageBox.Show("Password input missing", "Strawberry POS", MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtPassword.Focus()
            Exit Sub
        Else
            password = txtPassword.Text.Trim
        End If

        '*******************************************
        ''Department input validation
        'Dim dept As String
        'If cboDepartments.SelectedIndex = -1 Then
        '    MessageBox.Show("You must Select your department to continue", "Department", MessageBoxButtons.OK, MessageBoxIcon.Information)
        '    cboDepartments.Focus()
        '    Exit Sub
        'Else
        '    dept = cboDepartments.Text
        'End If
        '********************************************

        'retreave data from database
        strsql = "SELECT staffid,fullnames,username,MD5(paswad)," _
                 & " newuser FROM staff WHERE  username='" & username & "' " _
                 & " and paswad=MD5('" & password & "') "

        Dim datareader As MySqlDataReader
        Dim cmd As New MySqlCommand(strsql, Conn)
        cmd.CommandType = CommandType.Text
        datareader = cmd.ExecuteReader
        While datareader.Read
            'if user is found
            userFound = True
            userid = Integer.Parse(datareader(0).ToString)
            fullname = datareader(1).ToString
            username = datareader(2).ToString
            password = datareader(3).ToString
            newUser = Integer.Parse(datareader(4).ToString)
        End While
        datareader.Dispose()

        'checking the result
        If userFound = True Then
            If newUser = 1 Then
                NEWSYSTEMUSER = True
                txtPassword.Clear()
                If (MsgBox("For being new user to this system, you are required to change your password." & vbNewLine & _
                       "Click Ok to continue or Cancel to Exit system", MsgBoxStyle.OkCancel, "Strawberry Smart Solution")) = MsgBoxResult.Ok Then
                    frmChangePassword.ShowDialog()
                Else
                    End
                End If
            Else
                NEWSYSTEMUSER = False
                clearcontent()
                'If (dept = "Sales Management System") Then
                mdiSales.Show()
                Me.Hide()
                'ElseIf (dept = "Pharmacy System") Then
                'mdiPharmacy.Show()
                'Me.Hide()
                'Else
                '    mdiFarm.Show()
                '    Me.Hide()
                'End If
            End If
        Else
            MsgBox("Sorry, username or password is incorrect", MsgBoxStyle.OkOnly, "Invalid Login")
            clearcontent()
            Exit Sub
        End If
    End Sub

    'clear input areas
    Sub clearcontent()
        txtUsername.Clear()
        txtPassword.Clear()
        cboDepartments.SelectedIndex = -1
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Application.Exit()
    End Sub

    Private Sub frmLogin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtUsername.Text = "tmacharia"
        txtPassword.Text = "panamacity"
        'cboDepartments.Text = "Sales Management System"
    End Sub
End Class