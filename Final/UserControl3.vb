Imports MongoDB.Bson
Imports MongoDB.Driver

Public Class UserControl3


    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged


        Try

            Dim connectionString As String = "mongodb+srv://hardware1234:hardware1234@hardware.dknymeb.mongodb.net/HIMS"
            Dim phoneNumber As String = TextBox1.Text.Trim()

            If phoneNumber.Length = 10 Then
                Dim client As New MongoClient(connectionString)
                Dim database As IMongoDatabase = client.GetDatabase("HIMS")
                Dim collection As IMongoCollection(Of BsonDocument) = database.GetCollection(Of BsonDocument)("admins")

                Dim filter = Builders(Of BsonDocument).Filter.Eq(Of String)("Adminrfid", phoneNumber)
                Dim count As Long = collection.CountDocuments(filter)

                If count > 0 Then
                    'Dim newForm As New Form3()
                    'newForm.Show()
                    Form3.Show()
                    TextBox1.Clear()
                    Form1.Hide()
                Else
                    MessageBox.Show("Invalid RFID", "Error")
                    TextBox1.Clear()

                End If
                MessageBox.Show("Invalid RFID", "Error")
                TextBox1.Clear()
            End If
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try

    End Sub


    Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles PictureBox3.Click
        Call Form1.UserControl12.BringToFront()
        PictureBox4.Show()
        PictureBox2.Hide()
    End Sub

    Private Sub UserControl3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        PictureBox2.Hide()


    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click

    End Sub


    Private Sub PictureBox4_Click(sender As Object, e As EventArgs) Handles PictureBox4.Click
        PictureBox2.Show()
        TextBox1.Focus()
        PictureBox4.Hide()
    End Sub
End Class

