namespace Moedelo.BPM.CaseDocs.Client.Dtos
{
    public class DataParsedDto
    {
        public bool DataParsed { get; set; }

        public DataParsedDto(bool dataParsed)
        {
            DataParsed = dataParsed;
        }
    }
}