Imports System.Data.OleDb
Public Class Form_Login
    Dim id, msg As String
    Dim i As Integer
    Sub Cari()
        SQL = "select * from tbl_user " & _
                              "where Username='" & Trim(username.Text) & "'" & _
                              " and Password='" & _
                              Trim(password.Text) & "'"
        Perintah = New OleDbCommand(SQL, Koneksi)
        Pembaca = Perintah.ExecuteReader
    End Sub
    Private Sub btnlogin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnlogin.Click
        i = i + 1
        If i = 4 Then
            MsgBox("Anda telah 3 kali melakukan kesalahan. Program akan ditutup.", _
                       MsgBoxStyle.Critical, "Konfirmasi")
            End
        Else
            Call Cari()
            If username.Text.Trim() = "" And password.Text.Trim() = "" Then
                msg = MsgBox("Masukan Username dan Password", MsgBoxStyle.Information, "Konfirmasi")
                username.Focus()
            ElseIf username.Text = "" Then
                msg = MsgBox("Masukan Username Anda", MsgBoxStyle.Information, "Konfirmasi")
                username.Focus()
            ElseIf password.Text = "" Then
                msg = MsgBox("Masukan password Anda", MsgBoxStyle.Information, "Konfirmasi")
                password.Focus()
            Else
                Try
                    If Pembaca.Read = False Then
                        MsgBox("Username Atau Password Yang Anda Masukkan Salah!!", MsgBoxStyle.Critical, "Peringatan!")
                    Else
                        MsgBox("Selamat Datang", 64, "Informasi")
                        Form_Menu.Show()
                        Me.Hide()
                    End If
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
            End If
        End If
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call Bukadatabase()
        i = 0
    End Sub

    Private Sub btncancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncancel.Click
        If MsgBox("Apakah Anda Tidak Akan Melanjutkan Login ?", 32 + 4, "Informasi") = MsgBoxResult.No Then
            Exit Sub
        End If
        End

    End Sub
    Private Sub username_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles username.KeyDown
        If e.KeyCode = Keys.Enter Then
            btnlogin_Click(Nothing, Nothing)
        End If
    End Sub
    Private Sub password_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles password.KeyDown
        If e.KeyCode = Keys.Enter Then
            btnlogin_Click(Nothing, Nothing)
        End If
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        End
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        If Me.WindowState = FormWindowState.Normal Then
            Me.WindowState = FormWindowState.Maximized
        Else
            Me.WindowState = FormWindowState.Normal
        End If
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub
End Class
