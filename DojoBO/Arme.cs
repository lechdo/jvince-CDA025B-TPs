using System.ComponentModel.DataAnnotations.Schema;

namespace DojoBO
{
    public class Arme
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public int Degats { get; set; }
    }
}