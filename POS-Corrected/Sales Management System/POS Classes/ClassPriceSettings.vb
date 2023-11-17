Imports MySql.Data
Imports MySql.Data.MySqlClient

Public Class ClassPriceSettings
    Public Sub SetPricesRates(ByVal retailpercent As Double, _
                              ByVal wholesalepercent As Double, _
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

                strsql = "UPDATE pricelistsettings SET pricestatus=0 WHERE pricestatus=1"

                .CommandText = strsql
                .ExecuteNonQuery()

                strsql = "INSERT INTO pricelistsettings(retailpercent,wholesalepercent,dateset,setby,pricestatus)" _
                         & " VALUES(" & retailpercent & "," & wholesalepercent & ",'" & dateset & "'," & inuser & "," & status & ")"

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

        Dim strsql As String = "SELECT ps.retailpercent,ps.wholesalepercent," _
                               & " ps.dateset,s.fullnames, CASE WHEN ps.`pricestatus`=0 " _
                               & " THEN 'Inactive' ELSE 'Active' END AS 'Status' FROM pricelistsettings ps" _
                               & " INNER JOIN staff s ON s.staffid=ps.setby WHERE s.deleted!=1 "
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
                .Columns(0).HeaderText = "Retail Percent"
                .Columns(1).HeaderText = "Whosale Percent"
                .Columns(2).HeaderText = "Date Set"
                .Columns(3).HeaderText = "Set By"
                .Columns(4).HeaderText = "Status"
                .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells
                .Columns(2).DefaultCellStyle.WrapMode = DataGridViewTriState.True
                .Columns(3).DefaultCellStyle.WrapMode = DataGridViewTriState.True
                .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Public Sub insertPricelist(ByVal Barcode As String, ByVal NewBP As Double, _
                               ByVal NewRetail As Double, ByVal NewWholesale As Double, _
                               ByVal MinimumPrice As Double,
                               ByVal Dateset As Date, ByVal Selector As Integer)

        Dim strsql As String
        Dim dtaName As New MySqlClient.MySqlDataAdapter
        Try
            dtaName.InsertCommand = New MySqlClient.MySqlCommand
            With dtaName.InsertCommand
                .CommandTimeout = 60
                .Connection = Conn()
                .CommandType = CommandType.StoredProcedure
                strsql = "sp_prices"
                .CommandText = strsql
                .Parameters.AddWithValue("@inselector", Selector)
                .Parameters.AddWithValue("@inbarcode", Barcode)
                .Parameters.AddWithValue("@innewbp", NewBP)
                .Parameters.AddWithValue("@innewretail", NewRetail)
                .Parameters.AddWithValue("@innewwholesale", NewWholesale)
                .Parameters.AddWithValue("@inminsp", MinimumPrice)
                .Parameters.AddWithValue("@indateset", Dateset)
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
