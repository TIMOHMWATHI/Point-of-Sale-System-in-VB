Imports MySql.Data
Imports MySql.Data.MySqlClient

Public Class frmCashRunningTotals

    Private Sub btnCancel_Click(sender As Object, e As EventArgs)
        'Rub()
    End Sub

    Sub Compute_Variace()
        'For i = 0 To dgvCashFlow.RowCount - 1
        '    sumGenerators += Double.Parse(dgvCashFlow.Rows(i).Cells(1).Value.ToString)
        '    sumExpenses += Double.Parse(dgvCashFlow.Rows(i).Cells(2).Value.ToString)
        '    IncomeVariance = Double.Parse(sumGenerators - sumExpenses)
        'Next i
        'lblGeneratorTotals.Text = sumGenerators.ToString
        'lblDeductTotals.Text = sumExpenses.ToString
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnLoad_Click(sender As Object, e As EventArgs) Handles btnLoad.Click
        Dim dfrom As Date, dto As Date
        dfrom = dtpFrom.Value.Date
        dto = dtpTo.Value.Date
        LoadCashGenerators(dfrom, dto, dgvGenerators)
        LoadCashDeductors(dfrom, dto, dgvDeductors)
        lblOpenngBalance.Text = Double.Parse(get_OpeningBalance(dfrom, dto))
        'compute variance
        Dim AmountVariance As Double = 0, OpeningBal As Double = 0, _
             Generators As Double = 0, Expenses As Double = 0

        OpeningBal = Double.Parse(lblOpenngBalance.Text.ToString)
        Generators = Double.Parse(lblGeneratorTotals.Text.ToString)
        Expenses = Double.Parse(lblDeductTotals.Text)
        AmountVariance = ((OpeningBal + Generators) - Expenses)
        '###
        txtAmountVariance.Text = "KShs" & " " & Double.Parse(AmountVariance.ToString)
    End Sub

    Sub LoadCashGenerators(ByVal dfrom As Date, ByVal dto As Date, ByVal dgv As DataGridView)
        Dim TotalGenerators As Double = 0
        'Dim strsql As String = "SELECT DATE_FORMAT(cl.tdate,'%Y,%M,%d') AS tDate," _
        '                       & " cl.itemno,cl.sp AS 'Cost',cl.notes,cl.narration" _
        '                       & " FROM cashledger cl WHERE cl.trantype='+' AND" _
        '                       & " DATE(cl.tdate) BETWEEN '" & dfrom & "'" _
        '                       & " AND '" & dto & "' ORDER BY cl.entryid ASC"

        Dim strsql As String = "SELECT DATE_FORMAT(cl.tdate,'%Y,%M,%d') AS tDate," _
                               & " CONCAT(cl.fileno,' _ ',cl.itemno ) AS ReceptNo," _
                               & " GROUP_CONCAT(DISTINCT cl.qty, ' * ' ,pd.Description, '='," _
                               & " (sd.quantitybought * sd.itemsellingprice) ORDER BY sd.receptno SEPARATOR '  ; \r\n') AS ReceptItems," _
                               & " cl.sp AS 'Recept Total',cl.notes FROM cashledger cl  " _
                               & " INNER JOIN salesmaster sm ON cl.fileno=sm.receptno" _
                               & " INNER JOIN salesdetails sd ON cl.fileno=sd.receptno" _
                               & " INNER JOIN products pd ON sd.itemcode =pd.Barcode" _
                               & " WHERE cl.trantype='+' AND" _
                               & " DATE(cl.tdate) BETWEEN '" & dfrom & "' AND '" & dto & "' " _
                               & " GROUP BY sm.receptno ORDER BY cl.entryid ASC"

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
                .Columns(0).HeaderText = "Date"
                .Columns(1).HeaderText = "Receipt Desc"
                .Columns(2).HeaderText = "Receipt Items"
                .Columns(3).HeaderText = "Receipt Total"
                .Columns(4).HeaderText = "Affected by"
                .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells
                .Columns(0).DefaultCellStyle.WrapMode = DataGridViewTriState.True
                .Columns(1).DefaultCellStyle.WrapMode = DataGridViewTriState.True
                .Columns(3).DefaultCellStyle.WrapMode = DataGridViewTriState.True
                .Columns(4).DefaultCellStyle.WrapMode = DataGridViewTriState.True
                .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
                .Columns(2).Width = 250
            End With
            For i = 0 To dgv.RowCount - 1
                TotalGenerators += Double.Parse(dgv.Rows(i).Cells(3).Value)
            Next
            lblGeneratorTotals.Text = TotalGenerators.ToString
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Sub LoadCashDeductors(ByVal dfrom As Date, ByVal dto As Date, ByVal dgv As DataGridView)
        Dim ExpensesTotals As Double = 0

        'Dim strsql As String = "SELECT DATE_FORMAT(cl.tdate,'%Y,%M,%d') AS tDate," _
        '                         & " cl.itemno,cl.sp AS 'Cost',cl.notes,cl.narration" _
        '                         & " FROM cashledger cl WHERE cl.trantype='-' AND" _
        '                         & " DATE(cl.tdate) BETWEEN '" & dfrom & "'" _
        '                         & " AND '" & dto & "' ORDER BY cl.entryid ASC"

        Dim strsql As String = "SELECT DATE_FORMAT(cl.tdate,'%Y,%M,%d') AS tDate," _
                           & " CONCAT(cl.fileno,' _ ',cl.itemno ) AS ReceptNo," _
                           & " GROUP_CONCAT(DISTINCT cl.qty, ' * ' ,pd.Description, '='," _
                           & " (cl.sp) ORDER BY cl.entryid SEPARATOR '  ;  \r\n') AS ReceptItems," _
                           & " (cl.sp) AS 'Recept Total',cl.notes FROM cashledger cl " _
                           & " INNER JOIN salesmaster sm ON cl.fileno=sm.receptno" _
                           & " INNER JOIN salesdetails sd ON cl.fileno=sd.receptno" _
                           & " INNER JOIN products pd ON sd.itemcode =pd.Barcode" _
                           & " WHERE cl.trantype='-' AND" _
                           & " DATE(cl.tdate) BETWEEN '" & dfrom & "' AND '" & dto & "' " _
                           & " GROUP BY sm.receptno ORDER BY cl.entryid ASC"

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
                .Columns(0).HeaderText = "Date"
                .Columns(1).HeaderText = "Receipt Desc"
                .Columns(2).HeaderText = "Receipt Items"
                .Columns(3).HeaderText = "Receipt Total"
                .Columns(4).HeaderText = "Affected by"
                .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells
                .Columns(0).DefaultCellStyle.WrapMode = DataGridViewTriState.True
                .Columns(1).DefaultCellStyle.WrapMode = DataGridViewTriState.True
                .Columns(3).DefaultCellStyle.WrapMode = DataGridViewTriState.True
                .Columns(4).DefaultCellStyle.WrapMode = DataGridViewTriState.True
                .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
                .Columns(2).Width = 250
            End With
            For i = 0 To dgv.RowCount - 1
                ExpensesTotals += Double.Parse(dgv.Rows(i).Cells(3).Value)
            Next
            lblDeductTotals.Text = ExpensesTotals.ToString
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub frmCashRunningTotals_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Function get_OpeningBalance(ByVal dfrom As Date, _
                                       ByVal dto As Date) As Double

        Dim datareader As MySqlDataReader
        Dim OpeningBal As Double = 0

        Dim strsql As String = "SELECT SUM(op.amount) AS 'TotalBal' FROM openingbalance op" _
                               & " WHERE DATE(op.transactiondate) BETWEEN" _
                               & " '" & dfrom & "' AND '" & dto & "'"
        Try

            Dim cmd As New MySqlCommand(strsql, Conn)
            cmd.CommandType = CommandType.Text
            datareader = cmd.ExecuteReader
            While datareader.Read

                If (datareader("TotalBal").ToString) = String.Empty Then
                Else
                    OpeningBal = Double.Parse(datareader(0).ToString)
                End If

                '+++++++++++++
            End While
            datareader.Dispose()
            closeconn()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Return OpeningBal
    End Function

    Private Sub GroupBox1_Enter(sender As Object, e As EventArgs) Handles GroupBox1.Enter

    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim AmountVariance As Double = 0, OpeningBal As Double = 0, _
        Generators As Double = 0, Expenses As Double = 0

        OpeningBal = Double.Parse(lblOpenngBalance.Text.ToString)
        Generators = Double.Parse(lblGeneratorTotals.Text.ToString)
        Expenses = Double.Parse(lblDeductTotals.Text)
        AmountVariance = ((OpeningBal + Generators) - Expenses)

        'save to db
        Dim strsql As String
        Dim dtaName As New MySqlClient.MySqlDataAdapter
        Try
            dtaName.InsertCommand = New MySqlClient.MySqlCommand
            With dtaName.InsertCommand
                .CommandTimeout = 60
                .Connection = Conn()
                .CommandType = CommandType.Text
                strsql = "INSERT INTO cashrunningtotals (transactiondate,openingbal,incomegenerators,dailyexpenses,amntvariance,registeredby)" _
                                            & " VALUES (NOW()," & OpeningBal & "," & Generators & "," & Expenses & "," & AmountVariance & "," & userid & ")"
                'MsgBox(strsql)
                .CommandText = strsql
                .ExecuteNonQuery()
            End With
            dtaName.Dispose()
            closeconn()

            MessageBox.Show("Success", "Strawberry POS", MessageBoxButtons.OK, MessageBoxIcon.Information)
            'CLEAR
            Rub()

        Catch ex As Exception
            If Conn.State = ConnectionState.Open Then Conn.Close()
            MsgBox(ex.Message & " Error occured while saving data.If error persists, Inform your administrator for further action", MsgBoxStyle.Critical)
            Exit Sub
        End Try
    End Sub

    Sub Rub()
        lblOpenngBalance.Text = 0
        lblGeneratorTotals.Text = 0
        lblDeductTotals.Text = 0
        txtAmountVariance.Text = 0
    End Sub
End Class