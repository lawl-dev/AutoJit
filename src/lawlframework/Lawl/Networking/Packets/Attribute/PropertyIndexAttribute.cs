namespace Lawl.Networking.Packets.Attribute
{
    public class PropertyIndexAttribute : System.Attribute
    {
        public int Index { get; private set; }

        public PropertyIndexAttribute( int index ) {
            Index = index;
        }
    }
}