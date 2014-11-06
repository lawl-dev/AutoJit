using System.Runtime.InteropServices;

namespace AutoJITRuntime
{
    internal class MiscService
    {
        [DllImport( "user32.dll" )]
        private static extern bool BlockInput( bool fBlockIt );

        public Variant BlockInput( Variant flag ) {
            return BlockInput( flag.GetBool() );
        }
    }
}
