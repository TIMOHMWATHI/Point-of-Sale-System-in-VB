Imports MySql.Data
Imports MySql.Data.MySqlClient

Public Class frmStockMainReports

    Private Sub btnLoad_Click(sender As Object, e As EventArgs) Handles btnLoad.Click
        Dim StockDate As Date
        StockDate = dtpStockDate.Value.Date

        'MsgBox(dtpFrom.Value.Date)
        MainStockReport(StockDate, dgvStock)
    End Sub

    Sub MainStockReport(ByVal STOCK_DATE As Date, ByVal dgv As DataGridView)

        Dim strsql As String = " SELECT y.Barcode, y.Description, (y.opst + y.StockIn - y.StockOut) AS 'Openning Stock', " _
                               & " y.TodayStockIn AS 'Stockin'," _
                               & " ((y.opst + y.StockIn - y.StockOut) + y.TodayStockIn) as 'TotalStock'," _
                               & " y.TodayStockOut as 'Stock Out'," _
                               & " (y.opst + y.StockIn - y.StockOut +y.TodayStockIn - y.TodayStockOut) AS 'Closing Stock'" _
                               & " FROM " _
                               & " (SELECT p.Barcode,p.Description," _
                               & " SUM(if (smt.effect='op' AND YEAR(smt.movementdate)= YEAR('" & STOCK_DATE & "')," _
                               & " ABS(smt.quantity),0))AS opst," _
                               & " smt.movementdate < '" & STOCK_DATE & "' AND YEAR(smt.movementdate)= YEAR('" & STOCK_DATE & "')," _
                               & " SUM(if (smt.effect='+' AND smt.movementdate < '" & STOCK_DATE & "' AND" _
                               & " YEAR(smt.movementdate)= YEAR('" & STOCK_DATE & "'),ABS(smt.quantity),0))AS StockIn," _
                               & " SUM(if (smt.effect='-' AND smt.movementdate < '" & STOCK_DATE & "' AND" _
                               & " YEAR(smt.movementdate)= YEAR('" & STOCK_DATE & "'),ABS(smt.quantity),0))AS StockOut," _
                               & " SUM(if (smt.effect='+' AND smt.movementdate = '" & STOCK_DATE & "'," _
                               & " ABS(smt.quantity),0))AS TodayStockIn," _
                               & " SUM(if (smt.effect='-' AND smt.movementdate = '" & STOCK_DATE & "'," _
                               & " ABS(smt.quantity),0))AS TodayStockOut" _
                               & " FROM stockmovement smt LEFT JOIN products p ON p.Barcode = smt.itemcode" _
                               & " GROUP BY p.Barcode ORDER BY p.Description) as y "

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
                .Columns(0).HeaderText = "Barcode"
                .Columns(1).HeaderText = "Description"
                .Columns(2).HeaderText = "Opening Stock"
                .Columns(3).HeaderText = "Stock In"
                .Columns(4).HeaderText = "Total Stock"
                .Columns(5).HeaderText = "Stock Out"
                .Columns(6).HeaderText = "Closing Stock"
                .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells
                .Columns(0).DefaultCellStyle.WrapMode = DataGridViewTriState.True
                .Columns(1).DefaultCellStyle.WrapMode = DataGridViewTriState.True
                .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
                '.Columns(5).DefaultCellStyle.Format = "n2"  'code to set decimals places
            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    'Loads Autosearch products
    Sub StockAutosearch(ByVal STOCK_DATE As Date, ByVal itemname As String)
        Dim strsql As String = " SELECT y.Barcode, y.Description, (y.opst + y.StockIn - y.StockOut) AS 'Openning Stock', " _
                               & " y.TodayStockIn AS 'Stockin'," _
                               & " ((y.opst + y.StockIn - y.StockOut) + y.TodayStockIn) as 'TotalStock'," _
                               & " y.TodayStockOut as 'Stock Out'," _
                               & " (y.opst + y.StockIn - y.StockOut +y.TodayStockIn - y.TodayStockOut) AS 'Closing Stock'" _
                               & " FROM " _
                               & " (SELECT p.Barcode,p.Description," _
                               & " SUM(if (smt.effect='op' AND YEAR(smt.movementdate)= YEAR('" & STOCK_DATE & "')," _
                               & " ABS(smt.quantity),0))AS opst," _
                               & " smt.movementdate < '" & STOCK_DATE & "' AND YEAR(smt.movementdate)= YEAR('" & STOCK_DATE & "')," _
                               & " SUM(if (smt.effect='+' AND smt.movementdate < '" & STOCK_DATE & "' AND" _
                               & " YEAR(smt.movementdate)= YEAR('" & STOCK_DATE & "'),ABS(smt.quantity),0))AS StockIn," _
                               & " SUM(if (smt.effect='-' AND smt.movementdate < '" & STOCK_DATE & "' AND" _
                               & " YEAR(smt.movementdate)= YEAR('" & STOCK_DATE & "'),ABS(smt.quantity),0))AS StockOut," _
                               & " SUM(if (smt.effect='+' AND smt.movementdate = '" & STOCK_DATE & "'," _
                               & " ABS(smt.quantity),0))AS TodayStockIn," _
                               & " SUM(if (smt.effect='-' AND smt.movementdate = '" & STOCK_DATE & "'," _
                               & " ABS(smt.quantity),0))AS TodayStockOut" _
                               & " FROM stockmovement smt LEFT JOIN products p ON p.Barcode = smt.itemcode WHERE " _
                               & " p.Description LIKE '" & "%" & itemname & "%" & "' OR " _
                               & " p.Barcode  LIKE '" & "%" & itemname & "%" & "') as y "

        Try
            Dim cmd As New MySqlCommand(strsql, Conn)
            cmd.CommandType = CommandType.Text
            Dim adapter As New MySqlDataAdapter(cmd)
            Dim table As New DataTable
            adapter.Fill(table)
            'Display the data.
            dgvStock.DataSource = table
            adapter.Dispose()
            closeconn()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        '& " p.Description LIKE '" & "%" & txtSearch.Text & "%" & "' OR " _
        '                       & " pl.productid LIKE '" & "%" & txtSearch.Text & "%" & "' " 
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        printStatement()
    End Sub

    Private Sub printStatement()
        'Dim receiptNo As Long = Long.Parse(Now().Date.Day & Now().Date.Month & Now().Date.Year & Now().TimeOfDay.Hours & Now().TimeOfDay.Minutes)
        Dim dt As New DataTable
        Dim Barcode As String, Description As String, _
            OpeningStock As String, _
            StockIn As String, StockTotals As String, _
            StockOut As String, ClosingStock As String

        With dt
            .Columns.Add("barcode")
            .Columns.Add("description")
            .Columns.Add("openingstock")
            .Columns.Add("stockin")
            .Columns.Add("stocktotal")
            .Columns.Add("stockout")
            .Columns.Add("closingstock")
        End With

        With dgvStock
            For i = 0 To dgvStock.RowCount - 1
                Barcode = dgvStock.Rows(i).Cells(0).Value.ToString
                Description = dgvStock.Rows(i).Cells(1).Value.ToString
                OpeningStock = dgvStock.Rows(i).Cells(2).Value.ToString
                StockIn = dgvStock.Rows(i).Cells(3).Value.ToString
                StockTotals = dgvStock.Rows(i).Cells(4).Value.ToString
                StockOut = dgvStock.Rows(i).Cells(5).Value.ToString
                ClosingStock = dgvStock.Rows(i).Cells(6).Value.ToString
                'add rows
                dt.Rows.Add(Barcode, Description, OpeningStock, StockIn, StockTotals, StockOut, ClosingStock)
            Next
        End With
        Try
            Dim rptDoc As CrystalDecisions.CrystalReports.Engine.ReportDocument

            rptDoc = New StockReport
            rptDoc.SetDataSource(dt)
            rptDoc.SetParameterValue("StockDate", dtpStockDate.Value.Date)
            frmPrint.CRPrint.ReportSource = rptDoc
            frmPrint.CRPrint.RefreshReport()
            frmPrint.TopMost = True
            frmPrint.StartPosition = FormStartPosition.CenterScreen
            frmPrint.Show()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        Dim itemname As String, Stock_Date As Date
        Stock_Date = dtpStockDate.Value

        If txtSearch.Text.Trim.Length > 0 Then
            itemname = txtSearch.Text.Trim

            StockAutosearch(Stock_Date, itemname)
        Else
            MainStockReport(Stock_Date, dgvStock)
        End If
    End Sub

    Private Sub frmStockMainReports_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class