Imports MySql.Data
Imports MySql.Data.MySqlClient
Public Class frmStock
    Dim description As String, itemcode As String, _
        itemcategory As String, reorderlevel As Integer, _
        StockBalance As Integer, BP As Double

    Dim stock As New ClassStock

    Private Sub frmOutofStock_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'load data
        txtOutOfStockSearch.Text = ""
        LoadOutofStockdata(dgvAllStock)
        'load data
        txtAboveReOrderSearch.Text = ""
        LoadOutofStockdata(dgvAboveReorder)
        'load data
        txtAllStockSearch.Text = ""
        LoadOutofStockdata(dgvOutOfStock)
    End Sub

    'Loads AND RELOAD database table FROM Datagridview
    Sub LoadOutofStockdata(ByVal dgvdisplay As DataGridView)
        dgvdisplay.Rows.Clear()
        Dim strsql As String

        If dgvdisplay.Name = "dgvOutOfStock" Then
            strsql = "SELECT q1.Description,q1.categoryname,q1.b1," _
                     & " IFNULL(q1.reorderlevel,0) AS Re_OrderLevel," _
                     & " IFNULL(q2.quantityinstock,0) AS Qty_Instock," _
                     & " IFNULL(q2.buyingprice,0) AS BP" _
                     & " FROM" _
                     & " (SELECT p.Description,pc.categoryname,p.Barcode AS b1," _
                     & " p.reorderlevel FROM products p" _
                     & " INNER JOIN product_category pc ON " _
                     & " pc.id=p.categoryno WHERE p.deleted!=1) AS q1" _
                     & " Left Join " _
                     & " (SELECT s.itemcode AS b2,s.quantityinstock," _
                     & " s.buyingprice FROM stock s) AS q2 ON q2.b2=q1.b1" _
                     & " WHERE (q2.quantityinstock <= q1.reorderlevel)" _
                     & " ORDER BY q1.Description ASC"

        ElseIf dgvdisplay.Name = "dgvAllStock" Then
            strsql = "SELECT q1.Description,q1.categoryname,q1.b1," _
                      & " IFNULL(q1.reorderlevel,0) AS Re_OrderLevel," _
                      & " IFNULL(q2.quantityinstock,0) AS Qty_Instock," _
                      & " IFNULL(q2.buyingprice,0) AS BP" _
                      & " FROM" _
                      & " (SELECT p.Description,pc.categoryname,p.Barcode AS b1," _
                      & " p.reorderlevel FROM products p" _
                      & " INNER JOIN product_category pc ON " _
                      & " pc.id=p.categoryno WHERE p.deleted!=1) AS q1" _
                      & " Left Join " _
                      & " (SELECT s.itemcode AS b2,s.quantityinstock," _
                      & " s.buyingprice FROM stock s) AS q2 ON q2.b2=q1.b1" _
                      & " ORDER BY q1.Description ASC"

        Else
            strsql = "SELECT q1.Description,q1.categoryname,q1.b1," _
                     & " IFNULL(q1.reorderlevel,0) AS Re_OrderLevel," _
                     & " IFNULL(q2.quantityinstock,0) AS Qty_Instock," _
                     & " IFNULL(q2.buyingprice,0) AS BP" _
                     & " FROM" _
                     & " (SELECT p.Description,pc.categoryname,p.Barcode AS b1," _
                     & " p.reorderlevel FROM products p" _
                     & " INNER JOIN product_category pc ON " _
                     & " pc.id=p.categoryno WHERE p.deleted!=1) AS q1" _
                     & " Left Join " _
                     & " (SELECT s.itemcode AS b2,s.quantityinstock," _
                     & " s.buyingprice FROM stock s) AS q2 ON q2.b2=q1.b1" _
                     & " WHERE (q2.quantityinstock > q1.reorderlevel)" _
                     & " ORDER BY q1.Description ASC"
        End If

        Try

            Dim datareader As MySqlDataReader
            Dim cmd As New MySqlCommand(strsql, Conn)
            cmd.CommandType = CommandType.Text
            datareader = cmd.ExecuteReader
            While datareader.Read
                description = datareader(0).ToString
                itemcategory = datareader(1).ToString
                itemcode = datareader(2).ToString
                reorderlevel = datareader(3).ToString
                StockBalance = datareader(4).ToString
                BP = Double.Parse(datareader(5).ToString)
                'add rows to grid
                dgvdisplay.Rows.Add(description, itemcategory, itemcode, reorderlevel, StockBalance, BP, 0)
            End While
            datareader.Dispose()
            closeconn()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub StockReconcileToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles StockReconcileToolStripMenuItem.Click
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        'ASSIGN USER PRIVILEDGES CODE
        If Not Authorized(userid, TaskID.reconcile_stock) Then Exit Sub
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        If (dgvAllStock.SelectedRows.Count < 1) Then
            MessageBox.Show("Select full record to continue", "Strawberry POS", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        Else
            With frmReconcileStock
                .lblItemName.Text = dgvAllStock.CurrentRow.Cells(0).Value.ToString
                .lblBarcode.Text = dgvAllStock.CurrentRow.Cells(2).Value.ToString
                .lblSystemQty.Text = dgvAllStock.CurrentRow.Cells(4).Value.ToString
                .StartPosition = FormStartPosition.CenterScreen
                .ShowDialog()
            End With
        End If
    End Sub

    Private Sub ReconcileOutOfStockItemsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReconcileOutOfStockItemsToolStripMenuItem.Click
        If (dgvOutOfStock.SelectedRows.Count < 1) Then
            MessageBox.Show("Select full record to continue", "Strawberry POS", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        Else
            With frmReconcileStock
                .lblItemName.Text = dgvOutOfStock.CurrentRow.Cells(0).Value.ToString
                .lblBarcode.Text = dgvOutOfStock.CurrentRow.Cells(2).Value.ToString
                .lblSystemQty.Text = dgvOutOfStock.CurrentRow.Cells(4).Value.ToString
                .StartPosition = FormStartPosition.CenterScreen
                .ShowDialog()
            End With
        End If
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        LoadOutofStockdata(dgvAllStock)
        'formatOutOfStockGrid(dgvStock)
        txtAllStockSearch.Clear()
    End Sub

    Private Sub btnRefreesh_Click(sender As Object, e As EventArgs) Handles btnRefreesh.Click
        LoadOutofStockdata(dgvAboveReorder)
        'formatOutOfStockGrid(dgvOutOfStock)
        txtAboveReOrderSearch.Clear()
    End Sub

    'CODE TO AUTOSEARCH DATA
    Private Sub txtAllStockSearch_TextChanged(sender As Object, e As EventArgs) Handles txtAllStockSearch.TextChanged
        Dim itemname As String
        If txtAllStockSearch.Text.Trim.Length > 0 Then
            itemname = txtAllStockSearch.Text.Trim
            LoadAutosearch(dgvAllStock)
        Else
            LoadOutofStockdata(dgvAllStock)
        End If
    End Sub

    Private Sub txtOutOfStockSearch_TextChanged(sender As Object, e As EventArgs) Handles txtOutOfStockSearch.TextChanged
        Dim SearchName As String
        If txtOutOfStockSearch.Text.Trim.Length > 0 Then
            SearchName = txtOutOfStockSearch.Text.Trim
            LoadAutosearch(dgvOutOfStock)
        Else
            LoadOutofStockdata(dgvOutOfStock)
        End If
    End Sub

    Private Sub txtAboveReOrderSearch_TextChanged(sender As Object, e As EventArgs) Handles txtAboveReOrderSearch.TextChanged
        Dim itemname As String
        If txtAboveReOrderSearch.Text.Trim.Length > 0 Then
            itemname = txtAboveReOrderSearch.Text.Trim
            LoadAutosearch(dgvAboveReorder)
        Else
            LoadOutofStockdata(dgvAboveReorder)
        End If
    End Sub

    'Load AutoSearch
    Sub LoadAutosearch(ByVal dgv As DataGridView)
        Dim strsql As String, StockItemName As String, AboveReOrder As String, OutOfStockProduct As String
        dgv.Rows.Clear()
        StockItemName = txtAllStockSearch.Text.Trim
        OutOfStockProduct = txtOutOfStockSearch.Text.Trim
        AboveReOrder = txtAboveReOrderSearch.Text.Trim

        If dgv.Name = "dgvOutOfStock" Then
            strsql = "SELECT q1.Description,q1.categoryname,q1.b1," _
                     & " IFNULL(q1.reorderlevel,0) AS Re_OrderLevel," _
                     & " IFNULL(q2.quantityinstock,0) AS Qty_Instock," _
                     & " IFNULL(q2.buyingprice,0) AS BP" _
                     & " FROM" _
                     & " (SELECT p.Description,pc.categoryname,p.Barcode AS b1," _
                     & " p.reorderlevel FROM products p" _
                     & " INNER JOIN product_category pc ON " _
                     & " pc.id=p.categoryno WHERE p.deleted!=1) AS q1" _
                     & " Left Join " _
                     & " (SELECT s.itemcode AS b2,s.quantityinstock," _
                     & " s.buyingprice FROM stock s) AS q2 ON q2.b2=q1.b1" _
                     & " WHERE (q2.quantityinstock <= q1.reorderlevel) AND" _
                     & " q1.Description LIKE '" & "%" & OutOfStockProduct & "%" & "' OR" _
                     & " q1.categoryname  LIKE '" & "%" & OutOfStockProduct & "%" & "'" _
                     & " ORDER BY q1.Description ASC"

        ElseIf dgv.Name = "dgvAllStock" Then

            strsql = "SELECT q1.Description,q1.categoryname,q1.b1," _
                      & " IFNULL(q1.reorderlevel,0) AS Re_OrderLevel," _
                      & " IFNULL(q2.quantityinstock,0) AS Qty_Instock," _
                      & " IFNULL(q2.buyingprice,0) AS BP" _
                      & " FROM" _
                      & " (SELECT p.Description,pc.categoryname,p.Barcode AS b1," _
                      & " p.reorderlevel FROM products p" _
                      & " INNER JOIN product_category pc ON " _
                      & " pc.id=p.categoryno WHERE p.deleted!=1) AS q1" _
                      & " Left Join " _
                      & " (SELECT s.itemcode AS b2,s.quantityinstock," _
                      & " s.buyingprice FROM stock s) AS q2 ON q2.b2=q1.b1" _
                      & " WHERE q1.Description LIKE '" & "%" & StockItemName & "%" & "' OR" _
                      & " q1.categoryname  LIKE '" & "%" & StockItemName & "%" & "'" _
                      & " ORDER BY q1.Description ASC"
        Else
            strsql = "SELECT q1.Description,q1.categoryname,q1.b1," _
                     & " IFNULL(q1.reorderlevel,0) AS Re_OrderLevel," _
                     & " IFNULL(q2.quantityinstock,0) AS Qty_Instock," _
                     & " IFNULL(q2.buyingprice,0) AS BP" _
                     & " FROM" _
                     & " (SELECT p.Description,pc.categoryname,p.Barcode AS b1," _
                     & " p.reorderlevel FROM products p" _
                     & " INNER JOIN product_category pc ON " _
                     & " pc.id=p.categoryno WHERE p.deleted!=1) AS q1" _
                     & " Left Join " _
                     & " (SELECT s.itemcode AS b2,s.quantityinstock," _
                     & " s.buyingprice FROM stock s) AS q2 ON q2.b2=q1.b1" _
                     & " WHERE (q2.quantityinstock > q1.reorderlevel) AND" _
                     & " q1.Description LIKE '" & "%" & AboveReOrder & "%" & "' OR" _
                     & " q1.categoryname  LIKE '" & "%" & AboveReOrder & "%" & "'" _
                     & " ORDER BY q1.Description ASC"
        End If

        Try
            Dim datareader As MySqlDataReader
            Dim cmd As New MySqlCommand(strsql, Conn)
            cmd.CommandType = CommandType.Text
            datareader = cmd.ExecuteReader
            While datareader.Read
                description = datareader(0).ToString
                itemcategory = datareader(1).ToString
                itemcode = datareader(2).ToString
                reorderlevel = datareader(3).ToString
                StockBalance = datareader(4).ToString
                BP = Double.Parse(datareader(5).ToString)
                'add rows to grid
                dgv.Rows.Add(description, itemcategory, itemcode, reorderlevel, StockBalance, BP, 0)
            End While
            datareader.Dispose()
            closeconn()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnRefressh_Click(sender As Object, e As EventArgs) Handles btnRefressh.Click
        LoadOutofStockdata(dgvOutOfStock)
        'formatOutOfStockGrid(dgvOutOfStock)
        txtOutOfStockSearch.Clear()
    End Sub

    Private Sub btnReconcile_Click(sender As Object, e As EventArgs) Handles btnReconcileOnly.Click
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        'ASSIGN USER PRIVILEDGES CODE
        If Not Authorized(userid, TaskID.reconcile_stock) Then Exit Sub
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        Dim Desc As String, Category As String, _
            Barcode As String, StockBalance As Double = 0, _
            NewStock As Double = 0, narration As String = ""


        If dgvAllStock.RowCount > 0 Then

            If (MsgBox("Are you sure you want to reconcile only for the selected?", MsgBoxStyle.YesNo, "Strawberry POS") = MsgBoxResult.Yes) Then
                For Each Item_Row As DataGridViewRow In dgvAllStock.Rows

                    If Item_Row.Cells("ColNewQty").Value.ToString = Nothing Then Continue For
                    Desc = Item_Row.Cells("ColDescription").Value.ToString
                    Category = Item_Row.Cells("ColItemCategory").Value.ToString
                    Barcode = Item_Row.Cells("ColBarcode").Value.ToString
                    StockBalance = Double.Parse(Item_Row.Cells("ColQtyinStock").Value.ToString)
                    NewStock = Double.Parse(Item_Row.Cells("ColNewQty").Value.ToString)
                    BP = Double.Parse(Item_Row.Cells("ColBP").Value.ToString)

                    If NewStock > 0 Then

                        narration = "Reconciled Stock, ItemCode=" & Barcode & ", " & vbNewLine & " " _
                                  & " ItemName=" & Desc & ",  " & vbNewLine & " " _
                                  & " Old Qty=" & StockBalance & ",  " & vbNewLine & " " _
                                  & " New Qty=" & NewStock & ", " & vbNewLine & " " _
                                  & " Qty Variance=" & (NewStock - StockBalance) & "," & vbNewLine & "" _
                                  & " Buying Price=" & BP & "," & vbNewLine & "" _
                                  & " Stock Value =Shs. " & ((NewStock - StockBalance) * BP) & ""
                        'Update stock
                        stock.insertStock(Barcode, Desc, StockBalance, NewStock, fullname, narration, 1)

                    End If
                Next
                MessageBox.Show("Successfully reconciled", "Strawberry System", MessageBoxButtons.OK)
                closeconn()
                'load data
                txtOutOfStockSearch.Text = ""
                LoadOutofStockdata(dgvAllStock)
                LoadOutofStockdata(dgvOutOfStock)
            End If
        End If
        Exit Sub
    End Sub

    Private Sub btnReconcileAndReset_Click(sender As Object, e As EventArgs) Handles btnReconcileAndReset.Click
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        'ASSIGN USER PRIVILEDGES CODE
        If Not Authorized(userid, TaskID.reconcile_stock) Then Exit Sub
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        Dim Desc As String, Category As String, _
            Barcode As String, StockBalance As Double = 0, _
            NewStock As Double = 0, narration As String = ""


        If dgvAllStock.RowCount > 0 Then

            If (MsgBox("Are you sure you want to reconcile and reset others to zero?", MsgBoxStyle.YesNo, "Strawberry POS") = MsgBoxResult.Yes) Then

                For Each Item_Row As DataGridViewRow In dgvAllStock.Rows

                    If Item_Row.Cells("ColNewQty").Value.ToString = Nothing Then Continue For
                    Desc = Item_Row.Cells("ColDescription").Value.ToString
                    Category = Item_Row.Cells("ColItemCategory").Value.ToString
                    Barcode = Item_Row.Cells("ColBarcode").Value.ToString
                    StockBalance = Double.Parse(Item_Row.Cells("ColQtyinStock").Value.ToString)
                    NewStock = Double.Parse(Item_Row.Cells("ColNewQty").Value.ToString)
                    BP = Double.Parse(Item_Row.Cells("ColBP").Value.ToString)

                    If NewStock > 0 Then
                        ''update to reset
                        Dim strsql As String
                        Dim dtaName As New MySqlClient.MySqlDataAdapter
                        ''#########

                        For i = 0 To (dgvAllStock.RowCount - 1)
                            Dim ReconcileQty As Double
                            If Double.Parse(dgvAllStock.Rows(i).Cells(6).Value) > 0 Then
                                ReconcileQty = Double.Parse(dgvAllStock.Rows(i).Cells(6).Value)
                                strsql = "select count(quantityinstock) from stock where quantityinstock= " & ReconcileQty & " "
                                Dim datareader As MySqlDataReader
                                Dim cmd As New MySqlCommand(strsql, Conn)
                                cmd.CommandType = CommandType.Text
                                datareader = cmd.ExecuteReader
                                While datareader.Read
                                    If datareader(0) > 0 Then
                                        dtaName.UpdateCommand = New MySqlClient.MySqlCommand
                                        With dtaName.UpdateCommand
                                            .CommandTimeout = 60
                                            .Connection = Conn()
                                            .CommandType = CommandType.Text

                                            strsql = "UPDATE stock SET quantityinstock = 0 WHERE quantityinstock!=" & ReconcileQty & ""
                                            'MsgBox(strsql)
                                            .CommandText = strsql
                                            .ExecuteNonQuery()
                                        End With
                                        dtaName.Dispose()
                                        closeconn()
                                    End If
                                End While
                                datareader.Dispose()
                                closeconn()
                            End If
                        Next
                        '########## below here
                        narration = "Reconciled Stock, ItemCode=" & Barcode & ", " & vbNewLine & " " _
                                                    & " ItemName=" & Desc & ",  " & vbNewLine & " " _
                                                    & " Old Qty=" & StockBalance & ",  " & vbNewLine & " " _
                                                    & " New Qty=" & NewStock & ", " & vbNewLine & " " _
                                                    & " Qty Variance=" & (NewStock - StockBalance) & "," & vbNewLine & "" _
                                                    & " Buying Price=" & BP & "," & vbNewLine & "" _
                                                    & " Stock Value =Shs. " & ((NewStock - StockBalance) * BP) & ""
                        'Update stock
                        stock.insertStock(Barcode, Desc, StockBalance, NewStock, fullname, narration, 1)
                    End If
                Next
                MessageBox.Show("Successfully reconciled", "Strawberry System", MessageBoxButtons.OK)
                closeconn()
                'load data
                txtOutOfStockSearch.Text = ""
                txtAllStockSearch.Text = ""
                txtAboveReOrderSearch.Text = ""
                LoadOutofStockdata(dgvAllStock)
                LoadOutofStockdata(dgvAboveReorder)
                LoadOutofStockdata(dgvOutOfStock)
            End If
        End If
        Exit Sub
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        printStatement()
    End Sub

    Private Sub printStatement()
        'Dim receiptNo As Long = Long.Parse(Now().Date.Day & Now().Date.Month & Now().Date.Year & Now().TimeOfDay.Hours & Now().TimeOfDay.Minutes)
        Dim dt As New DataTable
        Dim Description As String, Category As String, _
            Barcode As String, Re_OrderLevel As Integer, _
            Qty As Double

        With dt
            .Columns.Add("description")
            .Columns.Add("category")
            .Columns.Add("barcode")
            .Columns.Add("re_order_level")
            .Columns.Add("qty")
        End With

        With dgvOutOfStock
            For i = 0 To dgvOutOfStock.RowCount - 1
                Description = dgvOutOfStock.Rows(i).Cells(0).Value.ToString
                Category = dgvOutOfStock.Rows(i).Cells(1).Value.ToString
                Barcode = dgvOutOfStock.Rows(i).Cells(2).Value.ToString
                Re_OrderLevel = Integer.Parse(dgvOutOfStock.Rows(i).Cells(3).Value.ToString)
                Qty = Double.Parse(dgvOutOfStock.Rows(i).Cells(4).Value.ToString)
                'add rows
                dt.Rows.Add(Description, Category, Barcode, Re_OrderLevel, Qty)
            Next
        End With
        Try
            Dim rptDoc As CrystalDecisions.CrystalReports.Engine.ReportDocument
            rptDoc = New Out_Of_Stock
            rptDoc.SetDataSource(dt)
            'rptDoc.SetParameterValue("TotalSales", lblTotalDailySales.Text)
            'rptDoc.SetParameterValue("Cost", lblCostOfGoodSold.Text)
            frmPrint.CRPrint.ReportSource = rptDoc
            frmPrint.CRPrint.RefreshReport()
            frmPrint.TopMost = True
            frmPrint.StartPosition = FormStartPosition.CenterScreen
            frmPrint.Show()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnClose1_Click(sender As Object, e As EventArgs) Handles btnClose1.Click
        Me.Close()
    End Sub

    Private Sub btnClose2_Click(sender As Object, e As EventArgs) Handles btnClose2.Click
        Me.Close()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.Close()
    End Sub
End Class