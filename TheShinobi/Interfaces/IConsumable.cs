using TheShinobi.Characters;

namespace TheShinobi.Interfaces
{
    interface IConsumable
    {
        public string Text { get; set; }
        public void Consume(Player player);
    }
}
