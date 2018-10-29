Imports Bio.Web.Blast
Imports Szunyi.BLAST.Enums
Imports org.mariuszgromada.math.mxparser
Imports System.Text.RegularExpressions

Public Class MathEvalution

    Private clonedAndFilteredBlastSearchRecords As List(Of BlastSearchRecord)
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

        Me.clonedAndFilteredBlastSearchRecords = clonedAndFilteredBlastSearchRecords
        extHsps = Szunyi.BLAST.BlastManipulation.Hsp.All(clonedAndFilteredBlastSearchRecords)
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

        Dim s = Split("Load,Save,Ok,Cancel,Test", ",")
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
        Dim Res As New List(Of Double)
        Select Case sender.Text
            Case "Load"
                Dim File = Szunyi.IO.Pick_Up.File(Szunyi.IO.File_Extensions.Filter)
                If IsNothing(File) = False Then
                    RichTextBox1.Text = Szunyi.IO.Import.Text.Full(File)
                End If
            Case "Save"
                Dim File = Szunyi.IO.Export.File_To_Save(Szunyi.IO.File_Extensions.Filter)
                If IsNothing(File) = False Then
                    Szunyi.IO.Export.Text(RichTextBox1.Text, File)
                End If
            Case "Cancel"
                Me.DialogResult = DialogResult.Cancel
                Me.Close()
            Case "OK"
                Me.DialogResult = DialogResult.OK
                Me.Close()
            Case "Test"
                Dim tmp = RichTextBox1.Text.Replace("(", "( ").Replace(")", ") ")
                Dim s = Split(tmp, " ")
                For Each M In Get_Matches_Aggregates()
                    Dim kj444 As Int16 = 54
                Next
                Dim x As New Expression(Szunyi.Common.Text.General.GetText(s, " "))
                Dim arguments = Szunyi.Math.mxparser.Get_Blast_Arguments(s)
                For Each arg In arguments
                    arg.Value.Value = Szunyi.BLAST.extHSP.Get_Values_By_Prop_Name(extHsps, arg.Key)
                    x.addArguments(arg.Value.Arg)
                Next


                Dim t = x.calculate

                For i1 = 0 To arguments.First.Value.Value.Count - 1
                    For Each arg In arguments
                        x.setArgumentValue(arg.Key, arg.Value.Value(i1))
                    Next
                    Res.Add(x.calculate)
                Next

                Dim kj As Int16 = 54

        End Select

    End Sub
    Sub Aggregates(sender As ToolStripButton, e As EventArgs)
        SetText(sender.Text)
        RichTextBox1.Update()
    End Sub
    Sub Numeric(sender As ToolStripButton, e As EventArgs)
        SetText(sender.Text)

    End Sub
    Sub Math_Op(sender As ToolStripButton, e As EventArgs)
        Dim x As Szunyi.Math.LogicalOperator = sender.Tag

        RichTextBox1.Text = RichTextBox1.Text & x.Symbol

    End Sub
    Sub Agg_Op(sender As ToolStripButton, e As EventArgs)
        SetText(sender.Text & "[]")
        RichTextBox1.SelectionStart = RichTextBox1.Text.Length - 1
    End Sub
    Sub Log_Op(sender As ToolStripButton, e As EventArgs)
        RichTextBox1.Text = "(" & RichTextBox1.Text & ") " & sender.Text & " ("

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
        For Each e In Szunyi.Common.RegExp.Get_Matches(RichTextBox1.Text, "Average\[[A-Z\s_]{1,45}\]")
            Yield e
        Next
        For Each e In Szunyi.Common.RegExp.Get_Matches(RichTextBox1.Text, "Average\[[A-Z\s_]{1,45}\]")
            Yield e
        Next
        For Each e In Szunyi.Common.RegExp.Get_Matches(RichTextBox1.Text, "Average\[[A-Z\s_]{1,45}\]")
            Yield e
        Next

    End Function
End Class