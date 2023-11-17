Imports MySql.Data
Imports MySql.Data.MySqlClient

Public Class frmExpensesReport

    Private Sub btnLoad_Click(sender As Object, e As EventArgs) Handles btnLoad.Click
        Dim dfrom As Date, dto As Date
        dfrom = dtpFrom.Value.Date
        dto = dtpTo.Value.Date
        'load data
        LoadExpenseReport(dfrom, dto, dgvExpenseReport)
    End Sub

    Sub LoadExpenseReport(ByVal dfrom As Date, _
                          ByVal dto As Date, _
                          ByVal dgv As DataGridView)

        Dim totalExpenses As Double = 0

        Dim strsql As String = "SELECT DATE_FORMAT(em.expensedate,'%Y %M %d')," _
                               & " GROUP_CONCAT(DISTINCT ed.expensedesc, ' = Shs. ' ,ed.expenseamount" _
                               & " ORDER BY ed.masterno ASC SEPARATOR '  ;  \r\n' ) AS ExpenseDetals," _
                               & " em.total,s.fullnames AS 'Servedby' FROM expensemaster em" _
                               & " INNER JOIN expensedetails ed ON ed.masterno=em.entryid" _
                               & " INNER JOIN staff s ON s.staffid=em.servedby" _
                               & " WHERE DATE(em.expensedate) BETWEEN '" & dfrom & "' AND '" & dto & "'" _
                               & " GROUP BY em.entryid ORDER BY DATE(em.expensedate) ASC "
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
                .Columns(0).HeaderText = "Expense Date"
                .Columns(1).HeaderText = "Details"
                .Columns(2).HeaderText = "Expense Totals"
                .Columns(3).HeaderText = "Served By"
                .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells
                .Columns(0).DefaultCellStyle.WrapMode = DataGridViewTriState.True
                .Columns(1).DefaultCellStyle.WrapMode = DataGridViewTriState.True
                .Columns(3).DefaultCellStyle.WrapMode = DataGridViewTriState.True
                .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
                .Columns(0).Width = 100
                .Columns(1).Width = 450
                .Columns(2).Width = 100
                .Columns(3).Width = 150
            End With
            For i = 0 To dgv.RowCount - 1
                totalExpenses += Double.Parse(dgv.Rows(i).Cells(2).Value)
            Next
            lblTotalCost.Text = "KShs" & " " & totalExpenses.ToString
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        printStatement()
    End Sub

    Private Sub printStatement()

        'Dim receiptNo As Long = Long.Parse(Now().Date.Day & Now().Date.Month & Now().Date.Year & Now().TimeOfDay.Hours & Now().TimeOfDay.Minutes)
        Dim dt As New DataTable
        Dim ExpenseDate As String, Description As String, _
             Cost As Double, ServedBy As String

        With dt
            .Columns.Add("expensedate")
            .Columns.Add("description")
            .Columns.Add("cost")
            .Columns.Add("servedby")
        End With

        With dgvExpenseReport
            For i = 0 To dgvExpenseReport.RowCount - 1
                ExpenseDate = dgvExpenseReport.Rows(i).Cells(0).Value.ToString
                Description = dgvExpenseReport.Rows(i).Cells(1).Value.ToString
                Cost = Double.Parse(dgvExpenseReport.Rows(i).Cells(2).Value.ToString)
                ServedBy = dgvExpenseReport.Rows(i).Cells(3).Value.ToString

                'add rows
                dt.Rows.Add(ExpenseDate, Description, Cost, ServedBy)
            Next
        End With
        Try
            Dim rptDoc As CrystalDecisions.CrystalReports.Engine.ReportDocument

            rptDoc = New ExpenseReport
            rptDoc.SetDataSource(dt)
            rptDoc.SetParameterValue("dFrom", dtpFrom.Value)
            rptDoc.SetParameterValue("dTo", dtpTo.Value)
            rptDoc.SetParameterValue("TotalCost", lblTotalCost.Text)
            frmPrint.CRPrint.ReportSource = rptDoc
            frmPrint.CRPrint.RefreshReport()
            frmPrint.TopMost = True
            frmPrint.StartPosition = FormStartPosition.CenterScreen
            frmPrint.Show()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class