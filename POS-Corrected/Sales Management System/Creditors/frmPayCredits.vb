Imports MySql.Data
Imports MySql.Data.MySqlClient
Public Class frmPayCredits
    'variables declaration 
    Dim fullnames As String, receiptNo As Integer, totalamnt As Double, amntpaid As Double, _
        balance As Double, addamnt As Double, creditstatus As Integer, entryid As Integer, strsql As String
    Dim fileNo As Integer, disc As Double, datepaid As Date

    Dim sale As New posClass

    'SAVE TO PAYMODE
    Dim Paymode As String, TransactionNo As String, PaymodeAmnt As Double = 0

    'Update credit payments
    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Dim amountreceived As Double = 0, _
            expectedAmount As Double = 0, _
            totalAmountPaid As Double = 0, _
            discount As Double = 0
        entryid = Integer.Parse(frmCreditors.dgvCreditsPayment.CurrentRow.Cells(0).Value.ToString())

        fullnames = txtfullnames.Text
        receiptNo = txtReceiptNo.Text
        totalamnt = txtTotalAmnt.Text
        amntpaid = txtAmntpaid.Text
        discount = txtDiscount.Text
        expectedAmount = Double.Parse(txtBalance.Text)
        fileNo = Integer.Parse(lblEntryNo.Text)
        customerno = Integer.Parse(lblCustomerNo.Text)
        ' balance = txtBalanceRemaining.Text

        'Add credits amount 
        If txtPayCredits.Text.Trim = String.Empty Then
            MessageBox.Show("Please enter amount", "Strawberry System", MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtPayCredits.Focus()
            Exit Sub
        Else
            addamnt = txtPayCredits.Text.Trim
        End If

        If (addamnt >= expectedAmount) Then
            'compute credit to be payed
            totalAmountPaid = expectedAmount + amntpaid + discount
            amountreceived = expectedAmount
            creditstatus = 1
        ElseIf ((addamnt > 0) And (addamnt < expectedAmount)) Then
            amountreceived = addamnt
            totalAmountPaid = addamnt + amntpaid + discount
            creditstatus = 2
        Else
            creditstatus = 0
        End If
        txtAmntpaid.Text = totalAmountPaid.ToString

        'save data
        sale.creditPayment(amountreceived, "Cash", "Cash", "Cash", creditstatus, 4, receiptNo, fileNo, disc, "")

        'SAVE TO PAYMODE TABLE
        If Double.Parse(txtPayCredits.Text) > 0 Then
            '###########
            For i = 0 To dgvPaymode.RowCount - 1
                'If (dgvPaymode.Rows(i).Cells(0).Value <> Nothing) Then Exit Sub
                If (dgvPaymode.Rows(i).Cells(2).Value = 0) Then Continue For

                If (dgvPaymode.Rows(i).Cells(0).Value <> Nothing) Then
                    Paymode = dgvPaymode.Rows(i).Cells(0).Value.ToString
                End If

                ''validate transaction no
                'If ((dgvPaymode.Rows(i).Cells(0).Value.ToString) <> "Cash") And (dgvPaymode.Rows(i).Cells(1).Value = Nothing) Then
                '    MessageBox.Show("Missing transaction number", "Strawberry POS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    dgvPaymode.CurrentRow.Cells(1).Style.BackColor = Color.Red
                '    Exit Sub
                'Else
                TransactionNo = dgvPaymode.Rows(i).Cells(1).Value.ToString.ToUpper
                'End If

                'validate paymode amount
                If Not IsNumeric(dgvPaymode.Rows(i).Cells(2).Value) OrElse ((dgvPaymode.Rows(i).Cells(2).Value) = 0) Then
                    MessageBox.Show("Invalid pay amount", "Strawberry POS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    dgvPaymode.CurrentRow.Cells(2).Style.BackColor = Color.Red
                    Exit Sub
                Else
                    PaymodeAmnt = Double.Parse(dgvPaymode.Rows(i).Cells(2).Value.ToString)
                End If

                'INSERT INTO SALE PAYMODE
                sale.Insert_To_SalesPaymode(receiptNo, Paymode, TransactionNo, PaymodeAmnt)
            Next
        End If
        
        'save credit payment details
        sale.creditupdationdetails(receiptNo, customerno, addamnt, Now())

        'INSERT INTO CASH LEDGER
        CashLedger(receiptNo, addamnt, Now(), userid, "Customer invoice payment", "+", "Sales", 1)

        'Prit Receipts
        ''####################################################################
        If rdbYes.Checked Then
            ''****print  receipt****'
            printReceipt()
        End If
        ''####################################################################
        'clear input areas
        clearcontrols()
        'RELOAD DATA
        frmCreditors.LoadCreditorsfromdatabase()
        Me.Close()
    End Sub

    'clear controls
    Sub clearcontrols()
        txtfullnames.Text = ""
        txtReceiptNo.Text = ""
        txtTotalAmnt.Text = ""
        txtAmntpaid.Text = ""
        txtBalance.Text = ""
        txtPayCredits.Text = ""
        dgvPaymode.Rows.Clear()
        compute_Paymode_dgv()
    End Sub

    Private Sub frmPayCredits_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtPayCredits.Focus()
    End Sub

    Private Sub txtPayCredits_KeyPress(sender As Object, e As KeyPressEventArgs)
        'only numbers input validation
        If e.KeyChar <> ChrW(Keys.Back) Then
            If Char.IsNumber(e.KeyChar) Then
            Else
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub txtPayCredits_TextChanged(sender As Object, e As EventArgs) Handles txtPayCredits.TextChanged
        Dim balance As Double = 0, receivedAmount As Double = 0, changeToGive As Double = 0

        If (txtPayCredits.Text.Trim = String.Empty) Then
            receivedAmount = 0
        Else
            receivedAmount = Double.Parse(txtPayCredits.Text)
        End If
        If (txtBalance.Text.Trim = String.Empty) Then
            balance = 0
        Else
            balance = Double.Parse(txtBalance.Text)
        End If
        If (receivedAmount > balance) Then
            changeToGive = receivedAmount - balance
        Else
            changeToGive = 0
        End If
        lblChange.Text = changeToGive.ToString
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Dim Paymode As String = "", _
     TransactionNo As String = "", _
     Amount As Double = 0, _
     TotalAmnt As Double = 0

        'validate paymode cbo
        If cboPaymode.SelectedIndex = -1 Then
            MessageBox.Show("Select paymode to continue", "Strawberry POS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        Else
            Paymode = cboPaymode.Text
        End If
        'add rows to dgv
        dgvPaymode.Rows.Add(Paymode, TransactionNo, 0)
        cboPaymode.SelectedIndex = -1
    End Sub

    Private Sub btnRemove_Click(sender As Object, e As EventArgs) Handles btnRemove.Click
        If dgvPaymode.SelectedRows.Count < 1 Then
            MessageBox.Show("Select full record to remove", "Strawberry POS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        Else
            'remove record
            Dim i As Integer = dgvPaymode.CurrentRow.Index
            Dim TotalAmnt As Double = 0
            dgvPaymode.Rows.Remove(dgvPaymode.Rows(i))
            dgvPaymode.Refresh()
            'computeTotal
            For i = 0 To dgvPaymode.RowCount - 1
                TotalAmnt += Double.Parse(dgvPaymode.Rows(i).Cells(2).Value.ToString)
            Next i
            compute_Paymode_dgv()
            txtPayCredits.Text = Double.Parse(TotalAmnt.ToString)
        End If
    End Sub

    Sub compute_Paymode_dgv()
        Dim TotalAmnt As Double = 0, balance As Double = 0
        'computeTotal
        Try
            For i = 0 To dgvPaymode.RowCount - 1
                'validate VAT_Totals
                If dgvPaymode.Rows(i).Cells(2).Value.ToString = String.Empty Then Continue For
                If IsNumeric(dgvPaymode.Rows(i).Cells(2).Value.ToString) Then
                    TotalAmnt += Double.Parse(dgvPaymode.Rows(i).Cells(2).Value.ToString)
                Else
                    dgvPaymode.Rows(i).Cells(2).Value = 0
                End If
            Next i
            txtPayCredits.Text = Double.Parse(TotalAmnt.ToString)
        Catch ex As Exception
            'do nothing
        End Try
    End Sub

    Private Sub dgvPaymode_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles dgvPaymode.CellValueChanged
        compute_Paymode_dgv()
    End Sub

    Private Sub dgvPaymode_CurrentCellDirtyStateChanged(sender As Object, e As EventArgs) Handles dgvPaymode.CurrentCellDirtyStateChanged
        If dgvPaymode.IsCurrentCellDirty Then
            dgvPaymode.CommitEdit(DataGridViewDataErrorContexts.Commit)
        End If
    End Sub

    Private Sub printReceipt()
        'Dim receiptNo As Long = Long.Parse(Now().Date.Day & Now().Date.Month & Now().Date.Year & Now().TimeOfDay.Hours & Now().TimeOfDay.Minutes)
        Dim dt As New DataTable
        Dim CustomerNames As String = "", ReceiptNo As String = "", _
            ReceiptTotal As String = "", _
            ReceiptDiscount As Double = 0, AmountPaid As Double = 0, _
            UpdateAmount As Double = 0, CreditBalance As Double = 0

        With dt
            .Columns.Add("custname")
            .Columns.Add("receiptno")
            .Columns.Add("recepttotal")
            .Columns.Add("receptdisc")
            .Columns.Add("amountpaid")
            .Columns.Add("Updateamnt")
            .Columns.Add("balance")
        End With

        CustomerNames = txtfullnames.Text
        ReceiptNo = txtReceiptNo.Text
        ReceiptTotal = txtTotalAmnt.Text
        ReceiptDiscount = txtDiscount.Text
        AmountPaid = txtAmntpaid.Text
        UpdateAmount = txtPayCredits.Text
        CreditBalance = ReceiptTotal - AmountPaid
        'add rows
        dt.Rows.Add(CustomerNames, ReceiptNo, ReceiptTotal, ReceiptDiscount, AmountPaid, UpdateAmount, CreditBalance)
        'End With
        Try
            Dim rptDoc As CrystalDecisions.CrystalReports.Engine.ReportDocument

            rptDoc = New UpdateCreditsPay
            rptDoc.SetDataSource(dt)
            rptDoc.SetParameterValue("CustNames", txtfullnames.Text)
            rptDoc.SetParameterValue("ReceiptNo", txtReceiptNo.Text)
            rptDoc.SetParameterValue("ReceiptTotals", txtTotalAmnt.Text)
            rptDoc.SetParameterValue("ReceiptDiscount", txtDiscount.Text)
            rptDoc.SetParameterValue("PaidAmount", txtAmntpaid.Text)
            rptDoc.SetParameterValue("UpdateAmount", txtPayCredits.Text)
            rptDoc.SetParameterValue("CreditBalance", CreditBalance)
            rptDoc.SetParameterValue("Servedby", fullname)
            rptDoc.PrintToPrinter(1, False, 0, 0)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        'If (MsgBox("Are you sure you want to close this page?", MsgBoxStyle.YesNo, "Pay Credits") = MsgBoxResult.Yes) Then
        Me.Close()
        'End If
    End Sub
End Class