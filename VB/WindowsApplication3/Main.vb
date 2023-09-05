Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.XtraEditors
Imports DevExpress.Skins.XtraForm
Imports DevExpress.Skins
Imports DevExpress.Utils
Imports System.Drawing.Drawing2D
Imports DevExpress.Utils.Drawing
Imports DevExpress.Utils.Drawing.Helpers

Namespace DXSample

    Public Partial Class Main
        Inherits XtraForm

        Public Sub New()
            InitializeComponent()
        End Sub

        Protected Overrides Function CreateFormBorderPainter() As FormPainter
            Return New MyFormPainter(Me, DevExpress.LookAndFeel.UserLookAndFeel.Default.ActiveLookAndFeel)
        End Function
    End Class

    Public Class MyFormPainter
        Inherits FormPainter

        Private x As Integer = 0, delta As Integer = 8

        Private imageField As Image

        Private timer As Timer

        Public Sub New(ByVal owner As Control, ByVal provider As ISkinProvider)
            MyBase.New(owner, provider)
            timer = New Timer()
            timer.Interval = 300
            AddHandler timer.Tick, AddressOf OnTick
            timer.Start()
        End Sub

        Private ReadOnly Property Image As Image
            Get
                If imageField Is Nothing Then imageField = CreateImage()
                Return imageField
            End Get
        End Property

        Private Function CreateImage() As Bitmap
            Dim temp As Bitmap = New Bitmap(TextBounds.Width, TextBounds.Height)
            Using gr As Graphics = Graphics.FromImage(temp)
                Dim i As Integer = 0
                While i < temp.Width
                    Dim j As Integer = 0
                    While j < temp.Height
                        Dim rect As Rectangle = New Rectangle(i, j, delta, delta)
                        Dim brush As LinearGradientBrush = New LinearGradientBrush(rect, Color.White, Color.Black, LinearGradientMode.BackwardDiagonal)
                        Dim borderBrush As LinearGradientBrush = New LinearGradientBrush(rect, Color.DarkGray, Color.White, LinearGradientMode.BackwardDiagonal)
                        Dim pen As Pen = New Pen(borderBrush)
                        gr.FillRectangle(brush, rect)
                        gr.DrawRectangle(pen, rect)
                        j += delta
                    End While

                    i += delta
                End While
            End Using

            Return temp
        End Function

        Private Sub OnTick(ByVal sender As Object, ByVal e As EventArgs)
            If x <= TextBounds.Right - delta Then
                x += delta
            Else
                x = 0
            End If

            UpdateText()
        End Sub

        Private Sub UpdateText()
            DrawFrameNC(New Message())
        End Sub

        Protected Overrides Sub DrawText(ByVal cache As DevExpress.Utils.Drawing.GraphicsCache)
            If Equals(Text, Nothing) OrElse Text.Length = 0 OrElse TextBounds.IsEmpty Then Return
            Dim appearance As AppearanceObject = New AppearanceObject(GetDefaultAppearance())
            cache.Graphics.DrawImage(Image, TextBounds)
            Dim textRect As Rectangle = New Rectangle(TextBounds.X + x, TextBounds.Y, TextBounds.Width - x, TextBounds.Height)
            DrawTextShadow(cache, appearance, textRect)
            cache.Graphics.DrawString(Text, appearance.Font, Brushes.WhiteSmoke, textRect, New StringFormat(StringFormatFlags.NoWrap))
        End Sub

        Public Overrides Sub Dispose()
            timer.Stop()
            RemoveHandler timer.Tick, AddressOf OnTick
            timer.Dispose()
            MyBase.Dispose()
        End Sub
    End Class
End Namespace
