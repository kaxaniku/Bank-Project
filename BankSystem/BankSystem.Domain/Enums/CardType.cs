using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Domain.Enums;

public enum CardType : byte
{
    Debit = 1,
    Credit = 2,
    Prepaid = 3
}
