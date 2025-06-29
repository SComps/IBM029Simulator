﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        components = New ComponentModel.Container()
        txtCard = New TabAwareTextBox()
        lblStatus = New Label()
        ListBox1 = New ListBox()
        sysTimer = New Timer(components)
        capslock = New CheckBox()
        MenuStrip1 = New MenuStrip()
        DeckToolStripMenuItem = New ToolStripMenuItem()
        btnNew = New ToolStripMenuItem()
        btnOpen = New ToolStripMenuItem()
        ToolStripMenuItem1 = New ToolStripSeparator()
        btnSave = New ToolStripMenuItem()
        btnSaveAs = New ToolStripMenuItem()
        ToolStripMenuItem2 = New ToolStripSeparator()
        ExitToolStripMenuItem = New ToolStripMenuItem()
        DevicesToolStripMenuItem = New ToolStripMenuItem()
        ReadersToolStripMenuItem = New ToolStripMenuItem()
        PunchesToolStripMenuItem = New ToolStripMenuItem()
        AboutToolStripMenuItem = New ToolStripMenuItem()
        ToolStrip1 = New ToolStrip()
        btnFirst = New ToolStripButton()
        btnPrev = New ToolStripButton()
        ToolStripSeparator1 = New ToolStripSeparator()
        btnNext = New ToolStripButton()
        btnLast = New ToolStripButton()
        ToolStripSeparator2 = New ToolStripSeparator()
        btnInsert = New ToolStripButton()
        btnDelete = New ToolStripButton()
        btnDup = New ToolStripButton()
        posDisplay = New ToolStripLabel()
        submitButton = New ToolStripButton()
        deviceSelect = New ToolStripComboBox()
        FakeButton = New Button()
        MenuStrip1.SuspendLayout()
        ToolStrip1.SuspendLayout()
        SuspendLayout()
        ' 
        ' txtCard
        ' 
        txtCard.AcceptsTab = True
        txtCard.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        txtCard.Font = New Font("Consolas", 12F)
        txtCard.Location = New Point(12, 512)
        txtCard.MaxLength = 80
        txtCard.Multiline = True
        txtCard.Name = "txtCard"
        txtCard.Size = New Size(810, 26)
        txtCard.TabIndex = 0
        ' 
        ' lblStatus
        ' 
        lblStatus.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        lblStatus.Location = New Point(12, 545)
        lblStatus.Name = "lblStatus"
        lblStatus.Size = New Size(960, 23)
        lblStatus.TabIndex = 1
        lblStatus.Text = "Card 1 / 1"
        ' 
        ' ListBox1
        ' 
        ListBox1.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        ListBox1.Font = New Font("OCR A Extended", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        ListBox1.FormattingEnabled = True
        ListBox1.Location = New Point(12, 71)
        ListBox1.Name = "ListBox1"
        ListBox1.Size = New Size(808, 429)
        ListBox1.TabIndex = 11
        ' 
        ' sysTimer
        ' 
        ' 
        ' capslock
        ' 
        capslock.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        capslock.AutoSize = True
        capslock.Checked = True
        capslock.CheckState = CheckState.Checked
        capslock.Location = New Point(737, 545)
        capslock.Name = "capslock"
        capslock.Size = New Size(83, 19)
        capslock.TabIndex = 13
        capslock.Text = "CAPS Lock"
        capslock.UseVisualStyleBackColor = True
        ' 
        ' MenuStrip1
        ' 
        MenuStrip1.Items.AddRange(New ToolStripItem() {DeckToolStripMenuItem, DevicesToolStripMenuItem, AboutToolStripMenuItem})
        MenuStrip1.Location = New Point(0, 0)
        MenuStrip1.Name = "MenuStrip1"
        MenuStrip1.Size = New Size(836, 24)
        MenuStrip1.TabIndex = 14
        MenuStrip1.Text = "MenuStrip1"
        ' 
        ' DeckToolStripMenuItem
        ' 
        DeckToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {btnNew, btnOpen, ToolStripMenuItem1, btnSave, btnSaveAs, ToolStripMenuItem2, ExitToolStripMenuItem})
        DeckToolStripMenuItem.Name = "DeckToolStripMenuItem"
        DeckToolStripMenuItem.Size = New Size(45, 20)
        DeckToolStripMenuItem.Text = "Deck"
        ' 
        ' btnNew
        ' 
        btnNew.Name = "btnNew"
        btnNew.Size = New Size(123, 22)
        btnNew.Text = "New"
        ' 
        ' btnOpen
        ' 
        btnOpen.Name = "btnOpen"
        btnOpen.Size = New Size(123, 22)
        btnOpen.Text = "Open"
        ' 
        ' ToolStripMenuItem1
        ' 
        ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        ToolStripMenuItem1.Size = New Size(120, 6)
        ' 
        ' btnSave
        ' 
        btnSave.Name = "btnSave"
        btnSave.Size = New Size(123, 22)
        btnSave.Text = "Save"
        ' 
        ' btnSaveAs
        ' 
        btnSaveAs.Name = "btnSaveAs"
        btnSaveAs.Size = New Size(123, 22)
        btnSaveAs.Text = "Save As..."
        ' 
        ' ToolStripMenuItem2
        ' 
        ToolStripMenuItem2.Name = "ToolStripMenuItem2"
        ToolStripMenuItem2.Size = New Size(120, 6)
        ' 
        ' ExitToolStripMenuItem
        ' 
        ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        ExitToolStripMenuItem.Size = New Size(123, 22)
        ExitToolStripMenuItem.Text = "Exit"
        ' 
        ' DevicesToolStripMenuItem
        ' 
        DevicesToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {ReadersToolStripMenuItem, PunchesToolStripMenuItem})
        DevicesToolStripMenuItem.Name = "DevicesToolStripMenuItem"
        DevicesToolStripMenuItem.Size = New Size(59, 20)
        DevicesToolStripMenuItem.Text = "Devices"
        ' 
        ' ReadersToolStripMenuItem
        ' 
        ReadersToolStripMenuItem.Name = "ReadersToolStripMenuItem"
        ReadersToolStripMenuItem.Size = New Size(180, 22)
        ReadersToolStripMenuItem.Text = "Readers"
        ' 
        ' PunchesToolStripMenuItem
        ' 
        PunchesToolStripMenuItem.Name = "PunchesToolStripMenuItem"
        PunchesToolStripMenuItem.Size = New Size(180, 22)
        PunchesToolStripMenuItem.Text = "Punches"
        ' 
        ' AboutToolStripMenuItem
        ' 
        AboutToolStripMenuItem.Name = "AboutToolStripMenuItem"
        AboutToolStripMenuItem.Size = New Size(52, 20)
        AboutToolStripMenuItem.Text = "About"
        ' 
        ' ToolStrip1
        ' 
        ToolStrip1.Items.AddRange(New ToolStripItem() {btnFirst, btnPrev, ToolStripSeparator1, btnNext, btnLast, ToolStripSeparator2, btnInsert, btnDelete, btnDup, posDisplay, submitButton, deviceSelect})
        ToolStrip1.Location = New Point(0, 24)
        ToolStrip1.Name = "ToolStrip1"
        ToolStrip1.Size = New Size(836, 38)
        ToolStrip1.TabIndex = 15
        ToolStrip1.Text = "ToolStrip1"
        ' 
        ' btnFirst
        ' 
        btnFirst.Image = My.Resources.Resources.GoToFirst
        btnFirst.ImageTransparentColor = Color.Magenta
        btnFirst.Name = "btnFirst"
        btnFirst.Size = New Size(33, 35)
        btnFirst.Text = "First"
        btnFirst.TextImageRelation = TextImageRelation.ImageAboveText
        ' 
        ' btnPrev
        ' 
        btnPrev.Image = My.Resources.Resources.GoToPrevious
        btnPrev.ImageTransparentColor = Color.Magenta
        btnPrev.Name = "btnPrev"
        btnPrev.Size = New Size(34, 35)
        btnPrev.Text = "Prev"
        btnPrev.TextImageRelation = TextImageRelation.ImageAboveText
        ' 
        ' ToolStripSeparator1
        ' 
        ToolStripSeparator1.Name = "ToolStripSeparator1"
        ToolStripSeparator1.Size = New Size(6, 38)
        ' 
        ' btnNext
        ' 
        btnNext.Image = My.Resources.Resources.GoToNext
        btnNext.ImageTransparentColor = Color.Magenta
        btnNext.Name = "btnNext"
        btnNext.Size = New Size(36, 35)
        btnNext.Text = "Next"
        btnNext.TextImageRelation = TextImageRelation.ImageAboveText
        ' 
        ' btnLast
        ' 
        btnLast.Image = My.Resources.Resources.GoToLast
        btnLast.ImageTransparentColor = Color.Magenta
        btnLast.Name = "btnLast"
        btnLast.Size = New Size(32, 35)
        btnLast.Text = "Last"
        btnLast.TextImageRelation = TextImageRelation.ImageAboveText
        ' 
        ' ToolStripSeparator2
        ' 
        ToolStripSeparator2.Name = "ToolStripSeparator2"
        ToolStripSeparator2.Size = New Size(6, 38)
        ' 
        ' btnInsert
        ' 
        btnInsert.Image = My.Resources.Resources.InsertMark
        btnInsert.ImageTransparentColor = Color.Magenta
        btnInsert.Name = "btnInsert"
        btnInsert.Size = New Size(40, 35)
        btnInsert.Text = "Insert"
        btnInsert.TextImageRelation = TextImageRelation.ImageAboveText
        ' 
        ' btnDelete
        ' 
        btnDelete.Image = My.Resources.Resources.Delete
        btnDelete.ImageTransparentColor = Color.Magenta
        btnDelete.Name = "btnDelete"
        btnDelete.Size = New Size(44, 35)
        btnDelete.Text = "Delete"
        btnDelete.TextImageRelation = TextImageRelation.ImageAboveText
        ' 
        ' btnDup
        ' 
        btnDup.Image = My.Resources.Resources.Duplicate
        btnDup.ImageTransparentColor = Color.Magenta
        btnDup.Name = "btnDup"
        btnDup.Size = New Size(33, 35)
        btnDup.Text = "Dup"
        btnDup.TextImageRelation = TextImageRelation.ImageAboveText
        ' 
        ' posDisplay
        ' 
        posDisplay.Alignment = ToolStripItemAlignment.Right
        posDisplay.AutoSize = False
        posDisplay.BackColor = Color.BlanchedAlmond
        posDisplay.BackgroundImageLayout = ImageLayout.Stretch
        posDisplay.DisplayStyle = ToolStripItemDisplayStyle.Text
        posDisplay.Font = New Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        posDisplay.ForeColor = Color.Maroon
        posDisplay.ImageScaling = ToolStripItemImageScaling.None
        posDisplay.ImageTransparentColor = Color.Black
        posDisplay.Name = "posDisplay"
        posDisplay.Size = New Size(105, 35)
        posDisplay.Text = "00"
        posDisplay.TextImageRelation = TextImageRelation.Overlay
        ' 
        ' submitButton
        ' 
        submitButton.Alignment = ToolStripItemAlignment.Right
        submitButton.Image = My.Resources.Resources.Send
        submitButton.ImageTransparentColor = Color.Magenta
        submitButton.Name = "submitButton"
        submitButton.Size = New Size(49, 35)
        submitButton.Text = "Submit"
        submitButton.TextImageRelation = TextImageRelation.ImageAboveText
        ' 
        ' deviceSelect
        ' 
        deviceSelect.Alignment = ToolStripItemAlignment.Right
        deviceSelect.Name = "deviceSelect"
        deviceSelect.Size = New Size(141, 38)
        deviceSelect.Text = "No device selected"
        ' 
        ' FakeButton
        ' 
        FakeButton.Location = New Point(20, 79)
        FakeButton.Name = "FakeButton"
        FakeButton.Size = New Size(75, 23)
        FakeButton.TabIndex = 16
        FakeButton.Text = "Button1"
        FakeButton.UseVisualStyleBackColor = True
        ' 
        ' Form1
        ' 
        AcceptButton = FakeButton
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(836, 569)
        Controls.Add(ToolStrip1)
        Controls.Add(capslock)
        Controls.Add(ListBox1)
        Controls.Add(txtCard)
        Controls.Add(lblStatus)
        Controls.Add(MenuStrip1)
        Controls.Add(FakeButton)
        MainMenuStrip = MenuStrip1
        Name = "Form1"
        StartPosition = FormStartPosition.CenterScreen
        Text = "IBM 029 Simulator"
        MenuStrip1.ResumeLayout(False)
        MenuStrip1.PerformLayout()
        ToolStrip1.ResumeLayout(False)
        ToolStrip1.PerformLayout()
        ResumeLayout(False)
        PerformLayout()
    End Sub
    Friend WithEvents lblStatus As Label
    Friend WithEvents ListBox1 As ListBox
    Friend WithEvents sysTimer As Timer

    Friend WithEvents txtCard As TabAwareTextBox
    Friend WithEvents capslock As CheckBox
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents DeckToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents btnNew As ToolStripMenuItem
    Friend WithEvents btnOpen As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem1 As ToolStripSeparator
    Friend WithEvents btnSave As ToolStripMenuItem
    Friend WithEvents btnSaveAs As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem2 As ToolStripSeparator
    Friend WithEvents ExitToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents DevicesToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ReadersToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents PunchesToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents AboutToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStrip1 As ToolStrip
    Friend WithEvents btnFirst As ToolStripButton
    Friend WithEvents btnPrev As ToolStripButton
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents btnNext As ToolStripButton
    Friend WithEvents btnLast As ToolStripButton
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents btnInsert As ToolStripButton
    Friend WithEvents btnDelete As ToolStripButton
    Friend WithEvents btnDup As ToolStripButton
    Friend WithEvents posDisplay As ToolStripLabel
    Friend WithEvents submitButton As ToolStripButton
    Friend WithEvents deviceSelect As ToolStripComboBox
    Friend WithEvents FakeButton As Button


End Class
