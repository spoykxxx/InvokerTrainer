namespace invoke
{
    public class SpellHelp
    {
        public string SpellIcon { get; set; }
        public string[] Orbs { get; set; }

        public SpellHelp(string spellicon, string[] orbs)
        {
            SpellIcon = spellicon;
            Orbs = orbs;
        }
    }
}
