using DAL.Models;

namespace DAL
{
    public class Repository
    {
        private readonly FameFindsContext context;
        public Repository(FameFindsContext Famecontext)
        {
            context = Famecontext;
        }

        public Repository()
        {
        }
        #region CUSTOMER RELATED METHODS
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
        public bool AddCustomer(Customer customer)
        {
            bool status = false;
            try
            {
                context.Customers.Add(customer);
                context.SaveChanges();
                status = true;
            }
            catch (Exception ex)
            {

                status = false;
            }
            return status;
        }
        public bool UpdateCustomerDeatils(Customer customer)
        {
            bool status = false;
            try
            {

                var customerOne = context.Customers.Find(customer.CustomerId);
                if (customerOne != null)
                {
                    customerOne.FullName = customer.FullName;
                    customerOne.PhoneNumber = customer.PhoneNumber;
                    customerOne.Email = customer.Email;
                    context.SaveChanges();
                }

            }
            catch (Exception ex)
            {

                status = false;
            }
            return status;
        }

        #endregion
        #region PRODUCT RELATED METHODS
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
        #endregion
    }
}
