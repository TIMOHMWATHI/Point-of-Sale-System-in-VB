Imports MySql.Data
Imports MySql.Data.MySqlClient

Public Class ClassItemCategory
    Public Sub managecategory(ByVal CategoryName As String, _
                              ByVal EntryNo As Integer, _
                              ByVal indeterminer As Integer)

        EntryNo = Integer.Parse(frmCategories.lblEntryNo.Text)
        'MsgBox(EntryNo)
        Dim strsql As String
        Dim dtaName As New MySqlClient.MySqlDataAdapter
        Try
            dtaName.InsertCommand = New MySqlClient.MySqlCommand
            With dtaName.InsertCommand
                .CommandTimeout = 60
                .Connection = Conn()
                .CommandType = CommandType.StoredProcedure
                strsql = "sp_manage_itemcateory"
                .CommandText = strsql
                .Parameters.AddWithValue("@incategoryname", CategoryName)
                .Parameters.AddWithValue("@inentryid", EntryNo)
                .Parameters.AddWithValue("@indeterminer", indeterminer)
                .ExecuteNonQuery()
            End With
            dtaName.Dispose()
            closeconn()
        Catch ex As Exception
            If Conn.State = ConnectionState.Open Then Conn.Close()
            MsgBox(ex.Message & " Error occured while saving data.If error persists, Inform your administrator for further action", MsgBoxStyle.Critical)
            Exit Sub
        End Try
    End Sub

    'Loads AND RELOAD database table FROM Datagridview
    Sub LoadCategoryDetails(ByVal dgv As DataGridView)
        Dim strsql As String = "SELECT pc.id,pc.categoryname FROM product_category pc" _
                               & " WHERE pc.deleted=0 ORDER BY pc.id ASC"
        Try
            Dim cmd As New MySqlCommand(strsql, Conn)
            cmd.CommandType = CommandType.Text
            Dim adapter As New MySqlDataAdapter(cmd)
            Dim table As New DataTable
            adapter.Fill(table)
            'Display the data.
            frmCategories.dgvCategory.DataSource = table
            closeconn()
            With dgv
                .Columns(0).HeaderText = "No"
                .Columns(1).HeaderText = "Category Name"
                .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells
                .Columns(0).Visible = False
                .Columns(1).DefaultCellStyle.WrapMode = DataGridViewTriState.True
                .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

End Class
