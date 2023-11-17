Imports MySql.Data
Imports MySql.Data.MySqlClient

Public Class frmSupplierPayments
    Dim supplier As New ClassSuppliers
    Dim pay As New ClassSupplierPayment
    Dim exp As New ClassExpenses

    'public variable
    Dim cashReceived As Double = 0
    Dim balRemaining As Double = 0
    Dim creditTotal As Double = 0

    Private Sub frmSupplierPayments_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Supplier Unpaid Invoices
        supplier.LoadSupplierAmnts(dgvSupplierAmnt)
        'dtpDate.MaxDate = Today()

        supplier.LoadSupplierPayment(dgvSupplierPayments)
        'load autosearch
        LoadSuppliers(txtSearch)
        LoadSuppliers(txtSearchToPay)
        'load combo
        Dim strsql As String = "SELECT bankname,entryid FROM banks WHERE deleted!=1 ORDER BY bankname ASC"
        loadcombo(cboBank, strsql, "bankname", "entryid")
    End Sub

   
    Sub computeBalance()
        Dim TotalAmnt As Double, _
            AmntPaid As Double, _
            Balance As Double
        'Credit totals validation
        txtCreditsTotal.Text = Double.Parse(TotalAmnt)

        ''Amnt paid validation
        'If txtAmntPaid.Text = String.Empty Then
        '    AmntPaid = 0
        'Else
        AmntPaid = Double.Parse(txtAmntPaid.Text)
        'End If

        'compute bal
        Balance = TotalAmnt - AmntPaid
        txtBal.Text = Double.Parse(Balance.ToString)
    End Sub

    Private Sub txtSearch_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSearch.KeyDown
        If e.KeyCode = Keys.Enter Then

            Dim datareader As MySqlDataReader

            Dim suppliername As String, suppNo As Integer = 0

            If txtSearch.Text.Trim.Length > 0 Then
                suppliername = txtSearch.Text.Trim
                suppNo = lblSupplier.Text

                'retrieve supplier number to label
                Dim strsql = "SELECT supplier_id FROM suppliers " _
                                 & " WHERE deleted!=1 AND relationtype='S'" _
                                 & " AND fullname='" & suppliername & "'"

                Dim cmd As New MySqlCommand(strsql, Conn)
                cmd.CommandType = CommandType.Text
                datareader = cmd.ExecuteReader
                While datareader.Read
                    lblSupplier.Text = datareader(0).ToString
                    'select supplier details
                    supplier.LoadSupplierAutosearch(suppliername, dgvSupplierPayments)
                End While
                datareader.Dispose()
                closeconn()
            End If
        End If
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        'clear control
        txtSearch.Clear()
        lblSupplier.Text = 0
        'reload data
        supplier.LoadSupplierPayment(dgvSupplierPayments)
        'Supplier Unpaid Invoices
        supplier.LoadSupplierAmnts(dgvSupplierAmnt)
    End Sub

    Private Sub btnLoad_Click(sender As Object, e As EventArgs) Handles btnLoad.Click
        Dim SupplierNo As Integer = 0
        If lblSupplierNo.Text = 0 Then
            'do nothing
            Exit Sub
        Else
            SupplierNo = Integer.Parse(lblSupplierNo.Text)
            supplier.LoadSupplierInvoices(dgvPaySupplierInvoices, SupplierNo)
        End If
    End Sub

    Private Sub txtAmntPaid_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtAmntPaid.KeyPress
        If e.KeyChar <> ChrW(Keys.Back) AndAlso Not e.KeyChar = "." Then
            If Char.IsNumber(e.KeyChar) Then
            Else
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub txtAmntPaid_TextChanged(sender As Object, e As EventArgs) Handles txtAmntPaid.TextChanged
        Dim amountPaid As Double = 0, rowBalance As Double = 0
        If txtAmntPaid.Text.Trim = String.Empty Then
            Exit Sub
        Else
            amountPaid = Double.Parse(txtAmntPaid.Text)
        End If
        Me.cashReceived = amountPaid
        Me.creditTotal = Double.Parse(FormatNumber(txtCreditsTotal.Text))

        If (amountPaid >= creditTotal) Then
            txtBal.Text = 0
        Else
            txtBal.Text = (creditTotal - amountPaid)
        End If


        'MsgBox("Row count " & dgvPayMultipleReceipts.RowCount)
        If dgvPaySupplierInvoices.RowCount < 1 Then Exit Sub
        For i = 0 To dgvPaySupplierInvoices.RowCount - 1
            rowBalance = Double.Parse(dgvPaySupplierInvoices.Rows(i).Cells(5).Value.ToString())
            If (amountPaid >= rowBalance) Then
                dgvPaySupplierInvoices.Rows(i).Cells(6).Value = rowBalance
                amountPaid -= rowBalance
            ElseIf (amountPaid < rowBalance) And (amountPaid > 0) Then
                dgvPaySupplierInvoices.Rows(i).Cells(6).Value = amountPaid
                amountPaid = 0
            Else
                dgvPaySupplierInvoices.Rows(i).Cells(6).Value = 0
            End If
        Next
    End Sub

    Private Sub rdbCash_CheckedChanged(sender As Object, e As EventArgs) Handles rdbCash.CheckedChanged
        If rdbCash.Checked = True Then
            rdbMobileTransfer.Checked = False
            rdbBankTransfer.Checked = False
            rdbCheque.Checked = False
            txtCash.Visible = True
        Else
            txtCash.Visible = False
        End If
    End Sub

    Private Sub rdbMpesa_CheckedChanged(sender As Object, e As EventArgs) Handles rdbMobileTransfer.CheckedChanged
        If rdbMobileTransfer.Checked = True Then
            rdbCash.Checked = False
            rdbBankTransfer.Checked = False
            rdbCheque.Checked = False
            lblCheque.Visible = False
            lblMpesaRef.Visible = True
            txtMpesaRef.Visible = True
        Else
            lblMpesaRef.Visible = False
            txtMpesaRef.Visible = False
        End If
    End Sub

    Private Sub rdbBankTransfer_CheckedChanged(sender As Object, e As EventArgs) Handles rdbBankTransfer.CheckedChanged
        If rdbBankTransfer.Checked = True Then
            rdbCash.Checked = False
            rdbMobileTransfer.Checked = False
            rdbCheque.Checked = False
            lblCheque.Visible = False
            lblBank.Visible = True
            cboBank.Visible = True
            lblBankRef.Visible = True
            txtBankRefNo.Visible = True
        Else
            lblBank.Visible = False
            cboBank.Visible = False
            lblBankRef.Visible = False
            txtBankRefNo.Visible = False
        End If
    End Sub

    Private Sub rdbCheque_CheckedChanged(sender As Object, e As EventArgs) Handles rdbCheque.CheckedChanged
        If rdbCheque.Checked = True Then
            rdbCash.Checked = False
            rdbBankTransfer.Checked = False
            rdbCheque.Checked = True
            lblCheque.Visible = True
            txtChequeNo.Visible = True
            lblMpesaRef.Visible = False
            txtMpesaRef.Visible = False
        Else
            rdbCheque.Checked = False
            lblCheque.Visible = False
            txtChequeNo.Visible = False
        End If
    End Sub

    Private Sub btnProcess_Click(sender As Object, e As EventArgs) Handles btnProcess.Click
        Dim SupplierNo As Integer = 0, CreditTotals As Double = 0, _
            AmountPaid As Double = 0, Balance As Double = 0, _
            Paymode As String, Bank As String, _
            TransactionNO As String, _
            DatePaid As Date, paystatus As Integer, _
            InvoiceNo As String = "", PayAmnt As Double

        Dim rowBalance As Double = 0, creditstatus As Integer = 0, _
            MasterNo As Integer, Othernotes As String

        'transaction declaration
        TransactionNO = (txtMpesaRef.Text.Trim.ToUpper) + (txtBankRefNo.Text.Trim.ToUpper)

        'supplier number validation
        If (lblSupplierNo.Text = 0) Then
            Exit Sub
        Else
            SupplierNo = Integer.Parse(lblSupplierNo.Text)
        End If

        'credit totals validation
        If txtCreditsTotal.Text <= 0 Then
            'do nothing
            Exit Sub
        Else
            CreditTotals = Double.Parse(txtCreditsTotal.Text)
        End If

        'Amount paid validation
        If txtAmntPaid.Text <= 0 Then
            MessageBox.Show("Invalid pay amount", "Strawberry POS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            txtAmntPaid.Focus()
            Exit Sub
        Else
            AmountPaid = Double.Parse(txtAmntPaid.Text)
        End If

        Balance = Double.Parse(txtBal.Text)

        'memo validation
        Othernotes = txtOtherNotes.Text.ToUpper

        'datepaid validation
        If dtpPayDate.Value.Date > Date.Today() Then
            MessageBox.Show("Invalid date", "Strawberry POS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        Else
            DatePaid = dtpPayDate.Value
        End If

        'paymode validation
        If (rdbMobileTransfer.Checked = False) And (rdbBankTransfer.Checked = False) And (rdbCash.Checked = False) And (rdbCheque.Checked = False) Then
            MessageBox.Show("Select Paymode", "Strawberry POS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        If (rdbMobileTransfer.Checked = True) Then
            'M-pesa Ref validation
            Paymode = "Mobile Transfer"
            '###########
            If txtMpesaRef.Text = String.Empty Then
                MessageBox.Show("Invalid M-pesa transaction code", "Strawberry POS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtMpesaRef.Focus()
                Exit Sub
            Else
                TransactionNO = (txtMpesaRef.Text.Trim.ToUpper) + (txtBankRefNo.Text.Trim.ToUpper)
            End If

        ElseIf (rdbBankTransfer.Checked = True) Then
            Paymode = "Bank Transfer"

            If cboBank.SelectedIndex = -1 Then
                MessageBox.Show("Select bank", "Strawberry POS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
                cboBank.Focus()
            Else
                Bank = cboBank.Text
            End If

            If txtBankRefNo.Text = String.Empty Then
                MessageBox.Show("Invalid Bank transaction code", "Strawberry POS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtBankRefNo.Focus()
                Exit Sub
            Else
                TransactionNO = (txtMpesaRef.Text.Trim) + (txtBankRefNo.Text.Trim)
            End If

        ElseIf (rdbCheque.Checked = True) Then

            Paymode = "Cheque"

            If txtChequeNo.Text = String.Empty Then
                MessageBox.Show("Invalid cheque number", "Strawberry POS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtChequeNo.Focus()
                Exit Sub
            Else
                TransactionNO = (txtMpesaRef.Text.Trim) + (txtBankRefNo.Text.Trim) + (txtChequeNo.Text.Trim)
            End If

        Else

            Paymode = "Cash"

        End If
        'SAVE TO DB
        For i = 0 To dgvPaySupplierInvoices.RowCount - 1
            Dim Bal As Double
            If (dgvPaySupplierInvoices.Rows(i).Cells(6).Value < 1) Then Continue For
            InvoiceNo = dgvPaySupplierInvoices.Rows(i).Cells(2).Value.ToString
            Bal = Double.Parse(dgvPaySupplierInvoices.Rows(i).Cells(5).Value.ToString)
            PayAmnt = Double.Parse(dgvPaySupplierInvoices.Rows(i).Cells(6).Value.ToString)
            MasterNo = Integer.Parse(dgvPaySupplierInvoices.Rows(i).Cells(7).Value.ToString)

            'pay status validation
            AmountPaid = PayAmnt
            If (PayAmnt = 0) OrElse (PayAmnt < 1) Then
                paystatus = 0
            ElseIf (PayAmnt > 0) And (PayAmnt < Bal) Then
                paystatus = 2
            Else
                paystatus = 1
            End If

            'insert into master table
            pay.UpdatePaymentMaster(SupplierNo, CreditTotals, PayAmnt, InvoiceNo, MasterNo, paystatus)
            '#######
            If PayAmnt > 0 Then
                CashLedger(MasterNo, PayAmnt, Now(), userid, "Suppliers credit payment", "-", "Supplier_Payments", 1)
            End If
            'insert into payment table
            pay.insertPayNarration(MasterNo, PayAmnt, Paymode, cboBank.Text, TransactionNO, DatePaid, Othernotes, userid)
        Next
        MessageBox.Show("Success", "Strawberry POS", MessageBoxButtons.OK, MessageBoxIcon.Information)
        'clear controls
        Rub()
        'reload data
        supplier.LoadSupplierPayment(dgvSupplierPayments)
        'Supplier Unpaid Invoices
        supplier.LoadSupplierAmnts(dgvSupplierAmnt)
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Rub()
    End Sub

    Sub Rub()
        dgvPaySupplierInvoices.DataSource = Nothing
        dgvPaySupplierInvoices.Rows.Clear()
        txtSearchToPay.Clear()
        txtCreditsTotal.Clear()
        txtAmntPaid.Clear()
        txtBal.Clear()
        txtOtherNotes.Clear()
        dtpPayDate.Value = Today
        lblSupplierNo.Text = 0
        rdbCash.Checked = False
        rdbMobileTransfer.Checked = False
        rdbBankTransfer.Checked = False
        rdbCheque.Checked = False
        txtMpesaRef.Clear()
        cboBank.SelectedIndex = -1
        txtBankRefNo.Clear()
    End Sub

    Private Sub txtSearchToPay_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSearchToPay.KeyDown
        Dim SupplierName As String

        'input validation on txtItemSearch
        If (txtSearchToPay.Text = String.Empty) Then
            txtSearchToPay.Focus()
            Exit Sub
        Else
            SupplierName = txtSearchToPay.Text.Trim
        End If
        '#################################################################################################
        'AUTO SEARCH FARMER NAMES
        Dim datareader As MySqlDataReader
        If e.KeyCode = Keys.Enter Then    'work if enter button is clicked
            Try
                Dim strsql = "SELECT fullname,supplier_id FROM suppliers " _
                             & " WHERE deleted!=1 AND relationtype='S'" _
                             & " AND fullname='" & SupplierName & "'"

                Dim cmd As New MySqlCommand(strsql, Conn)
                cmd.CommandType = CommandType.Text
                datareader = cmd.ExecuteReader
                While datareader.Read
                    txtSearchToPay.Text = datareader(0).ToString
                    lblSupplierNo.Text = datareader(1).ToString
                    btnLoad.PerformClick()
                End While
                datareader.Dispose()
                closeconn()
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnPaySelected_Click(sender As Object, e As EventArgs) Handles btnPaySelected.Click
        '  lblSelectedTotals.Text = 0

        '  Dim supplierNo As Integer = Integer.Parse(lblSupplier.Text)
        '  Dim supplierName As String = txtSearch.Text
        '  Dim PaidTotals As Double = Double.Parse(lblSelectedTotals.Text)


        '  Dim CheckStatus As Boolean, InvoiceDate As Date, _
        'InvoiceNo As String = "", InvoiceTotal As Double, _
        'InvoicePaid As Double, Balance As Double

        '  For Each Item_Row As DataGridViewRow In dgvSupplierPayments.Rows
        '      If dgvSupplierPayments.RowCount > 0 Then

        '          If Item_Row.Cells("ColPay").Value = True Then

        '              CheckStatus = Item_Row.Cells("ColPay").Value.ToString

        '              'InvoiceDate = Item_Row.Cells("ColDate").Value.ToString
        '              'InvoiceNo = Item_Row.Cells("ColInvoiceNo").Value.ToString
        '              InvoiceTotal = Double.Parse(Item_Row.Cells("ColTotal").Value.ToString)
        '              InvoicePaid = Double.Parse(Item_Row.Cells("ColPaid").Value.ToString)

        '              InvoiceTotal = Double.Parse(Item_Row.Cells("ColTotal").Value.ToString)
        '              InvoicePaid = Double.Parse(Item_Row.Cells("ColPaid").Value.ToString)

        '              'MsgBox("Status:" & CheckStatus & " : " & "Invoice Total :" & InvoiceTotal & " : " & "Invoice Paid:" & InvoicePaid)

        '              lblSelectedTotals.Text = PaidTotals + (InvoiceTotal - InvoicePaid).ToString
        '          End If
        '      End If
        '  Next
        '  lblSupplierNo.Text = lblSupplier.Text
        '  txtSearchToPay.Text = txtSearch.Text
        '  txtCreditsTotal.Text = lblSelectedTotals.Text
        '  supplier.LoadSupplierInvoices(dgvPaySupplierInvoices, supplierNo)
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged

    End Sub

    Private Sub txtSearchToPay_TextChanged(sender As Object, e As EventArgs) Handles txtSearchToPay.TextChanged

    End Sub
End Class