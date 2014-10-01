using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Lawl.Networking.Packets.Attribute;
using Lawl.Networking.Packets.ConstantClass;

namespace Lawl.Networking.Packets.Base
{
    public abstract class PacketBase 
    {
        public abstract int PacketType { get; }
        public abstract string PacketGuid { get; set; }


        private static readonly Dictionary<Type, List<PropertyInfo>> PropertyInfoCache;
        static PacketBase()
        {
            PropertyInfoCache = new Dictionary<Type, List<PropertyInfo>>();
        }

        private static IEnumerable<PropertyInfo> GetPropertyInfo<T>(T packet) where T : PacketBase
        {
            Type type = packet.GetType();
            if (!PropertyInfoCache.ContainsKey(type))
            {
                PropertyInfoCache.Add(type, packet.DataProperties.ToList());
            }
            return PropertyInfoCache[type];
        }

        private IEnumerable<PropertyInfo> DataProperties
        {
            get
            {
                var res = new List<PropertyInfo>();
                var allProperties = GetType().GetProperties();
                var propsFromDerivat = allProperties
                    .Where(x => x.GetCustomAttributes(typeof (PropertyIndexAttribute), true).Length > 0)
                    .Where(p => p.Name != "PacketGuid")
                    .OrderBy(
                        x =>
                            ((PropertyIndexAttribute)
                                x.GetCustomAttributes(typeof (PropertyIndexAttribute), false).First()).Index);
                var propsFromBase = allProperties.Single(x=> x.Name == "PacketGuid");
                res.Add(propsFromBase);
                res.AddRange(propsFromDerivat);
                return res;
            }
        }
        

        public virtual PacketBase Read(Stream stream)
        {
            using (var binaryReader = new BinaryReader(stream, Encoding.UTF8, true))
            {
                foreach (var dataProperty in GetPropertyInfo(this))
                {
                    object data;
                    var propertyType = dataProperty.PropertyType;
                    if (propertyType.IsEnum)
                    {
                        propertyType = System.Enum.GetUnderlyingType(propertyType);
                    }
                    switch (propertyType.Name)
                    {
                        case ValueTypes.String:
                            data = binaryReader.ReadString();
                            break;
                        case ValueTypes.Bool:
                            data = binaryReader.ReadBoolean();
                            break;
                        case ValueTypes.Int32:
                            data = binaryReader.ReadInt32();
                            break;
                        case ValueTypes.GenericList:
                            data = DeserializeList(binaryReader, dataProperty);
                            break;
                        case ValueTypes.Float:
                            data = binaryReader.ReadSingle();
                            break;
                        default:
                            throw new NotImplementedException();
                    }
                    dataProperty.SetValue(this, data, null);
                }
            }
            return this;
        }

        
        public virtual byte[] ToByteArray()
        {
            byte[] res;
            using (var memoryStream = new MemoryStream())
            {
                using (var binaryWriter = new BinaryWriter(memoryStream))
                {
                    binaryWriter.Write(PacketType);
                    foreach (var propertyInfo in GetPropertyInfo(this))
                    {
                        var propertyType = propertyInfo.PropertyType;
                        if (propertyType.IsEnum)
                        {
                            propertyType = System.Enum.GetUnderlyingType(propertyType);
                        }

                        switch (propertyType.Name)
                        {
                            case ValueTypes.String:
                                binaryWriter.Write((string)propertyInfo.GetValue(this, null) ?? string.Empty);
                                break;
                            case ValueTypes.Float:
                                binaryWriter.Write((float)propertyInfo.GetValue(this, null));
                                break;
                            case ValueTypes.Int32:
                                binaryWriter.Write((Int32)propertyInfo.GetValue(this, null));
                                break;
                            case ValueTypes.Bool:
                                binaryWriter.Write((bool)propertyInfo.GetValue(this, null));
                                break;
                            case ValueTypes.GenericList:
                                var type = propertyType.GetGenericArguments()[0];
                                if (type.IsEnum)
                                {
                                    type = System.Enum.GetUnderlyingType(type);
                                }
                                WriteList(type, propertyInfo, binaryWriter);
                                break;
                            default:
                                throw new NotImplementedException();
                        }
                    }
                }
                res = memoryStream.ToArray();
            }
            return res;
        }

        private object DeserializeList(BinaryReader binaryReader, PropertyInfo dataProperty)
        {
            var type = dataProperty.PropertyType.GetGenericArguments()[0];
            if (type.IsEnum)
            {
                type = System.Enum.GetUnderlyingType(type);
            }


            var length = binaryReader.ReadInt32();

            switch (type.Name)
            {
                case ValueTypes.String:
                    return DeserializeListOfType<string>(length, binaryReader);
                case ValueTypes.Float:
                    return DeserializeListOfType<float>(length, binaryReader);
                case ValueTypes.Int32:
                    return DeserializeListOfType<int>(length, binaryReader);
                case ValueTypes.Bool:
                    return DeserializeListOfType<bool>(length, binaryReader);
                default:
                    throw new NotImplementedException();
            }
        }


        private object DeserializeListOfType<T>(int length, BinaryReader reader)
        {
            var toReturn = new List<T>();
            switch (typeof(T).Name)
            {
                case ValueTypes.String:
                    FillListOfType(toReturn, reader.ReadString, length);
                    break;
                case ValueTypes.Float:
                    FillListOfType(toReturn, reader.ReadSingle, length);
                    break;
                case ValueTypes.Int32:
                    FillListOfType(toReturn, reader.ReadInt32, length);
                    break;
                case ValueTypes.Bool:
                    FillListOfType(toReturn, reader.ReadBoolean, length);
                    break;
                default:
                    throw new NotImplementedException();
            }
            return toReturn;
        }


        private void WriteList(Type type, PropertyInfo listPropertyInfo, BinaryWriter writer)
        {
            var list = (IEnumerable<object>) listPropertyInfo.GetValue(this, null);

            if (list == null || !list.Any())
            {
                writer.Write(0);
                return;
            }
            
            writer.Write(list.Count());


            switch (type.Name)
            {
                case ValueTypes.String:
                    var strList = (List<string>) listPropertyInfo.GetValue(this, null);
                    foreach (string t in strList)
                    {
                        writer.Write(t ?? string.Empty);
                    }
                    break;
                case ValueTypes.Int32:
                    var intList = (List<int>) listPropertyInfo.GetValue(this, null);
                    foreach (int t in intList)
                    {
                        writer.Write(t);
                    }
                    break;
                case ValueTypes.Float:
                    var singleList = (List<float>) listPropertyInfo.GetValue(this, null);
                    foreach (Single t in singleList)
                    {
                        writer.Write(t);
                    }
                    break; 
                case ValueTypes.Bool:
                    var boolList = (List<bool>) listPropertyInfo.GetValue(this, null);
                    foreach (bool t in boolList)
                    {
                        writer.Write(t);
                    }
                    break;    
                default:
                    throw new NotImplementedException();
            }
        }

        private void FillListOfType<T>(object toFill, Func<T> readerDelegate, int length)
        {
            var list = toFill as List<T>;
            if (list == null) return;


            for (var i = 0; i < length; i++)
            {
                list.Add(readerDelegate.Invoke());
            }
        }
    }
}
