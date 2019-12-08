using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    class Program
    {
        static List<int> RandomList(int a, int b)
        {
            List<int> l = new List<int>();
            Parallel.For(a, b, i => { l.Add(i); });
            Random rng = new Random();
            int n = l.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                int value = l[k];
                l[k] = l[n];
                l[n] = value;
            }
            return l;
        }
        static ListSpaces CreateBoard()
        {
            // Creation of the game spaces ---------------------------------

            //Proprieties
            Spaces pr1 = new Street("Boulevard de Belleville", 60, "brown", 2, 10, 30, 90, 160, 250);
            Spaces pr2 = new Street("Rue Lecourbe", 80, "brown", 4, 20, 60, 180, 320, 450);
            Spaces pr3 = new Street("Rue de Vaugirard", 100, "sky_blue", 6, 30, 90, 270, 400, 550);
            Spaces pr4 = new Street("Rue de Courcelles", 100, "sky_blue", 6, 30, 90, 270, 400, 550);
            Spaces pr5 = new Street("Avenue de la Republique", 120, "sky_blue", 8, 40, 100, 300, 450, 600);
            Spaces pr6 = new Street("Boulevard de la Villette", 140, "pink", 10, 50, 150, 450, 625, 750);
            Spaces pr7 = new Street("Avenue de Neuilly", 140, "pink", 10, 50, 150, 450, 625, 750);
            Spaces pr8 = new Street("Rue de Paradis", 160, "pink", 12, 60, 180, 500, 700, 900);
            Spaces pr9 = new Street("Avenue Mozart", 180, "orange", 14, 70, 200, 550, 750, 950);
            Spaces pr10 = new Street("Boulevart Saint-Michel", 180, "orange", 14, 70, 200, 550, 750, 950);
            Spaces pr11 = new Street("Place Pigalle", 200, "orange", 16, 80, 220, 600, 800, 1000);
            Spaces pr12 = new Street("Avenue Matignon", 220, "red", 18, 90, 250, 700, 875, 1050);
            Spaces pr13 = new Street("Boulevard Malesherbes", 220, "red", 18, 90, 250, 700, 875, 1050);
            Spaces pr14 = new Street("Avenue Henri-Martin", 240, "red", 20, 100, 300, 750, 925, 1100);
            Spaces pr15 = new Street("Faubourg Saint-Honoré", 260, "yellow", 22, 110, 330, 800, 975, 1150);
            Spaces pr16 = new Street("Place de la Bourse", 260, "yellow", 22, 110, 330, 800, 975, 1150);
            Spaces pr17 = new Street("Rue de la Fayette", 280, "yellow", 24, 120, 360, 850, 1025, 1200);
            Spaces pr18 = new Street("Avenue De Breteuil", 300, "green", 26, 130, 390, 900, 1100, 1275);
            Spaces pr19 = new Street("Avenue de Foch", 300, "green", 26, 130, 390, 900, 1100, 1275);
            Spaces pr20 = new Street("Boulevard des Capucines", 320, "green", 28, 150, 450, 1000, 1200, 1400);
            Spaces pr21 = new Street("Avenue des Champs-Elysées", 350, "blue", 35, 175, 500, 1100, 1300, 1500);
            Spaces pr22 = new Street("Rue de la Paix", 400, "blue", 50, 200, 600, 1400, 1700, 2000);

            // Train Stations
            Spaces tr1 = new Station("Gare Montparnasse", 200, 25, 50, 100, 200);
            Spaces tr2 = new Station("Gare de Lyon", 200, 25, 50, 100, 200);
            Spaces tr3 = new Station("Gare du Nord", 200, 25, 50, 100, 200);
            Spaces tr4 = new Station("Gare Saint-Lazare", 200, 25, 50, 100, 200);

            // Public Service Companies
            Spaces pc1 = new Company("Electric Company", 150, 0);
            Spaces pc2 = new Company("Water Works", 150, 0);

            // Special Spaces
            Spaces ss1 = new SpecialSpace("Start");
            Spaces ss2 = new SpecialSpace("Community");
            Spaces ss3 = new SpecialSpace("Tax");
            Spaces ss4 = new SpecialSpace("Chance");
            Spaces ss5 = new SpecialSpace("PrisonVisit");
            Spaces ss6 = new SpecialSpace("Community");
            Spaces ss7 = new SpecialSpace("Parking");
            Spaces ss8 = new SpecialSpace("Chance");
            Spaces ss9 = new SpecialSpace("GtP");
            Spaces ss10 = new SpecialSpace("Community");
            Spaces ss11 = new SpecialSpace("Chance");
            Spaces ss12 = new SpecialSpace("LuxeTax");

            // Creation of game board --------------------------------------------
            ListSpaces Board = new ListSpaces(ss1);
            Board.AddTail(pr1);
            Board.AddTail(ss2);
            Board.AddTail(pr2);
            Board.AddTail(ss3);
            Board.AddTail(tr1);
            Board.AddTail(pr3);
            Board.AddTail(ss4);
            Board.AddTail(pr4);
            Board.AddTail(pr5);
            Board.AddTail(ss5);
            Board.AddTail(pr6);
            Board.AddTail(pc1);
            Board.AddTail(pr7);
            Board.AddTail(pr8);
            Board.AddTail(tr2);
            Board.AddTail(pr9);
            Board.AddTail(ss6);
            Board.AddTail(pr10);
            Board.AddTail(pr11);
            Board.AddTail(ss7);
            Board.AddTail(pr12);
            Board.AddTail(ss8);
            Board.AddTail(pr13);
            Board.AddTail(pr14);
            Board.AddTail(tr3);
            Board.AddTail(pr15);
            Board.AddTail(pr16);
            Board.AddTail(pc2);
            Board.AddTail(pr17);
            Board.AddTail(ss9);
            Board.AddTail(pr18);
            Board.AddTail(pr19);
            Board.AddTail(ss10);
            Board.AddTail(pr20);
            Board.AddTail(tr4);
            Board.AddTail(ss11);
            Board.AddTail(pr21);
            Board.AddTail(ss12);
            Board.AddTail(pr22);
            pr22.next = ss1;
            return Board;
        }
        static ListPlayer CreatePlayers(int nb_pl)
        {
            Console.WriteLine("Enter the name of player 0");
            string name = Console.ReadLine();
            Player p = new Player(name);
            ListPlayer LiPl = new ListPlayer(p);
            Console.WriteLine(p);
            for (int i = 1; i < nb_pl; i++)
            {
                Console.WriteLine("Enter the name of player " + i);
                name = Console.ReadLine();
                p = new Player(name);
                LiPl.AddTail(p);
                Console.WriteLine(p);
            }
            Console.WriteLine("Enter the position of the player who will be banker : ");
            int pos = Convert.ToInt32(Console.ReadLine());
            while (pos < 0 || pos >= nb_pl)
            {
                Console.WriteLine("Please enter a correct value ");
                Console.WriteLine("The position should be between 0 and " + (nb_pl - 1));
                pos = Convert.ToInt32(Console.ReadLine());
            }

            Banker ban = Banker.GetInstance(LiPl.getPlayer(pos));
            LiPl.getPlayer(nb_pl - 1).next = LiPl.getPlayer(0);
            return LiPl;
        }
        static void Game(ListSpaces l_s, ListPlayer l_p, Player p, int size, Queue<SpecialCards> Chance, Queue<SpecialCards> CommunityChest)
        {
            while (p.next != p)
            {
                Console.Clear();
                Console.WriteLine(p);
                Console.WriteLine("This is your turn to play !");

                if (p.in_prison)
                {
                    p.outOfPrison(l_s, l_p);
                }
                else
                {
                    p.rollDice(l_s, l_p, Chance, CommunityChest);
                    if (l_s.getSpace(p.position) is SpecialSpace)
                    {
                        p.playSpecialSpace(l_s, l_p, Chance, CommunityChest);
                    }
                    if (p.passByStart(p.position))
                    {
                        Console.WriteLine("Collect your 200 euros for completing a tour ");
                        p.account += 200;
                        Console.WriteLine("Your account is now : " + p.account);
                    }
                    p.recalculatePosition();
                    p.makeAction(l_s, l_p);
                }
                if (p.bankruptcy())
                {
                    Console.WriteLine(p.name + ", you're in bankruptcy, you're out of the game!");
                    int pos_del = l_p.getPlayerPosition(p, size);
                    l_p.deletePlayer(pos_del);
                    size = size - 1;
                }
                p = p.next;
            }
            Console.WriteLine("This game is over !");
            Console.WriteLine("Bravo " + p.name + " you won this game !");
        }
        static Queue<SpecialCards> CreateChanceQueue()
        {
            List<int> li = RandomList(0, 16);
            Queue<SpecialCards> ChanceCards = new Queue<SpecialCards>();
            for (int i = 0; i < 16; i++)
            {
                int val = li[i];
                SpecialCards c = new SpecialCards(val, true);
                ChanceCards.Enqueue(c);
            }
            return ChanceCards;
        }
        static Queue<SpecialCards> CreateCommunityChestQueue()
        {
            List<int> li = RandomList(0, 16);
            Queue<SpecialCards> CommunityChestCards = new Queue<SpecialCards>();
            for (int i = 0; i < 16; i++)
            {
                int val = li[i];
                SpecialCards c = new SpecialCards(val, false);
                CommunityChestCards.Enqueue(c);
            }
            return CommunityChestCards;
        }
        static void Main(string[] args)
        {

            // -------------------------Intialization-------------------------------------------------------
            ListSpaces Board = CreateBoard();
            Console.WriteLine("How many players will play this game ? ");
            Console.WriteLine("Please enter a number : ");
            int number_players = Convert.ToInt32(Console.ReadLine());
            while (number_players < 2)
            {
                Console.WriteLine("At least two players needed to start a game ! ");
                Console.WriteLine("How many players will play this game ? ");
                Console.WriteLine("Please enter a number : ");
                number_players = Convert.ToInt32(Console.ReadLine());
            }
            ListPlayer List_of_players = CreatePlayers(number_players);
            Queue<SpecialCards> cc = CreateChanceQueue();
            Queue<SpecialCards> ccc = CreateCommunityChestQueue();
            // ----------------------------------------------------------------------------------------------
            Game(Board, List_of_players, List_of_players.getPlayer(0), number_players, cc, ccc);
            Console.ReadLine();
        }

    }
}