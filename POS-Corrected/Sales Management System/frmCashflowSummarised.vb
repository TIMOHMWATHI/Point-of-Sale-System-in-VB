Imports MySql.Data
Imports MySql.Data.MySqlClient

Public Class frmCashflowSummarised
    Dim flow As New ClassCashFlow

    Dim desc As String, effect As String, cost As Double, Memo As String, _
        TotalCashIn As Double, TotalCashOut As Double

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        If txtDescription.Text.Trim = String.Empty Then
            MessageBox.Show("Invalid description input", "Strawberry POS", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        Else
            desc = txtDescription.Text.ToUpper
        End If

        If cboEffect.SelectedIndex = -1 Then
            MessageBox.Show("Invalid description input", "Strawberry POS", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        Else
            effect = cboEffect.Text.ToUpper
        End If

        If (txtAmnt.Text.Trim = String.Empty) OrElse (txtAmnt.Text.Trim < 1) Then
            MessageBox.Show("Invalid amount input", "Strawberry POS", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        Else
            cost = Double.Parse(txtAmnt.Text.Trim)
        End If
        'memo
        Memo = txtMemo.Text.Trim.ToUpper

        'add rows
        dgvFlow.Rows.Add(desc, effect, cost, memo)

        'clear controls
        rub()

        computeTotals()

    End Sub

    'clear controls
    Sub rub()
        txtDescription.Clear()
        txtAmnt.Clear()
        txtMemo.Clear()
        cboEffect.SelectedIndex = -1
    End Sub

    Private Sub btnRemove_Click(sender As Object, e As EventArgs) Handles btnRemove.Click
        If dgvFlow.SelectedRows.Count < 1 Then
            MessageBox.Show("Select full record to continue", "Strawberry POS", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        Else
            Dim i As Integer = dgvFlow.CurrentRow.Index
            dgvFlow.Rows.Remove(dgvFlow.Rows(i))
            dgvFlow.Refresh()
            ''computeTotal
            'DGVcellvaluechange(dgvReveiveItems, lblPriceTotals)
        End If
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim cashin As Double = Double.Parse(lblTotalCashIn.Text)
        Dim cashout As Double = Double.Parse(lblTotalCashOut.Text)
        Dim variance As Double = Double.Parse(lblVariance.Text)

        'loop thru
        For i = 0 To dgvFlow.RowCount - 1
            desc = dgvFlow.Rows(i).Cells(0).Value.ToString
            effect = dgvFlow.Rows(i).Cells(1).Value.ToString
            cost = Double.Parse(dgvFlow.Rows(i).Cells(2).Value.ToString)
            Memo = dgvFlow.Rows(i).Cells(3).Value.ToString

            'SAVE TO DB
            flow.InsertCashFlow(Now(), cashin, cashout, variance, userid, desc, effect, cost, Memo, 1)

        Next i
        MessageBox.Show("Success", "Strawberry POS", MessageBoxButtons.OK, MessageBoxIcon.Information)

        'CLEAR
        rub()
        dgvFlow.Rows.Clear()
    End Sub

    Sub computeTotals()
        Dim effect As String, cost As Double, TotalCashIn As Double, TotalCashOut As Double, variance As Double
        For i = 0 To dgvFlow.RowCount - 1
            effect = dgvFlow.Rows(i).Cells(1).Value.ToString
            cost = Double.Parse(dgvFlow.Rows(i).Cells(2).Value.ToString)

            If dgvFlow.Rows(i).Cells(1).Value.ToString = "+" Then
                TotalCashIn += cost
            Else
                TotalCashOut += cost
            End If

            variance = TotalCashIn - TotalCashOut
        Next i
        lblTotalCashIn.Text = TotalCashIn.ToString
        lblTotalCashOut.Text = TotalCashOut.ToString
        lblVariance.Text = variance.ToString
    End Sub
End Class