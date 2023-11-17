Imports MySql.Data
Imports MySql.Data.MySqlClient

Public Class frmDetailedSalesReports

    Private Sub btnLoad_Click(sender As Object, e As EventArgs) Handles btnLoad.Click
        'first clear the grid
        dgvSalesDetailed.Rows.Clear()
        'reload data
        Dim dFrom As Date, dTo As Date, DateDelivered As Date,
            UnpaidTotals As Double = 0, InvoiceNo As String, _
            i As Integer = 0, SalesTotals As Double = 0

        dFrom = dtpFrom.Value.ToShortDateString
        dTo = dtpTo.Value.ToShortDateString

        'add to grid
        get_Sales_Reports(dFrom, dTo, dgvSalesDetailed)

        'loop through the grid
        For i = 0 To dgvSalesDetailed.RowCount - 1
            'progress_bar variables
            Dim percentage_Processed As Double = 0, totalRecords As Double = 0
            totalRecords = dgvSalesDetailed.RowCount - 1
            PB_Progress.Maximum = totalRecords

            If dgvSalesDetailed.Rows(i).Cells(0).Value = Nothing Then Continue For
            If dgvSalesDetailed.Rows(i).Cells(0).Value.ToString = String.Empty Then Continue For

            'assign progress_bar variables
            percentage_Processed = (i / totalRecords) * 100
            lblCount.Text = i.ToString
            lblPercentage.Text = FormatNumber(percentage_Processed, 1) & " %"

            DateDelivered = dgvSalesDetailed.Rows(i).Cells(0).Value.ToString
            InvoiceNo = dgvSalesDetailed.Rows(i).Cells(2).Value.ToString

            PB_Progress.Value = i
        Next

        'CLEAR PROGRESS BAR / CONTROLS
        PB_Progress.Value = 0
        lblCount.Text = 0
        lblPercentage.Text = 0
    End Sub

    Private Sub get_Sales_Reports(ByVal dfrom As Date, _
                                  ByVal dto As Date, _
                                  ByVal Grid As DataGridView)

        Dim datareader As MySqlDataReader
        Dim ReceiptDate As String, PurchaseMode As String, _
            ReceiptNo As String, ReceiptItems As String, _
            ReceiptTotal As Double, AmountPaid As Double, _
            Discount As Double, SalesMode As String, _
            Soldby As String, Paymode As String

        Dim strsql As String = "SELECT q1.rc1,q1.ReceiptDate,q1.ReceiptItems,q1.PMode," _
                               & " IFNULL(q1.SalesTotals,0) AS TotalSales," _
                               & " IFNULL(q1.amountpaid,0) AS AmntPaid," _
                               & " IFNULL(q1.discount,0) AS Discnt," _
                               & " q2.ReceiptPaymode, q1.SalesMode, q1.Soldby" _
                               & " FROM" _
                               & " (SELECT sm.receptno AS rc1," _
                               & " DATE_FORMAT(sm.receptdatetime, '%Y,%M,%d') AS ReceiptDate," _
                               & " GROUP_CONCAT(DISTINCT sd.quantitybought, ' - ' ,p.Description" _
                               & " ORDER BY sd.receptno SEPARATOR '  ;   \r\n') AS ReceiptItems," _
                               & " sm.purchasemode AS PMode,(sm.totalamount - sm.amountreturned) AS SalesTotals," _
                               & " sm.amountpaid,sm.discount, " _
                               & " CASE WHEN sm.salesmode='R' THEN 'Retail Sales' ELSE 'Wholesale' END AS SalesMode," _
                               & " s.fullnames AS Soldby FROM salesmaster sm " _
                               & " INNER JOIN salesdetails sd ON sd.receptno=sm.receptno" _
                               & " INNER JOIN products p ON p.Barcode=sd.itemcode" _
                               & " INNER JOIN staff s ON sm.soldby=s.staffid" _
                               & " WHERE DATE(sm.receptdatetime) BETWEEN '" & dfrom & "' AND '" & dto & "'" _
                               & " AND sm.purchasemode='Cash' GROUP BY DATE(sm.receptdatetime),sm.receptno " _
                               & " ORDER BY DATE(sm.receptdatetime) ASC) AS q1" _
                               & " Left Join" _
                               & " (SELECT sp.receiptno AS rc2," _
                               & " case when (GROUP_CONCAT(DISTINCT sp.paymode,' - ', ' Transaction No: ',' - '," _
                               & " sp.transactionno, ' - ', ' Amount: ',sp.amountpaid " _
                               & " ORDER BY sp.receiptno SEPARATOR '  ;   \r\n'))='' then 'N/A' " _
                               & " ELSE " _
                               & " (GROUP_CONCAT(DISTINCT sp.paymode,' - ', ' Transaction No: ',' - '," _
                               & " sp.transactionno, ' - ', ' Amount: ',sp.amountpaid " _
                               & " ORDER BY sp.receiptno SEPARATOR '  ;   \r\n'))" _
                               & " END AS ReceiptPaymode" _
                               & " FROM salespaymode sp " _
                               & " GROUP BY sp.receiptno ORDER BY sp.receiptno ASC ) AS q2" _
                               & " ON q1.rc1=q2.rc2 GROUP BY q1.rc1"

        Try
            Dim cmd As New MySqlCommand(strsql, Conn)
            cmd.CommandType = CommandType.Text
            datareader = cmd.ExecuteReader
            While datareader.Read
                ReceiptDate = datareader(0).ToString
                ReceiptNo = datareader(1).ToString
                ReceiptItems = datareader(2).ToString
                PurchaseMode = datareader(3).ToString
                ReceiptTotal = Double.Parse(datareader(4).ToString)
                AmountPaid = Double.Parse(datareader(5).ToString)
                Discount = Double.Parse(datareader(6).ToString)
                Paymode = datareader(7).ToString
                SalesMode = datareader(8).ToString
                Soldby = datareader(9).ToString
                'add to grid
                dgvSalesDetailed.Rows.Add(ReceiptNo, ReceiptDate, ReceiptItems, PurchaseMode, _
                                          ReceiptTotal, AmountPaid, Discount, Paymode, _
                                          SalesMode, Soldby)
                '+++++++++++++
            End While
            lblMobilePayTotals.Text = "Kshs" & " " & get_MobilePayments(dfrom, dto)
            lblSolidCashTotals.Text = "Kshs" & " " & get_SolidCash(dfrom, dto)
            datareader.Dispose()
            closeconn()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Rub()
    End Sub

    Private Function get_MobilePayments(ByVal datefrom As Date, _
                                        ByVal dateto As Date) As Double

        Dim datareader As MySqlDataReader
        Dim MobileAmnt As Double = 0

        Dim strsql As String = "SELECT SUM(sp.amountpaid) AS 'Mobile Payments'" _
                               & " FROM salespaymode sp" _
                               & " INNER JOIN salesmaster sm ON sm.receptno=sp.receiptno" _
                               & " WHERE sp.paymode='M-pesa' OR sp.paymode='Mobile Banking'" _
                               & " AND DATE(sm.receptdatetime)" _
                               & " BETWEEN '" & datefrom & "' AND '" & dateto & "'"

        Try
            Dim cmd As New MySqlCommand(strsql, Conn)
            cmd.CommandType = CommandType.Text
            datareader = cmd.ExecuteReader
            While datareader.Read
                If (datareader(0).ToString) = String.Empty Then
                    MobileAmnt = 0
                Else
                    MobileAmnt = datareader(0).ToString
                End If
            End While
            datareader.Dispose()
            closeconn()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Return MobileAmnt
    End Function

    Private Function get_SolidCash(ByVal datefrom As Date, _
                                   ByVal dateto As Date) As Double

        Dim datareader As MySqlDataReader
        Dim SolidCashAmnt As Double = 0

        Dim strsql As String = "SELECT SUM(sp.amountpaid) AS 'Solid Cash'" _
                               & " FROM salespaymode sp" _
                               & " INNER JOIN salesmaster sm ON sm.receptno=sp.receiptno" _
                               & " WHERE sp.paymode='Cash' " _
                               & " AND DATE(sm.receptdatetime)" _
                               & " BETWEEN'" & datefrom & "' AND '" & dateto & "'"

        Try
            Dim cmd As New MySqlCommand(strsql, Conn)
            cmd.CommandType = CommandType.Text
            datareader = cmd.ExecuteReader
            While datareader.Read
                If (datareader(0).ToString) = String.Empty Then
                    SolidCashAmnt = 0
                Else
                    SolidCashAmnt = datareader(0).ToString
                End If
            End While
            datareader.Dispose()
            closeconn()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Return SolidCashAmnt
    End Function

    Sub Rub()
        lblSolidCashTotals.Text = 0
        dtpFrom.Value = Today
        dtpTo.Value = Today
        lblMobilePayTotals.Text = "0.0"
        lblSolidCashTotals.Text = "0.0"
        dgvSalesDetailed.Rows.Clear()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class