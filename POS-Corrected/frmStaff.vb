Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.Text.RegularExpressions
Public Class frmStaff
    'string declarations on regex variables
    Private fullnamesvalid As String
    Private mobilevalid As String
    Private idvalid As String
    Private usernamevalid As String


    'STRING DECLARATION
    Dim fullnames As String, phonenumber As String, nationalidnumber As String, _
        username As String, paswad As String, staffid As Integer
    Dim deleted As Byte, newuser As Byte

    Dim users As New ClassStaff


    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        'ASSIGN USER PRIVILEDGES CODE
        If Not Authorized(userid, TaskID.addsystemusers) Then Exit Sub
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        'input validation on staff input
        If txtfullnames.Text.Trim = String.Empty Then
            MessageBox.Show("user fullnames input missing", "Recommended", MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtfullnames.Focus()
            Exit Sub
        Else
            fullnames = txtfullnames.Text.ToUpper
        End If

        'input validation on staff phone
        If txtphonenumber.Text.Trim = String.Empty Then
            MessageBox.Show("staff phone number input missing", "Recommended", MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtphonenumber.Focus()
            Exit Sub
        Else
            phonenumber = txtphonenumber.Text
        End If

        'input validation on staff nationalidnumber input
        If txtidnumber.Text.Trim = String.Empty Then
            MessageBox.Show("staff national id number input missing", "Recommended", MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtidnumber.Focus()
            Exit Sub
        ElseIf IsInputChar(txtidnumber.Text) OrElse
            txtidnumber.TextLength < 5 Then
            MessageBox.Show("staff national id number input error", "write a correct id number", MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtidnumber.Focus()
            Exit Sub
        Else
            nationalidnumber = txtidnumber.Text
        End If

        'input validation on staff username input
        If txtusername.Text.Trim = String.Empty Then
            MessageBox.Show("staff username input missing", "Recommended", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        Else
            username = txtusername.Text.ToLower
        End If

        paswad = "password"

        'INPUT DATA INTO DATABASE
        If btnsave.Text = "Save" Then
            users.insertstaff(fullnames, phonenumber, nationalidnumber, username, paswad)
        Else
            users.updatestaff(fullnames, phonenumber, nationalidnumber, username, staffid)
        End If
        'reload data
        LoadStaff()
        clearcontent()
    End Sub

    'clears user input
    Sub clearcontent()
        txtfullnames.Clear()
        txtphonenumber.Clear()
        txtidnumber.Clear()
        txtusername.Clear()
        btnsave.Text = "Save"
    End Sub

    Private Sub frmUsers_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'load data to grid
        LoadStaff()
    End Sub

    'exit form and back to Mdiform
    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        'If (MsgBox("Are you sure you want to close this page?", MsgBoxStyle.YesNo, "Staff") = MsgBoxResult.Yes) Then
        Me.Close()
        'End If
    End Sub


    'code to update data in the database....double click on the datagrid and on declarations you choose double click
    Private Sub dgvStaff_DoubleClick(sender As Object, e As EventArgs) Handles dgvStaff.DoubleClick
        staffid = dgvStaff.CurrentRow.Cells(0).Value.ToString
        txtfullnames.Text = dgvStaff.CurrentRow.Cells(1).Value.ToString
        txtphonenumber.Text = dgvStaff.CurrentRow.Cells(2).Value.ToString
        txtidnumber.Text = dgvStaff.CurrentRow.Cells(3).Value.ToString
        txtusername.Text = dgvStaff.CurrentRow.Cells(4).Value.ToString
        btnsave.Text = "Update"
        txtfullnames.ReadOnly = False

    End Sub

    Private Sub btnRemove_Click(sender As Object, e As EventArgs) Handles btnRemove.Click
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        'ASSIGN USER PRIVILEDGES CODE
        If Not Authorized(userid, TaskID.removesystemusers) Then Exit Sub
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        Dim strsql As String
        staffid = dgvStaff.CurrentRow.Cells(0).Value.ToString
        fullnames = dgvStaff.CurrentRow.Cells(1).Value.ToString
        strsql = "update staff set deleted=1 where staffid= " & staffid & ""
        Try
            Dim dtaName As New MySqlClient.MySqlDataAdapter
            dtaName.UpdateCommand = New MySqlClient.MySqlCommand
            With dtaName.UpdateCommand
                .CommandTimeout = 60
                .Connection = Conn()
                .CommandType = CommandType.Text
                .CommandText = strsql

                If (MsgBox("Are you sure you want to delete " & fullnames & " completely from database?", MsgBoxStyle.YesNo, "Staff Deletion") = MsgBoxResult.Yes) Then
                    .ExecuteNonQuery()
                    MessageBox.Show("Record Successfully Deleted from Database", "Staff", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            End With
            dtaName.Dispose()
            closeconn()
            'reload data
            LoadStaff()

        Catch ex As Exception
            If Conn.State = ConnectionState.Open Then Conn.Close()
            MsgBox(ex.Message & "An Error Occured While Trying To Delete Data From Database")
            Exit Sub
        End Try
    End Sub


    'Loads database table to Datagridview

    Sub LoadStaff()
        Dim strsql As String = "select staffid, fullnames,phonenumber," _
                               & " nationalidnumber,username from staff where deleted = 0 "
        Try
            Dim cmd As New MySqlCommand(strsql, Conn)
            cmd.CommandType = CommandType.Text
            Dim adapter As New MySqlDataAdapter(cmd)
            Dim table As New DataTable
            adapter.Fill(table)
            'Display the data.
            dgvStaff.DataSource = table
            closeconn()
            adapter.Dispose()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        With dgvStaff
            .Columns(0).HeaderText = "#"
            .Columns(1).HeaderText = "Fullnames"
            .Columns(2).HeaderText = "Phone"
            .Columns(3).HeaderText = "Id No:"
            .Columns(4).HeaderText = "Username"
            .Columns(0).Visible = False
            .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells
            .Columns(1).DefaultCellStyle.WrapMode = DataGridViewTriState.True
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        End With

    End Sub

    Private Sub txtphonenumber_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtphonenumber.KeyPress
        'only numbers input validation
        If e.KeyChar <> ChrW(Keys.Back) Then
            If Char.IsNumber(e.KeyChar) Then
            Else
                e.Handled = True
            End If
        End If
    End Sub

    'regex mobile number input validation on staff
    Private Sub txtphonenumber_Leave(sender As Object, e As EventArgs) Handles txtphonenumber.Leave
        If Not Regex.Match(txtphonenumber.Text, "^[0-9]*$", RegexOptions.IgnoreCase).Success Then
            MessageBox.Show("Please enter numeric values only", "Staff mobile number", MessageBoxButtons.OK, MessageBoxIcon.Information)

            txtphonenumber.Focus()
            txtphonenumber.Clear()

            mobilevalid = False
        Else
            mobilevalid = True
        End If
    End Sub

    Private Sub txtidnumber_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtidnumber.KeyPress
        'only numbers input validation
        If e.KeyChar <> ChrW(Keys.Back) Then
            If Char.IsNumber(e.KeyChar) Then
            Else
                e.Handled = True
            End If
        End If
    End Sub

    'id number regular expression input validation
    Private Sub txtidnumber_Leave(sender As Object, e As EventArgs) Handles txtidnumber.Leave
        If Not Regex.Match(txtidnumber.Text, "^[0-9]*$", RegexOptions.IgnoreCase).Success Then
            MessageBox.Show("Please enter numeric values only", "Staff identity number", MessageBoxButtons.OK, MessageBoxIcon.Information)

            txtidnumber.Focus()
            txtidnumber.Clear()

            idvalid = False
        Else
            idvalid = True
        End If
    End Sub


    'regex input validation on staff username
    Private Sub txtusername_Leave(sender As Object, e As EventArgs) Handles txtusername.Leave
        If Not Regex.Match(txtusername.Text, "^[a-zA-Z\s]*$", RegexOptions.IgnoreCase).Success Then
            MessageBox.Show("Please enter alphabetical characters only and a valid name", "Staff usernames", MessageBoxButtons.OK, MessageBoxIcon.Information)

            txtusername.Focus()
            txtusername.Clear()

            usernamevalid = False
        Else
            usernamevalid = True
        End If
    End Sub

    'call form to change password
    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles radchangepassword.CheckedChanged
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        'ASSIGN USER PRIVILEDGES CODE
        If Not Authorized(userid, TaskID.changepassword) Then Exit Sub
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&

        staffid = Integer.Parse(dgvStaff.CurrentRow.Cells(0).Value.ToString)
        'username input validation
        If dgvStaff.SelectedRows.Count = 1 Then
            If radchangepassword.Checked Then
                strsql = "UPDATE staff SET paswad=MD5('password'), newuser=1 WHERE staffid=" & staffid & " AND deleted = 0 "
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
                'clear controls
                radchangepassword.Checked = False
                'reload data
                LoadStaff()
            End If
        Else
            MessageBox.Show("Select full record to reset password", "Strawberry System", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If
    End Sub

End Class