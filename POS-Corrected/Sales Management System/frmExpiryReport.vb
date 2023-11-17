Imports MySql.Data
Imports MySql.Data.MySqlClient

Public Class frmExpiryReport
    Dim itemcode As String, description As String, _
       batchno As String, ExpiryDate As String, _
       ExpiryDays As Integer, StockBalance As Integer

    Private Sub frmExpiryReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'load data
        txtGoodForUseSearch.Text = ""
        LoadConsumables(dgvConsumables)
        txtAlmostExpiredSearch.Text = ""
        LoadConsumables(dgvNonConsubles)
    End Sub

    Private Sub txtGoodForUseSearch_TextChanged(sender As Object, e As EventArgs) Handles txtGoodForUseSearch.TextChanged
        Dim itemname As String
        If txtGoodForUseSearch.Text.Trim.Length > 0 Then
            itemname = txtGoodForUseSearch.Text.Trim
            LoadAutosearch(dgvConsumables)
        Else
            LoadConsumables(dgvConsumables)
        End If
    End Sub

    Private Sub txtAlmostExpiredSearch_TextChanged(sender As Object, e As EventArgs) Handles txtAlmostExpiredSearch.TextChanged
        Dim itemname As String
        If txtAlmostExpiredSearch.Text.Trim.Length > 0 Then
            itemname = txtAlmostExpiredSearch.Text.Trim
            LoadAutosearch(dgvNonConsubles)
        Else
            LoadConsumables(dgvNonConsubles)
        End If
    End Sub

    'Loads AND RELOAD database table FROM Datagridview
    Sub LoadConsumables(ByVal dgvdisplay As DataGridView)
        dgvdisplay.Rows.Clear()
        Dim strsql As String

        If dgvdisplay.Name = "dgvNonConsubles" Then
            strsql = "SELECT p.Barcode,p.Description,dd.batchno," _
                     & " DATE_FORMAT(dd.expirydate,'%d %M %Y') AS ExpiryDate," _
                     & " TIMESTAMPDIFF(DAY,CURDATE(),dd.expirydate) AS 'Days_To_Expiry'," _
                     & " s.quantityinstock FROM deliverydetails dd" _
                     & " INNER JOIN products p ON p.Barcode=dd.barcode" _
                     & " INNER JOIN stock s ON s.itemcode=dd.barcode" _
                     & " WHERE p.deleted!=1 And TIMESTAMPDIFF(DAY,CURDATE(),dd.expirydate)<30" _
                     & " GROUP BY p.barcode,dd.batchno ORDER BY dd.barcode ASC "

        Else
            strsql = "SELECT p.Barcode,p.Description,dd.batchno," _
                     & " DATE_FORMAT(dd.expirydate,'%d %M %Y') AS ExpiryDate," _
                     & " TIMESTAMPDIFF(DAY,CURDATE(),dd.expirydate) AS 'Days_To_Expiry'," _
                     & " s.quantityinstock FROM deliverydetails dd" _
                     & " INNER JOIN products p ON p.Barcode=dd.barcode" _
                     & " INNER JOIN stock s ON s.itemcode=dd.barcode" _
                     & " WHERE p.deleted!=1 And TIMESTAMPDIFF(Day, CURDATE(), dd.expirydate)>30" _
                     & " GROUP BY p.barcode,dd.batchno ORDER BY dd.barcode ASC"
        End If

        Try

            Dim datareader As MySqlDataReader
            Dim cmd As New MySqlCommand(strsql, Conn)
            cmd.CommandType = CommandType.Text
            datareader = cmd.ExecuteReader
            While datareader.Read
                itemcode = datareader(0).ToString
                description = datareader(1).ToString
                batchno = datareader(2).ToString
                ExpiryDate = datareader(3).ToString
                ExpiryDays = Integer.Parse(datareader(4).ToString)
                StockBalance = Integer.Parse(datareader(5).ToString)

                'add rows to grid
                dgvdisplay.Rows.Add(itemcode, description, batchno, ExpiryDate, ExpiryDays, StockBalance)
            End While
            datareader.Dispose()
            closeconn()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    'Load AutoSearch
    Sub LoadAutosearch(ByVal dgv As DataGridView)
        Dim strsql As String, GoodForUse As String, NotGoodForUse As String
        dgv.Rows.Clear()
        GoodForUse = txtGoodForUseSearch.Text.Trim
        NotGoodForUse = txtAlmostExpiredSearch.Text.Trim

        If dgv.Name = "dgvNonConsubles" Then
            strsql = "SELECT p.Barcode,p.Description,dd.batchno," _
                     & " DATE_FORMAT(dd.expirydate,'%d %M %Y') AS ExpiryDate," _
                     & " TIMESTAMPDIFF(DAY,CURDATE(),dd.expirydate) AS 'Days_To_Expiry'," _
                     & " s.quantityinstock FROM deliverydetails dd" _
                     & " INNER JOIN products p ON p.Barcode=dd.barcode" _
                     & " INNER JOIN stock s ON s.itemcode=dd.barcode" _
                     & " WHERE p.deleted!=1 And TIMESTAMPDIFF(DAY,CURDATE(),dd.expirydate)<30" _
                     & " AND p.Description LIKE '" & "%" & NotGoodForUse & "%" & "'" _
                     & " OR dd.batchno LIKE '" & "%" & NotGoodForUse & "%" & "' " _
                     & " GROUP BY p.barcode,dd.batchno ORDER BY dd.barcode ASC "

        Else
            strsql = "SELECT p.Barcode,p.Description,dd.batchno," _
                     & " DATE_FORMAT(dd.expirydate,'%d %M %Y') AS ExpiryDate," _
                     & " TIMESTAMPDIFF(DAY,CURDATE(),dd.expirydate) AS 'Days_To_Expiry'," _
                     & " s.quantityinstock FROM deliverydetails dd" _
                     & " INNER JOIN products p ON p.Barcode=dd.barcode" _
                     & " INNER JOIN stock s ON s.itemcode=dd.barcode" _
                     & " WHERE p.deleted!=1 And TIMESTAMPDIFF(DAY,CURDATE(),dd.expirydate)>30" _
                     & " AND p.Description LIKE '" & "%" & GoodForUse & "%" & "'" _
                     & " OR dd.batchno LIKE '" & "%" & GoodForUse & "%" & "' " _
                     & " GROUP BY p.barcode,dd.batchno ORDER BY dd.barcode ASC "
        End If

        Try

            Dim datareader As MySqlDataReader
            Dim cmd As New MySqlCommand(strsql, Conn)
            cmd.CommandType = CommandType.Text
            datareader = cmd.ExecuteReader
            While datareader.Read
                itemcode = datareader(0).ToString
                description = datareader(1).ToString
                batchno = datareader(2).ToString
                ExpiryDate = datareader(3).ToString
                ExpiryDays = Integer.Parse(datareader(4).ToString)
                StockBalance = Integer.Parse(datareader(5).ToString)

                'add rows to grid
                dgv.Rows.Add(itemcode, description, batchno, ExpiryDate, ExpiryDays, StockBalance)
            End While
            datareader.Dispose()
            closeconn()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnRefressh_Click(sender As Object, e As EventArgs) Handles btnRefressh.Click
        txtAlmostExpiredSearch.Text = ""
        LoadConsumables(dgvNonConsubles)
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        txtGoodForUseSearch.Text = ""
        LoadConsumables(dgvConsumables)
    End Sub

    Private Sub btnClose1_Click(sender As Object, e As EventArgs) Handles btnClose1.Click
        Me.Close()
    End Sub

    Private Sub btnClose2_Click(sender As Object, e As EventArgs) Handles btnClose2.Click
        Me.Close()
    End Sub
End Class