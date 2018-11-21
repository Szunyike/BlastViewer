Imports Bio.Web.Blast
Imports Szunyi.BLAST.Enums
Imports org.mariuszgromada.math.mxparser
Imports System.Text.RegularExpressions
Imports Szunyi.Math

Public Class MathEvalution

    Public bRecords As New List(Of BlastSearchRecord)
    Public cOwnRecords As New List(Of Szunyi.BLAST.OwnBlastRecord)
    Public Records As List(Of Szunyi.BLAST.OwnBlastRecord)
    Private extHsps As List(Of Szunyi.BLAST.extHSP)
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub



    Public Sub New(clonedAndFilteredBlastSearchRecords As List(Of BlastSearchRecord))
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        Me.bRecords = clonedAndFilteredBlastSearchRecords
        extHsps = Szunyi.BLAST.BlastManipulation.Hsp.All(clonedAndFilteredBlastSearchRecords)
    End Sub
    Public Sub New(clonedAndFilteredBlastSearchRecords As List(Of Szunyi.BLAST.OwnBlastRecord))
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        extHsps = Szunyi.BLAST.BlastManipulation.Hsp.All(clonedAndFilteredBlastSearchRecords)
        cOwnRecords = clonedAndFilteredBlastSearchRecords
        '  extHsps = Szunyi.BLAST.BlastManipulation.Hsp.All(clonedAndFilteredBlastSearchRecords)
    End Sub

    Private Sub MathEvalution_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim x As New Szunyi.Math.Operators
        For Each Item In x.MathematicalOperators
            Dim t As New ToolStripButton
            t.Text = Item.Name
            t.ToolTipText = Item.Description
            t.Tag = Item
            AddHandler t.Click, AddressOf Math_Op
            ToolStrip1.Items.Add(t)
        Next
        For Each Item In x.LogicalOperators
            Dim t As New ToolStripButton
            t.Text = Item.Name
            t.ToolTipText = Item.Description
            t.Tag = Item
            AddHandler t.Click, AddressOf Log_Op
            ToolStrip2.Items.Add(t)
        Next

        For Each Item In x.AggregateOperators
            Dim t As New ToolStripButton
            t.Text = Item.Name
            t.ToolTipText = Item.Description
            t.Tag = Item
            AddHandler t.Click, AddressOf Agg_Op
            ToolStrip3.Items.Add(t)
        Next
        For Each Item In x.BinaryOperators
            Dim t As New ToolStripButton
            t.Text = Item.Name
            t.ToolTipText = Item.Description
            t.Tag = Item
            AddHandler t.Click, AddressOf Bin_Op
            ToolStrip4.Items.Add(t)
        Next

        Dim s = Split("Load,Save,Maintain True,Maintain False,Cancel,Test", ",")
        For Each s1 In s
            Dim t As New ToolStripButton
            t.Text = s1
            AddHandler t.Click, AddressOf FileDialog
            ToolStrip7.Items.Add(t)
        Next
        Dim Items = Szunyi.Common.Util_Helpers.Get_All_Enum_Names(Of Hsp_Hit_Record_Numeric)(Hsp_Hit_Record_Numeric.Hsp_Score)
        For Each s1 In Items
            Dim t As New ToolStripButton
            t.Text = s1
            AddHandler t.Click, AddressOf Numeric
            ToolStrip5.Items.Add(t)
        Next
        Items = Szunyi.Common.Util_Helpers.Get_All_Enum_Names(Of Hsp_Hit_Aggregate)(Hsp_Hit_Aggregate.Record_Hit_Length)
        For Each s1 In Items
            Dim t As New ToolStripButton
            t.Text = s1
            AddHandler t.Click, AddressOf Aggregates
            ToolStrip6.Items.Add(t)
        Next
    End Sub


    Sub FileDialog(sender As ToolStripButton, e As EventArgs)

        Select Case sender.Text
            Case "Load"
                Dim File = Szunyi.IO.Pick_Up.File(Szunyi.IO.File_Extensions.Filter, "Select Filter File", New System.IO.DirectoryInfo(My.Settings.Filter))
                If IsNothing(File) = False Then
                    RichTextBox1.Text = Szunyi.IO.Import.Text.Full(File)
                End If
            Case "Save"
                Dim File = Szunyi.IO.Export.File_To_Save(Szunyi.IO.File_Extensions.Filter, New System.IO.DirectoryInfo(My.Settings.Filter))
                If IsNothing(File) = False Then
                    Szunyi.IO.Export.Text(RichTextBox1.Text, File)
                End If
            Case "Cancel"
                Me.DialogResult = DialogResult.Cancel
                Me.Close()
            Case "Maintain True"
                Dim Arguments = GetArguments()
                Dim Index As Integer = 0
                Dim Removed As Integer = 0
                For Each Item In Calculate(Arguments)
                    If Item = 0 Then
                        cOwnRecords(extHsps(Index).RecordID).Hits(extHsps(Index).HitID).Hsps(extHsps(Index).HSPID) = Nothing
                        Removed += 1
                    End If
                    Index += 1
                Next
                Me.cOwnRecords = Szunyi.BLAST.Filter.HSP.Clear(cOwnRecords)
                Dim kj = "True:" & Index - Removed & vbCrLf & "False:" & Removed
                MsgBox(kj)
                Me.DialogResult = DialogResult.OK
                Me.Close()
            Case "Maintain False"
                Dim Arguments = GetArguments(1000)
                Dim Index As Integer = 0
                For Each Item In Calculate(Arguments)
                    If Item = 1 Then
                        Me.cOwnRecords(extHsps(Index).RecordID).Hits(extHsps(Index).HitID).Hsps(extHsps(Index).HSPID) = Nothing
                    End If
                    Index += 1
                Next
                Me.cOwnRecords = Szunyi.BLAST.Filter.HSP.Clear(Me.cOwnRecords)
                Me.DialogResult = DialogResult.OK
                Me.Close()
            Case "Test"
                Dim Arguments = GetArguments(1000)
                Dim Res = Calculate(Arguments).ToList

                Dim kj = "True:" & (From x4 In Res Where x4 = 1).Count & vbCrLf & "False:" & (From x4 In Res Where x4 = 0).Count
                MsgBox(kj)
        End Select

    End Sub

    Private Iterator Function Calculate(arguments As Dictionary(Of String, Own_Argument)) As IEnumerable(Of Double)
        Dim tmp = RichTextBox1.Text.Replace("(", " ( ").Replace(")", " ) ")
        Dim s = Split(tmp, " ")
        Dim txt = Szunyi.Common.Text.General.GetText(s, " ").Trim()
        Dim x As New Expression(txt)
        For Each a In arguments
            x.addArguments(a.Value.Arg)
        Next
        For i1 = 0 To arguments.First.Value.Value.Count - 1
            For Each arg In arguments
                x.setArgumentValue(arg.Key, arg.Value.Value(i1))

            Next
            Yield x.calculate

        Next
    End Function

    Private Function GetArguments(Optional MaxNof = -1) As Dictionary(Of String, Szunyi.Math.Own_Argument)
        Dim tmp = RichTextBox1.Text.Replace("(", "( ").Replace(")", ") ")

        Dim Index As Integer = 0
        Dim Arguments As New Dictionary(Of String, Szunyi.Math.Own_Argument)

        For Each M In Get_Matches_Aggregates()
            Dim Name As String = "aa_" & Index
            tmp = tmp.Replace(M.Value, Name)
            Index += 1
            Dim a As New Szunyi.Math.Own_Argument(Name)
            a.Value = Szunyi.BLAST.extHSP.Get_Values_By_Aggregate_Name(extHsps, M.Value, MaxNof)
            Arguments.Add(Name, a)
        Next
        Dim s = Split(tmp, " ")

        Dim arguments2 = Szunyi.Math.mxparser.Get_Blast_Arguments(s)
        For Each arg In arguments2
            arg.Value.Value = Szunyi.BLAST.extHSP.Get_Values_By_Prop_Name(extHsps, arg.Key, MaxNof)
            Arguments.Add(arg.Key, arg.Value)
        Next
        Return Arguments

    End Function
    Sub Aggregates(sender As ToolStripButton, e As EventArgs)
        SetText(sender.Text)
        RichTextBox1.Update()
    End Sub
    Sub Numeric(sender As ToolStripButton, e As EventArgs)
        SetText(sender.Text)

    End Sub
    Sub Math_Op(sender As ToolStripButton, e As EventArgs)
        Dim x As Szunyi.Math.LogicalOperator = sender.Tag

        SetText(" " & x.Symbol & "  ")

    End Sub
    Sub Agg_Op(sender As ToolStripButton, e As EventArgs)
        SetText(sender.Text & "[]")
        RichTextBox1.SelectionStart = RichTextBox1.Text.Length - 1
    End Sub
    Sub Log_Op(sender As ToolStripButton, e As EventArgs)
        Dim t As LogicalOperator = sender.Tag
        RichTextBox1.Text = "( " & RichTextBox1.Text & " ) " & t.Symbol & " ( "

    End Sub
    Sub Bin_Op(sender As ToolStripButton, e As EventArgs)
        RichTextBox1.Text = RichTextBox1.Text & sender.Text
        RichTextBox1.Update()

    End Sub
    Private Sub SetText(txt As String)
        If RichTextBox1.TextLength > 0 Then
            Dim sg = RichTextBox1.Text.Substring(0, RichTextBox1.SelectionStart) & " " & txt & RichTextBox1.Text.Substring(RichTextBox1.SelectionStart)
            ' txt = " " & RichTextBox1.Text & " " & txt & " "
            txt = sg.Replace("  ", " ")
        End If

        RichTextBox1.Text = txt
        RichTextBox1.Update()
        RichTextBox1.SelectionStart = RichTextBox1.Text.Length
    End Sub

    Private Iterator Function Get_Matches_Aggregates() As IEnumerable(Of match)


        For Each e In Szunyi.Common.RegExp.Get_Matches(RichTextBox1.Text, "Average\[[A-Z\s_]{1,45}\]")
            Yield e
        Next
        For Each e In Szunyi.Common.RegExp.Get_Matches(RichTextBox1.Text, "Sum\[[A-Z\s_]{1,45}\]")
            Yield e
        Next
        For Each e In Szunyi.Common.RegExp.Get_Matches(RichTextBox1.Text, "Minimum value\[[A-Z\s_]{1,45}\]")
            Yield e
        Next
        For Each e In Szunyi.Common.RegExp.Get_Matches(RichTextBox1.Text, "Maximum value\[[A-Z\s_]{1,45}\]")
            Yield e
        Next

    End Function
End Class