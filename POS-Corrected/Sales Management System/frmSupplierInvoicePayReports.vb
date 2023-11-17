Imports MySql.Data
Imports MySql.Data.MySqlClient

Public Class frmSupplierInvoicePayReports
    Private Sub frmSupplierPaymentReports_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
                               & " SUM(spn.amountpaid) AS 'Amnt Paid'," _
                               & " (sp.totalamnt- sp.amountpaid) AS 'Balance', " _
                               & " DATE_FORMAT(spn.paydate, '%Y %M %d') AS PayDate," _
                               & " spn.othernotes,st.fullnames AS 'Receivedby'," _
                               & " CASE WHEN sp.paystatus=2 THEN 'Partially Paid'" _
                               & " ELSE 'Fully Paid' END AS 'Pay Status'" _
                               & " FROM supplierpaynarration spn " _
                               & " INNER JOIN supplierpayments sp ON sp.masterno=spn.masterno" _
                               & " INNER JOIN suppliers s ON s.supplier_id=sp.suppliedby" _
                               & " INNER JOIN staff st ON st.staffid=spn.servedby" _
                               & " WHERE sp.paystatus!=0 AND s.deleted!=1 AND " _
                               & " s.fullname='" & itemname & "' AND sp.paymode='Invoice Payment'" _
                               & " GROUP BY sp.transactionno "

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
            .Columns(2).HeaderText = "Invoice No"
            .Columns(3).HeaderText = "Invoice Total"
            .Columns(4).HeaderText = "Amount Paid"
            .Columns(5).HeaderText = "Balance"
            .Columns(6).HeaderText = "Pay Date"
            .Columns(7).HeaderText = "Memo"
            .Columns(8).HeaderText = "Received By"
            .Columns(9).HeaderText = "Pay Status"
            .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells
            .Columns(0).DefaultCellStyle.WrapMode = DataGridViewTriState.True
            .Columns(1).DefaultCellStyle.WrapMode = DataGridViewTriState.True
            .Columns(2).DefaultCellStyle.WrapMode = DataGridViewTriState.True
            .Columns(5).DefaultCellStyle.WrapMode = DataGridViewTriState.True
            .Columns(6).DefaultCellStyle.WrapMode = DataGridViewTriState.True
            .Columns(7).DefaultCellStyle.WrapMode = DataGridViewTriState.True
            .Columns(8).DefaultCellStyle.WrapMode = DataGridViewTriState.True
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            .Columns(3).DefaultCellStyle.Format = "n2"  'code to set decimals places
            .Columns(4).DefaultCellStyle.Format = "n2"  'code to set decimals places
        End With
        For i = 0 To gview.RowCount - 1
            TotalAmntPaid = TotalAmntPaid + Double.Parse(gview.Rows(i).Cells(4).Value)
        Next
        lbl.Text = FormatCurrency(TotalAmntPaid.ToString, 2)
    End Sub

    Sub SupplierPaymentReports(ByVal dfrom As Date, ByVal dto As Date, ByVal dgv As DataGridView)

        Dim TotalAmntPaid As Double = 0

        Dim strsql As String = "SELECT s.fullname,s.phone,sp.transactionno,sp.totalamnt," _
                               & " SUM(spn.amountpaid) AS 'Amnt Paid'," _
                               & " (sp.totalamnt- sp.amountpaid) AS 'Balance', " _
                               & " DATE_FORMAT(spn.paydate, '%Y %M %d') AS PayDate," _
                               & " spn.othernotes,st.fullnames AS 'Receivedby'," _
                               & " CASE WHEN sp.paystatus=2 THEN 'Partially Paid'" _
                               & " ELSE 'Fully Paid' END AS 'Pay Status'" _
                               & " FROM supplierpaynarration spn " _
                               & " INNER JOIN supplierpayments sp ON sp.masterno=spn.masterno" _
                               & " INNER JOIN suppliers s ON s.supplier_id=sp.suppliedby" _
                               & " INNER JOIN staff st ON st.staffid=spn.servedby" _
                               & " WHERE sp.paystatus!=0 AND s.deleted!=1 AND sp.paymode='Invoice Payment' AND " _
                               & " DATE(spn.paydate) BETWEEN '" & dfrom & "' AND '" & dto & "'" _
                               & " GROUP BY sp.transactionno ORDER BY s.fullname,sp.transactionno ASC"
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
                .Columns(2).HeaderText = "Invoice No"
                .Columns(3).HeaderText = "Invoice Total"
                .Columns(4).HeaderText = "Amount Paid"
                .Columns(5).HeaderText = "Balance"
                .Columns(6).HeaderText = "Pay Date"
                .Columns(7).HeaderText = "Memo"
                .Columns(8).HeaderText = "Received By"
                .Columns(9).HeaderText = "Pay Status"
                .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells
                .Columns(0).DefaultCellStyle.WrapMode = DataGridViewTriState.True
                .Columns(1).DefaultCellStyle.WrapMode = DataGridViewTriState.True
                .Columns(2).DefaultCellStyle.WrapMode = DataGridViewTriState.True
                .Columns(5).DefaultCellStyle.WrapMode = DataGridViewTriState.True
                .Columns(6).DefaultCellStyle.WrapMode = DataGridViewTriState.True
                .Columns(7).DefaultCellStyle.WrapMode = DataGridViewTriState.True
                .Columns(8).DefaultCellStyle.WrapMode = DataGridViewTriState.True
                .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
                .Columns(3).DefaultCellStyle.Format = "n2"  'code to set decimals places
                .Columns(4).DefaultCellStyle.Format = "n2"  'code to set decimals places
            End With
            For i = 0 To dgv.RowCount - 1
                TotalAmntPaid = TotalAmntPaid + Double.Parse(dgv.Rows(i).Cells(4).Value)
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