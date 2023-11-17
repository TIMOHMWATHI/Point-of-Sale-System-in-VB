Imports MySql.Data
Imports MySql.Data.MySqlClient
Public Class frmSimpleDeliveries
    'variables
    Dim Received As New ClassProductsDelivery
    Dim inventorystock As New inventory
    Dim supplierno As Integer, barcode As String, itemname As String, _
              qty As Double, totalcost As Double, itemBP As Double, _
              orderno As Integer, totalitemsdelivered As Double, _
              batchno As String, expirydate As Date, InvoiceNo As String, _
              manufacturedby As String, manufacturedate As Date, _
              MasterNo As Integer

    Dim strsql As String, FORMLOAD As Boolean = True

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        'If (MsgBox("Are you sure you want to close this page?", MsgBoxStyle.YesNo, "Simple deliveries") = MsgBoxResult.Yes) Then
        Me.Close()
        'End If
    End Sub


    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        'supplier input validation
        Dim receiptnumber As String
        If cboSuppliers.SelectedIndex = -1 Then
            MessageBox.Show("Select supplier's name", "Strawberry System", MessageBoxButtons.OK, MessageBoxIcon.Information)
            cboSuppliers.Focus()
            Exit Sub
        Else
            supplierno = cboSuppliers.SelectedValue
        End If


        'itemname input validation
        If txtItemSearch.Text.Trim = String.Empty Then
            MessageBox.Show("Product input missing", "Strawberry System", MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtItemSearch.Focus()
            Exit Sub
        Else
            itemname = txtItemSearch.Text.Trim
        End If

        'barcode input validation
        If txtBarcode.Text.Trim = String.Empty Then
            MessageBox.Show("Barcode input is missing", "Strawberry System", MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtBarcode.Focus()
            Exit Sub
        Else
            barcode = txtBarcode.Text.Trim
        End If

        'qty input validation
        If txtQtydelivered.Text.Trim = String.Empty Then
            MessageBox.Show("Item quantity missing", "Strawberry System", MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtQtydelivered.Focus()
            Exit Sub
        Else
            qty = Double.Parse(txtQtydelivered.Text.Trim)
        End If

        'total cost input validation
        If txtTotalCost.Text.Trim = String.Empty Then
            MessageBox.Show("Total cost value missing", "Strawberry System", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        Else
            totalcost = Double.Parse(txtTotalCost.Text.Trim)
        End If

        'item buying price input validation
        If txtBuyingPrice.Text.Trim = String.Empty Then
            MessageBox.Show("Item buying price missing", "Strawberry System", MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtBuyingPrice.Focus()
            Exit Sub
        Else
            itemBP = txtBuyingPrice.Text.Trim
        End If

        '################
        'If txtBatchNo.Text = String.Empty Then
        '    MessageBox.Show("Batch number input missing", "Strawberry POS", MessageBoxButtons.OK, MessageBoxIcon.Information)
        '    txtBatchNo.Focus()
        '    Exit Sub
        'Else
        batchno = txtBatchNo.Text.Trim.ToUpper
        'End If

        'If dtpExpiryDate.Value = Today() Then
        '    MessageBox.Show("Invalid expiry date input", "Strawberry POS", MessageBoxButtons.OK, MessageBoxIcon.Information)
        '    dtpExpiryDate.Focus()
        '    Exit Sub
        'Else
        expirydate = dtpExpiryDate.Value.ToShortDateString
        'End If

        manufacturedby = txtManufacturedBy.Text
        manufacturedate = dtpManufactureDate.Value.ToShortDateString
        MasterNo = Integer.Parse(get_EventID())

        '=================
        'INPUT DATA INTO DATABASE
        receiptnumber = txtReceiptno.Text.Trim
        Received.insertDeliveryMaster(InvoiceNo, receiptnumber, Now(), supplierno, totalcost, "", 0, MasterNo, 1)

        Received.insertDeliverydetails(barcode, receiptnumber, qty, itemBP, totalcost, _
                                       itemBP, qty, qty, 0, 0, batchno, expirydate, _
                                       manufacturedby, manufacturedate, 0, 1)

        inventorystock.alterStock(barcode, qty, 1)

        'INSERT INTO STOCK MOVEMENT
        stockMovement(barcode, "purchases", "+", qty, 0, itemBP, Now(), userid, 1)

        MsgBox("Successfully saved", MsgBoxStyle.OkOnly, "Strawberry Solution")
        closeconn()
        Rub()
    End Sub

    '============
    'compute item buying price
    Sub computeItemBP()
        'qty input validation
        If txtQtydelivered.Text.Trim = String.Empty Then
            Exit Sub
        Else
            qty = txtQtydelivered.Text.Trim
        End If
        'BP input validation
        If txtBuyingPrice.Text.Trim = String.Empty Then
            Exit Sub
        Else
            itemBP = txtBuyingPrice.Text.Trim
        End If
        'compute
        totalcost = Double.Parse(itemBP * qty)
        txtTotalCost.Text = totalcost.ToString
    End Sub

    Private Sub frmSimpleDeliveries_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ''hide controls sub
        hidecontrols()
        'supplier combo
        Dim strsql As String = "SELECT supplier_id,fullname FROM suppliers where relationtype='S'"
        loadcombo(cboSuppliers, strsql, "fullname", "supplier_id")

        FORMLOAD = False
        'load data to dgv
        LoadSimpleDeliveries()
        'auto search products
        Loadproducts(txtItemSearch)
        Loadproducts(txtFilter)
    End Sub

    'Loads AND RELOAD database table FROM Datagridview
    Sub LoadSimpleDeliveries()
        Dim strsql As String = "SELECT S.fullname AS 'Supplied by', DM.receiptnumber, P.Description," _
                               & " P.Barcode,ST.buyingprice, DD.quantitydelivered, DM.totalamnt," _
                               & " DD.batchno,DD.expirydate,DD.manufacturedby," _
                               & " CASE WHEN DD.manufacturedate='1990-01-01' THEN 'N/A' ELSE" _
                               & " DD.manufacturedate END AS 'Manufacture Date'," _
                               & " DATE_FORMAT(DM.datereceived, '%d %M %Y') FROM" _
                               & " deliverymaster DM INNER JOIN deliverydetails DD ON " _
                               & " DD.deliveryno = DM.deliveryid" _
                               & " INNER JOIN products P ON P.Barcode=DD.barcode" _
                               & " INNER JOIN suppliers S ON S.supplier_id=DM.deliveredby" _
                               & " INNER JOIN stock ST ON ST.itemcode=P.Barcode" _
                               & " ORDER BY P.Description,DM.datereceived ASC "
        ' Try
        Dim cmd As New MySqlCommand(strsql, Conn)
        cmd.CommandType = CommandType.Text
        Dim adapter As New MySqlDataAdapter(cmd)
        Dim table As New DataTable
        adapter.Fill(table)
        'Display the data.
        dgvSimpleDeliveries.DataSource = table
        closeconn()
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message)
        'End Try
        With dgvSimpleDeliveries
            .Columns(0).HeaderText = "Fullnames"
            .Columns(1).HeaderText = "Receipt No:"
            .Columns(2).HeaderText = "Description"
            .Columns(3).HeaderText = "Barcode"
            .Columns(4).HeaderText = "Buying Price"
            .Columns(5).HeaderText = "Item Qty"
            .Columns(6).HeaderText = "Total Amount"
            .Columns(7).HeaderText = "Batch No"
            .Columns(8).HeaderText = "Expiry Date"
            .Columns(9).HeaderText = "Manufactured By"
            .Columns(10).HeaderText = "Manufacture Date"
            .Columns(11).HeaderText = "Date Received"
            .Columns(7).Visible = False
            .Columns(8).Visible = False
            .Columns(9).Visible = False
            .Columns(10).Visible = False
            .Columns(11).Visible = False
            .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells
            .Columns(0).DefaultCellStyle.WrapMode = DataGridViewTriState.True
            .Columns(2).DefaultCellStyle.WrapMode = DataGridViewTriState.True
            '.Columns(4).DefaultCellStyle.WrapMode = DataGridViewTriState.True
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        End With

    End Sub

    'clear data
    Sub Rub()
        cboSuppliers.SelectedIndex = -1
        txtReceiptno.Clear()
        txtItemSearch.Clear()
        txtBarcode.Clear()
        txtQtydelivered.Clear()
        txtBuyingPrice.Clear()
        txtTotalCost.Clear()
        txtFilter.Clear()
        chkPharmaceuticalProduct.Checked = False
        txtBatchNo.Clear()
        dtpExpiryDate.Value = Today
        txtManufacturedBy.Clear()
        dtpManufactureDate.Value = Today
    End Sub

    Private Sub txtTotalCost_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTotalCost.KeyPress
        'only numbers input validation
        If e.KeyChar <> ChrW(Keys.Back) Then
            If Char.IsNumber(e.KeyChar) Then
            Else
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub txtTotalCost_TextChanged(sender As Object, e As EventArgs) Handles txtTotalCost.TextChanged
        computeItemBP()
    End Sub

    Private Sub txtQtydelivered_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtQtydelivered.KeyPress
        'only numbers input validation
        If e.KeyChar <> ChrW(Keys.Back) Then
            If Char.IsNumber(e.KeyChar) Then
            Else
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub txtQtydelivered_TextChanged(sender As Object, e As EventArgs) Handles txtQtydelivered.TextChanged
        computeItemBP()
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        LoadSimpleDeliveries()
        txtFilter.Clear()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Rub()
    End Sub

    Private Sub txtBuyingPrice_TextChanged(sender As Object, e As EventArgs) Handles txtBuyingPrice.TextChanged
        computeItemBP()
    End Sub

    Private Sub chkPharmaceuticalProduct_CheckedChanged(sender As Object, e As EventArgs) Handles chkPharmaceuticalProduct.CheckedChanged
        If chkPharmaceuticalProduct.Checked = True Then
            lblBatchNo.Visible = True
            lblExpiry.Visible = True
            txtBatchNo.Visible = True
            dtpExpiryDate.Visible = True
            lblManafacturedBy.Visible = True
            txtManufacturedBy.Visible = True
            lblManufactureDate.Visible = True
            dtpManufactureDate.Visible = True
        Else
            lblBatchNo.Visible = False
            lblExpiry.Visible = False
            txtBatchNo.Visible = False
            dtpExpiryDate.Visible = False
            lblManafacturedBy.Visible = False
            txtManufacturedBy.Visible = False
            lblManufactureDate.Visible = False
            dtpManufactureDate.Visible = False
        End If
    End Sub

    'hide controls
    Sub hidecontrols()
        lblBatchNo.Visible = False
        lblExpiry.Visible = False
        txtBatchNo.Visible = False
        dtpExpiryDate.Visible = False
        lblManafacturedBy.Visible = False
        txtManufacturedBy.Visible = False
        lblManufactureDate.Visible = False
        dtpManufactureDate.Visible = False
    End Sub

    Private Sub txtItemSearch_KeyDown(sender As Object, e As KeyEventArgs) Handles txtItemSearch.KeyDown
        Dim itemname As String

        'input validation on txtItemSearch
        If (txtItemSearch.Text = String.Empty) Then
            txtItemSearch.Focus()
            Exit Sub
        Else
            itemname = txtItemSearch.Text.Trim
        End If
        '#################################################################################################
        Dim datareader As MySqlDataReader
        If e.KeyCode = Keys.Enter Then    'work if enter button is clicked
            Try
                Dim strsql = "SELECT Description,Barcode FROM products WHERE deleted=0 AND (Description='" & itemname & "')"

                Dim cmd As New MySqlCommand(strsql, Conn)
                cmd.CommandType = CommandType.Text
                datareader = cmd.ExecuteReader
                While datareader.Read
                    txtItemSearch.Text = datareader(0).ToString
                    txtBarcode.Text = datareader(1).ToString
                End While
                closeconn()
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If
    End Sub

    Private Sub txtFilter_KeyDown(sender As Object, e As KeyEventArgs) Handles txtFilter.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim itemname As String
            If txtFilter.Text.Trim.Length > 0 Then
                itemname = txtFilter.Text.Trim
                LoadSimpleDeliveriesAutosearch(itemname)
            Else
                LoadSimpleDeliveries()
            End If
        End If
    End Sub
    Sub LoadSimpleDeliveriesAutosearch(ByVal itemname As String)
        Dim strsql As String = "SELECT S.fullname, DM.receiptnumber, P.Description," _
                               & " P.Barcode,ST.buyingprice, DD.quantitydelivered, DM.totalamnt," _
                               & " DD.batchno,DD.expirydate FROM " _
                               & " deliverymaster DM INNER JOIN deliverydetails DD ON " _
                               & " DD.deliveryno = DM.deliveryid" _
                               & " INNER JOIN products P ON P.Barcode=DD.barcode" _
                               & " INNER JOIN suppliers S ON S.supplier_id=DM.deliveredby" _
                               & " INNER JOIN stock ST ON ST.itemcode=P.Barcode " _
                               & " WHERE P.Barcode='" & itemname & "'" _
                               & " OR P.Description='" & itemname & "'"

        ' Try
        Dim cmd As New MySqlCommand(strsql, Conn)
        cmd.CommandType = CommandType.Text
        Dim adapter As New MySqlDataAdapter(cmd)
        Dim table As New DataTable
        adapter.Fill(table)
        'Display the data.
        dgvSimpleDeliveries.DataSource = table
        closeconn()
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message)
        'End Try
        With dgvSimpleDeliveries
            .Columns(0).HeaderText = "Fullnames"
            .Columns(1).HeaderText = "Receipt No:"
            .Columns(2).HeaderText = "Description"
            .Columns(3).HeaderText = "Barcode"
            .Columns(4).HeaderText = "Buying Price"
            .Columns(5).HeaderText = "Item Qty"
            .Columns(6).HeaderText = "Total Amount"
            .Columns(7).HeaderText = "Batch No"
            .Columns(8).HeaderText = "Expiry Date"
            .Columns(9).HeaderText = "Manufactured By"
            .Columns(10).HeaderText = "Manufacture Date"
            .Columns(11).HeaderText = "Date Received"
            .Columns(7).Visible = False
            .Columns(8).Visible = False
            .Columns(9).Visible = False
            .Columns(10).Visible = False
            .Columns(11).Visible = False
            .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells
            .Columns(0).DefaultCellStyle.WrapMode = DataGridViewTriState.True
            .Columns(2).DefaultCellStyle.WrapMode = DataGridViewTriState.True
            '.Columns(4).DefaultCellStyle.WrapMode = DataGridViewTriState.True
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        End With
    End Sub

    'get maximum receipt number
    Public Function get_EventID() As Integer
        Dim dr As MySqlDataReader
        Dim strsql As String = "SELECT max(deliveryid) FROM deliverymaster"
        Dim eventid As Integer = 0
        Try

            Dim cmd As New MySqlCommand(strsql, Conn)
            cmd.CommandType = CommandType.Text
            dr = cmd.ExecuteReader
            While dr.Read
                eventid = Integer.Parse(dr(0).ToString)
            End While
            closeconn()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return eventid
    End Function

    Private Sub txtFilter_TextChanged(sender As Object, e As EventArgs) Handles txtFilter.TextChanged

    End Sub

    Private Sub txtItemSearch_TextChanged(sender As Object, e As EventArgs) Handles txtItemSearch.TextChanged

    End Sub
End Class