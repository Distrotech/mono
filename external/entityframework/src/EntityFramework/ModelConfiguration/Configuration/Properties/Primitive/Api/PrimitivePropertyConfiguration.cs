// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.

namespace System.Data.Entity.ModelConfiguration.Configuration
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Core.Metadata.Edm;
    using System.Data.Entity.ModelConfiguration.Configuration.Properties.Primitive;
    using System.Data.Entity.Utilities;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    ///     Used to configure a primitive property of an entity type or complex type.
    ///     This configuration functionality is available via the Code First Fluent API, see <see cref="DbModelBuilder" />.
    /// </summary>
    public class PrimitivePropertyConfiguration<TConfiguration>
        where TConfiguration : PrimitivePropertyConfiguration
    {
        private readonly TConfiguration _configuration;

        internal PrimitivePropertyConfiguration(TConfiguration configuration)
        {
            DebugCheck.NotNull(configuration);

            _configuration = configuration;
        }

        internal TConfiguration Configuration
        {
            get { return _configuration; }
        }

        /// <summary>
        ///     Configures the property to be optional.
        ///     The database column used to store this property will be nullable.
        /// </summary>
        /// <returns> The same PrimitivePropertyConfiguration instance so that multiple calls can be chained. </returns>
        public PrimitivePropertyConfiguration<TConfiguration> IsOptional()
        {
            Configuration.IsNullable = true;

            return this;
        }

        /// <summary>
        ///     Configures the property to be required.
        ///     The database column used to store this property will be non-nullable.
        /// </summary>
        /// <returns> The same PrimitivePropertyConfiguration instance so that multiple calls can be chained. </returns>
        public PrimitivePropertyConfiguration<TConfiguration> IsRequired()
        {
            Configuration.IsNullable = false;

            return this;
        }

        /// <summary>
        ///     Configures how values for the property are generated by the database.
        /// </summary>
        /// <param name="databaseGeneratedOption"> The pattern used to generate values for the property in the database. Setting 'null' will remove the database generated pattern facet from the property. Setting 'null' will cause the same runtime behavior as specifying 'None'. </param>
        /// <returns> The same PrimitivePropertyConfiguration instance so that multiple calls can be chained. </returns>
        public PrimitivePropertyConfiguration<TConfiguration> HasDatabaseGeneratedOption(
            DatabaseGeneratedOption? databaseGeneratedOption)
        {
            if (!((databaseGeneratedOption == null)
                  || Enum.IsDefined(typeof(DatabaseGeneratedOption), databaseGeneratedOption)))
            {
                throw new ArgumentOutOfRangeException("databaseGeneratedOption");
            }

            Configuration.DatabaseGeneratedOption = databaseGeneratedOption;

            return this;
        }

        /// <summary>
        ///     Configures the property to be used as an optimistic concurrency token.
        /// </summary>
        /// <returns> The same PrimitivePropertyConfiguration instance so that multiple calls can be chained. </returns>
        public PrimitivePropertyConfiguration<TConfiguration> IsConcurrencyToken()
        {
            IsConcurrencyToken(true);

            return this;
        }

        /// <summary>
        ///     Configures whether or not the property is to be used as an optimistic concurrency token.
        /// </summary>
        /// <param name="concurrencyToken"> Value indicating if the property is a concurrency token or not. Specifying 'null' will remove the concurrency token facet from the property. Specifying 'null' will cause the same runtime behavior as specifying 'false'. </param>
        /// <returns> The same PrimitivePropertyConfiguration instance so that multiple calls can be chained. </returns>
        public PrimitivePropertyConfiguration<TConfiguration> IsConcurrencyToken(bool? concurrencyToken)
        {
            Configuration.ConcurrencyMode
                = (concurrencyToken == null)
                      ? (ConcurrencyMode?)null
                      : (concurrencyToken.Value
                             ? ConcurrencyMode.Fixed
                             : ConcurrencyMode.None);

            return this;
        }

        /// <summary>
        ///     Configures the data type of the database column used to store the property.
        /// </summary>
        /// <param name="columnType"> Name of the database provider specific data type. </param>
        /// <returns> The same PrimitivePropertyConfiguration instance so that multiple calls can be chained. </returns>
        public PrimitivePropertyConfiguration<TConfiguration> HasColumnType(string columnType)
        {
            Configuration.ColumnType = columnType;

            return this;
        }

        /// <summary>
        ///     Configures the name of the database column used to store the property.
        /// </summary>
        /// <param name="columnName"> The name of the column. </param>
        /// <returns> The same PrimitivePropertyConfiguration instance so that multiple calls can be chained. </returns>
        public PrimitivePropertyConfiguration<TConfiguration> HasColumnName(string columnName)
        {
            Configuration.ColumnName = columnName;

            return this;
        }

        /// <summary>
        ///     Configures the order of the database column used to store the property.
        ///     This method is also used to specify key ordering when an entity type has a composite key.
        /// </summary>
        /// <param name="columnOrder"> The order that this column should appear in the database table. </param>
        /// <returns> The same PrimitivePropertyConfiguration instance so that multiple calls can be chained. </returns>
        public PrimitivePropertyConfiguration<TConfiguration> HasColumnOrder(int? columnOrder)
        {
            if (!(columnOrder == null || columnOrder.Value >= 0))
            {
                throw new ArgumentOutOfRangeException("columnOrder");
            }

            Configuration.ColumnOrder = columnOrder;

            return this;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public override string ToString()
        {
            return base.ToString();
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new Type GetType()
        {
            return base.GetType();
        }
    }
}
