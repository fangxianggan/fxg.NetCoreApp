
using NetCore.IRepository.UnitWork;
using NetCore.IRepository.Dapper.Enum;
using System;
using System.Data;
using System.Data.Common;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using StackExchange.Profiling;
using StackExchange.Profiling.Data;

namespace NetCore.Repository.UnitWork
{
    /// <summary>
    /// 工作单元
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(IConfiguration configuration)
        {
            var connStr = configuration.GetConnectionString("MySqlConnection");
            //if (!string.IsNullOrEmpty("MySQL Data Provider"))
            //    _providerName = "MySQL Data Provider";
            //else
            //    throw new Exception("ConnectionStrings中没有配置提供程序ProviderName！");
            //_dbFactory = DbProviderFactories.GetFactory(_providerName);
            //_connection = _dbFactory.CreateConnection();
            //_connection.ConnectionString = connStr;
            _connection = new ProfiledDbConnection(new MySqlConnection(connStr), MiniProfiler.Current);

            _connection.Open();
            SetParamPrefix();
        }

        private string _paramPrefix = "@";
        private readonly string _providerName = "System.Data.Odbc";
        private readonly DbProviderFactory _dbFactory;
        private DBType _dbType = DBType.MySql;
        private string _connectionStringName = "";

        public string ConnectionStringName { get { return _connectionStringName; } }
        public string ParamPrefix
        {
            get
            {
                return _paramPrefix;
            }
        }
        public string ProviderName
        {
            get
            {
                return _providerName;
            }
        }
        public DBType DbType
        {
            get
            {
                return _dbType;
            }
        }

        private bool _disposed;
        private IDbTransaction _trans = null;
        /// <summary>
        /// 事务
        /// </summary>
        public IDbTransaction DbTransaction { get { return _trans; } }

        private IDbConnection _connection;
        /// <summary>
        /// 数据连接
        /// </summary>
        public IDbConnection DbConnection { get { return _connection; } }

      
        private void SetParamPrefix()
        {
            string dbtype = (_dbFactory == null ? _connection.GetType() : _dbFactory.GetType()).Name;

            // 使用类型名判断
            if (dbtype.StartsWith("MySql")) _dbType = DBType.MySql;
            else if (dbtype.StartsWith("SqlCe")) _dbType = DBType.SqlServerCE;
            else if (dbtype.StartsWith("Npgsql")) _dbType = DBType.PostgreSQL;
            else if (dbtype.StartsWith("Oracle")) _dbType = DBType.Oracle;
            else if (dbtype.StartsWith("SQLite")) _dbType = DBType.SQLite;
            else if (dbtype.StartsWith("System.Data.SqlClient.")) _dbType = DBType.SqlServer;
            // else try with provider name
            else if (_providerName.IndexOf("MySql", StringComparison.InvariantCultureIgnoreCase) >= 0) _dbType = DBType.MySql;
            else if (_providerName.IndexOf("SqlServerCe", StringComparison.InvariantCultureIgnoreCase) >= 0) _dbType = DBType.SqlServerCE;
            else if (_providerName.IndexOf("Npgsql", StringComparison.InvariantCultureIgnoreCase) >= 0) _dbType = DBType.PostgreSQL;
            else if (_providerName.IndexOf("Oracle", StringComparison.InvariantCultureIgnoreCase) >= 0) _dbType = DBType.Oracle;
            else if (_providerName.IndexOf("SQLite", StringComparison.InvariantCultureIgnoreCase) >= 0) _dbType = DBType.SQLite;

            if (_dbType == DBType.MySql && _connection != null && _connection.ConnectionString != null && _connection.ConnectionString.IndexOf("Allow User Variables=true") >= 0)
                _paramPrefix = "?";
            if (_dbType == DBType.Oracle)
                _paramPrefix = ":";
        }
        /// <summary>
        /// 开启事务
        /// </summary>
        public void BeginTransaction()
        {
            _trans = _connection.BeginTransaction();
        }
        /// <summary>
        /// 完成事务
        /// </summary>
        public void Commit() => _trans?.Commit();
        /// <summary>
        /// 回滚事务
        /// </summary>
        public void Rollback() => _trans?.Rollback();

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~UnitOfWork() => Dispose(false);

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;
            if (disposing)
            {
                _trans?.Dispose();
                _connection?.Dispose();
            }
            _trans = null;
            _connection = null;
            _disposed = true;
        }
    }
}
