using ChallengeLibrary;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace lab
{
    public class RequestsFirst
    {
        /// <summary>
        /// Поиск сданных экзаменов
        /// </summary>
        /// <param name="gradebook">Зачетка, в которой их нужно найти</param>
        /// <param name="isLinq">Вызов для Linq запроса или нет</param>
        /// <returns>Все успешно сданные экзамены</returns>
        public static IEnumerable<Challenge> FindPassedExams(Stack<Dictionary<Challenge, Challenge>> gradebook, bool isLinq)
        {
            IEnumerable<Challenge> result = gradebook.SelectMany(dict => dict.Values).
                Where(p => p is Exam && ((Exam)p).IsPassed);

            if (isLinq)
            {
                result = from dict in gradebook 
                         from p in dict.Values 
                         where p is Exam && ((Exam)p).IsPassed select p;
            }
            return result;
        }

        /// <summary>
        /// Подсчет экзаменов
        /// </summary>
        /// <param name="gradebook">Зачетка для поиска</param>
        /// <param name="isLinq">Вызов для Linq запроса или нет</param>
        /// <returns>Количество экзаменов</returns>
        public static int CountExams(Stack<Dictionary<Challenge, Challenge>> gradebook, bool isLinq)
        {
            int result = gradebook.SelectMany(dict => dict.Values).Where(p => p is Exam).Count();
            if (isLinq)
            {
                result = (from dict in gradebook 
                          from p in dict.Values 
                          where p is Exam select p).Count();
            }
            return result;
        }


        /// <summary>
        /// Поиск общих предметв
        /// </summary>
        /// <param name="gradebook">Зачетка для поиска</param>
        /// <param name="isLinq">Вызов для Linq запроса или нет</param>
        /// <returns>Список общих предметов</returns>
        public static IEnumerable<string> FindCommonChallenges(Stack<Dictionary<Challenge, Challenge>> gradebook, bool isLinq)
        {
            var gradebookClone = new Stack<Dictionary<Challenge, Challenge>>(gradebook.Reverse());
            if (!isLinq)
            {
                IEnumerable<string> resultEx = (gradebookClone.Pop().Select(x => x.Value.Name)).
                Intersect(gradebookClone.Pop().Select(x => x.Value.Name));
                return resultEx;
            }
            
            IEnumerable<string> result = (from pair in gradebookClone.Pop() 
                          select pair.Value.Name).
                    Intersect((from pair in gradebookClone.Pop() 
                               select pair.Value.Name));
            
            return result;
        }


        /// <summary>
        /// Поиск самого долгого испытания
        /// </summary>
        /// <param name="gradebook">Зачетка для поиска</param>
        /// <param name="isLinq">Вызов для Linq запроса или нет</param>
        /// <returns>Самое долгое испытание</returns>
        public static Challenge FindMaxTimeToPass(Stack<Dictionary<Challenge, Challenge>> gradebook, bool isLinq)
        {
            Challenge result = gradebook.SelectMany(dict => dict.Values).
                OrderBy(challenge => challenge.TimeToPass).Last();
            if (isLinq)
            {
                result = (from dict in gradebook 
                          from challenge in dict.Values 
                          orderby challenge.TimeToPass
                          select challenge).Last();
            }
            
            return result;
        }


        /// <summary>
        /// Группировка по предметам
        /// </summary>
        /// <param name="gradebook">Зачетка для поиска</param>
        /// <param name="isLinq">Вызов для Linq запроса или нет</param>
        /// <returns>Список сгруппированных испытаний по предметам</returns>
        public static List<Challenge> GroupBySubject(Stack<Dictionary<Challenge, Challenge>> gradebook, bool isLinq)
        {
            List<Challenge> result = (gradebook.SelectMany(dict => dict.Values).
                OrderBy(challenge => challenge.Name)).ToList<Challenge>();
            if (isLinq)
            {
                result = ((from dict in gradebook
                         from challenge in dict.Values
                         orderby challenge.Name
                         select challenge)).ToList<Challenge>();
            }
            return result;
        }
    }
}
