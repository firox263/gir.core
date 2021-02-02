﻿using System;
using Repository.Model;

#nullable enable

namespace Repository.Analysis
{
    public enum ReferenceType
    {
        Internal,
        External
    }

    public interface ITypeReference
    {
        IType? Type { get;  }
        bool IsForeign { get;  }
        bool IsArray { get; }
        string Name { get; }
    }

    internal interface IResolveable : ITypeReference
    {
        void ResolveAs(IType type, ReferenceType referenceType);
    }
    
    public class TypeReference : IEquatable<TypeReference>, ITypeReference, IResolveable
    {
        public IType? Type { get; private set; }
        public bool IsForeign { get; private set; }
        public bool IsArray { get; }
        public string Name { get; }

        public TypeReference(string unresolvedName, bool isArray)
        {
            Name = unresolvedName;
            IsArray = isArray;
        }

        public override string ToString()
        {
            // TODO: More advanced type resolution logic?

            if (Type is null)
                throw new InvalidOperationException("The Type Reference has not been resolved. It cannot be printed.");

            // Fundamental Type
            if (Type.Namespace == null)
                return Type.ManagedName;

            // External Array
            if (IsForeign && IsArray)
                return $"{Type.Namespace}.{Type.ManagedName}[]";

            // External Type
            if (IsForeign)
                return $"{Type.Namespace}.{Type.ManagedName}";

            // Internal Array
            if (IsArray)
                return $"{Type.ManagedName}[]";

            // Internal Type
            return Type.ManagedName;
        }

        public void ResolveAs(IType type, ReferenceType referenceType)
        {
            Type = type;
            IsForeign = (referenceType == ReferenceType.External);
        }

        public bool Equals(TypeReference? other)
        {
            if (ReferenceEquals(null, other))
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return IsArray == other.IsArray
                   && Name == other.Name;
        }

        public override bool Equals(object? obj)
        {
            if (obj?.GetType() != this.GetType())
                return false;

            return Equals((TypeReference) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(IsArray, Name);
        }
    }
}
