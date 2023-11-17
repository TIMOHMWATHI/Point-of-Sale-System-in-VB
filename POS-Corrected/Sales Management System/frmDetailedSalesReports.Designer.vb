<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDetailedSalesReports
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDetailedSalesReports))
        Me.lblMobilePayTotals = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.btnLoad = New System.Windows.Forms.Button()
        Me.dtpTo = New System.Windows.Forms.DateTimePicker()
        Me.dtpFrom = New System.Windows.Forms.DateTimePicker()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lblCount = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.lblPercentage = New System.Windows.Forms.Label()
        Me.PB_Progress = New System.Windows.Forms.ProgressBar()
        Me.btnPrint = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.lblSolidCashTotals = New System.Windows.Forms.Label()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.dgvSalesDetailed = New System.Windows.Forms.DataGridView()
        Me.ColReceiptNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColItems = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColPurchaseMode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColTotal = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColAmntPaid = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColDisc = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColPayMode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColSalesMode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColSoldBy = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.dgvSalesDetailed, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblMobilePayTotals
        '
        Me.lblMobilePayTotals.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lblMobilePayTotals.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblMobilePayTotals.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMobilePayTotals.ForeColor = System.Drawing.Color.Red
        Me.lblMobilePayTotals.Location = New System.Drawing.Point(592, 82)
        Me.lblMobilePayTotals.Name = "lblMobilePayTotals"
        Me.lblMobilePayTotals.Size = New System.Drawing.Size(167, 26)
        Me.lblMobilePayTotals.TabIndex = 26
        Me.lblMobilePayTotals.Text = "0.0"
        Me.lblMobilePayTotals.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label2
        '
        Me.Label2.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(592, 60)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(117, 13)
        Me.Label2.TabIndex = 25
        Me.Label2.Text = "Mobile Payment Totals:"
        '
        'Label11
        '
        Me.Label11.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(592, 117)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(136, 13)
        Me.Label11.TabIndex = 7
        Me.Label11.Text = "Solid Cash Payment Totals:"
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
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(23, 72)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(59, 13)
        Me.Label7.TabIndex = 0
        Me.Label7.Text = "Date From:"
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.GroupBox1.Controls.Add(Me.lblMobilePayTotals)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.GroupBox2)
        Me.GroupBox1.Controls.Add(Me.btnPrint)
        Me.GroupBox1.Controls.Add(Me.btnClose)
        Me.GroupBox1.Controls.Add(Me.lblSolidCashTotals)
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
        'lblSolidCashTotals
        '
        Me.lblSolidCashTotals.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lblSolidCashTotals.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblSolidCashTotals.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSolidCashTotals.ForeColor = System.Drawing.Color.Red
        Me.lblSolidCashTotals.Location = New System.Drawing.Point(592, 139)
        Me.lblSolidCashTotals.Name = "lblSolidCashTotals"
        Me.lblSolidCashTotals.Size = New System.Drawing.Size(167, 26)
        Me.lblSolidCashTotals.TabIndex = 8
        Me.lblSolidCashTotals.Text = "0.0"
        Me.lblSolidCashTotals.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
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
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(59, 135)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(23, 13)
        Me.Label8.TabIndex = 1
        Me.Label8.Text = "To:"
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.dgvSalesDetailed)
        Me.SplitContainer1.Size = New System.Drawing.Size(984, 561)
        Me.SplitContainer1.SplitterDistance = 120
        Me.SplitContainer1.TabIndex = 2
        '
        'dgvSalesDetailed
        '
        Me.dgvSalesDetailed.AllowUserToAddRows = False
        Me.dgvSalesDetailed.AllowUserToDeleteRows = False
        Me.dgvSalesDetailed.AllowUserToResizeColumns = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dgvSalesDetailed.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvSalesDetailed.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvSalesDetailed.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells
        Me.dgvSalesDetailed.BackgroundColor = System.Drawing.Color.SeaShell
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvSalesDetailed.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.dgvSalesDetailed.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvSalesDetailed.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ColReceiptNo, Me.ColDate, Me.ColItems, Me.ColPurchaseMode, Me.ColTotal, Me.ColAmntPaid, Me.ColDisc, Me.ColPayMode, Me.ColSalesMode, Me.ColSoldBy})
        Me.dgvSalesDetailed.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvSalesDetailed.Location = New System.Drawing.Point(0, 0)
        Me.dgvSalesDetailed.Name = "dgvSalesDetailed"
        Me.dgvSalesDetailed.ReadOnly = True
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvSalesDetailed.RowsDefaultCellStyle = DataGridViewCellStyle4
        Me.dgvSalesDetailed.Size = New System.Drawing.Size(980, 433)
        Me.dgvSalesDetailed.TabIndex = 3
        '
        'ColReceiptNo
        '
        Me.ColReceiptNo.DividerWidth = 3
        Me.ColReceiptNo.FillWeight = 27.83418!
        Me.ColReceiptNo.HeaderText = "Date"
        Me.ColReceiptNo.MinimumWidth = 90
        Me.ColReceiptNo.Name = "ColReceiptNo"
        Me.ColReceiptNo.ReadOnly = True
        '
        'ColDate
        '
        Me.ColDate.DividerWidth = 3
        Me.ColDate.FillWeight = 27.83418!
        Me.ColDate.HeaderText = "Receipt No"
        Me.ColDate.MinimumWidth = 60
        Me.ColDate.Name = "ColDate"
        Me.ColDate.ReadOnly = True
        '
        'ColItems
        '
        Me.ColItems.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.ColItems.DefaultCellStyle = DataGridViewCellStyle3
        Me.ColItems.DividerWidth = 3
        Me.ColItems.FillWeight = 180.0!
        Me.ColItems.HeaderText = "Items Sold"
        Me.ColItems.MinimumWidth = 180
        Me.ColItems.Name = "ColItems"
        Me.ColItems.ReadOnly = True
        Me.ColItems.Width = 180
        '
        'ColPurchaseMode
        '
        Me.ColPurchaseMode.DividerWidth = 3
        Me.ColPurchaseMode.FillWeight = 50.0!
        Me.ColPurchaseMode.HeaderText = "Purchase Mode"
        Me.ColPurchaseMode.MinimumWidth = 80
        Me.ColPurchaseMode.Name = "ColPurchaseMode"
        Me.ColPurchaseMode.ReadOnly = True
        '
        'ColTotal
        '
        Me.ColTotal.DividerWidth = 3
        Me.ColTotal.FillWeight = 25.0!
        Me.ColTotal.HeaderText = "Receipt Total"
        Me.ColTotal.MinimumWidth = 70
        Me.ColTotal.Name = "ColTotal"
        Me.ColTotal.ReadOnly = True
        '
        'ColAmntPaid
        '
        Me.ColAmntPaid.DividerWidth = 3
        Me.ColAmntPaid.FillWeight = 25.0!
        Me.ColAmntPaid.HeaderText = "Amount Paid"
        Me.ColAmntPaid.MinimumWidth = 70
        Me.ColAmntPaid.Name = "ColAmntPaid"
        Me.ColAmntPaid.ReadOnly = True
        '
        'ColDisc
        '
        Me.ColDisc.DividerWidth = 3
        Me.ColDisc.FillWeight = 25.0!
        Me.ColDisc.HeaderText = "Discount"
        Me.ColDisc.MinimumWidth = 70
        Me.ColDisc.Name = "ColDisc"
        Me.ColDisc.ReadOnly = True
        '
        'ColPayMode
        '
        Me.ColPayMode.DividerWidth = 3
        Me.ColPayMode.FillWeight = 280.0!
        Me.ColPayMode.HeaderText = "Pay Mode"
        Me.ColPayMode.MinimumWidth = 280
        Me.ColPayMode.Name = "ColPayMode"
        Me.ColPayMode.ReadOnly = True
        '
        'ColSalesMode
        '
        Me.ColSalesMode.DividerWidth = 3
        Me.ColSalesMode.FillWeight = 40.0!
        Me.ColSalesMode.HeaderText = "Sales Mode"
        Me.ColSalesMode.MinimumWidth = 100
        Me.ColSalesMode.Name = "ColSalesMode"
        Me.ColSalesMode.ReadOnly = True
        '
        'ColSoldBy
        '
        Me.ColSoldBy.HeaderText = "Sold By"
        Me.ColSoldBy.MinimumWidth = 90
        Me.ColSoldBy.Name = "ColSoldBy"
        Me.ColSoldBy.ReadOnly = True
        '
        'frmDetailedSalesReports
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(984, 561)
        Me.Controls.Add(Me.SplitContainer1)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmDetailedSalesReports"
        Me.Text = "Detailed Sales Reports"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.dgvSalesDetailed, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblMobilePayTotals As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents btnLoad As System.Windows.Forms.Button
    Friend WithEvents dtpTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents lblCount As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents lblPercentage As System.Windows.Forms.Label
    Friend WithEvents PB_Progress As System.Windows.Forms.ProgressBar
    Friend WithEvents btnPrint As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents lblSolidCashTotals As System.Windows.Forms.Label
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents dgvSalesDetailed As System.Windows.Forms.DataGridView
    Friend WithEvents ColReceiptNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColItems As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColPurchaseMode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColTotal As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColAmntPaid As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColDisc As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColPayMode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColSalesMode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColSoldBy As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
