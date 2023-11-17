Imports MySql.Data
Imports MySql.Data.MySqlClient

Public Class frmBankDepositReports

    Private Sub btnLoad_Click(sender As Object, e As EventArgs) Handles btnLoad.Click
        Dim dfrom As Date, dto As Date
        dfrom = dtpFrom.Value.Date
        dto = dtpTo.Value.Date
        LoadReport(dfrom, dto, dgvBankDeposits)
    End Sub

    Sub LoadReport(ByVal dfrom As Date, _
                 ByVal dto As Date, _
                 ByVal dgv As DataGridView)

        Dim DepositTotals As Double = 0

        Dim strsql As String = "SELECT bd.entryid,DATE_FORMAT(bd.depositdate,'%Y,%M,%d')," _
                               & " b.bankname,bd.amountdeposited," _
                               & " bd.paymode,bd.memo,s.fullnames AS depositedby FROM bankdeposits bd " _
                               & " INNER JOIN banks b ON b.entryid=bd.bank" _
                               & " INNER JOIN staff s ON s.staffid=bd.registerdby" _
                               & " WHERE DATE(bd.depositdate) BETWEEN '" & dfrom & "' AND '" & dto & "'" _
                               & " ORDER BY bd.depositdate,bd.entryid ASC"
        'MsgBox(strsql)
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
                .Columns(0).HeaderText = "#"
                .Columns(1).HeaderText = "Deposit Date"
                .Columns(2).HeaderText = "Bank"
                .Columns(3).HeaderText = "Amount"
                .Columns(4).HeaderText = "Paymode"
                .Columns(5).HeaderText = "Memo"
                .Columns(6).HeaderText = "Registered by"
                .Columns(0).Visible = False
                .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells
                .Columns(1).DefaultCellStyle.WrapMode = DataGridViewTriState.True
                .Columns(6).DefaultCellStyle.WrapMode = DataGridViewTriState.True
                .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
                '.Columns(5).DefaultCellStyle.Format = "n2"  'code to set decimals places
            End With

            For i = 0 To dgv.RowCount - 1
                DepositTotals += Double.Parse(dgv.Rows(i).Cells(3).Value)
            Next
            lblDepositTotals.Text = FormatCurrency(DepositTotals.ToString, 2)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        printByEpos()
    End Sub

    Private Sub printByEpos()
        'Dim receiptNo As Long = Long.Parse(Now().Date.Day & Now().Date.Month & Now().Date.Year & Now().TimeOfDay.Hours & Now().TimeOfDay.Minutes)
        Dim dt As New DataTable

        Dim DateRegistered As String, _
            Bank As String, _
            Amnt As Double, _
            Paymode As String, _
            Memo As String, _
            Registeredby As String

        With dt
            .Columns.Add("depositdate")
            .Columns.Add("bank")
            .Columns.Add("amount")
            .Columns.Add("paymode")
            .Columns.Add("memo")
            .Columns.Add("registeredby")
        End With

        With dgvBankDeposits
            For i = 0 To dgvBankDeposits.RowCount - 1
                DateRegistered = dgvBankDeposits.Rows(i).Cells(1).Value.ToString
                Bank = dgvBankDeposits.Rows(i).Cells(2).Value.ToString
                Amnt = Double.Parse(dgvBankDeposits.Rows(i).Cells(3).Value.ToString)
                Paymode = dgvBankDeposits.Rows(i).Cells(4).Value.ToString
                Memo = dgvBankDeposits.Rows(i).Cells(5).Value.ToString
                Registeredby = dgvBankDeposits.Rows(i).Cells(6).Value.ToString
                'add rows
                dt.Rows.Add(DateRegistered, Bank, Amnt, Paymode, Memo, Registeredby)
            Next
        End With
        Try
            Dim rptDoc As CrystalDecisions.CrystalReports.Engine.ReportDocument

            rptDoc = New BankDeposits
            rptDoc.SetDataSource(dt)
            rptDoc.SetParameterValue("dfrom", dtpFrom.Value)
            rptDoc.SetParameterValue("dto", dtpTo.Value)
            rptDoc.SetParameterValue("DepositTotals", lblDepositTotals.Text)
            'rptDoc.SetParameterValue("TotalSales", lblBranchInvAmnt.Text)
            ''print by epos 
            'rptDoc.PrintToPrinter(1, False, 0, 0)
            frmPrint.CRPrint.ReportSource = rptDoc
            frmPrint.CRPrint.RefreshReport()
            frmPrint.TopMost = True
            frmPrint.StartPosition = FormStartPosition.CenterScreen
            frmPrint.Show()
        Catch ex As Exception

        End Try
    End Sub

End Class