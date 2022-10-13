Imports System.Data.OleDb

Module koneksidatabase
    Public Koneksi As OleDbConnection
    Public DA As OleDbDataAdapter
    Public DS As DataSet
    Public Perintah, Perintah1, Perintah2, Perintah3, Perintah4 As OleDbCommand
    Public Pembaca, Pembaca1, Pembaca2, Pembaca3, Pembaca4 As OleDbDataReader
    Public SQL, SQL1, SQL2, SQL3, SQL4 As String
    Public AlamatDatabase As String = "Provider=Microsoft.JET.OLEDB.4.0;Data Source=" & _
              Application.StartupPath & "\Sibayakmulti.mdb"
    Public Sub Bukadatabase()
        Koneksi = New OleDbConnection(AlamatDatabase)
        If Koneksi.State = ConnectionState.Closed Then
            Koneksi.Close()
            Try
                Koneksi.Open()

            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End If
    End Sub
End Module
