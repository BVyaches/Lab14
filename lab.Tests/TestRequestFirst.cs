using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChallengeLibrary;
using lab;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
namespace TestCollections
{
    [TestClass]
    public class RequestsFirstTests
    {
        [TestMethod]
        public void FindPassedExamsLinqTest()
        {
            var zachetka = new Stack<Dictionary<Challenge, Challenge>>();

            var dict1 = new Dictionary<Challenge, Challenge>
            {
                { new Challenge("Математика", 60), new Exam("Математика", 60, "Ирина", true) },
                { new Challenge("История", 90), new Exam("История", 90, "Иван", false) },
                { new Challenge("Физра", 30), new Test("Физра", 30) }
            };
            var dict2 = new Dictionary<Challenge, Challenge>
            {
                { new Challenge("География", 120), new Exam("География", 120, "Игорь", true) },
                { new Challenge("Изо", 45), new Test("Изо", 45) }
            };
            zachetka.Push(dict1);
            zachetka.Push(dict2);

            var result = RequestsFirst.FindPassedExams(zachetka, true);

            Assert.AreEqual(2, result.Count());
            Assert.IsTrue(result.All(p => p is Exam && ((Exam)p).IsPassed));
        }

        [TestMethod]
        public void CountExamsLinqTest()
        {
            var zachetka = new Stack<Dictionary<Challenge, Challenge>>();
            var dict1 = new Dictionary<Challenge, Challenge>
            {
                { new Challenge("Математика", 60), new Exam("Математика", 60, "Ирина", true) },
                { new Challenge("История", 90), new Exam("История", 90, "Иван", false) },
                { new Challenge("Физра", 30), new Test("Физра", 30) }
            };
            var dict2 = new Dictionary<Challenge, Challenge>
            {
                { new Challenge("География", 120), new Exam("География", 120, "Игорь", true) },
                { new Challenge("Изо", 45), new Test("Изо", 45) }
            };
            zachetka.Push(dict1);
            zachetka.Push(dict2);

            var result = RequestsFirst.CountExams(zachetka, true);

            Assert.AreEqual(3, result);
        }

        [TestMethod]
        public void FindCommonChallengesLinqTest()
        {
            var zachetka = new Stack<Dictionary<Challenge, Challenge>>();
            var dict1 = new Dictionary<Challenge, Challenge>
            {
                { new Challenge("Математика", 60), new Exam("Математика", 60, "Ирина", true) },
                { new Challenge("История", 30), new Test("История", 30) },
                { new Challenge("Общее", 45), new Challenge("Общее", 45) }
            };
            var dict2 = new Dictionary<Challenge, Challenge>
            {
                { new Challenge("География", 90), new Exam("География", 90, "Иван", false) },
                { new Challenge("Изо", 45), new Test("Изо", 45) },
                { new Challenge("Общее", 45), new Challenge("Общее", 45) }
            };
            zachetka.Push(dict1);
            zachetka.Push(dict2);

            var result = RequestsFirst.FindCommonChallenges(zachetka, true);

            CollectionAssert.Contains(result.ToList(), "Общее");
            Assert.AreEqual(1, result.Count());
        }
        [TestMethod]
        public void FindMaxTimeToPassLinqTest()
        {
            var zachetka = new Stack<Dictionary<Challenge, Challenge>>();
            var dict1 = new Dictionary<Challenge, Challenge>
            {
                { new Challenge("Математика", 60), new Exam("Математика", 60, "Ирина", true) },
                { new Challenge("История", 90), new Exam("История", 90, "Иван", false) },
                { new Challenge("Физра", 30), new Test("Физра", 30) }
            };
            var dict2 = new Dictionary<Challenge, Challenge>
            {
                { new Challenge("География", 120), new Exam("География", 120, "Игорь", true) },
                { new Challenge("Изо", 45), new Test("Изо", 45) }
            };
            zachetka.Push(dict1);
            zachetka.Push(dict2);

            var result = RequestsFirst.FindMaxTimeToPass(zachetka, true);

            Assert.AreEqual("География", result.Name);
            Assert.AreEqual(120, result.TimeToPass);
        }

        [TestMethod]
        public void GroupBySubjectLinqTest()
        {
            var zachetka = new Stack<Dictionary<Challenge, Challenge>>();

            var dict1 = new Dictionary<Challenge, Challenge>
            {
                { new Challenge("Математика", 60), new Exam("Математика", 60, "Ирина", true) },
                { new Challenge("История", 90), new Exam("История", 90, "Иван", false) },
                { new Challenge("Физра", 30), new Test("Физра", 30) }
            };
            var dict2 = new Dictionary<Challenge, Challenge>
            {
                { new Challenge("История", 120), new Exam("История", 120, "Игорь", true) },
                { new Challenge("Математика", 45), new Test("Математика", 45) }
            };
            zachetka.Push(dict1);
            zachetka.Push(dict2);

            var result = RequestsFirst.GroupBySubject(zachetka, true);

            Assert.AreEqual(5, result.Count);
            Assert.AreEqual("История", result.ElementAt(0).Name);
            Assert.AreEqual("История", result.ElementAt(1).Name);
            Assert.AreEqual("Математика", result.ElementAt(2).Name);
            Assert.AreEqual("Математика", result.ElementAt(3).Name);
            Assert.AreEqual("Физра", result.ElementAt(4).Name);
        }

