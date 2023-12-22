Imports MongoDB.Bson
Imports MongoDB.Driver
Imports System.IO.Ports



Public Class UserControl6
    ' MongoDB Atlas connection string
    Private Const ConnectionString As String = "mongodb+srv://hardware1234:hardware1234@hardware.dknymeb.mongodb.net/HIMS"

    ' MongoDB collection and database names
    Private Const DatabaseName As String = "HIMS"
    Private Const CollectionName As String = "customer"

    'GSM module settings
    '   Private Const PortName As String = "COM5" ' Replace with the actual COM port of your GSM module
    '   Private Const BaudRate As Integer = 9600

    ' MongoDB client and collection
    Private client As MongoClient
    Private collection As IMongoCollection(Of BsonDocument)
    Dim SerialPort1 As New SerialPort
    Dim orders As String
    Dim prices As String
    Dim quantities As String
    Private Sub UserControl6_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SerialPort1 = New SerialPort()
        SerialPort1.PortName = "COM5"
        SerialPort1.BaudRate = 115200
        SerialPort1.Parity = Parity.None
        SerialPort1.StopBits = StopBits.One
        SerialPort1.DataBits = 8
        SerialPort1.Handshake = Handshake.None
        SerialPort1.DtrEnable = True
        SerialPort1.RtsEnable = True
        SerialPort1.NewLine = vbCrLf
        SerialPort1.Open()



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
                Dim CustomerName = doc.GetValue("CustomerName", String.Empty)
                Dim CustomerNumber = doc.GetValue("CustomerNumber", String.Empty)
                Dim CustomerAddress = doc.GetValue("CustomerAddress", String.Empty)
                Dim Order = doc.GetValue("Order", String.Empty)
                Dim Quantity = doc.GetValue("Quantity", String.Empty)
                Dim Price = doc.GetValue("Price", String.Empty)


                DataGridView1.Rows.Add(Time, CustomerName, CustomerNumber, CustomerAddress, Order, Quantity, Price)

            Next
        Catch ex As Exception
            ' Handle exceptions, e.g., connection errors or data format issues.
            MessageBox.Show("An error occurred: " & ex.Message)
        End Try

        ' Load data into the DataGridView (you may implement this based on your application)
        'LoadDataIntoDataGridView()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim receiverNumber As String = TextBox1.Text
        Dim messageContent1 As String = "Good day! thankyou for buying in our hardware store, kindly prepare "
        Dim messageContent2 As String = " pesos for your order, "
        Dim messageContent As String = messageContent1 & prices & messageContent2 & orders & ". Have a nice day!"



        ' Check if the receiver's number and message content are not empty
        If String.IsNullOrEmpty(receiverNumber) OrElse String.IsNullOrEmpty(messageContent) Then
            MessageBox.Show("Please enter a receiver's number and a message.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If
        Dim remainingQuantity As String
        Dim connectionString As String = "mongodb+srv://hardware1234:hardware1234@hardware.dknymeb.mongodb.net/HIMS"
        Dim client As MongoClient = New MongoClient(connectionString)
        Dim database As IMongoDatabase
        Dim collection As IMongoCollection(Of BsonDocument)
        ' Set up the database and collection
        database = client.GetDatabase("HIMS")
        collection = database.GetCollection(Of BsonDocument)("item")

        Dim filter = Builders(Of BsonDocument).Filter.Eq(Of String)("ItemName", orders)
        Dim document = collection.Find(filter).FirstOrDefault()


        If document IsNot Nothing Then
            remainingQuantity = document.GetValue("ItemQuantity")
        End If
        Dim TotalQuantity As Integer = Val(remainingQuantity) - Val(quantities)
        Dim TotalQuantity1 As String = TotalQuantity.ToString

        Dim filter1 = Builders(Of BsonDocument).Filter.Eq(Of String)("ItemName", orders)

        ' Define the update operation   
        Dim update = Builders(Of BsonDocument).Update.Set(Of String)("ItemQuantity", TotalQuantity1)

        ' Perform the update
        Dim result = collection.UpdateOne(filter, update)
        If result.ModifiedCount > 0 Then
            ' Document updated successfully


            Dim atCommand As String = "AT+CMGS=" & """" & receiverNumber & """" & vbCr


            If SerialPort1.IsOpen = True Then
                SerialPort1.Write("AT" & vbCrLf)
                SerialPort1.Write("AT+CMGF=1" & vbCrLf)
                SerialPort1.Write(atCommand)
                Dim response As String = SerialPort1.ReadExisting()
                Do Until response.Contains(">")
                    response &= SerialPort1.ReadExisting()
                Loop
                SerialPort1.Write(messageContent & Chr(26))
                System.Threading.Thread.Sleep(5000)
                Dim newresponse = SerialPort1.ReadExisting()
                If newresponse.Contains("OK") Then
                    MessageBox.Show("Message sent successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    MessageBox.Show("Failed to send message.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            Else
                MessageBox.Show("Error: Invalid Port", "Port", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If


        Else
            MessageBox.Show("No matching document found.")
        End If
        '' Initialize MongoDB client and collection
        'client = New MongoClient(ConnectionString)
        'Dim database As IMongoDatabase = client.GetDatabase(DatabaseName)
        'collection = database.GetCollection(Of BsonDocument)(CollectionName)

        '' Retrieve customer number based on TextBox1 value
        'Dim customerNumber As String = GetCustomerNumber(TextBox1.Text)

        'If Not String.IsNullOrEmpty(customerNumber) Then
        '    ' Send SMS using the GSM module
        '    SendSMS(customerNumber, "Please prepare Exact Amount of the order, Thank youu!")
        'Else
        '    MessageBox.Show("Customer number not found.")
        'End If
    End Sub

    ' Function to retrieve customer number based on TextBox1 value
    Private Function GetCustomerNumber(textBoxValue As String) As String
        Dim filter = Builders(Of BsonDocument).Filter.Eq(Of String)("CustomerNumber", textBoxValue)
        Dim projection = Builders(Of BsonDocument).Projection.Include("CustomerNumber")
        Dim document = collection.Find(filter).Project(projection).FirstOrDefault()

        If document IsNot Nothing AndAlso document.Contains("CustomerNumber") Then
            Return document.GetValue("CustomerNumber").AsString
        Else
            Return String.Empty
        End If
    End Function

    ' Function to send SMS using the GSM module
    'Private Sub SendSMS(phoneNumber As String, message As String)
    '    Dim serialPort As New SerialPort(PortName, BaudRate)
    '    serialPort.Open()

    '    ' Format the AT command to send SMS
    '    Dim atCommand As String = "AT+CMGS=""" & TextBox1.text & """" & vbCrLf

    '    ' Write the AT command to the serial port
    '    serialPort.Write(atCommand)

    '    ' Wait for the module to respond
    '    Threading.Thread.Sleep(3000)

    '    ' Write the message to the serial port
    '    serialPort.Write(message & Chr(26))

    '    ' Wait for the module to send the SMS
    '    Threading.Thread.Sleep(10000)

    '    ' Close the serial port
    '    serialPort.Close()
    'End Sub
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
                Dim CustomerName = doc.GetValue("CustomerName", String.Empty)
                Dim CustomerNumber = doc.GetValue("CustomerNumber", String.Empty)
                Dim CustomerAddress = doc.GetValue("CustomerAddress", String.Empty)
                Dim Order = doc.GetValue("Order", String.Empty)
                Dim Quantity = doc.GetValue("Quantity", String.Empty)
                Dim Price = doc.GetValue("Price", String.Empty)


                DataGridView1.Rows.Add(Time, CustomerName, CustomerNumber, CustomerAddress, Order, Quantity, Price)

            Next
        Catch ex As Exception
            ' Handle exceptions, e.g., connection errors or data format issues.
            MessageBox.Show("An error occurred: " & ex.Message)
        End Try




    End Sub
    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged

        If DataGridView1.SelectedRows.Count > 0 Then
            Dim selectedRow = DataGridView1.SelectedRows(0)
            TextBox1.Text = selectedRow.Cells("CustomerNumber").Value.ToString()
            orders = selectedRow.Cells("Order").Value.ToString()
            prices = selectedRow.Cells("Price").Value.ToString()
            quantities = selectedRow.Cells("Quantity").Value.ToString()
            'TextBox2.Text = selectedRow.Cells("Firstname").Value.ToString()
            'TextBox3.Text = selectedRow.Cells("Username").Value.ToString()
            'TextBox4.Text = selectedRow.Cells("Password").Value.ToString()

        End If
        'LoadDataIntoDataGridView()
    End Sub


    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub
End Class
