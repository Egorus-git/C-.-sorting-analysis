using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsSortMyList
{
    public class Node<T> where T : IComparable<T> // это объек, который содержит в себе информацию о элементе
    {
        private T element; // ссылка на значение элемента списка
        private Node<T> next; // ссылка на значение следующего элемента списка 
        private Node<T> previous;


        public T Element // это поле, которое предоставляет значение элемента по ссылке
        {
            get { return this.element; }
            set { this.element = value; }
        }

        public Node<T> Next //это поле, которое возвращает ссылку на следующий элемент
        {
            get { return this.next; }
        }
        public void SetNextNode(Node<T> _nextNode) //функция, которая устанавливает связь со следующим элементом
        { this.next = _nextNode; }
        
        public Node<T> Previous 
        {
            get { return this.previous; }
        }
        public void SetPreviousNode(Node<T> _previousNode) 
        {
            this.previous = _previousNode;
        }
        public static bool operator >(Node<T> n1, Node<T> n2)
        {
            if (n1.element.CompareTo(n2.element) > 0)
                return true;
            else
                return false;
        }
        public static bool operator <(Node<T> n1, Node<T> n2)
        {
            if (n1.element.CompareTo(n2.element) < 0)
                return true;
            else
                return false;
        }
        public static bool operator <=(Node<T> n1, Node<T> n2)
        {
            int compare = n1.element.CompareTo(n2.element);
            if (compare <= 0)
                return true;
            else
                return false;
        }
        public static bool operator >=(Node<T> n1, Node<T> n2)
        {
            int compare = n1.element.CompareTo(n2.element);
            if (compare >= 0)
                return true;
            else
                return false;
        }
    }
    class Utilities<T> where T : IComparable<T>
    {
        public static vector<int> RandomInt(int size)
        {
            var vec = new vector<int>();
            var rnd = new Random();
            for (int i = 0; i < size; i++)
            {
                vec.Push(rnd.Next(-5000, 5000));
            }
            return vec;
        }

        public static vector<double> RandomDouble(int size)
        {
            var vec = new vector<double>();
            var rnd = new Random();
            for (int i = 0; i < size; i++)
            {
                vec.Push(rnd.NextDouble() * rnd.Next(-5000, 5000));
            }
            return vec;
        }

        public static vector<string> RandomString(int size)   
        {
            var vec = new vector<string>();
            var rnd = new Random();
            string tstr = "";
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < rnd.Next(1, 4); j++)
                {
                    tstr += (char)rnd.Next(65, 91);
                }
                vec.Push(tstr);
                tstr = "";
            }
            return vec;
        }

        public static vector<T> Copy(vector<T> vec)
        {
            vector<T> vec1 = new vector<T>();
            for (int i = 0; i < vec.Length; i++)
                vec1.Push(vec[i].Element);
            return vec1;
        }
        public static vector<T> SortVar(vector<T> vec, sortAbstract<T> sort) //это вешний метод, в котором применяется абстрактный класс. Передача самого объекта это и есть позднее связывание. 
        {

            vector<T> vecsorted = new vector<T>();
            vecsorted = sort.Sorting(vec);
            return vecsorted;
        }
    }

    public class vector<T> where T : IComparable<T>
    {
        // класс Node это наследник класса vector
        public Node<T> headNode;
        public Node<T> tailNode;
        public Node<T> Cur;
        public int Ncur;
        public int Length;
        public vector() // конструктор
        {
            this.headNode = null;
            this.tailNode = this.headNode;
            this.Length = 0;
        }
        public Node<T> this[int _position]
        {
            get
            {

                Node<T> tempNode = new Node<T>(); //выделение памяти под текущий элемент
                tempNode.Element = default(T); // это для того, чтобы наш элемент мог принимать разные типы данных

                if (_position < this.Length) //Если индекс меньше размера списка

                {

                    if (Math.Abs(Ncur - _position) < Math.Abs(this.Length / 2 - _position) || Math.Abs(Ncur - _position) < Math.Abs(this.Length / 2 + 1 - _position))
                    //  проверка того, что быстрее дойти от текущего элемента чем от серидины. 2 условия тк  список может быть чётный или нечётный
                    {

                        tempNode = Cur;

                        if ((_position - Ncur) < 0)
                        // если текущий элемент стоит после позиции, то идём через преивоус  
                        {

                            for (int i = Ncur; i > _position + 1; --i) tempNode = tempNode.Previous; //Ссылаюсь на предыдущий элемент

                        }
                        
                        if ((_position - Ncur) > 0)
                        // если текущий элемент стоит перед позицией то идём через некст
                        {

                            for (int i = Ncur; i < _position; ++i) tempNode = tempNode.Next; //Ссылаюсь на следующий элемент

                        }
                    }
                   
                    if (_position <= this.Length / 2)
                    {
                        tempNode = headNode;
                        for (int i = 0; i < _position; ++i) tempNode = tempNode.Next; //Ссылаюсь на следующий элемент
                    }
                    
                    if (_position > this.Length / 2)
                    {

                        tempNode = tailNode;

                        for (int i = this.Length; i > _position + 1; --i) tempNode = tempNode.Previous; //Ссылаюсь на предыдущий элемент

                    }

                    Cur = tempNode;

                    Ncur = _position;

                }

                return tempNode;

            }
        }
        public void Push(T _element)
        {
            if (headNode == null)
            {
                this.headNode = new Node<T>();
                this.headNode.Element = _element;
                this.tailNode = this.headNode;
                this.headNode.SetNextNode(null);
                this.headNode.SetPreviousNode(null);
            }
            else
            {
                Node<T> newNode = new Node<T>();
                this.tailNode.SetNextNode(newNode);
                newNode.SetPreviousNode(this.tailNode);
                this.tailNode = newNode;
                this.tailNode.Element = _element;
                this.tailNode.SetNextNode(null);
            }
            ++this.Length;
        }
        public void swap(int position1, int position2)
        {
            { 
                Node<T> tempNode = new Node<T>();
                tempNode.Element = this[position1].Element;
                this[position1].Element = this[position2].Element;
                this[position2].Element = tempNode.Element;
            }
        }
        public void clear()
        {
            this.headNode = null;
            this.tailNode = this.headNode;
            this.Length = 0;
        }
        public void Insert(int h, T value) // вставка элемента по индексу
        {
            Node<T> newNode = new Node<T>();
            newNode.Element = value;
            if (h == 0)
            {

                this.headNode.SetPreviousNode(newNode);
                newNode.SetNextNode(this.headNode);
                newNode.SetPreviousNode(null);
                this.headNode = newNode;
                this.Length++;
            }
            else if (h == this.Length)
            {
                this.Push(value);
            }
            else
            {
                Node<T> prev = this[h];
                newNode.SetPreviousNode(prev.Previous);
                prev.SetPreviousNode(newNode);
                newNode.SetNextNode(prev);
                this[h - 1].SetNextNode(newNode);
                this.Length++;
            }
        }
        public void RemoveAt(int h) // удаление элемента по индексу
        {
            // следующий элемент получает ссылку на сслыку предыдущего элемента. А предыдущий элемент получает ссылку на ссылку следующего элемента
            if (h < this.Length && h >= 0)
            {
                if (h == 0)
                {
                    this.headNode = this[h + 1];
                    this.headNode.SetPreviousNode(null);
                }
                else if (h == this.Length - 1)
                {
                    this.tailNode = this[h - 1];
                    this.tailNode.SetNextNode(null);
                }
                else
                {
                    Node<T> temp1 = this[h];
                    temp1.Previous.SetNextNode(temp1.Next);
                    temp1.Next.SetPreviousNode(temp1.Previous);
                }
                this.Length--;
            }
        }

    }
    abstract class sortAbstract<T> where T : IComparable<T>
    {
        public abstract vector<T> Sorting(vector<T> unsorted);
    }

    class sortingByExchange<T> : sortAbstract<T> where T : IComparable<T> // сортировка 
    {
        public int comparison, shift;
        
        public override vector<T> Sorting(vector<T> vec)
        {
            comparison = 0;
            shift = 0;
            for (int i = 0; i < vec.Length; i++)
            {
                for (int j = i + 1; j < vec.Length; j++)
                {
                    comparison++;
                    if (vec[i] > vec[j])
                    {
                        shift++;
                        vec.swap(i, j);
                    }
                }
            }
            return vec;
        }
    }

    class sortingByInsert<T> : sortAbstract<T> where T : IComparable<T>
    {
        public int comparison, shift;

        public override vector<T> Sorting(vector<T> vec)
        {
            int j;
            Node<T> x;
            for (int i = 1; i < vec.Length; i++)
            {
                x = vec[i];
                j = i;
                int temp = 1;
                while (j > 0 && vec[j - 1] > x)
                {
                    temp++;
                    j--;
                }
                if (j == 0)
                    temp--;
                if (i != j)
                {
                    vec.Insert(j, x.Element);
                    shift++;
                    vec.RemoveAt(i + 1);
                }
                comparison += temp;
            }
            return vec;
        }


    }
   
}
