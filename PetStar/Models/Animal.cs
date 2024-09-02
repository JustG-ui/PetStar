using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace PetStar.Models
{
    public class Animal
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public int Idade { get; set; }

        public string Especie { get; set; }

        public string CódigoAnimal { get; set; }
    }

}
