Imports MySql.Data
Imports MySql.Data.MySqlClient
Public Class frmSalesReturns

    Sub LoadSalesReturns(ByVal dfrom As Date, ByVal dto As Date, _
                         ByVal descr As String, ByVal dgv As DataGridView)

        Dim strsql As String = "SELECT sd.receptno,p.Description,p.Barcode," _
                               & " CASE WHEN p.vatrated=0 then 'NO' ELSE 'YES' END AS 'VAT Rated'," _
                               & " sd.quantitybought,sd.itemsellingprice,sd.totalcost," _
                               & " sd.vatamount,sm.receptdatetime,sd.entryid,sd.itembuyingprice FROM products p" _
                               & " INNER JOIN salesdetails sd ON sd.itemcode=p.Barcode" _
                               & " INNER JOIN salesmaster sm ON sm.receptno=sd.receptno WHERE" _
                               & " p.Description='" & descr & "' AND DATE(sm.receptdatetime)" _
                               & " BETWEEN '" & dfrom & "' AND '" & dto & "' AND sd.quantitybought > 0 " _
                               & " ORDER BY sm.receptno ASC"
        ''Try
        Dim cmd As New MySqlCommand(strsql, Conn)
        cmd.CommandType = CommandType.Text
        Dim adapter As New MySqlDataAdapter(cmd)
        Dim table As New DataTable
        adapter.Fill(table)
        'Display the data.
        dgv.DataSource = table
        closeconn()
        With dgv
            .Columns(0).HeaderText = "Receipt No"
            .Columns(1).HeaderText = "Description"
            .Columns(2).HeaderText = "Barcode"
            .Columns(3).HeaderText = "VAT Rated"
            .Columns(4).HeaderText = "Qty Bought"
            .Columns(5).HeaderText = "Selling price"
            .Columns(6).HeaderText = "Total Cost"
            .Columns(7).HeaderText = "VAT Amnt"
            .Columns(8).HeaderText = "Date And Time"
            .Columns(9).HeaderText = "File No"
            .Columns(10).HeaderText = "Item BP"
            .Columns(9).Visible = False
            .Columns(10).Visible = False
            .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells
            .Columns(1).DefaultCellStyle.WrapMode = DataGridViewTriState.True
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        End With


        'Catch ex As Exception
        '    MessageBox.Show(ex.Message)
        'End Try
    End Sub

    Private Sub frmSalesReturns_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ''load combo
        'Dim strsql As String = "select Barcode,Description from products order by Description asc"
        'loadcombo(cboProducts, strsql, "Description", "Barcode")

        'load autosearch
        Loadproducts(txtSearch)
    End Sub

    Private Sub btnLoad_Click_1(sender As Object, e As EventArgs) Handles btnLoad.Click
        Dim desc As String
        If txtSearch.Text.Trim = String.Empty Then
            MessageBox.Show("Select Product", "Strawberry POS", MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtSearch.Focus()
            Exit Sub
        Else
            desc = txtSearch.Text.Trim
        End If

        Dim dfrom As Date = dtpStartDate.Value.Date
        Dim dto As Date = dtpEndDate.Value.Date
        'MsgBox(Customerid)
        LoadSalesReturns(dfrom, dto, desc, dgvSalesReturns)
    End Sub

    Private Sub ReceiveReturnsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReceiveReturnsToolStripMenuItem.Click
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        'ASSIGN USER PRIVILEDGES CODE
        If Not Authorized(userid, TaskID.receive_returns) Then Exit Sub
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        Dim receipno As Long
        Dim itemName As String, barcode As String
        receipno = Long.Parse(dgvSalesReturns.CurrentRow.Cells(0).Value.ToString)
        itemName = dgvSalesReturns.CurrentRow.Cells(1).Value.ToString
        barcode = dgvSalesReturns.CurrentRow.Cells(2).Value.ToString
        With frmReceiveReturns
            .lblDescription.Text = itemName
            .lblReceiptNo.Text = receipno.ToString
            .lblBarcode.Text = barcode
            .lblVATstatus.Text = dgvSalesReturns.CurrentRow.Cells(3).Value.ToString
            .lblFileNo.Text = dgvSalesReturns.CurrentRow.Cells(9).Value.ToString
            .lblSellingPrice.Text = dgvSalesReturns.CurrentRow.Cells(5).Value.ToString
            .lblqtysold.Text = dgvSalesReturns.CurrentRow.Cells(4).Value.ToString
            .lblItemBP.Text = Double.Parse(dgvSalesReturns.CurrentRow.Cells(10).Value.ToString)
            .WindowState = FormWindowState.Normal
            .StartPosition = FormStartPosition.CenterScreen
            .ShowDialog()
        End With

    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        'reload grid 
        btnLoad.PerformClick()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged

    End Sub
End Class