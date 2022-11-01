using System;
using System.IO;

namespace dtp5_contacts_0
{
    class Person
    {
        public string firstname, lastname, phone, address, birthdate;

        private static string input(string userInput)
        {
            Console.Write(userInput);
            return Console.ReadLine();
        }

        public Person(bool ask = false)
        {
            if (ask)
            {
                firstname = input("Firstname: ");
                lastname = input("Lastname: ");
                phone = input("Phonenumber: ");
                address = input("Address: ");
                birthdate = input("Birthdate: ");
            }
        }

        public Person(string[] create)
        {
            firstname = create[0];

            lastname = create[1];

            string[] phones = create[2].Split(';');
            phone = phones[0];

            string[] addresses = create[3].Split(';');
            address = addresses[0];

            birthdate = create[4];
        }
    }

    class Program
    {
        static Person[] contactList = new Person[100];

        public static void Main(string[] args)
        {
            string lastFileName = "address.txt";
            string[] commandLine;

            Console.WriteLine("Welcome!\n");
            helpAndWelcome();

            do
            {
                Console.Write("> ");
                commandLine = Console.ReadLine().Split(' ');

                if (commandLine[0] == "quit")
                {
                    Console.WriteLine("Goodbye");
                }

                else if (commandLine[0] == "load")
                {
                    if (commandLine.Length < 2)
                    {
                        lastFileName = "address.txt";
                        readContactlistFile(lastFileName);
                    }

                    else
                    {
                        lastFileName = commandLine[1];
                        readContactlistFile(lastFileName);
                    }
                }

                else if (commandLine[0] == "save")
                {
                    if (commandLine.Length < 2)
                    {
                        writeContactlistFile(lastFileName);
                    }

                    else
                    {
                        lastFileName = commandLine[1];
                        writeContactlistFile(lastFileName);
                    }
                }

                else if (commandLine[0] == "new")
                {
                    if (commandLine.Length < 2)
                    {
                        Person pers = new Person(true);
                        personToContactlist(pers);
                    }
                    else
                    {
                        // NYI!
                        Console.WriteLine("Not yet implemented: new /person/");
                    }
                }

                else if (commandLine[0] == "help")
                {
                    helpAndWelcome();
                }

                else
                {
                    Console.WriteLine($"Unknown command: '{commandLine[0]}'");
                }

            } while (commandLine[0] != "quit");
        }

        private static void personToContactlist(Person pers)
        {
            for (int i = 0; i < contactList.Length; i++)
            {
                if (contactList[i] == null)
                {
                    contactList[i] = pers;
                    break;
                }
            }
        }

        private static void writeContactlistFile(string lastFileName)
        {
            using (StreamWriter outfile = new StreamWriter(lastFileName))
            {
                foreach (Person pers in contactList)
                {
                    if (pers != null)
                        outfile.WriteLine($"{pers.firstname}|{pers.lastname}|{pers.phone}|{pers.address}|{pers.birthdate}");
                }
            }
        }

        private static void readContactlistFile(string lastFileName)
        {
            using (StreamReader infile = new StreamReader(lastFileName))
            {
                string line;
                while ((line = infile.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                    string[] create = line.Split('|');

                    Person pers = new Person(create);

                    personToContactlist(pers);
                }
            }
        }



        private static void helpAndWelcome()
        {
            Console.WriteLine("Avaliable commands: ");
            Console.WriteLine("  delete                         - emtpy the contact list");
            Console.WriteLine("  delete /firstname/ /lastname/  - delete a person");
            Console.WriteLine("  load                           - load contact list data from the file address.lis");
            Console.WriteLine("  load /file/                    - load contact list data from the file");
            Console.WriteLine("  new                            - create new person");
            Console.WriteLine("  new /persname/ /surname/       - create new person with personal name and surname");
            Console.WriteLine("  quit                           - quit the program");
            Console.WriteLine("  save                           - save contact list data to the file previously loaded");
            Console.WriteLine("  save /file/                    - save contact list data to the file");
            Console.WriteLine();
        }
    }
}
