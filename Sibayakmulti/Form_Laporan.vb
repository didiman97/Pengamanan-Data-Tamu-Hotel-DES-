Imports System.Windows.Forms
Imports System.Data.OleDb
Public Class Form_Laporan


    Private m_ChildFormNumber As Integer

    Private Sub Form_Laporan_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CrystalReportViewer1.Refresh()

    End Sub

    Private Sub CrystalReportViewer1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CrystalReportViewer1.Load
        Try
            Dim objreport As New CrystalDecisions.CrystalReports.Engine.ReportDocument
            Dim ConInfo As New CrystalDecisions.Shared.TableLogOnInfo
            Dim strAlamatReport As String = Application.StartupPath & "\" & "Report2.rpt"
            objreport.Load(strAlamatReport)
            With ConInfo.ConnectionInfo
                .UserID = "Admin"
                .ServerName = Application.StartupPath & "\Sibayakmulti.mdb"
                .DatabaseName = Application.StartupPath & "\Sibayakmulti.mdb"
                .IntegratedSecurity = False
            End With
            For IntCounter As Integer = 0 To objreport.Database.Tables.Count - 1
                objreport.Database.Tables(IntCounter).ApplyLogOnInfo(ConInfo)
            Next

            CrystalReportViewer1.ReportSource = objreport
            CrystalReportViewer1.RefreshReport() : CrystalReportViewer1.Show()
        Catch ex As Exception
            MsgBox(ex.Message)

        End Try
    End Sub

    Private Sub Button8_Click_1(sender As Object, e As EventArgs) Handles Button8.Click
        If Me.WindowState = FormWindowState.Normal Then
            Me.WindowState = FormWindowState.Maximized
        Else
            Me.WindowState = FormWindowState.Normal
        End If
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Me.Close()
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub
End Class
