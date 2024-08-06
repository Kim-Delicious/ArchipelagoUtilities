﻿namespace KaitoKid.ArchipelagoUtilities.Net.Client
{
    public class ArchipelagoItem
    {
        public string Name { get; set; }
        public long Id { get; set; }
        public ItemClassification Classification { get; set; }

        public ArchipelagoItem(string name, long id, ItemClassification classification)
        {
            Name = name;
            Id = id;
            Classification = classification;
        }
    }
}
