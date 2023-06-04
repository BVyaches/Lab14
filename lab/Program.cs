using ChallengeLibrary;
using MyHashTable;
using System.Collections;
using CI = ChallengeLibrary.ConsoleInteraction;
using RF = lab.RequestsFirst;
using RS = lab.RequestsSecond;
using System.Linq;
using System.Diagnostics.CodeAnalysis;

namespace lab
{
    public static class IsNullOrEmptyExtension
    {
        public static bool IsNullOrEmpty(this IEnumerable source)
        {
            if (source != null)
            {
                foreach (object obj in source)
                {
                    return false;
                }
            }
            return true;
        }
    }

    [ExcludeFromCodeCoverage]
    internal class Program
    {
        static void RandomZachetka(ref Stack<Dictionary<Challenge, Challenge>> zachetka)
        {
            zachetka = new();
            Random rand = new();
            Dictionary<Challenge, Challenge> firstTerm = new();
            Dictionary<Challenge, Challenge> secondTerm = new();

            int length = rand.Next(1, 5);
            for (int i = 0; i < length; i++)
            {
                Challenge challenge = CI.RandomChallenge();
                object baseChallenge = new();
                challenge.ToBase(ref baseChallenge);
                firstTerm.Add(baseChallenge as Challenge, challenge);
            }

            length = rand.Next(1, 5);
            for (int i = 0; i < length; i++)
            {
                Challenge challenge = CI.RandomChallenge();
                object baseChallenge = new();
                challenge.ToBase(ref baseChallenge);
                secondTerm.Add(baseChallenge as Challenge, challenge);
            }
            zachetka.Push(firstTerm);
            zachetka.Push(secondTerm);
        }

        static void ShowZachetka(Stack<Dictionary<Challenge, Challenge>> zachetka)
        {
            int i = 1;
            foreach (var term in zachetka)
            {
                Console.WriteLine($"{i} семестр:");
                foreach (var dictpair in term)
                {
                    Console.WriteLine(dictpair.Value);
                }
                i++;
            }
        }

        

        static void Main(string[] args)
        {
            int[] parameters;
            int currentMenu = 0, currentFunc = 0;

            Stack<Dictionary<Challenge, Challenge>> zachetka = new();
            MyHashTable<Challenge> table = new();
            MyHashTable<Challenge> second = new();


            RandomZachetka(ref zachetka);
            table.RandomInit(10);
            second.RandomInit(5 );



            while (currentMenu!=10)
            {
                parameters = CI.ChooseMenu(currentMenu);
                currentMenu = parameters[0];
                currentFunc = parameters[1];

                switch (currentMenu)
                {
                    case 1:
                        {
                            Console.WriteLine("Вы работаете с зачёткой:");
                            ShowZachetka(zachetka);
                            Console.WriteLine();

                            switch (currentFunc)
                            {
                                    case 0:
                                    {
                                        var result = RF.FindPassedExams(zachetka, true);

                                        if (result.IsNullOrEmpty())
                                        {
                                            Console.WriteLine("Похоже, сданных экзаменов нет");
                                            break;
                                        }

                                        Console.WriteLine("Все успешно сданные экзамены:");
                                        foreach (var exam in result)
                                        {
                                            Console.WriteLine(exam);
                                        }
                                        break;
                                    }
                                    
                                    case 1:
                                    {
                                        int result = RF.CountExams(zachetka, true);
                                        if (result == 0)
                                        {
                                            Console.WriteLine("Похоже, экзаменов нет");
                                            break;
                                        }

                                        Console.WriteLine($"Количество экзаменов за оба семестра:\n{result}");
                                        break;
                                    }
                                    case 2:
                                    {
                                        var result = RF.FindCommonChallenges(zachetka, true);
                                        if (IsNullOrEmptyExtension.IsNullOrEmpty(result))
                                        {
                                            Console.WriteLine("Похоже, общих предметов у семестров нет");
                                            break;
                                        }

                                        Console.WriteLine("Общие предметы в обоих семестрах:");
                                        foreach (var exam in result)
                                        {
                                            Console.WriteLine(exam);
                                        }
                                        break;
                                    }
                                    case 3:
                                    {
                                        var result = RF.FindMaxTimeToPass(zachetka, false);
                                        Console.WriteLine($"Самое долгое испытание:\n{result}");
                                        break;
                                    }
                                    case 4:
                                    {
                                        var result = RF.GroupBySubject(zachetka, false);
                                        Console.WriteLine("Испытания, сгруппированные по предметам:");
                                        Challenge previous = new();
                                        foreach (Challenge item in result) 
                                        { 
                                            if (item.Name != previous.Name)
                                            {
                                                Console.WriteLine();
                                            }
                                            Console.WriteLine(item);
                                            previous = item;
                                        }
                                        break;
                                    }
                            }
                            break;
                        }
                    case 2:
                        {
                            Console.WriteLine($"Вы работаете с зачёткой из таблицы:\n{table}\n");

                            switch (currentFunc)
                            {
                                case 0:
                                    {
                                        var result = RS.FindPassedExams(table, false);

                                        if (IsNullOrEmptyExtension.IsNullOrEmpty(result))
                                        {
                                            Console.WriteLine("Похоже, сданных экзаменов нет");
                                            break;
                                        }

                                        Console.WriteLine("Все успешно сданные экзамены:");
                                        foreach (var exam in result)
                                        {
                                            Console.WriteLine(exam);
                                        }
                                        break;
                                    }

                                case 1:
                                    {
                                        int result = RS.CountExams(table, false);
                                        if (result == 0)
                                        {
                                            Console.WriteLine("Похоже, экзаменов нет");
                                            break;
                                        }

                                        Console.WriteLine($"Количество экзаменов в зачетке:\n{result}");
                                        break;
                                    }
                                case 2:
                                    {
                                        Console.WriteLine("Вторая зачетка:");
                                        second.Show();
                                        Console.WriteLine();

                                        var result = RS.FindCommonChallenges(table, second, false);
                                        if (IsNullOrEmptyExtension.IsNullOrEmpty(result))
                                        {
                                            Console.WriteLine("Похоже, общих предметов у зачеток нет");
                                            break;
                                        }

                                        Console.WriteLine("Общие предметы в зачетках:");
                                        foreach (var exam in result)
                                        {
                                            Console.WriteLine(exam);
                                        }
                                        break;
                                    }
                                case 3:
                                    {
                                        var result = RS.FindMaxTimeToPass(table, true);
                                        Console.WriteLine($"Самое долгое испытание:\n{result}");
                                        break;
                                    }
                                case 4:
                                    {
                                        var result = RS.GroupBySubject(table, true);
                                        Console.WriteLine("Испытания, сгруппированные по предметам:");
                                        Challenge previous = new();
                                        foreach (Challenge item in result)
                                        {
                                            if (item.Name != previous.Name)
                                            {
                                                Console.WriteLine();
                                            }
                                            Console.WriteLine(item);
                                            previous = item;
                                        }
                                        break;
                                    }
                            }
                            break;
                        }

                }
                Console.WriteLine("\n\nНажмите любую клавишу для возвращения в меню");
                Console.ReadLine();
            }

            
        }
    }
}