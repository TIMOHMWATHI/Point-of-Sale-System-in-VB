Imports MySql.Data
Imports MySql.Data.MySqlClient

Public Class frmPriceRateSettings
    Dim setting As New ClassPriceSettings
    Dim RetailPercent As Double, _
        WholesalePercent As Double

    Private Sub frmPriceRateSettings_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'reload data
        setting.LoadToGrid(dgvPriceRates)
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        'validate retail
        If (NUDRetail.Value < 0) Then
            MessageBox.Show("Invalid retail percent value", "Strawberry POS", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        Else
            RetailPercent = Double.Parse(NUDRetail.Value)
        End If

        'validate wholesale
        If (NUDWholesale.Value < 0) Then
            MessageBox.Show("Invalid wholesale percent value", "Strawberry POS", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        Else
            WholesalePercent = Double.Parse(NUDWholesale.Value)
        End If

        'save data
        setting.SetPricesRates(RetailPercent, WholesalePercent, Now(), userid, 1)
        'clear controls
        Rub()
        'reload data
        setting.LoadToGrid(dgvPriceRates)
    End Sub

    Sub Rub()
        NUDRetail.Value = 0
        NUDWholesale.Value = 0
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub NUDRetail_ValueChanged(sender As Object, e As EventArgs) Handles NUDRetail.ValueChanged

    End Sub
End Class