<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMainDelivery
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMainDelivery))
        Dim DataGridViewCellStyle12 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.rdoInvoiceno = New System.Windows.Forms.RadioButton()
        Me.rdoOrderno = New System.Windows.Forms.RadioButton()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtsearch = New System.Windows.Forms.TextBox()
        Me.dgvdeliveries = New System.Windows.Forms.DataGridView()
        Me.colitemName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colQtyOrdered = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colOrderUnitPrice = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colTotalOrderPrice = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colQtyDelivered = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colDeliveryUPrice = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colTotalDeliveryPrice = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colItemBP = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colItemsPerPack = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colTotalIDelivered = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colQtyVariance = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colPriceVariance = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colBarcode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colOrderNum = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cmsReceivedStock = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.RemoveProductToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.UpdateExpiryAndBatchNoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.btnsave = New System.Windows.Forms.Button()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.dtpExpiryDate = New System.Windows.Forms.DateTimePicker()
        Me.txtBatchNo = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtTotalAmnt = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.cboSuppliers = New System.Windows.Forms.ComboBox()
        Me.txtreceiptnumber = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtothernotes = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtvehicleregno = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.dtpDateReceived = New System.Windows.Forms.DateTimePicker()
        Me.txtInvoiceNo = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout()
        CType(Me.dgvdeliveries, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.cmsReceivedStock.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.GroupBox1.Controls.Add(Me.rdoInvoiceno)
        Me.GroupBox1.Controls.Add(Me.rdoOrderno)
        Me.GroupBox1.Controls.Add(Me.btnSearch)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.txtsearch)
        Me.GroupBox1.Location = New System.Drawing.Point(689, 4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(390, 63)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'rdoInvoiceno
        '
        Me.rdoInvoiceno.BackColor = System.Drawing.Color.SpringGreen
        Me.rdoInvoiceno.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdoInvoiceno.Location = New System.Drawing.Point(135, 34)
        Me.rdoInvoiceno.Name = "rdoInvoiceno"
        Me.rdoInvoiceno.Size = New System.Drawing.Size(115, 23)
        Me.rdoInvoiceno.TabIndex = 4
        Me.rdoInvoiceno.TabStop = True
        Me.rdoInvoiceno.Text = "Invoice \ Delivery No."
        Me.rdoInvoiceno.UseVisualStyleBackColor = False
        '
        'rdoOrderno
        '
        Me.rdoOrderno.BackColor = System.Drawing.Color.SpringGreen
        Me.rdoOrderno.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdoOrderno.Location = New System.Drawing.Point(135, 7)
        Me.rdoOrderno.Name = "rdoOrderno"
        Me.rdoOrderno.Size = New System.Drawing.Size(115, 24)
        Me.rdoOrderno.TabIndex = 3
        Me.rdoOrderno.TabStop = True
        Me.rdoOrderno.Text = "Order No."
        Me.rdoOrderno.UseVisualStyleBackColor = False
        '
        'btnSearch
        '
        Me.btnSearch.Image = CType(resources.GetObject("btnSearch.Image"), System.Drawing.Image)
        Me.btnSearch.Location = New System.Drawing.Point(322, 8)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(64, 49)
        Me.btnSearch.TabIndex = 2
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Black
        Me.Label1.Font = New System.Drawing.Font("Times New Roman", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.Label1.Location = New System.Drawing.Point(3, 3)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(251, 57)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "      Search by :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtsearch
        '
        Me.txtsearch.Font = New System.Drawing.Font("Bookman Old Style", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtsearch.Location = New System.Drawing.Point(256, 9)
        Me.txtsearch.Multiline = True
        Me.txtsearch.Name = "txtsearch"
        Me.txtsearch.Size = New System.Drawing.Size(64, 48)
        Me.txtsearch.TabIndex = 0
        '
        'dgvdeliveries
        '
        Me.dgvdeliveries.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvdeliveries.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvdeliveries.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight
        Me.dgvdeliveries.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvdeliveries.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colitemName, Me.colQtyOrdered, Me.colOrderUnitPrice, Me.colTotalOrderPrice, Me.colQtyDelivered, Me.colDeliveryUPrice, Me.colTotalDeliveryPrice, Me.colItemBP, Me.colItemsPerPack, Me.colTotalIDelivered, Me.colQtyVariance, Me.colPriceVariance, Me.colBarcode, Me.colOrderNum})
        Me.dgvdeliveries.ContextMenuStrip = Me.cmsReceivedStock
        Me.dgvdeliveries.Location = New System.Drawing.Point(3, 104)
        Me.dgvdeliveries.Name = "dgvdeliveries"
        DataGridViewCellStyle12.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgvdeliveries.RowsDefaultCellStyle = DataGridViewCellStyle12
        Me.dgvdeliveries.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.dgvdeliveries.Size = New System.Drawing.Size(1073, 265)
        Me.dgvdeliveries.TabIndex = 1
        '
        'colitemName
        '
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.colitemName.DefaultCellStyle = DataGridViewCellStyle1
        Me.colitemName.HeaderText = "Item Name"
        Me.colitemName.Name = "colitemName"
        Me.colitemName.ReadOnly = True
        '
        'colQtyOrdered
        '
        Me.colQtyOrdered.HeaderText = "Qty Ordered"
        Me.colQtyOrdered.Name = "colQtyOrdered"
        Me.colQtyOrdered.ReadOnly = True
        '
        'colOrderUnitPrice
        '
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.colOrderUnitPrice.DefaultCellStyle = DataGridViewCellStyle2
        Me.colOrderUnitPrice.HeaderText = "Order Unit Price"
        Me.colOrderUnitPrice.Name = "colOrderUnitPrice"
        Me.colOrderUnitPrice.ReadOnly = True
        '
        'colTotalOrderPrice
        '
        Me.colTotalOrderPrice.HeaderText = "Total Order Price"
        Me.colTotalOrderPrice.Name = "colTotalOrderPrice"
        Me.colTotalOrderPrice.ReadOnly = True
        '
        'colQtyDelivered
        '
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.colQtyDelivered.DefaultCellStyle = DataGridViewCellStyle3
        Me.colQtyDelivered.HeaderText = "Quantity Delivered"
        Me.colQtyDelivered.Name = "colQtyDelivered"
        '
        'colDeliveryUPrice
        '
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.colDeliveryUPrice.DefaultCellStyle = DataGridViewCellStyle4
        Me.colDeliveryUPrice.HeaderText = "Delivery Unit Price Per Carton"
        Me.colDeliveryUPrice.Name = "colDeliveryUPrice"
        '
        'colTotalDeliveryPrice
        '
        DataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.colTotalDeliveryPrice.DefaultCellStyle = DataGridViewCellStyle5
        Me.colTotalDeliveryPrice.HeaderText = "Total Delivery Price"
        Me.colTotalDeliveryPrice.Name = "colTotalDeliveryPrice"
        Me.colTotalDeliveryPrice.ReadOnly = True
        '
        'colItemBP
        '
        DataGridViewCellStyle6.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle6.ForeColor = System.Drawing.Color.Red
        Me.colItemBP.DefaultCellStyle = DataGridViewCellStyle6
        Me.colItemBP.HeaderText = "Single Item Buying Price in Carton"
        Me.colItemBP.Name = "colItemBP"
        Me.colItemBP.ReadOnly = True
        '
        'colItemsPerPack
        '
        DataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        DataGridViewCellStyle7.ForeColor = System.Drawing.Color.Red
        Me.colItemsPerPack.DefaultCellStyle = DataGridViewCellStyle7
        Me.colItemsPerPack.HeaderText = "Items Per Pack"
        Me.colItemsPerPack.Name = "colItemsPerPack"
        Me.colItemsPerPack.ReadOnly = True
        '
        'colTotalIDelivered
        '
        DataGridViewCellStyle8.ForeColor = System.Drawing.Color.Red
        Me.colTotalIDelivered.DefaultCellStyle = DataGridViewCellStyle8
        Me.colTotalIDelivered.HeaderText = "Total Items Delivered"
        Me.colTotalIDelivered.Name = "colTotalIDelivered"
        Me.colTotalIDelivered.ReadOnly = True
        '
        'colQtyVariance
        '
        DataGridViewCellStyle9.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        DataGridViewCellStyle9.ForeColor = System.Drawing.Color.Red
        Me.colQtyVariance.DefaultCellStyle = DataGridViewCellStyle9
        Me.colQtyVariance.HeaderText = "Quantity Variance"
        Me.colQtyVariance.Name = "colQtyVariance"
        Me.colQtyVariance.ReadOnly = True
        '
        'colPriceVariance
        '
        DataGridViewCellStyle10.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        DataGridViewCellStyle10.ForeColor = System.Drawing.Color.Red
        Me.colPriceVariance.DefaultCellStyle = DataGridViewCellStyle10
        Me.colPriceVariance.HeaderText = "Price Variance"
        Me.colPriceVariance.Name = "colPriceVariance"
        Me.colPriceVariance.ReadOnly = True
        '
        'colBarcode
        '
        Me.colBarcode.HeaderText = "Barcode"
        Me.colBarcode.Name = "colBarcode"
        Me.colBarcode.ReadOnly = True
        '
        'colOrderNum
        '
        DataGridViewCellStyle11.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.colOrderNum.DefaultCellStyle = DataGridViewCellStyle11
        Me.colOrderNum.HeaderText = "Order Number"
        Me.colOrderNum.Name = "colOrderNum"
        Me.colOrderNum.ReadOnly = True
        '
        'cmsReceivedStock
        '
        Me.cmsReceivedStock.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.RemoveProductToolStripMenuItem, Me.UpdateExpiryAndBatchNoToolStripMenuItem})
        Me.cmsReceivedStock.Name = "cmsReceivedStock"
        Me.cmsReceivedStock.Size = New System.Drawing.Size(224, 48)
        '
        'RemoveProductToolStripMenuItem
        '
        Me.RemoveProductToolStripMenuItem.Name = "RemoveProductToolStripMenuItem"
        Me.RemoveProductToolStripMenuItem.Size = New System.Drawing.Size(223, 22)
        Me.RemoveProductToolStripMenuItem.Text = "Remove Product"
        '
        'UpdateExpiryAndBatchNoToolStripMenuItem
        '
        Me.UpdateExpiryAndBatchNoToolStripMenuItem.Name = "UpdateExpiryAndBatchNoToolStripMenuItem"
        Me.UpdateExpiryAndBatchNoToolStripMenuItem.Size = New System.Drawing.Size(223, 22)
        Me.UpdateExpiryAndBatchNoToolStripMenuItem.Text = "Update Expiry And Batch No"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Image = Global.Strawberry.My.Resources.Resources.Save
        Me.btnsave.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnsave.Location = New System.Drawing.Point(819, 452)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(79, 54)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "Save"
        Me.btnsave.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnsave.UseVisualStyleBackColor = True
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnExit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExit.Image = Global.Strawberry.My.Resources.Resources.close
        Me.btnExit.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnExit.Location = New System.Drawing.Point(1003, 452)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(78, 54)
        Me.btnExit.TabIndex = 2
        Me.btnExit.Text = "Close"
        Me.btnExit.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.BackColor = System.Drawing.Color.LightGray
        Me.GroupBox2.Controls.Add(Me.dtpExpiryDate)
        Me.GroupBox2.Controls.Add(Me.txtBatchNo)
        Me.GroupBox2.Controls.Add(Me.Label11)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Controls.Add(Me.txtTotalAmnt)
        Me.GroupBox2.Controls.Add(Me.dgvdeliveries)
        Me.GroupBox2.Controls.Add(Me.Label9)
        Me.GroupBox2.Controls.Add(Me.cboSuppliers)
        Me.GroupBox2.Controls.Add(Me.txtreceiptnumber)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.txtothernotes)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.txtvehicleregno)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.dtpDateReceived)
        Me.GroupBox2.Controls.Add(Me.txtInvoiceNo)
        Me.GroupBox2.Controls.Add(Me.Label10)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.Location = New System.Drawing.Point(2, 70)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(1079, 372)
        Me.GroupBox2.TabIndex = 0
        Me.GroupBox2.TabStop = False
        '
        'dtpExpiryDate
        '
        Me.dtpExpiryDate.Location = New System.Drawing.Point(772, 74)
        Me.dtpExpiryDate.Name = "dtpExpiryDate"
        Me.dtpExpiryDate.Size = New System.Drawing.Size(148, 21)
        Me.dtpExpiryDate.TabIndex = 31
        '
        'txtBatchNo
        '
        Me.txtBatchNo.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBatchNo.Location = New System.Drawing.Point(772, 41)
        Me.txtBatchNo.Multiline = True
        Me.txtBatchNo.Name = "txtBatchNo"
        Me.txtBatchNo.Size = New System.Drawing.Size(148, 21)
        Me.txtBatchNo.TabIndex = 30
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.Gray
        Me.Label11.Location = New System.Drawing.Point(687, 77)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(79, 19)
        Me.Label11.TabIndex = 29
        Me.Label11.Text = "Expiry Date"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.Gray
        Me.Label7.Location = New System.Drawing.Point(687, 44)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(79, 19)
        Me.Label7.TabIndex = 28
        Me.Label7.Text = "Batch No"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtTotalAmnt
        '
        Me.txtTotalAmnt.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalAmnt.Location = New System.Drawing.Point(421, 37)
        Me.txtTotalAmnt.Name = "txtTotalAmnt"
        Me.txtTotalAmnt.Size = New System.Drawing.Size(153, 21)
        Me.txtTotalAmnt.TabIndex = 27
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.Gray
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(306, 9)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(106, 19)
        Me.Label9.TabIndex = 26
        Me.Label9.Text = "Supplier"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cboSuppliers
        '
        Me.cboSuppliers.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboSuppliers.FormattingEnabled = True
        Me.cboSuppliers.Location = New System.Drawing.Point(421, 7)
        Me.cboSuppliers.Name = "cboSuppliers"
        Me.cboSuppliers.Size = New System.Drawing.Size(260, 23)
        Me.cboSuppliers.TabIndex = 25
        '
        'txtreceiptnumber
        '
        Me.txtreceiptnumber.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtreceiptnumber.Location = New System.Drawing.Point(130, 38)
        Me.txtreceiptnumber.Name = "txtreceiptnumber"
        Me.txtreceiptnumber.Size = New System.Drawing.Size(154, 21)
        Me.txtreceiptnumber.TabIndex = 21
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.Gray
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(18, 39)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(106, 20)
        Me.Label6.TabIndex = 20
        Me.Label6.Text = "Receipt-No:"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtothernotes
        '
        Me.txtothernotes.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtothernotes.Location = New System.Drawing.Point(772, 7)
        Me.txtothernotes.Multiline = True
        Me.txtothernotes.Name = "txtothernotes"
        Me.txtothernotes.Size = New System.Drawing.Size(301, 21)
        Me.txtothernotes.TabIndex = 0
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.Gray
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(689, 7)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(77, 23)
        Me.Label4.TabIndex = 19
        Me.Label4.Text = "Other Notes"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtvehicleregno
        '
        Me.txtvehicleregno.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtvehicleregno.Location = New System.Drawing.Point(421, 66)
        Me.txtvehicleregno.Name = "txtvehicleregno"
        Me.txtvehicleregno.Size = New System.Drawing.Size(167, 21)
        Me.txtvehicleregno.TabIndex = 18
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Gray
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(306, 67)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(106, 19)
        Me.Label2.TabIndex = 17
        Me.Label2.Text = "Vehicle Reg No:"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'dtpDateReceived
        '
        Me.dtpDateReceived.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpDateReceived.Location = New System.Drawing.Point(130, 67)
        Me.dtpDateReceived.Name = "dtpDateReceived"
        Me.dtpDateReceived.Size = New System.Drawing.Size(153, 21)
        Me.dtpDateReceived.TabIndex = 8
        '
        'txtInvoiceNo
        '
        Me.txtInvoiceNo.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInvoiceNo.Location = New System.Drawing.Point(130, 10)
        Me.txtInvoiceNo.Name = "txtInvoiceNo"
        Me.txtInvoiceNo.Size = New System.Drawing.Size(154, 21)
        Me.txtInvoiceNo.TabIndex = 2
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.Gray
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(306, 38)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(106, 19)
        Me.Label10.TabIndex = 13
        Me.Label10.Text = "Total  Amount"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.Gray
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(18, 9)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(106, 20)
        Me.Label5.TabIndex = 1
        Me.Label5.Text = "Invoice-No:"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.Gray
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(18, 68)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(106, 20)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "Date  Received"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancel.Image = Global.Strawberry.My.Resources.Resources.cancel
        Me.btnCancel.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnCancel.Location = New System.Drawing.Point(911, 452)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(79, 54)
        Me.btnCancel.TabIndex = 1
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(4, 46)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(282, 23)
        Me.Label8.TabIndex = 8
        Me.Label8.Text = "FILL ALL REQUIRED FIELDS BELOW:"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'frmMainDelivery
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.CancelButton = Me.btnExit
        Me.ClientSize = New System.Drawing.Size(1084, 511)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnsave)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmMainDelivery"
        Me.Text = "Stock Delivered"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.dgvdeliveries, System.ComponentModel.ISupportInitialize).EndInit()
        Me.cmsReceivedStock.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtsearch As System.Windows.Forms.TextBox
    Friend WithEvents dgvdeliveries As System.Windows.Forms.DataGridView
    Friend WithEvents btnsave As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents dtpDateReceived As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtInvoiceNo As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmsReceivedStock As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents txtothernotes As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtvehicleregno As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtreceiptnumber As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents rdoInvoiceno As System.Windows.Forms.RadioButton
    Friend WithEvents rdoOrderno As System.Windows.Forms.RadioButton
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents cboSuppliers As System.Windows.Forms.ComboBox
    Friend WithEvents txtTotalAmnt As System.Windows.Forms.TextBox
    Friend WithEvents RemoveProductToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents UpdateExpiryAndBatchNoToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents dtpExpiryDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtBatchNo As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents colitemName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colQtyOrdered As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colOrderUnitPrice As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colTotalOrderPrice As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colQtyDelivered As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colDeliveryUPrice As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colTotalDeliveryPrice As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colItemBP As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colItemsPerPack As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colTotalIDelivered As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colQtyVariance As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colPriceVariance As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colBarcode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colOrderNum As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
