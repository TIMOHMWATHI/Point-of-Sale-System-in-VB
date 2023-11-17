Imports MySql.Data
Imports MySql.Data.MySqlClient

Public Class frmSupplierFullPayReports

    Private Sub frmSupplierFullPayReports_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadSuppliers(txtSearch)
    End Sub

    Private Sub btnLoad_Click(sender As Object, e As EventArgs) Handles btnLoad.Click
        Dim dfrom As Date, dto As Date
        dfrom = dtpFrom.Value.Date
        dto = dtpTo.Value.Date
        'MsgBox(dtpFrom.Value.Date)
        SupplierPaymentReports(dfrom, dto, dgvSupplierPayReport)
    End Sub

    Private Sub txtSearch_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSearch.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim itemname As String
            Dim dfrom As Date, dto As Date
            dfrom = dtpFrom.Value.Date
            dto = dtpTo.Value.Date

            If txtSearch.Text.Trim.Length > 0 Then
                itemname = txtSearch.Text.Trim
                SearchSuppliers(dgvSupplierPayReport, itemname, lblTotalAmntPaid)
            Else
                SupplierPaymentReports(dfrom, dto, dgvSupplierPayReport)
            End If
        End If
    End Sub

    'Loads database table to Datagridview
    Public Sub SearchSuppliers(ByVal gview As DataGridView, _
                               ByVal itemname As String, _
                               ByVal lbl As Label)

        Dim TotalAmntPaid As Double = 0
        'itemname = "%" & itemname & "%"

        Dim strsql As String = "SELECT s.fullname,s.phone,sp.transactionno,sp.totalamnt," _
                                         & " GROUP_CONCAT(DISTINCT  dd.quantitydelivered , ' - ' ,p.Description" _
                                         & " ORDER BY dd.deliveryno SEPARATOR '  ;  ') AS 'Receipt Items'," _
                                         & " sp.othernotes,DATE_FORMAT(spn.paydate, '%Y %M %d') AS PayDate" _
                                         & " FROM  supplierpayments sp " _
                                         & " INNER JOIN supplierpaynarration spn ON spn.masterno=sp.masterno" _
                                         & " INNER JOIN deliverymaster dm ON dm.receiptnumber=sp.transactionno" _
                                         & " INNER JOIN deliverydetails dd ON dd.deliveryno=dm.deliveryid" _
                                         & " INNER JOIN products p ON p.Barcode=dd.barcode" _
                                         & " INNER JOIN suppliers s ON s.supplier_id=sp.suppliedby" _
                                         & " WHERE s.deleted!=1 AND spn.paymode='Full Payment'" _
                                         & " AND s.fullname='" & itemname & "'" _
                                         & " GROUP BY sp.transactionno ORDER BY s.fullname,sp.transactionno ASC"

        Dim cmd As New MySqlCommand(strsql, Conn)
        cmd.CommandType = CommandType.Text
        Dim adapter As New MySqlDataAdapter(cmd)
        Dim table As New DataTable
        adapter.Fill(table)
        'Display the data.
        gview.DataSource = table
        closeconn()
        With gview
            .Columns(0).HeaderText = "Supplier Name"
            .Columns(1).HeaderText = "Contacts"
            .Columns(2).HeaderText = "Receipt No"
            .Columns(3).HeaderText = "Receipt Total"
            .Columns(4).HeaderText = "Receipt Items"
            .Columns(5).HeaderText = "Notes"
            .Columns(6).HeaderText = "Pay Date"
            .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells
            .Columns(0).DefaultCellStyle.WrapMode = DataGridViewTriState.True
            .Columns(1).DefaultCellStyle.WrapMode = DataGridViewTriState.True
            .Columns(2).DefaultCellStyle.WrapMode = DataGridViewTriState.True
            .Columns(5).DefaultCellStyle.WrapMode = DataGridViewTriState.True
            .Columns(6).DefaultCellStyle.WrapMode = DataGridViewTriState.True
            .Columns(4).DefaultCellStyle.WrapMode = DataGridViewTriState.True
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            .Columns(3).DefaultCellStyle.Format = "n2"  'code to set decimals places
        End With
        For i = 0 To gview.RowCount - 1
            TotalAmntPaid = TotalAmntPaid + Double.Parse(gview.Rows(i).Cells(3).Value)
        Next
        lbl.Text = FormatCurrency(TotalAmntPaid.ToString, 2)
    End Sub


    Sub SupplierPaymentReports(ByVal dfrom As Date, ByVal dto As Date, ByVal dgv As DataGridView)

        Dim TotalAmntPaid As Double = 0

        Dim strsql As String = "SELECT s.fullname,s.phone,sp.transactionno,sp.totalamnt," _
                               & " GROUP_CONCAT(DISTINCT  dd.quantitydelivered , ' - ' ,p.Description" _
                               & " ORDER BY dd.deliveryno SEPARATOR '  ;  ') AS 'Receipt Items'," _
                               & " sp.othernotes,DATE_FORMAT(spn.paydate, '%Y %M %d') AS PayDate" _
                               & " FROM  supplierpayments sp " _
                               & " INNER JOIN supplierpaynarration spn ON spn.masterno=sp.masterno" _
                               & " INNER JOIN deliverymaster dm ON dm.receiptnumber=sp.transactionno" _
                               & " INNER JOIN deliverydetails dd ON dd.deliveryno=dm.deliveryid" _
                               & " INNER JOIN products p ON p.Barcode=dd.barcode" _
                               & " INNER JOIN suppliers s ON s.supplier_id=sp.suppliedby" _
                               & " WHERE s.deleted!=1 AND spn.paymode='Full Payment' AND " _
                               & " DATE(spn.paydate) BETWEEN '" & dfrom & "' AND '" & dto & "'" _
                               & " GROUP BY sp.transactionno,spn.paymode ORDER BY s.fullname,sp.transactionno ASC"
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
                .Columns(0).HeaderText = "Supplier Name"
                .Columns(1).HeaderText = "Contacts"
                .Columns(2).HeaderText = "Receipt No"
                .Columns(3).HeaderText = "Receipt Total"
                .Columns(4).HeaderText = "Receipt Items"
                .Columns(5).HeaderText = "Notes"
                .Columns(6).HeaderText = "Pay Date"
                .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells
                .Columns(0).DefaultCellStyle.WrapMode = DataGridViewTriState.True
                .Columns(1).DefaultCellStyle.WrapMode = DataGridViewTriState.True
                .Columns(2).DefaultCellStyle.WrapMode = DataGridViewTriState.True
                .Columns(5).DefaultCellStyle.WrapMode = DataGridViewTriState.True
                .Columns(6).DefaultCellStyle.WrapMode = DataGridViewTriState.True
                .Columns(4).DefaultCellStyle.WrapMode = DataGridViewTriState.True
                .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
                .Columns(3).DefaultCellStyle.Format = "n2"  'code to set decimals places
            End With
            For i = 0 To dgv.RowCount - 1
                TotalAmntPaid = TotalAmntPaid + Double.Parse(dgv.Rows(i).Cells(3).Value)
            Next
            lblTotalAmntPaid.Text = FormatCurrency(TotalAmntPaid.ToString, 2)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        'clear textbox
        txtSearch.Clear()

        Dim dfrom As Date, dto As Date
        dfrom = dtpFrom.Value.Date
        dto = dtpTo.Value.Date
        'MsgBox(dtpFrom.Value.Date)
        SupplierPaymentReports(dfrom, dto, dgvSupplierPayReport)
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class