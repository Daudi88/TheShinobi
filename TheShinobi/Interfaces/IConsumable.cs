using TheShinobi.Characters;

namespace TheShinobi.Interfaces
{
    interface IConsumable
    {
        public int Healing { get; set; }
        public string Text { get; set; }
        public void Consume(Player player);
    }
}
