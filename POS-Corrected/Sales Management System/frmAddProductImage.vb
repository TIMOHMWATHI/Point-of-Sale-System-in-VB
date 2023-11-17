Imports MySql.Data
Imports MySql.Data.MySqlClient
Public Class frmAddProductImage

    Private Sub btnBrowse_Click(sender As Object, e As EventArgs) Handles btnBrowse.Click

        Try
            With OpenFileDialog1

                'CHECK THE SELECTED FILE IF IT EXIST OTHERWISE THE DIALOG BOX WILL DISPLAY A WARNING.
                .CheckFileExists = True

                'CHECK THE SELECTED PATH IF IT EXIST OTHERWISE THE DIALOG BOX WILL DISPLAY A WARNING.
                .CheckPathExists = True

                'GET AND SET THE DEFAULT EXTENSION
                .DefaultExt = "jpg"

                'RETURN THE FILE LINKED TO THE LNK FILE
                .DereferenceLinks = True

                'SET THE FILE NAME TO EMPTY 
                .FileName = ""

                'FILTERING THE FILES
                .Filter = "(*.jpg)|*.jpg|(*.png)|*.png|(*.jpg)|*.jpg|All files|*.*"
                'SET THIS FOR ONE FILE SELECTION ONLY.
                .Multiselect = False



                'SET THIS TO PUT THE CURRENT FOLDER BACK TO WHERE IT HAS STARTED.
                .RestoreDirectory = True

                'SET THE TITLE OF THE DIALOG BOX.
                .Title = "Select a file to open"

                'ACCEPT ONLY THE VALID WIN32 FILE NAMES.
                .ValidateNames = True

                If .ShowDialog = DialogResult.OK Then
                    Try
                        PictureBox1.Image = Image.FromFile(OpenFileDialog1.FileName)
                    Catch fileException As Exception
                        Throw fileException
                    End Try
                End If

            End With
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim fileName = "images/noimage.jpg"
        Dim caption As String
        caption = System.IO.Path.GetFileName(OpenFileDialog1.FileName)
        caption = "images/" & caption

        Dim arrImage() As Byte
        Dim mstream As New System.IO.MemoryStream()
        'Dim CropRect As New Rectangle(102, 0, 102, 102)
        'Dim OriginalImage = Image.FromFile(fileName)
        'Dim CropImage = New Bitmap(CropRect.Width, CropRect.Height)
        'Using grp = Graphics.FromImage(CropImage)
        '    grp.DrawImage(OriginalImage, New Rectangle(0, 0, CropRect.Width, CropRect.Height), CropRect, GraphicsUnit.Pixel)
        '    OriginalImage.Dispose()
        '    CropImage.Save(caption)
        'End Using


        'SPECIFIES THE FILE FORMAT OF THE IMAGE
        PictureBox1.Image.Save(mstream, System.Drawing.Imaging.ImageFormat.Jpeg)

        'RETURNS THE ARRAY OF UNSIGNED BYTES FROM WHICH THIS STREAM WAS CREATED
        arrImage = mstream.GetBuffer()

        'GET THE SIZE OF THE STREAM IN BYTES
        Dim FileSize As UInt32
        FileSize = mstream.Length
        'CLOSES THE CURRENT STREAM AND RELEASE ANY RESOURCES ASSOCIATED WITH THE CURRENT STREAM
        mstream.Close()
        Try
            Dim barcode As String = frmproducts.dgvItems.CurrentRow.Cells(0).Value.ToString
            Dim strsql As String = "UPDATE products SET productimage ='" & caption & "',imagesaved= 1 WHERE Barcode='" & barcode & "' AND deleted=0 "

            Dim dtaName As New MySqlClient.MySqlDataAdapter
            dtaName.UpdateCommand = New MySqlClient.MySqlCommand
            With dtaName.UpdateCommand
                .CommandTimeout = 60
                .Connection = Conn()
                .CommandType = CommandType.Text
                .CommandText = strsql
                .ExecuteNonQuery()
            End With

            MsgBox("Picture has been save in the database", MsgBoxStyle.Information, "Strawberry Assets Manager")
            Me.Close()
            ''frmproducts.Loadproductstogrid()
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            con.Close()
        End Try

    End Sub
End Class