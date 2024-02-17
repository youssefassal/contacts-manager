using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Remoting.Services;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace ContactManager
{
    class Program
    {
        static void Main(string[] args)
        {
            ContactStore contactStore = new ContactStore();
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("___________________Welcome to Contact Manager_________________");
                Console.WriteLine();
                Console.WriteLine("1_ Add Contact");
                Console.WriteLine("2_ Edit Contact");
                Console.WriteLine("3_ Delete Contact");
                Console.WriteLine("4_ Search Contact");
                Console.WriteLine("5_ Show All Contact");
                Console.WriteLine("6_ Exit");
                Console.WriteLine();

                Console.WriteLine("Enter your choice please ");
                int choice = int.Parse(Console.ReadLine());
                Console.WriteLine();


                switch (choice)
                {
                    case 1:
                        contactStore.AddContact();
                        break;
                    case 2:
                        contactStore.EditContact();
                        break;
                    case 3:
                        contactStore.DeleteContact();
                        break;
                    case 4:
                        contactStore.SearchContact();
                        break; 
                    case 5:
                        contactStore.ShowAllContacts();
                        break;
                    case 6:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again");
                        break;
                }
                Console.WriteLine();
            }
        }
    }

    class ContactStore
    {
        private List<User> users;
        public ContactStore()
        {
            users = new List<User> ();
        }
        public void AddContact()
        {
            User user = new User();
            Console.WriteLine("Enter user details:");
            Console.WriteLine("ID: ");
            user.Id = int.Parse(Console.ReadLine());
            Console.WriteLine("First name: ");
            user.FirstName = Console.ReadLine();
            Console.WriteLine("Last name: ");
            user.LastName = Console.ReadLine();
            Console.WriteLine("Gender: ");
            user.Gender = Console.ReadLine();
            Console.WriteLine("City: ");
            user.City = Console.ReadLine();
            user.AddedDate = DateTime.Now;

            Console.WriteLine("enter number of addresses: ");
            int numAddresses = int.Parse(Console.ReadLine());
            for (int i = 0; i < numAddresses; i++)
            {
                Address address = new Address();
                Console.WriteLine("enter address number " + (i + 1) + " details");
                Console.WriteLine("Place: ");
                address.Place = Console.ReadLine();
                Console.WriteLine("Type: ");
                address.Type = Console.ReadLine();
                Console.WriteLine("Description: ");
                address.Description = Console.ReadLine();
                user.Addresses.Add(address);
                Console.WriteLine();
            }

            Console.WriteLine("enter number of Phones: ");
            int numPhones = int.Parse(Console.ReadLine());
            for (int i = 0; i < numPhones; i++)
            {
                Phone phone = new Phone();
                Console.WriteLine("enter Phone number " + (i + 1) + " details.");
                Console.WriteLine("Phone: ");
                phone.Number = Console.ReadLine();
                Console.WriteLine("Type: ");
                phone.Type = Console.ReadLine();
                Console.WriteLine("Description: ");
                phone.Description = Console.ReadLine();
                user.Phones.Add(phone);
                Console.WriteLine();
            }

            Console.WriteLine("enter number of Emails: ");
            int numEmails = int.Parse(Console.ReadLine());
            for (int i = 0; i < numEmails; i++)
            {
                Email email = new Email();
                Console.WriteLine("enter Email " + (i + 1) + " details.");
                Console.WriteLine("Email: ");
                email.Address = Console.ReadLine();
                Console.WriteLine("Type: ");
                email.Type = Console.ReadLine();
                Console.WriteLine("Description: ");
                email.Description = Console.ReadLine();
                user.Emails.Add(email);
                Console.WriteLine();
            }
            users.Add(user);
            Console.WriteLine("Contact added successfully");
        }

        public void EditContact()
        {
            Console.WriteLine("enter the ID of the contactto edit: ");
            int id = int.Parse(Console.ReadLine());
            User user = FindContactById(id);

            if (user != null)
            {
                Console.WriteLine("enter new details for the contact: ");
                Console.WriteLine("First name: ");
                user.FirstName = Console.ReadLine();
                Console.WriteLine("Last name: ");
                user.LastName = Console.ReadLine();
                Console.WriteLine("Gender: ");
                user.Gender = Console.ReadLine();
                Console.WriteLine("City: ");
                user.City = Console.ReadLine();

                Console.WriteLine("enter number of addresses: ");
                int numAddresses = int.Parse(Console.ReadLine());
                for (int i = 0; i < numAddresses; i++)
                {
                    Address address = new Address();
                    Console.WriteLine("enter address number " + (i + 1) + " details");
                    Console.WriteLine("Place: ");
                    address.Place = Console.ReadLine();
                    Console.WriteLine("Type: ");
                    address.Type = Console.ReadLine();
                    Console.WriteLine("Description: ");
                    address.Description = Console.ReadLine();
                    user.Addresses.Add(address);
                    Console.WriteLine();
                }

                Console.WriteLine("enter number of Phones: ");
                int numPhones = int.Parse(Console.ReadLine());
                for (int i = 0; i < numPhones; i++)
                {
                    Phone phone = new Phone();
                    Console.WriteLine("enter Phone number " + (i + 1) + " details.");
                    Console.WriteLine("Phone: ");
                    phone.Number = Console.ReadLine();
                    Console.WriteLine("Type: ");
                    phone.Type = Console.ReadLine();
                    Console.WriteLine("Description: ");
                    phone.Description = Console.ReadLine();
                    user.Phones.Add(phone);
                    Console.WriteLine();

                }

                Console.WriteLine("enter number of Emails: ");
                int numEmails = int.Parse(Console.ReadLine());
                for (int i = 0; i < numEmails; i++)
                {
                    Email email = new Email();
                    Console.WriteLine("enter Email " + (i + 1) + " details.");
                    Console.WriteLine("Email: ");
                    email.Address = Console.ReadLine();
                    Console.WriteLine("Type: ");
                    email.Type = Console.ReadLine();
                    Console.WriteLine("Description: ");
                    email.Description = Console.ReadLine();
                    user.Emails.Add(email);
                    Console.WriteLine();

                }
                Console.WriteLine("Contact edited successfully. ");
            }
            else 
            { 
                Console.WriteLine("Contact not found. "); 
            }
        }
        public void DeleteContact()
        {
            Console.WriteLine("Enter the ID of the contact to delete: ");
            int id = int.Parse(Console.ReadLine());
            User user = FindContactById(id);

            if (user != null)
            {
                users.Remove(user);
                Console.WriteLine("Contact deleted successfully. ");
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Contact not found");
            }
        }
        public void SearchContact()
        {
            Console.WriteLine("enter the search term: ");
            string searchTerm = Console.ReadLine();

            List<User> matchedUsers = new List<User> ();
            foreach (User user in users)
            {
                if (user.SearchUser(searchTerm))
                {
                    matchedUsers.Add(user);
                }
            }
            if (matchedUsers.Count > 0)
            {
                Console.WriteLine();
                Console.WriteLine("Matched contacts: ");
                foreach (User user in users)
                {
                    user.ShowUser();
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("no contact found"); 
            }
        }
        public void ShowAllContacts()
        {
            if (users.Count > 0)
            {
                Console.WriteLine("All contacts: ");
                Console.WriteLine();

                foreach (User user in users)
                {
                    user.ShowUser();
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("no contacts found."); 
            }
        }
        private User FindContactById(int id)
        {
            foreach(User user in users)
            {
                if(user.Id == id)
                {
                    return user;
                }
            }
            return null;
        }
    }
    class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string City { get; set; }
        public DateTime AddedDate { get; set; }
        public List<Address> Addresses { get; set; }
        public List<Phone> Phones { get; set; }
        public List<Email> Emails { get; set; }

        public User()
        {
            Addresses = new List<Address>();
            Phones = new List<Phone>();
            Emails = new List<Email>();
        }

        public bool SearchUser(string searchTerm)
        {
            if (Id.ToString() == searchTerm || 
                FirstName.Contains(searchTerm) ||
                LastName.Contains(searchTerm) || 
                Gender.Contains(searchTerm) || 
                City.Contains(searchTerm)) 
            { 
                return true; 
            }

            foreach (Address address in Addresses)
            {
                if (address.SearchAddress(searchTerm))
                {
                    return true;
                }
            }

            foreach (Phone phone in Phones)
            {
                if (phone.SearchPhone(searchTerm))
                {
                    return true;
                }
            }

            foreach (Email email in Emails)
            {
                if (email.SearchEmail(searchTerm))
                {
                    return true;
                }
            }
            return false;
        }

        public void ShowUser()
        {
            Console.WriteLine("ID: " + Id);
            Console.WriteLine("First Name: " + FirstName);
            Console.WriteLine("Last Name: " + LastName);
            Console.WriteLine("Gender: " + Gender);
            Console.WriteLine("City: " + City);
            Console.WriteLine("Added Date: " + AddedDate);
            Console.WriteLine();

            Console.WriteLine("Addresses: ");
            Console.WriteLine();

            foreach (Address address in Addresses)
            {
                address.ShowAddress();
            }

            Console.WriteLine();
            Console.WriteLine("Phones: ");
            Console.WriteLine();

            foreach (Phone phone in Phones)
            {
                phone.ShowPhone();
            }

            Console.WriteLine();
            Console.WriteLine("Emails: ");
            Console.WriteLine();

            foreach (Email email in Emails)
            {
                email.ShowEmail();
            }
        }
    }
    class Address
    {
        public string Place { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }

        public void ShowAddress()
        {
            Console.WriteLine("Place: " + Place);
            Console.WriteLine("Type: " + Type);
            Console.WriteLine("Description: " + Description);
        }

        public bool SearchAddress(string searchTerm)
        {
            if (Place.Contains(searchTerm) ||
                Type.Contains(searchTerm) ||
                Description.Contains(searchTerm))
            {
                return true;
            }
            return false;
        }

        public string GetAddress()
        {
            return $"{Place}, {Type}, {Description}";
        }
    }
    class Phone
    {
        public string Number { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }

        public void ShowPhone()
        {
            Console.WriteLine("Phone: " + Number);
            Console.WriteLine("Type: " + Type);
            Console.WriteLine("Description: " + Description);
        }

        public bool SearchPhone(string searchTerm)
        {
            if (Number.Contains(searchTerm) ||
                Type.Contains(searchTerm) ||
                Description.Contains(searchTerm))
            {
                return true;
            }
            return false;
        }

        public string GetPhone()
        {
            return $"{Number}, {Type}, {Description}";
        }
    }
    class Email
    {
        public string Address { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }

        public void ShowEmail()
        {
            Console.WriteLine("Email: " + Address);
            Console.WriteLine("Type: " + Type);
            Console.WriteLine("Description: " + Description);
        }

        public bool SearchEmail(string searchTerm)
        {
            if (Address.Contains(searchTerm) ||
                Type.Contains(searchTerm) ||
                Description.Contains(searchTerm))
            {
                return true;
            }
            return false;
        }
        public string GetEmail()
        {
            return $"{Address}, {Type}, {Description}";
        }
    }
}