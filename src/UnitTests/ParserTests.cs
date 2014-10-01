using System;
using System.IO;
using System.Linq;
using AutoJIT;
using AutoJIT.Compiler;
using Microsoft.CodeAnalysis;
using NUnit.Framework;

namespace UnitTests
{
    public class ParserTests
    {
        private readonly ICompiler _compiler;

        public ParserTests() {
            var standardAutoJITContainer = new CompilerBootStrapper();
            _compiler = standardAutoJITContainer.GetInstance<ICompiler>();
        }

        [TestCase( "APIComConstants.au3" )]
        [TestCase( "APIConstants.au3" )]
        [TestCase( "APIDiagConstants.au3" )]
        [TestCase( "APIDlgConstants.au3" )]
        [TestCase( "APIErrorsConstants.au3" )]
        [TestCase( "APIFilesConstants.au3" )]
        [TestCase( "APIGdiConstants.au3" )]
        [TestCase( "APILocaleConstants.au3" )]
        [TestCase( "APIMiscConstants.au3" )]
        [TestCase( "APIProcConstants.au3" )]
        [TestCase( "APIRegConstants.au3" )]
        [TestCase( "APIResConstants.au3" )]
        [TestCase( "APIShellExConstants.au3" )
        ]
        [TestCase( "APIShPathConstants.au3" )]
        [TestCase( "APISysConstants.au3" )]
        [TestCase( "APIThemeConstants.au3" )]
        [TestCase( "AutoItConstants.au3" )]
        [TestCase( "AVIConstants.au3" )]
        [TestCase( "BorderConstants.au3" )]
        [TestCase( "ButtonConstants.au3" )]
        [TestCase( "Clipboard.au3" )]
        [TestCase( "Color.au3" )]
        [TestCase( "ColorConstants.au3" )]
        [TestCase( "ComboConstants.au3" )]
        [TestCase( "Constants.au3" )]
        [TestCase( "Crypt.au3" )]
        [TestCase( "DateTimeConstants.au3" )]
        [TestCase( "DirConstants.au3" )]
        [TestCase( "EditConstants.au3" )]
        [TestCase( "EventLog.au3" )]
        [TestCase( "ExcelConstants.au3" )]
        [TestCase( "File.au3" )]
        [TestCase( "FileConstants.au3" )]
        [TestCase( "FontConstants.au3" )]
        [TestCase( "FrameConstants.au3" )]
        [TestCase( "FTPEx.au3" )]
        [TestCase( "GDIPlus.au3" )]
        [TestCase( "GDIPlusConstants.au3" )]
        [TestCase( "GuiAVI.au3" )]
        [TestCase( "GuiButton.au3" )]
        [TestCase( "GuiComboBox.au3" )]
        [TestCase( "GuiComboBoxEx.au3" )]
        [TestCase( "GUIConstants.au3" )]
        [TestCase( "GUIConstantsEx.au3" )]
        [TestCase( "GuiDateTimePicker.au3" )]
        [TestCase( "GuiEdit.au3" )]
        [TestCase( "GuiHeader.au3" )]
        [TestCase( "GuiImageList.au3" )]
        [TestCase( "GuiIPAddress.au3" )]
        [TestCase( "GuiListBox.au3" )]
        [TestCase( "GuiListView.au3" )]
        [TestCase( "GuiMenu.au3" )]
        [TestCase( "GuiMonthCal.au3" )]
        [TestCase( "GuiReBar.au3" )]
        [TestCase( "GuiRichEdit.au3" )]
        [TestCase( "GuiScrollBars.au3" )]
        [TestCase( "GuiSlider.au3" )]
        [TestCase( "GuiStatusBar.au3" )]
        [TestCase( "GuiTab.au3" )]
        [TestCase( "GuiToolbar.au3" )]
        [TestCase( "GuiToolTip.au3" )]
        [TestCase( "GuiTreeView.au3" )]
        [TestCase( "HeaderConstants.au3" )]
        [TestCase( "IE.au3" )]
        [TestCase( "ImageListConstants.au3" )]
        [TestCase( "Inet.au3" )]
        [TestCase( "InetConstants.au3" )]
        [TestCase( "IPAddressConstants.au3" )]
        [TestCase( "ListBoxConstants.au3" )]
        [TestCase( "ListViewConstants.au3" )]
        [TestCase( "Math.au3" )]
        [TestCase( "Memory.au3" )]
        [TestCase( "MemoryConstants.au3" )]
        [TestCase( "MenuConstants.au3" )]
        [TestCase( "Misc.au3" )]
        [TestCase( "MsgBoxConstants.au3" )]
        [TestCase( "NamedPipes.au3" )]
        [TestCase( "NetShare.au3" )]
        [TestCase( "NTSTATUSConstants.au3" )]
        [TestCase( "Process.au3" )]
        [TestCase( "ProcessConstants.au3" )]
        [TestCase( "ProgressConstants.au3" )]
        [TestCase( "RebarConstants.au3" )]
        [TestCase( "RichEditConstants.au3" )]
        [TestCase( "ScreenCapture.au3" )]
        [TestCase( "ScrollBarConstants.au3" )]
        [TestCase( "ScrollBarsConstants.au3" )]
        [TestCase( "Security.au3" )]
        [TestCase( "SecurityConstants.au3" )]
        [TestCase( "SendMessage.au3" )]
        [TestCase( "SliderConstants.au3" )]
        [TestCase( "Sound.au3" )]
        [TestCase( "SQLite.au3" )]
        [TestCase( "SQLite.dll.au3" )]
        [TestCase( "StaticConstants.au3" )]
        [TestCase( "StatusBarConstants.au3" )]
        [TestCase( "String.au3" )]
        [TestCase( "StringConstants.au3" )]
        [TestCase( "StructureConstants.au3" )]
        [TestCase( "TabConstants.au3" )]
        [TestCase( "Timers.au3" )]
        [TestCase( "ToolbarConstants.au3" )]
        [TestCase( "ToolTipConstants.au3" )]
        [TestCase( "TrayConstants.au3" )]
        [TestCase( "TreeViewConstants.au3" )]
        [TestCase( "UDFGlobalID.au3" )]
        [TestCase( "UpDownConstants.au3" )]
        [TestCase( "Visa.au3" )]
        [TestCase( "WinAPI.au3" )]
        [TestCase( "WinAPICom.au3" )]
        [TestCase( "WinAPIConstants.au3" )]
        [TestCase( "WinAPIDiag.au3" )]
        [TestCase( "WinAPIDlg.au3" )]
        [TestCase( "WinAPIError.au3" )]
        [TestCase( "WinAPIEx.au3" )]
        [TestCase( "WinAPIFiles.au3" )]
        [TestCase( "WinAPIGdi.au3" )]
        [TestCase( "WinAPIInternals.au3" )]
        [TestCase( "WinAPIlangConstants.au3" )]
        [TestCase( "WinAPILocale.au3" )]
        [TestCase( "WinAPIMisc.au3" )]
        [TestCase( "WinAPIProc.au3" )]
        [TestCase( "WinAPIReg.au3" )]
        [TestCase( "WinAPIRes.au3" )]
        [TestCase( "WinAPIShellEx.au3" )]
        [TestCase( "WinAPIShPath.au3" )]
        [TestCase( "WinAPISys.au3" )]
        [TestCase("WinAPIsysinfoConstants.au3" )]
        [TestCase( "WinAPITheme.au3" )]
        [TestCase( "WinAPIvkeysConstants.au3")]
        [TestCase( "WindowsConstants.au3" )]
        [TestCase( "WinNet.au3" )]
        [TestCase( "Word.au3" )]
        [TestCase( "WordConstants.au3" )]
        public void Test_compile_includes( string file ) {
            var path = string.Format( "{0}..\\..\\..\\testdata\\ParserTests\\{1}", Environment.CurrentDirectory, file );
            var script = File.ReadAllText( path );

            Assert.DoesNotThrow( () => {
                var assembly = _compiler.Compile( script, OutputKind.DynamicallyLinkedLibrary, false );

                File.WriteAllBytes(@"C:\Users\Brunnmeier\Desktop\backup\WUHUUU.dll", assembly);
            } );

        }
    }
}