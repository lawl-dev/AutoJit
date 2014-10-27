using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using AutoJITRuntime.Attrubutes;
using AutoJITRuntime.Exceptions;
using AutoJITRuntime.Services;
using AutoJITRuntime.Variants;
using Microsoft.Win32.SafeHandles;

namespace AutoJITRuntime
{
    public class AutoitRuntime<T>
    {
        private readonly AutoitContext<T> _context;
        private readonly EnvironmentService _environmentService;
        private readonly MarshalService _marshalService;
        private readonly MathService _mathService;
        private readonly Dictionary<string, MethodInfo> _methodStore;
        private readonly MiscService _miscService;
        private readonly ProcessService _processService;
        private readonly StringService _stringService;
        private readonly VariablesAndConversionsService _variablesAndConversionsService;

        public AutoitRuntime( AutoitContext<T> context ) {
            _context = context;
            MethodInfo[] methodInfos = GetType().GetMethods();
            _methodStore = methodInfos.ToDictionary( x => x.Name, x => x );
            _marshalService = new MarshalService();
            _environmentService = new EnvironmentService();
            _mathService = new MathService();
            _stringService = new StringService();
            _variablesAndConversionsService = new VariablesAndConversionsService();
            _processService = new ProcessService();
            _miscService = new MiscService();
        }

        [Inlineable]
        public Variant ACos( Variant expression ) {
            SetError( 0, 0, 0 );

            try {
                return _mathService.ACos( expression );
            }
            catch(AutoJITExceptionBase ex) {
                return SetError( Variant.Create( ex.Error ), Variant.Create( ex.Extended ), Variant.Create( ex.Return ) );
            }
        }

        [Inlineable]
        public Variant ASin( Variant expression ) {
            SetError( 0, 0, 0 );

            try {
                return _mathService.ACos( expression );
            }
            catch(AutoJITExceptionBase ex) {
                return SetError( Variant.Create( ex.Error ), Variant.Create( ex.Extended ), Variant.Create( ex.Return ) );
            }
        }

        [Inlineable]
        public Variant ATan( Variant expression ) {
            SetError( 0, 0, 0 );

            try {
                return _mathService.ATan( expression );
            }
            catch(AutoJITExceptionBase ex) {
                return SetError( Variant.Create( ex.Error ), Variant.Create( ex.Extended ), Variant.Create( ex.Return ) );
            }
        }

        [Inlineable]
        public Variant Abs( Variant expression ) {
            SetError( 0, 0, 0 );

            try {
                return _mathService.Abs( expression );
            }
            catch(AutoJITExceptionBase ex) {
                return SetError( Variant.Create( ex.Error ), Variant.Create( ex.Extended ), Variant.Create( ex.Return ) );
            }
        }

        public Variant AdlibRegister( Variant function, Variant time = null ) {
            SetError( 0, 0, 0 );

            if( time == null ) {
                time = 250;
            }
            if( !_methodStore.ContainsKey( function ) ) {
                return false;
            }
            if( _methodStore[function].GetParameters().Any() ) {
                return false;
            }
            _context.LibRegister.Add( _methodStore[function], time );
            return true;
        }

        public Variant AdlibUnRegister( Variant function = null ) {
            SetError( 0, 0, 0 );

            if( function == null ) {
                if( _context.LibRegister.Any() ) {
                    _context.LibRegister.Remove( _context.LibRegister.Last().Key );
                    return _context.LibRegister.Count;
                }
                return 0;
            }
            KeyValuePair<MethodInfo, int> valuePair = _context.LibRegister.SingleOrDefault( x => x.Key.Name.Equals( function, StringComparison.InvariantCultureIgnoreCase ) );
            if( valuePair.Key != null ) {
                _context.LibRegister.Remove( valuePair.Key );
            }
            return _context.LibRegister.Count;
        }

        [Inlineable]
        public Variant Asc( Variant @char ) {
            SetError( 0, 0, 0 );

            try {
                return _variablesAndConversionsService.Asc( @char );
            }
            catch(AutoJITExceptionBase ex) {
                return SetError( Variant.Create( ex.Error ), Variant.Create( ex.Extended ), Variant.Create( ex.Return ) );
            }
        }

        public Variant AscW( Variant @char ) {
            SetError( 0, 0, 0 );

            try {
                return _variablesAndConversionsService.AscW( @char );
            }
            catch(AutoJITExceptionBase ex) {
                return SetError( Variant.Create( ex.Error ), Variant.Create( ex.Extended ), Variant.Create( ex.Return ) );
            }
        }

        public Variant Assign( Variant varname, Variant data, Variant flag = null ) {
            throw new NotSupportedException( "Function 'Assign' is not supported" );
        }

        public Variant Opt( Variant option, Variant param = null ) {
            SetError( 0, 0, 0 );

            return AutoItSetOption( option, param );
        }

