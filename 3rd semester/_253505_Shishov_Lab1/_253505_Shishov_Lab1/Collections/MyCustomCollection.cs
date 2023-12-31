using _253505_Shishov_Lab1.Interfaces;
using System.Collections;
using System.Reflection.PortableExecutable;
using System.Runtime.Versioning;

namespace _253505_Shishov_Lab1.Collections
{
    public class MyException : Exception
    {
        public MyException(string message) : base(message) { }
    }
    class MyCustomCollection<T> : ICustomCollection<T>, IEnumerable<T>
    {
        Node<T> head;
        Node<T> cur;
        int size;
        int cur_index;
        public MyCustomCollection()
        {
            head = null;
            cur_index = -1;
            cur = null;
            size = 0;
        }

        public void Next()
        {
            cur_index++;
            if(cur.Next != null)
            {
                cur = cur.Next;
            } else
            {
                throw new IndexOutOfRangeException("index out of range");
            }
        }

        public int Count
        {
            get { return size; }
        }

        public void Reset()
        {
            cur_index = 0;
            if (head != null)
            {
                cur = head;
            }
        }

        public void Remove(T item)
        {
            if(head == null)
            {
                throw new MyException("empty collection");
            }
            bool is_found = false;
            Node<T> buf = head;
            while (buf.Next != null)
            {
                if (buf.Next.Data.Equals(item))
                {
                    buf.Next = buf.Next.Next;
                    is_found = true;
                    break;
                }

                buf = buf.Next;
            }

            if (!is_found)
            {
                throw new MyException("can't find item in collection");
            }
            cur = buf;
            size--;
            cur_index--;
        }
        
        public T RemoveCurrent()
        {
            if (cur == null)
            {
                throw new MyException("current is missing");
                return default(T);
            }
            Node<T> tmp = cur;
            Remove(tmp.Data);
            if (head != null)
            {
                cur = head;
            }
            cur_index = 0;
            return tmp.Data;
        }

        public void Add(T item)
        {
            if(head == null)
            {
                head = new Node<T>(item);
                cur = head;
            }
            else
            {
                Node<T> buf = head;
                while(buf.Next != null)
                {
                    buf = buf.Next;
                }
                buf.Next = new Node<T>(item);
            }
            cur_index++;
            size++;
        }

        private Node<T> getNode(int index)
        {
            if(head == null)
            {
                return null;
            }
            Node<T> buf = head;
            int count = 0;
            while (count != index)
            {
                buf = buf.Next;
                count++;
            }
            return buf;
        }

        public T this[int index]
        {
            get
            {
                if(index > size - 1)
                {
                    throw new IndexOutOfRangeException("index out of range");
                }
                return getNode(index).Data;
            }
            set
            {
                if(index > size -1)
                {
                    throw new IndexOutOfRangeException("index out of range");
                }
                this.getNode(index).Data = value;
            }
        }

        public T Current()
        {
            if (cur_index != -1 && size > 0)
            {
                return cur.Data;
            }
            return default(T);
        }

        public override string ToString()
        {
            string collection = "";
            int count = 0;
            Node<T> buf = head;
            while (count!=size)
            {
                collection += buf.Data.ToString() + ", ";
                buf = buf.Next;
                count++;
            }
            return collection;
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            Node<T> cur = head;
            while(cur != null)
            {
                yield return cur.Data;
                cur = cur.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)this).GetEnumerator();
        }
    }

    public class Node<T>
    {
        T? data;
        Node<T>? next;

        public T? Data { get { return data; } set { this.data = value; } }
        public Node<T>? Next { get { return next; } set { this.next = value; } }

        public Node()
        {
            Data = default(T);
            Next = null;
        }
        public Node(T item)
        {
            Data = item;
            Next = null;
        }
        
    }
}