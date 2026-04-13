using System.Collections.Generic;
using Moedelo.LinkedDocuments.Enums;

namespace Moedelo.LinkedDocuments.ApiClient.legacy.Dtos;

public class LinksFromRequestDto
{
    public List<long> LinkFromIds { get; set; }
    public LinkType LinkType { get; set; }
    public bool UseReadonlyDb { get; set; }
}