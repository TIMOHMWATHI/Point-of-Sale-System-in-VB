Imports MySql.Data
Imports MySql.Data.MySqlClient

Public Class frmCashRunningTotalsRpt

    Private Sub btnLoad_Click(sender As Object, e As EventArgs) Handles btnLoad.Click
        Dim dfrom As Date, dto As Date
        dfrom = dtpFrom.Value.Date
        dto = dtpTo.Value.Date
        'load data
        LoadCashFlowReport(dfrom, dto, dgvCashFlowReports)
    End Sub

    Sub LoadCashFlowReport(ByVal dfrom As Date, _
                        ByVal dto As Date, _
                        ByVal dgv As DataGridView)

        Dim totalGenerators As Double = 0
        Dim totalDeductors As Double = 0
        Dim totalVariance As Double = 0
        Dim strsql As String = "SELECT DATE_FORMAT(rt.transactiondate,'%Y %M %D') AS 'Date'," _
                               & " rt.openingbal,rt.incomegenerators," _
                               & " rt.dailyexpenses,rt.amntvariance,s.fullnames FROM cashrunningtotals rt" _
                               & " INNER JOIN staff s ON s.staffid=rt.registeredby WHERE s.deleted!=1" _
                               & " AND  DATE(rt.transactiondate) BETWEEN '" & dfrom & "' AND '" & dto & "' "
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
                .Columns(0).HeaderText = "Date"
                .Columns(1).HeaderText = "Opening Balance"
                .Columns(2).HeaderText = "Cash Generator"
                .Columns(3).HeaderText = "Expenses"
                .Columns(4).HeaderText = "Variance"
                .Columns(5).HeaderText = "Registered by"
                '.Columns(0).Visible = False
                .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells
                .Columns(0).DefaultCellStyle.WrapMode = DataGridViewTriState.True
                .Columns(5).DefaultCellStyle.WrapMode = DataGridViewTriState.True
                .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            End With
            For i = 0 To dgv.RowCount - 1
                totalGenerators = totalGenerators + Double.Parse(dgv.Rows(i).Cells(2).Value)
                totalDeductors = totalDeductors + Double.Parse(dgv.Rows(i).Cells(3).Value)
                totalVariance = totalVariance + Double.Parse(dgv.Rows(i).Cells(4).Value)
            Next
            lblIncomeGenerators.Text = "KShs" & " " & totalGenerators.ToString
            lblIncomeDeducts.Text = "KShs" & " " & totalDeductors.ToString
            lblVarianceTotal.Text = "KShs" & " " & totalVariance.ToString
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click

    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class