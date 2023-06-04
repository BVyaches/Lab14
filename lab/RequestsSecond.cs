using ChallengeLibrary;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyHashTable;

namespace lab
{
    public class RequestsSecond
    {
        /// <summary>
        /// Поиск сданных экзаменов
        /// </summary>
        /// <param name="gradebook">Зачетка, в которой их нужно найти</param>
        /// <param name="isLinq">Вызов для Linq запроса или нет</param>
        /// <returns>Все успешно сданные экзамены</returns>
        public static IEnumerable<Challenge> FindPassedExams(MyHashTable<Challenge> gradebook, bool isLinq)
        {

            IEnumerable<Challenge> result = gradebook.Where(challenge => (challenge is Exam && ((Exam)challenge).IsPassed));
            if (isLinq)
            {
                result = from challenge in gradebook where challenge is Exam && ((Exam)challenge).IsPassed select challenge;
            }
            return result;
        }

        /// <summary>
        /// Подсчет экзаменов
        /// </summary>
        /// <param name="gradebook">Зачетка для поиска</param>
        /// <param name="isLinq">Вызов для Linq запроса или нет</param>
        /// <returns>Количество экзаменов</returns>
        public static int CountExams(MyHashTable<Challenge> gradebook, bool isLinq)
        {
            int result = gradebook.Where(challenge => challenge is Exam).Count();
            if (isLinq)
            {
                result = (from challenge in gradebook where challenge is Exam select challenge).Count();
            } 
            return result;
        }

        /// <summary>
        /// Поиск общих предметв
        /// </summary>
        /// <param name="gradebook">Зачетка для поиска</param>
        /// <param name="isLinq">Вызов для Linq запроса или нет</param>
        /// <returns>Список общих предметов</returns>
        public static IEnumerable<string> FindCommonChallenges(MyHashTable<Challenge> zachetka1, MyHashTable<Challenge> zachetka2, bool isLinq)
        {
            IEnumerable<string> result = zachetka1.Select(pair => pair.Name).Intersect(zachetka2.Select(pair => pair.Name));
            if (isLinq)
            {
                result = (from pair in zachetka1 select pair.Name).Intersect((from pair in zachetka2 select pair.Name));
            }
            return result;
        }

        /// <summary>
        /// Поиск самого долгого испытания
        /// </summary>
        /// <param name="gradebook">Зачетка для поиска</param>
        /// <param name="isLinq">Вызов для Linq запроса или нет</param>
        /// <returns>Длину самого долгого испытания</returns>
        public static int FindMaxTimeToPass(MyHashTable<Challenge> gradebook, bool isLinq)
        {
            int result = gradebook.Aggregate(int.MinValue, (max, x) => Math.Max(max, x.TimeToPass));
            if (isLinq)
            {
                result = (from x in gradebook select x.TimeToPass).Max();
            }
            return result;
        }

        /// <summary>
        /// Группировка по предметам
        /// </summary>
        /// <param name="gradebook">Зачетка для поиска</param>
        /// <param name="isLinq">Вызов для Linq запроса или нет</param>
        /// <returns>Список сгруппированных испытаний по предметам</returns>
        public static IEnumerable<Challenge> GroupBySubject(MyHashTable<Challenge> gradebook, bool isLinq)
        {
            IEnumerable<Challenge> result = gradebook.OrderBy((challenge1, challenge2) => challenge1.Name.CompareTo(challenge2.Name));
            if (isLinq)
            {
                result = from challenge in gradebook orderby challenge.Name select challenge;
            }
            
            return result;
        }
    }
}
