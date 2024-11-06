namespace InvoicesManagmentSystem
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NIP { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string HomeNumber { get; set; }
        public string LocalNumber { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public void PrintData()
        {
            string line = ToCSVLine();
            Console.WriteLine(line);
        }

        public string ToCSVLine()
        {
            return $"{Id},{Name},{NIP},{City},{Street},{HomeNumber},{LocalNumber},{Email},{Phone}";
        }

        public static Client FromCSV(string line)
        {
            string[] splited = line.Split(',');
            return new Client
            {
                Id = int.Parse(splited[0]),
                Name = splited[1],
                NIP = splited[2],
                City = splited[3],
                Street = splited[4],
                HomeNumber = splited[5],
                LocalNumber = splited[6],
                Email = splited[7],
                Phone = splited[8],
            };
        }
    }
}
