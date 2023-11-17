Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports Excel = Microsoft.Office.Interop.Excel
'Imports ExcelAutoFormat = Microsoft.Office.Interop.Excel.XlRangeAutoFormat

Public Class frmSalesReports

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnLoad.Click
        Dim dfrom As Date, dto As Date
        dfrom = dtpFrom.Value.Date
        dto = dtpTo.Value.Date

        '###########################
        LoadDailyCash(dfrom, dto)
        LoadDailyCashAndCredit(dfrom, dto, 1, dgvCash)
        LoadDailyCashAndCredit(dfrom, dto, 2, dgvCredit)
        'compute cash and credits total
        computeCashTotal(dgvCash, lblReportCashSales)
        computeCashTotal(dgvCredit, lblReportCreditSales)
        'compute cash and credit discounts
        computeDailyCashTotals(dgvCash)
        computeDailyCreditTotals(dgvCredit)
        'compute gross profit
        computeGrossProfit(dfrom, dto)
        'load cashier reports
        LoadCashierSales(dfrom, dto, dgvCashierSales)
        DetailedCashierReport(dfrom, dto, dgvDetailedCashier)
        'load retail/wholesale reports
        computeRetailSales(dfrom, dto)
        computeWholeSales(dfrom, dto)

        '#### load Fast Moving Items
        'first clear grid
        dgvFastMovingItems.Rows.Clear()
        FastMovingItems(dfrom, dto, dgvFastMovingItems)

        Dim itemcode As String, desc As String, qtybought As Integer
        Dim totalQty As Double = Double.Parse(lblFastMovingItems.Text)
        'loop through the grid
        For i = 0 To dgvFastMovingItems.RowCount - 1
            ' MsgBox(DateDelivered)
            itemcode = dgvFastMovingItems.Rows(i).Cells(0).Value.ToString
            desc = dgvFastMovingItems.Rows(i).Cells(1).Value.ToString
            qtybought = Double.Parse(dgvFastMovingItems.Rows(i).Cells(2).Value.ToString)
            'dgvFastMovingItems.Rows(i).Cells(3).Value = Double.Parse(ComputePercentage(itemcode, totalQty))

            'compute Totals on labels
            totalQty += Double.Parse(dgvFastMovingItems.Rows(i).Cells(2).Value)
        Next
        lblFastMovingItems.Text = totalQty.ToString
    End Sub

    Sub LoadDailyCash(ByVal dfrom As Date, ByVal dto As Date)
        Dim totalsales As Double = 0
        Dim strsql As String = "SELECT p.receptno,GROUP_CONCAT(DISTINCT sd.quantitybought, ' * ' ,pd.Description, '='," _
                               & " (sd.quantitybought * sd.itemsellingprice) ORDER BY sd.receptno SEPARATOR '  ;  \r\n' ) AS ItemsSold," _
                               & " (sm.totalamount  - sm.salesreturns),SUM(sd.itemdiscount) AS discount," _
                               & " (sm.totalamount - (sm.discount + sm.salesreturns)) AS AmntPaid,sm.VATamount AS 'V.A.T'," _
                               & " p.paydatetime from payments p" _
                               & " INNER JOIN salesdetails sd ON p.receptno =sd.receptno" _
                               & " INNER JOIN salesmaster sm ON sm.receptno=sd.receptno " _
                               & " INNER JOIN products pd ON sd.itemcode =pd.Barcode" _
                               & " WHERE DATE(p.paydatetime) BETWEEN '" & dfrom & "'" _
                               & " AND '" & dto & "' AND sm.purchasemode='Cash' GROUP BY p.receptno"
        Try
            Dim cmd As New MySqlCommand(strsql, Conn)
            cmd.CommandType = CommandType.Text
            Dim adapter As New MySqlDataAdapter(cmd)
            Dim table As New DataTable
            adapter.Fill(table)
            'Display the data.
            dgvDisplay.DataSource = table
            closeconn()
            With dgvDisplay
                .Columns(0).HeaderText = "Receipt No"
                .Columns(1).HeaderText = "Items Bought"
                .Columns(2).HeaderText = "Total Sales"
                .Columns(3).HeaderText = "Receipt Discount"
                .Columns(4).HeaderText = "Amount Paid"
                .Columns(5).HeaderText = "V.A.T Amnt"
                .Columns(6).HeaderText = "Pay Date & Time"
                .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells
                .Columns(1).DefaultCellStyle.WrapMode = DataGridViewTriState.True
                .Columns(6).DefaultCellStyle.WrapMode = DataGridViewTriState.True
                .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
                .Columns(1).Width = 250
            End With
            For i = 0 To dgvDisplay.RowCount - 1
                totalsales = totalsales + Double.Parse(dgvDisplay.Rows(i).Cells(4).Value)
            Next
            lblTotal.Text = "KShs" & " " & totalsales.ToString
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub


    Sub LoadDailyCashAndCredit(ByVal dfrom As Date, _
                               ByVal dto As Date, _
                               ByVal SQL_String As Integer, _
                               ByVal dgv As DataGridView)

        Dim CashTotals As Double = 0, CashDiscountTotals As Double = 0, _
            CreditsTotal As Double = 0, CreditPaid As Double = 0, _
            CreditDiscTotals As Double = 0, CreditBalances As Double = 0, _
            strsql As String

        If SQL_String = 1 Then
            strsql = "SELECT sm.receptno,sm.receptdatetime," _
                     & " (sm.totalamount-sm.salesreturns)," _
                     & " (sm.totalamount - (sm.discount + sm.salesreturns)) AS AmntPaid," _
                     & " sm.discount,sm.VATamount FROM salesmaster sm " _
                     & " WHERE sm.purchasemode ='Cash' AND DATE(sm.receptdatetime)" _
                     & " BETWEEN '" & dfrom & "' AND '" & dto & "' ORDER by sm.receptdatetime ASC"
        Else

            strsql = "SELECT sm.receptno,sm.receptdatetime," _
                     & " (sm.totalamount - sm.salesreturns)," _
                     & " (sm.totalamount - (sm.discount + sm.salesreturns)) AS AmntPaid," _
                     & " sm.discount,sm.VATamount FROM salesmaster sm " _
                     & " WHERE sm.purchasemode ='CREDIT' AND DATE(sm.receptdatetime)" _
                     & " BETWEEN '" & dfrom & "' AND '" & dto & "' ORDER by sm.receptdatetime ASC"

        End If
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
                .Columns(0).HeaderText = "Receipt No"
                .Columns(1).HeaderText = "Date Sold"
                .Columns(2).HeaderText = "Total Amount"
                .Columns(3).HeaderText = "Amount Paid"
                .Columns(4).HeaderText = "Discount "
                .Columns(5).HeaderText = "VAT"
                .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells
                .Columns(1).DefaultCellStyle.WrapMode = DataGridViewTriState.True
                .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
                .Columns(5).DefaultCellStyle.Format = "n2"  'code to set decimals places
            End With

        Catch ex As Exception
            'MessageBox.Show(ex.Message)
        End Try
    End Sub

    Sub computeGrossProfit(ByVal dfrom As Date, ByVal dto As Date)
        Dim totalsales As Double = 0, totalcostsales As Double = 0, _
            totalgrossprofit As Double = 0, totaldiscount As Double = 0, _
            totalVAT As Double = 0
        Dim strsql As String = "SELECT pd.Barcode,pd.Description,SUM(sd.quantitybought) AS 'Total Qty Sold'," _
                               & " sd.itembuyingprice AS 'Item BP',sd.itemsellingprice AS 'Item SP',SUM(sd.totalcost) AS 'Total SP', " _
                               & " SUM(sd.quantitybought) * s.buyingprice AS 'Total bp', SUM(sd.itemdiscount) AS 'Disc'," _
                               & " (SUM(sd.totalcost) - ((SUM(sd.quantitybought) * s.buyingprice+SUM(sd.itemdiscount)))) AS gp," _
                               & " CASE WHEN pd.vatrated=0 THEN 'No' ELSE 'Yes' END AS 'V.A.T Rated'," _
                               & " SUM(sd.vatamount) AS 'VAT Amnt', " _
                               & " DATE_FORMAT(sm.receptdatetime, '%d %M %Y') AS 'Receipt Date' " _
                               & " FROM salesdetails sd INNER JOIN  salesmaster sm ON sm.receptno = sd.receptno " _
                               & " INNER JOIN products pd ON pd.Barcode = sd.itemcode " _
                               & " INNER JOIN stock s on s.itemcode = sd.itemcode " _
                               & " WHERE DATE(sm.receptdatetime) BETWEEN '" & dfrom & "' AND '" & dto & "' " _
                               & " GROUP BY sd.itemcode,sd.itembuyingprice,sd.vatamount,(sm.receptdatetime)" _
                               & " ORDER BY pd.Description,sd.vatamount,DATE(sm.receptdatetime) ASC"
        Try
            Dim cmd As New MySqlCommand(strsql, Conn)
            cmd.CommandType = CommandType.Text
            Dim adapter As New MySqlDataAdapter(cmd)
            Dim table As New DataTable
            adapter.Fill(table)
            'Display the data.
            dgvGrossProfit.DataSource = table
            closeconn()
            With dgvGrossProfit
                .Columns(0).HeaderText = "Barcode"
                .Columns(1).HeaderText = "Description"
                .Columns(1).Width = 250
                .Columns(2).HeaderText = "Qty Sold"
                .Columns(3).HeaderText = "Buying Price"
                .Columns(4).HeaderText = "Selling Price"
                .Columns(5).HeaderText = "Total Sales"
                .Columns(6).HeaderText = "Cost of Sales"
                .Columns(7).HeaderText = "Discounts"
                .Columns(8).HeaderText = "Gross Profit"
                .Columns(9).HeaderText = "V.A.T Rated"
                .Columns(10).HeaderText = "V.A.T Amnt"
                .Columns(11).HeaderText = "Sales Date"
                .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells
                .Columns(1).Width = 250
                .Columns(1).DefaultCellStyle.WrapMode = DataGridViewTriState.True
                .Columns(11).DefaultCellStyle.WrapMode = DataGridViewTriState.True
                .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
                .Columns(5).DefaultCellStyle.Format = "n2"  'decimals places
                .Columns(6).DefaultCellStyle.Format = "n2"  'decimals places
                .Columns(7).DefaultCellStyle.Format = "n2"  'decimals places
                .Columns(8).DefaultCellStyle.Format = "n2"  'decimals places
                .Columns(10).DefaultCellStyle.Format = "n2"  'decimals places
            End With
            For i = 0 To dgvGrossProfit.RowCount - 1
                totalsales = totalsales + Double.Parse(dgvGrossProfit.Rows(i).Cells(5).Value)
                totalcostsales = totalcostsales + Double.Parse(dgvGrossProfit.Rows(i).Cells(6).Value)
                totaldiscount = totaldiscount + Double.Parse(dgvGrossProfit.Rows(i).Cells(7).Value)
                totalgrossprofit = totalgrossprofit + Double.Parse(dgvGrossProfit.Rows(i).Cells(8).Value)
                totalVAT = totalVAT + Double.Parse(dgvGrossProfit.Rows(i).Cells(10).Value)
            Next
            lblTotalDailySales.Text = FormatCurrency(totalsales.ToString, 2)
            lblCostOfGoodSold.Text = FormatCurrency(totalcostsales.ToString, 2)
            lblGrossProfit.Text = FormatCurrency(totalgrossprofit.ToString, 2)
            lblDiscounts.Text = FormatCurrency(totaldiscount.ToString, 2)
            lblVAT_Totals.Text = FormatCurrency(totalVAT.ToString, 2)
        Catch ex As Exception
            'MessageBox.Show(ex.Message)
        End Try
    End Sub


    Sub LoadCashierSales(ByVal dfrom As Date, ByVal dto As Date, ByVal dgv As DataGridView)
        Dim strsql As String = "SELECT S.fullnames AS SoldBy,SUM((sm.totalamount - (sm.discount + sm.salesreturns))) AS 'Amnt Sold'" _
                               & " FROM salesmaster SM INNER JOIN staff S ON S.staffid=SM.soldby" _
                               & " WHERE DATE(SM.receptdatetime) BETWEEN '" & dfrom & "' AND '" & dto & "'" _
                               & " GROUP BY SM.soldby"
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
                .Columns(0).HeaderText = "Cashier Names"
                .Columns(1).HeaderText = "Sales Totals"
                .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells
                .Columns(0).DefaultCellStyle.WrapMode = DataGridViewTriState.True
                .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            End With
        Catch ex As Exception
            'MessageBox.Show(ex.Message)
        End Try
    End Sub

    'Detailed Cashier Report
    Sub DetailedCashierReport(ByVal dfrom As Date, _
                              ByVal dto As Date, _
                              ByVal dgv As DataGridView)

        Dim strsql As String = "SELECT SM.receptno,GROUP_CONCAT(DISTINCT SD.quantitybought, ' * ' ,P.Description, '='," _
                               & " (SD.quantitybought * SD.itemsellingprice) ORDER BY SD.receptno SEPARATOR '  ;  \r\n' ) AS ItemsSold," _
                               & " (SM.totalamount - SM.salesreturns),(SM.totalamount - (SM.discount + SM.salesreturns))," _
                               & " SM.discount,SM.VATamount," _
                               & " SM.receptdatetime,SM.purchasemode, S.fullnames AS 'Sold By' FROM salesmaster SM" _
                               & " INNER JOIN salesdetails SD ON SM.receptno =SD.receptno" _
                               & " INNER JOIN products P ON SD.itemcode =P.Barcode" _
                               & " INNER JOIN staff S ON S.staffid=SM.soldby" _
                               & " WHERE DATE(SM.receptdatetime) BETWEEN '" & dfrom & "'" _
                               & " AND '" & dto & "' GROUP BY SM.receptno ORDER BY S.fullnames ASC"
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
                .Columns(0).HeaderText = "Receipt No"
                .Columns(1).HeaderText = "Items Bought"
                .Columns(2).HeaderText = "Total Sales"
                .Columns(3).HeaderText = "Amount Paid"
                .Columns(4).HeaderText = "Discount"
                .Columns(5).HeaderText = "V.A.T Amnt"
                .Columns(6).HeaderText = "Purchase Date"
                .Columns(7).HeaderText = "Purchase Mode"
                .Columns(8).HeaderText = "Sold By"
                .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells
                .Columns(1).DefaultCellStyle.WrapMode = DataGridViewTriState.True
                .Columns(6).DefaultCellStyle.WrapMode = DataGridViewTriState.True
                .Columns(8).DefaultCellStyle.WrapMode = DataGridViewTriState.True
                .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
                .Columns(1).Width = 250
                .Columns(2).DefaultCellStyle.Format = "n2"  'set decimals places
                .Columns(3).DefaultCellStyle.Format = "n2"  'set decimals places
                .Columns(4).DefaultCellStyle.Format = "n2"  'set decimals places
                .Columns(5).DefaultCellStyle.Format = "n2"  'set decimals places
            End With
        Catch ex As Exception
            'MessageBox.Show(ex.Message)
        End Try
    End Sub

    Sub computeRetailSales(ByVal dfrom As Date, ByVal dto As Date)
        Dim Retailsales As Double = 0

        Dim strsql As String = "SELECT SM.receptno,SM.receptdatetime,(SM.totalamount - SM.salesreturns)," _
                               & " (SM.totalamount - (SM.discount + SM.salesreturns))," _
                               & " SM.discount,SM.VATamount,SM.purchasemode" _
                               & " FROM salesmaster SM WHERE DATE(SM.receptdatetime) BETWEEN" _
                               & " '" & dfrom & "' AND '" & dto & "' AND SM.salesmode='R'"
        Try
            Dim cmd As New MySqlCommand(strsql, Conn)
            cmd.CommandType = CommandType.Text
            Dim adapter As New MySqlDataAdapter(cmd)
            Dim table As New DataTable
            adapter.Fill(table)
            'Display the data.
            dgvRetailSales.DataSource = table
            closeconn()
            With dgvRetailSales
                .Columns(0).HeaderText = "Receip No"
                .Columns(1).HeaderText = "Date"
                .Columns(2).HeaderText = "Total Amnt"
                .Columns(3).HeaderText = "Amount Paid"
                .Columns(4).HeaderText = "Discount"
                .Columns(5).HeaderText = "VAT Amnt"
                .Columns(6).HeaderText = "Purchase Mode"
                .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells
                .Columns(1).DefaultCellStyle.WrapMode = DataGridViewTriState.True
                '.Columns(1).Width = 400
                .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
                .Columns(2).DefaultCellStyle.Format = "n2"  'code to set decimals places
            End With
            For i = 0 To dgvRetailSales.RowCount - 1
                Retailsales = Retailsales + Double.Parse(dgvRetailSales.Rows(i).Cells(2).Value)
            Next
            lblRetailTotals.Text = FormatCurrency(Retailsales.ToString, 2)
        Catch ex As Exception
            'MessageBox.Show(ex.Message)
        End Try
    End Sub

    Sub computeWholeSales(ByVal dfrom As Date, ByVal dto As Date)
        Dim Wholesales As Double = 0

        Dim strsql As String = "SELECT SM.receptno,SM.receptdatetime,(SM.totalamount - SM.salesreturns)," _
                               & " (SM.totalamount - (SM.discount + SM.salesreturns))," _
                               & " SM.discount,SM.VATamount,SM.purchasemode" _
                               & " FROM salesmaster SM WHERE DATE(SM.receptdatetime) BETWEEN" _
                               & " '" & dfrom & "' AND '" & dto & "' AND SM.salesmode='W'"
        Try
            Dim cmd As New MySqlCommand(strsql, Conn)
            cmd.CommandType = CommandType.Text
            Dim adapter As New MySqlDataAdapter(cmd)
            Dim table As New DataTable
            adapter.Fill(table)
            'Display the data.
            dgvWholesales.DataSource = table
            closeconn()
            With dgvWholesales
                .Columns(0).HeaderText = "Receip No"
                .Columns(1).HeaderText = "Date"
                .Columns(2).HeaderText = "Total Amnt"
                .Columns(3).HeaderText = "Amount Paid"
                .Columns(4).HeaderText = "Discount"
                .Columns(5).HeaderText = "VAT Amnt"
                .Columns(6).HeaderText = "Purchase Mode"
                .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells
                .Columns(1).DefaultCellStyle.WrapMode = DataGridViewTriState.True
                '.Columns(1).Width = 400
                .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
                .Columns(2).DefaultCellStyle.Format = "n2"  'code to set decimals places
            End With
            For i = 0 To dgvWholesales.RowCount - 1
                Wholesales = Wholesales + Double.Parse(dgvWholesales.Rows(i).Cells(2).Value)
            Next
            lblWholesaleTotals.Text = FormatCurrency(Wholesales.ToString, 2)
        Catch ex As Exception
            'MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub FastMovingItems(ByVal dfrom As Date, _
                                ByVal dto As Date, _
                                ByVal Grid As DataGridView)

        Dim datareader As MySqlDataReader
        Dim ItemCode As String, Desc As String, _
            QtySold As Integer = 0

        Dim strsql As String = "SELECT p.Barcode AS itemcodeno1 ,p.Description," _
                   & " SUM(sd.quantitybought) AS 'QtySold' FROM salesmaster sm" _
                   & " INNER JOIN salesdetails sd ON sd.receptno=sm.receptno" _
                   & " INNER JOIN products p ON p.Barcode=sd.itemcode" _
                   & " WHERE DATE(sm.receptdatetime)" _
                   & " BETWEEN '" & dfrom & "' AND '" & dto & "' " _
                   & " GROUP BY sd.itemcode ORDER BY SUM(sd.quantitybought) DESC," _
                   & " p.Description Asc"

        Try
            Dim cmd As New MySqlCommand(strsql, Conn)
            cmd.CommandType = CommandType.Text
            datareader = cmd.ExecuteReader
            While datareader.Read
                ItemCode = datareader(0).ToString
                Desc = datareader(1).ToString
                QtySold = Integer.Parse(datareader(2).ToString)

                'add to grid
                dgvFastMovingItems.Rows.Add(ItemCode, Desc, QtySold, 0)
                '+++++++++++++
            End While
            datareader.Dispose()
            closeconn()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    'sub to compute cash and credit totals
    Sub computeCashTotal(ByVal dgv As DataGridView, ByVal lbl As Label)
  
    End Sub

    'sub to compute cash and credit discounts
    Sub computeDailyCashTotals(ByVal dgv As DataGridView)

        Dim CashTotals As Double = 0, CashDiscountTotals As Double = 0

        'DISPLAY SALES TOTALS ON LABELS
        For i = 0 To dgvCash.RowCount - 1
            CashTotals += Double.Parse(dgv.Rows(i).Cells(3).Value)
            CashDiscountTotals += Double.Parse(dgv.Rows(i).Cells(4).Value)
        Next
        lblReportCashSales.Text = FormatCurrency(CashTotals.ToString, 2)
        lblCashDiscounts.Text = FormatCurrency(CashDiscountTotals.ToString, 2)
    End Sub

    'sub to compute cash and credit discounts
    Sub computeDailyCreditTotals(ByVal dgv As DataGridView)

        Dim CreditsTotal As Double = 0, CreditPaid As Double = 0, _
            CreditDiscTotals As Double = 0, CreditBalances As Double = 0

        'DISPLAY SALES TOTALS ON LABELS
        For i = 0 To dgv.RowCount - 1
            CreditsTotal += Double.Parse(dgv.Rows(i).Cells(2).Value)
            CreditPaid += Double.Parse(dgv.Rows(i).Cells(3).Value)
            CreditDiscTotals += Double.Parse(dgv.Rows(i).Cells(4).Value)
            CreditBalances = Double.Parse(CreditsTotal - (CreditPaid + CreditDiscTotals))
        Next
        lblReportCreditSales.Text = FormatCurrency(CreditsTotal.ToString, 2)
        lblCreditsPaid.Text = FormatCurrency(CreditPaid.ToString, 2)
        lblCreditDiscounts.Text = FormatCurrency(CreditDiscTotals.ToString, 2)
        lblCreditBalances.Text = FormatCurrency(CreditBalances.ToString, 2)
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Dim dfrom As Date = dtpFrom.Value.Date
        Dim dto As Date = dtpTo.Value.Date
        btnLoad.PerformClick()
        computeGrossProfit(dfrom, dto)
        txtSearch.Clear()
    End Sub

    Public Function ReturnDataset(ByVal QryString As String, ByVal tName As String) As DataSet
        Dim ds As New DataSet
        'Try
        Dim da As New MySqlDataAdapter(QryString, Conn)
        da.Fill(ds, tName)
        closeconn()
        Return ds
    End Function

    Private Sub frmReports_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Loadproducts(txtSearch)
    End Sub

    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        Dim dfrom As Date, dto As Date
        dfrom = dtpFrom.Value.Date
        dto = dtpTo.Value.Date
        generateHTNExcels(dfrom, dto)
    End Sub

    'GENERATE ISSUED ITEMS
    Public Sub generateHTNExcels(ByVal dfrom As Date, ByVal dto As Date)
        Try
            Dim i, j As Integer
            Dim describer As String
            Dim xlApp As Microsoft.Office.Interop.Excel.Application
            Dim xlWorkBook As Microsoft.Office.Interop.Excel.Workbook
            Dim xlWorkSheet As Microsoft.Office.Interop.Excel.Worksheet
            Dim misValue As Object = System.Reflection.Missing.Value
            xlApp = New Microsoft.Office.Interop.Excel.ApplicationClass
            xlWorkBook = xlApp.Workbooks.Add(misValue)
            ' Sheet Name or Number
            xlWorkSheet = CType(xlWorkBook.Sheets("sheet1"), Excel.Worksheet)

            Dim strsql As String = " SELECT pd.Barcode,pd.Description,SUM(sd.quantitybought)," _
                             & " s.buyingprice,sd.itemsellingprice,SUM(sd.totalcost), " _
                             & " SUM(sd.quantitybought) * s.buyingprice AS bp, " _
                             & " SUM(sd.totalcost) - (SUM(sd.quantitybought) * s.buyingprice) AS gp, " _
                             & " SUM(sd.itemdiscount),DATE_FORMAT(sm.receptdatetime, '%d %M %Y') " _
                             & " FROM salesdetails sd INNER JOIN  salesmaster sm ON sm.receptno = sd.receptno " _
                             & " INNER JOIN products pd ON pd.Barcode = sd.itemcode " _
                             & " INNER JOIN stock s on s.itemcode = sd.itemcode " _
                             & " WHERE DATE(sm.receptdatetime) BETWEEN '" & dfrom & "' AND '" & dto & "'  GROUP BY sd.itemcode "


            Dim cmd As New MySqlCommand(strsql, Conn)
            cmd.CommandType = CommandType.Text
            Dim dscmd As New MySqlDataAdapter(cmd)
            ' DataSet
            Dim ds As New DataSet
            dscmd.Fill(ds)
            'COLUMN NAME ADD IN EXCEL SHEET OR HEADING

            describer = "Sales Reports"
            xlWorkSheet.Cells(1, 1).Value = "Barcode"
            xlWorkSheet.Cells(1, 2).Value = "Description"
            xlWorkSheet.Cells(1, 3).Value = "Qty Sold"
            xlWorkSheet.Cells(1, 4).Value = "Buying Price"
            xlWorkSheet.Cells(1, 5).Value = "Selling Price"
            xlWorkSheet.Cells(1, 6).Value = "Total Sales"
            xlWorkSheet.Cells(1, 7).Value = "Cost Of Sales"
            xlWorkSheet.Cells(1, 8).Value = "Gross Profit"
            xlWorkSheet.Cells(1, 9).Value = "Discounts"
            xlWorkSheet.Cells(1, 10).Value = "Sales Date"

            xlWorkSheet.Range("A1:K1").WrapText = True
            For x = 1 To 10
                xlWorkSheet.Columns(x).ColumnWidth = 15
            Next x
            xlWorkSheet.Range("A1:K1").Rows.AutoFit()

            xlWorkSheet.Range("A1:K1").Font.Name = "Calibri"
            xlWorkSheet.Range("A1:K1").Font.Bold = True
            xlWorkSheet.Range("A1:K1").Font.Size = 12
            xlWorkSheet.Range("A1:K1").VerticalAlignment = Excel.XlVAlign.xlVAlignCenter
            xlWorkSheet.Range("A1:K1").HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter
            xlWorkSheet.Range("A1:K1").Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Silver)
            With xlWorkSheet.Range("A1:K1")
                With .Borders(Excel.XlBordersIndex.xlEdgeLeft)
                    .LineStyle = Excel.XlLineStyle.xlDouble
                    .ColorIndex = 0
                    .TintAndShade = 0
                    .Weight = Excel.XlBorderWeight.xlThin
                End With

                With .Borders(Excel.XlBordersIndex.xlEdgeTop)
                    .LineStyle = Excel.XlLineStyle.xlContinuous
                    .ColorIndex = 0
                    .TintAndShade = 0
                    .Weight = Excel.XlBorderWeight.xlThin
                End With

                With .Borders(Excel.XlBordersIndex.xlEdgeBottom)
                    .LineStyle = Excel.XlLineStyle.xlContinuous
                    .ColorIndex = 0
                    .TintAndShade = 0
                    .Weight = Excel.XlBorderWeight.xlThin
                End With

                With .Borders(Excel.XlBordersIndex.xlEdgeRight)
                    .LineStyle = Excel.XlLineStyle.xlContinuous
                    .ColorIndex = 0
                    .TintAndShade = 0
                    .Weight = Excel.XlBorderWeight.xlThin
                End With

                With .Borders(Excel.XlBordersIndex.xlInsideVertical)
                    .LineStyle = Excel.XlLineStyle.xlContinuous
                    .ColorIndex = 0
                    .TintAndShade = 0
                    .Weight = Excel.XlBorderWeight.xlThin
                End With

                With .Borders(Excel.XlBordersIndex.xlInsideHorizontal)
                    .LineStyle = Excel.XlLineStyle.xlContinuous
                    .ColorIndex = 0
                    .TintAndShade = 0
                    .Weight = Excel.XlBorderWeight.xlThin
                End With

            End With

            For i = 0 To ds.Tables(0).Rows.Count - 1
                For j = 0 To ds.Tables(0).Columns.Count - 1
                    xlWorkSheet.Cells(i + 3, j + 1) = ds.Tables(0).Rows(i).Item(j)
                    xlWorkSheet.Cells(i + 3, j + 1).WrapText = True
                    xlWorkSheet.Cells(i + 3, j + 1).VerticalAlignment = Excel.XlVAlign.xlVAlignCenter
                    xlWorkSheet.Cells(i + 3, j + 1).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter
                Next
            Next

            Dim savepath As String = "C:\Users\Public\" & describer & " " & dfrom & " TO " & dto & ".xlsx"
            ' Save as path of excel sheet
            xlWorkSheet.SaveAs(savepath)
            xlWorkBook.Close()
            xlApp.Quit()
            releaseObject(xlApp)
            releaseObject(xlWorkBook)
            releaseObject(xlWorkSheet)
            '======open generated file
            Dim xlsApp As Excel.Application = Nothing
            Dim xlsWorkBooks As Excel.Workbooks = Nothing
            Dim xlsWB As Excel.Workbook = Nothing
            Try
                xlsApp = New Excel.Application
                xlsApp.Visible = True
                xlsWorkBooks = xlsApp.Workbooks
                xlsWB = xlsWorkBooks.Open(savepath)
            Catch ex As Exception
                MsgBox(ex.Message)
            Finally
            End Try
        Catch ex As Exception
            Conn.Close()
            MsgBox(ex.Message)
        Finally
            Conn.Close()
            Conn.Dispose()
        End Try
    End Sub

    Private Sub releaseObject(ByVal obj As Object)
        Try
            System.Runtime.InteropServices.Marshal.ReleaseComObject(obj)
            obj = Nothing
        Catch ex As Exception
            obj = Nothing
        Finally
            GC.Collect()
        End Try
    End Sub

    Private Sub txtSearch_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSearch.KeyDown
        Dim dfrom As Date, dto As Date
        dfrom = dtpFrom.Value.Date
        dto = dtpTo.Value.Date
        If e.KeyCode = Keys.Enter Then
            Dim itemname As String
            If txtSearch.Text.Trim.Length > 0 Then
                itemname = txtSearch.Text.Trim
                LoadproductsSearch(dfrom, dto)
            Else
                computeGrossProfit(dfrom, dto)
            End If
        End If
    End Sub


    'Loads database table to Datagridview
    Sub LoadproductsSearch(ByVal dfrom As Date, ByVal dto As Date)
        Dim barcode As String = txtSearch.Text.Trim, _
            description As String = txtSearch.Text.Trim

        Dim totalsales As Double = 0, totalcostsales As Double = 0, _
          totalgrossprofit As Double = 0, totaldiscount As Double = 0, _
          totalVAT As Double = 0

        Dim strsql As String = "SELECT pd.Barcode,pd.Description,SUM(sd.quantitybought) AS 'Total Qty Sold'," _
                       & " sd.itembuyingprice AS 'Item BP',sd.itemsellingprice AS 'Item SP',SUM(sd.totalcost) AS 'Total SP', " _
                       & " SUM(sd.quantitybought) * s.buyingprice AS 'Total bp', SUM(sd.itemdiscount) AS 'Disc'," _
                       & " (SUM(sd.totalcost) - ((SUM(sd.quantitybought) * s.buyingprice+SUM(sd.itemdiscount)))) AS gp," _
                       & " CASE WHEN pd.vatrated=0 THEN 'No' ELSE 'Yes' END AS 'V.A.T Rated'," _
                       & " SUM(sd.vatamount) AS 'VAT Amnt', " _
                       & " DATE_FORMAT(sm.receptdatetime, '%d %M %Y') AS 'Receipt Date' " _
                       & " FROM salesdetails sd INNER JOIN  salesmaster sm ON sm.receptno = sd.receptno " _
                       & " INNER JOIN products pd ON pd.Barcode = sd.itemcode " _
                       & " INNER JOIN stock s on s.itemcode = sd.itemcode " _
                       & " WHERE pd.Barcode LIKE '" & "%" & barcode & "%" & "' OR " _
                       & " pd.Description LIKE '" & "%" & description & "%" & "'" _
                       & " AND  DATE(sm.receptdatetime) BETWEEN '" & dfrom & "' AND '" & dto & "' " _
                       & " GROUP BY sd.itemcode,sd.itembuyingprice,sd.vatamount,sm.receptdatetime" _
                       & " ORDER BY pd.Description,sd.vatamount ASC"

        Try

            Dim cmd As New MySqlCommand(strsql, Conn)
            cmd.CommandType = CommandType.Text
            Dim adapter As New MySqlDataAdapter(cmd)
            Dim table As New DataTable
            adapter.Fill(table)
            'Display the data.
            dgvGrossProfit.DataSource = table
            closeconn()
            With dgvGrossProfit
                .Columns(0).HeaderText = "Barcode"
                .Columns(1).HeaderText = "Description"
                .Columns(1).Width = 250
                .Columns(2).HeaderText = "Qty Sold"
                .Columns(3).HeaderText = "Buying Price"
                .Columns(4).HeaderText = "Selling Price"
                .Columns(5).HeaderText = "Total Sales"
                .Columns(6).HeaderText = "Cost of Sales"
                .Columns(7).HeaderText = "Discounts"
                .Columns(8).HeaderText = "Gross Profit"
                .Columns(9).HeaderText = "V.A.T Rated"
                .Columns(10).HeaderText = "V.A.T Amnt"
                .Columns(11).HeaderText = "Sales Date"
                .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells
                .Columns(1).DefaultCellStyle.WrapMode = DataGridViewTriState.True
                .Columns(11).DefaultCellStyle.WrapMode = DataGridViewTriState.True
                .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
                .Columns(5).DefaultCellStyle.Format = "n2"  'decimals places
                .Columns(6).DefaultCellStyle.Format = "n2"  'decimals places
                .Columns(7).DefaultCellStyle.Format = "n2"  'decimals places
                .Columns(8).DefaultCellStyle.Format = "n2"  'decimals places
                .Columns(10).DefaultCellStyle.Format = "n2"  'decimals places
            End With

            For i = 0 To dgvGrossProfit.RowCount - 1
                totalsales = totalsales + Double.Parse(dgvGrossProfit.Rows(i).Cells(5).Value)
                totalcostsales = totalcostsales + Double.Parse(dgvGrossProfit.Rows(i).Cells(6).Value)
                totaldiscount = totaldiscount + Double.Parse(dgvGrossProfit.Rows(i).Cells(7).Value)
                totalgrossprofit = totalgrossprofit + Double.Parse(dgvGrossProfit.Rows(i).Cells(8).Value)
                totalVAT = totalVAT + Double.Parse(dgvGrossProfit.Rows(i).Cells(10).Value)
            Next
            lblTotalDailySales.Text = FormatCurrency(totalsales.ToString, 2)
            lblCostOfGoodSold.Text = FormatCurrency(totalcostsales.ToString, 2)
            lblGrossProfit.Text = FormatCurrency(totalgrossprofit.ToString, 2)
            lblDiscounts.Text = FormatCurrency(totaldiscount.ToString, 2)
            lblVAT_Totals.Text = FormatCurrency(totalVAT.ToString, 2)

        Catch ex As Exception
            'MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        With frmReprintCashReceipt
            .StartPosition = FormStartPosition.CenterScreen
            .WindowState = FormWindowState.Normal
            .Show()
            .Location = New Point(350, 200)
        End With
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        Dim dfrom As Date, dto As Date
        dfrom = dtpFrom.Value.Date
        dto = dtpTo.Value.Date

        Dim itemname As String
        If txtSearch.Text.Trim.Length > 0 Then
            itemname = txtSearch.Text.Trim
            LoadproductsSearch(dfrom, dto)
        Else
            computeGrossProfit(dfrom, dto)
        End If
    End Sub

    Private Sub btnPrintToPDF_Click(sender As Object, e As EventArgs) Handles btnPrintToPDF.Click
        printStatement()
    End Sub

    Private Sub printStatement()
        'Dim receiptNo As Long = Long.Parse(Now().Date.Day & Now().Date.Month & Now().Date.Year & Now().TimeOfDay.Hours & Now().TimeOfDay.Minutes)
        Dim dt As New DataTable
        Dim Barcode As String, Description As String, _
            Qty As Double, Bp As Double, _
            Sp As Double, TotalSales As Double, _
            Cost As Double, Discount As Double, _
            Profit As Double, VAT_Rated As String, _
            VAT_Amnt As Double, SalesDate As String

        With dt
            .Columns.Add("barcode")
            .Columns.Add("description")
            .Columns.Add("qty")
            .Columns.Add("bp")
            .Columns.Add("sp")
            .Columns.Add("totalsales")
            .Columns.Add("cost")
            .Columns.Add("discount")
            .Columns.Add("profit")
            .Columns.Add("vat_rated")
            .Columns.Add("vat_amnt")
            .Columns.Add("saledate")
        End With

        With dgvGrossProfit
            For i = 0 To dgvGrossProfit.RowCount - 1
                Barcode = dgvGrossProfit.Rows(i).Cells(0).Value.ToString
                Description = dgvGrossProfit.Rows(i).Cells(1).Value.ToString
                Qty = Double.Parse(dgvGrossProfit.Rows(i).Cells(2).Value.ToString)
                Bp = Double.Parse(dgvGrossProfit.Rows(i).Cells(3).Value.ToString)
                Sp = Double.Parse(dgvGrossProfit.Rows(i).Cells(4).Value.ToString)
                TotalSales = Double.Parse(dgvGrossProfit.Rows(i).Cells(5).Value.ToString)
                Cost = Double.Parse(dgvGrossProfit.Rows(i).Cells(6).Value.ToString)
                Discount = Double.Parse(dgvGrossProfit.Rows(i).Cells(7).Value.ToString)
                Profit = Double.Parse(dgvGrossProfit.Rows(i).Cells(8).Value.ToString)
                VAT_Rated = dgvGrossProfit.Rows(i).Cells(9).Value.ToString
                VAT_Amnt = Double.Parse(dgvGrossProfit.Rows(i).Cells(10).Value.ToString)
                SalesDate = dgvGrossProfit.Rows(i).Cells(11).Value.ToString
                'add rows
                dt.Rows.Add(Barcode, Description, Qty, Bp, Sp, TotalSales, Cost, Discount, Profit, VAT_Rated, VAT_Amnt, SalesDate)
            Next
        End With
        Try
            Dim rptDoc As CrystalDecisions.CrystalReports.Engine.ReportDocument

            rptDoc = New SalesReport
            rptDoc.SetDataSource(dt)
            rptDoc.SetParameterValue("TotalSales", lblTotalDailySales.Text)
            rptDoc.SetParameterValue("Cost", lblCostOfGoodSold.Text)
            rptDoc.SetParameterValue("Profit", lblGrossProfit.Text)
            rptDoc.SetParameterValue("Discount", lblDiscounts.Text)
            rptDoc.SetParameterValue("VAT", lblVAT_Totals.Text)
            rptDoc.SetParameterValue("DateFrom", dtpFrom.Value.Date)
            rptDoc.SetParameterValue("DateTo", dtpTo.Value.Date)
            frmPrint.CRPrint.ReportSource = rptDoc
            frmPrint.CRPrint.RefreshReport()
            frmPrint.TopMost = True
            frmPrint.StartPosition = FormStartPosition.CenterScreen
            frmPrint.Show()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnRemoveData_Click(sender As Object, e As EventArgs) Handles btnRemoveData.Click
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        'ASSIGN USER PRIVILEDGES CODE
        If Not Authorized(userid, TaskID.delete_dummy_sales_report) Then Exit Sub
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        Dim dfrom As Date, dto As Date, strsql As String
        dfrom = dtpFrom.Value.Date
        dto = dtpTo.Value.Date

        ''delete record from database
        Try
            Dim dtaName As New MySqlClient.MySqlDataAdapter
            dtaName.UpdateCommand = New MySqlClient.MySqlCommand
            With dtaName.UpdateCommand
                .CommandTimeout = 60
                .Connection = Conn()
                .CommandType = CommandType.Text

                If (MsgBox("Are you sure you want to permanently delete all items from server between the selected dates?", MsgBoxStyle.YesNo, "Strawberry POS") = MsgBoxResult.Yes) Then
                    '########################
                    strsql = "DELETE FROM salesmaster WHERE DATE(receptdatetime)  BETWEEN '" & dfrom & "' AND '" & dto & "' AND receptno!=1"
                    .CommandText = strsql
                    .ExecuteNonQuery()

                    strsql = "DELETE FROM creditsales WHERE receiptno NOT IN (SELECT receptno FROM salesmaster) AND entryid!=1"
                    .CommandText = strsql
                    .ExecuteNonQuery()

                    strsql = "DELETE FROM salesdetails WHERE receptno NOT IN (SELECT receptno FROM salesmaster) AND receptno!=1"
                    .CommandText = strsql
                    .ExecuteNonQuery()

                    strsql = "ALTER table salesmaster AUTO_INCREMENT=2"
                    .CommandText = strsql
                    .ExecuteNonQuery()

                    strsql = "ALTER table creditsales AUTO_INCREMENT=2"
                    .CommandText = strsql
                    .ExecuteNonQuery()

                    strsql = "ALTER table salesdetails AUTO_INCREMENT=2"
                    .CommandText = strsql
                    .ExecuteNonQuery()
                    MessageBox.Show("Successfully Erased", "Strawberry System", MessageBoxButtons.OK, MessageBoxIcon.Information)

                End If
            End With
            closeconn()
            'reload data
            btnLoad.PerformClick()
        Catch ex As Exception
            If Conn.State = ConnectionState.Open Then Conn.Close()
            MsgBox(ex.Message & "An Error Occured While Trying To Delete Data From Database")
            Exit Sub
        End Try

    End Sub

    Private Sub btnCloze_Click(sender As Object, e As EventArgs) Handles btnCloze.Click
        Me.Close()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub buttonClose_Click(sender As Object, e As EventArgs)
        Me.Close()
    End Sub

    Private Sub btnExiit_Click(sender As Object, e As EventArgs) Handles btnExiit.Click
        Me.Close()
    End Sub

    Private Sub btnCloose_Click(sender As Object, e As EventArgs) Handles btnCloose.Click
        Me.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnClose0.Click
        Me.Close()
    End Sub

    Private Sub btnClose3_Click_1(sender As Object, e As EventArgs) Handles btnClose3.Click
        Me.Close()
    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click

    End Sub
    Private Sub lblTotal_Click(sender As Object, e As EventArgs) Handles lblTotal.Click

    End Sub
End Class