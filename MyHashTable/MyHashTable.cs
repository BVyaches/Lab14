using ChallengeLibrary;
using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;

namespace MyHashTable
{
    public class MyHashTable<T>: IEnumerable<T>, ICollection<T> where T : class, IComparable<T>, IComparer<T>, IInit, ICloneable, new()
    {
        private HashPoint<T>[] table;
        private int capacity;
        private int emptyPlaces;


        public int Count 
        {
            get
            {
                return capacity-emptyPlaces;
            }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public T this[T key]
        {
            get
            {
                if (key is null)
                {
                    throw new ArgumentNullException(nameof(key), "Ключ не задан");
                }

                if (FindPoint(key, out T foundElement, out int foundIndex))
                {
                    return foundElement;
                }

                throw new KeyNotFoundException("Данный ключ отсутствует в таблице");
            }
            set
            {
                if (key is null)
                {
                    throw new ArgumentNullException(nameof(key), "Ключ не задан");
                }

                if (value is null)
                {
                    throw new ArgumentNullException(nameof(value), "Элемент не задан");
                }

                if (FindPoint(key, out T foundElement, out int foundIndex))
                {
                    ChangeElementByKey(value, foundIndex);
                }
                else
                {
                    throw new KeyNotFoundException("Данный ключ отсутствует в таблице");
                }
            }
        }

        /// <summary>
        /// Конструктор по заданному размеру
        /// </summary>
        /// <param name="size">Требуемый размер</param>
        public MyHashTable(int size = 0)
        {
            capacity = size;
            table = new HashPoint<T>[capacity];
            emptyPlaces = capacity;
        }

        /// <summary>
        /// Конструктор по уже существующей таблице
        /// </summary>
        /// <param name="hashtable">Исходная таблица</param>
        public MyHashTable(MyHashTable<T> hashtable)
        {
            capacity = hashtable.capacity;
            emptyPlaces = capacity;
            table = new HashPoint<T>[capacity];
            // Перебираем все элементы и добавляем их в новую таблицу, выполняя клонирование
            foreach (T item in hashtable)
            {
                if (item != null)
                    Add(item.Clone() as T);
            }
        }

        [ExcludeFromCodeCoverage]
        virtual public bool RandomInit(int size)
        {
            if (size <= 0)
            {
                return false;
            }
            capacity = size;
            table = new HashPoint<T>[size];
            emptyPlaces = capacity;
            for (int i = 0; i < capacity; i++)
            {
                T randomChallenge = ConsoleInteraction.RandomChallenge() as T;
                if (randomChallenge is null)
                {
                    Console.WriteLine(randomChallenge);
                }
                else
                {
                    Add(randomChallenge);
                }
                
            }
            return true;
        }

        /// <summary>
        /// Добавление элемента в таблицу
        /// </summary>
        /// <param name="data">Данные нового элемента</param>
        virtual public void Add(T data)
        {
            Add(data, false);
        }

        virtual public void Add(T data, bool isForChange)
        {
            if (data is null)
            {
                return;
            }
            object key = new();
            data.ToBase(ref key);
            if (FindPoint(key as T, out _, out _))
            {
                throw new ArgumentException("Данный элемент уже есть в таблице");
            }
            // Если таблица пустая, задаем ей минимальный размер для одного элемента
            if (capacity == 0)
            {
                capacity = 1;
                table= new HashPoint<T>[capacity];
                emptyPlaces = 1;
            }

            HashPoint<T> point = new(data);

            // Если не осталось свободных мест, удваиваем размер таблицы и заново заполняем её
            if (emptyPlaces == 0)
            {
                HashPoint<T>[] newTable = new HashPoint<T>[capacity*2];
                capacity *= 2;
                emptyPlaces = capacity;
                foreach (HashPoint<T> p in table)
                {
                    int currentIndex = Math.Abs(p.GetHashCode()) % capacity;
                    AddHelper(ref newTable, p);
                }
                table = newTable;
            }

            AddHelper(ref table, point);
            return;
        }

        protected void ChangeElementByKey(T newData, int currentIndex)
        {

            table[currentIndex] = null;
            emptyPlaces++;
            Add(newData, true);
            
            
        }

        /// <summary>
        /// Вспомогательная функция добавления, чтобы не повторять одно и то же
        /// </summary>
        /// <param name="table">Таблица, в которую требуется добавить</param>
        /// <param name="point">Элемент для добавления</param>
        private void AddHelper(ref HashPoint<T>[] table, HashPoint<T> point)
        {
            int index = Math.Abs(point.GetHashCode()) % capacity;
            // Проверка, свободно ли место
            if (table[index] is null)
            {
                table[index] = point;
                emptyPlaces--;
                return;
            }
            // Если есть свободные места, но место по индексу занято, пишем в первое свободное место
            for (int i = 0; i<capacity; i++)
            {
                if (table[i] is null)
                {
                    table[i] = point;
                    emptyPlaces--;
                    return;
                }
            }
        }

        [ExcludeFromCodeCoverage]
        public void Show()
        {
            Console.WriteLine(ToString());
        }

        /// <summary>
        /// Поиск элемента по ключу и его вывод
        /// </summary>
        /// <param name="keyToFind">Ключ для поиска</param>
        /// <param name="foundElement">Найденный элемент</param>
        /// <param name="foundIndex">Индекс найденного элемента</param>
        /// <returns>Есть ли элемент в таблице</returns>
        public bool FindPoint(T keyToFind, out T foundElement, out int foundIndex)
        {

            foundElement = null;
            // Если таблица пустая или ключ не существует, то сразу прекращаем поиск
            if (IsEmpty() || keyToFind is null)
            {
                foundIndex = -1;
                return false;
            }

            HashPoint<T> point = new(keyToFind);
            int index = Math.Abs(point.GetHashCode()) % capacity;
            // Если элемент лежит по вычесленному индексу, возвращаем его
            if (table[index] is not null && table[index].Equals(point))
            {
                foundElement = table[index].data;
                foundIndex = index;
                return true;
            }

            // Если же по ожидаемому индексу находится иной элемент, перебираем все элементы
            for (int i = 0; i<capacity; i++)
            {
                HashPoint<T>? currentPoint = table[i];
                object key = new();
                keyToFind.ToBase(ref key);
                if (currentPoint is not null && currentPoint.key.Equals(key))
                {
                    foundElement = currentPoint.data;
                    foundIndex = i;
                    return true;
                }
            }
            // Если элемент не найден, сообщаем об этом
            foundIndex = -1;
            return false;
        }

        /// <summary>
        /// Проверка, пустая ли хэш-таблица
        /// </summary>
        /// <returns>Является ли таблица пустой</returns>
        public bool IsEmpty()
        {
            return capacity == emptyPlaces;
        }

        /// <summary>
        /// Перевод таблицы в строку    
        /// </summary>
        /// <returns>Таблица в виде строки</returns>
        public override string ToString()
        {
            string result = "";
            if (capacity== 0 || capacity == emptyPlaces) 
            {
                return "Похоже, таблица пустая";
            }
            for (int i = 0; i < capacity; i++)
            {
                if (table[i] is null)
                {
                    result += "\n" + i + " : " + "\n";
                }
                else
                {
                    result += "\n" + i + " : ";
                    HashPoint<T> point = table[i];
                    result += point.ToString() + "\n";
                    
                }
            }
            return result;
        }

        
        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < capacity; i++)
            {
                if (table[i] != null)
                {
                    yield return table[i].data;
                }
            }
        }

