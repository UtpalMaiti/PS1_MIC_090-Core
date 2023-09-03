using System.Collections.Generic;

namespace BlazorApp.Client.Services
{
    public interface IDataAccess
    {
        Task<IReadOnlyList<Customer>> GetAllCustomersAsync();
        IReadOnlyList<Customer> Users { get; set; }
    }

    public class Customer
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
    public class DataAccess : IDataAccess
    {
        private readonly HttpClient http;

        public DataAccess()
        {
        }
        public IReadOnlyList<Customer> Users { get; set; }

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
