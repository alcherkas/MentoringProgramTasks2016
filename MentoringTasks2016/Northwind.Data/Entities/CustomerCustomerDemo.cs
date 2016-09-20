using LinqToDB.Mapping;

namespace DataModels
{
    [Table(Schema = "dbo", Name = "CustomerCustomerDemo")]
    public partial class CustomerCustomerDemo
    {
        [PrimaryKey(1), NotNull]
        public string CustomerID { get; set; } // nchar(5)
        [PrimaryKey(2), NotNull]
        public string CustomerTypeID { get; set; } // nchar(10)

        #region Associations

        /// <summary>
        /// FK_CustomerCustomerDemo_Customers
        /// </summary>
        [Association(ThisKey = "CustomerID", OtherKey = "CustomerID", CanBeNull = false, KeyName = "FK_CustomerCustomerDemo_Customers", BackReferenceName = "CustomerCustomerDemoes")]
        public Customer Customer { get; set; }

        /// <summary>
        /// FK_CustomerCustomerDemo
        /// </summary>
        [Association(ThisKey = "CustomerTypeID", OtherKey = "CustomerTypeID", CanBeNull = false, KeyName = "FK_CustomerCustomerDemo", BackReferenceName = "CustomerCustomerDemoes")]
        public CustomerDemographic FK_CustomerCustomerDemo { get; set; }

        #endregion
    }
}