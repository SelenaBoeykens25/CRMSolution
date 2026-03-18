using CRM.Models;

namespace CRMProject.Client.Models
{
    public class KlantViewModel
    {
        public KlantViewModel(Klant klant)
        {
            Id = klant.Id;
            Naam = $"{klant.Voornaam} {klant.Naam}";
        }
        public KlantViewModel(int id, string naam)
        {
            Id = id;
            Naam = naam;
        }

        public int Id { get; set; }
        public string Naam { get; set; }
    }
}
