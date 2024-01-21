using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//for help and demonstartion of this impementation of iEnumerable (non-generic)
//https://learn.microsoft.com/en-us/dotnet/api/system.collections.ienumerable?view=net-8.0


namespace IEnumarable_implementation
{
    internal class Dogs : IEnumerable
    {
        private Dog[] _doggyArray;
        public Dogs(Dog[] dogArray)
        {
            _doggyArray = new Dog[dogArray.Length];
            for (int i = 0; i < dogArray.Length; i++)
            {
                _doggyArray[i] = dogArray[i];
            }

            
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumarator();
        }

        public DogsEnum GetEnumarator()
        {
            return new DogsEnum(_doggyArray);
        }

        
    }

    public class DogsEnum : IEnumerator
    {
        public Dog[] array;

        int position = -1;

        public DogsEnum(Dog[] Array )
        {
            array = Array;
        }

        public bool MoveNext()
        {
            position++;
            return position < array.Length;
        }

        public void Reset()
        {
            position = -1;
        }

        Object IEnumerator.Current {
            get 
            {
                try
                {
                    return array[position];
                }
                catch (IndexOutOfRangeException)
                {

                    throw new InvalidOperationException();
                }
            }
        }

        
    }

}
