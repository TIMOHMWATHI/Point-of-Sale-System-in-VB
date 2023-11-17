Imports MySql.Data
Imports MySql.Data.MySqlClient
Public Class frmSpoiltItems
    Dim inventorystock As New inventory
    'variables
    Dim barcode As String, datereg As Date, qty As Double, cost As Double, narration As String
    Dim entryid As Integer, totalitemsdelivered As Double
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        'barcode input validation
        If txtSearch.Text.Trim = String.Empty Then
            MessageBox.Show("Input product barcode", "Strawberry POS", MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtSearch.Focus()
            Exit Sub
        Else
            'check if it exists in the database
            '
            barcode = lblBarcode.Text.ToString
        End If

        'date validation
        datereg = Now()

        'quantity input validation
        If txtQuantity.Text.Trim = String.Empty Then
            MessageBox.Show("Input product quantity", "Strawberry POS", MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtQuantity.Focus()
            Exit Sub
        Else
            qty = Double.Parse(txtQuantity.Text.Trim)
        End If

        'cost input validation
        If txtCost.Text.Trim = String.Empty Then
            MessageBox.Show("Input product cost", "Strawberry POS", MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtCost.Focus()
            Exit Sub
        Else
            cost = Double.Parse(txtCost.Text.Trim)
        End If

        'Narration input validation
        If txtNarration.Text.Trim = String.Empty Then
            MessageBox.Show("Input a reason for wastage or spoilage", "Strawberry POS", MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtNarration.Focus()
            Exit Sub
        Else
            narration = txtNarration.Text.Trim
        End If
        'save data
        Try
            If btnSave.Text = "Save" Then
                'insert data
                inventorystock.wasteRegister(barcode, qty, cost, narration, 3)

                ''INSERT INTO CASH LEDGER
                'CashLedger(recNo, barcode, qty, sp, Now(), userid, "Stock returns of sold items", "+", "SalesReturns", 1)

                'INSERT INTO STOCK MOVEMENT
                stockMovement(barcode, "wastes", "-", qty, 0, 0, Now(), userid, 1) 'insert values of bp and sp after qty
            Else
                'update data in pricelist database
                strsql = "UPDATE wasteregister SET itemcode='" & barcode & "',dateregistered='" & datereg & "',qty=" & qty & "," _
                          & " cost=" & cost & ",narration='" & narration & "',enteredby='" & userid & "' WHERE entryid=" & entryid & ""
                Dim dtaName As New MySqlClient.MySqlDataAdapter
                dtaName.UpdateCommand = New MySqlClient.MySqlCommand
                With dtaName.UpdateCommand
                    .CommandTimeout = 60
                    .Connection = Conn()
                    .CommandType = CommandType.Text
                    .CommandText = strsql
                    .ExecuteNonQuery()
                End With
            End If
            MessageBox.Show("Successful", "Strawberry POS", MessageBoxButtons.OK, MessageBoxIcon.Information)
            'reload data
            LoadWasteS()
            closeconn()
            'clear content
            clearcontent()
        Catch ex As Exception
            If Conn.State = ConnectionState.Open Then closeconn()
            MsgBox(ex.Message & "An error occured when trying to save data. If error persists, inform your administrator for further action", MsgBoxStyle.Critical)
            Exit Sub
        End Try

    End Sub

    'clear sub
    Sub clearcontent()
        txtSearch.Clear()
        dtpdateRegistered.Value = Today
        txtQuantity.Clear()
        txtCost.Clear()
        txtNarration.Clear()
        btnSave.Text = "Save"
    End Sub

    'Loads AND RELOAD database table FROM Datagridview
    Sub LoadWasteS()
        Dim strsql As String = "SELECT w.entryid,w.itemcode,p.Description,w.dateregistered,w.qty," _
                               & " w.cost,w.narration,S.fullnames FROM wasteregister w " _
                               & " INNER JOIN products p ON p.Barcode=w.itemcode" _
                               & " INNER JOIN staff S ON S.staffid= w.enteredby"
        Try
            Dim cmd As New MySqlCommand(strsql, Conn)
            cmd.CommandType = CommandType.Text
            Dim adapter As New MySqlDataAdapter(cmd)
            Dim table As New DataTable
            adapter.Fill(table)
            'Display the data.
            dgvWaste2.DataSource = table
            closeconn()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        With dgvWaste2
            .Columns(0).HeaderText = "#"
            .Columns(1).HeaderText = "Item Code"
            .Columns(2).HeaderText = "Item Name"
            .Columns(3).HeaderText = "Date"
            .Columns(4).HeaderText = "Quantity"
            .Columns(5).HeaderText = "Cost of item"
            .Columns(6).HeaderText = "Narration"
            .Columns(7).HeaderText = "Removed by"
            .Columns(0).Visible = False
            .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells
            .Columns(2).DefaultCellStyle.WrapMode = DataGridViewTriState.True
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        End With
    End Sub

    'double click event
    Private Sub dgvWaste_DoubleClick(sender As Object, e As EventArgs) Handles dgvWaste2.DoubleClick
        entryid = dgvWaste2.CurrentRow.Cells(0).Value.ToString
        txtSearch.Text = dgvWaste2.CurrentRow.Cells(1).Value.ToString
        dtpdateRegistered.Value = dgvWaste2.CurrentRow.Cells(2).Value.ToString
        txtQuantity.Text = dgvWaste2.CurrentRow.Cells(3).Value.ToString
        txtCost.Text = dgvWaste2.CurrentRow.Cells(4).Value.ToString
        txtNarration.Text = dgvWaste2.CurrentRow.Cells(5).Value.ToString
        btnSave.Text = "Update"
        txtSearch.ReadOnly = True
    End Sub

    'close waste form
    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        'If (MsgBox("Are you sure you want to close this page?", MsgBoxStyle.YesNo, "Wastes or damaged products") = MsgBoxResult.Yes) Then
        Me.Close()
        'End If
    End Sub

    Private Sub frmWastes_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'autosearch items
        Loadproducts(txtSearch)
        'load data
        LoadWasteS()
    End Sub

    Private Sub txtSearch_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSearch.KeyDown
        Dim itemname As String

        'input validation on txtItemSearch
        If (txtSearch.Text = String.Empty) Then
            txtSearch.Focus()
            Exit Sub
        Else
            itemname = txtSearch.Text.Trim
        End If
        '#################################################################################################
        'AUTO SEARCH FARMER NAMES
        Dim datareader As MySqlDataReader
        If e.KeyCode = Keys.Enter Then    'work if enter button is clicked
            Try
                Dim strsql = "SELECT Description,Barcode FROM products" _
                             & " WHERE deleted=0 AND (Description='" & itemname & "')"

                Dim cmd As New MySqlCommand(strsql, Conn)
                cmd.CommandType = CommandType.Text
                datareader = cmd.ExecuteReader
                While datareader.Read
                    txtSearch.Text = datareader(0).ToString
                    lblBarcode.Text = datareader(1).ToString
                End While
                closeconn()
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If
    End Sub
End Class