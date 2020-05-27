using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace NetCore.UnitOfWork.Common
{
    /// <summary>
    /// 工作单元
    /// </summary>
    public class UnitOfWork :IUnitOfWork.Common.IUnitOfWork
    {

        private string _paramPrefix = "@";
        private readonly string _providerName = "System.Data.SqlClient";
        private readonly DbProviderFactory _dbFactory;
        private DBType _dbType = DBType.SqlServer;


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

        public UnitOfWork(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("SqlConnection");
            _connection = new SqlConnection(connectionString); //这里使用的mysql
            _connection.Open();
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
