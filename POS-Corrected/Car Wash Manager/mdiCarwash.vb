Public Class mdiCarwash

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        ToolStripLabel2.Text = Date.Today
        ToolStripLabel1.Text = TimeOfDay
    End Sub

    Private Sub LogOutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LogOutToolStripMenuItem.Click
        Application.Exit()
    End Sub

    Private Sub SystemUsersToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SystemUsersToolStripMenuItem.Click
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        'ASSIGN USER PRIVILEDGES CODE
        If Not Authorized(userid, TaskID.alterstaffdetails) Then Exit Sub
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        'call frmStaff
        With frmStaff
            .MdiParent = Me
            .WindowState = FormWindowState.Maximized
            .StartPosition = FormStartPosition.CenterScreen
            .Show()
        End With
    End Sub

    Private Sub UserRightsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UserRightsToolStripMenuItem.Click
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        'ASSIGN USER PRIVILEDGES CODE
        If Not Authorized(userid, TaskID.assignuserrights) Then Exit Sub
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        With frmUserRights
            .TopMost = True
            .MaximizeBox = False
            .MinimizeBox = False
            '.MdiParent = Me
            .StartPosition = FormStartPosition.CenterScreen
            .WindowState = FormWindowState.Normal
            .Show()
        End With
    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        Application.Exit()
    End Sub

    Private Sub DataBackupToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DataBackupToolStripMenuItem.Click
        With frmBackup
            .MdiParent = Me
            .StartPosition = FormStartPosition.CenterScreen
            .WindowState = FormWindowState.Normal
            '.TopMost = True
            .Show()
            '.Location = New Point(10, 10)
        End With
    End Sub

    Private Sub CalculatorToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CalculatorToolStripMenuItem.Click
        'call system calculator
        Shell("C:\WINDOWS\system32\calc")
    End Sub

    Private Sub ReportsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReportsToolStripMenuItem.Click
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        'ASSIGN USER PRIVILEDGES CODE
        If Not Authorized(userid, TaskID.viewsalesreport) Then Exit Sub
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        With frmCarwashReports
            .MdiParent = Me
            .StartPosition = FormStartPosition.WindowsDefaultLocation
            .WindowState = FormWindowState.Maximized
            .Show()
            .Location = New Point(10, 10)
        End With
    End Sub

    Private Sub SalesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SalesToolStripMenuItem.Click
        ''&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        ''ASSIGN USER PRIVILEDGES CODE
        'If Not Authorized(userid, TaskID.viewsalesreport) Then Exit Sub
        ''&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        With frmCarwashSales
            .MdiParent = Me
            .StartPosition = FormStartPosition.WindowsDefaultLocation
            .WindowState = FormWindowState.Maximized
            .Show()
            .Location = New Point(10, 10)
        End With
    End Sub

    Private Sub ServicesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ServicesToolStripMenuItem.Click
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        'ASSIGN USER PRIVILEDGES CODE
        If Not Authorized(userid, TaskID.addproductInventory) Then Exit Sub
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        With frmServicesReport
            .MdiParent = Me
            .StartPosition = FormStartPosition.CenterScreen
            .WindowState = FormWindowState.Normal
            .Show()
            '.Location = New Point(10, 10)
        End With
    End Sub

    Private Sub PayRollToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PayRollToolStripMenuItem.Click
        ''&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        ''ASSIGN USER PRIVILEDGES CODE
        'If Not Authorized(userid, TaskID.viewsalesreport) Then Exit Sub
        ''&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        With frmCarwashPayroll
            .MdiParent = Me
            .StartPosition = FormStartPosition.WindowsDefaultLocation
            .WindowState = FormWindowState.Maximized
            .Show()
            .Location = New Point(10, 10)
        End With
    End Sub

    Private Sub StaffToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles StaffToolStripMenuItem.Click
        ''&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        ''ASSIGN USER PRIVILEDGES CODE
        'If Not Authorized(userid, TaskID.viewsalesreport) Then Exit Sub
        ''&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        With frmCarwashEmployees
            .MdiParent = Me
            .StartPosition = FormStartPosition.CenterScreen
            .WindowState = FormWindowState.Normal
            .Show()
            '.Location = New Point(10, 10)
        End With
    End Sub

    Private Sub mdiCarwash_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Timer1.Start()
    End Sub

    Private Sub ServicePricesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ServicePricesToolStripMenuItem.Click
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        'ASSIGN USER PRIVILEDGES CODE
        If Not Authorized(userid, TaskID.setprice) Then Exit Sub
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        With frmPricelist
            .TopMost = True
            .StartPosition = FormStartPosition.CenterParent
            .ShowDialog()
            .Location = New Point(10, 10)
        End With
    End Sub
End Class