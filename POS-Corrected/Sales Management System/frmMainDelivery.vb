Imports MySql.Data
Imports MySql.Data.MySqlClient
Public Class frmMainDelivery
    'class declarations
    Dim Received As New ClassProductsDelivery
    Dim item As New ClassProducts
    Dim order As New ClassOrder
    Dim inventorystock As New inventory

    'variables declarations from deliverymaster
    Dim deliverynotenumber As String, _
        invoicenumber As String, receiptnumber As String, _
        datereceived As Date, deliveredby As String, _
        totalamnt As Double, vehicleregno As String, _
      othernotes As String, deliveryid As Integer

    'variables declaration from delivery details
    Dim itemname As String, orderunitprice As Double, _
        quantitydelivered As Integer, totalprice As Double, _
        singleitembuyingprice As Double, totalitemsdelivered As Integer, _
        quantityvariance As Integer, barcode As String, _
        ordernumber As Integer, deliverynumber As String, _
        batchno As String, expirydate As Date, _
        manufacturedby As String, manufacturedate As Date

    'other variables
    Dim totaldeliveryprice As Double, deliveryunitprice As Double
    Dim pricevariance As Double, totalorderprice As Double
    Dim qtyvariance As Integer, qtydelivered As Integer, qtyordered As Integer
    Dim itembuyingprice As Double, itemsperpack As Integer
    Dim totalstockitems As Integer, orderno As Integer


    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnsave.Click

        'txtdeliverynotenumber input validation
        If txtreceiptnumber.Text.Trim = String.Empty Then
            MessageBox.Show("Input Delivery Number", "Recommended", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        Else
            deliverynotenumber = txtreceiptnumber.Text.Trim
        End If

        ordernumber = Integer.Parse(dgvdeliveries.Rows(0).Cells(13).Value.ToString)


        ''input validation on InvoiceNumber
        'If txtInvoiceNo.Text.Trim = String.Empty Then
        '    MessageBox.Show("Input the invoice Number", "Recommended", MessageBoxButtons.OK, MessageBoxIcon.Information)
        '    Exit Sub
        'Else
        invoicenumber = txtInvoiceNo.Text
        'End If

        'supplier input validation
        If cboSuppliers.SelectedIndex = -1 Then
            MessageBox.Show("Select supplier's name", "Simple deliveries", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        Else
            deliveredby = cboSuppliers.SelectedValue
        End If

        'input validation on receipt number
        receiptnumber = txtreceiptnumber.Text.Trim

        'input validation on TotalAmnt
        'input validation on TotalAmnt
        If txtTotalAmnt.Text.Trim = String.Empty Then
            MessageBox.Show("Input the Total Stock Amnt", "Total amount", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        Else
            totalamnt = txtTotalAmnt.Text
        End If

        vehicleregno = txtvehicleregno.Text.Trim.ToUpper

        'input validation on othernotes  
        othernotes = txtothernotes.Text.Trim

        'validation on dtpdatereceived
        datereceived = Date.Parse(dtpDateReceived.Value.ToShortDateString)

        'INPUT DATA INTO DATABASE
        If btnsave.Text = "Save" Then

            Received.insertDeliveryMaster(receiptnumber, datereceived, deliveredby, totalamnt, _
                                           vehicleregno, othernotes, ordernumber, 0, 1)

            For counter = 0 To dgvdeliveries.RowCount - 2
                barcode = dgvdeliveries.Rows(counter).Cells(12).Value.ToString
                deliverynumber = txtreceiptnumber.Text.Trim
                quantitydelivered = dgvdeliveries.Rows(counter).Cells(4).Value.ToString
                deliveryunitprice = dgvdeliveries.Rows(counter).Cells(5).Value.ToString
                totalprice = dgvdeliveries.Rows(counter).Cells(6).Value.ToString
                singleitembuyingprice = dgvdeliveries.Rows(counter).Cells(7).Value.ToString
                itemsperpack = dgvdeliveries.Rows(counter).Cells(8).Value.ToString
                totalitemsdelivered = dgvdeliveries.Rows(counter).Cells(9).Value.ToString
                quantityvariance = dgvdeliveries.Rows(counter).Cells(10).Value.ToString
                pricevariance = dgvdeliveries.Rows(counter).Cells(11).Value.ToString
                batchno = txtBatchNo.Text.ToUpper
                expirydate = dtpExpiryDate.Value.ToShortDateString
                orderno = Integer.Parse(dgvdeliveries.Rows(counter).Cells(13).Value.ToString)

                Received.insertDeliverydetails(barcode, deliverynotenumber, quantitydelivered, deliveryunitprice, totalprice, _
                                               singleitembuyingprice, itemsperpack, totalitemsdelivered, quantityvariance, _
                                               pricevariance, batchno, expirydate, manufacturedby, manufacturedate, orderno, 1)

                inventorystock.alterStock(barcode, totalitemsdelivered, 1)
            Next counter
            MsgBox("Success", MsgBoxStyle.OkOnly, "Strawberry POS")
            Clearcontent()
            'update table stock
        End If
    End Sub

    'Clear content after it has been succefully saved into database
    Sub Clearcontent()
        txtsearch.Clear()
        txtreceiptnumber.Clear()
        txtInvoiceNo.Clear()
        txtreceiptnumber.Clear()
        dtpDateReceived.Value = Today
        cboSuppliers.SelectedIndex = -1
        txtTotalAmnt.Text = ""
        txtvehicleregno.Clear()
        txtothernotes.Clear()
        dgvdeliveries.Rows.Clear()
        btnsave.Text = "Save"
    End Sub

    Sub FormatGrid()
        Try
            With dgvdeliveries
                .AutoResizeColumn(0)
            End With

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    'exit form
    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        'If (MsgBox("Are you sure you want to close this page?", MsgBoxStyle.YesNo, "Product delivery") = MsgBoxResult.Yes) Then
        Me.Close()
        'End If
    End Sub


    'sub load data from database
    Sub Loaddatafromdatabase()

        Dim strsql As String = "select deliveryid,deliverynotenumber,invoicenumber,datereceived,deliveredby,totalamnt,vehicleregno,othernotes from deliverymaster" _
                                & " inner join orderdetails on deliverymaster.orderno=orderdetails.orderno"
        Try

            Dim cmd As New MySqlCommand(strsql, Conn)
            cmd.CommandType = CommandType.Text
            Dim adapter As New MySqlDataAdapter(cmd)
            Dim table As New DataTable
            adapter.Fill(table)
            'Display the data.
            dgvdeliveries.DataSource = table
            closeconn()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    'Loads database table to Datagridview
    Sub Loaddeliverydatafromdatabase()

        Dim strsql As String = "select deliveryid,deliverynotenumber,invoicenumber,datereceived,deliveredby," _
                                & " totalamnt,vehicleregno,othernotes from deliverymaster" _
                                & " inner join orderdetails on deliverymaster.orderno=orderdetails.orderno"
        Try

            Dim cmd As New MySqlCommand(strsql, Conn)
            cmd.CommandType = CommandType.Text
            Dim adapter As New MySqlDataAdapter(cmd)
            Dim table As New DataTable
            adapter.Fill(table)
            'Display the data.
            dgvdeliveries.DataSource = table
            closeconn()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    'deliveries search button
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Dim strsql As String = ""
        Dim searchtext As String, searchInteger As Integer, rowcounter As Integer = 0

        'button click validation
        If txtsearch.Text.Trim = String.Empty Then
            MessageBox.Show("Order Number Or Invoice Number Missing", "Strawberry Solution", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtsearch.Focus()
            Exit Sub
        Else
            searchtext = txtsearch.Text.Trim
        End If

        If rdoOrderno.Checked Then
            searchInteger = Integer.Parse(searchtext)

            strsql = "select itemid ,p.Description,quantityordered," _
                     & " unitprice,totalprice,p.Itemsperpack from orderdetails od " _
                     & " INNER JOIN products p ON od.itemid = p.Barcode where orderno = " & searchInteger & "  "
        Else
            'strsql = "select itemid,concat(p.Description,' ',p.Weight,' ',p.Color,' ',p.Flavor),quantityordered," _
            ' & " unitprice,totalprice from orderdetails od " _
            ' & " INNER JOIN products p ON od.itemid = p.Barcode where orderno = " & searchInteger & " "
        End If

        Dim dr As MySqlDataReader = Nothing
        Dim cmd As New MySqlCommand(strsql, Conn())
        cmd.CommandType = CommandType.Text
        dr = cmd.ExecuteReader

        If (dr.HasRows) Then
            While dr.Read
                dgvdeliveries.Rows.Add()
                dgvdeliveries.Rows(rowcounter).Cells(0).Value = dr(1).ToString
                dgvdeliveries.Rows(rowcounter).Cells(1).Value = dr(2).ToString
                dgvdeliveries.Rows(rowcounter).Cells(2).Value = dr(3).ToString
                dgvdeliveries.Rows(rowcounter).Cells(3).Value = dr(4).ToString
                dgvdeliveries.Rows(rowcounter).Cells(8).Value = dr(5).ToString
                dgvdeliveries.Rows(rowcounter).Cells(12).Value = dr(0).ToString
                dgvdeliveries.Rows(rowcounter).Cells(13).Value = searchInteger.ToString
                rowcounter += 1
            End While
            dgvdeliveries.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells

        End If
        closeconn()

    End Sub

    Private Sub dgvdeliveries_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles dgvdeliveries.CellValueChanged
        Dim i As Integer
        i = dgvdeliveries.RowCount

        'this loop checks if something has been inputed
        'if nothing has been inputed on cell 4 and 5 then nothing happens
        For i = 0 To dgvdeliveries.RowCount - 1
            If ((dgvdeliveries.Rows(i).Cells(4).Value = String.Empty) Or (dgvdeliveries.Rows(i).Cells(5).Value = String.Empty)) Then
                Exit Sub
            Else
                deliveryunitprice = Double.Parse(dgvdeliveries.Rows(i).Cells(5).Value.ToString)
                qtydelivered = Integer.Parse(dgvdeliveries.Rows(i).Cells(4).Value.ToString)
                totalorderprice = Double.Parse(dgvdeliveries.Rows(i).Cells(3).Value.ToString)
                qtyordered = Integer.Parse(dgvdeliveries.Rows(i).Cells(1).Value.ToString)
                itemsperpack = Integer.Parse(dgvdeliveries.Rows(i).Cells(8).Value.ToString)
            End If

            'code to compute the total delivery price
            totaldeliveryprice = deliveryunitprice * qtydelivered
            dgvdeliveries.Rows(i).Cells(6).Value = totaldeliveryprice.ToString

            'code to compute the price variance
            pricevariance = totaldeliveryprice - totalorderprice
            dgvdeliveries.Rows(i).Cells(11).Value = pricevariance.ToString

            'code to compute the quantity variance
            qtyvariance = qtydelivered - qtyordered
            dgvdeliveries.Rows(i).Cells(10).Value = qtyvariance.ToString

            'code to compute item buying price
            itembuyingprice = deliveryunitprice / itemsperpack
            dgvdeliveries.Rows(i).Cells(7).Value = itembuyingprice.ToString

            'code to compute the number of total stock items
            totalstockitems = qtydelivered * itemsperpack
            dgvdeliveries.Rows(i).Cells(9).Value = totalstockitems.ToString
        Next

    End Sub

    Private Sub frmProductDelivery_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'supplier combo
        Dim strsql As String = "SELECT supplier_id,fullname FROM suppliers where relationtype='S'"
        loadcombo(cboSuppliers, strsql, "fullname", "supplier_id")
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Clearcontent()
    End Sub

    Private Sub RemoveProductToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RemoveProductToolStripMenuItem.Click
        Dim i As Integer = dgvdeliveries.CurrentRow.Index
        dgvdeliveries.Rows.Remove(dgvdeliveries.Rows(i))
        dgvdeliveries.Refresh()
    End Sub

    Private Sub UpdateExpiryAndBatchNoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UpdateExpiryAndBatchNoToolStripMenuItem.Click
        If dgvdeliveries.RowCount < 1 Then
            Exit Sub
        End If
        'With frmUpdateExpiryBatch

        '    Try
        '        If dgvdeliveries.CurrentRow.Cells(0).Value.ToString.Trim = Nothing Then Exit Sub
        '        .txtItemDesc.Text = dgvdeliveries.CurrentRow.Cells(0).Value.ToString
        '        .StartPosition = FormStartPosition.CenterScreen
        '        .TopMost = True
        '        .Show()
        '    Catch ex As Exception
        '        'MsgBox(ex.Message)
        '    End Try

        'End With
    End Sub

    Private Sub txtTotalAmnt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTotalAmnt.KeyPress
        'only numbers input validation
        If e.KeyChar <> ChrW(Keys.Back) Then
            If Char.IsNumber(e.KeyChar) Then
            Else
                e.Handled = True
            End If
        End If
    End Sub
End Class