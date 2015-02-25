#region Using

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

#endregion

namespace CodaUtil
{
    public class Util
    {
        #region I/O routines

        private const string MIMETYPE_UNKNOWN = "application/unknown";
        private const string CONTENT_TYPE = "Content Type";
        private static readonly Object syncRoot = new Object();

        /// <summary>
        /// Get MIME type of filename
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetMimeType(string fileName)
        {
            string mimeType = MIMETYPE_UNKNOWN;
            string ext = Path.GetExtension(fileName).ToLower();
            Microsoft.Win32.RegistryKey regKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext);
            if (regKey != null && regKey.GetValue(CONTENT_TYPE) != null)
                mimeType = regKey.GetValue(CONTENT_TYPE).ToString();
            return mimeType;
        }

        /// <summary>
        /// Calculate MD5 by byte stream.  Generic method.
        /// </summary>
        /// <typeparam name="TStream"></typeparam>
        /// <param name="byteStream"></param>
        /// <returns></returns>
        private static string CalculateStreamMD5<TStream>(TStream byteStream) where TStream : System.IO.Stream
        {
            byte[] hash = MD5.Create().ComputeHash(byteStream);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("x2"));
            }
            return sb.ToString();
        }

        /// <summary>
        /// Calculate MD5 hash on file
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string CalculateFileMD5(string filePath)
        {
            string ret = String.Empty;

            if (!(new FileInfo(filePath)).Exists) return ret;
            try
            {
                using (
                    FileStream fstream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite
                    /*!!*/))
                {
                    return CalculateStreamMD5(fstream);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Calculate md5 for byte array
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string CalculateMD5(byte[] data)
        {
            using (MemoryStream memStream = new MemoryStream(data))
                return CalculateStreamMD5(memStream);
        }

        /// <summary>
        /// Запись байтового массива в файл fileName. 
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string SaveTemporaryFile(string fileName, byte[] data)
        {
            return SaveTemporaryFile(null, data, 0);
        }

        /// <summary>
        /// Запись байтового массива в файл fileName, созданном в каталоге %TEMP%
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string SaveTemporaryFile(byte[] data)
        {
            return SaveTemporaryFile(null, data, 0);
        }

        /// <summary>
        /// Запись байтового массива в файл fileName. Если fileName пусто,  то он будет создан в %TEMP%. Returns REAL filename (path).
        /// Если указан position!=0, то файл будет дозаписан (append).
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="data"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public static string SaveTemporaryFile(string fileName, byte[] data, long position)
        {
            bool createInTempFolder = false;
            string dstFile = Path.GetTempFileName();
            if (String.IsNullOrEmpty(fileName))
            {
                fileName = dstFile;
                createInTempFolder = true;
            }
            else
            {
                dstFile = fileName;
            }

            FileInfo fi = new FileInfo(fileName);
            string ext = fi.Extension;

            DirectoryInfo di = new DirectoryInfo(fi.DirectoryName);
            if (!di.Exists)
                di.Create();

            lock (syncRoot)
            {
                try
                {
                    using (
                        FileStream fstream = new FileStream(fileName,
                                                            (position != 0) ? FileMode.Append : FileMode.OpenOrCreate,
                                                            FileAccess.Write))
                    {
                        BinaryWriter writer = new BinaryWriter(fstream);
                        writer.Write(data);
                        writer.Close();
                    }

                    if (createInTempFolder) // set original file extension
                    {
                        dstFile = String.Format("{0}{1}", fi.FullName, ext);
                        fi.MoveTo(dstFile);
                    }
                }
                catch (Exception ex)
                {
                    dstFile = null;
                    throw ex;
                }
            }

            return dstFile;
        }

        /// <summary>
        /// Чтение блока данных из файла fileName.
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="position"></param>
        /// <param name="bufferSize"></param>
        /// <returns></returns>
        public static byte[] ReadFile(string fileName, long position, int bufferSize)
        {
            byte[] data = null;

            if (!(new FileInfo(fileName)).Exists) return data;
            lock (syncRoot)
                try
                {
                    using (
                        FileStream fstream = new FileStream(fileName, FileMode.Open, FileAccess.Read,
                                                            FileShare.ReadWrite /*!!*/))
                    {
                        BinaryReader reader = new BinaryReader(fstream);
                        fstream.Seek(position, SeekOrigin.Begin);
                        data = reader.ReadBytes(bufferSize);
                        reader.Close();
                    }
                }
                catch
                {
                    throw;
                }
            return data;
        }

        #endregion I/O routines

        #region Parsing

        public static Guid? TryParseGuid(string value)
        {
            try
            {
                return new Guid(value);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static long? TryParseLong(string value)
        {
            long result;
            if (long.TryParse(value, out result))
            {
                return result;
            }
            return null;
        }

        public static decimal TryParseDecimal(string value, decimal defValue)
        {
            decimal result;
            if (decimal.TryParse(value, out result))
            {
                return result;
            }
            return defValue;
        }

        public static decimal? TryParseDecimal(string value)
        {
            decimal result;
            if (decimal.TryParse(value, out result))
            {
                return result;
            }
            return null;
        }

        public static double TryParseDouble(string value, double defValue)
        {
            double result;
            if (double.TryParse(value, out result))
            {
                return result;
            }
            return defValue;
        }

        public static double? TryParseDouble(string value)
        {
            double result;
            if (double.TryParse(value, out result))
            {
                return result;
            }
            return null;
        }

        public static DateTime TryParseDateTime(string value, DateTime defValue)
        {
            DateTime result;
            double dResult;

            if (DateTime.TryParse(value, out result))
            {
                return result;
            }

            if (double.TryParse(value, out dResult))
            {
                return DateTime.FromOADate(dResult);
            }

            return defValue;
        }

        public static DateTime? TryParseDateTime(string value)
        {
            DateTime result;
            if (DateTime.TryParse(value, out result))
            {
                return result;
            }
            return null;
        }

        public static bool TryParseCurrencyDecimal(string value, out decimal resultDecimal)
        {
            char separatorInvariant = System.Globalization.NumberFormatInfo.InvariantInfo.CurrencyDecimalSeparator[0];
            char separatorCurrency = System.Globalization.NumberFormatInfo.CurrentInfo.CurrencyDecimalSeparator[0];
            char point = '.';
            char comma = ',';

            char separator = '.';

            bool allDigits = true;

            value = value.Trim();
            foreach (char ch in value)
            {
                if (!Char.IsDigit(ch) &&
                    !(ch == separatorCurrency || ch == separatorInvariant || ch == comma || ch == point))
                {
                    allDigits = false;
                    break;
                }
                if (allDigits && (ch == separatorCurrency || ch == separatorInvariant || ch == comma || ch == point))
                    separator = ch;
            }

            bool result = false;

            resultDecimal = Decimal.MinusOne;

            if (allDigits)
            {
                try
                {
                    CultureInfo cci = new CultureInfo(System.Threading.Thread.CurrentThread.CurrentCulture.Name);
                    cci.NumberFormat.NumberDecimalSeparator = separator.ToString();
                    result = Decimal.TryParse(value,
                                              System.Globalization.NumberStyles.Currency |
                                              System.Globalization.NumberStyles.AllowCurrencySymbol, cci,
                                              out resultDecimal);
                }
                catch (Exception ex)
                {
                    string err = ex.ToString();
                }
            }

            return result;
        }

        public static int TryParseInt(string value, int defValue)
        {
            int result;
            if (int.TryParse(value, out result))
            {
                return result;
            }
            return defValue;
        }

        public static int TryParseInt_AllowCurrencySymbol(string value, int defValue)
        {
            CultureInfo cci = new CultureInfo(System.Threading.Thread.CurrentThread.CurrentCulture.Name);

            int result;
            if (int.TryParse(value, NumberStyles.Any, cci, out result))
            {
                return result;
            }
            return defValue;
        }

        public static int? TryParseInt(string value)
        {
            int result;
            if (int.TryParse(value, out result))
            {
                return result;
            }
            return null;
        }

        public static long TryParseLong(string value, long defValue)
        {
            long result;
            if (long.TryParse(value, out result))
            {
                return result;
            }
            return defValue;
        }

        public static Guid TryParseGuid(string value, Guid defValue)
        {
            try
            {
                return new Guid(value);
            }
            catch (Exception)
            {
                return defValue;
            }
        }

        public static bool TryParseBool(string value, bool defValue)
        {
            bool result;
            if (bool.TryParse(value, out result))
            {
                return result;
            }
            return defValue;
        }

        public static bool? TryParseBool(string value)
        {
            bool result;
            if (bool.TryParse(value, out result))
            {
                return result;
            }
            return null;
        }

        public static bool TryParseBoolSql(string value, bool defValue)
        {
            bool result;

            if (bool.TryParse(value, out result))
                return result;

            if (string.IsNullOrEmpty(value))
                return false;

            if ((value == "1") || (value.ToUpper() == "ДА") || (value.ToUpper() == "ТАК") || (value.ToUpper() == "YES"))
                return true;

            return defValue;
        }


        /// <summary>
        /// COPY_PAST FROM CodaSQL.Directory_MatchCS
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static int MatchString(string a, string b)
        {
            // declare @s varchar(16), @n int, @xlen int, @ylen int, @count int;
            string s;
            int n, xlen, ylen;
            int count;

            // set @x = upper(@x); set @x = replace(@x, 'І', 'И'); -- Ukrainian
            // set @x = replace(@x, ' ', ''); set @x = replace(@x, '.', ''); set @x = replace(@x, ',', ''); set @x = replace(@x, '/', ''); set @x = replace(@x, '+', '');
            // set @x = replace(@x, '"', ''); set @x = replace(@x, '(', ''); set @x = replace(@x, ')', ''); set @x = replace(@x, '*', ''); set @x = replace(@x, '~', '');
            // set @xlen = len(@x);

            string x = a.ToUpper();
            x = x.Replace("І", "И");
            x = x.Replace(" ", "");
            x = x.Replace(".", "");
            x = x.Replace(",", "");
            x = x.Replace("/", "");
            x = x.Replace("+", "");
            x = x.Replace("\"", "");
            x = x.Replace("(", "");
            x = x.Replace(")", "");
            x = x.Replace("*", "");
            x = x.Replace("~", "");
            xlen = x.Length;

            // set @y = upper(@y); set @y = replace(@y, 'І', 'И'); -- Ukrainian
            // set @y = replace(@y, ' ', ''); set @y = replace(@y, '.', ''); set @y = replace(@y, ',', ''); set @y = replace(@y, '/', ''); set @y = replace(@y, '+', '');
            // set @y = replace(@y, '"', ''); set @y = replace(@y, '(', ''); set @y = replace(@y, ')', ''); set @y = replace(@y, '*', ''); set @y = replace(@y, '~', '');
            // set @ylen = len(@y);
            string y = b.ToUpper();
            y = y.Replace("І", "И");
            y = y.Replace(" ", "");
            y = y.Replace(".", "");
            y = y.Replace(",", "");
            y = y.Replace("/", "");
            y = y.Replace("+", "");
            y = y.Replace("\"", "");
            y = y.Replace("(", "");
            y = y.Replace(")", "");
            y = y.Replace("*", "");
            y = y.Replace("~", "");
            ylen = y.Length;

            // set @count = 0;
            count = 0;

            // set @n = 1; while (@n <= @xlen - 5 + 1) begin set @s = substring(@x, @n, 5); if (charindex(@s, @y, @n) > 0) set @count = @count + 5; set @n = @n + 1; end;
            // set @n = 1; while (@n <= @ylen - 5 + 1) begin set @s = substring(@y, @n, 5); if (charindex(@s, @x, @n) > 0) set @count = @count + 5; set @n = @n + 1; end;
            // set @n = 1; while (@n <= @xlen - 4 + 1) begin set @s = substring(@x, @n, 4); if (charindex(@s, @y, @n) > 0) set @count = @count + 4; set @n = @n + 1; end;
            // set @n = 1; while (@n <= @ylen - 4 + 1) begin set @s = substring(@y, @n, 4); if (charindex(@s, @x, @n) > 0) set @count = @count + 4; set @n = @n + 1; end;
            // set @n = 1; while (@n <= @xlen - 3 + 1) begin set @s = substring(@x, @n, 3); if (charindex(@s, @y, @n) > 0) set @count = @count + 2; set @n = @n + 1; end;
            // set @n = 1; while (@n <= @ylen - 3 + 1) begin set @s = substring(@y, @n, 3); if (charindex(@s, @x, @n) > 0) set @count = @count + 2; set @n = @n + 1; end;
            // set @n = 1; while (@n <= @xlen - 2 + 1) begin set @s = substring(@x, @n, 2); if (charindex(@s, @y, @n) > 0) set @count = @count + 1; set @n = @n + 1; end;
            // set @n = 1; while (@n <= @ylen - 2 + 1) begin set @s = substring(@y, @n, 2); if (charindex(@s, @x, @n) > 0) set @count = @count + 1; set @n = @n + 1; end;
            n = 0;
            while (n <= xlen - 5 && n < ylen)
            {
                s = x.Substring(n, 5);
                if (y.IndexOf(s, n) >= 0) count += 5;
                n++;
            }
            n = 0;
            while (n <= ylen - 5 && n < xlen)
            {
                s = y.Substring(n, 5);
                if (x.IndexOf(s, n) >= 0) count += 5;
                n++;
            }
            n = 0;
            while (n <= xlen - 4 && n < ylen)
            {
                s = x.Substring(n, 4);
                if (y.IndexOf(s, n) >= 0) count += 4;
                n++;
            }
            n = 0;
            while (n <= ylen - 4 && n < xlen)
            {
                s = y.Substring(n, 4);
                if (x.IndexOf(s, n) >= 0) count += 4;
                n++;
            }
            n = 0;
            while (n <= xlen - 3 && n < ylen)
            {
                s = x.Substring(n, 3);
                if (y.IndexOf(s, n) >= 0) count += 2;
                n++;
            }
            n = 0;
            while (n <= ylen - 3 && n < xlen)
            {
                s = y.Substring(n, 3);
                if (x.IndexOf(s, n) >= 0) count += 2;
                n++;
            }
            n = 0;
            while (n <= xlen - 2 && n < ylen)
            {
                s = x.Substring(n, 2);
                if (y.IndexOf(s, n) >= 0) count += 1;
                n++;
            }
            n = 0;
            while (n <= ylen - 2 && n < xlen)
            {
                s = y.Substring(n, 2);
                if (x.IndexOf(s, n) >= 0) count += 1;
                n++;
            }

            // return @count;
            return count;
        }

        /// <summary>
        /// matches input against dictionary values and returns best key 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public static string MatchDictionary(string input, Dictionary<string, string> dictionary)
        {
            string result = "";
            int max = 0;
            int n = 0;

            foreach (string key in dictionary.Keys)
            {
                n = Util.MatchString(input, dictionary[key]);
                if (n >= max)
                {
                    max = n;
                    result = key;
                }
            }

            return result;
        }

        /// <summary>
        /// Invoke-вызов метода methodName(parameters) 
        /// Обработка всех исключений проводить в вызывающем коде.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="methodName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static Object InvokeMethod(Object target, string methodName, object[] parameters)
        {
            string assemblyName = methodName.Substring(0, methodName.IndexOf('.'));
            string className = methodName.Substring(0, methodName.LastIndexOf('.'));
            string method = methodName.Substring(methodName.LastIndexOf('.') + 1);

            Assembly assembly = Assembly.Load(assemblyName);
            Type type = assembly.GetType(className);

            if (type == null)
                throw new TargetInvocationException(new Exception("Type not found !"));

            MethodInfo info = type.GetMethod(method);
            if (info == null)
                throw new TargetInvocationException(new Exception("MethodInfo not found !"));

            object result = info.Invoke(target, parameters);
            return result;
        }

        #endregion

        #region Cashdesk

        public static int CashdeskQuantityToInt(decimal value)
        {
            return Convert.ToInt32(Math.Round(value * 1000));
        }

        public static int CashdeskPriceToInt(decimal value)
        {
            return Convert.ToInt32(Math.Round(value * 100));
        }

        public static int CashdeskTaxToInt(decimal value)
        {
            if (value == 20.0m)
                return 1;

            return 2;
        }

        public static bool IsCashdeskQuantitySimple(decimal value)
        {
            if ((value - Math.Truncate(value)) == 0)
                return true;

            return false;
        }

        #endregion
    }
}