using System.Collections.Generic;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KittyLikeability
{

    //Create unit test for checking correct behavior of IKitty implementation as key
    //paste your unit tests here
    [TestClass]
    public class QuickKittyLikeabilityProviderTests
    {
        private IKittyLikeabilityProvider _kittyLikeabilityProvider;

        [TestInitialize]
        public void Initialize()
        {
            _kittyLikeabilityProvider = new QuickKittyLikeabilityProvider(new KittyLikeabilityCalculator());
        }

        [TestCleanup]
        public void Cleanup()
        {
            _kittyLikeabilityProvider = null;
        }

        [TestMethod]
        public void GetLikeability_SameKeys_HasSameLikeability()
        {
            // Arrange.
            var superKitty = new SuperKitty(4, 2, KittyBreed.Bald, 2);
            var simpleKitty = new SimpleKitty(4, 2, KittyBreed.Bald, 2);

            // Act.
            var superKittyLikeability = _kittyLikeabilityProvider.GetLikeability(superKitty);
            var simpleKittyLikeabilty = _kittyLikeabilityProvider.GetLikeability(simpleKitty);

            // Assert.
            Assert.AreEqual(superKittyLikeability, simpleKittyLikeabilty);
        }

        [TestMethod]
        public void GetLikeability_DifferentKeys_HasDifferentLikeability()
        {
            // Arrange.
            var superKitty = new SuperKitty(5, 3, KittyBreed.Bald, 2);
            var simpleKitty = new SimpleKitty(4, 2, KittyBreed.Bald, 2);

            // Act.
            var superKittyLikeability = _kittyLikeabilityProvider.GetLikeability(superKitty);
            var simpleKittyLikeabilty = _kittyLikeabilityProvider.GetLikeability(simpleKitty);

            // Assert.
            Assert.AreNotEqual(superKittyLikeability, simpleKittyLikeabilty);
        }

        [TestMethod]
        public void GetLikeability_DifferentProvidersAndSameKeys_HasSameLikeability()
        {
            // Arrange.
            var superKitty = new SuperKitty(4, 2, KittyBreed.Bald, 2);
            var simpleKitty = new SimpleKitty(4, 2, KittyBreed.Bald, 2);
            var secondProvider = new QuickKittyLikeabilityProvider(new KittyLikeabilityCalculator());

            // Act.
            var superKittyLikeability = _kittyLikeabilityProvider.GetLikeability(superKitty);
            var simpleKittyLikeabilty = secondProvider.GetLikeability(simpleKitty);

            // Assert.
            Assert.AreEqual(superKittyLikeability, simpleKittyLikeabilty);
        }

        [TestMethod]
        public void GetLikeability_DifferentProvidersAndKeys_HasDifferentLikeability()
        {
            // Arrange.
            var superKitty = new SuperKitty(5, 3, KittyBreed.Bald, 2);
            var simpleKitty = new SimpleKitty(4, 2, KittyBreed.Bald, 2);
            var secondProvider = new QuickKittyLikeabilityProvider(new KittyLikeabilityCalculator());

            // Act.
            var superKittyLikeability = _kittyLikeabilityProvider.GetLikeability(superKitty);
            var simpleKittyLikeabilty = secondProvider.GetLikeability(simpleKitty);

            // Assert.
            Assert.AreNotEqual(superKittyLikeability, simpleKittyLikeabilty);
        }
    }

    public enum KittyBreed
    {
        Black,
        Bald
    }

    public interface IKitty
    {
        int LegCount { get; }
        int EarCount { get; }
        KittyBreed Breed { get; }
        int Weight { get; }
    }

    internal class SuperKitty : IKitty
    {
        public SuperKitty(int legCount, int earCount, KittyBreed breed, int weight)
        {
            LegCount = legCount;
            EarCount = earCount;
            Breed = breed;
            Weight = weight;
        }

        public int LegCount { get; }
        public int EarCount { get; }
        public KittyBreed Breed { get; }
        public int Weight { get; }
    }

    internal class SimpleKitty : IKitty
    {
        public SimpleKitty(int legCount, int earCount, KittyBreed breed, int weight)
        {
            LegCount = legCount;
            EarCount = earCount;
            Breed = breed;
            Weight = weight;
        }

        public int LegCount { get; }
        public int EarCount { get; }
        public KittyBreed Breed { get; }
        public int Weight { get; }
    }

    public interface IKittyLikeabilityProvider
    {
        double GetLikeability(IKitty kitty);
    }

    internal class KittyLikeabilityCalculator : IKittyLikeabilityProvider
    {
        public double GetLikeability(IKitty kitty)
        {
            // a lot of expensive calculations 
            Thread.Sleep(1000);
            return (kitty.LegCount / 4.0) + (kitty.EarCount / 2.0) + (kitty.Weight / 10.0) * (int)kitty.Breed;
        }
    }
    internal class QuickKittyLikeabilityProvider : IKittyLikeabilityProvider
    {
        private readonly Dictionary<IKitty, double> _likeabilityStorage;
        private readonly IKittyLikeabilityProvider _kittyLikeabilityCalculator;
        public QuickKittyLikeabilityProvider(IKittyLikeabilityProvider kittyLikeabilityCalculator)
        {
            _kittyLikeabilityCalculator = kittyLikeabilityCalculator;
            _likeabilityStorage = new Dictionary<IKitty, double>();
        }

        //provide possibility for quick recalculation (Use hashmap)
        //use IKitty implementation as key 
        //use (double) Likeability as value 
        //paste your code here 
        public double GetLikeability(IKitty kitty)
        {
            double likeability;
            var hasLikeability = _likeabilityStorage.TryGetValue(kitty, out likeability);
            if (!hasLikeability)
            {
                likeability = _kittyLikeabilityCalculator.GetLikeability(kitty);
                _likeabilityStorage.Add(kitty, likeability);
            }

            return likeability;
        }
    }
}
