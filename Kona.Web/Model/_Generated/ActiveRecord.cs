


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SubSonic.Extensions;
using System.Linq.Expressions;
using SubSonic.Schema;
using System.Collections;
using SubSonic;
using System.ComponentModel;
using System.Data.Common;

namespace Kona.Data
{
    
    
    /// <summary>
    /// A class which represents the Customers table in the Kona Database.
    /// </summary>
    public partial class Customer: IActiveRecord
    {
    
        #region Built-in testing
        static IList<Customer> TestItems;
        static TestRepository<Customer> _testRepo;
        
        static void SetTestRepo(){
            _testRepo = _testRepo ?? new TestRepository<Customer>(new Kona.Data.KonaDB());
        }
        public static void ResetTestRepo(){
            _testRepo = null;
            SetTestRepo();
        }
        public static void Setup(List<Customer> testlist){
            SetTestRepo();
            _testRepo._items = testlist;
        }
        public static void Setup(Customer item) {
            SetTestRepo();
            _testRepo._items.Add(item);
        }
        public static void Setup(int testItems) {
            SetTestRepo();
            for(int i=0;i<testItems;i++){
                Customer item=new Customer();
                _testRepo._items.Add(item);
            }
        }
        
        public bool TestMode {
            get {
                return this._db.DataProvider.ConnectionString.Equals("test", StringComparison.InvariantCultureIgnoreCase);
            }
        }

        #endregion

        IRepository<Customer> _repo;
        ITable tbl;
        bool _isNew;
        public bool IsNew(){
            return _isNew;
        }
        public void SetIsNew(bool isNew){
            _isNew=isNew;
        }
        public void SetIsLoaded(bool isLoaded){
            _isLoaded=isLoaded;
        }
        bool _isLoaded;
        public bool IsLoaded(){
            return _isLoaded;
        }
                
        List<IColumn> _dirtyColumns;
        public bool IsDirty(){
            return _dirtyColumns.Count>0;
        }
        
        public List<IColumn> GetDirtyColumns (){
            return _dirtyColumns;
        }

        Kona.Data.KonaDB _db;
        public Customer() {
             _db=new Kona.Data.KonaDB();
            _dirtyColumns=new List<IColumn>();
            
            if(TestMode){
                Customer.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<Customer>(_db);
            }
            tbl=_repo.GetTable();
            _isNew = true;
            OnCreated();
        }
        
       
        partial void OnCreated();
            
        partial void OnLoaded();
        
        partial void OnSaved();
        
        partial void OnChanged();
        
        public IList<IColumn> Columns{
            get{
                return tbl.Columns;
            }
        }
        
        public Customer(object key):this() {
            _isLoaded=_repo.Load(this,this.KeyName(),key);
            if(_isLoaded)
                OnLoaded();

        }

        public Customer(Expression<Func<Customer, bool>> expression):this() {
            _isLoaded=_repo.Load(this,expression);
            if(_isLoaded)
                OnLoaded();
        }
        
        public static SubSonic.Query.SqlQuery Select{
            get{
                var db=new Kona.Data.KonaDB();
                return db.Select.From<Customer>();
            }
        }
        public static SubSonic.Query.SqlQuery SelectColumns(params string[] columns){
            var db=new Kona.Data.KonaDB();
            return new SubSonic.Query.Select(db.DataProvider, columns).From<Customer>();
        }
        internal static IRepository<Customer> GetRepo(){
            var db=new Kona.Data.KonaDB();
            IRepository<Customer> _repo;
            
            if(db.TestMode){
                Customer.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<Customer>(db);
            }
            return _repo;
        }
        
        public static Customer SingleOrDefault(Expression<Func<Customer, bool>> expression) {
           
            var single= All().SingleOrDefault(expression);
            if (single != null){
                single.OnLoaded();
                single.SetIsLoaded(true);
                single.SetIsNew(false);
            }
            return single;
        }      
        public static bool Exists(Expression<Func<Customer, bool>> expression) {
           
            return All().Any(expression);
        }        
        public static IList<Customer> Find(Expression<Func<Customer, bool>> expression) {
           
            return GetRepo().Find(expression).ToList();
        }
        public static IQueryable<Customer> All() {
            return GetRepo().GetAll();
        }
        public static PagedList<Customer> GetPaged<TKey>(Func<Customer,TKey> orderby, int pageIndex, int pageSize){
            return GetRepo().GetPaged(orderby, pageIndex, pageSize);
        }
        public static PagedList<Customer> GetPaged<TKey>(string sortBy, int pageIndex, int pageSize) {
            return GetRepo().GetPaged(sortBy, pageIndex, pageSize);
        }
        public static PagedList<Customer> GetPaged<TKey>(int pageIndex, int pageSize) {
            return GetRepo().GetPaged(pageIndex, pageSize);
            
        }

        public string KeyName()
        {
            return "UserName";
        }

        public object KeyValue()
        {
            return this.UserName;
        }
        
        public void SetKeyValue(object value) {
            if (value != DBNull.Value) {
                var settable = value.ChangeTypeTo<string>();
                this.GetType().GetProperty(this.KeyName()).SetValue(this, settable, null);
            }
        }
        
        public override string ToString(){
            return this.UserName.ToString();
        }

        public override bool Equals(object obj){
            if(obj.GetType()==typeof(Customer)){
                Customer compare=(Customer)obj;
                string thisPk=(string)this.KeyValue();
                string comparePk=(string)compare.KeyValue();
                return thisPk.Equals(comparePk);
            }else{
                return base.Equals(obj);
            }
        }

        public string DescriptorValue()
        {
            return this.UserName.ToString();
        }

        public string DescriptorColumn() {
            return "UserName";
        }
        
        public static string GetDescriptorColumn()
        {
            return "UserName";
        }
        
        #region ' Foreign Keys '
        IList<Address> _Addresses;
        public IList<Address> Addresses
        {
            get
            {
                
                  if(_Addresses==null){
                      var repo=Address.GetRepo();
                  
                       _Addresses = (from items in repo.GetAll()
                           where items.UserName == _UserName
                           select items).ToList();
                   }
                  return _Addresses;
            }
            set{
                _Addresses=value;
            }
        }

        IList<CustomerEvent> _CustomerEvents;
        public IList<CustomerEvent> CustomerEvents
        {
            get
            {
                
                  if(_CustomerEvents==null){
                      var repo=CustomerEvent.GetRepo();
                  
                       _CustomerEvents = (from items in repo.GetAll()
                           where items.UserName == _UserName
                           select items).ToList();
                   }
                  return _CustomerEvents;
            }
            set{
                _CustomerEvents=value;
            }
        }

        IList<Order> _Orders;
        public IList<Order> Orders
        {
            get
            {
                
                  if(_Orders==null){
                      var repo=Order.GetRepo();
                  
                       _Orders = (from items in repo.GetAll()
                           where items.UserName == _UserName
                           select items).ToList();
                   }
                  return _Orders;
            }
            set{
                _Orders=value;
            }
        }

        #endregion
        

        string _UserName;
        public string UserName
        {
            get { return _UserName; }
            set
            {
                
                _UserName=value;
                var col=tbl.Columns.SingleOrDefault(x=>x.Name=="UserName");
                if(col!=null){
                    if(!_dirtyColumns.Contains(col) && _isLoaded){
                        _dirtyColumns.Add(col);
                    }
                }
                OnChanged();
            }
        }

        string _Email;
        public string Email
        {
            get { return _Email; }
            set
            {
                
                _Email=value;
                var col=tbl.Columns.SingleOrDefault(x=>x.Name=="Email");
                if(col!=null){
                    if(!_dirtyColumns.Contains(col) && _isLoaded){
                        _dirtyColumns.Add(col);
                    }
                }
                OnChanged();
            }
        }

        string _First;
        public string First
        {
            get { return _First; }
            set
            {
                
                _First=value;
                var col=tbl.Columns.SingleOrDefault(x=>x.Name=="First");
                if(col!=null){
                    if(!_dirtyColumns.Contains(col) && _isLoaded){
                        _dirtyColumns.Add(col);
                    }
                }
                OnChanged();
            }
        }

        string _Last;
        public string Last
        {
            get { return _Last; }
            set
            {
                
                _Last=value;
                var col=tbl.Columns.SingleOrDefault(x=>x.Name=="Last");
                if(col!=null){
                    if(!_dirtyColumns.Contains(col) && _isLoaded){
                        _dirtyColumns.Add(col);
                    }
                }
                OnChanged();
            }
        }

        string _LanguageCode;
        public string LanguageCode
        {
            get { return _LanguageCode; }
            set
            {
                
                _LanguageCode=value;
                var col=tbl.Columns.SingleOrDefault(x=>x.Name=="LanguageCode");
                if(col!=null){
                    if(!_dirtyColumns.Contains(col) && _isLoaded){
                        _dirtyColumns.Add(col);
                    }
                }
                OnChanged();
            }
        }



        public DbCommand GetUpdateCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return _repo.BuildUpdateQuery(this).GetCommand().ToDbCommand();
            
        }
        public DbCommand GetInsertCommand() {
 
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else{
            
            
                return _repo.BuildInsertQuery(this).GetCommand().ToDbCommand();
                
            }
        }
        
