Imports MongoDB.Bson
Imports MongoDB.Driver

Public Class UserControl1
    Private Sub LinkLabel2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        Call Form1.UserControl32.BringToFront()
        TextBox1.Focus()

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox1.Text = "" Or TextBox2.Text = "" Then

            TextBox1.Focus()
            TextBox2.Focus()
            Label5.Visible = True
            Return
        Else
            Dim username As String = TextBox1.Text
            Dim password As String = TextBox2.Text

            ' Connect to MongoDB Atlas
            Dim connectionString As String = "mongodb+srv://hardware1234:hardware1234@hardware.dknymeb.mongodb.net/HIMS"
            Dim client As New MongoClient(connectionString)
            Dim database As IMongoDatabase = client.GetDatabase("HIMS")
            Dim collection As IMongoCollection(Of BsonDocument) = database.GetCollection(Of BsonDocument)("employees")

            ' Create a filter for username and password
            Dim filter As FilterDefinition(Of BsonDocument) = Builders(Of BsonDocument).Filter.And(
             Builders(Of BsonDocument).Filter.Regex("Username", New BsonRegularExpression(username, "i")), ' "i" for case-insensitive matching
             Builders(Of BsonDocument).Filter.Regex("Password", New BsonRegularExpression(password, "i")))


            ' Execute the query
            Dim result As BsonDocument = collection.Find(filter).FirstOrDefault()

            If result IsNot Nothing Then
                ' Login successful   
                MessageBox.Show("Login successful!")

                Form2.Show()
                Form1.Hide()

                TextBox1.Text = ""
                TextBox2.Text = ""
            Else
                ' Check if the user is signed up
                Dim signUpFilter = Builders(Of BsonDocument).Filter.Eq(Of String)("Username", TextBox1.Text)

                If collection.Find(signUpFilter).Any() Then
                    ' User is signed up but login failed
                    MessageBox.Show("Incorrect username or password")
                Else
                    ' User is not signed up
                    MessageBox.Show("Please sign up")
                End If

                TextBox1.Clear()
                TextBox2.Clear()

                Return
            End If

        End If

    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) 
        Call Form1.UserControl32.BringToFront()
    End Sub

    Private Sub UserControl1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
    Public Sub test1()
        TextBox1.Select()
    End Sub


End Class
