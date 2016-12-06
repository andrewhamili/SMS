<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.SerialPort1 = New System.IO.Ports.SerialPort(Me.components)
        Me.btnConnect = New System.Windows.Forms.Button()
        Me.lbl_serial = New System.Windows.Forms.Label()
        Me.txt_recepient = New System.Windows.Forms.TextBox()
        Me.txt_message = New System.Windows.Forms.TextBox()
        Me.btnSend = New System.Windows.Forms.Button()
        Me.btnRefreshList = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'ComboBox1
        '
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Location = New System.Drawing.Point(41, 60)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(381, 21)
        Me.ComboBox1.TabIndex = 0
        '
        'SerialPort1
        '
        '
        'btnConnect
        '
        Me.btnConnect.Location = New System.Drawing.Point(44, 121)
        Me.btnConnect.Name = "btnConnect"
        Me.btnConnect.Size = New System.Drawing.Size(75, 23)
        Me.btnConnect.TabIndex = 1
        Me.btnConnect.Text = "Connect"
        Me.btnConnect.UseVisualStyleBackColor = True
        '
        'lbl_serial
        '
        Me.lbl_serial.AutoSize = True
        Me.lbl_serial.Location = New System.Drawing.Point(41, 88)
        Me.lbl_serial.Name = "lbl_serial"
        Me.lbl_serial.Size = New System.Drawing.Size(47, 13)
        Me.lbl_serial.TabIndex = 2
        Me.lbl_serial.Text = "lbl_serial"
        '
        'txt_recepient
        '
        Me.txt_recepient.Location = New System.Drawing.Point(41, 179)
        Me.txt_recepient.Name = "txt_recepient"
        Me.txt_recepient.Size = New System.Drawing.Size(244, 20)
        Me.txt_recepient.TabIndex = 3
        '
        'txt_message
        '
        Me.txt_message.Location = New System.Drawing.Point(41, 214)
        Me.txt_message.Name = "txt_message"
        Me.txt_message.Size = New System.Drawing.Size(244, 20)
        Me.txt_message.TabIndex = 4
        '
        'btnSend
        '
        Me.btnSend.Location = New System.Drawing.Point(41, 241)
        Me.btnSend.Name = "btnSend"
        Me.btnSend.Size = New System.Drawing.Size(75, 23)
        Me.btnSend.TabIndex = 5
        Me.btnSend.Text = "Button1"
        Me.btnSend.UseVisualStyleBackColor = True
        '
        'btnRefreshList
        '
        Me.btnRefreshList.Location = New System.Drawing.Point(429, 57)
        Me.btnRefreshList.Name = "btnRefreshList"
        Me.btnRefreshList.Size = New System.Drawing.Size(75, 23)
        Me.btnRefreshList.TabIndex = 6
        Me.btnRefreshList.Text = "Re-scan"
        Me.btnRefreshList.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(246, 121)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 7
        Me.Button1.Text = "Button1"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(529, 277)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.btnRefreshList)
        Me.Controls.Add(Me.btnSend)
        Me.Controls.Add(Me.txt_message)
        Me.Controls.Add(Me.txt_recepient)
        Me.Controls.Add(Me.lbl_serial)
        Me.Controls.Add(Me.btnConnect)
        Me.Controls.Add(Me.ComboBox1)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
    Friend WithEvents SerialPort1 As System.IO.Ports.SerialPort
    Friend WithEvents btnConnect As System.Windows.Forms.Button
    Friend WithEvents lbl_serial As System.Windows.Forms.Label
    Friend WithEvents txt_recepient As System.Windows.Forms.TextBox
    Friend WithEvents txt_message As System.Windows.Forms.TextBox
    Friend WithEvents btnSend As System.Windows.Forms.Button
    Friend WithEvents btnRefreshList As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button

End Class
