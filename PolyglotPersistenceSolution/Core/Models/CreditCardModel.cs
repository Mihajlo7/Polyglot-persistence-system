using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class CreditCardModel
    {
        public Guid Id { get; set; }
        public string Number { get; set; }
        public string CardType { get; set; }
        public Guid ConsumerId { get; set; }
    }
}
