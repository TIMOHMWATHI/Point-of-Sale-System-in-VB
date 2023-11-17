Imports MySql.Data
Imports MySql.Data.MySqlClient

Public Class frmReprintCashReceipt

    Private Sub txtSearchReceipt_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSearchReceipt.KeyDown
        Dim dfrom As Date = frmSalesReports.dtpFrom.Value.Date
        Dim dto As Date = frmSalesReports.dtpTo.Value.Date
        Dim ReceiptTotals As Double = 0

        If txtSearchReceipt.Text.Trim = String.Empty Then
            'do nothing
        ElseIf e.KeyCode = Keys.Enter Then

            Dim receiptnumber As String = txtSearchReceipt.Text.Trim
            '###################################################################
            'retrieve salesperson and sales date
            Dim datareader As MySqlDataReader

            Dim strsql = "SELECT DATE_FORMAT(sm.receptdatetime, '%Y,%M,%d') AS ReceiptDate," _
                         & " s.fullnames,(sm.amountpaid - sm.discount),sm.amountreturned" _
                         & " FROM salesmaster sm " _
                         & " INNER JOIN staff s ON s.staffid=sm.soldby" _
                         & " WHERE sm.receptno='" & receiptnumber & "'"

            Dim cmd As New MySqlCommand(strsql, Conn)
            cmd.CommandType = CommandType.Text
            datareader = cmd.ExecuteReader
            While datareader.Read
                lblReceptDate.Text = datareader(0).ToString
                lblSoldBy.Text = datareader(1).ToString
                lblAmntPaid.Text = Double.Parse(datareader(2).ToString)
                lblChange.Text = Double.Parse(datareader(3).ToString)
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
                LoadRecept_Data(dgvCashItemsSold)
            Else
                LoadItemsSold(dfrom, dto, dgvCashItemsSold)
            End If
        End If

        For i = 0 To dgvCashItemsSold.RowCount - 1
            ReceiptTotals = ReceiptTotals + Double.Parse(dgvCashItemsSold.Rows(i).Cells(5).Value)
        Next
        lblReceptTotal.Text = ReceiptTotals.ToString
        'MsgBox(i)
    End Sub

    'Loads database table to Datagridview
    Sub LoadRecept_Data(ByVal dgv As DataGridView)
        Dim dfrom As Date = frmSalesReports.dtpFrom.Value.Date
        Dim dto As Date = frmSalesReports.dtpTo.Value.Date
        'Dim customerid As Integer = Integer.Parse(lblEntryid.Text.ToString)

        Dim receiptTotals As Double = 0, ReceiptDiscount As Double = 0

        Dim strsql As String = "SELECT DATE_FORMAT(sm.receptdatetime, '%Y,%M,%d') AS ReceiptDate," _
                               & " sd.receptno, p.Description, sd.itemsellingprice,sd.quantitybought," _
                               & " (sd.itemsellingprice * sd.quantitybought) AS Total,sd.itemdiscount," _
                               & " ((sd.quantitybought * sd.itemsellingprice)- sd.itemdiscount) AS AmntPald," _
                               & " s.fullnames AS 'Sold by' FROM salesdetails sd " _
                               & " INNER JOIN salesmaster sm ON sm.receptno=sd.receptno" _
                               & " INNER JOIN products p ON p.Barcode=sd.itemcode " _
                               & " INNER JOIN staff s ON s.staffid = sm.soldby " _
                               & " WHERE sm.receptno=" & txtSearchReceipt.Text & " AND " _
                               & " DATE(sm.receptdatetime) BETWEEN '" & dfrom & "' AND '" & dto & "'" _
                               & " AND sm.purchasemode='Cash'"
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
                .Columns(3).HeaderText = "Item Selling Price"
                .Columns(4).HeaderText = "Quantity Bought"
                .Columns(5).HeaderText = "Total Amount"
                .Columns(6).HeaderText = "Discount"
                .Columns(7).HeaderText = "Amount Paid"
                .Columns(8).HeaderText = "Sold By"
                .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells
                .Columns(0).DefaultCellStyle.WrapMode = DataGridViewTriState.True
                .Columns(2).DefaultCellStyle.WrapMode = DataGridViewTriState.True
                .Columns(8).DefaultCellStyle.WrapMode = DataGridViewTriState.True
                .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            End With

            For i = 0 To dgvCashItemsSold.RowCount - 1
                receiptTotals += Double.Parse(dgvCashItemsSold.Rows(i).Cells(5).Value)
                ReceiptDiscount += Double.Parse(dgvCashItemsSold.Rows(i).Cells(6).Value)
            Next
            lblReceptTotal.Text = receiptTotals.ToString
            lblReceptDiscount.Text = ReceiptDiscount.ToString
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Sub LoadItemsSold(ByVal dfrom As Date, ByVal dto As Date, ByVal dgv As DataGridView)
        Dim Totals As Double = 0, Discount As Double = 0, _
           AmntToPay As Double = 0, DiscountTotals As Double = 0, _
           SumAmntToPay As Double = 0, ReceptTotals As Double = 0
        Dim i As Integer

        Dim strsql As String = " SELECT DATE_FORMAT(sm.receptdatetime, '%Y,%M,%d') AS ReceiptDate," _
                               & " sd.receptno, p.Description, sd.quantitybought, sd.itemsellingprice, " _
                               & " (sd.itemsellingprice * sd.quantitybought) as Total,sd.itemdiscount, " _
                               & " ((sd.quantitybought * sd.itemsellingprice)- sd.itemdiscount) AS AmntPald," _
                               & " s.fullnames AS 'Sold by' " _
                               & " FROM salesdetails sd INNER JOIN salesmaster sm ON sm.receptno=sd.receptno " _
                               & " INNER JOIN products p ON p.Barcode=sd.itemcode " _
                               & " INNER JOIN staff s ON s.staffid=sm.soldby " _
                               & " WHERE DATE(sm.receptdatetime) BETWEEN '" & dfrom & "' AND '" & dto & "'" _
                               & " AND sm.purchasemode='Cash'ORDER BY sm.fullypaid asc, sm.receptno asc "

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
            .Columns(3).HeaderText = "Item Selling Price"
            .Columns(4).HeaderText = "Quantity Bought"
            .Columns(5).HeaderText = "Total Amount"
            .Columns(6).HeaderText = "Discount"
            .Columns(7).HeaderText = "Amount Paid"
            .Columns(8).HeaderText = "Sold By"
            .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells
            .Columns(0).DefaultCellStyle.WrapMode = DataGridViewTriState.True
            .Columns(2).DefaultCellStyle.WrapMode = DataGridViewTriState.True
            .Columns(8).DefaultCellStyle.WrapMode = DataGridViewTriState.True
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        End With
        'MsgBox(i)
        For i = 0 To dgvCashItemsSold.RowCount - 1
            Totals = Double.Parse(dgvCashItemsSold.Rows(i).Cells(5).Value)
            ReceptTotals += Double.Parse(dgvCashItemsSold.Rows(i).Cells(5).Value)
            Discount = Double.Parse(dgvCashItemsSold.Rows(i).Cells(6).Value)
            DiscountTotals += Double.Parse(dgvCashItemsSold.Rows(i).Cells(6).Value)
        Next
        lblReceptTotal.Text = ReceptTotals.ToString
        lblReceptDiscount.Text = DiscountTotals.ToString
    End Sub

    Private Sub btnReprint_Click(sender As Object, e As EventArgs) Handles btnReprint.Click
        If (txtSearchReceipt.Text = String.Empty) Then
            MessageBox.Show("Search receipt number to proceed", "Strawberry System",
                            MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

            txtSearchReceipt.Focus()
            Exit Sub
        Else
            ReprintSalesReceipt()
        End If
    End Sub

    Private Sub ReprintSalesReceipt()
        'Dim receiptNo As Long = Long.Parse(Now().Date.Day & Now().Date.Month & Now().Date.Year & Now().TimeOfDay.Hours & Now().TimeOfDay.Minutes)
        Dim dt As New DataTable
        Dim Description As String, sp As Double = 0, _
            qty As Double = 0, disc As Double, _
            tcost As Double = 0
        Dim AmountDue As Double = Double.Parse(lblAmntPaid.Text.ToString)
        Dim Change As Double = Double.Parse(lblChange.Text.ToString)
        'Try
        With dt
            .Columns.Add("description")
            .Columns.Add("qty")
            .Columns.Add("price")
            .Columns.Add("disc")
            .Columns.Add("total")
        End With

        For i = 0 To dgvCashItemsSold.RowCount - 1
            Description = dgvCashItemsSold.Rows(i).Cells(2).Value.ToString()
            qty = Double.Parse(dgvCashItemsSold.Rows(i).Cells(4).Value.ToString())
            sp = Double.Parse(dgvCashItemsSold.Rows(i).Cells(3).Value.ToString())
            disc = Double.Parse(dgvCashItemsSold.Rows(i).Cells(6).Value.ToString())
            tcost = Double.Parse(dgvCashItemsSold.Rows(i).Cells(5).Value.ToString())
            dt.Rows.Add(Description, qty, sp, disc, tcost)
        Next

        Dim rptDoc As CrystalDecisions.CrystalReports.Engine.ReportDocument

        rptDoc = New ReprintCashReceipt

        rptDoc.SetDataSource(dt)
        rptDoc.SetParameterValue("ReceiptNo", txtSearchReceipt.Text)
        rptDoc.SetParameterValue("ReceiptDate", lblReceptDate.Text.ToString)
        rptDoc.SetParameterValue("Totals", lblReceptTotal.Text.ToString)
        rptDoc.SetParameterValue("AmntPaid", lblAmntPaid.Text.ToString)
        rptDoc.SetParameterValue("Discount", lblReceptDiscount.Text.ToString)
        rptDoc.SetParameterValue("SoldBy", lblSoldBy.Text.ToString)
        rptDoc.SetParameterValue("CashPaid", (AmountDue + Change))
        rptDoc.SetParameterValue("Change", lblChange.Text.ToString)
        'send directly to printer
        rptDoc.PrintToPrinter(1, False, 0, 0)
        'Catch ex As Exception
        '    'MsgBox(ex.Message)
        'End Try
    End Sub

End Class