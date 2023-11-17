Imports MySql.Data
Imports MySql.Data.MySqlClient

Public Class frmBanks
    'declarations
    Dim bank As New ClassBanks
    Dim bankname As String, entryid As Integer

    Private Sub frmBanks_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'load data to grid
        bank.LoadBankDetails(dgvBanks)
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        'validation
        If txtBankName.Text.Trim = String.Empty Then
            txtBankName.Focus()
            Exit Sub
        Else
            bankname = txtBankName.Text.Trim.ToUpper
        End If
        'save data
        If btnSave.Text = "Save" Then
            bank.managebanks(bankname, 0, 0)
            MessageBox.Show("Successfully saved", "Strawberry System", MessageBoxButtons.OK, MessageBoxIcon.Information)
            bank.LoadBankDetails(dgvBanks)
            'clear controls
            rub()
        Else
            bank.managebanks(bankname, entryid, 1)
            MessageBox.Show("Successfully updated", "Strawberry System", MessageBoxButtons.OK, MessageBoxIcon.Information)
            bank.LoadBankDetails(dgvBanks)
            'clear controls
            rub()
        End If
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Dim bankname As String = dgvBanks.CurrentRow.Cells(1).Value.ToString
        'If dgvBanks.SelectedRows.Count > 1 Then
        If (MsgBox("Are you sure you want to delete " & bankname & " completely from database?", MsgBoxStyle.YesNo, "Strawberry POS") = MsgBoxResult.Yes) Then
            bank.managebanks(bankname, entryid, 2)
            MessageBox.Show("Successfully Deleted", "Strawberry System", MessageBoxButtons.OK, MessageBoxIcon.Information)
            bank.LoadBankDetails(dgvBanks)
        End If
    End Sub

    Private Sub dgvBanks_DoubleClick(sender As Object, e As EventArgs) Handles dgvBanks.DoubleClick
        txtBankName.Text = dgvBanks.CurrentRow.Cells(1).Value.ToString
        btnSave.Text = "Update"
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        rub()
    End Sub

    'clear inputs
    Sub rub()
        txtBankName.Clear()
        btnSave.Text = "Save"
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub dgvBanks_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvBanks.CellContentClick

    End Sub
End Class