Imports MySql.Data
Imports MySql.Data.MySqlClient
Public Class frmAdvancedDelivery
    Dim Received As New ClassProductsDelivery
    Dim inventorystock As New inventory

    Dim supplierno As Integer, barcode As String, itemname As String, _
             qty As Double, totalcost As Double, itemBP As Double, _
             orderno As Integer, totalitemsdelivered As Double, _
             batchno As String, expirydate As Date, _
             manufacturedby As String, manufacturedate As Date, _
             receiptnumber As String

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        'supplier validation

        If cboSuppliers.SelectedIndex = -1 Then
            MessageBox.Show("Select supplier's name", "Strawberry System", MessageBoxButtons.OK, MessageBoxIcon.Information)
            cboSuppliers.Focus()
            Exit Sub
        Else
            supplierno = cboSuppliers.SelectedValue
        End If

        'receipt validation
        receiptnumber = txtReceiptNo.Text.Trim

        'save to database
        Received.insertDeliverymaster(receiptnumber, Now(), supplierno, totalcost, "", "", 0)

        Received.insertDeliverydetails(barcode, receiptnumber, qty, itemBP, totalcost, _
                                       itemBP, qty, qty, 0, 0, batchno, expirydate, _
                                       manufacturedby, manufacturedate, 0)

        inventorystock.alterStock(barcode, qty, 1)

        'INSERT INTO STOCK MOVEMENT
        stockMovement(barcode, "purchases", "+", qty, 0, itemBP, Now(), userid, 1)

        MsgBox("Successfully saved", MsgBoxStyle.OkOnly, "Strawberry Solution")
        closeconn()
        rub()

    End Sub

    Private Sub frmAdvancedDelivery_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Load products autosearch
        Loadproducts(txtSearch)
        'supplier combo
        Dim strsql As String = "SELECT supplier_id,fullname FROM suppliers where relationtype='S'"
        loadcombo(cboSuppliers, strsql, "fullname", "supplier_id")
        'load items to grid
        LoadItems(dgvItems)
    End Sub

    'Loads AND RELOAD database table FROM Datagridview
    Sub LoadItems(ByVal dgv As DataGridView)
        Dim strsql As String = "SELECT p.Barcode,p.Description,p.Itemsperpack " _
                               & " FROM products p WHERE p.deleted=0"
        ' Try
        Dim cmd As New MySqlCommand(strsql, Conn)
        cmd.CommandType = CommandType.Text
        Dim adapter As New MySqlDataAdapter(cmd)
        Dim table As New DataTable
        adapter.Fill(table)
        'Display the data.
        dgv.DataSource = table
        closeconn()
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message)
        'End Try
        With dgv
            .Columns(0).HeaderText = "Barcode"
            .Columns(1).HeaderText = "Description"
            .Columns(2).HeaderText = "Items Per Pack"
            .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells
            .Columns(1).DefaultCellStyle.WrapMode = DataGridViewTriState.True
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        End With
    End Sub

    Private Sub dgvItems_DoubleClick(sender As Object, e As EventArgs) Handles dgvItems.DoubleClick
        Dim Barcode As String, Description As String, itemsPerPack As Integer = 0
        Barcode = dgvItems.CurrentRow.Cells(0).Value.ToString
        Description = dgvItems.CurrentRow.Cells(1).Value.ToString
        itemsPerPack = Integer.Parse(dgvItems.CurrentRow.Cells(2).Value.ToString)
        'add variables to grid
        dgvReveiveItems.Rows.Add(Barcode, Description, itemsPerPack, 0, 0, 0, "", "", "", "")
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        rub()
    End Sub

    'clear controls
    Sub rub()
        cboSuppliers.SelectedIndex = -1
        txtReceiptNo.Clear()
        dgvReveiveItems.Rows.Clear()
        lblPriceTotals.Text = 0
        btnRefresh.PerformClick()
    End Sub

    Private Sub RemoveItemToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RemoveItemToolStripMenuItem.Click
        Dim i As Integer = dgvReveiveItems.CurrentRow.Index
        dgvReveiveItems.Rows.Remove(dgvReveiveItems.Rows(i))
        dgvReveiveItems.Refresh()
        'computeTotal
        DGVcellvaluechange(dgvReveiveItems, lblPriceTotals)
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
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
                         & " AND (description='" & itemname & "') OR (barcode='" & itemname & "') "

            Dim cmd As New MySqlCommand(strsql, Conn)
            cmd.CommandType = CommandType.Text
            Dim adapter As New MySqlDataAdapter(cmd)
            Dim table As New DataTable
            adapter.Fill(table)
            'Display the data.
            dgvItems.DataSource = table
            closeconn()
        Else
            LoadItems(dgvItems)
        End If
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        'reload data
        txtSearch.Clear()
        LoadItems(dgvItems)
    End Sub

    Private Sub DGVcellvaluechange(ByVal DataGridView1 As DataGridView, _
                                 ByVal lbl As Label)
        'solves calculations on dgvproducts and computes the total price of products on order

        Try
            Dim unitprice As Double, qty As Integer, totalprice As Double
            Dim PriceTotals As Double = 0, i As Integer
            qty = Integer.Parse(DataGridView1.CurrentRow.Cells(3).Value.ToString)
            unitprice = Double.Parse(DataGridView1.CurrentRow.Cells(4).Value.ToString)
            totalprice = qty * unitprice
            DataGridView1.CurrentRow.Cells(5).Value = totalprice

            'compute total
            For i = 0 To DataGridView1.RowCount - 1
                PriceTotals += Double.Parse(DataGridView1.Rows(i).Cells(5).Value.ToString)
            Next i
            lblPriceTotals.Text = PriceTotals.ToString
        Catch ex As Exception

        End Try
    End Sub

    Private Sub dgvReveiveItems_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles dgvReveiveItems.CellValueChanged
        'compute cell values
        DGVcellvaluechange(dgvReveiveItems, lblPriceTotals)
    End Sub
End Class