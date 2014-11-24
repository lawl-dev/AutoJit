using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using AutoJITRuntime.Exceptions;
using AutoJITRuntime.Variants;

namespace AutoJITRuntime
{
    public abstract class Variant : IEnumerable
    {
        private static readonly Dictionary<Type, Func<object, Variant>> CreateStrategies = new Dictionary<Type, Func<object, Variant>> {
            { typeof (int), o => Create( (int) o ) },
            { typeof (byte[]), o => Create( (byte[]) o ) },
            { typeof (Int64), o => Create( (Int64) o ) },
            { typeof (double), o => Create( (double) o ) },
            { typeof (bool), o => Create( (bool) o ) },
            { typeof (string), o => Create( (string) o ) },
            { typeof (IntPtr), o => Create( (IntPtr) o ) },
            { typeof (Default), o => Create( (Default) o ) },
            { typeof (char), o => Create( (char) o ) },
            { typeof (StringBuilder), o => Create( (StringBuilder) o ) },
            { typeof (Variant[]), o => Create( (Variant[]) o ) },
            { typeof (Variant[,]), o => Create( (Variant[,]) o ) },
            { typeof (Variant[,,]), o => Create( (Variant[,,]) o ) },
            { typeof (Variant), o => (Variant) o },
            { typeof (byte), o => Create( (byte) o ) },
            { typeof (UIntPtr), o => Create( unchecked( (IntPtr) (long) (ulong) (UIntPtr) o ) ) },
            { typeof (Int16), o => Create( (Int16) o ) },
            { typeof (UInt16), o => Create( (UInt16) o ) }, {
                typeof (UInt32), o => {
                    var uint32 = (UInt32) o;
                    if ( uint32 > int.MaxValue
                         ||
                         uint32 < int.MinValue ) {
                        return Create( uint32 );
                    }
                    return Create( (Int32) uint32 );
                }
            },
            { typeof (UInt64), o => Create( (Int64) (UInt64) o ) },
            { typeof (float), o => Create( (float) o ) }
        };

        #region explicit_operaotr
        public static implicit operator Int32( Variant a ) {
            return a.GetInt();
        }

        public static implicit operator Int64( Variant a ) {
            return a.GetInt64();
        }

        public static implicit operator Double( Variant a ) {
            return a.GetDouble();
        }

        public static implicit operator String( Variant a ) {
            return a.GetString();
        }

        public static implicit operator Boolean( Variant a ) {
            return a.GetBool();
        }

        public static implicit operator IntPtr( Variant a ) {
            return a.GetIntPtr();
        }

        public static implicit operator byte[]( Variant a ) {
            return a.GetBinary();
        }
        #endregion

        public abstract DataType DataType { get; }

        public virtual bool IsInt32 {
            get { return false; }
        }

        public virtual bool IsInt64 {
            get { return false; }
        }

        public virtual bool IsDouble {
            get { return false; }
        }

        public virtual bool IsBool {
            get { return false; }
        }

        public virtual bool IsString {
            get { return false; }
        }

        public virtual bool IsArray {
            get { return false; }
        }

        public virtual bool IsPtr {
            get { return false; }
        }

        public virtual bool IsStruct {
            get { return false; }
        }

        public virtual bool IsBinary {
            get { return false; }
        }

        public virtual bool IsDefault {
            get { return false; }
        }

        public virtual bool IsNull {
            get { return false; }
        }

        public virtual Variant this[ params int[] index ] {
            get { throw new AutoJITRuntimerException( "" ); }
            set { throw new AutoJITRuntimerException( "" ); }
        }

        public virtual IEnumerator GetEnumerator() {
            return new VariantEnumerator( (IEnumerable) GetValue() );
        }

        public static Variant Create( Int32 @int ) {
            return new Int32Variant( @int );
        }

        public static Variant Create( Int64 int64 ) {
            return new Int64Variant( int64 );
        }

        public static Variant Create( Double int64 ) {
            return new DoubleVariant( int64 );
        }

        public static Variant Create( Boolean @bool ) {
            return new BoolVariant( @bool );
        }

        public static Variant Create( String @string ) {
            return new StringVariant( @string );
        }

        public static Variant Create( StringBuilder @string ) {
            return new StringVariant( @string.ToString() );
        }

        public static Variant Create( Variant[] @array ) {
            return new ArrayVariant( array );
        }

        public static Variant Create( Variant[,] @array ) {
            return new ArrayVariant( array );
        }

        public static Variant Create( Variant[,,] @array ) {
            return new ArrayVariant( array );
        }

        public static Variant Create( IntPtr ptr ) {
            return new PtrVariant( ptr );
        }

        public static Variant Create( IRuntimeStruct @struct ) {
            return new StructVariant( @struct );
        }

        public static Variant Create( byte[] binary ) {
            return new BinaryVariant( binary );
        }

        public static Variant Create( Default @default ) {
            return new DefaultVariant();
        }

        public static Variant Create( Char @char ) {
            return new StringVariant( @char.ToString( CultureInfo.InvariantCulture ) );
        }

