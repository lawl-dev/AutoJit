using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Lawl.Reflection;

namespace AutoJITRuntime
{
    internal class AutoitRuntimeSpike<T>
    {
        private readonly AutoitContext<T> _context;

        public AutoitRuntimeSpike( AutoitContext<T> context ) {
            _context = context;
        }

        private object CallDll(Variant returntype, Variant function, Variant[] paramtypen, IntPtr library)
        {
            var procAddress = CompilerRuntimeHelper.GetProcAddress(library, function);

            var parameters = new List<object>();
            var typeNames = new List<string>();
            for (int i = 0; i < paramtypen.Length; i += 2)
            {
                var typeName = paramtypen[i];
                typeNames.Add(typeName);
                parameters.Add(CompilerRuntimeHelper.ConverToManagedType(typeName, paramtypen[i + 1]));
            }

            var delegateType = AutoitTypeBridge.CreateMarshalDelegate(returntype, typeNames.ToArray());


            var objects = parameters.ToArray();
            var res = Marshal.GetDelegateForFunctionPointer(procAddress, delegateType).DynamicInvoke(objects);

            for (int i = 0; i < paramtypen.Length; i++)
            {
                if (paramtypen[i].IsStruct)
                {
                    var intPtr = (IntPtr)objects[i / 2];
                    Marshal.PtrToStructure(intPtr, paramtypen[i].GetValue());
                }
            }

            return res;
        }

        public Variant DllCall( Variant dll, Variant returntype, Variant function, Variant[] paramtypen ) {
            object res = null;
            if (dll.IsString)
            {
                var library = CompilerRuntimeHelper.LoadLibrary(dll);
                res = CallDll(returntype, function, paramtypen, library);
                CompilerRuntimeHelper.FreeLibrary(library);
            }

            if (dll.IsPtr)
            {
                res = CallDll(returntype, function, paramtypen, dll);
            }

            var toReturn = new Variant[paramtypen.Length / 2 + 1];
            toReturn[0] = Variant.Create(res);
            var objects = new List<object>();
            for (int i = 1; i < paramtypen.Length; i++)
            {
                if (i % 2 != 0) objects.Add(paramtypen[i]);
            }

            for (int i = 0; i < objects.Count; i++)
            {
                toReturn[i + 1] = Variant.Create(objects[i]);
            }
            return toReturn;
        }

        public Variant DllClose( Variant dllhandle ) {
            CompilerRuntimeHelper.FreeLibrary(dllhandle);
            return Variant.Create((object)null);
        }

        public Variant DllOpen( Variant filename ) {
            try
            {
                return CompilerRuntimeHelper.LoadLibrary(filename);
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public Variant DllStructCreate( Variant Struct, Variant pointer ) {
            var runtimeStruct = AutoitTypeBridge.CreateRuntimeStruct(Struct);
            var @struct = runtimeStruct.CreateInstanceWithDefaultParameters<IRuntimeStruct>();
            return Variant.Create(@struct);
        }

        public Variant DllStructGetData( Variant Struct, Variant Element, Variant Index ) {
            var structVariant = Struct as StructVariant;

            if (Element.IsString)
            {
                return Variant.Create(structVariant.GetElement(Element.ToString()));
            }

            if (Element.IsInt32)
            {
                return Variant.Create(structVariant.GetElement(Element.GetInt() - 1));
            }
            throw new NotImplementedException();
        }

        public Variant DllStructGetPtr( Variant Struct, Variant Element ) {
            return Struct.GetIntPtr();
        }

        public Variant DllStructGetSize(Variant Struct)
        {
            if (!Struct.IsStruct)
            {
                _context.Error = 1;
                return Variant.Create((object)null);
            }
            return Marshal.SizeOf(Struct.GetValue());
        }

        public Variant DllStructSetData( Variant Struct, Variant Element, Variant value, Variant index ) {
            var structVariant = Struct as StructVariant;

            Type elementType = null;
            if (Element.IsString)
            {
                elementType = structVariant.GetElement(Element.GetString()).GetType();
            }
            if (Element.IsInt32)
            {
                elementType = structVariant.GetElement(Element.GetInt() - 1).GetType();
            }

            if (value.IsString && elementType == typeof(byte[]) && elementType.IsArray)
            {
                var bytes = value.GetBinary();
                if (index == null || index.IsDefault)
                {
                    index = 1;
                }

                var currentBytes = new byte[] { };
                if (Element.IsString)
                {
                    currentBytes = (byte[])structVariant.GetElement(Element.GetString());
                }
                if (Element.IsInt32)
                {
                    currentBytes = (byte[])structVariant.GetElement(Element.GetInt() - 1);
                }

                Array.Copy(bytes, 0, currentBytes, index - 1, currentBytes.Length - index - 1);


                if (Element.IsString)
                {
                    structVariant.SetElement(Element.GetString(), bytes);
                    return bytes;
                }
                if (Element.IsInt32)
                {
                    structVariant.SetElement(Element.GetInt() - 1, bytes);
                    return bytes;
                }
            }
            throw new NotImplementedException();
        }
    }
}