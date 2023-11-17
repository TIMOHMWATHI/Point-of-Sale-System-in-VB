Imports MySql.Data
Imports MySql.Data.MySqlClient

Public Class frmStockMovementStatement

    Private Sub frmStockMovementStatement_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'load autosearch
        Loadproducts(txtSearch)
    End Sub

    Private Sub txtSearch_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSearch.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim ItemName As String
            'input validation on txtItemSearch
            If (txtSearch.Text = String.Empty) Then
                txtSearch.Focus()
                Exit Sub
            Else
                ItemName = txtSearch.Text.Trim
            End If
            '#################################################################################################
            'AUTO SEARCH FARMER NAMES
            Dim datareader As MySqlDataReader
            If e.KeyCode = Keys.Enter Then    'work if enter button is clicked
                Try
                    Dim strsql = "SELECT p.Description,smt.itemcode FROM stockmovement smt" _
                                 & " INNER JOIN products p ON p.Barcode=smt.itemcode WHERE " _
                                 & " p.deleted!=1 AND p.Description='" & ItemName & "' OR " _
                                 & " smt.itemcode='" & ItemName & "'"

                    Dim cmd As New MySqlCommand(strsql, Conn)
                    cmd.CommandType = CommandType.Text
                    datareader = cmd.ExecuteReader
                    While datareader.Read
                        lblItemName.Text = datareader(0).ToString
                        lblBarcode.Text = datareader(1).ToString
                        lblBarcode.Visible = True
                        lblItemName.Visible = True
                        lblcode.Visible = True
                        lblItem.Visible = True
                        '########
                        'InvoicesAutosearch(InvoiceNo)
                    End While
                    datareader.Dispose()
                    closeconn()
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                End Try
            End If
        End If
    End Sub

    Private Sub btnLoad_Click(sender As Object, e As EventArgs) Handles btnLoad.Click
        Dim dfrom As Date, dto As Date
        dfrom = dtpFrom.Value.Date
        dto = dtpTo.Value.Date

        'MsgBox("Date From : " & dfrom & "  Date To: " & dto)

        Dim ItemName As String = lblItemName.Text
        Dim ItemCode As String = lblBarcode.Text
        'search items
        StockMovement(ItemName, ItemCode, dfrom, dto)
    End Sub

    Sub StockMovement(ByVal Itemname As String, ByVal ItemCode As String, _
                      ByVal dFrom As Date, ByVal dTo As Date)

        'Dim TotalAmntPaid As Double = 0

        Dim strsql As String = "SELECT smt.movementdate,smt.itemcode," _
                               & " p.Description,smt.quantity," _
                               & " smt.narration,s.fullnames FROM stockmovement smt" _
                               & " INNER JOIN products p ON p.Barcode=smt.itemcode" _
                               & " INNER JOIN staff s ON s.staffid=smt.movedby" _
                               & " WHERE  smt.itemcode='" & ItemCode & "' OR " _
                               & " p.Description='" & Itemname & "'" _
                               & " AND DATE(smt.movementdate)" _
                               & " BETWEEN '" & dFrom & "' AND '" & dTo & "'" _
                               & " ORDER BY smt.movementdate ASC"
        'MsgBox(strsql)
        Try
            Dim cmd As New MySqlCommand(strsql, Conn)
            cmd.CommandType = CommandType.Text
            Dim adapter As New MySqlDataAdapter(cmd)
            Dim table As New DataTable
            adapter.Fill(table)
            'Display the data.
            dgvStockMovement.DataSource = table
            adapter.Dispose()
            closeconn()
            With dgvStockMovement
                .Columns(0).HeaderText = "Date"
                .Columns(1).HeaderText = "Barcode"
                .Columns(2).HeaderText = "Description"
                .Columns(3).HeaderText = "Qty"
                .Columns(4).HeaderText = "Narration"
                .Columns(5).HeaderText = "Moved by"
                .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells
                .Columns(0).DefaultCellStyle.WrapMode = DataGridViewTriState.True
                .Columns(5).DefaultCellStyle.WrapMode = DataGridViewTriState.True
                .Columns(2).Visible = False
                .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Dim dfrom As Date = dtpFrom.Value
        Dim dto As Date = dtpTo.Value

        lblBarcode.Visible = False
        lblItemName.Visible = False
        lblcode.Visible = False
        lblItem.Visible = False
        'search items
        'StockMovement(dgvStockMovement, dfrom, dto)

    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click

    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class