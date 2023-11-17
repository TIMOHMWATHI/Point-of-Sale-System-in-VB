Imports MySql.Data
Imports MySql.Data.MySqlClient

Public Class frmStockReconciliationReport

    Private Sub btnLoad_Click(sender As Object, e As EventArgs) Handles btnLoad.Click
        Dim dfrom As Date, dto As Date
        dfrom = dtpFrom.Value.Date
        dto = dtpTo.Value.Date
        LoadReport(dfrom, dto, dgvAdjustStock)
    End Sub

    Sub LoadReport(ByVal dfrom As Date, _
                   ByVal dto As Date, _
                   ByVal dgv As DataGridView)
        Dim totalsales As Double = 0

        Dim strsql As String = "SELECT a.entrydate,a.narration,a.changedby" _
                               & " AS ReconciledBy FROM audittrail a " _
                               & " WHERE DATE(a.entrydate) BETWEEN '" & dfrom & "' AND '" & dto & "' " _
                               & " ORDER BY a.entrydate,a.entryno ASC"
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
                .Columns(0).HeaderText = "Reconciliation Date"
                .Columns(1).HeaderText = "Narration"
                .Columns(2).HeaderText = "Reconciled By"
                .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells
                .Columns(1).DefaultCellStyle.WrapMode = DataGridViewTriState.True
                .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
                '.Columns(5).DefaultCellStyle.Format = "n2"  'code to set decimals places
            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

End Class