Imports MySql.Data
Imports MySql.Data.MySqlClient
Public Class frmCreditRunningTotals

    Private Sub frmCreditRunningTotals_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'LoadFarmerfullnames(txtCreditorName)
        LoadCreditorNames(txtCreditorName)
    End Sub

    Private Sub txtCreditorName_KeyDown(sender As Object, e As KeyEventArgs) Handles txtCreditorName.KeyDown
        'input validation on txtfarmernames
        If e.KeyCode = Keys.Enter Then    'work if enter button is clicked
            'creditorname = txtCreditorName.Text.Trim
            If (txtCreditorName.Text = String.Empty) Then
                txtCreditorName.Focus()
                Exit Sub
            End If

            Dim datareader As MySqlDataReader
            'Try
            Dim strsql = "SELECT fullname,phone,supplier_id FROM suppliers" _
                         & " WHERE (fullname='" & txtCreditorName.Text.Trim & "') " _
                         & " AND relationtype='C' AND deleted= 0 "

            'Dim strsql = "SELECT fullnames,telephone,entryid FROM farmerregistry" _
            '             & " WHERE deleted= 0 AND (fullnames='" & txtCreditorName.Text.Trim & "') "

            Dim cmd As New MySqlCommand(strsql, Conn)
            cmd.CommandType = CommandType.Text
            datareader = cmd.ExecuteReader
            While datareader.Read
                txtCreditorName.Text = datareader(0).ToString
                lblEntryid.Text = datareader(2).ToString
            End While
            closeconn()
        End If
        btnLoadGrid.PerformClick()
    End Sub

    Private Sub txtCreditorName_TextChanged(sender As Object, e As EventArgs) Handles txtCreditorName.TextChanged

    End Sub

    Private Sub btnLoadGrid_Click(sender As Object, e As EventArgs) Handles btnLoadGrid.Click
        Dim totalunpaidcredit As Double = 0
        Dim customerId As Integer
        Dim strsql As String = "sp_managecreditaccounts"
        customerId = Integer.Parse(lblEntryid.Text)
        Dim cmd As New MySqlCommand(strsql, Conn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@incustomerid", customerId)
        cmd.Parameters.AddWithValue("@intransactiondate", Today())
        cmd.Parameters.AddWithValue("@increditamount", 0)
        cmd.Parameters.AddWithValue("@inamountpaid", 0)
        cmd.Parameters.AddWithValue("@indiscount", 0)
        cmd.Parameters.AddWithValue("@indeterminer", 4)
        Dim adapter As New MySqlDataAdapter(cmd)
        Dim table As New DataTable
        adapter.Fill(table)
        'Display the data.
        dgvCreditStatement.DataSource = table
        closeconn()

        With dgvCreditStatement
            .Columns(0).HeaderText = "Tran. Date"
            .Columns(1).HeaderText = "Receipt No:"
            .Columns(2).HeaderText = "Purchases"
            .Columns(3).HeaderText = "Amnt Paid"
            .Columns(4).HeaderText = "Discount"
            .Columns(5).HeaderText = "Closing Balance"
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            .Columns(1).DefaultCellStyle.WrapMode = DataGridViewTriState.True
            .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells
        End With
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class