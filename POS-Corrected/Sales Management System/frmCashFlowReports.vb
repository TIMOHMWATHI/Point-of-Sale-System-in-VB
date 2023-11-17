Imports MySql.Data
Imports MySql.Data.MySqlClient

Public Class frmCashFlowReports

    Private Sub btnLoad_Click(sender As Object, e As EventArgs) Handles btnLoad.Click
        'first clear the grid
        dgvCashflow.Rows.Clear()

        Dim dfrom As Date, dto As Date
        dfrom = dtpFrom.Value.Date
        dto = dtpTo.Value.Date
        Dim variance As Double

        LoadCashFlowReport(dfrom, dto, dgvCashflow)

        'loop through the grid
        For i = 0 To dgvCashflow.RowCount - 1

            variance = Double.Parse(dgvCashflow.Rows(i).Cells(3).Value.ToString)

            'compute Totals on labels
            variance = Double.Parse(dgvCashflow.Rows(i).Cells(3).Value)

        Next
        lblVariaceTotals.Text = "Kshs" & " " & variance.ToString
    End Sub

    Private Sub LoadCashFlowReport(ByVal dfrom As Date, _
                                    ByVal dto As Date, _
                                    ByVal Grid As DataGridView)

        Dim datareader As MySqlDataReader
        Dim FlowDate As Date, CashIn As Double, CashOut As Double, _
            Variance As Double, Items As String, Registeredby As String

        Dim strsql As String = "SELECT DATE_FORMAT(cfm.transactiondate, '%Y,%M,%d') AS FlowDate," _
                                     & " cfm.cashin,cfm.cashout,cfm.cashvariance," _
                                     & " GROUP_CONCAT(DISTINCT cd.description, ' _ ' ,cd.effect, ' _ ' ,cd.cost" _
                                     & " ORDER BY cd.masterno SEPARATOR '  ;  ') AS 'Flow Items',s.fullnames  AS registerdby" _
                                     & " FROM cashflowmaster cfm INNER JOIN cashflowdetails cd ON cd.masterno=cfm.entryid" _
                                     & " INNER JOIN staff s ON s.staffid=cfm.registeredby" _
                                     & " WHERE DATE(cfm.transactiondate) BETWEEN '" & dfrom & "' AND '" & dto & "' " _
                                     & " GROUP BY DATE(cfm.transactiondate) ASC"
        Try
            Dim cmd As New MySqlCommand(strsql, Conn)
            cmd.CommandType = CommandType.Text
            datareader = cmd.ExecuteReader
            While datareader.Read
                FlowDate = datareader(0).ToString
                CashIn = Double.Parse(datareader(1).ToString)
                CashOut = Double.Parse(datareader(2).ToString)
                Variance = Double.Parse(datareader(3).ToString)
                Items = datareader(4).ToString
                Registeredby = datareader(5).ToString

                'add to grid
                dgvCashflow.Rows.Add(FlowDate, CashIn, CashOut, Variance, Items, Registeredby)
                '+++++++++++++
            End While
            datareader.Dispose()
            closeconn()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class