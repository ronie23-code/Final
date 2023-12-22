Imports MongoDB.Bson
Imports MongoDB.Driver
Public Class UserControl10

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Or TextBox4.Text = "" Then
            MsgBox("Please fillup all the boxes!")

        Else

            MsgBox("account successfully log in!")


            Dim Adminname As String = TextBox1.Text
            Dim Adminaddress As String = TextBox2.Text
            Dim Adminage As String = TextBox3.Text
            Dim Adminrfid As String = TextBox4.Text
            Dim connectionString As String = "mongodb+srv://hardware1234:hardware1234@hardware.dknymeb.mongodb.net/HIMS"

            ' Step 2: Connect to the MongoDB Atlas cluster
            Dim client As MongoClient = New MongoClient(connectionString)
            Dim database As IMongoDatabase = client.GetDatabase("HIMS")
            Dim document As BsonDocument = New BsonDocument()
            document.Add("Adminname", Adminname)
            document.Add("Adminaddress", Adminaddress)
            document.Add("Adminage", Adminage)
            document.Add("Adminrfid", Adminrfid)


            ' Step 3: Perform database operation
            Dim collection As IMongoCollection(Of BsonDocument) = database.GetCollection(Of BsonDocument)("admins")
            collection.InsertOne(document)
            ' Step 4: Display inserted document
            Dim insertedDocument As BsonDocument = collection.Find(New BsonDocument()).FirstOrDefault()
            MessageBox.Show("New Admin Added!")
            TextBox1.Clear()
            TextBox2.Clear()
            TextBox3.Clear()
            TextBox4.Clear()
        End If

    End Sub

    Private Sub UserControl10_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click

    End Sub
End Class
