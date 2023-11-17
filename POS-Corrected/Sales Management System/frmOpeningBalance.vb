Imports MySql.Data
Imports MySql.Data.MySqlClient

Public Class frmOpeningBalance
    Dim OpeningBal As Double = 0

    Private Sub frmOpeningBalance_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadOpeningBal(dgvOpeningBal)
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If txtOpeningBalance.Text = String.Empty Then
            MessageBox.Show("Invalid amount", "Strawberry POS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        Else
            OpeningBal = Double.Parse(txtOpeningBalance.Text)
        End If
        'save to db
        Dim strsql As String
        Dim dtaName As New MySqlClient.MySqlDataAdapter
        Try
            dtaName.InsertCommand = New MySqlClient.MySqlCommand
            With dtaName.InsertCommand
                .CommandTimeout = 60
                .Connection = Conn()
                .CommandType = CommandType.Text
                strsql = "INSERT INTO openingbalance (amount,transactiondate,registeredby)" _
                                        & " VALUES (" & OpeningBal & ",NOW()," & userid & ")"
                'MsgBox(strsql)
                .CommandText = strsql
                .ExecuteNonQuery()
            End With
            dtaName.Dispose()
            closeconn()

            MessageBox.Show("Success", "Strawberry POS", MessageBoxButtons.OK, MessageBoxIcon.Information)
            'CLEAR
            txtOpeningBalance.Clear()
            'reload data
            LoadOpeningBal(dgvOpeningBal)
        Catch ex As Exception
            If Conn.State = ConnectionState.Open Then Conn.Close()
            MsgBox(ex.Message & " Error occured while saving data.If error persists, Inform your administrator for further action", MsgBoxStyle.Critical)
            Exit Sub
        End Try
    End Sub

    'Loads AND RELOAD database table FROM Datagridview
    Sub LoadOpeningBal(ByVal dgv As DataGridView)
        Dim strsql As String = "SELECT DATE_FORMAT(ob.transactiondate,'%Y,%M,%d') AS tDate," _
                               & " ob.amount,s.fullnames FROM openingbalance ob" _
                               & " INNER JOIN staff s ON s.staffid=ob.registeredby"
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
                .Columns(0).HeaderText = " Date "
                .Columns(1).HeaderText = "Amount"
                .Columns(2).HeaderText = "Registered by"
                '.Columns(0).Visible = False
                .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells
                .Columns(2).DefaultCellStyle.WrapMode = DataGridViewTriState.True
                .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub txtOpeningBalance_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtOpeningBalance.KeyPress
        'input validation code for numeric and decimal values only
        Dim c As Char
        c = e.KeyChar
        If Not (Char.IsDigit(c) Or c = "." Or
        Char.IsControl(c)) Then
            e.Handled = True
        End If
    End Sub
End Class