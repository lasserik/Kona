


using System;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using SubSonic;
using SubSonic.DataProviders;
using SubSonic.Extensions;
using SubSonic.Linq.Structure;
using SubSonic.Query;
using SubSonic.Schema;
using System.Data.Common;
using System.Collections.Generic;

namespace Kona.Data
{
    public partial class KonaDB : IQuerySurface
    {

        public IDataProvider DataProvider;
        public DbQueryProvider provider;
        
        public bool TestMode{
            get{
                return DataProvider.ConnectionString.Equals("test",StringComparison.InvariantCultureIgnoreCase);
            }
        }

        public KonaDB() 
        { 
            DataProvider = ProviderFactory.GetProvider("Kona");
            Init();
        
        }

        public KonaDB(string connectStr)
        {

            DataProvider = ProviderFactory.GetProvider(connectStr);

            Init();

        }

        public ITable FindByPrimaryKey(string pkName)
        {
            return DataProvider.Schema.Tables.SingleOrDefault(x => x.PrimaryKey.Name.Equals(pkName, StringComparison.InvariantCultureIgnoreCase));
        }

        public Query<T> GetQuery<T>()
        {
            return new Query<T>(provider);
        }
        
        public ITable FindTable(string tableName)
        {
            return DataProvider.FindTable(tableName);
        }
               
        public IDataProvider Provider
        {
            get { return DataProvider; }
        }
        
        public DbQueryProvider QueryProvider
        {
            get { return provider; }
        }
        
        
        
        
        BatchQuery _batch = null;
        public void Queue<T>(IQueryable<T> qry)
        {
            if (_batch == null)
                _batch = new BatchQuery(Provider, QueryProvider);
            _batch.Queue(qry);
        }

        public void Queue(ISqlQuery qry)
        {
            if (_batch == null)
                _batch = new BatchQuery(Provider, QueryProvider);
            _batch.Queue(qry);
        }

        public void ExecuteTransaction(IList<DbCommand> commands) {
            
            if(!TestMode){
                using(var connection = commands[0].Connection){
                   
                   if (connection.State == ConnectionState.Closed)
                        connection.Open();
                   
                   using (var trans = connection.BeginTransaction()) {
                        foreach (var cmd in commands) {
                            cmd.Transaction = trans;
                            cmd.Connection = connection;
                            cmd.ExecuteNonQuery();
                        }
                        trans.Commit();
                    }
                    connection.Close();
                }
            }

        }

        public IDataReader ExecuteBatch()
        {
            if (_batch == null)
                throw new InvalidOperationException("There's nothing in the queue");
            else if(!TestMode)
                return _batch.ExecuteReader();
            else
                return null;
        }
			
        public Query<Customer> Customers{ get; set; }
        public Query<CustomerBehavior> CustomerBehaviors{ get; set; }
        public Query<Address> Addresses{ get; set; }
        public Query<Categories_Product> Categories_Products{ get; set; }
        public Query<Products_CrossSell> Products_CrossSells{ get; set; }
        public Query<Products_Related> Products_Relateds{ get; set; }
        public Query<ProductImage> ProductImages{ get; set; }
        public Query<ProductDescriptor> ProductDescriptors{ get; set; }
        public Query<InventoryRecord> InventoryRecords{ get; set; }
        public Query<Transaction> Transactions{ get; set; }
        public Query<CustomerEvent> CustomerEvents{ get; set; }
        public Query<Categories_Widget> Categories_Widgets{ get; set; }
        public Query<OrderItem> OrderItems{ get; set; }
        public Query<CategoryLocalized> CategoryLocalizeds{ get; set; }
        public Query<Widget> Widgets{ get; set; }
        public Query<Product> Products{ get; set; }
        public Query<Order> Orders{ get; set; }
        public Query<Category> Categories{ get; set; }
        public Query<InventoryStatus> InventoryStatuses{ get; set; }

			

        #region ' Aggregates and SubSonic Queries '
        public Select SelectColumns(params string[] columns)
        {
            return new Select(DataProvider, columns);
        }

        public Select Select
        {
            get { return new Select(DataProvider); }
        }

        public Insert Insert {
            get { return new Insert(DataProvider); }
        }

        public Update<T> Update<T>() where T:new(){
            return new Update<T>(DataProvider);
        }

        public SqlQuery Delete<T>(Expression<Func<T,bool>> column) where T:new()
        {
            System.Linq.Expressions.LambdaExpression lamda = column as System.Linq.Expressions.LambdaExpression;
            SqlQuery result = new Delete<T>(DataProvider);
            result = result.From<T>();
            SubSonic.Query.Constraint c = lamda.ParseConstraint();
            result.Constraints.Add(c);
            return result;
        }

        public SqlQuery Max<T>(Expression<Func<T,object>> column)
        {
            System.Linq.Expressions.LambdaExpression lamda = column as System.Linq.Expressions.LambdaExpression;
            string colName = lamda.ParseObjectValue();
            string objectName=typeof(T).Name;
            string tableName = this.DataProvider.FindTable(objectName).Name;
            return new Select(DataProvider, new Aggregate(colName, AggregateFunction.Max)).From(tableName);
        }

        public SqlQuery Min<T>(Expression<Func<T,object>> column)
        {
            System.Linq.Expressions.LambdaExpression lamda = column as System.Linq.Expressions.LambdaExpression;
            string colName = lamda.ParseObjectValue();
            string objectName=typeof(T).Name;
            string tableName = this.DataProvider.FindTable(objectName).Name;
            return new Select(DataProvider, new Aggregate(colName, AggregateFunction.Min)).From(tableName);
        }

        public SqlQuery Sum<T>(Expression<Func<T,object>> column)
        {
            System.Linq.Expressions.LambdaExpression lamda = column as System.Linq.Expressions.LambdaExpression;
            string colName = lamda.ParseObjectValue();
            string objectName =typeof(T).Name;
            string tableName = this.DataProvider.FindTable(objectName).Name;
            return new Select(DataProvider, new Aggregate(colName, AggregateFunction.Sum)).From(tableName);
        }

        public SqlQuery Avg<T>(Expression<Func<T,object>> column)
        {
            System.Linq.Expressions.LambdaExpression lamda = column as System.Linq.Expressions.LambdaExpression;
            string colName = lamda.ParseObjectValue();
            string objectName=typeof(T).Name;
            string tableName = this.DataProvider.FindTable(objectName).Name;
            return new Select(DataProvider, new Aggregate(colName, AggregateFunction.Avg)).From(tableName);
        }

        public SqlQuery Count<T>(Expression<Func<T,object>> column)
        {
            System.Linq.Expressions.LambdaExpression lamda = column as System.Linq.Expressions.LambdaExpression;
            string colName = lamda.ParseObjectValue();
            string objectName=typeof(T).Name;
            string tableName = this.DataProvider.FindTable(objectName).Name;
            return new Select(DataProvider, new Aggregate(colName, AggregateFunction.Count)).From(tableName);
        }

        public SqlQuery Variance<T>(Expression<Func<T,object>> column)
        {
            System.Linq.Expressions.LambdaExpression lamda = column as System.Linq.Expressions.LambdaExpression;
            string colName = lamda.ParseObjectValue();
            string objectName=typeof(T).Name;
            string tableName = this.DataProvider.FindTable(objectName).Name;
            return new Select(DataProvider, new Aggregate(colName, AggregateFunction.Var)).From(tableName);
        }

        public SqlQuery StandardDeviation<T>(Expression<Func<T,object>> column)
        {
            System.Linq.Expressions.LambdaExpression lamda = column as System.Linq.Expressions.LambdaExpression;
            string colName = lamda.ParseObjectValue();
            string objectName = typeof(T).Name;
            string tableName = this.DataProvider.FindTable(objectName).Name;
            return new Select(DataProvider, new Aggregate(colName, AggregateFunction.StDev)).From(tableName);
        }

        #endregion

