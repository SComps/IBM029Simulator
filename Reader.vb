Imports System.ComponentModel.Design
Imports System.IO
Imports System.Net.Sockets
Imports System.Text

Public Class Reader
    ' Class to define Hercules Readers
    Private myConnection As String = ""
    Private myName As String = ""
    Property isDefault As Boolean = False
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

    Property UseDefault As Boolean
        Get
            Return isDefault
        End Get
        Set(value As Boolean)
            isDefault = value
        End Set
    End Property

    Public Sub New()

    End Sub

    Public Sub New(connection As String, Name As String, setDefault As Boolean)
        myConnection = connection
        myName = Name
        isDefault = setDefault
    End Sub

    Public Sub Submit(deck As List(Of String))
        Dim remoteHost As String = ""
        Dim remotePort As Integer = 0
        Dim vals As (rhost As String, rport As Integer) = ("", 0)
        Dim LineBuff As Byte()
        vals = SplitDestination(myConnection)
        remoteHost = vals.rhost
        remotePort = vals.rport
        Try
            Dim remote As New TcpClient(remoteHost, remotePort)
            Dim portWriter As NetworkStream = remote.GetStream
            For Each card As String In deck
                card = card & vbCrLf
                LineBuff = Encoding.ASCII.GetBytes(card)    ' Encode an array of bytes in ASCII from card
                portWriter.Write(LineBuff)
            Next
            portWriter.Close()
            remote.Close()
        Catch ex As Exception
            Debug.Print(ex.Message)
        End Try
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

End Class
