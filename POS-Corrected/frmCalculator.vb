Public Class frmCalculator
    Dim num1, num2 As Decimal
    Dim operan As Integer
    Dim operanselected As Boolean = False
  
    Private Sub btn_7_Click(sender As Object, e As EventArgs) Handles btn_7.Click

        If lbl_Display.Text <> "0" Then
            lbl_Display.Text += "7"
        Else
            lbl_Display.Text = "7"
        End If
    End Sub

    Private Sub btn_8_Click(sender As Object, e As EventArgs) Handles btn_8.Click
        If lbl_Display.Text <> "0" Then
            lbl_Display.Text += "8"
        Else
            lbl_Display.Text = "8"
        End If
    End Sub

    Private Sub btn_9_Click(sender As Object, e As EventArgs) Handles btn_9.Click
        If lbl_Display.Text <> "0" Then
            lbl_Display.Text += "9"
        Else
            lbl_Display.Text = "9"
        End If
    End Sub

    Private Sub btn_4_Click(sender As Object, e As EventArgs) Handles btn_4.Click
        'This CODE handles btn_4 click on the calculator

        If lbl_Display.Text <> "0" Then
            lbl_Display.Text += "4"
        Else
            lbl_Display.Text = "4"
        End If
    End Sub

    Private Sub btn_5_Click(sender As Object, e As EventArgs) Handles btn_5.Click
        'This CODE handles btn_5 click on the calculator

        If lbl_Display.Text <> "0" Then
            lbl_Display.Text += "5"
        Else
            lbl_Display.Text = "5"
        End If
    End Sub

    Private Sub btn_6_Click(sender As Object, e As EventArgs) Handles btn_6.Click

        'This CODE handles btn_6 click on the calculator

        If lbl_Display.Text <> "0" Then
            lbl_Display.Text += "6"
        Else
            lbl_Display.Text = "6"
        End If

    End Sub

    Private Sub btn_1_Click(sender As Object, e As EventArgs) Handles btn_1.Click

        'This CODE handles btn_1 click on the calculator

        If lbl_Display.Text <> "0" Then
            lbl_Display.Text += "1"
        Else
            lbl_Display.Text = "1"
        End If

    End Sub

    Private Sub btn_2_Click(sender As Object, e As EventArgs) Handles btn_2.Click

        'This CODE handles btn_2 click on the calculator

        If lbl_Display.Text <> "0" Then
            lbl_Display.Text += "2"
        Else
            lbl_Display.Text = "2"
        End If
    End Sub

    Private Sub btn_3_Click(sender As Object, e As EventArgs) Handles btn_3.Click

        'This CODE handles btn_3 click on the calculator

        If lbl_Display.Text <> "0" Then
            lbl_Display.Text += "3"
        Else
            lbl_Display.Text = "3"
        End If
    End Sub

    Private Sub btn_zero_Click(sender As Object, e As EventArgs) Handles btn_zero.Click
        'This CODE handles btn_zero click on the calculator

        If lbl_Display.Text <> "0" Then
            lbl_Display.Text += "0"
        Else
            lbl_Display.Text = "0"
        End If
    End Sub

    Private Sub btn_plus_Click(sender As Object, e As EventArgs) Handles btn_plus.Click
        'This CODE handles btn_plus click on the calculator

        num1 = lbl_Display.Text
        lbl_Display.Text = "0"
        operanselected = True
        operan = 1
    End Sub

    Private Sub btn_minus_Click(sender As Object, e As EventArgs) Handles btn_minus.Click

        'This CODE handles btn_minus click on the calculator
        num1 = lbl_Display.Text
        lbl_Display.Text = "0"
        operanselected = True
        operan = 2

    End Sub

    Private Sub btn_multiplication_Click(sender As Object, e As EventArgs) Handles btn_multiplication.Click

        'This CODE handles btn_multiplication click on the calculator
        num1 = lbl_Display.Text
        lbl_Display.Text = "0"
        operanselected = True
        operan = 3

    End Sub

    Private Sub btn_division_Click(sender As Object, e As EventArgs) Handles btn_division.Click

        'This CODE handles btn_division click on the calculator
        num1 = lbl_Display.Text
        lbl_Display.Text = "0"
        operanselected = True
        operan = 5
    End Sub

    Private Sub btn_Mod_Click(sender As Object, e As EventArgs) Handles btn_Mod.Click

        'This CODE handles btn_Mod click on the calculator
        num1 = lbl_Display.Text
        lbl_Display.Text = "0"
        operanselected = True
        operan = 4

    End Sub

    Private Sub btn_Dot_Click(sender As Object, e As EventArgs) Handles btn_Dot.Click
        'This CODE handles btn_Dot click on the calculator
        If InStr(lbl_Display.Text, ".") Then
            lbl_Display.Text = lbl_Display.Text + "."
        End If
    End Sub

    Private Sub btn_clear_Click(sender As Object, e As EventArgs) Handles btn_clear.Click
        'This CODE handles btn_Clear click on the calculator
        lbl_Display.Text = ""
        lbl_Display.Text = "0"
    End Sub

    Private Sub btn_equals_Click(sender As Object, e As EventArgs) Handles btn_equals.Click

        'This CODE handles btn_Equals click on the calculator
        If operanselected = True Then
            num2 = lbl_Display.Text
            If operan = 1 Then
                lbl_Display.Text = num1 + num2
            ElseIf operan = 2 Then
                lbl_Display.Text = num1 - num2
            ElseIf operan = 3 Then
                lbl_Display.Text = num1 * num2
            ElseIf operan = 4 Then
                lbl_Display.Text = num1 Mod num2
            Else

                If num2 = "0" Then
                    lbl_Display.Text = "Error!"
                Else
                    lbl_Display.Text = num1 / num2
                End If

            End If
            operanselected = False

        End If

    End Sub

  

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        'closes the calculator
        Me.Close()
    End Sub

    'key press code on digit number 7
    Private Sub btn_7_KeyDown(sender As Object, e As KeyEventArgs) Handles btn_7.KeyDown
        If e.KeyCode = Keys.D7 Then

            If lbl_Display.Text <> "0" Then
                lbl_Display.Text += "7"
            Else
                lbl_Display.Text = "7"
            End If
        ElseIf e.KeyCode = Keys.D8 Then
            If lbl_Display.Text <> "0" Then
                lbl_Display.Text += "8"
            Else
                lbl_Display.Text = "8"
            End If
        ElseIf e.KeyCode = Keys.D9 Then
            If lbl_Display.Text <> "0" Then
                lbl_Display.Text += "9"
            Else
                lbl_Display.Text = "9"
            End If
        End If

    End Sub

    'key press code on digit number 8
    Private Sub btn_8_KeyDown(sender As Object, e As KeyEventArgs) Handles btn_8.KeyDown
        If e.KeyCode = Keys.D8 Then
            If lbl_Display.Text <> "0" Then
                lbl_Display.Text += "8"
            Else
                lbl_Display.Text = "8"
            End If
        End If
    End Sub


    'key press code on digit number 9
    Private Sub btn_9_KeyDown(sender As Object, e As KeyEventArgs) Handles btn_9.KeyDown
        If e.KeyCode = Keys.D9 Then
            If lbl_Display.Text <> "0" Then
                lbl_Display.Text += "9"
            Else
                lbl_Display.Text = "9"
            End If
        End If
    End Sub

    'key press code on digit number 4
    Private Sub btn_4_KeyDown(sender As Object, e As KeyEventArgs) Handles btn_4.KeyDown
        If e.KeyCode = Windows.Forms.Keys.D4 Then
            If lbl_Display.Text <> "0" Then
                lbl_Display.Text += "4"
            Else
                lbl_Display.Text = "4"
            End If
        End If
    End Sub

    'key press code on digit number 5
    Private Sub btn_5_KeyDown(sender As Object, e As KeyEventArgs) Handles btn_5.KeyDown
        If e.KeyCode = Keys.D5 Then
            If lbl_Display.Text <> "0" Then
                lbl_Display.Text += "5"
            Else
                lbl_Display.Text = "5"
            End If
        End If
    End Sub

End Class