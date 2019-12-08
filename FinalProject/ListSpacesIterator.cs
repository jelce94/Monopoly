using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    class ListSpacesIterator : IEnumerator
    {
        ListSpaces list;
        int current_space_idx;

        public ListSpacesIterator(ListSpaces l)
        {
            list = l;
            current_space_idx = -1;
        }

        public Spaces Current
        {
            get
            {
                if (list.Size > 0)
                    return list.getSpace(current_space_idx);
                return null;
            }
        }

        object IEnumerator.Current
        {
            get { return Current; }
        }

        public bool MoveNext()
        {
            current_space_idx++;

            if (current_space_idx >= list.Size)
                return false;
            return true;
        }

        public void Reset()
        {
            current_space_idx = -1;
        }


    }
}