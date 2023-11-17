Imports MySql.Data
Imports MySql.Data.MySqlClient

Public Class frmCategories
    'declarations
    Dim cat As New ClassItemCategory
    Dim CategoryName As String, EntryNo As Integer = 0

    Private Sub frmCategories_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'load data to grid
        cat.LoadCategoryDetails(dgvCategory)
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        'validation
        If txtCategory.Text.Trim = String.Empty Then
            txtCategory.Focus()
            Exit Sub
        Else
            CategoryName = txtCategory.Text.Trim.ToUpper
        End If
        'save data
        If btnSave.Text = "Save" Then
            'save
            cat.managecategory(CategoryName, EntryNo, 1)
            MessageBox.Show("Successfully saved", "Strawberry System", MessageBoxButtons.OK, MessageBoxIcon.Information)
            'reload data
            cat.LoadCategoryDetails(dgvCategory)
            'clear controls
            rub()
        Else
            'update
            cat.managecategory(CategoryName, EntryNo, 2)

            MessageBox.Show("Success", "Strawberry System", MessageBoxButtons.OK, MessageBoxIcon.Information)
            'reload data
            cat.LoadCategoryDetails(dgvCategory)
            'clear controls
            rub()
        End If
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        rub()
    End Sub

    'clear inputs
    Sub rub()
        txtCategory.Clear()
        btnSave.Text = "Save"
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Dim EntryNo As Integer = Integer.Parse(dgvCategory.CurrentRow.Cells(0).Value.ToString)
        Dim Category As String = dgvCategory.CurrentRow.Cells(1).Value.ToString

        'If dgvBanks.SelectedRows.Count > 1 Then
        If (MsgBox("Are you sure you want to delete " & Category & " completely from database?", MsgBoxStyle.YesNo, "Strawberry System") = MsgBoxResult.Yes) Then
            cat.managecategory(Category, EntryNo, 3)

            MessageBox.Show("Success", "Strawberry System", MessageBoxButtons.OK, MessageBoxIcon.Information)
            cat.LoadCategoryDetails(dgvCategory)
        End If
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub dgvCategory_DoubleClick(sender As Object, e As EventArgs) Handles dgvCategory.DoubleClick
        lblEntryNo.Text = Integer.Parse(dgvCategory.CurrentRow.Cells(0).Value.ToString)
        txtCategory.Text = dgvCategory.CurrentRow.Cells(1).Value.ToString
        btnSave.Text = "Update"
    End Sub

End Class