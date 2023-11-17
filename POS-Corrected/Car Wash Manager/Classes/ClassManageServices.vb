Imports MySql.Data
Imports MySql.Data.MySqlClient

Public Class ClassManageServices
    Public Sub ManageServices(ByVal Services As String, _
                               ByVal Price As Double, _
                               ByVal EntryNo As String, _
                               ByVal Indeterminer As Integer)

        EntryNo = frmServicesReport.dgvServices.CurrentRow.Cells(0).Value.ToString
        Dim strsql As String
        Dim dtaName As New MySqlClient.MySqlDataAdapter
        Try
            dtaName.InsertCommand = New MySqlClient.MySqlCommand
            With dtaName.InsertCommand
                .CommandTimeout = 60
                .Connection = Conn()
                .CommandType = CommandType.StoredProcedure
                strsql = "sp_managecarwash"
                .CommandText = strsql
                .Parameters.AddWithValue("@inservice", Services)
                .Parameters.AddWithValue("@inprice", Price)
                .Parameters.AddWithValue("@inentryno", EntryNo)
                .Parameters.AddWithValue("@indeterminer", Indeterminer)
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

    'RELOAD data to Datagridview
    Sub LoadServices(ByVal dgv As DataGridView)
        Dim strsql As String = "SELECT cw.entryid,cw.serviceoffered,cw.price" _
                               & " FROM carwashservices cw WHERE cw.deleted=0"
        'Try
        Dim cmd As New MySqlCommand(strsql, Conn)
        cmd.CommandType = CommandType.Text
        Dim adapter As New MySqlDataAdapter(cmd)
        Dim table As New DataTable
        adapter.Fill(table)
        'Display the data.
        dgv.DataSource = table
        closeconn()
        With dgv
            .Columns(0).HeaderText = "#"
            .Columns(1).HeaderText = "Service Offered"
            .Columns(2).HeaderText = "Price"
            .Columns(0).Visible = False
            .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells
            .Columns(1).DefaultCellStyle.WrapMode = DataGridViewTriState.True
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        End With
    End Sub

    Public Sub deleteServices()
        'delete record from database
        Dim servicename As String = frmServicesReport.dgvServices.CurrentRow.Cells(1).Value.ToString
        Dim serviceno As String = frmServicesReport.dgvServices.CurrentRow.Cells(0).Value.ToString

        Dim strsql As String = "UPDATE carwashservices SET deleted=1 WHERE entryid='" & serviceno & "'"

        Try
            Dim dtaName As New MySqlClient.MySqlDataAdapter
            dtaName.UpdateCommand = New MySqlClient.MySqlCommand
            With dtaName.UpdateCommand
                .CommandTimeout = 60
                .Connection = Conn()
                .CommandType = CommandType.Text
                .CommandText = strsql
                If (MsgBox("Are you sure you want to delete " & servicename & " completely from database?", MsgBoxStyle.YesNo, "Strawberry System") = MsgBoxResult.Yes) Then
                    .ExecuteNonQuery()
                    MessageBox.Show("Record successfully deleted", "Strawberry System", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            End With
            closeconn()
            'reload data
            LoadServices(frmServicesReport.dgvServices)
        Catch ex As Exception
            If Conn.State = ConnectionState.Open Then Conn.Close()
            MsgBox(ex.Message & "An Error Occured While Trying To Delete Data From Database")
            Exit Sub
        End Try
    End Sub

    Public Function getNextSalesNo() As Long
        Dim datareader As MySqlDataReader
        Dim strsql As String = "select max(entryid) from carwashmaster"
        Dim salesno As Long = 0
        Try

            Dim cmd As New MySqlCommand(strsql, Conn)
            cmd.CommandType = CommandType.Text
            datareader = cmd.ExecuteReader
            While datareader.Read
                salesno = Long.Parse(datareader(0).ToString)
            End While
            closeconn()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return salesno
    End Function

    Public Sub InsertCarwashSales(ByVal EmpName As String, _
                                  ByVal VehicleReg As String, _
                                  ByVal Phone As String, _
                                  ByVal Remarks As String, _
                                  ByVal TotalAmnt As Double, _
                                  ByVal DateServed As Date, _
                                  ByVal Servedby As Integer, _
                                  ByVal SalesCode As Long, _
                                  ByVal service As String, _
                                  ByVal qty As Integer, _
                                  ByVal unitprice As Double, _
                                  ByVal Price As Double, _
                                  ByVal Indeterminer As Integer)

        'EntryNo = frmServicesReport.dgvServices.CurrentRow.Cells(0).Value.ToString
        Dim strsql As String
        Dim dtaName As New MySqlClient.MySqlDataAdapter
        Try
            dtaName.InsertCommand = New MySqlClient.MySqlCommand
            With dtaName.InsertCommand
                .CommandTimeout = 60
                .Connection = Conn()
                .CommandType = CommandType.StoredProcedure
                strsql = "sp_managecarwashsales"
                .CommandText = strsql
                .Parameters.AddWithValue("@inempname", EmpName)
                .Parameters.AddWithValue("@invehiclereg", VehicleReg)
                .Parameters.AddWithValue("@inphone", Phone)
                .Parameters.AddWithValue("@inremarks", Remarks)
                .Parameters.AddWithValue("@intotalamnt", TotalAmnt)
                .Parameters.AddWithValue("@indateserved", DateServed)
                .Parameters.AddWithValue("@inuser", Servedby)
                .Parameters.AddWithValue("@insalesno", SalesCode)
                .Parameters.AddWithValue("@inservice", service)
                .Parameters.AddWithValue("@inqty", qty)
                .Parameters.AddWithValue("@inunitprice", unitprice)
                .Parameters.AddWithValue("@inamount", Price)
                .Parameters.AddWithValue("@indeterminer", Indeterminer)
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
End Class
