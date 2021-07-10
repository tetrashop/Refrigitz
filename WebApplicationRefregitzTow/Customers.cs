using System;

namespace RefrigtzW
{
    [Serializable]
    public class Customers
    {
        private static int NumberOfObjects = 0;
        private const char NULL = '\0';
        private int CustomerID = new int();
        private string Name = new string(NULL, 20);
        private string FamilyName = new string(NULL, 20);
        private string Address = new string(NULL, 20);
        private int TelephonNumber = new int();

        public Customers()
        {

        }
        public int CustomenrNumberOfObjectsAccess
        {
            get => NumberOfObjects;
            set => NumberOfObjects = value;
        }
        public int CustomersCustomerID
        {
            get => CustomerID;
            set => CustomerID = value;
        }
        public string CustomersName
        {
            get => Name;
            set => Name = value;
        }
        public string CustomersFamilyName
        {
            get => FamilyName;
            set => FamilyName = value;
        }
        public string CustomersAddress
        {
            get => Address;
            set => Address = value;
        }
        public int CustomersTellefon
        {
            get => TelephonNumber;
            set => TelephonNumber = value;
        }

    }
}
