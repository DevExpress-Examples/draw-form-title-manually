<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128620774/13.1.4%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E3327)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->

# How to draw a form title manually

You can draw an XtraForm's title if title bar skinning is enabled (for example, the [DevExpress.Skins.SkinManager.EnableFormSkins](https://docs.devexpress.com/WindowsForms/DevExpress.Skins.SkinManager.EnableFormSkins) method is called at application startup). This example shows how to create a custom `FormPainter` and override its methods to display an animated running line within the form's caption.


## Files to Review

* [Main.cs](./CS/WindowsApplication3/Main.cs) (VB: [Main.vb](./VB/WindowsApplication3/Main.vb))
* [Program.cs](./CS/WindowsApplication3/Program.cs) (VB: [Program.vb](./VB/WindowsApplication3/Program.vb))