        // Не протестировать данный метод, тк он не вызывается из-за обощенного класса
        [ExcludeFromCodeCoverage]
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Очистка таблицы
        /// </summary>
        virtual public void Clear()
        {
            emptyPlaces = capacity;
            table = new HashPoint<T>[capacity];

        }

        /// <summary>
        /// Проверка содержания элемента в таблице
        /// </summary>
        /// <param name="item">Искомый элемент</param>
        /// <returns>Есть элемент в таблице или нет</returns>
        public bool Contains(T item)
        {
            return FindPoint(item, out _, out _);
        }

        /// <summary>
        /// Копирование таблицы в массив
        /// </summary>
        /// <param name="array">Массив для копирования</param>
        /// <param name="arrayIndex">Элемент, с которого начинать копировать</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public void CopyTo(T[] array, int arrayIndex)
        {
            // Если массив пустой, выдаём ошибку
            if (array == null)
            {
                throw new ArgumentNullException(nameof(array), "Заданный массив пустой");
            }

            // Если индекс меньше 0 или индекс больше длины массива, выдаём ошибку
            if (arrayIndex < 0 || arrayIndex >= array.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(arrayIndex), "Начальный индекс за пределами массива");
            }

            // Если в массиве недостаточно места, выдаём ошибку
            if (array.Length - arrayIndex < Count)
            {
                throw new ArgumentException("Места в массиве недостаточно для записи указанного количества элементов");
            }

            int index = arrayIndex;
            foreach (T item in this)
            {
                array[index] = item;
                index++;
            }
        }

        public virtual bool Remove(T item)
        {
            bool result = FindPoint(item, out _, out int index);
            if (result)
            {
                table[index] = null;
                emptyPlaces += 1;
                return true;
            }
            return false;
        }

        public MyHashTable<T> Clone()
        {
            MyHashTable<T> newTable = new MyHashTable<T>(capacity);
            foreach (HashPoint<T> item in table)
            {
                if (item != null)
                    newTable.Add(item.data.Clone() as T);
            }
            return newTable;
        }

        public MyHashTable<T> ShallowCopy()
        {
            MyHashTable<T> newTable = new MyHashTable<T>(capacity);
            foreach (HashPoint<T> item in table)
            {
                if (item != null)
                    newTable.Add(item.data);
            }

            return newTable;
        }

        public void TestCopyClone()
        {
            Challenge element = table[0].data as Challenge;
            element.teacher.Name = "Тестер";
            Console.WriteLine("Измененный элемент:");
            Console.WriteLine(table[0]);
        }
    } 
}