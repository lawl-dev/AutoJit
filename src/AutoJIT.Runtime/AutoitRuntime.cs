using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using AutoJITRuntime.Attrubutes;
using AutoJITRuntime.Exceptions;
using AutoJITRuntime.Variants;
using Lawl.Reflection;
using Microsoft.Win32.SafeHandles;

namespace AutoJITRuntime
{
    public class AutoitRuntime<T>
    {
        private readonly AutoitContext<T> _context;
        private readonly Dictionary<string, MethodInfo> _methodStore;
        private readonly MarshalBridge _marshalBridge;

        public AutoitRuntime( AutoitContext<T> context ) {
            _context = context;
            var methodInfos = GetType().GetMethods();
            _methodStore = methodInfos.ToDictionary( x => x.Name, x => x );
            _marshalBridge = new MarshalBridge();
        }

        [Inlineable]
        public Variant ACos( Variant expression ) {
            return Math.Acos( expression );
        }

        [Inlineable]
        public Variant ASin( Variant expression ) {
            return Math.Asin( expression );
        }

        [Inlineable]
        public Variant ATan( Variant expression ) {
            return Math.Atan( expression );
        }

        [Inlineable]
        public Variant Abs( Variant expression ) {
            return Math.Abs( expression );
        }

        public Variant AdlibRegister( Variant function, Variant time = null ) {
            if ( time == null ) {
                time = 250;
            }
            if ( !_methodStore.ContainsKey( function ) ) {
                return false;
            }
            if ( _methodStore[function].GetParameters().Any() ) {
                return false;
            }
            _context.LibRegister.Add( _methodStore[function], time );
            return true;
        }

        public Variant AdlibUnRegister( Variant function = null ) {
            if ( function == null ) {
                if ( _context.LibRegister.Any() ) {
                    _context.LibRegister.Remove( _context.LibRegister.Last().Key );
                    return _context.LibRegister.Count;
                }
                return 0;
            }
            var valuePair = _context.LibRegister.SingleOrDefault( x => x.Key.Name.Equals( function, StringComparison.InvariantCultureIgnoreCase ) );
            if ( valuePair.Key != null ) {
                _context.LibRegister.Remove( valuePair.Key );
            }
            return _context.LibRegister.Count;
        }

        [Inlineable]
        public Variant Asc( Variant @char ) {
            var character = ( (string) @char ).FirstOrDefault();
            return character;
        }

        public Variant AscW( Variant @char ) {
            var character = ( (string) @char ).FirstOrDefault();
            return character;
        }

        public Variant Assign( Variant varname, Variant data, Variant flag = null ) {
            throw new NotSupportedException( "Function 'Assign' is not supported" );
        }

        public Variant Opt( Variant option, Variant param = null ) {
            return AutoItSetOption( option, param );
        }

        public Variant AutoItSetOption( Variant option, Variant param = null ) {
            throw new NotImplementedException();
        }

        public Variant AutoItWinGetTitle() {
            throw new NotImplementedException();
        }

        public Variant AutoItWinSetTitle( Variant newtitle ) {
            throw new NotImplementedException();
        }

        public Variant Beep( Variant Frequency = null, Variant Duration = null ) {
            if ( Frequency == null ) {
                Frequency = 500;
            }

            if ( Duration == null ) {
                Duration = 1000;
            }

            Console.Beep( Frequency, Duration );

            return 1;
        }

        public Variant Binary( Variant expression ) {
            return expression.GetBinary();
        }

        public Variant BinaryLen( Variant binary ) {
            return binary.GetBinary().Length;
        }

        public Variant BinaryMid( Variant binary, Variant start, Variant count = null ) {
            var bytes = binary.GetBinary();
            if ( start < 1 ||
                 start >= bytes.Length ||
                 ( count != null && start >= count ) ) {
                return new byte[0];
            }
            return bytes.Skip( start-1 ).Take( count ?? bytes.Length-start-1 ).ToArray();
        }

        public Variant BinaryToString( Variant expression, Variant flag = null ) {
            if ( flag == null ) {
                flag = 1;
            }

            var @string = expression.GetString();

            if ( @string.Length % 2 != 0 ) {
                return SetError( 2, null, string.Empty );
            }
            if ( @string.Length == 0 ) {
                return SetError( 1, null, string.Empty );
            }

            Encoding encoding;
            switch (flag.GetInt()) {
                case 1:
                    encoding = Encoding.Default;
                    break;
                case 2:
                    encoding = Encoding.Unicode;
                    break;
                case 3:
                    encoding = Encoding.BigEndianUnicode;
                    break;
                default:
                    encoding = Encoding.UTF8;
                    break;
            }

            if ( @string.StartsWith( "0x", StringComparison.InvariantCultureIgnoreCase ) &&
                 @string.Skip( 2 ).All(
                     c => ( c >= '0' && c <= '9' ) ||
                          ( c >= 'a' && c <= 'f' ) ||
                          ( c >= 'A' && c <= 'F' ) ) ) {
                var hex = @string.Substring( 2, @string.Length-2 );
                var raw = new byte[hex.Length / 2];
                for ( int i = 0; i < raw.Length; i++ ) {
                    raw[i] = Convert.ToByte( hex.Substring( i * 2, 2 ), 16 );
                }
                return encoding.GetString( raw );
            }

            return encoding.GetString( expression.GetBinary() );
        }

        [Inlineable]
        public Variant BitAND( Variant value1, Variant value2, params Variant[] valuen ) {
            var res = (int) value1&(int) value2;
            foreach (var variant in valuen) {
                res &= variant;
            }
            return res;
        }

        [Inlineable]
        public Variant BitNOT( Variant value ) {
            return ~value;
        }

        [Inlineable]
        public Variant BitOR( Variant value1, Variant value2, params Variant[] valuen ) {
            var res = (int) value1|(int) value2;
            foreach (var variant in valuen) {
                res |= variant;
            }
            return res;
        }

        public Variant BitRotate( Variant value, Variant shift = null, Variant size = null ) {
            throw new NotImplementedException();
        }

        [Inlineable]
        public Variant BitShift( Variant value, Variant shift ) {
            if ( shift > 0 ) {
                return value >> shift.GetInt();
            }
            return value << -shift.GetInt();
        }

        [Inlineable]
        public Variant BitXOR( Variant value1, Variant value2, params Variant[] valuen ) {
            var res = (int) value1^(int) value2;
            foreach (var variant in valuen) {
                res ^= variant;
            }
            return res;
        }

        public Variant BlockInput( Variant flag ) {
            throw new NotImplementedException();
        }

        public Variant Break( Variant mode ) {
            throw new NotImplementedException();
        }

        public Variant CDTray( Variant drive, Variant status ) {
            throw new NotImplementedException();
        }

        public Variant Call( Variant function, params Variant[] paramsN ) {
            if ( _methodStore.ContainsKey( function ) ) {
                return Variant.Create( _methodStore[function].Invoke( this, paramsN ) );
            }
            if ( _context.MethodStore.ContainsKey( function ) ) {
                return Variant.Create( _context.MethodStore[function].Invoke( _context.Context, paramsN ) );
            }
            return SetError( 0xDEAD, 0xBEEF, 0 );
        }

        [Inlineable]
        public Variant Ceiling( Variant expression ) {
            return Math.Ceiling( (double) expression );
        }

        [Inlineable]
        public Variant Chr( Variant ASCIIcode ) {
            if ( ASCIIcode < 0 ) {
                return string.Empty;
            }
            if ( ASCIIcode > 255 ) {
                return SetError( 1, null, string.Empty );
            }

            return ( (char) ASCIIcode ).ToString( CultureInfo.InvariantCulture );
        }

        [Inlineable]
        public Variant ChrW( Variant UNICODEcode ) {
            if ( UNICODEcode > ushort.MaxValue ) {
                return SetError( UNICODEcode, null, string.Empty );
            }

            if ( UNICODEcode < 0 ) {
                return string.Empty;
            }

            return (char) UNICODEcode;
        }

        public Variant ClipGet() {
            throw new NotImplementedException();
        }

        public Variant ClipPut( Variant value ) {
            throw new NotImplementedException();
        }

        public Variant ConsoleRead( Variant peek = null, Variant binary = null ) {
            if ( peek == null ) {
                peek = false;
            }

            if ( binary == null ) {
                binary = false;
            }

            int read;
            if ( peek ) {
                read = Console.In.Peek();
            }
            else {
                read = Console.Read();
            }

            if ( binary ) {
                return new byte[] { (byte) read };
            }
            return new String( new[] { (char) read } );
        }

        public Variant ConsoleWrite( Variant data ) {
            var @string = data.GetString();

            Console.Write( @string );
            return @string.Length;
        }

