Imports MySql.Data
Imports MySql.Data.MySqlClient

Public Class frmDetailedCreditReports

    Private Sub frmDetailedCreditReports_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'load autosearch
        LoadCreditorNames(txtSearchCustomer)
    End Sub

    Private Sub btnLoad_Click_1(sender As Object, e As EventArgs) Handles btnLoad.Click
        'first clear the grid
        dgvCreditDetailed.Rows.Clear()

        'reload data
        Dim dFrom As Date, dTo As Date, DateDelivered As Date,
            UnpaidTotals As Double = 0, InvoiceNo As String, _
            i As Integer = 0, CreditTotals As Double

        dFrom = dtpFrom.Value.ToShortDateString
        dTo = dtpTo.Value.ToShortDateString

        'add to grid
        Load_Customer_Data(dFrom, dTo, dgvCreditDetailed)

        'loop through the grid
        For i = 0 To dgvCreditDetailed.RowCount - 1
            'progress_bar variables
            Dim percentage_Processed As Double = 0, totalRecords As Double = 0
            totalRecords = dgvCreditDetailed.RowCount - 1
            PB_Progress.Maximum = totalRecords

            If dgvCreditDetailed.Rows(i).Cells(0).Value = Nothing Then Continue For
            If dgvCreditDetailed.Rows(i).Cells(0).Value.ToString = String.Empty Then Continue For

            'assign progress_bar variables
            percentage_Processed = (i / totalRecords) * 100
            lblCount.Text = i.ToString
            lblPercentage.Text = FormatNumber(percentage_Processed, 1) & " %"

            DateDelivered = dgvCreditDetailed.Rows(i).Cells(0).Value.ToString
            InvoiceNo = dgvCreditDetailed.Rows(i).Cells(2).Value.ToString


            'compute Totals on labels
            CreditTotals = CreditTotals + Double.Parse(dgvCreditDetailed.Rows(i).Cells(4).Value)
            UnpaidTotals = UnpaidTotals + Double.Parse(dgvCreditDetailed.Rows(i).Cells(7).Value)

            PB_Progress.Value = i
        Next
        lblCreditBalances.Text = "Kshs" & " " & UnpaidTotals.ToString
        lblCreditTotals.Text = "Kshs" & " " & CreditTotals.ToString

        'CLEAR PROGRESS BAR / CONTROLS
        PB_Progress.Value = 0
        lblCount.Text = 0
        lblPercentage.Text = 0
    End Sub

    Private Sub Load_Customer_Data(ByVal dfrom As Date, _
                                      ByVal dto As Date, _
                                      ByVal Grid As DataGridView)

        Dim datareader As MySqlDataReader
        Dim ReceiptDate As String, CustomerNames, _
            InvoiceNo As String, ReceiptItems As String, _
            ReceiptTotal As Double, AmountPaid As Double, _
            Discount As Double, Balance As Double, PayMode As String

        Dim strsql As String = "SELECT q1.ReceiptDate,q1.fullname,q1.rc1,q1.ReceiptItems," _
                               & " IFNULL(q1.SalesTotals,0) AS TotalSales," _
                               & " IFNULL(q1.amountpaid,0) AS AmntPaid," _
                               & " IFNULL(q1.discount,0) AS Discnt," _
                               & " IFNULL(q1.balance,0) AS Bal," _
                               & " q2.ReceiptPaymode" _
                               & " FROM" _
                               & " (SELECT DATE_FORMAT(sm.receptdatetime, '%Y,%M,%d') AS ReceiptDate," _
                               & " s.fullname,sm.receptno AS rc1," _
                               & " GROUP_CONCAT(DISTINCT sd.quantitybought, ' - ' ,p.Description" _
                               & " ORDER BY sd.receptno SEPARATOR '  ;  \r\n') AS ReceiptItems," _
                               & " sm.totalamount AS SalesTotals,sm.amountpaid,sm.discount," _
                               & " (sm.totalamount - (sm.amountpaid + sm.discount)) AS balance" _
                               & " FROM salesmaster sm " _
                               & " INNER JOIN salesdetails sd ON sd.receptno=sm.receptno" _
                               & " INNER JOIN salespaymode sp ON sp.receiptno=sm.receptno" _
                               & " INNER JOIN products p ON p.Barcode=sd.itemcode" _
                               & " INNER JOIN suppliers s ON s.supplier_id=sm.customerid" _
                               & " WHERE DATE(sm.receptdatetime) BETWEEN '" & dfrom & "' AND '" & dto & "' AND" _
                               & " sm.purchasemode='Credit' GROUP BY DATE(sm.receptdatetime),sm.receptno " _
                               & " ORDER BY DATE(sm.receptdatetime) ASC) AS q1" _
                               & " Left Join" _
                               & " (SELECT sm.receptno AS rc2," _
                               & " case when (GROUP_CONCAT(DISTINCT sp.paymode,' - ', ' Transaction No: ',' - '," _
                               & " sp.transactionno, ' - ', ' Amount: ',sp.amountpaid " _
                               & " ORDER BY sp.receiptno SEPARATOR '  ;   \r\n')) then 'N/A' " _
                               & " ELSE " _
                               & " (GROUP_CONCAT(DISTINCT sp.paymode,' - ', ' Transaction No: ',' - '," _
                               & " sp.transactionno, ' - ', ' Amount: ',sp.amountpaid " _
                               & " ORDER BY sp.receiptno SEPARATOR '  ;   \r\n'))" _
                               & " END AS ReceiptPaymode" _
                               & " FROM salespaymode sp INNER JOIN salesmaster sm ON sm.receptno=sp.receiptno" _
                               & " WHERE DATE(sm.receptdatetime) BETWEEN '" & dfrom & "' AND '" & dto & "'" _
                               & " GROUP BY sp.receiptno ORDER BY sp.receiptno ASC) AS q2" _
                               & " ON q1.rc1=q2.rc2 GROUP BY q1.rc1"
        Try
            Dim cmd As New MySqlCommand(strsql, Conn)
            cmd.CommandType = CommandType.Text
            datareader = cmd.ExecuteReader
            While datareader.Read
                ReceiptDate = datareader(0).ToString
                CustomerNames = datareader(1).ToString
                InvoiceNo = datareader(2).ToString
                ReceiptItems = datareader(3).ToString
                ReceiptTotal = Double.Parse(datareader(4).ToString)
                AmountPaid = Double.Parse(datareader(5).ToString)
                Discount = Double.Parse(datareader(6).ToString)
                Balance = Double.Parse(datareader(7).ToString)
                PayMode = datareader(8).ToString

                'add to grid
                dgvCreditDetailed.Rows.Add(ReceiptDate, CustomerNames, InvoiceNo, ReceiptItems, _
                                           ReceiptTotal, AmountPaid, Discount, Balance, PayMode)
                '+++++++++++++
            End While
            datareader.Dispose()
            closeconn()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub get_Customer_Statement(ByVal dfrom As Date, _
                                       ByVal dto As Date, _
                                       ByVal CustomerName As String, _
                                       ByVal Grid As DataGridView)

        Dim datareader As MySqlDataReader
        Dim ReceiptDate As String, CustomerNames, _
            InvoiceNo As String, ReceiptItems As String, _
            ReceiptTotal As Double, AmountPaid As Double, _
            Discount As Double, Balance As Double, PayMode As String

        Dim strsql As String = "SELECT q1.ReceiptDate,q1.fullname,q1.rc1,q1.ReceiptItems," _
                        & " IFNULL(q1.SalesTotals,0) AS TotalSales," _
                        & " IFNULL(q1.amountpaid,0) AS AmntPaid," _
                        & " IFNULL(q1.discount,0) AS Discnt," _
                        & " IFNULL(q1.balance,0) AS Bal," _
                        & " q2.ReceiptPaymode" _
                        & " FROM" _
                        & " (SELECT DATE_FORMAT(sm.receptdatetime, '%Y,%M,%d') AS ReceiptDate," _
                        & " s.fullname,sm.receptno AS rc1," _
                        & " GROUP_CONCAT(DISTINCT sd.quantitybought, ' - ' ,p.Description" _
                        & " ORDER BY sd.receptno SEPARATOR '  ;  \r\n') AS ReceiptItems," _
                        & " sm.totalamount AS SalesTotals,sm.amountpaid,sm.discount," _
                        & " (sm.totalamount - (sm.amountpaid + sm.discount)) AS balance" _
                        & " FROM salesmaster sm " _
                        & " INNER JOIN salesdetails sd ON sd.receptno=sm.receptno" _
                        & " INNER JOIN salespaymode sp ON sp.receiptno=sm.receptno" _
                        & " INNER JOIN products p ON p.Barcode=sd.itemcode" _
                        & " INNER JOIN suppliers s ON s.supplier_id=sm.customerid" _
                        & " WHERE DATE(sm.receptdatetime) BETWEEN '" & dfrom & "' AND '" & dto & "' AND" _
                        & " sm.purchasemode='Credit' GROUP BY DATE(sm.receptdatetime),sm.receptno " _
                        & " ORDER BY DATE(sm.receptdatetime) ASC) AS q1" _
                        & " Left Join" _
                        & " (SELECT sm.receptno AS rc2," _
                        & " case when (GROUP_CONCAT(DISTINCT sp.paymode,' - ', ' Transaction No: ',' - '," _
                        & " sp.transactionno, ' - ', ' Amount: ',sp.amountpaid " _
                        & " ORDER BY sp.receiptno SEPARATOR '  ;   \r\n')) then 'N/A' " _
                        & " ELSE " _
                        & " (GROUP_CONCAT(DISTINCT sp.paymode,' - ', ' Transaction No: ',' - '," _
                        & " sp.transactionno, ' - ', ' Amount: ',sp.amountpaid " _
                        & " ORDER BY sp.receiptno SEPARATOR '  ;   \r\n'))" _
                        & " END AS ReceiptPaymode" _
                        & " FROM salespaymode sp INNER JOIN salesmaster sm ON sm.receptno=sp.receiptno" _
                        & " WHERE DATE(sm.receptdatetime) BETWEEN '" & dfrom & "' AND '" & dto & "'" _
                        & " GROUP BY sp.receiptno ORDER BY sp.receiptno ASC) AS q2" _
                        & " ON q1.rc1=q2.rc2 WHERE q1.fullname='" & CustomerName & "' GROUP BY q1.rc1"
        Try
            Dim cmd As New MySqlCommand(strsql, Conn)
            cmd.CommandType = CommandType.Text
            datareader = cmd.ExecuteReader
            While datareader.Read
                ReceiptDate = datareader(0).ToString
                CustomerNames = datareader(1).ToString
                InvoiceNo = datareader(2).ToString
                ReceiptItems = datareader(3).ToString
                ReceiptTotal = Double.Parse(datareader(4).ToString)
                AmountPaid = Double.Parse(datareader(5).ToString)
                Discount = Double.Parse(datareader(6).ToString)
                Balance = Double.Parse(datareader(7).ToString)
                PayMode = datareader(8).ToString

                'add to grid
                dgvCreditDetailed.Rows.Add(ReceiptDate, CustomerNames, InvoiceNo, ReceiptItems, _
                                           ReceiptTotal, AmountPaid, Discount, Balance, PayMode)
                '+++++++++++++
            End While
            datareader.Dispose()
            closeconn()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Rub()
    End Sub

    Sub Rub()
        'first clear the grid
        dgvCreditDetailed.Rows.Clear()
        '############
        lblCustomerNo.Text = 0
        lblCreditBalances.Text = 0
        txtSearchCustomer.Clear()
        btnLoad.PerformClick()
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        printStatement()
    End Sub

    Private Sub printStatement()
        'Dim receiptNo As Long = Long.Parse(Now().Date.Day & Now().Date.Month & Now().Date.Year & Now().TimeOfDay.Hours & Now().TimeOfDay.Minutes)
        Dim dt As New DataTable
        Dim ReceiptDate As String, Names As String, _
            InvoiceNo As String, ReceiptItems As String, _
            ReceiptTotal As Double, Disc As Double, _
            AmountPaid As Double, Balance As Double

        With dt
            .Columns.Add("receptdate")
            .Columns.Add("custnames")
            .Columns.Add("invoiceno")
            .Columns.Add("invoiceitems")
            .Columns.Add("invoicetotal")
            .Columns.Add("amountpaid")
            .Columns.Add("disc")
            .Columns.Add("balance")
        End With

        With dgvCreditDetailed
            For i = 0 To dgvCreditDetailed.RowCount - 1
                ReceiptDate = dgvCreditDetailed.Rows(i).Cells(0).Value.ToString
                Names = dgvCreditDetailed.Rows(i).Cells(1).Value.ToString
                InvoiceNo = dgvCreditDetailed.Rows(i).Cells(2).Value.ToString
                ReceiptItems = dgvCreditDetailed.Rows(i).Cells(3).Value.ToString
                ReceiptTotal = dgvCreditDetailed.Rows(i).Cells(4).Value.ToString
                AmountPaid = dgvCreditDetailed.Rows(i).Cells(5).Value.ToString
                Disc = dgvCreditDetailed.Rows(i).Cells(6).Value.ToString
                Balance = dgvCreditDetailed.Rows(i).Cells(7).Value.ToString

                'add rows
                dt.Rows.Add(ReceiptDate, Names, InvoiceNo, ReceiptItems, ReceiptTotal, AmountPaid, Disc, Balance)
            Next
        End With
        Try
            Dim rptDoc As CrystalDecisions.CrystalReports.Engine.ReportDocument

            rptDoc = New CreditDetailedReport
            rptDoc.SetDataSource(dt)
            rptDoc.SetParameterValue("DateFrom", dtpFrom.Value.Date)
            rptDoc.SetParameterValue("DateTo", dtpTo.Value.Date)
            rptDoc.SetParameterValue("CreditTotals", lblCreditTotals.Text.ToString)
            rptDoc.SetParameterValue("CreditBalances", lblCreditBalances.Text.ToString)
            ''rptDoc.PrintToPrinter(1, False, 0, 0)
            frmPrint.CRPrint.ReportSource = rptDoc
            frmPrint.CRPrint.RefreshReport()
            frmPrint.TopMost = True
            frmPrint.StartPosition = FormStartPosition.CenterScreen
            frmPrint.Show()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class