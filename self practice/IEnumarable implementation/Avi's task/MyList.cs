using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//for furthur examples and help on IEnumerable<T> (Generic) visit:
//https://learn.microsoft.com/en-us/dotnet/api/system.collections.ienumerable?view=net-8.0


namespace Avi_s_task
{
    internal class MyList<T> : IEnumerable<T>
    {
        private readonly List<T> list = new List<T>();

        public void Add(T item)
        {
            list.Add(item);
        }

        public MyList() { }
        public IEnumerator<T> GetEnumerator()
        {
            return (IEnumerator<T>)new MyListEnumerator<T>(list);
        }
       


        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }

    internal class MyListEnumerator<T> : IEnumerator<T>
    {

        public List<T> list;

        int position = -1;
        public MyListEnumerator(List<T> TheList)
        {
            list = TheList;
        }
            
        private T _current;
        public T Current 
        {
            get
            {
                try
                {
                    return list[position];
                }
                catch (IndexOutOfRangeException)
                {

                    throw new InvalidOperationException();
                }
            }
        }

        object IEnumerator.Current { get { return this.Current; } }

        public void Dispose()
        {
            
        }

        public bool MoveNext()
        {
            position++;
            return position < list.Count;
        }

        public void Reset()
        {
            position = -1;
        }
    }



}

