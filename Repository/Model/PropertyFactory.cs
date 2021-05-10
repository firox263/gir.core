﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository.Model
{
    internal class PropertyFactory
    {
        private readonly TransferFactory _transferFactory;
        private readonly TypeReferenceFactory _typeReferenceFactory;
        private readonly CaseConverter _caseConverter;
        private readonly TypeInformationFactory _typeInformationFactory;

        public PropertyFactory(TransferFactory transferFactory, TypeReferenceFactory typeReferenceFactory, CaseConverter caseConverter, TypeInformationFactory typeInformationFactory)
        {
            _transferFactory = transferFactory;
            _typeReferenceFactory = typeReferenceFactory;
            _caseConverter = caseConverter;
            _typeInformationFactory = typeInformationFactory;
        }

        private Property Create(Xml.Property property, NamespaceName namespaceName)
        {
            if (property.Name is null)
                throw new Exception("Property is missing a name");

            return new Property(
                elementName: new ElementName(property.Name),
                symbolName: new SymbolName(_caseConverter.ToPascalCase(property.Name)),
                typeReference: _typeReferenceFactory.Create(property, namespaceName),
                writeable: property.Writeable,
                readable: property.Readable,
                typeInformation: _typeInformationFactory.Create(property),
                transfer: _transferFactory.FromText(property.TransferOwnership)
            );
        }

        public IEnumerable<Property> Create(IEnumerable<Xml.Property> properties, NamespaceName namespaceName)
            => properties.Select(x => Create(x, namespaceName)).ToList();
    }
}