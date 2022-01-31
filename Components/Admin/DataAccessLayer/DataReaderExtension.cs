using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;

namespace DataAccessLayer
{
    /** This class handles all Operations for Getting Parameters from DB's procedures & converting them to data entities.
     * */
    public static class DataReaderExtensions
    {


        public static bool GetParameterBoolean(this Database database, DbCommand sp, string parameter)
        {
            var paramName = parameter.StartsWith("@") ? parameter : "@" + parameter;
            var exists = database.GetParameterValue(sp, paramName);
            if (exists == null || exists == DBNull.Value)
                return false;
            return Convert.ToBoolean(exists);
        }

        public static string GetParameterString(this Database database, DbCommand sp, string parameter)
        {
            var paramName = parameter.StartsWith("@") ? parameter : "@" + parameter;
            var exists = database.GetParameterValue(sp, paramName);
            if (exists == null || exists == DBNull.Value)
                return null;
            return Convert.ToString(exists);
        }

        public static int GetParameterInt(this Database database, DbCommand sp, string parameter)
        {
            var paramName = parameter.StartsWith("@") ? parameter : "@" + parameter;
            var parameterValue = database.GetParameterValue(sp, paramName);
            if (parameterValue == null || parameterValue == DBNull.Value)
                return 0;
            return Convert.ToInt32(parameterValue);
        }

        public static int? GetParameterIntNullable(this Database database, DbCommand sp, string parameter)
        {
            var paramName = parameter.StartsWith("@") ? parameter : "@" + parameter;
            var parameterValue = database.GetParameterValue(sp, paramName);
            if (parameterValue == null || parameterValue == DBNull.Value)
                return (int?)null;
            return Convert.ToInt32(parameterValue);
        }

        public static decimal GetParameterDecimal(this Database database, DbCommand sp, string parameter)
        {
            var paramName = parameter.StartsWith("@") ? parameter : "@" + parameter;
            var parameterValue = database.GetParameterValue(sp, paramName);
            if (parameterValue == null || parameterValue == DBNull.Value)
                return 0;
            return Convert.ToDecimal(parameterValue);
        }

        public static DateTime? GetParameterDate(this Database database, DbCommand sp, string parameter)
        {
            var paramName = parameter.StartsWith("@") ? parameter : "@" + parameter;
            var parameterValue = database.GetParameterValue(sp, paramName);
            if (parameterValue == null || parameterValue == DBNull.Value)
                return null;
            return Convert.ToDateTime(parameterValue);
        }

        public static void AddOutParameterDecimal(this IDbCommand sp, string paramName)
        {
            var p = new SqlParameter
            {
                ParameterName = paramName,
                Direction = ParameterDirection.Output,
                SqlDbType = SqlDbType.Decimal,
                Precision = 8,
                Scale = 2,
                // IsNullable = true,
            };
            sp.Parameters.Add(p);
        }

        public static void AddTableValueParameter<T>(this IDbCommand sp, string paramName, IEnumerable<T> tableValues) where T : struct
        {
            var p = new SqlParameter
            {
                ParameterName = paramName,
                SqlDbType = SqlDbType.Structured,
                IsNullable = true
            };
            var sqlTypeName = (typeof(T) == typeof(int)) ? "@IdsDataType" : "@BetStatuses";
            var table = new DataTable(sqlTypeName);
            table.Columns.Add("@Status", typeof(T));
            if (tableValues != null)
            {
                foreach (var tableValue in tableValues)
                {
                    table.Rows.Add(tableValue);
                }
            }
            p.Value = table;
            sp.Parameters.Add(p);
        }

        private static bool ColumnExists(this IDataReader rdr, string columnName)
        {
            return Enumerable.Range(0, rdr.FieldCount).Any(i => rdr.GetName(i) == columnName);
        }

        public static T As<T>(this IDataReader rdr, string columnName)
        {
            if (!ColumnExists(rdr, columnName))
                return default(T);
            return rdr[columnName] == DBNull.Value || rdr[columnName] == null
                ? default(T)
                : (T)(rdr[columnName]);
        }

        public static string AsString(this IDataReader rdr, string columnName)
        {
            if (ColumnExists(rdr, columnName))
                return rdr[columnName] == DBNull.Value || rdr[columnName] == null ? null : (string)(rdr[columnName]);
            return default(string);
        }

