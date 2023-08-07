Imports System.Windows.Forms
Imports System.IO

Public Class Dialog1

    Private Sub MostrarNotificacion(titulo As String, mensaje As String)
        ' Mostrar una notificación emergente
        notifyIcon.BalloonTipTitle = titulo
        notifyIcon.BalloonTipText = mensaje
        notifyIcon.ShowBalloonTip(3000) ' Mostrar durante 3 segundos
        AddHandler notifyIcon.BalloonTipClicked, AddressOf NotifyIcon_BalloonTipClicked
    End Sub

    Private Sub NotifyIcon_BalloonTipClicked(sender As Object, e As EventArgs)
        ' Manejar el evento cuando se hace clic en la notificación
    End Sub

    Private notifyIcon As NotifyIcon

    Public Sub New()
        InitializeComponent()

        ' Configura el NotifyIcon
        notifyIcon = New NotifyIcon()
        notifyIcon.Icon = SystemIcons.Information
        notifyIcon.Visible = True
    End Sub


    Dim DateSelected As String

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click

        Dim rfile As String = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "CPDates/Dates/Dates.txt")
        Dim sData As String = DateSelected & " - " & ComboBox1.Text + ": " & TextBox1.Text


        File.AppendAllText(rfile, Environment.NewLine & sData)
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
        MostrarNotificacion("Date sucefully saved", "The date " & DateSelected & " is sucefully saved. We dont dude on remember")
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
        MostrarNotificacion("Date deleted", "The date will not saved")
    End Sub

    Private Sub MonthCalendar1_DateSelected(sender As Object, e As DateRangeEventArgs) Handles MonthCalendar1.DateSelected
        Dim day As Integer = MonthCalendar1.SelectionStart.Day
        Dim month As Integer = MonthCalendar1.SelectionStart.Month
        DateSelected = day.ToString & "/" & month
    End Sub
End Class
