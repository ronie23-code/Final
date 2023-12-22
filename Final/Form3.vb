Public Class Form3
    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        Call UserControl102.BringToFront()

    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        UserControl112.BringToFront()
        Form1.Show()

        Me.Hide()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Call UserControl122.BringToFront()


    End Sub

    Private Sub UserControl102_Load(sender As Object, e As EventArgs)

    End Sub

    Private Sub UserControl122_Load(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        UserControl52.BringToFront()
    End Sub

    Private Sub UserControl52_Load(sender As Object, e As EventArgs)

    End Sub

    Private Sub UserControl112_Load(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        UserControl61.BringToFront()

    End Sub

    Private Sub Panel3_Paint(sender As Object, e As PaintEventArgs) Handles Panel3.Paint

    End Sub


    Private Sub UserControl52_Load_1(sender As Object, e As EventArgs)

    End Sub

    Private Sub UserControl22_Load(sender As Object, e As EventArgs) Handles UserControl22.Load

    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        UserControl41.BringToFront()
    End Sub
End Class