using System;
using System.Linq;
using System.Reflection;

namespace AutoJITRuntime.Variants
{
	public sealed class FunctionVariant : Variant
	{
		private readonly object _instance;
		private readonly string _name;
		private readonly MethodInfo _methodInfo;

		public FunctionVariant(object instance, string name) {
			_instance = instance;
			_name = name;
			_methodInfo = _instance.GetType().GetMethods().Single(x=>x.Name.Equals( name, StringComparison.InvariantCultureIgnoreCase ));
		}

		protected override DataType DataType {
			get {
				return DataType.Function;
			}
		}

		public override Variant Invoke( params object[] parameter ) {
			var result = _methodInfo.Invoke( _instance, parameter );
			return (Variant)result;
		}

		public override object GetValue() {
			throw new NotImplementedException();
		}

		public override string GetString() {
			return string.Empty;
		}

		public override bool GetBool() {
			return false;
		}

		public override double GetDouble() {
			return 0.0d;
		}

		public override long GetInt64() {
			return 0;
		}

		public override int GetInt() {
			return 0;
		}

		public override IntPtr GetIntPtr() {
			throw new NotImplementedException();
		}

		public override byte[] GetBinary() {
			throw new NotImplementedException();
		}

		public override Type GetRealType() {
			throw new NotImplementedException();
		}
	}
}