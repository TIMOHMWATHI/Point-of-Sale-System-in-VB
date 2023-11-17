Imports MySql.Data
Imports MySql.Data.MySqlClient
Public Class frmSuppliers
    Dim supplier_id As Integer

    'calls the classsuppliers
    Dim suppliers As New ClassSuppliers

    Private Sub btnExit_Click(sender As Object, e As EventArgs)
        'If (MsgBox("Are you sure you want to close this page?", MsgBoxStyle.YesNo, "Suppliers") = MsgBoxResult.Yes) Then
        Me.Close()
        'End If
    End Sub

    'STARTS BY LOADING CURRENT DAY'S DATA ON THE DATAGRIDS FORM
    Private Sub frmSupplierss_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        suppliers.selectsuppliers(Today, dgvSuppliers)
        'auto complete search code
        LoadSuppliers()
        LoadCustomers()
    End Sub


    'sub load data from database
    Sub LoadSuppliers()

        Dim strsql As String = "SELECT s.fullname,s.pinid,s.address,s.town,s.physicallocation," _
                               & " s.phone, s.email, s.contactperson, s.supplier_id" _
                               & " FROM suppliers s WHERE s.relationtype='S' AND s.deleted =0"
        Try

            Dim cmd As New MySqlCommand(strsql, Conn)
            cmd.CommandType = CommandType.Text
            Dim adapter As New MySqlDataAdapter(cmd)
            Dim table As New DataTable
            adapter.Fill(table)
            'Display the data.
            dgvSuppliers.DataSource = table
            adapter.Dispose()
            closeconn()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        With dgvSuppliers
            .Columns(0).HeaderText = "Fullname"
            .Columns(1).HeaderText = "Business pin"
            .Columns(2).HeaderText = "Address"
            .Columns(3).HeaderText = "Town"
            .Columns(4).HeaderText = "Physical Location"
            .Columns(5).HeaderText = "Phone"
            .Columns(6).HeaderText = "Email"
            .Columns(7).HeaderText = "Contact person"
            '.Columns(8).HeaderText = "Relation Type"
            .Columns(8).HeaderText = " #"
            .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells
            .Columns(0).DefaultCellStyle.WrapMode = DataGridViewTriState.True
            .Columns(1).DefaultCellStyle.WrapMode = DataGridViewTriState.True
            .Columns(2).DefaultCellStyle.WrapMode = DataGridViewTriState.True
            .Columns(3).DefaultCellStyle.WrapMode = DataGridViewTriState.True
            .Columns(4).DefaultCellStyle.WrapMode = DataGridViewTriState.True
            .Columns(5).DefaultCellStyle.WrapMode = DataGridViewTriState.True
            .Columns(6).DefaultCellStyle.WrapMode = DataGridViewTriState.True
            .Columns(7).DefaultCellStyle.WrapMode = DataGridViewTriState.True
            .Columns(8).Visible = False
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        End With
    End Sub

    'sub load data from database
    Sub LoadCustomers()

        Dim strsql As String = "SELECT s.fullname,s.phone,s.supplier_id" _
                               & " FROM suppliers s WHERE s.relationtype='C' AND s.deleted =0"
        Try

            Dim cmd As New MySqlCommand(strsql, Conn)
            cmd.CommandType = CommandType.Text
            Dim adapter As New MySqlDataAdapter(cmd)
            Dim table As New DataTable
            adapter.Fill(table)
            'Display the data.
            dgvCustomers.DataSource = table
            adapter.Dispose()
            closeconn()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        With dgvCustomers
            .Columns(0).HeaderText = "Fullname"
            .Columns(1).HeaderText = "Phone"
            .Columns(2).HeaderText = " #"
            .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells
            .Columns(0).DefaultCellStyle.WrapMode = DataGridViewTriState.True
            .Columns(2).Visible = False
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        End With
    End Sub


    'suppliers soft deletion code on delete button
    Private Sub btnDelete_Click(sender As Object, e As EventArgs)
        Dim suppname As String = dgvSuppliers.CurrentRow.Cells(0).Value.ToString
        supplier_id = Integer.Parse(dgvSuppliers.CurrentRow.Cells(9).Value.ToString)
        Dim strsql As String = "UPDATE suppliers SET deleted=1 WHERE supplier_id='" & supplier_id & "'  AND deleted=0"
        Try
            Dim dtaName As New MySqlClient.MySqlDataAdapter
            dtaName.UpdateCommand = New MySqlClient.MySqlCommand
            With dtaName.UpdateCommand
                .CommandTimeout = 60
                .Connection = Conn()
                .CommandType = CommandType.Text
                .CommandText = strsql
                If (MsgBox("Are you sure you want to delete " & suppname & " completely from database?", MsgBoxStyle.YesNo, "Suppliers Deletion") = MsgBoxResult.Yes) Then
                    .ExecuteNonQuery()
                    MessageBox.Show("Record Successfully Deleted from Database", "Suppliers", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            End With
            dtaName.Dispose()
            closeconn()
            'reload data
            LoadSuppliers()
        Catch ex As Exception
            If Conn.State = ConnectionState.Open Then Conn.Close()
            MsgBox(ex.Message & "An Error Occured While Trying To Delete Data From Database")
            Exit Sub
        End Try
    End Sub

    'context menustrip code
    Private Sub DeleteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteToolStripMenuItem.Click
       
    End Sub


    Private Sub btnDelete_Click_1(sender As Object, e As EventArgs) Handles btnDelete.Click
        Dim strsql As String
        fullname = dgvSuppliers.CurrentRow.Cells(0).Value.ToString
        supplier_id = Integer.Parse(dgvSuppliers.CurrentRow.Cells(8).Value.ToString)
        strsql = "update suppliers set deleted=1 where supplier_id='" & supplier_id & "'"
        Try
            Dim dtaName As New MySqlClient.MySqlDataAdapter
            dtaName.UpdateCommand = New MySqlClient.MySqlCommand
            With dtaName.UpdateCommand
                .CommandTimeout = 60
                .Connection = Conn()
                .CommandType = CommandType.Text
                .CommandText = strsql
                If (MsgBox("Are you sure you want to delete " & fullname & " completely from database?", MsgBoxStyle.YesNo, "Suppliers Deletion") = MsgBoxResult.Yes) Then
                    .ExecuteNonQuery()
                    MessageBox.Show("Successfully Deleted", "Strawberry System", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
                closeconn()
            End With
            'reload data
            LoadSuppliers()
        Catch ex As Exception
            If Conn.State = ConnectionState.Open Then Conn.Close()
            MsgBox(ex.Message & "An Error Occured While Trying To Delete Data From Database")
            Exit Sub
        End Try
    End Sub


    Private Sub btnAddSupplier_Click(sender As Object, e As EventArgs) Handles btnAddSupplier.Click
        With frmAddSupplier
            .StartPosition = FormStartPosition.CenterScreen
            .MaximizeBox = False
            .MinimizeBox = False
            .ShowDialog()
        End With
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        'reload data
        LoadSuppliers()
        LoadCustomers()
    End Sub

    Private Sub btnUpdateSupplier_Click(sender As Object, e As EventArgs) Handles btnUpdateSupplier.Click

        If dgvSuppliers.RowCount = -1 Then
            MessageBox.Show("No name has been selected inorder to update", "Update Supplier", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        Else
            With frmAddSupplier
                .lblid.Text = dgvSuppliers.CurrentRow.Cells(8).Value.ToString
                .txtFullname.Text = dgvSuppliers.CurrentRow.Cells(0).Value.ToString
                .txtbusinesspin.Text = dgvSuppliers.CurrentRow.Cells(1).Value.ToString
                .txtaddress.Text = dgvSuppliers.CurrentRow.Cells(2).Value.ToString
                .txttown.Text = dgvSuppliers.CurrentRow.Cells(3).Value.ToString
                .txtPLocation.Text = dgvSuppliers.CurrentRow.Cells(4).Value.ToString
                .txtPhone.Text = dgvSuppliers.CurrentRow.Cells(5).Value.ToString
                .txtEmail.Text = dgvSuppliers.CurrentRow.Cells(6).Value.ToString
                .txtcontactperson.Text = dgvSuppliers.CurrentRow.Cells(7).Value.ToString
                .cbosupplier.SelectedItem = dgvSuppliers.CurrentRow.Cells(8).Value.ToString
                .btnSave.Text = "Update"
                '.txtFullname.ReadOnly = True
                '.txtbusinesspin.ReadOnly = False
                .ShowDialog()
            End With
        End If
    End Sub
End Class