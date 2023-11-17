<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSales
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
        Me.components = New System.ComponentModel.Container()
        Dim Label8 As System.Windows.Forms.Label
        Dim TotalLabel As System.Windows.Forms.Label
        Dim SalesIDLabel As System.Windows.Forms.Label
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSales))
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.GroupBox7 = New System.Windows.Forms.GroupBox()
        Me.txtDiscount = New System.Windows.Forms.TextBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.txttotalamount = New System.Windows.Forms.TextBox()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.txtamountTOpay = New System.Windows.Forms.TextBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtchange = New System.Windows.Forms.TextBox()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.lblVATTotals = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnexit = New System.Windows.Forms.Button()
        Me.lblVATRate = New System.Windows.Forms.Label()
        Me.lblServed = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.txtTotal = New System.Windows.Forms.TextBox()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.dgvSales = New System.Windows.Forms.DataGridView()
        Me.RemoveProductToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SalesCMS = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.btnCreditReport = New System.Windows.Forms.Button()
        Me.btnPayCredits = New System.Windows.Forms.Button()
        Me.btnAddNewCustomer = New System.Windows.Forms.Button()
        Me.btnCreditSales = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.GroupBox6 = New System.Windows.Forms.GroupBox()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.rdbAtCost = New System.Windows.Forms.RadioButton()
        Me.rdbWholeSalePrice = New System.Windows.Forms.RadioButton()
        Me.rdbRetailPrice = New System.Windows.Forms.RadioButton()
        Me.txtBarcode = New System.Windows.Forms.TextBox()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.colDescription = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colqtyBought = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colPrice = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colTotal = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colbarcode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colstock = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colDiscount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColNotPickedItems = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColVAT = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColItemMinSP = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Label8 = New System.Windows.Forms.Label()
        TotalLabel = New System.Windows.Forms.Label()
        SalesIDLabel = New System.Windows.Forms.Label()
        Me.Panel5.SuspendLayout()
        Me.GroupBox7.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel4.SuspendLayout()
        CType(Me.dgvSales, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SalesCMS.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label8
        '
        Label8.BackColor = System.Drawing.SystemColors.ButtonFace
        Label8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Label8.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label8.ForeColor = System.Drawing.Color.Black
        Label8.Location = New System.Drawing.Point(12, 21)
        Label8.Name = "Label8"
        Label8.Size = New System.Drawing.Size(79, 22)
        Label8.TabIndex = 12
        Label8.Text = "Served by:"
        Label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TotalLabel
        '
        TotalLabel.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        TotalLabel.Font = New System.Drawing.Font("Bookman Old Style", 54.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        TotalLabel.Location = New System.Drawing.Point(4, 29)
        TotalLabel.Name = "TotalLabel"
        TotalLabel.Size = New System.Drawing.Size(303, 351)
        TotalLabel.TabIndex = 0
        TotalLabel.Text = "TOTAL:"
        '
        'SalesIDLabel
        '
        SalesIDLabel.BackColor = System.Drawing.SystemColors.ButtonFace
        SalesIDLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        SalesIDLabel.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        SalesIDLabel.Location = New System.Drawing.Point(3, 77)
        SalesIDLabel.Name = "SalesIDLabel"
        SalesIDLabel.Size = New System.Drawing.Size(103, 26)
        SalesIDLabel.TabIndex = 38
        SalesIDLabel.Text = "Item Search:"
        SalesIDLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Panel5.Controls.Add(Me.GroupBox7)
        Me.Panel5.Controls.Add(Me.GroupBox2)
        Me.Panel5.Controls.Add(Me.GroupBox4)
        Me.Panel5.Controls.Add(Me.GroupBox1)
        Me.Panel5.Controls.Add(Me.GroupBox5)
        Me.Panel5.Controls.Add(Me.Panel2)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel5.Location = New System.Drawing.Point(0, 534)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(1084, 87)
        Me.Panel5.TabIndex = 34
        '
        'GroupBox7
        '
        Me.GroupBox7.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox7.BackColor = System.Drawing.Color.Silver
        Me.GroupBox7.Controls.Add(Me.txtDiscount)
        Me.GroupBox7.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox7.Location = New System.Drawing.Point(207, 7)
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.Size = New System.Drawing.Size(180, 75)
        Me.GroupBox7.TabIndex = 64
        Me.GroupBox7.TabStop = False
        Me.GroupBox7.Text = "Discount"
        '
        'txtDiscount
        '
        Me.txtDiscount.BackColor = System.Drawing.Color.White
        Me.txtDiscount.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtDiscount.Font = New System.Drawing.Font("Microsoft Sans Serif", 26.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDiscount.ForeColor = System.Drawing.Color.Lime
        Me.txtDiscount.Location = New System.Drawing.Point(3, 20)
        Me.txtDiscount.Multiline = True
        Me.txtDiscount.Name = "txtDiscount"
        Me.txtDiscount.ReadOnly = True
        Me.txtDiscount.Size = New System.Drawing.Size(174, 52)
        Me.txtDiscount.TabIndex = 3
        Me.txtDiscount.TabStop = False
        Me.txtDiscount.Text = "0.00"
        Me.txtDiscount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.BackColor = System.Drawing.Color.Silver
        Me.GroupBox2.Controls.Add(Me.txttotalamount)
        Me.GroupBox2.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.Location = New System.Drawing.Point(3, 5)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(198, 75)
        Me.GroupBox2.TabIndex = 60
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "TOTAL"
        '
        'txttotalamount
        '
        Me.txttotalamount.BackColor = System.Drawing.Color.White
        Me.txttotalamount.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txttotalamount.Font = New System.Drawing.Font("Microsoft Sans Serif", 26.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txttotalamount.ForeColor = System.Drawing.Color.Red
        Me.txttotalamount.Location = New System.Drawing.Point(3, 18)
        Me.txttotalamount.Multiline = True
        Me.txttotalamount.Name = "txttotalamount"
        Me.txttotalamount.ReadOnly = True
        Me.txttotalamount.Size = New System.Drawing.Size(192, 54)
        Me.txttotalamount.TabIndex = 1
        Me.txttotalamount.TabStop = False
        Me.txttotalamount.Text = "0.00"
        Me.txttotalamount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'GroupBox4
        '
        Me.GroupBox4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox4.BackColor = System.Drawing.Color.Silver
        Me.GroupBox4.Controls.Add(Me.txtamountTOpay)
        Me.GroupBox4.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox4.Location = New System.Drawing.Point(393, 7)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(188, 75)
        Me.GroupBox4.TabIndex = 61
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "AMOUNT TO PAY"
        '
        'txtamountTOpay
        '
        Me.txtamountTOpay.BackColor = System.Drawing.Color.White
        Me.txtamountTOpay.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtamountTOpay.Font = New System.Drawing.Font("Microsoft Sans Serif", 26.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtamountTOpay.Location = New System.Drawing.Point(3, 18)
        Me.txtamountTOpay.Multiline = True
        Me.txtamountTOpay.Name = "txtamountTOpay"
        Me.txtamountTOpay.Size = New System.Drawing.Size(182, 54)
        Me.txtamountTOpay.TabIndex = 2
        Me.txtamountTOpay.Text = "0.00"
        Me.txtamountTOpay.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.GroupBox1.Controls.Add(Me.txtchange)
        Me.GroupBox1.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(586, 7)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(186, 75)
        Me.GroupBox1.TabIndex = 62
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "CHANGE"
        '
        'txtchange
        '
        Me.txtchange.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtchange.Font = New System.Drawing.Font("Microsoft Sans Serif", 26.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtchange.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtchange.Location = New System.Drawing.Point(3, 18)
        Me.txtchange.Multiline = True
        Me.txtchange.Name = "txtchange"
        Me.txtchange.Size = New System.Drawing.Size(180, 54)
        Me.txtchange.TabIndex = 3
        Me.txtchange.Text = "0.00"
        Me.txtchange.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'GroupBox5
        '
        Me.GroupBox5.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.GroupBox5.Controls.Add(Me.lblVATTotals)
        Me.GroupBox5.Controls.Add(Me.Label3)
        Me.GroupBox5.Controls.Add(Me.btnexit)
        Me.GroupBox5.Controls.Add(Me.lblVATRate)
        Me.GroupBox5.Controls.Add(Me.lblServed)
        Me.GroupBox5.Controls.Add(Label8)
        Me.GroupBox5.Controls.Add(Me.Label1)
        Me.GroupBox5.Dock = System.Windows.Forms.DockStyle.Right
        Me.GroupBox5.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox5.ForeColor = System.Drawing.Color.Black
        Me.GroupBox5.Location = New System.Drawing.Point(776, 0)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(308, 87)
        Me.GroupBox5.TabIndex = 63
        Me.GroupBox5.TabStop = False
        '
        'lblVATTotals
        '
        Me.lblVATTotals.AutoSize = True
        Me.lblVATTotals.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVATTotals.ForeColor = System.Drawing.Color.Red
        Me.lblVATTotals.Location = New System.Drawing.Point(178, 37)
        Me.lblVATTotals.Name = "lblVATTotals"
        Me.lblVATTotals.Size = New System.Drawing.Size(13, 13)
        Me.lblVATTotals.TabIndex = 56
        Me.lblVATTotals.Text = "0"
        Me.lblVATTotals.Visible = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(107, 37)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(62, 13)
        Me.Label3.TabIndex = 55
        Me.Label3.Text = "VAT Totals:"
        Me.Label3.Visible = False
        '
        'btnexit
        '
        Me.btnexit.BackColor = System.Drawing.Color.Aquamarine
        Me.btnexit.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnexit.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnexit.Image = Global.Strawberry.My.Resources.Resources.close
        Me.btnexit.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnexit.Location = New System.Drawing.Point(223, 21)
        Me.btnexit.Name = "btnexit"
        Me.btnexit.Size = New System.Drawing.Size(82, 63)
        Me.btnexit.TabIndex = 52
        Me.btnexit.Text = "Close"
        Me.btnexit.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnexit.UseVisualStyleBackColor = False
        '
        'lblVATRate
        '
        Me.lblVATRate.AutoSize = True
        Me.lblVATRate.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVATRate.ForeColor = System.Drawing.Color.Red
        Me.lblVATRate.Location = New System.Drawing.Point(178, 14)
        Me.lblVATRate.Name = "lblVATRate"
        Me.lblVATRate.Size = New System.Drawing.Size(13, 13)
        Me.lblVATRate.TabIndex = 3
        Me.lblVATRate.Text = "0"
        Me.lblVATRate.Visible = False
        '
        'lblServed
        '
        Me.lblServed.BackColor = System.Drawing.Color.White
        Me.lblServed.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblServed.ForeColor = System.Drawing.Color.Black
        Me.lblServed.Location = New System.Drawing.Point(9, 54)
        Me.lblServed.Name = "lblServed"
        Me.lblServed.Size = New System.Drawing.Size(181, 23)
        Me.lblServed.TabIndex = 13
        Me.lblServed.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(99, 14)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(70, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "VAT % Rate:"
        Me.Label1.Visible = False
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Panel2.Controls.Add(TotalLabel)
        Me.Panel2.Controls.Add(Me.txtTotal)
        Me.Panel2.Location = New System.Drawing.Point(9, -474)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(590, 357)
        Me.Panel2.TabIndex = 50
        '
        'txtTotal
        '
        Me.txtTotal.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtTotal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotal.Font = New System.Drawing.Font("Bookman Old Style", 54.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotal.ForeColor = System.Drawing.Color.Black
        Me.txtTotal.Location = New System.Drawing.Point(313, 288)
        Me.txtTotal.Name = "txtTotal"
        Me.txtTotal.ReadOnly = True
        Me.txtTotal.Size = New System.Drawing.Size(271, 93)
        Me.txtTotal.TabIndex = 1
        Me.txtTotal.TabStop = False
        Me.txtTotal.Text = "000"
        Me.txtTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Panel4
        '
        Me.Panel4.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel4.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.Panel4.Controls.Add(Me.dgvSales)
        Me.Panel4.Location = New System.Drawing.Point(5, 134)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(1072, 394)
        Me.Panel4.TabIndex = 36
        '
        'dgvSales
        '
        Me.dgvSales.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvSales.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvSales.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvSales.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvSales.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colDescription, Me.colqtyBought, Me.colPrice, Me.colTotal, Me.colbarcode, Me.colstock, Me.colDiscount, Me.ColNotPickedItems, Me.ColVAT, Me.ColItemMinSP})
        Me.dgvSales.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvSales.GridColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.dgvSales.Location = New System.Drawing.Point(0, 0)
        Me.dgvSales.Name = "dgvSales"
        DataGridViewCellStyle11.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgvSales.RowsDefaultCellStyle = DataGridViewCellStyle11
        Me.dgvSales.Size = New System.Drawing.Size(1072, 394)
        Me.dgvSales.TabIndex = 35
        '
        'RemoveProductToolStripMenuItem
        '
        Me.RemoveProductToolStripMenuItem.Name = "RemoveProductToolStripMenuItem"
        Me.RemoveProductToolStripMenuItem.Size = New System.Drawing.Size(162, 22)
        Me.RemoveProductToolStripMenuItem.Text = "Remove product"
        '
        'SalesCMS
        '
        Me.SalesCMS.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.RemoveProductToolStripMenuItem})
        Me.SalesCMS.Name = "SalesCMS"
        Me.SalesCMS.Size = New System.Drawing.Size(163, 26)
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        Me.Timer1.Interval = 1000
        '
        'GroupBox3
        '
        Me.GroupBox3.BackColor = System.Drawing.Color.MintCream
        Me.GroupBox3.Controls.Add(Me.btnCreditReport)
        Me.GroupBox3.Controls.Add(Me.btnPayCredits)
        Me.GroupBox3.Controls.Add(Me.btnAddNewCustomer)
        Me.GroupBox3.Controls.Add(Me.btnCreditSales)
        Me.GroupBox3.Dock = System.Windows.Forms.DockStyle.Right
        Me.GroupBox3.Location = New System.Drawing.Point(542, 0)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(518, 116)
        Me.GroupBox3.TabIndex = 52
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Invoice Sales:"
        '
        'btnCreditReport
        '
        Me.btnCreditReport.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btnCreditReport.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCreditReport.Image = CType(resources.GetObject("btnCreditReport.Image"), System.Drawing.Image)
        Me.btnCreditReport.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnCreditReport.Location = New System.Drawing.Point(389, 27)
        Me.btnCreditReport.Name = "btnCreditReport"
        Me.btnCreditReport.Size = New System.Drawing.Size(107, 75)
        Me.btnCreditReport.TabIndex = 58
        Me.btnCreditReport.Text = "Customer Invoice A/C"
        Me.btnCreditReport.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnCreditReport.UseVisualStyleBackColor = True
        '
        'btnPayCredits
        '
        Me.btnPayCredits.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btnPayCredits.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPayCredits.Image = Global.Strawberry.My.Resources.Resources.paycredit
        Me.btnPayCredits.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnPayCredits.Location = New System.Drawing.Point(267, 27)
        Me.btnPayCredits.Name = "btnPayCredits"
        Me.btnPayCredits.Size = New System.Drawing.Size(107, 75)
        Me.btnPayCredits.TabIndex = 57
        Me.btnPayCredits.Text = "Invoice Payment"
        Me.btnPayCredits.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnPayCredits.UseVisualStyleBackColor = True
        '
        'btnAddNewCustomer
        '
        Me.btnAddNewCustomer.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btnAddNewCustomer.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddNewCustomer.Image = Global.Strawberry.My.Resources.Resources.add
        Me.btnAddNewCustomer.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnAddNewCustomer.Location = New System.Drawing.Point(23, 27)
        Me.btnAddNewCustomer.Name = "btnAddNewCustomer"
        Me.btnAddNewCustomer.Size = New System.Drawing.Size(107, 75)
        Me.btnAddNewCustomer.TabIndex = 53
        Me.btnAddNewCustomer.Text = "Add Invoice Customer"
        Me.btnAddNewCustomer.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnAddNewCustomer.UseVisualStyleBackColor = True
        '
        'btnCreditSales
        '
        Me.btnCreditSales.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btnCreditSales.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCreditSales.Image = Global.Strawberry.My.Resources.Resources.creditsale
        Me.btnCreditSales.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnCreditSales.Location = New System.Drawing.Point(145, 27)
        Me.btnCreditSales.Name = "btnCreditSales"
        Me.btnCreditSales.Size = New System.Drawing.Size(107, 75)
        Me.btnCreditSales.TabIndex = 52
        Me.btnCreditSales.Text = "Invoice Sales"
        Me.btnCreditSales.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnCreditSales.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.Panel1.Controls.Add(Me.GroupBox6)
        Me.Panel1.Controls.Add(SalesIDLabel)
        Me.Panel1.Controls.Add(Me.txtBarcode)
        Me.Panel1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(536, 114)
        Me.Panel1.TabIndex = 54
        '
        'GroupBox6
        '
        Me.GroupBox6.BackColor = System.Drawing.Color.Honeydew
        Me.GroupBox6.Controls.Add(Me.btnCancel)
        Me.GroupBox6.Controls.Add(Me.rdbAtCost)
        Me.GroupBox6.Controls.Add(Me.rdbWholeSalePrice)
        Me.GroupBox6.Controls.Add(Me.rdbRetailPrice)
        Me.GroupBox6.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox6.Font = New System.Drawing.Font("Times New Roman", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox6.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(536, 65)
        Me.GroupBox6.TabIndex = 40
        Me.GroupBox6.TabStop = False
        Me.GroupBox6.Text = "Sale Type:"
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancel.Location = New System.Drawing.Point(330, 11)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(100, 47)
        Me.btnCancel.TabIndex = 3
        Me.btnCancel.Text = "Cancel Transaction"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'rdbAtCost
        '
        Me.rdbAtCost.AutoSize = True
        Me.rdbAtCost.Enabled = False
        Me.rdbAtCost.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbAtCost.Location = New System.Drawing.Point(238, 24)
        Me.rdbAtCost.Name = "rdbAtCost"
        Me.rdbAtCost.Size = New System.Drawing.Size(79, 23)
        Me.rdbAtCost.TabIndex = 2
        Me.rdbAtCost.Text = "At Cost"
        Me.rdbAtCost.UseVisualStyleBackColor = True
        '
        'rdbWholeSalePrice
        '
        Me.rdbWholeSalePrice.AutoSize = True
        Me.rdbWholeSalePrice.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbWholeSalePrice.Location = New System.Drawing.Point(120, 24)
        Me.rdbWholeSalePrice.Name = "rdbWholeSalePrice"
        Me.rdbWholeSalePrice.Size = New System.Drawing.Size(105, 23)
        Me.rdbWholeSalePrice.TabIndex = 1
        Me.rdbWholeSalePrice.Text = "Whole Sale"
        Me.rdbWholeSalePrice.UseVisualStyleBackColor = True
        '
        'rdbRetailPrice
        '
        Me.rdbRetailPrice.AutoSize = True
        Me.rdbRetailPrice.Checked = True
        Me.rdbRetailPrice.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbRetailPrice.Location = New System.Drawing.Point(7, 24)
        Me.rdbRetailPrice.Name = "rdbRetailPrice"
        Me.rdbRetailPrice.Size = New System.Drawing.Size(100, 23)
        Me.rdbRetailPrice.TabIndex = 0
        Me.rdbRetailPrice.TabStop = True
        Me.rdbRetailPrice.Text = "Retail Sale"
        Me.rdbRetailPrice.UseVisualStyleBackColor = True
        '
        'txtBarcode
        '
        Me.txtBarcode.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBarcode.Location = New System.Drawing.Point(112, 77)
        Me.txtBarcode.Name = "txtBarcode"
        Me.txtBarcode.Size = New System.Drawing.Size(318, 27)
        Me.txtBarcode.TabIndex = 39
        Me.txtBarcode.TabStop = False
        '
        'Panel3
        '
        Me.Panel3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel3.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.Panel3.Controls.Add(Me.Panel1)
        Me.Panel3.Controls.Add(Me.GroupBox3)
        Me.Panel3.Location = New System.Drawing.Point(12, 12)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(1060, 116)
        Me.Panel3.TabIndex = 33
        '
        'colDescription
        '
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.colDescription.DefaultCellStyle = DataGridViewCellStyle2
        Me.colDescription.DividerWidth = 3
        Me.colDescription.FillWeight = 255.0!
        Me.colDescription.HeaderText = "Description"
        Me.colDescription.MinimumWidth = 300
        Me.colDescription.Name = "colDescription"
        Me.colDescription.ReadOnly = True
        Me.colDescription.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        '
        'colqtyBought
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.colqtyBought.DefaultCellStyle = DataGridViewCellStyle3
        Me.colqtyBought.DividerWidth = 3
        Me.colqtyBought.FillWeight = 40.0!
        Me.colqtyBought.HeaderText = "Qty "
        Me.colqtyBought.MinimumWidth = 100
        Me.colqtyBought.Name = "colqtyBought"
        Me.colqtyBought.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        '
        'colPrice
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.colPrice.DefaultCellStyle = DataGridViewCellStyle4
        Me.colPrice.DividerWidth = 3
        Me.colPrice.FillWeight = 40.0!
        Me.colPrice.HeaderText = "Price"
        Me.colPrice.MinimumWidth = 120
        Me.colPrice.Name = "colPrice"
        Me.colPrice.ReadOnly = True
        Me.colPrice.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        '
        'colTotal
        '
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        DataGridViewCellStyle5.Format = "N2"
        DataGridViewCellStyle5.NullValue = Nothing
        Me.colTotal.DefaultCellStyle = DataGridViewCellStyle5
        Me.colTotal.DividerWidth = 3
        Me.colTotal.FillWeight = 50.0!
        Me.colTotal.HeaderText = "Total"
        Me.colTotal.MinimumWidth = 200
        Me.colTotal.Name = "colTotal"
        Me.colTotal.ReadOnly = True
        Me.colTotal.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        '
        'colbarcode
        '
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.colbarcode.DefaultCellStyle = DataGridViewCellStyle6
        Me.colbarcode.DividerWidth = 2
        Me.colbarcode.HeaderText = "Barcode"
        Me.colbarcode.MinimumWidth = 150
        Me.colbarcode.Name = "colbarcode"
        Me.colbarcode.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.colbarcode.Visible = False
        '
        'colstock
        '
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle7.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle7.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.colstock.DefaultCellStyle = DataGridViewCellStyle7
        Me.colstock.DividerWidth = 3
        Me.colstock.FillWeight = 40.0!
        Me.colstock.HeaderText = "Stock Balance"
        Me.colstock.MinimumWidth = 150
        Me.colstock.Name = "colstock"
        Me.colstock.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        '
        'colDiscount
        '
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle8.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.colDiscount.DefaultCellStyle = DataGridViewCellStyle8
        Me.colDiscount.FillWeight = 80.0!
        Me.colDiscount.HeaderText = "Discount"
        Me.colDiscount.MinimumWidth = 90
        Me.colDiscount.Name = "colDiscount"
        Me.colDiscount.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        '
        'ColNotPickedItems
        '
        Me.ColNotPickedItems.DividerWidth = 2
        Me.ColNotPickedItems.FillWeight = 40.0!
        Me.ColNotPickedItems.HeaderText = "ItemBP"
        Me.ColNotPickedItems.MinimumWidth = 150
        Me.ColNotPickedItems.Name = "ColNotPickedItems"
        Me.ColNotPickedItems.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.ColNotPickedItems.Visible = False
        '
        'ColVAT
        '
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.ColVAT.DefaultCellStyle = DataGridViewCellStyle9
        Me.ColVAT.DividerWidth = 5
        Me.ColVAT.FillWeight = 80.0!
        Me.ColVAT.HeaderText = "V.A.T"
        Me.ColVAT.MinimumWidth = 100
        Me.ColVAT.Name = "ColVAT"
        Me.ColVAT.ReadOnly = True
        Me.ColVAT.Visible = False
        '
        'ColItemMinSP
        '
        DataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.ColItemMinSP.DefaultCellStyle = DataGridViewCellStyle10
        Me.ColItemMinSP.HeaderText = "Minimum SP"
        Me.ColItemMinSP.MinimumWidth = 80
        Me.ColItemMinSP.Name = "ColItemMinSP"
        Me.ColItemMinSP.ReadOnly = True
        Me.ColItemMinSP.Visible = False
        '
        'frmSales
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Gray
        Me.ClientSize = New System.Drawing.Size(1084, 621)
        Me.ContextMenuStrip = Me.SalesCMS
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Panel3)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmSales"
        Me.Text = "Sales"
        Me.Panel5.ResumeLayout(False)
        Me.GroupBox7.ResumeLayout(False)
        Me.GroupBox7.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        CType(Me.dgvSales, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SalesCMS.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents lblServed As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents txttotalamount As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents txtamountTOpay As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtchange As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox7 As System.Windows.Forms.GroupBox
    Friend WithEvents txtDiscount As System.Windows.Forms.TextBox
    Friend WithEvents lblVATRate As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents txtTotal As System.Windows.Forms.TextBox
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents dgvSales As System.Windows.Forms.DataGridView
    Friend WithEvents RemoveProductToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SalesCMS As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents btnCreditReport As System.Windows.Forms.Button
    Friend WithEvents btnPayCredits As System.Windows.Forms.Button
    Friend WithEvents btnAddNewCustomer As System.Windows.Forms.Button
    Friend WithEvents btnCreditSales As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Friend WithEvents rdbWholeSalePrice As System.Windows.Forms.RadioButton
    Friend WithEvents rdbRetailPrice As System.Windows.Forms.RadioButton
    Friend WithEvents txtBarcode As System.Windows.Forms.TextBox
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents btnexit As System.Windows.Forms.Button
    Friend WithEvents rdbAtCost As System.Windows.Forms.RadioButton
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents lblVATTotals As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents colDescription As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colqtyBought As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colPrice As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colTotal As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colbarcode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colstock As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colDiscount As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColNotPickedItems As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColVAT As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColItemMinSP As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
