Imports System.Management
Imports System.Threading
Imports System.Text.RegularExpressions
Imports System.Runtime.InteropServices

Public Class Form1

    Public rcvdata As String = ""
    Public myPort As Array

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
       
        lbl_serial.BackColor = Color.Red
        queryCOMPorts()
    End Sub
    Private Sub SerialPort1_datareceived(ByVal sender As Object, ByVal e As System.IO.Ports.SerialDataReceivedEventArgs) Handles SerialPort1.DataReceived
        rcvdata = ""
        Dim datain As String = ""
        Dim numbytes As Integer = SerialPort1.BytesToRead
        For i As Integer = 1 To numbytes
            datain &= Chr(SerialPort1.ReadChar)
        Next
        test(datain)
        Console.WriteLine(rcvdata.ToString)
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
        If SerialPort1.IsOpen Then
            Dim page1, page2 As Integer
            Dim fulltext As String
            Dim splitted() As String
            If txt_message.Text.Length > 160 Then
                fulltext = txt_message.Text
                fulltext = fulltext.Insert(0, "(1/2)" & vbCrLf)
                fulltext = fulltext.Insert(160, "@@(2/2)" & vbCrLf)
                splitted = Split(fulltext, "@@", , CompareMethod.Text)
            End If
            
            Dim i As Integer = 0
            While i <> 2
                With SerialPort1
                    .Write("AT" & vbCr)
                    .Write("AT+CMGF=1" & vbCr)
                    Console.WriteLine(splitted(i).Length)
                    .Write("AT+CMGS=" & Chr(34) & txt_recepient.Text & Chr(34) & vbCr & Chr(26))
                    .Write(splitted(i) & Chr(26))
                    Thread.Sleep(2000)
                    rcvdata = ""
                End With
                Thread.Sleep(1000)
                i += 1
            End While

        End If

    End Sub
    Public Sub queryCOMPorts()
        ComboBox1.Items.Clear()
        Dim ports() As String
        ports = Split(ModemsConnected, "***")
        For i As Integer = 0 To ports.Length - 2
            ComboBox1.Items.Add(ports(i))
        Next
    End Sub

    Private Sub btnRefreshList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefreshList.Click

        queryCOMPorts()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        With SerialPort1
            .Write("AT" & vbCrLf)
            .Write("AT+CMGF=1" & vbCrLf)
            .Write("AT+CMGL=""All""" & vbCrLf)
            Console.WriteLine("AT+CMGL=""All""" & vbCrLf)
            Thread.Sleep(1000)
            'MsgBox(rcvdata.ToString)
            read()
            rcvdata = ""
        End With
    End Sub
    Public Sub read()
        Try
            Dim LineOfText As String
            Dim i As Integer
            Dim arytextfile() As String
            LineOfText = rcvdata.ToString
            arytextfile = Split(LineOfText, "+CMGL", , CompareMethod.Text)
            Dim counter As Integer
            Dim rowcounter As Integer
            For i = 1 To UBound(arytextfile)
                Dim input As String = arytextfile(i)
                Dim result() As String
                Dim pattern As String = "(:)|(,"")|("","")"
                result = Regex.Split(input, pattern)
                Dim lvi As New ListViewItem
                Dim concat() As String
                
                Dim index As New List(Of String)
                Dim status As New List(Of String)
                Dim number As New List(Of String)
                Dim dateandtime As New List(Of String)
                Dim msg As New List(Of String)

                index.AddRange(New String() {result(2)})
                status.AddRange(New String() {result(4)})
                Dim rawnumber, position As String
                rawnumber = result(6)
                position = rawnumber.Length - 2
                rawnumber = rawnumber.Remove(position, 2)
                number.Add(rawnumber)
                concat = New String() {result(8) & result(9) & result(10) & result(11) & result(12).Substring(0, 2)}

                dateandtime.AddRange(concat)
                Dim lineoftexts As String
                Dim arytextfiles() As String
                LineOfText = arytextfile(i)
                arytextfiles = Split(LineOfText, "+32", , CompareMethod.Text)

                msg.Add(arytextfiles(1))
                Dim indexarray As Array = index.ToArray
                Dim statusarray As Array = status.ToArray
                Dim numberarray As Array = number.ToArray
                Dim datetimearray As Array = dateandtime.ToArray
                Dim msgarray As Array = msg.ToArray

                With DataGridView1
                    .Rows.Add()
                    .Rows(rowcounter).Cells(0).Value = indexarray(0)
                    .Rows(rowcounter).Cells(1).Value = statusarray(0)
                    .Rows(rowcounter).Cells(2).Value = numberarray(0)
                    .Rows(rowcounter).Cells(3).Value = datetimearray(0)
                    .Rows(rowcounter).Cells(4).Value = msgarray(0)
                    rowcounter += 1
                End With
            Next
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try



        'With ListView1.Items.Add("null")
        '    .SubItems.AddRange(New String() {result(2)})
        '    .SubItems.AddRange(New String() {result(4)})
        '    Dim mystring, position As String
        '    mystring = result(6)
        '    position = mystring.Length - 2
        '    mystring = mystring.Remove(position, 2)
        '    .SubItems.Add(mystring)
        '    concat = New String() {result(8) & result(9) & result(10), result(11), result(12).Substring(0, 2)}
        '    .SubItems.AddRange(concat)
        '    Dim lineoftexts As String
        '    Dim arytextfiles() As String
        '    lineoftexts = arytextfile(i)
        '    arytextfiles = Split(lineoftexts, "+32", , CompareMethod.Text)
        '    .SubItems.Add(arytextfiles(1))
        '    MsgBox(arytextfiles(1))
        'End With
        'With DataGridView1
        '    .Rows.Add()
        '    .Rows(rowcounter).Cells(0).Value = (New String() {result(2)}).ToString
        '    rowcounter += 1
        'End With

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If SerialPort1.IsOpen Then
            With SerialPort1
                '.Write("AT+CMEE=2" & vbCrLf)
                .Write(TextBox1.Text & vbCrLf)
            End With
        End If

    End Sub
    Public Class Win32
        <DllImport("kernel32.dll")> Public Shared Function AllocConsole() As Boolean

        End Function
        <DllImport("kernel32.dll")> Public Shared Function FreeConsole() As Boolean

        End Function

    End Class

    Private Sub NotifyIcon1_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles NotifyIcon1.MouseDoubleClick
        If Win32.AllocConsole = False Then
            Win32.AllocConsole()
        Else
            Win32.FreeConsole()
        End If
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Win32.AllocConsole()
        Thread.Sleep(1000)
        Dim a As Integer = 165 / 160
        Console.WriteLine(a)
    End Sub
End Class
