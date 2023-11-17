Imports MySql.Data
Imports MySql.Data.MySqlClient

Public Class frmInvoiceReports

    Private Sub frmInvoiceReports_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'load invoices
        LoadInvoices(txtSearchInvoice)
    End Sub

    Private Sub btnLoad_Click(sender As Object, e As EventArgs) Handles btnLoad.Click
        Dim dfrom As Date, dto As Date
        dfrom = dtpFrom.Value.Date
        dto = dtpTo.Value.Date
        'MsgBox(dtpFrom.Value.Date)
        InvoiceDeliveries(dfrom, dto, dgvInvDeliveries)
    End Sub

    Private Sub txtSearch_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSearch.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim dfrom As Date, dto As Date, itemname As String
            dfrom = dtpFrom.Value.Date
            dto = dtpTo.Value.Date

            If txtSearch.Text.Trim.Length > 0 Then
                itemname = txtSearch.Text.Trim
                SearchInvoice(dfrom, dto, dgvInvDeliveries, itemname, lblTotalAmntPaid)
            Else
                InvoiceDeliveries(dfrom, dto, dgvInvDeliveries)
            End If
        End If
    End Sub

    Sub InvoiceDeliveries(ByVal dfrom As Date, ByVal dto As Date, ByVal dgv As DataGridView)

        Dim TotalAmntPaid As Double = 0

        Dim strsql As String = "SELECT sb.branchname,id.invoiceno,id.itemid,p.Description,id.qty,id.unitprice," _
                               & " id.total,s.fullnames AS 'Issued by',im.invoicedate FROM invoicedetails id" _
                               & " INNER JOIN products p ON p.Barcode=id.itemid" _
                               & " INNER JOIN invoicemaster im ON im.entryno=id.masterno" _
                               & " INNER JOIN staff s ON s.staffid=im.servedby" _
                               & " INNER JOIN salesbranches sb ON sb.entryno=im.branchid" _
                               & " WHERE im.deliverystatus!=1 AND DATE(im.invoicedate)" _
                               & " BETWEEN '" & dfrom & "' AND '" & dto & "' ORDER BY " _
                               & " sb.branchname,id.itemid, p.Description,im.invoicedate ASC"
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
                .Columns(0).HeaderText = "Invoiced To"
                .Columns(1).HeaderText = "Invoice No"
                .Columns(2).HeaderText = "Barcode"
                .Columns(3).HeaderText = "Description"
                .Columns(4).HeaderText = "Qty"
                .Columns(5).HeaderText = "Unit Price"
                .Columns(6).HeaderText = "Total"
                .Columns(7).HeaderText = "Issued By"
                .Columns(8).HeaderText = "Invoice Date"
                .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells
                .Columns(1).DefaultCellStyle.WrapMode = DataGridViewTriState.True
                .Columns(3).DefaultCellStyle.WrapMode = DataGridViewTriState.True
                .Columns(7).DefaultCellStyle.WrapMode = DataGridViewTriState.True
                .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
                .Columns(6).DefaultCellStyle.Format = "n2"  'code to set decimals places
            End With
            For i = 0 To dgv.RowCount - 1
                TotalAmntPaid = TotalAmntPaid + Double.Parse(dgv.Rows(i).Cells(6).Value)
            Next
            lblTotalAmntPaid.Text = FormatCurrency(TotalAmntPaid.ToString, 2)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    'Loads database table to Datagridview
    Public Sub SearchInv_Deliveries(ByVal dfrom As Date, _
                                    ByVal dto As Date, _
                                    ByVal dgv As DataGridView, _
                                    ByVal itemname As String, _
                                    ByVal lbl As Label)

        Dim TotalAmntPaid As Double = 0

        Dim strsql As String = "SELECT sb.branchname,id.invoiceno,id.itemid,p.Description,id.qty," _
                               & " id.unitprice,id.total,s.fullnames AS 'Issued by',im.invoicedate FROM invoicedetails id" _
                                     & " INNER JOIN products p ON p.Barcode=id.itemid" _
                                     & " INNER JOIN invoicemaster im ON im.entryno=id.masterno" _
                                     & " INNER JOIN staff s ON s.staffid=im.servedby" _
                                     & " INNER JOIN salesbranches sb ON sb.entryno=im.branchid" _
                                     & " WHERE im.deliverystatus!=1 AND  " _
                                     & " p.Description LIKE '" & "%" & itemname & "%" & "' OR " _
                                     & " id.invoiceno LIKE '" & "%" & itemname & "%" & "' OR " _
                                     & " sb.branchname LIKE '" & "%" & itemname & "%" & "' OR " _
                                     & " id.itemid LIKE '" & "%" & itemname & "%" & "' ORDER BY " _
                                     & " sb.branchname,id.itemid, p.Description,im.invoicedate ASC "

        Dim cmd As New MySqlCommand(strsql, Conn)
        cmd.CommandType = CommandType.Text
        Dim adapter As New MySqlDataAdapter(cmd)
        Dim table As New DataTable
        adapter.Fill(table)
        'Display the data.
        dgv.DataSource = table
        closeconn()
        Try
            With dgv
                .Columns(0).HeaderText = "Invoiced To"
                .Columns(1).HeaderText = "Invoice No"
                .Columns(2).HeaderText = "Barcode"
                .Columns(3).HeaderText = "Description"
                .Columns(4).HeaderText = "Qty"
                .Columns(5).HeaderText = "Unit Price"
                .Columns(6).HeaderText = "Total"
                .Columns(7).HeaderText = "Issued By"
                .Columns(8).HeaderText = "Invoice Date"
                .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells
                .Columns(1).DefaultCellStyle.WrapMode = DataGridViewTriState.True
                .Columns(3).DefaultCellStyle.WrapMode = DataGridViewTriState.True
                .Columns(7).DefaultCellStyle.WrapMode = DataGridViewTriState.True
                .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
                .Columns(6).DefaultCellStyle.Format = "n2"  'code to set decimals places
            End With
            For i = 0 To dgv.RowCount - 1
                TotalAmntPaid = TotalAmntPaid + Double.Parse(dgv.Rows(i).Cells(6).Value)
            Next
            lblTotalAmntPaid.Text = FormatCurrency(TotalAmntPaid.ToString, 2)
        Catch ex As Exception
            ' MessageBox.Show(ex.Message)
        End Try
    End Sub

    'Loads database table to Datagridview
    Public Sub SearchInvoice(ByVal dfrom As Date, _
                                    ByVal dto As Date, _
                                    ByVal dgv As DataGridView, _
                                    ByVal itemname As String, _
                                    ByVal lbl As Label)

        Dim TotalAmntPaid As Double = 0

        Dim strsql As String = "SELECT sb.branchname,id.invoiceno,id.itemid,p.Description,id.qty," _
                               & " id.unitprice,id.total,s.fullnames AS 'Issued by',im.invoicedate FROM invoicedetails id" _
                                     & " INNER JOIN products p ON p.Barcode=id.itemid" _
                                     & " INNER JOIN invoicemaster im ON im.entryno=id.masterno" _
                                     & " INNER JOIN staff s ON s.staffid=im.servedby" _
                                     & " INNER JOIN salesbranches sb ON sb.entryno=im.branchid" _
                                     & " WHERE im.deliverystatus!=1 AND  id.invoiceno= '" & itemname & "' " _
                                     & " ORDER BY sb.branchname,id.itemid, p.Description,im.invoicedate ASC "

        Dim cmd As New MySqlCommand(strsql, Conn)
        cmd.CommandType = CommandType.Text
        Dim adapter As New MySqlDataAdapter(cmd)
        Dim table As New DataTable
        adapter.Fill(table)
        'Display the data.
        dgv.DataSource = table
        closeconn()
        Try
            With dgv
                .Columns(0).HeaderText = "Invoiced To"
                .Columns(1).HeaderText = "Invoice No"
                .Columns(2).HeaderText = "Barcode"
                .Columns(3).HeaderText = "Description"
                .Columns(4).HeaderText = "Qty"
                .Columns(5).HeaderText = "Unit Price"
                .Columns(6).HeaderText = "Total"
                .Columns(7).HeaderText = "Issued By"
                .Columns(8).HeaderText = "Invoice Date"
                .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells
                .Columns(1).DefaultCellStyle.WrapMode = DataGridViewTriState.True
                .Columns(3).DefaultCellStyle.WrapMode = DataGridViewTriState.True
                .Columns(7).DefaultCellStyle.WrapMode = DataGridViewTriState.True
                .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
                .Columns(6).DefaultCellStyle.Format = "n2"  'code to set decimals places
            End With
            For i = 0 To dgv.RowCount - 1
                TotalAmntPaid = TotalAmntPaid + Double.Parse(dgv.Rows(i).Cells(6).Value)
            Next
            lblTotalAmntPaid.Text = FormatCurrency(TotalAmntPaid.ToString, 2)
        Catch ex As Exception
            ' MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Dim dfrom As Date, dto As Date
        dfrom = dtpFrom.Value.Date
        dto = dtpTo.Value.Date
        'MsgBox(dtpFrom.Value.Date)
        InvoiceDeliveries(dfrom, dto, dgvInvDeliveries)

        'clear textbox
        lblbranch.Visible = False
        lblInuser.Visible = False
        txtSearch.Clear()
        txtSearchInvoice.Clear()
        lblBranchName.Text = "."
        lblInvoicedBy.Text = "."
        lblBranchInvAmnt.Text = "."
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        Dim dfrom As Date, dto As Date, itemname As String
        dfrom = dtpFrom.Value.Date
        dto = dtpTo.Value.Date

        If txtSearch.Text.Trim.Length > 0 Then
            itemname = txtSearch.Text.Trim
            SearchInv_Deliveries(dfrom, dto, dgvInvDeliveries, itemname, lblTotalAmntPaid)
            'btnLoad.PerformClick()
        Else
            InvoiceDeliveries(dfrom, dto, dgvInvDeliveries)
        End If
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        If (txtSearchInvoice.Text.Trim = String.Empty) OrElse (lblBranchName.Text = "") Then
            MessageBox.Show("Invalid Invoice Number", "Strawberry POS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        Else
            printStatement()
        End If
    End Sub


    Private Sub btnPrintEpos_Click(sender As Object, e As EventArgs) Handles btnPrintEpos.Click
        If (txtSearchInvoice.Text.Trim = String.Empty) OrElse (lblBranchName.Text = "") Then
            MessageBox.Show("Invalid Invoice Number", "Strawberry POS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        Else
            printByEpos()
        End If
    End Sub

    Private Sub printStatement()
        'Dim receiptNo As Long = Long.Parse(Now().Date.Day & Now().Date.Month & Now().Date.Year & Now().TimeOfDay.Hours & Now().TimeOfDay.Minutes)
        Dim dt As New DataTable

        Dim Description As String, _
            Qty As Integer, _
            UnitPrice As Double, _
            TotalAmnt As Double

        With dt
            .Columns.Add("description")
            .Columns.Add("qty")
            .Columns.Add("unitprice")
            .Columns.Add("total")
        End With

        With dgvInvDeliveries
            For i = 0 To dgvInvDeliveries.RowCount - 1
                Description = dgvInvDeliveries.Rows(i).Cells(3).Value.ToString
                Qty = Integer.Parse(dgvInvDeliveries.Rows(i).Cells(4).Value.ToString)
                UnitPrice = Double.Parse(dgvInvDeliveries.Rows(i).Cells(5).Value.ToString)
                TotalAmnt = Double.Parse(dgvInvDeliveries.Rows(i).Cells(6).Value.ToString)
                'add rows
                dt.Rows.Add(Description, Qty, UnitPrice, TotalAmnt)
            Next
        End With
        Try
            Dim rptDoc As CrystalDecisions.CrystalReports.Engine.ReportDocument

            rptDoc = New CRInvoices
            rptDoc.SetDataSource(dt)
            rptDoc.SetParameterValue("BranchName", lblBranchName.Text)
            rptDoc.SetParameterValue("IssuedBy", lblInvoicedBy.Text)
            rptDoc.SetParameterValue("InvoiceNo", txtSearchInvoice.Text)
            rptDoc.SetParameterValue("TotalSales", lblBranchInvAmnt.Text)
            'rptDoc.SetParameterValue("Profit", lblGrossProfit.Text)
            'rptDoc.SetParameterValue("Discount", lblDiscounts.Text)
            frmPrint.CRPrint.ReportSource = rptDoc
            frmPrint.CRPrint.RefreshReport()
            frmPrint.TopMost = True
            frmPrint.StartPosition = FormStartPosition.CenterScreen
            frmPrint.Show()
        Catch ex As Exception

        End Try
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
            .Columns.Add("unitprice")
            .Columns.Add("total")
        End With

        With dgvInvDeliveries
            For i = 0 To dgvInvDeliveries.RowCount - 1
                Description = dgvInvDeliveries.Rows(i).Cells(3).Value.ToString
                Qty = Integer.Parse(dgvInvDeliveries.Rows(i).Cells(4).Value.ToString)
                UnitPrice = Double.Parse(dgvInvDeliveries.Rows(i).Cells(5).Value.ToString)
                TotalAmnt = Double.Parse(dgvInvDeliveries.Rows(i).Cells(6).Value.ToString)
                'add rows
                dt.Rows.Add(Description, Qty, UnitPrice, TotalAmnt)
            Next
        End With
        Try
            Dim rptDoc As CrystalDecisions.CrystalReports.Engine.ReportDocument

            rptDoc = New CRInvoiceEposprint
            rptDoc.SetDataSource(dt)
            rptDoc.SetParameterValue("BranchName", lblBranchName.Text)
            rptDoc.SetParameterValue("IssuedBy", lblInvoicedBy.Text)
            rptDoc.SetParameterValue("InvoiceNo", txtSearchInvoice.Text)
            rptDoc.SetParameterValue("TotalSales", lblBranchInvAmnt.Text)
            'print by epos 
            rptDoc.PrintToPrinter(1, False, 0, 0)
        Catch ex As Exception

        End Try
    End Sub


    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub txtSearchInvoice_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSearchInvoice.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim InvoiceNo As String
            Dim dfrom As Date, dto As Date, TotalAmntPaid As Double = 0
            dfrom = dtpFrom.Value.Date
            dto = dtpTo.Value.Date

            'input validation on txtItemSearch
            If (txtSearchInvoice.Text = String.Empty) Then
                txtSearchInvoice.Focus()
                Exit Sub
            Else
                InvoiceNo = txtSearchInvoice.Text.Trim
            End If
            '#################################################################################################
            'AUTO SEARCH FARMER NAMES
            Dim datareader As MySqlDataReader
            If e.KeyCode = Keys.Enter Then    'work if enter button is clicked
                Try
                    Dim strsql = "SELECT sb.branchname,s.fullnames FROM invoicemaster im" _
                                 & " INNER JOIN staff s ON s.staffid=im.servedby" _
                                 & " INNER JOIN salesbranches sb ON sb.entryno=im.branchid" _
                                 & " WHERE im.invoiceno='" & InvoiceNo & "'"

                    Dim cmd As New MySqlCommand(strsql, Conn)
                    cmd.CommandType = CommandType.Text
                    datareader = cmd.ExecuteReader
                    While datareader.Read
                        lblBranchName.Text = datareader(0).ToString
                        lblInvoicedBy.Text = datareader(1).ToString
                        lblbranch.Visible = True
                        lblInuser.Visible = True
                        '########
                        InvoicesAutosearch(InvoiceNo)
                    End While
                    datareader.Dispose()
                    closeconn()
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                End Try
            End If
            '#################
            For i = 0 To dgvInvDeliveries.RowCount - 1
                TotalAmntPaid = TotalAmntPaid + Double.Parse(dgvInvDeliveries.Rows(i).Cells(6).Value)
            Next
            lblBranchInvAmnt.Text = FormatCurrency(TotalAmntPaid.ToString, 2)
            lblTotalAmntPaid.Text = FormatCurrency(TotalAmntPaid.ToString, 2)
        End If
    End Sub


    'Loads Autosearch products
    Sub InvoicesAutosearch(ByVal InvoiceNo As String)

        Dim strsql = "SELECT sb.branchname,id.invoiceno,id.itemid,p.Description,id.qty," _
                                    & " id.unitprice,id.total,s.fullnames AS 'Issued by',im.invoicedate FROM invoicedetails id" _
                                    & " INNER JOIN products p ON p.Barcode=id.itemid" _
                                    & " INNER JOIN invoicemaster im ON im.entryno=id.masterno" _
                                    & " INNER JOIN staff s ON s.staffid=im.servedby" _
                                    & " INNER JOIN salesbranches sb ON sb.entryno=im.branchid" _
                                    & " WHERE im.deliverystatus!=1 AND " _
                                    & " id.invoiceno = '" & InvoiceNo & "' ORDER BY " _
                                    & " sb.branchname,id.itemid, p.Description,im.invoicedate ASC "
        Try
            Dim cmd As New MySqlCommand(strsql, Conn)
            cmd.CommandType = CommandType.Text
            Dim adapter As New MySqlDataAdapter(cmd)
            Dim table As New DataTable
            adapter.Fill(table)
            'Display the data.
            dgvInvDeliveries.DataSource = table
            closeconn()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub txtSearchInvoice_TextChanged(sender As Object, e As EventArgs) Handles txtSearchInvoice.TextChanged

    End Sub
End Class