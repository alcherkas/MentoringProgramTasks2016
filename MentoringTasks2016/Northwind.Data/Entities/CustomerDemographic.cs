using System.Collections.Generic;
using LinqToDB.Mapping;

namespace DataModels
{
    [Table(Schema = "dbo", Name = "CustomerDemographics")]
    public partial class CustomerDemographic
    {
        [PrimaryKey, NotNull]
        public string CustomerTypeID { get; set; } // nchar(10)
        [Column, Nullable]
        public string CustomerDesc { get; set; } // ntext

        #region Associations

        /// <summary>
        /// FK_CustomerCustomerDemo_BackReference
        /// </summary>
        [Association(ThisKey = "CustomerTypeID", OtherKey = "CustomerTypeID", CanBeNull = true, IsBackReference = true)]
        public IEnumerable<CustomerCustomerDemo> CustomerCustomerDemoes { get; set; }

        #endregion
    }
}