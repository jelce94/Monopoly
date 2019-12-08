using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    class ListSpaces : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            return new ListSpacesIterator(this);
        }

        private IEnumerator GetEnumerator1()
        {
            return this.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator1();
        }
        public Spaces Head { get; set; }
        public int Size { get { return ComputeSize(); } }
        public ListSpaces() => Head = null;
        public ListSpaces(Spaces sp) => Head = sp;
        public int ComputeSize()
        {
            Spaces current = Head;
            int pos = 0;
            while (current != null)
            {
                current = current.next;
                pos++;
            }
            return pos;
        }
        public void AddHead(Spaces newSpace)
        {
            Spaces current = Head;
            newSpace.next = Head;
        }
        public void AddTail(Spaces newSpace)
        {
            Spaces current = Head;
            while (current.next != null)
            {
                current = current.next;
            }
            current.next = newSpace;
        }
        public void changeSpace(int idx, Spaces newSpace)
        {
            Spaces current = Head;
            int pos = 0;
            while (pos != idx)
            {
                current = current.next;
                pos++;
            }
            current = newSpace;
        }
        public string GetSpaceName(int idx)
        {
            Spaces current = Head;
            if (Size >= idx)
            {
                for (int i = 0; i < idx; i++)
                {
                    current = current.next;
                }
            }
            return current.name;
        }
        public Spaces getSpace(int idx)
        {
            Spaces current = Head;
            int pos = 0;
            while (current != null)
            {
                if (pos == idx)
                    return current;

                current = current.next;
                pos++;
            }

            return null;
        }
        public bool allColors(string c)
        {
            bool res = false;
            if (this != null)
            {
                int count = 0;
                foreach (Street s in this)
                {
                    if (s.color == c)
                    {
                        count++;
                    }
                }
                if (c == "brown" || c == "blue")
                {
                    if (count == 2)
                    {
                        res = true;
                    }
                }
                else
                {
                    if (count == 3)
                    {
                        res = true;
                    }
                }
            }
            return res;
        }
        public int number_of_companies()
        {
            int res = 0;
            foreach (Spaces s in this)
            {
                if (s is Company)
                {
                    res++;
                }
            }
            return res;
        }
        public int number_of_stations()
        {
            int res = 0;
            foreach (Spaces s in this)
            {
                if (s is Station)
                {
                    res++;
                }
            }
            return res;
        }
        public bool onList(string n, int size)
        {
            int count = 0;
            bool on_list = false;
            while (count < size && on_list == false)
            {
                if (getSpace(count).name == n)
                {
                    on_list = true;
                }
                count++;
            }
            return on_list;
        }
        public int getSpacePosition(string n)
        {
            int pos = 0;
            while (pos < 40 && n != getSpace(pos).name)
            {
                pos++;
            }
            return pos;
        }
        public override string ToString()
        {
            string res = Size + " \n";
            foreach (Spaces s in this)
            {
                res = res + s.name + "\n";
            }
            return res;
        }
    }
}