Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.XtraEditors
Imports DevExpress.Skins.XtraForm
Imports DevExpress.Skins
Imports DevExpress.Utils
Imports System.Drawing.Drawing2D
Imports DevExpress.Utils.Drawing
Imports DevExpress.Utils.Drawing.Helpers


Namespace DXSample
	Partial Public Class Main
		Inherits XtraForm
		Public Sub New()
			InitializeComponent()
		End Sub

		Protected Overrides Function CreateFormBorderPainter() As DevExpress.Skins.XtraForm.FormPainter
			Return New MyFormPainter(Me, DevExpress.LookAndFeel.UserLookAndFeel.Default.ActiveLookAndFeel)
		End Function
	End Class

	Public Class MyFormPainter
		Inherits FormPainter
		Private x As Integer = 0, delta As Integer = 8
		Private image_Renamed As Image
		Private timer As Timer

		Public Sub New(ByVal owner As Control, ByVal provider As ISkinProvider)
			MyBase.New(owner, provider)
			timer = New Timer()
			timer.Interval = 300
			AddHandler timer.Tick, AddressOf OnTick
			timer.Start()
		End Sub

		Private ReadOnly Property Image() As Image
			Get
				If image_Renamed Is Nothing Then
					image_Renamed = CreateImage()
				End If
				Return image_Renamed
			End Get
		End Property
		Private Function CreateImage() As Bitmap
			Dim temp As New Bitmap(TextBounds.Width, TextBounds.Height)
			Using gr As Graphics = Graphics.FromImage(temp)
				For i As Integer = 0 To temp.Width - 1 Step delta
					For j As Integer = 0 To temp.Height - 1 Step delta
						Dim rect As New Rectangle(i, j, delta, delta)
						Dim brush As New LinearGradientBrush(rect, Color.White, Color.Black, LinearGradientMode.BackwardDiagonal)
						Dim borderBrush As New LinearGradientBrush(rect, Color.DarkGray, Color.White, LinearGradientMode.BackwardDiagonal)
						Dim pen As New Pen(borderBrush)
						gr.FillRectangle(brush, rect)
						gr.DrawRectangle(pen, rect)
					Next j
				Next i
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
			If Text Is Nothing OrElse Text.Length = 0 OrElse TextBounds.IsEmpty Then
				Return
			End If
			Dim appearance As New AppearanceObject(GetDefaultAppearance())
			cache.Graphics.DrawImage(Image, TextBounds)
			Dim textRect As New Rectangle(TextBounds.X + x, TextBounds.Y, TextBounds.Width - x, TextBounds.Height)
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
