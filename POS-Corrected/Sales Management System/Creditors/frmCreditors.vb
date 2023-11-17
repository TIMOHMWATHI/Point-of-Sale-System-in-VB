Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports Excel = Microsoft.Office.Interop.Excel
Imports ExcelAutoFormat = Microsoft.Office.Interop.Excel.XlRangeAutoFormat
Public Class frmCreditors

   'Loads AND RELOAD database table FROM Datagridview
    Sub LoadCreditorsfromdatabase()
        Dim totalunpaidcredit As Double = 0

        Dim strsql As String = "SELECT C.entryid,S.fullname ,C.receiptno,C.totalamount,C.amountpaid," _
                               & " C.discounts, SM.purchasemode, C.totalamount - (C.amountpaid + C.discounts)" _
                               & " AS BAL,DATE_FORMAT(C.datedue, '%d %M %Y') AS PurDate,C.supplier_id," _
                               & " TIMESTAMPDIFF(DAY,C.datedue,CURDATE()) AS 'Days_With_Arrears' FROM creditsales C" _
                               & " INNER JOIN salesmaster SM ON SM.receptno=C.receiptno" _
                               & " INNER JOIN suppliers S ON S.supplier_id=C.supplier_id" _
                               & " WHERE creditstatus !=1 GROUP BY C.entryid ORDER BY S.fullname ASC"

        Dim cmd As New MySqlCommand(strsql, Conn)
        cmd.CommandType = CommandType.Text
        Dim adapter As New MySqlDataAdapter(cmd)
        Dim table As New DataTable
        adapter.Fill(table)
        'Display the data.
        dgvCreditsPayment.DataSource = table
        closeconn()
        With dgvCreditsPayment
            .Columns(0).HeaderText = "#"
            .Columns(1).HeaderText = "Creditors Name"
            .Columns(2).HeaderText = "Receipt No:"
            .Columns(3).HeaderText = "Total Amount"
            .Columns(4).HeaderText = "Amount Paid"
            .Columns(5).HeaderText = "Discounts"
            .Columns(6).HeaderText = "Purchase mode"
            .Columns(7).HeaderText = "Balance Remaining"
            .Columns(8).HeaderText = "Purchases Date"
            .Columns(9).HeaderText = "customer No"
            .Columns(10).HeaderText = "Days With Arrears"
            .Columns(0).Visible = False
            .Columns(9).Visible = False
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            .Columns(1).DefaultCellStyle.WrapMode = DataGridViewTriState.True
            .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells
        End With

        For i = 0 To dgvCreditsPayment.RowCount - 1
            totalunpaidcredit = totalunpaidcredit + Double.Parse(dgvCreditsPayment.Rows(i).Cells(7).Value)
        Next
        lblUnpaidCredits.Text = "KShs" & " " & totalunpaidcredit.ToString
    End Sub

    Private Sub frmPayCredits_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'load data 
        LoadCreditorsfromdatabase()

        'load customer autosearch
        LoadCreditorNames(txtSearch)
    End Sub


    'CONTEXT MENU PAY CREDITS CODE
    Private Sub PAYCREDITSToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PAYCREDITSToolStripMenuItem.Click
               If dgvCreditsPayment.SelectedRows.Count = 1 Then
            With frmPayCredits
                .lblEntryNo.Text = dgvCreditsPayment.CurrentRow.Cells(0).Value.ToString
                .txtfullnames.Text = dgvCreditsPayment.CurrentRow.Cells(1).Value.ToString
                .txtReceiptNo.Text = dgvCreditsPayment.CurrentRow.Cells(2).Value.ToString
                .txtTotalAmnt.Text = dgvCreditsPayment.CurrentRow.Cells(3).Value.ToString
                .txtAmntpaid.Text = dgvCreditsPayment.CurrentRow.Cells(4).Value.ToString
                .txtDiscount.Text = dgvCreditsPayment.CurrentRow.Cells(5).Value.ToString
                .txtBalance.Text = dgvCreditsPayment.CurrentRow.Cells(7).Value.ToString
                .lblCustomerNo.Text = Integer.Parse(dgvCreditsPayment.CurrentRow.Cells(9).Value.ToString)
                'MsgBox(.lblCustomerNo.Text)
                .ShowDialog()
            End With
        Else
            MessageBox.Show("Select name to Pay Credits", "Strawberry System", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnPayCredits_Click(sender As Object, e As EventArgs) Handles btnPayCredits.Click
        If dgvCreditsPayment.SelectedRows.Count = 1 Then
            With frmPayCredits
                .lblEntryNo.Text = dgvCreditsPayment.CurrentRow.Cells(0).Value.ToString
                .txtfullnames.Text = dgvCreditsPayment.CurrentRow.Cells(1).Value.ToString
                .txtReceiptNo.Text = dgvCreditsPayment.CurrentRow.Cells(2).Value.ToString
                .txtTotalAmnt.Text = dgvCreditsPayment.CurrentRow.Cells(3).Value.ToString
                .txtAmntpaid.Text = dgvCreditsPayment.CurrentRow.Cells(4).Value.ToString
                .txtDiscount.Text = dgvCreditsPayment.CurrentRow.Cells(5).Value.ToString
                .txtBalance.Text = dgvCreditsPayment.CurrentRow.Cells(7).Value.ToString
                .ShowDialog()
            End With
        Else
            MessageBox.Show("No name is selected in order to Pay Credits", "Pay Credits", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        'load data 
        txtSearch.Clear()
        LoadCreditorsfromdatabase()
    End Sub

    Private Sub txtSearch_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSearch.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim customername As String = txtSearch.Text.Trim
            If txtSearch.Text.Trim.Length > 0 Then
                LoadCreditorsAutosearch(txtSearch)
            Else
                'refresh 
                txtSearch.Clear()
                LoadCreditorsfromdatabase()
            End If
        End If
    End Sub

    Sub LoadCreditorsAutosearch(ByVal txt As TextBox)
        Dim totalunpaidcredit As Double = 0
        Dim customername As String = txtSearch.Text.Trim

        Dim strsql As String = "SELECT C.entryid,S.fullname ,C.receiptno,C.totalamount,C.amountpaid," _
                               & " C.discounts, SM.purchasemode, C.totalamount - (C.amountpaid + C.discounts)" _
                               & " AS BAL,DATE_FORMAT(C.datedue, '%d %M %Y') AS PurDate,C.supplier_id," _
                               & " TIMESTAMPDIFF(DAY,C.datedue,CURDATE()) AS 'Days_With_Arrears' FROM creditsales C" _
                               & " INNER JOIN salesmaster SM ON SM.receptno=C.receiptno " _
                               & " INNER JOIN suppliers S ON S.supplier_id=C.supplier_id " _
                               & " WHERE creditstatus !=1 AND S.fullname='" & customername & "' "

        Dim cmd As New MySqlCommand(strsql, Conn)
        cmd.CommandType = CommandType.Text
        Dim adapter As New MySqlDataAdapter(cmd)
        Dim table As New DataTable
        adapter.Fill(table)
        'Display the data.
        dgvCreditsPayment.DataSource = table
        closeconn()

         With dgvCreditsPayment
            .Columns(0).HeaderText = "#"
            .Columns(1).HeaderText = "Creditors Name"
            .Columns(2).HeaderText = "Receipt No:"
            .Columns(3).HeaderText = "Total Amount"
            .Columns(4).HeaderText = "Amount Paid"
            .Columns(5).HeaderText = "Discounts"
            .Columns(6).HeaderText = "Purchase mode"
            .Columns(7).HeaderText = "Balance Remaining"
            .Columns(8).HeaderText = "Purchases Date"
            .Columns(9).HeaderText = "customer No"
            .Columns(10).HeaderText = "Days With Arrears"
            .Columns(0).Visible = False
            .Columns(9).Visible = False
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            .Columns(1).DefaultCellStyle.WrapMode = DataGridViewTriState.True
            .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells
        End With

        For i = 0 To dgvCreditsPayment.RowCount - 1
            totalunpaidcredit = totalunpaidcredit + Double.Parse(dgvCreditsPayment.Rows(i).Cells(7).Value)
        Next
        lblUnpaidCredits.Text = "KShs" & " " & totalunpaidcredit.ToString
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        printreceipt()
    End Sub

    Private Sub printreceipt()
        'Dim receiptNo As Long = Long.Parse(Now().Date.Day & Now().Date.Month & Now().Date.Year & Now().TimeOfDay.Hours & Now().TimeOfDay.Minutes)
        Dim dt As New DataTable
        Dim Names As String, ReceiptNo As String, _
            CreditTotal As String, AmntPaid As String, _
            Discount As String, PurchaseMode As String, _
            CreditBal As String, CreditDate As String, _
            SumBalances As Double
        Try
            With dt
                .Columns.Add("creditornames")
                .Columns.Add("receiptno")
                .Columns.Add("total_amnt")
                .Columns.Add("amnt_paid")
                .Columns.Add("discount")
                .Columns.Add("purchasemode")
                .Columns.Add("balance")
                .Columns.Add("purchase_date")
            End With
            With dgvCreditsPayment
                For i = 0 To dgvCreditsPayment.RowCount - 1
                    Names = dgvCreditsPayment.Rows(i).Cells(1).Value.ToString
                    ReceiptNo = dgvCreditsPayment.Rows(i).Cells(2).Value.ToString
                    CreditTotal = dgvCreditsPayment.Rows(i).Cells(3).Value.ToString
                    AmntPaid = dgvCreditsPayment.Rows(i).Cells(4).Value.ToString
                    Discount = dgvCreditsPayment.Rows(i).Cells(5).Value.ToString
                    PurchaseMode = dgvCreditsPayment.Rows(i).Cells(6).Value.ToString
                    CreditBal = dgvCreditsPayment.Rows(i).Cells(7).Value.ToString
                    CreditDate = dgvCreditsPayment.Rows(i).Cells(8).Value.ToString
                    SumBalances += Double.Parse(dgvCreditsPayment.Rows(i).Cells(7).Value.ToString)
                    'add rows
                    dt.Rows.Add(Names, ReceiptNo, CreditTotal, AmntPaid, Discount, PurchaseMode, CreditBal, CreditDate)
                Next
            End With
            Dim rptDoc As CrystalDecisions.CrystalReports.Engine.ReportDocument

            rptDoc = New PayCreditsSummary
            'Dim fr As New frmPrintDailyMilkDelivery
            rptDoc.SetDataSource(dt)
            'rptDoc.SetParameterValue("DateFrom", dfrom)
            'rptDoc.SetParameterValue("DateTo", dto)
            rptDoc.SetParameterValue("CreditTotals", SumBalances)
            'rptDoc.PrintToPrinter(1, False, 0, 0)
            With frmPrint
                .CRPrint.ReportSource = rptDoc
                .CRPrint.RefreshReport()
                .StartPosition = FormStartPosition.CenterScreen
                .WindowState = FormWindowState.Maximized
                .Show()
            End With

        Catch ex As Exception

        End Try
    End Sub

    Private Sub PayCreditToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PayCreditToolStripMenuItem.Click
        With frmPayMultipleCreditReceipts
            .StartPosition = FormStartPosition.CenterScreen
            .WindowState = FormWindowState.Normal
            .ShowDialog()
        End With
    End Sub

    Private Sub btnMultipleReceipts_Click(sender As Object, e As EventArgs) Handles btnMultipleReceipts.Click
        With frmPayMultipleCreditReceipts
            .StartPosition = FormStartPosition.CenterScreen
            .WindowState = FormWindowState.Normal
            .ShowDialog()
        End With
    End Sub
End Class