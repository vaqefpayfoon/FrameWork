using System;
using System.Collections.Generic;
using System.Text;

namespace KavoshFrameWorkCore.Models
{
    public class Enums
    {
        public enum FormStatus
        {
            Pending = 0,
            Verified = 1,
            Rejected = 2
        }
        public enum ContractState
        {
            Active = 1,
            Deactive = 2,
            Finish = 3,
            Invalid = 4
        }
    }
}
