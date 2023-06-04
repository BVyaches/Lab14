using ChallengeLibrary;
using lab;
namespace lab.Tests
{
    [TestClass]
    public class TestChallenge
    {
        [TestMethod]
        public void ChallengeConstructorNameTest()
        {
            string name = "����������";
            int timeToPass = 10;
            string teacherName = "�����";

            Challenge challenge = new Challenge(name, timeToPass, teacherName);

            Assert.AreEqual(challenge.Name, name);
        }

		[TestMethod]
		public void ChallengeConstructorTimeToPassTest()
		{
			string name = "����������";
			int timeToPass = 10;
			string teacherName = "�����";

			Challenge challenge = new Challenge(name, timeToPass, teacherName);

			Assert.AreEqual(challenge.TimeToPass, timeToPass);
		}

		[TestMethod]
		public void ChallengeConstructorTeacherNameTest()
		{
			string name = "����������";
			int timeToPass = 10;
			string teacherName = "�����";

			Challenge challenge = new Challenge(name, timeToPass, teacherName);

			Assert.AreEqual(challenge.teacher.Name, teacherName);
		}

		[TestMethod]
		public void ChallengeNoNameConstructorNameTest()
		{
			string name = "NoName";

			Challenge challenge = new Challenge();

			Assert.AreEqual(challenge.Name, name);
		}

		[TestMethod]
		public void ChallengeNoNameConstructorTimeToPassTest()
		{
			int timeToPass = 0;

			Challenge challenge = new Challenge();

			Assert.AreEqual(challenge.TimeToPass, timeToPass);
		}

		[TestMethod]
		public void ChallengeNoNameConstructorTeacherNameTest()
		{
			string teacherName = "NoName";

			Challenge challenge = new Challenge();

			Assert.AreEqual(challenge.teacher.Name, teacherName);
		}

		[TestMethod]
        public void ChallengeNamePropertyTest()
        {
            Challenge challenge = new Challenge();
            string name = "����������";

            challenge.Name = name;

            Assert.AreEqual(challenge.Name, name);
        }

        [TestMethod]
        public void ChallengeEmptyNamePropertyTest()
        {
            Challenge challenge = new Challenge();
            string name = "";

            challenge.Name = name;

            Assert.AreEqual(challenge.Name, "NoName");
        }

        [TestMethod]
        public void ChallengeIsEasyPropertyTest()
        {
            Challenge challenge = new Challenge();
            string teacherName = "�����";

            challenge.teacher.Name = teacherName;

            Assert.AreEqual(challenge.teacher.Name, teacherName);
        }

        [TestMethod]
        public void ChallengeGetKindTest()
        {
            string expectedKind = "���������";
            Challenge challenge = new Challenge();

            Assert.AreEqual(expectedKind, challenge.GetKind());
        }

		[TestMethod]
		public void ChallengeCompareToDifferentObjTest()
		{
			Challenge challenge1 = new Challenge("����������", 25, "�����");
			Person person = new Person("����");
			int expectedCompareResult = 1;

			Assert.AreEqual(expectedCompareResult, challenge1.CompareTo(person));
		}

		[TestMethod]
        public void ChallengeCompareToDifferentNamesTest()
        {
            Challenge challenge1 = new Challenge("����������", 25, "�����");
            Challenge challenge2 = new Challenge("�������", 10, "����");
            int expectedCompareResult = -1;

            Assert.AreEqual(expectedCompareResult, challenge2.CompareTo(challenge1));
        }

		[TestMethod]
		public void ChallengeCompareToEqualNamesDifferentTimesTest()
		{
			Challenge challenge1 = new Challenge("����������", 25, "�����");
			Challenge challenge2 = new Challenge("����������", 10, "����");

			Assert.IsTrue(challenge1.CompareTo(challenge2) > 0);
		}

		[TestMethod]
		public void ChallengeCompareToEqualNamesEqualTimesTest()
		{
			Challenge challenge1 = new Challenge("����������", 10, "�����");
			Challenge challenge2 = new Challenge("����������", 10, "����");

			int expectedCompareResult = -1;

			Assert.AreEqual(expectedCompareResult, challenge1.CompareTo(challenge2));
		}

        [TestMethod]
        public void ChallengeCompareTest()
        {
            Challenge challenge1 = new Challenge("����������", 25, "�����");
            Challenge challenge2 = new Challenge("�������", 10, "����");
            int expectedCompareResult = -1;

            Assert.AreEqual(expectedCompareResult, challenge2.Compare(challenge2, challenge1));
        }


		[TestMethod]
        public void TestShallowCopy()
        {
            Challenge challenge1 = new Challenge("����������", 60);
            Challenge challenge2 = (Challenge)challenge1.ShallowCopy();

            Assert.AreEqual(challenge1, challenge2);
        }

        [TestMethod]
        public void ChallengeCloneTest()
        {
            Challenge challenge1 = new Challenge("����������", 60, "������");
            Challenge challenge2 = (Challenge)challenge1.Clone();

            Assert.AreEqual(challenge1, challenge2);
        }



        [TestMethod]
        public void ChallengeEqualsMethodTest()
        {
            Challenge challenge1 = new Challenge("����������", 25, "�����");
            Challenge challenge2 = new Challenge("����������", 25, "�����");

            Assert.IsTrue(challenge1.Equals(challenge2));
        }

        [TestMethod]
        public void ChallengeNotEqualsMethodTest()
        {
            Challenge challenge1 = new Challenge("����������", 25, "�����");
            Challenge challenge2 = new Challenge("�������", 10, "����");

            Assert.IsFalse(challenge1.Equals(challenge2));
        }

        [TestMethod]
        public void ChallengeToStringTest()
        {
            Challenge challenge = new Challenge("����������", 25, "�����");

            string expectedString = "���������: ���������� ����� �� �����������: 25 �������: �����";

            Assert.AreEqual(expectedString, challenge.ToString());
        }

        [TestMethod]
        public void ChallengeGetHashCodeGivesSameForSameObjectsCodeTest()
        {
            Challenge challenge1 = new Challenge("����������", 25, "�����");
            Challenge challenge2 = new Challenge("����������", 25, "�����");

            Assert.AreEqual(challenge1.GetHashCode(), challenge2.GetHashCode());
        }


        [TestMethod]
        public void ChallengeGetHashCodeGivesNotSameForNotSameObjectsCodeTest()
        {
            Challenge challenge1 = new Challenge("����������", 25, "�����");
            Challenge challenge2 = new Challenge("������", 10, "�������");

            Assert.AreNotEqual(challenge1.GetHashCode(), challenge2.GetHashCode());
        }

        [TestMethod]
        public void ChallengeToBaseTest()
        {
            Challenge challenge = new Challenge("����������", 25, "�����");
            object obj = new();

            challenge.ToBase(ref obj);

            Assert.AreEqual(challenge, obj);
        }
    }
}