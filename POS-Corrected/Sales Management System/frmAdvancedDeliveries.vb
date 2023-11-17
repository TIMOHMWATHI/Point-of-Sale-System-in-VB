Imports MySql.Data
Imports MySql.Data.MySqlClient

Public Class frmAdvancedDeliveries
    Dim Received As New ClassProductsDelivery
    Dim inventorystock As New inventory
    Dim pay As New ClassSupplierPayment
    Private CheckColIndex As Integer

    Dim supplierno As Integer, barcode As String, itemname As String, _
             qty As Double, totalcost As Double, UnitPrice As Double, _
             MasterNo As Integer, totalitemsdelivered As Double, _
             receiptnumber As String, itemsperpack As Integer, _
             SingleItemBP As Double, InvoiceNo As String, OtherNotes As String = "",
             Paymode As String, paystatus As Integer, batchNo As String, _
             ExpiryDate As Date, Manafucturedby As String

    Dim ReceiptTotal As Double

    Private Sub frmAdvancedDeliveries_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'supplier combo
        Dim strsql As String = "SELECT supplier_id,fullname FROM suppliers where relationtype='S'"
        loadcombo(cboSuppliers, strsql, "fullname", "supplier_id")
        'load items to grid
        LoadItems(dgvItems)
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        Dim itemname As String
        If txtSearch.Text.Trim.Length > 0 Then
            itemname = txtSearch.Text.Trim
            dgvItems.Rows.Clear()
            LoadItemsAutosearch(itemname)
        Else
            LoadItems(dgvItems)
        End If
    End Sub

    Private Sub txtInvoiceNo_LostFocus(sender As Object, e As EventArgs) Handles txtInvoiceNo.LostFocus
        Dim InvoiceNo As String, SupplierNo As Integer, strsql As String

        '#####
        If cboSuppliers.SelectedIndex = -1 Then
            MessageBox.Show("Select supplier's name", "Strawberry System", MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtInvoiceNo.Clear()
            cboSuppliers.Focus()
            Exit Sub
        End If
        '#####

        If txtInvoiceNo.Text.Trim.Length > 0 Then
            InvoiceNo = txtInvoiceNo.Text.Trim
            SupplierNo = Integer.Parse(cboSuppliers.SelectedValue)
            'MsgBox(SupplierNo)

            strsql = "SELECT COUNT(invoicenumber) FROM deliverymaster WHERE" _
                     & " invoicenumber= '" & InvoiceNo & "' AND " _
                     & " deliveredby=" & SupplierNo & ""

            Dim datareader As MySqlDataReader
            Dim cmd As New MySqlCommand(strsql, Conn)
            cmd.CommandType = CommandType.Text
            datareader = cmd.ExecuteReader
            While datareader.Read
                If datareader(0) > 0 Then
                    MessageBox.Show("Invoice number already exists", "Strawberry System", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    txtInvoiceNo.Clear()
                    txtInvoiceNo.Focus()
                    Exit Sub
                End If
            End While
            datareader.Dispose()
            closeconn()
        End If
    End Sub

    Private Sub txtReceiptNo_LostFocus(sender As Object, e As EventArgs) Handles txtReceiptNo.LostFocus
        Dim ReceiptNo As String, SupplierNo As Integer, strsql As String

        '#####
        If cboSuppliers.SelectedIndex = -1 Then
            MessageBox.Show("Select supplier's name", "Strawberry System", MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtInvoiceNo.Clear()
            cboSuppliers.Focus()
            Exit Sub
        End If
        '#####

        If txtReceiptNo.Text.Trim.Length > 0 Then
            ReceiptNo = txtReceiptNo.Text.Trim
            SupplierNo = Integer.Parse(cboSuppliers.SelectedValue)
            'MsgBox(SupplierNo)

            strsql = "SELECT COUNT(receiptnumber) FROM deliverymaster WHERE" _
                     & " receiptnumber= '" & ReceiptNo & "' AND " _
                     & " deliveredby=" & SupplierNo & ""

            Dim datareader As MySqlDataReader
            Dim cmd As New MySqlCommand(strsql, Conn)
            cmd.CommandType = CommandType.Text
            datareader = cmd.ExecuteReader
            While datareader.Read
                If datareader(0) > 0 Then
                    MessageBox.Show("Receipt number already exists", "Strawberry System", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    txtReceiptNo.Clear()
                    txtReceiptNo.Focus()
                    Exit Sub
                End If
            End While
            datareader.Dispose()
            closeconn()
        End If
    End Sub

    'Loads database table to Datagridview
    Sub LoadItems(ByVal dgv As DataGridView)
        Dim strsql As String = "SELECT p.Barcode,p.Description,p.Itemsperpack," _
                               & " s.quantityinstock,s.buyingprice,pl.retailprice," _
                               & " pl.wholesaleprice FROM products p" _
                               & " INNER JOIN pricelist pl ON pl.productid=p.Barcode" _
                               & " INNER JOIN stock s ON s.itemcode=p.Barcode WHERE" _
                               & " p.deleted=0 AND pl.active=1 GROUP BY p.Barcode" _
                               & " ORDER BY p.Description ASC"
        Try

            Dim dr As MySqlDataReader
            Dim cmd As New MySqlCommand(strsql, Conn)
            dr = cmd.ExecuteReader()
            'clear controls
            txtSearch.Clear()
            dgvItems.Rows.Clear()
            'loop thru
            Do While dr.Read = True
                'DataGridView1.Rows.Add(dr(0), dr(1))
                Dim n As Integer = dgvItems.Rows.Add()
                dgvItems.Rows(n).Cells("ColItemCode").Value = dr(0).ToString
                dgvItems.Rows(n).Cells("ColDesc").Value = dr(1).ToString
                dgvItems.Rows(n).Cells("ColInPack").Value = dr(2).ToString
                dgvItems.Rows(n).Cells("ColInStock").Value = dr(3).ToString
                dgvItems.Rows(n).Cells("ColBuyingPrice").Value = dr(4).ToString
                dgvItems.Rows(n).Cells("ColRetail").Value = dr(5).ToString
                dgvItems.Rows(n).Cells("ColWholesale").Value = dr(6).ToString
            Loop
            dr.Dispose()
            closeconn()
            Exit Sub
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    'Loads database table to Datagridview
    Sub LoadItemsAutosearch(ByVal itemname As String)

        Dim strsql As String = "SELECT p.Barcode,p.Description,p.Itemsperpack," _
                               & " s.quantityinstock,s.buyingprice,pl.retailprice," _
                               & " pl.wholesaleprice FROM products p" _
                               & " INNER JOIN pricelist pl ON pl.productid=p.Barcode" _
                               & " INNER JOIN stock s ON s.itemcode=p.Barcode WHERE" _
                               & " p.deleted=0 AND pl.active=1 AND " _
                               & " (p.description LIKE '" & "%" & itemname & "%" & "')" _
                               & " OR (p.barcode LIKE '" & "%" & itemname & "%" & "') "

        Try
            Dim dr As MySqlDataReader
            Dim cmd As New MySqlCommand(strsql, Conn)
            dr = cmd.ExecuteReader()
            'clear controls
            'txtSearch.Clear()
            dgvItems.Rows.Clear()
            'loop thru
            Do While dr.Read = True
                'DataGridView1.Rows.Add(dr(0), dr(1))
                Dim n As Integer = dgvItems.Rows.Add()
                dgvItems.Rows(n).Cells("ColItemCode").Value = dr(0).ToString
                dgvItems.Rows(n).Cells("ColDesc").Value = dr(1).ToString
                dgvItems.Rows(n).Cells("ColInPack").Value = dr(2).ToString
                dgvItems.Rows(n).Cells("ColInStock").Value = dr(3).ToString
                dgvItems.Rows(n).Cells("ColBuyingPrice").Value = dr(4).ToString
                dgvItems.Rows(n).Cells("ColRetail").Value = dr(5).ToString
                dgvItems.Rows(n).Cells("ColWholesale").Value = dr(6).ToString
            Loop
            dr.Dispose()
            closeconn()
            Exit Sub
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub dgvItems_DoubleClick(sender As Object, e As EventArgs) Handles dgvItems.DoubleClick
    
        Dim Barcode As String, Description As String, itemsPerPack As Integer = 0, _
                InStock As Integer = 0, BuyingPrice As Double = 0, _
                Retail As Double = 0, Wholesale As Double = 0

            Barcode = dgvItems.CurrentRow.Cells(0).Value.ToString
            Description = dgvItems.CurrentRow.Cells(1).Value.ToString
            itemsPerPack = Integer.Parse(dgvItems.CurrentRow.Cells(2).Value.ToString)
            InStock = Integer.Parse(dgvItems.CurrentRow.Cells(3).Value.ToString)
            BuyingPrice = Double.Parse(dgvItems.CurrentRow.Cells(4).Value.ToString)
            Retail = Double.Parse(dgvItems.CurrentRow.Cells(5).Value.ToString)
            Wholesale = Double.Parse(dgvItems.CurrentRow.Cells(6).Value.ToString)


        If rdbPiece.Checked = True Then
            'add variables to grid
            dgvReceiveItems.Rows.Add(Barcode, Description, 1, InStock, BuyingPrice, Retail, Wholesale, 0, 0, "", FormatDateTime(Today(), DateFormat.ShortDate), "", 0, 0, 0)
            'clear rdb
            rdbPiece.Checked = False
        ElseIf rdbWhole.Checked = True Then
            'add variables to grid
            dgvReceiveItems.Rows.Add(Barcode, Description, itemsPerPack, InStock, BuyingPrice, Retail, Wholesale, 0, 0, "", FormatDateTime(Today(), DateFormat.ShortDate), "", 0, 0, 0)
            'clear rdb
            rdbWhole.Checked = False
        Else
            MessageBox.Show("Select purchase mode to continue", "Strawberry POS", MessageBoxButtons.OK)
            Exit Sub
        End If
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        rub()
    End Sub

    'clear controls
    Sub rub()
        cboSuppliers.SelectedIndex = -1
        cboPayMode.SelectedIndex = -1
        txtInvoiceNo.Clear()
        txtReceiptNo.Clear()
        txtOtherNotes.Clear()
        dgvReceiveItems.Rows.Clear()
        lblPriceTotals.Text = 0
        btnRefresh.PerformClick()
    End Sub

    Private Sub RemoveItemToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RemoveItemToolStripMenuItem.Click
        Dim i As Integer = dgvReceiveItems.CurrentRow.Index
        dgvReceiveItems.Rows.Remove(dgvReceiveItems.Rows(i))
        dgvReceiveItems.Refresh()
        'computeTotal
        DGVcellvaluechange(dgvReceiveItems, lblPriceTotals)
    End Sub

    Private Sub DGVcellvaluechange(ByVal DataGridView1 As DataGridView, _
                                   ByVal lbl As Label)
        'solves calculations on dgvproducts and computes the total price of products on order

        Try
            Dim unitprice As Double, qty As Integer, _
                totalprice As Double, totalitemsdelivered As Double, _
                singleitemBP As Double, itemsperpack As Integer

            Dim PriceTotals As Double = 0, i As Integer
            itemsperpack = Integer.Parse(DataGridView1.CurrentRow.Cells(2).Value.ToString)
            qty = Integer.Parse(DataGridView1.CurrentRow.Cells(7).Value.ToString)
            unitprice = Double.Parse(DataGridView1.CurrentRow.Cells(8).Value.ToString)
            totalprice = qty * unitprice
            totalitemsdelivered = itemsperpack * qty
            singleitemBP = FormatNumber((totalprice / totalitemsdelivered), 0)

            DataGridView1.CurrentRow.Cells(12).Value = totalprice
            DataGridView1.CurrentRow.Cells(13).Value = totalitemsdelivered
            DataGridView1.CurrentRow.Cells(14).Value = singleitemBP
            'compute total
            For i = 0 To DataGridView1.RowCount - 1
                PriceTotals += Double.Parse(DataGridView1.Rows(i).Cells(12).Value.ToString)
            Next i
            lblPriceTotals.Text = PriceTotals.ToString
        Catch ex As Exception

        End Try
    End Sub

    Private Sub dgvReceiveItems_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgvReceiveItems.CellMouseClick
        Try
            dtpExpiryDate.Value = DateTime.Parse(dgvReceiveItems.CurrentCell.Value.ToString)
        Catch
        End Try
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        If dtpExpiryDate.Value < Today() Then
            MessageBox.Show("Invalid expiry date" & vbNewLine & "Expiry date cannot be less than current date", "Strawberry POS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        Else
            dgvReceiveItems.CurrentRow.Cells(10).Value = FormatDateTime(dtpExpiryDate.Value.ToString(), DateFormat.ShortDate)
        End If
    End Sub

    Private Sub dgvReveiveItems_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles dgvReceiveItems.CellValueChanged
        'compute cell values
        DGVcellvaluechange(dgvReceiveItems, lblPriceTotals)
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        'reload data
        txtSearch.Clear()
        LoadItems(dgvItems)
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        'supplier validation
        If cboSuppliers.SelectedIndex = -1 Then
            MessageBox.Show("Select supplier's name", "Strawberry System", MessageBoxButtons.OK, MessageBoxIcon.Information)
            cboSuppliers.Focus()
            Exit Sub
        Else
            supplierno = cboSuppliers.SelectedValue
        End If

        'Paymode validation
        If cboPayMode.SelectedIndex = -1 Then
            MessageBox.Show("Select paymode", "Strawberry System", MessageBoxButtons.OK, MessageBoxIcon.Information)
            cboPayMode.Focus()
            Exit Sub
        Else
            Paymode = cboPayMode.Text
        End If

        'receipt validation
        If cboPayMode.SelectedIndex = 0 Then
            If txtReceiptNo.Text.Trim = String.Empty OrElse txtReceiptNo.Text = "0" Then
                MessageBox.Show("Receipt number missing", "Strawberry System", MessageBoxButtons.OK)
                txtReceiptNo.Focus()
                Exit Sub
                'receiptnumber = "0"
            Else
                receiptnumber = txtReceiptNo.Text.Trim
            End If
        Else
            'invoice validation
            If txtInvoiceNo.Text = String.Empty OrElse txtInvoiceNo.Text = "0" Then
                MessageBox.Show("Invoice number missing", "Strawberry System", MessageBoxButtons.OK)
                txtInvoiceNo.Focus()
                Exit Sub
                'InvoiceNo = "0"
            Else
                InvoiceNo = txtInvoiceNo.Text.Trim
            End If
        End If

        'other notes
        OtherNotes = txtOtherNotes.Text.ToUpper

        'validate pay status
        If cboPayMode.SelectedIndex = 0 Then
            paystatus = 1
        Else
            paystatus = 0
        End If

        '##################################
        'save to database
        totalcost = Double.Parse(lblPriceTotals.Text)
        Received.insertDeliveryMaster(InvoiceNo, receiptnumber, Now(), supplierno, totalcost, OtherNotes, 0, 0, 1)

        'insert to supplier payments
        MasterNo = Integer.Parse(Received.getMasterNo())
        If cboPayMode.SelectedIndex = 0 Then
            Received.insertSupplierPayment(supplierno, Paymode, (receiptnumber + InvoiceNo), totalcost, totalcost, MasterNo, OtherNotes, paystatus, 1)
            'INSERT INTO CASH LEDGER
            If totalcost > 0 Then
                CashLedger(MasterNo, totalcost, Now(), userid, "Payment of suppliers in cash", "-", "Supplier_Payments", 1)
            End If
        Else
            Received.insertSupplierPayment(supplierno, Paymode, (receiptnumber + InvoiceNo), totalcost, 0, MasterNo, OtherNotes, paystatus, 1)
        End If

        'Insert to payment narration
        If cboPayMode.SelectedIndex = 0 Then
            pay.insertPayNarration(MasterNo, totalcost, Paymode, "Full Payment", receiptnumber, Now(), OtherNotes, userid)
        End If

        'progress_bar variables
        Dim percentage_Processed As Double = 0, totalRecords As Double = 0
        totalRecords = dgvReceiveItems.RowCount - 1
        PBStockDelivery.Maximum = totalRecords

        For i = 0 To dgvReceiveItems.RowCount - 1

            'assign progress_bar variables
            percentage_Processed = (i / totalRecords) * 100
            lblCount.Text = i.ToString
            lblPercentage.Text = FormatNumber(percentage_Processed, 1) & " %"


            Dim MasterNo As Integer = Integer.Parse(Received.getMasterNo())
            'MsgBox(MasterNo)
            barcode = dgvReceiveItems.Rows(i).Cells(0).Value.ToString
            itemname = dgvReceiveItems.Rows(i).Cells(1).Value.ToString
            itemsperpack = dgvReceiveItems.Rows(i).Cells(2).Value.ToString
            qty = dgvReceiveItems.Rows(i).Cells(7).Value.ToString
            UnitPrice = dgvReceiveItems.Rows(i).Cells(8).Value.ToString
            '#########
            ''validate batch no
            'If (dgvReceiveItems.Rows(i).Cells(9).Value) = Nothing Then
            '    MessageBox.Show("Missing batch no", "Strawberry POS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    dgvReceiveItems.CurrentRow.Cells(9).Style.BackColor = Color.Red
            '    Exit Sub
            'Else
            batchNo = dgvReceiveItems.Rows(i).Cells(9).Value.ToString.ToUpper
            '    'batchNo = Long.Parse(Now().Date.Day & Now().Date.Month & Now().Date.Year & Now().TimeOfDay.Hours & Now().TimeOfDay.Minutes)
            'End If

            '#########
            ''validate expiry date
            'If (dgvReceiveItems.Rows(i).Cells(10).Value) <= Date.Today Then
            '    MessageBox.Show("Invalid expiry date" & vbNewLine & "Expiry date must be a future date", "Strawberry POS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    dgvReceiveItems.CurrentRow.Cells(10).Style.BackColor = Color.Red
            '    Exit Sub
            'Else
            ExpiryDate = dgvReceiveItems.Rows(i).Cells(10).Value.ToString
            'End If
            'ExpiryDate = "2024-09-30"
            '#########   

            Manafucturedby = dgvReceiveItems.Rows(i).Cells(11).Value.ToString
            totalcost = dgvReceiveItems.Rows(i).Cells(12).Value.ToString
            totalitemsdelivered = dgvReceiveItems.Rows(i).Cells(13).Value.ToString

            'validate single item BP
            If (qty >= 1) And (SingleItemBP < 1) Then
                SingleItemBP = 1
            Else
                SingleItemBP = dgvReceiveItems.Rows(i).Cells(14).Value.ToString
            End If

            ReceiptTotal += dgvReceiveItems.Rows(i).Cells(12).Value.ToString

            'save in details table
            Received.insertDeliverydetails(barcode, receiptnumber + InvoiceNo, qty, UnitPrice, totalcost, _
                                           SingleItemBP, itemsperpack, totalitemsdelivered, _
                                           0, 0, batchNo, ExpiryDate, Manafucturedby, Today(), MasterNo, 1)

            'INSERT INTO STOCK LEDGER
            StockLedger(MasterNo, barcode, qty, UnitPrice, Now(), userid, "Supplier Purchases", "+", "Purchases", 1)

            'change stock
            inventorystock.alterStock(barcode, totalitemsdelivered, 1)

            'INSERT INTO STOCK MOVEMENT
            stockMovement(barcode, "purchases", "+", totalitemsdelivered, 0, SingleItemBP, Now(), userid, 1)

            PBStockDelivery.Value = i
        Next i

        MsgBox("Successfully saved", MsgBoxStyle.OkOnly, "Strawberry POS")

        'CLEAR PROGRESS BAR
        PBStockDelivery.Value = 0
        '##########################
        rub()
        'clear PB Controls
        lblCount.Text = 0
        lblPercentage.Text = 0
        closeconn()
    End Sub

    Private Sub cboPayMode_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboPayMode.SelectedIndexChanged
        If cboPayMode.SelectedIndex = -1 Then
            txtReceiptNo.Enabled = False
            'lblReceiptNo.Enabled = False
            txtInvoiceNo.Enabled = False
            'lblInvoiceNo.Enabled = False
        ElseIf cboPayMode.SelectedIndex = 0 Then
            txtReceiptNo.Enabled = True
            'lblReceiptNo.Enabled = True
            txtInvoiceNo.Enabled = False
            'lblInvoiceNo.Enabled = False
            txtInvoiceNo.Text = "0"
        Else
            txtInvoiceNo.Enabled = True
            'lblInvoiceNo.Enabled = True
            txtReceiptNo.Enabled = False
            'lblReceiptNo.Enabled = False
            txtReceiptNo.Text = "0"
        End If
    End Sub


    Private Sub dgvReveiveItems_CurrentCellDirtyStateChanged(sender As Object, e As EventArgs) Handles dgvReceiveItems.CurrentCellDirtyStateChanged
        If dgvReceiveItems.IsCurrentCellDirty Then
            dgvReceiveItems.CommitEdit(DataGridViewDataErrorContexts.Commit)
        End If
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub dgvItems_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvItems.CellContentClick

    End Sub
End Class