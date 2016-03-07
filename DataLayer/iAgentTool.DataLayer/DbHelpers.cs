using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace iAgentTool.DataLayer
{
    public static class DbHelpers
    {
        public static DateTime? GetNullableDateTime(object readerColumn)
        {
            if (readerColumn == DBNull.Value)
                return null;
            else
                return (DateTime)readerColumn;
        }

        public static DateTime? GetNullableDateTime(string sDate)
        {
            if (string.IsNullOrEmpty(sDate))
                return null;

            DateTime temp = DateTime.MinValue;
            DateTime.TryParse(sDate, out temp);

            if (temp == DateTime.MinValue)
                return null;
            else
                return temp;
        }

        public static DateTimeOffset? GetNullableDateTimeOffset(object readerColumn)
        {
            if (readerColumn == DBNull.Value)
                return null;
            else
                return (DateTimeOffset)readerColumn;
        }

        public static object GetSafePersistDbValue(object obj)
        {
            if (obj == null)
                return DBNull.Value;
            else
                return (object)obj;
        }

        public static T GetValueOrDefault<T>(this IDataRecord row, string fieldName)
        {
            int ordinal = row.GetOrdinal(fieldName);
            return row.GetValueOrDefault<T>(ordinal);
        }

        public static T GetValueOrDefault<T>(this IDataRecord row, int ordinal)
        {
            return (T)(row.IsDBNull(ordinal) ? default(T) : row.GetValue(ordinal));
        }

        public static TEnum GetEnumValueByName<TEnum>(this IDataRecord row, string fieldName) where TEnum : struct
        {
            string nameValue = GetValueOrDefault<string>(row, fieldName);
            TEnum myEnum = default(TEnum);
            if (!Enum.TryParse<TEnum>(nameValue, out myEnum))
            {
                throw new ArgumentException("This is not a valid enum name!");
            }
            return myEnum;
        }

    }
}
