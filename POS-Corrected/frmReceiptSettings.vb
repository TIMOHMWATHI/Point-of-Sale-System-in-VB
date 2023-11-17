Imports MySql.Data
Imports MySql.Data.MySqlClient

Public Class frmReceiptSettings
    Dim header As New ClassReceiptHeader

    Dim BusibessName As String, Phone As String, _
        Paybill As String, Motto As String, _
        Entryid As Integer, Narration As String = ""

    Private Sub frmReceiptSettings_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'load controls
        RetriveData()
        'load to grid
        LoadHeaderText(dgvReceiptHeader)
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        'business name validation
        If txtBizName.Text.Trim = String.Empty Then
            MessageBox.Show("Invalid business name", "Strawberry POS", MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtBizName.Focus()
            Exit Sub
        Else
            BusibessName = txtBizName.Text.Trim
        End If

        'phone validation
        If txtPhone.Text.Trim = String.Empty Then
            MessageBox.Show("Invalid phone number", "Strawberry POS", MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtPhone.Focus()
            Exit Sub
        Else
            Phone = txtPhone.Text.Trim
        End If

        'other declarations
        Paybill = txtPaybill.Text.Trim
        Motto = txtMotto.Text.Trim

        'save to db
        If btnSave.Text = "Save" Then
            header.InsertHeader(BusibessName, Phone, Paybill, Motto, 0, True)
            'reload data
            LoadHeaderText(dgvReceiptHeader)
        Else
            header.InsertHeader(BusibessName, Phone, Paybill, Motto, Entryid, False)
            'reload data
            LoadHeaderText(dgvReceiptHeader)
        End If
    End Sub

    Sub LoadHeaderText(ByVal dgv As DataGridView)
        Dim strsql As String = "SELECT rh.entryid,rh.businessname," _
                               & " rh.phone, rh.paybillno, rh.motto " _
                               & " FROM  receiptheader rh "
        Try
            Dim cmd As New MySqlCommand(strsql, Conn)
            cmd.CommandType = CommandType.Text
            Dim adapter As New MySqlDataAdapter(cmd)
            Dim table As New DataTable
            adapter.Fill(table)
            'Display the data.
            dgv.DataSource = table
            closeconn()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        With dgv
            .Columns(0).HeaderText = "#"
            .Columns(1).HeaderText = "Business Name"
            .Columns(2).HeaderText = "Phone"
            .Columns(3).HeaderText = "Paybill No"
            .Columns(4).HeaderText = "Motto"
            .Columns(0).Visible = False
            .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells
            .Columns(1).DefaultCellStyle.WrapMode = DataGridViewTriState.True
            .Columns(2).DefaultCellStyle.WrapMode = DataGridViewTriState.True
            .Columns(3).DefaultCellStyle.WrapMode = DataGridViewTriState.True
            .Columns(4).DefaultCellStyle.WrapMode = DataGridViewTriState.True
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        End With

    End Sub

    Private Sub dgvReceiptHeader_DoubleClick(sender As Object, e As EventArgs) Handles dgvReceiptHeader.DoubleClick
        btnSave.Text = "Update"
        lblEntryNo.Text = dgvReceiptHeader.CurrentRow.Cells(0).Value.ToString
        txtBizName.Text = dgvReceiptHeader.CurrentRow.Cells(1).Value.ToString
        txtPhone.Text = dgvReceiptHeader.CurrentRow.Cells(2).Value.ToString
        txtPaybill.Text = dgvReceiptHeader.CurrentRow.Cells(3).Value.ToString
        txtMotto.Text = dgvReceiptHeader.CurrentRow.Cells(4).Value.ToString
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Sub RetriveData()
        Dim datareader As MySqlDataReader

        strsql = "SELECT rh.entryid,rh.businessname," _
                 & " rh.phone, rh.paybillno, rh.motto " _
                 & " FROM  receiptheader rh "

        Dim cmd As New MySqlCommand(strsql, Conn)
        cmd.CommandType = CommandType.Text
        datareader = cmd.ExecuteReader
        While datareader.Read
            txtBizName.Text = datareader(1).ToString
            txtPhone.Text = datareader(2).ToString
            txtPaybill.Text = datareader(3).ToString
            txtMotto.Text = datareader(4).ToString

            'dgvSales.Rows.Add(itemname, 1, price, price, barcode, StockBal, 0, ItemBP)
            '+++++++++++++
        End While
        closeconn()
    End Sub
End Class