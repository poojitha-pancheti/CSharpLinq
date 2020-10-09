using CSharp.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp.Biz
{

    public class Company
    {
        public int VendorId { get; set; }
        public string CompanyName { get; set; }
        public string Email { get; set; }

        public OperationResult<bool> PlaceOrder(Collections collections, int quantity,
                                            DateTimeOffset? deliverBy = null,
                                            string instructions = "standard delivery")
        {
            if (collections == null)
                throw new ArgumentNullException(nameof(collections));
            if (quantity <= 0)
                throw new ArgumentOutOfRangeException(nameof(quantity));
            if (deliverBy <= DateTimeOffset.Now)
                throw new ArgumentOutOfRangeException(nameof(deliverBy));

            var success = false;

            var orderTextBuilder = new StringBuilder("Order from Acme, Inc" +
                            System.Environment.NewLine +
                            "Product: " + collections.CollectionsName +
                            System.Environment.NewLine +
                            "Quantity: " + quantity);
            if (deliverBy.HasValue)
            {
                orderTextBuilder.Append(System.Environment.NewLine +
                            "Deliver By: " + deliverBy.Value.ToString("d"));
            }
            if (!String.IsNullOrWhiteSpace(instructions))
            {
                orderTextBuilder.Append(System.Environment.NewLine +
                            "Instructions: " + instructions);
            }
            var orderText = orderTextBuilder.ToString();

            var emailService = new EmailService();
            var confirmation = emailService.SendMessage("New Order", orderText,
                                                                     this.Email);
            if (confirmation.StartsWith("Message sent:"))
            {
                success = true;
            }
            var operationResult = new OperationResult<bool>(success, orderText);
            return operationResult;
        }

        public override string ToString()
        {
            return $"Vendor: {this.CompanyName} ({this.VendorId})";
        }

        public override bool Equals(object obj)
        {
            if (obj == null || this.GetType() != obj.GetType())
                return false;

            Company compareVendor = obj as Company;

            if (compareVendor != null &&
                this.VendorId == compareVendor.VendorId &&
                this.CompanyName == compareVendor.CompanyName &&
                this.Email == compareVendor.Email)

                return true;

            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public static List<string> SendEmail(ICollection<Company> companies, string message)
        {

            var confirmations = new List<string>();
            var emailService = new EmailService();

            Console.WriteLine(companies.Count);

            foreach (var company in companies)
            {
                var Subject = "Important message for: " + company.CompanyName;


                var confirmation = emailService.SendMessage(Subject,
                                                         message,
                                                         company.Email);
                confirmations.Add(confirmation);
            }
            return confirmations;
        }

    }
}
