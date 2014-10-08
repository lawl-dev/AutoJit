using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;

namespace Lawl.Networking.FrameworkHelper
{
    internal sealed class DefaultNetworkBrowser : INetworkBrowser
    {
        [DllImport("Netapi32", CharSet = CharSet.Auto, SetLastError = true), SuppressUnmanagedCodeSecurity]
        public static extern int NetServerEnum( 
            string ServerNane,
            int dwLevel,
            ref IntPtr pBuf,
            int dwPrefMaxLen,
            out int dwEntriesRead,
            out int dwTotalEntries,
            int dwServerType,
            string domain,
            out int dwResumeHandle
            );

        [DllImport("Netapi32", SetLastError = true), SuppressUnmanagedCodeSecurity]      
        public static extern int NetApiBufferFree(IntPtr pBuf);

        
        [StructLayout(LayoutKind.Sequential)]
        public struct ServerInfo100
        {
            internal int sv100_platform_id;
            [MarshalAs(UnmanagedType.LPWStr)]
            internal string sv100_name;
        }
        
        public List<string> GetNetworkComputers()
        {
            var networkComputers = new List<string>();
            const int MAX_PREFERRED_LENGTH = -1;
            int SV_TYPE_WORKSTATION = 1;
            int SV_TYPE_SERVER = 2;
            IntPtr buffer = IntPtr.Zero;
            IntPtr tmpBuffer = IntPtr.Zero;
            int entriesRead = 0;
            int totalEntries = 0;
            int resHandle = 0;
            int sizeofINFO = Marshal.SizeOf(typeof(ServerInfo100));


            try
            {
                int ret = NetServerEnum(null, 100, ref buffer,
                    MAX_PREFERRED_LENGTH,
                    out entriesRead,
                    out totalEntries, SV_TYPE_WORKSTATION |
                                      SV_TYPE_SERVER, null, out 
                                          resHandle);
                
                if (ret == 0)
                {
                    for (int i = 0; i < totalEntries; i++)
                    {
                        tmpBuffer = new IntPtr((int)buffer +
                                               (i * sizeofINFO));
                        var svrInfo = (ServerInfo100)
                            Marshal.PtrToStructure(tmpBuffer,
                                typeof(ServerInfo100));

                        networkComputers.Add(svrInfo.sv100_name);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                NetApiBufferFree(buffer);
            }
            return networkComputers;
        }
    }
}