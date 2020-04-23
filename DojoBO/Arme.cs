using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace DojoBO
{
    public class Arme : DojoEntity
    {
        [DisplayName("Arme")]
        public string Nom { get; set; }
        public int Degats { get; set; }
    }
}