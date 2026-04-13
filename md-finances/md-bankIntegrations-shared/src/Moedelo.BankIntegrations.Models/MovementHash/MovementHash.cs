using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using System.Text;

namespace Moedelo.BankIntegrations.Models.MovementHash
{
    public class MovementHash
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("firm_id")]
        public int FirmId { get; set; }

        [Column("partner_id")]
        public int PartnerId { get; set; }

        [Column("number_doc")]
        public string NumberDoc { get; set; }

        [Column("settlement_number")]
        public string SettlementNumber { get; set; }

        [Column("sum")]
        public decimal Sum { get; set; }

        [Column("date")]
        public DateTime Date { get; set; }

        [Column("integration_call_type")]
        public int? IntegrationCallType { get; set; }

        private string hash;

        [Column("hash")]
        public string Hash
        {
            get
            {
                if (string.IsNullOrEmpty(hash))
                    hash = CreateMD5();
                return hash;
            }
            set
            {
                hash = value;
            }
        }

        /// <summary> нельзя менять порядок формирование хэш'а если не хотим повторной отправке всех выписок в импорт /// </summary>
        public string StrForMD5
        {
            get
            {
                return $"{FirmId}-{PartnerId}-{NumberDoc}-{SettlementNumber}-{Sum.ToString("0.##")}-{Date.ToString("yyyy-MM-dd hh:mm:ss")}";
            }
        }
        private string CreateMD5()
        {
            using (MD5 md5 = MD5.Create())
            {
                var inputBytes = System.Text.Encoding.UTF8.GetBytes(StrForMD5);
                var hashBytes = md5.ComputeHash(inputBytes);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }

    }
}
