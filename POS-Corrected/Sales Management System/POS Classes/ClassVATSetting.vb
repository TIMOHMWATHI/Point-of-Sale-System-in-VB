Imports MySql.Data
Imports MySql.Data.MySqlClient

Public Class ClassVATSetting
    Public Sub Set_VAT_Percent(ByVal VATpercent As Double, _
                               ByVal dateset As Date, _
                               ByVal inuser As Integer, _
                               ByVal status As Integer)
        Dim strsql As String
        Dim dtaName As New MySqlClient.MySqlDataAdapter
        Try
            dtaName.InsertCommand = New MySqlClient.MySqlCommand
            With dtaName.InsertCommand
                .CommandTimeout = 60
                .Connection = Conn()
                .CommandType = CommandType.Text

                strsql = "UPDATE vatsettings SET vatstatus=0 WHERE vatstatus=1"

                .CommandText = strsql
                .ExecuteNonQuery()

                strsql = "INSERT INTO vatsettings(vatpercent,dateset,setby,vatstatus)" _
                         & " VALUES(" & VATpercent & ",'" & dateset & "'," & inuser & "," & status & ")"

                .CommandText = strsql
                .ExecuteNonQuery()
                MessageBox.Show("Success", "Strawberry POS", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End With
            dtaName.Dispose()
            closeconn()
        Catch ex As Exception
            If Conn.State = ConnectionState.Open Then closeconn()
            MsgBox(ex.Message & " Error occured while saving data.If error persists, Inform your administrator for further action", MsgBoxStyle.Critical)
            Exit Sub
        End Try
    End Sub

    Sub LoadToGrid(ByVal dgv As DataGridView)

        Dim strsql As String = "SELECT v.vatpercent,v.dateset,s.fullnames," _
                               & " CASE WHEN v.vatstatus=1 THEN 'Active'" _
                               & " ELSE 'Inactive' END AS 'VAT Status' FROM" _
                               & " vatsettings v INNER JOIN staff s ON " _
                               & " s.staffid=v.setby  WHERE v.vatstatus!=0"
        Try
            Dim cmd As New MySqlCommand(strsql, Conn)
            cmd.CommandType = CommandType.Text
            Dim adapter As New MySqlDataAdapter(cmd)
            Dim table As New DataTable
            adapter.Fill(table)
            'Display the data.
            dgv.DataSource = table
            closeconn()
            With dgv
                .Columns(0).HeaderText = "VAT Percent"
                .Columns(1).HeaderText = "Date Set"
                .Columns(2).HeaderText = "Set By"
                .Columns(3).HeaderText = "Status"
                .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells
                .Columns(1).DefaultCellStyle.WrapMode = DataGridViewTriState.True
                .Columns(3).DefaultCellStyle.WrapMode = DataGridViewTriState.True
                .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

End Class
