using System.Collections.Generic;

namespace Moedelo.Billing.Abstractions.Dto.PaymentCategory;

public class CategoryDataDto
{
    public bool IsManualEditing { get; set; }
    public IReadOnlyCollection<CategoryDto> Categories { get; set; }
}