Imports MongoDB.Driver
Imports MongoDB.Bson
Public Class UserControl5
    Private mongoClient As MongoClient
    Private mongoDatabase As IMongoDatabase
    Private mongoCollection As IMongoCollection(Of BsonDocument)

    Private Sub UserControl5_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try
            DataGridView1.Rows.Clear()

            ' Replace "YourConnectionString" with your actual MongoDB connection string.
            Dim connectionString As String = "mongodb+srv://hardware1234:hardware1234@hardware.dknymeb.mongodb.net/HIMS"
            Dim client As MongoClient = New MongoClient(connectionString)
            Dim database As IMongoDatabase = client.GetDatabase("HIMS")
            Dim collection As IMongoCollection(Of BsonDocument) = database.GetCollection(Of BsonDocument)("item")

            Dim filter = Builders(Of BsonDocument).Filter.Empty
            Dim documents = collection.Find(filter).ToList()

            'Update DataGridView's data source

            For Each doc As BsonDocument In documents
                Dim Item_Name = doc.GetValue("ItemName", String.Empty)
                Dim Item_Quantity = doc.GetValue("ItemQuantity", String.Empty)
                Dim Item_Price = doc.GetValue("ItemPrice", String.Empty)


                DataGridView1.Rows.Add(Item_Name, Item_Quantity, Item_Price)

            Next
        Catch ex As Exception
            ' Handle exceptions, e.g., connection errors or data format issues.
            MessageBox.Show("An error occurred: " & ex.Message)
        End Try


        ' Connect to MongoDB
        mongoClient = New MongoClient("mongodb+srv://hardware1234:hardware1234@hardware.dknymeb.mongodb.net/HIMS") ' Connection string to your MongoDB server
        mongoDatabase = mongoClient.GetDatabase("HIMS") ' Replace with your database name
        mongoCollection = mongoDatabase.GetCollection(Of BsonDocument)("item") ' Replace with your collection name


    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        Dim userInput As String = TextBox2.Text
        Dim isValidNumber As Boolean = Integer.TryParse(userInput, Nothing)
        Dim userInputs As String = TextBox3.Text
        Dim isValidNumbers As Boolean = Integer.TryParse(userInput, Nothing)


        Dim userIn As String = TextBox1.Text

        ' Create a MongoClient to establish a connection to the MongoDB server
        Dim client As New MongoClient("mongodb+srv://hardware1234:hardware1234@hardware.dknymeb.mongodb.net/HIMS")


        ' Get a reference to the database
        Dim database As IMongoDatabase = client.GetDatabase("HIMS")

        ' Get a reference to the collection
        Dim collection As IMongoCollection(Of BsonDocument) = database.GetCollection(Of BsonDocument)("item")

        ' Define a filter to check if the document with the given value already exists
        Dim filter As FilterDefinition(Of BsonDocument) = Builders(Of BsonDocument).Filter.Eq(Of String)("ItemName", userIn)



        If isValidNumber Then
            ' Process the valid number (you can add your logic here)


        Else
            If TextBox1.Text = "" Then

                MessageBox.Show("Please Enter Item Name")

            ElseIf TextBox2.Text = "" Then
                MessageBox.Show("Please Enter Item Quantity")

            ElseIf TextBox3.Text = "" Then

                MessageBox.Show("Please Enter Item Price")


            End If
            ' Display an error message for invalid input
            MessageBox.Show("Invalid input. Please enter a valid number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return


        End If












        If TextBox1.Text = "" Then

            MessageBox.Show("Please Input what is needed")

        ElseIf TextBox2.Text = "" Then
            MessageBox.Show("Please Input what is needed")

        ElseIf TextBox3.Text = "" Then

            MessageBox.Show("Please Input what is needed")
        Else

            Dim Item_Name = TextBox1.Text
            Dim Item_Quantity = TextBox2.Text
            Dim Item_Price = TextBox3.Text

            ' Add more fields as needed

            Dim items = New BsonDocument()
            items.Add("ItemName", Item_Name)
            items.Add("ItemQuantity", Item_Quantity)
            items.Add("ItemPrice", Item_Price)



            ' Check if a document with the given value already exists
            If collection.Find(filter).Any() Then
                ' Document already exists, show a message or take other actions
                MessageBox.Show("Data already exists in the database.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                ' Document doesn't exist, proceed with the insertion
                Dim document As New BsonDocument("ItemName", userIn)

                ' Insert the document into the collection

                mongoCollection.InsertOne(items)
                ' Optionally, show a success message or take other actions
                MessageBox.Show("Data added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If




            ' Reload the data in the DataGridView
            LoadDataIntoDataGridView()

            TextBox1.Clear()
            TextBox2.Clear()
            TextBox3.Clear()



        End If




    End Sub



    Private Sub LoadDataIntoDataGridView()
        Try
            DataGridView1.Rows.Clear()

            ' Replace "YourConnectionString" with your actual MongoDB connection string.
            Dim connectionString As String = "mongodb+srv://hardware1234:hardware1234@hardware.dknymeb.mongodb.net/HIMS"
            Dim client As MongoClient = New MongoClient(connectionString)
            Dim database As IMongoDatabase = client.GetDatabase("HIMS")
            Dim collection As IMongoCollection(Of BsonDocument) = database.GetCollection(Of BsonDocument)("item")

            Dim filter = Builders(Of BsonDocument).Filter.Empty
            Dim documents = collection.Find(filter).ToList()

            'Update DataGridView's data source

            For Each doc As BsonDocument In documents
                Dim Item_Name = doc.GetValue("ItemName", String.Empty)
                Dim Item_Quantity = doc.GetValue("ItemQuantity", String.Empty)
                Dim Item_Price = doc.GetValue("ItemPrice", String.Empty)


                DataGridView1.Rows.Add(Item_Name, Item_Quantity, Item_Price)

            Next
        Catch ex As Exception
            ' Handle exceptions, e.g., connection errors or data format issues.
            MessageBox.Show("An error occurred: " & ex.Message)
        End Try




    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If DataGridView1.SelectedRows.Count > 0 Then
            ' Display a confirmation message box
            Dim result As DialogResult = MessageBox.Show("Are you sure you want to delete the selected row?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

            ' Check the user's choice
            If result = DialogResult.Yes Then
                ' Get the selected row and the AccountNumber
                Dim selectedRow = DataGridView1.SelectedRows(0)

                Dim Item_Name = TextBox1.Text


                ' Connect to MongoDB
                Dim connectionString As String = "mongodb+srv://hardware1234:hardware1234@hardware.dknymeb.mongodb.net/HIMS"
                Dim dbName As String = "HIMS"
                Dim client As New MongoClient(connectionString)

                ' Access the MongoDB database
                Dim database As IMongoDatabase = client.GetDatabase(dbName)

                ' Access the collection you want to delete documents from
                Dim collectionName As String = "item"
                Dim collection As IMongoCollection(Of BsonDocument) = database.GetCollection(Of BsonDocument)(collectionName)

                ' Define the filter to find the document by AccountNumber
                Dim filter As FilterDefinition(Of BsonDocument) = Builders(Of BsonDocument).Filter.Eq(Of String)("ItemName", Item_Name)

                ' Delete the document from the collection
                collection.DeleteOne(filter)

                ' Reload data
                LoadDataIntoDataGridView()
                MessageBox.Show("Item Successfully Deleted!")

                ' Clear input fields
                TextBox1.Clear()
                TextBox2.Clear()
                TextBox3.Clear()
            End If
        Else
            MessageBox.Show("Please select a row to delete.", "No Row Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If DataGridView1.SelectedRows.Count > 0 Then
            Dim selectedRow = DataGridView1.SelectedRows(0)


            Dim newItem_Name = TextBox1.Text
            Dim newItem_Quantity = TextBox2.Text
            Dim newItem_Price = TextBox3.Text


            ' Define the filter to find the document by AccountNumber
            Dim filter As FilterDefinition(Of BsonDocument) = Builders(Of BsonDocument).Filter.Eq(Of String)("ItemName", newItem_Name)

            ' Define the update operation to set the values of fields
            Dim update As UpdateDefinition(Of BsonDocument) = Builders(Of BsonDocument).Update.Set(Function(doc) doc("ItemQuantity"), newItem_Quantity) _
                .Set(Function(doc) doc("ItemPrice"), newItem_Price) _


            ' Connect to MongoDB
            Dim connectionString As String = "mongodb+srv://hardware1234:hardware1234@hardware.dknymeb.mongodb.net/HIMS"
            Dim dbName As String = "HIMS"
            Dim client As New MongoClient(connectionString)

            ' Access the MongoDB database
            Dim database As IMongoDatabase = client.GetDatabase(dbName)

            ' Access the collection you want to update documents in
            Dim collectionName As String = "item"
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



        End If
    End Sub



    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged

        If DataGridView1.SelectedRows.Count > 0 Then
            Dim selectedRow = DataGridView1.SelectedRows(0)
            TextBox1.Text = selectedRow.Cells("ItemName").Value.ToString()
            TextBox2.Text = selectedRow.Cells("ItemQuantity").Value.ToString()
            TextBox3.Text = selectedRow.Cells("ItemPrice").Value.ToString()

        End If
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub


    Private Function IsValueUnique(value As String) As Boolean
        Dim connectionString As String = "mongodb+srv://hardware1234:hardware1234@hardware.dknymeb.mongodb.net/HIMS"
        Dim client As MongoClient = New MongoClient(connectionString)
        Dim database As IMongoDatabase = client.GetDatabase("HIMS")
        Dim filter = Builders(Of BsonDocument).Filter.Eq(Of String)("ItemName", value) ' Replace "field_name" with the actual field name
        Dim collection As IMongoCollection(Of BsonDocument) = database.GetCollection(Of BsonDocument)("item")

        Dim count = collection.CountDocuments(filter)

        Return count = 0
    End Function
    Private Sub AddToMongoDB(data As BsonDocument)
        Try
            ' Check if the value is unique
            Dim connectionString As String = "mongodb+srv://hardware1234:hardware1234@hardware.dknymeb.mongodb.net/HIMS"
            Dim client As MongoClient = New MongoClient(connectionString)
            Dim database As IMongoDatabase = client.GetDatabase("HIMS")
            Dim filter = Builders(Of BsonDocument).Filter.Eq(Of String)("ItemName", data)
            Dim valueToCheck As String = data.GetValue("field_name").AsString ' Replace "field_name" with the actual field name
            Dim collection As IMongoCollection(Of BsonDocument) = database.GetCollection(Of BsonDocument)("item")
            If IsValueUnique(valueToCheck) Then
                ' Insert the document
                collection.InsertOne(data)
                MessageBox.Show("Data added successfully!")
            Else
                MessageBox.Show("Data already exists in MongoDB. It was not added.")
            End If
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click

    End Sub

    Private Sub TextBox2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox2.KeyPress
        Call ValidateTextBox()
    End Sub

    Function ValidateTextBox() As Boolean
        If Not IsNumeric(TextBox2.Text) Then

            Return False
        End If


        Return True
    End Function

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged

    End Sub
End Class
