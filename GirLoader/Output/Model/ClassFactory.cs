﻿using System;

namespace GirLoader.Output.Model
{
    internal class ClassFactory
    {
        private readonly TypeReferenceFactory _typeReferenceFactory;
        private readonly MethodFactory _methodFactory;
        private readonly PropertyFactory _propertyFactory;
        private readonly FieldFactory _fieldFactory;
        private readonly SignalFactory _signalFactory;

        public ClassFactory(TypeReferenceFactory typeReferenceFactory, MethodFactory methodFactory, PropertyFactory propertyFactory, FieldFactory fieldFactory, SignalFactory signalFactory)
        {
            _typeReferenceFactory = typeReferenceFactory;
            _methodFactory = methodFactory;
            _propertyFactory = propertyFactory;
            _fieldFactory = fieldFactory;
            _signalFactory = signalFactory;
        }

        public Class Create(Input.Model.Class cls, Repository repository)
        {
            if (cls.Name is null)
                throw new Exception("Class is missing data");

            if (cls.GetTypeFunction is null)
                throw new Exception($"Class {cls.Name} is missing a get type function");

            CTypeName? cTypeName = null;
            if (cls.Type is { })
                cTypeName = new CTypeName(cls.Type);

            return new Class(
                repository: repository,
                typeName: new TypeName(cls.Name),
                symbolName: new SymbolName(cls.Name),
                cTypeName: cTypeName,
                parent: CreateParentTypeReference(cls.Parent, repository.Namespace.Name),
                implements: _typeReferenceFactory.Create(cls.Implements, repository.Namespace.Name),
                methods: _methodFactory.Create(cls.Methods, repository.Namespace.Name),
                functions: _methodFactory.Create(cls.Functions, repository.Namespace.Name),
                getTypeFunction: _methodFactory.CreateGetTypeMethod(cls.GetTypeFunction),
                properties: _propertyFactory.Create(cls.Properties, repository.Namespace.Name),
                fields: _fieldFactory.Create(cls.Fields, repository),
                signals: _signalFactory.Create(cls.Signals, repository.Namespace.Name),
                constructors: _methodFactory.Create(cls.Constructors, repository.Namespace.Name),
                isFundamental: cls.Fundamental
            );
        }

        private TypeReference? CreateParentTypeReference(string? parentName, NamespaceName currentNamespace)
        {
            if (parentName is { })
                return CreateTypeReference(parentName, currentNamespace);

            return null;
        }

        private TypeReference CreateTypeReference(string type, NamespaceName currentNamespace)
        {
            string? ctype = null;
            if (!type.Contains("."))
            {
                //Prefix type if it is not prefixed already
                ctype = currentNamespace + type;
            }
            return _typeReferenceFactory.Create(type, ctype, currentNamespace);
        }
    }
}
