Imports MySql.Data
Imports MySql.Data.MySqlClient
Public Class frmproducts
    Dim items As New ClassProducts

    Private Sub frmproducts_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'reload data
        items.Loadproductstogrid(dgvItems)
        '###################################################################################
        Dim Inventory_Items As Integer
        Inventory_Items = Integer.Parse(items.Compute_InventoryItems())
        txtInventoryCount.Text = Inventory_Items.ToString
        '###################################################################################
    End Sub

    Private Sub btnAddNew_Click(sender As Object, e As EventArgs) Handles btnAddNew.Click
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        'ASSIGN USER PRIVILEDGES CODE
        If Not Authorized(userid, TaskID.addinventory) Then Exit Sub
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        With frmAddProducts
            .StartPosition = FormStartPosition.CenterScreen
            .WindowState = FormWindowState.Normal
            '.TopMost = True
            .ShowDialog()
        End With
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        'ASSIGN USER PRIVILEDGES CODE
        If Not Authorized(userid, TaskID.updateinventory) Then Exit Sub
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        'populate category combobox
        Dim strsql As String = "SELECT pc.id,pc.categoryname FROM product_category pc ORDER BY pc.categoryname ASC"
        loadcombo(frmAddProducts.cbocategory, strsql, "categoryname", "id")


        If dgvItems.SelectedRows.Count < 1 Then
            MessageBox.Show("No name to update", "Strawberry System", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        Else
            With frmAddProducts
                .txtbarcode.Text = dgvItems.CurrentRow.Cells(1).Value.ToString
                .txtDescription.Text = dgvItems.CurrentRow.Cells(2).Value.ToString
                .txtItemsPerPack.Text = dgvItems.CurrentRow.Cells(3).Value.ToString
                .cbocategory.Text = dgvItems.CurrentRow.Cells(4).Value.ToString
                .cboreorderlevel.Text = dgvItems.CurrentRow.Cells(5).Value.ToString
                .txtRetail.Text = dgvItems.CurrentRow.Cells(7).Value.ToString
                .txtWholesale.Text = dgvItems.CurrentRow.Cells(8).Value.ToString
                .lblRetail.Enabled = False
                .lblWholesale.Enabled = False
                .txtRetail.Enabled = False
                .txtWholesale.Enabled = False
                .btnSave.Text = "Update"
                .txtbarcode.ReadOnly = True
                .StartPosition = FormStartPosition.CenterScreen
                .WindowState = FormWindowState.Normal
                '.TopMost = True
                .ShowDialog()
            End With
        End If
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        'reload data
        items.Loadproductstogrid(dgvItems)
        'rub search textbox
        txtProductsSearc.Clear()
    End Sub

    Private Sub txtProductsSearc_TextChanged(sender As Object, e As EventArgs) Handles txtProductsSearc.TextChanged
        Dim itemname As String
        If txtProductsSearc.Text.Trim.Length > 0 Then
            itemname = txtProductsSearc.Text.Trim
            dgvItems.Rows.Clear()
            items.LoadAutosearch(dgvItems)
        Else
            items.Loadproductstogrid(dgvItems)
        End If
    End Sub

    'Loads products_category 
    Sub Loadcategory()
        Dim strsql As String = "SELECT categoryname FROM product_category ORDER BY categoryname ASC"
        Try
            Dim cmd As New MySqlCommand(strsql, Conn)
            cmd.CommandType = CommandType.Text
            Dim adapter As New MySqlDataAdapter(cmd)
            Dim table As New DataTable
            adapter.Fill(table)
            'Display the data.
            frmAddProducts.cbocategory.DataSource = table
            closeconn()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub DeleteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteToolStripMenuItem.Click
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        'ASSIGN USER PRIVILEDGES CODE
        If Not Authorized(userid, TaskID.deleteinventory) Then Exit Sub
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        'If dgvItems.RowCount > 0 Then

        If (MsgBox("Are you sure you want to delete" & vbNewLine & "the selection completely from database?", MsgBoxStyle.YesNo, "Strawberry POS") = MsgBoxResult.Yes) Then

            For i = 0 To dgvItems.RowCount - 1
                'MsgBox(i)
                Dim CheckStates As Boolean = dgvItems.Rows(i).Cells(0).Value()
                Dim Barcodes As String = dgvItems.Rows(i).Cells(1).Value.ToString()

                If CheckStates = True Then
                    ' put the delete code here
                    items.insertItems(Barcodes, "", 0, 0, 0, 0, 0, "", 0, 0, 4)
                End If
            Next
            MessageBox.Show("Successfully Erased", "Strawberry POS", MessageBoxButtons.OK, MessageBoxIcon.Information)
            'reload data
            items.Loadproductstogrid(dgvItems)
        End If
        'End If
        Exit Sub
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        'ASSIGN USER PRIVILEDGES CODE
        If Not Authorized(userid, TaskID.deleteinventory) Then Exit Sub
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        'delete record from database
        Dim Barcode As String = dgvItems.CurrentRow.Cells(1).Value.ToString
        Dim Desc As String = dgvItems.CurrentRow.Cells(2).Value.ToString

        Try

            If (MsgBox("Are you sure you want to delete " & Desc & " completely from database?", _
                MsgBoxStyle.YesNo, "Strawberry POS") = MsgBoxResult.Yes) Then

                items.insertItems(Barcode, "", 0, 0, 0, 0, 0, "", 0, 0, 3)
                MessageBox.Show("Success", "Strawberry POS", MessageBoxButtons.OK, MessageBoxIcon.Information)

                'reload data
                items.Loadproductstogrid(dgvItems)
            Else
                'do nothing
            End If

        Catch ex As Exception
            If Conn.State = ConnectionState.Open Then Conn.Close()
            MsgBox(ex.Message & "An Error Occured While Trying To Delete Data From Database")
            Exit Sub
        End Try
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        'If (MsgBox("Are you sure you want to close this page?", MsgBoxStyle.YesNo, "Products") = MsgBoxResult.Yes) Then
        Me.Close()
        'End If
    End Sub

End Class