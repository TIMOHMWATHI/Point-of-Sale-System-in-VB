Imports MySql.Data
Imports MySql.Data.MySqlClient

Public Class frmSupplierDeliveriesReport

    Private Sub btnLoad_Click(sender As Object, e As EventArgs) Handles btnLoad.Click
        Dim dfrom As Date, dto As Date
        dfrom = dtpFrom.Value.Date
        dto = dtpTo.Value.Date
        'MsgBox(dtpFrom.Value.Date)
        SupplierDeliveries(dfrom, dto, dgvDeliveries)
    End Sub

    Sub SupplierDeliveries(ByVal dfrom As Date, ByVal dto As Date, ByVal dgv As DataGridView)

        Dim TotalAmntPaid As Double = 0

        Dim strsql As String = "SELECT dd.deliverynumber,dd.barcode,p.Description," _
                               & " dd.quantitydelivered,dd.deliveryunitprice," _
                               & " dd.totalprice,dd.singleitembuyingprice,dd.itemsperpack," _
                               & " dd.totalitemsdelivered,DATE_FORMAT(dm.datereceived, '%Y %M %d')" _
                               & " AS Date_Received,s.fullname AS 'Suppliedby' FROM deliverydetails dd" _
                               & " INNER JOIN products p ON p.Barcode=dd.barcode" _
                               & " INNER JOIN deliverymaster dm ON dm.deliveryid=dd.deliveryno" _
                               & " INNER JOIN suppliers s ON s.supplier_id=dm.deliveredby" _
                               & " WHERE DATE(dm.datereceived) BETWEEN '" & dfrom & "' AND '" & dto & "' " _
                               & " ORDER BY s.fullname, dm.datereceived,dd.deliverynumber ASC"
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
                .Columns(0).HeaderText = "Delivery No"
                .Columns(1).HeaderText = "Barcode"
                .Columns(2).HeaderText = "Description"
                .Columns(3).HeaderText = "Qty"
                .Columns(4).HeaderText = "Unit Price"
                .Columns(5).HeaderText = "Total"
                .Columns(6).HeaderText = "Item Buying Price"
                .Columns(7).HeaderText = "Items Per Pack"
                .Columns(8).HeaderText = "Total Qty Delivered"
                .Columns(9).HeaderText = "Date Received"
                .Columns(10).HeaderText = "Received By"
                .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells
                .Columns(0).DefaultCellStyle.WrapMode = DataGridViewTriState.True
                .Columns(2).DefaultCellStyle.WrapMode = DataGridViewTriState.True
                .Columns(4).DefaultCellStyle.WrapMode = DataGridViewTriState.True
                .Columns(5).DefaultCellStyle.WrapMode = DataGridViewTriState.True
                .Columns(6).DefaultCellStyle.WrapMode = DataGridViewTriState.True
                .Columns(7).DefaultCellStyle.WrapMode = DataGridViewTriState.True
                .Columns(8).DefaultCellStyle.WrapMode = DataGridViewTriState.True
                .Columns(9).DefaultCellStyle.WrapMode = DataGridViewTriState.True
                .Columns(10).DefaultCellStyle.WrapMode = DataGridViewTriState.True
                .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
                .Columns(5).DefaultCellStyle.Format = "n2"  'code to set decimals places
            End With
            For i = 0 To dgv.RowCount - 1
                TotalAmntPaid = TotalAmntPaid + Double.Parse(dgv.Rows(i).Cells(5).Value)
            Next
            lblTotalAmntPaid.Text = FormatCurrency(TotalAmntPaid.ToString, 2)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    'Loads database table to Datagridview
    Public Sub SearchSuppliers(ByVal dfrom As Date, _
                               ByVal dto As Date, _
                               ByVal dgv As DataGridView, _
                               ByVal itemname As String, _
                               ByVal lbl As Label)

        Dim TotalAmntPaid As Double = 0

        Dim strsql As String = "SELECT dd.deliverynumber,dd.barcode,p.Description," _
                               & " dd.quantitydelivered,dd.deliveryunitprice," _
                               & " dd.totalprice,dd.singleitembuyingprice,dd.itemsperpack," _
                               & " dd.totalitemsdelivered,DATE_FORMAT(dm.datereceived, '%Y %M %d')" _
                               & " AS Date_Received,s.fullname AS 'Suppliedby' FROM deliverydetails dd" _
                               & " INNER JOIN products p ON p.Barcode=dd.barcode" _
                               & " INNER JOIN deliverymaster dm ON dm.deliveryid=dd.deliveryno" _
                               & " INNER JOIN suppliers s ON s.supplier_id=dm.deliveredby" _
                               & " WHERE DATE(dm.datereceived) BETWEEN '" & dfrom & "' AND '" & dto & "' " _
                               & " AND s.deleted!=1 AND  s.fullname='" & itemname & "'" _
                               & " OR dd.deliverynumber='" & itemname & "'"

        Dim cmd As New MySqlCommand(strsql, Conn)
        cmd.CommandType = CommandType.Text
        Dim adapter As New MySqlDataAdapter(cmd)
        Dim table As New DataTable
        adapter.Fill(table)
        'Display the data.
        dgv.DataSource = table
        closeconn()
        With dgv
            .Columns(0).HeaderText = "Delivery No"
            .Columns(1).HeaderText = "Barcode"
            .Columns(2).HeaderText = "Description"
            .Columns(3).HeaderText = "Qty"
            .Columns(4).HeaderText = "Unit Price"
            .Columns(5).HeaderText = "Total"
            .Columns(6).HeaderText = "Item Buying Price"
            .Columns(7).HeaderText = "Items Per Pack"
            .Columns(8).HeaderText = "Total Qty Delivered"
            .Columns(9).HeaderText = "Date Received"
            .Columns(10).HeaderText = "Received By"
            .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells
            .Columns(0).DefaultCellStyle.WrapMode = DataGridViewTriState.True
            .Columns(2).DefaultCellStyle.WrapMode = DataGridViewTriState.True
            .Columns(4).DefaultCellStyle.WrapMode = DataGridViewTriState.True
            .Columns(5).DefaultCellStyle.WrapMode = DataGridViewTriState.True
            .Columns(6).DefaultCellStyle.WrapMode = DataGridViewTriState.True
            .Columns(7).DefaultCellStyle.WrapMode = DataGridViewTriState.True
            .Columns(8).DefaultCellStyle.WrapMode = DataGridViewTriState.True
            .Columns(9).DefaultCellStyle.WrapMode = DataGridViewTriState.True
            .Columns(10).DefaultCellStyle.WrapMode = DataGridViewTriState.True
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            .Columns(5).DefaultCellStyle.Format = "n2"  'code to set decimals places
        End With
        For i = 0 To dgv.RowCount - 1
            TotalAmntPaid = TotalAmntPaid + Double.Parse(dgv.Rows(i).Cells(5).Value)
        Next
        lbl.Text = FormatCurrency(TotalAmntPaid.ToString, 2)
    End Sub


    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        'clear textbox
        txtSearch.Clear()

        Dim dfrom As Date, dto As Date
        dfrom = dtpFrom.Value.Date
        dto = dtpTo.Value.Date
        'MsgBox(dtpFrom.Value.Date)
        SupplierDeliveries(dfrom, dto, dgvDeliveries)
    End Sub

    Private Sub txtSearch_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSearch.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim dfrom As Date, dto As Date, itemname As String
            dfrom = dtpFrom.Value.Date
            dto = dtpTo.Value.Date

            If txtSearch.Text.Trim.Length > 0 Then
                itemname = txtSearch.Text.Trim
                SearchSuppliers(dfrom, dto, dgvDeliveries, itemname, lblTotalAmntPaid)
            Else
                SupplierDeliveries(dfrom, dto, dgvDeliveries)
            End If
        End If
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub frmSupplierDeliveriesReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'load suppliers
        LoadSuppliers(txtSearch)
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged

    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        If txtSearch.Text = String.Empty Then
            MessageBox.Show("Search invoice Number to proceed", "Strawberry POS", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        Else
            printByEpos()
        End If
    End Sub

    Private Sub printByEpos()
        'Dim receiptNo As Long = Long.Parse(Now().Date.Day & Now().Date.Month & Now().Date.Year & Now().TimeOfDay.Hours & Now().TimeOfDay.Minutes)
        Dim dt As New DataTable

        Dim Description As String, _
            Qty As Integer, _
            UnitPrice As Double, _
            TotalAmnt As Double

        With dt
            .Columns.Add("description")
            .Columns.Add("qty")
            .Columns.Add("price")
            .Columns.Add("total")
        End With

        With dgvDeliveries
            For i = 0 To dgvDeliveries.RowCount - 1
                Description = dgvDeliveries.Rows(i).Cells(2).Value.ToString
                Qty = Integer.Parse(dgvDeliveries.Rows(i).Cells(3).Value.ToString)
                UnitPrice = Double.Parse(dgvDeliveries.Rows(i).Cells(4).Value.ToString)
                TotalAmnt = Double.Parse(dgvDeliveries.Rows(i).Cells(5).Value.ToString)
                'add rows
                dt.Rows.Add(Description, Qty, UnitPrice, TotalAmnt)
            Next
        End With
        Try
            Dim rptDoc As CrystalDecisions.CrystalReports.Engine.ReportDocument

            rptDoc = New StockDeliveries
            rptDoc.SetDataSource(dt)
            rptDoc.SetParameterValue("Receivedby", fullname)
            rptDoc.SetParameterValue("InvoiceNo", txtSearch.Text)
            rptDoc.SetParameterValue("TotalSales", lblTotalAmntPaid.Text)
            'print by epos 
            rptDoc.PrintToPrinter(1, False, 0, 0)
        Catch ex As Exception

        End Try
    End Sub
End Class