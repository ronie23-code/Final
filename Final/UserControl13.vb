Imports MongoDB.Bson
Imports MongoDB.Driver
Public Class UserControl13
    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub

    Private Sub UserControl2_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Or TextBox4.Text = "" Then
                MsgBox("Please fillup all the boxes!")

                'If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Or TextBox4.Text = "" Or TextBox5.Text = "" Then
                'MsgBox("Please fillup all the boxes!")
            Else
                MsgBox("account successfully log in!")
                Dim Surname As String = TextBox1.Text
                Dim Firstname As String = TextBox2.Text
                Dim Username As String = TextBox3.Text
                Dim Password As String = TextBox4.Text
            Dim connectionString As String = "mongodb+srv://hardware1234:hardware1234@hardware.dknymeb.mongodb.net/HIMS"

            ' Step 2: Connect to the MongoDB Atlas cluster
            Dim client As MongoClient = New MongoClient(connectionString)
                Dim database As IMongoDatabase = client.GetDatabase("HIMS")

                'Step 3: Perform database operation
                Dim collection As IMongoCollection(Of BsonDocument) = database.GetCollection(Of BsonDocument)("employees")
                Dim document As BsonDocument = New BsonDocument()
                document.Add("Surname", Surname)
                document.Add("Firstname", Firstname)
                document.Add("Username", Username)
                document.Add("Password", Password)
                collection.InsertOne(document)

                ' Step 4: Display inserted document
                Dim insertedDocument As BsonDocument = collection.Find(New BsonDocument()).FirstOrDefault()
                MessageBox.Show("Registration Staff Successful!")



            End If
            TextBox1.Clear()
            TextBox2.Clear()
            TextBox3.Clear()
            TextBox4.Clear()

    End Sub


    Private Sub PictureBox1_Click_1(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Call Form3.UserControl122.BringToFront()

    End Sub
End Class
