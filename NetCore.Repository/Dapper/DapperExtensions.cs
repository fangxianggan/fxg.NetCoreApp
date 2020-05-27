using Dapper;
using NetCore.Core.Attributes;
using NetCore.Core.Extensions;
using NetCore.Core.Util;
using NetCore.Repository.Dapper.Entity;
using NetCore.IRepository.UnitWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCore.Repository.Dapper
{
    public static class DapperExtensions
    {

        //#region 写入日志
        ///// <summary>
        ///// 写入SqlLog日志
        ///// </summary>
        ///// <param name="log"></param>
        //public static void WriteSqlLog(SqlLog log)
        //{
        //    SqlLogHandler handler = new SqlLogHandler(log.OperateSql, log.EndDateTime, log.ElapsedTime, log.Parameter);
        //    handler.WriteLog();
        //}


        ///// <summary>
        ///// 写入数据日志
        ///// </summary>
        ///// <param name="operateType"></param>
        ///// <param name="tableName">表名</param>
        ///// <param name="current"></param>
        ///// <returns></returns>
        //private static void WriteDataLog(DataLog log)
        //{
        //    DataLogHandler _dataLoginHandler = new DataLogHandler(log.OperateType, log.OperateTable, log.OperationBefore, log.OperationAfterData);
        //    _dataLoginHandler.WriteLog();

        //}

        //#endregion

        #region 映射

        #region 插入

        /// <summary>
        ///     插入数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbs"></param>
        /// <param name="t"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public static async Task<bool> Insert<T>(this IUnitOfWork dbs, T t, IDbTransaction transaction = null, int? commandTimeout = null)
            where T : class
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var db = dbs.DbConnection;
            var sql = SqlQuery<T>.Builder(dbs);
            //SqlLog log = new SqlLog
            //{
            //    CreateTime = DateTime.Now,
            //    OperateSql = sql.InsertSql,
            //    Parameter = JsonConvert.SerializeObject(t)
            //};
            var result =await db.ExecuteAsync(sql.InsertSql, t, transaction, commandTimeout) > 0 ? true : false;
            stopwatch.Stop();
            //DataLog datalog = new DataLog
            //{
            //    OperateType = OperateType.Add.ToString(),
            //    OperateTable = sql._ModelDes.TableName,
            //    OperationBefore = "",
            //    OperationAfterData = JsonConvert.SerializeObject(t),
            //};
            //log.EndDateTime = DateTime.Now;
            //log.ElapsedTime = stopwatch.Elapsed.TotalSeconds;
            //WriteSqlLog(log);
            //WriteDataLog(datalog);
            return result;
        }

        /// <summary>
        ///     插入数据 返回自动递增字段的值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbs"></param>
        /// <param name="t"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public static async Task<int> InsertScalar<T>(this IUnitOfWork dbs, T t, IDbTransaction transaction = null,
            int? commandTimeout = null)
            where T : class
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var db = dbs.DbConnection;
            var sql = SqlQuery<T>.Builder(dbs);
            //SqlLog log = new SqlLog
            //{
            //    CreateTime = DateTime.Now,
            //    //返回自动递增字段的值
            //    OperateSql = sql.InsertSql + ";SELECT @@Identity"
            //};
            var result =await db.ExecuteScalarAsync<int>(sql.InsertSql + ";SELECT @@Identity", t, transaction, commandTimeout);
            stopwatch.Stop();
            //DataLog datalog = new DataLog
            //{
            //    OperateType = OperateType.Add.ToString(),
            //    OperateTable = sql._ModelDes.TableName,
            //    OperationBefore = "",
            //    OperationAfterData = JsonConvert.SerializeObject(t),
            //};
            //log.EndDateTime = DateTime.Now;
            //log.ElapsedTime = stopwatch.Elapsed.TotalSeconds;
            //WriteSqlLog(log);
            //WriteDataLog(datalog);
            return result;
        }



        /// <summary>
        /// 插入数据 返回插入的值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbs"></param>
        /// <param name="t"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public static async Task<T> InsertReT<T>(this IUnitOfWork dbs, T t, IDbTransaction transaction = null,
            int? commandTimeout = null)
            where T : class
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var db = dbs.DbConnection;
            var sql = SqlQuery<T>.Builder(dbs);
            //SqlLog log = new SqlLog
            //{
            //    CreateTime = DateTime.Now,
            //    //返回自动递增字段的值
            //    OperateSql = sql.InsertSql + ";SELECT @@Identity"
            //};
            var id =await db.ExecuteScalarAsync<int>(sql.InsertSql + ";SELECT @@Identity", t, transaction, commandTimeout);
            var result =await db.QueryFirstAsync<T>(sql.QuerySqlById, new { id });
            stopwatch.Stop();
            //DataLog datalog = new DataLog
            //{
            //    OperateType = OperateType.Add.ToString(),
            //    OperateTable = sql._ModelDes.TableName,
            //    OperationBefore = "",
            //    OperationAfterData = JsonConvert.SerializeObject(t),
            //};
            //log.EndDateTime = DateTime.Now;
            //log.ElapsedTime = stopwatch.Elapsed.TotalSeconds;
            //WriteSqlLog(log);
            //WriteDataLog(datalog);
            return result;
        }

        /// <summary>
        ///     批量插入数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbs"></param>
        /// <param name="lt"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public static async Task<int> InsertBatch<T>(this IUnitOfWork dbs, IEnumerable<T> lt, IDbTransaction transaction = null,
            int? commandTimeout = null) where T : class
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var db = dbs.DbConnection;
            var sql = SqlQuery<T>.Builder(dbs);
            //SqlLog log = new SqlLog
            //{
            //    CreateTime = DateTime.Now,
            //    OperateSql = sql.InsertSql
            //};
            var result =await db.ExecuteAsync(sql.InsertSql, lt, transaction, commandTimeout);
            stopwatch.Stop();
            //DataLog datalog = new DataLog
            //{
            //    OperateType = OperateType.Add.ToString(),
            //    OperateTable = sql._ModelDes.TableName,
            //    OperationBefore = "",
            //    OperationAfterData = JsonConvert.SerializeObject(lt),
            //};
            //log.EndDateTime = DateTime.Now;
            //log.ElapsedTime = stopwatch.Elapsed.TotalSeconds;
            //WriteSqlLog(log);
            //WriteDataLog(datalog);
            return result;
        }
        #endregion

        #region 删除
        /// <summary>
        /// 按条件删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbs"></param>
        /// <param name="sql"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static async Task<int> Delete<T>(this IUnitOfWork dbs, SqlQuery sql = null, IDbTransaction transaction = null) where T : class
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var db = dbs.DbConnection;
            if (sql == null)
                sql = SqlQuery<T>.Builder(dbs);
            //SqlLog log = new SqlLog
            //{
            //    CreateTime = DateTime.Now,
            //    OperateSql = sql.DeleteSql,
            //    Parameter = sql.Param == null ? "" : sql.Param.ToString()
            //};

            //DataLog datalog = new DataLog
            //{
            //    OperateType = OperateType.Delete.ToString(),
            //    OperateTable = sql._ModelDes.TableName,
            //    OperationBefore = JsonConvert.SerializeObject(dbs.DbConnection.Query(sql.QuerySql, sql.Param)),
            //    OperationAfterData = "",
            //};
            var result =await db.ExecuteAsync(sql.DeleteSql, sql.Param, transaction);
            stopwatch.Stop();

            //log.EndDateTime = DateTime.Now;
            //log.ElapsedTime = stopwatch.Elapsed.TotalSeconds;
            //WriteSqlLog(log);
            //WriteDataLog(datalog);
            return result;
        }

        /// <summary>
        ///  按Id删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbs"></param>
        /// <param name="sql"></param>
        /// <param name="id"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static async Task<int> DeleteById<T>(this IUnitOfWork dbs, SqlQuery sql = null, IDbTransaction transaction = null, object id = null) where T : class
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var db = dbs.DbConnection;
            if (sql == null)
                sql = SqlQuery<T>.Builder(dbs);
            //SqlLog log = new SqlLog
            //{
            //    CreateTime = DateTime.Now,
            //    OperateSql = sql.DeleteSqlById,
            //    Parameter = id.ToString()
            //};
            //DataLog datalog = new DataLog
            //{
            //    OperateType = OperateType.删除.ToString(),
            //    OperateTable = sql._ModelDes.TableName,
            //    OperationBefore = JsonConvert.SerializeObject(dbs.DbConnection.Query(sql.QuerySqlById)),
            //    OperationAfterData = "",
            //};
            var result =await db.ExecuteAsync(sql.DeleteSqlById, new { id }, transaction);
            stopwatch.Stop();
            //log.EndDateTime = DateTime.Now;
            //log.ElapsedTime = stopwatch.Elapsed.TotalSeconds;
            //WriteSqlLog(log);
            // WriteDataLog(datalog);
            return result;
        }

        /// <summary>
        ///  按Ids删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbs"></param>
        /// <param name="ids"></param>
        /// <param name="sql"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static async Task<int> DeleteByIds<T>(this IUnitOfWork dbs, string ids, SqlQuery sql = null, IDbTransaction transaction = null) where T : class
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var db = dbs.DbConnection;
            if (sql == null)
                sql = SqlQuery<T>.Builder(dbs);

            //记录数据日志
            //DataLog datalog = new DataLog
            //{
            //    OperateType = OperateType.Delete.ToString(),
            //    OperateTable = sql._ModelDes.TableName,
            //    OperationBefore = JsonConvert.SerializeObject(dbs.DbConnection.Query(sql.QuerySqlByIds.Replace("@id", ids.TrimEnd(',').SqlRemoveStr()))),
            //    OperationAfterData = "",
            //};
            ////记录sql日志
            //SqlLog log = new SqlLog
            //{
            //    CreateTime = DateTime.Now,
            //    OperateSql = sql.DeleteSqlByIds,
            //    Parameter = ids
            //};
            var result =await db.ExecuteAsync(sql.DeleteSqlByIds.Replace("@id", ids.TrimEnd(',').SqlRemoveStr()), null, transaction);
            stopwatch.Stop();
            //log.EndDateTime = DateTime.Now;
            //log.ElapsedTime = stopwatch.Elapsed.TotalSeconds;
            //WriteSqlLog(log);
            //WriteDataLog(datalog);
            return result;
        }
        #endregion

        #region 修改
        /// <summary>
        ///     修改
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbs"></param>
        /// <param name="t">如果sql为null，则根据t的主键进行修改</param>
        /// <param name="sql">按条件修改</param>
        /// <returns></returns>
        public static async Task<int> Update<T>(this IUnitOfWork dbs, T t, IDbTransaction transaction = null, SqlQuery sql = null) where T : class
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var db = dbs.DbConnection;
            if (sql == null)
                sql = SqlQuery<T>.Builder(dbs);
            sql = sql.AppendParam(t);

            #region 之前的数据
            Type Ts = t.GetType();
            var prikey = Ts.GetProperty(DapperCacheCommon.GetPrimary(sql._ModelDes).Field).GetValue(t, null).ToString();
            var obj = dbs.DbConnection.Query(sql.QuerySqlById.Replace("@id", prikey.TrimEnd(',').SqlRemoveStr())).FirstOrDefault();
            #endregion
            //DataLog datalog = new DataLog
            //{
            //    OperateType = OperateType.Edit.ToString(),
            //    OperateTable = sql._ModelDes.TableName,
            //    OperationBefore = JsonConvert.SerializeObject(obj),
            //    OperationAfterData = JsonConvert.SerializeObject(t),
            //};
            //SqlLog log = new SqlLog
            //{
            //    CreateTime = DateTime.Now,
            //    OperateSql = sql.UpdateSql,
            //    Parameter = sql.Param == null ? "" : sql.Param.ToString()
            //};
            var result =await db.ExecuteAsync(sql.UpdateSql, sql.Param, transaction);
            stopwatch.Stop();

            //log.EndDateTime = DateTime.Now;
            //log.ElapsedTime = stopwatch.Elapsed.TotalSeconds;
            //WriteSqlLog(log);
            //WriteDataLog(datalog);
            return result;
        }

        /// <summary>
        ///     修改
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbs"></param>
        /// <param name="t">如果sql为null，则根据t的主键进行修改</param>
        /// <param name="updateProperties">要修改的属性集合</param>
        /// <param name="sql">按条件修改</param>
        /// <returns></returns>
        public static async Task<int> Update<T>(this IUnitOfWork dbs, T t, IList<string> updateProperties, IDbTransaction transaction = null, SqlQuery sql = null)
            where T : class
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var db = dbs.DbConnection;
            if (sql == null)
                sql = SqlQuery<T>.Builder(dbs);
            sql = sql.AppendParam(t)
                .SetExcProperties<T>(updateProperties);

            #region 之前的数据
            Type Ts = t.GetType();
            var prikey = Ts.GetProperty(DapperCacheCommon.GetPrimary(sql._ModelDes).Field).GetValue(t, null).ToString();
            var obj = dbs.DbConnection.Query(sql.QuerySqlById.Replace("@id", prikey.TrimEnd(',').SqlRemoveStr())).FirstOrDefault();
            #endregion

            //SqlLog log = new SqlLog
            //{
            //    CreateTime = DateTime.Now,
            //    OperateSql = sql.UpdateSql,
            //    Parameter = sql.Param == null ? "" : sql.Param.ToString()
            //};

            //DataLog datalog = new DataLog
            //{
            //    OperateType = OperateType.Edit.ToString(),
            //    OperateTable = sql._ModelDes.TableName,
            //    OperationBefore = JsonConvert.SerializeObject(obj),
            //    OperationAfterData = JsonConvert.SerializeObject(t),
            //};

            var result =await db.ExecuteAsync(sql.UpdateSql, sql.Param, transaction);
            stopwatch.Stop();

            //log.EndDateTime = DateTime.Now;
            //log.ElapsedTime = stopwatch.Elapsed.TotalSeconds;
            //WriteSqlLog(log);
            //WriteDataLog(datalog);
            return result;
        }
        #endregion

        #region 查询
        /// <summary>
        ///     获取默认一条数据，没有则为NULL
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbs"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static async Task<T> SingleOrDefault<T>(this IUnitOfWork dbs, SqlQuery sql) where T : class
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var db = dbs.DbConnection;
            if (sql == null)
                sql = SqlQuery<T>.Builder(dbs);
            sql = sql.Top(1);
            //SqlLog log = new SqlLog
            //{
            //    CreateTime = DateTime.Now,
            //    OperateSql = sql.QuerySql,
            //    Parameter = sql.Param == null ? "" : sql.Param.ToString()
            //};
            var result =await db.QueryFirstAsync<T>(sql.QuerySql, sql.Param);
            stopwatch.Stop();
            //log.EndDateTime = DateTime.Now;
            //log.ElapsedTime = stopwatch.Elapsed.TotalSeconds;
            //WriteSqlLog(log);
            return result;
        }

        /// <summary>
        ///     获取默认一条数据，没有则为NULL
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbs"></param>
        /// <param name="sql"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static async Task<T> SingleOrDefault<T>(this IUnitOfWork dbs, SqlQuery sql = null, object id = null) where T : class
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var db = dbs.DbConnection;
            if (sql == null)
                sql = SqlQuery<T>.Builder(dbs);
            sql = sql.Top(1);
            //SqlLog log = new SqlLog
            //{
            //    CreateTime = DateTime.Now,
            //    OperateSql = sql.QuerySqlById,
            //    Parameter = sql.Param == null ? "" : sql.Param.ToString()
            //};
            var result =await db.QueryFirstAsync<T>(sql.QuerySqlById, new { id });
            stopwatch.Stop();
            //log.EndDateTime = DateTime.Now;
            //log.ElapsedTime = stopwatch.Elapsed.TotalSeconds;
            //WriteSqlLog(log);
            return result;
        }

        /// <summary>
        ///     分页查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbs"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="dataCount"></param>
        /// <param name="sqlQuery"></param>
        /// <returns></returns>
        public static IEnumerable<T> Page<T>(this IUnitOfWork dbs, int pageIndex, int pageSize, out int dataCount,
            SqlQuery sqlQuery = null) where T : class
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var db = dbs.DbConnection;
            if (sqlQuery == null)
                sqlQuery = SqlQuery<T>.Builder(dbs);

            //SqlLog log = new SqlLog
            //{
            //    CreateTime = DateTime.Now,
            //    OperateSql = sqlQuery.QuerySql,
            //    Parameter = sqlQuery.Param == null ? "" : sqlQuery.Param.ToString()
            //};
            sqlQuery = sqlQuery.Page(pageIndex, pageSize);
            var para = sqlQuery.Param;
            var sql = sqlQuery.PageSql + " ; " + sqlQuery.CountSql;
            using (var result = db.QueryMultiple(sql, para))
            {
                var list = result.Read<T>();
                var totalCount = result.Read<int>().FirstOrDefault();
                dataCount = totalCount;
                return list;
            }

            //var cr = db.Query(sqlQuery.CountSql, para).SingleOrDefault();
            //dataCount = cr.DataCount;
            //var result = db.Query<T>(sqlQuery.PageSql, para).ToList();
            //log.EndDateTime = DateTime.Now;
            //log.ElapsedTime = stopwatch.Elapsed.TotalSeconds;
            //WriteSqlLog(log);
           // return result;
        }



        /// <summary>
        ///     查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbs"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static async Task<IEnumerable<T>> Query<T>(this IUnitOfWork dbs, SqlQuery sql = null) where T : class
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var db = dbs.DbConnection;
            if (sql == null)
                sql = SqlQuery<T>.Builder(dbs);
            //SqlLog log = new SqlLog
            //{
            //    CreateTime = DateTime.Now,
            //    OperateSql = sql.QuerySql,
            //    Parameter = sql.Param == null ? "" : sql.Param.ToString()
            //};
            var result =await db.QueryAsync<T>(sql.QuerySql, sql.Param);
            stopwatch.Stop();
            //log.EndDateTime = DateTime.Now;
            //log.ElapsedTime = stopwatch.Elapsed.TotalSeconds;
            //WriteSqlLog(log);
            return result;
        }


        /// <summary>
        ///     数据数量
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbs"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static async Task<int> Count<T>(this IUnitOfWork dbs, SqlQuery sql = null) where T : class
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var db = dbs.DbConnection;
            if (sql == null)
                sql = SqlQuery<T>.Builder(dbs);
            //SqlLog log = new SqlLog
            //{
            //    CreateTime = DateTime.Now,
            //    OperateSql = sql.CountSql,
            //    Parameter = sql.Param == null ? "" : sql.Param.ToString()
            //};
            var result = await db.QueryFirstAsync(sql.CountSql, sql.Param);
            stopwatch.Stop();
            //log.EndDateTime = DateTime.Now;
            //log.ElapsedTime = stopwatch.Elapsed.TotalSeconds;
            // WriteSqlLog(log);
            return result;
        }
        #endregion

        #endregion


        #region  sql to dapper

        #region  纯sql语句的 增删改

        /// <summary>
        ///     执行增加删除修改语句
        /// </summary>
        /// <param name="dbs"></param>
        /// <param name="sql">Sql语句</param>
        /// <param name="parms">参数信息</param>
        /// <param name="isSetConnectionStr">是否需要重置连接字符串</param>
        /// <returns>影响数</returns>
        public static async Task<int> InsertUpdateOrDeleteSql(this IUnitOfWork dbs, string sql, IDbTransaction transaction = null, dynamic parms = null,
            bool isSetConnectionStr = true)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            //SqlLog log = new SqlLog
            //{
            //    CreateTime = DateTime.Now,
            //    OperateSql = sql,
            //    Parameter = parms == null ? "" : parms.ToString(),
            //};
            var result =await dbs.DbConnection.ExecuteAsync(sql, (object)parms, transaction);
            stopwatch.Stop();
            //log.EndDateTime = DateTime.Now;
            //log.ElapsedTime = stopwatch.Elapsed.TotalSeconds;
            //WriteSqlLog(log);
            return result;
        }

        /// <summary>
        ///     使用SqlBulkCopy批量进行插入数据
        /// </summary>
        /// <typeparam name="T">实体对象</typeparam>
        /// <param name="dbs"></param>
        /// <param name="entitys">实体对象集合</param>
        public static int InsertWithBulkCopy<T>(this IUnitOfWork dbs, List<T> entitys, IDbTransaction transaction = null) where T : new()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            int result = 1;

            try
            {
                using (var destinationConnection = (SqlConnection)dbs.DbConnection)
                {
                    using (transaction = destinationConnection.BeginTransaction())
                    {

                        using (var bulkCopy = new SqlBulkCopy(destinationConnection))
                        {
                            Type type = entitys[0].GetType();
                            object classAttr = type.GetCustomAttributes(false)[0];
                            if (classAttr is TableAttribute)
                            {
                                TableAttribute tableAttr = classAttr as TableAttribute;
                                bulkCopy.DestinationTableName = tableAttr.Name; //要插入的表的表明 
                            }

                            DataTable dt = DTListConvertUtil<T>.FillDataTable(entitys);
                            destinationConnection.Open();
                            if (dt != null && dt.Rows.Count != 0)
                            {
                                bulkCopy.WriteToServer(dt);
                            }
                            transaction.Commit();
                        }
                    }
                }
                //SqlLog log = new SqlLog
                //{
                //    CreateTime = DateTime.Now,
                //    OperateSql = "BulkCopy批量插入"
                //};
                //stopwatch.Stop();
                //log.EndDateTime = DateTime.Now;
                //log.ElapsedTime = stopwatch.Elapsed.TotalSeconds;
                //WriteSqlLog(log);

            }
            catch (Exception ex)
            {

                transaction.Rollback();

            }

            return result;
        }

        #endregion

        #region 查询

        /// <summary>
        ///     执行语句返回bool
        /// </summary>
        /// <param name="dbs"></param>
        /// <param name="sql"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public static  bool SqlWithParamsBool(this IUnitOfWork dbs, string sql, dynamic parms = null)
        {
            //Stopwatch stopwatch = new Stopwatch();
           // stopwatch.Start();
            //SqlLog log = new SqlLog
            //{
            //    CreateTime = DateTime.Now,
            //    OperateSql = sql,
            //    Parameter = parms == null ? "" : parms.ToString()
            //};
            var result = dbs.DbConnection.Query(sql, (object)parms).Any();
            //stopwatch.Stop();
            //log.EndDateTime = DateTime.Now;
            //log.ElapsedTime = stopwatch.Elapsed.TotalSeconds;
            //WriteSqlLog(log);
            return result;
        }

        /// <summary>
        ///     执行Sql语句带参数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbs"></param>
        /// <param name="sql"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public static IEnumerable<T> SqlWithParams<T>(this IUnitOfWork dbs, string sql, dynamic parms = null)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            //SqlLog log = new SqlLog
            //{
            //    CreateTime = DateTime.Now,
            //    OperateSql = sql,
            //    Parameter = parms == null ? "" : parms.ToString()
            //};
            stopwatch.Stop();
            //log.EndDateTime = DateTime.Now;
            //log.ElapsedTime = stopwatch.Elapsed.TotalSeconds;
            //WriteSqlLog(log);
            var result = dbs.DbConnection.Query<T>(sql, (object)parms);

            return result;
        }

        /// <summary>
        /// 执行 sql 分页
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbs"></param>
        /// <param name="sql"></param>
        /// <param name="countSql"></param>
        /// <param name="totalCount"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public static IEnumerable<T> SqlWithParamsPage<T>(this IUnitOfWork dbs, string sql, string countSql, out int totalCount, dynamic parms = null)
        {
            Stopwatch stopwatch = new Stopwatch();
            var db = dbs.DbConnection;
            stopwatch.Start();
            //SqlLog log = new SqlLog
            //{
            //    CreateTime = DateTime.Now,
            //    OperateSql = sql,
            //    Parameter = parms == null ? "" : JsonUtil.JsonSerialize(parms)
            //};
            stopwatch.Stop();
            //log.EndDateTime = DateTime.Now;
            //log.ElapsedTime = stopwatch.Elapsed.TotalSeconds;
            var result = db.Query<T>(sql, (object)parms);
            var cr = db.Query<dynamic>(countSql, (object)parms).SingleOrDefault();
            totalCount = cr.DataCount;
           // WriteSqlLog(log);
            return result;
        }
        public static IEnumerable<dynamic> SqlWithParams(this IUnitOfWork dbs, string sql, dynamic parms)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            //SqlLog log = new SqlLog
            //{
            //    CreateTime = DateTime.Now,
            //    OperateSql = sql,
            //    Parameter = parms == null ? "" : parms.ToString()
            //};
            var result = dbs.DbConnection.Query(sql, (object)parms);
            stopwatch.Stop();
            //log.EndDateTime = DateTime.Now;
            //log.ElapsedTime = stopwatch.Elapsed.TotalSeconds;
            // WriteSqlLog(log);
            return result;
        }

        public static DataTable SqlWithParamsToDataTable(this IUnitOfWork dbs, string sql, dynamic parms)
        {
            DataTable dataTable = new DataTable();
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            //SqlLog log = new SqlLog
            //{
            //    CreateTime = DateTime.Now,
            //    OperateSql = sql,
            //    Parameter = parms == null ? "" : parms.ToString()
            //};
            var result = dbs.DbConnection.ExecuteReader(sql, (object)parms);
            stopwatch.Stop();
            //log.EndDateTime = DateTime.Now;
            //log.ElapsedTime = stopwatch.Elapsed.TotalSeconds;
            dataTable.Load(result);
           // WriteSqlLog(log);
            return dataTable;
        }

        public static T SqlWithParamsSingle<T>(this IUnitOfWork dbs, string sql, dynamic parms = null)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            //SqlLog log = new SqlLog
            //{
            //    CreateTime = DateTime.Now,
            //    OperateSql = sql,
            //    Parameter = parms == null ? "" : parms.ToString()
            //};
            var result = dbs.DbConnection.Query<T>(sql, (object)parms).FirstOrDefault();
            stopwatch.Stop();
            //log.EndDateTime = DateTime.Now;
            //log.ElapsedTime = stopwatch.Elapsed.TotalSeconds;
            //WriteSqlLog(log);
            return result;
        }


        /// <summary>
        /// 查询  一对一 或是一对多
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbs"></param>
        /// <param name="sql"></param>
        /// <param name="types"></param>
        /// <param name="map"></param>
        /// <param name="splitOn"></param>
        /// <returns></returns>
        public static IEnumerable<T> SqlWithParamsPageMult<T>(this IUnitOfWork dbs, string sql, string countSql, out int total, Type[] types, Func<object[], T> map, object param, string splitOn = "Id") where T : class
        {
           // Stopwatch stopwatch = new Stopwatch();
           // stopwatch.Start();
            var db = dbs.DbConnection;
            //SqlLog log = new SqlLog
            //{
            //    CreateTime = DateTime.Now,
            //    OperateSql = sql,
            //    Parameter = param == null ? "" : JsonUtil.JsonSerialize(param)
            //};
            var result = db.Query<T>(sql, types, map, param, null, true, splitOn);
            if (!string.IsNullOrEmpty(countSql))
            {
                total = db.Query(countSql).SingleOrDefault().DataCount;
            }
            else
            {
                total = 0;
            }
            //stopwatch.Stop();
            //log.EndDateTime = DateTime.Now;
            //log.ElapsedTime = stopwatch.Elapsed.TotalSeconds;
            //WriteSqlLog(log);
            return result;
        }

        #endregion

        #region 存储过程

        /// <summary>
        ///     存储过程增加删除修改
        /// </summary>
        /// <param name="dbs"></param>
        /// <param name="procName">存储过程名称</param>
        /// <param name="parms">参数</param>
        /// <returns>影响条数</returns>
        public static int InsertUpdateOrDeleteStoredProc(this IUnitOfWork dbs, string procName, IDbTransaction transaction = null, dynamic parms = null)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            //SqlLog log = new SqlLog
            //{
            //    CreateTime = DateTime.Now,
            //    OperateSql = procName,
            //    Parameter = parms == null ? "" : parms.ToString()
            //};
            var result = dbs.DbConnection.Execute(procName, (object)parms, transaction, commandType: CommandType.StoredProcedure);
            stopwatch.Stop();
            //log.EndDateTime = DateTime.Now;
            //log.ElapsedTime = stopwatch.Elapsed.TotalSeconds;
            //WriteSqlLog(log);
            return result;
        }

        /// <summary>
        ///     存储过程查询所有值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbs"></param>
        /// <param name="procName">The procName.</param>
        /// <param name="parms">The parms.</param>
        /// <returns></returns>
        public static IEnumerable<T> StoredProcWithParams<T>(this IUnitOfWork dbs, string procName, dynamic parms)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            //SqlLog log = new SqlLog
            //{
            //    CreateTime = DateTime.Now,
            //    OperateSql = procName,
            //    Parameter = parms == null ? "" : parms.ToString()
            //};
            var result = dbs.DbConnection.Query<T>(procName, (object)parms, commandType: CommandType.StoredProcedure);
            stopwatch.Stop();
            //log.EndDateTime = DateTime.Now;
            //log.ElapsedTime = stopwatch.Elapsed.TotalSeconds;
            //WriteSqlLog(log);
            return result;
        }

        /// <summary>
        ///     存储过程返回满足条件的第一条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbs"></param>
        /// <param name="procName"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public static T StoredProcWithParamsSingle<T>(this IUnitOfWork dbs, string procName, dynamic parms = null)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            //SqlLog log = new SqlLog
            //{
            //    CreateTime = DateTime.Now,
            //    OperateSql = procName,
            //    Parameter = parms == null ? "" : parms.ToString()
            //};
            var result = dbs.DbConnection.Query<T>(procName, (object)parms, commandType: CommandType.StoredProcedure)
                .SingleOrDefault();
            stopwatch.Stop();
            //log.EndDateTime = DateTime.Now;
            //log.ElapsedTime = stopwatch.Elapsed.TotalSeconds;
            //WriteSqlLog(log);
            return result;
        }

        #endregion

        #endregion
    }
}
