using System;
using System.Collections.Generic;

namespace Moedelo.Infrastructure.AspNetCore.Swagger
{
    public class MoedeloSwaggerSettings
    {
        public string AppName { get; set; }
        public string GroupNameTitle { get; set; }
        public bool FixEnumComments { get; set; }
        public List<Type> DocumentFilters { get; set; } = [];
    }
}
