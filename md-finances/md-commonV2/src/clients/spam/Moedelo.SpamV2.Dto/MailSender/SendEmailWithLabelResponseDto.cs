using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moedelo.SpamV2.Dto.MailSender
{
    public class SendEmailWithLabelResponseDto
    {
        public string LabelForIdentification { get; set; }

        public bool IsSuccess { get; set; }
    }
}
