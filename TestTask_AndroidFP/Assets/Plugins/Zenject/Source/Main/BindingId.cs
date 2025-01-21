using ModestTree;
using System;
using System.Diagnostics;

namespace Zenject
{
	[DebuggerStepThrough]
	public struct BindingId : IEquatable<BindingId>
	{
		public Type Type
		{
			get => _type;
			set => _type = value;
		}

		public object Identifier
		{
			get => _identifier;
			set => _identifier = value;
		}
		object _identifier;
		Type _type;

		public BindingId(Type type, object identifier)
		{
			_type = type;
			_identifier = identifier;
		}

		public bool Equals(BindingId that)
		{
			return this == that;
		}

		public override string ToString()
		{
			if (_identifier == null)
				return _type.PrettyName();

			return "{0} (ID: {1})".Fmt(_type, _identifier);
		}

		public override int GetHashCode()
		{
			unchecked // Overflow is fine, just wrap
			{
				var hash = 17;
				hash = hash * 29 + _type.GetHashCode();
				hash = hash * 29 + (_identifier == null ? 0 : _identifier.GetHashCode());
				return hash;
			}
		}

		public override bool Equals(object other)
		{
			if (other is BindingId)
			{
				var otherId = (BindingId) other;
				return otherId == this;
			}

			return false;
		}

		public static bool operator ==(BindingId left, BindingId right)
		{
			return left.Type == right.Type && Equals(left.Identifier, right.Identifier);
		}

		public static bool operator !=(BindingId left, BindingId right)
		{
			return !left.Equals(right);
		}
	}
}