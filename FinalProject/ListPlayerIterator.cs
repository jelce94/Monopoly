using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    class ListPlayerIterator : IEnumerator
    {
        ListPlayer list;
        int current_player_idx;

        public ListPlayerIterator(ListPlayer l)
        {
            list = l;
            current_player_idx = -1;
        }

        public Player Current
        {
            get
            {
                if (list.Size > 0)
                    return list.getPlayer(current_player_idx);
                return null;
            }
        }

        object IEnumerator.Current
        {
            get { return Current; }
        }

        public bool MoveNext()
        {
            current_player_idx++;

            if (current_player_idx >= list.Size)
                return false;
            return true;
        }

        public void Reset()
        {
            current_player_idx = -1;
        }
    }
}