        public Variant ConsoleWriteError( Variant data ) {
            if ( data.IsBinary ) {
                var binary = data.GetBinary();
                Console.Error.Write( binary.Select( x => (char) x ).ToArray() );
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
            Variant y = null ) {
            throw new NotImplementedException();
        }

        public Variant ControlCommand( Variant title, Variant text, Variant controlID, Variant command, Variant option = null ) {
            throw new NotImplementedException();
        }

        public Variant ControlDisable( Variant title, Variant text, Variant controlID ) {
            throw new NotImplementedException();
        }

        public Variant ControlEnable( Variant title, Variant text, Variant controlID ) {
            throw new NotImplementedException();
        }

        public Variant ControlFocus( Variant title, Variant text, Variant controlID ) {
            throw new NotImplementedException();
        }

        public Variant ControlGetFocus( Variant title, Variant text = null ) {
            throw new NotImplementedException();
        }

        public Variant ControlGetHandle( Variant title, Variant text, Variant controlID ) {
            throw new NotImplementedException();
        }

        public Variant ControlGetPos( Variant title, Variant text, Variant controlID ) {
            throw new NotImplementedException();
        }

        public Variant ControlGetText( Variant title, Variant text, Variant controlID ) {
            throw new NotImplementedException();
        }

        public Variant ControlHide( Variant title, Variant text, Variant controlID ) {
            throw new NotImplementedException();
        }

        public Variant ControlListView( Variant title, Variant text, Variant controlID, Variant command, params Variant[] optionN ) {
            throw new NotImplementedException();
        }

        public Variant ControlMove( Variant title, Variant text, Variant controlID, Variant x, Variant y, Variant width = null, Variant height = null ) {
            throw new NotImplementedException();
        }

        public Variant ControlSend( Variant title, Variant text, Variant controlID, Variant @string, Variant flag = null ) {
            throw new NotImplementedException();
        }

        public Variant ControlSetText( Variant title, Variant text, Variant controlID, Variant newtext, Variant flag = null ) {
            throw new NotImplementedException();
        }

        public Variant ControlShow( Variant title, Variant text, Variant controlID ) {
            throw new NotImplementedException();
        }

        public Variant ControlTreeView( Variant title, Variant text, Variant controlID, Variant command, Variant option1 = null ) {
            throw new NotImplementedException();
        }

        [Inlineable]
        public Variant Cos( Variant expression ) {
            return Math.Cos( expression );
        }

        [Inlineable]
        public Variant Dec( Variant hex, Variant flag = null ) {
            Int64 result;
            if ( Int64.TryParse( hex, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out result ) ) {
                if ( result <= int.MaxValue &&
                     result >= int.MinValue ) {
                    return (int) result;
                }
                return result;
            }
            return SetError( 1, null, 0 );
        }

        public Variant DirCopy( Variant sourcedir, Variant destdir, Variant flag = null ) {
            throw new NotImplementedException();
        }

        public Variant DirCreate( Variant path ) {
            throw new NotImplementedException();
        }

        public Variant DirGetSize( Variant path, Variant flag = null ) {
            throw new NotImplementedException();
        }

        public Variant DirMove( Variant sourcedir, Variant destdir, Variant flag = null ) {
            throw new NotImplementedException();
        }

        public Variant DirRemove( Variant path, Variant recurse = null ) {
            throw new NotImplementedException();
        }

        public Variant DllCall( Variant dll, Variant returntype, Variant function, params Variant[] paramtypen ) {
            
            
            if ( dll.IsPtr ) {
                return DllCallInternal( dll.GetIntPtr(), returntype.GetString(), function.GetString(), paramtypen );
            }

            var handle = DllOpen( dll );
            
            if ( handle == -1 ) {
                return SetError( 1, null, 0 );
            }

            var result = DllCallInternal(handle.GetIntPtr(), returntype.GetString(), function.GetString(), paramtypen);

            if ( result.IsInt32 ) {
                return result;
            }

            DllClose( handle );
            return result;
        }

        private Variant DllCallInternal( IntPtr dll, string returntype, string function, Variant[] paramtypen ) {
            try {
                return _marshalBridge.DllCall( dll, returntype, function, paramtypen );
            }
            catch (ProcAddressZeroException) {
                return SetError( 1, null, 0 );
            }
            catch (BadReturnTypeException) {
                return SetError( 2, null, 0 );
            }
            catch (BadNumberOfParameterException) {
                return SetError( 4, null, 0 );
            }
            catch (BadParameterException) {
                return SetError( 5, null, 0 );
            }
        }

        public Variant DllCallAddress( Variant returntype, Variant address, params Variant[] paramtypen ) {
            throw new NotImplementedException();
        }

        public Variant DllCallbackFree( Variant handle ) {
            throw new NotImplementedException();
        }

        public Variant DllCallbackGetPtr( Variant handle ) {
            throw new NotImplementedException();
        }

        public Variant DllCallbackRegister( Variant function, Variant returntype, params Variant[] @params ) {
            throw new NotImplementedException();
        }

        public Variant DllClose( Variant dllhandle ) {
            MarshalBridge.FreeLibrary( dllhandle );
            return 0;
        }

        public Variant DllOpen( Variant filename ) {
            try {
                var library = MarshalBridge.LoadLibrary( filename.GetString() );
                if ( library == IntPtr.Zero ) {
                    var error = Marshal.GetLastWin32Error();
                }
                return library;
            }
            catch (Exception) {
                return -1;
            }
        }

        public Variant DllStructCreate( Variant Struct, Variant Pointer = null ) {
            var runtimeStruct = _marshalBridge.CreateRuntimeStruct( Struct.GetString() );

            var instance = (IRuntimeStruct) runtimeStruct.CreateInstance<object>();

            var @struct = (StructVariant)Variant.Create(instance);

            if ( Pointer != null ) {
                @struct.InitUnmanaged(Pointer.GetIntPtr());
            }

            return @struct;
        }

        public Variant DllStructGetData( Variant Struct, Variant Element, Variant index = null ) {
            if ( index != null ) {
                throw new NotImplementedException();
            }
            var structVariant = Struct as StructVariant;
            if ( structVariant == null ) {
                return SetError( 1, null, 0 );
            }

            return Variant.Create( structVariant.GetElement( Element.GetInt()-1 ) );
        }

        public Variant DllStructGetPtr( Variant Struct, Variant Element = null ) {
            var structVariant = Struct as StructVariant;
            if ( structVariant == null ) {
                return SetError( 1, null, 0 );
            }

            if ( Element != null ) {
                throw new NotImplementedException();
            }

            structVariant.InitUnmanaged();
            return structVariant.Ptr;
        }

        public Variant DllStructGetSize( Variant Struct ) {
            var runtimeStruct = Struct.GetValue() as IRuntimeStruct;
            if ( runtimeStruct == null ) {
                return SetError( 1, null, 0 );
            }

            return Marshal.SizeOf( runtimeStruct );
        }

        public Variant DllStructSetData( Variant Struct, Variant Element, Variant value, Variant index = null ) {
            if ( index == null ) {
                index = 1;
            }

            var runtimeStruct = Struct as StructVariant;
            if ( runtimeStruct == null ) {
                return SetError( 1, null, 0 );
            }

            if ( Element.IsInt32 ) {
                var val = value.GetValue();
                if ( val is IEnumerable ) {
                    var element = runtimeStruct.GetElement( Element.GetInt()-1 );
                    if ( element is Array ) {
                        int i = index.GetInt()-1;
                        foreach (var o in (IEnumerable) val) {
                            var array = ( (Array) element );
                            var elementType = array.GetType().GetElementType();
                            array.SetValue( Convert.ChangeType( o, elementType ), i );
                            i++;
                        }
                        runtimeStruct.SetElement( Element.GetInt()-1, element );
                        return Variant.Create( runtimeStruct.GetElement( Element.GetInt()-1 ) );
                    }
                }
                else {
                    runtimeStruct.SetElement( Element.GetInt()-1, value.GetValue() );
                    return Variant.Create( runtimeStruct.GetElement( Element.GetInt()-1 ) );
                }
            }
            throw new NotImplementedException();
        }

        public Variant DriveGetDrive( Variant type ) {
            throw new NotImplementedException();
        }

        public Variant DriveGetFileSystem( Variant path ) {
            throw new NotImplementedException();
        }

        public Variant DriveGetLabel( Variant path ) {
            throw new NotImplementedException();
        }

        public Variant DriveGetSerial( Variant path ) {
            throw new NotImplementedException();
        }

        public Variant DriveGetType( Variant path, Variant operation = null ) {
            throw new NotImplementedException();
        }

        public Variant DriveMapAdd( Variant device, Variant remoteshare, Variant flags = null, Variant user = null, Variant password = null ) {
            throw new NotImplementedException();
        }

        public Variant DriveMapDel( Variant drive ) {
            throw new NotImplementedException();
        }

        public Variant DriveMapGet( Variant device ) {
            throw new NotImplementedException();
        }

        public Variant DriveSetLabel( Variant path, Variant label ) {
            throw new NotImplementedException();
        }

        public Variant DriveSpaceFree( Variant path ) {
            throw new NotImplementedException();
        }

        public Variant DriveSpaceTotal( Variant path ) {
            throw new NotImplementedException();
        }

        public Variant DriveStatus( Variant path ) {
            throw new NotImplementedException();
        }

        public Variant EnvGet( Variant envvariable ) {
            throw new NotImplementedException();
        }

        public Variant EnvSet( Variant envvariable, Variant value = null ) {
            throw new NotImplementedException();
        }

        public Variant EnvUpdate() {
            throw new NotImplementedException();
        }

        public Variant Eval( Variant @string ) {
            throw new NotSupportedException( "Function 'Eval' is not supported" );
        }

        public Variant Execute( Variant @string ) {
            throw new NotSupportedException( "Function 'Execute' is not supported" );
        }

        [Inlineable]
        public Variant Exp( Variant expression ) {
            return Math.Exp( expression );
        }

        public Variant FileChangeDir( Variant path ) {
            throw new NotImplementedException();
        }

        public Variant FileClose( Variant filehandle ) {
            throw new NotImplementedException();
        }

        public Variant FileCopy( Variant source, Variant dest, Variant flag = null ) {
            throw new NotImplementedException();
        }

        public Variant FileCreateNTFSLink( Variant source, Variant hardlink, Variant flag = null ) {
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
            Variant state = null ) {
            throw new NotImplementedException();
        }

        public Variant FileDelete( Variant filename ) {
            var fragments = filename.GetString().Split( '\\' );
            var dir = string.Join( "\\", fragments.Take( fragments.Length-1 ) );
            var toDelete = Directory.GetFiles( dir, fragments.Last() );
            if ( !toDelete.Any() ) {
                return 0;
            }

            try {
                foreach (var file in toDelete) {
                    File.Delete( file );
                }
            }
            catch (Exception) {
                return 0;
            }
            return 1;
        }

        public Variant FileExists( Variant path ) {
            throw new NotImplementedException();
        }

        public Variant FileFindFirstFile( Variant filename ) {
            throw new NotImplementedException();
        }

        public Variant FileFindNextFile( Variant search ) {
            throw new NotImplementedException();
        }

        public Variant FileFlush( Variant filehandle ) {
            throw new NotImplementedException();
        }

        public Variant FileGetAttrib( Variant filename ) {
            throw new NotImplementedException();
        }

        public Variant FileGetEncoding( Variant filehandlefilename, Variant mode = null ) {
            throw new NotImplementedException();
        }

        public Variant FileGetLongName( Variant filename, Variant flag = null ) {
            throw new NotImplementedException();
        }

        public Variant FileGetPos( Variant filehandle ) {
            throw new NotImplementedException();
        }

        public Variant FileGetShortName( Variant filename, Variant flag = null ) {
            throw new NotImplementedException();
        }

        public Variant FileGetShortcut( Variant lnk ) {
            throw new NotImplementedException();
        }

        public Variant FileGetSize( Variant filename ) {
            throw new NotImplementedException();
        }

        public Variant FileGetTime( Variant filename, Variant option = null, Variant format = null ) {
            throw new NotImplementedException();
        }

        public Variant FileGetVersion( Variant filename, Variant stringname = null ) {
            throw new NotImplementedException();
        }

        public Variant FileInstall( Variant source, Variant dest, Variant flag = null ) {
            throw new NotImplementedException();
        }

        public Variant FileMove( Variant source, Variant dest, Variant flag = null ) {
            throw new NotImplementedException();
        }

        public Variant FileOpen( Variant filename, Variant mode = null ) {
            throw new NotImplementedException();
        }

        public Variant FileOpenDialog( Variant title, Variant initdir, Variant filter, Variant options = null, Variant defaultname = null, Variant hwnd = null ) {
            throw new NotImplementedException();
        }

        public Variant FileRead( Variant filehandlefilename, Variant count = null ) {
            if ( count == null ) {
                count = -1;
            }

            Stream fileStream;
            if ( filehandlefilename.IsPtr ) {
                var fileHandle = new SafeFileHandle( filehandlefilename.GetIntPtr(), false );
                fileStream = new FileStream( fileHandle, FileAccess.Read );
            }
            else {
                fileStream = new FileStream( filehandlefilename.GetString(), FileMode.Open );
            }

            var streamReader = new StreamReader( fileStream );
            if ( count < 0 ) {
                var readToEnd = streamReader.ReadToEnd();
                if ( !filehandlefilename.IsPtr ) {
                    streamReader.Close();
                }

                return SetExtended( readToEnd.Length, readToEnd );
            }

            var buffer = new char[count];
            var read = streamReader.Read( buffer, (int) fileStream.Position, count );
            streamReader.Close();
            return SetExtended( read, new StringVariant( new string( buffer ) ) );
        }

        public Variant FileReadLine( Variant filehandlefilename, Variant line = null ) {
            throw new NotImplementedException();
        }

        public Variant FileReadToArray( Variant filehandlefilename ) {
            throw new NotImplementedException();
        }

        public Variant FileRecycle( Variant source ) {
            throw new NotImplementedException();
        }

        public Variant FileRecycleEmpty( Variant source ) {
            throw new NotImplementedException();
        }

        public Variant FileSaveDialog( Variant title, Variant initdir, Variant filter, Variant options = null, Variant defaultname = null, Variant hwnd = null ) {
            throw new NotImplementedException();
        }

        public Variant FileSelectFolder( Variant dialogtext, Variant rootdir, Variant flag = null, Variant initialdir = null, Variant hwnd = null ) {
            throw new NotImplementedException();
        }

        public Variant FileSetAttrib( Variant filepattern, Variant RASHNOT, Variant recurse = null ) {
            throw new NotImplementedException();
        }

        public Variant FileSetPos( Variant filehandle, Variant offset, Variant origin ) {
            throw new NotImplementedException();
        }

        public Variant FileSetTime( Variant filepattern, Variant time, Variant type = null, Variant recurse = null ) {
            throw new NotImplementedException();
        }

        public Variant FileWrite( Variant filehandlefilename, Variant textdata ) {
            try {
                FileStream fileStream;
                if ( filehandlefilename.IsPtr ) {
                    fileStream = new FileStream( new SafeFileHandle( filehandlefilename, true ), FileAccess.Write );
                }
                else {
                    fileStream = new FileStream( filehandlefilename.GetString(), FileMode.Open, FileAccess.Write );
                }
                fileStream.Write( textdata.GetBinary(), 0, textdata.GetBinary().Length );
            }
            catch (Exception) {
                return false;
            }
            return true;
        }

        public Variant FileWriteLine( Variant filehandlefilename, Variant line ) {
            throw new NotImplementedException();
        }

        [Inlineable]
        public Variant Floor( Variant expression ) {
            return Math.Floor( (double) expression );
        }

        public Variant FtpSetProxy( Variant mode = null, Variant proxyport = null, Variant username = null, Variant password = null ) {
            throw new NotImplementedException();
        }

        public Variant FuncName( Variant Functionvariable ) {
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
            Variant parent = null ) {
            if ( width == null ) {
                width = 0;
            }

            if ( height == null ) {
                height = 0;
            }

            if ( left == null ) {
                left = -1;
            }

            if ( top == null ) {
                top = -1;
            }

            if ( style == null ) {
                style = -1;
            }

            if ( exStyle == null ) {
                exStyle = -1;
            }

            if ( parent == null ) {
                parent = -1;
            }

            var form = new Form() { Text = title, Width = width, Height = height, Left = left, Top = top, Visible = false };
            var parentHandle = Form.FromHandle( new IntPtr( parent ) );
            if ( parentHandle != null ) {
                form.Parent = parentHandle;
            }

            Task.Factory.StartNew( form.Show );

            _context.Guis.Add( form.Handle );
            return form.Handle;
        }

        public Variant GUICtrlCreateAvi(
            Variant filename,
            Variant subfileid,
            Variant left,
            Variant top,
            Variant width = null,
            Variant height = null,
            Variant style = null,
            Variant exStyle = null ) {
            throw new NotImplementedException();
        }

        public Variant GUICtrlCreateButton(
            Variant text,
            Variant left,
            Variant top,
            Variant width = null,
            Variant height = null,
            Variant style = null,
            Variant exStyle = null ) {
            throw new NotImplementedException();
        }

        public Variant GUICtrlCreateCheckbox(
            Variant text,
            Variant left,
            Variant top,
            Variant width = null,
            Variant height = null,
            Variant style = null,
            Variant exStyle = null ) {
            throw new NotImplementedException();
        }

        public Variant GUICtrlCreateCombo(
            Variant text,
            Variant left,
            Variant top,
            Variant width = null,
            Variant height = null,
            Variant style = null,
            Variant exStyle = null ) {
            throw new NotImplementedException();
        }

        public Variant GUICtrlCreateContextMenu( Variant controlID ) {
            throw new NotImplementedException();
        }

        public Variant GUICtrlCreateDate(
            Variant text,
            Variant left,
            Variant top,
            Variant width = null,
            Variant height = null,
            Variant style = null,
            Variant exStyle = null ) {
            throw new NotImplementedException();
        }

        public Variant GUICtrlCreateDummy( Variant d ) {
            throw new NotImplementedException();
        }

        public Variant GUICtrlCreateEdit(
            Variant text,
            Variant left,
            Variant top,
            Variant width = null,
            Variant height = null,
            Variant style = null,
            Variant exStyle = null ) {
            throw new NotImplementedException();
        }

        public Variant GUICtrlCreateGraphic( Variant left, Variant top, Variant width = null, Variant height = null, Variant style = null ) {
            throw new NotImplementedException();
        }

        public Variant GUICtrlCreateGroup(
            Variant text,
            Variant left,
            Variant top = null,
            Variant width = null,
            Variant height = null,
            Variant style = null,
            Variant exStyle = null ) {
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
            Variant exStyle = null ) {
            throw new NotImplementedException();
        }

        public Variant GUICtrlCreateInput(
            Variant text,
            Variant left,
            Variant top,
            Variant width = null,
            Variant height = null,
            Variant style = null,
            Variant exStyle = null ) {
            throw new NotImplementedException();
        }

        public Variant GUICtrlCreateLabel(
            Variant text,
            Variant left,
            Variant top,
            Variant width = null,
            Variant height = null,
            Variant style = null,
            Variant exStyle = null ) {
            throw new NotImplementedException();
        }

        public Variant GUICtrlCreateList(
            Variant text,
            Variant left,
            Variant top,
            Variant width = null,
            Variant height = null,
            Variant style = null,
            Variant exStyle = null ) {
            throw new NotImplementedException();
        }

        public Variant GUICtrlCreateListView(
            Variant text,
            Variant left,
            Variant top,
            Variant width = null,
            Variant height = null,
            Variant style = null,
            Variant exStyle = null ) {
            throw new NotImplementedException();
        }

        public Variant GUICtrlCreateListViewItem( Variant text, Variant listviewID ) {
            throw new NotImplementedException();
        }

        public Variant GUICtrlCreateMenu( Variant submenutext, Variant menuID = null, Variant menuentry = null ) {
            throw new NotImplementedException();
        }

        public Variant GUICtrlCreateMenuItem( Variant text, Variant menuID, Variant menuentry = null, Variant menuradioitem = null ) {
            throw new NotImplementedException();
        }

        public Variant GUICtrlCreateMonthCal(
            Variant text,
            Variant left,
            Variant top,
            Variant width = null,
            Variant height = null,
            Variant style = null,
            Variant exStyle = null ) {
            throw new NotImplementedException();
        }

        public Variant GUICtrlCreateObj( Variant ObjectVar, Variant left, Variant top, Variant width = null, Variant height = null ) {
            throw new NotImplementedException();
        }

        public Variant GUICtrlCreatePic(
            Variant filename,
            Variant left,
            Variant top,
            Variant width = null,
            Variant height = null,
            Variant style = null,
            Variant exStyle = null ) {
            throw new NotImplementedException();
        }

        public Variant GUICtrlCreateProgress(
            Variant left,
            Variant top,
            Variant width = null,
            Variant height = null,
            Variant style = null,
            Variant exStyle = null ) {
            throw new NotImplementedException();
        }

        public Variant GUICtrlCreateRadio(
            Variant text,
            Variant left,
            Variant top,
            Variant width = null,
            Variant height = null,
            Variant style = null,
            Variant exStyle = null ) {
            throw new NotImplementedException();
        }

        public Variant GUICtrlCreateSlider(
            Variant left,
            Variant top,
            Variant width = null,
            Variant height = null,
            Variant style = null,
            Variant exStyle = null ) {
            throw new NotImplementedException();
        }

        public Variant GUICtrlCreateTab( Variant left, Variant top, Variant width = null, Variant height = null, Variant style = null, Variant exStyle = null ) {
            throw new NotImplementedException();
        }

        public Variant GUICtrlCreateTabItem( Variant text ) {
            throw new NotImplementedException();
        }

        public Variant GUICtrlCreateTreeView( Variant left, Variant top, Variant width, Variant height = null, Variant style = null, Variant exStyle = null ) {
            throw new NotImplementedException();
        }

        public Variant GUICtrlCreateTreeViewItem( Variant text, Variant treeviewID ) {
            throw new NotImplementedException();
        }

        public Variant GUICtrlCreateUpdown( Variant inputcontrolID, Variant style = null ) {
            throw new NotImplementedException();
        }

        public Variant GUICtrlDelete( Variant controlID ) {
            throw new NotImplementedException();
        }

        public Variant GUICtrlGetHandle( Variant controlID ) {
            throw new NotImplementedException();
        }

        public Variant GUICtrlGetState( Variant controlID ) {
            throw new NotImplementedException();
        }

        public Variant GUICtrlRead( Variant controlID, Variant advanced = null ) {
            throw new NotImplementedException();
        }

        public Variant GUICtrlRecvMsg( Variant controlID, Variant msg, Variant wParam = null, Variant lParamType = null ) {
            throw new NotImplementedException();
        }

        public Variant GUICtrlRegisterListViewSort( Variant controlID, Variant function ) {
            throw new NotImplementedException();
        }

        public Variant GUICtrlSendMsg( Variant controlID, Variant msg, Variant wParam, Variant lParam ) {
            throw new NotImplementedException();
        }

        public Variant GUICtrlSendToDummy( Variant controlID, Variant state = null ) {
            throw new NotImplementedException();
        }

        public Variant GUICtrlSetBkColor( Variant controlID, Variant backgroundcolor ) {
            throw new NotImplementedException();
        }

        public Variant GUICtrlSetColor( Variant controlID, Variant textcolor ) {
            throw new NotImplementedException();
        }

        public Variant GUICtrlSetCursor( Variant controlID, Variant cursorID ) {
            throw new NotImplementedException();
        }

        public Variant GUICtrlSetData( Variant controlID, Variant data, Variant @default = null ) {
            throw new NotImplementedException();
        }

        public Variant GUICtrlSetDefBkColor( Variant defbkcolor, Variant winhandle = null ) {
            throw new NotImplementedException();
        }

        public Variant GUICtrlSetDefColor( Variant deftextcolor, Variant winhandle = null ) {
            throw new NotImplementedException();
        }

        public Variant GUICtrlSetFont(
            Variant controlID,
            Variant size,
            Variant weight = null,
            Variant attribute = null,
            Variant fontname = null,
            Variant quality = null ) {
            throw new NotImplementedException();
        }

        public Variant GUICtrlSetGraphic( Variant controlID, Variant type, params Variant[] parN ) {
            throw new NotImplementedException();
        }

        public Variant GUICtrlSetImage( Variant controlID, Variant filename, Variant iconname = null, Variant icontype = null ) {
            throw new NotImplementedException();
        }

        public Variant GUICtrlSetLimit( Variant controlID, Variant max, Variant min = null ) {
            throw new NotImplementedException();
        }

        public Variant GUICtrlSetOnEvent( Variant controlID, Variant function ) {
            throw new NotImplementedException();
        }

        public Variant GUICtrlSetPos( Variant controlID, Variant left, Variant top = null, Variant width = null, Variant height = null ) {
            throw new NotImplementedException();
        }

        public Variant GUICtrlSetResizing( Variant controlID, Variant resizing ) {
            throw new NotImplementedException();
        }

        public Variant GUICtrlSetState( Variant controlID, Variant state ) {
            throw new NotImplementedException();
        }

        public Variant GUICtrlSetStyle( Variant controlID, Variant style, Variant exStyle = null ) {
            throw new NotImplementedException();
        }

        public Variant GUICtrlSetTip( Variant controlID, Variant tiptext, Variant title = null, Variant icon = null, Variant options = null ) {
            throw new NotImplementedException();
        }

        public Variant GUIDelete( Variant winhandle ) {
            throw new NotImplementedException();
        }

        public Variant GUIGetCursorInfo( Variant winhandle ) {
            throw new NotImplementedException();
        }

        public Variant GUIGetMsg( Variant advanced = null ) {
            throw new NotImplementedException();
        }

        public Variant GUIGetStyle( Variant winhandle = null ) {
            throw new NotImplementedException();
        }

        public Variant GUIRegisterMsg( Variant msgID, Variant function ) {
            throw new NotImplementedException();
        }

        public Variant GUISetAccelerators( Variant accelerators, Variant winhandle = null ) {
            throw new NotImplementedException();
        }

        public Variant GUISetBkColor( Variant background, Variant winhandle = null ) {
            throw new NotImplementedException();
        }

        public Variant GUISetCoord( Variant left, Variant top, Variant width = null, Variant height = null, Variant winhandle = null ) {
            throw new NotImplementedException();
        }

        public Variant GUISetCursor( Variant cursorID = null, Variant @override = null, Variant winhandle = null ) {
            throw new NotImplementedException();
        }

        public Variant GUISetFont(
            Variant size,
            Variant weight = null,
            Variant attribute = null,
            Variant fontname = null,
            Variant winhandle = null,
            Variant quality = null ) {
            throw new NotImplementedException();
        }

        public Variant GUISetHelp( Variant helpfile, Variant winhandle = null ) {
            throw new NotImplementedException();
        }

        public Variant GUISetIcon( Variant iconfile, Variant iconID = null, Variant winhandle = null ) {
            throw new NotImplementedException();
        }

        public Variant GUISetOnEvent( Variant specialID, Variant function, Variant winhandle = null ) {
            throw new NotImplementedException();
        }

        public Variant GUISetState( Variant flag = null, Variant winhandle = null ) {
            throw new NotImplementedException();
        }

        public Variant GUISetStyle( Variant Style, Variant ExStyle = null, Variant winhandle = null ) {
            throw new NotImplementedException();
        }

        public Variant GUIStartGroup( Variant winhandle = null ) {
            throw new NotImplementedException();
        }

        public Variant GUISwitch( Variant winhandle, Variant tabitemID = null ) {
            throw new NotImplementedException();
        }

        public Variant HWnd( Variant expression ) {
            throw new NotImplementedException();
        }

        [Inlineable]
        public Variant Hex( Variant expression, Variant length = null ) {
            if ( expression.IsInt32 ) {
                if ( length == null ||
                     length.IsDefault ) {
                    return expression.GetInt().ToString( "x8" ).ToUpper();
                }
                if ( length > 16 ) {
                    length = 16;
                }
                return expression.GetInt().ToString( "x"+length ).ToUpper();
            }
            if ( expression.IsInt64 ) {
                if ( length == null ||
                     length.IsDefault ) {
                    return expression.GetInt64().ToString( "x16" ).ToUpper();
                }
                if ( length > 16 ) {
                    length = 16;
                }
                return expression.GetInt().ToString( "x"+length ).ToUpper();
            }
            if ( expression.IsPtr ) {
                var intPtr = expression.GetIntPtr();
                var size = IntPtr.Size * 2;
                return intPtr.ToString( "x"+size );
            }

            if ( expression.IsBinary ) {
                var bytes = expression.GetBinary();
                var c = new char[bytes.Length * 2];
                for ( int i = 0; i < bytes.Length; i++ ) {
                    int b = bytes[i] >> 4;
                    c[i * 2] = (char) ( 55+b+( ( ( b-10 ) >> 31 )&-7 ) );
                    b = bytes[i]&0xF;
                    c[i * 2+1] = (char) ( 55+b+( ( ( b-10 ) >> 31 )&-7 ) );
                }
                return new string( c ).ToUpper();
            }
            throw new NotImplementedException();
        }

        public Variant HotKeySet( Variant key, Variant function = null ) {
            throw new NotImplementedException();
        }

        public Variant HttpSetProxy( Variant mode = null, Variant proxyport = null, Variant username = null, Variant password = null ) {
            throw new NotImplementedException();
        }

        public Variant HttpSetUserAgent( Variant useragent ) {
            throw new NotImplementedException();
        }

        public Variant InetClose( Variant handle ) {
            throw new NotImplementedException();
        }

        public Variant InetGet( Variant URL, Variant filename, Variant options = null, Variant background = null ) {
            throw new NotImplementedException();
        }

        public Variant InetGetInfo( Variant handle = null, Variant index = null ) {
            throw new NotImplementedException();
        }

        public Variant InetGetSize( Variant URL, Variant options = null ) {
            throw new NotImplementedException();
        }

        public Variant InetRead( Variant URL, Variant options = null ) {
            throw new NotImplementedException();
        }

        public Variant IniDelete( Variant filename, Variant section, Variant key = null ) {
            throw new NotImplementedException();
        }

        public Variant IniRead( Variant filename, Variant section, Variant key, Variant @default ) {
            throw new NotImplementedException();
        }

        public Variant IniReadSection( Variant filename, Variant section ) {
            throw new NotImplementedException();
        }

        public Variant IniReadSectionNames( Variant filename ) {
            throw new NotImplementedException();
        }

        public Variant IniRenameSection( Variant filename, Variant section, Variant newsection, Variant flag = null ) {
            throw new NotImplementedException();
        }

        public Variant IniWrite( Variant filename, Variant section, Variant key, Variant value ) {
            throw new NotImplementedException();
        }

        public Variant IniWriteSection( Variant filename, Variant section, Variant data, Variant index = null ) {
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
            Variant hwnd = null ) {
            throw new NotImplementedException();
        }

        public Variant Int( Variant expression, Variant flag = null ) {
            throw new NotImplementedException();
        }

        public Variant IsAdmin() {
            throw new NotImplementedException();
        }

        public Variant IsArray( Variant variable ) {
            return variable.IsArray;
        }

        public Variant IsBinary( Variant variable ) {
            return variable.IsBinary;
        }

        [Inlineable]
        public Variant IsBool( Variant variable ) {
            return variable.IsBool;
        }

        public Variant IsDeclared( Variant expression ) {
            throw new NotImplementedException();
        }

        public Variant IsDllStruct( Variant variable ) {
            return variable.IsStruct;
        }

        [Inlineable]
        public Variant IsFloat( Variant variable ) {
            return variable.IsDouble;
        }

        public Variant IsFunc( Variant expression ) {
            throw new NotImplementedException();
        }

        public Variant IsHWnd( Variant variable ) {
            return variable.IsPtr && MarshalBridge.IsWindow( variable );
        }

        [Inlineable]
        public Variant IsInt( Variant variable ) {
            return variable.IsInt32 || variable.IsInt64;
        }

        [Inlineable]
        public Variant IsKeyword( Variant variable ) {
            return variable.IsDefault || variable.IsNull;
        }

        [Inlineable]
        public Variant IsNumber( Variant variable ) {
            return variable.IsInt32 || variable.IsInt64 || variable.IsDouble;
        }

        public Variant IsObj( Variant variable ) {
            throw new NotImplementedException();
        }

        public Variant IsPtr( Variant variable ) {
            return variable.IsPtr;
        }

        [Inlineable]
        public Variant IsString( Variant variable ) {
            return variable.IsString;
        }

        [Inlineable]
        public Variant Log( Variant expression ) {
            return Math.Log( expression );
        }

        public Variant MemGetStats() {
            throw new NotImplementedException();
        }

        [Inlineable]
        public Variant Mod( Variant value1, Variant value2 ) {
            if ( value1.IsInt32 ||
                 value2.IsInt32 ) {
                return value1.GetInt() % value2.GetInt();
            }

            return value1.GetDouble() % value2.GetDouble();
        }

        public Variant MouseClick( Variant button, Variant x = null, Variant y = null, Variant clicks = null, Variant speed = null ) {
            throw new NotImplementedException();
        }

        public Variant MouseClickDrag( Variant button, Variant x1, Variant y1, Variant x2, Variant y2, Variant speed = null ) {
            throw new NotImplementedException();
        }

        public Variant MouseDown( Variant button ) {
            throw new NotImplementedException();
        }

        public Variant MouseGetCursor() {
            throw new NotImplementedException();
        }

        public Variant MouseGetPos( Variant dimension = null ) {
            throw new NotImplementedException();
        }

        public Variant MouseMove( Variant x, Variant y, Variant speed = null ) {
            throw new NotImplementedException();
        }

        public Variant MouseUp( Variant button ) {
            throw new NotImplementedException();
        }

        public Variant MouseWheel( Variant direction, Variant clicks = null ) {
            throw new NotImplementedException();
        }

        public Variant MsgBox( Variant flag, Variant title, Variant text, Variant timeout = null, Variant hwnd = null ) {
            throw new NotImplementedException();
        }

        public Variant Number( Variant expression, Variant flag = null ) {
            throw new NotImplementedException();
        }

        public Variant ObjCreate( Variant classname, Variant servername = null, Variant username = null, Variant password = null ) {
            throw new NotImplementedException();
        }

        public Variant ObjCreateInterface( Variant CLSID, Variant IID = null, Variant interfacedescription = null, Variant flag = null ) {
            throw new NotImplementedException();
        }

        public Variant ObjEvent( Variant ObjectVar, Variant functionprefix = null, Variant interfacename = null ) {
            throw new NotImplementedException();
        }

        public Variant ObjGet( Variant filename, Variant classname = null, Variant instance = null ) {
            throw new NotImplementedException();
        }

        public Variant ObjName( Variant Objectvariable, Variant Flag = null ) {
            throw new NotImplementedException();
        }

        public Variant OnAutoItExitRegister( Variant function ) {
            throw new NotImplementedException();
        }

        public Variant OnAutoItExitUnRegister( Variant function ) {
            throw new NotImplementedException();
        }

        public Variant Ping( Variant addresshostname, Variant timeout = null ) {
            if ( timeout == null ) {
                timeout = 4000;
            }
            return new System.Net.NetworkInformation.Ping().Send( addresshostname, timeout ).RoundtripTime;
        }

        public Variant PixelChecksum( Variant left, Variant top, Variant right, Variant bottom, Variant step = null, Variant hwnd = null, Variant mode = null ) {
            throw new NotImplementedException();
        }

        public Variant PixelGetColor( Variant x, Variant y, Variant hwnd = null ) {
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
            Variant hwnd = null ) {
            throw new NotImplementedException();
        }

        public Variant ProcessClose( Variant process ) {
            throw new NotImplementedException();
        }

        public Variant ProcessExists( Variant process ) {
            var processesByName = Process.GetProcessesByName( process ).SingleOrDefault();
            if ( processesByName != null ) {
                return processesByName.Id;
            }

            try {
                var byId = Process.GetProcessById( process );
                return byId.Id;
            }
            catch (Exception) {
                return 0;
            }
        }

        public Variant ProcessGetStats( Variant process = null, Variant type = null ) {
            throw new NotImplementedException();
        }

        public Variant ProcessList( Variant name = null ) {
            var processes = name == null
                ? Process.GetProcesses()
                : Process.GetProcessesByName( name );

            var toReturn = new Variant[processes.Length+1, 1];

            toReturn[0, 0] = processes.Length;

            for ( int index = 0; index < processes.Length; index++ ) {
                var process = processes[index];

                toReturn[index+1, 0] = process.ProcessName;
                toReturn[index+1, 1] = process.Id;
            }

            return toReturn;
        }

        public Variant ProcessSetPriority( Variant process, Variant priority ) {
            throw new NotImplementedException();
        }

        public Variant ProcessWait( Variant process, Variant timeout = null ) {
            throw new NotImplementedException();
        }

        public Variant ProcessWaitClose( Variant process, Variant timeout = null ) {
            throw new NotImplementedException();
        }

        public Variant ProgressOff() {
            throw new NotImplementedException();
        }

        public Variant ProgressOn( Variant title, Variant maintext, Variant subtext = null, Variant xpos = null, Variant ypos = null, Variant opt = null ) {
            throw new NotImplementedException();
        }

        public Variant ProgressSet( Variant percent, Variant subtext = null, Variant maintext = null ) {
            throw new NotImplementedException();
        }

        public Variant Ptr( Variant expression ) {
            return new IntPtr( expression );
        }

        public Variant Random( Variant Min = null, Variant Max = null, Variant Flag = null ) {
            if ( Min == null ) {
                Min = 0;
            }

            if ( Max == null ) {
                Max = 1;
            }

            if ( Flag == null ) {
                Flag = 0;
            }

            throw new NotImplementedException();
        }

        public Variant RegDelete( Variant keyname, Variant valuename = null ) {
            throw new NotImplementedException();
        }

        public Variant RegEnumKey( Variant keyname, Variant instance ) {
            throw new NotImplementedException();
        }

        public Variant RegEnumVal( Variant keyname, Variant instance ) {
            throw new NotImplementedException();
        }

        public Variant RegRead( Variant keyname, Variant valuename ) {
            throw new NotImplementedException();
        }

        public Variant RegWrite( Variant keyname, Variant valuename = null, Variant type = null, Variant value = null ) {
            throw new NotImplementedException();
        }

        [Inlineable]
        public Variant Round( Variant expression, Variant decimalplaces = null ) {
            if ( decimalplaces == null ) {
                decimalplaces = 0;
            }

            return Math.Round( (double) expression, decimalplaces );
        }

        public Variant Run( Variant program, Variant workingdir = null, Variant showflag = null, Variant opt_flag = null ) {
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
            Variant optflag = null ) {
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
            Variant opt_flag = null ) {
            throw new NotImplementedException();
        }

        public Variant RunWait( Variant program, Variant workingdir = null, Variant showflag = null, Variant optflag = null ) {
            throw new NotImplementedException();
        }

        public Variant SRandom( Variant Seed ) {
            throw new NotImplementedException();
        }

        public Variant Send( Variant keys, Variant flag = null ) {
            throw new NotImplementedException();
        }

        public Variant SendKeepActive( Variant title, Variant text = null ) {
            throw new NotImplementedException();
        }

        public Variant SetError( Variant code, Variant extended = null, Variant returnvalue = null ) {
            if ( returnvalue == null ) {
                returnvalue = 0;
            }

            _context.Error = code;
            _context.Extended = extended;
            return returnvalue;
        }

        public Variant SetExtended( Variant code, Variant returnvalue = null ) {
            if ( returnvalue == null ) {
                returnvalue = 0;
            }

            _context.Extended = code.GetInt();

            return returnvalue;
        }

        public Variant ShellExecute( Variant filename, Variant parameters = null, Variant workingdir = null, Variant verb = null, Variant showflag = null ) {
            if ( parameters == null ) {
                parameters = string.Empty;
            }

            if ( workingdir == null ) {
                workingdir = string.Empty;
            }

            if ( verb == null ) {
                verb = string.Empty;
            }

            if ( showflag == null ) {
                showflag = _context.SW_SHOW;
            }

            var startInfo = new ProcessStartInfo( filename );
            if ( verb != string.Empty ) {
                startInfo.Verb = verb;
            }

            if ( workingdir != string.Empty ) {
                startInfo.WorkingDirectory = workingdir;
            }

            if ( parameters != string.Empty ) {
                startInfo.Arguments = parameters;
            }

            var flag = showflag.GetInt();
            if ( flag == _context.SW_HIDE ) {
                startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            }
            else if ( flag == _context.SW_MINIMIZE ) {
                startInfo.WindowStyle = ProcessWindowStyle.Minimized;
            }
            else if ( flag == _context.SW_MAXIMIZE ) {
                startInfo.WindowStyle = ProcessWindowStyle.Maximized;
            }
            startInfo.UseShellExecute = true;

            try {
                var process = Process.Start( startInfo );
                return process.Id;
            }
            catch (Win32Exception exception) {
                return SetError( exception.NativeErrorCode, null, 0 );
            }
            catch (Exception) {
                return SetError( 1, null, 0 );
            }
        }

        public Variant ShellExecuteWait( Variant filename, Variant parameters = null, Variant workingdir = null, Variant verb = null, Variant showflag = null ) {
            var pid = ShellExecute( filename, parameters, workingdir, verb, showflag );
            if ( pid ) {
                var process = Process.GetProcessById( pid );
                while ( !process.HasExited ) {
                    Thread.Sleep( 10 );
                }
                return process.ExitCode;
            }
            return 0;
        }

        public Variant Shutdown( Variant code ) {
            throw new NotImplementedException();
        }

        [Inlineable]
        public Variant Sin( Variant expression ) {
            return Math.Sin( expression );
        }

        public Variant Sleep( Variant delay ) {
            Thread.Sleep( delay );
            return 0;
        }

        public Variant SoundPlay( Variant filename, Variant wait = null ) {
            throw new NotImplementedException();
        }

        public Variant SoundSetWaveVolume( Variant percent ) {
            throw new NotImplementedException();
        }

        public Variant SplashImageOn(
            Variant title,
            Variant file,
            Variant width = null,
            Variant height = null,
            Variant xpos = null,
            Variant ypos = null,
            Variant opt = null ) {
            throw new NotImplementedException();
        }

        public Variant SplashOff( Variant expression ) {
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
            Variant fontwt = null ) {
            throw new NotImplementedException();
        }

        [Inlineable]
        public Variant Sqrt( Variant expression ) {
            return Math.Sqrt( expression );
        }

        public Variant StatusbarGetText( Variant title, Variant text = null, Variant part = null ) {
            throw new NotImplementedException();
        }

        public Variant StderrRead( Variant process_id, Variant peek = null, Variant binary = null ) {
            throw new NotImplementedException();
        }

        public Variant StdinWrite( Variant process_id, Variant data = null ) {
            throw new NotImplementedException();
        }

        public Variant StdioClose( Variant process_id ) {
            throw new NotImplementedException();
        }

        public Variant StdoutRead( Variant process_id, Variant peek = null, Variant binary = null ) {
            throw new NotImplementedException();
        }

        [Inlineable]
        public Variant String( Variant expression ) {
            return expression.GetString();
        }

        [Inlineable]
        public Variant StringAddCR( Variant @string ) {
            return @string.GetString().Replace( "\n", "\r\n" );
        }

        public Variant StringCompare( Variant string1, Variant string2, Variant casesense = null ) {
            throw new NotImplementedException();
        }

        public Variant StringFormat( Variant formatcontrol, Variant var1, params Variant[] varN ) {
            throw new NotImplementedException();
        }

        public Variant StringFromASCIIArray( Variant array, Variant start = null, Variant end = null, Variant encoding = null ) {
            throw new NotImplementedException();
        }

        public Variant StringInStr(
            Variant @string,
            Variant substring,
            Variant casesense = null,
            Variant occurrence = null,
            Variant start = null,
            Variant count = null ) {
            throw new NotImplementedException();
        }

        [Inlineable]
        public Variant StringIsASCII( Variant @string ) {
            return @string.GetString().All( c => ( c > -1 && c < 128 ) );
        }

        public Variant StringIsAlNum( Variant @string ) {
            throw new NotImplementedException();
        }

        [Inlineable]
        public Variant StringIsAlpha( Variant @string ) {
            return @string.GetString().All( char.IsLetter );
        }

        [Inlineable]
        public Variant StringIsDigit( Variant @string ) {
            return @string.GetString().All( char.IsDigit );
        }

        public Variant StringIsFloat( Variant @string ) {
            throw new NotImplementedException();
        }

        public Variant StringIsInt( Variant @string ) {
            throw new NotImplementedException();
        }

        [Inlineable]
        public Variant StringIsLower( Variant @string ) {
            return @string.GetString().All( char.IsLower );
        }

        [Inlineable]
        public Variant StringIsSpace( Variant @string ) {
            return @string.GetString().All( char.IsWhiteSpace );
        }

        [Inlineable]
        public Variant StringIsUpper( Variant @string ) {
            return @string.GetString().All( char.IsUpper );
        }

        [Inlineable]
        public Variant StringIsXDigit( Variant @string ) {
            return @string.GetString().All( c => ( ( c >= 0 && c <= 9 ) || ( ( c >= 'a' || c >= 'A' ) && ( c <= 'F' || c <= 'f' ) ) ) );
        }

        [Inlineable]
        public Variant StringLeft( Variant @string, Variant count ) {
            var fullString = @string.GetString();
            if ( fullString.Length <= count ) {
                return fullString;
            }
            return fullString.Substring( 0, count );
        }

        [Inlineable]
        public Variant StringLen( Variant @string ) {
            return @string.ToString().Length;
        }

        [Inlineable]
        public Variant StringLower( Variant @string ) {
            return @string.GetString().ToLower();
        }

        [Inlineable]
        public Variant StringMid( Variant @string, Variant start, Variant count = null ) {
            var toMid = @string.GetString();
            if ( count == null ) {
                count = toMid.Length-start;
            }

            if ( start < 1 ||
                 start-1 > toMid.Length ) {
                return string.Empty;
            }

            if ( start-1+count > toMid.Length ) {
                return toMid.Substring( start-1, toMid.Length-( start-1 ) );
            }

            return toMid.Substring( start-1, count );
        }

        public Variant StringRegExp( Variant test, Variant pattern, Variant flag = null, Variant offset = null ) {
            throw new NotImplementedException();
        }

        public Variant StringRegExpReplace( Variant test, Variant pattern, Variant replace, Variant count = null ) {
            throw new NotImplementedException();
        }

        public Variant StringReplace( Variant @string, Variant searchstringstart, Variant replacestring, Variant occurrence = null, Variant casesense = null ) {
            throw new NotImplementedException();
        }

        [Inlineable]
        public Variant StringReverse( Variant @string, Variant flag = null ) {
            if ( flag == null ) {
                flag = 0;
            }
            return new String( @string.GetString().Reverse().ToArray() );
        }

        [Inlineable]
        public Variant StringRight( Variant @string, Variant count ) {
            var fullString = @string.GetString();
            if ( fullString.Length <= count ) {
                return fullString;
            }
            return fullString.Substring( fullString.Length-count, count );
        }

        public Variant StringSplit( Variant @string, Variant delimiters, Variant flag = null ) {
            throw new NotImplementedException();
        }

        [Inlineable]
        public Variant StringStripCR( Variant @string ) {
            return @string.GetString().Replace( "\r", string.Empty );
        }

        [Inlineable]
        public Variant StringStripWS( Variant @string, Variant flag ) {
            return @string.GetString().Replace( " ", string.Empty );
        }

        public Variant StringToASCIIArray( Variant @string, Variant start = null, Variant end = null, Variant encoding = null ) {
            var toConvert = @string.GetString();

            if ( start == null ) {
                start = 0;
            }

            if ( end == null ) {
                end = toConvert.Length;
            }

            if ( encoding == null ) {
                encoding = 0;
            }

            if ( start+end >= toConvert.Length ) {
                return string.Empty;
            }

            byte[] bytes;

            switch (encoding.GetInt()) {
                case 0:
                    bytes = Encoding.Unicode.GetBytes( toConvert.Substring( start, end ) );
                    break;
                case 1:
                    bytes = Encoding.Default.GetBytes( toConvert.Substring( start, end ) );
                    break;
                case 2:
                    bytes = Encoding.UTF8.GetBytes( toConvert.Substring( start, end ) );
                    break;
                default:
                    return string.Empty;
            }

            return bytes.Select( x => Variant.Create( (int) x ) ).ToArray();
        }

        public Variant StringToBinary( Variant expression, Variant flag = null ) {
            if ( flag == null ) {
                flag = 1;
            }

            var @string = expression.GetString();

            Encoding encoding;
            switch (flag.GetInt()) {
                case 1:
                    encoding = Encoding.Default;
                    break;
                case 2:
                    encoding = Encoding.Unicode;
                    break;
                case 3:
                    encoding = Encoding.BigEndianUnicode;
                    break;
                default:
                    encoding = Encoding.UTF8;
                    break;
            }

            return encoding.GetBytes( expression.GetString() );
        }

        [Inlineable]
        public Variant StringTrimLeft( Variant @string, Variant count ) {
            var toTrim = @string.GetString();
            if ( toTrim.Length-count <= 0 ) {
                return string.Empty;
            }
            return toTrim.Substring( count, toTrim.Length-count );
        }

        [Inlineable]
        public Variant StringTrimRight( Variant @string, Variant count ) {
            var toTrim = @string.GetString();
            if ( toTrim.Length-count <= 0 ) {
                return string.Empty;
            }
            return toTrim.Substring( 0, toTrim.Length-count );
        }

        [Inlineable]
        public Variant StringUpper( Variant @string ) {
            return @string.ToString().ToUpper();
        }

        public Variant TCPAccpet( Variant mainsocket ) {
            throw new NotImplementedException();
        }

        public Variant TCPCloseSocket( Variant socket ) {
            throw new NotImplementedException();
        }

        public Variant TCPConnect( Variant IPAddr, Variant port ) {
            throw new NotImplementedException();
        }

        public Variant TCPListen( Variant IPAddr, Variant port, Variant MaxPendingConnection = null ) {
            throw new NotImplementedException();
        }

        public Variant TCPNameToIP( Variant name ) {
            throw new NotImplementedException();
        }

        public Variant TCPRecv( Variant mainsocket, Variant maxlen, Variant flag = null ) {
            throw new NotImplementedException();
        }

        public Variant TCPSend( Variant mainsocket, Variant data ) {
            throw new NotImplementedException();
        }

        public Variant TCPShutdown() {
            throw new NotImplementedException();
        }

        private Variant UDPShutdown() {
            throw new NotImplementedException();
        }

        public Variant TCPStartup() {
            throw new NotImplementedException();
        }

        private Variant UDPStartup() {
            throw new NotImplementedException();
        }

        [Inlineable]
        public Variant Tan( Variant expression ) {
            return Math.Tan( expression );
        }

        public Variant TimerDiff( Variant handle ) {
            var tickDiff = ( Stopwatch.GetTimestamp()-handle ) / Stopwatch.Frequency * 1000;
            return tickDiff;
        }

        public Variant TimerInit() {
            return Stopwatch.GetTimestamp();
        }

        public Variant ToolTip( Variant text, Variant x = null, Variant y = null, Variant title = null, Variant icon = null, Variant options = null ) {
            throw new NotImplementedException();
        }

        public Variant TrayCreateItem( Variant text, Variant menuID = null, Variant menuentry = null, Variant menuradioitem = null ) {
            throw new NotImplementedException();
        }

        public Variant TrayCreateMenu( Variant submenutext, Variant menuID = null, Variant menuentry = null ) {
            throw new NotImplementedException();
        }

        public Variant TrayGetMsg() {
            throw new NotImplementedException();
        }

        public Variant TrayItemDelete( Variant controlID ) {
            throw new NotImplementedException();
        }

        public Variant TrayItemGetHandle( Variant controlID ) {
            throw new NotImplementedException();
        }

        public Variant TrayItemGetState( Variant controlID = null ) {
            throw new NotImplementedException();
        }

        public Variant TrayItemGetText( Variant controlID ) {
            throw new NotImplementedException();
        }

        public Variant TrayItemSetOnEvent( Variant itemID, Variant function ) {
            throw new NotImplementedException();
        }

        public Variant TrayItemSetState( Variant controlID, Variant state ) {
            throw new NotImplementedException();
        }

        public Variant TrayItemSetText( Variant controlID, Variant text ) {
            throw new NotImplementedException();
        }

        public Variant TraySetClick( Variant flag ) {
            throw new NotImplementedException();
        }

        public Variant TraySetIcon( Variant filename = null, Variant iconID = null ) {
            throw new NotImplementedException();
        }

        public Variant TraySetOnEvent( Variant specialID, Variant function ) {
            throw new NotImplementedException();
        }

        public Variant TraySetPauseIcon( Variant filename = null, Variant iconID = null ) {
            throw new NotImplementedException();
        }

        public Variant TraySetState( Variant flag = null ) {
            throw new NotImplementedException();
        }

        public Variant TraySetToolTip( Variant text = null ) {
            throw new NotImplementedException();
        }

        public Variant TrayTip( Variant title, Variant text, Variant timeout, Variant option = null ) {
            throw new NotImplementedException();
        }

        public Variant UBound( Variant Array, Variant Dimension = null ) {
            if ( Dimension == null ) {
                Dimension = 1;
            }

            var array = Array.GetValue() as Array;

            if ( array == null ) {
                return SetError( 1, null, 0 );
            }

            switch (Dimension.GetInt()) {
                case 0:
                    return array.Rank;
                case 1:
                    return array.GetLength( 0 );
                case 2:
                    return array.GetLength( 1 );
                default:
                    return SetError( 2, null, 0 );
            }
        }

        public Variant UDPBind( Variant IPAddr, Variant port ) {
            throw new NotImplementedException();
        }

        public Variant UDPCloseSocket( Variant socketarray ) {
            throw new NotImplementedException();
        }

        public Variant UDPOpen( Variant IPAddr, Variant port, Variant flag = null ) {
            throw new NotImplementedException();
        }

        public Variant UDPRecv( Variant socketarray, Variant maxlen, Variant flag = null ) {
            throw new NotImplementedException();
        }

        public Variant UDPSend( Variant socketarray, Variant data ) {
            throw new NotImplementedException();
        }

        public Variant VarGetType( Variant expression ) {
            throw new NotImplementedException();
        }

        public Variant WinActivate( Variant title, Variant text = null ) {
            throw new NotImplementedException();
        }

        public Variant WinActive( Variant title, Variant text = null ) {
            throw new NotImplementedException();
        }

        public Variant WinClose( Variant title, Variant text = null ) {
            throw new NotImplementedException();
        }

        public Variant WinExists( Variant title, Variant text = null ) {
            throw new NotImplementedException();
        }

        public Variant WinFlash( Variant title, Variant text = null, Variant flashes = null, Variant delay = null ) {
            throw new NotImplementedException();
        }

        public Variant WinGetCaretPos() {
            throw new NotImplementedException();
        }

        public Variant WinGetClassList( Variant title, Variant text = null ) {
            throw new NotImplementedException();
        }

        public Variant WinGetClientSize( Variant title, Variant text = null ) {
            throw new NotImplementedException();
        }

        public Variant WinGetHandle( Variant title, Variant text = null ) {
            throw new NotImplementedException();
        }

        public Variant WinGetPos( Variant title, Variant text = null ) {
            throw new NotImplementedException();
        }

        public Variant WinGetProcess( Variant title, Variant text = null ) {
            throw new NotImplementedException();
        }

        public Variant WinGetState( Variant title, Variant text = null ) {
            throw new NotImplementedException();
        }

        public Variant WinGetText( Variant title, Variant text = null ) {
            throw new NotImplementedException();
        }

        public Variant WinGetTitle( Variant title, Variant text = null ) {
            throw new NotImplementedException();
        }

        public Variant WinKill( Variant title, Variant text = null ) {
            throw new NotImplementedException();
        }

        public Variant WinList( Variant title = null, Variant text = null ) {
            throw new NotImplementedException();
        }

        public Variant WinMenuSelectItem( Variant title, Variant text, params Variant[] items ) {
            throw new NotImplementedException();
        }

        public Variant WinMinimizeAll() {
            throw new NotImplementedException();
        }

        public Variant WinMinimizeAllUndo() {
            throw new NotImplementedException();
        }

        public Variant WinMove( Variant title, Variant text, Variant x, Variant y, Variant width = null, Variant height = null, Variant speed = null ) {
            throw new NotImplementedException();
        }

        public Variant WinSetOnTop( Variant title, Variant text, Variant flag ) {
            throw new NotImplementedException();
        }

        public Variant WinSetState( Variant title, Variant text, Variant flag ) {
            throw new NotImplementedException();
        }

        public Variant WinSetTitle( Variant title, Variant text, Variant newtitle ) {
            throw new NotImplementedException();
        }

        public Variant WinSetTrans( Variant title, Variant text, Variant transparency ) {
            throw new NotImplementedException();
        }

        public Variant WinWait( Variant title, Variant text = null, Variant timeout = null ) {
            throw new NotImplementedException();
        }

        public Variant WinWaitActive( Variant title, Variant text = null, Variant timeout = null ) {
            throw new NotImplementedException();
        }

        public Variant WinWaitClose( Variant title, Variant text = null, Variant timeout = null ) {
            throw new NotImplementedException();
        }

        public Variant WinWaitNotActive( Variant title, Variant text = null, Variant timeout = null ) {
            throw new NotImplementedException();
        }

        [Inlineable]
        [AutoJITCompilerFunction]
        public Variant Pow( Variant a, Variant b ) {
            return Math.Pow( a, b );
        }

        [Inlineable]
        [AutoJITCompilerFunction]
        public Variant Equal( Variant a, Variant b ) {
            if ( a.IsString ||
                 b.IsString ) {
                return string.Compare( a.ToString(), b.ToString(), StringComparison.InvariantCultureIgnoreCase ) == 0;
            }
            return a == b;
        }

        [Inlineable]
        [AutoJITCompilerFunction]
        public Variant EqualString( Variant a, Variant b ) {
            return a.ToString() == b.ToString();
        }

        [Inlineable]
        [AutoJITCompilerFunction]
        public Variant NOT( Variant variable ) {
            return !variable;
        }

        [Inlineable]
        [AutoJITCompilerFunction]
        public Variant OR( Variant a, Variant b ) {
            return a || b;
        }

        [Inlineable]
        [AutoJITCompilerFunction]
        public Variant AND( Variant a, Variant b ) {
            return a && b;
        }

        [Inlineable]
        [AutoJITCompilerFunction]
        public Variant Negate( Variant o ) {
            return -o;
        }

        [Inlineable]
        [AutoJITCompilerFunction]
        public bool To( Variant from, Variant to, Variant toTest ) {
            return toTest >= @from && toTest <= to;
        }

        [AutoJITCompilerFunction]
        public void Exit( Variant exitCode = null ) {
            if ( exitCode == null ) {
                exitCode = 0;
            }
            Environment.Exit( exitCode );
        }

        [Inlineable]
        public Variant Concat( Variant a, Variant b ) {
            return a.ToString()+b.ToString();
        }
    }
}
