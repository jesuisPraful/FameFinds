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
                Console.WriteLine(ex.Message);
                customers = null;
            }
            return customers;
        } 
       

        public List<Product> getAllProducts()
        {
            List<Product> products = new List<Product>();
            try
            {
                products=context.Products.ToList();
            }
            catch (Exception ex)
            {
                products = null;
            }
            return products;
        }

    }
}
