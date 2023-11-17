<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDetailedCreditReports
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDetailedCreditReports))
        Me.dgvCreditDetailed = New System.Windows.Forms.DataGridView()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lblCount = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.lblPercentage = New System.Windows.Forms.Label()
        Me.PB_Progress = New System.Windows.Forms.ProgressBar()
        Me.btnPrint = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.lblCreditBalances = New System.Windows.Forms.Label()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.lblCreditTotals = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.btnLoad = New System.Windows.Forms.Button()
        Me.dtpTo = New System.Windows.Forms.DateTimePicker()
        Me.dtpFrom = New System.Windows.Forms.DateTimePicker()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.btnLoadCustomerData = New System.Windows.Forms.Button()
        Me.lblCustomerNo = New System.Windows.Forms.Label()
        Me.txtSearchCustomer = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ColDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColNames = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColInvoice = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColItems = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColInvoiceTotal = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColAmntPaid = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColDisc = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColBalance = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColPayMode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.dgvCreditDetailed, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        Me.SuspendLayout()
        '
        'dgvCreditDetailed
        '
        Me.dgvCreditDetailed.AllowUserToAddRows = False
        Me.dgvCreditDetailed.AllowUserToDeleteRows = False
        Me.dgvCreditDetailed.AllowUserToResizeColumns = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dgvCreditDetailed.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvCreditDetailed.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvCreditDetailed.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells
        Me.dgvCreditDetailed.BackgroundColor = System.Drawing.Color.SeaShell
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvCreditDetailed.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.dgvCreditDetailed.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvCreditDetailed.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ColDate, Me.ColNames, Me.ColInvoice, Me.ColItems, Me.ColInvoiceTotal, Me.ColAmntPaid, Me.ColDisc, Me.ColBalance, Me.ColPayMode})
        Me.dgvCreditDetailed.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvCreditDetailed.Location = New System.Drawing.Point(0, 0)
        Me.dgvCreditDetailed.Name = "dgvCreditDetailed"
        Me.dgvCreditDetailed.ReadOnly = True
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvCreditDetailed.RowsDefaultCellStyle = DataGridViewCellStyle4
        Me.dgvCreditDetailed.Size = New System.Drawing.Size(980, 390)
        Me.dgvCreditDetailed.TabIndex = 2
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.GroupBox2.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.lblCount)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.lblPercentage)
        Me.GroupBox2.Controls.Add(Me.PB_Progress)
        Me.GroupBox2.Location = New System.Drawing.Point(317, 63)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(254, 98)
        Me.GroupBox2.TabIndex = 24
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Progress:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(30, 75)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(139, 16)
        Me.Label4.TabIndex = 12
        Me.Label4.Text = "Percentage Processed:"
        '
        'lblCount
        '
        Me.lblCount.AutoSize = True
        Me.lblCount.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblCount.Location = New System.Drawing.Point(193, 46)
        Me.lblCount.Name = "lblCount"
        Me.lblCount.Size = New System.Drawing.Size(15, 16)
        Me.lblCount.TabIndex = 9
        Me.lblCount.Text = "0"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Red
        Me.Label5.Location = New System.Drawing.Point(66, 46)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(103, 16)
        Me.Label5.TabIndex = 11
        Me.Label5.Text = "Processed rows:"
        '
        'lblPercentage
        '
        Me.lblPercentage.AutoSize = True
        Me.lblPercentage.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPercentage.ForeColor = System.Drawing.Color.Green
        Me.lblPercentage.Location = New System.Drawing.Point(193, 75)
        Me.lblPercentage.Name = "lblPercentage"
        Me.lblPercentage.Size = New System.Drawing.Size(15, 16)
        Me.lblPercentage.TabIndex = 10
        Me.lblPercentage.Text = "0"
        '
        'PB_Progress
        '
        Me.PB_Progress.Location = New System.Drawing.Point(6, 19)
        Me.PB_Progress.Name = "PB_Progress"
        Me.PB_Progress.Size = New System.Drawing.Size(236, 18)
        Me.PB_Progress.TabIndex = 0
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Image = Global.Strawberry.My.Resources.Resources.print
        Me.btnPrint.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnPrint.Location = New System.Drawing.Point(856, 61)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(58, 93)
        Me.btnPrint.TabIndex = 10
        Me.btnPrint.Text = "Print"
        Me.btnPrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnPrint.UseVisualStyleBackColor = True
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Image = Global.Strawberry.My.Resources.Resources.close
        Me.btnClose.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnClose.Location = New System.Drawing.Point(921, 61)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(58, 93)
        Me.btnClose.TabIndex = 9
        Me.btnClose.Text = "Close"
        Me.btnClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'lblCreditBalances
        '
        Me.lblCreditBalances.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lblCreditBalances.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblCreditBalances.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCreditBalances.ForeColor = System.Drawing.Color.Red
        Me.lblCreditBalances.Location = New System.Drawing.Point(592, 139)
        Me.lblCreditBalances.Name = "lblCreditBalances"
        Me.lblCreditBalances.Size = New System.Drawing.Size(167, 26)
        Me.lblCreditBalances.TabIndex = 8
        Me.lblCreditBalances.Text = "0.0"
        Me.lblCreditBalances.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnRefresh
        '
        Me.btnRefresh.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnRefresh.Image = Global.Strawberry.My.Resources.Resources.refresh
        Me.btnRefresh.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnRefresh.Location = New System.Drawing.Point(789, 61)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(58, 93)
        Me.btnRefresh.TabIndex = 5
        Me.btnRefresh.Text = "Refresh"
        Me.btnRefresh.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'SplitContainer1
        '
        Me.SplitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.GroupBox1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.SplitContainer2)
        Me.SplitContainer1.Size = New System.Drawing.Size(984, 561)
        Me.SplitContainer1.SplitterDistance = 120
        Me.SplitContainer1.TabIndex = 1
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.GroupBox1.Controls.Add(Me.lblCreditTotals)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.GroupBox2)
        Me.GroupBox1.Controls.Add(Me.btnPrint)
        Me.GroupBox1.Controls.Add(Me.btnClose)
        Me.GroupBox1.Controls.Add(Me.lblCreditBalances)
        Me.GroupBox1.Controls.Add(Me.btnRefresh)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.btnLoad)
        Me.GroupBox1.Controls.Add(Me.dtpTo)
        Me.GroupBox1.Controls.Add(Me.dtpFrom)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GroupBox1.Location = New System.Drawing.Point(0, -51)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(980, 167)
        Me.GroupBox1.TabIndex = 15
        Me.GroupBox1.TabStop = False
        '
        'lblCreditTotals
        '
        Me.lblCreditTotals.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lblCreditTotals.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblCreditTotals.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCreditTotals.ForeColor = System.Drawing.Color.Red
        Me.lblCreditTotals.Location = New System.Drawing.Point(592, 82)
        Me.lblCreditTotals.Name = "lblCreditTotals"
        Me.lblCreditTotals.Size = New System.Drawing.Size(167, 26)
        Me.lblCreditTotals.TabIndex = 26
        Me.lblCreditTotals.Text = "0.0"
        Me.lblCreditTotals.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label2
        '
        Me.Label2.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(592, 60)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(69, 13)
        Me.Label2.TabIndex = 25
        Me.Label2.Text = "Credit Totals:"
        '
        'Label11
        '
        Me.Label11.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(592, 117)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(91, 13)
        Me.Label11.TabIndex = 7
        Me.Label11.Text = "Unpaid Balances:"
        '
        'btnLoad
        '
        Me.btnLoad.Image = Global.Strawberry.My.Resources.Resources.load
        Me.btnLoad.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnLoad.Location = New System.Drawing.Point(233, 62)
        Me.btnLoad.Name = "btnLoad"
        Me.btnLoad.Size = New System.Drawing.Size(67, 93)
        Me.btnLoad.TabIndex = 4
        Me.btnLoad.Text = "Load"
        Me.btnLoad.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnLoad.UseVisualStyleBackColor = True
        '
        'dtpTo
        '
        Me.dtpTo.Location = New System.Drawing.Point(90, 131)
        Me.dtpTo.Name = "dtpTo"
        Me.dtpTo.Size = New System.Drawing.Size(137, 20)
        Me.dtpTo.TabIndex = 3
        '
        'dtpFrom
        '
        Me.dtpFrom.Location = New System.Drawing.Point(88, 69)
        Me.dtpFrom.Name = "dtpFrom"
        Me.dtpFrom.Size = New System.Drawing.Size(137, 20)
        Me.dtpFrom.TabIndex = 2
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(59, 135)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(23, 13)
        Me.Label8.TabIndex = 1
        Me.Label8.Text = "To:"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(23, 72)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(59, 13)
        Me.Label7.TabIndex = 0
        Me.Label7.Text = "Date From:"
        '
        'SplitContainer2
        '
        Me.SplitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer2.IsSplitterFixed = True
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.btnLoadCustomerData)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblCustomerNo)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtSearchCustomer)
        Me.SplitContainer2.Panel1.Controls.Add(Me.Label1)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.dgvCreditDetailed)
        Me.SplitContainer2.Size = New System.Drawing.Size(984, 437)
        Me.SplitContainer2.SplitterDistance = 39
        Me.SplitContainer2.TabIndex = 0
        '
        'btnLoadCustomerData
        '
        Me.btnLoadCustomerData.Location = New System.Drawing.Point(423, 7)
        Me.btnLoadCustomerData.Name = "btnLoadCustomerData"
        Me.btnLoadCustomerData.Size = New System.Drawing.Size(172, 23)
        Me.btnLoadCustomerData.TabIndex = 3
        Me.btnLoadCustomerData.Text = "Load Customer Statement"
        Me.btnLoadCustomerData.UseVisualStyleBackColor = True
        '
        'lblCustomerNo
        '
        Me.lblCustomerNo.AutoSize = True
        Me.lblCustomerNo.Font = New System.Drawing.Font("Wingdings 2", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.lblCustomerNo.Location = New System.Drawing.Point(377, 11)
        Me.lblCustomerNo.Name = "lblCustomerNo"
        Me.lblCustomerNo.Size = New System.Drawing.Size(13, 11)
        Me.lblCustomerNo.TabIndex = 2
        Me.lblCustomerNo.Text = "0"
        '
        'txtSearchCustomer
        '
        Me.txtSearchCustomer.Location = New System.Drawing.Point(133, 7)
        Me.txtSearchCustomer.Name = "txtSearchCustomer"
        Me.txtSearchCustomer.Size = New System.Drawing.Size(237, 20)
        Me.txtSearchCustomer.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(35, 11)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(91, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Search Customer:"
        '
        'ColDate
        '
        Me.ColDate.DividerWidth = 3
        Me.ColDate.FillWeight = 27.83418!
        Me.ColDate.HeaderText = "Date"
        Me.ColDate.MinimumWidth = 100
        Me.ColDate.Name = "ColDate"
        Me.ColDate.ReadOnly = True
        '
        'ColNames
        '
        Me.ColNames.DividerWidth = 3
        Me.ColNames.FillWeight = 80.0!
        Me.ColNames.HeaderText = "Customer Names"
        Me.ColNames.MinimumWidth = 100
        Me.ColNames.Name = "ColNames"
        Me.ColNames.ReadOnly = True
        '
        'ColInvoice
        '
        Me.ColInvoice.DividerWidth = 3
        Me.ColInvoice.FillWeight = 27.83418!
        Me.ColInvoice.HeaderText = "Invoice No"
        Me.ColInvoice.MinimumWidth = 90
        Me.ColInvoice.Name = "ColInvoice"
        Me.ColInvoice.ReadOnly = True
        '
        'ColItems
        '
        Me.ColItems.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.ColItems.DefaultCellStyle = DataGridViewCellStyle3
        Me.ColItems.DividerWidth = 3
        Me.ColItems.FillWeight = 200.0!
        Me.ColItems.HeaderText = "Items Bought"
        Me.ColItems.MinimumWidth = 200
        Me.ColItems.Name = "ColItems"
        Me.ColItems.ReadOnly = True
        Me.ColItems.Width = 200
        '
        'ColInvoiceTotal
        '
        Me.ColInvoiceTotal.DividerWidth = 3
        Me.ColInvoiceTotal.FillWeight = 27.83418!
        Me.ColInvoiceTotal.HeaderText = "Invoice Total"
        Me.ColInvoiceTotal.MinimumWidth = 80
        Me.ColInvoiceTotal.Name = "ColInvoiceTotal"
        Me.ColInvoiceTotal.ReadOnly = True
        '
        'ColAmntPaid
        '
        Me.ColAmntPaid.DividerWidth = 3
        Me.ColAmntPaid.FillWeight = 27.83418!
        Me.ColAmntPaid.HeaderText = "Amount Paid"
        Me.ColAmntPaid.MinimumWidth = 80
        Me.ColAmntPaid.Name = "ColAmntPaid"
        Me.ColAmntPaid.ReadOnly = True
        '
        'ColDisc
        '
        Me.ColDisc.DividerWidth = 3
        Me.ColDisc.FillWeight = 27.83418!
        Me.ColDisc.HeaderText = "Discount"
        Me.ColDisc.MinimumWidth = 80
        Me.ColDisc.Name = "ColDisc"
        Me.ColDisc.ReadOnly = True
        '
        'ColBalance
        '
        Me.ColBalance.DividerWidth = 2
        Me.ColBalance.FillWeight = 50.0!
        Me.ColBalance.HeaderText = "Invoice Balance"
        Me.ColBalance.MinimumWidth = 80
        Me.ColBalance.Name = "ColBalance"
        Me.ColBalance.ReadOnly = True
        '
        'ColPayMode
        '
        Me.ColPayMode.FillWeight = 250.0!
        Me.ColPayMode.HeaderText = "Payment Mode"
        Me.ColPayMode.MinimumWidth = 300
        Me.ColPayMode.Name = "ColPayMode"
        Me.ColPayMode.ReadOnly = True
        '
        'frmDetailedCreditReports
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(984, 561)
        Me.Controls.Add(Me.SplitContainer1)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmDetailedCreditReports"
        Me.Text = "Detailed Credit Reports"
        CType(Me.dgvCreditDetailed, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents dgvCreditDetailed As System.Windows.Forms.DataGridView
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents lblCount As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents lblPercentage As System.Windows.Forms.Label
    Friend WithEvents PB_Progress As System.Windows.Forms.ProgressBar
    Friend WithEvents btnPrint As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents lblCreditBalances As System.Windows.Forms.Label
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents btnLoad As System.Windows.Forms.Button
    Friend WithEvents dtpTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents lblCreditTotals As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtSearchCustomer As System.Windows.Forms.TextBox
    Friend WithEvents lblCustomerNo As System.Windows.Forms.Label
    Friend WithEvents btnLoadCustomerData As System.Windows.Forms.Button
    Friend WithEvents ColDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColNames As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColInvoice As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColItems As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColInvoiceTotal As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColAmntPaid As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColDisc As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColBalance As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColPayMode As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