        void Init()
        {
            this.provider = new DbQueryProvider(DataProvider);

            #region ' Query Defs '
            this.Customers = new Query<Customer>(this.provider);
            this.CustomerBehaviors = new Query<CustomerBehavior>(this.provider);
            this.Addresses = new Query<Address>(this.provider);
            this.Categories_Products = new Query<Categories_Product>(this.provider);
            this.Products_CrossSells = new Query<Products_CrossSell>(this.provider);
            this.Products_Relateds = new Query<Products_Related>(this.provider);
            this.ProductImages = new Query<ProductImage>(this.provider);
            this.ProductDescriptors = new Query<ProductDescriptor>(this.provider);
            this.InventoryRecords = new Query<InventoryRecord>(this.provider);
            this.Transactions = new Query<Transaction>(this.provider);
            this.CustomerEvents = new Query<CustomerEvent>(this.provider);
            this.Categories_Widgets = new Query<Categories_Widget>(this.provider);
            this.OrderItems = new Query<OrderItem>(this.provider);
            this.CategoryLocalizeds = new Query<CategoryLocalized>(this.provider);
            this.Widgets = new Query<Widget>(this.provider);
            this.Products = new Query<Product>(this.provider);
            this.Orders = new Query<Order>(this.provider);
            this.Categories = new Query<Category>(this.provider);
            this.InventoryStatuses = new Query<InventoryStatus>(this.provider);
            #endregion


            #region ' Schemas '
        if(DataProvider.Schema.Tables.Count==0){

            // Table: Customers
            // Primary Key: UserName
            ITable CustomersSchema = new DatabaseTable("Customers", DataProvider);
            CustomersSchema.ClassName = "Customer";
            IColumn CustomersUserName = new DatabaseColumn("UserName",CustomersSchema);
            CustomersUserName.IsPrimaryKey = true;
            CustomersUserName.DataType=DbType.String;
            CustomersUserName.IsNullable = false;
            CustomersUserName.AutoIncrement = false;
            CustomersUserName.IsForeignKey = true;
            CustomersSchema.Columns.Add(CustomersUserName);

            IColumn CustomersEmail = new DatabaseColumn("Email",CustomersSchema);
            CustomersEmail.DataType=DbType.String;
            CustomersEmail.IsNullable = true;
            CustomersEmail.AutoIncrement = false;
            CustomersEmail.IsForeignKey = false;
            CustomersSchema.Columns.Add(CustomersEmail);

            IColumn CustomersFirst = new DatabaseColumn("First",CustomersSchema);
            CustomersFirst.DataType=DbType.String;
            CustomersFirst.IsNullable = true;
            CustomersFirst.AutoIncrement = false;
            CustomersFirst.IsForeignKey = false;
            CustomersSchema.Columns.Add(CustomersFirst);

            IColumn CustomersLast = new DatabaseColumn("Last",CustomersSchema);
            CustomersLast.DataType=DbType.String;
            CustomersLast.IsNullable = true;
            CustomersLast.AutoIncrement = false;
            CustomersLast.IsForeignKey = false;
            CustomersSchema.Columns.Add(CustomersLast);

            IColumn CustomersLanguageCode = new DatabaseColumn("LanguageCode",CustomersSchema);
            CustomersLanguageCode.DataType=DbType.AnsiStringFixedLength;
            CustomersLanguageCode.IsNullable = true;
            CustomersLanguageCode.AutoIncrement = false;
            CustomersLanguageCode.IsForeignKey = false;
            CustomersSchema.Columns.Add(CustomersLanguageCode);

            DataProvider.Schema.Tables.Add(CustomersSchema);

            // Table: CustomerBehaviors
            // Primary Key: UserBehaviorID
            ITable CustomerBehaviorsSchema = new DatabaseTable("CustomerBehaviors", DataProvider);
            CustomerBehaviorsSchema.ClassName = "CustomerBehavior";
            IColumn CustomerBehaviorsUserBehaviorID = new DatabaseColumn("UserBehaviorID",CustomerBehaviorsSchema);
            CustomerBehaviorsUserBehaviorID.IsPrimaryKey = true;
            CustomerBehaviorsUserBehaviorID.DataType=DbType.Int32;
            CustomerBehaviorsUserBehaviorID.IsNullable = false;
            CustomerBehaviorsUserBehaviorID.AutoIncrement = false;
            CustomerBehaviorsUserBehaviorID.IsForeignKey = true;
            CustomerBehaviorsSchema.Columns.Add(CustomerBehaviorsUserBehaviorID);

            IColumn CustomerBehaviorsDescription = new DatabaseColumn("Description",CustomerBehaviorsSchema);
            CustomerBehaviorsDescription.DataType=DbType.String;
            CustomerBehaviorsDescription.IsNullable = false;
            CustomerBehaviorsDescription.AutoIncrement = false;
            CustomerBehaviorsDescription.IsForeignKey = false;
            CustomerBehaviorsSchema.Columns.Add(CustomerBehaviorsDescription);

            DataProvider.Schema.Tables.Add(CustomerBehaviorsSchema);

            // Table: Addresses
            // Primary Key: AddressID
            ITable AddressesSchema = new DatabaseTable("Addresses", DataProvider);
            AddressesSchema.ClassName = "Address";
            IColumn AddressesAddressID = new DatabaseColumn("AddressID",AddressesSchema);
            AddressesAddressID.IsPrimaryKey = true;
            AddressesAddressID.DataType=DbType.Int32;
            AddressesAddressID.IsNullable = false;
            AddressesAddressID.AutoIncrement = true;
            AddressesAddressID.IsForeignKey = true;
            AddressesSchema.Columns.Add(AddressesAddressID);

            IColumn AddressesUserName = new DatabaseColumn("UserName",AddressesSchema);
            AddressesUserName.DataType=DbType.String;
            AddressesUserName.IsNullable = false;
            AddressesUserName.AutoIncrement = false;
            AddressesUserName.IsForeignKey = true;
            AddressesSchema.Columns.Add(AddressesUserName);

            IColumn AddressesFirstName = new DatabaseColumn("FirstName",AddressesSchema);
            AddressesFirstName.DataType=DbType.String;
            AddressesFirstName.IsNullable = false;
            AddressesFirstName.AutoIncrement = false;
            AddressesFirstName.IsForeignKey = false;
            AddressesSchema.Columns.Add(AddressesFirstName);

            IColumn AddressesLastName = new DatabaseColumn("LastName",AddressesSchema);
            AddressesLastName.DataType=DbType.String;
            AddressesLastName.IsNullable = false;
            AddressesLastName.AutoIncrement = false;
            AddressesLastName.IsForeignKey = false;
            AddressesSchema.Columns.Add(AddressesLastName);

            IColumn AddressesEmail = new DatabaseColumn("Email",AddressesSchema);
            AddressesEmail.DataType=DbType.String;
            AddressesEmail.IsNullable = false;
            AddressesEmail.AutoIncrement = false;
            AddressesEmail.IsForeignKey = false;
            AddressesSchema.Columns.Add(AddressesEmail);

            IColumn AddressesStreet1 = new DatabaseColumn("Street1",AddressesSchema);
            AddressesStreet1.DataType=DbType.String;
            AddressesStreet1.IsNullable = false;
            AddressesStreet1.AutoIncrement = false;
            AddressesStreet1.IsForeignKey = false;
            AddressesSchema.Columns.Add(AddressesStreet1);

            IColumn AddressesStreet2 = new DatabaseColumn("Street2",AddressesSchema);
            AddressesStreet2.DataType=DbType.String;
            AddressesStreet2.IsNullable = true;
            AddressesStreet2.AutoIncrement = false;
            AddressesStreet2.IsForeignKey = false;
            AddressesSchema.Columns.Add(AddressesStreet2);

            IColumn AddressesCity = new DatabaseColumn("City",AddressesSchema);
            AddressesCity.DataType=DbType.String;
            AddressesCity.IsNullable = false;
            AddressesCity.AutoIncrement = false;
            AddressesCity.IsForeignKey = false;
            AddressesSchema.Columns.Add(AddressesCity);

            IColumn AddressesStateOrProvince = new DatabaseColumn("StateOrProvince",AddressesSchema);
            AddressesStateOrProvince.DataType=DbType.String;
            AddressesStateOrProvince.IsNullable = false;
            AddressesStateOrProvince.AutoIncrement = false;
            AddressesStateOrProvince.IsForeignKey = false;
            AddressesSchema.Columns.Add(AddressesStateOrProvince);

            IColumn AddressesZip = new DatabaseColumn("Zip",AddressesSchema);
            AddressesZip.DataType=DbType.String;
            AddressesZip.IsNullable = false;
            AddressesZip.AutoIncrement = false;
            AddressesZip.IsForeignKey = false;
            AddressesSchema.Columns.Add(AddressesZip);

            IColumn AddressesCountry = new DatabaseColumn("Country",AddressesSchema);
            AddressesCountry.DataType=DbType.String;
            AddressesCountry.IsNullable = false;
            AddressesCountry.AutoIncrement = false;
            AddressesCountry.IsForeignKey = false;
            AddressesSchema.Columns.Add(AddressesCountry);

            IColumn AddressesLatitude = new DatabaseColumn("Latitude",AddressesSchema);
            AddressesLatitude.DataType=DbType.String;
            AddressesLatitude.IsNullable = true;
            AddressesLatitude.AutoIncrement = false;
            AddressesLatitude.IsForeignKey = false;
            AddressesSchema.Columns.Add(AddressesLatitude);

            IColumn AddressesLongitude = new DatabaseColumn("Longitude",AddressesSchema);
            AddressesLongitude.DataType=DbType.String;
            AddressesLongitude.IsNullable = true;
            AddressesLongitude.AutoIncrement = false;
            AddressesLongitude.IsForeignKey = false;
            AddressesSchema.Columns.Add(AddressesLongitude);

            IColumn AddressesIsDefault = new DatabaseColumn("IsDefault",AddressesSchema);
            AddressesIsDefault.DataType=DbType.Boolean;
            AddressesIsDefault.IsNullable = false;
            AddressesIsDefault.AutoIncrement = false;
            AddressesIsDefault.IsForeignKey = false;
            AddressesSchema.Columns.Add(AddressesIsDefault);

            DataProvider.Schema.Tables.Add(AddressesSchema);

            // Table: Categories_Products
            // Primary Key: CategoryID
            ITable Categories_ProductsSchema = new DatabaseTable("Categories_Products", DataProvider);
            Categories_ProductsSchema.ClassName = "Categories_Product";
            IColumn Categories_ProductsCategoryID = new DatabaseColumn("CategoryID",Categories_ProductsSchema);
            Categories_ProductsCategoryID.IsPrimaryKey = true;
            Categories_ProductsCategoryID.DataType=DbType.Int32;
            Categories_ProductsCategoryID.IsNullable = false;
            Categories_ProductsCategoryID.AutoIncrement = false;
            Categories_ProductsCategoryID.IsForeignKey = true;
            Categories_ProductsSchema.Columns.Add(Categories_ProductsCategoryID);

            IColumn Categories_ProductsSKU = new DatabaseColumn("SKU",Categories_ProductsSchema);
            Categories_ProductsSKU.DataType=DbType.String;
            Categories_ProductsSKU.IsNullable = false;
            Categories_ProductsSKU.AutoIncrement = false;
            Categories_ProductsSKU.IsForeignKey = true;
            Categories_ProductsSchema.Columns.Add(Categories_ProductsSKU);

            DataProvider.Schema.Tables.Add(Categories_ProductsSchema);

            // Table: Products_CrossSell
            // Primary Key: CrossSKU
            ITable Products_CrossSellSchema = new DatabaseTable("Products_CrossSell", DataProvider);
            Products_CrossSellSchema.ClassName = "Products_CrossSell";
            IColumn Products_CrossSellSKU = new DatabaseColumn("SKU",Products_CrossSellSchema);
            Products_CrossSellSKU.DataType=DbType.String;
            Products_CrossSellSKU.IsNullable = false;
            Products_CrossSellSKU.AutoIncrement = false;
            Products_CrossSellSKU.IsForeignKey = true;
            Products_CrossSellSchema.Columns.Add(Products_CrossSellSKU);

            IColumn Products_CrossSellCrossSKU = new DatabaseColumn("CrossSKU",Products_CrossSellSchema);
            Products_CrossSellCrossSKU.IsPrimaryKey = true;
            Products_CrossSellCrossSKU.DataType=DbType.String;
            Products_CrossSellCrossSKU.IsNullable = false;
            Products_CrossSellCrossSKU.AutoIncrement = false;
            Products_CrossSellCrossSKU.IsForeignKey = true;
            Products_CrossSellSchema.Columns.Add(Products_CrossSellCrossSKU);

            DataProvider.Schema.Tables.Add(Products_CrossSellSchema);

            // Table: Products_Related
            // Primary Key: RelatedSKU
            ITable Products_RelatedSchema = new DatabaseTable("Products_Related", DataProvider);
            Products_RelatedSchema.ClassName = "Products_Related";
            IColumn Products_RelatedSKU = new DatabaseColumn("SKU",Products_RelatedSchema);
            Products_RelatedSKU.DataType=DbType.String;
            Products_RelatedSKU.IsNullable = false;
            Products_RelatedSKU.AutoIncrement = false;
            Products_RelatedSKU.IsForeignKey = true;
            Products_RelatedSchema.Columns.Add(Products_RelatedSKU);

            IColumn Products_RelatedRelatedSKU = new DatabaseColumn("RelatedSKU",Products_RelatedSchema);
            Products_RelatedRelatedSKU.IsPrimaryKey = true;
            Products_RelatedRelatedSKU.DataType=DbType.String;
            Products_RelatedRelatedSKU.IsNullable = false;
            Products_RelatedRelatedSKU.AutoIncrement = false;
            Products_RelatedRelatedSKU.IsForeignKey = true;
            Products_RelatedSchema.Columns.Add(Products_RelatedRelatedSKU);

            DataProvider.Schema.Tables.Add(Products_RelatedSchema);

            // Table: ProductImages
            // Primary Key: ProductImageID
            ITable ProductImagesSchema = new DatabaseTable("ProductImages", DataProvider);
            ProductImagesSchema.ClassName = "ProductImage";
            IColumn ProductImagesProductImageID = new DatabaseColumn("ProductImageID",ProductImagesSchema);
            ProductImagesProductImageID.IsPrimaryKey = true;
            ProductImagesProductImageID.DataType=DbType.Int32;
            ProductImagesProductImageID.IsNullable = false;
            ProductImagesProductImageID.AutoIncrement = true;
            ProductImagesProductImageID.IsForeignKey = false;
            ProductImagesSchema.Columns.Add(ProductImagesProductImageID);

            IColumn ProductImagesSKU = new DatabaseColumn("SKU",ProductImagesSchema);
            ProductImagesSKU.DataType=DbType.String;
            ProductImagesSKU.IsNullable = false;
            ProductImagesSKU.AutoIncrement = false;
            ProductImagesSKU.IsForeignKey = true;
            ProductImagesSchema.Columns.Add(ProductImagesSKU);

            IColumn ProductImagesThumbUrl = new DatabaseColumn("ThumbUrl",ProductImagesSchema);
            ProductImagesThumbUrl.DataType=DbType.String;
            ProductImagesThumbUrl.IsNullable = false;
            ProductImagesThumbUrl.AutoIncrement = false;
            ProductImagesThumbUrl.IsForeignKey = false;
            ProductImagesSchema.Columns.Add(ProductImagesThumbUrl);

            IColumn ProductImagesFullImageUrl = new DatabaseColumn("FullImageUrl",ProductImagesSchema);
            ProductImagesFullImageUrl.DataType=DbType.String;
            ProductImagesFullImageUrl.IsNullable = false;
            ProductImagesFullImageUrl.AutoIncrement = false;
            ProductImagesFullImageUrl.IsForeignKey = false;
            ProductImagesSchema.Columns.Add(ProductImagesFullImageUrl);

            DataProvider.Schema.Tables.Add(ProductImagesSchema);

            // Table: ProductDescriptors
            // Primary Key: DescriptorID
            ITable ProductDescriptorsSchema = new DatabaseTable("ProductDescriptors", DataProvider);
            ProductDescriptorsSchema.ClassName = "ProductDescriptor";
            IColumn ProductDescriptorsDescriptorID = new DatabaseColumn("DescriptorID",ProductDescriptorsSchema);
            ProductDescriptorsDescriptorID.IsPrimaryKey = true;
            ProductDescriptorsDescriptorID.DataType=DbType.Int32;
            ProductDescriptorsDescriptorID.IsNullable = false;
            ProductDescriptorsDescriptorID.AutoIncrement = true;
            ProductDescriptorsDescriptorID.IsForeignKey = false;
            ProductDescriptorsSchema.Columns.Add(ProductDescriptorsDescriptorID);

            IColumn ProductDescriptorsSKU = new DatabaseColumn("SKU",ProductDescriptorsSchema);
            ProductDescriptorsSKU.DataType=DbType.String;
            ProductDescriptorsSKU.IsNullable = false;
            ProductDescriptorsSKU.AutoIncrement = false;
            ProductDescriptorsSKU.IsForeignKey = true;
            ProductDescriptorsSchema.Columns.Add(ProductDescriptorsSKU);

            IColumn ProductDescriptorsLanguageCode = new DatabaseColumn("LanguageCode",ProductDescriptorsSchema);
            ProductDescriptorsLanguageCode.DataType=DbType.AnsiStringFixedLength;
            ProductDescriptorsLanguageCode.IsNullable = false;
            ProductDescriptorsLanguageCode.AutoIncrement = false;
            ProductDescriptorsLanguageCode.IsForeignKey = false;
            ProductDescriptorsSchema.Columns.Add(ProductDescriptorsLanguageCode);

            IColumn ProductDescriptorsTitle = new DatabaseColumn("Title",ProductDescriptorsSchema);
            ProductDescriptorsTitle.DataType=DbType.String;
            ProductDescriptorsTitle.IsNullable = false;
            ProductDescriptorsTitle.AutoIncrement = false;
            ProductDescriptorsTitle.IsForeignKey = false;
            ProductDescriptorsSchema.Columns.Add(ProductDescriptorsTitle);

            IColumn ProductDescriptorsIsDefault = new DatabaseColumn("IsDefault",ProductDescriptorsSchema);
            ProductDescriptorsIsDefault.DataType=DbType.Boolean;
            ProductDescriptorsIsDefault.IsNullable = false;
            ProductDescriptorsIsDefault.AutoIncrement = false;
            ProductDescriptorsIsDefault.IsForeignKey = false;
            ProductDescriptorsSchema.Columns.Add(ProductDescriptorsIsDefault);

            IColumn ProductDescriptorsBody = new DatabaseColumn("Body",ProductDescriptorsSchema);
            ProductDescriptorsBody.DataType=DbType.String;
            ProductDescriptorsBody.IsNullable = false;
            ProductDescriptorsBody.AutoIncrement = false;
            ProductDescriptorsBody.IsForeignKey = false;
            ProductDescriptorsSchema.Columns.Add(ProductDescriptorsBody);

            DataProvider.Schema.Tables.Add(ProductDescriptorsSchema);

            // Table: InventoryRecords
            // Primary Key: InventoryRecordID
            ITable InventoryRecordsSchema = new DatabaseTable("InventoryRecords", DataProvider);
            InventoryRecordsSchema.ClassName = "InventoryRecord";
            IColumn InventoryRecordsInventoryRecordID = new DatabaseColumn("InventoryRecordID",InventoryRecordsSchema);
            InventoryRecordsInventoryRecordID.IsPrimaryKey = true;
            InventoryRecordsInventoryRecordID.DataType=DbType.Int32;
            InventoryRecordsInventoryRecordID.IsNullable = false;
            InventoryRecordsInventoryRecordID.AutoIncrement = true;
            InventoryRecordsInventoryRecordID.IsForeignKey = false;
            InventoryRecordsSchema.Columns.Add(InventoryRecordsInventoryRecordID);

            IColumn InventoryRecordsSKU = new DatabaseColumn("SKU",InventoryRecordsSchema);
            InventoryRecordsSKU.DataType=DbType.String;
            InventoryRecordsSKU.IsNullable = false;
            InventoryRecordsSKU.AutoIncrement = false;
            InventoryRecordsSKU.IsForeignKey = true;
            InventoryRecordsSchema.Columns.Add(InventoryRecordsSKU);

            IColumn InventoryRecordsIncrement = new DatabaseColumn("Increment",InventoryRecordsSchema);
            InventoryRecordsIncrement.DataType=DbType.Int32;
            InventoryRecordsIncrement.IsNullable = false;
            InventoryRecordsIncrement.AutoIncrement = false;
            InventoryRecordsIncrement.IsForeignKey = false;
            InventoryRecordsSchema.Columns.Add(InventoryRecordsIncrement);

            IColumn InventoryRecordsDateEntered = new DatabaseColumn("DateEntered",InventoryRecordsSchema);
            InventoryRecordsDateEntered.DataType=DbType.DateTime;
            InventoryRecordsDateEntered.IsNullable = false;
            InventoryRecordsDateEntered.AutoIncrement = false;
            InventoryRecordsDateEntered.IsForeignKey = false;
            InventoryRecordsSchema.Columns.Add(InventoryRecordsDateEntered);

            IColumn InventoryRecordsNotes = new DatabaseColumn("Notes",InventoryRecordsSchema);
            InventoryRecordsNotes.DataType=DbType.String;
            InventoryRecordsNotes.IsNullable = true;
            InventoryRecordsNotes.AutoIncrement = false;
            InventoryRecordsNotes.IsForeignKey = false;
            InventoryRecordsSchema.Columns.Add(InventoryRecordsNotes);

            DataProvider.Schema.Tables.Add(InventoryRecordsSchema);

            // Table: Transactions
            // Primary Key: TransactionID
            ITable TransactionsSchema = new DatabaseTable("Transactions", DataProvider);
            TransactionsSchema.ClassName = "Transaction";
            IColumn TransactionsTransactionID = new DatabaseColumn("TransactionID",TransactionsSchema);
            TransactionsTransactionID.IsPrimaryKey = true;
            TransactionsTransactionID.DataType=DbType.Guid;
            TransactionsTransactionID.IsNullable = false;
            TransactionsTransactionID.AutoIncrement = false;
            TransactionsTransactionID.IsForeignKey = false;
            TransactionsSchema.Columns.Add(TransactionsTransactionID);

            IColumn TransactionsOrderID = new DatabaseColumn("OrderID",TransactionsSchema);
            TransactionsOrderID.DataType=DbType.Guid;
            TransactionsOrderID.IsNullable = false;
            TransactionsOrderID.AutoIncrement = false;
            TransactionsOrderID.IsForeignKey = true;
            TransactionsSchema.Columns.Add(TransactionsOrderID);

            IColumn TransactionsTransactionDate = new DatabaseColumn("TransactionDate",TransactionsSchema);
            TransactionsTransactionDate.DataType=DbType.DateTime;
            TransactionsTransactionDate.IsNullable = false;
            TransactionsTransactionDate.AutoIncrement = false;
            TransactionsTransactionDate.IsForeignKey = false;
            TransactionsSchema.Columns.Add(TransactionsTransactionDate);

            IColumn TransactionsAmount = new DatabaseColumn("Amount",TransactionsSchema);
            TransactionsAmount.DataType=DbType.Currency;
            TransactionsAmount.IsNullable = false;
            TransactionsAmount.AutoIncrement = false;
            TransactionsAmount.IsForeignKey = false;
            TransactionsSchema.Columns.Add(TransactionsAmount);

            IColumn TransactionsAuthorizationCode = new DatabaseColumn("AuthorizationCode",TransactionsSchema);
            TransactionsAuthorizationCode.DataType=DbType.String;
            TransactionsAuthorizationCode.IsNullable = true;
            TransactionsAuthorizationCode.AutoIncrement = false;
            TransactionsAuthorizationCode.IsForeignKey = false;
            TransactionsSchema.Columns.Add(TransactionsAuthorizationCode);

            IColumn TransactionsNotes = new DatabaseColumn("Notes",TransactionsSchema);
            TransactionsNotes.DataType=DbType.String;
            TransactionsNotes.IsNullable = true;
            TransactionsNotes.AutoIncrement = false;
            TransactionsNotes.IsForeignKey = false;
            TransactionsSchema.Columns.Add(TransactionsNotes);

            IColumn TransactionsProcessor = new DatabaseColumn("Processor",TransactionsSchema);
            TransactionsProcessor.DataType=DbType.String;
            TransactionsProcessor.IsNullable = true;
            TransactionsProcessor.AutoIncrement = false;
            TransactionsProcessor.IsForeignKey = false;
            TransactionsSchema.Columns.Add(TransactionsProcessor);

            DataProvider.Schema.Tables.Add(TransactionsSchema);

            // Table: CustomerEvents
            // Primary Key: EventID
            ITable CustomerEventsSchema = new DatabaseTable("CustomerEvents", DataProvider);
            CustomerEventsSchema.ClassName = "CustomerEvent";
            IColumn CustomerEventsEventID = new DatabaseColumn("EventID",CustomerEventsSchema);
            CustomerEventsEventID.IsPrimaryKey = true;
            CustomerEventsEventID.DataType=DbType.Int32;
            CustomerEventsEventID.IsNullable = false;
            CustomerEventsEventID.AutoIncrement = true;
            CustomerEventsEventID.IsForeignKey = false;
            CustomerEventsSchema.Columns.Add(CustomerEventsEventID);

            IColumn CustomerEventsUserBehaviorID = new DatabaseColumn("UserBehaviorID",CustomerEventsSchema);
            CustomerEventsUserBehaviorID.DataType=DbType.Int32;
            CustomerEventsUserBehaviorID.IsNullable = false;
            CustomerEventsUserBehaviorID.AutoIncrement = false;
            CustomerEventsUserBehaviorID.IsForeignKey = true;
            CustomerEventsSchema.Columns.Add(CustomerEventsUserBehaviorID);

            IColumn CustomerEventsUserName = new DatabaseColumn("UserName",CustomerEventsSchema);
            CustomerEventsUserName.DataType=DbType.String;
            CustomerEventsUserName.IsNullable = false;
            CustomerEventsUserName.AutoIncrement = false;
            CustomerEventsUserName.IsForeignKey = true;
            CustomerEventsSchema.Columns.Add(CustomerEventsUserName);

            IColumn CustomerEventsEventDate = new DatabaseColumn("EventDate",CustomerEventsSchema);
            CustomerEventsEventDate.DataType=DbType.DateTime;
            CustomerEventsEventDate.IsNullable = false;
            CustomerEventsEventDate.AutoIncrement = false;
            CustomerEventsEventDate.IsForeignKey = false;
            CustomerEventsSchema.Columns.Add(CustomerEventsEventDate);

            IColumn CustomerEventsIP = new DatabaseColumn("IP",CustomerEventsSchema);
            CustomerEventsIP.DataType=DbType.String;
            CustomerEventsIP.IsNullable = false;
            CustomerEventsIP.AutoIncrement = false;
            CustomerEventsIP.IsForeignKey = false;
            CustomerEventsSchema.Columns.Add(CustomerEventsIP);

            IColumn CustomerEventsSKU = new DatabaseColumn("SKU",CustomerEventsSchema);
            CustomerEventsSKU.DataType=DbType.String;
            CustomerEventsSKU.IsNullable = true;
            CustomerEventsSKU.AutoIncrement = false;
            CustomerEventsSKU.IsForeignKey = false;
            CustomerEventsSchema.Columns.Add(CustomerEventsSKU);

            IColumn CustomerEventsCategoryID = new DatabaseColumn("CategoryID",CustomerEventsSchema);
            CustomerEventsCategoryID.DataType=DbType.Int32;
            CustomerEventsCategoryID.IsNullable = true;
            CustomerEventsCategoryID.AutoIncrement = false;
            CustomerEventsCategoryID.IsForeignKey = false;
            CustomerEventsSchema.Columns.Add(CustomerEventsCategoryID);

            IColumn CustomerEventsOrderID = new DatabaseColumn("OrderID",CustomerEventsSchema);
            CustomerEventsOrderID.DataType=DbType.Guid;
            CustomerEventsOrderID.IsNullable = true;
            CustomerEventsOrderID.AutoIncrement = false;
            CustomerEventsOrderID.IsForeignKey = false;
            CustomerEventsSchema.Columns.Add(CustomerEventsOrderID);

            DataProvider.Schema.Tables.Add(CustomerEventsSchema);

            // Table: Categories_Widgets
            // Primary Key: CategoryID
            ITable Categories_WidgetsSchema = new DatabaseTable("Categories_Widgets", DataProvider);
            Categories_WidgetsSchema.ClassName = "Categories_Widget";
            IColumn Categories_WidgetsCategoryID = new DatabaseColumn("CategoryID",Categories_WidgetsSchema);
            Categories_WidgetsCategoryID.IsPrimaryKey = true;
            Categories_WidgetsCategoryID.DataType=DbType.Int32;
            Categories_WidgetsCategoryID.IsNullable = false;
            Categories_WidgetsCategoryID.AutoIncrement = false;
            Categories_WidgetsCategoryID.IsForeignKey = true;
            Categories_WidgetsSchema.Columns.Add(Categories_WidgetsCategoryID);

            IColumn Categories_WidgetsWidgetID = new DatabaseColumn("WidgetID",Categories_WidgetsSchema);
            Categories_WidgetsWidgetID.DataType=DbType.Guid;
            Categories_WidgetsWidgetID.IsNullable = false;
            Categories_WidgetsWidgetID.AutoIncrement = false;
            Categories_WidgetsWidgetID.IsForeignKey = true;
            Categories_WidgetsSchema.Columns.Add(Categories_WidgetsWidgetID);

            DataProvider.Schema.Tables.Add(Categories_WidgetsSchema);

            // Table: OrderItems
            // Primary Key: OrderID
            ITable OrderItemsSchema = new DatabaseTable("OrderItems", DataProvider);
            OrderItemsSchema.ClassName = "OrderItem";
            IColumn OrderItemsOrderID = new DatabaseColumn("OrderID",OrderItemsSchema);
            OrderItemsOrderID.IsPrimaryKey = true;
            OrderItemsOrderID.DataType=DbType.Guid;
            OrderItemsOrderID.IsNullable = false;
            OrderItemsOrderID.AutoIncrement = false;
            OrderItemsOrderID.IsForeignKey = true;
            OrderItemsSchema.Columns.Add(OrderItemsOrderID);

            IColumn OrderItemsSKU = new DatabaseColumn("SKU",OrderItemsSchema);
            OrderItemsSKU.DataType=DbType.String;
            OrderItemsSKU.IsNullable = false;
            OrderItemsSKU.AutoIncrement = false;
            OrderItemsSKU.IsForeignKey = true;
            OrderItemsSchema.Columns.Add(OrderItemsSKU);

            IColumn OrderItemsQuantity = new DatabaseColumn("Quantity",OrderItemsSchema);
            OrderItemsQuantity.DataType=DbType.Int32;
            OrderItemsQuantity.IsNullable = false;
            OrderItemsQuantity.AutoIncrement = false;
            OrderItemsQuantity.IsForeignKey = false;
            OrderItemsSchema.Columns.Add(OrderItemsQuantity);

            IColumn OrderItemsDateAdded = new DatabaseColumn("DateAdded",OrderItemsSchema);
            OrderItemsDateAdded.DataType=DbType.DateTime;
            OrderItemsDateAdded.IsNullable = false;
            OrderItemsDateAdded.AutoIncrement = false;
            OrderItemsDateAdded.IsForeignKey = false;
            OrderItemsSchema.Columns.Add(OrderItemsDateAdded);

            IColumn OrderItemsLineItemPrice = new DatabaseColumn("LineItemPrice",OrderItemsSchema);
            OrderItemsLineItemPrice.DataType=DbType.Decimal;
            OrderItemsLineItemPrice.IsNullable = false;
            OrderItemsLineItemPrice.AutoIncrement = false;
            OrderItemsLineItemPrice.IsForeignKey = false;
            OrderItemsSchema.Columns.Add(OrderItemsLineItemPrice);

            IColumn OrderItemsDiscount = new DatabaseColumn("Discount",OrderItemsSchema);
            OrderItemsDiscount.DataType=DbType.Decimal;
            OrderItemsDiscount.IsNullable = false;
            OrderItemsDiscount.AutoIncrement = false;
            OrderItemsDiscount.IsForeignKey = false;
            OrderItemsSchema.Columns.Add(OrderItemsDiscount);

            IColumn OrderItemsDiscountReason = new DatabaseColumn("DiscountReason",OrderItemsSchema);
            OrderItemsDiscountReason.DataType=DbType.String;
            OrderItemsDiscountReason.IsNullable = true;
            OrderItemsDiscountReason.AutoIncrement = false;
            OrderItemsDiscountReason.IsForeignKey = false;
            OrderItemsSchema.Columns.Add(OrderItemsDiscountReason);

            IColumn OrderItemsLineItemWeightInPounds = new DatabaseColumn("LineItemWeightInPounds",OrderItemsSchema);
            OrderItemsLineItemWeightInPounds.DataType=DbType.Decimal;
            OrderItemsLineItemWeightInPounds.IsNullable = false;
            OrderItemsLineItemWeightInPounds.AutoIncrement = false;
            OrderItemsLineItemWeightInPounds.IsForeignKey = false;
            OrderItemsSchema.Columns.Add(OrderItemsLineItemWeightInPounds);

            DataProvider.Schema.Tables.Add(OrderItemsSchema);

            // Table: CategoryLocalized
            // Primary Key: CategoryNameID
            ITable CategoryLocalizedSchema = new DatabaseTable("CategoryLocalized", DataProvider);
            CategoryLocalizedSchema.ClassName = "CategoryLocalized";
            IColumn CategoryLocalizedCategoryNameID = new DatabaseColumn("CategoryNameID",CategoryLocalizedSchema);
            CategoryLocalizedCategoryNameID.IsPrimaryKey = true;
            CategoryLocalizedCategoryNameID.DataType=DbType.Int32;
            CategoryLocalizedCategoryNameID.IsNullable = false;
            CategoryLocalizedCategoryNameID.AutoIncrement = true;
            CategoryLocalizedCategoryNameID.IsForeignKey = false;
            CategoryLocalizedSchema.Columns.Add(CategoryLocalizedCategoryNameID);

            IColumn CategoryLocalizedCategoryID = new DatabaseColumn("CategoryID",CategoryLocalizedSchema);
            CategoryLocalizedCategoryID.DataType=DbType.Int32;
            CategoryLocalizedCategoryID.IsNullable = false;
            CategoryLocalizedCategoryID.AutoIncrement = false;
            CategoryLocalizedCategoryID.IsForeignKey = true;
            CategoryLocalizedSchema.Columns.Add(CategoryLocalizedCategoryID);

            IColumn CategoryLocalizedLanguageCode = new DatabaseColumn("LanguageCode",CategoryLocalizedSchema);
            CategoryLocalizedLanguageCode.DataType=DbType.AnsiStringFixedLength;
            CategoryLocalizedLanguageCode.IsNullable = false;
            CategoryLocalizedLanguageCode.AutoIncrement = false;
            CategoryLocalizedLanguageCode.IsForeignKey = false;
            CategoryLocalizedSchema.Columns.Add(CategoryLocalizedLanguageCode);

            IColumn CategoryLocalizedName = new DatabaseColumn("Name",CategoryLocalizedSchema);
            CategoryLocalizedName.DataType=DbType.String;
            CategoryLocalizedName.IsNullable = false;
            CategoryLocalizedName.AutoIncrement = false;
            CategoryLocalizedName.IsForeignKey = false;
            CategoryLocalizedSchema.Columns.Add(CategoryLocalizedName);

            IColumn CategoryLocalizedSlug = new DatabaseColumn("Slug",CategoryLocalizedSchema);
            CategoryLocalizedSlug.DataType=DbType.String;
            CategoryLocalizedSlug.IsNullable = false;
            CategoryLocalizedSlug.AutoIncrement = false;
            CategoryLocalizedSlug.IsForeignKey = false;
            CategoryLocalizedSchema.Columns.Add(CategoryLocalizedSlug);

            IColumn CategoryLocalizedDefaultImageFile = new DatabaseColumn("DefaultImageFile",CategoryLocalizedSchema);
            CategoryLocalizedDefaultImageFile.DataType=DbType.String;
            CategoryLocalizedDefaultImageFile.IsNullable = true;
            CategoryLocalizedDefaultImageFile.AutoIncrement = false;
            CategoryLocalizedDefaultImageFile.IsForeignKey = false;
            CategoryLocalizedSchema.Columns.Add(CategoryLocalizedDefaultImageFile);

            IColumn CategoryLocalizedDescription = new DatabaseColumn("Description",CategoryLocalizedSchema);
            CategoryLocalizedDescription.DataType=DbType.String;
            CategoryLocalizedDescription.IsNullable = true;
            CategoryLocalizedDescription.AutoIncrement = false;
            CategoryLocalizedDescription.IsForeignKey = false;
            CategoryLocalizedSchema.Columns.Add(CategoryLocalizedDescription);

            IColumn CategoryLocalizedIsHome = new DatabaseColumn("IsHome",CategoryLocalizedSchema);
            CategoryLocalizedIsHome.DataType=DbType.Boolean;
            CategoryLocalizedIsHome.IsNullable = false;
            CategoryLocalizedIsHome.AutoIncrement = false;
            CategoryLocalizedIsHome.IsForeignKey = false;
            CategoryLocalizedSchema.Columns.Add(CategoryLocalizedIsHome);

            DataProvider.Schema.Tables.Add(CategoryLocalizedSchema);

            // Table: Widgets
            // Primary Key: WidgetID
            ITable WidgetsSchema = new DatabaseTable("Widgets", DataProvider);
            WidgetsSchema.ClassName = "Widget";
            IColumn WidgetsWidgetID = new DatabaseColumn("WidgetID",WidgetsSchema);
            WidgetsWidgetID.IsPrimaryKey = true;
            WidgetsWidgetID.DataType=DbType.Guid;
            WidgetsWidgetID.IsNullable = false;
            WidgetsWidgetID.AutoIncrement = false;
            WidgetsWidgetID.IsForeignKey = true;
            WidgetsSchema.Columns.Add(WidgetsWidgetID);

            IColumn WidgetsListOrder = new DatabaseColumn("ListOrder",WidgetsSchema);
            WidgetsListOrder.DataType=DbType.Int32;
            WidgetsListOrder.IsNullable = true;
            WidgetsListOrder.AutoIncrement = false;
            WidgetsListOrder.IsForeignKey = false;
            WidgetsSchema.Columns.Add(WidgetsListOrder);

            IColumn WidgetsZone = new DatabaseColumn("Zone",WidgetsSchema);
            WidgetsZone.DataType=DbType.String;
            WidgetsZone.IsNullable = true;
            WidgetsZone.AutoIncrement = false;
            WidgetsZone.IsForeignKey = false;
            WidgetsSchema.Columns.Add(WidgetsZone);

            IColumn WidgetsTitle = new DatabaseColumn("Title",WidgetsSchema);
            WidgetsTitle.DataType=DbType.String;
            WidgetsTitle.IsNullable = true;
            WidgetsTitle.AutoIncrement = false;
            WidgetsTitle.IsForeignKey = false;
            WidgetsSchema.Columns.Add(WidgetsTitle);

            IColumn WidgetsBody = new DatabaseColumn("Body",WidgetsSchema);
            WidgetsBody.DataType=DbType.String;
            WidgetsBody.IsNullable = true;
            WidgetsBody.AutoIncrement = false;
            WidgetsBody.IsForeignKey = false;
            WidgetsSchema.Columns.Add(WidgetsBody);

            IColumn WidgetsSKUList = new DatabaseColumn("SKUList",WidgetsSchema);
            WidgetsSKUList.DataType=DbType.String;
            WidgetsSKUList.IsNullable = true;
            WidgetsSKUList.AutoIncrement = false;
            WidgetsSKUList.IsForeignKey = false;
            WidgetsSchema.Columns.Add(WidgetsSKUList);

            IColumn WidgetsViewName = new DatabaseColumn("ViewName",WidgetsSchema);
            WidgetsViewName.DataType=DbType.String;
            WidgetsViewName.IsNullable = true;
            WidgetsViewName.AutoIncrement = false;
            WidgetsViewName.IsForeignKey = false;
            WidgetsSchema.Columns.Add(WidgetsViewName);

            IColumn WidgetsLanguageCode = new DatabaseColumn("LanguageCode",WidgetsSchema);
            WidgetsLanguageCode.DataType=DbType.AnsiStringFixedLength;
            WidgetsLanguageCode.IsNullable = false;
            WidgetsLanguageCode.AutoIncrement = false;
            WidgetsLanguageCode.IsForeignKey = false;
            WidgetsSchema.Columns.Add(WidgetsLanguageCode);

            IColumn WidgetsCreatedOn = new DatabaseColumn("CreatedOn",WidgetsSchema);
            WidgetsCreatedOn.DataType=DbType.DateTime;
            WidgetsCreatedOn.IsNullable = false;
            WidgetsCreatedOn.AutoIncrement = false;
            WidgetsCreatedOn.IsForeignKey = false;
            WidgetsSchema.Columns.Add(WidgetsCreatedOn);

            IColumn WidgetsModifiedOn = new DatabaseColumn("ModifiedOn",WidgetsSchema);
            WidgetsModifiedOn.DataType=DbType.DateTime;
            WidgetsModifiedOn.IsNullable = false;
            WidgetsModifiedOn.AutoIncrement = false;
            WidgetsModifiedOn.IsForeignKey = false;
            WidgetsSchema.Columns.Add(WidgetsModifiedOn);

            IColumn WidgetsCreatedBy = new DatabaseColumn("CreatedBy",WidgetsSchema);
            WidgetsCreatedBy.DataType=DbType.String;
            WidgetsCreatedBy.IsNullable = true;
            WidgetsCreatedBy.AutoIncrement = false;
            WidgetsCreatedBy.IsForeignKey = false;
            WidgetsSchema.Columns.Add(WidgetsCreatedBy);

            IColumn WidgetsModifiedBy = new DatabaseColumn("ModifiedBy",WidgetsSchema);
            WidgetsModifiedBy.DataType=DbType.String;
            WidgetsModifiedBy.IsNullable = true;
            WidgetsModifiedBy.AutoIncrement = false;
            WidgetsModifiedBy.IsForeignKey = false;
            WidgetsSchema.Columns.Add(WidgetsModifiedBy);

            DataProvider.Schema.Tables.Add(WidgetsSchema);

            // Table: Products
            // Primary Key: SKU
            ITable ProductsSchema = new DatabaseTable("Products", DataProvider);
            ProductsSchema.ClassName = "Product";
            IColumn ProductsSKU = new DatabaseColumn("SKU",ProductsSchema);
            ProductsSKU.IsPrimaryKey = true;
            ProductsSKU.DataType=DbType.String;
            ProductsSKU.IsNullable = false;
            ProductsSKU.AutoIncrement = false;
            ProductsSKU.IsForeignKey = true;
            ProductsSchema.Columns.Add(ProductsSKU);

            IColumn ProductsSiteID = new DatabaseColumn("SiteID",ProductsSchema);
            ProductsSiteID.DataType=DbType.Guid;
            ProductsSiteID.IsNullable = false;
            ProductsSiteID.AutoIncrement = false;
            ProductsSiteID.IsForeignKey = false;
            ProductsSchema.Columns.Add(ProductsSiteID);

            IColumn ProductsDeliveryMethodID = new DatabaseColumn("DeliveryMethodID",ProductsSchema);
            ProductsDeliveryMethodID.DataType=DbType.Int32;
            ProductsDeliveryMethodID.IsNullable = false;
            ProductsDeliveryMethodID.AutoIncrement = false;
            ProductsDeliveryMethodID.IsForeignKey = false;
            ProductsSchema.Columns.Add(ProductsDeliveryMethodID);

            IColumn ProductsProductName = new DatabaseColumn("ProductName",ProductsSchema);
            ProductsProductName.DataType=DbType.String;
            ProductsProductName.IsNullable = false;
            ProductsProductName.AutoIncrement = false;
            ProductsProductName.IsForeignKey = false;
            ProductsSchema.Columns.Add(ProductsProductName);

            IColumn ProductsBasePrice = new DatabaseColumn("BasePrice",ProductsSchema);
            ProductsBasePrice.DataType=DbType.Decimal;
            ProductsBasePrice.IsNullable = false;
            ProductsBasePrice.AutoIncrement = false;
            ProductsBasePrice.IsForeignKey = false;
            ProductsSchema.Columns.Add(ProductsBasePrice);

            IColumn ProductsWeightInPounds = new DatabaseColumn("WeightInPounds",ProductsSchema);
            ProductsWeightInPounds.DataType=DbType.Currency;
            ProductsWeightInPounds.IsNullable = false;
            ProductsWeightInPounds.AutoIncrement = false;
            ProductsWeightInPounds.IsForeignKey = false;
            ProductsSchema.Columns.Add(ProductsWeightInPounds);

            IColumn ProductsDateAvailable = new DatabaseColumn("DateAvailable",ProductsSchema);
            ProductsDateAvailable.DataType=DbType.DateTime;
            ProductsDateAvailable.IsNullable = false;
            ProductsDateAvailable.AutoIncrement = false;
            ProductsDateAvailable.IsForeignKey = false;
            ProductsSchema.Columns.Add(ProductsDateAvailable);

            IColumn ProductsInventoryStatusID = new DatabaseColumn("InventoryStatusID",ProductsSchema);
            ProductsInventoryStatusID.DataType=DbType.Int32;
            ProductsInventoryStatusID.IsNullable = false;
            ProductsInventoryStatusID.AutoIncrement = false;
            ProductsInventoryStatusID.IsForeignKey = true;
            ProductsSchema.Columns.Add(ProductsInventoryStatusID);

            IColumn ProductsEstimatedDelivery = new DatabaseColumn("EstimatedDelivery",ProductsSchema);
            ProductsEstimatedDelivery.DataType=DbType.String;
            ProductsEstimatedDelivery.IsNullable = false;
            ProductsEstimatedDelivery.AutoIncrement = false;
            ProductsEstimatedDelivery.IsForeignKey = false;
            ProductsSchema.Columns.Add(ProductsEstimatedDelivery);

            IColumn ProductsAllowBackOrder = new DatabaseColumn("AllowBackOrder",ProductsSchema);
            ProductsAllowBackOrder.DataType=DbType.Boolean;
            ProductsAllowBackOrder.IsNullable = false;
            ProductsAllowBackOrder.AutoIncrement = false;
            ProductsAllowBackOrder.IsForeignKey = false;
            ProductsSchema.Columns.Add(ProductsAllowBackOrder);

            IColumn ProductsIsTaxable = new DatabaseColumn("IsTaxable",ProductsSchema);
            ProductsIsTaxable.DataType=DbType.Boolean;
            ProductsIsTaxable.IsNullable = false;
            ProductsIsTaxable.AutoIncrement = false;
            ProductsIsTaxable.IsForeignKey = false;
            ProductsSchema.Columns.Add(ProductsIsTaxable);

            IColumn ProductsDefaultImageFile = new DatabaseColumn("DefaultImageFile",ProductsSchema);
            ProductsDefaultImageFile.DataType=DbType.String;
            ProductsDefaultImageFile.IsNullable = true;
            ProductsDefaultImageFile.AutoIncrement = false;
            ProductsDefaultImageFile.IsForeignKey = false;
            ProductsSchema.Columns.Add(ProductsDefaultImageFile);

            IColumn ProductsAmountOnHand = new DatabaseColumn("AmountOnHand",ProductsSchema);
            ProductsAmountOnHand.DataType=DbType.Int32;
            ProductsAmountOnHand.IsNullable = false;
            ProductsAmountOnHand.AutoIncrement = false;
            ProductsAmountOnHand.IsForeignKey = false;
            ProductsSchema.Columns.Add(ProductsAmountOnHand);

            IColumn ProductsAllowPreOrder = new DatabaseColumn("AllowPreOrder",ProductsSchema);
            ProductsAllowPreOrder.DataType=DbType.Boolean;
            ProductsAllowPreOrder.IsNullable = false;
            ProductsAllowPreOrder.AutoIncrement = false;
            ProductsAllowPreOrder.IsForeignKey = false;
            ProductsSchema.Columns.Add(ProductsAllowPreOrder);

            DataProvider.Schema.Tables.Add(ProductsSchema);

            // Table: Orders
            // Primary Key: OrderID
            ITable OrdersSchema = new DatabaseTable("Orders", DataProvider);
            OrdersSchema.ClassName = "Order";
            IColumn OrdersOrderID = new DatabaseColumn("OrderID",OrdersSchema);
            OrdersOrderID.IsPrimaryKey = true;
            OrdersOrderID.DataType=DbType.Guid;
            OrdersOrderID.IsNullable = false;
            OrdersOrderID.AutoIncrement = false;
            OrdersOrderID.IsForeignKey = true;
            OrdersSchema.Columns.Add(OrdersOrderID);

            IColumn OrdersOrderNumber = new DatabaseColumn("OrderNumber",OrdersSchema);
            OrdersOrderNumber.DataType=DbType.String;
            OrdersOrderNumber.IsNullable = true;
            OrdersOrderNumber.AutoIncrement = false;
            OrdersOrderNumber.IsForeignKey = false;
            OrdersSchema.Columns.Add(OrdersOrderNumber);

            IColumn OrdersUserName = new DatabaseColumn("UserName",OrdersSchema);
            OrdersUserName.DataType=DbType.String;
            OrdersUserName.IsNullable = false;
            OrdersUserName.AutoIncrement = false;
            OrdersUserName.IsForeignKey = true;
            OrdersSchema.Columns.Add(OrdersUserName);

            IColumn OrdersUserLanguageCode = new DatabaseColumn("UserLanguageCode",OrdersSchema);
            OrdersUserLanguageCode.DataType=DbType.AnsiStringFixedLength;
            OrdersUserLanguageCode.IsNullable = false;
            OrdersUserLanguageCode.AutoIncrement = false;
            OrdersUserLanguageCode.IsForeignKey = false;
            OrdersSchema.Columns.Add(OrdersUserLanguageCode);

            IColumn OrdersTaxAmount = new DatabaseColumn("TaxAmount",OrdersSchema);
            OrdersTaxAmount.DataType=DbType.Currency;
            OrdersTaxAmount.IsNullable = false;
            OrdersTaxAmount.AutoIncrement = false;
            OrdersTaxAmount.IsForeignKey = false;
            OrdersSchema.Columns.Add(OrdersTaxAmount);

            IColumn OrdersShippingService = new DatabaseColumn("ShippingService",OrdersSchema);
            OrdersShippingService.DataType=DbType.String;
            OrdersShippingService.IsNullable = true;
            OrdersShippingService.AutoIncrement = false;
            OrdersShippingService.IsForeignKey = false;
            OrdersSchema.Columns.Add(OrdersShippingService);

            IColumn OrdersShippingAmount = new DatabaseColumn("ShippingAmount",OrdersSchema);
            OrdersShippingAmount.DataType=DbType.Currency;
            OrdersShippingAmount.IsNullable = false;
            OrdersShippingAmount.AutoIncrement = false;
            OrdersShippingAmount.IsForeignKey = false;
            OrdersSchema.Columns.Add(OrdersShippingAmount);

            IColumn OrdersDiscountAmount = new DatabaseColumn("DiscountAmount",OrdersSchema);
            OrdersDiscountAmount.DataType=DbType.Currency;
            OrdersDiscountAmount.IsNullable = false;
            OrdersDiscountAmount.AutoIncrement = false;
            OrdersDiscountAmount.IsForeignKey = false;
            OrdersSchema.Columns.Add(OrdersDiscountAmount);

            IColumn OrdersDiscountReason = new DatabaseColumn("DiscountReason",OrdersSchema);
            OrdersDiscountReason.DataType=DbType.String;
            OrdersDiscountReason.IsNullable = true;
            OrdersDiscountReason.AutoIncrement = false;
            OrdersDiscountReason.IsForeignKey = false;
            OrdersSchema.Columns.Add(OrdersDiscountReason);

            IColumn OrdersShippingAddressID = new DatabaseColumn("ShippingAddressID",OrdersSchema);
            OrdersShippingAddressID.DataType=DbType.Int32;
            OrdersShippingAddressID.IsNullable = true;
            OrdersShippingAddressID.AutoIncrement = false;
            OrdersShippingAddressID.IsForeignKey = true;
            OrdersSchema.Columns.Add(OrdersShippingAddressID);

            IColumn OrdersBillingAddressID = new DatabaseColumn("BillingAddressID",OrdersSchema);
            OrdersBillingAddressID.DataType=DbType.Int32;
            OrdersBillingAddressID.IsNullable = true;
            OrdersBillingAddressID.AutoIncrement = false;
            OrdersBillingAddressID.IsForeignKey = true;
            OrdersSchema.Columns.Add(OrdersBillingAddressID);

            IColumn OrdersDateShipped = new DatabaseColumn("DateShipped",OrdersSchema);
            OrdersDateShipped.DataType=DbType.DateTime;
            OrdersDateShipped.IsNullable = true;
            OrdersDateShipped.AutoIncrement = false;
            OrdersDateShipped.IsForeignKey = false;
            OrdersSchema.Columns.Add(OrdersDateShipped);

            IColumn OrdersTrackingNumber = new DatabaseColumn("TrackingNumber",OrdersSchema);
            OrdersTrackingNumber.DataType=DbType.String;
            OrdersTrackingNumber.IsNullable = true;
            OrdersTrackingNumber.AutoIncrement = false;
            OrdersTrackingNumber.IsForeignKey = false;
            OrdersSchema.Columns.Add(OrdersTrackingNumber);

            IColumn OrdersEstimatedDelivery = new DatabaseColumn("EstimatedDelivery",OrdersSchema);
            OrdersEstimatedDelivery.DataType=DbType.DateTime;
            OrdersEstimatedDelivery.IsNullable = true;
            OrdersEstimatedDelivery.AutoIncrement = false;
            OrdersEstimatedDelivery.IsForeignKey = false;
            OrdersSchema.Columns.Add(OrdersEstimatedDelivery);

            IColumn OrdersSubTotal = new DatabaseColumn("SubTotal",OrdersSchema);
            OrdersSubTotal.DataType=DbType.Currency;
            OrdersSubTotal.IsNullable = false;
            OrdersSubTotal.AutoIncrement = false;
            OrdersSubTotal.IsForeignKey = false;
            OrdersSchema.Columns.Add(OrdersSubTotal);

            IColumn OrdersOrderStatusID = new DatabaseColumn("OrderStatusID",OrdersSchema);
            OrdersOrderStatusID.DataType=DbType.Int32;
            OrdersOrderStatusID.IsNullable = false;
            OrdersOrderStatusID.AutoIncrement = false;
            OrdersOrderStatusID.IsForeignKey = true;
            OrdersSchema.Columns.Add(OrdersOrderStatusID);

            IColumn OrdersCreatedOn = new DatabaseColumn("CreatedOn",OrdersSchema);
            OrdersCreatedOn.DataType=DbType.DateTime;
            OrdersCreatedOn.IsNullable = false;
            OrdersCreatedOn.AutoIncrement = false;
            OrdersCreatedOn.IsForeignKey = false;
            OrdersSchema.Columns.Add(OrdersCreatedOn);

            IColumn OrdersExecutedOn = new DatabaseColumn("ExecutedOn",OrdersSchema);
            OrdersExecutedOn.DataType=DbType.DateTime;
            OrdersExecutedOn.IsNullable = true;
            OrdersExecutedOn.AutoIncrement = false;
            OrdersExecutedOn.IsForeignKey = false;
            OrdersSchema.Columns.Add(OrdersExecutedOn);

            IColumn OrdersModifiedOn = new DatabaseColumn("ModifiedOn",OrdersSchema);
            OrdersModifiedOn.DataType=DbType.DateTime;
            OrdersModifiedOn.IsNullable = false;
            OrdersModifiedOn.AutoIncrement = false;
            OrdersModifiedOn.IsForeignKey = false;
            OrdersSchema.Columns.Add(OrdersModifiedOn);

            DataProvider.Schema.Tables.Add(OrdersSchema);

            // Table: Categories
            // Primary Key: CategoryID
            ITable CategoriesSchema = new DatabaseTable("Categories", DataProvider);
            CategoriesSchema.ClassName = "Category";
            IColumn CategoriesCategoryID = new DatabaseColumn("CategoryID",CategoriesSchema);
            CategoriesCategoryID.IsPrimaryKey = true;
            CategoriesCategoryID.DataType=DbType.Int32;
            CategoriesCategoryID.IsNullable = false;
            CategoriesCategoryID.AutoIncrement = true;
            CategoriesCategoryID.IsForeignKey = true;
            CategoriesSchema.Columns.Add(CategoriesCategoryID);

            IColumn CategoriesParentID = new DatabaseColumn("ParentID",CategoriesSchema);
            CategoriesParentID.DataType=DbType.Int32;
            CategoriesParentID.IsNullable = true;
            CategoriesParentID.AutoIncrement = false;
            CategoriesParentID.IsForeignKey = false;
            CategoriesSchema.Columns.Add(CategoriesParentID);

            IColumn CategoriesIsDefault = new DatabaseColumn("IsDefault",CategoriesSchema);
            CategoriesIsDefault.DataType=DbType.Boolean;
            CategoriesIsDefault.IsNullable = false;
            CategoriesIsDefault.AutoIncrement = false;
            CategoriesIsDefault.IsForeignKey = false;
            CategoriesSchema.Columns.Add(CategoriesIsDefault);

            IColumn CategoriesDefaultImageFile = new DatabaseColumn("DefaultImageFile",CategoriesSchema);
            CategoriesDefaultImageFile.DataType=DbType.String;
            CategoriesDefaultImageFile.IsNullable = true;
            CategoriesDefaultImageFile.AutoIncrement = false;
            CategoriesDefaultImageFile.IsForeignKey = false;
            CategoriesSchema.Columns.Add(CategoriesDefaultImageFile);

            DataProvider.Schema.Tables.Add(CategoriesSchema);

            // Table: InventoryStatus
            // Primary Key: InventoryStatusID
            ITable InventoryStatusSchema = new DatabaseTable("InventoryStatus", DataProvider);
            InventoryStatusSchema.ClassName = "InventoryStatus";
            IColumn InventoryStatusInventoryStatusID = new DatabaseColumn("InventoryStatusID",InventoryStatusSchema);
            InventoryStatusInventoryStatusID.IsPrimaryKey = true;
            InventoryStatusInventoryStatusID.DataType=DbType.Int32;
            InventoryStatusInventoryStatusID.IsNullable = false;
            InventoryStatusInventoryStatusID.AutoIncrement = true;
            InventoryStatusInventoryStatusID.IsForeignKey = true;
            InventoryStatusSchema.Columns.Add(InventoryStatusInventoryStatusID);

            IColumn InventoryStatusDescription = new DatabaseColumn("Description",InventoryStatusSchema);
            InventoryStatusDescription.DataType=DbType.String;
            InventoryStatusDescription.IsNullable = true;
            InventoryStatusDescription.AutoIncrement = false;
            InventoryStatusDescription.IsForeignKey = false;
            InventoryStatusSchema.Columns.Add(InventoryStatusDescription);

            DataProvider.Schema.Tables.Add(InventoryStatusSchema);
            }
            #endregion
        }
    }
}