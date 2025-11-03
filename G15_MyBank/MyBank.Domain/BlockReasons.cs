using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBank.Domain
{
    public static class BlockReasons
    {
            public const string Lost = "Lost";
            public const string Stolen = "Stolen";
            public const string UserFreeze = "UserFreeze";
            public const string Fraud = "Fraud";
            public const string KYC = "KYC";
            public const string RiskRule = "RiskRule";
            public const string PINAttemptsExceeded = "PINAttemptsExceeded";
    }
}