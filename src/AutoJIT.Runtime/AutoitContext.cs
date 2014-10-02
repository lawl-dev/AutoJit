using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace AutoJITRuntime
{
    public class AutoitContext<T>
    {
        public Dictionary<MethodInfo, int> LibRegister = new Dictionary<MethodInfo, int>();

        public Dictionary<string, object> AssignVariableStore = new Dictionary<string, object>();

        public T Context { get; set; }
        public readonly Dictionary<string, MethodInfo> MethodStore;
        public IntPtr Gui;

        public AutoitContext( T context ) {
            Context = context;
            MethodStore = context.GetType().GetMethods().ToDictionary( x => x.Name, x => x );
            @Error = 0;
            @Extended = 0;
        }


        //http://www.autoitscript.com/autoit3/docs/macros.htm
        public Variant @AppDataCommonDir {
            get { return Environment.GetFolderPath( Environment.SpecialFolder.CommonApplicationData ); }
        }

        public Variant @TempDir {
            get { return Path.GetTempPath(); }
        }

        public Variant @AppDataDir {
            get { return Environment.GetFolderPath( Environment.SpecialFolder.ApplicationData ); }
        }

        public Variant @AutoItExe {
            get { return Assembly.GetExecutingAssembly().Location; }
        }

        public Variant @AutoItPID {
            get { return Process.GetCurrentProcess().Id; }
        }

        public Variant @AutoItVersion {
            get { return "1.3.3.7"; }
        }

        public Variant @AutoItX64 {
            get { throw new NotImplementedException(); }
        }

        public Variant @CommonFilesDir {
            get { return Environment.GetFolderPath( Environment.SpecialFolder.CommonProgramFiles ); }
        }

        public Variant @Compiled {
            get { return true; }
        }

        public Variant @ComputerName {
            get { return Environment.UserDomainName; }
        }

        public Variant @HOUR {
            get { return DateTime.Now.Hour; }
        }

        public Variant @MIN {
            get { return DateTime.Now.Minute; }
        }

        public Variant @OSBuild {
            get { throw new NotImplementedException(); }
        }

        public Variant @WindowsDir {
            get { return Environment.GetFolderPath( Environment.SpecialFolder.Windows ); }
        }

        public Variant @SW_HIDE {
            get { throw new NotImplementedException(); }
        }

        public Variant @SystemDir {
            get { throw new NotImplementedException(); }
        }

        public Variant @LF {
            get { throw new NotImplementedException(); }
        }

        public Variant @CR {
            get { throw new NotImplementedException(); }
        }

        public Variant @TAB {
            get { throw new NotImplementedException(); }
        }

        public Variant @CRLF {
            get { return Environment.NewLine; }
        }

        public Variant @SW_SHOW {
            get { return Environment.NewLine; }
        }

        public Variant @NumParams {
            get { throw new NotImplementedException(); }
        }

        public Variant @ScriptDir {
            get { return Environment.CurrentDirectory; }
        }

        public Variant @ComSpec {
            get { return Environment.GetEnvironmentVariable( "COMSPEC" ); }
        }

        public Variant @YEAR {
            get { return DateTime.Now.Year; }
        }

        public Variant @MON {
            get { return DateTime.Now.Year; }
        }

        public Variant @Error { get; set; }
        public Variant @Extended { get; set; }



        public Variant @COM_EventObj {
            get { throw new NotImplementedException(); }
        }

        public Variant @CPUArch {
            get { throw new NotImplementedException(); }
        }

        public Variant @DesktopCommonDir {
            get { throw new NotImplementedException(); }
        }

        public Variant @DesktopDepth {
            get { throw new NotImplementedException(); }
        }

        public Variant @DesktopDir {
            get { throw new NotImplementedException(); }
        }

        public Variant @DesktopHeight {
            get { throw new NotImplementedException(); }
        }

        public Variant @DesktopRefresh {
            get { throw new NotImplementedException(); }
        }

        public Variant @DesktopWidth {
            get { throw new NotImplementedException(); }
        }

        public Variant @DocumentsCommonDir {
            get { throw new NotImplementedException(); }
        }

        public Variant @exitCode {
            get { throw new NotImplementedException(); }
        }

        public Variant @exitMethod {
            get { throw new NotImplementedException(); }
        }

        public Variant @FavoritesCommonDir {
            get { throw new NotImplementedException(); }
        }

        public Variant @FavoritesDir {
            get { throw new NotImplementedException(); }
        }

        public Variant @GUI_CtrlHandle {
            get { throw new NotImplementedException(); }
        }

        public Variant @GUI_CtrlId {
            get { throw new NotImplementedException(); }
        }

        public Variant @GUI_DragFile {
            get { throw new NotImplementedException(); }
        }

        public Variant @GUI_DragId {
            get { throw new NotImplementedException(); }
        }

        public Variant @GUI_DropId {
            get { throw new NotImplementedException(); }
        }

        public Variant @GUI_WinHandle {
            get { throw new NotImplementedException(); }
        }

        public Variant @HomeDrive {
            get { throw new NotImplementedException(); }
        }

        public Variant @HomePath {
            get { throw new NotImplementedException(); }
        }

        public Variant @HomeShare {
            get { throw new NotImplementedException(); }
        }

        public Variant @HotKeyPressed {
            get { throw new NotImplementedException(); }
        }

        public Variant @IPAddress1 {
            get { throw new NotImplementedException(); }
        }

        public Variant @IPAddress2 {
            get { throw new NotImplementedException(); }
        }

        public Variant @IPAddress3 {
            get { throw new NotImplementedException(); }
        }

        public Variant @IPAddress4 {
            get { throw new NotImplementedException(); }
        }

        public Variant @KBLayout {
            get { throw new NotImplementedException(); }
        }

        public Variant @LocalAppDataDir {
            get { throw new NotImplementedException(); }
        }

        public Variant @LogonDNSDomain {
            get { throw new NotImplementedException(); }
        }

        public Variant @LogonDomain {
            get { throw new NotImplementedException(); }
        }

        public Variant @LogonServer {
            get { throw new NotImplementedException(); }
        }

        public Variant @MDAY {
            get { throw new NotImplementedException(); }
        }

        public Variant @MSEC {
            get { throw new NotImplementedException(); }
        }

        public Variant @MUILang {
            get { throw new NotImplementedException(); }
        }

        public Variant @MyDocumentsDir {
            get { throw new NotImplementedException(); }
        }

        public Variant @OSArch {
            get { throw new NotImplementedException(); }
        }

        public Variant @OSLang {
            get { throw new NotImplementedException(); }
        }

        public Variant @OSServicePack {
            get { throw new NotImplementedException(); }
        }

        public Variant @OSType {
            get { throw new NotImplementedException(); }
        }

        public Variant @OSVersion {
            get { return Environment.OSVersion.ToString(); }
        }

        public Variant @ProgramFilesDir {
            get { throw new NotImplementedException(); }
        }

        public Variant @ProgramsCommonDir {
            get { throw new NotImplementedException(); }
        }

        public Variant @ProgramsDir {
            get { throw new NotImplementedException(); }
        }

        public Variant @ScriptFullPath {
            get { throw new NotImplementedException(); }
        }

        public Variant @ScriptLineNumber {
            get { throw new NotImplementedException(); }
        }

        public Variant @ScriptName {
            get { throw new NotImplementedException(); }
        }

        public Variant @SEC {
            get { throw new NotImplementedException(); }
        }

        public Variant @StartMenuCommonDir {
            get { throw new NotImplementedException(); }
        }

        public Variant @StartMenuDir {
            get { throw new NotImplementedException(); }
        }

        public Variant @StartupCommonDir {
            get { throw new NotImplementedException(); }
        }

        public Variant @StartupDir {
            get { throw new NotImplementedException(); }
        }

        public Variant @SW_DISABLE {
            get { throw new NotImplementedException(); }
        }

        public Variant @SW_ENABLE {
            get { throw new NotImplementedException(); }
        }


        public Variant @SW_LOCK {
            get { throw new NotImplementedException(); }
        }

        public Variant @SW_MAXIMIZE {
            get { throw new NotImplementedException(); }
        }

        public Variant @SW_MINIMIZE {
            get { throw new NotImplementedException(); }
        }

        public Variant @SW_RESTORE {
            get { throw new NotImplementedException(); }
        }

        public Variant @SW_SHOWDEFAULT {
            get { throw new NotImplementedException(); }
        }

        public Variant @SW_SHOWMAXIMIZED {
            get { throw new NotImplementedException(); }
        }

        public Variant @SW_SHOWMINIMIZED {
            get { throw new NotImplementedException(); }
        }

        public Variant @SW_SHOWMINNOACTIVE {
            get { throw new NotImplementedException(); }
        }

        public Variant @SW_SHOWNA {
            get { throw new NotImplementedException(); }
        }

        public Variant @SW_SHOWNOACTIVATE {
            get { throw new NotImplementedException(); }
        }

        public Variant @SW_SHOWNORMAL {
            get { throw new NotImplementedException(); }
        }

        public Variant @SW_UNLOCK {
            get { throw new NotImplementedException(); }
        }

        public Variant @TRAY_ID {
            get { throw new NotImplementedException(); }
        }

        public Variant @TrayIconFlashing {
            get { throw new NotImplementedException(); }
        }

        public Variant @TrayIconVisible {
            get { throw new NotImplementedException(); }
        }

        public Variant @UserName {
            get { throw new NotImplementedException(); }
        }

        public Variant @UserProfileDir {
            get { throw new NotImplementedException(); }
        }

        public Variant @WDAY {
            get { throw new NotImplementedException(); }
        }

        public Variant @WorkingDir {
            get { throw new NotImplementedException(); }
        }

        public Variant @YDAY {
            get { throw new NotImplementedException(); }
        }
    }
}