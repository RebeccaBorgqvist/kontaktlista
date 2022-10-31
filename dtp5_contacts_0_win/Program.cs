using System;
using System.IO;

namespace dtp5_contacts_0
{
    class Person
    {
        public string firstname, lastname, phone, address, birthdate;
    }

    class Program
    {
        static Person[] contactList = new Person[100];

        public static void Main(string[] args)
        {
            string lastFileName = "address.lis";
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

                else if (commandLine[0] == "load") // kopior här TBD
                {
                    if (commandLine.Length < 2)
                    {
                        lastFileName = "address.lis";
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
                        // NYI!
                        Console.WriteLine("Not yet implemented: save /file/");
                    }
                }

                else if (commandLine[0] == "new")
                {
                    if (commandLine.Length < 2)
                    {
                        string firstname = input("Firstname: ");
                        string lastname = input("Lastname: ");                        
                        string phone = input("Phonenumber: ");
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

        private static void writeContactlistFile(string lastFileName)
        {
            using (StreamWriter outfile = new StreamWriter(lastFileName))
            {
                foreach (Person p in contactList)
                {
                    if (p != null)
                        outfile.WriteLine($"{p.firstname};{p.lastname};{p.phone};{p.address};{p.birthdate}");
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
                    string[] attrs = line.Split('|');
                    Person p = new Person();
                    p.firstname = attrs[0];
                    p.lastname = attrs[1];
                    string[] phones = attrs[2].Split(';');
                    p.phone = phones[0];
                    string[] addresses = attrs[3].Split(';');
                    p.address = addresses[0];
                    for (int ix = 0; ix < contactList.Length; ix++)
                    {
                        if (contactList[ix] == null)
                        {
                            contactList[ix] = p;
                            break;
                        }
                    }
                }
            }
        }

        private static string input(string userInput)
        {
            Console.Write(userInput);
            return Console.ReadLine();
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
