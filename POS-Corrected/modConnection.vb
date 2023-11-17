Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.Web
Imports System.Xml
Imports System.Net
Imports System.Text.RegularExpressions
Imports System.IO
'######### 
'send email
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.Net.Mail

Module modConnection
    'connection module that bridges the application and database
    Public SELLWHENOUTOFSTOCK As Integer = 0 '0 NO 1 YES
    Public connstring As String
    Public con As MySqlConnection
    Public userid As Integer, strsql As String, fullname As String
    Public username As String
    Public customerfullname As String
    Public customerphone As String
    Public customerno As Integer
    Public NEWSYSTEMUSER As Boolean
    Public ExpiryDate As Date = #4/19/2035# ' Expiry date 29th March 2021

    Public Function Conn() As MySqlConnection
        Connstring = My.Settings.constring    'connectstring                  
        con = New MySqlConnection(connstring)
        Try
            If con.State = ConnectionState.Closed Then
                con.Open()
            End If
            Return con
        Catch ex As Exception
            MsgBox("Cannot connect to Main Server " & vbNewLine & "Please check your network connection and try again ", MsgBoxStyle.Critical)
            Return Nothing
        Finally
        End Try
    End Function

    Public Sub closeconn()
        Try
            MySqlConnection.ClearAllPools()
            If (Not con Is Nothing) Then
                con.Close()
                con.Dispose()
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
        End Try
    End Sub


    'sub to load combo
    Public Sub loadcombo(ByVal cbo As ComboBox, _
                         ByVal strsql As String, _
                         ByVal dispmem As String, _
                         ByVal valuemem As String)
        Try
            Dim ds As New DataSet
            cbo.Items.Clear()
            Dim da = New MySqlDataAdapter(strsql, Conn())
            da.SelectCommand.CommandType = CommandType.Text
            da.Fill(ds, "data")
            With cbo
                .Items.Add(ds.Tables("data").DefaultView)
                .DataSource = ds.Tables("data")
                .DisplayMember = dispmem
                .ValueMember = valuemem
                .SelectedIndex = -1
            End With
        Catch ex As Exception
        Finally
        End Try
    End Sub

    'FUNCTION TO GIVE USERS AUTHORIZATION IN APPLICATION
    Public Function Authorized(ByVal UserID As Integer, ByVal Task As TaskID) As Boolean
        Dim dr As MySqlDataReader
        Dim cmd As New MySqlCommand("SELECT * FROM userright WHERE userid=" & UserID & " AND Taskid=" & Task & "", Conn)
        Dim ANSWER As Boolean

        With cmd
            .CommandTimeout = 60
            .Connection = Conn()
            .CommandType = CommandType.Text
        End With

        Try
            'If Conn.State = ConnectionState.Closed Then Conn.Open()
            dr = cmd.ExecuteReader()
            With dr
                If .HasRows Then
                    ANSWER = True
                Else
                    MessageBox.Show("Sorry, you are not authorized to perform this task!" & vbCrLf &
                                    "Contact your system administrator.", "Authorization",
                                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    ANSWER = False
                End If
            End With
            dr.Dispose()
            closeconn()
        Catch ex As Exception
            closeconn()
        Finally
            closeconn()
        End Try
        Return ANSWER
    End Function

    Public Enum TaskID
        viewinventory = 1
        addinventory = 2
        updateinventory = 3
        deleteinventory = 4
        viewsuppliers = 5
        createorder = 6
        vieworders = 7
        receiveitems = 8
        supplierpayment = 9
        supplierpaymentreport = 10
        viewsystemuser = 11
        addsystemusers = 12
        removesystemusers = 13
        changepassword = 14
        makesales = 15
        create_credit_customer = 16
        sell_on_credit = 17
        receive_credit_payment = 18
        view_credit_running_totals = 19
        adjust_price_rate = 20
        view_price_list = 21
        adjust_all_item_prices = 22
        view_banks = 23
        view_sales_returns = 24
        receive_returns = 25
        view_sale_returns_report = 26
        view_product_voids = 27
        view_main_stock_reports = 28
        view_stock_balances = 29
        reconcile_stock = 30
        view_reconciliation_reports = 31
        view_stock_value = 32
        create_expenses = 33
        view_expense_reports = 34
        change_sale_settings = 35
        view_user_rights = 36
        give_user_rights = 37
        view_credit_reports = 38
        view_all_sales_reports = 39
        delete_dummy_sales_report = 40
        view_vat_rates = 41
        change_vat_rates = 42
        view_supplier_deliveries = 43
        view_proforma_invoice = 44
        create_cash_running_totals = 45
        view_cash_running_totals = 46
        register_bank_deposits = 47
        view_bank_deposits = 48
        create_sales_invoices = 49
        view_sales_invoiced_reports = 50
        create_inventory_categories = 51
        register_opening_balance = 52
        retail_sale = 53
        whole_sale = 54
        atc_sale = 55
        cancel_remove_from_sale = 56
    End Enum

    'POS load products to text box
    Sub Loadproducts(ByVal txt As TextBox)
        Dim products As New AutoCompleteStringCollection, strsql As String
        Dim DS As New DataSet
        strsql = "SELECT Description FROM products WHERE deleted !=1"
        DS = ReturnDataset(strsql, "items")
        For x = 0 To DS.Tables("items").Rows.Count - 1
            products.Add(DS.Tables("items").Rows(x).Item("Description"))
        Next
        txt.AutoCompleteCustomSource = products
        txt.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        txt.AutoCompleteSource = AutoCompleteSource.CustomSource
    End Sub

    Sub LoadSuppliers(ByVal txt As TextBox)
        Dim fullnames As New AutoCompleteStringCollection, strsql As String
        Dim DS As New DataSet
        strsql = "SELECT fullname FROM suppliers WHERE deleted!=1 AND relationtype='S'"
        DS = ReturnDataset(strsql, "items")
        For x = 0 To DS.Tables("items").Rows.Count - 1
            fullnames.Add(DS.Tables("items").Rows(x).Item("fullname"))
        Next
        txt.AutoCompleteCustomSource = fullnames
        txt.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        txt.AutoCompleteSource = AutoCompleteSource.CustomSource
    End Sub

    Sub LoadCreditorNames(ByVal txt As TextBox)
        Dim fullname As New AutoCompleteStringCollection, strsql As String
        Dim DS As New DataSet
        strsql = "SELECT fullname FROM suppliers WHERE deleted=0 AND relationtype='C'"
        DS = ReturnDataset(strsql, "items")
        For x = 0 To DS.Tables("items").Rows.Count - 1
            fullname.Add(DS.Tables("items").Rows(x).Item("fullname"))
        Next
        txt.AutoCompleteCustomSource = fullname
        txt.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        txt.AutoCompleteSource = AutoCompleteSource.CustomSource
    End Sub

    Sub LoadBanks(ByVal txt As TextBox)
        Dim bankname As New AutoCompleteStringCollection, strsql As String
        Dim DS As New DataSet
        strsql = "SELECT bankname FROM banks WHERE deleted=0"
        DS = ReturnDataset(strsql, "items")
        For x = 0 To DS.Tables("items").Rows.Count - 1
            bankname.Add(DS.Tables("items").Rows(x).Item("bankname"))
        Next
        txt.AutoCompleteCustomSource = bankname
        txt.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        txt.AutoCompleteSource = AutoCompleteSource.CustomSource
    End Sub

    Sub LoadInvoices(ByVal txt As TextBox)
        Dim invoiceno As New AutoCompleteStringCollection, strsql As String
        Dim DS As New DataSet
        strsql = "SELECT invoiceno FROM invoicemaster WHERE deliverystatus!=1"
        DS = ReturnDataset(strsql, "items")
        For x = 0 To DS.Tables("items").Rows.Count - 1
            invoiceno.Add(DS.Tables("items").Rows(x).Item("invoiceno"))
        Next
        txt.AutoCompleteCustomSource = invoiceno
        txt.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        txt.AutoCompleteSource = AutoCompleteSource.CustomSource
    End Sub

    Public Function ReturnDataset(ByVal QryString As String, ByVal tName As String) As DataSet
        Dim ds As New DataSet
        Try
            Dim da As New MySqlDataAdapter(QryString, Conn)
            da.Fill(ds, tName)
            closeconn()
            Return ds
        Catch ex As System.Exception
            System.Windows.Forms.MessageBox.Show(ex.Message)
            Return Nothing
        Finally
            closeconn()
        End Try
    End Function

    '##############################################################################################################################
    '################################### send email as backup
    Sub Send_PDF(ByVal MyFileName As String, ByVal to_email As String, _
                 ByVal msg_Subject As String, ByVal msg_Body As String)

        Dim dir As String = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\emails\pdf"

        Dim FName As String = dir & "\" & MyFileName & ".pdf"

        Dim REPORTNAME As String = "Appointment_Single"

        Try
            Dim cryRpt As New ReportDocument
            Dim ConInfo As New CrystalDecisions.Shared.TableLogOnInfo
            'cryRpt.Load(App_Path() & "Reports\" & GetReportFolder("Reports") & "\" & REPORTNAME & ".rpt")
            'cryRpt.FileName = App_Path() & "Reports\" & GetReportFolder("Reports") & "\" & REPORTNAME & ".rpt"

            frmPrint.CRPrint.ReportSource = cryRpt
            frmPrint.CRPrint.Refresh()

            ConInfo.ConnectionInfo.UserID = "root"
            ConInfo.ConnectionInfo.Password = "madeinmanhattan"
            ConInfo.ConnectionInfo.ServerName = "localhost"
            ConInfo.ConnectionInfo.DatabaseName = "pv"

            Dim pBarMaxValue = cryRpt.Database.Tables.Count - 1
            For intCounter = 0 To cryRpt.Database.Tables.Count - 1
                cryRpt.Database.Tables(intCounter).ApplyLogOnInfo(ConInfo)
            Next

            cryRpt.SetParameterValue("@AppointmentNo", 2)
            cryRpt.SetParameterValue("@PrintUser", "Test")
            cryRpt.SetParameterValue("@CompanyNo", "Smartec")          'CurrentCompanyID imereplesiwa na "Smartec"

            Dim CrExportOptions As ExportOptions
            Dim CrDiskFileDestinationOptions As New  _
            DiskFileDestinationOptions()
            Dim CrFormatTypeOptions As New PdfRtfWordFormatOptions()
            CrDiskFileDestinationOptions.DiskFileName = FName
            CrExportOptions = cryRpt.ExportOptions
            With CrExportOptions
                .ExportDestinationType = ExportDestinationType.DiskFile
                .ExportFormatType = ExportFormatType.PortableDocFormat
                .DestinationOptions = CrDiskFileDestinationOptions
                .FormatOptions = CrFormatTypeOptions
            End With
            cryRpt.Export()

            Send_Email_Custom(to_email, msg_Subject, msg_Body, FName)

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Sub Send_Email_Custom(ByVal ToEmailAddress As String, ByVal EmailSubject As String, _
                          ByVal EmailBodyToSend As String, ByVal File_Name_Path As String)
        Try
            Dim Smtp_Server As New SmtpClient()
            Dim e_mail As New MailMessage()

            Smtp_Server.UseDefaultCredentials = False

            Smtp_Server.Credentials = New Net.NetworkCredential("", "")
            Smtp_Server.Port = 587
            Smtp_Server.EnableSsl = True
            Smtp_Server.Host = "smtp.gmail.com"

            e_mail = New MailMessage()
            e_mail.From = New MailAddress("")

            e_mail.To.Add(ToEmailAddress)
            e_mail.Subject = EmailSubject
            e_mail.IsBodyHtml = True
            e_mail.Body = EmailBodyToSend

            If String.IsNullOrEmpty(File_Name_Path) = True Or File_Name_Path = "" Then
                ' No Attachment
            Else
                '' Check File Exists
                'If FileExists(File_Name_Path) Then
                '    ' Attach File
                '    e_mail.Attachments.Add(New Attachment(File_Name_Path))
                'End If

            End If

            Smtp_Server.Send(e_mail)
            MessageBox.Show("Email Sent")
        Catch error_t As Exception
            MsgBox(error_t.Message)
        End Try
    End Sub

    '##############################################################################################################################
    Public Sub getCustomerCredit(ByVal custNo As Integer, _
                                  ByVal lbl As Label)
        Dim strsql As String
        Dim dtaName As New MySqlClient.MySqlDataAdapter
        'Try
        Dim datareader As MySqlDataReader
        dtaName.SelectCommand = New MySqlClient.MySqlCommand
        With dtaName.SelectCommand
            .CommandTimeout = 60
            .Connection = Conn()
            .CommandType = CommandType.StoredProcedure
            strsql = "sp_managecreditaccounts"
            .CommandText = strsql
            .Parameters.AddWithValue("@incustomerid", custNo)
            .Parameters.AddWithValue("@intransactiondate", Today())
            .Parameters.AddWithValue("@increditamount", 0)
            .Parameters.AddWithValue("@inamountpaid", 0)
            .Parameters.AddWithValue("@indiscount", 0)
            .Parameters.AddWithValue("@indeterminer", 2)
            datareader = .ExecuteReader

            If datareader.HasRows Then
                While datareader.Read
                    If (IsDBNull(datareader(0))) Then
                        lbl.Text = 0
                    Else
                        lbl.Text = datareader(0).ToString
                    End If
                End While
            Else
                lbl.Text = 0
            End If

        End With
        dtaName.Dispose()
        datareader.Dispose()
        closeconn()
    End Sub

    '''=========   Stock Movement Function  ==================

    Public Function stockMovement(ByVal barcode As String, _
                                  ByVal narration As String, _
                                  ByVal effect As String, _
                                  ByVal qty As Double, _
                                  ByVal sellingprice As Double, _
                                  ByVal buyingprice As Double, _
                                  ByVal transactiondate As Date, _
                                  ByVal user As Integer, _
                                  ByVal selector As Integer) As Integer

        Dim dtaName As New MySqlDataAdapter
        Dim success As Integer = 0

        dtaName.InsertCommand = New MySqlCommand
        'Try
        With dtaName.InsertCommand
            .CommandTimeout = 60
            .Connection = Conn()
            .CommandType = CommandType.StoredProcedure
            .CommandText = "sp_stockmovement"
            .Parameters.AddWithValue("@inbarcode", barcode)
            .Parameters.AddWithValue("@innarration", narration)
            .Parameters.AddWithValue("@ineffect", effect)
            .Parameters.AddWithValue("@inqty", qty)
            .Parameters.AddWithValue("@insp", sellingprice)
            .Parameters.AddWithValue("@inbp", buyingprice)
            .Parameters.AddWithValue("@intransdate", transactiondate)
            .Parameters.AddWithValue("@inuser", userid)
            .Parameters.AddWithValue("@indeterminer", selector)
            .ExecuteScalar()
            success = 1
        End With
        dtaName.Dispose()
        closeconn()
        'Catch ex As Exception
        '    MsgBox("Error occured and stock take was aborted.  Inform your administrator for further action", MsgBoxStyle.Critical)
        'End Try
        Return success
    End Function

    '''=========   Cash Ledger Function  ==================
    Public Function CashLedger(ByVal FileNo As String, _
                               ByVal TotalAmount As Double, _
                               ByVal TransactionDate As Date, _
                               ByVal User As Integer, _
                               ByVal Notes As String, _
                               ByVal Effect As String, _
                               ByVal Narration As String, _
                               ByVal Selector As Integer) As Integer

        Dim dtaName As New MySqlDataAdapter
        Dim success As Integer = 0

        dtaName.InsertCommand = New MySqlCommand
        Try
            With dtaName.InsertCommand
                .CommandTimeout = 60
                .Connection = Conn()
                .CommandType = CommandType.StoredProcedure
                .CommandText = "sp_cashledger"
                .Parameters.AddWithValue("@infileno", FileNo)
                .Parameters.AddWithValue("@intotal", TotalAmount)
                .Parameters.AddWithValue("@intransdate", TransactionDate)
                .Parameters.AddWithValue("@inuser", User)
                .Parameters.AddWithValue("@innotes", Notes)
                .Parameters.AddWithValue("@intranstype", Effect)
                .Parameters.AddWithValue("@innarration", Narration)
                .Parameters.AddWithValue("@indeterminer", Selector)
                .ExecuteScalar()
                success = 1
            End With
            dtaName.Dispose()
            closeconn()
        Catch ex As Exception
            MsgBox("Error occured and operation aborted. Inform your administrator for further action", MsgBoxStyle.Critical)
        End Try
        Return success
    End Function

    '''=========   Stock Ledger Function  ==================
    Public Function StockLedger(ByVal FileNo As String, _
                               ByVal ItemNo As String, _
                               ByVal Qty As Integer, _
                               ByVal SP As Double, _
                               ByVal TransactionDate As Date, _
                               ByVal User As Integer, _
                               ByVal Notes As String, _
                               ByVal Effect As String, _
                               ByVal Narration As String, _
                               ByVal Selector As Integer) As Integer

        Dim dtaName As New MySqlDataAdapter
        Dim success As Integer = 0

        dtaName.InsertCommand = New MySqlCommand
        Try
            With dtaName.InsertCommand
                .CommandTimeout = 60
                .Connection = Conn()
                .CommandType = CommandType.StoredProcedure
                .CommandText = "sp_stockledger"
                .Parameters.AddWithValue("@infileno", FileNo)
                .Parameters.AddWithValue("@initemno", ItemNo)
                .Parameters.AddWithValue("@inqty", Qty)
                .Parameters.AddWithValue("@insp", SP)
                .Parameters.AddWithValue("@intransdate", TransactionDate)
                .Parameters.AddWithValue("@inuser", User)
                .Parameters.AddWithValue("@innotes", Notes)
                .Parameters.AddWithValue("@intranstype", Effect)
                .Parameters.AddWithValue("@innarration", Narration)
                .Parameters.AddWithValue("@indeterminer", Selector)
                .ExecuteScalar()
                success = 1
            End With
            dtaName.Dispose()
            closeconn()
        Catch ex As Exception
            MsgBox("Error occured and operation aborted. Inform your administrator for further action", MsgBoxStyle.Critical)
        End Try
        Return success
    End Function

    'TIMER CHECK LICENSE CODE  sysdate
    Public Valid_Key As String = "c4ca4238a0b9hgvb820d234" ' Use this to validate

    Function License_Is_valid() As Integer
        Dim is_validated As Integer = 0
        Dim sql As String = "SELECT licensekey FROM organisationsettings WHERE fieldname=@fieldname LIMIT 1"

        Dim dr As MySqlDataReader = Nothing
        Dim cmd As New MySqlCommand(sql, Conn())
        cmd.CommandType = CommandType.Text
        cmd.Parameters.AddWithValue("@fieldname", "LICENSE")
        Try
            Dim is_validated_key As String = CStr(cmd.ExecuteScalar)
            If is_validated_key = Valid_Key Then
                is_validated = 1
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        cmd.Connection.Close()

        Return is_validated
    End Function

    Function License_Expiry_Days() As Integer
        Dim Expiry_Days As Integer = 0
        Dim sql As String = "SELECT DATEDIFF(sysdate, NOW()) AS days FROM organisationsettings " _
                            & " WHERE fieldname=@fieldname LIMIT 1"

        Dim dr As MySqlDataReader = Nothing
        Dim cmd As New MySqlCommand(sql, Conn())
        cmd.CommandType = CommandType.Text
        cmd.Parameters.AddWithValue("@fieldname", "LICENSE")
        Try
            Expiry_Days = CInt(cmd.ExecuteScalar)
            'If is_validated_key = Valid_Key Then
            '    is_validated = 1
            'End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        'dr.Dispose()
        cmd.Connection.Close()
        closeconn()
        'MsgBox(Expiry_Days)
        Return Expiry_Days
    End Function

    Sub Check_License(ByRef Menu_Strip As MenuStrip, ByRef Menu_Strip_btns As MenuStrip)
        Dim DaysNum As Integer = License_Expiry_Days()      'DateDiff(DateInterval.Day, Today, ExpiryDate)
        'mdiSales.ToolStripLabel4.Text = "Expiry days remaining:" & DaysNum
        mdiSales.ToolStripLabel4.Visible = False
        If DaysNum <= 0 Then
            If License_Is_valid() = 0 Then
                Menu_Strip.Enabled = False
                Menu_Strip_btns.Enabled = False
                'show below controls
                mdiSales.ToolStripLabel4.Visible = True
                mdiSales.ToolStripLabel4.Text = "Trial days remaining:" & DaysNum

                With frmLisenceSettings
                    .lblSystemTime.Enabled = False
                    .dtpExpiryDate.Enabled = False
                    .btnActivate.Enabled = False
                    .lblSystemTime.Enabled = False
                    .lblExpiryDate.Visible = False
                    .dtpExpiryDate.Visible = False
                    .btnActivate.Visible = False
                    .TopMost = True
                    .Show()
                End With
            Else
                Menu_Strip.Enabled = True
                Menu_Strip_btns.Enabled = True
                mdiSales.ToolStripLabel4.Visible = False
                '#######
                With frmLisenceSettings
                    .lblSystemTime.Enabled = True
                    .dtpExpiryDate.Enabled = True
                    .btnActivate.Enabled = True
                    .lblSystemTime.Enabled = True
                    .lblExpiryDate.Visible = True
                    .dtpExpiryDate.Visible = True
                    .btnActivate.Visible = True
                    '.Hide()
                End With
            End If
        Else
        End If
    End Sub

End Module
