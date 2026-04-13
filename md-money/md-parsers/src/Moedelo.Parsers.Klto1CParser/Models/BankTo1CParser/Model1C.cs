using System.Collections.Generic;

namespace Moedelo.Parsers.Klto1CParser.Models.BankTo1CParser
{
    public class Model1C
    {
        public Model1CHeader Header { get; set; }
        public List<Model1CDoc> Docs { get; set; }
    }
}