Imports MySql.Data
Imports MySql.Data.MySqlClient

Public Class frmProductsOrder
    Dim items As New ClassProducts
    Dim supplier As New ClassSuppliers
    Dim orders As New ClassOrder
    Dim strsql As String
    'on formload event
    Private Sub frmProductsOrder_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        items.selectitems(Today, dgvItems, 3, "")
        formatgrid()
        supplier.loadcombo(cboSupplier)
        ''LOAD AUTOSEARCH
        'Loadproducts(txtSearch)
    End Sub
    'Alters dgvItems content 
    Sub formatgrid()
        With dgvItems
            .Columns(0).HeaderText = "Barcode"
            .Columns(1).HeaderText = "Description"
            .Columns(2).HeaderText = "Items Per Pack/Carton"
            .Columns(3).HeaderText = "Buying Price"
            .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells
            .Columns(1).DefaultCellStyle.WrapMode = DataGridViewTriState.True
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        End With
    End Sub

    'transfer products from items data to where ordering will be done
    Private Sub dgvItems_DoubleClick(sender As Object, e As EventArgs) Handles dgvItems.DoubleClick
        Dim bcode As String, itemname As String, itemsperpack As Integer, _
            BP As Double, TotalCost As Double
        bcode = dgvItems.CurrentRow.Cells(0).Value.ToString
        itemname = dgvItems.CurrentRow.Cells(1).Value.ToString
        itemsperpack = Integer.Parse(dgvItems.CurrentRow.Cells(2).Value.ToString)
        BP = Double.Parse(dgvItems.CurrentRow.Cells(3).Value.ToString)
        ''compute total cost
        TotalCost = Double.Parse((dgvItems.CurrentRow.Cells(2).Value) * BP)
        'adds new row in datagrid order after doubleclick of items grid
        dgvProductOrder.Rows.Add(bcode, itemname, itemsperpack, 1, BP, TotalCost)

        'compute total cost
        datagridcellvaluechange(dgvProductOrder, txtTotal)

    End Sub

    Private Sub datagridcellvaluechange(ByVal DataGridView1 As DataGridView, _
                                        ByVal txttotal As TextBox)
        'solves calculations on dgvproducts and computes the total price of products on order

        Try
            Dim unitprice As Double, qty As Integer, totalprice As Double
            Dim ordercost As Double = 0, i As Integer
            unitprice = Double.Parse(DataGridView1.CurrentRow.Cells(4).Value.ToString)
            qty = Integer.Parse(DataGridView1.CurrentRow.Cells(3).Value.ToString)
            totalprice = qty * unitprice
            DataGridView1.CurrentRow.Cells(5).Value = totalprice

            'compute total
            For i = 0 To DataGridView1.RowCount - 1
                ordercost += Double.Parse(DataGridView1.Rows(i).Cells(5).Value.ToString)
            Next i
            txttotal.Text = ordercost.ToString
        Catch ex As Exception

        End Try
    End Sub


    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        'item declaration on order master
        Dim supplierid As Integer, _
             dateordered As Date, _
             orderamount As Double
        'item declaration on order details
        Dim orderno As Integer, _
            itemid As String, _
            quantityordered As Integer, _
            unitprice As Double, _
            totalprice As Double
        'looping variable
        Dim i As Integer
        'data validation
        If (cboSupplier.SelectedIndex = -1) Or (cboSupplier.Text = "-Select Supplier-") Then
            MessageBox.Show("Select Supplier", "Strawberry Solution", MessageBoxButtons.OK, MessageBoxIcon.Error)
            cboSupplier.Focus()
            Exit Sub
        Else
            supplierid = Integer.Parse(cboSupplier.SelectedValue)
        End If

        dateordered = dtpDateOrdered.Value.ToLongDateString

        If ((txtTotal.Text = "0") Or (txtTotal.Text.Trim = String.Empty)) Then
            MessageBox.Show("Order Total Amount Missing", "Strawberry Solution", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        Else
            orderamount = Double.Parse(txtTotal.Text)
        End If
        'save in ordermaster
        orders.insertOrderMaster(supplierid, dateordered, orderamount)
        'save in orderDetails
        orderno = orders.getNextOrderNo()
        For i = 0 To dgvProductOrder.RowCount - 1
            itemid = dgvProductOrder.Rows(i).Cells(0).Value.ToString
            quantityordered = dgvProductOrder.Rows(i).Cells(3).Value.ToString
            unitprice = dgvProductOrder.Rows(i).Cells(4).Value.ToString
            totalprice = dgvProductOrder.Rows(i).Cells(5).Value.ToString
            orders.insertOrderDetails(orderno, itemid, quantityordered, unitprice, totalprice)
        Next
        MessageBox.Show("Transaction completed successfully", "Place Order", MessageBoxButtons.OK, MessageBoxIcon.Information)
        clearcontent()
    End Sub

    'clear content
    Sub clearcontent()
        dtpDateOrdered.Value = Today
        cboSupplier.SelectedIndex = -1
        dgvProductOrder.Rows.Clear()
        txtTotal.Clear()
    End Sub

    'exit form
    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        'If (MsgBox("Are you sure you want to close this page?", MsgBoxStyle.YesNo, "Product Order") = MsgBoxResult.Yes) Then
        Me.Close()
        'End If
    End Sub

    Private Sub btnEmail_Click(sender As Object, e As EventArgs) Handles btnEmail.Click
        With frmEmail
            .StartPosition = FormStartPosition.CenterScreen
            .WindowState = FormWindowState.Normal
            .FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
            .TopMost = True
            .MaximizeBox = False
            .MinimizeBox = False
            .Show()
        End With
    End Sub

    Private Sub txtSearch_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSearch.KeyDown
        If e.KeyCode = Keys.Enter Then
            'input validation on txtfarmernames
            Dim itemname As String
            If (txtSearch.Text = String.Empty) Then
                Exit Sub
            Else
                itemname = txtSearch.Text.Trim
            End If
            'Try
            Dim strsql = "SELECT barcode,description,itemsperpack from products WHERE deleted=0" _
                         & " AND (description LIKE '" & "%" & itemname & "%" & "')" _
                         & " OR (barcode LIKE '" & "%" & itemname & "%" & "') "

            Dim cmd As New MySqlCommand(strsql, Conn)
            cmd.CommandType = CommandType.Text
            Dim adapter As New MySqlDataAdapter(cmd)
            Dim table As New DataTable
            adapter.Fill(table)
            'Display the data.
            dgvItems.DataSource = table
            closeconn()
        Else
            items.selectitems(Today, dgvItems, 3, "")
            formatgrid()
        End If
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message)
        'End Try
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
                  'input validation on txtfarmernames
        Dim itemname As String
        If (txtSearch.Text = String.Empty) Then
            Exit Sub
        Else
            itemname = txtSearch.Text.Trim
        End If
        'Try
        Dim strsql = "SELECT q2.b2,q1.Description,q1.Itemsperpack," _
                     & " IFNULL(q2.buyingprice,0) AS BP" _
                     & " FROM" _
                     & " (SELECT p.Barcode AS b1,p.Description,p.Itemsperpack" _
                     & " FROM products p WHERE p.deleted!=1) AS q1" _
                     & " Left Join" _
                     & " (SELECT pl.productid AS b2,s.buyingprice" _
                     & " FROM pricelist pl" _
                     & " INNER JOIN stock s ON s.itemcode=pl.productid WHERE pl.active!=0) AS q2 " _
                     & " ON q2.b2=q1.b1 WHERE" _
                     & " (q1.Description LIKE '" & "%" & itemname & "%" & "')" _
                     & " OR (q2.b2 LIKE '" & "%" & itemname & "%" & "') GROUP BY q2.b2 "

        Dim cmd As New MySqlCommand(strsql, Conn)
        cmd.CommandType = CommandType.Text
        Dim adapter As New MySqlDataAdapter(cmd)
        Dim table As New DataTable
        adapter.Fill(table)
        'Display the data.
        dgvItems.DataSource = table
        closeconn()
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        txtSearch.Clear()
        items.selectitems(Today, dgvItems, 3, "")
        formatgrid()
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        rub()
    End Sub

    'clear control
    Sub rub()
        cboSupplier.SelectedIndex = -1
        dtpDateOrdered.Value = Date.Today
        dgvProductOrder.Rows.Clear()
        txtTotal.Clear()
    End Sub

    Private Sub dgvProductOrder_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles dgvProductOrder.CellValueChanged
        'call value changed method to compute total price as quantity /price changes
        datagridcellvaluechange(dgvProductOrder, txtTotal)
    End Sub

    Private Sub dgvProductOrder_CurrentCellDirtyStateChanged(sender As Object, e As EventArgs) Handles dgvProductOrder.CurrentCellDirtyStateChanged
        If dgvProductOrder.IsCurrentCellDirty Then
            dgvProductOrder.CommitEdit(DataGridViewDataErrorContexts.Commit)
        End If
    End Sub

    Private Sub dgvProductOrder_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvProductOrder.CellContentClick

    End Sub
End Class