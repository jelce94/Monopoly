using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    class SpecialCards
    {
        public int card_number { get; set; }
        bool chance { get; set; }
        public SpecialCards(int cn, bool ch) => (card_number, chance) = (cn, ch);
        public string SpecialCardAction(Player p, ListPlayer l_p, ListSpaces l_s)
        {
            string message = "";
            if (chance)
            {
                Console.WriteLine("This chance card text is :\n");
                if (card_number == 0)
                {
                    message = "Advance to 'Start'. (Collect $200)";
                    p.position = 40;
                }
                if (card_number == 1)
                {
                    message = "Advance to Avenue Henri-Martin. If you pass 'Start', collect $200.";
                    if (p.position > 24)
                    {
                        p.position = 64;
                    }
                    else
                    {
                        p.position = 24;
                    }
                    p.payRent(l_s, 0);
                }
                if (card_number == 2)
                {
                    message = "Advance to Boulevard de la Villette. If you pass 'Start', collect $200. ";
                    if (p.position > 11)
                    {
                        p.position = 51;
                    }
                    else
                    {
                        p.position = 11;
                    }
                    p.payRent(l_s, 0);
                }
                if (card_number == 3)
                {
                    message = "Advance to the nearest Company. If unowned, you may buy it from the Bank, else pay the rent (if you pay the rent, the value of dices will be 10).";
                    int res = 0;
                    if (p.position >= 28 && p.position <= 39)
                    {
                        res = 52;
                    }
                    if (p.position >= 0 && p.position <= 11)
                    {
                        res = 12;
                    }
                    if (p.position >= 12 && p.position <= 27)
                    {
                        res = 28;
                    }
                    p.position = res;
                    p.payRent(l_s, 10);
                }
                if (card_number == 4)
                {
                    message = "Advance to the nearest Station. If unowned, you may buy it from the Bank, else pay the rent.";
                    int res = 0;
                    if (p.position >= 35 && p.position <= 39)
                    {
                        res = 45;
                    }
                    if (p.position >= 0 && p.position <= 4)
                    {
                        res = 5;
                    }
                    if (p.position >= 5 && p.position <= 14)
                    {
                        res = 15;
                    }
                    if (p.position >= 15 && p.position <= 24)
                    {
                        res = 25;
                    }
                    if (p.position >= 25 && p.position <= 34)
                    {
                        res = 35;
                    }
                    p.position = res;
                    p.payRent(l_s, 10);
                }
                if (card_number == 5)
                {
                    message = "Bank pays you dividend of $50. ";
                    p.account += 50;
                    Console.WriteLine("Your account have been credited of 50 euros !");
                }
                if (card_number == 6)
                {
                    message = "Go back 3 spaces";
                    p.position -= 3;
                }
                if (card_number == 7)
                {
                    message = "Go to Jail. Go directly to Jail. Do not pass 'Start', do not collect $200";
                    p.goToPrison();
                }
                if (card_number == 8)
                {
                    message = "Make general repairs on all your property: For each house pay $25, For each hotel $100.";
                    int houses = p.housesOwend();
                    int hotels = p.hotelsOwend();
                    Console.WriteLine("You owe " + houses + " houses and " + hotels + " hotels");
                    if (houses == 0 && hotels == 0)
                    {
                        Console.WriteLine("You're not concerned by this card, you don't owe any house/hotel. ");
                    }
                    else
                    {
                        Console.WriteLine("You will pay " + 25 * houses + " euros for all your houses and  " + 100 * hotels + " euros for all your hotels");
                        int pr = (25 * houses) + (100 * hotels);
                        Console.WriteLine("A total of " + pr + " euros");
                        p.account -= pr;
                        SpecialSpace spec = (SpecialSpace)l_s.getSpace(20);
                        spec.money_in_parking += pr;
                        Console.WriteLine("The money will go to the Public Parking (position 20)");
                        Console.WriteLine("The actual amount of money in the parking is : " + spec.money_in_parking);
                    }
                }
                if (card_number == 9)
                {
                    message = "Pay poor tax of $15 ";
                    p.account -= 15;
                    SpecialSpace spec = (SpecialSpace)l_s.getSpace(20);
                    spec.money_in_parking += 15;
                    Console.WriteLine("The money will go to the Public Parking (position 20)");
                    Console.WriteLine("The actual amount of money in the parking is : " + spec.money_in_parking);

                }
                if (card_number == 10)
                {
                    message = "Take a trip to Gare Montparnasse. If you pass 'Start', collect $200. ";
                    if (p.position > 5)
                    {
                        p.position = 45;
                    }
                    else
                    {
                        p.position = 5;
                    }
                    p.payRent(l_s, 0);
                }
                if (card_number == 11)
                {
                    message = "Advance to the nearest Station. If unowned, you may buy it from the Bank, else pay the rent.";
                    int res = 0;
                    if (p.position >= 35 && p.position <= 39)
                    {
                        res = 45;
                    }
                    if (p.position >= 0 && p.position <= 4)
                    {
                        res = 5;
                    }
                    if (p.position >= 5 && p.position <= 14)
                    {
                        res = 15;
                    }
                    if (p.position >= 15 && p.position <= 24)
                    {
                        res = 25;
                    }
                    if (p.position >= 25 && p.position <= 34)
                    {
                        res = 35;
                    }
                    p.position = res;
                    p.payRent(l_s, 10);
                }
                if (card_number == 12)
                {
                    message = "Take a walk on Rue de la Paix.";
                    p.position = 39;
                    p.payRent(l_s, 0);
                }
                if (card_number == 13)
                {
                    message = "You have been elected Chairman of the Board. Pay each player 50 euros. ";
                    Player pla = p.next;
                    while (pla.name != p.name)
                    {
                        if (!(pla.in_prison))
                        {
                            Console.WriteLine(pla.name + ", you've earned 50 euros ! ");
                            pla.account += 50;
                            p.account -= 50;
                        }
                        else
                        {
                            Console.WriteLine(pla.name + ", you can't earn any money because you're in prison !");
                        }
                        pla = pla.next;
                    }
                }
                if (card_number == 14)
                {
                    message = "Your building loan matures. Receive $150. ";
                    p.account += 150;
                    Console.WriteLine("Your account have been credited of 150 euros !");
                }
                if (card_number == 15)
                {
                    message = "You have won a crossword competition. Collect $100.";
                    p.account += 100;
                    Console.WriteLine("Your account have been credited of 100 euros !");
                }
            }
            else
            {
                Console.WriteLine("This community chest card text is :\n");
                if (card_number == 0)
                {
                    message = "Advance to 'Start'. (Collect $200)";
                    p.position = 40;
                }
                if (card_number == 1)
                {
                    p.account += 200;
                    message = "Bank error in your favor. Collect 200 euros.";
                    Console.WriteLine("Your account have been credited of 200 euros !");
                }
                if (card_number == 2)
                {
                    p.account -= 50;
                    message = "Doctor's fees. Pay 50 euros.";
                    SpecialSpace spec = (SpecialSpace)l_s.getSpace(20);
                    spec.money_in_parking += 50;
                    Console.WriteLine("The money will go to the Public Parking (position 20)");
                    Console.WriteLine("The actual amount of money in the parking is : " + spec.money_in_parking);
                }
                if (card_number == 3)
                {
                    p.account += 50;
                    message = "From sale of stock you get 50 euros.";
                    Console.WriteLine("Your account have been credited of 500 euros !");
                }
                if (card_number == 4)
                {
                    message = "Go to Jail. Go directly to Jail. Do not pass 'Start', do not collect $200";
                    p.goToPrison();
                }
                if (card_number == 5)
                {
                    message = "Grand Opera Night. Collect 50 euros from every player for opening night seats.";
                    Player pla = p.next;
                    while (pla.name != p.name)
                    {
                        Console.WriteLine(pla.name + ", you have to pay 50 euros to " + p.name);
                        pla.account -= 50;
                        p.account += 50;
                        pla = pla.next;
                    }
                }
                if (card_number == 6)
                {
                    p.account += 100;
                    message = "Holiday Fund matures. Receive 100 euros.";
                    Console.WriteLine("Your account have been credited of 100 euros !");
                }
                if (card_number == 7)
                {
                    p.account += 20;
                    message = "Income tax refund. Collect 20 euros.";
                    Console.WriteLine("Your account have been credited of 20 euros !");
                }
                if (card_number == 8)
                {
                    message = "It is your birthday. Collect 10 euros from every player.";
                    Player pla = p.next;
                    while (pla.name != p.name)
                    {
                        Console.WriteLine(pla.name + ", you have to pay 10 euros to " + p.name);
                        pla.account -= 10;
                        p.account += 10;
                        pla = pla.next;
                    }
                }
                if (card_number == 9)
                {
                    p.account += 100;
                    message = "Life insurance matures. Collect 100 euros.";
                    Console.WriteLine("Your account have been credited of 100 euros !");
                }
                if (card_number == 10)
                {
                    p.account -= 50;
                    message = "Hospital Fees. Pay 50 euros.";
                    SpecialSpace spec = (SpecialSpace)l_s.getSpace(20);
                    spec.money_in_parking += 50;
                    Console.WriteLine("The money will go to the Public Parking (position 20)");
                    Console.WriteLine("The actual amount of money in the parking is : " + spec.money_in_parking);
                }
                if (card_number == 11)
                {
                    p.account -= 50;
                    message = "School fees. Pay 50 euros.";
                    SpecialSpace spec = (SpecialSpace)l_s.getSpace(20);
                    spec.money_in_parking += 50;
                    Console.WriteLine("The money will go to the Public Parking (position 20)");
                    Console.WriteLine("The actual amount of money in the parking is : " + spec.money_in_parking);
                }
                if (card_number == 12)
                {
                    p.account += 25;
                    message = "Receive 25 euros consultancy fee.";
                    Console.WriteLine("Your account have been credited of 25 euros !");
                }
                if (card_number == 13)
                {
                    message = "You are assessed for street repairs: Pay 40 euros per house and 115 euros per hotel you own.";
                    int houses = p.housesOwend();
                    int hotels = p.hotelsOwend();
                    Console.WriteLine("You owe " + houses + " houses and " + hotels + " hotels");
                    if (houses == 0 && hotels == 0)
                    {
                        Console.WriteLine("You're not concerned by this card, you don't owe any house/hotel. ");
                    }
                    else
                    {
                        Console.WriteLine("You will pay " + 40 * houses + " euros for all your houses and  " + 115 * hotels + " euros for all your hotels");
                        int pr = (40 * houses) + (115 * hotels);
                        Console.WriteLine("A total of " + pr + " euros");
                        p.account -= pr;
                        SpecialSpace spec = (SpecialSpace)l_s.getSpace(20);
                        spec.money_in_parking += pr;
                        Console.WriteLine("The money will go to the Public Parking (position 20)");
                        Console.WriteLine("The actual amount of money in the parking is : " + spec.money_in_parking);
                    }
                }
                if (card_number == 14)
                {
                    p.account += 10;
                    message = "You have won second prize in a beauty contest. Collect 10 euros.";
                    Console.WriteLine("Your account have been credited of 10 euros !");
                }
                if (card_number == 15)
                {
                    p.account += 100;
                    message = "You inherit 100 euros.";
                    Console.WriteLine("Your account have been credited of 100 euros !");
                }
            }
            return message;
        }
    }
}
