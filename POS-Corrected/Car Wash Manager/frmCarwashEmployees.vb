Imports MySql.Data
Imports MySql.Data.MySqlClient

Public Class frmCarwashEmployees

    Dim emp As New ClassManageEmployees

    Dim fullnames As String, idnumber As String, _
        phone As String, role As String, _
        residence As String, entryid As String

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        'employee names validation
        If txtFullnames.Text.Trim = String.Empty Then
            MessageBox.Show("Invalid names input", "Strawberry System", MessageBoxButtons.OK)
            txtFullnames.Focus()
            Exit Sub
        Else
            fullnames = txtFullnames.Text.Trim.ToUpper
        End If

        'employee ID number validation
        If txtIdNo.Text.Trim = String.Empty Then
            MessageBox.Show("Invalid ID number input", "Strawberry System", MessageBoxButtons.OK)
            txtIdNo.Focus()
            Exit Sub
        Else
            idnumber = txtIdNo.Text.Trim
        End If

        'employee names validation
        If txtPhone.Text.Trim = String.Empty Then
            MessageBox.Show("Input staff phone number", "Strawberry System", MessageBoxButtons.OK)
            txtPhone.Focus()
            Exit Sub
        Else
            phone = txtPhone.Text.Trim
        End If

        'employee names validation
        If txtResidence.Text.Trim = String.Empty Then
            MessageBox.Show("Residence input missing", "Strawberry System", MessageBoxButtons.OK)
            txtResidence.Focus()
            Exit Sub
        Else
            residence = txtResidence.Text.Trim.ToUpper
        End If

        'employees role validation
        If cboEmployeeRole.SelectedIndex = -1 Then
            MessageBox.Show("Select staff role to proceed", "Strawberry System", MessageBoxButtons.OK)
            cboEmployeeRole.Focus()
            Exit Sub
        Else
            role = cboEmployeeRole.SelectedValue
        End If

        'SAVE DATA
        If btnSave.Text = "Save" Then
            emp.ManageEmployees(fullnames, idnumber, phone, residence, role, entryid, 1)
            MessageBox.Show("Operation Successful", "Strawberry System", MessageBoxButtons.OK, MessageBoxIcon.Information)
            closeconn()
            'reload data
            emp.LoadEmployees()
            'clear controls
            Rub()
        Else
            'update data
            emp.ManageEmployees(fullnames, idnumber, phone, residence, role, entryid, 2)
            MessageBox.Show("Operation Successful", "Strawberry System", MessageBoxButtons.OK, MessageBoxIcon.Information)
            closeconn()
            'reload data
            emp.LoadEmployees()
            'clear controls
            Rub()
        End If
    End Sub

    'clear inputs
    Sub Rub()
        txtFullnames.Clear()
        txtPhone.Clear()
        txtIdNo.Clear()
        txtResidence.Clear()
        cboEmployeeRole.SelectedIndex = -1
        btnSave.Text = "Save"
    End Sub

    Private Sub frmCarwashEmployees_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'load to grid
        emp.LoadEmployees()
        'load combo
        Dim strsql As String = "SELECT emproll,entryid FROM employeeroll"
        loadcombo(cboEmployeeRole, strsql, "emproll", "entryid")
    End Sub

    Private Sub btnAddRoll_Click(sender As Object, e As EventArgs) Handles btnAddRoll.Click
        'input box code to add Employee role combo
        Dim AddEmployeeRole As String = ""
        AddEmployeeRole = InputBox("Add new roles" & vbNewLine & " eg. Superviser", "Strawberry System").ToUpper
        If (AddEmployeeRole.Length < 4) Then
            Exit Sub
        End If
        Try
            'save
            emp.ManageEmplRolls(AddEmployeeRole, 1)
            MessageBox.Show("Operation Successful", "Strawberry System", MessageBoxButtons.OK, MessageBoxIcon.Information)
            closeconn()
            'reload data
            emp.LoadRoles()

        Catch ex As Exception
            If Conn.State = ConnectionState.Open Then Conn.Close()
            MessageBox.Show("An error occured while try to save data. If error persists, inform your administrator for more action", "Add color", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
            closeconn()
        End Try
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Rub()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub dgvEmployees_DoubleClick(sender As Object, e As EventArgs) Handles dgvEmployees.DoubleClick
        txtFullnames.Text = dgvEmployees.CurrentRow.Cells(1).Value.ToString
        txtIdNo.Text = dgvEmployees.CurrentRow.Cells(2).Value.ToString
        txtPhone.Text = dgvEmployees.CurrentRow.Cells(3).Value.ToString
        txtResidence.Text = dgvEmployees.CurrentRow.Cells(4).Value.ToString
        cboEmployeeRole.Text = dgvEmployees.CurrentRow.Cells(5).Value.ToString
        btnSave.Text = "Update"
        'txtFullnames.ReadOnly = True
    End Sub

    Private Sub txtIdNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtIdNo.KeyPress
        'only numbers input validation
        If e.KeyChar <> ChrW(Keys.Back) Then
            If Char.IsNumber(e.KeyChar) Then
            Else
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub txtPhone_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPhone.KeyPress
        'only numbers input validation
        If e.KeyChar <> ChrW(Keys.Back) Then
            If Char.IsNumber(e.KeyChar) Then
            Else
                e.Handled = True
            End If
        End If
    End Sub
End Class