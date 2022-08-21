namespace UI.Models
{
    public class Client
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string СodeOfTheCountry { get; set; }

        public string RegionCode { get; set; }

        public string SubscriberNumber { get; set; }

        public string Number 
        {
            get 
            { 
                return $"+{СodeOfTheCountry} ({RegionCode}) {SubscriberNumber}"; 
            }
        }
    }
}