        public static int AsInt(this IDataReader rdr, string columnName)
        {
            if (ColumnExists(rdr, columnName))
                return rdr[columnName] == DBNull.Value || rdr[columnName] == null ? 0 : Convert.ToInt32(rdr[columnName]);
            return default(int);
        }

        public static string AsStringFromBase64(this IDataReader rdr, string columnName)
        {
            string result = "";
            if (ColumnExists(rdr, columnName) && (rdr[columnName] as string) != null)
            {
                var bitArr = Convert.FromBase64String(rdr[columnName] as string);
                foreach (var item in bitArr)
                {
                    result += item;
                }
                return result;
            }
            return result;

        }

        public static byte AsByte(this IDataReader reader, string columnName)
        {
            if (!ColumnExists(reader, columnName)) return default(byte); //todo do we need check?
            var value = reader[columnName];
            if (value == DBNull.Value) return 0;
            return Convert.ToByte(value);
        }

        public static byte? AsByteNullable(this IDataReader reader, string name)
        {
            if (!ColumnExists(reader, name)) return null; //todo do we need check?
            var value = reader[name];
            if (value == null || value == DBNull.Value) return null;
            return Convert.ToByte(value);
        }

        public static bool AsBool(this IDataReader rdr, string columnName)
        {
            if (!ColumnExists(rdr, columnName))
                return default(bool);
            return Convert.ToBoolean(rdr[columnName]);
        }

        public static DateTime AsDate(this IDataReader rdr, string columnName)
        {
            if (!ColumnExists(rdr, columnName)) return default(DateTime);
            var parameterValue = rdr[columnName];
            if (parameterValue == null || parameterValue == DBNull.Value)
                return default(DateTime);
            return Convert.ToDateTime(parameterValue);
        }

        public static byte[] AsByteArray(this IDataReader rdr, string columnName)
        {
            if (!ColumnExists(rdr, columnName)) return null;
            var parameterValue = rdr[columnName];
            if (parameterValue == null || parameterValue == DBNull.Value)
                return null;
            return (byte[])parameterValue;
        }

        public static DateTime? AsDateNullable(this IDataReader rdr, string columnName)
        {
            if (!ColumnExists(rdr, columnName)) return null;
            var parameterValue = rdr[columnName];
            if (parameterValue == null || parameterValue == DBNull.Value)
                return null;
            return Convert.ToDateTime(parameterValue);
        }

        public static int? AsIntNullable(this IDataReader reader, string name)
        {
            if (!ColumnExists(reader, name)) return null; //todo do we need check?
            var value = reader[name];
            if (value == null || value == DBNull.Value) return null;
            return Convert.ToInt32(value);
        }

        public static decimal? AsDecimalNullable(this IDataReader reader, string name)
        {
            if (!ColumnExists(reader, name)) return null;
            var value = reader[name];
            if (value == DBNull.Value) return null;
            return Convert.ToDecimal(value);
        }

        public static decimal AsDecimal(this IDataReader reader, string name)
        {
            if (!ColumnExists(reader, name)) return decimal.Zero;
            var value = reader[name];
            if (value == DBNull.Value) return decimal.Zero;
            return Convert.ToDecimal(value);
        }

        public static float AsFloat(this IDataReader reader, string name)
        {
            if (!ColumnExists(reader, name)) return 0;
            var value = reader[name];
            if (value == DBNull.Value) return 0;
            return Convert.ToSingle(value);
        }

        public static float? AsFloatNullable(this IDataReader reader, string name)
        {
            if (!ColumnExists(reader, name)) return 0;
            var value = reader[name];
            if (value == DBNull.Value) return null;
            return Convert.ToSingle(value);
        }

        public static Guid AsGuid(this IDataReader reader, string name)
        {
            var value = reader[name];
            if (value == DBNull.Value) return Guid.Empty;
            return Guid.Parse(value.ToString());
        }

        public static Guid? AsGuidNullable(this IDataReader reader, string name)
        {
            if (!ColumnExists(reader, name)) return null;
            var value = reader[name];
            if (value == DBNull.Value) return null;
            return Guid.Parse(value.ToString());
        }
    }
}
