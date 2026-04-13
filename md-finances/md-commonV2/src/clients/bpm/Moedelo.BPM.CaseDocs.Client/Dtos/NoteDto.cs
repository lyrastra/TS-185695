using System;

namespace Moedelo.BPM.CaseDocs.Client.Dtos
{
    public class NoteDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid? CaseUpdateId { get; set; }
    }
}
