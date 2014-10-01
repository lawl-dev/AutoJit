using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace AutoJITRuntime
{
    public class AutoitRuntime<T>
    {
        private readonly AutoitContext<T> _context;
        private readonly Dictionary<string, MethodInfo> _methodStore;
        private readonly AutoitRuntimeSpike<T> _spikeRuntime;

        public AutoitRuntime(AutoitContext<T> context) {
            _context = context;
            _spikeRuntime = new AutoitRuntimeSpike<T>(_context);
            var methodInfos = GetType().GetMethods();
            _methodStore = methodInfos.ToDictionary( x => x.Name, x => x );
        }


        public Variant ACos(Variant expression) {
            return Math.Acos( expression );
        }

        public Variant ASin(Variant expression) {
            return Math.Asin( expression );
        }

        public Variant ATan(Variant expression) {
            return Math.Atan( expression );
        }

        public Variant Abs(Variant expression) {
            return Math.Abs( expression );
        }

        public Variant AdlibRegister( Variant function, Variant time = null ) {
            throw new NotImplementedException();
        }

        public Variant AdlibUnRegister(Variant function)
        {
            throw new NotImplementedException();
        }

        public Variant Asc(Variant @char) {
            var character = ((string)@char).FirstOrDefault();
            return character;
        }

        public Variant AscW(Variant @char)
        {
            var character = ((string)@char).FirstOrDefault();
            return character;
        }

        public Variant Assign(Variant varname, Variant data, Variant flag = null)
        {
            throw new NotImplementedException();
        }

        public Variant Opt(Variant option, Variant param = null) {
            return AutoItSetOption( option, param );
        }

        public Variant AutoItSetOption(Variant option, Variant param = null)
        {
            throw new NotImplementedException();
        }

        public Variant AutoItWinGetTitle()
        {
            throw new NotImplementedException();
        }

        public Variant AutoItWinSetTitle(Variant newtitle)
        {
            throw new NotImplementedException();
        }

        public Variant Beep(Variant Frequency = null, Variant Duration = null)
        {
            if ( Frequency == null ) {
                Frequency = 500;
            }

            if ( Duration == null ) {
                Duration = 1000;
            }

            Console.Beep(Frequency, Duration);

            return 1;
        }

        public Variant Binary(Variant expression) {
            return expression.GetBinary();
        }

        public Variant BinaryLen(Variant binary) {
            return binary.GetBinary().Length;
        }

        public Variant BinaryMid(Variant binary, Variant start, Variant count = null)
        {
            var bytes = binary.GetBinary();
            if ( start < 1 ||
                 start >= bytes.Length || (count != null && start >= count) ) {
                return new byte[0];
            }
            return bytes.Skip( start-1 ).Take(count ?? bytes.Length-start-1 ).ToArray();
        }

        public Variant BinaryToString(Variant expression, Variant flag = null) {
            if ( flag == null ) {
                flag = 1;
            }

            switch (flag.GetInt()) {
                case 1:
                    return expression.GetString();
            }
            throw new NotImplementedException();
        }

        public Variant BitAND(Variant value1, Variant value2, params Variant[] valuen)
        {
            var res = (int)value1 & (int)value1;
            foreach (var variant in valuen)
            {
                res &= variant;
            }
            return res;
        }

        public Variant BitNOT(Variant value) {
            return ~value;
        }

        public Variant BitOR(Variant value1, Variant value2, params Variant[] valuen) {
            var res = (int)value1 | (int)value1;
            foreach (var variant in valuen) {
                res |= variant;
            }
            return res;
        }

        public Variant BitRotate(Variant value, Variant shift = null, Variant size = null)
        {
            throw new NotImplementedException();
        }

        public Variant BitShift(Variant value, Variant shift) {
            var intValue = (int)value;
            if ( intValue > 0 ) {
                return intValue >> (int) shift;
            }
            return intValue << (int)shift;
        }

        public Variant BitXOR(Variant value1, Variant value2, params Variant[] valuen) {
            var res = (int)value1 ^ (int)value1;
            foreach (var variant in valuen)
            {
                res ^= variant;
            }
            return res;
        }

        public Variant BlockInput(Variant flag)
        {
            throw new NotImplementedException();
        }

        public Variant Break(Variant mode)
        {
            throw new NotImplementedException();
        }

        public Variant CDTray(Variant drive, Variant status)
        {
            throw new NotImplementedException();
        }

        public Variant Call(Variant function, params Variant[] paramsN)
        {
            if ( _methodStore.ContainsKey( function ) ) {
                return Variant.Create(_methodStore[function].Invoke( this, paramsN ));
            }
            if ( _context.MethodStore.ContainsKey( function ) ) {
                return Variant.Create( _context.MethodStore[function].Invoke( _context.Context, paramsN ) );
            }

            _context.Error = 0xDEAD;
            _context.Extended = 0xBEEF;
            return 0;
        }

        public Variant Ceiling(Variant expression) {
            return Math.Ceiling( (double) expression );
        }

        public Variant Chr(Variant ASCIIcode)
        {
            if ( ASCIIcode < 0) {
                return string.Empty;
            }
            if ( ASCIIcode > 255 ) {
                _context.Error = 1;
                return string.Empty;
            }

            return ( (char) ASCIIcode ).ToString( CultureInfo.InvariantCulture );
        }

        public Variant ChrW(Variant UNICODEcode)
        {
            if ( UNICODEcode > ushort.MaxValue ) {
                _context.Error = UNICODEcode;
                return string.Empty;
            }

            if ( UNICODEcode < 0 ) {
                return string.Empty;
            }


            return (char) UNICODEcode;
        }

        public Variant ClipGet()
        {
            throw new NotImplementedException();
        }

        public Variant ClipPut(Variant value)
        {
            throw new NotImplementedException();
        }

        public Variant ConsoleRead(Variant peek = null, Variant binary = null)
        {
            throw new NotImplementedException();
        }

        public Variant ConsoleWrite(Variant data)
        {
            var @string = data.GetString();

            Console.Write( @string );
            return @string.Length;
        }

        public Variant ConsoleWriteError(Variant data)
        {
            if ( data.IsBinary ) {
                var binary = data.GetBinary();
                Console.Error.Write(binary.Select( x=>(char)x ).ToArray());
                return binary.Length;
            }
            var @string = data.GetString();
            Console.Error.Write( @string );
            return @string;
        }

        public Variant ControlClick(
            Variant title,
            Variant text,
            Variant controlID,
            Variant button = null,
            Variant clicks = null,
            Variant x = null,
            Variant y = null)
        {
            throw new NotImplementedException();
        }

        public Variant ControlCommand(Variant title, Variant text, Variant controlID, Variant command, Variant option = null)
        {
            throw new NotImplementedException();
        }

        public Variant ControlDisable(Variant title, Variant text, Variant controlID)
        {
            throw new NotImplementedException();
        }

        public Variant ControlEnable(Variant title, Variant text, Variant controlID)
        {
            throw new NotImplementedException();
        }

        public Variant ControlFocus(Variant title, Variant text, Variant controlID)
        {
            throw new NotImplementedException();
        }

        public Variant ControlGetFocus(Variant title, Variant text = null)
        {
            throw new NotImplementedException();
        }

        public Variant ControlGetHandle(Variant title, Variant text, Variant controlID)
        {
            throw new NotImplementedException();
        }

        public Variant ControlGetPos(Variant title, Variant text, Variant controlID)
        {
            throw new NotImplementedException();
        }

        public Variant ControlGetText(Variant title, Variant text, Variant controlID)
        {
            throw new NotImplementedException();
        }

        public Variant ControlHide(Variant title, Variant text, Variant controlID)
        {
            throw new NotImplementedException();
        }

        public Variant ControlListView(Variant title, Variant text, Variant controlID, Variant command, params Variant[] optionN)
        {
            throw new NotImplementedException();
        }

        public Variant ControlMove(Variant title, Variant text, Variant controlID, Variant x, Variant y, Variant width = null, Variant height = null)
        {
            throw new NotImplementedException();
        }

        public Variant ControlSend(Variant title, Variant text, Variant controlID, Variant @string, Variant flag = null)
        {
            throw new NotImplementedException();
        }

        public Variant ControlSetText(Variant title, Variant text, Variant controlID, Variant newtext, Variant flag = null)
        {
            throw new NotImplementedException();
        }

        public Variant ControlShow(Variant title, Variant text, Variant controlID)
        {
            throw new NotImplementedException();
        }

        public Variant ControlTreeView(Variant title, Variant text, Variant controlID, Variant command, Variant option1 = null)
        {
            throw new NotImplementedException();
        }

        public Variant Cos(Variant expression) {
            return Math.Cos( expression );
        }

        public Variant Dec(Variant hex, Variant flag = null) {
            Int64 result;
            if(Int64.TryParse( hex, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out result))
            {
                if ( result <= int.MaxValue &&
                     result >= int.MinValue ) {
                    return (int)result;
                }
                return result;
            }
            _context.Error = 1;
            return 0;
        }

        public Variant DirCopy(Variant sourcedir, Variant destdir, Variant flag = null)
        {
            throw new NotImplementedException();
        }

        public Variant DirCreate(Variant path)
        {
            throw new NotImplementedException();
        }

        public Variant DirGetSize(Variant path, Variant flag = null)
        {
            throw new NotImplementedException();
        }

        public Variant DirMove(Variant sourcedir, Variant destdir, Variant flag = null)
        {
            throw new NotImplementedException();
        }

        public Variant DirRemove(Variant path, Variant recurse = null)
        {
            throw new NotImplementedException();
        }

        public Variant DllCall(Variant dll, Variant returntype, Variant function, params Variant[] paramtypen) {
            return _spikeRuntime.DllCall( dll, returntype, function, paramtypen );
        }

        public Variant DllCallAddress(Variant returntype, Variant address, params Variant[] paramtypen)
        {
            throw new NotImplementedException();
        }

        public Variant DllCallbackFree(Variant handle)
        {
            throw new NotImplementedException();
        }

        public Variant DllCallbackGetPtr(Variant handle)
        {
            throw new NotImplementedException();
        }

        public Variant DllCallbackRegister(Variant function, Variant returntype, params Variant[] @params)
        {
            throw new NotImplementedException();
        }

        public Variant DllClose(Variant dllhandle) {
            return _spikeRuntime.DllClose( dllhandle );
        }

        public Variant DllOpen(Variant filename) {
            return _spikeRuntime.DllOpen( filename );
        }

        public Variant DllStructCreate( Variant Struct, Variant Pointer = null ) {
            return _spikeRuntime.DllStructCreate( Struct, Pointer );
        }


        public Variant DllStructGetData(Variant Struct, Variant Element, Variant index = null) {
            return _spikeRuntime.DllStructGetData( Struct, Element, index );
        }

        public Variant DllStructGetPtr(Variant Struct, Variant Element = null) {
            return _spikeRuntime.DllStructGetPtr( Struct, Element );
        }

        public Variant DllStructGetSize(Variant Struct) {
            return _spikeRuntime.DllStructGetSize( Struct );
        }

        public Variant DllStructSetData(Variant Struct, Variant Element, Variant value, Variant index = null) {
            return _spikeRuntime.DllStructSetData( Struct, Element, value, index );
        }

        public Variant DriveGetDrive(Variant type)
        {
            throw new NotImplementedException();
        }
 


        public Variant DriveGetFileSystem(Variant path)
        {
            throw new NotImplementedException();
        }

        public Variant DriveGetLabel(Variant path)
        {
            throw new NotImplementedException();
        }

        public Variant DriveGetSerial(Variant path)
        {
            throw new NotImplementedException();
        }

        public Variant DriveGetType(Variant path, Variant operation = null)
        {
            throw new NotImplementedException();
        }

        public Variant DriveMapAdd(Variant device, Variant remoteshare, Variant flags = null, Variant user = null, Variant password = null)
        {
            throw new NotImplementedException();
        }

        public Variant DriveMapDel(Variant drive)
        {
            throw new NotImplementedException();
        }

        public Variant DriveMapGet(Variant device)
        {
            throw new NotImplementedException();
        }

        public Variant DriveSetLabel(Variant path, Variant label)
        {
            throw new NotImplementedException();
        }

        public Variant DriveSpaceFree(Variant path)
        {
            throw new NotImplementedException();
        }

        public Variant DriveSpaceTotal(Variant path)
        {
            throw new NotImplementedException();
        }

        public Variant DriveStatus(Variant path)
        {
            throw new NotImplementedException();
        }

        public Variant EnvGet(Variant envvariable)
        {
            throw new NotImplementedException();
        }

        public Variant EnvSet(Variant envvariable, Variant value = null)
        {
            throw new NotImplementedException();
        }

        public Variant EnvUpdate()
        {
            throw new NotImplementedException();
        }

        public Variant Eval(Variant @string)
        {
            throw new NotImplementedException();
        }

        public Variant Execute(Variant @string)
        {
            throw new NotImplementedException();
        }

        public Variant Exp(Variant expression)
        {
            throw new NotImplementedException();
        }

        public Variant FileChangeDir(Variant path)
        {
            throw new NotImplementedException();
        }

        public Variant FileClose(Variant filehandle)
        {
            throw new NotImplementedException();
        }

        public Variant FileCopy(Variant source, Variant dest, Variant flag = null)
        {
            throw new NotImplementedException();
        }

        public Variant FileCreateNTFSLink(Variant source, Variant hardlink, Variant flag = null)
        {
            throw new NotImplementedException();
        }

        public Variant FileCreateShortcut(
            Variant file,
            Variant lnk,
            Variant workdir = null,
            Variant args = null,
            Variant desc = null,
            Variant icon = null,
            Variant hotkey = null,
            Variant iconnumber = null,
            Variant state = null)
        {
            throw new NotImplementedException();
        }

        public Variant FileDelete(Variant filename)
        {
            throw new NotImplementedException();
        }

        public Variant FileExists(Variant path)
        {
            throw new NotImplementedException();
        }

        public Variant FileFindFirstFile(Variant filename)
        {
            throw new NotImplementedException();
        }

        public Variant FileFindNextFile(Variant search)
        {
            throw new NotImplementedException();
        }

        public Variant FileFlush(Variant filehandle)
        {
            throw new NotImplementedException();
        }

        public Variant FileGetAttrib(Variant filename)
        {
            throw new NotImplementedException();
        }

        public Variant FileGetEncoding(Variant filehandlefilename, Variant mode = null)
        {
            throw new NotImplementedException();
        }

        public Variant FileGetLongName(Variant filename, Variant flag = null)
        {
            throw new NotImplementedException();
        }

        public Variant FileGetPos(Variant filehandle)
        {
            throw new NotImplementedException();
        }

        public Variant FileGetShortName(Variant filename, Variant flag = null)
        {
            throw new NotImplementedException();
        }

        public Variant FileGetShortcut(Variant lnk)
        {
            throw new NotImplementedException();
        }

        public Variant FileGetSize(Variant filename)
        {
            throw new NotImplementedException();
        }

        public Variant FileGetTime(Variant filename, Variant option = null, Variant format = null)
        {
            throw new NotImplementedException();
        }

        public Variant FileGetVersion(Variant filename, Variant stringname = null)
        {
            throw new NotImplementedException();
        }

        public Variant FileInstall(Variant source, Variant dest, Variant flag = null)
        {
            throw new NotImplementedException();
        }

        public Variant FileMove(Variant source, Variant dest, Variant flag = null)
        {
            throw new NotImplementedException();
        }

        public Variant FileOpen(Variant filename, Variant mode = null)
        {
            throw new NotImplementedException();
        }

        public Variant FileOpenDialog(Variant title, Variant initdir, Variant filter, Variant options = null, Variant defaultname = null, Variant hwnd = null)
        {
            throw new NotImplementedException();
        }

        public Variant FileRead(Variant filehandlefilename, Variant count = null)
        {
            throw new NotImplementedException();
        }

        public Variant FileReadLine(Variant filehandlefilename, Variant line = null)
        {
            throw new NotImplementedException();
        }

        public Variant FileReadToArray(Variant filehandlefilename)
        {
            throw new NotImplementedException();
        }

        public Variant FileRecycle(Variant source)
        {
            throw new NotImplementedException();
        }

        public Variant FileRecycleEmpty(Variant source)
        {
            throw new NotImplementedException();
        }

        public Variant FileSaveDialog(Variant title, Variant initdir, Variant filter, Variant options = null, Variant defaultname = null, Variant hwnd = null)
        {
            throw new NotImplementedException();
        }

        public Variant FileSelectFolder(Variant dialogtext, Variant rootdir, Variant flag = null, Variant initialdir = null, Variant hwnd = null)
        {
            throw new NotImplementedException();
        }

        public Variant FileSetAttrib(Variant filepattern, Variant RASHNOT, Variant recurse = null)
        {
            throw new NotImplementedException();
        }

        public Variant FileSetPos(Variant filehandle, Variant offset, Variant origin)
        {
            throw new NotImplementedException();
        }

        public Variant FileSetTime(Variant filepattern, Variant time, Variant type = null, Variant recurse = null)
        {
            throw new NotImplementedException();
        }

        public Variant FileWrite(Variant filehandlefilename, Variant textdata)
        {
            throw new NotImplementedException();
        }

        public Variant FileWriteLine(Variant filehandlefilename, Variant line)
        {
            throw new NotImplementedException();
        }

        public Variant Floor(Variant expression)
        {
            throw new NotImplementedException();
        }

        public Variant FtpSetProxy(Variant mode = null, Variant proxyport = null, Variant username = null, Variant password = null)
        {
            throw new NotImplementedException();
        }

        public Variant FuncName(Variant Functionvariable)
        {
            throw new NotImplementedException();
        }

        public Variant GUICreate(
            Variant title,
            Variant width = null,
            Variant height = null,
            Variant left = null,
            Variant top = null,
            Variant style = null,
            Variant exStyle = null,
            Variant parent = null)
        {
            throw new NotImplementedException();
        }


        public Variant GUICtrlCreateAvi(
            Variant filename,
            Variant subfileid,
            Variant left,
            Variant top,
            Variant width = null,
            Variant height = null,
            Variant style = null,
            Variant exStyle = null)
        {
            throw new NotImplementedException();
        }

        public Variant GUICtrlCreateButton(
            Variant text,
            Variant left,
            Variant top,
            Variant width = null,
            Variant height = null,
            Variant style = null,
            Variant exStyle = null)
        {
            throw new NotImplementedException();
        }

        public Variant GUICtrlCreateCheckbox(
            Variant text,
            Variant left,
            Variant top,
            Variant width = null,
            Variant height = null,
            Variant style = null,
            Variant exStyle = null)
        {
            throw new NotImplementedException();
        }

        public Variant GUICtrlCreateCombo(
            Variant text,
            Variant left,
            Variant top,
            Variant width = null,
            Variant height = null,
            Variant style = null,
            Variant exStyle = null)
        {
            throw new NotImplementedException();
        }

        public Variant GUICtrlCreateContextMenu(Variant controlID)
        {
            throw new NotImplementedException();
        }

        public Variant GUICtrlCreateDate(
            Variant text,
            Variant left,
            Variant top,
            Variant width = null,
            Variant height = null,
            Variant style = null,
            Variant exStyle = null)
        {
            throw new NotImplementedException();
        }

        public Variant GUICtrlCreateDummy(Variant d)
        {
            throw new NotImplementedException();
        }

        public Variant GUICtrlCreateEdit(
            Variant text,
            Variant left,
            Variant top,
            Variant width = null,
            Variant height = null,
            Variant style = null,
            Variant exStyle = null)
        {
            throw new NotImplementedException();
        }

        public Variant GUICtrlCreateGraphic(Variant left, Variant top, Variant width = null, Variant height = null, Variant style = null)
        {
            throw new NotImplementedException();
        }

        public Variant GUICtrlCreateGroup(
            Variant text,
            Variant left,
            Variant top = null,
            Variant width = null,
            Variant height = null,
            Variant style = null,
            Variant exStyle = null)
        {
            throw new NotImplementedException();
        }

        public Variant GUICtrlCreateIcon(
            Variant filename,
            Variant iconName,
            Variant left,
            Variant top,
            Variant width = null,
            Variant height = null,
            Variant style = null,
            Variant exStyle = null)
        {
            throw new NotImplementedException();
        }

        public Variant GUICtrlCreateInput(
            Variant text,
            Variant left,
            Variant top,
            Variant width = null,
            Variant height = null,
            Variant style = null,
            Variant exStyle = null)
        {
            throw new NotImplementedException();
        }

        public Variant GUICtrlCreateLabel(
            Variant text,
            Variant left,
            Variant top,
            Variant width = null,
            Variant height = null,
            Variant style = null,
            Variant exStyle = null)
        {
            throw new NotImplementedException();
        }

        public Variant GUICtrlCreateList(
            Variant text,
            Variant left,
            Variant top,
            Variant width = null,
            Variant height = null,
            Variant style = null,
            Variant exStyle = null)
        {
            throw new NotImplementedException();
        }

        public Variant GUICtrlCreateListView(
            Variant text,
            Variant left,
            Variant top,
            Variant width = null,
            Variant height = null,
            Variant style = null,
            Variant exStyle = null)
        {
            throw new NotImplementedException();
        }

        public Variant GUICtrlCreateListViewItem(Variant text, Variant listviewID)
        {
            throw new NotImplementedException();
        }

        public Variant GUICtrlCreateMenu(Variant submenutext, Variant menuID = null, Variant menuentry = null)
        {
            throw new NotImplementedException();
        }

        public Variant GUICtrlCreateMenuItem(Variant text, Variant menuID, Variant menuentry = null, Variant menuradioitem = null)
        {
            throw new NotImplementedException();
        }

        public Variant GUICtrlCreateMonthCal(
            Variant text,
            Variant left,
            Variant top,
            Variant width = null,
            Variant height = null,
            Variant style = null,
            Variant exStyle = null)
        {
            throw new NotImplementedException();
        }

        public Variant GUICtrlCreateObj(Variant ObjectVar, Variant left, Variant top, Variant width = null, Variant height = null)
        {
            throw new NotImplementedException();
        }

        public Variant GUICtrlCreatePic(
            Variant filename,
            Variant left,
            Variant top,
            Variant width = null,
            Variant height = null,
            Variant style = null,
            Variant exStyle = null)
        {
            throw new NotImplementedException();
        }

        public Variant GUICtrlCreateProgress(
            Variant left,
            Variant top,
            Variant width = null,
            Variant height = null,
            Variant style = null,
            Variant exStyle = null)
        {
            throw new NotImplementedException();
        }

        public Variant GUICtrlCreateRadio(
            Variant text,
            Variant left,
            Variant top,
            Variant width = null,
            Variant height = null,
            Variant style = null,
            Variant exStyle = null)
        {
            throw new NotImplementedException();
        }

        public Variant GUICtrlCreateSlider(
            Variant left,
            Variant top,
            Variant width = null,
            Variant height = null,
            Variant style = null,
            Variant exStyle = null)
        {
            throw new NotImplementedException();
        }

        public Variant GUICtrlCreateTab(Variant left, Variant top, Variant width = null, Variant height = null, Variant style = null, Variant exStyle = null)
        {
            throw new NotImplementedException();
        }

        public Variant GUICtrlCreateTabItem(Variant text)
        {
            throw new NotImplementedException();
        }

        public Variant GUICtrlCreateTreeView(Variant left, Variant top, Variant width, Variant height = null, Variant style = null, Variant exStyle = null)
        {
            throw new NotImplementedException();
        }

        public Variant GUICtrlCreateTreeViewItem(Variant text, Variant treeviewID)
        {
            throw new NotImplementedException();
        }

        public Variant GUICtrlCreateUpdown(Variant inputcontrolID, Variant style = null)
        {
            throw new NotImplementedException();
        }

        public Variant GUICtrlDelete(Variant controlID)
        {
            throw new NotImplementedException();
        }

        public Variant GUICtrlGetHandle(Variant controlID)
        {
            throw new NotImplementedException();
        }

        public Variant GUICtrlGetState(Variant controlID)
        {
            throw new NotImplementedException();
        }

        public Variant GUICtrlRead(Variant controlID, Variant advanced = null)
        {
            throw new NotImplementedException();
        }

        public Variant GUICtrlRecvMsg(Variant controlID, Variant msg, Variant wParam = null, Variant lParamType = null)
        {
            throw new NotImplementedException();
        }

        public Variant GUICtrlRegisterListViewSort(Variant controlID, Variant function)
        {
            throw new NotImplementedException();
        }

        public Variant GUICtrlSendMsg(Variant controlID, Variant msg, Variant wParam, Variant lParam)
        {
            throw new NotImplementedException();
        }

        public Variant GUICtrlSendToDummy(Variant controlID, Variant state = null)
        {
            throw new NotImplementedException();
        }

        public Variant GUICtrlSetBkColor(Variant controlID, Variant backgroundcolor)
        {
            throw new NotImplementedException();
        }

        public Variant GUICtrlSetColor(Variant controlID, Variant textcolor)
        {
            throw new NotImplementedException();
        }

        public Variant GUICtrlSetCursor(Variant controlID, Variant cursorID)
        {
            throw new NotImplementedException();
        }

        public Variant GUICtrlSetData(Variant controlID, Variant data, Variant @default = null)
        {
            throw new NotImplementedException();
        }

        public Variant GUICtrlSetDefBkColor(Variant defbkcolor, Variant winhandle = null)
        {
            throw new NotImplementedException();
        }

        public Variant GUICtrlSetDefColor(Variant deftextcolor, Variant winhandle = null)
        {
            throw new NotImplementedException();
        }

        public Variant GUICtrlSetFont(
            Variant controlID,
            Variant size,
            Variant weight = null,
            Variant attribute = null,
            Variant fontname = null,
            Variant quality = null)
        {
            throw new NotImplementedException();
        }

        public Variant GUICtrlSetGraphic(Variant controlID, Variant type, params Variant[] parN)
        {
            throw new NotImplementedException();
        }

        public Variant GUICtrlSetImage(Variant controlID, Variant filename, Variant iconname = null, Variant icontype = null)
        {
            throw new NotImplementedException();
        }

        public Variant GUICtrlSetLimit(Variant controlID, Variant max, Variant min = null)
        {
            throw new NotImplementedException();
        }

        public Variant GUICtrlSetOnEvent(Variant controlID, Variant function)
        {
            throw new NotImplementedException();
        }

        public Variant GUICtrlSetPos(Variant controlID, Variant left, Variant top = null, Variant width = null, Variant height = null)
        {
            throw new NotImplementedException();
        }

        public Variant GUICtrlSetResizing(Variant controlID, Variant resizing)
        {
            throw new NotImplementedException();
        }

        public Variant GUICtrlSetState(Variant controlID, Variant state)
        {
            throw new NotImplementedException();
        }

        public Variant GUICtrlSetStyle(Variant controlID, Variant style, Variant exStyle = null)
        {
            throw new NotImplementedException();
        }

        public Variant GUICtrlSetTip(Variant controlID, Variant tiptext, Variant title = null, Variant icon = null, Variant options = null)
        {
            throw new NotImplementedException();
        }

        public Variant GUIDelete(Variant winhandle)
        {
            throw new NotImplementedException();
        }

        public Variant GUIGetCursorInfo(Variant winhandle)
        {
            throw new NotImplementedException();
        }

        public Variant GUIGetMsg(Variant advanced = null)
        {
            throw new NotImplementedException();
        }

        public Variant GUIGetStyle(Variant winhandle = null)
        {
            throw new NotImplementedException();
        }

        public Variant GUIRegisterMsg(Variant msgID, Variant function)
        {
            throw new NotImplementedException();
        }

        public Variant GUISetAccelerators(Variant accelerators, Variant winhandle = null)
        {
            throw new NotImplementedException();
        }

        public Variant GUISetBkColor(Variant background, Variant winhandle = null)
        {
            throw new NotImplementedException();
        }

        public Variant GUISetCoord(Variant left, Variant top, Variant width = null, Variant height = null, Variant winhandle = null)
        {
            throw new NotImplementedException();
        }

        public Variant GUISetCursor(Variant cursorID = null, Variant @override = null, Variant winhandle = null)
        {
            throw new NotImplementedException();
        }


        public Variant GUISetFont(
            Variant size,
            Variant weight = null,
            Variant attribute = null,
            Variant fontname = null,
            Variant winhandle = null,
            Variant quality = null)
        {
            throw new NotImplementedException();
        }

        public Variant GUISetHelp(Variant helpfile, Variant winhandle = null)
        {
            throw new NotImplementedException();
        }

        public Variant GUISetIcon(Variant iconfile, Variant iconID = null, Variant winhandle = null)
        {
            throw new NotImplementedException();
        }

        public Variant GUISetOnEvent(Variant specialID, Variant function, Variant winhandle = null)
        {
            throw new NotImplementedException();
        }

        public Variant GUISetState(Variant flag = null, Variant winhandle = null)
        {
            throw new NotImplementedException();
        }

        public Variant GUISetStyle(Variant Style, Variant ExStyle = null, Variant winhandle = null)
        {
            throw new NotImplementedException();
        }

        public Variant GUIStartGroup(Variant winhandle = null)
        {
            throw new NotImplementedException();
        }

        public Variant GUISwitch(Variant winhandle, Variant tabitemID = null)
        {
            throw new NotImplementedException();
        }

        public Variant HWnd(Variant expression)
        {
            throw new NotImplementedException();
        }

        public Variant Hex(Variant expression, Variant length = null)
        {
            throw new NotImplementedException();
        }

        public Variant HotKeySet(Variant key, Variant function = null)
        {
            throw new NotImplementedException();
        }

        public Variant HttpSetProxy(Variant mode = null, Variant proxyport = null, Variant username = null, Variant password = null)
        {
            throw new NotImplementedException();
        }

        public Variant HttpSetUserAgent(Variant useragent)
        {
            throw new NotImplementedException();
        }

        public Variant InetClose(Variant handle)
        {
            throw new NotImplementedException();
        }

        public Variant InetGet(Variant URL, Variant filename, Variant options = null, Variant background = null)
        {
            throw new NotImplementedException();
        }

        public Variant InetGetInfo(Variant handle = null, Variant index = null)
        {
            throw new NotImplementedException();
        }

        public Variant InetGetSize(Variant URL, Variant options = null)
        {
            throw new NotImplementedException();
        }

        public Variant InetRead(Variant URL, Variant options = null)
        {
            throw new NotImplementedException();
        }

        public Variant IniDelete(Variant filename, Variant section, Variant key = null)
        {
            throw new NotImplementedException();
        }

        public Variant IniRead(Variant filename, Variant section, Variant key, Variant @default)
        {
            throw new NotImplementedException();
        }

        public Variant IniReadSection(Variant filename, Variant section)
        {
            throw new NotImplementedException();
        }

        public Variant IniReadSectionNames(Variant filename)
        {
            throw new NotImplementedException();
        }

        public Variant IniRenameSection(Variant filename, Variant section, Variant newsection, Variant flag = null)
        {
            throw new NotImplementedException();
        }

        public Variant IniWrite(Variant filename, Variant section, Variant key, Variant value)
        {
            throw new NotImplementedException();
        }

        public Variant IniWriteSection(Variant filename, Variant section, Variant data, Variant index = null)
        {
            throw new NotImplementedException();
        }

        public Variant InputBox(
            Variant title,
            Variant prompt,
            Variant @default = null,
            Variant passwordchar = null,
            Variant width = null,
            Variant height = null,
            Variant left = null,
            Variant top = null,
            Variant timeout = null,
            Variant hwnd = null)
        {
            throw new NotImplementedException();
        }

        public Variant Int(Variant expression, Variant flag = null)
        {
            throw new NotImplementedException();
        }

        public Variant IsAdmin(Variant expression)
        {
            throw new NotImplementedException();
        }

        public Variant IsArray(Variant variable) {
            return variable.IsArray;
        }

        public Variant IsBinary(Variant variable) {
            return variable.IsBinary;
        }

        public Variant IsBool(Variant variable) {
            return variable.IsBool;
        }

        public Variant IsDeclared(Variant expression)
        {
            throw new NotImplementedException();
        }

        public Variant IsDllStruct(Variant variable)
        {
            throw new NotImplementedException();
        }

        public Variant IsFloat(Variant variable) {
            return variable.IsDouble;
        }

        public Variant IsFunc(Variant expression)
        {
            throw new NotImplementedException();
        }

        public Variant IsHWnd(Variant variable) {
            return variable.IsPtr && Control.FromHandle( variable ) != null;
        }

        public Variant IsInt(Variant variable) {
            return variable.IsInt32 || variable.IsInt64;
        }

        public Variant IsKeyword(Variant variable)
        {
            throw new NotImplementedException();
        }

        public Variant IsNumber(Variant variable) {
            return variable.IsInt32 || variable.IsInt64 || variable.IsDouble;
        }

        public Variant IsObj(Variant variable)
        {
            throw new NotImplementedException();
        }

        public Variant IsPtr(Variant variable) {
            return variable.IsPtr;
        }

        public Variant IsString(Variant variable) {
            return variable.IsString;
        }

        public Variant Log(Variant expression)
        {
            throw new NotImplementedException();
        }

        public Variant MemGetStats()
        {
            throw new NotImplementedException();
        }

        public Variant Mod(Variant value1, Variant value2)
        {
            throw new NotImplementedException();
        }

        public Variant MouseClick(Variant button, Variant x = null, Variant y = null, Variant clicks = null, Variant speed = null)
        {
            throw new NotImplementedException();
        }

        public Variant MouseClickDrag(Variant button, Variant x1, Variant y1, Variant x2, Variant y2, Variant speed = null)
        {
            throw new NotImplementedException();
        }

        public Variant MouseDown(Variant button)
        {
            throw new NotImplementedException();
        }

        public Variant MouseGetCursor()
        {
            throw new NotImplementedException();
        }

        public Variant MouseGetPos(Variant dimension = null)
        {
            throw new NotImplementedException();
        }

        public Variant MouseMove(Variant x, Variant y, Variant speed = null)
        {
            throw new NotImplementedException();
        }

        public Variant MouseUp(Variant button)
        {
            throw new NotImplementedException();
        }

        public Variant MouseWheel(Variant direction, Variant clicks = null)
        {
            throw new NotImplementedException();
        }

        public Variant MsgBox(Variant flag, Variant title, Variant text, Variant timeout = null, Variant hwnd = null)
        {
            throw new NotImplementedException();
        }

        public Variant Number(Variant expression, Variant flag = null)
        {
            throw new NotImplementedException();
        }

        public Variant ObjCreate(Variant classname, Variant servername = null, Variant username = null, Variant password = null)
        {
            throw new NotImplementedException();
        }

        public Variant ObjCreateInterface(Variant CLSID, Variant IID = null, Variant interfacedescription = null, Variant flag = null)
        {
            throw new NotImplementedException();
        }

        public Variant ObjEvent(Variant ObjectVar, Variant functionprefix = null, Variant interfacename = null)
        {
            throw new NotImplementedException();
        }


        public Variant ObjGet(Variant filename, Variant classname = null, Variant instance = null)
        {
            throw new NotImplementedException();
        }

        public Variant ObjName(Variant Objectvariable, Variant Flag = null)
        {
            throw new NotImplementedException();
        }

        public Variant OnAutoItExitRegister(Variant function)
        {
            throw new NotImplementedException();
        }

        public Variant OnAutoItExitUnRegister(Variant function)
        {
            throw new NotImplementedException();
        }

        public Variant Ping(Variant addresshostname, Variant timeout = null)
        {
            throw new NotImplementedException();
        }

        public Variant PixelChecksum(Variant left, Variant top, Variant right, Variant bottom, Variant step = null, Variant hwnd = null, Variant mode = null)
        {
            throw new NotImplementedException();
        }

        public Variant PixelGetColor(Variant x, Variant y, Variant hwnd = null)
        {
            throw new NotImplementedException();
        }

        public Variant PixelSearch(
            Variant left,
            Variant top,
            Variant right,
            Variant bottom,
            Variant color,
            Variant shadevariation = null,
            Variant step = null,
            Variant hwnd = null)
        {
            throw new NotImplementedException();
        }

        public Variant ProcessClose(Variant process)
        {
            throw new NotImplementedException();
        }

        public Variant ProcessExists(Variant process)
        {
            throw new NotImplementedException();
        }

        public Variant ProcessGetStats(Variant process = null, Variant type = null)
        {
            throw new NotImplementedException();
        }

        public Variant ProcessList(Variant name = null)
        {
            throw new NotImplementedException();
        }

        public Variant ProcessSetPriority(Variant process, Variant priority)
        {
            throw new NotImplementedException();
        }

        public Variant ProcessWait(Variant process, Variant timeout = null)
        {
            throw new NotImplementedException();
        }

        public Variant ProcessWaitClose(Variant process, Variant timeout = null)
        {
            throw new NotImplementedException();
        }

        public Variant ProgressOff()
        {
            throw new NotImplementedException();
        }

        public Variant ProgressOn(Variant title, Variant maintext, Variant subtext = null, Variant xpos = null, Variant ypos = null, Variant opt = null)
        {
            throw new NotImplementedException();
        }

        public Variant ProgressSet(Variant percent, Variant subtext = null, Variant maintext = null)
        {
            throw new NotImplementedException();
        }

        public Variant Ptr(Variant expression) {
            var intptr = Marshal.AllocHGlobal( Marshal.SizeOf( expression.GetValue() ) );
            Marshal.StructureToPtr( expression.GetValue(), intptr, true );
            return intptr;
        }

        public Variant Random(Variant Min = null, Variant Max = null, Variant Flag = null)
        {
            throw new NotImplementedException();
        }

        public Variant RegDelete(Variant keyname, Variant valuename = null)
        {
            throw new NotImplementedException();
        }

        public Variant RegEnumKey(Variant keyname, Variant instance)
        {
            throw new NotImplementedException();
        }

        public Variant RegEnumVal(Variant keyname, Variant instance)
        {
            throw new NotImplementedException();
        }

        public Variant RegRead(Variant keyname, Variant valuename)
        {
            throw new NotImplementedException();
        }

        public Variant RegWrite(Variant keyname, Variant valuename = null, Variant type = null, Variant value = null)
        {
            throw new NotImplementedException();
        }

        public Variant Round(Variant expression, Variant decimalplaces = null)
        {
            throw new NotImplementedException();
        }

        public Variant Run(Variant program, Variant workingdir = null, Variant showflag = null, Variant opt_flag = null)
        {
            throw new NotImplementedException();
        }

        public Variant RunAs(
            Variant username,
            Variant domain,
            Variant password,
            Variant logonflag,
            Variant program,
            Variant workingdir = null,
            Variant showflag = null,
            Variant optflag = null)
        {
            throw new NotImplementedException();
        }

        public Variant RunAsWait(
            Variant username,
            Variant domain,
            Variant password,
            Variant logonflag,
            Variant program,
            Variant workingdir = null,
            Variant show_flag = null,
            Variant opt_flag = null)
        {
            throw new NotImplementedException();
        }

        public Variant RunWait(Variant program, Variant workingdir = null, Variant showflag = null, Variant optflag = null)
        {
            throw new NotImplementedException();
        }

        public Variant SRandom(Variant Seed)
        {
            throw new NotImplementedException();
        }

        public Variant Send(Variant keys, Variant flag = null)
        {
            throw new NotImplementedException();
        }

        public Variant SendKeepActive(Variant title, Variant text = null)
        {
            throw new NotImplementedException();
        }

        public Variant SetError(Variant code, Variant extended = null, Variant returnvalue = null) {
            _context.Error = code;
            _context.Extended = extended;
            return returnvalue;
        }

        public Variant SetExtended(Variant code, Variant returnvalue = null)
        {
            throw new NotImplementedException();
        }

        public Variant ShellExecute(Variant filename, Variant parameters = null, Variant workingdir = null, Variant verb = null, Variant showflag = null)
        {
            throw new NotImplementedException();
        }

        public Variant ShellExecuteWait(Variant filename, Variant parameters = null, Variant workingdir = null, Variant verb = null, Variant showflag = null)
        {
            throw new NotImplementedException();
        }

        public Variant Shutdown(Variant code)
        {
            throw new NotImplementedException();
        }

        public Variant Sin(Variant expression)
        {
            throw new NotImplementedException();
        }

        public Variant Sleep(Variant delay)
        {
            throw new NotImplementedException();
        }

        public Variant SoundPlay(Variant filename, Variant wait = null)
        {
            throw new NotImplementedException();
        }

        public Variant SoundSetWaveVolume(Variant percent)
        {
            throw new NotImplementedException();
        }

        public Variant SplashImageOn(
            Variant title,
            Variant file,
            Variant width = null,
            Variant height = null,
            Variant xpos = null,
            Variant ypos = null,
            Variant opt = null)
        {
            throw new NotImplementedException();
        }

        public Variant SplashOff(Variant expression)
        {
            throw new NotImplementedException();
        }

        public Variant SplashTextOn(
            Variant title,
            Variant text,
            Variant w = null,
            Variant h = null,
            Variant xpos = null,
            Variant ypos = null,
            Variant opt = null,
            Variant fontname = null,
            Variant fontsz = null,
            Variant fontwt = null)
        {
            throw new NotImplementedException();
        }

        public Variant Sqrt(Variant expression)
        {
            throw new NotImplementedException();
        }

        public Variant StatusbarGetText(Variant title, Variant text = null, Variant part = null)
        {
            throw new NotImplementedException();
        }

        public Variant StderrRead(Variant process_id, Variant peek = null, Variant binary = null)
        {
            throw new NotImplementedException();
        }

        public Variant StdinWrite(Variant process_id, Variant data = null)
        {
            throw new NotImplementedException();
        }

        public Variant StdioClose(Variant process_id)
        {
            throw new NotImplementedException();
        }

        public Variant StdoutRead(Variant process_id, Variant peek = null, Variant binary = null)
        {
            throw new NotImplementedException();
        }

        public Variant String(Variant expression)
        {
            throw new NotImplementedException();
        }

        public Variant StringAddCR(Variant @string)
        {
            throw new NotImplementedException();
        }

        public Variant StringCompare(Variant string1, Variant string2, Variant casesense = null)
        {
            throw new NotImplementedException();
        }

        public Variant StringFormat(Variant formatcontrol, Variant var1, params Variant[] varN)
        {
            throw new NotImplementedException();
        }

        public Variant StringFromASCIIArray(Variant array, Variant start = null, Variant end = null, Variant encoding = null)
        {
            throw new NotImplementedException();
        }

        public Variant StringInStr(
            Variant @string,
            Variant substring,
            Variant casesense = null,
            Variant occurrence = null,
            Variant start = null,
            Variant count = null)
        {
            throw new NotImplementedException();
        }

        public Variant StringIsASCII(Variant @string)
        {
            throw new NotImplementedException();
        }

        public Variant StringIsAlNum(Variant @string)
        {
            throw new NotImplementedException();
        }

        public Variant StringIsAlpha(Variant @string)
        {
            throw new NotImplementedException();
        }

        public Variant StringIsDigit(Variant @string)
        {
            throw new NotImplementedException();
        }

        public Variant StringIsFloat(Variant @string)
        {
            throw new NotImplementedException();
        }

        public Variant StringIsInt(Variant @string)
        {
            throw new NotImplementedException();
        }

        public Variant StringIsLower(Variant @string)
        {
            throw new NotImplementedException();
        }

        public Variant StringIsSpace(Variant @string)
        {
            throw new NotImplementedException();
        }

        public Variant StringIsUpper(Variant @string)
        {
            throw new NotImplementedException();
        }

        public Variant StringIsXDigit(Variant @string)
        {
            throw new NotImplementedException();
        }

        public Variant StringLeft(Variant @string, Variant count)
        {
            throw new NotImplementedException();
        }

        public Variant StringLen(Variant @string) {
            return @string.ToString().Length;
        }

        public Variant StringLower(Variant @string)
        {
            throw new NotImplementedException();
        }

        public Variant StringMid(Variant @string, Variant start, Variant count = null)
        {
            throw new NotImplementedException();
        }

        public Variant StringRegExp(Variant test, Variant pattern, Variant flag = null, Variant offset = null)
        {
            throw new NotImplementedException();
        }

        public Variant StringRegExpReplace(Variant test, Variant pattern, Variant replace, Variant count = null)
        {
            throw new NotImplementedException();
        }

        public Variant StringReplace(Variant @string, Variant searchstringstart, Variant replacestring, Variant occurrence = null, Variant casesense = null)
        {
            throw new NotImplementedException();
        }

        public Variant StringReverse(Variant @string, Variant flag = null)
        {
            throw new NotImplementedException();
        }

        public Variant StringRight(Variant @string, Variant count)
        {
            throw new NotImplementedException();
        }

        public Variant StringSplit(Variant @string, Variant delimiters, Variant flag = null)
        {
            throw new NotImplementedException();
        }

        public Variant StringStripCR(Variant @string)
        {
            throw new NotImplementedException();
        }

        public Variant StringStripWS(Variant @string, Variant flag)
        {
            throw new NotImplementedException();
        }

        public Variant StringToASCIIArray(Variant @string, Variant start = null, Variant end = null, Variant encoding = null)
        {
            throw new NotImplementedException();
        }

        public Variant StringToBinary(Variant expression, Variant flag = null)
        {
            throw new NotImplementedException();
        }

        public Variant StringTrimLeft(Variant @string, Variant count)
        {
            throw new NotImplementedException();
        }

        public Variant StringTrimRight(Variant @string, Variant count)
        {
            throw new NotImplementedException();
        }

        public Variant StringUpper(Variant @string) {
            return @string.ToString().ToUpper();
        }

        public Variant TCPAccpet(Variant mainsocket)
        {
            throw new NotImplementedException();
        }

        public Variant TCPCloseSocket(Variant socket)
        {
            throw new NotImplementedException();
        }

        public Variant TCPConnect(Variant IPAddr, Variant port)
        {
            throw new NotImplementedException();
        }

        public Variant TCPListen(Variant IPAddr, Variant port, Variant MaxPendingConnection = null)
        {
            throw new NotImplementedException();
        }

        public Variant TCPNameToIP(Variant name)
        {
            throw new NotImplementedException();
        }

        public Variant TCPRecv(Variant mainsocket, Variant maxlen, Variant flag = null)
        {
            throw new NotImplementedException();
        }

        public Variant TCPSend(Variant mainsocket, Variant data)
        {
            throw new NotImplementedException();
        }

        public Variant TCPShutdown()
        {
            throw new NotImplementedException();
        }

        private Variant UDPShutdown()
        {
            throw new NotImplementedException();
        }

        public Variant TCPStartup()
        {
            throw new NotImplementedException();
        }

        private Variant UDPStartup()
        {
            throw new NotImplementedException();
        }

        public Variant Tan(Variant expression)
        {
            throw new NotImplementedException();
        }

        public Variant TimerDiff(Variant handle)
        {
            throw new NotImplementedException();
        }

        public Variant TimerInit()
        {
            throw new NotImplementedException();
        }

        public Variant ToolTip(Variant text, Variant x = null, Variant y = null, Variant title = null, Variant icon = null, Variant options = null)
        {
            throw new NotImplementedException();
        }

        public Variant TrayCreateItem(Variant text, Variant menuID = null, Variant menuentry = null, Variant menuradioitem = null)
        {
            throw new NotImplementedException();
        }

        public Variant TrayCreateMenu(Variant submenutext, Variant menuID = null, Variant menuentry = null)
        {
            throw new NotImplementedException();
        }

        public Variant TrayGetMsg()
        {
            throw new NotImplementedException();
        }

        public Variant TrayItemDelete(Variant controlID)
        {
            throw new NotImplementedException();
        }

        public Variant TrayItemGetHandle(Variant controlID)
        {
            throw new NotImplementedException();
        }

        public Variant TrayItemGetState(Variant controlID = null)
        {
            throw new NotImplementedException();
        }

        public Variant TrayItemGetText(Variant controlID)
        {
            throw new NotImplementedException();
        }

        public Variant TrayItemSetOnEvent(Variant itemID, Variant function)
        {
            throw new NotImplementedException();
        }

        public Variant TrayItemSetState(Variant controlID, Variant state)
        {
            throw new NotImplementedException();
        }

        public Variant TrayItemSetText(Variant controlID, Variant text)
        {
            throw new NotImplementedException();
        }

        public Variant TraySetClick(Variant flag)
        {
            throw new NotImplementedException();
        }

        public Variant TraySetIcon(Variant filename = null, Variant iconID = null)
        {
            throw new NotImplementedException();
        }

        public Variant TraySetOnEvent(Variant specialID, Variant function)
        {
            throw new NotImplementedException();
        }

        public Variant TraySetPauseIcon(Variant filename = null, Variant iconID = null)
        {
            throw new NotImplementedException();
        }

        public Variant TraySetState(Variant flag = null)
        {
            throw new NotImplementedException();
        }

        public Variant TraySetToolTip(Variant text = null)
        {
            throw new NotImplementedException();
        }

        public Variant TrayTip(Variant title, Variant text, Variant timeout, Variant option = null)
        {
            throw new NotImplementedException();
        }

        public Variant UBound(Variant Array, Variant Dimension = null) {
            if ( Dimension == null ) {
                return ( (Array) Array.GetValue() ).Length;
            }
            throw new NotImplementedException();
        }

        public Variant UDPBind(Variant IPAddr, Variant port)
        {
            throw new NotImplementedException();
        }

        public Variant UDPCloseSocket(Variant socketarray)
        {
            throw new NotImplementedException();
        }

        public Variant UDPOpen(Variant IPAddr, Variant port, Variant flag = null)
        {
            throw new NotImplementedException();
        }

        public Variant UDPRecv(Variant socketarray, Variant maxlen, Variant flag = null)
        {
            throw new NotImplementedException();
        }

        public Variant UDPSend(Variant socketarray, Variant data)
        {
            throw new NotImplementedException();
        }

        public Variant
            VarGetType(Variant expression)
        {
            throw new NotImplementedException();
        }

        public Variant WinActivate(Variant title, Variant text = null)
        {
            throw new NotImplementedException();
        }

        public Variant WinActive(Variant title, Variant text = null)
        {
            throw new NotImplementedException();
        }

        public Variant WinClose(Variant title, Variant text = null)
        {
            throw new NotImplementedException();
        }

        public Variant WinExists(Variant title, Variant text = null)
        {
            throw new NotImplementedException();
        }

        public Variant WinFlash(Variant title, Variant text = null, Variant flashes = null, Variant delay = null)
        {
            throw new NotImplementedException();
        }

        public Variant WinGetCaretPos()
        {
            throw new NotImplementedException();
        }

        public Variant WinGetClassList(Variant title, Variant text = null)
        {
            throw new NotImplementedException();
        }

        public Variant WinGetClientSize(Variant title, Variant text = null)
        {
            throw new NotImplementedException();
        }

        public Variant WinGetHandle(Variant title, Variant text = null)
        {
            throw new NotImplementedException();
        }

        public Variant WinGetPos(Variant title, Variant text = null)
        {
            throw new NotImplementedException();
        }

        public Variant WinGetProcess(Variant title, Variant text = null)
        {
            throw new NotImplementedException();
        }

        public Variant WinGetState(Variant title, Variant text = null)
        {
            throw new NotImplementedException();
        }

        public Variant WinGetText(Variant title, Variant text = null)
        {
            throw new NotImplementedException();
        }

        public Variant WinGetTitle(Variant title, Variant text = null)
        {
            throw new NotImplementedException();
        }

        public Variant WinKill(Variant title, Variant text = null)
        {
            throw new NotImplementedException();
        }

        public Variant WinList(Variant title = null, Variant text = null)
        {
            throw new NotImplementedException();
        }

        public Variant WinMenuSelectItem(Variant title, Variant text, params Variant[] items)
        {
            throw new NotImplementedException();
        }

        public Variant WinMinimizeAll()
        {
            throw new NotImplementedException();
        }

        public Variant WinMinimizeAllUndo()
        {
            throw new NotImplementedException();
        }

        public Variant WinMove(Variant title, Variant text, Variant x, Variant y, Variant width = null, Variant height = null, Variant speed = null)
        {
            throw new NotImplementedException();
        }

        public Variant WinSetOnTop(Variant title, Variant text, Variant flag)
        {
            throw new NotImplementedException();
        }

        public Variant WinSetState(Variant title, Variant text, Variant flag)
        {
            throw new NotImplementedException();
        }

        public Variant WinSetTitle(Variant title, Variant text, Variant newtitle)
        {
            throw new NotImplementedException();
        }

        public Variant WinSetTrans(Variant title, Variant text, Variant transparency)
        {
            throw new NotImplementedException();
        }

        public Variant WinWait(Variant title, Variant text = null, Variant timeout = null)
        {
            throw new NotImplementedException();
        }

        public Variant WinWaitActive(Variant title, Variant text = null, Variant timeout = null)
        {
            throw new NotImplementedException();
        }

        public Variant WinWaitClose(Variant title, Variant text = null, Variant timeout = null)
        {
            throw new NotImplementedException();
        }

        public Variant WinWaitNotActive(Variant title, Variant text = null, Variant timeout = null)
        {
            throw new NotImplementedException();
        }


        
        [PreExecutable]
        [AutoJITCompilerFunction]
        public Variant Pow(Variant a, Variant b ) {
            return Math.Pow(a, b);
        }

        [PreExecutable]
        [AutoJITCompilerFunction]
        public Variant Equal(Variant a, Variant b)
        {
            if ( a.IsString ||
                 b.IsString ) {
                return string.Compare( a.ToString(), b.ToString(), StringComparison.InvariantCultureIgnoreCase ) == 0;
            }
            return a == b;
        }

        [PreExecutable]
        [AutoJITCompilerFunction]
        public Variant EqualString(Variant a, Variant b ) {
            return a.ToString() == b.ToString();
        }


        [PreExecutable]
        [AutoJITCompilerFunction]
        public Variant NOT(Variant variable)
        {
            return !variable;
        }

        [PreExecutable]
        [AutoJITCompilerFunction]
        public Variant OR(Variant a, Variant b) {
            return a || b;
        }

        [PreExecutable]
        [AutoJITCompilerFunction]
        public Variant AND(Variant a, Variant b) {
            return a && b;
        }

        [PreExecutable]
        [AutoJITCompilerFunction]
        public Variant Negate(Variant o) {
            return -o;
        }

        [AutoJITCompilerFunction]
        public bool To( Variant from, Variant to, Variant toTest ) {
            return toTest >= @from && toTest <= to;
        }

        [AutoJITCompilerFunction]
        public void Exit( Variant exitCode = null) {
            if ( exitCode == null ) {
                Environment.Exit( 0 );
            }
            Environment.Exit( (int)exitCode );
        }

        public Variant Concat(Variant a, Variant b) {
            return a.ToString()+b.ToString();
        }
    }
}
