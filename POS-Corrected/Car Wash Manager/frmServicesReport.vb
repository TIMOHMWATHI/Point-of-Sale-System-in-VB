Imports MySql.Data
Imports MySql.Data.MySqlClient

Public Class frmServicesReport
    Dim sv As New ClassManageServices

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        With frmServices
            .StartPosition = FormStartPosition.CenterScreen
            .TopMost = True
            .Show()
        End With
    End Sub

    Private Sub frmServicesReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        sv.LoadServices(dgvServices)
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Try
            If dgvServices.SelectedRows.Count < 1 Then
                MessageBox.Show("Select full record to update", "Strawberry Manager", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            Else
                With frmServices
                    '.lblFarmerNo.Visible = True
                    .lblEntryNo.Text = dgvServices.CurrentRow.Cells(0).Value.ToString
                    .txtServices.Text = dgvServices.CurrentRow.Cells(1).Value.ToString
                    .txtPrice.Text = dgvServices.CurrentRow.Cells(2).Value.ToString
                    .StartPosition = FormStartPosition.CenterScreen
                    .TopMost = True
                    .btnSave.Text = "Update"
                    .Show()
                End With
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        sv.deleteServices()
    End Sub
End Class