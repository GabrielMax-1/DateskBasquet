Imports System.Globalization
Imports System.IO
Imports System.Windows.Forms
Public Class Form1

    Dim rutaArchivo As String = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "CPDates/Dates/Dates.txt")


    Private Sub MostrarNotificacion(titulo As String, mensaje As String)
        ' Mostrar una notificación emergente
        notifyIcon.BalloonTipTitle = titulo
        notifyIcon.BalloonTipText = mensaje
        notifyIcon.ShowBalloonTip(3000) ' Mostrar durante 3 segundos
        AddHandler notifyIcon.BalloonTipClicked, AddressOf NotifyIcon_BalloonTipClicked
    End Sub

    Private Sub NotifyIcon_BalloonTipClicked(sender As Object, e As EventArgs)
        ' Manejar el evento cuando se hace clic en la notificación
        MessageBox.Show("Notificación clicada.")
    End Sub

    Private notifyIcon As NotifyIcon

    Public Sub New()
        InitializeComponent()

        ' Configura el NotifyIcon
        notifyIcon = New NotifyIcon()
        notifyIcon.Icon = SystemIcons.Information
        notifyIcon.Visible = True
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load






        ' Lee todas las líneas del archivo


        Dim fechaActual As DateTime = DateTime.Now
        Dim lineasFuturas As New List(Of String)()


        ' Verifica si el archivo existe
        If File.Exists(rutaArchivo) Then
            Console.WriteLine("El archivo existe.")
        Else
            ' Crea el directorio si no existe
            Dim directorio As String = Path.GetDirectoryName(rutaArchivo)
            If Not Directory.Exists(directorio) Then
                Directory.CreateDirectory(directorio)
            End If

            ' Crea el archivo
            File.Create(rutaArchivo).Dispose()
        End If
        Dim idiomaActual As CultureInfo = CultureInfo.CurrentCulture

        ' Define las configuraciones culturales del español y sus variantes
        Dim configuracionesEspañol() As String = {
            "es",   ' Español genérico
            "es-ES", ' Español de España
            "es-AR", ' Español de Argentina
            "es-BO", ' Español de Bolivia
            "es-CL", ' Español de Chile
            "es-CO", ' Español de Colombia
            "es-CR", ' Español de Costa Rica
            "es-CU", ' Español de Cuba
            "es-DO", ' Español de República Dominicana
            "es-EC", ' Español de Ecuador
            "es-SV", ' Español de El Salvador
            "es-GT", ' Español de Guatemala
            "es-HN", ' Español de Honduras
            "es-MX", ' Español de México
            "es-NI", ' Español de Nicaragua
            "es-PA", ' Español de Panamá
            "es-PY", ' Español de Paraguay
            "es-PE", ' Español de Perú
            "es-PR", ' Español de Puerto Rico
            "es-ES", ' Español de España
            "es-UY", ' Español de Uruguay
            "es-VE"  ' Español de Venezuela
        }

        ' Filtra las líneas que son futuras y las agrega a la lista
        Dim lineas As String() = File.ReadAllLines(rutaArchivo)
        For Each linea As String In lineas
            Dim partes As String() = linea.Split({" - "}, StringSplitOptions.None)
            If partes.Length = 2 Then
                Dim fechaPartes As String() = partes(0).Split("/"c)
                If fechaPartes.Length = 2 Then
                    Dim diaRecordatorio As Integer
                    Dim mesRecordatorio As Integer
                    If Integer.TryParse(fechaPartes(0), diaRecordatorio) AndAlso
                       Integer.TryParse(fechaPartes(1), mesRecordatorio) Then

                        Dim fechaRecordatorio As New DateTime(fechaActual.Year, mesRecordatorio, diaRecordatorio)
                        If fechaRecordatorio >= fechaActual Then
                            lineasFuturas.Add(linea)
                        End If
                    End If
                End If
            End If
        Next

        ' Ordena la lista por fecha y toma las 3 primeras líneas
        Dim lineasProximas As List(Of String) = lineasFuturas.OrderBy(Function(linea) DateTime.ParseExact(linea.Split({" - "}, StringSplitOptions.None)(0), "d/M", CultureInfo.InvariantCulture)).Take(3).ToList()

        Dim labels() As Label = {Label4, Label5, Label6}
        For i As Integer = 0 To lineasProximas.Count - 1
            labels(i).Text = lineasProximas(i)
        Next





        ' Comprueba si la configuración cultural actual es una variante del español
        If configuracionesEspañol.Contains(idiomaActual.Name, StringComparer.OrdinalIgnoreCase) Then
            Label2.Text = por_max
            Button1.Text = nueva_fecha
            Label3.Text = prox_fechas
        Else
            Label2.Text = by_max
            Button1.Text = new_date
            Label3.Text = nexts_dates
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dialog1.Show()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles PictureBox3.Click

        Dim fechaActual As DateTime = DateTime.Now
        Dim lineasFuturas As New List(Of String)()

        Dim lineas As String() = File.ReadAllLines(rutaArchivo)
        For Each linea As String In lineas
            Dim partes As String() = linea.Split({" - "}, StringSplitOptions.None)
            If partes.Length = 2 Then
                Dim fechaPartes As String() = partes(0).Split("/"c)
                If fechaPartes.Length = 2 Then
                    Dim diaRecordatorio As Integer
                    Dim mesRecordatorio As Integer
                    If Integer.TryParse(fechaPartes(0), diaRecordatorio) AndAlso
                       Integer.TryParse(fechaPartes(1), mesRecordatorio) Then

                        Dim fechaRecordatorio As New DateTime(fechaActual.Year, mesRecordatorio, diaRecordatorio)
                        If fechaRecordatorio >= fechaActual Then
                            lineasFuturas.Add(linea)
                        End If
                    End If
                End If
            End If
        Next

        ' Ordena la lista por fecha y toma las 3 primeras líneas
        Dim lineasProximas As List(Of String) = lineasFuturas.OrderBy(Function(linea) DateTime.ParseExact(linea.Split({" - "}, StringSplitOptions.None)(0), "d/M", CultureInfo.InvariantCulture)).Take(3).ToList()

        Dim labels() As Label = {Label4, Label5, Label6}
        For i As Integer = 0 To lineasProximas.Count - 1
            labels(i).Text = lineasProximas(i)
        Next
    End Sub
End Class
