Imports MySql.Data
Imports MySql.Data.MySqlClient

Public Class frmStockValue
    'Loads database table to Datagridview
    Sub LoadStockValueData(ByVal dgv As DataGridView)
        Dim StockValueTotals As Double = 0

        Dim strsql As String = "SELECT p.Barcode,pc.categoryname,p.Description,SUM(s.quantityinstock) AS Qty," _
                               & " s.buyingprice AS 'Unit Price'," _
                               & " ROUND((s.quantityinstock* s.buyingprice),2) AS 'StockValue' FROM stock s" _
                               & " INNER JOIN products p ON p.Barcode=s.itemcode" _
                               & " INNER JOIN product_category pc ON pc.id=p.categoryno" _
                               & " WHERE p.deleted=0 AND s.quantityinstock !=0 " _
                               & " GROUP BY s.itemcode ASC ORDER BY pc.categoryname, p.Description ASC"
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
                .Columns(1).HeaderText = "Category"
                .Columns(2).HeaderText = " Description "
                .Columns(3).HeaderText = "Qty"
                .Columns(4).HeaderText = "Unit Price"
                .Columns(5).HeaderText = "Stock Value"
                .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells
                .Columns(1).DefaultCellStyle.WrapMode = DataGridViewTriState.True
                .Columns(2).DefaultCellStyle.WrapMode = DataGridViewTriState.True
                .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
                .Columns(4).DefaultCellStyle.Format = "n2"  'code to set dgv cell to a specific number of decimals
                .Columns(5).DefaultCellStyle.Format = "n2"  'code to set dgv cell to a specific number of decimals
            End With

            For i = 0 To dgvStockValue.RowCount - 1
                StockValueTotals = StockValueTotals + Double.Parse(dgvStockValue.Rows(i).Cells(5).Value)
            Next
            lblStockValue.Text = "KShs" & " " & StockValueTotals.ToString
            'lblStockValue.Text = FormatCurrency(StockValueTotals.ToString, 2)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    'Loads database table to Datagridview
    Sub StockValueAutosearch(ByVal dgv As DataGridView)

        Dim itemname As String = txtSearch.Text.Trim
        Dim StockValueTotals As Double = 0

        Dim strsql As String = "SELECT p.Barcode,pc.categoryname,p.Description,SUM(s.quantityinstock) AS Qty," _
                               & " s.buyingprice AS 'Unit Price'," _
                               & " ROUND((s.quantityinstock* s.buyingprice),2) AS 'StockValue' FROM stock s" _
                               & " INNER JOIN products p ON p.Barcode=s.itemcode" _
                               & " INNER JOIN product_category pc ON pc.id=p.categoryno" _
                               & " WHERE p.deleted=0 AND s.quantityinstock>0 AND" _
                               & " p.Barcode LIKE '" & "%" & itemname & "%" & "' OR " _
                               & " pc.categoryname LIKE '" & "%" & itemname & "%" & "' OR " _
                               & " p.Description LIKE '" & "%" & itemname & "%" & "' " _
                               & " GROUP BY s.itemcode ASC ORDER BY pc.categoryname, p.Description ASC"

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
                .Columns(1).HeaderText = "Category"
                .Columns(2).HeaderText = " Description "
                .Columns(3).HeaderText = "Qty"
                .Columns(4).HeaderText = "Unit Price"
                .Columns(5).HeaderText = "Stock Value"
                .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells
                .Columns(1).DefaultCellStyle.WrapMode = DataGridViewTriState.True
                .Columns(2).DefaultCellStyle.WrapMode = DataGridViewTriState.True
                .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
                .Columns(4).DefaultCellStyle.Format = "n2"  'code to set decimals
                .Columns(5).DefaultCellStyle.Format = "n2"  'code to set decimals
            End With

            For i = 0 To dgvStockValue.RowCount - 1
                StockValueTotals = StockValueTotals + Double.Parse(dgvStockValue.Rows(i).Cells(5).Value)
            Next
            lblStockValue.Text = "KShs" & " " & StockValueTotals.ToString
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        LoadStockValueData(dgvStockValue)
        txtSearch.Clear()
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        Dim itemname As String
        Dim Barcode As String
        If txtSearch.Text.Trim.Length > 0 Then
            itemname = txtSearch.Text.Trim
            Barcode = txtSearch.Text.Trim
            StockValueAutosearch(dgvStockValue)
        Else
            LoadStockValueData(dgvStockValue)
        End If
    End Sub

    Private Sub frmStockValue_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadStockValueData(dgvStockValue)
    End Sub


    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class