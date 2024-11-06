namespace InvoicesManagmentSystem
{
    public class Invoice
    {
        public Invoice()
        {
            Date = DateTime.Now;
        }

        public int Id { get; set; }
        public string Number { get; set; }
        public DateTime Date { get; set; }
        public int ClientId { get; set; }

        public void PrintData()
        {
            string line = ToCSVLine();
            Console.WriteLine(line);
        }

        public string ToCSVLine()
        {
            return $"{Id},{Number},{Date},{ClientId}";
        }

        public static Invoice FromCSV(string line)
        {
            string[] splited = line.Split(',');
            return new Invoice
            {
                Id = int.Parse(splited[0]),
                Number = splited[1],
                Date = DateTime.Parse(splited[2]),
                ClientId = int.Parse(splited[3])
            };
        }
    }
}
