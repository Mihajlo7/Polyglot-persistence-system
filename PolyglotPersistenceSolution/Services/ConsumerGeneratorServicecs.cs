using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.ExternalData;
using Core.Models;

namespace Services
{
    public static class ConsumerGeneratorServicecs
    {
        private const int CONSUMER_INDEX = 1_000_000;
        private const int CREDIT_CARD = 2_000_000;

        public static List<ConsumerModel> GenerateConsumers(this List<ConsumerEx> consumersRaw)
        {
            int index = CONSUMER_INDEX;
            int creditCardIndex=CREDIT_CARD;
            List<ConsumerModel> consumers = new List<ConsumerModel>();
            foreach (ConsumerEx consumerRaw in consumersRaw)
            {
                ConsumerModel consumer = new ConsumerModel();
                consumer.FirstName = consumerRaw.FirstName;
                consumer.LastName = consumerRaw.LastName;
                consumer.Password = consumerRaw.Password;
                consumer.Email = consumerRaw.Email;
                consumer.Telephone = consumerRaw.Telephone;
                consumer.BirthDate = consumerRaw.BirthDay;
                consumer.Id = index++;
                consumer.CreditCards = new List<CreditCardModel>();
                consumer.CreditCards.Add(new CreditCardModel() { Id=creditCardIndex++,ConsumerId=consumer.Id,Number=consumerRaw.CreditCardNumber1,CardType=consumerRaw.CreditCardType1});
                if(consumerRaw.CreditCardNumber2 != null)
                {
                    consumer.CreditCards.Add(new CreditCardModel() { Id = creditCardIndex++, ConsumerId = consumer.Id, Number = consumerRaw.CreditCardNumber2, CardType = consumerRaw.CreditCardType2 });
                }
                consumers.Add(consumer);    
            }
            return consumers;
        }
    }
}
