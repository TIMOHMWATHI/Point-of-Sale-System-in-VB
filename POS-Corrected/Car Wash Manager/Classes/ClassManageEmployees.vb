Imports MySql.Data
Imports MySql.Data.MySqlClient

Public Class ClassManageEmployees
    Public Sub ManageEmployees(ByVal fullnames As String, _
                                ByVal IDNumber As String, _
                                ByVal Telephone As String, _
                                ByVal Residence As String, _
                                ByVal Role As String, _
                                ByVal EntryNo As String, _
                                ByVal indeterminer As Integer)

        EntryNo = frmCarwashEmployees.dgvEmployees.CurrentRow.Cells(0).Value.ToString
        Dim strsql As String
        Dim dtaName As New MySqlClient.MySqlDataAdapter
        Try
            dtaName.InsertCommand = New MySqlClient.MySqlCommand
            With dtaName.InsertCommand
                .CommandTimeout = 60
                .Connection = Conn()
                .CommandType = CommandType.StoredProcedure
                strsql = "sp_manageemployees"
                .CommandText = strsql
                .Parameters.AddWithValue("@innames", fullnames)
                .Parameters.AddWithValue("@inidno", IDNumber)
                .Parameters.AddWithValue("@inphone", Telephone)
                .Parameters.AddWithValue("@inresidence", Residence)
                .Parameters.AddWithValue("@inemprole", Role)
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

    Public Sub ManageEmplRolls(ByVal Roll As String, _
                               ByVal indeterminer As Integer)

        'EntryNo = frmEmployeees.dgvEmployees.CurrentRow.Cells(0).Value.ToString
        Dim strsql As String
        Dim dtaName As New MySqlClient.MySqlDataAdapter
        Try
            dtaName.InsertCommand = New MySqlClient.MySqlCommand
            With dtaName.InsertCommand
                .CommandTimeout = 60
                .Connection = Conn()
                .CommandType = CommandType.StoredProcedure
                strsql = "sp_employeerolls"
                .CommandText = strsql
                .Parameters.AddWithValue("@inrole", Roll)
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
    Sub LoadEmployees()
        Dim strsql As String = "SELECT E.entryid,E.fullnames,E.idnumber,E.phone," _
                               & " E.residence,ER.emproll FROM employees E " _
                               & " INNER JOIN employeeroll ER ON ER.entryid=E.role" _
                               & " WHERE E.deleted=0"

        'Try
        Dim cmd As New MySqlCommand(strsql, Conn)
        cmd.CommandType = CommandType.Text
        Dim adapter As New MySqlDataAdapter(cmd)
        Dim table As New DataTable
        adapter.Fill(table)
        'Display the data.
        frmCarwashEmployees.dgvEmployees.DataSource = table
        closeconn()
        With frmCarwashEmployees.dgvEmployees
            .Columns(0).HeaderText = "#"
            .Columns(1).HeaderText = "Full Names"
            .Columns(2).HeaderText = "ID No:"
            .Columns(3).HeaderText = "Phone"
            .Columns(4).HeaderText = "Residence"
            .Columns(5).HeaderText = "Role"
            .Columns(0).Visible = False
            .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells
            .Columns(1).DefaultCellStyle.WrapMode = DataGridViewTriState.True
            .Columns(2).DefaultCellStyle.WrapMode = DataGridViewTriState.True
            .Columns(3).DefaultCellStyle.WrapMode = DataGridViewTriState.True
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        End With
    End Sub

    'delete staff 
    Public Sub deletestaff(ByVal fullnames As String, ByVal entryid As Integer)

        entryid = Integer.Parse(frmCarwashEmployees.dgvEmployees.CurrentRow.Cells(0).Value.ToString)
        fullnames = frmCarwashEmployees.dgvEmployees.CurrentRow.Cells(1).Value.ToString
        Dim strsql As String
        Dim dtaName As New MySqlClient.MySqlDataAdapter
        Try
            dtaName.DeleteCommand = New MySqlClient.MySqlCommand
            With dtaName.DeleteCommand
                .CommandTimeout = 60
                .Connection = Conn()
                .CommandType = CommandType.Text
                strsql = "UPDATE wasteemployees SET deleted=1 WHERE entryid=" & entryid & ""
                .CommandText = strsql

                If (MsgBox("Are you sure you want to delete " & fullnames & " completely from database?", MsgBoxStyle.YesNo, "Staff Deletion") = MsgBoxResult.Yes) Then
                    .ExecuteNonQuery()
                    MessageBox.Show("Record Successfully Deleted from Database", "Staff", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
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
    Sub LoadRoles()
        Dim strsql As String = "SELECT entryid,emproll FROM employeeroll ORDER BY emproll ASC"

        'Try
        Dim cmd As New MySqlCommand(strsql, Conn)
        cmd.CommandType = CommandType.Text
        Dim adapter As New MySqlDataAdapter(cmd)
        Dim table As New DataTable
        adapter.Fill(table)
        'Display the data.
        frmCarwashEmployees.cboEmployeeRole.DataSource = table
        closeconn()
    End Sub
End Class
