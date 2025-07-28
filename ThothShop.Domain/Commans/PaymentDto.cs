using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThothShop.Domain.Commans
{
    public record PaymentDto(int Id, int OrderId, decimal Amount, string? PaymentMethod, string Status, string? TransactionId);
}
