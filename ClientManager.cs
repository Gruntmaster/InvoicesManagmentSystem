namespace InvoicesManagmentSystem
{
    public class ClientManager
    {
        private const string FilePath = @"D:\C# projects folder\Nauka_C#\InvoicesManagmentSystem\data\clients.csv";

        public void CreateClient()
        {
            Console.WriteLine("Podaj nazwę klienta");
            string name = Console.ReadLine();

            Console.WriteLine("Podaj NIP klienta");
            string nip = Console.ReadLine();

            Console.WriteLine("Podaj miasto klienta");
            string city = Console.ReadLine();

            Console.WriteLine("Podaj ulicę klienta");
            string street = Console.ReadLine();

            Console.WriteLine("Podaj numer domu klienta");
            string homeNumber = Console.ReadLine();

            Console.WriteLine("Podaj numer mieszkania klienta");
            string localNumber = Console.ReadLine();

            Console.WriteLine("Podaj email klienta");
            string email = Console.ReadLine();

            Console.WriteLine("Podaj telefon klienta");
            string phone = Console.ReadLine();

            List<Client> clients = GetClients();

            int newId = 1;
            if(clients.Any())
            {
                newId = clients.Max(x => x.Id) + 1;
            }

            Client client = new Client
            {
                Id = newId,
                Name = name,
                NIP = nip,
                City = city,
                Street = street,
                HomeNumber = homeNumber,
                LocalNumber = localNumber,
                Email = email,
                Phone = phone
            };
            clients.Add(client);
            SaveData(clients);
        }

        public void PrintAllClients()
        {
            List<Client> clients = GetClients();
            foreach (Client client in clients)
            {
                client.PrintData();
            }
        }

        public void EditClient() //EDIT
        {
            List<Client> clients = GetClients();
            PrintAllClients();

            Console.WriteLine("Podaj ID klienta, którego chcesz edytować:");
            int id = int.Parse(Console.ReadLine());

            Client client = clients.FirstOrDefault(c => c.Id == id);
            if (client == null)
            {
                Console.WriteLine("Klient o podanym ID nie istnieje.");
                return;
            }

            Console.WriteLine("Edytuj dane klienta (pozostaw puste, aby nie zmieniać):");

            Console.Write($"Nazwa: ({client.Name})");
            string name = Console.ReadLine();
            client.Name = !string.IsNullOrWhiteSpace(name) ? name : client.Name;

            Console.Write($"NIP: ({client.NIP})");
            string nip = Console.ReadLine();
            client.NIP = !string.IsNullOrWhiteSpace(nip) ? nip : client.NIP;

            Console.Write($"Miasto: ({client.City})");
            string city = Console.ReadLine();
            client.City = !string.IsNullOrWhiteSpace(city) ? city : client.City;

            Console.Write($"Ulica: ({client.Street})");
            string street = Console.ReadLine();
            client.Street = !string.IsNullOrWhiteSpace(street) ? street : client.Street;

            Console.Write($"Numer domu: ({client.HomeNumber})");
            string homeNumber = Console.ReadLine();
            client.HomeNumber = !string.IsNullOrWhiteSpace(homeNumber) ? homeNumber : client.HomeNumber;

            Console.Write($"Numer mieszkania: ({client.LocalNumber})");
            string localNumber = Console.ReadLine();
            client.LocalNumber = !string.IsNullOrWhiteSpace(localNumber) ? localNumber : client.LocalNumber;

            Console.Write($"Email: ({client.Email})");
            string email = Console.ReadLine();
            client.Email = !string.IsNullOrWhiteSpace(email) ? email : client.Email;

            Console.Write($"Telefon: ({client.Phone})");
            string phone = Console.ReadLine();
            client.Phone = !string.IsNullOrWhiteSpace(phone) ? phone : client.Phone;

            SaveData(clients);
            Console.WriteLine("Dane klienta zaktualizowane.");
        }

        public void DeleteClient() //DELETE
        {
            List<Client> clients = GetClients();
            PrintAllClients();

            Console.WriteLine("Podaj ID klienta do usunięcia:");
            int id = int.Parse(Console.ReadLine());

            Client client = clients.FirstOrDefault(c => c.Id == id);
            if (client == null)
            {
                Console.WriteLine("Klient o podanym ID nie istnieje.");
                return;
            }

            clients.Remove(client);
            SaveData(clients);
            Console.WriteLine("Klient usunięty.");
        }

        public void SearchClientByName() //SEARCH
        {
            Console.WriteLine("Podaj nazwę klienta do wyszukania:");
            string name = Console.ReadLine();

            List<Client> clients = GetClients();
            var matchingClients = clients.Where(c => c.Name
            .Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();

            if (matchingClients.Any())
            {
                Console.WriteLine("Znaleziono klientów:");
                foreach (var client in matchingClients)
                {
                    client.PrintData();
                }
            }
            else
            {
                Console.WriteLine("Nie znaleziono klientów o podanej nazwie.");
            }
        }

        public List<Client> GetClients()
        {
            List<Client> clients = new List<Client>();
            string[] lines = File
                .ReadAllLines(FilePath)
                .Skip(1)
                .ToArray();

            foreach (string line in lines)
            {
                Client client = Client.FromCSV(line);
                clients.Add(client);
            }
            return clients;
        }

        public string GetClientNameById(int clientId)
        {
            List<Client> clients = GetClients();
            var client = clients.FirstOrDefault(c => c.Id == clientId);
            return client != null ? client.Name : "Unknown Client";
        }


        private void SaveData(List<Client> clients)
        {
            List<string> lines = new List<string>()
            {
                "Id,Name,NIP,City,Street,HomeNumber,LocalNumber,Email,Phone"
            };

            foreach (Client client in clients)
            {
                string line = client.ToCSVLine();
                lines.Add(line);
            }

            File.WriteAllLines(FilePath, lines);
        }
    }
}
