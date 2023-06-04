using ChallengeLibrary;
using MyHashTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab.Tests
{
    [TestClass]
    public class RequestsSecondTests
    {
        [TestMethod]
        public void FindPassedExamsLinqTest()
        {
            var zachetka = new MyHashTable<Challenge>
            {
                new Exam("Математика", 25, "Гриша", false),
                new Exam("История", 10, "Ирина", true),
                new Test("Физра", 20)
            };

            var result = RequestsSecond.FindPassedExams(zachetka, true);

            Assert.AreEqual(1, result.Count());
            Assert.IsTrue(result.All(challenge => challenge is Exam && ((Exam)challenge).IsPassed));
        }

        [TestMethod]
        public void CountExamsLinqTest()
        {
            var zachetka = new MyHashTable<Challenge>
            {
                new Exam("Математика", 25),
                new Exam("История", 10),
                new Test("Физра", 20)
            };

            var result = RequestsSecond.CountExams(zachetka, true);

            Assert.AreEqual(2, result);
        }

        [TestMethod]
        public void FindCommonChallengesLinqTest()
        {
            var zachetka1 = new MyHashTable<Challenge>
            {
                new Challenge("Математика", 25),
                new Challenge("История", 10),
                new Challenge("Общее", 20)
            };

            var zachetka2 = new MyHashTable<Challenge>
            {
                new Challenge("География", 120),
                new Challenge("Изо", 45),
                new Challenge("Общее", 20)
            };

            var result = RequestsSecond.FindCommonChallenges(zachetka1, zachetka2, true);

            Assert.AreEqual(1, result.Count());
            Assert.AreEqual("Общее", result.First());
        }

        [TestMethod]
        public void FindMaxTimeToPassLinqTest()
        {
            var zachetka = new MyHashTable<Challenge>
            {
                new Exam("Математика", 25),
                new Exam("История", 10),
                new Exam("Физра", 20)
            };

            var result = RequestsSecond.FindMaxTimeToPass(zachetka, true);

            Assert.AreEqual(25, result);
        }

        [TestMethod]
        public void GroupBySubjectLinqTest()
        {
            var zachetka = new MyHashTable<Challenge>
            {
                new Challenge("Математика", 25),
                new Challenge("История", 10),
                new Challenge("Физра", 20)
            };

            var result = RequestsSecond.GroupBySubject(zachetka, true);

            CollectionAssert.AreEqual(new List<Challenge>
            {
                new Challenge("История", 10),
                new Challenge("Математика", 25),
                new Challenge("Физра", 20)
            }, 
            result.ToList());
        }

        [TestMethod]
        public void FindPassedExamsExtensionTest()
        {
            var zachetka = new MyHashTable<Challenge>
            {
                new Exam("Математика", 25, "Гриша", false),
                new Exam("История", 10, "Ирина", true),
                new Test("Физра", 20)
            };

            var result = RequestsSecond.FindPassedExams(zachetka, false);

            Assert.AreEqual(1, result.Count());
            Assert.IsTrue(result.All(challenge => challenge is Exam && ((Exam)challenge).IsPassed));
        }

        [TestMethod]
        public void CountExamsExtensionTest()
        {
            var zachetka = new MyHashTable<Challenge>
            {
                new Exam("Математика", 25),
                new Exam("История", 10),
                new Test("Физра", 20)
            };

            var result = RequestsSecond.CountExams(zachetka, false);

            Assert.AreEqual(2, result);
        }

        [TestMethod]
        public void FindCommonChallengesExtensionTest()
        {
            var zachetka1 = new MyHashTable<Challenge>
            {
                new Challenge("Математика", 25),
                new Challenge("История", 10),
                new Challenge("Общее", 20)
            };

            var zachetka2 = new MyHashTable<Challenge>
            {
                new Challenge("География", 120),
                new Challenge("Изо", 45),
                new Challenge("Общее", 20)
            };

            var result = RequestsSecond.FindCommonChallenges(zachetka1, zachetka2, false);

            Assert.AreEqual(1, result.Count());
            Assert.AreEqual("Общее", result.First());
        }

        [TestMethod]
        public void FindMaxTimeToPassExtensionTest()
        {
            var zachetka = new MyHashTable<Challenge>
            {
                new Exam("Математика", 25),
                new Exam("История", 10),
                new Exam("Физра", 20)
            };

            var result = RequestsSecond.FindMaxTimeToPass(zachetka, false);

            Assert.AreEqual(25, result);
        }

        [TestMethod]
        public void GroupBySubjectExtensionTest()
        {
            var zachetka = new MyHashTable<Challenge>
            {
                new Challenge("Математика", 25),
                new Challenge("История", 10),
                new Challenge("Физра", 20)
            };

            var result = RequestsSecond.GroupBySubject(zachetka, false);

            CollectionAssert.AreEqual(new List<Challenge>
            {
                new Challenge("История", 10),
                new Challenge("Математика", 25),
                new Challenge("Физра", 20)
            },
            result.ToList());
        }
    }
}
