Imports System.IO
Imports System.Runtime.InteropServices.JavaScript
Imports System.Text
Imports System.Threading

Public Class Punch
    Private client As New TcpClient
    Private clientStream As NetworkStream
    Private clientReader As StreamReader
    Private clientWriter As StreamWriter 'Should never be needed but what the hay.
    Private _cancellationTokenSource As CancellationTokenSource
    Private currentDocument As New List(Of String)
    Private IsConnected As Boolean = False
    Private Receiving = False
    Private myConnection As String = ""
    Private myName As String = ""
    Private RemoteHost As String = ""
    Private RemotePort As Integer = 0
    Private OutDest As String = ""
    Property ConnectionString As String
        Get
            Return myConnection
        End Get
        Set(value As String)
            myConnection = value
        End Set
    End Property

    Property Name As String
        Get
            Return myName
        End Get
        Set(value As String)
            myName = value
        End Set
    End Property

    Public Sub New()

    End Sub

    Public Sub New(connection As String, Name As String)
        myConnection = connection
        myName = Name
    End Sub

    Private Function SplitDestination(dest As String) As (remoteHost As String, remotePort As Integer)
        Dim thisHost As String
        Dim thisPort As Integer
        Dim splitDev As String()
        Dim remoteHost As String
        Dim remotePort As Integer
        splitDev = dest.Split(":")
        thisHost = splitDev(0)
        thisPort = Val(splitDev(1))
        If thisHost.Trim <> "" Then
            remoteHost = thisHost.Trim
        Else
            Throw (New Exception("Destination does not contain a valid hostname"))
        End If
        If thisPort <> 0 Then
            remotePort = thisPort
        Else
            Throw (New Exception("Destination does not contain a valid port."))
        End If
        Debug.Print($"Returning: Hostname={remoteHost}, on port {remotePort}")
        Return (remoteHost, remotePort)
    End Function

    Public Async Sub Connect()

        Await StartAsync()  ' Connects to the server and starts receiving data
    End Sub
    Public Async Function StartAsync() As Task
        client = New TcpClient()
        _cancellationTokenSource = New CancellationTokenSource()
        Dim vals As (remoteHost As String, remotePort As Integer) = ("", 0)
        vals = SplitDestination(myConnection)
        RemoteHost = vals.remoteHost
        RemotePort = vals.remotePort
        Try

            Await client.ConnectAsync(RemoteHost, RemotePort)
            clientStream = client.GetStream()
            IsConnected = True
            ' Start receiving data
            Await ReceiveDataAsync(_cancellationTokenSource.Token)

        Catch ex As Exception
            Debug.Print(ex.Message)
            IsConnected = False
        Finally
            Try
                Disconnect()
            Catch disconnectEx As Exception

                IsConnected = False
            End Try
            IsConnected = False
        End Try
    End Function

    ' Continuously receives data from the server
    Private Async Function ReceiveDataAsync(cancellationToken As CancellationToken) As Task
        Dim buffer(8192) As Byte ' Larger buffer for fewer ReadAsync calls, a bit over a full page.
        Dim dataBuilder As New StringBuilder()
        Dim lastReceivedTime As DateTime = DateTime.Now
        Dim inactivityTimeout As TimeSpan = TimeSpan.FromSeconds(5) ' Timeout period (5 seconds)

        Try
            While Not cancellationToken.IsCancellationRequested
                ' Check for data availability or cancellation
                If Not clientStream.DataAvailable Then
                    ' Wait for data to become available (with a small delay to avoid busy-waiting)
                    Await Task.Delay(100, cancellationToken) ' Block for 100ms and check again
                    clientStream.WriteByte(0)
                    ' If no data available and we are inactive for too long, process the current document
                    If DateTime.Now - lastReceivedTime > inactivityTimeout AndAlso dataBuilder.Length > 0 Then
                        ' Process the complete document if we have accumulated data and timeout has occurred

                        '******************************************************
                        '* 
                        '*    ONCE THE DECK IS RECEIVED CALL THE FOLLOWING
                        '*    FUNCTION.
                        '*
                        '*    MAKE SURE YOU PROCESS IT COMPLETELY, BECAUSE 
                        '*    ONCE YOU RETURN FROM THE FUNCTION, THE DECK
                        '*    WILL BE CLEARED!
                        '*
                        '******************************************************

                        Debug.Print("Processing...")
                        ProcessDocumentData(dataBuilder.ToString())

                        dataBuilder.Clear() ' Clear data for the next document
                        lastReceivedTime = DateTime.Now ' Reset the inactivity timer
                    End If
                Else
                    ' If data is available, read it
                    If Not Receiving Then
                        Receiving = True
                    End If
                    Dim recd As Integer = Await clientStream.ReadAsync(buffer, 0, buffer.Length, cancellationToken)
                    If recd > 0 Then
                        ' Append received data to the data builder
                        Dim receivedPart As String = Encoding.UTF8.GetString(buffer, 0, recd)
                        dataBuilder.Append(receivedPart)
                        Debug.Print(receivedPart)
                        ' Update last received time to now
                        lastReceivedTime = DateTime.Now
                    End If
                End If

            End While
        Catch ex As OperationCanceledException

        Catch ex As Exception
            If ex.HResult = -2146232800 Then

            Else

            End If
        End Try
    End Function

    Sub ProcessDocumentData(deck As String)
        Dim filename As String = $"PUNCH-DECK-{Now.Month}-{Now.Day}-{Now.Year}-{Now.Hour}-{Now.Minute}-{Now.Second}.deck"
        Debug.Print("Saving " & filename)
        Dim outStream As New StreamWriter(filename)
        outStream.Write(deck)
        Debug.Print("Write:" & deck)
        outStream.Flush()
        outStream.Close()
    End Sub



    ' Disconnects the client
    Public Sub Disconnect()
        Try
            _cancellationTokenSource?.Cancel()
            clientStream?.Close()
            client?.Close()
        Catch ex As Exception

        End Try
    End Sub
End Class
