using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace DAL.Repositories
{
    public class SotStagingRepository : GenericRepository<SotStaging>
    {
        /// <summary>
        /// Sot staging repository constructor
        /// </summary>
        /// <param name="context">The database context</param>
        public SotStagingRepository(TCRCEntities context)
            : base(context) { }

        /// <summary>
        /// Truncates sot staging
        /// </summary>
        public void TruncateSotStaging()
        {
            try
            {
                context.Database.ExecuteSqlCommand("TRUNCATE TABLE DBO.SotStaging");
            }
            catch (Exception e)
            {
                //todo: handle exception
            }
        }

        /// <summary>
        /// Execute bulk insert sot staging
        /// </summary>
        /// <param name="connection">The database connection</param>
        /// <param name="tableName">The table name</param>
        /// <param name="list">Sot staging records</param>
        public virtual void BulkInsert(string connection, string tableName, IList<SotStaging> list)
        {
            try
            {
                using (var bulkCopy = new SqlBulkCopy(connection))
                {
                    bulkCopy.BatchSize = list.Count;
                    bulkCopy.DestinationTableName = tableName;

                    var table = new DataTable();
                    var props = TypeDescriptor.GetProperties(typeof(SotStaging))
                        //Dirty hack to make sure we only have system data types 
                        //i.e. filter out the relationships/collections
                        .Cast<PropertyDescriptor>()
                        .Where(propertyInfo => propertyInfo.PropertyType.Namespace.Equals("System"))
                        .ToArray();

                    foreach (var propertyInfo in props)
                    {
                        bulkCopy.ColumnMappings.Add(propertyInfo.Name, propertyInfo.Name);
                        table.Columns.Add(propertyInfo.Name, Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType);
                    }

                    var values = new object[props.Length];
                    foreach (var item in list)
                    {
                        for (var i = 0; i < values.Length; i++)
                        {
                            values[i] = props[i].GetValue(item);
                        }
                        table.Rows.Add(values);
                    }
                    bulkCopy.WriteToServer(table);
                }
            } catch (Exception e)
            {
                //todo: log error
            }
        }

        /// <summary>
        /// Execute import sot records
        /// </summary>
        public void ImportSot()
        {
            try
            {
                context.Database.ExecuteSqlCommand("EXEC ImportSot");
            }
            catch (Exception e)
            {
                //todo: handle exception
            }
        }
    }
}