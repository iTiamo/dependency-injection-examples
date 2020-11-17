using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dependency_injection_examples.Services
{
    // Normally we would use a separate file, but I declare it here now for brevity!
    public interface IEmailService
    {
        public void SendMail(string addressee, string message);
        public void SendMailToAllAddressees(string message);
    }

    public class EmailService : IEmailService
    {
        private readonly IAddressBookService _addressBookService;

        public EmailService(IAddressBookService addressBookService)
        {
            _addressBookService = addressBookService;
        }

        public void SendMail(string addressee, string message)
        {
            Console.WriteLine($"Sent {message} to {addressee}");
        }

        public void SendMailToAllAddressees(string message)
        {
            List<string> addressees = _addressBookService.GetAllAddressees();

            foreach (string addressee in addressees)
            {
                SendMail(addressee, message);
            }
        }
    }
}
