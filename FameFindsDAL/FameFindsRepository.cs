using FameFindsDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FameFindsDAL
{
    public class FameFindsRepository
    {
        private readonly FameFindsContext context;
        public FameFindsRepository(FameFindsContext Famecontext)
        {
            context = Famecontext;
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

        public bool AddVendor(Vendor vendor)
        {
            bool status = false;
            try
            {
                context.Vendors.Add(vendor);
                context.SaveChanges();
                status = true;
            }
            catch (Exception)
            {
                status = false;
            }
            return status;
        }
    }
}