        [TestMethod]
        public void FindPassedExamsExtensionTest()
        {
            var zachetka = new Stack<Dictionary<Challenge, Challenge>>();

            var dict1 = new Dictionary<Challenge, Challenge>
            {
                { new Challenge("Математика", 60), new Exam("Математика", 60, "Ирина", true) },
                { new Challenge("История", 90), new Exam("История", 90, "Иван", false) },
                { new Challenge("Физра", 30), new Test("Физра", 30) }
            };
            var dict2 = new Dictionary<Challenge, Challenge>
            {
                { new Challenge("География", 120), new Exam("География", 120, "Игорь", true) },
                { new Challenge("Изо", 45), new Test("Изо", 45) }
            };
            zachetka.Push(dict1);
            zachetka.Push(dict2);

            var result = RequestsFirst.FindPassedExams(zachetka, false);

            Assert.AreEqual(2, result.Count());
            Assert.IsTrue(result.All(p => p is Exam && ((Exam)p).IsPassed));
        }

        [TestMethod]
        public void CountExamsExtensionTest()
        {
            var zachetka = new Stack<Dictionary<Challenge, Challenge>>();
            var dict1 = new Dictionary<Challenge, Challenge>
            {
                { new Challenge("Математика", 60), new Exam("Математика", 60, "Ирина", true) },
                { new Challenge("История", 90), new Exam("История", 90, "Иван", false) },
                { new Challenge("Физра", 30), new Test("Физра", 30) }
            };
            var dict2 = new Dictionary<Challenge, Challenge>
            {
                { new Challenge("География", 120), new Exam("География", 120, "Игорь", true) },
                { new Challenge("Изо", 45), new Test("Изо", 45) }
            };
            zachetka.Push(dict1);
            zachetka.Push(dict2);

            var result = RequestsFirst.CountExams(zachetka, false);

            Assert.AreEqual(3, result);
        }

        [TestMethod]
        public void FindCommonChallengesExtensionTest()
        {
            var zachetka = new Stack<Dictionary<Challenge, Challenge>>();
            var dict1 = new Dictionary<Challenge, Challenge>
            {
                { new Challenge("Математика", 60), new Exam("Математика", 60, "Ирина", true) },
                { new Challenge("История", 30), new Test("История", 30) },
                { new Challenge("Общее", 45), new Challenge("Общее", 45) }
            };
            var dict2 = new Dictionary<Challenge, Challenge>
            {
                { new Challenge("География", 90), new Exam("География", 90, "Иван", false) },
                { new Challenge("Изо", 45), new Test("Изо", 45) },
                { new Challenge("Общее", 45), new Challenge("Общее", 45) }
            };
            zachetka.Push(dict1);
            zachetka.Push(dict2);

            var result = RequestsFirst.FindCommonChallenges(zachetka, false);

            CollectionAssert.Contains(result.ToList(), "Общее");
            Assert.AreEqual(1, result.Count());
        }
        [TestMethod]
        public void FindMaxTimeToPassExtensionTest()
        {
            var zachetka = new Stack<Dictionary<Challenge, Challenge>>();
            var dict1 = new Dictionary<Challenge, Challenge>
            {
                { new Challenge("Математика", 60), new Exam("Математика", 60, "Ирина", true) },
                { new Challenge("История", 90), new Exam("История", 90, "Иван", false) },
                { new Challenge("Физра", 30), new Test("Физра", 30) }
            };
            var dict2 = new Dictionary<Challenge, Challenge>
            {
                { new Challenge("География", 120), new Exam("География", 120, "Игорь", true) },
                { new Challenge("Изо", 45), new Test("Изо", 45) }
            };
            zachetka.Push(dict1);
            zachetka.Push(dict2);

            var result = RequestsFirst.FindMaxTimeToPass(zachetka, false);

            Assert.AreEqual("География", result.Name);
            Assert.AreEqual(120, result.TimeToPass);
        }

        [TestMethod]
        public void GroupBySubjectExtensionTest()
        {
            var zachetka = new Stack<Dictionary<Challenge, Challenge>>();

            var dict1 = new Dictionary<Challenge, Challenge>
            {
                { new Challenge("Математика", 60), new Exam("Математика", 60, "Ирина", true) },
                { new Challenge("История", 90), new Exam("История", 90, "Иван", false) },
                { new Challenge("Физра", 30), new Test("Физра", 30) }
            };
            var dict2 = new Dictionary<Challenge, Challenge>
            {
                { new Challenge("История", 120), new Exam("История", 120, "Игорь", true) },
                { new Challenge("Математика", 45), new Test("Математика", 45) }
            };
            zachetka.Push(dict1);
            zachetka.Push(dict2);

            var result = RequestsFirst.GroupBySubject(zachetka, false);

            Assert.AreEqual(5, result.Count());
            Assert.AreEqual("История", result.ElementAt(0).Name);
            Assert.AreEqual("История", result.ElementAt(1).Name);
            Assert.AreEqual("Математика", result.ElementAt(2).Name);
            Assert.AreEqual("Математика", result.ElementAt(3).Name);
            Assert.AreEqual("Физра", result.ElementAt(4).Name);
        }

    }
}
