// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.

namespace System.Data.Entity.Core.Metadata.Edm
{
    using System.Collections.Generic;
    using System.Data.Entity.Core.Mapping;

    // TODO: METADATA: Rename?
    public class DbDatabaseMapping
    {
        private readonly List<StorageEntityContainerMapping> _entityContainerMappings
            = new List<StorageEntityContainerMapping>();

        public EdmModel Model { get; set; }
        public EdmModel Database { get; set; }

        internal IList<StorageEntityContainerMapping> EntityContainerMappings
        {
            get { return _entityContainerMappings; }
        }
    }
}
