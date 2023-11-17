Imports MySql.Data
Imports MySql.Data.MySqlClient

Public Class frmUserRights

    Private Sub frmUserRights_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'form load event
        loadcombo(cboUserrights, "SELECT staffid,fullnames FROM staff WHERE deleted=0", "fullnames", "staffid")
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        'ASSIGN USER PRIVILEDGES CODE
        If Not Authorized(userid, TaskID.give_user_rights) Then Exit Sub
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&

        'SAVE TO DATABASE
        If cboUserrights.SelectedIndex = -1 Then
            MessageBox.Show("Please select a user, before you continue with save", "Required...", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        SavePrivs()
        If MessageBox.Show("User Priveleges successfully saved" & vbCrLf & "Do you wish to add privileges for another user?", "Save...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            ListViewUserRights.Items.Clear()
            cboUserrights.SelectedIndex = -1
        Else
            Me.Close()
        End If
    End Sub

    ' +++++++++++++++++++++
    Sub SavePrivs()
        Dim User As String = ""
        User = cboUserrights.SelectedValue.ToString
        Dim dt As DataTable = ReturnDataset("select taskid from userright where userid='" & User & "'", "userright").Tables(0)
        For s As Integer = 0 To ListViewUserRights.Items.Count - 1
            If ListViewUserRights.Items(s).Checked Then
                '//if item is in the current list of privileges then
                Dim foundRows() As DataRow
                foundRows = dt.Select("Taskid=" & ListViewUserRights.Items(s).Text)
                If foundRows.Length < 1 Then
                    ExecuteNonQuery("Insert into userright(userid,Taskid) values ('" & User & "'," & ListViewUserRights.Items(s).Text & ")")
                End If
                foundRows = Nothing
            Else
                '//if the user had the privileges but now they are  no longer there
                Dim foundRows() As DataRow
                foundRows = dt.Select("Taskid=" & ListViewUserRights.Items(s).Text)
                If foundRows.Length >= 1 Then
                    ExecuteNonQuery("Delete from  userright where userid='" & User & "' and Taskid=" & ListViewUserRights.Items(s).Text & "")
                End If
                foundRows = Nothing
            End If
        Next
    End Sub
    '+++++++++++++++++++++++++++++++++++

    Public Function ReturnDataset(ByVal QryString As String, ByVal tName As String) As DataSet
        Dim ds As New DataSet
        Try

            Dim da As New MySqlDataAdapter(QryString, Conn())
            da.Fill(ds, tName)
            da.Dispose()
            closeconn()
            Return ds
        Catch ex As System.Exception

            closeconn()
            System.Windows.Forms.MessageBox.Show(ex.Message)
            Return Nothing
        Finally
            closeconn()
        End Try
    End Function

    Function ExecuteNonQuery(ByVal SqlString As String) As Boolean
        Dim success As Integer = 0
        Try
            Dim cmd As New MySqlCommand(SqlString, Conn())
            cmd.CommandType = CommandType.Text
            cmd.ExecuteNonQuery()
            success = 1
            cmd.Dispose()
            closeconn()
            Return True
        Catch ex As Exception
            closeconn()
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Error")
            success = 0
            Return False
        End Try
    End Function

    Private Sub cboUserrights_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboUserrights.SelectedIndexChanged
        If cboUserrights.SelectedIndex <> -1 Then
            loadTasks(ListViewUserRights)
            LoadPrivs()
        End If
        ListViewUserRights.Focus()
        LoadPrivs()
    End Sub

    Sub loadTasks(ByVal lvPrivs As ListView)
        Dim RowId As Integer = 0
        lvPrivs.Items.Clear()
        Dim drd As MySqlDataReader
        Dim strsql As String = " SELECT taskid,taskname FROM tasks "
        Dim cmd As New MySqlCommand(strsql, Conn())
        Try
            With cmd
                .CommandTimeout = 60
                .Connection = Conn()
                .CommandType = CommandType.Text
                .CommandType = CommandType.Text
                drd = .ExecuteReader()
            End With
            While drd.Read
                Dim LI As New ListViewItem
                If IsDBNull(drd(0)) Then
                    Exit Sub
                Else
                    LI.Text = drd(0).ToString
                    LI.SubItems.Add(drd(1).ToString)
                    lvPrivs.Items.Add(LI)
                    RowId += 1
                    If RowId Mod 2 = 0 Then
                        LI.BackColor = Color.White
                    Else
                        LI.BackColor = Color.Honeydew
                    End If
                    LI = Nothing
                End If
            End While
            cmd.Dispose()
            closeconn()
        Catch ex As Exception
            closeconn()
        End Try
    End Sub

    Sub LoadPrivs()
        Dim userno As String
        If cboUserrights.SelectedIndex = -1 Then
            Exit Sub
        Else
            userno = cboUserrights.SelectedValue.ToString
        End If
        Dim dt As DataTable = ReturnDataset("select taskid from userright where userid='" & userno & "'", "Userright").Tables(0)
        For s As Integer = 0 To ListViewUserRights.Items.Count - 1
            Dim foundRows() As DataRow
            foundRows = dt.Select("Taskid=" & ListViewUserRights.Items(s).Text)
            If foundRows.Count >= 1 Then
                ListViewUserRights.Items(s).Checked = True
            End If
        Next
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class