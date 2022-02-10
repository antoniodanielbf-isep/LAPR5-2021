using System;

namespace DDDSample1.Domain.Shared
{
    /// <summary>
    ///     Base class for entities.
    /// </summary>
    public abstract class EntityId : IEquatable<EntityId>, IComparable<EntityId>
    {
        protected EntityId(object value)
        {
            if (value.GetType() == typeof(string))
                ObjValue = createFromString((string) value);
            else
                ObjValue = value;
        }


        protected object ObjValue { get; }

        public string Value
        {
            get
            {
                if (ObjValue.GetType() == typeof(string))
                    return (string) ObjValue;
                return AsString();
            }
        }

        public int CompareTo(EntityId other)
        {
            if (other == null)
                return -1;
            return Value.CompareTo(other.Value);
        }

        public bool Equals(EntityId other)
        {
            if (other == null)
                return false;
            if (GetType() != other.GetType())
                return false;
            return Value == other.Value;
        }


        protected abstract object createFromString(string text);

        public abstract string AsString();


        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is EntityId other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public static bool operator ==(EntityId obj1, EntityId obj2)
        {
            if (Equals(obj1, null))
            {
                if (Equals(obj2, null)) return true;
                return false;
            }

            return obj1.Equals(obj2);
        }

        public static bool operator !=(EntityId x, EntityId y)
        {
            return !(x == y);
        }
    }
}