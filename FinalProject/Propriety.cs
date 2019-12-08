using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    class Propriety : Spaces
    {
        public int price { get; }
        public bool possessed { get; set; }
        public Player pl { get; set; }
        public int rent { get; set; }

        public Propriety(string name, int pri, int ren) : base(name)
        {
            price = pri;
            possessed = false;
            pl = null;
            rent = ren;
        }
        public Player auction(ListPlayer P)
        {
            Console.Clear();
            Player pla = P.getPlayer(0);
            Console.WriteLine("This auction starts");
            Console.WriteLine("The propriety to buy is " + name);
            int amount = 0;
            int res = 10;
            string n = "";
            while (n != pla.name)
            {
                if (!(pla.in_prison))
                {
                    Console.WriteLine(pla.name + " this is you turn to auction");
                    Console.WriteLine("Do you want to bid for this propriety : " + name + " ?");
                    Console.WriteLine("The current bid is " + amount);
                    Console.WriteLine("Press 1 for yes, 0 for no");
                    res = Convert.ToInt32(Console.ReadLine());
                    while (res != 0 && res != 1)
                    {
                        Console.WriteLine("Please enter a correct awnser.");
                        Console.WriteLine("Press 1 for yes or 0 for no. ");
                        res = Convert.ToInt32(Console.ReadLine());
                    }
                    if (res == 1)
                    {
                        int am = 0;
                        bool bid_placed = false;
                        while (!(bid_placed) && am != 9999)
                        {
                            Console.WriteLine("Enter the amount of your bid :");
                            am = Convert.ToInt32(Console.ReadLine());
                            if (am > amount)
                            {
                                if (am <= pla.account)
                                {
                                    Console.WriteLine("You place your bid for this propriety !");
                                    amount = am;
                                    bid_placed = true;
                                    n = pla.name;
                                }
                                else
                                {
                                    Console.WriteLine("You don't have enough money in your account !");
                                    Console.WriteLine("Press 9999 to cancel your bid");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Your bid is lower than the current amount !");
                                Console.WriteLine("The current bid is " + amount);
                                Console.WriteLine("Press 9999 to cancel your bid");
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine(pla.name + ", you can't play be in this action because you're in prison !");
                }
                pla = pla.next;
            }
            Console.WriteLine(pla.name + " won this auction");
            Console.WriteLine("He bought " + name + " for " + amount + " euros.");
            pla.account = pla.account - amount;
            return pla;
        }
        public override string ToString()
        {
            return ("\n -----------------------------------------------" +
                    base.ToString() +
                    "\n Price : " + price +
                    "\n Is Possessed : " + possessed +
                    "\n Possessed by : \n" + pl +
                    "\n Price of rent : " + rent);
        }
    }
}