        public static Variant CreateFunction( object instance, string name ) {
            return new FunctionVariant( instance, name );
        }

        public static Variant Create( object @object ) {
            if ( @object == null ) {
                return new NullVariant();
            }
            if ( @object is NullVariant ) {
                return new NullVariant();
            }

            var runtimeStruct = @object as IRuntimeStruct;
            if ( runtimeStruct != null ) {
                return Create( runtimeStruct );
            }

            if ( @object is Variant ) {
                return CreateStrategies[( (Variant) @object ).GetRealType()]( ( (Variant) @object ).GetValue() );
            }
            return CreateStrategies[@object.GetType()]( @object );
        }

        public abstract object GetValue();

        public virtual Variant Invoke( params object[] parameter ) {
            throw new NotImplementedException();
        }

        public Variant PowAssign( Variant b ) {
            return Math.Pow( GetDouble(), b.GetDouble() );
        }

        public Variant ConcatAssign( Variant b ) {
            return GetString()+b.GetString();
        }

        public abstract string GetString();
        public abstract bool GetBool();
        public abstract double GetDouble();
        public abstract Int64 GetInt64();
        public abstract int GetInt();
        public abstract IntPtr GetIntPtr();

        public virtual void ReDim( params Variant[] indexs ) {
            throw new AutoJITRuntimerException( "\"ReDim\" used without an array variable." );
        }

        public virtual void InitArray( Variant[] variants ) {
            throw new AutoJITRuntimerException( "" );
        }

        public abstract byte[] GetBinary();
        public abstract Type GetRealType();

        public static Variant operator +( Variant a, Variant b ) {
            switch (a.DataType) {
                case DataType.Null:
                case DataType.Int32:
                case DataType.Double:
                case DataType.String:
                case DataType.Int64:
                    return a.GetDouble()+b.GetDouble();
                case DataType.IntPtr:
                    return a.GetIntPtr()+b.GetInt();
                default:
                    throw new NotImplementedException( a.DataType.ToString() );
            }
        }

        public static Variant operator -( Variant a ) {
            switch (a.DataType) {
                case DataType.Int32:
                    return -a.GetInt();
                case DataType.Int64:
                    return -a.GetInt64();
                case DataType.Double:
                    return -a.GetDouble();
                default:
                    throw new NotImplementedException();
            }
        }

        public static Variant operator -( Variant a, Variant b ) {
            switch (a.DataType) {
                case DataType.Double:
                case DataType.String:
                case DataType.Int32:
                case DataType.Int64:
                    return a.GetDouble()-b.GetDouble();
                case DataType.IntPtr:
                    return a.GetIntPtr()-b.GetInt();
                default:
                    throw new NotImplementedException( a.DataType.ToString() );
            }
        }

        public static Variant operator *( Variant a, Variant b ) {
            switch (a.DataType) {
                case DataType.Int32:
                case DataType.Int64:
                case DataType.Double:
                case DataType.String:
                    return a.GetDouble() * b.GetDouble();
                case DataType.IntPtr:
                    return a.GetInt() * b.GetInt();
                case DataType.Bool:
                    return a.GetBool()
                        ? b
                        : (Variant) 0;
                default:
                    throw new NotImplementedException( a.DataType.ToString() );
            }
        }

        public static bool operator ==( Variant a, Variant b ) {
            if ( ReferenceEquals( a, b ) ) {
                return true;
            }

            if ( ( (object) a == null )
                 ||
                 ( (object) b == null ) ) {
                return false;
            }

            if ( a.IsString
                 ||
                 b.IsString ) {
                return string.Compare( a.ToString(), b.ToString(), StringComparison.InvariantCultureIgnoreCase ) == 0;
            }

            DataType dataType = GetDatatypeToCompare( a.DataType, b.DataType );
            switch (dataType) {
                case DataType.Int32:
                    return a.GetInt() == b.GetInt();
                case DataType.Int64:
                    return a.GetInt64() == b.GetInt64();
                case DataType.Double:
                    return Math.Abs( a.GetDouble()-b.GetDouble() ) < double.Epsilon;
                case DataType.String:
                    return a.GetString() == b.GetString();
            }
            return false;
        }

        public static bool operator !=( Variant a, Variant b ) {
            return !( a == b );
        }

        public static Variant operator /( Variant a, Variant b ) {
            return a.GetDouble() / b.GetDouble();
        }

        public static Variant operator ^( Variant a, Variant b ) {
            switch (a.DataType) {
                case DataType.Double:
                case DataType.Int32:
                    return a.GetInt()^b.GetInt();
                case DataType.Int64:
                    return a.GetInt64()^b.GetInt64();
                case DataType.IntPtr:
                    return a.GetInt()^b.GetInt();
                case DataType.String:
                    throw new NotImplementedException();
                default:
                    throw new NotImplementedException( a.DataType.ToString() );
            }
        }

