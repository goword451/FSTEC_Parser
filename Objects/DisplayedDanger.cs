
namespace FSTEC.Objects
{
    public class DisplayedDanger
    {
        public int Id { get; set; }
        public string DisplayedId { get; set; }
        public string Name { get; set; }
        public Danger Danger { get; set; }
        public DisplayedDanger(Danger danger)
        {
            this.Id = danger.Id;
            this.DisplayedId = "УБИ." + danger.Id;
            this.Name = danger.Name;
            this.Danger = danger;
        }
        
    }
}
