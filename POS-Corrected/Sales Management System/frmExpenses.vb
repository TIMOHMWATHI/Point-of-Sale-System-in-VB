Imports MySql.Data
Imports MySql.Data.MySqlClient

Public Class frmExpensess
    Dim exp As New ClassExpenses

    Private Sub frmExpenses_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim ExpenseName As String = "", _
           Amount As Double = 0, _
           Narration As String = ""

        'add rows to dgv
        dgvExpense.Rows.Add(ExpenseName, Amount, Narration)
    End Sub


    Private Sub btnAddRecord_Click(sender As Object, e As EventArgs) Handles btnAddRecord.Click
        Dim ExpenseName As String = "", _
           Amount As Double = 0, _
           Narration As String = ""

        'add rows to dgv
        dgvExpense.Rows.Add(ExpenseName, Amount, Narration)
    End Sub

    Private Sub btnRemove_Click(sender As Object, e As EventArgs) Handles btnRemove.Click
        If dgvExpense.SelectedRows.Count < 1 Then
            MessageBox.Show("Select full record to remove", "Strawberry POS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        Else
            'remove record
            Dim i As Integer = dgvExpense.CurrentRow.Index
            Dim TotalAmnt As Double = 0
            dgvExpense.Rows.Remove(dgvExpense.Rows(i))
            dgvExpense.Refresh()
            'computeTotal
            For i = 0 To dgvExpense.RowCount - 1
                TotalAmnt += Double.Parse(dgvExpense.Rows(i).Cells(1).Value.ToString)
            Next i
            computeTotal()
            lblExpenseTotal.Text = TotalAmnt.ToString
        End If
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        lblExpenseTotal.Text = 0
        dgvExpense.Rows.Clear()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim ExpenseDate As Date, ExpenseTotals As Double

        'datepaid validation
        If dtpExpenseDate.Value.Date > Date.Today() Then
            MessageBox.Show("Invalid date", "Strawberry POS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        Else
            ExpenseDate = dtpExpenseDate.Value
        End If

        '####
        ExpenseTotals = Double.Parse(lblExpenseTotal.Text.ToString)

        'save to db
        If Double.Parse(lblExpenseTotal.Text.ToString) > 0 Then

            exp.ManageExpenseMaster(ExpenseDate, ExpenseTotals, "", userid)

            'save to details table
            Dim MasterNo As Integer = Integer.Parse(exp.getExpenseMasterNo())

            'INSERT INTO CASH LEDGER
            CashLedger(MasterNo, ExpenseTotals, Now(), userid, "Business expenditure", "-", "Expenses", 1)

            For i = 0 To dgvExpense.RowCount - 1
                Dim Description As String = "", _
                    Cost As Double = 0, _
                    Narration As String = ""

                If dgvExpense.Rows(i).Cells(1).Value = Nothing Then Continue For

                'validate description
                If (dgvExpense.Rows(i).Cells(0).Value = Nothing) Then
                    MessageBox.Show("Missing expense description", "Strawberry POS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    dgvExpense.CurrentRow.Cells(0).Style.BackColor = Color.Red
                    Exit Sub
                Else
                    Description = dgvExpense.Rows(i).Cells(0).Value.ToString.ToUpper
                End If

                '#####
                If (dgvExpense.Rows(i).Cells(1).Value = Nothing) Then
                    MessageBox.Show("Missing expense amount", "Strawberry POS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    dgvExpense.CurrentRow.Cells(1).Style.BackColor = Color.Red
                    Exit Sub
                Else
                    Cost = Double.Parse(dgvExpense.Rows(i).Cells(1).Value.ToString)
                End If

                Narration = dgvExpense.Rows(i).Cells(2).Value.ToString.ToUpper

                'save to db
                exp.ManageExpenseDetails(MasterNo, Description, Cost, Narration)
            Next
            MessageBox.Show("Success", "Strawberry System", MessageBoxButtons.OK, MessageBoxIcon.Information)
            'clear controls
            dgvExpense.Rows.Clear()
            lblExpenseTotal.Text = 0
        End If
    End Sub

    Sub computeTotal()
        Dim cost As Double = 0, TotalExpenses As Double = 0
        Try

            For i = 0 To dgvExpense.RowCount - 1
                'If dgvExpense.Rows(i).Cells(1).Value.ToString = String.Empty Then Continue For
                If IsNumeric(dgvExpense.Rows(i).Cells(1).Value.ToString) Then
                    cost = Double.Parse(dgvExpense.Rows(i).Cells(1).Value.ToString)
                Else
                    dgvExpense.Rows(i).Cells(2).Value = 0
                End If

                TotalExpenses += Double.Parse(dgvExpense.Rows(i).Cells(1).Value.ToString)
            Next i
            lblExpenseTotal.Text = TotalExpenses.ToString
        Catch ex As Exception

        End Try
    End Sub

    Private Sub dgvExpense_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles dgvExpense.CellValueChanged
        computeTotal()
    End Sub

    Private Sub lblExpenseTotal_TextChanged(sender As Object, e As EventArgs) Handles lblExpenseTotal.TextChanged
        computeTotal()
    End Sub

    Private Sub dgvExpense_CurrentCellDirtyStateChanged(sender As Object, e As EventArgs) Handles dgvExpense.CurrentCellDirtyStateChanged
        If dgvExpense.IsCurrentCellDirty Then
            dgvExpense.CommitEdit(DataGridViewDataErrorContexts.Commit)
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.Close()
    End Sub
End Class