        public static Variant operator |( Variant a, Variant b ) {
            switch (a.DataType) {
                case DataType.Double:
                case DataType.Int32:
                    return a.GetInt()|b.GetInt();
                case DataType.Int64:
                    return a.GetInt64()|b.GetInt64();
                case DataType.IntPtr:
                    return a.GetInt()|b.GetInt();
                case DataType.String:
                    throw new NotImplementedException();
                case DataType.Bool:
                    return a.GetBool()|b.GetBool();
                default:
                    throw new NotImplementedException( a.DataType.ToString() );
            }
        }

        public static bool operator false( Variant a ) {
            return !a.GetBool();
        }

        public static bool operator true( Variant a ) {
            return a.GetBool();
        }

        public static Variant operator >( Variant a, Variant b ) {
            DataType toCompare = GetDatatypeToCompare( a.DataType, b.DataType );
            switch (toCompare) {
                case DataType.Int32:
                    return a.GetInt() > b.GetInt();
                case DataType.Int64:
                    return a.GetInt64() > b.GetInt64();
                case DataType.Double:
                    return a.GetDouble() > b.GetDouble();
                case DataType.IntPtr:
                    return a.GetInt() > b.GetInt();
                case DataType.String:
                    throw new NotImplementedException();
                default:
                    throw new NotImplementedException( a.DataType.ToString() );
            }
        }

        public static Variant operator <=( Variant a, Variant b ) {
            return a < b || a == b;
        }

        public static Variant operator >=( Variant a, Variant b ) {
            return a > b || a == b;
        }

        public static Variant operator <( Variant a, Variant b ) {
            DataType toCompare = GetDatatypeToCompare( a.DataType, b.DataType );
            switch (toCompare) {
                case DataType.Int32:
                    return a.GetInt() < b.GetInt();
                case DataType.Int64:
                    return a.GetInt64() < b.GetInt64();
                case DataType.Double:
                    return a.GetDouble() < b.GetDouble();
                case DataType.IntPtr:
                    return a.GetInt() < b.GetInt();
                case DataType.String:
                    throw new NotImplementedException();
                default:
                    throw new NotImplementedException( a.DataType.ToString() );
            }
        }

        private static DataType GetDatatypeToCompare( DataType a, DataType b ) {
            var compareType = DataType.Int32;

            switch (a) {
                case DataType.Int32:
                    switch (b) {
                        case DataType.Int32:
                            compareType = DataType.Int32;
                            break;
                        case DataType.Int64:
                            compareType = DataType.Int64;
                            break;
                        case DataType.Double:
                            compareType = DataType.Double;
                            break;
                        case DataType.String:
                            compareType = DataType.Double;
                            break;
                    }
                    break;
                case DataType.Int64:
                    switch (b) {
                        case DataType.Int32:
                        case DataType.Int64:
                            compareType = DataType.Int64;
                            break;
                        case DataType.Double:
                        case DataType.String:
                            compareType = DataType.Double;
                            break;
                    }
                    break;
                case DataType.Double:
                    compareType = DataType.Double;
                    break;
                case DataType.String:
                    switch (b) {
                        case DataType.String:
                            compareType = DataType.String;
                            break;
                    }
                    compareType = DataType.Double;
                    break;
            }
            return compareType;
        }

        public static implicit operator Variant( Variant[] a ) {
            return new ArrayVariant( a );
        }

        public static implicit operator Variant( Variant[,] a ) {
            return new ArrayVariant( a );
        }

        public static implicit operator Variant( Variant[,,] a ) {
            return new ArrayVariant( a );
        }

        public static implicit operator Variant( int a ) {
            return new Int32Variant( a );
        }

        public static implicit operator Variant( Int64 a ) {
            return new Int64Variant( a );
        }

        public static implicit operator Variant( double a ) {
            return new DoubleVariant( a );
        }

        public static implicit operator Variant( string a ) {
            return new StringVariant( a );
        }

        public static implicit operator Variant( bool a ) {
            return new BoolVariant( a );
        }

        public static implicit operator Variant( byte[] a ) {
            return new BinaryVariant( a );
        }

        public static implicit operator Variant( IntPtr a ) {
            return new PtrVariant( a );
        }

        public override string ToString() {
            return GetString();
        }

        public override bool Equals( object obj ) {
            var variant = obj as Variant;
            if ( variant != null ) {
                return GetValue().Equals( variant.GetValue() );
            }
            return GetValue().Equals( obj );
        }

        public override int GetHashCode() {
            return GetValue().GetHashCode();
        }

        public static Variant CreateArray( Variant array ) {
            Init( (Array) array.GetValue() );
            return array;
        }

        private static void Init( Array array ) {
            var indicies = new int[array.Rank];
            SetDimension( array, indicies, 0 );
        }

        private static void SetDimension( Array array, int[] indicies, int dimension ) {
            for ( int i = 0; i <= array.GetUpperBound( dimension ); i++ ) {
                indicies[dimension] = i;

                if ( dimension < array.Rank-1 ) {
                    SetDimension( array, indicies, dimension+1 );
                }
                else {
                    array.SetValue( new NullVariant(), indicies );
                }
            }
        }
    }
}
