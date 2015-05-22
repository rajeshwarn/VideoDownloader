using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoDownloader.Core
{
    public class FileSizeFormatProvider : IFormatProvider, ICustomFormatter
    {
        private const string FileSizeFormat = "fs", SpeedFormat = "s";
        private const Decimal OneKiloByte = 1024M;
        private const Decimal OneMegaByte = OneKiloByte * 1024M;
        private const Decimal OneGigaByte = OneMegaByte * 1024M;

        public object GetFormat(Type formatType)
        {
            return formatType == typeof(ICustomFormatter) ? this : null;
        }

        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            if (format == null || (!format.StartsWith(FileSizeFormat) && !format.StartsWith(SpeedFormat)))
            {
                return HandleOtherFormats(format, arg);
            }

            Decimal size;

            try
            {
                size = Convert.ToDecimal(arg);
            }
            catch (InvalidCastException)
            {
                return HandleOtherFormats(format, arg);
            }
            string suffix;
            if (size > OneGigaByte)
            {
                size /= OneGigaByte;
                suffix = "GB";
            }
            else if (size > OneMegaByte)
            {
                size /= OneMegaByte;
                suffix = "MB";
            }
            else if (size > OneKiloByte)
            {
                size /= OneKiloByte;
                suffix = "KB";
            }
            else
            {
                suffix = "Bytes";
            }
            if (format.StartsWith(SpeedFormat)) suffix += "/sec";
            var postion = format.StartsWith(SpeedFormat) ? SpeedFormat.Length : FileSizeFormat.Length;
            var precision = format.Substring(postion);
            if (String.IsNullOrEmpty(precision)) precision = "2";
            return String.Format("{0:N" + precision + "}{1}", size, " " + suffix);
        }

        private string HandleOtherFormats(string format, object arg)
        {
            // ReSharper disable once CanBeReplacedWithTryCastAndCheckForNull
            if (arg is IFormattable)
                return ((IFormattable)arg).ToString(format, CultureInfo.CurrentCulture);
            return arg != null ? arg.ToString() : String.Empty;
        }
    }
}
