Imports System.Windows.Forms
Public Class mdiSales
    Dim stock As New inventory

    Private Sub ToolStripMenuItem2_Click(sender As Object, e As EventArgs)
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        'ASSIGN USER PRIVILEDGES CODE
        If Not Authorized(userid, TaskID.makesales) Then Exit Sub
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        With frmSales
            .MdiParent = Me
            .WindowState = FormWindowState.Maximized
            .StartPosition = FormStartPosition.CenterParent
            .Show()
            .Location = New Point(10, 10)
        End With
    End Sub

    Private Sub AddItemsToolStripMenuItem_Click(sender As Object, e As EventArgs)
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        'ASSIGN USER PRIVILEDGES CODE
        If Not Authorized(userid, TaskID.viewinventory) Then Exit Sub
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        With frmproducts
            .MdiParent = Me
            .StartPosition = FormStartPosition.CenterParent
            .WindowState = FormWindowState.Maximized
            .Show()
            .Location = New Point(10, 10)
        End With
    End Sub

    Private Sub UsersToolStripMenuItem1_Click(sender As Object, e As EventArgs)
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        'ASSIGN USER PRIVILEDGES CODE
        If Not Authorized(userid, TaskID.viewsystemuser) Then Exit Sub
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        With frmStaff
            .MdiParent = Me
            .WindowState = FormWindowState.Normal
            .StartPosition = FormStartPosition.CenterParent
            .Show()
            .Location = New Point(10, 10)
        End With
    End Sub

    Private Sub mdiAdmin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutMdi(MdiLayout.Cascade)     'Cascade all Mdi child windows

        'display current date and time
        ToolStripLabel1.Text = DateTime.Now
        ToolStripLabel6.Text = fullname
        stock.stockmanagement()
    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        ToolStripLabel2.Text = Date.Today
        ToolStripLabel1.Text = TimeOfDay
    End Sub


    Private Sub PlaceOrderToolStripMenuItem_Click(sender As Object, e As EventArgs)
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        'ASSIGN USER PRIVILEDGES CODE
        If Not Authorized(userid, TaskID.createorder) Then Exit Sub
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        With frmProductsOrder
            .MdiParent = Me
            .StartPosition = FormStartPosition.CenterParent
            .WindowState = FormWindowState.Maximized
            .Show()
        End With
    End Sub

    Private Sub CreditDetailsToolStripMenuItem_Click(sender As Object, e As EventArgs)
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        'ASSIGN USER PRIVILEDGES CODE
        If Not Authorized(userid, TaskID.sell_on_credit) Then Exit Sub
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        With frmCreditSales
            .MdiParent = Me
            .WindowState = FormWindowState.Normal
            .StartPosition = FormStartPosition.CenterParent
            .Show()
        End With
    End Sub

    Private Sub PriceListToolStripMenuItem1_Click(sender As Object, e As EventArgs)
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        'ASSIGN USER PRIVILEDGES CODE
        If Not Authorized(userid, TaskID.view_price_list) Then Exit Sub
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        With frmPricelist
            .TopMost = True
            .StartPosition = FormStartPosition.CenterParent
            .ShowDialog()
            .Location = New Point(10, 10)
        End With
    End Sub

    Private Sub ViewPlacedOrdersToolStripMenuItem_Click(sender As Object, e As EventArgs)
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        'ASSIGN USER PRIVILEDGES CODE
        If Not Authorized(userid, TaskID.vieworders) Then Exit Sub
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        With frmViewPlacedOrder
            .MdiParent = Me
            .StartPosition = FormStartPosition.CenterParent
            .WindowState = FormWindowState.Maximized
            .Show()
        End With
    End Sub

    Private Sub SuppliersToolStripMenuItem1_Click(sender As Object, e As EventArgs)
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        'ASSIGN USER PRIVILEDGES CODE
        If Not Authorized(userid, TaskID.viewsuppliers) Then Exit Sub
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        With frmSuppliers
            .MdiParent = Me
            .StartPosition = FormStartPosition.CenterParent
            .WindowState = FormWindowState.Maximized
            .Show()
            '.Location = New Point(10, 10)
        End With
    End Sub


    Private Sub LogoutToolStripMenuItem1_Click(sender As Object, e As EventArgs)
        Application.Exit()
    End Sub

    Private Sub ToolStripLogout_Click(sender As Object, e As EventArgs)
        Application.Exit()
    End Sub


    Private Sub LogOutToolStripMenuItem_Click(sender As Object, e As EventArgs)
        If (MsgBox("Are you sure you want to close the application completely?", MsgBoxStyle.YesNo, "Log out") = MsgBoxResult.Yes) Then
            Application.Exit()
        End If
    End Sub


    Private Sub CalculatorMenuItem_Click(sender As Object, e As EventArgs)
        'call system calculator
        Shell("C:\WINDOWS\system32\calc")
    End Sub

    Private Sub DetailedToolStripMenuItem_Click(sender As Object, e As EventArgs)
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        'ASSIGN USER PRIVILEDGES CODE
        If Not Authorized(userid, TaskID.receiveitems) Then Exit Sub
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        With frmMainDelivery
            .WindowState = FormWindowState.Maximized
            .StartPosition = FormStartPosition.CenterScreen
            .MdiParent = Me
            .Show()
        End With
    End Sub

    Private Sub MainToolStripMenuItem_Click(sender As Object, e As EventArgs)
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        'ASSIGN USER PRIVILEDGES CODE
        If Not Authorized(userid, TaskID.receiveitems) Then Exit Sub
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        With frmSimpleDeliveries
            .WindowState = FormWindowState.Normal
            .StartPosition = FormStartPosition.CenterScreen
            .MdiParent = Me
            .Show()
        End With
    End Sub


    Private Sub DamegedSpoiltProductsToolStripMenuItem_Click(sender As Object, e As EventArgs)
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        'ASSIGN USER PRIVILEDGES CODE
        If Not Authorized(userid, TaskID.view_product_voids) Then Exit Sub
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        With frmSpoiltItems
            .MdiParent = Me
            .WindowState = FormWindowState.Maximized
            .StartPosition = FormStartPosition.CenterScreen
            '.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
            .MaximizeBox = True
            .MinimizeBox = True
            .Show()
        End With
    End Sub

    Private Sub CreditPaymentsToolStripMenuItem_Click(sender As Object, e As EventArgs)
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        'ASSIGN USER PRIVILEDGES CODE
        If Not Authorized(userid, TaskID.receive_credit_payment) Then Exit Sub
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        With frmCreditors
            .StartPosition = FormStartPosition.WindowsDefaultLocation
            .WindowState = FormWindowState.Normal
            .MdiParent = Me
            .Show()
            .Location = New Point(10, 10)
        End With
    End Sub


    Private Sub EmployeesToolStripMenuItem_Click(sender As Object, e As EventArgs)
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        'ASSIGN USER PRIVILEDGES CODE
        If Not Authorized(userid, TaskID.viewsystemuser) Then Exit Sub
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        'call frmStaff
        With frmStaff
            .MdiParent = Me
            .WindowState = FormWindowState.Maximized
            .StartPosition = FormStartPosition.CenterScreen
            .Show()
        End With
    End Sub

    Private Sub UserRightsToolStripMenuItem_Click(sender As Object, e As EventArgs)
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        'ASSIGN USER PRIVILEDGES CODE
        If Not Authorized(userid, TaskID.view_user_rights) Then Exit Sub
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        With frmUserRights
            .MdiParent = Me
            .StartPosition = FormStartPosition.CenterScreen
            .WindowState = FormWindowState.Normal
            .Show()
        End With
    End Sub

    Private Sub ViewStockReportsToolStripMenuItem_Click(sender As Object, e As EventArgs)
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        'ASSIGN USER PRIVILEDGES CODE
        If Not Authorized(userid, TaskID.view_stock_balances) Then Exit Sub
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        With frmStock
            .MdiParent = Me
            .StartPosition = FormStartPosition.WindowsDefaultLocation
            .WindowState = FormWindowState.Maximized
            .Show()
            .Location = New Point(10, 10)
        End With
    End Sub

    Private Sub ViewSalesReportsToolStripMenuItem_Click(sender As Object, e As EventArgs)
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        'ASSIGN USER PRIVILEDGES CODE
        If Not Authorized(userid, TaskID.view_all_sales_reports) Then Exit Sub
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        With frmSalesReports
            .MdiParent = Me
            .StartPosition = FormStartPosition.CenterScreen
            .WindowState = FormWindowState.Maximized
            .Show()
            .Location = New Point(10, 10)
        End With
    End Sub

    Private Sub ViewSalesReportsToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ViewSalesReportsToolStripMenuItem1.Click
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        'ASSIGN USER PRIVILEDGES CODE
        If Not Authorized(userid, TaskID.view_all_sales_reports) Then Exit Sub
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        With frmSalesReports
            .MdiParent = Me
            .StartPosition = FormStartPosition.WindowsDefaultLocation
            .WindowState = FormWindowState.Maximized
            .Show()
            .Location = New Point(10, 10)
        End With
    End Sub

    Private Sub ReceiveDeliveriesToolStripMenuItem_Click(sender As Object, e As EventArgs)
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        'ASSIGN USER PRIVILEDGES CODE
        If Not Authorized(userid, TaskID.receiveitems) Then Exit Sub
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        With frmSimpleDeliveries
            .WindowState = FormWindowState.Normal
            .StartPosition = FormStartPosition.CenterScreen
            .MdiParent = Me
            .Show()
        End With
    End Sub

    Private Sub SetPricesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SetPricesToolStripMenuItem.Click
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        'ASSIGN USER PRIVILEDGES CODE
        If Not Authorized(userid, TaskID.view_price_list) Then Exit Sub
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        With frmPricelist
            .MdiParent = Me
            '.TopMost = True
            .StartPosition = FormStartPosition.WindowsDefaultLocation
            .WindowState = FormWindowState.Maximized
            .Show()
            '.Location = New Point(400, 150)
        End With
    End Sub

    Private Sub PriceSettingsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PriceSettingsToolStripMenuItem.Click
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        'ASSIGN USER PRIVILEDGES CODE
        If Not Authorized(userid, TaskID.adjust_price_rate) Then Exit Sub
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        With frmPriceRateSettings
            .TopMost = True
            .StartPosition = FormStartPosition.CenterScreen
            .ShowDialog()
            '.Location = New Point(450, 370)
        End With
    End Sub

    Private Sub MainDeliveriesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MainDeliveriesToolStripMenuItem.Click
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        'ASSIGN USER PRIVILEDGES CODE
        If Not Authorized(userid, TaskID.receiveitems) Then Exit Sub
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        With frmMainDelivery
            .WindowState = FormWindowState.Maximized
            .StartPosition = FormStartPosition.CenterScreen
            .MdiParent = Me
            .Show()
        End With
    End Sub

    Private Sub SimpleDeliveriesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SimpleDeliveriesToolStripMenuItem.Click
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        'ASSIGN USER PRIVILEDGES CODE
        If Not Authorized(userid, TaskID.receive_credit_payment) Then Exit Sub
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        With frmSimpleDeliveries
            .FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
            .WindowState = FormWindowState.Maximized
            .StartPosition = FormStartPosition.CenterScreen
            .MdiParent = Me
            .Show()
        End With
    End Sub

    Private Sub POSMenuItem_Click(sender As Object, e As EventArgs) Handles POSMenuItem.Click
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        'ASSIGN USER PRIVILEDGES CODE
        If Not Authorized(userid, TaskID.makesales) Then Exit Sub
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        With frmSales
            .MdiParent = Me
            .WindowState = FormWindowState.Maximized
            .StartPosition = FormStartPosition.WindowsDefaultLocation
            .Show()
            .Location = New Point(10, 10)
        End With
    End Sub

    Private Sub CalculatorMenuItem_Click_1(sender As Object, e As EventArgs) Handles CalculatorMenuItem.Click
        'call system calculator
        Shell("C:\WINDOWS\system32\calc")
    End Sub

    Private Sub UserRightsToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles UserRightsToolStripMenuItem1.Click
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        'ASSIGN USER PRIVILEDGES CODE
        If Not Authorized(userid, TaskID.view_user_rights) Then Exit Sub
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        With frmUserRights
            .TopMost = True
            .MaximizeBox = False
            .MinimizeBox = False
            '.MdiParent = Me
            .StartPosition = FormStartPosition.CenterScreen
            .WindowState = FormWindowState.Normal
            .Show()
            .Location = New Point(500, 150)
        End With
    End Sub

    Private Sub ProductsMenuItem_Click(sender As Object, e As EventArgs) Handles ProductsMenuItem.Click
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        'ASSIGN USER PRIVILEDGES CODE
        If Not Authorized(userid, TaskID.viewinventory) Then Exit Sub
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        With frmproducts
            .MdiParent = Me
            .StartPosition = FormStartPosition.WindowsDefaultLocation
            .WindowState = FormWindowState.Maximized
            .Show()
            '.Location = New Point(10, 10)
        End With
    End Sub

    Private Sub SalesSettingsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SalesSettingsToolStripMenuItem.Click
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        'ASSIGN USER PRIVILEDGES CODE
        If Not Authorized(userid, TaskID.change_sale_settings) Then Exit Sub
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&

        With frmSettings
            .TopMost = True
            .WindowState = FormWindowState.Normal
            .StartPosition = FormStartPosition.CenterScreen
            .FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
            .Show()
        End With
    End Sub

    Private Sub AddSystemUsersToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AddSystemUsersToolStripMenuItem.Click
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        'ASSIGN USER PRIVILEDGES CODE
        If Not Authorized(userid, TaskID.viewsystemuser) Then Exit Sub
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        'call frmStaff
        With frmStaff
            .MdiParent = Me
            .WindowState = FormWindowState.Maximized
            .StartPosition = FormStartPosition.CenterScreen
            .Show()
        End With
    End Sub

    Private Sub MakeItemsOrderToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MakeItemsOrderToolStripMenuItem.Click
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        'ASSIGN USER PRIVILEDGES CODE
        If Not Authorized(userid, TaskID.createorder) Then Exit Sub
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        With frmProductsOrder
            .MdiParent = Me
            .WindowState = FormWindowState.Maximized
            .StartPosition = FormStartPosition.CenterScreen
            .Show()
            .Location = New Point(10, 10)
        End With
    End Sub

    Private Sub DamagedSpoiltItemsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DamagedSpoiltItemsToolStripMenuItem.Click
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        'ASSIGN USER PRIVILEDGES CODE
        If Not Authorized(userid, TaskID.view_product_voids) Then Exit Sub
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        With frmSpoiltItems
            .MdiParent = Me
            .WindowState = FormWindowState.Maximized
            .StartPosition = FormStartPosition.CenterScreen
            '.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
            .MaximizeBox = True
            .MinimizeBox = True
            .Show()
        End With
    End Sub

    Private Sub SalesReturnsToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles SalesReturnsToolStripMenuItem1.Click
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        'ASSIGN USER PRIVILEDGES CODE
        If Not Authorized(userid, TaskID.view_sales_returns) Then Exit Sub
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        With frmSalesReturns
            .MdiParent = Me
            .StartPosition = FormStartPosition.WindowsDefaultLocation
            .WindowState = FormWindowState.Maximized
            .FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
            .Show()
            .Location = New Point(10, 10)
        End With
    End Sub


    Private Sub HelpToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HelpToolStripMenuItem.Click
        With frmHelp
            .TopMost = True
            '.MdiParent = Me
            .StartPosition = FormStartPosition.CenterScreen
            .Show()
            '.Location = New Point(10, 10)
        End With
    End Sub

    Private Sub SMS_Timer_Tick(sender As Object, e As EventArgs) Handles SMS_Timer.Tick
        'Upesi_Queue_SMS()
    End Sub


    Private Sub DataBackupToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DataBackupToolStripMenuItem.Click
        With frmBackup
            .StartPosition = FormStartPosition.CenterScreen
            .WindowState = FormWindowState.Normal
            .TopMost = True
            .Show()
            '.Location = New Point(10, 10)
        End With
    End Sub

    Private Sub ReceiptHeaderToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReceiptHeaderToolStripMenuItem.Click
        With frmReceiptSettings
            .TopMost = True
            .MaximizeBox = False
            .MinimizeBox = False
            '.MdiParent = Me
            .StartPosition = FormStartPosition.CenterScreen
            .WindowState = FormWindowState.Normal
            .Show()
        End With
    End Sub

    Private Sub StockValueToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles StockValueToolStripMenuItem1.Click
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        'ASSIGN USER PRIVILEDGES CODE
        If Not Authorized(userid, TaskID.view_stock_value) Then Exit Sub
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        With frmStockValue
            .MdiParent = Me
            .StartPosition = FormStartPosition.WindowsDefaultLocation
            .WindowState = FormWindowState.Maximized
            .Show()
            .Location = New Point(10, 10)
        End With
    End Sub

    Private Sub StockBalancesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles StockBalancesToolStripMenuItem.Click
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        'ASSIGN USER PRIVILEDGES CODE
        If Not Authorized(userid, TaskID.view_stock_balances) Then Exit Sub
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        With frmStock
            .MdiParent = Me
            .StartPosition = FormStartPosition.WindowsDefaultLocation
            .WindowState = FormWindowState.Maximized
            .Show()
            .Location = New Point(10, 10)
        End With
    End Sub

    Private Sub StockReconciliationReportsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles StockReconciliationReportsToolStripMenuItem.Click
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        'ASSIGN USER PRIVILEDGES CODE
        If Not Authorized(userid, TaskID.view_reconciliation_reports) Then Exit Sub
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        With frmStockReconciliationReport
            .MdiParent = Me
            .StartPosition = FormStartPosition.CenterScreen
            .WindowState = FormWindowState.Maximized
            .Show()
            '.Location = New Point(10, 10)
        End With
    End Sub

    Private Sub SupplierRegistryToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SupplierRegistryToolStripMenuItem.Click
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        'ASSIGN USER PRIVILEDGES CODE
        If Not Authorized(userid, TaskID.viewsuppliers) Then Exit Sub
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        With frmSuppliers
            .MdiParent = Me
            .StartPosition = FormStartPosition.CenterParent
            .WindowState = FormWindowState.Maximized
            .Show()
            .Location = New Point(10, 10)
        End With
    End Sub

    Private Sub ReceiveItemsAndEnterBillToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReceiveItemsAndEnterBillToolStripMenuItem.Click
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        'ASSIGN USER PRIVILEDGES CODE
        If Not Authorized(userid, TaskID.receiveitems) Then Exit Sub
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        With frmAdvancedDeliveries
            .WindowState = FormWindowState.Maximized
            .StartPosition = FormStartPosition.CenterScreen
            .MdiParent = Me
            .Show()
        End With
    End Sub

    Private Sub CreatePurchaseOrdersToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CreatePurchaseOrdersToolStripMenuItem.Click
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        'ASSIGN USER PRIVILEDGES CODE
        If Not Authorized(userid, TaskID.createorder) Then Exit Sub
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        With frmProductsOrder
            .MdiParent = Me
            .WindowState = FormWindowState.Maximized
            .StartPosition = FormStartPosition.CenterScreen
            .Show()
            .Location = New Point(10, 10)
        End With
    End Sub

    Private Sub BanksToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BanksToolStripMenuItem.Click
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        'ASSIGN USER PRIVILEDGES CODE
        If Not Authorized(userid, TaskID.view_banks) Then Exit Sub
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&

        With frmBanks
            .WindowState = FormWindowState.Normal
            .MdiParent = Me
            .StartPosition = FormStartPosition.CenterScreen
            .Show()
            '.Location = New Point(10, 10)
        End With
    End Sub

    Private Sub SupplierInvoicePaymentsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SupplierInvoicePaymentsToolStripMenuItem.Click
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        'ASSIGN USER PRIVILEDGES CODE
        If Not Authorized(userid, TaskID.supplierpayment) Then Exit Sub
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        With frmSupplierPayments
            .WindowState = FormWindowState.Maximized
            .StartPosition = FormStartPosition.CenterScreen
            .MdiParent = Me
            .Show()
        End With
    End Sub

    Private Sub AdvancedStockReportToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AdvancedStockReportToolStripMenuItem.Click
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        'ASSIGN USER PRIVILEDGES CODE
        If Not Authorized(userid, TaskID.view_main_stock_reports) Then Exit Sub
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        With frmStockMainReports
            .MdiParent = Me
            .StartPosition = FormStartPosition.WindowsDefaultLocation
            .WindowState = FormWindowState.Maximized
            .Show()
            .Location = New Point(10, 10)
        End With
    End Sub

    Private Sub SaleReturnsReportToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaleReturnsReportToolStripMenuItem.Click
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        'ASSIGN USER PRIVILEDGES CODE
        If Not Authorized(userid, TaskID.view_sale_returns_report) Then Exit Sub
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        With frmSaleReturnsReport
            .MdiParent = Me
            .StartPosition = FormStartPosition.WindowsDefaultLocation
            .WindowState = FormWindowState.Maximized
            .Show()
            '.Location = New Point(10, 10)
        End With
    End Sub

    Private Sub ExpensesRegistryToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExpensesRegistryToolStripMenuItem.Click
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        'ASSIGN USER PRIVILEDGES CODE
        If Not Authorized(userid, TaskID.create_expenses) Then Exit Sub
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        With frmExpensess
            .MdiParent = Me
            .StartPosition = FormStartPosition.CenterScreen
            .WindowState = FormWindowState.Normal
            .Show()
            '.Location = New Point(10, 10)
        End With
    End Sub

    Private Sub ExpensesReportsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExpensesReportsToolStripMenuItem.Click
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        'ASSIGN USER PRIVILEDGES CODE
        If Not Authorized(userid, TaskID.view_expense_reports) Then Exit Sub
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        With frmExpensesReport
            .MdiParent = Me
            .StartPosition = FormStartPosition.CenterScreen
            .WindowState = FormWindowState.Maximized
            .Show()
            '.Location = New Point(10, 10)
        End With
    End Sub

    Private Sub VATSettingsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles VATSettingsToolStripMenuItem.Click
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        'ASSIGN USER PRIVILEDGES CODE
        If Not Authorized(userid, TaskID.view_vat_rates) Then Exit Sub
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        With frmVATSettings
            .TopMost = True
            .MaximizeBox = False
            .MinimizeBox = False
            '.MdiParent = Me
            .StartPosition = FormStartPosition.CenterScreen
            .WindowState = FormWindowState.Normal
            .Show()
        End With
    End Sub

    Private Sub Timer_License_Tick(sender As Object, e As EventArgs) Handles Timer_License.Tick
        ' Use this
        Check_License(MenuStrip1, MenuStrip)
    End Sub

    Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click
        With frmLicenseLogins
            .TopMost = True
            .StartPosition = FormStartPosition.CenterScreen
            .WindowState = FormWindowState.Normal
            .Show()
        End With
    End Sub

    Private Sub RegisterSystemUsersToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RegisterSystemUsersToolStripMenuItem.Click
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        'ASSIGN USER PRIVILEDGES CODE
        If Not Authorized(userid, TaskID.viewsystemuser) Then Exit Sub
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        'call frmStaff
        With frmStaff
            .MdiParent = Me
            .WindowState = FormWindowState.Maximized
            .StartPosition = FormStartPosition.CenterScreen
            .Show()
        End With
    End Sub

    Private Sub AssignUserRightsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AssignUserRightsToolStripMenuItem.Click
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        'ASSIGN USER PRIVILEDGES CODE
        If Not Authorized(userid, TaskID.view_user_rights) Then Exit Sub
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

    Private Sub SupplierProformaInvoiceToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SupplierProformaInvoiceToolStripMenuItem.Click
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        'ASSIGN USER PRIVILEDGES CODE
        If Not Authorized(userid, TaskID.view_proforma_invoice) Then Exit Sub
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        With frmProformaInvoice
            .MdiParent = Me
            .StartPosition = FormStartPosition.CenterScreen
            .WindowState = FormWindowState.Maximized
            .Show()
            '.Location = New Point(10, 10)
        End With
    End Sub

    Private Sub ToolStripLabel4_Click(sender As Object, e As EventArgs) Handles ToolStripLabel4.Click
        ToolStripMenuItem1_Click(sender, e)
    End Sub

    Private Sub ItemCategoriesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ItemCategoriesToolStripMenuItem.Click
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        'ASSIGN USER PRIVILEDGES CODE
        If Not Authorized(userid, TaskID.create_inventory_categories) Then Exit Sub
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        With frmCategories
            .WindowState = FormWindowState.Normal
            .TopMost = True
            .StartPosition = FormStartPosition.CenterScreen
            .Show()
            .Location = New Point(500, 180)
        End With
    End Sub

    Private Sub RegisterBranchesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RegisterBranchesToolStripMenuItem.Click
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        'ASSIGN USER PRIVILEDGES CODE
        If Not Authorized(userid, TaskID.create_sales_invoices) Then Exit Sub
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        With frmInvoices
            .MdiParent = Me
            .StartPosition = FormStartPosition.WindowsDefaultLocation
            .WindowState = FormWindowState.Maximized
            .Show()
        End With
    End Sub

    Private Sub InvoiceReportsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles InvoiceReportsToolStripMenuItem.Click
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        'ASSIGN USER PRIVILEDGES CODE
        If Not Authorized(userid, TaskID.view_sales_invoiced_reports) Then Exit Sub
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        With frmInvoiceReports
            .MdiParent = Me
            .StartPosition = FormStartPosition.WindowsDefaultLocation
            .WindowState = FormWindowState.Maximized
            .Show()
        End With
    End Sub

    Private Sub RegisterFlowToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RegisterFlowToolStripMenuItem.Click
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        'ASSIGN USER PRIVILEDGES CODE
        If Not Authorized(userid, TaskID.create_cash_running_totals) Then Exit Sub
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        With frmCashRunningTotals
            .MdiParent = Me
            .StartPosition = FormStartPosition.WindowsDefaultLocation
            .WindowState = FormWindowState.Maximized
            .Show()
            '.Location = New Point(300, 180)
        End With
    End Sub

    Private Sub GenerateReportsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GenerateReportsToolStripMenuItem.Click
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        'ASSIGN USER PRIVILEDGES CODE
        If Not Authorized(userid, TaskID.view_cash_running_totals) Then Exit Sub
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        With frmCashRunningTotalsRpt
            .MdiParent = Me
            .StartPosition = FormStartPosition.CenterScreen
            .WindowState = FormWindowState.Maximized
            .Show()
            '.Location = New Point(10, 10)
        End With
    End Sub

    Private Sub CashSalesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CashSalesToolStripMenuItem.Click
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        'ASSIGN USER PRIVILEDGES CODE
        If Not Authorized(userid, TaskID.view_all_sales_reports) Then Exit Sub
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        With frmSalesReports
            .MdiParent = Me
            .StartPosition = FormStartPosition.WindowsDefaultLocation
            .WindowState = FormWindowState.Maximized
            .Show()
            .Location = New Point(10, 10)
        End With
    End Sub

    Private Sub CreditSalesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CreditSalesToolStripMenuItem.Click
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        'ASSIGN USER PRIVILEDGES CODE
        If Not Authorized(userid, TaskID.view_credit_reports) Then Exit Sub
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        With frmCreditReports
            .MdiParent = Me
            .WindowState = FormWindowState.Maximized
            .StartPosition = FormStartPosition.WindowsDefaultLocation
            .Show()
        End With
    End Sub

    Private Sub ReceivedItemsReportToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReceivedItemsReportToolStripMenuItem.Click
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        'ASSIGN USER PRIVILEDGES CODE
        If Not Authorized(userid, TaskID.view_supplier_deliveries) Then Exit Sub
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        With frmSupplierDeliveriesReport
            .WindowState = FormWindowState.Maximized
            .StartPosition = FormStartPosition.CenterScreen
            .MdiParent = Me
            .Show()
        End With
    End Sub

    Private Sub PaymentReportsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PaymentReportsToolStripMenuItem.Click
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        'ASSIGN USER PRIVILEDGES CODE
        If Not Authorized(userid, TaskID.supplierpaymentreport) Then Exit Sub
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        With frmSupplierInvoicePayReports
            .MdiParent = Me
            .StartPosition = FormStartPosition.WindowsDefaultLocation
            .WindowState = FormWindowState.Maximized
            .Show()
            '.Location = New Point(10, 10)
        End With
    End Sub

    Private Sub StockRunningTotalsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles StockRunningTotalsToolStripMenuItem.Click
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        'ASSIGN USER PRIVILEDGES CODE
        If Not Authorized(userid, TaskID.view_main_stock_reports) Then Exit Sub
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        With frmStockMainReports
            .MdiParent = Me
            .StartPosition = FormStartPosition.WindowsDefaultLocation
            .WindowState = FormWindowState.Maximized
            .Show()
            .Location = New Point(10, 10)
        End With
    End Sub

    Private Sub ReconciliationReportsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReconciliationReportsToolStripMenuItem.Click
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        'ASSIGN USER PRIVILEDGES CODE
        If Not Authorized(userid, TaskID.view_reconciliation_reports) Then Exit Sub
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        With frmStockReconciliationReport
            .MdiParent = Me
            .StartPosition = FormStartPosition.WindowsDefaultLocation
            .WindowState = FormWindowState.Maximized
            .Show()
            '.Location = New Point(10, 10)
        End With
    End Sub

    Private Sub DailyExpensesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DailyExpensesToolStripMenuItem.Click
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        'ASSIGN USER PRIVILEDGES CODE
        If Not Authorized(userid, TaskID.view_expense_reports) Then Exit Sub
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        With frmExpensesReport
            .MdiParent = Me
            .StartPosition = FormStartPosition.WindowsDefaultLocation
            .WindowState = FormWindowState.Maximized
            .Show()
            '.Location = New Point(10, 10)
        End With
    End Sub

    Private Sub DetailedCreditReportsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DetailedCreditReportsToolStripMenuItem.Click
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        'ASSIGN USER PRIVILEDGES CODE
        If Not Authorized(userid, TaskID.view_credit_reports) Then Exit Sub
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        With frmDetailedCreditReports
            .MdiParent = Me
            .WindowState = FormWindowState.Maximized
            .StartPosition = FormStartPosition.WindowsDefaultLocation
            .Show()
        End With
    End Sub

    Private Sub BankDepositsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BankDepositsToolStripMenuItem.Click
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        'ASSIGN USER PRIVILEDGES CODE
        If Not Authorized(userid, TaskID.register_bank_deposits) Then Exit Sub
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        With frmBankDeposits
            .TopMost = True
            '.MdiParent = Me
            .StartPosition = FormStartPosition.CenterScreen
            .WindowState = FormWindowState.Normal
            .Show()
            .Location = New Point(350, 180)
        End With
    End Sub

    Private Sub BankDepositsToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles BankDepositsToolStripMenuItem1.Click
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        'ASSIGN USER PRIVILEDGES CODE
        If Not Authorized(userid, TaskID.view_bank_deposits) Then Exit Sub
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        With frmBankDepositReports
            .MdiParent = Me
            .StartPosition = FormStartPosition.WindowsDefaultLocation
            .WindowState = FormWindowState.Maximized
            .Show()
            .Location = New Point(10, 10)
        End With
    End Sub

    Private Sub CashRunningTotalsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CashRunningTotalsToolStripMenuItem.Click
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        'ASSIGN USER PRIVILEDGES CODE
        If Not Authorized(userid, TaskID.view_cash_running_totals) Then Exit Sub
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        With frmCashRunningTotalsRpt
            .MdiParent = Me
            .StartPosition = FormStartPosition.WindowsDefaultLocation
            .WindowState = FormWindowState.Maximized
            .Show()
            '.Location = New Point(10, 10)
        End With
    End Sub

    Private Sub RegisterOpeningBalanceToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RegisterOpeningBalanceToolStripMenuItem.Click
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        'ASSIGN USER PRIVILEDGES CODE
        If Not Authorized(userid, TaskID.register_opening_balance) Then Exit Sub
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        With frmOpeningBalance
            .TopMost = True
            '.MdiParent = Me
            .StartPosition = FormStartPosition.CenterScreen
            .WindowState = FormWindowState.Normal
            .Show()
            .Location = New Point(400, 190)
        End With
    End Sub

    Private Sub SalesReturnsReportsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SalesReturnsReportsToolStripMenuItem.Click
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        'ASSIGN USER PRIVILEDGES CODE
        If Not Authorized(userid, TaskID.view_sale_returns_report) Then Exit Sub
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        With frmSaleReturnsReport
            .MdiParent = Me
            .StartPosition = FormStartPosition.WindowsDefaultLocation
            .WindowState = FormWindowState.Maximized
            .Show()
            '.Location = New Point(10, 10)
        End With
    End Sub

    Private Sub StockValueReportsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles StockValueReportsToolStripMenuItem.Click
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        'ASSIGN USER PRIVILEDGES CODE
        If Not Authorized(userid, TaskID.view_stock_value) Then Exit Sub
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        With frmStockValue
            .MdiParent = Me
            .StartPosition = FormStartPosition.WindowsDefaultLocation
            .WindowState = FormWindowState.Maximized
            .Show()
            .Location = New Point(10, 10)
        End With
    End Sub

    Private Sub StockMovementStatementToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles StockMovementStatementToolStripMenuItem.Click
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        'ASSIGN USER PRIVILEDGES CODE
        If Not Authorized(userid, TaskID.view_main_stock_reports) Then Exit Sub
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        With frmStockMovementStatement
            .MdiParent = Me
            .StartPosition = FormStartPosition.WindowsDefaultLocation
            .WindowState = FormWindowState.Maximized
            .Show()
            .Location = New Point(10, 10)
        End With
    End Sub

    Private Sub DetailedSalesReportsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DetailedSalesReportsToolStripMenuItem.Click
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        'ASSIGN USER PRIVILEDGES CODE
        If Not Authorized(userid, TaskID.view_all_sales_reports) Then Exit Sub
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        With frmDetailedSalesReports
            .MdiParent = Me
            .WindowState = FormWindowState.Maximized
            .StartPosition = FormStartPosition.WindowsDefaultLocation
            .Show()
        End With
    End Sub

    Private Sub CashFlowSummaryToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CashFlowSummaryToolStripMenuItem.Click
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        'ASSIGN USER PRIVILEDGES CODE
        If Not Authorized(userid, TaskID.create_cash_running_totals) Then Exit Sub
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        With frmCashflowSummarised
            .MdiParent = Me
            .StartPosition = FormStartPosition.CenterScreen
            .WindowState = FormWindowState.Normal
            .Show()
        End With
    End Sub

    Private Sub CashFlowReportsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CashFlowReportsToolStripMenuItem.Click
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        'ASSIGN USER PRIVILEDGES CODE
        If Not Authorized(userid, TaskID.view_cash_running_totals) Then Exit Sub
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        With frmCashFlowReports
            .MdiParent = Me
            .StartPosition = FormStartPosition.WindowsDefaultLocation
            .WindowState = FormWindowState.Maximized
            .Show()
            '.Location = New Point(10, 10)
        End With
    End Sub

    Private Sub ViewPlacedOrdersToolStripMenuItem_Click_1(sender As Object, e As EventArgs) Handles ViewPlacedOrdersToolStripMenuItem.Click
        With frmViewPlacedOrder
            .MdiParent = Me
            .WindowState = FormWindowState.Maximized
            .Show()
        End With
    End Sub

    Private Sub LogOutToolStripMenuItem_Click_1(sender As Object, e As EventArgs) Handles LogOutToolStripMenuItem.Click
        If (MsgBox("Are you sure you want to close the application completely?", MsgBoxStyle.YesNo, "Strawberry System") = MsgBoxResult.Yes) Then
            Application.Exit()
        End If
    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        If (MsgBox("Are you sure you want to close the application completely?", MsgBoxStyle.YesNo, "Strawberry System") = MsgBoxResult.Yes) Then
            Application.Exit()
        End If
    End Sub

    Private Sub InvoicePaymentToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles InvoicePaymentToolStripMenuItem.Click
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        'ASSIGN USER PRIVILEDGES CODE
        If Not Authorized(userid, TaskID.supplierpaymentreport) Then Exit Sub
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        With frmSupplierInvoicePayReports
            .MdiParent = Me
            .StartPosition = FormStartPosition.WindowsDefaultLocation
            .WindowState = FormWindowState.Maximized
            .Show()
            '.Location = New Point(10, 10)
        End With
    End Sub

    Private Sub CashPaymentToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CashPaymentToolStripMenuItem.Click
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        'ASSIGN USER PRIVILEDGES CODE
        If Not Authorized(userid, TaskID.supplierpaymentreport) Then Exit Sub
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        With frmSupplierFullPayReports
            .MdiParent = Me
            .StartPosition = FormStartPosition.WindowsDefaultLocation
            .WindowState = FormWindowState.Maximized
            .Show()
            '.Location = New Point(10, 10)
        End With
    End Sub

    Private Sub ViewOrderedItemsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ViewOrderedItemsToolStripMenuItem.Click
        With frmViewPlacedOrder
            .MdiParent = Me
            .StartPosition = FormStartPosition.WindowsDefaultLocation
            .WindowState = FormWindowState.Maximized
            .Show()
            '.Location = New Point(10, 10)
        End With
    End Sub

    Private Sub StockExpiryReportsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles StockExpiryReportsToolStripMenuItem.Click
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        'ASSIGN USER PRIVILEDGES CODE
        If Not Authorized(userid, TaskID.view_all_sales_reports) Then Exit Sub
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        With frmExpiryReport
            .MdiParent = Me
            .StartPosition = FormStartPosition.WindowsDefaultLocation
            .WindowState = FormWindowState.Maximized
            .Show()
            .Location = New Point(10, 10)
        End With
    End Sub
End Class