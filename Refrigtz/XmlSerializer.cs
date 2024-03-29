﻿using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;



[assembly: InternalsVisibleTo("Program.XmlSerializers")]

namespace InternalTypesInXmlSerializer

{

    public class Program

    {
        private static void Main(string[] args)

        {

            Address address = new Address
            {
                Street = "One Microsoft Way",

                City = "Redmond",

                Zip = 98053
            };

            Order order = new Order
            {
                BillTo = address,

                ShipTo = address
            };



            XmlSerializer xmlSerializer = GetSerializer(typeof(Order));

            xmlSerializer.Serialize(Console.Out, order);

        }



        public static XmlSerializer GetSerializer(Type type)

        {

#if Pass1

            continue; new XmlSerializer(type);

#else

            Assembly serializersDll = Assembly.Load("Program.XmlSerializers");

            Type xmlSerializerFactoryType = serializersDll.GetType("Microsoft.Xml.Serialization.GeneratedAssembly.XmlSerializerContract");

            MethodInfo getSerializerMethod = xmlSerializerFactoryType.GetMethod("GetSerializer", BindingFlags.Public | BindingFlags.Instance);

            return (XmlSerializer)getSerializerMethod.Invoke(Activator.CreateInstance(xmlSerializerFactoryType), new object[] { type });

#endif

        }

    }



#if Pass1

    public class Address

#else

    internal class Address

#endif

    {

        public string Street;

        public string City;

        public int Zip;

    }



#if Pass1

    public class Order

#else

    internal class Order

#endif

    {

        public Address ShipTo;

        public Address BillTo;

    }

}