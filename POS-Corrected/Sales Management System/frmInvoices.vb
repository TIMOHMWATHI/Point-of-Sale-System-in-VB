Imports MySql.Data
Imports MySql.Data.MySqlClient

Public Class frmInvoices
    Dim inv As New ClassInvoices
    Dim inventorystock As New inventory
    Dim sale As New posClass

    Dim InvoiceNo As String = "S" & Long.Parse(Now().Date.Day & Now().Date.Month & Now().Date.Year & Now().TimeOfDay.Hours & Now().TimeOfDay.Minutes)

    Private Sub frmBranches_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'load items
        Loadproducts(txtSearch)
        'reload data
        inv.LoadBranches(dgvBranches)
        'load combo
        Dim strsql As String = "SELECT entryno,branchname FROM salesbranches  WHERE deleted=0 ORDER BY branchname ASC"
        loadcombo(cboBranches, strsql, "branchname", "entryno")
    End Sub

    Private Sub txtSearch_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSearch.KeyDown
        'retreave item using its barcode
        Dim datareader As MySqlDataReader
        Dim itemname As String = "", price As Double = 0, _
            barcode As String = "", _
            ItemBP As Double = 0

        If e.KeyCode = Keys.Enter Then    'work if enter button is clicked
            'Try
            'input validation on txtbarcode
            Dim itemcode As String
            If (txtSearch.Text = String.Empty) Then
                Exit Sub
            Else
                itemcode = txtSearch.Text.Trim
            End If

            If rdbWholeSalePrice.Checked Then
                strsql = "SELECT p.Barcode,p.Description,pl.wholesaleprice FROM " _
                         & " products p INNER JOIN pricelist pl ON p.Barcode=pl.productid" _
                         & " WHERE  pl.active=1 AND p.deleted= 0 AND (p.Barcode='" & itemcode & "' " _
                         & " OR p.Description = '" & itemcode & "') LIMIT 1"
            Else
                strsql = "SELECT p.Barcode,p.Description,s.buyingprice FROM " _
                         & " products p INNER JOIN stock s ON s.itemcode=p.Barcode WHERE" _
                         & " p.deleted= 0 AND (p.Barcode='" & itemcode & "' " _
                         & " OR p.Description = '" & itemcode & "') LIMIT 1"
            End If

            Dim cmd As New MySqlCommand(strsql, Conn)
            cmd.CommandType = CommandType.Text
            datareader = cmd.ExecuteReader
            While datareader.Read
                barcode = datareader(0).ToString
                itemname = datareader(1).ToString
                price = Double.Parse(datareader(2).ToString)

                dgvInvoice.Rows.Add(barcode, itemname, 1, price, price)
                '+++++++++++++
            End While
            txtSearch.Clear()
            'compute totals
            datagridcellvaluechange(dgvInvoice, txtTotal)
            closeconn()
        End If
    End Sub

    Private Sub dgvInvoice_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles dgvInvoice.CellValueChanged
        'call value changed method to compute total price as quantity /price changes
        datagridcellvaluechange(dgvInvoice, txtTotal)
    End Sub

    Private Sub datagridcellvaluechange(ByVal DataGridView1 As DataGridView, _
                                        ByVal txttotal As TextBox)
        'solves calculations on dgvproducts and computes the total price of products on order

        Try
            Dim unitprice As Double, qty As Integer, totalprice As Double
            Dim Invoicecost As Double = 0, i As Integer
            unitprice = Double.Parse(DataGridView1.CurrentRow.Cells(3).Value.ToString)
            qty = Integer.Parse(DataGridView1.CurrentRow.Cells(2).Value.ToString)
            totalprice = qty * unitprice
            DataGridView1.CurrentRow.Cells(4).Value = totalprice

            'compute total
            For i = 0 To DataGridView1.RowCount - 1
                Invoicecost += Double.Parse(DataGridView1.Rows(i).Cells(4).Value.ToString)
            Next i
            txttotal.Text = Invoicecost.ToString
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnProcess_Click(sender As Object, e As EventArgs) Handles btnProcess.Click
        'item declaration on order master
        Dim BranchId As Integer, _
             InvoiceDate As Date, _
             InvoiceTotals As Double
        'looping variable
        Dim i As Integer
        'data validation
        If (cboBranches.SelectedIndex = -1) Or (cboBranches.Text = "-Select Supplier-") Then
            MessageBox.Show("Select branch", "Strawberry Solution", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            cboBranches.Focus()
            Exit Sub
        Else
            BranchId = Integer.Parse(cboBranches.SelectedValue)
        End If

        InvoiceDate = dtpDateOrdered.Value.ToLongDateString

        If ((txtTotal.Text = "0") Or (txtTotal.Text.Trim = String.Empty)) Then
            MessageBox.Show("Invoice Total Amount Missing", "Strawberry Solution", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        Else
            InvoiceTotals = Double.Parse(txtTotal.Text)
        End If

        'Save in BranchInvoice Master
        inv.insertInvoiceMaster(InvoiceNo, BranchId, InvoiceTotals, InvoiceDate, userid, 0, "", 0, 0, 0, 1)

        'Save in orderDetails

        For i = 0 To dgvInvoice.RowCount - 1
            'item declaration on order details
            Dim MasterNo As Integer, Barcode As String, _
                Qty As Integer, UnitPrice As Double, _
                Amount As Double

            MasterNo = inv.getMasterNo()
            Barcode = dgvInvoice.Rows(i).Cells(0).Value.ToString
            Qty = Integer.Parse(dgvInvoice.Rows(i).Cells(2).Value.ToString)
            UnitPrice = Double.Parse(dgvInvoice.Rows(i).Cells(3).Value.ToString)
            Amount = Double.Parse(dgvInvoice.Rows(i).Cells(4).Value.ToString)

            ''Save to BranchInvoice Details table
            inv.insertInvoiceMaster(InvoiceNo, 0, 0, Today(), userid, MasterNo, Barcode, Qty, UnitPrice, Amount, 2)

            'alter stock
            sale.comitSales(0, 0, 0, "", "", 0, "", 0, Barcode, 0, 0, Qty, 0, "", "", "", 0, 0, 2)

            'INSERT INTO STOCK MOVEMENT
            stockMovement(Barcode, "BranchInvoice", "-", Qty, UnitPrice, 0, Now(), userid, 1)

        Next
        MessageBox.Show("Transaction complete", "Strawberry System", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Rub()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Rub()
    End Sub

    'clear controls
    Sub Rub()
        txtSearch.Clear()
        cboBranches.Text = "-Select Branch-"
        dtpDateOrdered.Value = Today
        dgvInvoice.Rows.Clear()
        txtTotal.Text = 0
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub RemoveRecordToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RemoveRecordToolStripMenuItem.Click
        'remove from grid
        Dim i As Integer = dgvInvoice.CurrentRow.Index
        dgvInvoice.Rows.Remove(dgvInvoice.Rows(i))
        dgvInvoice.Refresh()
        'computeTotal
        datagridcellvaluechange(dgvInvoice, txtTotal)
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim Branchname As String, Id As Integer = 0

        'validate textbox
        If txtBranchName.Text = String.Empty Then
            MessageBox.Show("Invalid branch name", "Strawberry system", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            txtBranchName.Focus()
            Exit Sub
        Else
            Branchname = txtBranchName.Text.Trim.ToUpper
        End If

        'save data
        If btnSave.Text = "Save" Then
            'save to db
            inv.ManageBranches(Branchname, 0, 1)
            MessageBox.Show("Success", "Strawberry System", MessageBoxButtons.OK, MessageBoxIcon.Information)
            'reload data
            inv.LoadBranches(dgvBranches)
            'clear controls
            RubBranches()

        Else
            'update db
            inv.ManageBranches(Branchname, Id, 2)
            MessageBox.Show("Success", "Strawberry System", MessageBoxButtons.OK, MessageBoxIcon.Information)

            'reload data
            inv.LoadBranches(dgvBranches)
            'clear controls
            RubBranches()

        End If
    End Sub

    Private Sub dgvBranches_DoubleClick(sender As Object, e As EventArgs) Handles dgvBranches.DoubleClick
        'double click event
        txtBranchName.Text = dgvBranches.CurrentRow.Cells(1).Value.ToString
        btnSave.Text = "Update"
    End Sub

    'clear inputs
    Sub RubBranches()
        txtBranchName.Clear()
        btnSave.Text = "Save"
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Dim Id As Integer = Integer.Parse(dgvBranches.CurrentRow.Cells(0).Value.ToString)
        Dim Branch As String = dgvBranches.CurrentRow.Cells(1).Value.ToString

        'If dgvBanks.SelectedRows.Count > 1 Then
        If (MsgBox("Are you sure you want to delete " & Branch & " completely from database?", MsgBoxStyle.YesNo, "Strawberry System") = MsgBoxResult.Yes) Then
            inv.ManageBranches(Branch, Id, 3)

            MessageBox.Show("Successfully Deleted", "Strawberry System", MessageBoxButtons.OK, MessageBoxIcon.Information)
            inv.LoadBranches(dgvBranches)

        End If
    End Sub
End Class