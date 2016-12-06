Imports System.Management
Imports System.Threading

Public Class Form1

    Public rcvdata As String = ""

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        lbl_serial.BackColor = Color.Red
        queryCOMPorts()
    End Sub
    Private Sub SerialPort1_datareceived(ByVal sender As Object, ByVal e As System.IO.Ports.SerialDataReceivedEventArgs) Handles SerialPort1.DataReceived

        Dim datain As String = ""
        Dim numbytes As Integer = SerialPort1.BytesToRead
        For i As Integer = 1 To numbytes
            datain &= Chr(SerialPort1.ReadChar)
        Next
        test(datain)
    End Sub
    Private Sub test(ByVal indata As String)
        rcvdata &= indata
    End Sub
    Public Function ModemsConnected() As String
        Dim modems As String = ""
        Try
            Dim searcher As New ManagementObjectSearcher( _
                "root\CIMV2", _
                "SELECT * FROM Win32_POTSModem")

            For Each queryObj As ManagementObject In searcher.Get()
                If queryObj("Status") = "OK" Then
                    modems = modems & (queryObj("AttachedTo") & " - " & queryObj("Description") & "***")
                End If
            Next
        Catch err As ManagementException
            MessageBox.Show("An error occurred while querying for WMI data: " & err.Message)
            Return ""
        End Try
        Return modems
    End Function

    Private Sub btnConnect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConnect.Click
        Try
            With SerialPort1
                .PortName = lbl_serial.Text
                .BaudRate = 9600
                .Parity = IO.Ports.Parity.None
                .DataBits = 8
                .StopBits = IO.Ports.StopBits.One
                .Handshake = IO.Ports.Handshake.None
                .RtsEnable = True
                .ReceivedBytesThreshold = 1
                .NewLine = vbCr
                .ReadTimeout = 1000
                .Open()
            End With
            If SerialPort1.IsOpen Then
                MsgBox("Connected")
                lbl_serial.BackColor = Color.Green
            Else
                MsgBox("Error Connecting to " & lbl_serial.Text & "")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        SerialPort1.Close()
        lbl_serial.BackColor = Color.Red
        lbl_serial.Text = Trim(Mid(ComboBox1.Text, 1, 5))

    End Sub

    Private Sub btnSend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSend.Click
       
        With SerialPort1
            .Write("at" & vbCr)
            .Write("at+cmgf=1" & vbCr)
            .Write("at+cmgs=" & Chr(34) & txt_recepient.Text & Chr(34) & vbCr)
            .Write(txt_message.Text & Chr(26))
            Thread.Sleep(1000)
            MsgBox(rcvdata.ToString)
            rcvdata = ""
        End With

        With SerialPort1
            .Close()
        End With
        lbl_serial.BackColor = Color.Red

    End Sub
    Public Sub queryCOMPorts()
        Dim i As Integer = 0
        ComboBox1.Items.Clear()
        Dim ports() As String
        ports = Split(ModemsConnected(), "---")
            While i <> ports.Length
                ComboBox1.Items.Add(ports(i))
                i += 1
            End While

        
        
    End Sub

    Private Sub btnRefreshList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefreshList.Click

        queryCOMPorts()
    End Sub
End Class
