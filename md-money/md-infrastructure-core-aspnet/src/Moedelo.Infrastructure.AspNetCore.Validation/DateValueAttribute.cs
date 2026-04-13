using System.ComponentModel.DataAnnotations;

namespace Moedelo.Infrastructure.AspNetCore.Validation
{
    public class DateValueAttribute : DataTypeAttribute
    {
        public DateValueAttribute()
            : base(DataType.Date)
        {
            ErrorMessage = "Поле должно содержать только дату.";
        }
    }
}