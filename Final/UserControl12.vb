Imports MongoDB.Bson
Imports MongoDB.Driver


Public Class UserControl12
    Private mongoClient As MongoClient
    Private mongoDatabase As IMongoDatabase
    Private mongoCollection As IMongoCollection(Of BsonDocument)

    Dim connectionString As String = "mongodb+srv://hardware1234:hardware1234@hardware.dknymeb.mongodb.net/HIMS"
    Dim client As MongoClient = New MongoClient(connectionString)
    Dim database As IMongoDatabase
    Dim sourceCollection As IMongoCollection(Of BsonDocument)
    Dim destinationCollection As IMongoCollection(Of BsonDocument)


    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Set up the source and destination collections
        database = client.GetDatabase("HIMS")
        sourceCollection = database.GetCollection(Of BsonDocument)("employees")
        destinationCollection = database.GetCollection(Of BsonDocument)("archive")

        ' Load data into the DataGridView (you may implement this based on your application)
        LoadDataIntoDataGridView()



        ' Connect to MongoDB
        mongoClient = New MongoClient("mongodb+srv://hardware1234:hardware1234@hardware.dknymeb.mongodb.net/HIMS") ' Connection string to your MongoDB server
        mongoDatabase = mongoClient.GetDatabase("HIMS") ' Replace with your database name
        mongoCollection = mongoDatabase.GetCollection(Of BsonDocument)("employees") ' Replace with your collection name

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        For Each selectedRow As DataGridViewRow In DataGridView1.SelectedRows
            Dim documentId As String = selectedRow.Cells("Username").Value.ToString()
            Dim inputValue As String = TextBox1.Text ' Get the value from the textbox

            ' Define the filter to find the document
            Dim filter = Builders(Of BsonDocument).Filter.Eq(Of String)("Username", inputValue) ' Replace "YourField" with the actual field name

            ' Retrieve the document from the source collection
            Dim archivedDocument As BsonDocument = sourceCollection.Find(filter).FirstOrDefault()

            If archivedDocument IsNot Nothing Then
                ' Insert the archived document into the destination collection
                destinationCollection.InsertOne(archivedDocument)

                ' Remove the row from the DataGridView
                DataGridView1.Rows.Remove(selectedRow)
                sourceCollection.DeleteOne(filter)
                MessageBox.Show("Employee Archived!")
                TextBox1.Clear()
                TextBox2.Clear()
                TextBox3.Clear()
                TextBox4.Clear()
            Else
                MessageBox.Show("Document not found in source collection.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

        Next



    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        Call Form3.UserControl131.Show()
        Call Form3.UserControl131.BringToFront()

        Form3.UserControl131.BringToFront()

    End Sub
    Private Sub UserControl12(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim connectionString As String = "mongodb+srv://hardware1234:hardware1234@hardware.dknymeb.mongodb.net/HIMS"
        Dim client As MongoClient = New MongoClient(connectionString)
        Dim database As IMongoDatabase
        Dim collection As IMongoCollection(Of BsonDocument)
        ' Set up the database and collection
        database = client.GetDatabase("HIMS")
        collection = database.GetCollection(Of BsonDocument)("employees")

        ' Load data into the DataGridView (you may implement this based on your application)
        LoadDataIntoDataGridView()
    End Sub



    Private Sub LoadDataIntoDataGridView()
        Try
            DataGridView1.Rows.Clear()

            ' Replace "YourConnectionString" with your actual MongoDB connection string.
            Dim connectionString As String = "mongodb+srv://hardware1234:hardware1234@hardware.dknymeb.mongodb.net/HIMS"
            Dim client As MongoClient = New MongoClient(connectionString)
            Dim database As IMongoDatabase = client.GetDatabase("HIMS")
            Dim collection As IMongoCollection(Of BsonDocument) = database.GetCollection(Of BsonDocument)("employees")

            Dim filter = Builders(Of BsonDocument).Filter.Empty
            Dim documents = collection.Find(filter).ToList()

            'Update DataGridView's data source

            For Each doc As BsonDocument In documents
                Dim Surname = doc.GetValue("Surname", String.Empty)
                Dim Firstname = doc.GetValue("Firstname", String.Empty)
                Dim Username = doc.GetValue("Username", String.Empty)
                Dim Password = doc.GetValue("Password", String.Empty)


                DataGridView1.Rows.Add(Surname, Firstname, Username, Password)

            Next
        Catch ex As Exception
            ' Handle exceptions, e.g., connection errors or data format issues.
            MessageBox.Show("An error occurred: " & ex.Message)
        End Try




    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If DataGridView1.SelectedRows.Count > 0 Then
            Dim selectedRow = DataGridView1.SelectedRows(0)


            Dim newSurname = TextBox1.Text
            Dim newFirstname = TextBox2.Text
            Dim newUsername = TextBox3.Text
            Dim newPassword = TextBox4.Text


            ' Define the filter to find the document by AccountNumber
            Dim filter As FilterDefinition(Of BsonDocument) = Builders(Of BsonDocument).Filter.Eq(Of String)("Username", newUsername)

            ' Define the update operation to set the values of fields
            Dim update As UpdateDefinition(Of BsonDocument) = Builders(Of BsonDocument).Update.Set(Function(doc) doc("Surname"), newSurname) _
                .Set(Function(doc) doc("Firstname"), newFirstname) _
                .Set(Function(doc) doc("Password"), newPassword) _


            ' Connect to MongoDB
            Dim connectionString As String = "mongodb+srv://hardware1234:hardware1234@hardware.dknymeb.mongodb.net/HIMS"
            Dim dbName As String = "HIMS"
            Dim client As New MongoClient(connectionString)

            ' Access the MongoDB database
            Dim database As IMongoDatabase = client.GetDatabase(dbName)

            ' Access the collection you want to update documents in
            Dim collectionName As String = "employees"
            Dim collection As IMongoCollection(Of BsonDocument) = database.GetCollection(Of BsonDocument)(collectionName)

            ' Perform the update operation
            collection.UpdateOne(filter, update)
            ' Reload data   
            LoadDataIntoDataGridView()
            MessageBox.Show("Successfully Updated!")

            ' Clear input fields
            TextBox1.Clear()
            TextBox2.Clear()
            TextBox3.Clear()
            TextBox4.Clear()



        End If
    End Sub







    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub



    Private Sub UserControl12_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try
            DataGridView1.Rows.Clear()

            ' Replace "YourConnectionString" with your actual MongoDB connection string.
            Dim connectionString As String = "mongodb+srv://hardware1234:hardware1234@hardware.dknymeb.mongodb.net/HIMS"
            Dim client As MongoClient = New MongoClient(connectionString)
            Dim database As IMongoDatabase = client.GetDatabase("HIMS")
            Dim collection As IMongoCollection(Of BsonDocument) = database.GetCollection(Of BsonDocument)("employees")

            Dim filter = Builders(Of BsonDocument).Filter.Empty
            Dim documents = collection.Find(filter).ToList()

            'Update DataGridView's data source

            For Each doc As BsonDocument In documents
                Dim Surname = doc.GetValue("Surname", String.Empty)
                Dim Firstname = doc.GetValue("Firstname", String.Empty)
                Dim Username = doc.GetValue("Username", String.Empty)
                Dim Password = doc.GetValue("Password", String.Empty)



                DataGridView1.Rows.Add(Surname, Firstname, Username, Password)

            Next
        Catch ex As Exception
            ' Handle exceptions, e.g., connection errors or data format issues.
            MessageBox.Show("An error occurred: " & ex.Message)
        End Try


        ' Connect to MongoDB
        mongoClient = New MongoClient("mongodb+srv://hardware1234:hardware1234@hardware.dknymeb.mongodb.net/HIMS") ' Connection string to your MongoDB server
        mongoDatabase = mongoClient.GetDatabase("HIMS") ' Replace with your database name
        mongoCollection = mongoDatabase.GetCollection(Of BsonDocument)("employees") ' Replace with your collection name




    End Sub
    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged

        If DataGridView1.SelectedRows.Count > 0 Then
            Dim selectedRow = DataGridView1.SelectedRows(0)
            TextBox1.Text = selectedRow.Cells("Surname").Value.ToString()
            TextBox2.Text = selectedRow.Cells("Firstname").Value.ToString()
            TextBox3.Text = selectedRow.Cells("Username").Value.ToString()
            TextBox4.Text = selectedRow.Cells("Password").Value.ToString()

        End If
    End Sub



    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        LoadDataIntoDataGridView()
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Call Form3.UserControl22.BringToFront()
        Form3.UserControl22.BringToFront()
    End Sub
End Class
