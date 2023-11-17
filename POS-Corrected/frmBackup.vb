Imports MySql.Data
Imports MySql.Data.MySqlClient

Imports System.Text
Public Class frmBackup
    Dim OutputStream As System.IO.StreamWriter
    Sub OnDataReceived1(ByVal Sender As Object, ByVal e As System.Diagnostics.DataReceivedEventArgs)
        If e.Data IsNot Nothing Then
            Dim text As String = e.Data
            Dim bytes As Byte() = Encoding.Default.GetBytes(text)
            text = Encoding.UTF8.GetString(bytes)
            OutputStream.WriteLine(text)
        End If
    End Sub


    Sub CreateBackup()
        '###############
        Dim DB_Directory As String = lblLocalDisk.Text.ToString
        Dim DB_Name As String = lblDBName.Text.ToString
        '###############
        Dim mysqldumpPath As String = DB_Directory & ":\mysqldump.exe"  '(copy mysqldump from C:\Program Files\MySQL\MySQL Server 5.7\bin and place it in E:\)"
        Dim host As String = "localhost"
        Dim user As String = "root"
        Dim pswd As String = "madeinmanhattan"
        Dim dbnm As String = DB_Name
        Dim cmd As String = String.Format("-h{0} -u{1} -p{2} {3}", host, user, pswd, dbnm)
        Dim timestring As String = Today().Day & Month(Today) & Year(Today) & Now().Hour & Now().Minute
        Dim filename As String = "SalesMS" & timestring & ".sql"
        Dim filePath As String = DB_Directory & ":\Backup\ " & filename
        'MsgBox(filePath)
        'create a folder called backup in d:\
        OutputStream = New System.IO.StreamWriter(filePath, True, System.Text.Encoding.UTF8)
        Dim startInfo As System.Diagnostics.ProcessStartInfo = New System.Diagnostics.ProcessStartInfo()
        startInfo.FileName = mysqldumpPath
        startInfo.Arguments = cmd
        startInfo.RedirectStandardError = True
        startInfo.RedirectStandardInput = False
        startInfo.RedirectStandardOutput = True
        startInfo.UseShellExecute = False
        startInfo.CreateNoWindow = True
        startInfo.ErrorDialog = False
        Dim proc As System.Diagnostics.Process = New System.Diagnostics.Process()
        proc.StartInfo = startInfo
        AddHandler proc.OutputDataReceived, AddressOf OnDataReceived1
        proc.Start()
        proc.BeginOutputReadLine()
        proc.WaitForExit()
        OutputStream.Flush()
        OutputStream.Close()
        MsgBox("Backup complete")
    End Sub

    Private Sub btnBackup_Click(sender As Object, e As EventArgs) Handles btnBackup.Click
        CreateBackup()
    End Sub

    Private Sub frmBackup_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lblLocalDisk.Text = get_Backup_Directory()
        lblDBName.Text = get_Backup_Name()
    End Sub

    Private Function get_Backup_Directory() As String

        Dim datareader As MySqlDataReader
        Dim LocalDisk As String = ""

        Dim strsql As String = "SELECT bd.localdisk FROM backupdirectory bd WHERE bd.status=1"

        Try
            Dim cmd As New MySqlCommand(strsql, Conn)
            cmd.CommandType = CommandType.Text
            datareader = cmd.ExecuteReader
            While datareader.Read
                If (datareader(0).ToString) = String.Empty Then
                    LocalDisk = ""
                Else
                    LocalDisk = datareader(0).ToString
                End If
            End While
            datareader.Dispose()
            closeconn()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Return LocalDisk
    End Function

    Private Function get_Backup_Name() As String

        Dim datareader As MySqlDataReader
        Dim LocalDisk As String = "", DBName As String = ""

        Dim strsql As String = "SELECT bd.databasename FROM backupdirectory bd WHERE bd.status=1"

        Try
            Dim cmd As New MySqlCommand(strsql, Conn)
            cmd.CommandType = CommandType.Text
            datareader = cmd.ExecuteReader
            While datareader.Read
                If (datareader(0).ToString) = String.Empty Then
                    DBName = ""
                Else
                    DBName = datareader(0).ToString
                End If
            End While
            datareader.Dispose()
            closeconn()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Return DBName
    End Function
End Class