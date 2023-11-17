Imports MySql.Data
Imports MySql.Data.MySqlClient

Public Class frmServices
    Dim sv As New ClassManageServices
    Dim services As String, price As Double = 0, Entryid As String

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        'services validation
        If txtServices.Text.Trim = String.Empty Then
            MessageBox.Show("Invalid service input", "Strawberry System", MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtServices.Focus()
            Exit Sub
        Else
            services = txtServices.Text.Trim.ToUpper
        End If

        'price validation
        If txtPrice.Text.Trim = String.Empty Then
            MessageBox.Show("Invalid price input", "Strawberry System", MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtPrice.Focus()
            Exit Sub
        Else
            price = Double.Parse(txtPrice.Text.Trim)
        End If
        'save data
        If btnSave.Text = "Save" Then
            sv.ManageServices(services, price, Entryid, 1)
            MessageBox.Show("Operation Successful", "Strawberry System", MessageBoxButtons.OK, MessageBoxIcon.Information)
            closeconn()
            'clear controls
            rub()
            'reload data
            sv.LoadServices(frmServicesReport.dgvServices)
        Else
            sv.ManageServices(services, price, Entryid, 2)
            MessageBox.Show("Operation Successful", "Strawberry System", MessageBoxButtons.OK, MessageBoxIcon.Information)
            closeconn()
            'clear controls
            rub()
            'reload data
            sv.LoadServices(frmServicesReport.dgvServices)
        End If
    End Sub

    Sub rub()
        txtServices.Clear()
        txtPrice.Clear()
        lblEntryNo.Text = 0
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        rub()
    End Sub

    Private Sub txtPrice_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPrice.KeyPress
        'input validation code for numeric and decimal values only
        Dim c As Char
        c = e.KeyChar
        If Not (Char.IsDigit(c) Or c = "." Or
        Char.IsControl(c)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class