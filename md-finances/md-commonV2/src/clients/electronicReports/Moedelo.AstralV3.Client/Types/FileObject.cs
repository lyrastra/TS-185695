namespace Moedelo.AstralV3.Client.Types
{
    /// <summary>Объект файла, предназначенный для обмена с Астралом</summary>
    public class FileObject
    {
        /// <summary>Название файла</summary>
        public string Name { get; set; }

        /// <summary>Контент файла в BASE64</summary>
        public byte[] Content { get; set; }

        /// <summary>Целочисленный идентификатор файла на сервере Астрала</summary>
        public string Identity { get; set; }

        public byte[] Signatures { get; set; }
    }
}
