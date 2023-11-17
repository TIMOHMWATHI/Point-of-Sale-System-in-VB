Imports MySql.Data
Imports MySql.Data.MySqlClient
Public Class frmViewPlacedOrder
    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        If (MsgBox("Are you sure you want to close this page?", MsgBoxStyle.YesNo, "Placed orders") = MsgBoxResult.Yes) Then
            Me.Close()
        End If
    End Sub

    'Loads AND RELOAD database table FROM Datagridview
    Sub LoadPlacedOrderdatafromdatabase()
        Dim strsql As String = "select orderno,dateordered,orderamount from ordermaster"
        Try
            Dim cmd As New MySqlCommand(strsql, Conn)
            cmd.CommandType = CommandType.Text
            Dim adapter As New MySqlDataAdapter(cmd)
            Dim table As New DataTable
            adapter.Fill(table)
            'Display the data.
            dgvViewPlacedOrder.DataSource = table
            closeconn()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    'alter grid content
    Sub formatPlacedOrderGrid()
        With dgvViewPlacedOrder
            If .RowCount > 0 Then
                .Columns(0).HeaderText = "Order No:"
                .Columns(1).HeaderText = "Date ordered"
                .Columns(2).HeaderText = "Order Amnt"
                .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            End If
        End With
    End Sub

    Private Sub frmViewPlacedOrder_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'load data
        LoadPlacedOrderdatafromdatabase()
        'format grid
        formatPlacedOrderGrid()
        
    End Sub

    Private Sub txtSearchOrder_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSearchOrder.KeyDown
        Dim OrderNo As Double = 0
        Dim OrderTotals As Double = 0

        If txtSearchOrder.Text.Trim = String.Empty Then
            'do nothing
        ElseIf e.KeyCode = Keys.Enter Then

            OrderNo = Double.Parse(txtSearchOrder.Text.Trim)
            '###################################################################
            'retrieve salesperson and sales date
            Dim datareader As MySqlDataReader

            Dim strsql = "SELECT od.orderno,DATE_FORMAT(om.dateordered,'%Y %M %d') AS 'Order Date' FROM orderdetails od" _
                         & " INNER JOIN ordermaster om ON om.orderno=od.orderno" _
                         & " WHERE od.delivered!=1 AND od.orderno=" & OrderNo & ""

            Dim cmd As New MySqlCommand(strsql, Conn)
            cmd.CommandType = CommandType.Text
            datareader = cmd.ExecuteReader
            While datareader.Read
                lblOrderNo.Text = Double.Parse(datareader(0).ToString)
                lblDateOrdered.Text = datareader(1).ToString
            End While
            datareader.Dispose()
            closeconn()
            '###################################################################

            'retrieve data to grid
            If txtSearchOrder.Text.Trim.Length > 0 Then
                LoadRecept_Data(dgvOrderItems, OrderNo)
            End If
        End If
    End Sub

    'Loads database table to Datagridview
    Sub LoadRecept_Data(ByVal dgv As DataGridView, ByVal OrderNo As Double)

        Dim OrderTotals As Double = 0

        Dim strsql = "SELECT od.itemid,p.Description,od.quantityordered," _
                     & " od.unitprice,od.totalprice FROM orderdetails od" _
                     & " INNER JOIN products p ON p.Barcode=od.itemid" _
                     & " INNER JOIN ordermaster om ON om.orderno=od.orderno" _
                     & " WHERE om.orderno = " & OrderNo & ""
        Try
            Dim cmd As New MySqlCommand(strsql, Conn)
            cmd.CommandType = CommandType.Text
            Dim adapter As New MySqlDataAdapter(cmd)
            Dim table As New DataTable
            adapter.Fill(table)
            'Display the data.
            dgv.DataSource = table
            adapter.Dispose()
            closeconn()

            With dgv
                .Columns(0).HeaderText = "Barcode"
                .Columns(1).HeaderText = "Description"
                .Columns(2).HeaderText = "Qty Ordered"
                .Columns(3).HeaderText = "Unit Price"
                .Columns(4).HeaderText = "Total"
                .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells
                .Columns(1).DefaultCellStyle.WrapMode = DataGridViewTriState.True
                .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            End With
            For i = 0 To dgvOrderItems.RowCount - 1
                OrderTotals += Double.Parse(dgvOrderItems.Rows(i).Cells(4).Value)
            Next
            lblOrderTotals.Text = OrderTotals.ToString
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        printStatement()
    End Sub

    Private Sub printStatement()
        'Dim receiptNo As Long = Long.Parse(Now().Date.Day & Now().Date.Month & Now().Date.Year & Now().TimeOfDay.Hours & Now().TimeOfDay.Minutes)
        Dim dt As New DataTable
        Dim Barcode As String, Description As String, _
            Qty As Double, UnitPrice As Double, _
            TotalCost As Double

        Dim OrderTotals As Double = Double.Parse(lblOrderTotals.Text.ToString), _
            OrderDate As String = lblDateOrdered.Text.ToString, _
            OrderNo As Double = Double.Parse(txtSearchOrder.Text)

        With dt
            .Columns.Add("barcode")
            .Columns.Add("description")
            .Columns.Add("qty")
            .Columns.Add("unitprice")
            .Columns.Add("totals")
        End With

        With dgvOrderItems
            For i = 0 To dgvOrderItems.RowCount - 1
                Barcode = dgvOrderItems.Rows(i).Cells(0).Value.ToString
                Description = dgvOrderItems.Rows(i).Cells(1).Value.ToString
                Qty = Double.Parse(dgvOrderItems.Rows(i).Cells(2).Value.ToString)
                UnitPrice = Double.Parse(dgvOrderItems.Rows(i).Cells(3).Value.ToString)
                TotalCost = Double.Parse(dgvOrderItems.Rows(i).Cells(4).Value.ToString)
                'add rows
                dt.Rows.Add(Barcode, Description, Qty, UnitPrice, TotalCost)
            Next
        End With

        Try
            Dim rptDoc As CrystalDecisions.CrystalReports.Engine.ReportDocument
            rptDoc = New StockOrder
            rptDoc.SetDataSource(dt)
            rptDoc.SetParameterValue("OrderNo", OrderNo)
            rptDoc.SetParameterValue("TotalSales", OrderTotals)
            rptDoc.SetParameterValue("OrderDate", OrderDate)
            frmPrint.CRPrint.ReportSource = rptDoc
            frmPrint.CRPrint.RefreshReport()
            frmPrint.TopMost = True
            frmPrint.StartPosition = FormStartPosition.CenterScreen
            frmPrint.Show()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class