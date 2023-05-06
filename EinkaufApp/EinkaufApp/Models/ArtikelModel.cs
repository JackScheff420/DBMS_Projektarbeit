namespace EinkaufApp.Models
{
    public class ArtikelModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Kind { get; set; }
        public int QuantitiyIs { get; set; }
        public int QuanitityShall { get; set; }
        public int QuantityIncoming { get; set; }
        public int QuantityOutgoing { get; set; }
        public string Lieferant { get; set; }
        public float Price { get; set; }
    }
}
