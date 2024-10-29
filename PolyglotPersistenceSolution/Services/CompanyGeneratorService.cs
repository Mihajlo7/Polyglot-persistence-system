using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public static class CompanyGeneratorService
    {
        public static List<ContractCourierModel> GenerateContractsForCourier(List<CourierModel> couriers, List<CompanyModel> companies)
        {
            List<ContractCourierModel> contracts = new List<ContractCourierModel>();
            Random rand = new Random();
            foreach (var courier in couriers)
            {
                int numOfContracts=rand.Next(4,7);
                HashSet<long> companyIdsUsed = new HashSet<long>();
                for (int i = 0; i < numOfContracts; i++)
                {
                    int randomDays=rand.Next(30,700);
                    ContractCourierModel contract = new ContractCourierModel();
                    contract.CourierId = courier.Id;
                    contract.SerialNumContract = Guid.NewGuid();
                    contract.EndOfContract=DateTime.Now.AddDays(randomDays);

                    while (true)
                    {
                        int index = rand.Next(companies.Count);
                        long companyId = companies[index].Id;
                        if (companyId != courier.Id && companyIdsUsed.Add(companyId))
                        {
                            contract.Company= companies[index];
                            
                            break;
                        }
                    }

                    contracts.Add(contract);
                }

            }
            return contracts;
        }
    }
}