        public DbCommand GetDeleteCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return _repo.BuildDeleteQuery(this).GetCommand().ToDbCommand();
        }
        //persistence
        public void Save(){
            Save("");
        }
        
        public void Update(string userName){
            _repo.Update(this);
            OnSaved();
       }
        
        public void Add(string userName){
            
            _repo.Add(this);
            OnSaved();
        }
        
        public void Save(string userName) {
            
           
            if (_isNew) {
                Add(userName);
                
            } else {
                Update(userName);
            }
            
        }

        public void Delete() {
                   
                 
            _repo.Delete(KeyValue());
            
                    }

        public static void Delete(object key) {
        
            var repo = new SubSonicRepository<Customer>(new Kona.Data.KonaDB());

                   
                 
            repo.Delete(key);
            
                        
        }

        public static void Delete(Func<Customer, bool> expression) {
            var repo = GetRepo();
            
                   
            
            repo.Delete(expression);
            
                    }

        

        public void Load(IDataReader rdr) {
            Load(rdr, true);
        }
        public void Load(IDataReader rdr, bool closeReader) {
            if (rdr.Read()) {

                try {
                    rdr.Load(this);
                    _isNew = false;
                    _isLoaded = true;
                } catch {
                    _isLoaded = false;
                    throw;
                }
            }else{
                _isLoaded = false;
            }

            if (closeReader)
                rdr.Dispose();
        }
        

    } 
    
    
    /// <summary>
    /// A class which represents the CustomerBehaviors table in the Kona Database.
    /// </summary>
    public partial class CustomerBehavior: IActiveRecord
    {
    
        #region Built-in testing
        static IList<CustomerBehavior> TestItems;
        static TestRepository<CustomerBehavior> _testRepo;
        
        static void SetTestRepo(){
            _testRepo = _testRepo ?? new TestRepository<CustomerBehavior>(new Kona.Data.KonaDB());
        }
        public static void ResetTestRepo(){
            _testRepo = null;
            SetTestRepo();
        }
        public static void Setup(List<CustomerBehavior> testlist){
            SetTestRepo();
            _testRepo._items = testlist;
        }
        public static void Setup(CustomerBehavior item) {
            SetTestRepo();
            _testRepo._items.Add(item);
        }
        public static void Setup(int testItems) {
            SetTestRepo();
            for(int i=0;i<testItems;i++){
                CustomerBehavior item=new CustomerBehavior();
                _testRepo._items.Add(item);
            }
        }
        
        public bool TestMode {
            get {
                return this._db.DataProvider.ConnectionString.Equals("test", StringComparison.InvariantCultureIgnoreCase);
            }
        }

        #endregion

        IRepository<CustomerBehavior> _repo;
        ITable tbl;
        bool _isNew;
        public bool IsNew(){
            return _isNew;
        }
        public void SetIsNew(bool isNew){
            _isNew=isNew;
        }
        public void SetIsLoaded(bool isLoaded){
            _isLoaded=isLoaded;
        }
        bool _isLoaded;
        public bool IsLoaded(){
            return _isLoaded;
        }
                
        List<IColumn> _dirtyColumns;
        public bool IsDirty(){
            return _dirtyColumns.Count>0;
        }
        
        public List<IColumn> GetDirtyColumns (){
            return _dirtyColumns;
        }

        Kona.Data.KonaDB _db;
        public CustomerBehavior() {
             _db=new Kona.Data.KonaDB();
            _dirtyColumns=new List<IColumn>();
            
            if(TestMode){
                CustomerBehavior.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<CustomerBehavior>(_db);
            }
            tbl=_repo.GetTable();
            _isNew = true;
            OnCreated();
        }
        
       
        partial void OnCreated();
            
        partial void OnLoaded();
        
        partial void OnSaved();
        
        partial void OnChanged();
        
        public IList<IColumn> Columns{
            get{
                return tbl.Columns;
            }
        }
        
        public CustomerBehavior(object key):this() {
            _isLoaded=_repo.Load(this,this.KeyName(),key);
            if(_isLoaded)
                OnLoaded();

        }

        public CustomerBehavior(Expression<Func<CustomerBehavior, bool>> expression):this() {
            _isLoaded=_repo.Load(this,expression);
            if(_isLoaded)
                OnLoaded();
        }
        
        public static SubSonic.Query.SqlQuery Select{
            get{
                var db=new Kona.Data.KonaDB();
                return db.Select.From<CustomerBehavior>();
            }
        }
        public static SubSonic.Query.SqlQuery SelectColumns(params string[] columns){
            var db=new Kona.Data.KonaDB();
            return new SubSonic.Query.Select(db.DataProvider, columns).From<CustomerBehavior>();
        }
        internal static IRepository<CustomerBehavior> GetRepo(){
            var db=new Kona.Data.KonaDB();
            IRepository<CustomerBehavior> _repo;
            
            if(db.TestMode){
                CustomerBehavior.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<CustomerBehavior>(db);
            }
            return _repo;
        }
        
        public static CustomerBehavior SingleOrDefault(Expression<Func<CustomerBehavior, bool>> expression) {
           
            var single= All().SingleOrDefault(expression);
            if (single != null){
                single.OnLoaded();
                single.SetIsLoaded(true);
                single.SetIsNew(false);
            }
            return single;
        }      
        public static bool Exists(Expression<Func<CustomerBehavior, bool>> expression) {
           
            return All().Any(expression);
        }        
        public static IList<CustomerBehavior> Find(Expression<Func<CustomerBehavior, bool>> expression) {
           
            return GetRepo().Find(expression).ToList();
        }
        public static IQueryable<CustomerBehavior> All() {
            return GetRepo().GetAll();
        }
        public static PagedList<CustomerBehavior> GetPaged<TKey>(Func<CustomerBehavior,TKey> orderby, int pageIndex, int pageSize){
            return GetRepo().GetPaged(orderby, pageIndex, pageSize);
        }
        public static PagedList<CustomerBehavior> GetPaged<TKey>(string sortBy, int pageIndex, int pageSize) {
            return GetRepo().GetPaged(sortBy, pageIndex, pageSize);
        }
        public static PagedList<CustomerBehavior> GetPaged<TKey>(int pageIndex, int pageSize) {
            return GetRepo().GetPaged(pageIndex, pageSize);
            
        }

        public string KeyName()
        {
            return "UserBehaviorID";
        }

        public object KeyValue()
        {
            return this.UserBehaviorID;
        }
        
        public void SetKeyValue(object value) {
            if (value != DBNull.Value) {
                var settable = value.ChangeTypeTo<int>();
                this.GetType().GetProperty(this.KeyName()).SetValue(this, settable, null);
            }
        }
        
        public override string ToString(){
            return this.Description.ToString();
        }

        public override bool Equals(object obj){
            if(obj.GetType()==typeof(CustomerBehavior)){
                CustomerBehavior compare=(CustomerBehavior)obj;
                int thisPk=(int)this.KeyValue();
                int comparePk=(int)compare.KeyValue();
                return thisPk.Equals(comparePk);
            }else{
                return base.Equals(obj);
            }
        }

        public string DescriptorValue()
        {
            return this.Description.ToString();
        }

        public string DescriptorColumn() {
            return "Description";
        }
        
        public static string GetDescriptorColumn()
        {
            return "Description";
        }
        
        #region ' Foreign Keys '
        IList<CustomerEvent> _CustomerEvents;
        public IList<CustomerEvent> CustomerEvents
        {
            get
            {
                
                  if(_CustomerEvents==null){
                      var repo=CustomerEvent.GetRepo();
                  
                       _CustomerEvents = (from items in repo.GetAll()
                           where items.UserBehaviorID == _UserBehaviorID
                           select items).ToList();
                   }
                  return _CustomerEvents;
            }
            set{
                _CustomerEvents=value;
            }
        }

        #endregion
        

        int _UserBehaviorID;
        public int UserBehaviorID
        {
            get { return _UserBehaviorID; }
            set
            {
                
                _UserBehaviorID=value;
                var col=tbl.Columns.SingleOrDefault(x=>x.Name=="UserBehaviorID");
                if(col!=null){
                    if(!_dirtyColumns.Contains(col) && _isLoaded){
                        _dirtyColumns.Add(col);
                    }
                }
                OnChanged();
            }
        }

        string _Description;
        public string Description
        {
            get { return _Description; }
            set
            {
                
                _Description=value;
                var col=tbl.Columns.SingleOrDefault(x=>x.Name=="Description");
                if(col!=null){
                    if(!_dirtyColumns.Contains(col) && _isLoaded){
                        _dirtyColumns.Add(col);
                    }
                }
                OnChanged();
            }
        }



        public DbCommand GetUpdateCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return _repo.BuildUpdateQuery(this).GetCommand().ToDbCommand();
            
        }
        public DbCommand GetInsertCommand() {
 
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else{
            
            
                return _repo.BuildInsertQuery(this).GetCommand().ToDbCommand();
                
            }
        }
        
        public DbCommand GetDeleteCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return _repo.BuildDeleteQuery(this).GetCommand().ToDbCommand();
        }
        //persistence
        public void Save(){
            Save("");
        }
        
        public void Update(string userName){
            _repo.Update(this);
            OnSaved();
       }
        
        public void Add(string userName){
            
            _repo.Add(this);
            OnSaved();
        }
        
        public void Save(string userName) {
            
           
            if (_isNew) {
                Add(userName);
                
            } else {
                Update(userName);
            }
            
        }

        public void Delete() {
                   
                 
            _repo.Delete(KeyValue());
            
                    }

        public static void Delete(object key) {
        
            var repo = new SubSonicRepository<CustomerBehavior>(new Kona.Data.KonaDB());

                   
                 
            repo.Delete(key);
            
                        
        }

        public static void Delete(Func<CustomerBehavior, bool> expression) {
            var repo = GetRepo();
            
                   
            
            repo.Delete(expression);
            
                    }

        

        public void Load(IDataReader rdr) {
            Load(rdr, true);
        }
        public void Load(IDataReader rdr, bool closeReader) {
            if (rdr.Read()) {

                try {
                    rdr.Load(this);
                    _isNew = false;
                    _isLoaded = true;
                } catch {
                    _isLoaded = false;
                    throw;
                }
            }else{
                _isLoaded = false;
            }

            if (closeReader)
                rdr.Dispose();
        }
        

    } 
    
    
    /// <summary>
    /// A class which represents the Addresses table in the Kona Database.
    /// </summary>
    public partial class Address: IActiveRecord
    {
    
        #region Built-in testing
        static IList<Address> TestItems;
        static TestRepository<Address> _testRepo;
        
        static void SetTestRepo(){
            _testRepo = _testRepo ?? new TestRepository<Address>(new Kona.Data.KonaDB());
        }
        public static void ResetTestRepo(){
            _testRepo = null;
            SetTestRepo();
        }
        public static void Setup(List<Address> testlist){
            SetTestRepo();
            _testRepo._items = testlist;
        }
        public static void Setup(Address item) {
            SetTestRepo();
            _testRepo._items.Add(item);
        }
        public static void Setup(int testItems) {
            SetTestRepo();
            for(int i=0;i<testItems;i++){
                Address item=new Address();
                _testRepo._items.Add(item);
            }
        }
        
        public bool TestMode {
            get {
                return this._db.DataProvider.ConnectionString.Equals("test", StringComparison.InvariantCultureIgnoreCase);
            }
        }

        #endregion

        IRepository<Address> _repo;
        ITable tbl;
        bool _isNew;
        public bool IsNew(){
            return _isNew;
        }
        public void SetIsNew(bool isNew){
            _isNew=isNew;
        }
        public void SetIsLoaded(bool isLoaded){
            _isLoaded=isLoaded;
        }
        bool _isLoaded;
        public bool IsLoaded(){
            return _isLoaded;
        }
                
        List<IColumn> _dirtyColumns;
        public bool IsDirty(){
            return _dirtyColumns.Count>0;
        }
        
        public List<IColumn> GetDirtyColumns (){
            return _dirtyColumns;
        }

        Kona.Data.KonaDB _db;
        public Address() {
             _db=new Kona.Data.KonaDB();
            _dirtyColumns=new List<IColumn>();
            
            if(TestMode){
                Address.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<Address>(_db);
            }
            tbl=_repo.GetTable();
            _isNew = true;
            OnCreated();
        }
        
       
        partial void OnCreated();
            
        partial void OnLoaded();
        
        partial void OnSaved();
        
        partial void OnChanged();
        
        public IList<IColumn> Columns{
            get{
                return tbl.Columns;
            }
        }
        
        public Address(object key):this() {
            _isLoaded=_repo.Load(this,this.KeyName(),key);
            if(_isLoaded)
                OnLoaded();

        }

        public Address(Expression<Func<Address, bool>> expression):this() {
            _isLoaded=_repo.Load(this,expression);
            if(_isLoaded)
                OnLoaded();
        }
        
        public static SubSonic.Query.SqlQuery Select{
            get{
                var db=new Kona.Data.KonaDB();
                return db.Select.From<Address>();
            }
        }
        public static SubSonic.Query.SqlQuery SelectColumns(params string[] columns){
            var db=new Kona.Data.KonaDB();
            return new SubSonic.Query.Select(db.DataProvider, columns).From<Address>();
        }
        internal static IRepository<Address> GetRepo(){
            var db=new Kona.Data.KonaDB();
            IRepository<Address> _repo;
            
            if(db.TestMode){
                Address.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<Address>(db);
            }
            return _repo;
        }
        
        public static Address SingleOrDefault(Expression<Func<Address, bool>> expression) {
           
            var single= All().SingleOrDefault(expression);
            if (single != null){
                single.OnLoaded();
                single.SetIsLoaded(true);
                single.SetIsNew(false);
            }
            return single;
        }      
        public static bool Exists(Expression<Func<Address, bool>> expression) {
           
            return All().Any(expression);
        }        
        public static IList<Address> Find(Expression<Func<Address, bool>> expression) {
           
            return GetRepo().Find(expression).ToList();
        }
        public static IQueryable<Address> All() {
            return GetRepo().GetAll();
        }
        public static PagedList<Address> GetPaged<TKey>(Func<Address,TKey> orderby, int pageIndex, int pageSize){
            return GetRepo().GetPaged(orderby, pageIndex, pageSize);
        }
        public static PagedList<Address> GetPaged<TKey>(string sortBy, int pageIndex, int pageSize) {
            return GetRepo().GetPaged(sortBy, pageIndex, pageSize);
        }
        public static PagedList<Address> GetPaged<TKey>(int pageIndex, int pageSize) {
            return GetRepo().GetPaged(pageIndex, pageSize);
            
        }

        public string KeyName()
        {
            return "AddressID";
        }

        public object KeyValue()
        {
            return this.AddressID;
        }
        
        public void SetKeyValue(object value) {
            if (value != DBNull.Value) {
                var settable = value.ChangeTypeTo<int>();
                this.GetType().GetProperty(this.KeyName()).SetValue(this, settable, null);
            }
        }
        
        public override string ToString(){
            return this.UserName.ToString();
        }

        public override bool Equals(object obj){
            if(obj.GetType()==typeof(Address)){
                Address compare=(Address)obj;
                int thisPk=(int)this.KeyValue();
                int comparePk=(int)compare.KeyValue();
                return thisPk.Equals(comparePk);
            }else{
                return base.Equals(obj);
            }
        }

        public string DescriptorValue()
        {
            return this.UserName.ToString();
        }

        public string DescriptorColumn() {
            return "UserName";
        }
        
        public static string GetDescriptorColumn()
        {
            return "UserName";
        }
        
        #region ' Foreign Keys '
        IList<Customer> _Customers;
        public IList<Customer> Customers
        {
            get
            {
                
                  if(_Customers==null){
                      var repo=Customer.GetRepo();
                  
                       _Customers = (from items in repo.GetAll()
                           where items.UserName == _UserName
                           select items).ToList();
                   }
                  return _Customers;
            }
            set{
                _Customers=value;
            }
        }

        IList<Order> _Orders;
        public IList<Order> Orders
        {
            get
            {
                
                  if(_Orders==null){
                      var repo=Order.GetRepo();
                  
                       _Orders = (from items in repo.GetAll()
                           where items.ShippingAddressID == _AddressID
                           select items).ToList();
                   }
                  return _Orders;
            }
            set{
                _Orders=value;
            }
        }

        IList<Order> _Orders2;
        public IList<Order> Orders2
        {
            get
            {
                
                  if(_Orders2==null){
                      var repo=Order.GetRepo();
                  
                       _Orders2 = (from items in repo.GetAll()
                           where items.BillingAddressID == _AddressID
                           select items).ToList();
                   }
                  return _Orders2;
            }
            set{
                _Orders2=value;
            }
        }

        #endregion
        

        int _AddressID;
        public int AddressID
        {
            get { return _AddressID; }
            set
            {
                
                _AddressID=value;
                var col=tbl.Columns.SingleOrDefault(x=>x.Name=="AddressID");
                if(col!=null){
                    if(!_dirtyColumns.Contains(col) && _isLoaded){
                        _dirtyColumns.Add(col);
                    }
                }
                OnChanged();
            }
        }

        string _UserName;
        public string UserName
        {
            get { return _UserName; }
            set
            {
                
                _UserName=value;
                var col=tbl.Columns.SingleOrDefault(x=>x.Name=="UserName");
                if(col!=null){
                    if(!_dirtyColumns.Contains(col) && _isLoaded){
                        _dirtyColumns.Add(col);
                    }
                }
                OnChanged();
            }
        }

        string _FirstName;
        public string FirstName
        {
            get { return _FirstName; }
            set
            {
                
                _FirstName=value;
                var col=tbl.Columns.SingleOrDefault(x=>x.Name=="FirstName");
                if(col!=null){
                    if(!_dirtyColumns.Contains(col) && _isLoaded){
                        _dirtyColumns.Add(col);
                    }
                }
                OnChanged();
            }
        }

        string _LastName;
        public string LastName
        {
            get { return _LastName; }
            set
            {
                
                _LastName=value;
                var col=tbl.Columns.SingleOrDefault(x=>x.Name=="LastName");
                if(col!=null){
                    if(!_dirtyColumns.Contains(col) && _isLoaded){
                        _dirtyColumns.Add(col);
                    }
                }
                OnChanged();
            }
        }

        string _Email;
        public string Email
        {
            get { return _Email; }
            set
            {
                
                _Email=value;
                var col=tbl.Columns.SingleOrDefault(x=>x.Name=="Email");
                if(col!=null){
                    if(!_dirtyColumns.Contains(col) && _isLoaded){
                        _dirtyColumns.Add(col);
                    }
                }
                OnChanged();
            }
        }

        string _Street1;
        public string Street1
        {
            get { return _Street1; }
            set
            {
                
                _Street1=value;
                var col=tbl.Columns.SingleOrDefault(x=>x.Name=="Street1");
                if(col!=null){
                    if(!_dirtyColumns.Contains(col) && _isLoaded){
                        _dirtyColumns.Add(col);
                    }
                }
                OnChanged();
            }
        }

        string _Street2;
        public string Street2
        {
            get { return _Street2; }
            set
            {
                
                _Street2=value;
                var col=tbl.Columns.SingleOrDefault(x=>x.Name=="Street2");
                if(col!=null){
                    if(!_dirtyColumns.Contains(col) && _isLoaded){
                        _dirtyColumns.Add(col);
                    }
                }
                OnChanged();
            }
        }

        string _City;
        public string City
        {
            get { return _City; }
            set
            {
                
                _City=value;
                var col=tbl.Columns.SingleOrDefault(x=>x.Name=="City");
                if(col!=null){
                    if(!_dirtyColumns.Contains(col) && _isLoaded){
                        _dirtyColumns.Add(col);
                    }
                }
                OnChanged();
            }
        }

        string _StateOrProvince;
        public string StateOrProvince
        {
            get { return _StateOrProvince; }
            set
            {
                
                _StateOrProvince=value;
                var col=tbl.Columns.SingleOrDefault(x=>x.Name=="StateOrProvince");
                if(col!=null){
                    if(!_dirtyColumns.Contains(col) && _isLoaded){
                        _dirtyColumns.Add(col);
                    }
                }
                OnChanged();
            }
        }

        string _Zip;
        public string Zip
        {
            get { return _Zip; }
            set
            {
                
                _Zip=value;
                var col=tbl.Columns.SingleOrDefault(x=>x.Name=="Zip");
                if(col!=null){
                    if(!_dirtyColumns.Contains(col) && _isLoaded){
                        _dirtyColumns.Add(col);
                    }
                }
                OnChanged();
            }
        }

        string _Country;
        public string Country
        {
            get { return _Country; }
            set
            {
                
                _Country=value;
                var col=tbl.Columns.SingleOrDefault(x=>x.Name=="Country");
                if(col!=null){
                    if(!_dirtyColumns.Contains(col) && _isLoaded){
                        _dirtyColumns.Add(col);
                    }
                }
                OnChanged();
            }
        }

        string _Latitude;
        public string Latitude
        {
            get { return _Latitude; }
            set
            {
                
                _Latitude=value;
                var col=tbl.Columns.SingleOrDefault(x=>x.Name=="Latitude");
                if(col!=null){
                    if(!_dirtyColumns.Contains(col) && _isLoaded){
                        _dirtyColumns.Add(col);
                    }
                }
                OnChanged();
            }
        }

        string _Longitude;
        public string Longitude
        {
            get { return _Longitude; }
            set
            {
                
                _Longitude=value;
                var col=tbl.Columns.SingleOrDefault(x=>x.Name=="Longitude");
                if(col!=null){
                    if(!_dirtyColumns.Contains(col) && _isLoaded){
                        _dirtyColumns.Add(col);
                    }
                }
                OnChanged();
            }
        }

        bool _IsDefault;
        public bool IsDefault
        {
            get { return _IsDefault; }
            set
            {
                
                _IsDefault=value;
                var col=tbl.Columns.SingleOrDefault(x=>x.Name=="IsDefault");
                if(col!=null){
                    if(!_dirtyColumns.Contains(col) && _isLoaded){
                        _dirtyColumns.Add(col);
                    }
                }
                OnChanged();
            }
        }



        public DbCommand GetUpdateCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return _repo.BuildUpdateQuery(this).GetCommand().ToDbCommand();
            
        }
        public DbCommand GetInsertCommand() {
 
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else{
            
            
                return _repo.BuildInsertQuery(this).GetCommand().ToDbCommand();
                
            }
        }
        
        public DbCommand GetDeleteCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return _repo.BuildDeleteQuery(this).GetCommand().ToDbCommand();
        }
        //persistence
        public void Save(){
            Save("");
        }
        
        public void Update(string userName){
            _repo.Update(this);
            OnSaved();
       }
        
        public void Add(string userName){
            
            
            this.SetKeyValue(_repo.Add(this));
            OnSaved();
        }
        
        public void Save(string userName) {
            
           
            if (_isNew) {
                Add(userName);
                
            } else {
                Update(userName);
            }
            
        }

        public void Delete() {
                   
                 
            _repo.Delete(KeyValue());
            
                    }

        public static void Delete(object key) {
        
            var repo = new SubSonicRepository<Address>(new Kona.Data.KonaDB());

                   
                 
            repo.Delete(key);
            
                        
        }

        public static void Delete(Func<Address, bool> expression) {
            var repo = GetRepo();
            
                   
            
            repo.Delete(expression);
            
                    }

        

        public void Load(IDataReader rdr) {
            Load(rdr, true);
        }
        public void Load(IDataReader rdr, bool closeReader) {
            if (rdr.Read()) {

                try {
                    rdr.Load(this);
                    _isNew = false;
                    _isLoaded = true;
                } catch {
                    _isLoaded = false;
                    throw;
                }
            }else{
                _isLoaded = false;
            }

            if (closeReader)
                rdr.Dispose();
        }
        

    } 
    
    
    /// <summary>
    /// A class which represents the Categories_Products table in the Kona Database.
    /// </summary>
    public partial class Categories_Product: IActiveRecord
    {
    
        #region Built-in testing
        static IList<Categories_Product> TestItems;
        static TestRepository<Categories_Product> _testRepo;
        
        static void SetTestRepo(){
            _testRepo = _testRepo ?? new TestRepository<Categories_Product>(new Kona.Data.KonaDB());
        }
        public static void ResetTestRepo(){
            _testRepo = null;
            SetTestRepo();
        }
        public static void Setup(List<Categories_Product> testlist){
            SetTestRepo();
            _testRepo._items = testlist;
        }
        public static void Setup(Categories_Product item) {
            SetTestRepo();
            _testRepo._items.Add(item);
        }
        public static void Setup(int testItems) {
            SetTestRepo();
            for(int i=0;i<testItems;i++){
                Categories_Product item=new Categories_Product();
                _testRepo._items.Add(item);
            }
        }
        
        public bool TestMode {
            get {
                return this._db.DataProvider.ConnectionString.Equals("test", StringComparison.InvariantCultureIgnoreCase);
            }
        }

        #endregion

        IRepository<Categories_Product> _repo;
        ITable tbl;
        bool _isNew;
        public bool IsNew(){
            return _isNew;
        }
        public void SetIsNew(bool isNew){
            _isNew=isNew;
        }
        public void SetIsLoaded(bool isLoaded){
            _isLoaded=isLoaded;
        }
        bool _isLoaded;
        public bool IsLoaded(){
            return _isLoaded;
        }
                
        List<IColumn> _dirtyColumns;
        public bool IsDirty(){
            return _dirtyColumns.Count>0;
        }
        
        public List<IColumn> GetDirtyColumns (){
            return _dirtyColumns;
        }

        Kona.Data.KonaDB _db;
        public Categories_Product() {
             _db=new Kona.Data.KonaDB();
            _dirtyColumns=new List<IColumn>();
            
            if(TestMode){
                Categories_Product.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<Categories_Product>(_db);
            }
            tbl=_repo.GetTable();
            _isNew = true;
            OnCreated();
        }
        
       
        partial void OnCreated();
            
        partial void OnLoaded();
        
        partial void OnSaved();
        
        partial void OnChanged();
        
        public IList<IColumn> Columns{
            get{
                return tbl.Columns;
            }
        }
        
        public Categories_Product(object key):this() {
            _isLoaded=_repo.Load(this,this.KeyName(),key);
            if(_isLoaded)
                OnLoaded();

        }

        public Categories_Product(Expression<Func<Categories_Product, bool>> expression):this() {
            _isLoaded=_repo.Load(this,expression);
            if(_isLoaded)
                OnLoaded();
        }
        
        public static SubSonic.Query.SqlQuery Select{
            get{
                var db=new Kona.Data.KonaDB();
                return db.Select.From<Categories_Product>();
            }
        }
        public static SubSonic.Query.SqlQuery SelectColumns(params string[] columns){
            var db=new Kona.Data.KonaDB();
            return new SubSonic.Query.Select(db.DataProvider, columns).From<Categories_Product>();
        }
        internal static IRepository<Categories_Product> GetRepo(){
            var db=new Kona.Data.KonaDB();
            IRepository<Categories_Product> _repo;
            
            if(db.TestMode){
                Categories_Product.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<Categories_Product>(db);
            }
            return _repo;
        }
        
        public static Categories_Product SingleOrDefault(Expression<Func<Categories_Product, bool>> expression) {
           
            var single= All().SingleOrDefault(expression);
            if (single != null){
                single.OnLoaded();
                single.SetIsLoaded(true);
                single.SetIsNew(false);
            }
            return single;
        }      
        public static bool Exists(Expression<Func<Categories_Product, bool>> expression) {
           
            return All().Any(expression);
        }        
        public static IList<Categories_Product> Find(Expression<Func<Categories_Product, bool>> expression) {
           
            return GetRepo().Find(expression).ToList();
        }
        public static IQueryable<Categories_Product> All() {
            return GetRepo().GetAll();
        }
        public static PagedList<Categories_Product> GetPaged<TKey>(Func<Categories_Product,TKey> orderby, int pageIndex, int pageSize){
            return GetRepo().GetPaged(orderby, pageIndex, pageSize);
        }
        public static PagedList<Categories_Product> GetPaged<TKey>(string sortBy, int pageIndex, int pageSize) {
            return GetRepo().GetPaged(sortBy, pageIndex, pageSize);
        }
        public static PagedList<Categories_Product> GetPaged<TKey>(int pageIndex, int pageSize) {
            return GetRepo().GetPaged(pageIndex, pageSize);
            
        }

        public string KeyName()
        {
            return "CategoryID";
        }

        public object KeyValue()
        {
            return this.CategoryID;
        }
        
        public void SetKeyValue(object value) {
            if (value != DBNull.Value) {
                var settable = value.ChangeTypeTo<int>();
                this.GetType().GetProperty(this.KeyName()).SetValue(this, settable, null);
            }
        }
        
        public override string ToString(){
            return this.SKU.ToString();
        }

        public override bool Equals(object obj){
            if(obj.GetType()==typeof(Categories_Product)){
                Categories_Product compare=(Categories_Product)obj;
                int thisPk=(int)this.KeyValue();
                int comparePk=(int)compare.KeyValue();
                return thisPk.Equals(comparePk);
            }else{
                return base.Equals(obj);
            }
        }

        public string DescriptorValue()
        {
            return this.SKU.ToString();
        }

        public string DescriptorColumn() {
            return "SKU";
        }
        
        public static string GetDescriptorColumn()
        {
            return "SKU";
        }
        
        #region ' Foreign Keys '
        IList<Category> _Categories;
        public IList<Category> Categories
        {
            get
            {
                
                  if(_Categories==null){
                      var repo=Category.GetRepo();
                  
                       _Categories = (from items in repo.GetAll()
                           where items.CategoryID == _CategoryID
                           select items).ToList();
                   }
                  return _Categories;
            }
            set{
                _Categories=value;
            }
        }

        IList<Product> _Products;
        public IList<Product> Products
        {
            get
            {
                
                  if(_Products==null){
                      var repo=Product.GetRepo();
                  
                       _Products = (from items in repo.GetAll()
                           where items.SKU == _SKU
                           select items).ToList();
                   }
                  return _Products;
            }
            set{
                _Products=value;
            }
        }

        #endregion
        

        int _CategoryID;
        public int CategoryID
        {
            get { return _CategoryID; }
            set
            {
                
                _CategoryID=value;
                var col=tbl.Columns.SingleOrDefault(x=>x.Name=="CategoryID");
                if(col!=null){
                    if(!_dirtyColumns.Contains(col) && _isLoaded){
                        _dirtyColumns.Add(col);
                    }
                }
                OnChanged();
            }
        }

        string _SKU;
        public string SKU
        {
            get { return _SKU; }
            set
            {
                
                _SKU=value;
                var col=tbl.Columns.SingleOrDefault(x=>x.Name=="SKU");
                if(col!=null){
                    if(!_dirtyColumns.Contains(col) && _isLoaded){
                        _dirtyColumns.Add(col);
                    }
                }
                OnChanged();
            }
        }



        public DbCommand GetUpdateCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return _repo.BuildUpdateQuery(this).GetCommand().ToDbCommand();
            
        }
        public DbCommand GetInsertCommand() {
 
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else{
            
            
                return _repo.BuildInsertQuery(this).GetCommand().ToDbCommand();
                
            }
        }
        
        public DbCommand GetDeleteCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return _repo.BuildDeleteQuery(this).GetCommand().ToDbCommand();
        }
        //persistence
        public void Save(){
            Save("");
        }
        
        public void Update(string userName){
            _repo.Update(this);
            OnSaved();
       }
        
        public void Add(string userName){
            
            _repo.Add(this);
            OnSaved();
        }
        
        public void Save(string userName) {
            
           
            if (_isNew) {
                Add(userName);
                
            } else {
                Update(userName);
            }
            
        }

        public void Delete() {
                   
                 
            _repo.Delete(KeyValue());
            
                    }

        public static void Delete(object key) {
        
            var repo = new SubSonicRepository<Categories_Product>(new Kona.Data.KonaDB());

                   
                 
            repo.Delete(key);
            
                        
        }

        public static void Delete(Func<Categories_Product, bool> expression) {
            var repo = GetRepo();
            
                   
            
            repo.Delete(expression);
            
                    }

        

        public void Load(IDataReader rdr) {
            Load(rdr, true);
        }
        public void Load(IDataReader rdr, bool closeReader) {
            if (rdr.Read()) {

                try {
                    rdr.Load(this);
                    _isNew = false;
                    _isLoaded = true;
                } catch {
                    _isLoaded = false;
                    throw;
                }
            }else{
                _isLoaded = false;
            }

            if (closeReader)
                rdr.Dispose();
        }
        

    } 
    
    
    /// <summary>
    /// A class which represents the Products_CrossSell table in the Kona Database.
    /// </summary>
    public partial class Products_CrossSell: IActiveRecord
    {
    
        #region Built-in testing
        static IList<Products_CrossSell> TestItems;
        static TestRepository<Products_CrossSell> _testRepo;
        
        static void SetTestRepo(){
            _testRepo = _testRepo ?? new TestRepository<Products_CrossSell>(new Kona.Data.KonaDB());
        }
        public static void ResetTestRepo(){
            _testRepo = null;
            SetTestRepo();
        }
        public static void Setup(List<Products_CrossSell> testlist){
            SetTestRepo();
            _testRepo._items = testlist;
        }
        public static void Setup(Products_CrossSell item) {
            SetTestRepo();
            _testRepo._items.Add(item);
        }
        public static void Setup(int testItems) {
            SetTestRepo();
            for(int i=0;i<testItems;i++){
                Products_CrossSell item=new Products_CrossSell();
                _testRepo._items.Add(item);
            }
        }
        
        public bool TestMode {
            get {
                return this._db.DataProvider.ConnectionString.Equals("test", StringComparison.InvariantCultureIgnoreCase);
            }
        }

        #endregion

        IRepository<Products_CrossSell> _repo;
        ITable tbl;
        bool _isNew;
        public bool IsNew(){
            return _isNew;
        }
        public void SetIsNew(bool isNew){
            _isNew=isNew;
        }
        public void SetIsLoaded(bool isLoaded){
            _isLoaded=isLoaded;
        }
        bool _isLoaded;
        public bool IsLoaded(){
            return _isLoaded;
        }
                
        List<IColumn> _dirtyColumns;
        public bool IsDirty(){
            return _dirtyColumns.Count>0;
        }
        
        public List<IColumn> GetDirtyColumns (){
            return _dirtyColumns;
        }

        Kona.Data.KonaDB _db;
        public Products_CrossSell() {
             _db=new Kona.Data.KonaDB();
            _dirtyColumns=new List<IColumn>();
            
            if(TestMode){
                Products_CrossSell.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<Products_CrossSell>(_db);
            }
            tbl=_repo.GetTable();
            _isNew = true;
            OnCreated();
        }
        
       
        partial void OnCreated();
            
        partial void OnLoaded();
        
        partial void OnSaved();
        
        partial void OnChanged();
        
        public IList<IColumn> Columns{
            get{
                return tbl.Columns;
            }
        }
        
        public Products_CrossSell(object key):this() {
            _isLoaded=_repo.Load(this,this.KeyName(),key);
            if(_isLoaded)
                OnLoaded();

        }

        public Products_CrossSell(Expression<Func<Products_CrossSell, bool>> expression):this() {
            _isLoaded=_repo.Load(this,expression);
            if(_isLoaded)
                OnLoaded();
        }
        
        public static SubSonic.Query.SqlQuery Select{
            get{
                var db=new Kona.Data.KonaDB();
                return db.Select.From<Products_CrossSell>();
            }
        }
        public static SubSonic.Query.SqlQuery SelectColumns(params string[] columns){
            var db=new Kona.Data.KonaDB();
            return new SubSonic.Query.Select(db.DataProvider, columns).From<Products_CrossSell>();
        }
        internal static IRepository<Products_CrossSell> GetRepo(){
            var db=new Kona.Data.KonaDB();
            IRepository<Products_CrossSell> _repo;
            
            if(db.TestMode){
                Products_CrossSell.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<Products_CrossSell>(db);
            }
            return _repo;
        }
        
        public static Products_CrossSell SingleOrDefault(Expression<Func<Products_CrossSell, bool>> expression) {
           
            var single= All().SingleOrDefault(expression);
            if (single != null){
                single.OnLoaded();
                single.SetIsLoaded(true);
                single.SetIsNew(false);
            }
            return single;
        }      
        public static bool Exists(Expression<Func<Products_CrossSell, bool>> expression) {
           
            return All().Any(expression);
        }        
        public static IList<Products_CrossSell> Find(Expression<Func<Products_CrossSell, bool>> expression) {
           
            return GetRepo().Find(expression).ToList();
        }
        public static IQueryable<Products_CrossSell> All() {
            return GetRepo().GetAll();
        }
        public static PagedList<Products_CrossSell> GetPaged<TKey>(Func<Products_CrossSell,TKey> orderby, int pageIndex, int pageSize){
            return GetRepo().GetPaged(orderby, pageIndex, pageSize);
        }
        public static PagedList<Products_CrossSell> GetPaged<TKey>(string sortBy, int pageIndex, int pageSize) {
            return GetRepo().GetPaged(sortBy, pageIndex, pageSize);
        }
        public static PagedList<Products_CrossSell> GetPaged<TKey>(int pageIndex, int pageSize) {
            return GetRepo().GetPaged(pageIndex, pageSize);
            
        }

        public string KeyName()
        {
            return "CrossSKU";
        }

        public object KeyValue()
        {
            return this.CrossSKU;
        }
        
        public void SetKeyValue(object value) {
            if (value != DBNull.Value) {
                var settable = value.ChangeTypeTo<string>();
                this.GetType().GetProperty(this.KeyName()).SetValue(this, settable, null);
            }
        }
        
        public override string ToString(){
            return this.SKU.ToString();
        }

        public override bool Equals(object obj){
            if(obj.GetType()==typeof(Products_CrossSell)){
                Products_CrossSell compare=(Products_CrossSell)obj;
                string thisPk=(string)this.KeyValue();
                string comparePk=(string)compare.KeyValue();
                return thisPk.Equals(comparePk);
            }else{
                return base.Equals(obj);
            }
        }

        public string DescriptorValue()
        {
            return this.SKU.ToString();
        }

        public string DescriptorColumn() {
            return "SKU";
        }
        
        public static string GetDescriptorColumn()
        {
            return "SKU";
        }
        
        #region ' Foreign Keys '
        IList<Product> _Products;
        public IList<Product> Products
        {
            get
            {
                
                  if(_Products==null){
                      var repo=Product.GetRepo();
                  
                       _Products = (from items in repo.GetAll()
                           where items.SKU == _SKU
                           select items).ToList();
                   }
                  return _Products;
            }
            set{
                _Products=value;
            }
        }

        IList<Product> _Products1;
        public IList<Product> Products1
        {
            get
            {
                
                  if(_Products1==null){
                      var repo=Product.GetRepo();
                  
                       _Products1 = (from items in repo.GetAll()
                           where items.SKU == _CrossSKU
                           select items).ToList();
                   }
                  return _Products1;
            }
            set{
                _Products1=value;
            }
        }

        #endregion
        

        string _SKU;
        public string SKU
        {
            get { return _SKU; }
            set
            {
                
                _SKU=value;
                var col=tbl.Columns.SingleOrDefault(x=>x.Name=="SKU");
                if(col!=null){
                    if(!_dirtyColumns.Contains(col) && _isLoaded){
                        _dirtyColumns.Add(col);
                    }
                }
                OnChanged();
            }
        }

        string _CrossSKU;
        public string CrossSKU
        {
            get { return _CrossSKU; }
            set
            {
                
                _CrossSKU=value;
                var col=tbl.Columns.SingleOrDefault(x=>x.Name=="CrossSKU");
                if(col!=null){
                    if(!_dirtyColumns.Contains(col) && _isLoaded){
                        _dirtyColumns.Add(col);
                    }
                }
                OnChanged();
            }
        }



        public DbCommand GetUpdateCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return _repo.BuildUpdateQuery(this).GetCommand().ToDbCommand();
            
        }
        public DbCommand GetInsertCommand() {
 
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else{
            
            
                return _repo.BuildInsertQuery(this).GetCommand().ToDbCommand();
                
            }
        }
        
        public DbCommand GetDeleteCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return _repo.BuildDeleteQuery(this).GetCommand().ToDbCommand();
        }
        //persistence
        public void Save(){
            Save("");
        }
        
        public void Update(string userName){
            _repo.Update(this);
            OnSaved();
       }
        
        public void Add(string userName){
            
            _repo.Add(this);
            OnSaved();
        }
        
        public void Save(string userName) {
            
           
            if (_isNew) {
                Add(userName);
                
            } else {
                Update(userName);
            }
            
        }

        public void Delete() {
                   
                 
            _repo.Delete(KeyValue());
            
                    }

        public static void Delete(object key) {
        
            var repo = new SubSonicRepository<Products_CrossSell>(new Kona.Data.KonaDB());

                   
                 
            repo.Delete(key);
            
                        
        }

        public static void Delete(Func<Products_CrossSell, bool> expression) {
            var repo = GetRepo();
            
                   
            
            repo.Delete(expression);
            
                    }

        

        public void Load(IDataReader rdr) {
            Load(rdr, true);
        }
        public void Load(IDataReader rdr, bool closeReader) {
            if (rdr.Read()) {

                try {
                    rdr.Load(this);
                    _isNew = false;
                    _isLoaded = true;
                } catch {
                    _isLoaded = false;
                    throw;
                }
            }else{
                _isLoaded = false;
            }

            if (closeReader)
                rdr.Dispose();
        }
        

    } 
    
    
    /// <summary>
    /// A class which represents the Products_Related table in the Kona Database.
    /// </summary>
    public partial class Products_Related: IActiveRecord
    {
    
        #region Built-in testing
        static IList<Products_Related> TestItems;
        static TestRepository<Products_Related> _testRepo;
        
        static void SetTestRepo(){
            _testRepo = _testRepo ?? new TestRepository<Products_Related>(new Kona.Data.KonaDB());
        }
        public static void ResetTestRepo(){
            _testRepo = null;
            SetTestRepo();
        }
        public static void Setup(List<Products_Related> testlist){
            SetTestRepo();
            _testRepo._items = testlist;
        }
        public static void Setup(Products_Related item) {
            SetTestRepo();
            _testRepo._items.Add(item);
        }
        public static void Setup(int testItems) {
            SetTestRepo();
            for(int i=0;i<testItems;i++){
                Products_Related item=new Products_Related();
                _testRepo._items.Add(item);
            }
        }
        
        public bool TestMode {
            get {
                return this._db.DataProvider.ConnectionString.Equals("test", StringComparison.InvariantCultureIgnoreCase);
            }
        }

        #endregion

        IRepository<Products_Related> _repo;
        ITable tbl;
        bool _isNew;
        public bool IsNew(){
            return _isNew;
        }
        public void SetIsNew(bool isNew){
            _isNew=isNew;
        }
        public void SetIsLoaded(bool isLoaded){
            _isLoaded=isLoaded;
        }
        bool _isLoaded;
        public bool IsLoaded(){
            return _isLoaded;
        }
                
        List<IColumn> _dirtyColumns;
        public bool IsDirty(){
            return _dirtyColumns.Count>0;
        }
        
        public List<IColumn> GetDirtyColumns (){
            return _dirtyColumns;
        }

        Kona.Data.KonaDB _db;
        public Products_Related() {
             _db=new Kona.Data.KonaDB();
            _dirtyColumns=new List<IColumn>();
            
            if(TestMode){
                Products_Related.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<Products_Related>(_db);
            }
            tbl=_repo.GetTable();
            _isNew = true;
            OnCreated();
        }
        
       
        partial void OnCreated();
            
        partial void OnLoaded();
        
        partial void OnSaved();
        
        partial void OnChanged();
        
        public IList<IColumn> Columns{
            get{
                return tbl.Columns;
            }
        }
        
        public Products_Related(object key):this() {
            _isLoaded=_repo.Load(this,this.KeyName(),key);
            if(_isLoaded)
                OnLoaded();

        }

        public Products_Related(Expression<Func<Products_Related, bool>> expression):this() {
            _isLoaded=_repo.Load(this,expression);
            if(_isLoaded)
                OnLoaded();
        }
        
        public static SubSonic.Query.SqlQuery Select{
            get{
                var db=new Kona.Data.KonaDB();
                return db.Select.From<Products_Related>();
            }
        }
        public static SubSonic.Query.SqlQuery SelectColumns(params string[] columns){
            var db=new Kona.Data.KonaDB();
            return new SubSonic.Query.Select(db.DataProvider, columns).From<Products_Related>();
        }
        internal static IRepository<Products_Related> GetRepo(){
            var db=new Kona.Data.KonaDB();
            IRepository<Products_Related> _repo;
            
            if(db.TestMode){
                Products_Related.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<Products_Related>(db);
            }
            return _repo;
        }
        
        public static Products_Related SingleOrDefault(Expression<Func<Products_Related, bool>> expression) {
           
            var single= All().SingleOrDefault(expression);
            if (single != null){
                single.OnLoaded();
                single.SetIsLoaded(true);
                single.SetIsNew(false);
            }
            return single;
        }      
        public static bool Exists(Expression<Func<Products_Related, bool>> expression) {
           
            return All().Any(expression);
        }        
        public static IList<Products_Related> Find(Expression<Func<Products_Related, bool>> expression) {
           
            return GetRepo().Find(expression).ToList();
        }
        public static IQueryable<Products_Related> All() {
            return GetRepo().GetAll();
        }
        public static PagedList<Products_Related> GetPaged<TKey>(Func<Products_Related,TKey> orderby, int pageIndex, int pageSize){
            return GetRepo().GetPaged(orderby, pageIndex, pageSize);
        }
        public static PagedList<Products_Related> GetPaged<TKey>(string sortBy, int pageIndex, int pageSize) {
            return GetRepo().GetPaged(sortBy, pageIndex, pageSize);
        }
        public static PagedList<Products_Related> GetPaged<TKey>(int pageIndex, int pageSize) {
            return GetRepo().GetPaged(pageIndex, pageSize);
            
        }

        public string KeyName()
        {
            return "RelatedSKU";
        }

        public object KeyValue()
        {
            return this.RelatedSKU;
        }
        
        public void SetKeyValue(object value) {
            if (value != DBNull.Value) {
                var settable = value.ChangeTypeTo<string>();
                this.GetType().GetProperty(this.KeyName()).SetValue(this, settable, null);
            }
        }
        
        public override string ToString(){
            return this.SKU.ToString();
        }

        public override bool Equals(object obj){
            if(obj.GetType()==typeof(Products_Related)){
                Products_Related compare=(Products_Related)obj;
                string thisPk=(string)this.KeyValue();
                string comparePk=(string)compare.KeyValue();
                return thisPk.Equals(comparePk);
            }else{
                return base.Equals(obj);
            }
        }

        public string DescriptorValue()
        {
            return this.SKU.ToString();
        }

        public string DescriptorColumn() {
            return "SKU";
        }
        
        public static string GetDescriptorColumn()
        {
            return "SKU";
        }
        
        #region ' Foreign Keys '
        IList<Product> _Products;
        public IList<Product> Products
        {
            get
            {
                
                  if(_Products==null){
                      var repo=Product.GetRepo();
                  
                       _Products = (from items in repo.GetAll()
                           where items.SKU == _SKU
                           select items).ToList();
                   }
                  return _Products;
            }
            set{
                _Products=value;
            }
        }

        IList<Product> _Products1;
        public IList<Product> Products1
        {
            get
            {
                
                  if(_Products1==null){
                      var repo=Product.GetRepo();
                  
                       _Products1 = (from items in repo.GetAll()
                           where items.SKU == _RelatedSKU
                           select items).ToList();
                   }
                  return _Products1;
            }
            set{
                _Products1=value;
            }
        }

        #endregion
        

        string _SKU;
        public string SKU
        {
            get { return _SKU; }
            set
            {
                
                _SKU=value;
                var col=tbl.Columns.SingleOrDefault(x=>x.Name=="SKU");
                if(col!=null){
                    if(!_dirtyColumns.Contains(col) && _isLoaded){
                        _dirtyColumns.Add(col);
                    }
                }
                OnChanged();
            }
        }

        string _RelatedSKU;
        public string RelatedSKU
        {
            get { return _RelatedSKU; }
            set
            {
                
                _RelatedSKU=value;
                var col=tbl.Columns.SingleOrDefault(x=>x.Name=="RelatedSKU");
                if(col!=null){
                    if(!_dirtyColumns.Contains(col) && _isLoaded){
                        _dirtyColumns.Add(col);
                    }
                }
                OnChanged();
            }
        }



        public DbCommand GetUpdateCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return _repo.BuildUpdateQuery(this).GetCommand().ToDbCommand();
            
        }
        public DbCommand GetInsertCommand() {
 
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else{
            
            
                return _repo.BuildInsertQuery(this).GetCommand().ToDbCommand();
                
            }
        }
        
        public DbCommand GetDeleteCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return _repo.BuildDeleteQuery(this).GetCommand().ToDbCommand();
        }
        //persistence
        public void Save(){
            Save("");
        }
        
        public void Update(string userName){
            _repo.Update(this);
            OnSaved();
       }
        
        public void Add(string userName){
            
            _repo.Add(this);
            OnSaved();
        }
        
        public void Save(string userName) {
            
           
            if (_isNew) {
                Add(userName);
                
            } else {
                Update(userName);
            }
            
        }

        public void Delete() {
                   
                 
            _repo.Delete(KeyValue());
            
                    }

        public static void Delete(object key) {
        
            var repo = new SubSonicRepository<Products_Related>(new Kona.Data.KonaDB());

                   
                 
            repo.Delete(key);
            
                        
        }

        public static void Delete(Func<Products_Related, bool> expression) {
            var repo = GetRepo();
            
                   
            
            repo.Delete(expression);
            
                    }

        

        public void Load(IDataReader rdr) {
            Load(rdr, true);
        }
        public void Load(IDataReader rdr, bool closeReader) {
            if (rdr.Read()) {

                try {
                    rdr.Load(this);
                    _isNew = false;
                    _isLoaded = true;
                } catch {
                    _isLoaded = false;
                    throw;
                }
            }else{
                _isLoaded = false;
            }

            if (closeReader)
                rdr.Dispose();
        }
        

    } 
    
    
    /// <summary>
    /// A class which represents the ProductImages table in the Kona Database.
    /// </summary>
    public partial class ProductImage: IActiveRecord
    {
    
        #region Built-in testing
        static IList<ProductImage> TestItems;
        static TestRepository<ProductImage> _testRepo;
        
        static void SetTestRepo(){
            _testRepo = _testRepo ?? new TestRepository<ProductImage>(new Kona.Data.KonaDB());
        }
        public static void ResetTestRepo(){
            _testRepo = null;
            SetTestRepo();
        }
        public static void Setup(List<ProductImage> testlist){
            SetTestRepo();
            _testRepo._items = testlist;
        }
        public static void Setup(ProductImage item) {
            SetTestRepo();
            _testRepo._items.Add(item);
        }
        public static void Setup(int testItems) {
            SetTestRepo();
            for(int i=0;i<testItems;i++){
                ProductImage item=new ProductImage();
                _testRepo._items.Add(item);
            }
        }
        
        public bool TestMode {
            get {
                return this._db.DataProvider.ConnectionString.Equals("test", StringComparison.InvariantCultureIgnoreCase);
            }
        }

        #endregion

        IRepository<ProductImage> _repo;
        ITable tbl;
        bool _isNew;
        public bool IsNew(){
            return _isNew;
        }
        public void SetIsNew(bool isNew){
            _isNew=isNew;
        }
        public void SetIsLoaded(bool isLoaded){
            _isLoaded=isLoaded;
        }
        bool _isLoaded;
        public bool IsLoaded(){
            return _isLoaded;
        }
                
        List<IColumn> _dirtyColumns;
        public bool IsDirty(){
            return _dirtyColumns.Count>0;
        }
        
        public List<IColumn> GetDirtyColumns (){
            return _dirtyColumns;
        }

        Kona.Data.KonaDB _db;
        public ProductImage() {
             _db=new Kona.Data.KonaDB();
            _dirtyColumns=new List<IColumn>();
            
            if(TestMode){
                ProductImage.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<ProductImage>(_db);
            }
            tbl=_repo.GetTable();
            _isNew = true;
            OnCreated();
        }
        
       
        partial void OnCreated();
            
        partial void OnLoaded();
        
        partial void OnSaved();
        
        partial void OnChanged();
        
        public IList<IColumn> Columns{
            get{
                return tbl.Columns;
            }
        }
        
        public ProductImage(object key):this() {
            _isLoaded=_repo.Load(this,this.KeyName(),key);
            if(_isLoaded)
                OnLoaded();

        }

        public ProductImage(Expression<Func<ProductImage, bool>> expression):this() {
            _isLoaded=_repo.Load(this,expression);
            if(_isLoaded)
                OnLoaded();
        }
        
        public static SubSonic.Query.SqlQuery Select{
            get{
                var db=new Kona.Data.KonaDB();
                return db.Select.From<ProductImage>();
            }
        }
        public static SubSonic.Query.SqlQuery SelectColumns(params string[] columns){
            var db=new Kona.Data.KonaDB();
            return new SubSonic.Query.Select(db.DataProvider, columns).From<ProductImage>();
        }
        internal static IRepository<ProductImage> GetRepo(){
            var db=new Kona.Data.KonaDB();
            IRepository<ProductImage> _repo;
            
            if(db.TestMode){
                ProductImage.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<ProductImage>(db);
            }
            return _repo;
        }
        
        public static ProductImage SingleOrDefault(Expression<Func<ProductImage, bool>> expression) {
           
            var single= All().SingleOrDefault(expression);
            if (single != null){
                single.OnLoaded();
                single.SetIsLoaded(true);
                single.SetIsNew(false);
            }
            return single;
        }      
        public static bool Exists(Expression<Func<ProductImage, bool>> expression) {
           
            return All().Any(expression);
        }        
        public static IList<ProductImage> Find(Expression<Func<ProductImage, bool>> expression) {
           
            return GetRepo().Find(expression).ToList();
        }
        public static IQueryable<ProductImage> All() {
            return GetRepo().GetAll();
        }
        public static PagedList<ProductImage> GetPaged<TKey>(Func<ProductImage,TKey> orderby, int pageIndex, int pageSize){
            return GetRepo().GetPaged(orderby, pageIndex, pageSize);
        }
        public static PagedList<ProductImage> GetPaged<TKey>(string sortBy, int pageIndex, int pageSize) {
            return GetRepo().GetPaged(sortBy, pageIndex, pageSize);
        }
        public static PagedList<ProductImage> GetPaged<TKey>(int pageIndex, int pageSize) {
            return GetRepo().GetPaged(pageIndex, pageSize);
            
        }

        public string KeyName()
        {
            return "ProductImageID";
        }

        public object KeyValue()
        {
            return this.ProductImageID;
        }
        
        public void SetKeyValue(object value) {
            if (value != DBNull.Value) {
                var settable = value.ChangeTypeTo<int>();
                this.GetType().GetProperty(this.KeyName()).SetValue(this, settable, null);
            }
        }
        
        public override string ToString(){
            return this.SKU.ToString();
        }

        public override bool Equals(object obj){
            if(obj.GetType()==typeof(ProductImage)){
                ProductImage compare=(ProductImage)obj;
                int thisPk=(int)this.KeyValue();
                int comparePk=(int)compare.KeyValue();
                return thisPk.Equals(comparePk);
            }else{
                return base.Equals(obj);
            }
        }

        public string DescriptorValue()
        {
            return this.SKU.ToString();
        }

        public string DescriptorColumn() {
            return "SKU";
        }
        
        public static string GetDescriptorColumn()
        {
            return "SKU";
        }
        
        #region ' Foreign Keys '
        IList<Product> _Products;
        public IList<Product> Products
        {
            get
            {
                
                  if(_Products==null){
                      var repo=Product.GetRepo();
                  
                       _Products = (from items in repo.GetAll()
                           where items.SKU == _SKU
                           select items).ToList();
                   }
                  return _Products;
            }
            set{
                _Products=value;
            }
        }

        #endregion
        

        int _ProductImageID;
        public int ProductImageID
        {
            get { return _ProductImageID; }
            set
            {
                
                _ProductImageID=value;
                var col=tbl.Columns.SingleOrDefault(x=>x.Name=="ProductImageID");
                if(col!=null){
                    if(!_dirtyColumns.Contains(col) && _isLoaded){
                        _dirtyColumns.Add(col);
                    }
                }
                OnChanged();
            }
        }

        string _SKU;
        public string SKU
        {
            get { return _SKU; }
            set
            {
                
                _SKU=value;
                var col=tbl.Columns.SingleOrDefault(x=>x.Name=="SKU");
                if(col!=null){
                    if(!_dirtyColumns.Contains(col) && _isLoaded){
                        _dirtyColumns.Add(col);
                    }
                }
                OnChanged();
            }
        }

        string _ThumbUrl;
        public string ThumbUrl
        {
            get { return _ThumbUrl; }
            set
            {
                
                _ThumbUrl=value;
                var col=tbl.Columns.SingleOrDefault(x=>x.Name=="ThumbUrl");
                if(col!=null){
                    if(!_dirtyColumns.Contains(col) && _isLoaded){
                        _dirtyColumns.Add(col);
                    }
                }
                OnChanged();
            }
        }

        string _FullImageUrl;
        public string FullImageUrl
        {
            get { return _FullImageUrl; }
            set
            {
                
                _FullImageUrl=value;
                var col=tbl.Columns.SingleOrDefault(x=>x.Name=="FullImageUrl");
                if(col!=null){
                    if(!_dirtyColumns.Contains(col) && _isLoaded){
                        _dirtyColumns.Add(col);
                    }
                }
                OnChanged();
            }
        }



        public DbCommand GetUpdateCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return _repo.BuildUpdateQuery(this).GetCommand().ToDbCommand();
            
        }
        public DbCommand GetInsertCommand() {
 
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else{
            
            
                return _repo.BuildInsertQuery(this).GetCommand().ToDbCommand();
                
            }
        }
        
        public DbCommand GetDeleteCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return _repo.BuildDeleteQuery(this).GetCommand().ToDbCommand();
        }
        //persistence
        public void Save(){
            Save("");
        }
        
        public void Update(string userName){
            _repo.Update(this);
            OnSaved();
       }
        
        public void Add(string userName){
            
            
            this.SetKeyValue(_repo.Add(this));
            OnSaved();
        }
        
        public void Save(string userName) {
            
           
            if (_isNew) {
                Add(userName);
                
            } else {
                Update(userName);
            }
            
        }

        public void Delete() {
                   
                 
            _repo.Delete(KeyValue());
            
                    }

        public static void Delete(object key) {
        
            var repo = new SubSonicRepository<ProductImage>(new Kona.Data.KonaDB());

                   
                 
            repo.Delete(key);
            
                        
        }

        public static void Delete(Func<ProductImage, bool> expression) {
            var repo = GetRepo();
            
                   
            
            repo.Delete(expression);
            
                    }

        

        public void Load(IDataReader rdr) {
            Load(rdr, true);
        }
        public void Load(IDataReader rdr, bool closeReader) {
            if (rdr.Read()) {

                try {
                    rdr.Load(this);
                    _isNew = false;
                    _isLoaded = true;
                } catch {
                    _isLoaded = false;
                    throw;
                }
            }else{
                _isLoaded = false;
            }

            if (closeReader)
                rdr.Dispose();
        }
        

    } 
    
    
    /// <summary>
    /// A class which represents the ProductDescriptors table in the Kona Database.
    /// </summary>
    public partial class ProductDescriptor: IActiveRecord
    {
    
        #region Built-in testing
        static IList<ProductDescriptor> TestItems;
        static TestRepository<ProductDescriptor> _testRepo;
        
        static void SetTestRepo(){
            _testRepo = _testRepo ?? new TestRepository<ProductDescriptor>(new Kona.Data.KonaDB());
        }
        public static void ResetTestRepo(){
            _testRepo = null;
            SetTestRepo();
        }
        public static void Setup(List<ProductDescriptor> testlist){
            SetTestRepo();
            _testRepo._items = testlist;
        }
        public static void Setup(ProductDescriptor item) {
            SetTestRepo();
            _testRepo._items.Add(item);
        }
        public static void Setup(int testItems) {
            SetTestRepo();
            for(int i=0;i<testItems;i++){
                ProductDescriptor item=new ProductDescriptor();
                _testRepo._items.Add(item);
            }
        }
        
        public bool TestMode {
            get {
                return this._db.DataProvider.ConnectionString.Equals("test", StringComparison.InvariantCultureIgnoreCase);
            }
        }

        #endregion

        IRepository<ProductDescriptor> _repo;
        ITable tbl;
        bool _isNew;
        public bool IsNew(){
            return _isNew;
        }
        public void SetIsNew(bool isNew){
            _isNew=isNew;
        }
        public void SetIsLoaded(bool isLoaded){
            _isLoaded=isLoaded;
        }
        bool _isLoaded;
        public bool IsLoaded(){
            return _isLoaded;
        }
                
        List<IColumn> _dirtyColumns;
        public bool IsDirty(){
            return _dirtyColumns.Count>0;
        }
        
        public List<IColumn> GetDirtyColumns (){
            return _dirtyColumns;
        }

        Kona.Data.KonaDB _db;
        public ProductDescriptor() {
             _db=new Kona.Data.KonaDB();
            _dirtyColumns=new List<IColumn>();
            
            if(TestMode){
                ProductDescriptor.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<ProductDescriptor>(_db);
            }
            tbl=_repo.GetTable();
            _isNew = true;
            OnCreated();
        }
        
       
        partial void OnCreated();
            
        partial void OnLoaded();
        
        partial void OnSaved();
        
        partial void OnChanged();
        
        public IList<IColumn> Columns{
            get{
                return tbl.Columns;
            }
        }
        
        public ProductDescriptor(object key):this() {
            _isLoaded=_repo.Load(this,this.KeyName(),key);
            if(_isLoaded)
                OnLoaded();

        }

        public ProductDescriptor(Expression<Func<ProductDescriptor, bool>> expression):this() {
            _isLoaded=_repo.Load(this,expression);
            if(_isLoaded)
                OnLoaded();
        }
        
        public static SubSonic.Query.SqlQuery Select{
            get{
                var db=new Kona.Data.KonaDB();
                return db.Select.From<ProductDescriptor>();
            }
        }
        public static SubSonic.Query.SqlQuery SelectColumns(params string[] columns){
            var db=new Kona.Data.KonaDB();
            return new SubSonic.Query.Select(db.DataProvider, columns).From<ProductDescriptor>();
        }
        internal static IRepository<ProductDescriptor> GetRepo(){
            var db=new Kona.Data.KonaDB();
            IRepository<ProductDescriptor> _repo;
            
            if(db.TestMode){
                ProductDescriptor.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<ProductDescriptor>(db);
            }
            return _repo;
        }
        
        public static ProductDescriptor SingleOrDefault(Expression<Func<ProductDescriptor, bool>> expression) {
           
            var single= All().SingleOrDefault(expression);
            if (single != null){
                single.OnLoaded();
                single.SetIsLoaded(true);
                single.SetIsNew(false);
            }
            return single;
        }      
        public static bool Exists(Expression<Func<ProductDescriptor, bool>> expression) {
           
            return All().Any(expression);
        }        
        public static IList<ProductDescriptor> Find(Expression<Func<ProductDescriptor, bool>> expression) {
           
            return GetRepo().Find(expression).ToList();
        }
        public static IQueryable<ProductDescriptor> All() {
            return GetRepo().GetAll();
        }
        public static PagedList<ProductDescriptor> GetPaged<TKey>(Func<ProductDescriptor,TKey> orderby, int pageIndex, int pageSize){
            return GetRepo().GetPaged(orderby, pageIndex, pageSize);
        }
        public static PagedList<ProductDescriptor> GetPaged<TKey>(string sortBy, int pageIndex, int pageSize) {
            return GetRepo().GetPaged(sortBy, pageIndex, pageSize);
        }
        public static PagedList<ProductDescriptor> GetPaged<TKey>(int pageIndex, int pageSize) {
            return GetRepo().GetPaged(pageIndex, pageSize);
            
        }

        public string KeyName()
        {
            return "DescriptorID";
        }

        public object KeyValue()
        {
            return this.DescriptorID;
        }
        
        public void SetKeyValue(object value) {
            if (value != DBNull.Value) {
                var settable = value.ChangeTypeTo<int>();
                this.GetType().GetProperty(this.KeyName()).SetValue(this, settable, null);
            }
        }
        
        public override string ToString(){
            return this.SKU.ToString();
        }

        public override bool Equals(object obj){
            if(obj.GetType()==typeof(ProductDescriptor)){
                ProductDescriptor compare=(ProductDescriptor)obj;
                int thisPk=(int)this.KeyValue();
                int comparePk=(int)compare.KeyValue();
                return thisPk.Equals(comparePk);
            }else{
                return base.Equals(obj);
            }
        }

        public string DescriptorValue()
        {
            return this.SKU.ToString();
        }

        public string DescriptorColumn() {
            return "SKU";
        }
        
        public static string GetDescriptorColumn()
        {
            return "SKU";
        }
        
        #region ' Foreign Keys '
        IList<Product> _Products;
        public IList<Product> Products
        {
            get
            {
                
                  if(_Products==null){
                      var repo=Product.GetRepo();
                  
                       _Products = (from items in repo.GetAll()
                           where items.SKU == _SKU
                           select items).ToList();
                   }
                  return _Products;
            }
            set{
                _Products=value;
            }
        }

        #endregion
        

        int _DescriptorID;
        public int DescriptorID
        {
            get { return _DescriptorID; }
            set
            {
                
                _DescriptorID=value;
                var col=tbl.Columns.SingleOrDefault(x=>x.Name=="DescriptorID");
                if(col!=null){
                    if(!_dirtyColumns.Contains(col) && _isLoaded){
                        _dirtyColumns.Add(col);
                    }
                }
                OnChanged();
            }
        }

        string _SKU;
        public string SKU
        {
            get { return _SKU; }
            set
            {
                
                _SKU=value;
                var col=tbl.Columns.SingleOrDefault(x=>x.Name=="SKU");
                if(col!=null){
                    if(!_dirtyColumns.Contains(col) && _isLoaded){
                        _dirtyColumns.Add(col);
                    }
                }
                OnChanged();
            }
        }

        string _LanguageCode;
        public string LanguageCode
        {
            get { return _LanguageCode; }
            set
            {
                
                _LanguageCode=value;
                var col=tbl.Columns.SingleOrDefault(x=>x.Name=="LanguageCode");
                if(col!=null){
                    if(!_dirtyColumns.Contains(col) && _isLoaded){
                        _dirtyColumns.Add(col);
                    }
                }
                OnChanged();
            }
        }

        string _Title;
        public string Title
        {
            get { return _Title; }
            set
            {
                
                _Title=value;
                var col=tbl.Columns.SingleOrDefault(x=>x.Name=="Title");
                if(col!=null){
                    if(!_dirtyColumns.Contains(col) && _isLoaded){
                        _dirtyColumns.Add(col);
                    }
                }
                OnChanged();
            }
        }

        bool _IsDefault;
        public bool IsDefault
        {
            get { return _IsDefault; }
            set
            {
                
                _IsDefault=value;
                var col=tbl.Columns.SingleOrDefault(x=>x.Name=="IsDefault");
                if(col!=null){
                    if(!_dirtyColumns.Contains(col) && _isLoaded){
                        _dirtyColumns.Add(col);
                    }
                }
                OnChanged();
            }
        }

        string _Body;
        public string Body
        {
            get { return _Body; }
            set
            {
                
                _Body=value;
                var col=tbl.Columns.SingleOrDefault(x=>x.Name=="Body");
                if(col!=null){
                    if(!_dirtyColumns.Contains(col) && _isLoaded){
                        _dirtyColumns.Add(col);
                    }
                }
                OnChanged();
            }
        }



        public DbCommand GetUpdateCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return _repo.BuildUpdateQuery(this).GetCommand().ToDbCommand();
            
        }
        public DbCommand GetInsertCommand() {
 
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else{
            
            
                return _repo.BuildInsertQuery(this).GetCommand().ToDbCommand();
                
            }
        }
        
        public DbCommand GetDeleteCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return _repo.BuildDeleteQuery(this).GetCommand().ToDbCommand();
        }
        //persistence
        public void Save(){
            Save("");
        }
        
        public void Update(string userName){
            _repo.Update(this);
            OnSaved();
       }
        
        public void Add(string userName){
            
            
            this.SetKeyValue(_repo.Add(this));
            OnSaved();
        }
        
        public void Save(string userName) {
            
           
            if (_isNew) {
                Add(userName);
                
            } else {
                Update(userName);
            }
            
        }

        public void Delete() {
                   
                 
            _repo.Delete(KeyValue());
            
                    }

        public static void Delete(object key) {
        
            var repo = new SubSonicRepository<ProductDescriptor>(new Kona.Data.KonaDB());

                   
                 
            repo.Delete(key);
            
                        
        }

        public static void Delete(Func<ProductDescriptor, bool> expression) {
            var repo = GetRepo();
            
                   
            
            repo.Delete(expression);
            
                    }

        

        public void Load(IDataReader rdr) {
            Load(rdr, true);
        }
        public void Load(IDataReader rdr, bool closeReader) {
            if (rdr.Read()) {

                try {
                    rdr.Load(this);
                    _isNew = false;
                    _isLoaded = true;
                } catch {
                    _isLoaded = false;
                    throw;
                }
            }else{
                _isLoaded = false;
            }

            if (closeReader)
                rdr.Dispose();
        }
        

    } 
    
    
    /// <summary>
    /// A class which represents the InventoryRecords table in the Kona Database.
    /// </summary>
    public partial class InventoryRecord: IActiveRecord
    {
    
        #region Built-in testing
        static IList<InventoryRecord> TestItems;
        static TestRepository<InventoryRecord> _testRepo;
        
        static void SetTestRepo(){
            _testRepo = _testRepo ?? new TestRepository<InventoryRecord>(new Kona.Data.KonaDB());
        }
        public static void ResetTestRepo(){
            _testRepo = null;
            SetTestRepo();
        }
        public static void Setup(List<InventoryRecord> testlist){
            SetTestRepo();
            _testRepo._items = testlist;
        }
        public static void Setup(InventoryRecord item) {
            SetTestRepo();
            _testRepo._items.Add(item);
        }
        public static void Setup(int testItems) {
            SetTestRepo();
            for(int i=0;i<testItems;i++){
                InventoryRecord item=new InventoryRecord();
                _testRepo._items.Add(item);
            }
        }
        
        public bool TestMode {
            get {
                return this._db.DataProvider.ConnectionString.Equals("test", StringComparison.InvariantCultureIgnoreCase);
            }
        }

        #endregion

        IRepository<InventoryRecord> _repo;
        ITable tbl;
        bool _isNew;
        public bool IsNew(){
            return _isNew;
        }
        public void SetIsNew(bool isNew){
            _isNew=isNew;
        }
        public void SetIsLoaded(bool isLoaded){
            _isLoaded=isLoaded;
        }
        bool _isLoaded;
        public bool IsLoaded(){
            return _isLoaded;
        }
                
        List<IColumn> _dirtyColumns;
        public bool IsDirty(){
            return _dirtyColumns.Count>0;
        }
        
        public List<IColumn> GetDirtyColumns (){
            return _dirtyColumns;
        }

        Kona.Data.KonaDB _db;
        public InventoryRecord() {
             _db=new Kona.Data.KonaDB();
            _dirtyColumns=new List<IColumn>();
            
            if(TestMode){
                InventoryRecord.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<InventoryRecord>(_db);
            }
            tbl=_repo.GetTable();
            _isNew = true;
            OnCreated();
        }
        
       
        partial void OnCreated();
            
        partial void OnLoaded();
        
        partial void OnSaved();
        
        partial void OnChanged();
        
        public IList<IColumn> Columns{
            get{
                return tbl.Columns;
            }
        }
        
        public InventoryRecord(object key):this() {
            _isLoaded=_repo.Load(this,this.KeyName(),key);
            if(_isLoaded)
                OnLoaded();

        }

        public InventoryRecord(Expression<Func<InventoryRecord, bool>> expression):this() {
            _isLoaded=_repo.Load(this,expression);
            if(_isLoaded)
                OnLoaded();
        }
        
        public static SubSonic.Query.SqlQuery Select{
            get{
                var db=new Kona.Data.KonaDB();
                return db.Select.From<InventoryRecord>();
            }
        }
        public static SubSonic.Query.SqlQuery SelectColumns(params string[] columns){
            var db=new Kona.Data.KonaDB();
            return new SubSonic.Query.Select(db.DataProvider, columns).From<InventoryRecord>();
        }
        internal static IRepository<InventoryRecord> GetRepo(){
            var db=new Kona.Data.KonaDB();
            IRepository<InventoryRecord> _repo;
            
            if(db.TestMode){
                InventoryRecord.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<InventoryRecord>(db);
            }
            return _repo;
        }
        
        public static InventoryRecord SingleOrDefault(Expression<Func<InventoryRecord, bool>> expression) {
           
            var single= All().SingleOrDefault(expression);
            if (single != null){
                single.OnLoaded();
                single.SetIsLoaded(true);
                single.SetIsNew(false);
            }
            return single;
        }      
        public static bool Exists(Expression<Func<InventoryRecord, bool>> expression) {
           
            return All().Any(expression);
        }        
        public static IList<InventoryRecord> Find(Expression<Func<InventoryRecord, bool>> expression) {
           
            return GetRepo().Find(expression).ToList();
        }
        public static IQueryable<InventoryRecord> All() {
            return GetRepo().GetAll();
        }
        public static PagedList<InventoryRecord> GetPaged<TKey>(Func<InventoryRecord,TKey> orderby, int pageIndex, int pageSize){
            return GetRepo().GetPaged(orderby, pageIndex, pageSize);
        }
        public static PagedList<InventoryRecord> GetPaged<TKey>(string sortBy, int pageIndex, int pageSize) {
            return GetRepo().GetPaged(sortBy, pageIndex, pageSize);
        }
        public static PagedList<InventoryRecord> GetPaged<TKey>(int pageIndex, int pageSize) {
            return GetRepo().GetPaged(pageIndex, pageSize);
            
        }

        public string KeyName()
        {
            return "InventoryRecordID";
        }

        public object KeyValue()
        {
            return this.InventoryRecordID;
        }
        
        public void SetKeyValue(object value) {
            if (value != DBNull.Value) {
                var settable = value.ChangeTypeTo<int>();
                this.GetType().GetProperty(this.KeyName()).SetValue(this, settable, null);
            }
        }
        
        public override string ToString(){
            return this.SKU.ToString();
        }

        public override bool Equals(object obj){
            if(obj.GetType()==typeof(InventoryRecord)){
                InventoryRecord compare=(InventoryRecord)obj;
                int thisPk=(int)this.KeyValue();
                int comparePk=(int)compare.KeyValue();
                return thisPk.Equals(comparePk);
            }else{
                return base.Equals(obj);
            }
        }

        public string DescriptorValue()
        {
            return this.SKU.ToString();
        }

        public string DescriptorColumn() {
            return "SKU";
        }
        
        public static string GetDescriptorColumn()
        {
            return "SKU";
        }
        
        #region ' Foreign Keys '
        IList<Product> _Products;
        public IList<Product> Products
        {
            get
            {
                
                  if(_Products==null){
                      var repo=Product.GetRepo();
                  
                       _Products = (from items in repo.GetAll()
                           where items.SKU == _SKU
                           select items).ToList();
                   }
                  return _Products;
            }
            set{
                _Products=value;
            }
        }

        #endregion
        

        int _InventoryRecordID;
        public int InventoryRecordID
        {
            get { return _InventoryRecordID; }
            set
            {
                
                _InventoryRecordID=value;
                var col=tbl.Columns.SingleOrDefault(x=>x.Name=="InventoryRecordID");
                if(col!=null){
                    if(!_dirtyColumns.Contains(col) && _isLoaded){
                        _dirtyColumns.Add(col);
                    }
                }
                OnChanged();
            }
        }

        string _SKU;
        public string SKU
        {
            get { return _SKU; }
            set
            {
                
                _SKU=value;
                var col=tbl.Columns.SingleOrDefault(x=>x.Name=="SKU");
                if(col!=null){
                    if(!_dirtyColumns.Contains(col) && _isLoaded){
                        _dirtyColumns.Add(col);
                    }
                }
                OnChanged();
            }
        }

        int _Increment;
        public int Increment
        {
            get { return _Increment; }
            set
            {
                
                _Increment=value;
                var col=tbl.Columns.SingleOrDefault(x=>x.Name=="Increment");
                if(col!=null){
                    if(!_dirtyColumns.Contains(col) && _isLoaded){
                        _dirtyColumns.Add(col);
                    }
                }
                OnChanged();
            }
        }

        DateTime _DateEntered;
        public DateTime DateEntered
        {
            get { return _DateEntered; }
            set
            {
                
                _DateEntered=value;
                var col=tbl.Columns.SingleOrDefault(x=>x.Name=="DateEntered");
                if(col!=null){
                    if(!_dirtyColumns.Contains(col) && _isLoaded){
                        _dirtyColumns.Add(col);
                    }
                }
                OnChanged();
            }
        }

        string _Notes;
        public string Notes
        {
            get { return _Notes; }
            set
            {
                
                _Notes=value;
                var col=tbl.Columns.SingleOrDefault(x=>x.Name=="Notes");
                if(col!=null){
                    if(!_dirtyColumns.Contains(col) && _isLoaded){
                        _dirtyColumns.Add(col);
                    }
                }
                OnChanged();
            }
        }



        public DbCommand GetUpdateCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return _repo.BuildUpdateQuery(this).GetCommand().ToDbCommand();
            
        }
        public DbCommand GetInsertCommand() {
 
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else{
            
            
                return _repo.BuildInsertQuery(this).GetCommand().ToDbCommand();
                
            }
        }
        
        public DbCommand GetDeleteCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return _repo.BuildDeleteQuery(this).GetCommand().ToDbCommand();
        }
        //persistence
        public void Save(){
            Save("");
        }
        
        public void Update(string userName){
            _repo.Update(this);
            OnSaved();
       }
        
        public void Add(string userName){
            
            
            this.SetKeyValue(_repo.Add(this));
            OnSaved();
        }
        
        public void Save(string userName) {
            
           
            if (_isNew) {
                Add(userName);
                
            } else {
                Update(userName);
            }
            
        }

        public void Delete() {
                   
                 
            _repo.Delete(KeyValue());
            
                    }

        public static void Delete(object key) {
        
            var repo = new SubSonicRepository<InventoryRecord>(new Kona.Data.KonaDB());

                   
                 
            repo.Delete(key);
            
                        
        }

        public static void Delete(Func<InventoryRecord, bool> expression) {
            var repo = GetRepo();
            
                   
            
            repo.Delete(expression);
            
                    }

        

        public void Load(IDataReader rdr) {
            Load(rdr, true);
        }
        public void Load(IDataReader rdr, bool closeReader) {
            if (rdr.Read()) {

                try {
                    rdr.Load(this);
                    _isNew = false;
                    _isLoaded = true;
                } catch {
                    _isLoaded = false;
                    throw;
                }
            }else{
                _isLoaded = false;
            }

            if (closeReader)
                rdr.Dispose();
        }
        

    } 
    
    
    /// <summary>
    /// A class which represents the Transactions table in the Kona Database.
    /// </summary>
    public partial class Transaction: IActiveRecord
    {
    
        #region Built-in testing
        static IList<Transaction> TestItems;
        static TestRepository<Transaction> _testRepo;
        
        static void SetTestRepo(){
            _testRepo = _testRepo ?? new TestRepository<Transaction>(new Kona.Data.KonaDB());
        }
        public static void ResetTestRepo(){
            _testRepo = null;
            SetTestRepo();
        }
        public static void Setup(List<Transaction> testlist){
            SetTestRepo();
            _testRepo._items = testlist;
        }
        public static void Setup(Transaction item) {
            SetTestRepo();
            _testRepo._items.Add(item);
        }
        public static void Setup(int testItems) {
            SetTestRepo();
            for(int i=0;i<testItems;i++){
                Transaction item=new Transaction();
                _testRepo._items.Add(item);
            }
        }
        
        public bool TestMode {
            get {
                return this._db.DataProvider.ConnectionString.Equals("test", StringComparison.InvariantCultureIgnoreCase);
            }
        }

        #endregion

        IRepository<Transaction> _repo;
        ITable tbl;
        bool _isNew;
        public bool IsNew(){
            return _isNew;
        }
        public void SetIsNew(bool isNew){
            _isNew=isNew;
        }
        public void SetIsLoaded(bool isLoaded){
            _isLoaded=isLoaded;
        }
        bool _isLoaded;
        public bool IsLoaded(){
            return _isLoaded;
        }
                
        List<IColumn> _dirtyColumns;
        public bool IsDirty(){
            return _dirtyColumns.Count>0;
        }
        
        public List<IColumn> GetDirtyColumns (){
            return _dirtyColumns;
        }

        Kona.Data.KonaDB _db;
        public Transaction() {
             _db=new Kona.Data.KonaDB();
            _dirtyColumns=new List<IColumn>();
            
            if(TestMode){
                Transaction.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<Transaction>(_db);
            }
            tbl=_repo.GetTable();
            _isNew = true;
            OnCreated();
        }
        
       
        partial void OnCreated();
            
        partial void OnLoaded();
        
        partial void OnSaved();
        
        partial void OnChanged();
        
        public IList<IColumn> Columns{
            get{
                return tbl.Columns;
            }
        }
        
        public Transaction(object key):this() {
            _isLoaded=_repo.Load(this,this.KeyName(),key);
            if(_isLoaded)
                OnLoaded();

        }

        public Transaction(Expression<Func<Transaction, bool>> expression):this() {
            _isLoaded=_repo.Load(this,expression);
            if(_isLoaded)
                OnLoaded();
        }
        
        public static SubSonic.Query.SqlQuery Select{
            get{
                var db=new Kona.Data.KonaDB();
                return db.Select.From<Transaction>();
            }
        }
        public static SubSonic.Query.SqlQuery SelectColumns(params string[] columns){
            var db=new Kona.Data.KonaDB();
            return new SubSonic.Query.Select(db.DataProvider, columns).From<Transaction>();
        }
        internal static IRepository<Transaction> GetRepo(){
            var db=new Kona.Data.KonaDB();
            IRepository<Transaction> _repo;
            
            if(db.TestMode){
                Transaction.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<Transaction>(db);
            }
            return _repo;
        }
        
        public static Transaction SingleOrDefault(Expression<Func<Transaction, bool>> expression) {
           
            var single= All().SingleOrDefault(expression);
            if (single != null){
                single.OnLoaded();
                single.SetIsLoaded(true);
                single.SetIsNew(false);
            }
            return single;
        }      
        public static bool Exists(Expression<Func<Transaction, bool>> expression) {
           
            return All().Any(expression);
        }        
        public static IList<Transaction> Find(Expression<Func<Transaction, bool>> expression) {
           
            return GetRepo().Find(expression).ToList();
        }
        public static IQueryable<Transaction> All() {
            return GetRepo().GetAll();
        }
        public static PagedList<Transaction> GetPaged<TKey>(Func<Transaction,TKey> orderby, int pageIndex, int pageSize){
            return GetRepo().GetPaged(orderby, pageIndex, pageSize);
        }
        public static PagedList<Transaction> GetPaged<TKey>(string sortBy, int pageIndex, int pageSize) {
            return GetRepo().GetPaged(sortBy, pageIndex, pageSize);
        }
        public static PagedList<Transaction> GetPaged<TKey>(int pageIndex, int pageSize) {
            return GetRepo().GetPaged(pageIndex, pageSize);
            
        }

        public string KeyName()
        {
            return "TransactionID";
        }

        public object KeyValue()
        {
            return this.TransactionID;
        }
        
        public void SetKeyValue(object value) {
            if (value != DBNull.Value) {
                var settable = value.ChangeTypeTo<Guid>();
                this.GetType().GetProperty(this.KeyName()).SetValue(this, settable, null);
            }
        }
        
        public override string ToString(){
            return this.AuthorizationCode.ToString();
        }

        public override bool Equals(object obj){
            if(obj.GetType()==typeof(Transaction)){
                Transaction compare=(Transaction)obj;
                Guid thisPk=(Guid)this.KeyValue();
                Guid comparePk=(Guid)compare.KeyValue();
                return thisPk.Equals(comparePk);
            }else{
                return base.Equals(obj);
            }
        }

        public string DescriptorValue()
        {
            return this.AuthorizationCode.ToString();
        }

        public string DescriptorColumn() {
            return "AuthorizationCode";
        }
        
        public static string GetDescriptorColumn()
        {
            return "AuthorizationCode";
        }
        
        #region ' Foreign Keys '
        IList<Order> _Orders;
        public IList<Order> Orders
        {
            get
            {
                
                  if(_Orders==null){
                      var repo=Order.GetRepo();
                  
                       _Orders = (from items in repo.GetAll()
                           where items.OrderID == _OrderID
                           select items).ToList();
                   }
                  return _Orders;
            }
            set{
                _Orders=value;
            }
        }

        #endregion
        

        Guid _TransactionID;
        public Guid TransactionID
        {
            get { return _TransactionID; }
            set
            {
                
                _TransactionID=value;
                var col=tbl.Columns.SingleOrDefault(x=>x.Name=="TransactionID");
                if(col!=null){
                    if(!_dirtyColumns.Contains(col) && _isLoaded){
                        _dirtyColumns.Add(col);
                    }
                }
                OnChanged();
            }
        }

        Guid _OrderID;
        public Guid OrderID
        {
            get { return _OrderID; }
            set
            {
                
                _OrderID=value;
                var col=tbl.Columns.SingleOrDefault(x=>x.Name=="OrderID");
                if(col!=null){
                    if(!_dirtyColumns.Contains(col) && _isLoaded){
                        _dirtyColumns.Add(col);
                    }
                }
                OnChanged();
            }
        }

        DateTime _TransactionDate;
        public DateTime TransactionDate
        {
            get { return _TransactionDate; }
            set
            {
                
                _TransactionDate=value;
                var col=tbl.Columns.SingleOrDefault(x=>x.Name=="TransactionDate");
                if(col!=null){
                    if(!_dirtyColumns.Contains(col) && _isLoaded){
                        _dirtyColumns.Add(col);
                    }
                }
                OnChanged();
            }
        }

        decimal _Amount;
        public decimal Amount
        {
            get { return _Amount; }
            set
            {
                
                _Amount=value;
                var col=tbl.Columns.SingleOrDefault(x=>x.Name=="Amount");
                if(col!=null){
                    if(!_dirtyColumns.Contains(col) && _isLoaded){
                        _dirtyColumns.Add(col);
                    }
                }
                OnChanged();
            }
        }

        string _AuthorizationCode;
        public string AuthorizationCode
        {
            get { return _AuthorizationCode; }
            set
            {
                
                _AuthorizationCode=value;
                var col=tbl.Columns.SingleOrDefault(x=>x.Name=="AuthorizationCode");
                if(col!=null){
                    if(!_dirtyColumns.Contains(col) && _isLoaded){
                        _dirtyColumns.Add(col);
                    }
                }
                OnChanged();
            }
        }

        string _Notes;
        public string Notes
        {
            get { return _Notes; }
            set
            {
                
                _Notes=value;
                var col=tbl.Columns.SingleOrDefault(x=>x.Name=="Notes");
                if(col!=null){
                    if(!_dirtyColumns.Contains(col) && _isLoaded){
                        _dirtyColumns.Add(col);
                    }
                }
                OnChanged();
            }
        }

        string _Processor;
        public string Processor
        {
            get { return _Processor; }
            set
            {
                
                _Processor=value;
                var col=tbl.Columns.SingleOrDefault(x=>x.Name=="Processor");
                if(col!=null){
                    if(!_dirtyColumns.Contains(col) && _isLoaded){
                        _dirtyColumns.Add(col);
                    }
                }
                OnChanged();
            }
        }



        public DbCommand GetUpdateCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return _repo.BuildUpdateQuery(this).GetCommand().ToDbCommand();
            
        }
        public DbCommand GetInsertCommand() {
 
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else{
            
            
                return _repo.BuildInsertQuery(this).GetCommand().ToDbCommand();
                
            }
        }
        
        public DbCommand GetDeleteCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return _repo.BuildDeleteQuery(this).GetCommand().ToDbCommand();
        }
        //persistence
        public void Save(){
            Save("");
        }
        
        public void Update(string userName){
            _repo.Update(this);
            OnSaved();
       }
        
        public void Add(string userName){
            
            _repo.Add(this);
            OnSaved();
        }
        
        public void Save(string userName) {
            
           
            if (_isNew) {
                Add(userName);
                
            } else {
                Update(userName);
            }
            
        }

        public void Delete() {
                   
                 
            _repo.Delete(KeyValue());
            
                    }

        public static void Delete(object key) {
        
            var repo = new SubSonicRepository<Transaction>(new Kona.Data.KonaDB());

                   
                 
            repo.Delete(key);
            
                        
        }

        public static void Delete(Func<Transaction, bool> expression) {
            var repo = GetRepo();
            
                   
            
            repo.Delete(expression);
            
                    }

        

        public void Load(IDataReader rdr) {
            Load(rdr, true);
        }
        public void Load(IDataReader rdr, bool closeReader) {
            if (rdr.Read()) {

                try {
                    rdr.Load(this);
                    _isNew = false;
                    _isLoaded = true;
                } catch {
                    _isLoaded = false;
                    throw;
                }
            }else{
                _isLoaded = false;
            }

            if (closeReader)
                rdr.Dispose();
        }
        

    } 
    
    
    /// <summary>
    /// A class which represents the CustomerEvents table in the Kona Database.
    /// </summary>
    public partial class CustomerEvent: IActiveRecord
    {
    
        #region Built-in testing
        static IList<CustomerEvent> TestItems;
        static TestRepository<CustomerEvent> _testRepo;
        
        static void SetTestRepo(){
            _testRepo = _testRepo ?? new TestRepository<CustomerEvent>(new Kona.Data.KonaDB());
        }
        public static void ResetTestRepo(){
            _testRepo = null;
            SetTestRepo();
        }
        public static void Setup(List<CustomerEvent> testlist){
            SetTestRepo();
            _testRepo._items = testlist;
        }
        public static void Setup(CustomerEvent item) {
            SetTestRepo();
            _testRepo._items.Add(item);
        }
        public static void Setup(int testItems) {
            SetTestRepo();
            for(int i=0;i<testItems;i++){
                CustomerEvent item=new CustomerEvent();
                _testRepo._items.Add(item);
            }
        }
        
        public bool TestMode {
            get {
                return this._db.DataProvider.ConnectionString.Equals("test", StringComparison.InvariantCultureIgnoreCase);
            }
        }

        #endregion

        IRepository<CustomerEvent> _repo;
        ITable tbl;
        bool _isNew;
        public bool IsNew(){
            return _isNew;
        }
        public void SetIsNew(bool isNew){
            _isNew=isNew;
        }
        public void SetIsLoaded(bool isLoaded){
            _isLoaded=isLoaded;
        }
        bool _isLoaded;
        public bool IsLoaded(){
            return _isLoaded;
        }
                
        List<IColumn> _dirtyColumns;
        public bool IsDirty(){
            return _dirtyColumns.Count>0;
        }
        
        public List<IColumn> GetDirtyColumns (){
            return _dirtyColumns;
        }

        Kona.Data.KonaDB _db;
        public CustomerEvent() {
             _db=new Kona.Data.KonaDB();
            _dirtyColumns=new List<IColumn>();
            
            if(TestMode){
                CustomerEvent.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<CustomerEvent>(_db);
            }
            tbl=_repo.GetTable();
            _isNew = true;
            OnCreated();
        }
        
       
        partial void OnCreated();
            
        partial void OnLoaded();
        
        partial void OnSaved();
        
        partial void OnChanged();
        
        public IList<IColumn> Columns{
            get{
                return tbl.Columns;
            }
        }
        
        public CustomerEvent(object key):this() {
            _isLoaded=_repo.Load(this,this.KeyName(),key);
            if(_isLoaded)
                OnLoaded();

        }

        public CustomerEvent(Expression<Func<CustomerEvent, bool>> expression):this() {
            _isLoaded=_repo.Load(this,expression);
            if(_isLoaded)
                OnLoaded();
        }
        
        public static SubSonic.Query.SqlQuery Select{
            get{
                var db=new Kona.Data.KonaDB();
                return db.Select.From<CustomerEvent>();
            }
        }
        public static SubSonic.Query.SqlQuery SelectColumns(params string[] columns){
            var db=new Kona.Data.KonaDB();
            return new SubSonic.Query.Select(db.DataProvider, columns).From<CustomerEvent>();
        }
        internal static IRepository<CustomerEvent> GetRepo(){
            var db=new Kona.Data.KonaDB();
            IRepository<CustomerEvent> _repo;
            
            if(db.TestMode){
                CustomerEvent.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<CustomerEvent>(db);
            }
            return _repo;
        }
        
        public static CustomerEvent SingleOrDefault(Expression<Func<CustomerEvent, bool>> expression) {
           
            var single= All().SingleOrDefault(expression);
            if (single != null){
                single.OnLoaded();
                single.SetIsLoaded(true);
                single.SetIsNew(false);
            }
            return single;
        }      
        public static bool Exists(Expression<Func<CustomerEvent, bool>> expression) {
           
            return All().Any(expression);
        }        
        public static IList<CustomerEvent> Find(Expression<Func<CustomerEvent, bool>> expression) {
           
            return GetRepo().Find(expression).ToList();
        }
        public static IQueryable<CustomerEvent> All() {
            return GetRepo().GetAll();
        }
        public static PagedList<CustomerEvent> GetPaged<TKey>(Func<CustomerEvent,TKey> orderby, int pageIndex, int pageSize){
            return GetRepo().GetPaged(orderby, pageIndex, pageSize);
        }
        public static PagedList<CustomerEvent> GetPaged<TKey>(string sortBy, int pageIndex, int pageSize) {
            return GetRepo().GetPaged(sortBy, pageIndex, pageSize);
        }
        public static PagedList<CustomerEvent> GetPaged<TKey>(int pageIndex, int pageSize) {
            return GetRepo().GetPaged(pageIndex, pageSize);
            
        }

        public string KeyName()
        {
            return "EventID";
        }

        public object KeyValue()
        {
            return this.EventID;
        }
        
        public void SetKeyValue(object value) {
            if (value != DBNull.Value) {
                var settable = value.ChangeTypeTo<int>();
                this.GetType().GetProperty(this.KeyName()).SetValue(this, settable, null);
            }
        }
        
        public override string ToString(){
            return this.UserName.ToString();
        }

        public override bool Equals(object obj){
            if(obj.GetType()==typeof(CustomerEvent)){
                CustomerEvent compare=(CustomerEvent)obj;
                int thisPk=(int)this.KeyValue();
                int comparePk=(int)compare.KeyValue();
                return thisPk.Equals(comparePk);
            }else{
                return base.Equals(obj);
            }
        }

        public string DescriptorValue()
        {
            return this.UserName.ToString();
        }

        public string DescriptorColumn() {
            return "UserName";
        }
        
        public static string GetDescriptorColumn()
        {
            return "UserName";
        }
        
        #region ' Foreign Keys '
        IList<Customer> _Customers;
        public IList<Customer> Customers
        {
            get
            {
                
                  if(_Customers==null){
                      var repo=Customer.GetRepo();
                  
                       _Customers = (from items in repo.GetAll()
                           where items.UserName == _UserName
                           select items).ToList();
                   }
                  return _Customers;
            }
            set{
                _Customers=value;
            }
        }

        IList<CustomerBehavior> _CustomerBehaviors;
        public IList<CustomerBehavior> CustomerBehaviors
        {
            get
            {
                
                  if(_CustomerBehaviors==null){
                      var repo=CustomerBehavior.GetRepo();
                  
                       _CustomerBehaviors = (from items in repo.GetAll()
                           where items.UserBehaviorID == _UserBehaviorID
                           select items).ToList();
                   }
                  return _CustomerBehaviors;
            }
            set{
                _CustomerBehaviors=value;
            }
        }

        #endregion
        

        int _EventID;
        public int EventID
        {
            get { return _EventID; }
            set
            {
                
                _EventID=value;
                var col=tbl.Columns.SingleOrDefault(x=>x.Name=="EventID");
                if(col!=null){
                    if(!_dirtyColumns.Contains(col) && _isLoaded){
                        _dirtyColumns.Add(col);
                    }
                }
                OnChanged();
            }
        }

        int _UserBehaviorID;
        public int UserBehaviorID
        {
            get { return _UserBehaviorID; }
            set
            {
                
                _UserBehaviorID=value;
                var col=tbl.Columns.SingleOrDefault(x=>x.Name=="UserBehaviorID");
                if(col!=null){
                    if(!_dirtyColumns.Contains(col) && _isLoaded){
                        _dirtyColumns.Add(col);
                    }
                }
                OnChanged();
            }
        }

        string _UserName;
        public string UserName
        {
            get { return _UserName; }
            set
            {
                
                _UserName=value;
                var col=tbl.Columns.SingleOrDefault(x=>x.Name=="UserName");
                if(col!=null){
                    if(!_dirtyColumns.Contains(col) && _isLoaded){
                        _dirtyColumns.Add(col);
                    }
                }
                OnChanged();
            }
        }

        DateTime _EventDate;
        public DateTime EventDate
        {
            get { return _EventDate; }
            set
            {
                
                _EventDate=value;
                var col=tbl.Columns.SingleOrDefault(x=>x.Name=="EventDate");
                if(col!=null){
                    if(!_dirtyColumns.Contains(col) && _isLoaded){
                        _dirtyColumns.Add(col);
                    }
                }
                OnChanged();
            }
        }

        string _IP;
        public string IP
        {
            get { return _IP; }
            set
            {
                
                _IP=value;
                var col=tbl.Columns.SingleOrDefault(x=>x.Name=="IP");
                if(col!=null){
                    if(!_dirtyColumns.Contains(col) && _isLoaded){
                        _dirtyColumns.Add(col);
                    }
                }
                OnChanged();
            }
        }

        string _SKU;
        public string SKU
        {
            get { return _SKU; }
            set
            {
                
                _SKU=value;
                var col=tbl.Columns.SingleOrDefault(x=>x.Name=="SKU");
                if(col!=null){
                    if(!_dirtyColumns.Contains(col) && _isLoaded){
                        _dirtyColumns.Add(col);
                    }
                }
                OnChanged();
            }
        }

        int? _CategoryID;
        public int? CategoryID
        {
            get { return _CategoryID; }
            set
            {
                
                _CategoryID=value;
                var col=tbl.Columns.SingleOrDefault(x=>x.Name=="CategoryID");
                if(col!=null){
                    if(!_dirtyColumns.Contains(col) && _isLoaded){
                        _dirtyColumns.Add(col);
                    }
                }
                OnChanged();
            }
        }

        Guid? _OrderID;
        public Guid? OrderID
        {
            get { return _OrderID; }
            set
            {
                
                _OrderID=value;
                var col=tbl.Columns.SingleOrDefault(x=>x.Name=="OrderID");
                if(col!=null){
                    if(!_dirtyColumns.Contains(col) && _isLoaded){
                        _dirtyColumns.Add(col);
                    }
                }
                OnChanged();
            }
        }



        public DbCommand GetUpdateCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return _repo.BuildUpdateQuery(this).GetCommand().ToDbCommand();
            
        }
        public DbCommand GetInsertCommand() {
 
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else{
            
            
                return _repo.BuildInsertQuery(this).GetCommand().ToDbCommand();
                
            }
        }
        
        public DbCommand GetDeleteCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return _repo.BuildDeleteQuery(this).GetCommand().ToDbCommand();
        }
        //persistence
        public void Save(){
            Save("");
        }
        
        public void Update(string userName){
            _repo.Update(this);
            OnSaved();
       }
        
        public void Add(string userName){
            
            
            this.SetKeyValue(_repo.Add(this));
            OnSaved();
        }
        
        public void Save(string userName) {
            
           
            if (_isNew) {
                Add(userName);
                
            } else {
                Update(userName);
            }
            
        }

        public void Delete() {
                   
                 
            _repo.Delete(KeyValue());
            
                    }

        public static void Delete(object key) {
        
            var repo = new SubSonicRepository<CustomerEvent>(new Kona.Data.KonaDB());

                   
                 
            repo.Delete(key);
            
                        
        }

        public static void Delete(Func<CustomerEvent, bool> expression) {
            var repo = GetRepo();
            
                   
            
            repo.Delete(expression);
            
                    }

        

        public void Load(IDataReader rdr) {
            Load(rdr, true);
        }
        public void Load(IDataReader rdr, bool closeReader) {
            if (rdr.Read()) {

                try {
                    rdr.Load(this);
                    _isNew = false;
                    _isLoaded = true;
                } catch {
                    _isLoaded = false;
                    throw;
                }
            }else{
                _isLoaded = false;
            }

            if (closeReader)
                rdr.Dispose();
        }
        

    } 
    
    
    /// <summary>
    /// A class which represents the Categories_Widgets table in the Kona Database.
    /// </summary>
    public partial class Categories_Widget: IActiveRecord
    {
    
        #region Built-in testing
        static IList<Categories_Widget> TestItems;
        static TestRepository<Categories_Widget> _testRepo;
        
        static void SetTestRepo(){
            _testRepo = _testRepo ?? new TestRepository<Categories_Widget>(new Kona.Data.KonaDB());
        }
        public static void ResetTestRepo(){
            _testRepo = null;
            SetTestRepo();
        }
        public static void Setup(List<Categories_Widget> testlist){
            SetTestRepo();
            _testRepo._items = testlist;
        }
        public static void Setup(Categories_Widget item) {
            SetTestRepo();
            _testRepo._items.Add(item);
        }
        public static void Setup(int testItems) {
            SetTestRepo();
            for(int i=0;i<testItems;i++){
                Categories_Widget item=new Categories_Widget();
                _testRepo._items.Add(item);
            }
        }
        
        public bool TestMode {
            get {
                return this._db.DataProvider.ConnectionString.Equals("test", StringComparison.InvariantCultureIgnoreCase);
            }
        }

        #endregion

        IRepository<Categories_Widget> _repo;
        ITable tbl;
        bool _isNew;
        public bool IsNew(){
            return _isNew;
        }
        public void SetIsNew(bool isNew){
            _isNew=isNew;
        }
        public void SetIsLoaded(bool isLoaded){
            _isLoaded=isLoaded;
        }
        bool _isLoaded;
        public bool IsLoaded(){
            return _isLoaded;
        }
                
        List<IColumn> _dirtyColumns;
        public bool IsDirty(){
            return _dirtyColumns.Count>0;
        }
        
        public List<IColumn> GetDirtyColumns (){
            return _dirtyColumns;
        }

        Kona.Data.KonaDB _db;
        public Categories_Widget() {
             _db=new Kona.Data.KonaDB();
            _dirtyColumns=new List<IColumn>();
            
            if(TestMode){
                Categories_Widget.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<Categories_Widget>(_db);
            }
            tbl=_repo.GetTable();
            _isNew = true;
            OnCreated();
        }
        
       
        partial void OnCreated();
            
        partial void OnLoaded();
        
        partial void OnSaved();
        
        partial void OnChanged();
        
        public IList<IColumn> Columns{
            get{
                return tbl.Columns;
            }
        }
        
        public Categories_Widget(object key):this() {
            _isLoaded=_repo.Load(this,this.KeyName(),key);
            if(_isLoaded)
                OnLoaded();

        }

        public Categories_Widget(Expression<Func<Categories_Widget, bool>> expression):this() {
            _isLoaded=_repo.Load(this,expression);
            if(_isLoaded)
                OnLoaded();
        }
        
        public static SubSonic.Query.SqlQuery Select{
            get{
                var db=new Kona.Data.KonaDB();
                return db.Select.From<Categories_Widget>();
            }
        }
        public static SubSonic.Query.SqlQuery SelectColumns(params string[] columns){
            var db=new Kona.Data.KonaDB();
            return new SubSonic.Query.Select(db.DataProvider, columns).From<Categories_Widget>();
        }
        internal static IRepository<Categories_Widget> GetRepo(){
            var db=new Kona.Data.KonaDB();
            IRepository<Categories_Widget> _repo;
            
            if(db.TestMode){
                Categories_Widget.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<Categories_Widget>(db);
            }
            return _repo;
        }
        
        public static Categories_Widget SingleOrDefault(Expression<Func<Categories_Widget, bool>> expression) {
           
            var single= All().SingleOrDefault(expression);
            if (single != null){
                single.OnLoaded();
                single.SetIsLoaded(true);
                single.SetIsNew(false);
            }
            return single;
        }      
        public static bool Exists(Expression<Func<Categories_Widget, bool>> expression) {
           
            return All().Any(expression);
        }        
        public static IList<Categories_Widget> Find(Expression<Func<Categories_Widget, bool>> expression) {
           
            return GetRepo().Find(expression).ToList();
        }
        public static IQueryable<Categories_Widget> All() {
            return GetRepo().GetAll();
        }
        public static PagedList<Categories_Widget> GetPaged<TKey>(Func<Categories_Widget,TKey> orderby, int pageIndex, int pageSize){
            return GetRepo().GetPaged(orderby, pageIndex, pageSize);
        }
        public static PagedList<Categories_Widget> GetPaged<TKey>(string sortBy, int pageIndex, int pageSize) {
            return GetRepo().GetPaged(sortBy, pageIndex, pageSize);
        }
        public static PagedList<Categories_Widget> GetPaged<TKey>(int pageIndex, int pageSize) {
            return GetRepo().GetPaged(pageIndex, pageSize);
            
        }

        public string KeyName()
        {
            return "CategoryID";
        }

        public object KeyValue()
        {
            return this.CategoryID;
        }
        
        public void SetKeyValue(object value) {
            if (value != DBNull.Value) {
                var settable = value.ChangeTypeTo<int>();
                this.GetType().GetProperty(this.KeyName()).SetValue(this, settable, null);
            }
        }
        
        public override string ToString(){
            return this.WidgetID.ToString();
        }

        public override bool Equals(object obj){
            if(obj.GetType()==typeof(Categories_Widget)){
                Categories_Widget compare=(Categories_Widget)obj;
                int thisPk=(int)this.KeyValue();
                int comparePk=(int)compare.KeyValue();
                return thisPk.Equals(comparePk);
            }else{
                return base.Equals(obj);
            }
        }

        public string DescriptorValue()
        {
            return this.WidgetID.ToString();
        }

        public string DescriptorColumn() {
            return "WidgetID";
        }
        
        public static string GetDescriptorColumn()
        {
            return "WidgetID";
        }
        
        #region ' Foreign Keys '
        IList<Category> _Categories;
        public IList<Category> Categories
        {
            get
            {
                
                  if(_Categories==null){
                      var repo=Category.GetRepo();
                  
                       _Categories = (from items in repo.GetAll()
                           where items.CategoryID == _CategoryID
                           select items).ToList();
                   }
                  return _Categories;
            }
            set{
                _Categories=value;
            }
        }

        IList<Widget> _Widgets;
        public IList<Widget> Widgets
        {
            get
            {
                
                  if(_Widgets==null){
                      var repo=Widget.GetRepo();
                  
                       _Widgets = (from items in repo.GetAll()
                           where items.WidgetID == _WidgetID
                           select items).ToList();
                   }
                  return _Widgets;
            }
            set{
                _Widgets=value;
            }
        }

        #endregion
        

        int _CategoryID;
        public int CategoryID
        {
            get { return _CategoryID; }
            set
            {
                
                _CategoryID=value;
                var col=tbl.Columns.SingleOrDefault(x=>x.Name=="CategoryID");
                if(col!=null){
                    if(!_dirtyColumns.Contains(col) && _isLoaded){
                        _dirtyColumns.Add(col);
                    }
                }
                OnChanged();
            }
        }

        Guid _WidgetID;
        public Guid WidgetID
        {
            get { return _WidgetID; }
            set
            {
                
                _WidgetID=value;
                var col=tbl.Columns.SingleOrDefault(x=>x.Name=="WidgetID");
                if(col!=null){
                    if(!_dirtyColumns.Contains(col) && _isLoaded){
                        _dirtyColumns.Add(col);
                    }
                }
                OnChanged();
            }
        }



        public DbCommand GetUpdateCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return _repo.BuildUpdateQuery(this).GetCommand().ToDbCommand();
            
        }
        public DbCommand GetInsertCommand() {
 
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else{
            
            
                return _repo.BuildInsertQuery(this).GetCommand().ToDbCommand();
                
            }
        }
        
        public DbCommand GetDeleteCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return _repo.BuildDeleteQuery(this).GetCommand().ToDbCommand();
        }
        //persistence
        public void Save(){
            Save("");
        }
        
        public void Update(string userName){
            _repo.Update(this);
            OnSaved();
       }
        
        public void Add(string userName){
            
            _repo.Add(this);
            OnSaved();
        }
        
        public void Save(string userName) {
            
           
            if (_isNew) {
                Add(userName);
                
            } else {
                Update(userName);
            }
            
        }

        public void Delete() {
                   
                 
            _repo.Delete(KeyValue());
            
                    }

        public static void Delete(object key) {
        
            var repo = new SubSonicRepository<Categories_Widget>(new Kona.Data.KonaDB());

                   
                 
            repo.Delete(key);
            
                        
        }

        public static void Delete(Func<Categories_Widget, bool> expression) {
            var repo = GetRepo();
            
                   
            
            repo.Delete(expression);
            
                    }

        

        public void Load(IDataReader rdr) {
            Load(rdr, true);
        }
        public void Load(IDataReader rdr, bool closeReader) {
            if (rdr.Read()) {

                try {
                    rdr.Load(this);
                    _isNew = false;
                    _isLoaded = true;
                } catch {
                    _isLoaded = false;
                    throw;
                }
            }else{
                _isLoaded = false;
            }

            if (closeReader)
                rdr.Dispose();
        }
        

    } 
    
    
    /// <summary>
    /// A class which represents the OrderItems table in the Kona Database.
    /// </summary>
    public partial class OrderItem: IActiveRecord
    {
    
        #region Built-in testing
        static IList<OrderItem> TestItems;
        static TestRepository<OrderItem> _testRepo;
        
        static void SetTestRepo(){
            _testRepo = _testRepo ?? new TestRepository<OrderItem>(new Kona.Data.KonaDB());
        }
        public static void ResetTestRepo(){
            _testRepo = null;
            SetTestRepo();
        }
        public static void Setup(List<OrderItem> testlist){
            SetTestRepo();
            _testRepo._items = testlist;
        }
        public static void Setup(OrderItem item) {
            SetTestRepo();
            _testRepo._items.Add(item);
        }
        public static void Setup(int testItems) {
            SetTestRepo();
            for(int i=0;i<testItems;i++){
                OrderItem item=new OrderItem();
                _testRepo._items.Add(item);
            }
        }
        
        public bool TestMode {
            get {
                return this._db.DataProvider.ConnectionString.Equals("test", StringComparison.InvariantCultureIgnoreCase);
            }
        }

        #endregion

        IRepository<OrderItem> _repo;
        ITable tbl;
        bool _isNew;
        public bool IsNew(){
            return _isNew;
        }
        public void SetIsNew(bool isNew){
            _isNew=isNew;
        }
        public void SetIsLoaded(bool isLoaded){
            _isLoaded=isLoaded;
        }
        bool _isLoaded;
        public bool IsLoaded(){
            return _isLoaded;
        }
                
        List<IColumn> _dirtyColumns;
        public bool IsDirty(){
            return _dirtyColumns.Count>0;
        }
        
        public List<IColumn> GetDirtyColumns (){
            return _dirtyColumns;
        }

        Kona.Data.KonaDB _db;
        public OrderItem() {
             _db=new Kona.Data.KonaDB();
            _dirtyColumns=new List<IColumn>();
            
            if(TestMode){
                OrderItem.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<OrderItem>(_db);
            }
            tbl=_repo.GetTable();
            _isNew = true;
            OnCreated();
        }
        
       
        partial void OnCreated();
            
        partial void OnLoaded();
        
        partial void OnSaved();
        
        partial void OnChanged();
        
        public IList<IColumn> Columns{
            get{
                return tbl.Columns;
            }
        }
        
        public OrderItem(object key):this() {
            _isLoaded=_repo.Load(this,this.KeyName(),key);
            if(_isLoaded)
                OnLoaded();

        }

        public OrderItem(Expression<Func<OrderItem, bool>> expression):this() {
            _isLoaded=_repo.Load(this,expression);
            if(_isLoaded)
                OnLoaded();
        }
        
        public static SubSonic.Query.SqlQuery Select{
            get{
                var db=new Kona.Data.KonaDB();
                return db.Select.From<OrderItem>();
            }
        }
        public static SubSonic.Query.SqlQuery SelectColumns(params string[] columns){
            var db=new Kona.Data.KonaDB();
            return new SubSonic.Query.Select(db.DataProvider, columns).From<OrderItem>();
        }
        internal static IRepository<OrderItem> GetRepo(){
            var db=new Kona.Data.KonaDB();
            IRepository<OrderItem> _repo;
            
            if(db.TestMode){
                OrderItem.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<OrderItem>(db);
            }
            return _repo;
        }
        
        public static OrderItem SingleOrDefault(Expression<Func<OrderItem, bool>> expression) {
           
            var single= All().SingleOrDefault(expression);
            if (single != null){
                single.OnLoaded();
                single.SetIsLoaded(true);
                single.SetIsNew(false);
            }
            return single;
        }      
        public static bool Exists(Expression<Func<OrderItem, bool>> expression) {
           
            return All().Any(expression);
        }        
        public static IList<OrderItem> Find(Expression<Func<OrderItem, bool>> expression) {
           
            return GetRepo().Find(expression).ToList();
        }
        public static IQueryable<OrderItem> All() {
            return GetRepo().GetAll();
        }
        public static PagedList<OrderItem> GetPaged<TKey>(Func<OrderItem,TKey> orderby, int pageIndex, int pageSize){
            return GetRepo().GetPaged(orderby, pageIndex, pageSize);
        }
        public static PagedList<OrderItem> GetPaged<TKey>(string sortBy, int pageIndex, int pageSize) {
            return GetRepo().GetPaged(sortBy, pageIndex, pageSize);
        }
        public static PagedList<OrderItem> GetPaged<TKey>(int pageIndex, int pageSize) {
            return GetRepo().GetPaged(pageIndex, pageSize);
            
        }

        public string KeyName()
        {
            return "OrderID";
        }

        public object KeyValue()
        {
            return this.OrderID;
        }
        
        public void SetKeyValue(object value) {
            if (value != DBNull.Value) {
                var settable = value.ChangeTypeTo<Guid>();
                this.GetType().GetProperty(this.KeyName()).SetValue(this, settable, null);
            }
        }
        
        public override string ToString(){
            return this.SKU.ToString();
        }

        public override bool Equals(object obj){
            if(obj.GetType()==typeof(OrderItem)){
                OrderItem compare=(OrderItem)obj;
                Guid thisPk=(Guid)this.KeyValue();
                Guid comparePk=(Guid)compare.KeyValue();
                return thisPk.Equals(comparePk);
            }else{
                return base.Equals(obj);
            }
        }

        public string DescriptorValue()
        {
            return this.SKU.ToString();
        }

        public string DescriptorColumn() {
            return "SKU";
        }
        
        public static string GetDescriptorColumn()
        {
            return "SKU";
        }
        
        #region ' Foreign Keys '
        IList<Order> _Orders;
        public IList<Order> Orders
        {
            get
            {
                
                  if(_Orders==null){
                      var repo=Order.GetRepo();
                  
                       _Orders = (from items in repo.GetAll()
                           where items.OrderID == _OrderID
                           select items).ToList();
                   }
                  return _Orders;
            }
            set{
                _Orders=value;
            }
        }

        IList<Product> _Products;
        public IList<Product> Products
        {
            get
            {
                
                  if(_Products==null){
                      var repo=Product.GetRepo();
                  
                       _Products = (from items in repo.GetAll()
                           where items.SKU == _SKU
                           select items).ToList();
                   }
                  return _Products;
            }
            set{
                _Products=value;
            }
        }

        #endregion
        

        Guid _OrderID;
        public Guid OrderID
        {
            get { return _OrderID; }
            set
            {
                
                _OrderID=value;
                var col=tbl.Columns.SingleOrDefault(x=>x.Name=="OrderID");
                if(col!=null){
                    if(!_dirtyColumns.Contains(col) && _isLoaded){
                        _dirtyColumns.Add(col);
                    }
                }
                OnChanged();
            }
        }

        string _SKU;
        public string SKU
        {
            get { return _SKU; }
            set
            {
                
                _SKU=value;
                var col=tbl.Columns.SingleOrDefault(x=>x.Name=="SKU");
                if(col!=null){
                    if(!_dirtyColumns.Contains(col) && _isLoaded){
                        _dirtyColumns.Add(col);
                    }
                }
                OnChanged();
            }
        }

        int _Quantity;
        public int Quantity
        {
            get { return _Quantity; }
            set
            {
                
                _Quantity=value;
                var col=tbl.Columns.SingleOrDefault(x=>x.Name=="Quantity");
                if(col!=null){
                    if(!_dirtyColumns.Contains(col) && _isLoaded){
                        _dirtyColumns.Add(col);
                    }
                }
                OnChanged();
            }
        }

        DateTime _DateAdded;
        public DateTime DateAdded
        {
            get { return _DateAdded; }
            set
            {
                
                _DateAdded=value;
                var col=tbl.Columns.SingleOrDefault(x=>x.Name=="DateAdded");
                if(col!=null){
                    if(!_dirtyColumns.Contains(col) && _isLoaded){
                        _dirtyColumns.Add(col);
                    }
                }
                OnChanged();
            }
        }

        decimal _LineItemPrice;
        public decimal LineItemPrice
        {
            get { return _LineItemPrice; }
            set
            {
                
                _LineItemPrice=value;
                var col=tbl.Columns.SingleOrDefault(x=>x.Name=="LineItemPrice");
                if(col!=null){
                    if(!_dirtyColumns.Contains(col) && _isLoaded){
                        _dirtyColumns.Add(col);
                    }
                }
                OnChanged();
            }
        }

        decimal _Discount;
        public decimal Discount
        {
            get { return _Discount; }
            set
            {
                
                _Discount=value;
                var col=tbl.Columns.SingleOrDefault(x=>x.Name=="Discount");
                if(col!=null){
                    if(!_dirtyColumns.Contains(col) && _isLoaded){
                        _dirtyColumns.Add(col);
                    }
                }
                OnChanged();
            }
        }

        string _DiscountReason;
        public string DiscountReason
        {
            get { return _DiscountReason; }
            set
            {
                
                _DiscountReason=value;
                var col=tbl.Columns.SingleOrDefault(x=>x.Name=="DiscountReason");
                if(col!=null){
                    if(!_dirtyColumns.Contains(col) && _isLoaded){
                        _dirtyColumns.Add(col);
                    }
                }
                OnChanged();
            }
        }

        decimal _LineItemWeightInPounds;
        public decimal LineItemWeightInPounds
        {
            get { return _LineItemWeightInPounds; }
            set
            {
                
                _LineItemWeightInPounds=value;
                var col=tbl.Columns.SingleOrDefault(x=>x.Name=="LineItemWeightInPounds");
                if(col!=null){
                    if(!_dirtyColumns.Contains(col) && _isLoaded){
                        _dirtyColumns.Add(col);
                    }
                }
                OnChanged();
            }
        }



        public DbCommand GetUpdateCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return _repo.BuildUpdateQuery(this).GetCommand().ToDbCommand();
            
        }
        public DbCommand GetInsertCommand() {
 
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else{
            
            
                return _repo.BuildInsertQuery(this).GetCommand().ToDbCommand();
                
            }
        }
        
        public DbCommand GetDeleteCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return _repo.BuildDeleteQuery(this).GetCommand().ToDbCommand();
        }
        //persistence
        public void Save(){
            Save("");
        }
        
        public void Update(string userName){
            _repo.Update(this);
            OnSaved();
       }
        
        public void Add(string userName){
            
            _repo.Add(this);
            OnSaved();
        }
        
        public void Save(string userName) {
            
           
            if (_isNew) {
                Add(userName);
                
            } else {
                Update(userName);
            }
            
        }

        public void Delete() {
                   
                 
            _repo.Delete(KeyValue());
            
                    }

        public static void Delete(object key) {
        
            var repo = new SubSonicRepository<OrderItem>(new Kona.Data.KonaDB());

                   
                 
            repo.Delete(key);
            
                        
        }

        public static void Delete(Func<OrderItem, bool> expression) {
            var repo = GetRepo();
            
                   
            
            repo.Delete(expression);
            
                    }

        

        public void Load(IDataReader rdr) {
            Load(rdr, true);
        }
        public void Load(IDataReader rdr, bool closeReader) {
            if (rdr.Read()) {

                try {
                    rdr.Load(this);
                    _isNew = false;
                    _isLoaded = true;
                } catch {
                    _isLoaded = false;
                    throw;
                }
            }else{
                _isLoaded = false;
            }

            if (closeReader)
                rdr.Dispose();
        }
        

    } 
    
    
    /// <summary>
    /// A class which represents the CategoryLocalized table in the Kona Database.
    /// </summary>
    public partial class CategoryLocalized: IActiveRecord
    {
    
        #region Built-in testing
        static IList<CategoryLocalized> TestItems;
        static TestRepository<CategoryLocalized> _testRepo;
        
        static void SetTestRepo(){
            _testRepo = _testRepo ?? new TestRepository<CategoryLocalized>(new Kona.Data.KonaDB());
        }
        public static void ResetTestRepo(){
            _testRepo = null;
            SetTestRepo();
        }
        public static void Setup(List<CategoryLocalized> testlist){
            SetTestRepo();
            _testRepo._items = testlist;
        }
        public static void Setup(CategoryLocalized item) {
            SetTestRepo();
            _testRepo._items.Add(item);
        }
        public static void Setup(int testItems) {
            SetTestRepo();
            for(int i=0;i<testItems;i++){
                CategoryLocalized item=new CategoryLocalized();
                _testRepo._items.Add(item);
            }
        }
        
        public bool TestMode {
            get {
                return this._db.DataProvider.ConnectionString.Equals("test", StringComparison.InvariantCultureIgnoreCase);
            }
        }

        #endregion

        IRepository<CategoryLocalized> _repo;
        ITable tbl;
        bool _isNew;
        public bool IsNew(){
            return _isNew;
        }
        public void SetIsNew(bool isNew){
            _isNew=isNew;
        }
        public void SetIsLoaded(bool isLoaded){
            _isLoaded=isLoaded;
        }
        bool _isLoaded;
        public bool IsLoaded(){
            return _isLoaded;
        }
                
        List<IColumn> _dirtyColumns;
        public bool IsDirty(){
            return _dirtyColumns.Count>0;
        }
        
        public List<IColumn> GetDirtyColumns (){
            return _dirtyColumns;
        }

        Kona.Data.KonaDB _db;
        public CategoryLocalized() {
             _db=new Kona.Data.KonaDB();
            _dirtyColumns=new List<IColumn>();
            
            if(TestMode){
                CategoryLocalized.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<CategoryLocalized>(_db);
            }
            tbl=_repo.GetTable();
            _isNew = true;
            OnCreated();
        }
        
       
        partial void OnCreated();
            
        partial void OnLoaded();
        
        partial void OnSaved();
        
        partial void OnChanged();
        
        public IList<IColumn> Columns{
            get{
                return tbl.Columns;
            }
        }
        
        public CategoryLocalized(object key):this() {
            _isLoaded=_repo.Load(this,this.KeyName(),key);
            if(_isLoaded)
                OnLoaded();

        }

        public CategoryLocalized(Expression<Func<CategoryLocalized, bool>> expression):this() {
            _isLoaded=_repo.Load(this,expression);
            if(_isLoaded)
                OnLoaded();
        }
        
        public static SubSonic.Query.SqlQuery Select{
            get{
                var db=new Kona.Data.KonaDB();
                return db.Select.From<CategoryLocalized>();
            }
        }
        public static SubSonic.Query.SqlQuery SelectColumns(params string[] columns){
            var db=new Kona.Data.KonaDB();
            return new SubSonic.Query.Select(db.DataProvider, columns).From<CategoryLocalized>();
        }
        internal static IRepository<CategoryLocalized> GetRepo(){
            var db=new Kona.Data.KonaDB();
            IRepository<CategoryLocalized> _repo;
            
            if(db.TestMode){
                CategoryLocalized.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<CategoryLocalized>(db);
            }
            return _repo;
        }
        
        public static CategoryLocalized SingleOrDefault(Expression<Func<CategoryLocalized, bool>> expression) {
           
            var single= All().SingleOrDefault(expression);
            if (single != null){
                single.OnLoaded();
                single.SetIsLoaded(true);
                single.SetIsNew(false);
            }
            return single;
        }      
        public static bool Exists(Expression<Func<CategoryLocalized, bool>> expression) {
           
            return All().Any(expression);
        }        
        public static IList<CategoryLocalized> Find(Expression<Func<CategoryLocalized, bool>> expression) {
           
            return GetRepo().Find(expression).ToList();
        }
        public static IQueryable<CategoryLocalized> All() {
            return GetRepo().GetAll();
        }
        public static PagedList<CategoryLocalized> GetPaged<TKey>(Func<CategoryLocalized,TKey> orderby, int pageIndex, int pageSize){
            return GetRepo().GetPaged(orderby, pageIndex, pageSize);
        }
        public static PagedList<CategoryLocalized> GetPaged<TKey>(string sortBy, int pageIndex, int pageSize) {
            return GetRepo().GetPaged(sortBy, pageIndex, pageSize);
        }
        public static PagedList<CategoryLocalized> GetPaged<TKey>(int pageIndex, int pageSize) {
            return GetRepo().GetPaged(pageIndex, pageSize);
            
        }

        public string KeyName()
        {
            return "CategoryNameID";
        }

        public object KeyValue()
        {
            return this.CategoryNameID;
        }
        
        public void SetKeyValue(object value) {
            if (value != DBNull.Value) {
                var settable = value.ChangeTypeTo<int>();
                this.GetType().GetProperty(this.KeyName()).SetValue(this, settable, null);
            }
        }
        
        public override string ToString(){
            return this.LanguageCode.ToString();
        }

        public override bool Equals(object obj){
            if(obj.GetType()==typeof(CategoryLocalized)){
                CategoryLocalized compare=(CategoryLocalized)obj;
                int thisPk=(int)this.KeyValue();
                int comparePk=(int)compare.KeyValue();
                return thisPk.Equals(comparePk);
            }else{
                return base.Equals(obj);
            }
        }

        public string DescriptorValue()
        {
            return this.LanguageCode.ToString();
        }

        public string DescriptorColumn() {
            return "LanguageCode";
        }
        
        public static string GetDescriptorColumn()
        {
            return "LanguageCode";
        }
        
        #region ' Foreign Keys '
        IList<Category> _Categories;
        public IList<Category> Categories
        {
            get
            {
                
                  if(_Categories==null){
                      var repo=Category.GetRepo();
                  
                       _Categories = (from items in repo.GetAll()
                           where items.CategoryID == _CategoryID
                           select items).ToList();
                   }
                  return _Categories;
            }
            set{
                _Categories=value;
            }
        }

        #endregion
        

        int _CategoryNameID;
        public int CategoryNameID
        {
            get { return _CategoryNameID; }
            set
            {
                
                _CategoryNameID=value;
                var col=tbl.Columns.SingleOrDefault(x=>x.Name=="CategoryNameID");
                if(col!=null){
                    if(!_dirtyColumns.Contains(col) && _isLoaded){
                        _dirtyColumns.Add(col);
                    }
                }
                OnChanged();
            }
        }

        int _CategoryID;
        public int CategoryID
        {
            get { return _CategoryID; }
            set
            {
                
                _CategoryID=value;
                var col=tbl.Columns.SingleOrDefault(x=>x.Name=="CategoryID");
                if(col!=null){
                    if(!_dirtyColumns.Contains(col) && _isLoaded){
                        _dirtyColumns.Add(col);
                    }
                }
                OnChanged();
            }
        }

        string _LanguageCode;
        public string LanguageCode
        {
            get { return _LanguageCode; }
            set
            {
                
                _LanguageCode=value;
                var col=tbl.Columns.SingleOrDefault(x=>x.Name=="LanguageCode");
                if(col!=null){
                    if(!_dirtyColumns.Contains(col) && _isLoaded){
                        _dirtyColumns.Add(col);
                    }
                }
                OnChanged();
            }
        }

        string _Name;
        public string Name
        {
            get { return _Name; }
            set
            {
                
                _Name=value;
                var col=tbl.Columns.SingleOrDefault(x=>x.Name=="Name");
                if(col!=null){
                    if(!_dirtyColumns.Contains(col) && _isLoaded){
                        _dirtyColumns.Add(col);
                    }
                }
                OnChanged();
            }
        }

        string _Slug;
        public string Slug
        {
            get { return _Slug; }
            set
            {
                
                _Slug=value;
                var col=tbl.Columns.SingleOrDefault(x=>x.Name=="Slug");
                if(col!=null){
                    if(!_dirtyColumns.Contains(col) && _isLoaded){
                        _dirtyColumns.Add(col);
                    }
                }
                OnChanged();
            }
        }

        string _DefaultImageFile;
        public string DefaultImageFile
        {
            get { return _DefaultImageFile; }
            set
            {
                
                _DefaultImageFile=value;
                var col=tbl.Columns.SingleOrDefault(x=>x.Name=="DefaultImageFile");
                if(col!=null){
                    if(!_dirtyColumns.Contains(col) && _isLoaded){
                        _dirtyColumns.Add(col);
                    }
                }
                OnChanged();
            }
        }

        string _Description;
        public string Description
        {
            get { return _Description; }
            set
            {
                
                _Description=value;
                var col=tbl.Columns.SingleOrDefault(x=>x.Name=="Description");
                if(col!=null){
                    if(!_dirtyColumns.Contains(col) && _isLoaded){
                        _dirtyColumns.Add(col);
                    }
                }
                OnChanged();
            }
        }

        bool _IsHome;
        public bool IsHome
        {
            get { return _IsHome; }
            set
            {
                
                _IsHome=value;
                var col=tbl.Columns.SingleOrDefault(x=>x.Name=="IsHome");
                if(col!=null){
                    if(!_dirtyColumns.Contains(col) && _isLoaded){
                        _dirtyColumns.Add(col);
                    }
                }
                OnChanged();
            }
        }



        public DbCommand GetUpdateCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return _repo.BuildUpdateQuery(this).GetCommand().ToDbCommand();
            
        }
        public DbCommand GetInsertCommand() {
 
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else{
            
            
                return _repo.BuildInsertQuery(this).GetCommand().ToDbCommand();
                
            }
        }
        
        public DbCommand GetDeleteCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return _repo.BuildDeleteQuery(this).GetCommand().ToDbCommand();
        }
        //persistence
        public void Save(){
            Save("");
        }
        
        public void Update(string userName){
            _repo.Update(this);
            OnSaved();
       }
        
        public void Add(string userName){
            
            
            this.SetKeyValue(_repo.Add(this));
            OnSaved();
        }
        
        public void Save(string userName) {
            
           
            if (_isNew) {
                Add(userName);
                
            } else {
                Update(userName);
            }
            
        }

        public void Delete() {
                   
                 
            _repo.Delete(KeyValue());
            
                    }

        public static void Delete(object key) {
        
            var repo = new SubSonicRepository<CategoryLocalized>(new Kona.Data.KonaDB());

                   
                 
            repo.Delete(key);
            
                        
        }

        public static void Delete(Func<CategoryLocalized, bool> expression) {
            var repo = GetRepo();
            
                   
            
            repo.Delete(expression);
            
                    }

        

        public void Load(IDataReader rdr) {
            Load(rdr, true);
        }
        public void Load(IDataReader rdr, bool closeReader) {
            if (rdr.Read()) {

                try {
                    rdr.Load(this);
                    _isNew = false;
                    _isLoaded = true;
                } catch {
                    _isLoaded = false;
                    throw;
                }
            }else{
                _isLoaded = false;
            }

            if (closeReader)
                rdr.Dispose();
        }
        

    } 
    
    
    /// <summary>
    /// A class which represents the Widgets table in the Kona Database.
    /// </summary>
    public partial class Widget: IActiveRecord
    {
    
        #region Built-in testing
        static IList<Widget> TestItems;
        static TestRepository<Widget> _testRepo;
        
        static void SetTestRepo(){
            _testRepo = _testRepo ?? new TestRepository<Widget>(new Kona.Data.KonaDB());
        }
        public static void ResetTestRepo(){
            _testRepo = null;
            SetTestRepo();
        }
        public static void Setup(List<Widget> testlist){
            SetTestRepo();
            _testRepo._items = testlist;
        }
        public static void Setup(Widget item) {
            SetTestRepo();
            _testRepo._items.Add(item);
        }
        public static void Setup(int testItems) {
            SetTestRepo();
            for(int i=0;i<testItems;i++){
                Widget item=new Widget();
                _testRepo._items.Add(item);
            }
        }
        
        public bool TestMode {
            get {
                return this._db.DataProvider.ConnectionString.Equals("test", StringComparison.InvariantCultureIgnoreCase);
            }
        }

        #endregion

        IRepository<Widget> _repo;
        ITable tbl;
        bool _isNew;
        public bool IsNew(){
            return _isNew;
        }
        public void SetIsNew(bool isNew){
            _isNew=isNew;
        }
        public void SetIsLoaded(bool isLoaded){
            _isLoaded=isLoaded;
        }
        bool _isLoaded;
        public bool IsLoaded(){
            return _isLoaded;
        }
                
        List<IColumn> _dirtyColumns;
        public bool IsDirty(){
            return _dirtyColumns.Count>0;
        }
        
        public List<IColumn> GetDirtyColumns (){
            return _dirtyColumns;
        }

        Kona.Data.KonaDB _db;
        public Widget() {
             _db=new Kona.Data.KonaDB();
            _dirtyColumns=new List<IColumn>();
            
            if(TestMode){
                Widget.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<Widget>(_db);
            }
            tbl=_repo.GetTable();
            _isNew = true;
            OnCreated();
        }
        
       
        partial void OnCreated();
            
        partial void OnLoaded();
        
        partial void OnSaved();
        
        partial void OnChanged();
        
        public IList<IColumn> Columns{
            get{
                return tbl.Columns;
            }
        }
        
        public Widget(object key):this() {
            _isLoaded=_repo.Load(this,this.KeyName(),key);
            if(_isLoaded)
                OnLoaded();

        }

        public Widget(Expression<Func<Widget, bool>> expression):this() {
            _isLoaded=_repo.Load(this,expression);
            if(_isLoaded)
                OnLoaded();
        }
        
        public static SubSonic.Query.SqlQuery Select{
            get{
                var db=new Kona.Data.KonaDB();
                return db.Select.From<Widget>();
            }
        }
        public static SubSonic.Query.SqlQuery SelectColumns(params string[] columns){
            var db=new Kona.Data.KonaDB();
            return new SubSonic.Query.Select(db.DataProvider, columns).From<Widget>();
        }
        internal static IRepository<Widget> GetRepo(){
            var db=new Kona.Data.KonaDB();
            IRepository<Widget> _repo;
            
            if(db.TestMode){
                Widget.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<Widget>(db);
            }
            return _repo;
        }
        
        public static Widget SingleOrDefault(Expression<Func<Widget, bool>> expression) {
           
            var single= All().SingleOrDefault(expression);
            if (single != null){
                single.OnLoaded();
                single.SetIsLoaded(true);
                single.SetIsNew(false);
            }
            return single;
        }      
        public static bool Exists(Expression<Func<Widget, bool>> expression) {
           
            return All().Any(expression);
        }        
        public static IList<Widget> Find(Expression<Func<Widget, bool>> expression) {
           
            return GetRepo().Find(expression).ToList();
        }
        public static IQueryable<Widget> All() {
            return GetRepo().GetAll();
        }
        public static PagedList<Widget> GetPaged<TKey>(Func<Widget,TKey> orderby, int pageIndex, int pageSize){
            return GetRepo().GetPaged(orderby, pageIndex, pageSize);
        }
        public static PagedList<Widget> GetPaged<TKey>(string sortBy, int pageIndex, int pageSize) {
            return GetRepo().GetPaged(sortBy, pageIndex, pageSize);
        }
        public static PagedList<Widget> GetPaged<TKey>(int pageIndex, int pageSize) {
            return GetRepo().GetPaged(pageIndex, pageSize);
            
        }

        public string KeyName()
        {
            return "WidgetID";
        }

        public object KeyValue()
        {
            return this.WidgetID;
        }
        
        public void SetKeyValue(object value) {
            if (value != DBNull.Value) {
                var settable = value.ChangeTypeTo<Guid>();
                this.GetType().GetProperty(this.KeyName()).SetValue(this, settable, null);
            }
        }
        
        public override string ToString(){
            return this.Zone.ToString();
        }

        public override bool Equals(object obj){
            if(obj.GetType()==typeof(Widget)){
                Widget compare=(Widget)obj;
                Guid thisPk=(Guid)this.KeyValue();
                Guid comparePk=(Guid)compare.KeyValue();
                return thisPk.Equals(comparePk);
            }else{
                return base.Equals(obj);
            }
        }

        public string DescriptorValue()
        {
            return this.Zone.ToString();
        }

        public string DescriptorColumn() {
            return "Zone";
        }
        
        public static string GetDescriptorColumn()
        {
            return "Zone";
        }
        
        #region ' Foreign Keys '
        IList<Categories_Widget> _Categories_Widgets;
        public IList<Categories_Widget> Categories_Widgets
        {
            get
            {
                
                  if(_Categories_Widgets==null){
                      var repo=Categories_Widget.GetRepo();
                  
                       _Categories_Widgets = (from items in repo.GetAll()
                           where items.WidgetID == _WidgetID
                           select items).ToList();
                   }
                  return _Categories_Widgets;
            }
            set{
                _Categories_Widgets=value;
            }
        }

        #endregion
        

        Guid _WidgetID;
        public Guid WidgetID
        {
            get { return _WidgetID; }
            set
            {
                
                _WidgetID=value;
                var col=tbl.Columns.SingleOrDefault(x=>x.Name=="WidgetID");
                if(col!=null){
                    if(!_dirtyColumns.Contains(col) && _isLoaded){
                        _dirtyColumns.Add(col);
                    }
                }
                OnChanged();
            }
        }

        int? _ListOrder;
        public int? ListOrder
        {
            get { return _ListOrder; }
            set
            {
                
                _ListOrder=value;
                var col=tbl.Columns.SingleOrDefault(x=>x.Name=="ListOrder");
                if(col!=null){
                    if(!_dirtyColumns.Contains(col) && _isLoaded){
                        _dirtyColumns.Add(col);
                    }
                }
                OnChanged();
            }
        }

        string _Zone;
        public string Zone
        {
            get { return _Zone; }
            set
            {
                
                _Zone=value;
                var col=tbl.Columns.SingleOrDefault(x=>x.Name=="Zone");
                if(col!=null){
                    if(!_dirtyColumns.Contains(col) && _isLoaded){
                        _dirtyColumns.Add(col);
                    }
                }
                OnChanged();
            }
        }

        string _Title;
        public string Title
        {
            get { return _Title; }
            set
            {
                
                _Title=value;
                var col=tbl.Columns.SingleOrDefault(x=>x.Name=="Title");
                if(col!=null){
                    if(!_dirtyColumns.Contains(col) && _isLoaded){
                        _dirtyColumns.Add(col);
                    }
                }
                OnChanged();
            }
        }

        string _Body;
        public string Body
        {
            get { return _Body; }
            set
            {
                
                _Body=value;
                var col=tbl.Columns.SingleOrDefault(x=>x.Name=="Body");
                if(col!=null){
                    if(!_dirtyColumns.Contains(col) && _isLoaded){
                        _dirtyColumns.Add(col);
                    }
                }
                OnChanged();
            }
        }

        string _SKUList;
        public string SKUList
        {
            get { return _SKUList; }
            set
            {
                
                _SKUList=value;
                var col=tbl.Columns.SingleOrDefault(x=>x.Name=="SKUList");
                if(col!=null){
                    if(!_dirtyColumns.Contains(col) && _isLoaded){
                        _dirtyColumns.Add(col);
                    }
                }
                OnChanged();
            }
        }

        string _ViewName;
        public string ViewName
        {
            get { return _ViewName; }
            set
            {
                
                _ViewName=value;
                var col=tbl.Columns.SingleOrDefault(x=>x.Name=="ViewName");
                if(col!=null){
                    if(!_dirtyColumns.Contains(col) && _isLoaded){
                        _dirtyColumns.Add(col);
                    }
                }
                OnChanged();
            }
        }

        string _LanguageCode;
        public string LanguageCode
        {
            get { return _LanguageCode; }
            set
            {
                
                _LanguageCode=value;
                var col=tbl.Columns.SingleOrDefault(x=>x.Name=="LanguageCode");
                if(col!=null){
                    if(!_dirtyColumns.Contains(col) && _isLoaded){
                        _dirtyColumns.Add(col);
                    }
                }
                OnChanged();
            }
        }

        DateTime _CreatedOn;
        public DateTime CreatedOn
        {
            get { return _CreatedOn; }
            set
            {
                
                _CreatedOn=value;
                var col=tbl.Columns.SingleOrDefault(x=>x.Name=="CreatedOn");
                if(col!=null){
                    if(!_dirtyColumns.Contains(col) && _isLoaded){
                        _dirtyColumns.Add(col);
                    }
                }
                OnChanged();
            }
        }

        DateTime _ModifiedOn;
        public DateTime ModifiedOn
        {
            get { return _ModifiedOn; }
            set
            {
                
                _ModifiedOn=value;
                var col=tbl.Columns.SingleOrDefault(x=>x.Name=="ModifiedOn");
                if(col!=null){
                    if(!_dirtyColumns.Contains(col) && _isLoaded){
                        _dirtyColumns.Add(col);
                    }
                }
                OnChanged();
            }
        }

        string _CreatedBy;
        public string CreatedBy
        {
            get { return _CreatedBy; }
            set
            {
                
                _CreatedBy=value;
                var col=tbl.Columns.SingleOrDefault(x=>x.Name=="CreatedBy");
                if(col!=null){
                    if(!_dirtyColumns.Contains(col) && _isLoaded){
                        _dirtyColumns.Add(col);
                    }
                }
                OnChanged();
            }
        }

        string _ModifiedBy;
        public string ModifiedBy
        {
            get { return _ModifiedBy; }
            set
            {
                
                _ModifiedBy=value;
                var col=tbl.Columns.SingleOrDefault(x=>x.Name=="ModifiedBy");
                if(col!=null){
                    if(!_dirtyColumns.Contains(col) && _isLoaded){
                        _dirtyColumns.Add(col);
                    }
                }
                OnChanged();
            }
        }



        public DbCommand GetUpdateCommand() {
            if (!_dirtyColumns.Any(x => x.Name.ToLower() == "modifiedon")) {
               this.ModifiedOn=DateTime.Now;
            }            
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return _repo.BuildUpdateQuery(this).GetCommand().ToDbCommand();
            
        }
        public DbCommand GetInsertCommand() {
 
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else{
            
            this.CreatedOn=DateTime.Now;
            this.CreatedBy="";
            this.ModifiedOn=DateTime.Now;
            this.ModifiedBy="";
            
            
                return _repo.BuildInsertQuery(this).GetCommand().ToDbCommand();
                
            }
        }
        
        public DbCommand GetDeleteCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return _repo.BuildDeleteQuery(this).GetCommand().ToDbCommand();
        }
        //persistence
        public void Save(){
            Save("");
        }
        
        public void Update(string userName){
            this.ModifiedBy=userName;
            this.ModifiedOn=DateTime.Now;
            _repo.Update(this);
            OnSaved();
       }
        
        public void Add(string userName){
            
            this.CreatedOn=DateTime.Now;
            this.CreatedBy=userName;
            this.ModifiedOn=DateTime.Now;
            this.ModifiedBy=userName;
            
            _repo.Add(this);
            OnSaved();
        }
        
        public void Save(string userName) {
            
           
            if (_isNew) {
                Add(userName);
                
            } else {
                Update(userName);
            }
            
        }

        public void Delete() {
                   
                 
            _repo.Delete(KeyValue());
            
                    }

        public static void Delete(object key) {
        
            var repo = new SubSonicRepository<Widget>(new Kona.Data.KonaDB());

                   
                 
            repo.Delete(key);
            
                        
        }

        public static void Delete(Func<Widget, bool> expression) {
            var repo = GetRepo();
            
                   
            
            repo.Delete(expression);
            
                    }

        

        public void Load(IDataReader rdr) {
            Load(rdr, true);
        }
        public void Load(IDataReader rdr, bool closeReader) {
            if (rdr.Read()) {

                try {
                    rdr.Load(this);
                    _isNew = false;
                    _isLoaded = true;
                } catch {
                    _isLoaded = false;
                    throw;
                }
            }else{
                _isLoaded = false;
            }

            if (closeReader)
                rdr.Dispose();
        }
        

    } 
    
    
    /// <summary>
    /// A class which represents the Products table in the Kona Database.
    /// </summary>
    public partial class Product: IActiveRecord
    {
    
        #region Built-in testing
        static IList<Product> TestItems;
        static TestRepository<Product> _testRepo;
        
        static void SetTestRepo(){
            _testRepo = _testRepo ?? new TestRepository<Product>(new Kona.Data.KonaDB());
        }
        public static void ResetTestRepo(){
            _testRepo = null;
            SetTestRepo();
        }
        public static void Setup(List<Product> testlist){
            SetTestRepo();
            _testRepo._items = testlist;
        }
        public static void Setup(Product item) {
            SetTestRepo();
            _testRepo._items.Add(item);
        }
        public static void Setup(int testItems) {
            SetTestRepo();
            for(int i=0;i<testItems;i++){
                Product item=new Product();
                _testRepo._items.Add(item);
            }
        }
        
        public bool TestMode {
            get {
                return this._db.DataProvider.ConnectionString.Equals("test", StringComparison.InvariantCultureIgnoreCase);
            }
        }

        #endregion

        IRepository<Product> _repo;
        ITable tbl;
        bool _isNew;
        public bool IsNew(){
            return _isNew;
        }
        public void SetIsNew(bool isNew){
            _isNew=isNew;
        }
        public void SetIsLoaded(bool isLoaded){
            _isLoaded=isLoaded;
        }
        bool _isLoaded;
        public bool IsLoaded(){
            return _isLoaded;
        }
                
        List<IColumn> _dirtyColumns;
        public bool IsDirty(){
            return _dirtyColumns.Count>0;
        }
        
        public List<IColumn> GetDirtyColumns (){
            return _dirtyColumns;
        }

        Kona.Data.KonaDB _db;
        public Product() {
             _db=new Kona.Data.KonaDB();
            _dirtyColumns=new List<IColumn>();
            
            if(TestMode){
                Product.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<Product>(_db);
            }
            tbl=_repo.GetTable();
            _isNew = true;
            OnCreated();
        }
        
       
        partial void OnCreated();
            
        partial void OnLoaded();
        
        partial void OnSaved();
        
        partial void OnChanged();
        
        public IList<IColumn> Columns{
            get{
                return tbl.Columns;
            }
        }
        
        public Product(object key):this() {
            _isLoaded=_repo.Load(this,this.KeyName(),key);
            if(_isLoaded)
                OnLoaded();

        }

        public Product(Expression<Func<Product, bool>> expression):this() {
            _isLoaded=_repo.Load(this,expression);
            if(_isLoaded)
                OnLoaded();
        }
        
        public static SubSonic.Query.SqlQuery Select{
            get{
                var db=new Kona.Data.KonaDB();
                return db.Select.From<Product>();
            }
        }
        public static SubSonic.Query.SqlQuery SelectColumns(params string[] columns){
            var db=new Kona.Data.KonaDB();
            return new SubSonic.Query.Select(db.DataProvider, columns).From<Product>();
        }
        internal static IRepository<Product> GetRepo(){
            var db=new Kona.Data.KonaDB();
            IRepository<Product> _repo;
            
            if(db.TestMode){
                Product.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<Product>(db);
            }
            return _repo;
        }
        
        public static Product SingleOrDefault(Expression<Func<Product, bool>> expression) {
           
            var single= All().SingleOrDefault(expression);
            if (single != null){
                single.OnLoaded();
                single.SetIsLoaded(true);
                single.SetIsNew(false);
            }
            return single;
        }      
        public static bool Exists(Expression<Func<Product, bool>> expression) {
           
            return All().Any(expression);
        }        
        public static IList<Product> Find(Expression<Func<Product, bool>> expression) {
           
            return GetRepo().Find(expression).ToList();
        }
        public static IQueryable<Product> All() {
            return GetRepo().GetAll();
        }
        public static PagedList<Product> GetPaged<TKey>(Func<Product,TKey> orderby, int pageIndex, int pageSize){
            return GetRepo().GetPaged(orderby, pageIndex, pageSize);
        }
        public static PagedList<Product> GetPaged<TKey>(string sortBy, int pageIndex, int pageSize) {
            return GetRepo().GetPaged(sortBy, pageIndex, pageSize);
        }
        public static PagedList<Product> GetPaged<TKey>(int pageIndex, int pageSize) {
            return GetRepo().GetPaged(pageIndex, pageSize);
            
        }

        public string KeyName()
        {
            return "SKU";
        }

        public object KeyValue()
        {
            return this.SKU;
        }
        
        public void SetKeyValue(object value) {
            if (value != DBNull.Value) {
                var settable = value.ChangeTypeTo<string>();
                this.GetType().GetProperty(this.KeyName()).SetValue(this, settable, null);
            }
        }
        
        public override string ToString(){
            return this.SKU.ToString();
        }

        public override bool Equals(object obj){
            if(obj.GetType()==typeof(Product)){
                Product compare=(Product)obj;
                string thisPk=(string)this.KeyValue();
                string comparePk=(string)compare.KeyValue();
                return thisPk.Equals(comparePk);
            }else{
                return base.Equals(obj);
            }
        }

        public string DescriptorValue()
        {
            return this.SKU.ToString();
        }

        public string DescriptorColumn() {
            return "SKU";
        }
        
        public static string GetDescriptorColumn()
        {
            return "SKU";
        }
        
        #region ' Foreign Keys '
        IList<Categories_Product> _Categories_Products;
        public IList<Categories_Product> Categories_Products
        {
            get
            {
                
                  if(_Categories_Products==null){
                      var repo=Categories_Product.GetRepo();
                  
                       _Categories_Products = (from items in repo.GetAll()
                           where items.SKU == _SKU
                           select items).ToList();
                   }
                  return _Categories_Products;
            }
            set{
                _Categories_Products=value;
            }
        }

        IList<InventoryRecord> _InventoryRecords;
        public IList<InventoryRecord> InventoryRecords
        {
            get
            {
                
                  if(_InventoryRecords==null){
                      var repo=InventoryRecord.GetRepo();
                  
                       _InventoryRecords = (from items in repo.GetAll()
                           where items.SKU == _SKU
                           select items).ToList();
                   }
                  return _InventoryRecords;
            }
            set{
                _InventoryRecords=value;
            }
        }

        IList<OrderItem> _OrderItems;
        public IList<OrderItem> OrderItems
        {
            get
            {
                
                  if(_OrderItems==null){
                      var repo=OrderItem.GetRepo();
                  
                       _OrderItems = (from items in repo.GetAll()
                           where items.SKU == _SKU
                           select items).ToList();
                   }
                  return _OrderItems;
            }
            set{
                _OrderItems=value;
            }
        }

        IList<ProductDescriptor> _ProductDescriptors;
        public IList<ProductDescriptor> ProductDescriptors
        {
            get
            {
                
                  if(_ProductDescriptors==null){
                      var repo=ProductDescriptor.GetRepo();
                  
                       _ProductDescriptors = (from items in repo.GetAll()
                           where items.SKU == _SKU
                           select items).ToList();
                   }
                  return _ProductDescriptors;
            }
            set{
                _ProductDescriptors=value;
            }
        }

        IList<ProductImage> _ProductImages;
        public IList<ProductImage> ProductImages
        {
            get
            {
                
                  if(_ProductImages==null){
                      var repo=ProductImage.GetRepo();
                  
                       _ProductImages = (from items in repo.GetAll()
                           where items.SKU == _SKU
                           select items).ToList();
                   }
                  return _ProductImages;
            }
            set{
                _ProductImages=value;
            }
        }

        IList<Products_CrossSell> _Products_CrossSells;
        public IList<Products_CrossSell> Products_CrossSells
        {
            get
            {
                
                  if(_Products_CrossSells==null){
                      var repo=Products_CrossSell.GetRepo();
                  
                       _Products_CrossSells = (from items in repo.GetAll()
                           where items.SKU == _SKU
                           select items).ToList();
                   }
                  return _Products_CrossSells;
            }
            set{
                _Products_CrossSells=value;
            }
        }

        IList<Products_CrossSell> _Products_CrossSells6;
        public IList<Products_CrossSell> Products_CrossSells6
        {
            get
            {
                
                  if(_Products_CrossSells6==null){
                      var repo=Products_CrossSell.GetRepo();
                  
                       _Products_CrossSells6 = (from items in repo.GetAll()
                           where items.CrossSKU == _SKU
                           select items).ToList();
                   }
                  return _Products_CrossSells6;
            }
            set{
                _Products_CrossSells6=value;
            }
        }

        IList<InventoryStatus> _InventoryStatuses;
        public IList<InventoryStatus> InventoryStatuses
        {
            get
            {
                
                  if(_InventoryStatuses==null){
                      var repo=InventoryStatus.GetRepo();
                  
                       _InventoryStatuses = (from items in repo.GetAll()
                           where items.InventoryStatusID == _InventoryStatusID
                           select items).ToList();
                   }
                  return _InventoryStatuses;
            }
            set{
                _InventoryStatuses=value;
            }
        }

        IList<Products_Related> _Products_Relateds;
        public IList<Products_Related> Products_Relateds
        {
            get
            {
                
                  if(_Products_Relateds==null){
                      var repo=Products_Related.GetRepo();
                  
                       _Products_Relateds = (from items in repo.GetAll()
                           where items.SKU == _SKU
                           select items).ToList();
                   }
                  return _Products_Relateds;
            }
            set{
                _Products_Relateds=value;
            }
        }

        IList<Products_Related> _Products_Relateds9;
        public IList<Products_Related> Products_Relateds9
        {
            get
            {
                
                  if(_Products_Relateds9==null){
                      var repo=Products_Related.GetRepo();
                  
                       _Products_Relateds9 = (from items in repo.GetAll()
                           where items.RelatedSKU == _SKU
                           select items).ToList();
                   }
                  return _Products_Relateds9;
            }
            set{
                _Products_Relateds9=value;
            }
        }

        #endregion
        

        string _SKU;
        public string SKU
        {
            get { return _SKU; }
            set
            {
                
                _SKU=value;
                var col=tbl.Columns.SingleOrDefault(x=>x.Name=="SKU");
                if(col!=null){
                    if(!_dirtyColumns.Contains(col) && _isLoaded){
                        _dirtyColumns.Add(col);
                    }
                }
                OnChanged();
            }
        }

        Guid _SiteID;
        public Guid SiteID
        {
            get { return _SiteID; }
            set
            {
                
                _SiteID=value;
                var col=tbl.Columns.SingleOrDefault(x=>x.Name=="SiteID");
                if(col!=null){
                    if(!_dirtyColumns.Contains(col) && _isLoaded){
                        _dirtyColumns.Add(col);
                    }
                }
                OnChanged();
            }
        }

        int _DeliveryMethodID;
        public int DeliveryMethodID
        {
            get { return _DeliveryMethodID; }
            set
            {
                
                _DeliveryMethodID=value;
                var col=tbl.Columns.SingleOrDefault(x=>x.Name=="DeliveryMethodID");
                if(col!=null){
                    if(!_dirtyColumns.Contains(col) && _isLoaded){
                        _dirtyColumns.Add(col);
                    }
                }
                OnChanged();
            }
        }

        string _ProductName;
        public string ProductName
        {
            get { return _ProductName; }
            set
            {
                
                _ProductName=value;
                var col=tbl.Columns.SingleOrDefault(x=>x.Name=="ProductName");
                if(col!=null){
                    if(!_dirtyColumns.Contains(col) && _isLoaded){
                        _dirtyColumns.Add(col);
                    }
                }
                OnChanged();
            }
        }

        decimal _BasePrice;
        public decimal BasePrice
        {
            get { return _BasePrice; }
            set
            {
                
                _BasePrice=value;
                var col=tbl.Columns.SingleOrDefault(x=>x.Name=="BasePrice");
                if(col!=null){
                    if(!_dirtyColumns.Contains(col) && _isLoaded){
                        _dirtyColumns.Add(col);
                    }
                }
                OnChanged();
            }
        }

        decimal _WeightInPounds;
        public decimal WeightInPounds
        {
            get { return _WeightInPounds; }
            set
            {
                
                _WeightInPounds=value;
                var col=tbl.Columns.SingleOrDefault(x=>x.Name=="WeightInPounds");
                if(col!=null){
                    if(!_dirtyColumns.Contains(col) && _isLoaded){
                        _dirtyColumns.Add(col);
                    }
                }
                OnChanged();
            }
        }

        DateTime _DateAvailable;
        public DateTime DateAvailable
        {
            get { return _DateAvailable; }
            set
            {
                
                _DateAvailable=value;
                var col=tbl.Columns.SingleOrDefault(x=>x.Name=="DateAvailable");
                if(col!=null){
                    if(!_dirtyColumns.Contains(col) && _isLoaded){
                        _dirtyColumns.Add(col);
                    }
                }
                OnChanged();
            }
        }

        int _InventoryStatusID;
        public int InventoryStatusID
        {
            get { return _InventoryStatusID; }
            set
            {
                
                _InventoryStatusID=value;
                var col=tbl.Columns.SingleOrDefault(x=>x.Name=="InventoryStatusID");
                if(col!=null){
                    if(!_dirtyColumns.Contains(col) && _isLoaded){
                        _dirtyColumns.Add(col);
                    }
                }
                OnChanged();
            }
        }

        string _EstimatedDelivery;
        public string EstimatedDelivery
        {
            get { return _EstimatedDelivery; }
            set
            {
                
                _EstimatedDelivery=value;
                var col=tbl.Columns.SingleOrDefault(x=>x.Name=="EstimatedDelivery");
                if(col!=null){
                    if(!_dirtyColumns.Contains(col) && _isLoaded){
                        _dirtyColumns.Add(col);
                    }
                }
                OnChanged();
            }
        }

        bool _AllowBackOrder;
        public bool AllowBackOrder
        {
            get { return _AllowBackOrder; }
            set
            {
                
                _AllowBackOrder=value;
                var col=tbl.Columns.SingleOrDefault(x=>x.Name=="AllowBackOrder");
                if(col!=null){
                    if(!_dirtyColumns.Contains(col) && _isLoaded){
                        _dirtyColumns.Add(col);
                    }
                }
                OnChanged();
            }
        }

        bool _IsTaxable;
        public bool IsTaxable
        {
            get { return _IsTaxable; }
            set
            {
                
                _IsTaxable=value;
                var col=tbl.Columns.SingleOrDefault(x=>x.Name=="IsTaxable");
                if(col!=null){
                    if(!_dirtyColumns.Contains(col) && _isLoaded){
                        _dirtyColumns.Add(col);
                    }
                }
                OnChanged();
            }
        }

        string _DefaultImageFile;
        public string DefaultImageFile
        {
            get { return _DefaultImageFile; }
            set
            {
                
                _DefaultImageFile=value;
                var col=tbl.Columns.SingleOrDefault(x=>x.Name=="DefaultImageFile");
                if(col!=null){
                    if(!_dirtyColumns.Contains(col) && _isLoaded){
                        _dirtyColumns.Add(col);
                    }
                }
                OnChanged();
            }
        }

        int _AmountOnHand;
        public int AmountOnHand
        {
            get { return _AmountOnHand; }
            set
            {
                
                _AmountOnHand=value;
                var col=tbl.Columns.SingleOrDefault(x=>x.Name=="AmountOnHand");
                if(col!=null){
                    if(!_dirtyColumns.Contains(col) && _isLoaded){
                        _dirtyColumns.Add(col);
                    }
                }
                OnChanged();
            }
        }

        bool _AllowPreOrder;
        public bool AllowPreOrder
        {
            get { return _AllowPreOrder; }
            set
            {
                
                _AllowPreOrder=value;
                var col=tbl.Columns.SingleOrDefault(x=>x.Name=="AllowPreOrder");
                if(col!=null){
                    if(!_dirtyColumns.Contains(col) && _isLoaded){
                        _dirtyColumns.Add(col);
                    }
                }
                OnChanged();
            }
        }



        public DbCommand GetUpdateCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return _repo.BuildUpdateQuery(this).GetCommand().ToDbCommand();
            
        }
        public DbCommand GetInsertCommand() {
 
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else{
            
            
                return _repo.BuildInsertQuery(this).GetCommand().ToDbCommand();
                
            }
        }
        
        public DbCommand GetDeleteCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return _repo.BuildDeleteQuery(this).GetCommand().ToDbCommand();
        }
        //persistence
        public void Save(){
            Save("");
        }
        
        public void Update(string userName){
            _repo.Update(this);
            OnSaved();
       }
        
        public void Add(string userName){
            
            _repo.Add(this);
            OnSaved();
        }
        
        public void Save(string userName) {
            
           
            if (_isNew) {
                Add(userName);
                
            } else {
                Update(userName);
            }
            
        }

        public void Delete() {
                   
                 
            _repo.Delete(KeyValue());
            
                    }

        public static void Delete(object key) {
        
            var repo = new SubSonicRepository<Product>(new Kona.Data.KonaDB());

                   
                 
            repo.Delete(key);
            
                        
        }

        public static void Delete(Func<Product, bool> expression) {
            var repo = GetRepo();
            
                   
            
            repo.Delete(expression);
            
                    }

        

        public void Load(IDataReader rdr) {
            Load(rdr, true);
        }
        public void Load(IDataReader rdr, bool closeReader) {
            if (rdr.Read()) {

                try {
                    rdr.Load(this);
                    _isNew = false;
                    _isLoaded = true;
                } catch {
                    _isLoaded = false;
                    throw;
                }
            }else{
                _isLoaded = false;
            }

            if (closeReader)
                rdr.Dispose();
        }
        

    } 
    
    
    /// <summary>
    /// A class which represents the Orders table in the Kona Database.
    /// </summary>
    public partial class Order: IActiveRecord
    {
    
        #region Built-in testing
        static IList<Order> TestItems;
        static TestRepository<Order> _testRepo;
        
        static void SetTestRepo(){
            _testRepo = _testRepo ?? new TestRepository<Order>(new Kona.Data.KonaDB());
        }
        public static void ResetTestRepo(){
            _testRepo = null;
            SetTestRepo();
        }
        public static void Setup(List<Order> testlist){
            SetTestRepo();
            _testRepo._items = testlist;
        }
        public static void Setup(Order item) {
            SetTestRepo();
            _testRepo._items.Add(item);
        }
        public static void Setup(int testItems) {
            SetTestRepo();
            for(int i=0;i<testItems;i++){
                Order item=new Order();
                _testRepo._items.Add(item);
            }
        }
        
        public bool TestMode {
            get {
                return this._db.DataProvider.ConnectionString.Equals("test", StringComparison.InvariantCultureIgnoreCase);
            }
        }

        #endregion

        IRepository<Order> _repo;
        ITable tbl;
        bool _isNew;
        public bool IsNew(){
            return _isNew;
        }
        public void SetIsNew(bool isNew){
            _isNew=isNew;
        }
        public void SetIsLoaded(bool isLoaded){
            _isLoaded=isLoaded;
        }
        bool _isLoaded;
        public bool IsLoaded(){
            return _isLoaded;
        }
                
        List<IColumn> _dirtyColumns;
        public bool IsDirty(){
            return _dirtyColumns.Count>0;
        }
        
        public List<IColumn> GetDirtyColumns (){
            return _dirtyColumns;
        }

        Kona.Data.KonaDB _db;
        public Order() {
             _db=new Kona.Data.KonaDB();
            _dirtyColumns=new List<IColumn>();
            
            if(TestMode){
                Order.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<Order>(_db);
            }
            tbl=_repo.GetTable();
            _isNew = true;
            OnCreated();
        }
        
       
        partial void OnCreated();
            
        partial void OnLoaded();
        
        partial void OnSaved();
        
        partial void OnChanged();
        
        public IList<IColumn> Columns{
            get{
                return tbl.Columns;
            }
        }
        
        public Order(object key):this() {
            _isLoaded=_repo.Load(this,this.KeyName(),key);
            if(_isLoaded)
                OnLoaded();

        }

        public Order(Expression<Func<Order, bool>> expression):this() {
            _isLoaded=_repo.Load(this,expression);
            if(_isLoaded)
                OnLoaded();
        }
        
        public static SubSonic.Query.SqlQuery Select{
            get{
                var db=new Kona.Data.KonaDB();
                return db.Select.From<Order>();
            }
        }
        public static SubSonic.Query.SqlQuery SelectColumns(params string[] columns){
            var db=new Kona.Data.KonaDB();
            return new SubSonic.Query.Select(db.DataProvider, columns).From<Order>();
        }
        internal static IRepository<Order> GetRepo(){
            var db=new Kona.Data.KonaDB();
            IRepository<Order> _repo;
            
            if(db.TestMode){
                Order.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<Order>(db);
            }
            return _repo;
        }
        
        public static Order SingleOrDefault(Expression<Func<Order, bool>> expression) {
           
            var single= All().SingleOrDefault(expression);
            if (single != null){
                single.OnLoaded();
                single.SetIsLoaded(true);
                single.SetIsNew(false);
            }
            return single;
        }      
        public static bool Exists(Expression<Func<Order, bool>> expression) {
           
            return All().Any(expression);
        }        
        public static IList<Order> Find(Expression<Func<Order, bool>> expression) {
           
            return GetRepo().Find(expression).ToList();
        }
        public static IQueryable<Order> All() {
            return GetRepo().GetAll();
        }
        public static PagedList<Order> GetPaged<TKey>(Func<Order,TKey> orderby, int pageIndex, int pageSize){
            return GetRepo().GetPaged(orderby, pageIndex, pageSize);
        }
        public static PagedList<Order> GetPaged<TKey>(string sortBy, int pageIndex, int pageSize) {
            return GetRepo().GetPaged(sortBy, pageIndex, pageSize);
        }
        public static PagedList<Order> GetPaged<TKey>(int pageIndex, int pageSize) {
            return GetRepo().GetPaged(pageIndex, pageSize);
            
        }

        public string KeyName()
        {
            return "OrderID";
        }

        public object KeyValue()
        {
            return this.OrderID;
        }
        
        public void SetKeyValue(object value) {
            if (value != DBNull.Value) {
                var settable = value.ChangeTypeTo<Guid>();
                this.GetType().GetProperty(this.KeyName()).SetValue(this, settable, null);
            }
        }
        
        public override string ToString(){
            return this.OrderNumber.ToString();
        }

        public override bool Equals(object obj){
            if(obj.GetType()==typeof(Order)){
                Order compare=(Order)obj;
                Guid thisPk=(Guid)this.KeyValue();
                Guid comparePk=(Guid)compare.KeyValue();
                return thisPk.Equals(comparePk);
            }else{
                return base.Equals(obj);
            }
        }

        public string DescriptorValue()
        {
            return this.OrderNumber.ToString();
        }

        public string DescriptorColumn() {
            return "OrderNumber";
        }
        
        public static string GetDescriptorColumn()
        {
            return "OrderNumber";
        }
        
        #region ' Foreign Keys '
        IList<OrderItem> _OrderItems;
        public IList<OrderItem> OrderItems
        {
            get
            {
                
                  if(_OrderItems==null){
                      var repo=OrderItem.GetRepo();
                  
                       _OrderItems = (from items in repo.GetAll()
                           where items.OrderID == _OrderID
                           select items).ToList();
                   }
                  return _OrderItems;
            }
            set{
                _OrderItems=value;
            }
        }

        IList<Address> _Addresses;
        public IList<Address> Addresses
        {
            get
            {
                
                  if(_Addresses==null){
                      var repo=Address.GetRepo();
                  
                       _Addresses = (from items in repo.GetAll()
                           where items.AddressID == _ShippingAddressID
                           select items).ToList();
                   }
                  return _Addresses;
            }
            set{
                _Addresses=value;
            }
        }

        IList<Address> _Addresses2;
        public IList<Address> Addresses2
        {
            get
            {
                
                  if(_Addresses2==null){
                      var repo=Address.GetRepo();
                  
                       _Addresses2 = (from items in repo.GetAll()
                           where items.AddressID == _BillingAddressID
                           select items).ToList();
                   }
                  return _Addresses2;
            }
            set{
                _Addresses2=value;
            }
        }

        IList<Customer> _Customers;
        public IList<Customer> Customers
        {
            get
            {
                
                  if(_Customers==null){
                      var repo=Customer.GetRepo();
                  
                       _Customers = (from items in repo.GetAll()
                           where items.UserName == _UserName
                           select items).ToList();
                   }
                  return _Customers;
            }
            set{
                _Customers=value;
            }
        }

        IList<Transaction> _Transactions;
        public IList<Transaction> Transactions
        {
            get
            {
                
                  if(_Transactions==null){
                      var repo=Transaction.GetRepo();
                  
                       _Transactions = (from items in repo.GetAll()
                           where items.OrderID == _OrderID
                           select items).ToList();
                   }
                  return _Transactions;
            }
            set{
                _Transactions=value;
            }
        }

        #endregion
        

        Guid _OrderID;
        public Guid OrderID
        {
            get { return _OrderID; }
            set
            {
                
                _OrderID=value;
                var col=tbl.Columns.SingleOrDefault(x=>x.Name=="OrderID");
                if(col!=null){
                    if(!_dirtyColumns.Contains(col) && _isLoaded){
                        _dirtyColumns.Add(col);
                    }
                }
                OnChanged();
            }
        }

        string _OrderNumber;
        public string OrderNumber
        {
            get { return _OrderNumber; }
            set
            {
                
                _OrderNumber=value;
                var col=tbl.Columns.SingleOrDefault(x=>x.Name=="OrderNumber");
                if(col!=null){
                    if(!_dirtyColumns.Contains(col) && _isLoaded){
                        _dirtyColumns.Add(col);
                    }
                }
                OnChanged();
            }
        }

        string _UserName;
        public string UserName
        {
            get { return _UserName; }
            set
            {
                
                _UserName=value;
                var col=tbl.Columns.SingleOrDefault(x=>x.Name=="UserName");
                if(col!=null){
                    if(!_dirtyColumns.Contains(col) && _isLoaded){
                        _dirtyColumns.Add(col);
                    }
                }
                OnChanged();
            }
        }

        string _UserLanguageCode;
        public string UserLanguageCode
        {
            get { return _UserLanguageCode; }
            set
            {
                
                _UserLanguageCode=value;
                var col=tbl.Columns.SingleOrDefault(x=>x.Name=="UserLanguageCode");
                if(col!=null){
                    if(!_dirtyColumns.Contains(col) && _isLoaded){
                        _dirtyColumns.Add(col);
                    }
                }
                OnChanged();
            }
        }

        decimal _TaxAmount;
        public decimal TaxAmount
        {
            get { return _TaxAmount; }
            set
            {
                
                _TaxAmount=value;
                var col=tbl.Columns.SingleOrDefault(x=>x.Name=="TaxAmount");
                if(col!=null){
                    if(!_dirtyColumns.Contains(col) && _isLoaded){
                        _dirtyColumns.Add(col);
                    }
                }
                OnChanged();
            }
        }

        string _ShippingService;
        public string ShippingService
        {
            get { return _ShippingService; }
            set
            {
                
                _ShippingService=value;
                var col=tbl.Columns.SingleOrDefault(x=>x.Name=="ShippingService");
                if(col!=null){
                    if(!_dirtyColumns.Contains(col) && _isLoaded){
                        _dirtyColumns.Add(col);
                    }
                }
                OnChanged();
            }
        }

        decimal _ShippingAmount;
        public decimal ShippingAmount
        {
            get { return _ShippingAmount; }
            set
            {
                
                _ShippingAmount=value;
                var col=tbl.Columns.SingleOrDefault(x=>x.Name=="ShippingAmount");
                if(col!=null){
                    if(!_dirtyColumns.Contains(col) && _isLoaded){
                        _dirtyColumns.Add(col);
                    }
                }
                OnChanged();
            }
        }

        decimal _DiscountAmount;
        public decimal DiscountAmount
        {
            get { return _DiscountAmount; }
            set
            {
                
                _DiscountAmount=value;
                var col=tbl.Columns.SingleOrDefault(x=>x.Name=="DiscountAmount");
                if(col!=null){
                    if(!_dirtyColumns.Contains(col) && _isLoaded){
                        _dirtyColumns.Add(col);
                    }
                }
                OnChanged();
            }
        }

        string _DiscountReason;
        public string DiscountReason
        {
            get { return _DiscountReason; }
            set
            {
                
                _DiscountReason=value;
                var col=tbl.Columns.SingleOrDefault(x=>x.Name=="DiscountReason");
                if(col!=null){
                    if(!_dirtyColumns.Contains(col) && _isLoaded){
                        _dirtyColumns.Add(col);
                    }
                }
                OnChanged();
            }
        }

        int? _ShippingAddressID;
        public int? ShippingAddressID
        {
            get { return _ShippingAddressID; }
            set
            {
                
                _ShippingAddressID=value;
                var col=tbl.Columns.SingleOrDefault(x=>x.Name=="ShippingAddressID");
                if(col!=null){
                    if(!_dirtyColumns.Contains(col) && _isLoaded){
                        _dirtyColumns.Add(col);
                    }
                }
                OnChanged();
            }
        }

        int? _BillingAddressID;
        public int? BillingAddressID
        {
            get { return _BillingAddressID; }
            set
            {
                
                _BillingAddressID=value;
                var col=tbl.Columns.SingleOrDefault(x=>x.Name=="BillingAddressID");
                if(col!=null){
                    if(!_dirtyColumns.Contains(col) && _isLoaded){
                        _dirtyColumns.Add(col);
                    }
                }
                OnChanged();
            }
        }

        DateTime? _DateShipped;
        public DateTime? DateShipped
        {
            get { return _DateShipped; }
            set
            {
                
                _DateShipped=value;
                var col=tbl.Columns.SingleOrDefault(x=>x.Name=="DateShipped");
                if(col!=null){
                    if(!_dirtyColumns.Contains(col) && _isLoaded){
                        _dirtyColumns.Add(col);
                    }
                }
                OnChanged();
            }
        }

        string _TrackingNumber;
        public string TrackingNumber
        {
            get { return _TrackingNumber; }
            set
            {
                
                _TrackingNumber=value;
                var col=tbl.Columns.SingleOrDefault(x=>x.Name=="TrackingNumber");
                if(col!=null){
                    if(!_dirtyColumns.Contains(col) && _isLoaded){
                        _dirtyColumns.Add(col);
                    }
                }
                OnChanged();
            }
        }

        DateTime? _EstimatedDelivery;
        public DateTime? EstimatedDelivery
        {
            get { return _EstimatedDelivery; }
            set
            {
                
                _EstimatedDelivery=value;
                var col=tbl.Columns.SingleOrDefault(x=>x.Name=="EstimatedDelivery");
                if(col!=null){
                    if(!_dirtyColumns.Contains(col) && _isLoaded){
                        _dirtyColumns.Add(col);
                    }
                }
                OnChanged();
            }
        }

        decimal _SubTotal;
        public decimal SubTotal
        {
            get { return _SubTotal; }
            set
            {
                
                _SubTotal=value;
                var col=tbl.Columns.SingleOrDefault(x=>x.Name=="SubTotal");
                if(col!=null){
                    if(!_dirtyColumns.Contains(col) && _isLoaded){
                        _dirtyColumns.Add(col);
                    }
                }
                OnChanged();
            }
        }

        int _OrderStatusID;
        public int OrderStatusID
        {
            get { return _OrderStatusID; }
            set
            {
                
                _OrderStatusID=value;
                var col=tbl.Columns.SingleOrDefault(x=>x.Name=="OrderStatusID");
                if(col!=null){
                    if(!_dirtyColumns.Contains(col) && _isLoaded){
                        _dirtyColumns.Add(col);
                    }
                }
                OnChanged();
            }
        }

        DateTime _CreatedOn;
        public DateTime CreatedOn
        {
            get { return _CreatedOn; }
            set
            {
                
                _CreatedOn=value;
                var col=tbl.Columns.SingleOrDefault(x=>x.Name=="CreatedOn");
                if(col!=null){
                    if(!_dirtyColumns.Contains(col) && _isLoaded){
                        _dirtyColumns.Add(col);
                    }
                }
                OnChanged();
            }
        }

        DateTime? _ExecutedOn;
        public DateTime? ExecutedOn
        {
            get { return _ExecutedOn; }
            set
            {
                
                _ExecutedOn=value;
                var col=tbl.Columns.SingleOrDefault(x=>x.Name=="ExecutedOn");
                if(col!=null){
                    if(!_dirtyColumns.Contains(col) && _isLoaded){
                        _dirtyColumns.Add(col);
                    }
                }
                OnChanged();
            }
        }

        DateTime _ModifiedOn;
        public DateTime ModifiedOn
        {
            get { return _ModifiedOn; }
            set
            {
                
                _ModifiedOn=value;
                var col=tbl.Columns.SingleOrDefault(x=>x.Name=="ModifiedOn");
                if(col!=null){
                    if(!_dirtyColumns.Contains(col) && _isLoaded){
                        _dirtyColumns.Add(col);
                    }
                }
                OnChanged();
            }
        }



        public DbCommand GetUpdateCommand() {
            if (!_dirtyColumns.Any(x => x.Name.ToLower() == "modifiedon")) {
               this.ModifiedOn=DateTime.Now;
            }            
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return _repo.BuildUpdateQuery(this).GetCommand().ToDbCommand();
            
        }
        public DbCommand GetInsertCommand() {
 
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else{
            
            this.CreatedOn=DateTime.Now;
            this.ModifiedOn=DateTime.Now;
            
            
                return _repo.BuildInsertQuery(this).GetCommand().ToDbCommand();
                
            }
        }
        
        public DbCommand GetDeleteCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return _repo.BuildDeleteQuery(this).GetCommand().ToDbCommand();
        }
        //persistence
        public void Save(){
            Save("");
        }
        
        public void Update(string userName){
            this.ModifiedOn=DateTime.Now;
            _repo.Update(this);
            OnSaved();
       }
        
        public void Add(string userName){
            
            this.CreatedOn=DateTime.Now;
            this.ModifiedOn=DateTime.Now;
            
            _repo.Add(this);
            OnSaved();
        }
        
        public void Save(string userName) {
            
           
            if (_isNew) {
                Add(userName);
                
            } else {
                Update(userName);
            }
            
        }

        public void Delete() {
                   
                 
            _repo.Delete(KeyValue());
            
                    }

        public static void Delete(object key) {
        
            var repo = new SubSonicRepository<Order>(new Kona.Data.KonaDB());

                   
                 
            repo.Delete(key);
            
                        
        }

        public static void Delete(Func<Order, bool> expression) {
            var repo = GetRepo();
            
                   
            
            repo.Delete(expression);
            
                    }

        

        public void Load(IDataReader rdr) {
            Load(rdr, true);
        }
        public void Load(IDataReader rdr, bool closeReader) {
            if (rdr.Read()) {

                try {
                    rdr.Load(this);
                    _isNew = false;
                    _isLoaded = true;
                } catch {
                    _isLoaded = false;
                    throw;
                }
            }else{
                _isLoaded = false;
            }

            if (closeReader)
                rdr.Dispose();
        }
        

    } 
    
    
    /// <summary>
    /// A class which represents the Categories table in the Kona Database.
    /// </summary>
    public partial class Category: IActiveRecord
    {
    
        #region Built-in testing
        static IList<Category> TestItems;
        static TestRepository<Category> _testRepo;
        
        static void SetTestRepo(){
            _testRepo = _testRepo ?? new TestRepository<Category>(new Kona.Data.KonaDB());
        }
        public static void ResetTestRepo(){
            _testRepo = null;
            SetTestRepo();
        }
        public static void Setup(List<Category> testlist){
            SetTestRepo();
            _testRepo._items = testlist;
        }
        public static void Setup(Category item) {
            SetTestRepo();
            _testRepo._items.Add(item);
        }
        public static void Setup(int testItems) {
            SetTestRepo();
            for(int i=0;i<testItems;i++){
                Category item=new Category();
                _testRepo._items.Add(item);
            }
        }
        
        public bool TestMode {
            get {
                return this._db.DataProvider.ConnectionString.Equals("test", StringComparison.InvariantCultureIgnoreCase);
            }
        }

        #endregion

        IRepository<Category> _repo;
        ITable tbl;
        bool _isNew;
        public bool IsNew(){
            return _isNew;
        }
        public void SetIsNew(bool isNew){
            _isNew=isNew;
        }
        public void SetIsLoaded(bool isLoaded){
            _isLoaded=isLoaded;
        }
        bool _isLoaded;
        public bool IsLoaded(){
            return _isLoaded;
        }
                
        List<IColumn> _dirtyColumns;
        public bool IsDirty(){
            return _dirtyColumns.Count>0;
        }
        
        public List<IColumn> GetDirtyColumns (){
            return _dirtyColumns;
        }

        Kona.Data.KonaDB _db;
        public Category() {
             _db=new Kona.Data.KonaDB();
            _dirtyColumns=new List<IColumn>();
            
            if(TestMode){
                Category.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<Category>(_db);
            }
            tbl=_repo.GetTable();
            _isNew = true;
            OnCreated();
        }
        
       
        partial void OnCreated();
            
        partial void OnLoaded();
        
        partial void OnSaved();
        
        partial void OnChanged();
        
        public IList<IColumn> Columns{
            get{
                return tbl.Columns;
            }
        }
        
        public Category(object key):this() {
            _isLoaded=_repo.Load(this,this.KeyName(),key);
            if(_isLoaded)
                OnLoaded();

        }

        public Category(Expression<Func<Category, bool>> expression):this() {
            _isLoaded=_repo.Load(this,expression);
            if(_isLoaded)
                OnLoaded();
        }
        
        public static SubSonic.Query.SqlQuery Select{
            get{
                var db=new Kona.Data.KonaDB();
                return db.Select.From<Category>();
            }
        }
        public static SubSonic.Query.SqlQuery SelectColumns(params string[] columns){
            var db=new Kona.Data.KonaDB();
            return new SubSonic.Query.Select(db.DataProvider, columns).From<Category>();
        }
        internal static IRepository<Category> GetRepo(){
            var db=new Kona.Data.KonaDB();
            IRepository<Category> _repo;
            
            if(db.TestMode){
                Category.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<Category>(db);
            }
            return _repo;
        }
        
        public static Category SingleOrDefault(Expression<Func<Category, bool>> expression) {
           
            var single= All().SingleOrDefault(expression);
            if (single != null){
                single.OnLoaded();
                single.SetIsLoaded(true);
                single.SetIsNew(false);
            }
            return single;
        }      
        public static bool Exists(Expression<Func<Category, bool>> expression) {
           
            return All().Any(expression);
        }        
        public static IList<Category> Find(Expression<Func<Category, bool>> expression) {
           
            return GetRepo().Find(expression).ToList();
        }
        public static IQueryable<Category> All() {
            return GetRepo().GetAll();
        }
        public static PagedList<Category> GetPaged<TKey>(Func<Category,TKey> orderby, int pageIndex, int pageSize){
            return GetRepo().GetPaged(orderby, pageIndex, pageSize);
        }
        public static PagedList<Category> GetPaged<TKey>(string sortBy, int pageIndex, int pageSize) {
            return GetRepo().GetPaged(sortBy, pageIndex, pageSize);
        }
        public static PagedList<Category> GetPaged<TKey>(int pageIndex, int pageSize) {
            return GetRepo().GetPaged(pageIndex, pageSize);
            
        }

        public string KeyName()
        {
            return "CategoryID";
        }

        public object KeyValue()
        {
            return this.CategoryID;
        }
        
        public void SetKeyValue(object value) {
            if (value != DBNull.Value) {
                var settable = value.ChangeTypeTo<int>();
                this.GetType().GetProperty(this.KeyName()).SetValue(this, settable, null);
            }
        }
        
        public override string ToString(){
            return this.DefaultImageFile.ToString();
        }

        public override bool Equals(object obj){
            if(obj.GetType()==typeof(Category)){
                Category compare=(Category)obj;
                int thisPk=(int)this.KeyValue();
                int comparePk=(int)compare.KeyValue();
                return thisPk.Equals(comparePk);
            }else{
                return base.Equals(obj);
            }
        }

        public string DescriptorValue()
        {
            return this.DefaultImageFile.ToString();
        }

        public string DescriptorColumn() {
            return "DefaultImageFile";
        }
        
        public static string GetDescriptorColumn()
        {
            return "DefaultImageFile";
        }
        
        #region ' Foreign Keys '
        IList<Categories_Product> _Categories_Products;
        public IList<Categories_Product> Categories_Products
        {
            get
            {
                
                  if(_Categories_Products==null){
                      var repo=Categories_Product.GetRepo();
                  
                       _Categories_Products = (from items in repo.GetAll()
                           where items.CategoryID == _CategoryID
                           select items).ToList();
                   }
                  return _Categories_Products;
            }
            set{
                _Categories_Products=value;
            }
        }

        IList<Categories_Widget> _Categories_Widgets;
        public IList<Categories_Widget> Categories_Widgets
        {
            get
            {
                
                  if(_Categories_Widgets==null){
                      var repo=Categories_Widget.GetRepo();
                  
                       _Categories_Widgets = (from items in repo.GetAll()
                           where items.CategoryID == _CategoryID
                           select items).ToList();
                   }
                  return _Categories_Widgets;
            }
            set{
                _Categories_Widgets=value;
            }
        }

        IList<CategoryLocalized> _CategoryLocalizeds;
        public IList<CategoryLocalized> CategoryLocalizeds
        {
            get
            {
                
                  if(_CategoryLocalizeds==null){
                      var repo=CategoryLocalized.GetRepo();
                  
                       _CategoryLocalizeds = (from items in repo.GetAll()
                           where items.CategoryID == _CategoryID
                           select items).ToList();
                   }
                  return _CategoryLocalizeds;
            }
            set{
                _CategoryLocalizeds=value;
            }
        }

        #endregion
        

        int _CategoryID;
        public int CategoryID
        {
            get { return _CategoryID; }
            set
            {
                
                _CategoryID=value;
                var col=tbl.Columns.SingleOrDefault(x=>x.Name=="CategoryID");
                if(col!=null){
                    if(!_dirtyColumns.Contains(col) && _isLoaded){
                        _dirtyColumns.Add(col);
                    }
                }
                OnChanged();
            }
        }

        int? _ParentID;
        public int? ParentID
        {
            get { return _ParentID; }
            set
            {
                
                _ParentID=value;
                var col=tbl.Columns.SingleOrDefault(x=>x.Name=="ParentID");
                if(col!=null){
                    if(!_dirtyColumns.Contains(col) && _isLoaded){
                        _dirtyColumns.Add(col);
                    }
                }
                OnChanged();
            }
        }

        bool _IsDefault;
        public bool IsDefault
        {
            get { return _IsDefault; }
            set
            {
                
                _IsDefault=value;
                var col=tbl.Columns.SingleOrDefault(x=>x.Name=="IsDefault");
                if(col!=null){
                    if(!_dirtyColumns.Contains(col) && _isLoaded){
                        _dirtyColumns.Add(col);
                    }
                }
                OnChanged();
            }
        }

        string _DefaultImageFile;
        public string DefaultImageFile
        {
            get { return _DefaultImageFile; }
            set
            {
                
                _DefaultImageFile=value;
                var col=tbl.Columns.SingleOrDefault(x=>x.Name=="DefaultImageFile");
                if(col!=null){
                    if(!_dirtyColumns.Contains(col) && _isLoaded){
                        _dirtyColumns.Add(col);
                    }
                }
                OnChanged();
            }
        }



        public DbCommand GetUpdateCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return _repo.BuildUpdateQuery(this).GetCommand().ToDbCommand();
            
        }
        public DbCommand GetInsertCommand() {
 
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else{
            
            
                return _repo.BuildInsertQuery(this).GetCommand().ToDbCommand();
                
            }
        }
        
        public DbCommand GetDeleteCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return _repo.BuildDeleteQuery(this).GetCommand().ToDbCommand();
        }
        //persistence
        public void Save(){
            Save("");
        }
        
        public void Update(string userName){
            _repo.Update(this);
            OnSaved();
       }
        
        public void Add(string userName){
            
            
            this.SetKeyValue(_repo.Add(this));
            OnSaved();
        }
        
        public void Save(string userName) {
            
           
            if (_isNew) {
                Add(userName);
                
            } else {
                Update(userName);
            }
            
        }

        public void Delete() {
                   
                 
            _repo.Delete(KeyValue());
            
                    }

        public static void Delete(object key) {
        
            var repo = new SubSonicRepository<Category>(new Kona.Data.KonaDB());

                   
                 
            repo.Delete(key);
            
                        
        }

        public static void Delete(Func<Category, bool> expression) {
            var repo = GetRepo();
            
                   
            
            repo.Delete(expression);
            
                    }

        

        public void Load(IDataReader rdr) {
            Load(rdr, true);
        }
        public void Load(IDataReader rdr, bool closeReader) {
            if (rdr.Read()) {

                try {
                    rdr.Load(this);
                    _isNew = false;
                    _isLoaded = true;
                } catch {
                    _isLoaded = false;
                    throw;
                }
            }else{
                _isLoaded = false;
            }

            if (closeReader)
                rdr.Dispose();
        }
        

    } 
    
    
    /// <summary>
    /// A class which represents the InventoryStatus table in the Kona Database.
    /// </summary>
    public partial class InventoryStatus: IActiveRecord
    {
    
        #region Built-in testing
        static IList<InventoryStatus> TestItems;
        static TestRepository<InventoryStatus> _testRepo;
        
        static void SetTestRepo(){
            _testRepo = _testRepo ?? new TestRepository<InventoryStatus>(new Kona.Data.KonaDB());
        }
        public static void ResetTestRepo(){
            _testRepo = null;
            SetTestRepo();
        }
        public static void Setup(List<InventoryStatus> testlist){
            SetTestRepo();
            _testRepo._items = testlist;
        }
        public static void Setup(InventoryStatus item) {
            SetTestRepo();
            _testRepo._items.Add(item);
        }
        public static void Setup(int testItems) {
            SetTestRepo();
            for(int i=0;i<testItems;i++){
                InventoryStatus item=new InventoryStatus();
                _testRepo._items.Add(item);
            }
        }
        
        public bool TestMode {
            get {
                return this._db.DataProvider.ConnectionString.Equals("test", StringComparison.InvariantCultureIgnoreCase);
            }
        }

        #endregion

        IRepository<InventoryStatus> _repo;
        ITable tbl;
        bool _isNew;
        public bool IsNew(){
            return _isNew;
        }
        public void SetIsNew(bool isNew){
            _isNew=isNew;
        }
        public void SetIsLoaded(bool isLoaded){
            _isLoaded=isLoaded;
        }
        bool _isLoaded;
        public bool IsLoaded(){
            return _isLoaded;
        }
                
        List<IColumn> _dirtyColumns;
        public bool IsDirty(){
            return _dirtyColumns.Count>0;
        }
        
        public List<IColumn> GetDirtyColumns (){
            return _dirtyColumns;
        }

        Kona.Data.KonaDB _db;
        public InventoryStatus() {
             _db=new Kona.Data.KonaDB();
            _dirtyColumns=new List<IColumn>();
            
            if(TestMode){
                InventoryStatus.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<InventoryStatus>(_db);
            }
            tbl=_repo.GetTable();
            _isNew = true;
            OnCreated();
        }
        
       
        partial void OnCreated();
            
        partial void OnLoaded();
        
        partial void OnSaved();
        
        partial void OnChanged();
        
        public IList<IColumn> Columns{
            get{
                return tbl.Columns;
            }
        }
        
        public InventoryStatus(object key):this() {
            _isLoaded=_repo.Load(this,this.KeyName(),key);
            if(_isLoaded)
                OnLoaded();

        }

        public InventoryStatus(Expression<Func<InventoryStatus, bool>> expression):this() {
            _isLoaded=_repo.Load(this,expression);
            if(_isLoaded)
                OnLoaded();
        }
        
        public static SubSonic.Query.SqlQuery Select{
            get{
                var db=new Kona.Data.KonaDB();
                return db.Select.From<InventoryStatus>();
            }
        }
        public static SubSonic.Query.SqlQuery SelectColumns(params string[] columns){
            var db=new Kona.Data.KonaDB();
            return new SubSonic.Query.Select(db.DataProvider, columns).From<InventoryStatus>();
        }
        internal static IRepository<InventoryStatus> GetRepo(){
            var db=new Kona.Data.KonaDB();
            IRepository<InventoryStatus> _repo;
            
            if(db.TestMode){
                InventoryStatus.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<InventoryStatus>(db);
            }
            return _repo;
        }
        
        public static InventoryStatus SingleOrDefault(Expression<Func<InventoryStatus, bool>> expression) {
           
            var single= All().SingleOrDefault(expression);
            if (single != null){
                single.OnLoaded();
                single.SetIsLoaded(true);
                single.SetIsNew(false);
            }
            return single;
        }      
        public static bool Exists(Expression<Func<InventoryStatus, bool>> expression) {
           
            return All().Any(expression);
        }        
        public static IList<InventoryStatus> Find(Expression<Func<InventoryStatus, bool>> expression) {
           
            return GetRepo().Find(expression).ToList();
        }
        public static IQueryable<InventoryStatus> All() {
            return GetRepo().GetAll();
        }
        public static PagedList<InventoryStatus> GetPaged<TKey>(Func<InventoryStatus,TKey> orderby, int pageIndex, int pageSize){
            return GetRepo().GetPaged(orderby, pageIndex, pageSize);
        }
        public static PagedList<InventoryStatus> GetPaged<TKey>(string sortBy, int pageIndex, int pageSize) {
            return GetRepo().GetPaged(sortBy, pageIndex, pageSize);
        }
        public static PagedList<InventoryStatus> GetPaged<TKey>(int pageIndex, int pageSize) {
            return GetRepo().GetPaged(pageIndex, pageSize);
            
        }

        public string KeyName()
        {
            return "InventoryStatusID";
        }

        public object KeyValue()
        {
            return this.InventoryStatusID;
        }
        
        public void SetKeyValue(object value) {
            if (value != DBNull.Value) {
                var settable = value.ChangeTypeTo<int>();
                this.GetType().GetProperty(this.KeyName()).SetValue(this, settable, null);
            }
        }
        
        public override string ToString(){
            return this.Description.ToString();
        }

        public override bool Equals(object obj){
            if(obj.GetType()==typeof(InventoryStatus)){
                InventoryStatus compare=(InventoryStatus)obj;
                int thisPk=(int)this.KeyValue();
                int comparePk=(int)compare.KeyValue();
                return thisPk.Equals(comparePk);
            }else{
                return base.Equals(obj);
            }
        }

        public string DescriptorValue()
        {
            return this.Description.ToString();
        }

        public string DescriptorColumn() {
            return "Description";
        }
        
        public static string GetDescriptorColumn()
        {
            return "Description";
        }
        
        #region ' Foreign Keys '
        IList<Product> _Products;
        public IList<Product> Products
        {
            get
            {
                
                  if(_Products==null){
                      var repo=Product.GetRepo();
                  
                       _Products = (from items in repo.GetAll()
                           where items.InventoryStatusID == _InventoryStatusID
                           select items).ToList();
                   }
                  return _Products;
            }
            set{
                _Products=value;
            }
        }

        #endregion
        

        int _InventoryStatusID;
        public int InventoryStatusID
        {
            get { return _InventoryStatusID; }
            set
            {
                
                _InventoryStatusID=value;
                var col=tbl.Columns.SingleOrDefault(x=>x.Name=="InventoryStatusID");
                if(col!=null){
                    if(!_dirtyColumns.Contains(col) && _isLoaded){
                        _dirtyColumns.Add(col);
                    }
                }
                OnChanged();
            }
        }

        string _Description;
        public string Description
        {
            get { return _Description; }
            set
            {
                
                _Description=value;
                var col=tbl.Columns.SingleOrDefault(x=>x.Name=="Description");
                if(col!=null){
                    if(!_dirtyColumns.Contains(col) && _isLoaded){
                        _dirtyColumns.Add(col);
                    }
                }
                OnChanged();
            }
        }



        public DbCommand GetUpdateCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return _repo.BuildUpdateQuery(this).GetCommand().ToDbCommand();
            
        }
        public DbCommand GetInsertCommand() {
 
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else{
            
            
                return _repo.BuildInsertQuery(this).GetCommand().ToDbCommand();
                
            }
        }
        
        public DbCommand GetDeleteCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return _repo.BuildDeleteQuery(this).GetCommand().ToDbCommand();
        }
        //persistence
        public void Save(){
            Save("");
        }
        
        public void Update(string userName){
            _repo.Update(this);
            OnSaved();
       }
        
        public void Add(string userName){
            
            
            this.SetKeyValue(_repo.Add(this));
            OnSaved();
        }
        
        public void Save(string userName) {
            
           
            if (_isNew) {
                Add(userName);
                
            } else {
                Update(userName);
            }
            
        }

        public void Delete() {
                   
                 
            _repo.Delete(KeyValue());
            
                    }

        public static void Delete(object key) {
        
            var repo = new SubSonicRepository<InventoryStatus>(new Kona.Data.KonaDB());

                   
                 
            repo.Delete(key);
            
                        
        }

        public static void Delete(Func<InventoryStatus, bool> expression) {
            var repo = GetRepo();
            
                   
            
            repo.Delete(expression);
            
                    }

        

        public void Load(IDataReader rdr) {
            Load(rdr, true);
        }
        public void Load(IDataReader rdr, bool closeReader) {
            if (rdr.Read()) {

                try {
                    rdr.Load(this);
                    _isNew = false;
                    _isLoaded = true;
                } catch {
                    _isLoaded = false;
                    throw;
                }
            }else{
                _isLoaded = false;
            }

            if (closeReader)
                rdr.Dispose();
        }
        

    } 
}
