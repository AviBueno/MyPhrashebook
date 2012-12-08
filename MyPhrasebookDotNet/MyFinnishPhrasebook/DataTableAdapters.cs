using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SQLite;
using MyFinnishPhrasebookNamespace.Properties;

namespace MyFinnishPhrasebookNamespace.MPBDataSetTableAdaptersCustom {
    
    
    /// <summary>
    ///Represents the connection and commands used to retrieve and save data.
    ///</summary>
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
    [global::System.ComponentModel.DesignerCategoryAttribute("code")]
    [global::System.ComponentModel.ToolboxItem(true)]
    [global::System.ComponentModel.DataObjectAttribute(true)]
    [global::System.ComponentModel.DesignerAttribute("Microsoft.VSDesigner.DataSource.Design.TableAdapterDesigner, Microsoft.VSDesigner" +
        ", Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
    [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
    public partial class Cat2PhraseTableAdapter : global::System.ComponentModel.Component {
        
        private global::System.Data.SQLite.SQLiteDataAdapter _adapter;
        
        private global::System.Data.SQLite.SQLiteConnection _connection;
        
        private global::System.Data.SQLite.SQLiteTransaction _transaction;
        
        private global::System.Data.SQLite.SQLiteCommand[] _commandCollection;
        
        private bool _clearBeforeFill;
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Cat2PhraseTableAdapter() {
            this.ClearBeforeFill = true;
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        protected internal global::System.Data.SQLite.SQLiteDataAdapter Adapter {
            get {
                if ((this._adapter == null)) {
                    this.InitAdapter();
                }
                return this._adapter;
            }
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        internal global::System.Data.SQLite.SQLiteConnection Connection {
            get {
                if ((this._connection == null)) {
                    this.InitConnection();
                }
                return this._connection;
            }
            set {
                this._connection = value;
                if ((this.Adapter.InsertCommand != null)) {
                    this.Adapter.InsertCommand.Connection = value;
                }
                if ((this.Adapter.DeleteCommand != null)) {
                    this.Adapter.DeleteCommand.Connection = value;
                }
                if ((this.Adapter.UpdateCommand != null)) {
                    this.Adapter.UpdateCommand.Connection = value;
                }
                for (int i = 0; (i < this.CommandCollection.Length); i = (i + 1)) {
                    if ((this.CommandCollection[i] != null)) {
                        ((global::System.Data.SQLite.SQLiteCommand)(this.CommandCollection[i])).Connection = value;
                    }
                }
            }
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        internal global::System.Data.SQLite.SQLiteTransaction Transaction {
            get {
                return this._transaction;
            }
            set {
                this._transaction = value;
                for (int i = 0; (i < this.CommandCollection.Length); i = (i + 1)) {
                    this.CommandCollection[i].Transaction = this._transaction;
                }
                if (((this.Adapter != null) 
                            && (this.Adapter.DeleteCommand != null))) {
                    this.Adapter.DeleteCommand.Transaction = this._transaction;
                }
                if (((this.Adapter != null) 
                            && (this.Adapter.InsertCommand != null))) {
                    this.Adapter.InsertCommand.Transaction = this._transaction;
                }
                if (((this.Adapter != null) 
                            && (this.Adapter.UpdateCommand != null))) {
                    this.Adapter.UpdateCommand.Transaction = this._transaction;
                }
            }
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        protected global::System.Data.SQLite.SQLiteCommand[] CommandCollection {
            get {
                if ((this._commandCollection == null)) {
                    this.InitCommandCollection();
                }
                return this._commandCollection;
            }
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public bool ClearBeforeFill {
            get {
                return this._clearBeforeFill;
            }
            set {
                this._clearBeforeFill = value;
            }
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private void InitAdapter() {
            this._adapter = new global::System.Data.SQLite.SQLiteDataAdapter();
            global::System.Data.Common.DataTableMapping tableMapping = new global::System.Data.Common.DataTableMapping();
            tableMapping.SourceTable = "Table";
            tableMapping.DataSetTable = "Cat2Phrase";
            tableMapping.ColumnMappings.Add("_id", "_id");
            tableMapping.ColumnMappings.Add("_catID", "_catID");
            tableMapping.ColumnMappings.Add("_phraseID", "_phraseID");
            this._adapter.TableMappings.Add(tableMapping);
            this._adapter.DeleteCommand = new global::System.Data.SQLite.SQLiteCommand();
            this._adapter.DeleteCommand.Connection = this.Connection;
            this._adapter.DeleteCommand.CommandText = "DELETE FROM [Cat2Phrase] WHERE (([_id] = @Original__id) AND ([_catID] = @Original" +
                "__catID) AND ([_phraseID] = @Original__phraseID))";
            this._adapter.DeleteCommand.CommandType = global::System.Data.CommandType.Text;
            global::System.Data.SQLite.SQLiteParameter param = new global::System.Data.SQLite.SQLiteParameter();
            param.ParameterName = "@Original__id";
            param.DbType = global::System.Data.DbType.Int64;
            param.DbType = global::System.Data.DbType.Int64;
            param.SourceColumn = "_id";
            param.SourceVersion = global::System.Data.DataRowVersion.Original;
            this._adapter.DeleteCommand.Parameters.Add(param);
            param = new global::System.Data.SQLite.SQLiteParameter();
            param.ParameterName = "@Original__catID";
            param.DbType = global::System.Data.DbType.Int64;
            param.DbType = global::System.Data.DbType.Int64;
            param.SourceColumn = "_catID";
            param.SourceVersion = global::System.Data.DataRowVersion.Original;
            this._adapter.DeleteCommand.Parameters.Add(param);
            param = new global::System.Data.SQLite.SQLiteParameter();
            param.ParameterName = "@Original__phraseID";
            param.DbType = global::System.Data.DbType.Int64;
            param.DbType = global::System.Data.DbType.Int64;
            param.SourceColumn = "_phraseID";
            param.SourceVersion = global::System.Data.DataRowVersion.Original;
            this._adapter.DeleteCommand.Parameters.Add(param);
            this._adapter.InsertCommand = new global::System.Data.SQLite.SQLiteCommand();
            this._adapter.InsertCommand.Connection = this.Connection;
			this._adapter.InsertCommand.CommandText = "INSERT INTO [Cat2Phrase] ([_catID], [_phraseID]) VALUES (@_catID, @_phraseID); SELECT last_insert_rowid() AS _id;";
            this._adapter.InsertCommand.CommandType = global::System.Data.CommandType.Text;
            param = new global::System.Data.SQLite.SQLiteParameter();
            param.ParameterName = "@_catID";
            param.DbType = global::System.Data.DbType.Int64;
            param.DbType = global::System.Data.DbType.Int64;
            param.SourceColumn = "_catID";
            this._adapter.InsertCommand.Parameters.Add(param);
            param = new global::System.Data.SQLite.SQLiteParameter();
            param.ParameterName = "@_phraseID";
            param.DbType = global::System.Data.DbType.Int64;
            param.DbType = global::System.Data.DbType.Int64;
            param.SourceColumn = "_phraseID";
            this._adapter.InsertCommand.Parameters.Add(param);
            this._adapter.UpdateCommand = new global::System.Data.SQLite.SQLiteCommand();
            this._adapter.UpdateCommand.Connection = this.Connection;
            this._adapter.UpdateCommand.CommandText = "UPDATE [Cat2Phrase] SET [_catID] = @_catID, [_phraseID] = @_phraseID WHERE (([_id" +
                "] = @Original__id) AND ([_catID] = @Original__catID) AND ([_phraseID] = @Origina" +
                "l__phraseID))";
            this._adapter.UpdateCommand.CommandType = global::System.Data.CommandType.Text;
            param = new global::System.Data.SQLite.SQLiteParameter();
            param.ParameterName = "@_catID";
            param.DbType = global::System.Data.DbType.Int64;
            param.DbType = global::System.Data.DbType.Int64;
            param.SourceColumn = "_catID";
            this._adapter.UpdateCommand.Parameters.Add(param);
            param = new global::System.Data.SQLite.SQLiteParameter();
            param.ParameterName = "@_phraseID";
            param.DbType = global::System.Data.DbType.Int64;
            param.DbType = global::System.Data.DbType.Int64;
            param.SourceColumn = "_phraseID";
            this._adapter.UpdateCommand.Parameters.Add(param);
            param = new global::System.Data.SQLite.SQLiteParameter();
            param.ParameterName = "@Original__id";
            param.DbType = global::System.Data.DbType.Int64;
            param.DbType = global::System.Data.DbType.Int64;
            param.SourceColumn = "_id";
            param.SourceVersion = global::System.Data.DataRowVersion.Original;
            this._adapter.UpdateCommand.Parameters.Add(param);
            param = new global::System.Data.SQLite.SQLiteParameter();
            param.ParameterName = "@Original__catID";
            param.DbType = global::System.Data.DbType.Int64;
            param.DbType = global::System.Data.DbType.Int64;
            param.SourceColumn = "_catID";
            param.SourceVersion = global::System.Data.DataRowVersion.Original;
            this._adapter.UpdateCommand.Parameters.Add(param);
            param = new global::System.Data.SQLite.SQLiteParameter();
            param.ParameterName = "@Original__phraseID";
            param.DbType = global::System.Data.DbType.Int64;
            param.DbType = global::System.Data.DbType.Int64;
            param.SourceColumn = "_phraseID";
            param.SourceVersion = global::System.Data.DataRowVersion.Original;
            this._adapter.UpdateCommand.Parameters.Add(param);
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private void InitConnection() {
            this._connection = new global::System.Data.SQLite.SQLiteConnection();
            this._connection.ConnectionString = global::MyFinnishPhrasebookNamespace.Properties.Settings.Default.MPBConnectionString;
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private void InitCommandCollection() {
            this._commandCollection = new global::System.Data.SQLite.SQLiteCommand[1];
            this._commandCollection[0] = new global::System.Data.SQLite.SQLiteCommand();
            this._commandCollection[0].Connection = this.Connection;
            this._commandCollection[0].CommandText = "SELECT [_id], [_catID], [_phraseID] FROM [Cat2Phrase]";
            this._commandCollection[0].CommandType = global::System.Data.CommandType.Text;
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        [global::System.ComponentModel.DataObjectMethodAttribute(global::System.ComponentModel.DataObjectMethodType.Fill, true)]
        public virtual int Fill(MPBDataSet.Cat2PhraseDataTable dataTable) {
            this.Adapter.SelectCommand = this.CommandCollection[0];
            if ((this.ClearBeforeFill == true)) {
                dataTable.Clear();
            }
            int returnValue = this.Adapter.Fill(dataTable);
            return returnValue;
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        [global::System.ComponentModel.DataObjectMethodAttribute(global::System.ComponentModel.DataObjectMethodType.Select, true)]
        public virtual MPBDataSet.Cat2PhraseDataTable GetData() {
            this.Adapter.SelectCommand = this.CommandCollection[0];
            MPBDataSet.Cat2PhraseDataTable dataTable = new MPBDataSet.Cat2PhraseDataTable();
            this.Adapter.Fill(dataTable);
            return dataTable;
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        public virtual int Update(MPBDataSet.Cat2PhraseDataTable dataTable) {
            return this.Adapter.Update(dataTable);
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        public virtual int Update(MPBDataSet dataSet) {
            return this.Adapter.Update(dataSet, "Cat2Phrase");
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        public virtual int Update(global::System.Data.DataRow dataRow) {
            return this.Adapter.Update(new global::System.Data.DataRow[] {
                        dataRow});
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        public virtual int Update(global::System.Data.DataRow[] dataRows) {
            return this.Adapter.Update(dataRows);
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        [global::System.ComponentModel.DataObjectMethodAttribute(global::System.ComponentModel.DataObjectMethodType.Delete, true)]
        public virtual int Delete(long Original__id, long Original__catID, long Original__phraseID) {
            this.Adapter.DeleteCommand.Parameters[0].Value = ((long)(Original__id));
            this.Adapter.DeleteCommand.Parameters[1].Value = ((long)(Original__catID));
            this.Adapter.DeleteCommand.Parameters[2].Value = ((long)(Original__phraseID));
            global::System.Data.ConnectionState previousConnectionState = this.Adapter.DeleteCommand.Connection.State;
            if (((this.Adapter.DeleteCommand.Connection.State & global::System.Data.ConnectionState.Open) 
                        != global::System.Data.ConnectionState.Open)) {
                this.Adapter.DeleteCommand.Connection.Open();
            }
            try {
                int returnValue = this.Adapter.DeleteCommand.ExecuteNonQuery();
                return returnValue;
			}
            finally {
                if ((previousConnectionState == global::System.Data.ConnectionState.Closed)) {
                    this.Adapter.DeleteCommand.Connection.Close();
                }
            }
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        [global::System.ComponentModel.DataObjectMethodAttribute(global::System.ComponentModel.DataObjectMethodType.Insert, true)]
        public virtual int Insert(long _catID, long _phraseID) {
            this.Adapter.InsertCommand.Parameters[0].Value = ((long)(_catID));
            this.Adapter.InsertCommand.Parameters[1].Value = ((long)(_phraseID));
            global::System.Data.ConnectionState previousConnectionState = this.Adapter.InsertCommand.Connection.State;
            if (((this.Adapter.InsertCommand.Connection.State & global::System.Data.ConnectionState.Open) 
                        != global::System.Data.ConnectionState.Open)) {
                this.Adapter.InsertCommand.Connection.Open();
            }
            try {
				long returnValue = (long)this.Adapter.InsertCommand.ExecuteScalar();
                return (int)returnValue;
            }
            finally {
                if ((previousConnectionState == global::System.Data.ConnectionState.Closed)) {
                    this.Adapter.InsertCommand.Connection.Close();
                }
            }
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        [global::System.ComponentModel.DataObjectMethodAttribute(global::System.ComponentModel.DataObjectMethodType.Update, true)]
        public virtual int Update(long _catID, long _phraseID, long Original__id, long Original__catID, long Original__phraseID) {
            this.Adapter.UpdateCommand.Parameters[0].Value = ((long)(_catID));
            this.Adapter.UpdateCommand.Parameters[1].Value = ((long)(_phraseID));
            this.Adapter.UpdateCommand.Parameters[2].Value = ((long)(Original__id));
            this.Adapter.UpdateCommand.Parameters[3].Value = ((long)(Original__catID));
            this.Adapter.UpdateCommand.Parameters[4].Value = ((long)(Original__phraseID));
            global::System.Data.ConnectionState previousConnectionState = this.Adapter.UpdateCommand.Connection.State;
            if (((this.Adapter.UpdateCommand.Connection.State & global::System.Data.ConnectionState.Open) 
                        != global::System.Data.ConnectionState.Open)) {
                this.Adapter.UpdateCommand.Connection.Open();
            }
            try {
                int returnValue = this.Adapter.UpdateCommand.ExecuteNonQuery();
                return returnValue;
            }
            finally {
                if ((previousConnectionState == global::System.Data.ConnectionState.Closed)) {
                    this.Adapter.UpdateCommand.Connection.Close();
                }
            }
        }
    }
    
    /// <summary>
    ///Represents the connection and commands used to retrieve and save data.
    ///</summary>
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
    [global::System.ComponentModel.DesignerCategoryAttribute("code")]
    [global::System.ComponentModel.ToolboxItem(true)]
    [global::System.ComponentModel.DataObjectAttribute(true)]
    [global::System.ComponentModel.DesignerAttribute("Microsoft.VSDesigner.DataSource.Design.TableAdapterDesigner, Microsoft.VSDesigner" +
        ", Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
    [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
    public partial class CategoriesTableAdapter : global::System.ComponentModel.Component {
        
        private global::System.Data.SQLite.SQLiteDataAdapter _adapter;
        
        private global::System.Data.SQLite.SQLiteConnection _connection;
        
        private global::System.Data.SQLite.SQLiteTransaction _transaction;
        
        private global::System.Data.SQLite.SQLiteCommand[] _commandCollection;
        
        private bool _clearBeforeFill;
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public CategoriesTableAdapter() {
            this.ClearBeforeFill = true;
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        protected internal global::System.Data.SQLite.SQLiteDataAdapter Adapter {
            get {
                if ((this._adapter == null)) {
                    this.InitAdapter();
                }
                return this._adapter;
            }
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        internal global::System.Data.SQLite.SQLiteConnection Connection {
            get {
                if ((this._connection == null)) {
                    this.InitConnection();
                }
                return this._connection;
            }
            set {
                this._connection = value;
                if ((this.Adapter.InsertCommand != null)) {
                    this.Adapter.InsertCommand.Connection = value;
                }
                if ((this.Adapter.DeleteCommand != null)) {
                    this.Adapter.DeleteCommand.Connection = value;
                }
                if ((this.Adapter.UpdateCommand != null)) {
                    this.Adapter.UpdateCommand.Connection = value;
                }
                for (int i = 0; (i < this.CommandCollection.Length); i = (i + 1)) {
                    if ((this.CommandCollection[i] != null)) {
                        ((global::System.Data.SQLite.SQLiteCommand)(this.CommandCollection[i])).Connection = value;
                    }
                }
            }
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        internal global::System.Data.SQLite.SQLiteTransaction Transaction {
            get {
                return this._transaction;
            }
            set {
                this._transaction = value;
                for (int i = 0; (i < this.CommandCollection.Length); i = (i + 1)) {
                    this.CommandCollection[i].Transaction = this._transaction;
                }
                if (((this.Adapter != null) 
                            && (this.Adapter.DeleteCommand != null))) {
                    this.Adapter.DeleteCommand.Transaction = this._transaction;
                }
                if (((this.Adapter != null) 
                            && (this.Adapter.InsertCommand != null))) {
                    this.Adapter.InsertCommand.Transaction = this._transaction;
                }
                if (((this.Adapter != null) 
                            && (this.Adapter.UpdateCommand != null))) {
                    this.Adapter.UpdateCommand.Transaction = this._transaction;
                }
            }
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        protected global::System.Data.SQLite.SQLiteCommand[] CommandCollection {
            get {
                if ((this._commandCollection == null)) {
                    this.InitCommandCollection();
                }
                return this._commandCollection;
            }
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public bool ClearBeforeFill {
            get {
                return this._clearBeforeFill;
            }
            set {
                this._clearBeforeFill = value;
            }
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private void InitAdapter() {
            this._adapter = new global::System.Data.SQLite.SQLiteDataAdapter();
            global::System.Data.Common.DataTableMapping tableMapping = new global::System.Data.Common.DataTableMapping();
            tableMapping.SourceTable = "Table";
            tableMapping.DataSetTable = "Categories";
            tableMapping.ColumnMappings.Add("_id", "_id");
            tableMapping.ColumnMappings.Add("_name", "_name");
            tableMapping.ColumnMappings.Add("_title", "_title");
            this._adapter.TableMappings.Add(tableMapping);
            this._adapter.DeleteCommand = new global::System.Data.SQLite.SQLiteCommand();
            this._adapter.DeleteCommand.Connection = this.Connection;
            this._adapter.DeleteCommand.CommandText = "DELETE FROM [Categories] WHERE (([_id] = @Original__id) AND ([_name] = @Original_" +
                "_name) AND ([_title] = @Original__title))";
            this._adapter.DeleteCommand.CommandType = global::System.Data.CommandType.Text;
            global::System.Data.SQLite.SQLiteParameter param = new global::System.Data.SQLite.SQLiteParameter();
            param.ParameterName = "@Original__id";
            param.DbType = global::System.Data.DbType.Int64;
            param.DbType = global::System.Data.DbType.Int64;
            param.SourceColumn = "_id";
            param.SourceVersion = global::System.Data.DataRowVersion.Original;
            this._adapter.DeleteCommand.Parameters.Add(param);
            param = new global::System.Data.SQLite.SQLiteParameter();
            param.ParameterName = "@Original__name";
            param.DbType = global::System.Data.DbType.String;
            param.SourceColumn = "_name";
            param.SourceVersion = global::System.Data.DataRowVersion.Original;
            this._adapter.DeleteCommand.Parameters.Add(param);
            param = new global::System.Data.SQLite.SQLiteParameter();
            param.ParameterName = "@Original__title";
            param.DbType = global::System.Data.DbType.String;
            param.SourceColumn = "_title";
            param.SourceVersion = global::System.Data.DataRowVersion.Original;
            this._adapter.DeleteCommand.Parameters.Add(param);
            this._adapter.InsertCommand = new global::System.Data.SQLite.SQLiteCommand();
            this._adapter.InsertCommand.Connection = this.Connection;
            this._adapter.InsertCommand.CommandText = "INSERT INTO [Categories] ([_name], [_title]) VALUES (@_name, @_title); SELECT last_insert_rowid() AS _id;";
            this._adapter.InsertCommand.CommandType = global::System.Data.CommandType.Text;
            param = new global::System.Data.SQLite.SQLiteParameter();
            param.ParameterName = "@_name";
            param.DbType = global::System.Data.DbType.String;
            param.SourceColumn = "_name";
            this._adapter.InsertCommand.Parameters.Add(param);
            param = new global::System.Data.SQLite.SQLiteParameter();
            param.ParameterName = "@_title";
            param.DbType = global::System.Data.DbType.String;
            param.SourceColumn = "_title";
            this._adapter.InsertCommand.Parameters.Add(param);
            this._adapter.UpdateCommand = new global::System.Data.SQLite.SQLiteCommand();
            this._adapter.UpdateCommand.Connection = this.Connection;
            this._adapter.UpdateCommand.CommandText = "UPDATE [Categories] SET [_name] = @_name, [_title] = @_title WHERE (([_id] = @Ori" +
                "ginal__id) AND ([_name] = @Original__name) AND ([_title] = @Original__title))";
            this._adapter.UpdateCommand.CommandType = global::System.Data.CommandType.Text;
            param = new global::System.Data.SQLite.SQLiteParameter();
            param.ParameterName = "@_name";
            param.DbType = global::System.Data.DbType.String;
            param.SourceColumn = "_name";
            this._adapter.UpdateCommand.Parameters.Add(param);
            param = new global::System.Data.SQLite.SQLiteParameter();
            param.ParameterName = "@_title";
            param.DbType = global::System.Data.DbType.String;
            param.SourceColumn = "_title";
            this._adapter.UpdateCommand.Parameters.Add(param);
            param = new global::System.Data.SQLite.SQLiteParameter();
            param.ParameterName = "@Original__id";
            param.DbType = global::System.Data.DbType.Int64;
            param.DbType = global::System.Data.DbType.Int64;
            param.SourceColumn = "_id";
            param.SourceVersion = global::System.Data.DataRowVersion.Original;
            this._adapter.UpdateCommand.Parameters.Add(param);
            param = new global::System.Data.SQLite.SQLiteParameter();
            param.ParameterName = "@Original__name";
            param.DbType = global::System.Data.DbType.String;
            param.SourceColumn = "_name";
            param.SourceVersion = global::System.Data.DataRowVersion.Original;
            this._adapter.UpdateCommand.Parameters.Add(param);
            param = new global::System.Data.SQLite.SQLiteParameter();
            param.ParameterName = "@Original__title";
            param.DbType = global::System.Data.DbType.String;
            param.SourceColumn = "_title";
            param.SourceVersion = global::System.Data.DataRowVersion.Original;
            this._adapter.UpdateCommand.Parameters.Add(param);
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private void InitConnection() {
            this._connection = new global::System.Data.SQLite.SQLiteConnection();
            this._connection.ConnectionString = global::MyFinnishPhrasebookNamespace.Properties.Settings.Default.MPBConnectionString;
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private void InitCommandCollection() {
            this._commandCollection = new global::System.Data.SQLite.SQLiteCommand[1];
            this._commandCollection[0] = new global::System.Data.SQLite.SQLiteCommand();
            this._commandCollection[0].Connection = this.Connection;
            this._commandCollection[0].CommandText = "SELECT [_id], [_name], [_title] FROM [Categories]";
            this._commandCollection[0].CommandType = global::System.Data.CommandType.Text;
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        [global::System.ComponentModel.DataObjectMethodAttribute(global::System.ComponentModel.DataObjectMethodType.Fill, true)]
        public virtual int Fill(MPBDataSet.CategoriesDataTable dataTable) {
            this.Adapter.SelectCommand = this.CommandCollection[0];
            if ((this.ClearBeforeFill == true)) {
                dataTable.Clear();
            }
            int returnValue = this.Adapter.Fill(dataTable);
            return returnValue;
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        [global::System.ComponentModel.DataObjectMethodAttribute(global::System.ComponentModel.DataObjectMethodType.Select, true)]
        public virtual MPBDataSet.CategoriesDataTable GetData() {
            this.Adapter.SelectCommand = this.CommandCollection[0];
            MPBDataSet.CategoriesDataTable dataTable = new MPBDataSet.CategoriesDataTable();
            this.Adapter.Fill(dataTable);
            return dataTable;
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        public virtual int Update(MPBDataSet.CategoriesDataTable dataTable) {
            return this.Adapter.Update(dataTable);
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        public virtual int Update(MPBDataSet dataSet) {
            return this.Adapter.Update(dataSet, "Categories");
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        public virtual int Update(global::System.Data.DataRow dataRow) {
            return this.Adapter.Update(new global::System.Data.DataRow[] {
                        dataRow});
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        public virtual int Update(global::System.Data.DataRow[] dataRows) {
            return this.Adapter.Update(dataRows);
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        [global::System.ComponentModel.DataObjectMethodAttribute(global::System.ComponentModel.DataObjectMethodType.Delete, true)]
        public virtual int Delete(long Original__id, string Original__name, string Original__title) {
            this.Adapter.DeleteCommand.Parameters[0].Value = ((long)(Original__id));
            if ((Original__name == null)) {
                throw new global::System.ArgumentNullException("Original__name");
            }
            else {
                this.Adapter.DeleteCommand.Parameters[1].Value = ((string)(Original__name));
            }
            if ((Original__title == null)) {
                throw new global::System.ArgumentNullException("Original__title");
            }
            else {
                this.Adapter.DeleteCommand.Parameters[2].Value = ((string)(Original__title));
            }
            global::System.Data.ConnectionState previousConnectionState = this.Adapter.DeleteCommand.Connection.State;
            if (((this.Adapter.DeleteCommand.Connection.State & global::System.Data.ConnectionState.Open) 
                        != global::System.Data.ConnectionState.Open)) {
                this.Adapter.DeleteCommand.Connection.Open();
            }
            try {
                int returnValue = this.Adapter.DeleteCommand.ExecuteNonQuery();
                return returnValue;
			}
            finally {
                if ((previousConnectionState == global::System.Data.ConnectionState.Closed)) {
                    this.Adapter.DeleteCommand.Connection.Close();
                }
            }
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        [global::System.ComponentModel.DataObjectMethodAttribute(global::System.ComponentModel.DataObjectMethodType.Insert, true)]
        public virtual int Insert(string _name, string _title) {
            if ((_name == null)) {
                throw new global::System.ArgumentNullException("_name");
            }
            else {
                this.Adapter.InsertCommand.Parameters[0].Value = ((string)(_name));
            }
            if ((_title == null)) {
                throw new global::System.ArgumentNullException("_title");
            }
            else {
                this.Adapter.InsertCommand.Parameters[1].Value = ((string)(_title));
            }
            global::System.Data.ConnectionState previousConnectionState = this.Adapter.InsertCommand.Connection.State;
            if (((this.Adapter.InsertCommand.Connection.State & global::System.Data.ConnectionState.Open) 
                        != global::System.Data.ConnectionState.Open)) {
                this.Adapter.InsertCommand.Connection.Open();
            }
			try {
				long returnValue = (long)this.Adapter.InsertCommand.ExecuteScalar();
				return (int)returnValue;
			}
            finally {
                if ((previousConnectionState == global::System.Data.ConnectionState.Closed)) {
                    this.Adapter.InsertCommand.Connection.Close();
                }
            }
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        [global::System.ComponentModel.DataObjectMethodAttribute(global::System.ComponentModel.DataObjectMethodType.Update, true)]
        public virtual int Update(string _name, string _title, long Original__id, string Original__name, string Original__title) {
            if ((_name == null)) {
                throw new global::System.ArgumentNullException("_name");
            }
            else {
                this.Adapter.UpdateCommand.Parameters[0].Value = ((string)(_name));
            }
            if ((_title == null)) {
                throw new global::System.ArgumentNullException("_title");
            }
            else {
                this.Adapter.UpdateCommand.Parameters[1].Value = ((string)(_title));
            }
            this.Adapter.UpdateCommand.Parameters[2].Value = ((long)(Original__id));
            if ((Original__name == null)) {
                throw new global::System.ArgumentNullException("Original__name");
            }
            else {
                this.Adapter.UpdateCommand.Parameters[3].Value = ((string)(Original__name));
            }
            if ((Original__title == null)) {
                throw new global::System.ArgumentNullException("Original__title");
            }
            else {
                this.Adapter.UpdateCommand.Parameters[4].Value = ((string)(Original__title));
            }
            global::System.Data.ConnectionState previousConnectionState = this.Adapter.UpdateCommand.Connection.State;
            if (((this.Adapter.UpdateCommand.Connection.State & global::System.Data.ConnectionState.Open) 
                        != global::System.Data.ConnectionState.Open)) {
                this.Adapter.UpdateCommand.Connection.Open();
            }
            try {
                int returnValue = this.Adapter.UpdateCommand.ExecuteNonQuery();
                return returnValue;
			}
            finally {
                if ((previousConnectionState == global::System.Data.ConnectionState.Closed)) {
                    this.Adapter.UpdateCommand.Connection.Close();
                }
            }
        }
    }
    
    /// <summary>
    ///Represents the connection and commands used to retrieve and save data.
    ///</summary>
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
    [global::System.ComponentModel.DesignerCategoryAttribute("code")]
    [global::System.ComponentModel.ToolboxItem(true)]
    [global::System.ComponentModel.DataObjectAttribute(true)]
    [global::System.ComponentModel.DesignerAttribute("Microsoft.VSDesigner.DataSource.Design.TableAdapterDesigner, Microsoft.VSDesigner" +
        ", Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
    [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
    public partial class PhrasebookTableAdapter : global::System.ComponentModel.Component {
        
        private global::System.Data.SQLite.SQLiteDataAdapter _adapter;
        
        private global::System.Data.SQLite.SQLiteConnection _connection;
        
        private global::System.Data.SQLite.SQLiteTransaction _transaction;
        
        private global::System.Data.SQLite.SQLiteCommand[] _commandCollection;
        
        private bool _clearBeforeFill;
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public PhrasebookTableAdapter() {
            this.ClearBeforeFill = true;
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        protected internal global::System.Data.SQLite.SQLiteDataAdapter Adapter {
            get {
                if ((this._adapter == null)) {
                    this.InitAdapter();
                }
                return this._adapter;
            }
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        internal global::System.Data.SQLite.SQLiteConnection Connection {
            get {
                if ((this._connection == null)) {
                    this.InitConnection();
                }
                return this._connection;
            }
            set {
                this._connection = value;
                if ((this.Adapter.InsertCommand != null)) {
                    this.Adapter.InsertCommand.Connection = value;
                }
                if ((this.Adapter.DeleteCommand != null)) {
                    this.Adapter.DeleteCommand.Connection = value;
                }
                if ((this.Adapter.UpdateCommand != null)) {
                    this.Adapter.UpdateCommand.Connection = value;
                }
                for (int i = 0; (i < this.CommandCollection.Length); i = (i + 1)) {
                    if ((this.CommandCollection[i] != null)) {
                        ((global::System.Data.SQLite.SQLiteCommand)(this.CommandCollection[i])).Connection = value;
                    }
                }
            }
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        internal global::System.Data.SQLite.SQLiteTransaction Transaction {
            get {
                return this._transaction;
            }
            set {
                this._transaction = value;
                for (int i = 0; (i < this.CommandCollection.Length); i = (i + 1)) {
                    this.CommandCollection[i].Transaction = this._transaction;
                }
                if (((this.Adapter != null) 
                            && (this.Adapter.DeleteCommand != null))) {
                    this.Adapter.DeleteCommand.Transaction = this._transaction;
                }
                if (((this.Adapter != null) 
                            && (this.Adapter.InsertCommand != null))) {
                    this.Adapter.InsertCommand.Transaction = this._transaction;
                }
                if (((this.Adapter != null) 
                            && (this.Adapter.UpdateCommand != null))) {
                    this.Adapter.UpdateCommand.Transaction = this._transaction;
                }
            }
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        protected global::System.Data.SQLite.SQLiteCommand[] CommandCollection {
            get {
                if ((this._commandCollection == null)) {
                    this.InitCommandCollection();
                }
                return this._commandCollection;
            }
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public bool ClearBeforeFill {
            get {
                return this._clearBeforeFill;
            }
            set {
                this._clearBeforeFill = value;
            }
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private void InitAdapter() {
            this._adapter = new global::System.Data.SQLite.SQLiteDataAdapter();
            global::System.Data.Common.DataTableMapping tableMapping = new global::System.Data.Common.DataTableMapping();
            tableMapping.SourceTable = "Table";
            tableMapping.DataSetTable = "Phrasebook";
            tableMapping.ColumnMappings.Add("_id", "_id");
            tableMapping.ColumnMappings.Add("_english", "_english");
            tableMapping.ColumnMappings.Add("_language", "_language");
            this._adapter.TableMappings.Add(tableMapping);
            this._adapter.DeleteCommand = new global::System.Data.SQLite.SQLiteCommand();
            this._adapter.DeleteCommand.Connection = this.Connection;
            this._adapter.DeleteCommand.CommandText = "DELETE FROM [Phrasebook] WHERE (([_id] = @Original__id) AND ([_english] = @Origin" +
                "al__english) AND ([_language] = @Original__language))";
            this._adapter.DeleteCommand.CommandType = global::System.Data.CommandType.Text;
            global::System.Data.SQLite.SQLiteParameter param = new global::System.Data.SQLite.SQLiteParameter();
            param.ParameterName = "@Original__id";
            param.DbType = global::System.Data.DbType.Int64;
            param.DbType = global::System.Data.DbType.Int64;
            param.SourceColumn = "_id";
            param.SourceVersion = global::System.Data.DataRowVersion.Original;
            this._adapter.DeleteCommand.Parameters.Add(param);
            param = new global::System.Data.SQLite.SQLiteParameter();
            param.ParameterName = "@Original__english";
            param.DbType = global::System.Data.DbType.String;
            param.SourceColumn = "_english";
            param.SourceVersion = global::System.Data.DataRowVersion.Original;
            this._adapter.DeleteCommand.Parameters.Add(param);
            param = new global::System.Data.SQLite.SQLiteParameter();
            param.ParameterName = "@Original__language";
            param.DbType = global::System.Data.DbType.String;
            param.SourceColumn = "_language";
            param.SourceVersion = global::System.Data.DataRowVersion.Original;
            this._adapter.DeleteCommand.Parameters.Add(param);
            this._adapter.InsertCommand = new global::System.Data.SQLite.SQLiteCommand();
            this._adapter.InsertCommand.Connection = this.Connection;
			this._adapter.InsertCommand.CommandText = "INSERT INTO [Phrasebook] ([_english], [_language]) VALUES (@_english, @_language); SELECT last_insert_rowid() AS _id;";
            this._adapter.InsertCommand.CommandType = global::System.Data.CommandType.Text;
            param = new global::System.Data.SQLite.SQLiteParameter();
            param.ParameterName = "@_english";
            param.DbType = global::System.Data.DbType.String;
            param.SourceColumn = "_english";
            this._adapter.InsertCommand.Parameters.Add(param);
            param = new global::System.Data.SQLite.SQLiteParameter();
            param.ParameterName = "@_language";
            param.DbType = global::System.Data.DbType.String;
            param.SourceColumn = "_language";
            this._adapter.InsertCommand.Parameters.Add(param);
            this._adapter.UpdateCommand = new global::System.Data.SQLite.SQLiteCommand();
            this._adapter.UpdateCommand.Connection = this.Connection;
            this._adapter.UpdateCommand.CommandText = "UPDATE [Phrasebook] SET [_english] = @_english, [_language] = @_language WHERE ((" +
                "[_id] = @Original__id) AND ([_english] = @Original__english) AND ([_language] = " +
                "@Original__language))";
            this._adapter.UpdateCommand.CommandType = global::System.Data.CommandType.Text;
            param = new global::System.Data.SQLite.SQLiteParameter();
            param.ParameterName = "@_english";
            param.DbType = global::System.Data.DbType.String;
            param.SourceColumn = "_english";
            this._adapter.UpdateCommand.Parameters.Add(param);
            param = new global::System.Data.SQLite.SQLiteParameter();
            param.ParameterName = "@_language";
            param.DbType = global::System.Data.DbType.String;
            param.SourceColumn = "_language";
            this._adapter.UpdateCommand.Parameters.Add(param);
            param = new global::System.Data.SQLite.SQLiteParameter();
            param.ParameterName = "@Original__id";
            param.DbType = global::System.Data.DbType.Int64;
            param.DbType = global::System.Data.DbType.Int64;
            param.SourceColumn = "_id";
            param.SourceVersion = global::System.Data.DataRowVersion.Original;
            this._adapter.UpdateCommand.Parameters.Add(param);
            param = new global::System.Data.SQLite.SQLiteParameter();
            param.ParameterName = "@Original__english";
            param.DbType = global::System.Data.DbType.String;
            param.SourceColumn = "_english";
            param.SourceVersion = global::System.Data.DataRowVersion.Original;
            this._adapter.UpdateCommand.Parameters.Add(param);
            param = new global::System.Data.SQLite.SQLiteParameter();
            param.ParameterName = "@Original__language";
            param.DbType = global::System.Data.DbType.String;
            param.SourceColumn = "_language";
            param.SourceVersion = global::System.Data.DataRowVersion.Original;
            this._adapter.UpdateCommand.Parameters.Add(param);
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private void InitConnection() {
            this._connection = new global::System.Data.SQLite.SQLiteConnection();
            this._connection.ConnectionString = global::MyFinnishPhrasebookNamespace.Properties.Settings.Default.MPBConnectionString;
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private void InitCommandCollection() {
            this._commandCollection = new global::System.Data.SQLite.SQLiteCommand[1];
            this._commandCollection[0] = new global::System.Data.SQLite.SQLiteCommand();
            this._commandCollection[0].Connection = this.Connection;
            this._commandCollection[0].CommandText = "SELECT [_id], [_english], [_language] FROM [Phrasebook]";
            this._commandCollection[0].CommandType = global::System.Data.CommandType.Text;
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        [global::System.ComponentModel.DataObjectMethodAttribute(global::System.ComponentModel.DataObjectMethodType.Fill, true)]
        public virtual int Fill(MPBDataSet.PhrasebookDataTable dataTable) {
            this.Adapter.SelectCommand = this.CommandCollection[0];
            if ((this.ClearBeforeFill == true)) {
                dataTable.Clear();
            }
            int returnValue = this.Adapter.Fill(dataTable);
            return returnValue;
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        [global::System.ComponentModel.DataObjectMethodAttribute(global::System.ComponentModel.DataObjectMethodType.Select, true)]
        public virtual MPBDataSet.PhrasebookDataTable GetData() {
            this.Adapter.SelectCommand = this.CommandCollection[0];
            MPBDataSet.PhrasebookDataTable dataTable = new MPBDataSet.PhrasebookDataTable();
            this.Adapter.Fill(dataTable);
            return dataTable;
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        public virtual int Update(MPBDataSet.PhrasebookDataTable dataTable) {
            return this.Adapter.Update(dataTable);
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        public virtual int Update(MPBDataSet dataSet) {
            return this.Adapter.Update(dataSet, "Phrasebook");
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        public virtual int Update(global::System.Data.DataRow dataRow) {
            return this.Adapter.Update(new global::System.Data.DataRow[] {
                        dataRow});
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        public virtual int Update(global::System.Data.DataRow[] dataRows) {
            return this.Adapter.Update(dataRows);
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        [global::System.ComponentModel.DataObjectMethodAttribute(global::System.ComponentModel.DataObjectMethodType.Delete, true)]
        public virtual int Delete(long Original__id, string Original__english, string Original__language) {
            this.Adapter.DeleteCommand.Parameters[0].Value = ((long)(Original__id));
            if ((Original__english == null)) {
                throw new global::System.ArgumentNullException("Original__english");
            }
            else {
                this.Adapter.DeleteCommand.Parameters[1].Value = ((string)(Original__english));
            }
            if ((Original__language == null)) {
                throw new global::System.ArgumentNullException("Original__language");
            }
            else {
                this.Adapter.DeleteCommand.Parameters[2].Value = ((string)(Original__language));
            }
            global::System.Data.ConnectionState previousConnectionState = this.Adapter.DeleteCommand.Connection.State;
            if (((this.Adapter.DeleteCommand.Connection.State & global::System.Data.ConnectionState.Open) 
                        != global::System.Data.ConnectionState.Open)) {
                this.Adapter.DeleteCommand.Connection.Open();
            }
            try {
                int returnValue = this.Adapter.DeleteCommand.ExecuteNonQuery();
                return returnValue;
			}
            finally {
                if ((previousConnectionState == global::System.Data.ConnectionState.Closed)) {
                    this.Adapter.DeleteCommand.Connection.Close();
                }
            }
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        [global::System.ComponentModel.DataObjectMethodAttribute(global::System.ComponentModel.DataObjectMethodType.Insert, true)]
        public virtual int Insert(string _english, string _language) {
            if ((_english == null)) {
                throw new global::System.ArgumentNullException("_english");
            }
            else {
                this.Adapter.InsertCommand.Parameters[0].Value = ((string)(_english));
            }
            if ((_language == null)) {
                throw new global::System.ArgumentNullException("_language");
            }
            else {
                this.Adapter.InsertCommand.Parameters[1].Value = ((string)(_language));
            }
            global::System.Data.ConnectionState previousConnectionState = this.Adapter.InsertCommand.Connection.State;
            if (((this.Adapter.InsertCommand.Connection.State & global::System.Data.ConnectionState.Open) 
                        != global::System.Data.ConnectionState.Open)) {
                this.Adapter.InsertCommand.Connection.Open();
            }
            try {
				long returnValue = (long)this.Adapter.InsertCommand.ExecuteScalar();
                return (int)returnValue;
            }
            finally {
                if ((previousConnectionState == global::System.Data.ConnectionState.Closed)) {
                    this.Adapter.InsertCommand.Connection.Close();
                }
            }
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        [global::System.ComponentModel.DataObjectMethodAttribute(global::System.ComponentModel.DataObjectMethodType.Update, true)]
        public virtual int Update(string _english, string _language, long Original__id, string Original__english, string Original__language) {
            if ((_english == null)) {
                throw new global::System.ArgumentNullException("_english");
            }
            else {
                this.Adapter.UpdateCommand.Parameters[0].Value = ((string)(_english));
            }
            if ((_language == null)) {
                throw new global::System.ArgumentNullException("_language");
            }
            else {
                this.Adapter.UpdateCommand.Parameters[1].Value = ((string)(_language));
            }
            this.Adapter.UpdateCommand.Parameters[2].Value = ((long)(Original__id));
            if ((Original__english == null)) {
                throw new global::System.ArgumentNullException("Original__english");
            }
            else {
                this.Adapter.UpdateCommand.Parameters[3].Value = ((string)(Original__english));
            }
            if ((Original__language == null)) {
                throw new global::System.ArgumentNullException("Original__language");
            }
            else {
                this.Adapter.UpdateCommand.Parameters[4].Value = ((string)(Original__language));
            }
            global::System.Data.ConnectionState previousConnectionState = this.Adapter.UpdateCommand.Connection.State;
            if (((this.Adapter.UpdateCommand.Connection.State & global::System.Data.ConnectionState.Open) 
                        != global::System.Data.ConnectionState.Open)) {
                this.Adapter.UpdateCommand.Connection.Open();
            }
            try {
                int returnValue = this.Adapter.UpdateCommand.ExecuteNonQuery();
                return returnValue;
			}
            finally {
                if ((previousConnectionState == global::System.Data.ConnectionState.Closed)) {
                    this.Adapter.UpdateCommand.Connection.Close();
                }
            }
        }
    }
    
    /// <summary>
    ///TableAdapterManager is used to coordinate TableAdapters in the dataset to enable Hierarchical Update scenarios
    ///</summary>
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
    [global::System.ComponentModel.DesignerCategoryAttribute("code")]
    [global::System.ComponentModel.ToolboxItem(true)]
    [global::System.ComponentModel.DesignerAttribute("Microsoft.VSDesigner.DataSource.Design.TableAdapterManagerDesigner, Microsoft.VSD" +
        "esigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
    [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapterManager")]
    public partial class TableAdapterManager : global::System.ComponentModel.Component {
        
        private UpdateOrderOption _updateOrder;
        
        private Cat2PhraseTableAdapter _cat2PhraseTableAdapter;
        
        private CategoriesTableAdapter _categoriesTableAdapter;
        
        private PhrasebookTableAdapter _phrasebookTableAdapter;
        
        private bool _backupDataSetBeforeUpdate;
        
        private global::System.Data.IDbConnection _connection;
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public UpdateOrderOption UpdateOrder {
            get {
                return this._updateOrder;
            }
            set {
                this._updateOrder = value;
            }
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.ComponentModel.EditorAttribute("Microsoft.VSDesigner.DataSource.Design.TableAdapterManagerPropertyEditor, Microso" +
            "ft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" +
            "", "System.Drawing.Design.UITypeEditor")]
        public Cat2PhraseTableAdapter Cat2PhraseTableAdapter {
            get {
                return this._cat2PhraseTableAdapter;
            }
            set {
                this._cat2PhraseTableAdapter = value;
            }
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.ComponentModel.EditorAttribute("Microsoft.VSDesigner.DataSource.Design.TableAdapterManagerPropertyEditor, Microso" +
            "ft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" +
            "", "System.Drawing.Design.UITypeEditor")]
        public CategoriesTableAdapter CategoriesTableAdapter {
            get {
                return this._categoriesTableAdapter;
            }
            set {
                this._categoriesTableAdapter = value;
            }
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.ComponentModel.EditorAttribute("Microsoft.VSDesigner.DataSource.Design.TableAdapterManagerPropertyEditor, Microso" +
            "ft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" +
            "", "System.Drawing.Design.UITypeEditor")]
        public PhrasebookTableAdapter PhrasebookTableAdapter {
            get {
                return this._phrasebookTableAdapter;
            }
            set {
                this._phrasebookTableAdapter = value;
            }
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public bool BackupDataSetBeforeUpdate {
            get {
                return this._backupDataSetBeforeUpdate;
            }
            set {
                this._backupDataSetBeforeUpdate = value;
            }
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.ComponentModel.Browsable(false)]
        public global::System.Data.IDbConnection Connection {
            get {
                if ((this._connection != null)) {
                    return this._connection;
                }
                if (((this._cat2PhraseTableAdapter != null) 
                            && (this._cat2PhraseTableAdapter.Connection != null))) {
                    return this._cat2PhraseTableAdapter.Connection;
                }
                if (((this._categoriesTableAdapter != null) 
                            && (this._categoriesTableAdapter.Connection != null))) {
                    return this._categoriesTableAdapter.Connection;
                }
                if (((this._phrasebookTableAdapter != null) 
                            && (this._phrasebookTableAdapter.Connection != null))) {
                    return this._phrasebookTableAdapter.Connection;
                }
                return null;
            }
            set {
                this._connection = value;
            }
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.ComponentModel.Browsable(false)]
        public int TableAdapterInstanceCount {
            get {
                int count = 0;
                if ((this._cat2PhraseTableAdapter != null)) {
                    count = (count + 1);
                }
                if ((this._categoriesTableAdapter != null)) {
                    count = (count + 1);
                }
                if ((this._phrasebookTableAdapter != null)) {
                    count = (count + 1);
                }
                return count;
            }
        }
        
        /// <summary>
        ///Update rows in top-down order.
        ///</summary>
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private int UpdateUpdatedRows(MPBDataSet dataSet, global::System.Collections.Generic.List<global::System.Data.DataRow> allChangedRows, global::System.Collections.Generic.List<global::System.Data.DataRow> allAddedRows) {
            int result = 0;
            if ((this._phrasebookTableAdapter != null)) {
                global::System.Data.DataRow[] updatedRows = dataSet.Phrasebook.Select(null, null, global::System.Data.DataViewRowState.ModifiedCurrent);
                updatedRows = this.GetRealUpdatedRows(updatedRows, allAddedRows);
                if (((updatedRows != null) 
                            && (0 < updatedRows.Length))) {
                    result = (result + this._phrasebookTableAdapter.Update(updatedRows));
                    allChangedRows.AddRange(updatedRows);
                }
            }
            if ((this._categoriesTableAdapter != null)) {
                global::System.Data.DataRow[] updatedRows = dataSet.Categories.Select(null, null, global::System.Data.DataViewRowState.ModifiedCurrent);
                updatedRows = this.GetRealUpdatedRows(updatedRows, allAddedRows);
                if (((updatedRows != null) 
                            && (0 < updatedRows.Length))) {
                    result = (result + this._categoriesTableAdapter.Update(updatedRows));
                    allChangedRows.AddRange(updatedRows);
                }
            }
            if ((this._cat2PhraseTableAdapter != null)) {
                global::System.Data.DataRow[] updatedRows = dataSet.Cat2Phrase.Select(null, null, global::System.Data.DataViewRowState.ModifiedCurrent);
                updatedRows = this.GetRealUpdatedRows(updatedRows, allAddedRows);
                if (((updatedRows != null) 
                            && (0 < updatedRows.Length))) {
                    result = (result + this._cat2PhraseTableAdapter.Update(updatedRows));
                    allChangedRows.AddRange(updatedRows);
                }
            }
            return result;
        }
        
        /// <summary>
        ///Insert rows in top-down order.
        ///</summary>
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private int UpdateInsertedRows(MPBDataSet dataSet, global::System.Collections.Generic.List<global::System.Data.DataRow> allAddedRows) {
            int result = 0;
            if ((this._phrasebookTableAdapter != null)) {
                global::System.Data.DataRow[] addedRows = dataSet.Phrasebook.Select(null, null, global::System.Data.DataViewRowState.Added);
                if (((addedRows != null) 
                            && (0 < addedRows.Length))) {
                    result = (result + this._phrasebookTableAdapter.Update(addedRows));
                    allAddedRows.AddRange(addedRows);
                }
            }
            if ((this._categoriesTableAdapter != null)) {
                global::System.Data.DataRow[] addedRows = dataSet.Categories.Select(null, null, global::System.Data.DataViewRowState.Added);
                if (((addedRows != null) 
                            && (0 < addedRows.Length))) {
                    result = (result + this._categoriesTableAdapter.Update(addedRows));
                    allAddedRows.AddRange(addedRows);
                }
            }
            if ((this._cat2PhraseTableAdapter != null)) {
                global::System.Data.DataRow[] addedRows = dataSet.Cat2Phrase.Select(null, null, global::System.Data.DataViewRowState.Added);
                if (((addedRows != null) 
                            && (0 < addedRows.Length))) {
                    result = (result + this._cat2PhraseTableAdapter.Update(addedRows));
                    allAddedRows.AddRange(addedRows);
                }
            }
            return result;
        }
        
        /// <summary>
        ///Delete rows in bottom-up order.
        ///</summary>
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private int UpdateDeletedRows(MPBDataSet dataSet, global::System.Collections.Generic.List<global::System.Data.DataRow> allChangedRows) {
            int result = 0;
            if ((this._cat2PhraseTableAdapter != null)) {
                global::System.Data.DataRow[] deletedRows = dataSet.Cat2Phrase.Select(null, null, global::System.Data.DataViewRowState.Deleted);
                if (((deletedRows != null) 
                            && (0 < deletedRows.Length))) {
                    result = (result + this._cat2PhraseTableAdapter.Update(deletedRows));
                    allChangedRows.AddRange(deletedRows);
                }
            }
            if ((this._categoriesTableAdapter != null)) {
                global::System.Data.DataRow[] deletedRows = dataSet.Categories.Select(null, null, global::System.Data.DataViewRowState.Deleted);
                if (((deletedRows != null) 
                            && (0 < deletedRows.Length))) {
                    result = (result + this._categoriesTableAdapter.Update(deletedRows));
                    allChangedRows.AddRange(deletedRows);
                }
            }
            if ((this._phrasebookTableAdapter != null)) {
                global::System.Data.DataRow[] deletedRows = dataSet.Phrasebook.Select(null, null, global::System.Data.DataViewRowState.Deleted);
                if (((deletedRows != null) 
                            && (0 < deletedRows.Length))) {
                    result = (result + this._phrasebookTableAdapter.Update(deletedRows));
                    allChangedRows.AddRange(deletedRows);
                }
            }
            return result;
        }
        
        /// <summary>
        ///Remove inserted rows that become updated rows after calling TableAdapter.Update(inserted rows) first
        ///</summary>
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private global::System.Data.DataRow[] GetRealUpdatedRows(global::System.Data.DataRow[] updatedRows, global::System.Collections.Generic.List<global::System.Data.DataRow> allAddedRows) {
            if (((updatedRows == null) 
                        || (updatedRows.Length < 1))) {
                return updatedRows;
            }
            if (((allAddedRows == null) 
                        || (allAddedRows.Count < 1))) {
                return updatedRows;
            }
            global::System.Collections.Generic.List<global::System.Data.DataRow> realUpdatedRows = new global::System.Collections.Generic.List<global::System.Data.DataRow>();
            for (int i = 0; (i < updatedRows.Length); i = (i + 1)) {
                global::System.Data.DataRow row = updatedRows[i];
                if ((allAddedRows.Contains(row) == false)) {
                    realUpdatedRows.Add(row);
                }
            }
            return realUpdatedRows.ToArray();
        }
        
        /// <summary>
        ///Update all changes to the dataset.
        ///</summary>
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public virtual int UpdateAll(MPBDataSet dataSet) {
            if ((dataSet == null)) {
                throw new global::System.ArgumentNullException("dataSet");
            }
            if ((dataSet.HasChanges() == false)) {
                return 0;
            }
            if (((this._cat2PhraseTableAdapter != null) 
                        && (this.MatchTableAdapterConnection(this._cat2PhraseTableAdapter.Connection) == false))) {
                throw new global::System.ArgumentException("All TableAdapters managed by a TableAdapterManager must use the same connection s" +
                        "tring.");
            }
            if (((this._categoriesTableAdapter != null) 
                        && (this.MatchTableAdapterConnection(this._categoriesTableAdapter.Connection) == false))) {
                throw new global::System.ArgumentException("All TableAdapters managed by a TableAdapterManager must use the same connection s" +
                        "tring.");
            }
            if (((this._phrasebookTableAdapter != null) 
                        && (this.MatchTableAdapterConnection(this._phrasebookTableAdapter.Connection) == false))) {
                throw new global::System.ArgumentException("All TableAdapters managed by a TableAdapterManager must use the same connection s" +
                        "tring.");
            }
            global::System.Data.IDbConnection workConnection = this.Connection;
            if ((workConnection == null)) {
                throw new global::System.ApplicationException("TableAdapterManager contains no connection information. Set each TableAdapterMana" +
                        "ger TableAdapter property to a valid TableAdapter instance.");
            }
            bool workConnOpened = false;
            if (((workConnection.State & global::System.Data.ConnectionState.Broken) 
                        == global::System.Data.ConnectionState.Broken)) {
                workConnection.Close();
            }
            if ((workConnection.State == global::System.Data.ConnectionState.Closed)) {
                workConnection.Open();
                workConnOpened = true;
            }
            global::System.Data.IDbTransaction workTransaction = workConnection.BeginTransaction();
            if ((workTransaction == null)) {
                throw new global::System.ApplicationException("The transaction cannot begin. The current data connection does not support transa" +
                        "ctions or the current state is not allowing the transaction to begin.");
            }
            global::System.Collections.Generic.List<global::System.Data.DataRow> allChangedRows = new global::System.Collections.Generic.List<global::System.Data.DataRow>();
            global::System.Collections.Generic.List<global::System.Data.DataRow> allAddedRows = new global::System.Collections.Generic.List<global::System.Data.DataRow>();
            global::System.Collections.Generic.List<global::System.Data.Common.DataAdapter> adaptersWithAcceptChangesDuringUpdate = new global::System.Collections.Generic.List<global::System.Data.Common.DataAdapter>();
            global::System.Collections.Generic.Dictionary<object, global::System.Data.IDbConnection> revertConnections = new global::System.Collections.Generic.Dictionary<object, global::System.Data.IDbConnection>();
            int result = 0;
            global::System.Data.DataSet backupDataSet = null;
            if (this.BackupDataSetBeforeUpdate) {
                backupDataSet = new global::System.Data.DataSet();
                backupDataSet.Merge(dataSet);
            }
            try {
                // ---- Prepare for update -----------
                //
                if ((this._cat2PhraseTableAdapter != null)) {
                    revertConnections.Add(this._cat2PhraseTableAdapter, this._cat2PhraseTableAdapter.Connection);
                    this._cat2PhraseTableAdapter.Connection = ((global::System.Data.SQLite.SQLiteConnection)(workConnection));
                    this._cat2PhraseTableAdapter.Transaction = ((global::System.Data.SQLite.SQLiteTransaction)(workTransaction));
                    if (this._cat2PhraseTableAdapter.Adapter.AcceptChangesDuringUpdate) {
                        this._cat2PhraseTableAdapter.Adapter.AcceptChangesDuringUpdate = false;
                        adaptersWithAcceptChangesDuringUpdate.Add(this._cat2PhraseTableAdapter.Adapter);
                    }
                }
                if ((this._categoriesTableAdapter != null)) {
                    revertConnections.Add(this._categoriesTableAdapter, this._categoriesTableAdapter.Connection);
                    this._categoriesTableAdapter.Connection = ((global::System.Data.SQLite.SQLiteConnection)(workConnection));
                    this._categoriesTableAdapter.Transaction = ((global::System.Data.SQLite.SQLiteTransaction)(workTransaction));
                    if (this._categoriesTableAdapter.Adapter.AcceptChangesDuringUpdate) {
                        this._categoriesTableAdapter.Adapter.AcceptChangesDuringUpdate = false;
                        adaptersWithAcceptChangesDuringUpdate.Add(this._categoriesTableAdapter.Adapter);
                    }
                }
                if ((this._phrasebookTableAdapter != null)) {
                    revertConnections.Add(this._phrasebookTableAdapter, this._phrasebookTableAdapter.Connection);
                    this._phrasebookTableAdapter.Connection = ((global::System.Data.SQLite.SQLiteConnection)(workConnection));
                    this._phrasebookTableAdapter.Transaction = ((global::System.Data.SQLite.SQLiteTransaction)(workTransaction));
                    if (this._phrasebookTableAdapter.Adapter.AcceptChangesDuringUpdate) {
                        this._phrasebookTableAdapter.Adapter.AcceptChangesDuringUpdate = false;
                        adaptersWithAcceptChangesDuringUpdate.Add(this._phrasebookTableAdapter.Adapter);
                    }
                }
                // 
                //---- Perform updates -----------
                //
                if ((this.UpdateOrder == UpdateOrderOption.UpdateInsertDelete)) {
                    result = (result + this.UpdateUpdatedRows(dataSet, allChangedRows, allAddedRows));
                    result = (result + this.UpdateInsertedRows(dataSet, allAddedRows));
                }
                else {
                    result = (result + this.UpdateInsertedRows(dataSet, allAddedRows));
                    result = (result + this.UpdateUpdatedRows(dataSet, allChangedRows, allAddedRows));
                }
                result = (result + this.UpdateDeletedRows(dataSet, allChangedRows));
                // 
                //---- Commit updates -----------
                //
                workTransaction.Commit();
                if ((0 < allAddedRows.Count)) {
                    global::System.Data.DataRow[] rows = new System.Data.DataRow[allAddedRows.Count];
                    allAddedRows.CopyTo(rows);
                    for (int i = 0; (i < rows.Length); i = (i + 1)) {
                        global::System.Data.DataRow row = rows[i];
                        row.AcceptChanges();
                    }
                }
                if ((0 < allChangedRows.Count)) {
                    global::System.Data.DataRow[] rows = new System.Data.DataRow[allChangedRows.Count];
                    allChangedRows.CopyTo(rows);
                    for (int i = 0; (i < rows.Length); i = (i + 1)) {
                        global::System.Data.DataRow row = rows[i];
                        row.AcceptChanges();
                    }
                }
            }
            catch (global::System.Exception ex) {
                workTransaction.Rollback();
                // ---- Restore the dataset -----------
                if (this.BackupDataSetBeforeUpdate) {
                    global::System.Diagnostics.Debug.Assert((backupDataSet != null));
                    dataSet.Clear();
                    dataSet.Merge(backupDataSet);
                }
                else {
                    if ((0 < allAddedRows.Count)) {
                        global::System.Data.DataRow[] rows = new System.Data.DataRow[allAddedRows.Count];
                        allAddedRows.CopyTo(rows);
                        for (int i = 0; (i < rows.Length); i = (i + 1)) {
                            global::System.Data.DataRow row = rows[i];
                            row.AcceptChanges();
                            row.SetAdded();
                        }
                    }
                }
                throw ex;
            }
            finally {
                if (workConnOpened) {
                    workConnection.Close();
                }
                if ((this._cat2PhraseTableAdapter != null)) {
                    this._cat2PhraseTableAdapter.Connection = ((global::System.Data.SQLite.SQLiteConnection)(revertConnections[this._cat2PhraseTableAdapter]));
                    this._cat2PhraseTableAdapter.Transaction = null;
                }
                if ((this._categoriesTableAdapter != null)) {
                    this._categoriesTableAdapter.Connection = ((global::System.Data.SQLite.SQLiteConnection)(revertConnections[this._categoriesTableAdapter]));
                    this._categoriesTableAdapter.Transaction = null;
                }
                if ((this._phrasebookTableAdapter != null)) {
                    this._phrasebookTableAdapter.Connection = ((global::System.Data.SQLite.SQLiteConnection)(revertConnections[this._phrasebookTableAdapter]));
                    this._phrasebookTableAdapter.Transaction = null;
                }
                if ((0 < adaptersWithAcceptChangesDuringUpdate.Count)) {
                    global::System.Data.Common.DataAdapter[] adapters = new System.Data.Common.DataAdapter[adaptersWithAcceptChangesDuringUpdate.Count];
                    adaptersWithAcceptChangesDuringUpdate.CopyTo(adapters);
                    for (int i = 0; (i < adapters.Length); i = (i + 1)) {
                        global::System.Data.Common.DataAdapter adapter = adapters[i];
                        adapter.AcceptChangesDuringUpdate = true;
                    }
                }
            }
            return result;
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        protected virtual void SortSelfReferenceRows(global::System.Data.DataRow[] rows, global::System.Data.DataRelation relation, bool childFirst) {
            global::System.Array.Sort<global::System.Data.DataRow>(rows, new SelfReferenceComparer(relation, childFirst));
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        protected virtual bool MatchTableAdapterConnection(global::System.Data.IDbConnection inputConnection) {
            if ((this._connection != null)) {
                return true;
            }
            if (((this.Connection == null) 
                        || (inputConnection == null))) {
                return true;
            }
            if (string.Equals(this.Connection.ConnectionString, inputConnection.ConnectionString, global::System.StringComparison.Ordinal)) {
                return true;
            }
            return false;
        }
        
        /// <summary>
        ///Update Order Option
        ///</summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
        public enum UpdateOrderOption {
            
            InsertUpdateDelete = 0,
            
            UpdateInsertDelete = 1,
        }
        
        /// <summary>
        ///Used to sort self-referenced table's rows
        ///</summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
        private class SelfReferenceComparer : object, global::System.Collections.Generic.IComparer<global::System.Data.DataRow> {
            
            private global::System.Data.DataRelation _relation;
            
            private int _childFirst;
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            internal SelfReferenceComparer(global::System.Data.DataRelation relation, bool childFirst) {
                this._relation = relation;
                if (childFirst) {
                    this._childFirst = -1;
                }
                else {
                    this._childFirst = 1;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            private bool IsChildAndParent(global::System.Data.DataRow child, global::System.Data.DataRow parent) {
                global::System.Diagnostics.Debug.Assert((child != null));
                global::System.Diagnostics.Debug.Assert((parent != null));
                global::System.Data.DataRow newParent = child.GetParentRow(this._relation, global::System.Data.DataRowVersion.Default);
                for (
                ; ((newParent != null) 
                            && ((object.ReferenceEquals(newParent, child) == false) 
                            && (object.ReferenceEquals(newParent, parent) == false))); 
                ) {
                    newParent = newParent.GetParentRow(this._relation, global::System.Data.DataRowVersion.Default);
                }
                if ((newParent == null)) {
                    for (newParent = child.GetParentRow(this._relation, global::System.Data.DataRowVersion.Original); ((newParent != null) 
                                && ((object.ReferenceEquals(newParent, child) == false) 
                                && (object.ReferenceEquals(newParent, parent) == false))); 
                    ) {
                        newParent = newParent.GetParentRow(this._relation, global::System.Data.DataRowVersion.Original);
                    }
                }
                if (object.ReferenceEquals(newParent, parent)) {
                    return true;
                }
                return false;
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public int Compare(global::System.Data.DataRow row1, global::System.Data.DataRow row2) {
                if (object.ReferenceEquals(row1, row2)) {
                    return 0;
                }
                if ((row1 == null)) {
                    return -1;
                }
                if ((row2 == null)) {
                    return 1;
                }

                // Is row1 the child or grandchild of row2
                if (this.IsChildAndParent(row1, row2)) {
                    return this._childFirst;
                }

                // Is row2 the child or grandchild of row1
                if (this.IsChildAndParent(row2, row1)) {
                    return (-1 * this._childFirst);
                }
                return 0;
            }
        }
    }

	public class MyDBHelper
	{
		static MyDBHelper m_Instance = null;
		string m_sConn;

		public static MyDBHelper Instance
		{
			get
			{
				if ( m_Instance == null )
				{
					m_Instance = new MyDBHelper();

					string s = m_Instance.ExecuteScalar( "SELECT max(_id) FROM Phrasebook" );
				}

				return m_Instance;
			}
		}

		private MyDBHelper()
		{
			m_sConn = Properties.Settings.Default.MPBConnectionString.Replace( "|DataDirectory|", AppDomain.CurrentDomain.GetData( "DataDirectory" ) as string );
		}

		public DataTable GetDataTable( string sql )
		{
			DataTable dt = new DataTable();
			try
			{
				SQLiteConnection cnn = new SQLiteConnection( m_sConn );
				cnn.Open();
				SQLiteCommand mycommand = new SQLiteCommand( cnn );
				mycommand.CommandText = sql;
				SQLiteDataReader reader = mycommand.ExecuteReader();
				dt.Load( reader );
				reader.Close();
				cnn.Close();
			}
			catch
			{
				// Catching exceptions is for communists
			}
			return dt;
		}

		public int ExecuteNonQuery( string sql )
		{
			SQLiteConnection cnn = new SQLiteConnection( m_sConn );
			cnn.Open();
			SQLiteCommand mycommand = new SQLiteCommand( cnn );
			mycommand.CommandText = sql;
			int rowsUpdated = mycommand.ExecuteNonQuery();
			cnn.Close();
			return rowsUpdated;
		}

		public string ExecuteScalar( string sql )
		{
			SQLiteConnection cnn = new SQLiteConnection( m_sConn );
			cnn.Open();
			SQLiteCommand mycommand = new SQLiteCommand( cnn );
			mycommand.CommandText = sql;
			object value = mycommand.ExecuteScalar();
			cnn.Close();
			if ( value != null )
			{
				return value.ToString();
			}
			return "";
		}
	}
}


