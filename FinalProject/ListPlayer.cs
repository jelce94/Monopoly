using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    class ListPlayer : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            return new ListPlayerIterator(this);
        }
        private IEnumerator GetEnumerator1()
        {
            return this.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator1();
        }
        public Player Head { get; set; }
        public int Size { get { return ComputeSize(); } }
        public ListPlayer() => Head = null;
        public ListPlayer(Player pl) => Head = pl;
        public int ComputeSize()
        {
            Player current = Head;
            int pos = 0;
            while (current != null)
            {
                current = current.next;
                pos++;
            }
            return pos;
        }
        public void AddHead(Player newPlayer)
        {
            Player current = Head;
            newPlayer.next = Head;
        }
        public void AddTail(Player newPlayer)
        {
            Player current = Head;
            while (current.next != null)
            {
                current = current.next;
            }
            current.next = newPlayer;
        }
        public string GetPlayerName(int idx)
        {
            Player current = Head;
            if (Size >= idx)
            {
                for (int i = 0; i < idx; i++)
                {
                    current = current.next;
                }
            }
            return current.name;
        }
        public Player getPlayer(int idx)
        {
            Player current = Head;
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
        public void deletePlayer(int idx)
        {
            getPlayer(idx - 1).next = getPlayer(idx + 1);

        }
        public bool onList(string n)
        {
            bool on_list = false;
            if (this.getPlayer(0).name == n)
            {
                on_list = true;
            }
            Player pl = this.getPlayer(1);
            while (pl != this.getPlayer(0))
            {
                if (pl.name == n)
                {
                    on_list = true;
                }
                pl = pl.next;
            }
            return on_list;
        }
        public Player getPlayerByName(string n)
        {
            Player current = Head;
            while (current.name != n)
            {
                current = current.next;
            }

            return current;
        }
        public int getPlayerPosition(Player p, int size)
        {
            int pos = 0;
            for (int i = 0; i < size - 1; i++)
            {
                if (getPlayer(i) == p)
                {
                    pos = i;
                }
            }
            return pos;
        }
    }
}