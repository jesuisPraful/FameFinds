using DAL.Models;

namespace DAL
{
    public class Repository
    {
        FameFindsContext context {  get; set; }
        public Repository()
        {
            context = new FameFindsContext();
        }

        public List<Customer> getAllCustomers()
        {
            List<Customer> customers = new List<Customer>();
            try
            {
                customers=context.Customers.ToList();
            }
            catch (Exception ex)
            {
                customers = null;
            }
            return customers;
        } 

    }
}
