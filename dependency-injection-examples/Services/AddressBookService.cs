using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dependency_injection_examples.Services
{
    // Normally we would use a separate file, but I declare it here now for brevity!
    public interface IAddressBookService
    {
        public List<string> GetAllAddressees();
    }

    public class AddressBookService : IAddressBookService
    {
        public List<string> GetAllAddressees()
        {
            return new List<string>
            {
                "user@mail.com",
                "anotheruser@mail.com",
                "differentuser@company.com"
            };
        }
    }
}
