Imports MongoDB.Bson
Imports MongoDB.Driver

Module Module1
    Sub Main()


    End Sub
End Module
Public Class UserControl4
    Dim totalPrice As Integer

    Private Sub UserControl4_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim connectionString As String = "mongodb+srv://hardware1234:hardware1234@hardware.dknymeb.mongodb.net/HIMS"
        Dim client As MongoClient = New MongoClient(connectionString)
        Dim database As IMongoDatabase
        Dim collection As IMongoCollection(Of BsonDocument)
        ' Set up the database and collection
        database = client.GetDatabase("HIMS")
        collection = database.GetCollection(Of BsonDocument)("customer")

        Try
            DataGridView1.Rows.Clear()


            Dim filter = Builders(Of BsonDocument).Filter.Empty
            Dim documents = collection.Find(filter).ToList()

            'Update DataGridView's data source
            For Each doc As BsonDocument In documents
                Dim Time = doc.GetValue("Date", String.Empty)

                Dim Order = doc.GetValue("Order", String.Empty)
                Dim Quantity = doc.GetValue("Quantity", String.Empty)
                Dim Price = doc.GetValue("Price", String.Empty)


                DataGridView1.Rows.Add(Time, Order, Quantity, Price)
                totalPrice = totalPrice + Val(Price)

            Next
        Catch ex As Exception
            ' Handle exceptions, e.g., connection errors or data format issues.
            MessageBox.Show("An error occurred: " & ex.Message)
        End Try
        Label1.Text = totalPrice
    End Sub

    Private Sub LoadDataIntoDataGridView()
        Try
            DataGridView1.Rows.Clear()

            ' Replace "YourConnectionString" with your actual MongoDB connection string.
            Dim connectionString As String = "mongodb+srv://hardware1234:hardware1234@hardware.dknymeb.mongodb.net/HIMS"
            Dim client As MongoClient = New MongoClient(connectionString)
            Dim database As IMongoDatabase = client.GetDatabase("HIMS")
            Dim collection As IMongoCollection(Of BsonDocument) = database.GetCollection(Of BsonDocument)("customer")

            Dim filter = Builders(Of BsonDocument).Filter.Empty
            Dim documents = collection.Find(filter).ToList()

            'Update DataGridView's data source

            For Each doc As BsonDocument In documents
                Dim Time = doc.GetValue("Date", String.Empty)

                Dim Order = doc.GetValue("Order", String.Empty)
                Dim Quantity = doc.GetValue("Quantity", String.Empty)
                Dim Price = doc.GetValue("Price", String.Empty)




                DataGridView1.Rows.Add(Time, Order, Quantity, Price)



            Next

        Catch ex As Exception
            ' Handle exceptions, e.g., connection errors or data format issues.
            MessageBox.Show("An error occurred: " & ex.Message)
        End Try






    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        ' Adjust the field name as per your MongoDB document
    End Sub

    Private Sub DateTimePicker1_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker1.ValueChanged

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        totalPrice = 0
        DataGridView1.Rows.Clear()
        Dim client As MongoClient = New MongoClient("mongodb+srv://hardware1234:hardware1234@hardware.dknymeb.mongodb.net/HIMS")
        Dim database As IMongoDatabase = client.GetDatabase("HIMS")
        Dim collection As IMongoCollection(Of BsonDocument) = database.GetCollection(Of BsonDocument)("customer")
        Dim parsedDate As DateTime

        ' Define the start and end dates for your date range
        Dim startDate As DateTime = DateTimePicker1.Value.Date
        Dim endDate As DateTime = DateTimePicker2.Value.Date

        ' Create a filter for the date range

        ' Retrieve documents based on the filter
        Dim documents = collection.Find(New BsonDocument()).ToList()

        ' Iterate through the filtered documents
        For Each doc As BsonDocument In documents
            If doc.Contains("Date") AndAlso Not String.IsNullOrEmpty(doc.GetValue("Date").ToString()) Then

                Dim Time = doc.GetValue("Date", String.Empty)
                Dim Order = doc.GetValue("Order", String.Empty)
                Dim Quantity = doc.GetValue("Quantity", String.Empty)
                Dim Price = doc.GetValue("Price", String.Empty)
                Console.WriteLine(doc)

                parsedDate = DateTime.Parse(Time).Date
                If startDate >= parsedDate And endDate <= parsedDate Then
                    DataGridView1.Rows.Add(Time, Order, Quantity, Price)
                    totalPrice = totalPrice + Val(Price)

                End If
            Else

            End If
        Next
        Label1.Text = totalPrice
    End Sub
End Class
