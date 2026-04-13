using System;

namespace Moedelo.ErptV2.Dto.DocumentEditing
{
    public sealed class SaveEditingDataDto
    {
        public string SendDocId { get; }
        public EditingTableType EditingTableType { get; }
        public string OperatorLogin { get; }
        public DateTime? CreateDate { get; }

        public SaveEditingDataDto(string sendDocId, EditingTableType editingTableType, string operatorLogin,
            DateTime? createDate)
        {
            SendDocId = sendDocId;
            EditingTableType = editingTableType;
            OperatorLogin = operatorLogin;
            CreateDate = createDate;
        }
    }
}