Imports MySql.Data
Imports MySql.Data.MySqlClient

Public Class frmSaleReturnsReport

    Private Sub frmSaleReturnsReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
      
    End Sub

    Sub LoadSaleReturnsReport(ByVal dfrom As Date, ByVal dto As Date)

        Dim strsql As String = "SELECT srn.barcode,p.Description,srn.qtyreturned,srn.narration," _
                               & " srn.datereturned,srn.receivedby FROM salereturnsnarration srn" _
                               & " INNER JOIN products p ON p.Barcode=srn.barcode" _
                               & " WHERE DATE(srn.datereturned) BETWEEN '" & dfrom & "' AND '" & dto & "'" _
                               & " ORDER BY srn.datereturned ASC"
        Try
            Dim cmd As New MySqlCommand(strsql, Conn)
            cmd.CommandType = CommandType.Text
            Dim adapter As New MySqlDataAdapter(cmd)
            Dim table As New DataTable
            adapter.Fill(table)
            'Display the data.
            dgvReturnsNarration.DataSource = table
            closeconn()
            With dgvReturnsNarration
                .Columns(0).HeaderText = "Barcode"
                .Columns(1).HeaderText = "Description"
                .Columns(2).HeaderText = "Qty"
                .Columns(3).HeaderText = "Narration"
                .Columns(4).HeaderText = "Date Returned"
                .Columns(5).HeaderText = "Received By"
                .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells
                .Columns(1).DefaultCellStyle.WrapMode = DataGridViewTriState.True
                .Columns(3).DefaultCellStyle.WrapMode = DataGridViewTriState.True
                .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnLoad_Click(sender As Object, e As EventArgs) Handles btnLoad.Click
        Dim dfrom As Date, dto As Date
        dfrom = dtpFrom.Value.Date
        dto = dtpTo.Value.Date
        LoadSaleReturnsReport(dfrom, dto)
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        dtpFrom.Value = Today()
        dtpTo.Value = Today()
        btnLoad.PerformClick()
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        Dim dfrom As Date, dto As Date
        dfrom = dtpFrom.Value.Date
        dto = dtpTo.Value.Date
        Dim itemname As String
        If txtSearch.Text.Trim.Length > 0 Then
            itemname = txtSearch.Text.Trim
            Autosearch(itemname, dgvReturnsNarration)
        Else
            LoadSaleReturnsReport(dfrom, dto)
        End If
    End Sub

    Private Sub Autosearch(ByVal itemname As String, ByVal gview As DataGridView)
        Dim strsql As String = ""

        itemname = "%" & itemname & "%"

        strsql = "SELECT srn.barcode,p.Description,srn.qtyreturned,srn.narration," _
                 & " srn.datereturned,srn.receivedby FROM salereturnsnarration srn" _
                 & " INNER JOIN products p ON p.Barcode=srn.barcode WHERE srn.barcode" _
                 & " LIKE '" & "%" & itemname & "%" & "' OR" _
                 & " p.Description LIKE '" & "%" & itemname & "%" & "'"

        Dim cmd As New MySqlCommand(strsql, Conn)
        cmd.CommandType = CommandType.Text
        Dim adapter As New MySqlDataAdapter(cmd)
        Dim table As New DataTable

        adapter.Fill(table)
        'Display the data.
        gview.DataSource = table
        closeconn()

    End Sub

End Class