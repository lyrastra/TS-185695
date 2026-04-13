using System.Collections.Generic;

namespace Moedelo.Authorization.Dto;

public class RoleDto
{
    public int Id { get; set; }

    public string RoleCode { get; set; }

    public string Name { get; set; }
        
    public HashSet<int> AccessRules { get; set; }
}