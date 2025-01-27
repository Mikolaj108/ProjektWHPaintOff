namespace WarhammerPaintCenter.Models.Entities
{
    public class Paint
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public bool Own { get; set; }

        // Klucz obcy użytkownika
        public int UserId { get; set; }

        // Właściwość nawigacyjna
        public UserAccount UserAccount { get; set; }
    }
}
