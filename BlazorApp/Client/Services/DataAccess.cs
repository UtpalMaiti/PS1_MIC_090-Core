using System.Collections.Generic;

namespace BlazorApp.Client.Services
{
    public class DataAccess : IDataAccess
    {
        public async Task<IReadOnlyList<Customer>> GetAllCustomersAsync()
        {
            IReadOnlyList < Customer > customers = new List<Customer>()
            { 
                new Customer() {FirstName="Utpsl", LastName="Mmaiti"}
            };
           

            return await Task.FromResult(customers);
        }
    }
}
