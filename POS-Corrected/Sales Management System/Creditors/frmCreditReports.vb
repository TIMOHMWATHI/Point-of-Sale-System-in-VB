Imports MySql.Data
Imports MySql.Data.MySqlClient
Public Class frmCreditReports

    Private Sub btnCreditSearch_Click(sender As Object, e As EventArgs) Handles btnCreditSearch.Click
        Dim Customerid As Integer
        If txtCustomerName.Text.Trim = String.Empty Then
            MessageBox.Show("Credit customer name missing", "Strawberry System", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtCustomerName.Focus()
            Exit Sub
        Else
            Customerid = Integer.Parse(lblEntryid.Text)
        End If
        Dim dfrom As Date = dtpFrom.Value.Date
        Dim dto As Date = dtpTo.Value.Date
        'MsgBox(Customerid)
        LoadCreditReports(dfrom, dto, Customerid, dgvcreditReports)
        LoadCreditItemsSold(dfrom, dto, Customerid, dgvCreditItemsSold)
        LoadPaymentNarration(dfrom, dto, Customerid, dgvPaymentNarration)
    End Sub

    Sub LoadCreditReports(ByVal dfrom As Date, ByVal dto As Date, ByVal customerid As Integer, _
                              ByVal dgv As DataGridView)
        Dim totalsales As Double = 0
        customerid = Integer.Parse(lblEntryid.Text)

        Dim strsql As String = "SELECT sd.receptno,GROUP_CONCAT(p.Description, ',')," _
                               & " sm.totalamount, sm.amountpaid, sm.discount," _
                               & " sm.totalamount-(sm.amountpaid+sm.discount) as Unpaid," _
                               & " sm.VATamount, CASE WHEN (sm.amountpaid + sm.discount=sm.totalamount)" _
                               & " THEN 'Fully Paid' Else 'Not Fully Paid' END as status," _
                               & " sm.receptdatetime  FROM salesmaster sm" _
                               & " INNER JOIN salesdetails sd ON sd.receptno= sm.receptno" _
                               & " INNER JOIN products p ON p.Barcode=sd.itemcode" _
                               & " WHERE sm.customerid=" & customerid & " AND DATE(sm.receptdatetime)" _
                               & " BETWEEN '" & dfrom & "' AND'" & dto & "' GROUP BY sm.receptno asc"

        ''Try
        Dim cmd As New MySqlCommand(strsql, Conn)
        cmd.CommandType = CommandType.Text
        Dim adapter As New MySqlDataAdapter(cmd)
        Dim table As New DataTable
        adapter.Fill(table)
        'Display the data.
        dgv.DataSource = table
        adapter.Dispose()
        closeconn()
        With dgv
            .Columns(0).HeaderText = "Receipt No"
            .Columns(1).HeaderText = "Description"
            .Columns(2).HeaderText = "Total"
            .Columns(3).HeaderText = "Amount Paid"
            .Columns(4).HeaderText = "Discount"
            .Columns(5).HeaderText = "Unpaid"
            .Columns(6).HeaderText = "V.A.T"
            .Columns(7).HeaderText = "Status"
            .Columns(8).HeaderText = "Date And Time"

            .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells
            .Columns(1).DefaultCellStyle.WrapMode = DataGridViewTriState.True
            .Columns(7).DefaultCellStyle.WrapMode = DataGridViewTriState.True
            .Columns(8).DefaultCellStyle.WrapMode = DataGridViewTriState.True
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        End With
        For i = 0 To dgvcreditReports.RowCount - 1
            totalsales = totalsales + Double.Parse(dgvcreditReports.Rows(i).Cells(2).Value)
        Next
        lblCreditTotals.Text = "KShs" & " " & totalsales.ToString

        'Catch ex As Exception
        '    MessageBox.Show(ex.Message)
        'End Try
    End Sub

    Sub LoadCreditItemsSold(ByVal dfrom As Date, ByVal dto As Date, ByVal customerid As Integer, _
                             ByVal dgv As DataGridView)
        Dim InvoiceTotals As Double = 0, InvoiceDiscount As Double = 0, _
           AmntToPay As Double = 0, DiscountTotals As Double = 0, _
           SumAmntToPay As Double = 0, ReceptTotals As Double = 0
        Dim i As Integer

        Dim strsql As String = " SELECT DATE_FORMAT(sm.receptdatetime, '%Y,%M,%d') AS ReceiptDate," _
                               & " sd.receptno, p.Description, sd.quantitybought, sd.itemsellingprice, " _
                               & " (sd.itemsellingprice * sd.quantitybought) as Total,sd.itemdiscount, " _
                               & " s.fullnames AS 'Sold by' " _
                               & " FROM salesdetails sd INNER JOIN salesmaster sm ON sm.receptno=sd.receptno " _
                               & " INNER JOIN products p ON p.Barcode=sd.itemcode " _
                               & " INNER JOIN staff s ON s.staffid=sm.soldby " _
                               & " WHERE sm.customerid=" & customerid & " AND DATE(sm.receptdatetime) BETWEEN '" & dfrom & "' " _
                               & "  AND '" & dto & "' ORDER BY sm.fullypaid asc, sm.receptno asc "

        'Try
        Dim cmd As New MySqlCommand(strsql, Conn)
        cmd.CommandType = CommandType.Text
        Dim adapter As New MySqlDataAdapter(cmd)
        Dim table As New DataTable
        adapter.Fill(table)
        'Display the data.
        dgv.DataSource = table
        closeconn()
        With dgv
            .Columns(0).HeaderText = "Receipt Date"
            .Columns(1).HeaderText = "Receipt No"
            .Columns(2).HeaderText = "Description"
            .Columns(3).HeaderText = "Qty"
            .Columns(4).HeaderText = "Selling Price"
            .Columns(5).HeaderText = "Total Amount"
            .Columns(6).HeaderText = "Discount"
            .Columns(7).HeaderText = "Sold By"
            .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells
            .Columns(1).DefaultCellStyle.WrapMode = DataGridViewTriState.True
            .Columns(2).DefaultCellStyle.WrapMode = DataGridViewTriState.True
            .Columns(7).DefaultCellStyle.WrapMode = DataGridViewTriState.True
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        End With
        'MsgBox(i)
        For i = 0 To dgvCreditItemsSold.RowCount - 1
            InvoiceTotals = Double.Parse(dgvCreditItemsSold.Rows(i).Cells(5).Value)
            ReceptTotals += Double.Parse(dgvCreditItemsSold.Rows(i).Cells(5).Value)
            InvoiceDiscount = Double.Parse(dgvCreditItemsSold.Rows(i).Cells(6).Value)
            DiscountTotals += Double.Parse(dgvCreditItemsSold.Rows(i).Cells(6).Value)
            'AmntToPay = Double.Parse(dgvCreditItemsSold.Rows(i).Cells(7).Value)
            'SumAmntToPay += Double.Parse(dgvCreditItemsSold.Rows(i).Cells(7).Value)
        Next
        lblReceptTotal.Text = ReceptTotals.ToString
        lblReceptDiscount.Text = DiscountTotals.ToString
    End Sub


    Private Sub frmCreditReports_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'LOAD CUSTOMER CREDITORS
        LoadCreditorNames(txtCustomerName)
    End Sub

    Sub Loadproducts()
        Dim receptno As Integer = Integer.Parse(txtSearchReceipt.Text)
        Dim products As New AutoCompleteStringCollection, strsql As String
        Dim DS As New DataSet

        Dim dfrom As Date = dtpFrom.Value.Date
        Dim dto As Date = dtpTo.Value.Date
        Dim totalsales As Double = 0
        strsql = " SELECT sd.receptno, p.Description, sd.itemsellingprice, sd.quantitybought, " _
                 & "(sd.itemsellingprice * sd.quantitybought) as Total " _
                 & "FROM salesdetails sd INNER JOIN salesmaster sm ON sm.receptno=sd.receptno " _
                 & "INNER JOIN products p ON p.Barcode=sd.itemcode " _
                 & "WHERE sm.receptno=" & txtSearchReceipt.Text & " "

        DS = ReturnDataset(strsql, "items")
        For x = 0 To DS.Tables("items").Rows.Count - 1
            products.Add(DS.Tables("items").Rows(x).Item("Description"))
        Next
        'DS.Dispose()
        txtSearchReceipt.AutoCompleteCustomSource = products
        txtSearchReceipt.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        txtSearchReceipt.AutoCompleteSource = AutoCompleteSource.CustomSource
    End Sub

    Public Function ReturnDataset(ByVal QryString As String, ByVal tName As String) As DataSet
        Dim ds As New DataSet
        'Try
        Dim da As New MySqlDataAdapter(QryString, Conn)
        da.Fill(ds, tName)
        closeconn()
        Return ds
    End Function

    Private Sub txtSearchReceipt_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSearchReceipt.KeyDown
        Dim dfrom As Date = dtpFrom.Value.Date
        Dim dto As Date = dtpTo.Value.Date
        Dim customerid = Integer.Parse(lblEntryid.Text)
        Dim ReceiptTotals As Double = 0

        If txtSearchReceipt.Text.Trim = String.Empty Then
            'do nothing
        ElseIf e.KeyCode = Keys.Enter Then

            Dim receiptnumber As String = txtSearchReceipt.Text.Trim
            '###################################################################
            'retrieve salesperson and sales date
            Dim datareader As MySqlDataReader

            Dim strsql = "SELECT DATE_FORMAT(sm.receptdatetime, '%Y,%M,%d') AS ReceiptDate," _
                         & " s.fullnames,sm.amountpaid FROM salesmaster sm " _
                         & " INNER JOIN staff s ON s.staffid=sm.soldby" _
                         & " WHERE sm.customerid=" & customerid & " AND sm.receptno='" & receiptnumber & "'"

            Dim cmd As New MySqlCommand(strsql, Conn)
            cmd.CommandType = CommandType.Text
            datareader = cmd.ExecuteReader
            While datareader.Read
                lblReceptDate.Text = datareader(0).ToString
                lblSoldBy.Text = datareader(1).ToString
                lblAmntPaid.Text = Double.Parse(datareader(2).ToString)
                lblsdate.Visible = True
                lblReceptDate.Visible = True
                lbluser.Visible = True
                lblSoldBy.Visible = True
                lblpaid.Visible = True
                lblAmntPaid.Visible = True
            End While
            datareader.Dispose()
            closeconn()
            '###################################################################

            'retrieve data to grid
            If txtSearchReceipt.Text.Trim.Length > 0 Then
                LoadRecept_Data(dgvCreditItemsSold)
            Else
                LoadCreditItemsSold(dfrom, dto, customerid, dgvCreditItemsSold)
            End If
        End If

        For i = 0 To dgvCreditItemsSold.RowCount - 1
            ReceiptTotals = ReceiptTotals + Double.Parse(dgvCreditItemsSold.Rows(i).Cells(5).Value)
        Next
        lblReceptTotal.Text = ReceiptTotals.ToString
        'MsgBox(i)
    End Sub

    'Loads database table to Datagridview
    Sub LoadRecept_Data(ByVal dgv As DataGridView)
        Dim dfrom As Date = dtpFrom.Value.Date
        Dim dto As Date = dtpTo.Value.Date
        Dim customerid As Integer = Integer.Parse(lblEntryid.Text.ToString)

        Dim receiptTotals As Double = 0, ReceiptDiscount As Double = 0

        Dim strsql As String = "SELECT DATE_FORMAT(sm.receptdatetime, '%Y,%M,%d') AS ReceiptDate," _
                               & " sd.receptno, p.Description, sd.itemsellingprice,sd.quantitybought," _
                              & " (sd.itemsellingprice * sd.quantitybought) AS Total,sd.itemdiscount," _
                              & " s.fullnames AS 'Sold by' FROM salesdetails sd " _
                              & " INNER JOIN salesmaster sm ON sm.receptno=sd.receptno" _
                              & " INNER JOIN products p ON p.Barcode=sd.itemcode " _
                              & " INNER JOIN staff s ON s.staffid = sm.soldby " _
                              & " WHERE sm.receptno=" & txtSearchReceipt.Text & " AND " _
                              & " sm.customerid=" & customerid & " AND DATE(sm.receptdatetime) " _
                              & " BETWEEN '" & dfrom & "' AND '" & dto & "'"
        Try
            Dim cmd As New MySqlCommand(strsql, Conn)
            cmd.CommandType = CommandType.Text
            Dim adapter As New MySqlDataAdapter(cmd)
            Dim table As New DataTable
            adapter.Fill(table)
            'Display the data.
            dgv.DataSource = table
            adapter.Dispose()
            closeconn()

            With dgv
                .Columns(0).HeaderText = "Receipt Date"
                .Columns(1).HeaderText = "Receipt No"
                .Columns(2).HeaderText = "Description"
                .Columns(3).HeaderText = "Qty"
                .Columns(4).HeaderText = "Selling Price"
                .Columns(5).HeaderText = "Total Amount"
                .Columns(6).HeaderText = "Discount"
                .Columns(7).HeaderText = "Sold By"
                .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells
                .Columns(1).DefaultCellStyle.WrapMode = DataGridViewTriState.True
                .Columns(2).DefaultCellStyle.WrapMode = DataGridViewTriState.True
                .Columns(7).DefaultCellStyle.WrapMode = DataGridViewTriState.True
                .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            End With
            For i = 0 To dgvCreditItemsSold.RowCount - 1
                receiptTotals += Double.Parse(dgvCreditItemsSold.Rows(i).Cells(5).Value)
                ReceiptDiscount += Double.Parse(dgvCreditItemsSold.Rows(i).Cells(6).Value)
            Next
            lblReceptTotal.Text = receiptTotals.ToString
            lblReceptDiscount.Text = ReceiptDiscount.ToString
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub


    Sub LoadPaymentNarration(ByVal dfrom As Date, ByVal dto As Date, ByVal customerid As Integer, _
                          ByVal dgv As DataGridView)
        'Dim totalreceiptItemssales As Double = 0
        'Dim i As Integer

        Dim strsql As String = "SELECT c.entryid,sm.receptno,s.fullname,c.amountpaid, " _
                               & " c.datepaid FROM creditpaynarration c" _
                               & " INNER JOIN salesmaster sm ON sm.receptno=c.receiptno " _
                               & " INNER JOIN suppliers s ON s.supplier_id=c.customerno" _
                               & " WHERE sm.customerid = " & customerid & "" _
                               & " AND DATE(sm.receptdatetime) BETWEEN '" & dfrom & "' " _
                               & " AND '" & dto & "' AND s.relationtype='C' ORDER BY sm.receptno,c.datepaid ASC "

        'Try
        Dim cmd As New MySqlCommand(strsql, Conn)
        cmd.CommandType = CommandType.Text
        Dim adapter As New MySqlDataAdapter(cmd)
        Dim table As New DataTable
        adapter.Fill(table)
        'Display the data.
        dgv.DataSource = table
        adapter.Dispose()
        closeconn()
        With dgv
            .Columns(0).HeaderText = "#"
            .Columns(1).HeaderText = "Receipt No"
            .Columns(2).HeaderText = "Fullnames"
            .Columns(3).HeaderText = "Amount Paid"
            .Columns(4).HeaderText = "Date Paid"
            .Columns(0).Visible = False
            .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells
            .Columns(2).DefaultCellStyle.WrapMode = DataGridViewTriState.True
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        End With

        'For i = 0 To dgvCreditItemsSold.RowCount - 2
        '    totalreceiptItemssales = totalreceiptItemssales + Double.Parse(dgv.Rows(i).Cells(4).Value)
        'Next
        'lblReceptTotal.Text = "KShs" & " " & totalreceiptItemssales.ToString
    End Sub


    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        txtSearchReceipt.Clear()
        lblReceptTotal.Text = 0
        lblReceptDate.Text = ""
        lblSoldBy.Text = ""
        lblAmntPaid.Text = 0
        lblsdate.Visible = False
        lblReceptDate.Visible = False
        lbluser.Visible = False
        lblSoldBy.Visible = False
        lblpaid.Visible = True
        lblAmntPaid.Visible = False
        btnCreditSearch.PerformClick()
    End Sub

    Private Sub txtCustomerName_KeyDown(sender As Object, e As KeyEventArgs) Handles txtCustomerName.KeyDown
        Dim datareader As MySqlDataReader
        Try
            If e.KeyCode = Keys.Enter Then
                'input validation on txtCustomerName
                Dim customername As String
                If (txtCustomerName.Text = String.Empty) Then
                    MessageBox.Show("Credit customer name missing", "Strawberry Solution", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    txtCustomerName.Focus()
                    Exit Sub
                Else
                    customername = txtCustomerName.Text.Trim
                End If

                Dim strsql = "SELECT fullname,supplier_id FROM suppliers WHERE relationtype='C'" _
                             & " AND deleted= 0 AND (fullname='" & txtCustomerName.Text.Trim & "') "

                Dim cmd As New MySqlCommand(strsql, Conn)
                cmd.CommandType = CommandType.Text
                datareader = cmd.ExecuteReader
                While datareader.Read
                    txtCustomerName.Text = datareader(0).ToString
                    lblEntryid.Text = Integer.Parse(datareader(1).ToString)
                End While
                datareader.Dispose()
                closeconn()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        clearcontrols()
    End Sub

    Sub clearcontrols()
        txtCustomerName.Clear()
        dtpFrom.Value = Today
        dtpTo.Value = Today
        lblEntryid.Text = ""
        lblCreditTotals.Text = 0
    End Sub

    'Reprint Invoice Receipt
    Private Sub btnReprint_Click(sender As Object, e As EventArgs) Handles btnReprint.Click
        If (txtSearchReceipt.Text = String.Empty) OrElse (lblEntryid.Text = 0) Then
            MessageBox.Show("Search Customer and receipt number to proceed", "Strawberry System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            txtSearchReceipt.Focus()
            Exit Sub
        Else
            ReprintSalesInvoice()
        End If
    End Sub

    Private Sub ReprintSalesInvoice()
        'Dim receiptNo As Long = Long.Parse(Now().Date.Day & Now().Date.Month & Now().Date.Year & Now().TimeOfDay.Hours & Now().TimeOfDay.Minutes)
        Dim dt As New DataTable
        Dim Description As String, sp As Double = 0, _
            qty As Double = 0, disc As Double, _
            tcost As Double = 0

        Dim Total As Double, AmntPaid As Double, Discount As Double
        Total = Double.Parse(lblReceptTotal.Text.ToString)
        AmntPaid = Double.Parse(lblAmntPaid.Text.ToString)
        Discount = Double.Parse(lblReceptDiscount.Text.ToString)

        'Try
        With dt
            .Columns.Add("description")
            .Columns.Add("qty")
            .Columns.Add("price")
            .Columns.Add("disc")
            .Columns.Add("total")
        End With

        For i = 0 To dgvCreditItemsSold.RowCount - 1
            'If dgvCreditItemsSold.Rows(i).Cells(0).Value = Nothing Then Continue For
            Description = dgvCreditItemsSold.Rows(i).Cells(2).Value.ToString()
            qty = Double.Parse(dgvCreditItemsSold.Rows(i).Cells(3).Value.ToString())
            sp = Double.Parse(dgvCreditItemsSold.Rows(i).Cells(4).Value.ToString())
            disc = Double.Parse(dgvCreditItemsSold.Rows(i).Cells(6).Value.ToString())
            tcost = Double.Parse(dgvCreditItemsSold.Rows(i).Cells(5).Value.ToString())
            dt.Rows.Add(Description, qty, sp, disc, tcost)
        Next

        Dim rptDoc As CrystalDecisions.CrystalReports.Engine.ReportDocument

        rptDoc = New ReprintSaleInvoice

        rptDoc.SetDataSource(dt)
        rptDoc.SetParameterValue("CustomerName", txtCustomerName.Text)
        rptDoc.SetParameterValue("ReceiptDate", lblReceptDate.Text)
        rptDoc.SetParameterValue("InvoiceNo", txtSearchReceipt.Text)
        rptDoc.SetParameterValue("Totals", lblReceptTotal.Text.ToString)
        rptDoc.SetParameterValue("AmntPaid", lblAmntPaid.Text.ToString)
        rptDoc.SetParameterValue("Balance", Double.Parse(Total - (Discount + AmntPaid)))

        rptDoc.SetParameterValue("Discount", lblReceptDiscount.Text.ToString)
        rptDoc.SetParameterValue("SoldBy", lblSoldBy.Text)
        'send directly to printer
        rptDoc.PrintToPrinter(1, False, 0, 0)
        'Catch ex As Exception
        '    'MsgBox(ex.Message)
        'End Try
    End Sub

    Private Sub buttonClose_Click(sender As Object, e As EventArgs) Handles buttonClose.Click
        Me.Close()
    End Sub

    Private Sub txtSearchReceipt_TextChanged(sender As Object, e As EventArgs) Handles txtSearchReceipt.TextChanged

    End Sub
End Class