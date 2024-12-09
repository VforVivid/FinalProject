namespace FinalProject.Models.Entities
{
    public class CharacterSpell
    {
        public int Id { get; set; }
        public int CharacterId { get; set; }
        public Character? Character { get; set; }
        public int SpellId { get; set; }
        public Spell? Spell { get; set; }
    }
}
