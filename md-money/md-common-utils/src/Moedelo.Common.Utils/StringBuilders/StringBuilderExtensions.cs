using System.Text;

namespace Moedelo.Common.Utils.StringBuilders
{
    public static class StringBuilderExtensions
    {
        public static void AppendNotEmpty(this StringBuilder builder, string value, string format = null)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return;
            }

            if (format == null)
            {
                builder.Append(value.Trim());
            }
            else
            {
                builder.AppendFormat(format, value.Trim());
            }
        }
    }
}