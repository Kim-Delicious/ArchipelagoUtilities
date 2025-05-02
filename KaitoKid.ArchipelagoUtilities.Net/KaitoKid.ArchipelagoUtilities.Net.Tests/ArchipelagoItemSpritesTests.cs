﻿using Archipelago.MultiClient.Net.Enums;
using FluentAssertions;
using KaitoKid.ArchipelagoUtilities.Net.Client;
using KaitoKid.ArchipelagoUtilities.Net.Interfaces;
using KaitoKid.ArchipelagoUtilities.Net.ItemSprites;

namespace KaitoKid.ArchipelagoUtilities.Net.Tests
{
    internal class ArchipelagoItemSpritesTests
    {
        private ILogger _logger;
        private ArchipelagoItemSprites _itemSprites;

        [SetUp]
        public void SetUp()
        {
            _logger = null;
        }

        [TestCase("Hollow Knight", "Desolate_Dive", true, true, null)]
        [TestCase("Stardew Valley", "Desolate_Dive", true, false, null)]
        [TestCase("Hollow Knight", "Combat Level", true, false, null)]
        [TestCase("Hollow Knight", "ABDJF", true, true, "")]
        [TestCase("ANOGF", "ABDJF", false, false, null)]
        [TestCase("A Hat in Time", "25 Pons", true, true, "Pons")]
        [TestCase("A Hat in Time", "50 Pons", true, true, "Pons")]
        [TestCase("A Hat in Time", "75 Pons", true, true, "")]
        [TestCase("A Hat in Time", "100 Pons", true, true, "Pons")]
        [TestCase("A Hat in Time", "Relic (Necklace Bust)", true, true, "Relic (Necklace Bust)")]
        [TestCase("A Hat in Time", "Relic (Necklace)", true, true, "Relic Necklace")]
        [TestCase("A Hat in Time", "Relic (Red Crayon)", true, true, "Relic (Red Crayon)")]
        public void Constructor_LoadsSpritesProperly(string gameName, string itemName, bool expectedSuccess, bool expectedCorrectGame, string expectedItem)
        {
            // Arrange
            if (expectedItem == null)
            {
                expectedItem = itemName;
            }
            _itemSprites = new ArchipelagoItemSprites(_logger);
            var scoutedLocation = new ScoutedLocation("", itemName, "", gameName, 1, 2, 3, ItemFlags.Advancement);

            // Act
            var success = _itemSprites.TryGetCustomAsset(scoutedLocation, "Stardew Valley", true, true, out var sprite);

            // Assert
            success.Should().Be(expectedSuccess);
            if (expectedSuccess)
            {
                sprite.Item.Should().Be(expectedItem);
                if (expectedCorrectGame)
                {
                    sprite.Game.Should().Be(gameName);
                    var expectedFilePath = $"{gameName}_{expectedItem}.png";
                    if (expectedItem == "")
                    {
                        expectedFilePath = $"{gameName}.png";
                    }
                    sprite.FilePath.Should().EndWith($@"Custom Assets\{gameName}\{expectedFilePath}");
                }
            }
        }
    }
}
