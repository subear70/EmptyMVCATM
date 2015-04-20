using System;

namespace Data.Entities
{
    public class LogInfo
    {
        public int Id { get; set; }
        public int OperationCode { get; set; } //TODO: move to enum
        public long OperationValue { get; set; }
    }
}
