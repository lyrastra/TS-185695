using System;
using System.Collections.Generic;

namespace Moedelo.CommonApiV2.Dto.AbTest;

public class CreateUpdateAbTestDto
{
    public int Id { get; set; }

    public string Description { get; set; }

    public string PageUrl { get; set; }

    public bool IsFocusGroup { get; set; }

    public DateTime ExpiryDate { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime ModifyDate { get; set; }

    public List<AbTestVariantDto> Variants { get; set; }

    public List<AbTestConditionDto> Conditions { get; set; }
}