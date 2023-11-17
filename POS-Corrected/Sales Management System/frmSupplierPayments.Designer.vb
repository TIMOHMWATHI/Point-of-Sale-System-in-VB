<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSupplierPayments
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
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle12 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle13 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSupplierPayments))
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.lblSupplier = New System.Windows.Forms.Label()
        Me.btnPaySelected = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.SplitContainer5 = New System.Windows.Forms.SplitContainer()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.dgvSupplierAmnt = New System.Windows.Forms.DataGridView()
        Me.dgvSupplierPayments = New System.Windows.Forms.DataGridView()
        Me.ColPay = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.ColDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColSupplier = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColInvoiceNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColTotal = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColPaid = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColStatus = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SplitContainer3 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer4 = New System.Windows.Forms.SplitContainer()
        Me.dtpPayDate = New System.Windows.Forms.DateTimePicker()
        Me.txtOtherNotes = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.lblCheque = New System.Windows.Forms.Label()
        Me.txtChequeNo = New System.Windows.Forms.TextBox()
        Me.rdbCheque = New System.Windows.Forms.RadioButton()
        Me.rdbBankTransfer = New System.Windows.Forms.RadioButton()
        Me.rdbMobileTransfer = New System.Windows.Forms.RadioButton()
        Me.rdbCash = New System.Windows.Forms.RadioButton()
        Me.lblBankRef = New System.Windows.Forms.Label()
        Me.txtBankRefNo = New System.Windows.Forms.TextBox()
        Me.lblMpesaRef = New System.Windows.Forms.Label()
        Me.txtMpesaRef = New System.Windows.Forms.TextBox()
        Me.txtCash = New System.Windows.Forms.TextBox()
        Me.cboBank = New System.Windows.Forms.ComboBox()
        Me.lblBank = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtBal = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.lblSupplierNo = New System.Windows.Forms.Label()
        Me.txtAmntPaid = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtCreditsTotal = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.btnLoad = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtSearchToPay = New System.Windows.Forms.TextBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.dgvPaySupplierInvoices = New System.Windows.Forms.DataGridView()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnProcess = New System.Windows.Forms.Button()
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.SplitContainer5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer5.Panel1.SuspendLayout()
        Me.SplitContainer5.Panel2.SuspendLayout()
        Me.SplitContainer5.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        CType(Me.dgvSupplierAmnt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvSupplierPayments, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainer3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer3.Panel1.SuspendLayout()
        Me.SplitContainer3.Panel2.SuspendLayout()
        Me.SplitContainer3.SuspendLayout()
        CType(Me.SplitContainer4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer4.Panel1.SuspendLayout()
        Me.SplitContainer4.Panel2.SuspendLayout()
        Me.SplitContainer4.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.dgvPaySupplierInvoices, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer2
        '
        Me.SplitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer2.IsSplitterFixed = True
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.SplitContainer1)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.SplitContainer3)
        Me.SplitContainer2.Size = New System.Drawing.Size(884, 511)
        Me.SplitContainer2.SplitterDistance = 385
        Me.SplitContainer2.TabIndex = 0
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblSupplier)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnPaySelected)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnRefresh)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtSearch)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.SplitContainer5)
        Me.SplitContainer1.Size = New System.Drawing.Size(385, 511)
        Me.SplitContainer1.SplitterDistance = 35
        Me.SplitContainer1.TabIndex = 0
        '
        'lblSupplier
        '
        Me.lblSupplier.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblSupplier.AutoSize = True
        Me.lblSupplier.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSupplier.ForeColor = System.Drawing.Color.Red
        Me.lblSupplier.Location = New System.Drawing.Point(3, 13)
        Me.lblSupplier.Name = "lblSupplier"
        Me.lblSupplier.Size = New System.Drawing.Size(13, 13)
        Me.lblSupplier.TabIndex = 4
        Me.lblSupplier.Text = "0"
        '
        'btnPaySelected
        '
        Me.btnPaySelected.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnPaySelected.Location = New System.Drawing.Point(301, 6)
        Me.btnPaySelected.Name = "btnPaySelected"
        Me.btnPaySelected.Size = New System.Drawing.Size(78, 23)
        Me.btnPaySelected.TabIndex = 3
        Me.btnPaySelected.Text = "Pay Selected"
        Me.btnPaySelected.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(5, 11)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(85, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Search Supplier:"
        '
        'btnRefresh
        '
        Me.btnRefresh.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnRefresh.Location = New System.Drawing.Point(245, 6)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(52, 23)
        Me.btnRefresh.TabIndex = 2
        Me.btnRefresh.Text = "Refresh"
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'txtSearch
        '
        Me.txtSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSearch.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtSearch.Location = New System.Drawing.Point(97, 7)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(142, 20)
        Me.txtSearch.TabIndex = 1
        '
        'SplitContainer5
        '
        Me.SplitContainer5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.SplitContainer5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer5.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer5.IsSplitterFixed = True
        Me.SplitContainer5.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer5.Name = "SplitContainer5"
        '
        'SplitContainer5.Panel1
        '
        Me.SplitContainer5.Panel1.Controls.Add(Me.GroupBox3)
        '
        'SplitContainer5.Panel2
        '
        Me.SplitContainer5.Panel2.Controls.Add(Me.dgvSupplierPayments)
        Me.SplitContainer5.Size = New System.Drawing.Size(385, 472)
        Me.SplitContainer5.SplitterDistance = 256
        Me.SplitContainer5.TabIndex = 0
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.dgvSupplierAmnt)
        Me.GroupBox3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox3.ForeColor = System.Drawing.Color.Green
        Me.GroupBox3.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(252, 468)
        Me.GroupBox3.TabIndex = 0
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Supplier Unpaid Totals:"
        '
        'dgvSupplierAmnt
        '
        Me.dgvSupplierAmnt.AllowUserToAddRows = False
        Me.dgvSupplierAmnt.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.dgvSupplierAmnt.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvSupplierAmnt.BackgroundColor = System.Drawing.Color.LightGray
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvSupplierAmnt.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.dgvSupplierAmnt.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvSupplierAmnt.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvSupplierAmnt.Location = New System.Drawing.Point(3, 16)
        Me.dgvSupplierAmnt.Name = "dgvSupplierAmnt"
        Me.dgvSupplierAmnt.ReadOnly = True
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black
        Me.dgvSupplierAmnt.RowsDefaultCellStyle = DataGridViewCellStyle3
        Me.dgvSupplierAmnt.Size = New System.Drawing.Size(246, 449)
        Me.dgvSupplierAmnt.TabIndex = 0
        '
        'dgvSupplierPayments
        '
        Me.dgvSupplierPayments.AllowUserToAddRows = False
        Me.dgvSupplierPayments.AllowUserToDeleteRows = False
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dgvSupplierPayments.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle4
        Me.dgvSupplierPayments.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvSupplierPayments.BackgroundColor = System.Drawing.Color.MintCream
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvSupplierPayments.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle5
        Me.dgvSupplierPayments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvSupplierPayments.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ColPay, Me.ColDate, Me.ColSupplier, Me.ColInvoiceNo, Me.ColTotal, Me.ColPaid, Me.ColStatus})
        Me.dgvSupplierPayments.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvSupplierPayments.Location = New System.Drawing.Point(0, 0)
        Me.dgvSupplierPayments.Name = "dgvSupplierPayments"
        DataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle10.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgvSupplierPayments.RowsDefaultCellStyle = DataGridViewCellStyle10
        Me.dgvSupplierPayments.Size = New System.Drawing.Size(121, 468)
        Me.dgvSupplierPayments.TabIndex = 0
        '
        'ColPay
        '
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle6.NullValue = False
        Me.ColPay.DefaultCellStyle = DataGridViewCellStyle6
        Me.ColPay.FillWeight = 18.87284!
        Me.ColPay.HeaderText = "Pay Selected"
        Me.ColPay.MinimumWidth = 10
        Me.ColPay.Name = "ColPay"
        Me.ColPay.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.ColPay.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'ColDate
        '
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle7.Format = "d"
        DataGridViewCellStyle7.NullValue = Nothing
        Me.ColDate.DefaultCellStyle = DataGridViewCellStyle7
        Me.ColDate.FillWeight = 48.97394!
        Me.ColDate.HeaderText = "Invoice Date"
        Me.ColDate.MinimumWidth = 15
        Me.ColDate.Name = "ColDate"
        Me.ColDate.ReadOnly = True
        '
        'ColSupplier
        '
        DataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.ColSupplier.DefaultCellStyle = DataGridViewCellStyle8
        Me.ColSupplier.FillWeight = 154.8211!
        Me.ColSupplier.HeaderText = "Supplier"
        Me.ColSupplier.MinimumWidth = 50
        Me.ColSupplier.Name = "ColSupplier"
        Me.ColSupplier.ReadOnly = True
        '
        'ColInvoiceNo
        '
        Me.ColInvoiceNo.FillWeight = 57.24263!
        Me.ColInvoiceNo.HeaderText = "Invoice No"
        Me.ColInvoiceNo.MinimumWidth = 30
        Me.ColInvoiceNo.Name = "ColInvoiceNo"
        Me.ColInvoiceNo.ReadOnly = True
        '
        'ColTotal
        '
        Me.ColTotal.FillWeight = 22.97111!
        Me.ColTotal.HeaderText = "Total"
        Me.ColTotal.MinimumWidth = 20
        Me.ColTotal.Name = "ColTotal"
        Me.ColTotal.ReadOnly = True
        '
        'ColPaid
        '
        Me.ColPaid.FillWeight = 22.97111!
        Me.ColPaid.HeaderText = "Amount Paid"
        Me.ColPaid.MinimumWidth = 20
        Me.ColPaid.Name = "ColPaid"
        Me.ColPaid.ReadOnly = True
        '
        'ColStatus
        '
        DataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.ColStatus.DefaultCellStyle = DataGridViewCellStyle9
        Me.ColStatus.FillWeight = 45.94222!
        Me.ColStatus.HeaderText = "Pay Status"
        Me.ColStatus.MinimumWidth = 40
        Me.ColStatus.Name = "ColStatus"
        Me.ColStatus.ReadOnly = True
        '
        'SplitContainer3
        '
        Me.SplitContainer3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.SplitContainer3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer3.IsSplitterFixed = True
        Me.SplitContainer3.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer3.Name = "SplitContainer3"
        Me.SplitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer3.Panel1
        '
        Me.SplitContainer3.Panel1.Controls.Add(Me.SplitContainer4)
        '
        'SplitContainer3.Panel2
        '
        Me.SplitContainer3.Panel2.Controls.Add(Me.btnCancel)
        Me.SplitContainer3.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer3.Panel2.Controls.Add(Me.btnProcess)
        Me.SplitContainer3.Size = New System.Drawing.Size(495, 511)
        Me.SplitContainer3.SplitterDistance = 463
        Me.SplitContainer3.TabIndex = 0
        '
        'SplitContainer4
        '
        Me.SplitContainer4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.SplitContainer4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer4.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer4.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer4.Name = "SplitContainer4"
        Me.SplitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer4.Panel1
        '
        Me.SplitContainer4.Panel1.Controls.Add(Me.dtpPayDate)
        Me.SplitContainer4.Panel1.Controls.Add(Me.txtOtherNotes)
        Me.SplitContainer4.Panel1.Controls.Add(Me.Label5)
        Me.SplitContainer4.Panel1.Controls.Add(Me.GroupBox2)
        Me.SplitContainer4.Panel1.Controls.Add(Me.Label9)
        Me.SplitContainer4.Panel1.Controls.Add(Me.txtBal)
        Me.SplitContainer4.Panel1.Controls.Add(Me.Label7)
        Me.SplitContainer4.Panel1.Controls.Add(Me.lblSupplierNo)
        Me.SplitContainer4.Panel1.Controls.Add(Me.txtAmntPaid)
        Me.SplitContainer4.Panel1.Controls.Add(Me.Label3)
        Me.SplitContainer4.Panel1.Controls.Add(Me.txtCreditsTotal)
        Me.SplitContainer4.Panel1.Controls.Add(Me.Label4)
        Me.SplitContainer4.Panel1.Controls.Add(Me.btnLoad)
        Me.SplitContainer4.Panel1.Controls.Add(Me.Label2)
        Me.SplitContainer4.Panel1.Controls.Add(Me.txtSearchToPay)
        '
        'SplitContainer4.Panel2
        '
        Me.SplitContainer4.Panel2.Controls.Add(Me.GroupBox1)
        Me.SplitContainer4.Size = New System.Drawing.Size(495, 463)
        Me.SplitContainer4.SplitterDistance = 255
        Me.SplitContainer4.TabIndex = 0
        '
        'dtpPayDate
        '
        Me.dtpPayDate.Location = New System.Drawing.Point(322, 50)
        Me.dtpPayDate.Name = "dtpPayDate"
        Me.dtpPayDate.Size = New System.Drawing.Size(136, 20)
        Me.dtpPayDate.TabIndex = 30
        '
        'txtOtherNotes
        '
        Me.txtOtherNotes.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtOtherNotes.Location = New System.Drawing.Point(321, 79)
        Me.txtOtherNotes.Multiline = True
        Me.txtOtherNotes.Name = "txtOtherNotes"
        Me.txtOtherNotes.Size = New System.Drawing.Size(164, 31)
        Me.txtOtherNotes.TabIndex = 3
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(246, 82)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(67, 13)
        Me.Label5.TabIndex = 29
        Me.Label5.Text = "Other Notes:"
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.GroupBox2.Controls.Add(Me.lblCheque)
        Me.GroupBox2.Controls.Add(Me.txtChequeNo)
        Me.GroupBox2.Controls.Add(Me.rdbCheque)
        Me.GroupBox2.Controls.Add(Me.rdbBankTransfer)
        Me.GroupBox2.Controls.Add(Me.rdbMobileTransfer)
        Me.GroupBox2.Controls.Add(Me.rdbCash)
        Me.GroupBox2.Controls.Add(Me.lblBankRef)
        Me.GroupBox2.Controls.Add(Me.txtBankRefNo)
        Me.GroupBox2.Controls.Add(Me.lblMpesaRef)
        Me.GroupBox2.Controls.Add(Me.txtMpesaRef)
        Me.GroupBox2.Controls.Add(Me.txtCash)
        Me.GroupBox2.Controls.Add(Me.cboBank)
        Me.GroupBox2.Controls.Add(Me.lblBank)
        Me.GroupBox2.Location = New System.Drawing.Point(14, 116)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(474, 132)
        Me.GroupBox2.TabIndex = 25
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Pay Mode:"
        '
        'lblCheque
        '
        Me.lblCheque.AutoSize = True
        Me.lblCheque.Location = New System.Drawing.Point(376, 46)
        Me.lblCheque.Name = "lblCheque"
        Me.lblCheque.Size = New System.Drawing.Size(61, 13)
        Me.lblCheque.TabIndex = 32
        Me.lblCheque.Text = "Cheque No"
        Me.lblCheque.Visible = False
        '
        'txtChequeNo
        '
        Me.txtChequeNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtChequeNo.Location = New System.Drawing.Point(370, 65)
        Me.txtChequeNo.Name = "txtChequeNo"
        Me.txtChequeNo.Size = New System.Drawing.Size(101, 20)
        Me.txtChequeNo.TabIndex = 31
        Me.txtChequeNo.Visible = False
        '
        'rdbCheque
        '
        Me.rdbCheque.AutoSize = True
        Me.rdbCheque.Location = New System.Drawing.Point(370, 18)
        Me.rdbCheque.Name = "rdbCheque"
        Me.rdbCheque.Size = New System.Drawing.Size(62, 17)
        Me.rdbCheque.TabIndex = 30
        Me.rdbCheque.Text = "Cheque"
        Me.rdbCheque.UseVisualStyleBackColor = True
        '
        'rdbBankTransfer
        '
        Me.rdbBankTransfer.AutoSize = True
        Me.rdbBankTransfer.Location = New System.Drawing.Point(216, 18)
        Me.rdbBankTransfer.Name = "rdbBankTransfer"
        Me.rdbBankTransfer.Size = New System.Drawing.Size(92, 17)
        Me.rdbBankTransfer.TabIndex = 2
        Me.rdbBankTransfer.Text = "Bank Transfer"
        Me.rdbBankTransfer.UseVisualStyleBackColor = True
        '
        'rdbMobileTransfer
        '
        Me.rdbMobileTransfer.AutoSize = True
        Me.rdbMobileTransfer.Location = New System.Drawing.Point(102, 18)
        Me.rdbMobileTransfer.Name = "rdbMobileTransfer"
        Me.rdbMobileTransfer.Size = New System.Drawing.Size(98, 17)
        Me.rdbMobileTransfer.TabIndex = 1
        Me.rdbMobileTransfer.Text = "Mobile Banking"
        Me.rdbMobileTransfer.UseVisualStyleBackColor = True
        '
        'rdbCash
        '
        Me.rdbCash.AutoSize = True
        Me.rdbCash.ForeColor = System.Drawing.Color.Black
        Me.rdbCash.Location = New System.Drawing.Point(12, 18)
        Me.rdbCash.Name = "rdbCash"
        Me.rdbCash.Size = New System.Drawing.Size(49, 17)
        Me.rdbCash.TabIndex = 0
        Me.rdbCash.Text = "Cash"
        Me.rdbCash.UseVisualStyleBackColor = True
        '
        'lblBankRef
        '
        Me.lblBankRef.AutoSize = True
        Me.lblBankRef.Location = New System.Drawing.Point(216, 89)
        Me.lblBankRef.Name = "lblBankRef"
        Me.lblBankRef.Size = New System.Drawing.Size(69, 13)
        Me.lblBankRef.TabIndex = 29
        Me.lblBankRef.Text = "Bank Ref No"
        Me.lblBankRef.Visible = False
        '
        'txtBankRefNo
        '
        Me.txtBankRefNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtBankRefNo.Location = New System.Drawing.Point(216, 106)
        Me.txtBankRefNo.Name = "txtBankRefNo"
        Me.txtBankRefNo.Size = New System.Drawing.Size(126, 20)
        Me.txtBankRefNo.TabIndex = 28
        Me.txtBankRefNo.Visible = False
        '
        'lblMpesaRef
        '
        Me.lblMpesaRef.AutoSize = True
        Me.lblMpesaRef.Location = New System.Drawing.Point(108, 46)
        Me.lblMpesaRef.Name = "lblMpesaRef"
        Me.lblMpesaRef.Size = New System.Drawing.Size(80, 13)
        Me.lblMpesaRef.TabIndex = 27
        Me.lblMpesaRef.Text = "Transaction No"
        Me.lblMpesaRef.Visible = False
        '
        'txtMpesaRef
        '
        Me.txtMpesaRef.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtMpesaRef.Location = New System.Drawing.Point(102, 65)
        Me.txtMpesaRef.Name = "txtMpesaRef"
        Me.txtMpesaRef.Size = New System.Drawing.Size(79, 20)
        Me.txtMpesaRef.TabIndex = 4
        Me.txtMpesaRef.Visible = False
        '
        'txtCash
        '
        Me.txtCash.Enabled = False
        Me.txtCash.Location = New System.Drawing.Point(6, 65)
        Me.txtCash.Name = "txtCash"
        Me.txtCash.ReadOnly = True
        Me.txtCash.Size = New System.Drawing.Size(79, 20)
        Me.txtCash.TabIndex = 3
        Me.txtCash.Text = "Cash"
        Me.txtCash.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.txtCash.Visible = False
        '
        'cboBank
        '
        Me.cboBank.FormattingEnabled = True
        Me.cboBank.Location = New System.Drawing.Point(216, 65)
        Me.cboBank.Name = "cboBank"
        Me.cboBank.Size = New System.Drawing.Size(127, 21)
        Me.cboBank.TabIndex = 21
        Me.cboBank.Visible = False
        '
        'lblBank
        '
        Me.lblBank.AutoSize = True
        Me.lblBank.Location = New System.Drawing.Point(216, 46)
        Me.lblBank.Name = "lblBank"
        Me.lblBank.Size = New System.Drawing.Size(35, 13)
        Me.lblBank.TabIndex = 20
        Me.lblBank.Text = "Bank:"
        Me.lblBank.Visible = False
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(256, 54)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(57, 13)
        Me.Label9.TabIndex = 27
        Me.Label9.Text = "Date Paid:"
        '
        'txtBal
        '
        Me.txtBal.Location = New System.Drawing.Point(101, 81)
        Me.txtBal.Name = "txtBal"
        Me.txtBal.ReadOnly = True
        Me.txtBal.Size = New System.Drawing.Size(119, 20)
        Me.txtBal.TabIndex = 24
        Me.txtBal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(43, 85)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(49, 13)
        Me.Label7.TabIndex = 23
        Me.Label7.Text = "Balance:"
        '
        'lblSupplierNo
        '
        Me.lblSupplierNo.AutoSize = True
        Me.lblSupplierNo.Font = New System.Drawing.Font("Wingdings 2", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.lblSupplierNo.Location = New System.Drawing.Point(319, 10)
        Me.lblSupplierNo.Name = "lblSupplierNo"
        Me.lblSupplierNo.Size = New System.Drawing.Size(13, 11)
        Me.lblSupplierNo.TabIndex = 22
        Me.lblSupplierNo.Text = "0"
        '
        'txtAmntPaid
        '
        Me.txtAmntPaid.Location = New System.Drawing.Point(101, 56)
        Me.txtAmntPaid.Name = "txtAmntPaid"
        Me.txtAmntPaid.Size = New System.Drawing.Size(119, 20)
        Me.txtAmntPaid.TabIndex = 1
        Me.txtAmntPaid.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(22, 60)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(70, 13)
        Me.Label3.TabIndex = 16
        Me.Label3.Text = "Amount Paid:"
        '
        'txtCreditsTotal
        '
        Me.txtCreditsTotal.Location = New System.Drawing.Point(101, 31)
        Me.txtCreditsTotal.Name = "txtCreditsTotal"
        Me.txtCreditsTotal.ReadOnly = True
        Me.txtCreditsTotal.Size = New System.Drawing.Size(119, 20)
        Me.txtCreditsTotal.TabIndex = 15
        Me.txtCreditsTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(23, 35)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(69, 13)
        Me.Label4.TabIndex = 14
        Me.Label4.Text = "Credits Total:"
        '
        'btnLoad
        '
        Me.btnLoad.Location = New System.Drawing.Point(384, 6)
        Me.btnLoad.Name = "btnLoad"
        Me.btnLoad.Size = New System.Drawing.Size(101, 35)
        Me.btnLoad.TabIndex = 4
        Me.btnLoad.Text = "Load Data"
        Me.btnLoad.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(7, 10)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(85, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Search Supplier:"
        '
        'txtSearchToPay
        '
        Me.txtSearchToPay.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtSearchToPay.Location = New System.Drawing.Point(101, 6)
        Me.txtSearchToPay.Name = "txtSearchToPay"
        Me.txtSearchToPay.Size = New System.Drawing.Size(212, 20)
        Me.txtSearchToPay.TabIndex = 0
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.dgvPaySupplierInvoices)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(491, 200)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Supplier Unpaid Invoices:"
        '
        'dgvPaySupplierInvoices
        '
        Me.dgvPaySupplierInvoices.AllowUserToAddRows = False
        Me.dgvPaySupplierInvoices.AllowUserToDeleteRows = False
        DataGridViewCellStyle11.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.dgvPaySupplierInvoices.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle11
        Me.dgvPaySupplierInvoices.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle12.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvPaySupplierInvoices.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle12
        Me.dgvPaySupplierInvoices.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvPaySupplierInvoices.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvPaySupplierInvoices.Location = New System.Drawing.Point(3, 16)
        Me.dgvPaySupplierInvoices.Name = "dgvPaySupplierInvoices"
        Me.dgvPaySupplierInvoices.ReadOnly = True
        DataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle13.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgvPaySupplierInvoices.RowsDefaultCellStyle = DataGridViewCellStyle13
        Me.dgvPaySupplierInvoices.Size = New System.Drawing.Size(485, 181)
        Me.dgvPaySupplierInvoices.TabIndex = 4
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancel.Location = New System.Drawing.Point(277, 4)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(101, 35)
        Me.btnCancel.TabIndex = 0
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(384, 3)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(101, 35)
        Me.btnClose.TabIndex = 1
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'btnProcess
        '
        Me.btnProcess.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnProcess.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnProcess.Location = New System.Drawing.Point(116, 5)
        Me.btnProcess.Name = "btnProcess"
        Me.btnProcess.Size = New System.Drawing.Size(151, 32)
        Me.btnProcess.TabIndex = 4
        Me.btnProcess.Text = "Process Payments"
        Me.btnProcess.UseVisualStyleBackColor = True
        '
        'frmSupplierPayments
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(884, 511)
        Me.Controls.Add(Me.SplitContainer2)
        Me.DoubleBuffered = True
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmSupplierPayments"
        Me.Text = "Supplier Payments"
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer5.Panel1.ResumeLayout(False)
        Me.SplitContainer5.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer5.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        CType(Me.dgvSupplierAmnt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvSupplierPayments, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer3.Panel1.ResumeLayout(False)
        Me.SplitContainer3.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer3.ResumeLayout(False)
        Me.SplitContainer4.Panel1.ResumeLayout(False)
        Me.SplitContainer4.Panel1.PerformLayout()
        Me.SplitContainer4.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer4.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.dgvPaySupplierInvoices, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dgvSupplierPayments As System.Windows.Forms.DataGridView
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainer3 As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainer4 As System.Windows.Forms.SplitContainer
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtSearchToPay As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents dgvPaySupplierInvoices As System.Windows.Forms.DataGridView
    Friend WithEvents btnLoad As System.Windows.Forms.Button
    Friend WithEvents txtAmntPaid As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtCreditsTotal As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents btnProcess As System.Windows.Forms.Button
    Friend WithEvents cboBank As System.Windows.Forms.ComboBox
    Friend WithEvents lblBank As System.Windows.Forms.Label
    Friend WithEvents lblSupplierNo As System.Windows.Forms.Label
    Friend WithEvents txtBal As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents txtMpesaRef As System.Windows.Forms.TextBox
    Friend WithEvents txtCash As System.Windows.Forms.TextBox
    Friend WithEvents lblMpesaRef As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtOtherNotes As System.Windows.Forms.TextBox
    Friend WithEvents lblBankRef As System.Windows.Forms.Label
    Friend WithEvents txtBankRefNo As System.Windows.Forms.TextBox
    Friend WithEvents rdbBankTransfer As System.Windows.Forms.RadioButton
    Friend WithEvents rdbMobileTransfer As System.Windows.Forms.RadioButton
    Friend WithEvents rdbCash As System.Windows.Forms.RadioButton
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents dtpPayDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblCheque As System.Windows.Forms.Label
    Friend WithEvents txtChequeNo As System.Windows.Forms.TextBox
    Friend WithEvents rdbCheque As System.Windows.Forms.RadioButton
    Friend WithEvents SplitContainer5 As System.Windows.Forms.SplitContainer
    Friend WithEvents dgvSupplierAmnt As System.Windows.Forms.DataGridView
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents btnPaySelected As System.Windows.Forms.Button
    Friend WithEvents lblSupplier As System.Windows.Forms.Label
    Friend WithEvents ColPay As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents ColDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColSupplier As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColInvoiceNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColTotal As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColPaid As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColStatus As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
