Imports MySql.Data
Imports MySql.Data.MySqlClient

Public Class ClassProducts
    Public Sub insertItems(ByVal Barcode As String, ByVal Description As String, _
                          ByVal Itemsperpack As Integer, ByVal categoryno As Integer, _
                          ByVal reorderlevel As Integer, ByVal retail As Double, _
                          ByVal wholesale As Double, ByVal modelNo As String, _
                          ByVal hasSerial As Byte, ByVal VAT_Rated As Integer, _
                          ByVal Selector As Integer)

        Dim strsql As String
        Dim dtaName As New MySqlClient.MySqlDataAdapter
        Try
            dtaName.InsertCommand = New MySqlClient.MySqlCommand
            With dtaName.InsertCommand
                .CommandTimeout = 60
                .Connection = Conn()
                .CommandType = CommandType.StoredProcedure
                strsql = "sp_inventory"
                .CommandText = strsql
                .Parameters.AddWithValue("@inselector", Selector)
                .Parameters.AddWithValue("@inbarcode", Barcode)
                .Parameters.AddWithValue("@indesc", Description)
                .Parameters.AddWithValue("@initemsperpack", Itemsperpack)
                .Parameters.AddWithValue("@incategoryno", categoryno)
                .Parameters.AddWithValue("@inreorderno", reorderlevel)
                .Parameters.AddWithValue("@inmodelno", modelNo)
                .Parameters.AddWithValue("@inhasserial", hasSerial)
                .Parameters.AddWithValue("@invat", VAT_Rated)
                .Parameters.AddWithValue("@inretail", retail)
                .Parameters.AddWithValue("@inwholesale", wholesale)
                .ExecuteNonQuery()
            End With
            dtaName.Dispose()
            closeconn()
        Catch ex As Exception
            If Conn.State = ConnectionState.Open Then Conn.Close()
            MsgBox(ex.Message & " Error occured while saving data.If error persists, Inform your administrator for further action", MsgBoxStyle.Critical)
            Exit Sub
        End Try
    End Sub


    'Loads database table to Datagridview
    Sub Loadproductstogrid(ByVal dgv As DataGridView)

        Dim strsql As String = "SELECT q1.Barcode AS Barcode,q1.Description,q1.Itemsperpack,q1.categoryname,q1.reorderlevel," _
                               & " q1.modelno, q2.retailprice, q2.wholesaleprice, q1.HAS_SERIAL, q1.VAT_Rated" _
                               & " FROM " _
                               & " (SELECT p.Barcode,p.Description,p.Itemsperpack," _
                               & " pc.categoryname, p.reorderlevel,p.modelno," _
                               & " CASE WHEN p.hasserialno=1 THEN 'YES' ELSE 'NO'" _
                               & " END AS 'HAS_SERIAL',CASE WHEN p.vatrated=1 THEN 'YES' ELSE 'NO'" _
                               & " END AS 'VAT_Rated' FROM products p" _
                               & " INNER JOIN product_category pc ON pc.id=p.categoryno WHERE p.deleted!=1) AS q1" _
                               & " Left Join" _
                               & " (SELECT pl.productid AS Barcode2,pl.retailprice,pl.wholesaleprice" _
                               & " FROM pricelist pl WHERE pl.active!=0) AS q2 ON q1.Barcode=q2.Barcode2" _
                               & " GROUP BY q1.Description,q1.categoryname ORDER BY q1.Barcode ASC"   ' q1.Description,q1.categoryname
        Try

            Dim dr As MySqlDataReader
            Dim cmd As New MySqlCommand(strsql, Conn)
            dr = cmd.ExecuteReader()
            'clear controls
            frmproducts.txtProductsSearc.Clear()
            dgv.Rows.Clear()
            'loop thru
            Do While dr.Read = True
                'DataGridView1.Rows.Add(dr(0), dr(1))
                Dim n As Integer = dgv.Rows.Add()
                dgv.Rows(n).Cells("Barcode").Value = dr(0).ToString
                dgv.Rows(n).Cells("Description").Value = dr(1).ToString
                dgv.Rows(n).Cells("ColItemsPerPack").Value = dr(2).ToString
                dgv.Rows(n).Cells("ColCategory").Value = dr(3).ToString
                dgv.Rows(n).Cells("ColRe_Order").Value = dr(4).ToString
                dgv.Rows(n).Cells("ColModel").Value = dr(5).ToString
                dgv.Rows(n).Cells("ColRetail").Value = dr(6).ToString
                dgv.Rows(n).Cells("ColWholesale").Value = dr(7).ToString
                dgv.Rows(n).Cells("ColHasSerial").Value = dr(8).ToString
                dgv.Rows(n).Cells("ColHasVAT").Value = dr(9).ToString
            Loop
            dr.Dispose()
            closeconn()
            Exit Sub
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    'Loads Autosearch products
    Sub LoadAutosearch(ByVal dgv As DataGridView)
        Dim strsql As String, ItemName As String
        dgv.Rows.Clear()
        ItemName = frmproducts.txtProductsSearc.Text.Trim

        Dim Barcode As String, Description As String, _
            Itemsperpack As String, CategoryName As String, _
            Reorder As String, ModelNo As String, _
            Retailprice As String, Wholesaleprice As String, _
            Serial As String, VAT As String, _
            DeleteStatus As Boolean = False

        strsql = "SELECT q1.Barcode AS Barcode,q1.Description,q1.Itemsperpack,q1.categoryname,q1.reorderlevel," _
                 & " q1.modelno, q2.retailprice, q2.wholesaleprice, q1.HAS_SERIAL, q1.VAT_Rated" _
                 & " FROM " _
                 & " (SELECT p.Barcode,p.Description,p.Itemsperpack," _
                 & " pc.categoryname, p.reorderlevel,p.modelno," _
                 & " CASE WHEN p.hasserialno=1 THEN 'YES' ELSE 'NO'" _
                 & " END AS 'HAS_SERIAL',CASE WHEN p.vatrated=1 THEN 'YES' ELSE 'NO'" _
                 & " END AS 'VAT_Rated' FROM products p" _
                 & " INNER JOIN product_category pc ON pc.id=p.categoryno WHERE p.deleted!=1) AS q1" _
                 & " Left Join" _
                 & " (SELECT pl.productid AS Barcode2,pl.retailprice,pl.wholesaleprice" _
                 & " FROM pricelist pl WHERE pl.active!=0) AS q2 ON q1.Barcode=q2.Barcode2" _
                 & " WHERE  q1.Description LIKE '" & "%" & ItemName & "%" & "' OR" _
                 & " q1.Barcode  LIKE '" & "%" & ItemName & "%" & "' OR " _
                 & " q1.categoryname  LIKE '" & "%" & ItemName & "%" & "' " _
                 & " ORDER BY q1.Description,q1.Barcode ASC"

        Try
            Dim datareader As MySqlDataReader
            Dim cmd As New MySqlCommand(strsql, Conn)
            cmd.CommandType = CommandType.Text
            datareader = cmd.ExecuteReader
            While datareader.Read
                Barcode = datareader(0).ToString
                Description = datareader(1).ToString
                Itemsperpack = datareader(2).ToString
                CategoryName = datareader(3).ToString
                Reorder = datareader(4).ToString
                ModelNo = datareader(5).ToString
                Retailprice = datareader(6).ToString
                Wholesaleprice = datareader(7).ToString
                Serial = datareader(8).ToString
                VAT = datareader(9).ToString

                'add rows to grid
                frmproducts.dgvItems.Rows.Add(DeleteStatus, Barcode, Description, Itemsperpack, CategoryName, _
                                  Reorder, ModelNo, Retailprice, Wholesaleprice, Serial, VAT)
            End While
            datareader.Dispose()
            closeconn()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    'Loads database table to Datagridview
    Public Sub selectitems(ByVal datereceived As Date, _
                           ByVal gview As DataGridView, _
                           ByVal choice As Integer, _
                           ByVal itemname As String)

        Dim strsql As String = ""

        itemname = "%" & itemname & "%"

        If choice = 0 Then
            strsql = " select barcode,description,itemsperpack,categoryno, reorderlevel from products WHERE deleted=0 "
        ElseIf (choice = 1) Then
            strsql = "select barcode,description,itemsperpack from products"
        ElseIf (choice = 2) Then

            strsql = "SELECT p.Barcode,p.Description,p.Itemsperpack," _
                           & " pc.categoryname, p.reorderlevel,p.modelno,pl.retailprice,pl.wholesaleprice," _
                           & " CASE WHEN p.hasserialno=1 THEN 'YES' ELSE 'NO'" _
                           & " END AS 'HAS SERIAL',CASE WHEN p.vatrated=1 THEN 'YES' ELSE 'NO'" _
                           & " END AS 'V.A.T Rated' FROM products p " _
                           & " INNER JOIN product_category pc ON pc.id=p.categoryno" _
                           & " INNER JOIN pricelist pl ON pl.productid=p.Barcode" _
                           & " WHERE p.deleted = 0 AND " _
                           & " (description LIKE '" & "%" & itemname & "%" & "' or barcode LIKE '" & "%" & itemname & "%" & "')"
        Else
            strsql = "SELECT q2.b2,q1.Description,q1.Itemsperpack," _
                     & " IFNULL(q2.buyingprice,0) AS BP" _
                     & " FROM" _
                     & " (SELECT p.Barcode AS b1,p.Description,p.Itemsperpack" _
                     & " FROM products p WHERE p.deleted!=1) AS q1" _
                     & " Left Join" _
                     & " (SELECT pl.productid AS b2,s.buyingprice" _
                     & " FROM pricelist pl" _
                     & " INNER JOIN stock s ON s.itemcode=pl.productid WHERE pl.active!=0) AS q2 " _
                     & " ON q2.b2=q1.b1 GROUP BY q2.b2 ORDER BY q1.Description ASC"

        End If
        Dim cmd As New MySqlCommand(strsql, Conn)
        cmd.CommandType = CommandType.Text
        Dim adapter As New MySqlDataAdapter(cmd)
        Dim table As New DataTable

        adapter.Fill(table)
        'Display the data.
        gview.DataSource = table
        closeconn()
    End Sub

    'Public Sub Load_Inventory(ByVal Barcode As String, _
    '                          ByVal Description As String, _
    '                          ByVal Itemsperpack As String, _
    '                          ByVal CategoryName As String, _
    '                          ByVal Reorder As String, _
    '                          ByVal ModelNo As String, _
    '                          ByVal Retailprice As String, _
    '                          ByVal Wholesaleprice As String, _
    '                          ByVal Serial As String, _
    '                          ByVal VAT As String, _
    '                          ByVal det As Integer, _
    '                          ByVal grid As DataGridView)

    '    Dim dr As MySqlDataReader = Nothing
    '    Dim cmd As New MySqlCommand()
    '    Try
    '        With cmd
    '            .CommandTimeout = 60
    '            .Connection = Conn()
    '            .CommandType = CommandType.StoredProcedure
    '            .CommandText = "sp_load_inventory"
    '            .Parameters.AddWithValue("@inbarcode", Barcode)
    '            .Parameters.AddWithValue("@indesc", Description)
    '            .Parameters.AddWithValue("@initempack", Itemsperpack)
    '            .Parameters.AddWithValue("@incategory", CategoryName)
    '            .Parameters.AddWithValue("@inreorderno", Reorder)
    '            .Parameters.AddWithValue("@inmodelno", ModelNo)
    '            .Parameters.AddWithValue("@inretail", Retailprice)
    '            .Parameters.AddWithValue("@inwholesale", Wholesaleprice)
    '            .Parameters.AddWithValue("@inserialno", Serial)
    '            .Parameters.AddWithValue("@invat", VAT)
    '            .Parameters.AddWithValue("@inselector", det)
    '            dr = .ExecuteReader()
    '        End With
    '        grid.DataSource = Nothing
    '        grid.Rows.Clear()
    '        If dr.HasRows Then
    '            While dr.Read
    '                grid.Rows.Add(Barcode, Description, Itemsperpack, CategoryName, Reorder, ModelNo, Retailprice, Wholesaleprice, Serial, VAT)

    '            End While
    '        End If
    '        dr.Close()
    '        cmd.Dispose()
    '        closeconn()
    '    Catch ex As Exception
    '        closeconn()
    '        MsgBox(ex.Message & "Error occured and bill was not retrieved. If error persists, inform your administrator for further action", MsgBoxStyle.Critical)
    '    End Try
    'End Sub

    'compute inventory items
    Public Function Compute_InventoryItems() As Integer
        Dim datareader As MySqlDataReader
        Dim sql As String = "SELECT COUNT(Barcode) FROM products WHERE deleted=0"
        Dim Items_No As Integer = 0
        Try
            Dim cmd As New MySqlCommand(sql, Conn)
            cmd.CommandType = CommandType.Text
            datareader = cmd.ExecuteReader
            If datareader.HasRows Then
                While datareader.Read
                    If (IsDBNull(datareader(0))) Then
                        Items_No = 0
                    Else
                        Items_No = Integer.Parse(datareader(0).ToString)
                    End If
                End While
            Else
                Items_No = 0
            End If
            closeconn()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return Items_No
    End Function

    Public Sub insertSerials(ByVal Barcode As String, ByVal SerialNo As String, ByVal updating As Boolean)

        Dim strsql As String

        If updating = False Then
            strsql = "insert into serialnumbers(itembarcode,serialnumber)" _
                               & " values('" & Barcode & "','" & SerialNo & "')"
        Else
            strsql = " UPDATE serialnumbers SET serialnumber='" & SerialNo & "' WHERE itembarcode='" & Barcode & "' "
        End If

        Dim dtaName As New MySqlClient.MySqlDataAdapter
        Try
            dtaName.InsertCommand = New MySqlClient.MySqlCommand
            With dtaName.InsertCommand
                .CommandTimeout = 60
                .Connection = Conn()
                .CommandType = CommandType.Text
                .CommandText = strsql
                .ExecuteNonQuery()
                MessageBox.Show("Success", "Smartec POS", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End With
            dtaName.Dispose()
            closeconn()
        Catch ex As Exception
            If Conn.State = ConnectionState.Open Then Conn.Close()
            MsgBox(ex.Message & " Error occured while saving data.If error persists, Inform your administrator for further action", MsgBoxStyle.Critical)
            Exit Sub
        End Try
    End Sub

    'CHECK DUPLICATE SERIAL NUMBERS
    Public Function checkDuplicate(ByVal Barcode As String, ByVal SerialNo As String) As Integer
        Dim strsql As String, serialCount As Integer = 0
        strsql = "SELECT count(serialnumber) FROM serialnumbers WHERE itembarcode='" & Barcode & "' AND serialnumber ='" & SerialNo & "'"
        Dim datareader As MySqlDataReader
        Try
            Dim cmd As New MySqlCommand(strsql, Conn)
            cmd.CommandType = CommandType.Text
            datareader = cmd.ExecuteReader
            While datareader.Read
                serialCount = Integer.Parse(datareader(0).ToString)
            End While
            datareader.Dispose()
            closeconn()
        Catch ex As Exception
            closeconn()
            MsgBox(ex.Message & " Error occured while saving data.If error persists, Inform your administrator for further action", MsgBoxStyle.Critical)
        End Try
        Return serialCount
    End Function
End Class
