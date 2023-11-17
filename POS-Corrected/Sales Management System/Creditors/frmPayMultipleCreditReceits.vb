Imports MySql.Data
Imports MySql.Data.MySqlClient

Public Class frmPayMultipleCreditReceipts
    Dim sale As New posClass

    Private Sub frmPayMultipleCreditReceipts_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'autosearch creditors
        LoadCreditorNames(txtCreditorName)
    End Sub

    Private Sub txtCreditorName_KeyDown(sender As Object, e As KeyEventArgs) Handles txtCreditorName.KeyDown
        If e.KeyCode = Keys.Enter Then    'work if enter button is clicked
            If (txtCreditorName.Text = String.Empty) Then
                txtCreditorName.Focus()
                Exit Sub
            End If

            Dim datareader As MySqlDataReader
            'Try
            Dim strsql = "SELECT fullname,supplier_id FROM suppliers" _
                         & " WHERE (fullname='" & txtCreditorName.Text.Trim & "') " _
                         & " AND relationtype='C' AND deleted= 0 "

            Dim cmd As New MySqlCommand(strsql, Conn)
            cmd.CommandType = CommandType.Text
            datareader = cmd.ExecuteReader
            While datareader.Read
                txtCreditorName.Text = datareader(0).ToString
                lblCustomerNo.Text = datareader(1).ToString
            End While
            closeconn()
            btnCustomerCredit.PerformClick()
        End If
    End Sub

    Private Sub btnCustomerCredit_Click(sender As Object, e As EventArgs) Handles btnCustomerCredit.Click
        If lblCustomerNo.Text = 0 Then
            'do nothing
            Exit Sub
        Else
            Dim customerno As Integer = 0
            customerno = Integer.Parse(lblCustomerNo.Text)
            LoadCreditorDetails(dgvPayMultipleReceipts, customerno)
        End If
    End Sub

    Sub LoadCreditorDetails(ByVal dgv As DataGridView, ByVal customerno As Integer)
        Dim CreditsTotal As Double = 0

        Dim strsql As String = "SELECT C.entryid,C.receiptno,C.totalamount,C.amountpaid," _
                               & " C.discounts, C.totalamount - (C.amountpaid + C.discounts)" _
                               & " AS BAL,DATE_FORMAT(C.datedue, '%d %M %Y') AS PurDate,1 FROM creditsales C " _
                               & " INNER JOIN salesmaster SM ON SM.receptno=C.receiptno " _
                               & " INNER JOIN suppliers S ON S.supplier_id=C.supplier_id " _
                               & " WHERE creditstatus !=1 AND S.supplier_id=" & customerno & "" _
                               & " ORDER BY C.receiptno ASC"
        Try
            Dim cmd As New MySqlCommand(strsql, Conn)
            cmd.CommandType = CommandType.Text
            Dim adapter As New MySqlDataAdapter(cmd)
            Dim table As New DataTable
            adapter.Fill(table)
            'Display the data.
            dgv.DataSource = table
            closeconn()
            With dgv
                .Columns(0).HeaderText = "#"
                .Columns(1).HeaderText = "Receip No"
                .Columns(2).HeaderText = "Total Amnt"
                .Columns(3).HeaderText = "Amount Paid"
                .Columns(4).HeaderText = "Discount"
                .Columns(5).HeaderText = "Balance"
                .Columns(6).HeaderText = "Date Due"
                .Columns(7).HeaderText = "Amount to Pay"
                .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells
                .Columns(6).DefaultCellStyle.WrapMode = DataGridViewTriState.True
                .Columns(0).Visible = False
                .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
                .Columns(2).DefaultCellStyle.Format = "n0"  'set decimals places
                .Columns(3).DefaultCellStyle.Format = "n0"  'set decimals places
                .Columns(4).DefaultCellStyle.Format = "n0"  'set decimals places
                .Columns(5).DefaultCellStyle.Format = "n0"  'set decimals places
            End With
            For i = 0 To dgv.RowCount - 1
                CreditsTotal = CreditsTotal + Double.Parse(dgv.Rows(i).Cells(5).Value)
            Next
            txtCreditsTotal.Text = CreditsTotal.ToString
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnProcess_Click(sender As Object, e As EventArgs) Handles btnProcess.Click
        If txtAmntPaid.Text.Trim = String.Empty Then
            txtAmntPaid.Focus()
            Exit Sub
        End If
        Dim amountreceived As Double = 0, _
            amount_to_pay As Double = 0, _
            totalAmountPaid As Double = 0, _
            discount As Double = 0, _
            entryid As Integer, fullnames As String, _
            receiptNo As Integer, totalamnt As Double = 0, _
            amntpaid As Double = 0, _
            fileNo As Integer = 0
        Dim rowBalance As Double = 0, creditstatus As Integer = 0
        For i = 0 To dgvPayMultipleReceipts.RowCount - 1

            entryid = Integer.Parse(Double.Parse(dgvPayMultipleReceipts.Rows(i).Cells(0).Value.ToString))
            fullnames = txtCreditorName.Text
            receiptNo = Integer.Parse(Double.Parse(dgvPayMultipleReceipts.Rows(i).Cells(1).Value.ToString))
            totalamnt = Double.Parse(dgvPayMultipleReceipts.Rows(i).Cells(2).Value.ToString)
            amntpaid = Double.Parse(dgvPayMultipleReceipts.Rows(i).Cells(3).Value.ToString)
            discount = Double.Parse(dgvPayMultipleReceipts.Rows(i).Cells(4).Value.ToString)
            amount_to_pay = Double.Parse(dgvPayMultipleReceipts.Rows(i).Cells(7).Value.ToString)
            rowBalance = Double.Parse(dgvPayMultipleReceipts.Rows(i).Cells(5).Value.ToString)
            fileNo = Integer.Parse(dgvPayMultipleReceipts.Rows(i).Cells(0).Value.ToString)

            If (amount_to_pay = rowBalance) Then
                creditstatus = 1
            Else
                creditstatus = 2
            End If
            'txtAmntPaid.Text = totalAmountPaid.ToString

            'save data
            sale.creditPayment(amount_to_pay, "Cash", "Cash", "Cash", creditstatus, 4, receiptNo, fileNo, 0, "")

            'update credit narration report
            If (amount_to_pay > 0) Then
                Dim customerno As Integer = Integer.Parse(lblCustomerNo.Text)
                sale.creditupdationdetails(receiptNo, customerno, amount_to_pay, Now())

                'INSERT INTO CASH LEDGER
                CashLedger(receiptNo, amount_to_pay, Now(), userid, "Customer invoice payment", "+", "Sales", 1)
            End If
        Next
        ''####################################################################
        ''****print receipt****
        If rdbPrint.Checked Then
            PrintReceipt()
        End If
        ''####################################################################

        'reload data
        frmCreditors.LoadCreditorsfromdatabase()
        'clear Controls
        closeconn()
        dgvPayMultipleReceipts.DataSource = Nothing
        dgvPayMultipleReceipts.Rows.Clear()
        txtCreditorName.Clear()
        txtCreditsTotal.Clear()
        txtAmntPaid.Clear()
        lblCustomerNo.Text = 0
        rdbPrint.Checked = True
    End Sub

    Private Sub btnLoop_Click(sender As Object, e As EventArgs) Handles btnLoop.Click
        For i = 0 To dgvPayMultipleReceipts.RowCount - 1

            MsgBox("Receipt No. " & dgvPayMultipleReceipts.Rows(i).Cells(1).Value.ToString() _
                    & vbNewLine & "Total Amount: " & dgvPayMultipleReceipts.Rows(i).Cells(2).Value.ToString() _
                    & vbNewLine & "Amount Paid: " & dgvPayMultipleReceipts.Rows(i).Cells(3).Value.ToString())

        Next
    End Sub

    Private Sub txtAmntPaid_TextChanged(sender As Object, e As EventArgs) Handles txtAmntPaid.TextChanged
        Dim amountPaid As Double = 0, rowBalance As Double = 0
        If txtAmntPaid.Text.Trim = String.Empty Then
        Else
            amountPaid = Double.Parse(txtAmntPaid.Text)
        End If
        'MsgBox("Row count " & dgvPayMultipleReceipts.RowCount)
        If dgvPayMultipleReceipts.RowCount < 1 Then Exit Sub
        For i = 0 To dgvPayMultipleReceipts.RowCount - 1
            rowBalance = Double.Parse(dgvPayMultipleReceipts.Rows(i).Cells(5).Value.ToString())
            If (amountPaid >= rowBalance) Then
                dgvPayMultipleReceipts.Rows(i).Cells(7).Value = rowBalance
                amountPaid -= rowBalance
            ElseIf (amountPaid < rowBalance) And (amountPaid > 0) Then
                dgvPayMultipleReceipts.Rows(i).Cells(7).Value = amountPaid
                amountPaid = 0
            Else
                dgvPayMultipleReceipts.Rows(i).Cells(7).Value = 0
            End If
        Next
    End Sub

    Private Sub PrintReceipt()
        '  Dim receiptNo As Long = Long.Parse(Now().Date.Day & Now().Date.Month & Now().Date.Year & Now().TimeOfDay.Hours & Now().TimeOfDay.Minutes)
        Dim dt As New DataTable


        Dim ReceiptNo As String, Total As Double = 0, _
          Discount As Double = 0, AmntPaid As Double = 0, _
          Balance As Double = 0, PaidAmnt As Double, SalesDate As String, _
        CreditTotals As Double, CreditPaid As Double, InvoiceBal As Double

        CreditTotals = Double.Parse(txtCreditsTotal.Text)
        CreditPaid = Double.Parse(txtAmntPaid.Text)
        InvoiceBal = CreditTotals - CreditPaid

        Try
            With dt
                .Columns.Add("receipt_no")
                .Columns.Add("totals")
                .Columns.Add("amntpaid")
                .Columns.Add("discount")
                .Columns.Add("receiptbal")
                .Columns.Add("salesdate")
                .Columns.Add("paid_amnt")
            End With

            With dgvPayMultipleReceipts
                For i = 0 To dgvPayMultipleReceipts.RowCount - 1
                    ReceiptNo = dgvPayMultipleReceipts.Rows(i).Cells(1).Value.ToString
                    Total = Double.Parse(dgvPayMultipleReceipts.Rows(i).Cells(2).Value.ToString)
                    AmntPaid = Double.Parse(dgvPayMultipleReceipts.Rows(i).Cells(3).Value.ToString)
                    Discount = Double.Parse(dgvPayMultipleReceipts.Rows(i).Cells(4).Value.ToString)
                    Balance = Double.Parse(dgvPayMultipleReceipts.Rows(i).Cells(5).Value.ToString)
                    SalesDate = dgvPayMultipleReceipts.Rows(i).Cells(6).Value.ToString
                    PaidAmnt = Double.Parse(dgvPayMultipleReceipts.Rows(i).Cells(7).Value.ToString)

                    'add rows
                    dt.Rows.Add(ReceiptNo, Total, AmntPaid, Discount, Balance, SalesDate, PaidAmnt)
                Next
            End With
            Dim rptDoc As CrystalDecisions.CrystalReports.Engine.ReportDocument

            rptDoc = New PayMultipleInvoices

            rptDoc.SetDataSource(dt)
            rptDoc.SetParameterValue("CustomerName", txtCreditorName.Text)
            rptDoc.SetParameterValue("CreditTotals", txtCreditsTotal.Text)
            rptDoc.SetParameterValue("CreditPaid", txtAmntPaid.Text)
            rptDoc.SetParameterValue("ServedBy", fullname)
            rptDoc.SetParameterValue("CreditBalance", InvoiceBal)
            rptDoc.PrintToPrinter(1, False, 0, 0)
        Catch ex As Exception
            'MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class