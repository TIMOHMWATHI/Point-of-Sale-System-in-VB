Imports MySql.Data
Imports MySql.Data.MySqlClient
Public Class frmReconcileStock
    Dim stock As New ClassStock

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim narration As String = "", newQty As Double
        Dim ItemCode As String, ItemName As String

        narration = "Reconciled Stock, ItemCode=" & lblBarcode.Text & ", " & vbNewLine & " " _
                    & " ItemName=" & lblItemName.Text & ",  " & vbNewLine & " " _
                    & " Old Qty=" & lblSystemQty.Text & ",  " & vbNewLine & " " _
                    & " New Qty=" & txtPhysicalQuantity.Text & ", " & vbNewLine & " " _
                    & " Qty Variance=" & lblVaraiance.Text.ToString & ""

        If txtPhysicalQuantity.Text.Trim.Length < 1 Then
            'error message
            Exit Sub
        Else
            newQty = Double.Parse(txtPhysicalQuantity.Text)
        End If

        ItemCode = lblBarcode.Text
        ItemName = lblItemName.Text

        'Update stock
        stock.insertStock(ItemCode, ItemName, lblSystemQty.Text, newQty, fullname, narration, 1)

        MessageBox.Show("Successfully reconciled", "Strawberry System", MessageBoxButtons.OK)
        frmStock.btnRefresh.PerformClick()
        closeconn()
        clearinputs()
        Me.Close()
    End Sub

    Sub compute_Qtyvariance()
        Dim oldqty As Double = 0, _
            newqty As Double = 0, _
            variance As Double = 0

        'Old Qty
        If lblSystemQty.Text = String.Empty Then
            oldqty = 0
        Else
            oldqty = Double.Parse(lblSystemQty.Text.ToString)
        End If

        'New Qty
        If txtPhysicalQuantity.Text = String.Empty Then
            newqty = 0
        Else
            newqty = Double.Parse(txtPhysicalQuantity.Text)
        End If

        'variance 
        variance = newqty - oldqty
        lblVaraiance.Text = Double.Parse(variance.ToString)
    End Sub

    'lear input areas
    Sub clearinputs()
        lblItemName.Text = ""
        lblBarcode.Text = ""
        lblSystemQty.Text = ""
        lblVaraiance.Text = ""
        txtPhysicalQuantity.Clear()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        clearinputs()
    End Sub

    Private Sub txtPhysicalQuantity_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPhysicalQuantity.KeyPress
        'only numbers input validation
        If e.KeyChar <> ChrW(Keys.Back) Then
            If Char.IsNumber(e.KeyChar) Then
            Else
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub txtPhysicalQuantity_TextChanged(sender As Object, e As EventArgs) Handles txtPhysicalQuantity.TextChanged
        compute_Qtyvariance()
    End Sub

    Private Sub lblVaraiance_TextChanged(sender As Object, e As EventArgs) Handles lblVaraiance.TextChanged
        compute_Qtyvariance()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub frmReconcileStock_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class