        public Variant AutoItSetOption( Variant option, Variant param = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant AutoItWinGetTitle() {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant AutoItWinSetTitle( Variant newtitle ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant Beep( Variant Frequency = null, Variant Duration = null ) {
            SetError( 0, 0, 0 );

            if( Frequency == null ) {
                Frequency = 500;
            }

            if( Duration == null ) {
                Duration = 1000;
            }

            Console.Beep( Frequency, Duration );

            return 1;
        }

        public Variant Binary( Variant expression ) {
            SetError( 0, 0, 0 );

            try {
                return _variablesAndConversionsService.Binary( expression );
            }
            catch(AutoJITExceptionBase ex) {
                return SetError( Variant.Create( ex.Error ), Variant.Create( ex.Extended ), Variant.Create( ex.Return ) );
            }
        }

        public Variant BinaryLen( Variant binary ) {
            SetError( 0, 0, 0 );

            try {
                return _variablesAndConversionsService.BinaryLen( binary );
            }
            catch(AutoJITExceptionBase ex) {
                return SetError( Variant.Create( ex.Error ), Variant.Create( ex.Extended ), Variant.Create( ex.Return ) );
            }
        }

        public Variant BinaryMid( Variant binary, Variant start, Variant count = null ) {
            SetError( 0, 0, 0 );

            try {
                return _variablesAndConversionsService.BinaryMid( binary, start, count );
            }
            catch(AutoJITExceptionBase ex) {
                return SetError( Variant.Create( ex.Error ), Variant.Create( ex.Extended ), Variant.Create( ex.Return ) );
            }
        }

        public Variant BinaryToString( Variant expression, Variant flag = null ) {
            SetError( 0, 0, 0 );

            if( flag == null ) {
                flag = 1;
            }

            try {
                return _variablesAndConversionsService.BinaryToString( expression, flag );
            }
            catch(AutoJITExceptionBase ex) {
                return SetError( Variant.Create( ex.Error ), Variant.Create( ex.Extended ), Variant.Create( ex.Return ) );
            }
        }

        [Inlineable]
        public Variant BitAND( Variant value1, Variant value2, params Variant[] valuen ) {
            SetError( 0, 0, 0 );

            try {
                return _mathService.BitAND( value1, value2, valuen );
            }
            catch(AutoJITExceptionBase ex) {
                return SetError( Variant.Create( ex.Error ), Variant.Create( ex.Extended ), Variant.Create( ex.Return ) );
            }
        }

        [Inlineable]
        public Variant BitNOT( Variant value ) {
            SetError( 0, 0, 0 );

            try {
                return _mathService.BitNOT( value );
            }
            catch(AutoJITExceptionBase ex) {
                return SetError( Variant.Create( ex.Error ), Variant.Create( ex.Extended ), Variant.Create( ex.Return ) );
            }
        }

        [Inlineable]
        public Variant BitOR( Variant value1, Variant value2, params Variant[] valuen ) {
            SetError( 0, 0, 0 );

            try {
                return _mathService.BitOR( value1, value2, valuen );
            }
            catch(AutoJITExceptionBase ex) {
                return SetError( Variant.Create( ex.Error ), Variant.Create( ex.Extended ), Variant.Create( ex.Return ) );
            }
        }

        public Variant BitRotate( Variant value, Variant shift = null, Variant size = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        [Inlineable]
        public Variant BitShift( Variant value, Variant shift ) {
            SetError( 0, 0, 0 );

            try {
                return _mathService.BitShift( value, shift );
            }
            catch(AutoJITExceptionBase ex) {
                return SetError( Variant.Create( ex.Error ), Variant.Create( ex.Extended ), Variant.Create( ex.Return ) );
            }
        }

        [Inlineable]
        public Variant BitXOR( Variant value1, Variant value2, params Variant[] valuen ) {
            SetError( 0, 0, 0 );

            try {
                return _mathService.BitXOR( value1, value2, valuen );
            }
            catch(AutoJITExceptionBase ex) {
                return SetError( Variant.Create( ex.Error ), Variant.Create( ex.Extended ), Variant.Create( ex.Return ) );
            }
        }

        public Variant BlockInput( Variant flag ) {
            SetError( 0, 0, 0 );

            try {
                return _miscService.BlockInput( flag );
            }
            catch(AutoJITExceptionBase ex) {
                return SetError( Variant.Create( ex.Error ), Variant.Create( ex.Extended ), Variant.Create( ex.Return ) );
            }
        }

        public Variant Break( Variant mode ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant CDTray( Variant drive, Variant status ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant Call( Variant function, params Variant[] paramsN ) {
            SetError( 0, 0, 0 );

            if( _methodStore.ContainsKey( function ) ) {
                return Variant.Create( _methodStore[function].Invoke( this, paramsN ) );
            }
            if( _context.MethodStore.ContainsKey( function ) ) {
                return Variant.Create( _context.MethodStore[function].Invoke( _context.Context, paramsN ) );
            }
            return SetError( 0xDEAD, 0xBEEF, 0 );
        }

        [Inlineable]
        public Variant Ceiling( Variant expression ) {
            SetError( 0, 0, 0 );

            try {
                return _mathService.Ceiling( expression );
            }
            catch(AutoJITExceptionBase ex) {
                return SetError( Variant.Create( ex.Error ), Variant.Create( ex.Extended ), Variant.Create( ex.Return ) );
            }
        }

        [Inlineable]
        public Variant Chr( Variant ASCIIcode ) {
            SetError( 0, 0, 0 );

            try {
                return _variablesAndConversionsService.Chr( ASCIIcode );
            }
            catch(AutoJITExceptionBase ex) {
                return SetError( Variant.Create( ex.Error ), Variant.Create( ex.Extended ), Variant.Create( ex.Return ) );
            }
        }

        [Inlineable]
        public Variant ChrW( Variant UNICODEcode ) {
            SetError( 0, 0, 0 );

            try {
                return _variablesAndConversionsService.ChrW( UNICODEcode );
            }
            catch(AutoJITExceptionBase ex) {
                return SetError( Variant.Create( ex.Error ), Variant.Create( ex.Extended ), Variant.Create( ex.Return ) );
            }
        }

        public Variant ClipGet() {
            SetError( 0, 0, 0 );

            try {
                return _environmentService.ClipGet();
            }
            catch(AutoJITExceptionBase ex) {
                return SetError( Variant.Create( ex.Error ), Variant.Create( ex.Extended ), Variant.Create( ex.Return ) );
            }
        }

        public Variant ClipPut( Variant value ) {
            SetError( 0, 0, 0 );

            try {
                return _environmentService.ClipPut( value );
            }
            catch(AutoJITExceptionBase ex) {
                return SetError( Variant.Create( ex.Error ), Variant.Create( ex.Extended ), Variant.Create( ex.Return ) );
            }
        }

        public Variant ConsoleRead( Variant peek = null, Variant binary = null ) {
            SetError( 0, 0, 0 );

            if( peek == null ) {
                peek = false;
            }

            if( binary == null ) {
                binary = false;
            }

            int read;
            if( peek ) {
                read = Console.In.Peek();
            }
            else {
                read = Console.Read();
            }

            if( binary ) {
                return new[] {
                    (byte)read
                };
            }
            return new String( new[] {
                (char)read
            } );
        }

        public Variant ConsoleWrite( Variant data ) {
            SetError( 0, 0, 0 );

            string @string = data.GetString();

            Console.Write( @string );
            return @string.Length;
        }

        public Variant ConsoleWriteError( Variant data ) {
            SetError( 0, 0, 0 );

            if( data.IsBinary ) {
                byte[] binary = data.GetBinary();
                Console.Error.Write( binary.Select( x => (char)x ).ToArray() );
                return binary.Length;
            }
            string @string = data.GetString();
            Console.Error.Write( @string );
            return @string;
        }

        public Variant ControlClick( Variant title, Variant text, Variant controlID, Variant button = null, Variant clicks = null, Variant x = null, Variant y = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant ControlCommand( Variant title, Variant text, Variant controlID, Variant command, Variant option = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant ControlDisable( Variant title, Variant text, Variant controlID ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant ControlEnable( Variant title, Variant text, Variant controlID ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant ControlFocus( Variant title, Variant text, Variant controlID ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant ControlGetFocus( Variant title, Variant text = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant ControlGetHandle( Variant title, Variant text, Variant controlID ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant ControlGetPos( Variant title, Variant text, Variant controlID ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant ControlGetText( Variant title, Variant text, Variant controlID ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant ControlHide( Variant title, Variant text, Variant controlID ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant ControlListView( Variant title, Variant text, Variant controlID, Variant command, params Variant[] optionN ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant ControlMove( Variant title, Variant text, Variant controlID, Variant x, Variant y, Variant width = null, Variant height = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant ControlSend( Variant title, Variant text, Variant controlID, Variant @string, Variant flag = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant ControlSetText( Variant title, Variant text, Variant controlID, Variant newtext, Variant flag = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant ControlShow( Variant title, Variant text, Variant controlID ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant ControlTreeView( Variant title, Variant text, Variant controlID, Variant command, Variant option1 = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        [Inlineable]
        public Variant Cos( Variant expression ) {
            SetError( 0, 0, 0 );

            try {
                return _mathService.Cos( expression );
            }
            catch(AutoJITExceptionBase ex) {
                return SetError( Variant.Create( ex.Error ), Variant.Create( ex.Extended ), Variant.Create( ex.Return ) );
            }
        }

        [Inlineable]
        public Variant Dec( Variant hex, Variant flag = null ) {
            SetError( 0, 0, 0 );

            try {
                return _variablesAndConversionsService.Dec( hex, flag );
            }
            catch(AutoJITExceptionBase ex) {
                return SetError( Variant.Create( ex.Error ), Variant.Create( ex.Extended ), Variant.Create( ex.Return ) );
            }
        }

        public Variant DirCopy( Variant sourcedir, Variant destdir, Variant flag = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant DirCreate( Variant path ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant DirGetSize( Variant path, Variant flag = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant DirMove( Variant sourcedir, Variant destdir, Variant flag = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant DirRemove( Variant path, Variant recurse = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant DllCall( Variant dll, Variant returntype, Variant function, params Variant[] paramtypen ) {
            SetError( 0, 0, 0 );

            try {
                return _marshalService.DllCall( dll, returntype, function, paramtypen );
            }
            catch(AutoJITExceptionBase ex) {
                return SetError( Variant.Create( ex.Error ), Variant.Create( ex.Extended ), Variant.Create( ex.Return ) );
            }
        }

        public Variant DllCallAddress( Variant returntype, Variant address, params Variant[] paramtypen ) {
            SetError( 0, 0, 0 );

            try {
                return _marshalService.DllCallAddress( returntype, address, paramtypen );
            }
            catch(AutoJITExceptionBase ex) {
                return SetError( Variant.Create( ex.Error ), Variant.Create( ex.Extended ), Variant.Create( ex.Return ) );
            }
        }

        public Variant DllCallbackFree( Variant handle ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant DllCallbackGetPtr( Variant handle ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant DllCallbackRegister( Variant function, Variant returntype, params Variant[] @params ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant DllClose( Variant dllhandle ) {
            SetError( 0, 0, 0 );

            try {
                return _marshalService.DllClose( dllhandle );
            }
            catch(AutoJITExceptionBase ex) {
                return SetError( Variant.Create( ex.Error ), Variant.Create( ex.Extended ), Variant.Create( ex.Return ) );
            }
        }

        public Variant DllOpen( Variant filename ) {
            SetError( 0, 0, 0 );

            try {
                return _marshalService.DllOpen( filename );
            }
            catch(AutoJITExceptionBase ex) {
                return SetError( Variant.Create( ex.Error ), Variant.Create( ex.Extended ), Variant.Create( ex.Return ) );
            }
        }

        public Variant DllStructCreate( Variant Struct, Variant Pointer = null ) {
            SetError( 0, 0, 0 );

            try {
                return _marshalService.DllStructCreate( Struct, Pointer );
            }
            catch(AutoJITExceptionBase ex) {
                return SetError( Variant.Create( ex.Error ), Variant.Create( ex.Extended ), Variant.Create( ex.Return ) );
            }
        }

        public Variant DllStructGetData( Variant Struct, Variant Element, Variant index = null ) {
            SetError( 0, 0, 0 );

            if( index == null ) {
                index = Variant.Create( new Default() );
            }

            var runtimeStruct = Struct as StructVariant;

            try {
                return _marshalService.DllStructGetData( runtimeStruct, Element, index );
            }
            catch(AutoJITExceptionBase ex) {
                return SetError( Variant.Create( ex.Error ), Variant.Create( ex.Extended ), Variant.Create( ex.Return ) );
            }
        }

        public Variant DllStructGetPtr( Variant Struct, Variant Element = null ) {
            SetError( 0, 0, 0 );

            var structVariant = Struct as StructVariant;

            try {
                return _marshalService.DllStructGetPtr( structVariant, Element );
            }
            catch(AutoJITExceptionBase ex) {
                return SetError( Variant.Create( ex.Error ), Variant.Create( ex.Extended ), Variant.Create( ex.Return ) );
            }
        }

        public Variant DllStructGetSize( Variant Struct ) {
            SetError( 0, 0, 0 );

            var runtimeStruct = Struct.GetValue() as IRuntimeStruct;

            try {
                return _marshalService.DllStructGetSize( runtimeStruct );
            }
            catch(AutoJITExceptionBase ex) {
                return SetError( Variant.Create( ex.Error ), Variant.Create( ex.Extended ), Variant.Create( ex.Return ) );
            }
        }

        public Variant DllStructSetData( Variant Struct, Variant Element, Variant value, Variant index = null ) {
            SetError( 0, 0, 0 );

            if( index == null ) {
                index = 1;
            }

            var runtimeStruct = Struct as StructVariant;

            try {
                return _marshalService.DllStructSetData( runtimeStruct, Element, value, index );
            }
            catch(AutoJITExceptionBase ex) {
                return SetError( Variant.Create( ex.Error ), Variant.Create( ex.Extended ), Variant.Create( ex.Return ) );
            }
        }

        public Variant DriveGetDrive( Variant type ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant DriveGetFileSystem( Variant path ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant DriveGetLabel( Variant path ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant DriveGetSerial( Variant path ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant DriveGetType( Variant path, Variant operation = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant DriveMapAdd( Variant device, Variant remoteshare, Variant flags = null, Variant user = null, Variant password = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant DriveMapDel( Variant drive ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant DriveMapGet( Variant device ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant DriveSetLabel( Variant path, Variant label ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant DriveSpaceFree( Variant path ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant DriveSpaceTotal( Variant path ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant DriveStatus( Variant path ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant EnvGet( Variant envvariable ) {
            SetError( 0, 0, 0 );

            try {
                return _environmentService.EnvGet( envvariable );
            }
            catch(AutoJITExceptionBase ex) {
                return SetError( Variant.Create( ex.Error ), Variant.Create( ex.Extended ), Variant.Create( ex.Return ) );
            }
        }

        public Variant EnvSet( Variant envvariable, Variant value = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant EnvUpdate() {
            SetError( 0, 0, 0 );

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
            SetError( 0, 0, 0 );

            try {
                return _mathService.Exp( expression );
            }
            catch(AutoJITExceptionBase ex) {
                return SetError( Variant.Create( ex.Error ), Variant.Create( ex.Extended ), Variant.Create( ex.Return ) );
            }
        }

        public Variant FileChangeDir( Variant path ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant FileClose( Variant filehandle ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant FileCopy( Variant source, Variant dest, Variant flag = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant FileCreateNTFSLink( Variant source, Variant hardlink, Variant flag = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant FileCreateShortcut( Variant file, Variant lnk, Variant workdir = null, Variant args = null, Variant desc = null, Variant icon = null, Variant hotkey = null, Variant iconnumber = null, Variant state = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant FileDelete( Variant filename ) {
            SetError( 0, 0, 0 );

            string[] fragments = filename.GetString().Split( '\\' );
            string dir = string.Join( "\\", fragments.Take( fragments.Length-1 ) );
            string[] toDelete = Directory.GetFiles( dir, fragments.Last() );
            if( !toDelete.Any() ) {
                return 0;
            }

            try {
                foreach(string file in toDelete) {
                    File.Delete( file );
                }
            }
            catch(Exception) {
                return 0;
            }
            return 1;
        }

        public Variant FileExists( Variant path ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant FileFindFirstFile( Variant filename ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant FileFindNextFile( Variant search ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant FileFlush( Variant filehandle ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant FileGetAttrib( Variant filename ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant FileGetEncoding( Variant filehandlefilename, Variant mode = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant FileGetLongName( Variant filename, Variant flag = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant FileGetPos( Variant filehandle ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant FileGetShortName( Variant filename, Variant flag = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant FileGetShortcut( Variant lnk ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant FileGetSize( Variant filename ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant FileGetTime( Variant filename, Variant option = null, Variant format = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant FileGetVersion( Variant filename, Variant stringname = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant FileInstall( Variant source, Variant dest, Variant flag = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant FileMove( Variant source, Variant dest, Variant flag = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant FileOpen( Variant filename, Variant mode = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant FileOpenDialog( Variant title, Variant initdir, Variant filter, Variant options = null, Variant defaultname = null, Variant hwnd = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant FileRead( Variant filehandlefilename, Variant count = null ) {
            SetError( 0, 0, 0 );

            if( count == null ) {
                count = -1;
            }

            Stream fileStream;
            if( filehandlefilename.IsPtr ) {
                var fileHandle = new SafeFileHandle( filehandlefilename.GetIntPtr(), false );
                fileStream = new FileStream( fileHandle, FileAccess.Read );
            }
            else {
                fileStream = new FileStream( filehandlefilename.GetString(), FileMode.Open );
            }

            var streamReader = new StreamReader( fileStream );
            if( count < 0 ) {
                string readToEnd = streamReader.ReadToEnd();
                if( !filehandlefilename.IsPtr ) {
                    streamReader.Close();
                }

                return SetExtended( readToEnd.Length, readToEnd );
            }

            var buffer = new char[count];
            int read = streamReader.Read( buffer, (int)fileStream.Position, count );
            streamReader.Close();
            return SetExtended( read, new StringVariant( new string( buffer ) ) );
        }

        public Variant FileReadLine( Variant filehandlefilename, Variant line = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant FileReadToArray( Variant filehandlefilename ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant FileRecycle( Variant source ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant FileRecycleEmpty( Variant source ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant FileSaveDialog( Variant title, Variant initdir, Variant filter, Variant options = null, Variant defaultname = null, Variant hwnd = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant FileSelectFolder( Variant dialogtext, Variant rootdir, Variant flag = null, Variant initialdir = null, Variant hwnd = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant FileSetAttrib( Variant filepattern, Variant RASHNOT, Variant recurse = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant FileSetPos( Variant filehandle, Variant offset, Variant origin ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant FileSetTime( Variant filepattern, Variant time, Variant type = null, Variant recurse = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant FileWrite( Variant filehandlefilename, Variant textdata ) {
            SetError( 0, 0, 0 );

            try {
                FileStream fileStream;
                if( filehandlefilename.IsPtr ) {
                    fileStream = new FileStream( new SafeFileHandle( filehandlefilename, true ), FileAccess.Write );
                }
                else {
                    fileStream = new FileStream( filehandlefilename.GetString(), FileMode.Open, FileAccess.Write );
                }
                fileStream.Write( textdata.GetBinary(), 0, textdata.GetBinary().Length );
            }
            catch(Exception) {
                return false;
            }
            return true;
        }

        public Variant FileWriteLine( Variant filehandlefilename, Variant line ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        [Inlineable]
        public Variant Floor( Variant expression ) {
            SetError( 0, 0, 0 );

            try {
                return _mathService.Floor( expression );
            }
            catch(AutoJITExceptionBase ex) {
                return SetError( Variant.Create( ex.Error ), Variant.Create( ex.Extended ), Variant.Create( ex.Return ) );
            }
        }

        public Variant FtpSetProxy( Variant mode = null, Variant proxyport = null, Variant username = null, Variant password = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant FuncName( Variant Functionvariable ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant GUICreate( Variant title, Variant width = null, Variant height = null, Variant left = null, Variant top = null, Variant style = null, Variant exStyle = null, Variant parent = null ) {
            SetError( 0, 0, 0 );

            if( width == null ) {
                width = 0;
            }

            if( height == null ) {
                height = 0;
            }

            if( left == null ) {
                left = -1;
            }

            if( top == null ) {
                top = -1;
            }

            if( style == null ) {
                style = -1;
            }

            if( exStyle == null ) {
                exStyle = -1;
            }

            if( parent == null ) {
                parent = -1;
            }

            var form = new Form {
                Text = title, Width = width, Height = height, Left = left, Top = top, Visible = false
            };
            Control parentHandle = Control.FromHandle( new IntPtr( parent ) );
            if( parentHandle != null ) {
                form.Parent = parentHandle;
            }

            Task.Factory.StartNew( form.Show );

            _context.Guis.Add( form.Handle );
            return form.Handle;
        }

        public Variant GUICtrlCreateAvi( Variant filename, Variant subfileid, Variant left, Variant top, Variant width = null, Variant height = null, Variant style = null, Variant exStyle = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant GUICtrlCreateButton( Variant text, Variant left, Variant top, Variant width = null, Variant height = null, Variant style = null, Variant exStyle = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant GUICtrlCreateCheckbox( Variant text, Variant left, Variant top, Variant width = null, Variant height = null, Variant style = null, Variant exStyle = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant GUICtrlCreateCombo( Variant text, Variant left, Variant top, Variant width = null, Variant height = null, Variant style = null, Variant exStyle = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant GUICtrlCreateContextMenu( Variant controlID ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant GUICtrlCreateDate( Variant text, Variant left, Variant top, Variant width = null, Variant height = null, Variant style = null, Variant exStyle = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant GUICtrlCreateDummy( Variant d ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant GUICtrlCreateEdit( Variant text, Variant left, Variant top, Variant width = null, Variant height = null, Variant style = null, Variant exStyle = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant GUICtrlCreateGraphic( Variant left, Variant top, Variant width = null, Variant height = null, Variant style = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant GUICtrlCreateGroup( Variant text, Variant left, Variant top = null, Variant width = null, Variant height = null, Variant style = null, Variant exStyle = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant GUICtrlCreateIcon( Variant filename, Variant iconName, Variant left, Variant top, Variant width = null, Variant height = null, Variant style = null, Variant exStyle = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant GUICtrlCreateInput( Variant text, Variant left, Variant top, Variant width = null, Variant height = null, Variant style = null, Variant exStyle = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant GUICtrlCreateLabel( Variant text, Variant left, Variant top, Variant width = null, Variant height = null, Variant style = null, Variant exStyle = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant GUICtrlCreateList( Variant text, Variant left, Variant top, Variant width = null, Variant height = null, Variant style = null, Variant exStyle = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant GUICtrlCreateListView( Variant text, Variant left, Variant top, Variant width = null, Variant height = null, Variant style = null, Variant exStyle = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant GUICtrlCreateListViewItem( Variant text, Variant listviewID ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant GUICtrlCreateMenu( Variant submenutext, Variant menuID = null, Variant menuentry = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant GUICtrlCreateMenuItem( Variant text, Variant menuID, Variant menuentry = null, Variant menuradioitem = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant GUICtrlCreateMonthCal( Variant text, Variant left, Variant top, Variant width = null, Variant height = null, Variant style = null, Variant exStyle = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant GUICtrlCreateObj( Variant ObjectVar, Variant left, Variant top, Variant width = null, Variant height = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant GUICtrlCreatePic( Variant filename, Variant left, Variant top, Variant width = null, Variant height = null, Variant style = null, Variant exStyle = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant GUICtrlCreateProgress( Variant left, Variant top, Variant width = null, Variant height = null, Variant style = null, Variant exStyle = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant GUICtrlCreateRadio( Variant text, Variant left, Variant top, Variant width = null, Variant height = null, Variant style = null, Variant exStyle = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant GUICtrlCreateSlider( Variant left, Variant top, Variant width = null, Variant height = null, Variant style = null, Variant exStyle = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant GUICtrlCreateTab( Variant left, Variant top, Variant width = null, Variant height = null, Variant style = null, Variant exStyle = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant GUICtrlCreateTabItem( Variant text ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant GUICtrlCreateTreeView( Variant left, Variant top, Variant width, Variant height = null, Variant style = null, Variant exStyle = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant GUICtrlCreateTreeViewItem( Variant text, Variant treeviewID ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant GUICtrlCreateUpdown( Variant inputcontrolID, Variant style = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant GUICtrlDelete( Variant controlID ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant GUICtrlGetHandle( Variant controlID ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant GUICtrlGetState( Variant controlID ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant GUICtrlRead( Variant controlID, Variant advanced = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant GUICtrlRecvMsg( Variant controlID, Variant msg, Variant wParam = null, Variant lParamType = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant GUICtrlRegisterListViewSort( Variant controlID, Variant function ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant GUICtrlSendMsg( Variant controlID, Variant msg, Variant wParam, Variant lParam ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant GUICtrlSendToDummy( Variant controlID, Variant state = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant GUICtrlSetBkColor( Variant controlID, Variant backgroundcolor ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant GUICtrlSetColor( Variant controlID, Variant textcolor ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant GUICtrlSetCursor( Variant controlID, Variant cursorID ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant GUICtrlSetData( Variant controlID, Variant data, Variant @default = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant GUICtrlSetDefBkColor( Variant defbkcolor, Variant winhandle = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant GUICtrlSetDefColor( Variant deftextcolor, Variant winhandle = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant GUICtrlSetFont( Variant controlID, Variant size, Variant weight = null, Variant attribute = null, Variant fontname = null, Variant quality = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant GUICtrlSetGraphic( Variant controlID, Variant type, params Variant[] parN ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant GUICtrlSetImage( Variant controlID, Variant filename, Variant iconname = null, Variant icontype = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant GUICtrlSetLimit( Variant controlID, Variant max, Variant min = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant GUICtrlSetOnEvent( Variant controlID, Variant function ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant GUICtrlSetPos( Variant controlID, Variant left, Variant top = null, Variant width = null, Variant height = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant GUICtrlSetResizing( Variant controlID, Variant resizing ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant GUICtrlSetState( Variant controlID, Variant state ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant GUICtrlSetStyle( Variant controlID, Variant style, Variant exStyle = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant GUICtrlSetTip( Variant controlID, Variant tiptext, Variant title = null, Variant icon = null, Variant options = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant GUIDelete( Variant winhandle ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant GUIGetCursorInfo( Variant winhandle ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant GUIGetMsg( Variant advanced = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant GUIGetStyle( Variant winhandle = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant GUIRegisterMsg( Variant msgID, Variant function ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant GUISetAccelerators( Variant accelerators, Variant winhandle = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant GUISetBkColor( Variant background, Variant winhandle = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant GUISetCoord( Variant left, Variant top, Variant width = null, Variant height = null, Variant winhandle = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant GUISetCursor( Variant cursorID = null, Variant @override = null, Variant winhandle = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant GUISetFont( Variant size, Variant weight = null, Variant attribute = null, Variant fontname = null, Variant winhandle = null, Variant quality = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant GUISetHelp( Variant helpfile, Variant winhandle = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant GUISetIcon( Variant iconfile, Variant iconID = null, Variant winhandle = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant GUISetOnEvent( Variant specialID, Variant function, Variant winhandle = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant GUISetState( Variant flag = null, Variant winhandle = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant GUISetStyle( Variant Style, Variant ExStyle = null, Variant winhandle = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant GUIStartGroup( Variant winhandle = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant GUISwitch( Variant winhandle, Variant tabitemID = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant HWnd( Variant expression ) {
            SetError( 0, 0, 0 );

            try {
                return _variablesAndConversionsService.HWnd( expression );
            }
            catch(AutoJITExceptionBase ex) {
                return SetError( Variant.Create( ex.Error ), Variant.Create( ex.Extended ), Variant.Create( ex.Return ) );
            }
        }

        [Inlineable]
        public Variant Hex( Variant expression, Variant length = null ) {
            SetError( 0, 0, 0 );

            try {
                return _variablesAndConversionsService.Hex( expression, length );
            }
            catch(AutoJITExceptionBase ex) {
                return SetError( Variant.Create( ex.Error ), Variant.Create( ex.Extended ), Variant.Create( ex.Return ) );
            }
        }

        public Variant HotKeySet( Variant key, Variant function = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant HttpSetProxy( Variant mode = null, Variant proxyport = null, Variant username = null, Variant password = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant HttpSetUserAgent( Variant useragent ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant InetClose( Variant handle ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant InetGet( Variant URL, Variant filename, Variant options = null, Variant background = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant InetGetInfo( Variant handle = null, Variant index = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant InetGetSize( Variant URL, Variant options = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant InetRead( Variant URL, Variant options = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant IniDelete( Variant filename, Variant section, Variant key = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant IniRead( Variant filename, Variant section, Variant key, Variant @default ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant IniReadSection( Variant filename, Variant section ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant IniReadSectionNames( Variant filename ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant IniRenameSection( Variant filename, Variant section, Variant newsection, Variant flag = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant IniWrite( Variant filename, Variant section, Variant key, Variant value ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant IniWriteSection( Variant filename, Variant section, Variant data, Variant index = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant InputBox( Variant title, Variant prompt, Variant @default = null, Variant passwordchar = null, Variant width = null, Variant height = null, Variant left = null, Variant top = null, Variant timeout = null, Variant hwnd = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant Int( Variant expression, Variant flag = null ) {
            SetError( 0, 0, 0 );

            try {
                return _variablesAndConversionsService.Int( expression, flag );
            }
            catch(AutoJITExceptionBase ex) {
                return SetError( Variant.Create( ex.Error ), Variant.Create( ex.Extended ), Variant.Create( ex.Return ) );
            }
        }

        public Variant IsAdmin() {
            SetError( 0, 0, 0 );

            try {
                return _variablesAndConversionsService.IsAdmin();
            }
            catch(AutoJITExceptionBase ex) {
                return SetError( Variant.Create( ex.Error ), Variant.Create( ex.Extended ), Variant.Create( ex.Return ) );
            }
        }

        public Variant IsArray( Variant variable ) {
            SetError( 0, 0, 0 );

            try {
                return _variablesAndConversionsService.IsArray( variable );
            }
            catch(AutoJITExceptionBase ex) {
                return SetError( Variant.Create( ex.Error ), Variant.Create( ex.Extended ), Variant.Create( ex.Return ) );
            }
        }

        public Variant IsBinary( Variant variable ) {
            SetError( 0, 0, 0 );

            try {
                return _variablesAndConversionsService.IsBinary( variable );
            }
            catch(AutoJITExceptionBase ex) {
                return SetError( Variant.Create( ex.Error ), Variant.Create( ex.Extended ), Variant.Create( ex.Return ) );
            }
        }

        [Inlineable]
        public Variant IsBool( Variant variable ) {
            SetError( 0, 0, 0 );

            try {
                return _variablesAndConversionsService.IsBool( variable );
            }
            catch(AutoJITExceptionBase ex) {
                return SetError( Variant.Create( ex.Error ), Variant.Create( ex.Extended ), Variant.Create( ex.Return ) );
            }
        }

        public Variant IsDeclared( Variant expression ) {
            throw new NotSupportedException( "'IsDeclared'" );
        }

        public Variant IsDllStruct( Variant variable ) {
            SetError( 0, 0, 0 );

            try {
                return _variablesAndConversionsService.IsDllStruct( variable );
            }
            catch(AutoJITExceptionBase ex) {
                return SetError( Variant.Create( ex.Error ), Variant.Create( ex.Extended ), Variant.Create( ex.Return ) );
            }
        }

        [Inlineable]
        public Variant IsFloat( Variant variable ) {
            SetError( 0, 0, 0 );

            try {
                return _variablesAndConversionsService.IsFloat( variable );
            }
            catch(AutoJITExceptionBase ex) {
                return SetError( Variant.Create( ex.Error ), Variant.Create( ex.Extended ), Variant.Create( ex.Return ) );
            }
        }

        public Variant IsFunc( Variant expression ) {
            SetError( 0, 0, 0 );

            try {
                return _variablesAndConversionsService.IsFunc( expression );
            }
            catch(AutoJITExceptionBase ex) {
                return SetError( Variant.Create( ex.Error ), Variant.Create( ex.Extended ), Variant.Create( ex.Return ) );
            }
        }

        public Variant IsHWnd( Variant variable ) {
            SetError( 0, 0, 0 );

            try {
                return _variablesAndConversionsService.IsHWnd( variable );
            }
            catch(AutoJITExceptionBase ex) {
                return SetError( Variant.Create( ex.Error ), Variant.Create( ex.Extended ), Variant.Create( ex.Return ) );
            }
        }

        [Inlineable]
        public Variant IsInt( Variant variable ) {
            SetError( 0, 0, 0 );

            try {
                return _variablesAndConversionsService.IsInt( variable );
            }
            catch(AutoJITExceptionBase ex) {
                return SetError( Variant.Create( ex.Error ), Variant.Create( ex.Extended ), Variant.Create( ex.Return ) );
            }
        }

        [Inlineable]
        public Variant IsKeyword( Variant variable ) {
            SetError( 0, 0, 0 );

            try {
                return _variablesAndConversionsService.IsKeyword( variable );
            }
            catch(AutoJITExceptionBase ex) {
                return SetError( Variant.Create( ex.Error ), Variant.Create( ex.Extended ), Variant.Create( ex.Return ) );
            }
        }

        [Inlineable]
        public Variant IsNumber( Variant variable ) {
            SetError( 0, 0, 0 );

            try {
                return _variablesAndConversionsService.IsNumber( variable );
            }
            catch(AutoJITExceptionBase ex) {
                return SetError( Variant.Create( ex.Error ), Variant.Create( ex.Extended ), Variant.Create( ex.Return ) );
            }
        }

        public Variant IsObj( Variant variable ) {
            SetError( 0, 0, 0 );

            try {
                return _variablesAndConversionsService.IsObj( variable );
            }
            catch(AutoJITExceptionBase ex) {
                return SetError( Variant.Create( ex.Error ), Variant.Create( ex.Extended ), Variant.Create( ex.Return ) );
            }
        }

        public Variant IsPtr( Variant variable ) {
            SetError( 0, 0, 0 );

            try {
                return _variablesAndConversionsService.IsPtr( variable );
            }
            catch(AutoJITExceptionBase ex) {
                return SetError( Variant.Create( ex.Error ), Variant.Create( ex.Extended ), Variant.Create( ex.Return ) );
            }
        }

        [Inlineable]
        public Variant IsString( Variant variable ) {
            SetError( 0, 0, 0 );

            try {
                return _variablesAndConversionsService.IsString( variable );
            }
            catch(AutoJITExceptionBase ex) {
                return SetError( Variant.Create( ex.Error ), Variant.Create( ex.Extended ), Variant.Create( ex.Return ) );
            }
        }

        [Inlineable]
        public Variant Log( Variant expression ) {
            SetError( 0, 0, 0 );

            try {
                return _mathService.Log( expression );
            }
            catch(AutoJITExceptionBase ex) {
                return SetError( Variant.Create( ex.Error ), Variant.Create( ex.Extended ), Variant.Create( ex.Return ) );
            }
        }

        public Variant MemGetStats() {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        [Inlineable]
        public Variant Mod( Variant value1, Variant value2 ) {
            SetError( 0, 0, 0 );

            try {
                return _mathService.Mod( value1, value2 );
            }
            catch(AutoJITExceptionBase ex) {
                return SetError( Variant.Create( ex.Error ), Variant.Create( ex.Extended ), Variant.Create( ex.Return ) );
            }
        }

        public Variant MouseClick( Variant button, Variant x = null, Variant y = null, Variant clicks = null, Variant speed = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant MouseClickDrag( Variant button, Variant x1, Variant y1, Variant x2, Variant y2, Variant speed = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant MouseDown( Variant button ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant MouseGetCursor() {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant MouseGetPos( Variant dimension = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant MouseMove( Variant x, Variant y, Variant speed = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant MouseUp( Variant button ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant MouseWheel( Variant direction, Variant clicks = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant MsgBox( Variant flag, Variant title, Variant text, Variant timeout = null, Variant hwnd = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant Number( Variant expression, Variant flag = null ) {
            SetError( 0, 0, 0 );

            try {
                return _variablesAndConversionsService.Number( expression, flag );
            }
            catch(AutoJITExceptionBase ex) {
                return SetError( Variant.Create( ex.Error ), Variant.Create( ex.Extended ), Variant.Create( ex.Return ) );
            }
        }

        public Variant ObjCreate( Variant classname, Variant servername = null, Variant username = null, Variant password = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant ObjCreateInterface( Variant CLSID, Variant IID = null, Variant interfacedescription = null, Variant flag = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant ObjEvent( Variant ObjectVar, Variant functionprefix = null, Variant interfacename = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant ObjGet( Variant filename, Variant classname = null, Variant instance = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant ObjName( Variant Objectvariable, Variant Flag = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant OnAutoItExitRegister( Variant function ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant OnAutoItExitUnRegister( Variant function ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant Ping( Variant addresshostname, Variant timeout = null ) {
            SetError( 0, 0, 0 );

            if( timeout == null ) {
                timeout = 4000;
            }
            return new Ping().Send( addresshostname, timeout ).RoundtripTime;
        }

        public Variant PixelChecksum( Variant left, Variant top, Variant right, Variant bottom, Variant step = null, Variant hwnd = null, Variant mode = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant PixelGetColor( Variant x, Variant y, Variant hwnd = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant PixelSearch( Variant left, Variant top, Variant right, Variant bottom, Variant color, Variant shadevariation = null, Variant step = null, Variant hwnd = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant ProcessClose( Variant process ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant ProcessExists( Variant process ) {
            SetError( 0, 0, 0 );

            Process processesByName = Process.GetProcessesByName( process ).SingleOrDefault();
            if( processesByName != null ) {
                return processesByName.Id;
            }

            try {
                Process byId = Process.GetProcessById( process );
                return byId.Id;
            }
            catch(Exception) {
                return 0;
            }
        }

        public Variant ProcessGetStats( Variant process = null, Variant type = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant ProcessList( Variant name = null ) {
            SetError( 0, 0, 0 );

            Process[] processes = name == null
            ? Process.GetProcesses()
            : Process.GetProcessesByName( name );

            var toReturn = new Variant[processes.Length+1, 1];

            toReturn[0, 0] = processes.Length;

            for( int index = 0; index < processes.Length; index++ ) {
                Process process = processes[index];

                toReturn[index+1, 0] = process.ProcessName;
                toReturn[index+1, 1] = process.Id;
            }

            return toReturn;
        }

        public Variant ProcessSetPriority( Variant process, Variant priority ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant ProcessWait( Variant process, Variant timeout = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant ProcessWaitClose( Variant process, Variant timeout = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant ProgressOff() {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant ProgressOn( Variant title, Variant maintext, Variant subtext = null, Variant xpos = null, Variant ypos = null, Variant opt = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant ProgressSet( Variant percent, Variant subtext = null, Variant maintext = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant Ptr( Variant expression ) {
            SetError( 0, 0, 0 );

            try {
                return _variablesAndConversionsService.Ptr( expression );
            }
            catch(AutoJITExceptionBase ex) {
                return SetError( Variant.Create( ex.Error ), Variant.Create( ex.Extended ), Variant.Create( ex.Return ) );
            }
        }

        public Variant Random( Variant Min = null, Variant Max = null, Variant Flag = null ) {
            SetError( 0, 0, 0 );

            if( Min == null ) {
                Min = 0;
            }

            if( Max == null ) {
                Max = 1;
            }

            if( Flag == null ) {
                Flag = 0;
            }

            try {
                return _mathService.Random( Min, Max, Flag );
            }
            catch(AutoJITExceptionBase ex) {
                return SetError( Variant.Create( ex.Error ), Variant.Create( ex.Extended ), Variant.Create( ex.Return ) );
            }
        }

        public Variant RegDelete( Variant keyname, Variant valuename = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant RegEnumKey( Variant keyname, Variant instance ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant RegEnumVal( Variant keyname, Variant instance ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant RegRead( Variant keyname, Variant valuename ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant RegWrite( Variant keyname, Variant valuename = null, Variant type = null, Variant value = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        [Inlineable]
        public Variant Round( Variant expression, Variant decimalplaces = null ) {
            SetError( 0, 0, 0 );

            if( decimalplaces == null ) {
                decimalplaces = 0;
            }

            try {
                return _mathService.Round( expression, decimalplaces );
            }
            catch(AutoJITExceptionBase ex) {
                return SetError( Variant.Create( ex.Error ), Variant.Create( ex.Extended ), Variant.Create( ex.Return ) );
            }
        }

        public Variant Run( Variant program, Variant workingdir = null, Variant showflag = null, Variant opt_flag = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant RunAs( Variant username, Variant domain, Variant password, Variant logonflag, Variant program, Variant workingdir = null, Variant showflag = null, Variant optflag = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant RunAsWait( Variant username, Variant domain, Variant password, Variant logonflag, Variant program, Variant workingdir = null, Variant show_flag = null, Variant opt_flag = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant RunWait( Variant program, Variant workingdir = null, Variant showflag = null, Variant optflag = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant SRandom( Variant Seed ) {
            SetError( 0, 0, 0 );

            try {
                return _mathService.SRandom( Seed );
            }
            catch(AutoJITExceptionBase ex) {
                return SetError( Variant.Create( ex.Error ), Variant.Create( ex.Extended ), Variant.Create( ex.Return ) );
            }
        }

        public Variant Send( Variant keys, Variant flag = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant SendKeepActive( Variant title, Variant text = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant SetError( Variant code, Variant extended = null, Variant returnvalue = null ) {
            if( returnvalue == null ) {
                returnvalue = 0;
            }

            _context.Error = code;
            _context.Extended = extended;
            return returnvalue;
        }

        public Variant SetExtended( Variant code, Variant returnvalue = null ) {
            SetError( 0, 0, 0 );

            if( returnvalue == null ) {
                returnvalue = 0;
            }

            _context.Extended = code.GetInt();

            return returnvalue;
        }

        public Variant ShellExecute( Variant filename, Variant parameters = null, Variant workingdir = null, Variant verb = null, Variant showflag = null ) {
            SetError( 0, 0, 0 );

            if( parameters == null ) {
                parameters = string.Empty;
            }

            if( workingdir == null ) {
                workingdir = string.Empty;
            }

            if( verb == null ) {
                verb = string.Empty;
            }

            if( showflag == null ) {
                showflag = _context.SW_SHOW;
            }

            try {
                return _processService.ShellExecute( filename, parameters, workingdir, verb, showflag );
            }
            catch(AutoJITExceptionBase ex) {
                return SetError( Variant.Create( ex.Error ), Variant.Create( ex.Extended ), Variant.Create( ex.Return ) );
            }
        }

        public Variant ShellExecuteWait( Variant filename, Variant parameters = null, Variant workingdir = null, Variant verb = null, Variant showflag = null ) {
            SetError( 0, 0, 0 );

            if( parameters == null ) {
                parameters = string.Empty;
            }

            if( workingdir == null ) {
                workingdir = string.Empty;
            }

            if( verb == null ) {
                verb = string.Empty;
            }

            if( showflag == null ) {
                showflag = _context.SW_SHOW;
            }

            try {
                return _processService.ShellExecuteWait( filename, parameters, workingdir, verb, showflag );
            }
            catch(AutoJITExceptionBase ex) {
                return SetError( Variant.Create( ex.Error ), Variant.Create( ex.Extended ), Variant.Create( ex.Return ) );
            }
        }

        public Variant Shutdown( Variant code ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        [Inlineable]
        public Variant Sin( Variant expression ) {
            SetError( 0, 0, 0 );

            try {
                return _mathService.Sin( expression );
            }
            catch(AutoJITExceptionBase ex) {
                return SetError( Variant.Create( ex.Error ), Variant.Create( ex.Extended ), Variant.Create( ex.Return ) );
            }
        }

        public Variant Sleep( Variant delay ) {
            SetError( 0, 0, 0 );

            Thread.Sleep( delay );
            return 0;
        }

        public Variant SoundPlay( Variant filename, Variant wait = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant SoundSetWaveVolume( Variant percent ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant SplashImageOn( Variant title, Variant file, Variant width = null, Variant height = null, Variant xpos = null, Variant ypos = null, Variant opt = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant SplashOff( Variant expression ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant SplashTextOn( Variant title, Variant text, Variant w = null, Variant h = null, Variant xpos = null, Variant ypos = null, Variant opt = null, Variant fontname = null, Variant fontsz = null, Variant fontwt = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        [Inlineable]
        public Variant Sqrt( Variant expression ) {
            SetError( 0, 0, 0 );

            try {
                return _mathService.Sqrt( expression );
            }
            catch(AutoJITExceptionBase ex) {
                return SetError( Variant.Create( ex.Error ), Variant.Create( ex.Extended ), Variant.Create( ex.Return ) );
            }
        }

        public Variant StatusbarGetText( Variant title, Variant text = null, Variant part = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant StderrRead( Variant process_id, Variant peek = null, Variant binary = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant StdinWrite( Variant process_id, Variant data = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant StdioClose( Variant process_id ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant StdoutRead( Variant process_id, Variant peek = null, Variant binary = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        [Inlineable]
        public Variant String( Variant expression ) {
            SetError( 0, 0, 0 );

            try {
                return _variablesAndConversionsService.String( expression );
            }
            catch(AutoJITExceptionBase ex) {
                return SetError( Variant.Create( ex.Error ), Variant.Create( ex.Extended ), Variant.Create( ex.Return ) );
            }
        }

        [Inlineable]
        public Variant StringAddCR( Variant @string ) {
            SetError( 0, 0, 0 );

            try {
                return _stringService.StringAddCR( @string );
            }
            catch(AutoJITExceptionBase ex) {
                return SetError( Variant.Create( ex.Error ), Variant.Create( ex.Extended ), Variant.Create( ex.Return ) );
            }
        }

        public Variant StringCompare( Variant string1, Variant string2, Variant casesense = null ) {
            SetError( 0, 0, 0 );

            try {
                return _stringService.StringCompare( string1, string2, casesense );
            }
            catch(AutoJITExceptionBase ex) {
                return SetError( Variant.Create( ex.Error ), Variant.Create( ex.Extended ), Variant.Create( ex.Return ) );
            }
        }

        public Variant StringFormat( Variant formatcontrol, Variant var1, params Variant[] varN ) {
            SetError( 0, 0, 0 );

            try {
                return _stringService.StingFormat( formatcontrol, var1, varN );
            }
            catch(AutoJITExceptionBase ex) {
                return SetError( Variant.Create( ex.Error ), Variant.Create( ex.Extended ), Variant.Create( ex.Return ) );
            }
        }

        public Variant StringFromASCIIArray( Variant array, Variant start = null, Variant end = null, Variant encoding = null ) {
            SetError( 0, 0, 0 );

            try {
                return _stringService.StingFromASCIIArray( array, start, end, encoding );
            }
            catch(AutoJITExceptionBase ex) {
                return SetError( Variant.Create( ex.Error ), Variant.Create( ex.Extended ), Variant.Create( ex.Return ) );
            }
        }

        public Variant StringInStr( Variant @string, Variant substring, Variant casesense = null, Variant occurrence = null, Variant start = null, Variant count = null ) {
            SetError( 0, 0, 0 );

            try {
                return _stringService.StingInString( @string, substring, casesense, occurrence, start, count );
            }
            catch(AutoJITExceptionBase ex) {
                return SetError( Variant.Create( ex.Error ), Variant.Create( ex.Extended ), Variant.Create( ex.Return ) );
            }
        }

        [Inlineable]
        public Variant StringIsASCII( Variant @string ) {
            SetError( 0, 0, 0 );

            try {
                return _stringService.StingIsASCII( @string );
            }
            catch(AutoJITExceptionBase ex) {
                return SetError( Variant.Create( ex.Error ), Variant.Create( ex.Extended ), Variant.Create( ex.Return ) );
            }
        }

        public Variant StringIsAlNum( Variant @string ) {
            SetError( 0, 0, 0 );

            try {
                return _stringService.StringIsAINum( @string );
            }
            catch(AutoJITExceptionBase ex) {
                return SetError( Variant.Create( ex.Error ), Variant.Create( ex.Extended ), Variant.Create( ex.Return ) );
            }
        }

        [Inlineable]
        public Variant StringIsAlpha( Variant @string ) {
            SetError( 0, 0, 0 );

            try {
                return _stringService.StingIsAlpha( @string );
            }
            catch(AutoJITExceptionBase ex) {
                return SetError( Variant.Create( ex.Error ), Variant.Create( ex.Extended ), Variant.Create( ex.Return ) );
            }
        }

        [Inlineable]
        public Variant StringIsDigit( Variant @string ) {
            SetError( 0, 0, 0 );

            try {
                return _stringService.StingIsDigit( @string );
            }
            catch(AutoJITExceptionBase ex) {
                return SetError( Variant.Create( ex.Error ), Variant.Create( ex.Extended ), Variant.Create( ex.Return ) );
            }
        }

        public Variant StringIsFloat( Variant @string ) {
            SetError( 0, 0, 0 );

            try {
                return _stringService.StingIsFloat( @string );
            }
            catch(AutoJITExceptionBase ex) {
                return SetError( Variant.Create( ex.Error ), Variant.Create( ex.Extended ), Variant.Create( ex.Return ) );
            }
        }

        public Variant StringIsInt( Variant @string ) {
            SetError( 0, 0, 0 );

            try {
                return _stringService.StingIsInt( @string );
            }
            catch(AutoJITExceptionBase ex) {
                return SetError( Variant.Create( ex.Error ), Variant.Create( ex.Extended ), Variant.Create( ex.Return ) );
            }
        }

        [Inlineable]
        public Variant StringIsLower( Variant @string ) {
            SetError( 0, 0, 0 );

            try {
                return _stringService.StringIsLower( @string );
            }
            catch(AutoJITExceptionBase ex) {
                return SetError( Variant.Create( ex.Error ), Variant.Create( ex.Extended ), Variant.Create( ex.Return ) );
            }
        }

        [Inlineable]
        public Variant StringIsSpace( Variant @string ) {
            SetError( 0, 0, 0 );

            try {
                return _stringService.StringIsSpace( @string );
            }
            catch(AutoJITExceptionBase ex) {
                return SetError( Variant.Create( ex.Error ), Variant.Create( ex.Extended ), Variant.Create( ex.Return ) );
            }
        }

        [Inlineable]
        public Variant StringIsUpper( Variant @string ) {
            SetError( 0, 0, 0 );

            try {
                return _stringService.StringIsUpper( @string );
            }
            catch(AutoJITExceptionBase ex) {
                return SetError( Variant.Create( ex.Error ), Variant.Create( ex.Extended ), Variant.Create( ex.Return ) );
            }
        }

        [Inlineable]
        public Variant StringIsXDigit( Variant @string ) {
            SetError( 0, 0, 0 );

            try {
                return _stringService.StringIsXDigit( @string );
            }
            catch(AutoJITExceptionBase ex) {
                return SetError( Variant.Create( ex.Error ), Variant.Create( ex.Extended ), Variant.Create( ex.Return ) );
            }
        }

        [Inlineable]
        public Variant StringLeft( Variant @string, Variant count ) {
            SetError( 0, 0, 0 );

            try {
                return _stringService.StringLeft( @string, count );
            }
            catch(AutoJITExceptionBase ex) {
                return SetError( Variant.Create( ex.Error ), Variant.Create( ex.Extended ), Variant.Create( ex.Return ) );
            }
        }

        [Inlineable]
        public Variant StringLen( Variant @string ) {
            SetError( 0, 0, 0 );

            try {
                return _stringService.StringLen( @string );
            }
            catch(AutoJITExceptionBase ex) {
                return SetError( Variant.Create( ex.Error ), Variant.Create( ex.Extended ), Variant.Create( ex.Return ) );
            }
        }

        [Inlineable]
        public Variant StringLower( Variant @string ) {
            SetError( 0, 0, 0 );

            try {
                return _stringService.StringLower( @string );
            }
            catch(AutoJITExceptionBase ex) {
                return SetError( Variant.Create( ex.Error ), Variant.Create( ex.Extended ), Variant.Create( ex.Return ) );
            }
        }

        [Inlineable]
        public Variant StringMid( Variant @string, Variant start, Variant count = null ) {
            SetError( 0, 0, 0 );

            string toMid = @string.GetString();

            if( count == null ) {
                count = toMid.Length-start;
            }

            try {
                return _stringService.StringMid( toMid, start, count );
            }
            catch(AutoJITExceptionBase ex) {
                return SetError( Variant.Create( ex.Error ), Variant.Create( ex.Extended ), Variant.Create( ex.Return ) );
            }
        }

        public Variant StringRegExp( Variant test, Variant pattern, Variant flag = null, Variant offset = null ) {
            SetError( 0, 0, 0 );

            try {
                return _stringService.StringRegExp( test, pattern, flag, offset );
            }
            catch(AutoJITExceptionBase ex) {
                return SetError( Variant.Create( ex.Error ), Variant.Create( ex.Extended ), Variant.Create( ex.Return ) );
            }
        }

        public Variant StringRegExpReplace( Variant test, Variant pattern, Variant replace, Variant count = null ) {
            SetError( 0, 0, 0 );

            try {
                return _stringService.StringRegExpReplace( test, pattern, replace, count );
            }
            catch(AutoJITExceptionBase ex) {
                return SetError( Variant.Create( ex.Error ), Variant.Create( ex.Extended ), Variant.Create( ex.Return ) );
            }
        }

        public Variant StringReplace( Variant @string, Variant searchstringstart, Variant replacestring, Variant occurrence = null, Variant casesense = null ) {
            SetError( 0, 0, 0 );

            try {
                return _stringService.StringReplace( @string, searchstringstart, replacestring, occurrence, casesense );
            }
            catch(AutoJITExceptionBase ex) {
                return SetError( Variant.Create( ex.Error ), Variant.Create( ex.Extended ), Variant.Create( ex.Return ) );
            }
        }

        [Inlineable]
        public Variant StringReverse( Variant @string, Variant flag = null ) {
            SetError( 0, 0, 0 );

            if( flag == null ) {
                flag = 0;
            }

            try {
                return _stringService.StringReverse( @string, flag );
            }
            catch(AutoJITExceptionBase ex) {
                return SetError( Variant.Create( ex.Error ), Variant.Create( ex.Extended ), Variant.Create( ex.Return ) );
            }
        }

        [Inlineable]
        public Variant StringRight( Variant @string, Variant count ) {
            SetError( 0, 0, 0 );

            try {
                return _stringService.StringRight( @string, count );
            }
            catch(AutoJITExceptionBase ex) {
                return SetError( Variant.Create( ex.Error ), Variant.Create( ex.Extended ), Variant.Create( ex.Return ) );
            }
        }

        public Variant StringSplit( Variant @string, Variant delimiters, Variant flag = null ) {
            SetError( 0, 0, 0 );

            try {
                return _stringService.StringSplit( @string, delimiters, flag );
            }
            catch(AutoJITExceptionBase ex) {
                return SetError( Variant.Create( ex.Error ), Variant.Create( ex.Extended ), Variant.Create( ex.Return ) );
            }
        }

        [Inlineable]
        public Variant StringStripCR( Variant @string ) {
            SetError( 0, 0, 0 );

            try {
                return _stringService.StringStripCR( @string );
            }
            catch(AutoJITExceptionBase ex) {
                return SetError( Variant.Create( ex.Error ), Variant.Create( ex.Extended ), Variant.Create( ex.Return ) );
            }
        }

        [Inlineable]
        public Variant StringStripWS( Variant @string, Variant flag ) {
            SetError( 0, 0, 0 );

            try {
                return _stringService.StringStripWS( @string, flag );
            }
            catch(AutoJITExceptionBase ex) {
                return SetError( Variant.Create( ex.Error ), Variant.Create( ex.Extended ), Variant.Create( ex.Return ) );
            }
        }

        public Variant StringToASCIIArray( Variant @string, Variant start = null, Variant end = null, Variant encoding = null ) {
            SetError( 0, 0, 0 );

            string toConvert = @string.GetString();

            if( start == null ) {
                start = 0;
            }

            if( end == null ) {
                end = toConvert.Length;
            }

            if( encoding == null ) {
                encoding = 0;
            }

            try {
                return _stringService.StringToASCIIArray( toConvert, start, end, encoding );
            }
            catch(AutoJITExceptionBase ex) {
                return SetError( Variant.Create( ex.Error ), Variant.Create( ex.Extended ), Variant.Create( ex.Return ) );
            }
        }

        public Variant StringToBinary( Variant expression, Variant flag = null ) {
            SetError( 0, 0, 0 );

            if( flag == null ) {
                flag = 1;
            }

            try {
                return _variablesAndConversionsService.StringToBinary( expression, flag );
            }
            catch(AutoJITExceptionBase ex) {
                return SetError( Variant.Create( ex.Error ), Variant.Create( ex.Extended ), Variant.Create( ex.Return ) );
            }
        }

        [Inlineable]
        public Variant StringTrimLeft( Variant @string, Variant count ) {
            SetError( 0, 0, 0 );

            try {
                return _stringService.StringTrimLeft( @string, count );
            }
            catch(AutoJITExceptionBase ex) {
                return SetError( Variant.Create( ex.Error ), Variant.Create( ex.Extended ), Variant.Create( ex.Return ) );
            }
        }

        [Inlineable]
        public Variant StringTrimRight( Variant @string, Variant count ) {
            SetError( 0, 0, 0 );

            try {
                return _stringService.StringTrimRight( @string, count );
            }
            catch(AutoJITExceptionBase ex) {
                return SetError( Variant.Create( ex.Error ), Variant.Create( ex.Extended ), Variant.Create( ex.Return ) );
            }
        }

        [Inlineable]
        public Variant StringUpper( Variant @string ) {
            SetError( 0, 0, 0 );

            try {
                return _stringService.StringUpper( @string );
            }
            catch(AutoJITExceptionBase ex) {
                return SetError( Variant.Create( ex.Error ), Variant.Create( ex.Extended ), Variant.Create( ex.Return ) );
            }
        }

        public Variant TCPAccpet( Variant mainsocket ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant TCPCloseSocket( Variant socket ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant TCPConnect( Variant IPAddr, Variant port ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant TCPListen( Variant IPAddr, Variant port, Variant MaxPendingConnection = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant TCPNameToIP( Variant name ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant TCPRecv( Variant mainsocket, Variant maxlen, Variant flag = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant TCPSend( Variant mainsocket, Variant data ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant TCPShutdown() {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        private Variant UDPShutdown() {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant TCPStartup() {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        private Variant UDPStartup() {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        [Inlineable]
        public Variant Tan( Variant expression ) {
            SetError( 0, 0, 0 );

            try {
                return _mathService.Tan( expression );
            }
            catch(AutoJITExceptionBase ex) {
                return SetError( Variant.Create( ex.Error ), Variant.Create( ex.Extended ), Variant.Create( ex.Return ) );
            }
        }

        public Variant TimerDiff( Variant handle ) {
            SetError( 0, 0, 0 );

            Variant tickDiff = ( Stopwatch.GetTimestamp()-handle ) / Stopwatch.Frequency * 1000;
            return tickDiff;
        }

        public Variant TimerInit() {
            SetError( 0, 0, 0 );

            return Stopwatch.GetTimestamp();
        }

        public Variant ToolTip( Variant text, Variant x = null, Variant y = null, Variant title = null, Variant icon = null, Variant options = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant TrayCreateItem( Variant text, Variant menuID = null, Variant menuentry = null, Variant menuradioitem = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant TrayCreateMenu( Variant submenutext, Variant menuID = null, Variant menuentry = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant TrayGetMsg() {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant TrayItemDelete( Variant controlID ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant TrayItemGetHandle( Variant controlID ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant TrayItemGetState( Variant controlID = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant TrayItemGetText( Variant controlID ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant TrayItemSetOnEvent( Variant itemID, Variant function ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant TrayItemSetState( Variant controlID, Variant state ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant TrayItemSetText( Variant controlID, Variant text ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant TraySetClick( Variant flag ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant TraySetIcon( Variant filename = null, Variant iconID = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant TraySetOnEvent( Variant specialID, Variant function ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant TraySetPauseIcon( Variant filename = null, Variant iconID = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant TraySetState( Variant flag = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant TraySetToolTip( Variant text = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant TrayTip( Variant title, Variant text, Variant timeout, Variant option = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant UBound( Variant Array, Variant Dimension = null ) {
            SetError( 0, 0, 0 );

            if( Dimension == null ) {
                Dimension = 1;
            }

            try {
                return _variablesAndConversionsService.UBound( Array, Dimension );
            }
            catch(AutoJITExceptionBase ex) {
                return SetError( Variant.Create( ex.Error ), Variant.Create( ex.Extended ), Variant.Create( ex.Return ) );
            }
        }

        public Variant UDPBind( Variant IPAddr, Variant port ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant UDPCloseSocket( Variant socketarray ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant UDPOpen( Variant IPAddr, Variant port, Variant flag = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant UDPRecv( Variant socketarray, Variant maxlen, Variant flag = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant UDPSend( Variant socketarray, Variant data ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant VarGetType( Variant expression ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant WinActivate( Variant title, Variant text = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant WinActive( Variant title, Variant text = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant WinClose( Variant title, Variant text = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant WinExists( Variant title, Variant text = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant WinFlash( Variant title, Variant text = null, Variant flashes = null, Variant delay = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant WinGetCaretPos() {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant WinGetClassList( Variant title, Variant text = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant WinGetClientSize( Variant title, Variant text = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant WinGetHandle( Variant title, Variant text = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant WinGetPos( Variant title, Variant text = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant WinGetProcess( Variant title, Variant text = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant WinGetState( Variant title, Variant text = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant WinGetText( Variant title, Variant text = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant WinGetTitle( Variant title, Variant text = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant WinKill( Variant title, Variant text = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant WinList( Variant title = null, Variant text = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant WinMenuSelectItem( Variant title, Variant text, params Variant[] items ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant WinMinimizeAll() {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant WinMinimizeAllUndo() {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant WinMove( Variant title, Variant text, Variant x, Variant y, Variant width = null, Variant height = null, Variant speed = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant WinSetOnTop( Variant title, Variant text, Variant flag ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant WinSetState( Variant title, Variant text, Variant flag ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant WinSetTitle( Variant title, Variant text, Variant newtitle ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant WinSetTrans( Variant title, Variant text, Variant transparency ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant WinWait( Variant title, Variant text = null, Variant timeout = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant WinWaitActive( Variant title, Variant text = null, Variant timeout = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant WinWaitClose( Variant title, Variant text = null, Variant timeout = null ) {
            SetError( 0, 0, 0 );

            throw new NotImplementedException();
        }

        public Variant WinWaitNotActive( Variant title, Variant text = null, Variant timeout = null ) {
            SetError( 0, 0, 0 );

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
            if( a.IsString
                || b.IsString ) {
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
            if( toTest.IsInt32
                || toTest.IsInt64
                || toTest.IsDouble ) {
                return toTest >= @from && toTest <= to;
            }
            if( toTest.IsString ) {
                return System.String.CompareOrdinal( toTest.GetString(), @from.GetString() ) >= 0 && string.CompareOrdinal( toTest.GetString(), to.GetString() ) <= 0;
            }
            throw new NotImplementedException();
        }

        [AutoJITCompilerFunction]
        public void Exit( Variant exitCode = null ) {
            if( exitCode == null ) {
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
