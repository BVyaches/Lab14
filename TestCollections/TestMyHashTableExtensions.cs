using MyHashTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChallengeLibrary;

namespace TestCollections
{
    [TestClass]
    public class TestMyHashTableExtensions
    {
        [TestMethod]
        public void WhereTest()
        {
            var hashTable = new MyHashTable<Challenge>();
            Challenge challenge1 = new Challenge("Математика", 25, "Ирина");
            Challenge challenge2 = new Challenge("История", 10, "Дима");
            Challenge challenge3 = new Challenge("Физра", 20, "Ваня");
            Challenge challenge4 = new Challenge("ИЗО", 5, "Сеня");
            hashTable.Add(challenge1);
            hashTable.Add(challenge2);
            hashTable.Add(challenge3);
            hashTable.Add(challenge4);

            IEnumerable<Challenge> result = hashTable.Where(x => x.TimeToPass % 2 == 0);
            bool assertion = result.Contains(challenge3)&& result.Contains(challenge2);
            
            Assert.IsTrue(assertion);
        }

        [TestMethod]
        public void AggregateTest()
        {
            var hashTable = new MyHashTable<Challenge>();
            Challenge challenge1 = new Challenge("Математика", 25, "Ирина");
            Challenge challenge2 = new Challenge("История", 10, "Дима");
            Challenge challenge3 = new Challenge("Физра", 20, "Ваня");
            Challenge challenge4 = new Challenge("ИЗО", 5, "Сеня");
            hashTable.Add(challenge1);
            hashTable.Add(challenge2);
            hashTable.Add(challenge3);
            hashTable.Add(challenge4);

            var result = hashTable.Aggregate(0, (acc, x) => acc + x.TimeToPass);

            Assert.AreEqual(60, result);
        }

        [TestMethod]
        public void OrderByTest()
        {
            var hashTable = new MyHashTable<Challenge>();
            Challenge challenge1 = new Challenge("Математика", 25, "Ирина");
            Challenge challenge2 = new Challenge("История", 10, "Дима");
            Challenge challenge3 = new Challenge("Физра", 20, "Ваня");
            Challenge challenge4 = new Challenge("ИЗО", 5, "Сеня");
            hashTable.Add(challenge1);
            hashTable.Add(challenge2);
            hashTable.Add(challenge3);
            hashTable.Add(challenge4);

            var result = hashTable.OrderBy((x, y) => x.CompareTo(y));

            CollectionAssert.AreEqual(new List<Challenge> { challenge4, challenge2, challenge1, challenge3 }, result.ToArray());
        }
    }
}
