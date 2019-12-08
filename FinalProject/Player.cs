using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    class Player
    {
        public string name { get; }
        public double account { get; set; }
        public int position { get; set; }
        public ListSpaces list_pr { get; set; }
        public bool in_prison { get; set; }
        public int time_in_prison { get; set; }
        public bool banker { get; set; }
        public Player next { get; set; }
        public bool Equals(Spaces other)
        {
            return this.GetHashCode() == other.GetHashCode();
        }
        public override int GetHashCode()
        {
            return name.GetHashCode();
        }
        public Player(string nm)
        {
            name = nm;
            account = 1500;
            position = 0;
            ListSpaces list_pr = new ListSpaces();
            in_prison = false;
            time_in_prison = 0;
            banker = false;
            next = null;
        }
        public void recalculatePosition()
        {
            if (position >= 40)
            {
                position = position - 40;
            }
        }
        public bool posBetween(int a, int b)
        {
            bool bwn = false;
            if (position >= a && position <= b)
            {
                bwn = true;
            }
            return bwn;
        }
        public void playSpecialSpace(ListSpaces B, ListPlayer P, Queue<SpecialCards> Chance, Queue<SpecialCards> CommunityChest)
        {
            SpecialSpace sp = (SpecialSpace)B.getSpace(position);
            if (sp.go_to_prison)
            {
                Console.WriteLine("Go to prison !");
                Console.WriteLine("You will not receive your 200 if you pas by the Start");
                goToPrison();
            }
            if (sp.tax)
            {
                Console.WriteLine("Pay a tax of 100 euros");
                SpecialSpace park = (SpecialSpace)B.getSpace(20);
                park.money_in_parking += 100;
                account -= 100;
                Console.WriteLine("The money will go to the Public Parking (position 20)");
                Console.WriteLine("The actual amount of money in the parking is : " + park.money_in_parking);
            }
            if (sp.luxe_tax)
            {
                Console.WriteLine("Pay a tax of 200 euros");
                SpecialSpace park = (SpecialSpace)B.getSpace(20);
                park.money_in_parking += 200;
                account -= 200;
                Console.WriteLine("The money will go to the Public Parking (position 20)");
                Console.WriteLine("The actual amount of money in the parking is : " + park.money_in_parking);
            }
            if (sp.chance)
            {
                Console.WriteLine("Press enter to pull a chance card");
                Console.ReadLine();
                SpecialCards cha = Chance.Dequeue();
                Console.WriteLine(cha.SpecialCardAction(this, P, B));
                Chance.Enqueue(cha);
            }
            if (sp.community_chest)
            {
                Console.WriteLine("Press enter to pull a community chest card");
                Console.ReadLine();
                SpecialCards cc = CommunityChest.Dequeue();
                Console.WriteLine(cc.SpecialCardAction(this, P, B));
                Chance.Enqueue(cc);
            }
        }
        public string rollDice(ListSpaces B, ListPlayer P, Queue<SpecialCards> Chance, Queue<SpecialCards> CommunityChest)
        {
            int count = 0;
            Console.WriteLine("Press enter to roll dices");
            Console.ReadLine();
            Random rd = new Random();
            int first_dice = rd.Next(1, 7);
            int second_dice = rd.Next(1, 7);
            Console.WriteLine("The value of the first dice is : " + first_dice);
            Console.WriteLine("The value of the second dice is : " + second_dice);
            while (first_dice == second_dice)
            {
                count++;
                if (count == 3)
                {
                    goToPrison();
                    return ("You're probably cheating, go to prison !");
                }
                position = position + 2 * first_dice;
                if (passByStart(position))
                {
                    Console.WriteLine("Collect your 200 euros for completing a tour ");
                    account += 200;
                    Console.WriteLine("Your account is now : " + account);
                }
                recalculatePosition();
                Console.WriteLine("You are now in the space number : " + position);
                Console.WriteLine(B.getSpace(position).name);
                payRent(B, first_dice + second_dice);
                if (B.getSpace(position) is SpecialSpace)
                {
                    playSpecialSpace(B, P, Chance, CommunityChest);
                }
                if (passByStart(position))
                {
                    Console.WriteLine("Collect your 200 euros for completing a tour ");
                    account += 200;
                    Console.WriteLine("Your account is now : " + account);
                }
                recalculatePosition();
                makeAction(B, P);
                Console.WriteLine("You made a double, you can play again !");
                Console.WriteLine("Press enter to roll dices again");
                Console.ReadLine();
                first_dice = rd.Next(1, 7);
                second_dice = rd.Next(1, 7);
                Console.WriteLine("The value of the first dice is : " + first_dice);
                Console.WriteLine("The value of the second dice is : " + second_dice);
            }
            int total = first_dice + second_dice;
            position = position + total;

            if (passByStart(position))
            {
                Console.WriteLine("Collect your 200 euros for completing a tour ");
                account += 200;
                Console.WriteLine("Your account is now : " + account);
            }
            recalculatePosition();
            payRent(B, total);
            Console.WriteLine("You are now in the space number : " + position);
            Console.WriteLine(B.getSpace(position).name);
            return (" You've turned the dices, make an action.");
        }
        public void payRent(ListSpaces B, int dices)
        {
            if (B.getSpace(position) is Propriety)
            {
                Propriety prop = (Propriety)B.getSpace(position);
                if (prop.possessed)
                {
                    if (prop.pl == this)
                    {
                        Console.WriteLine("This is your propriety, you're at home.");
                    }
                    else
                    {
                        Console.WriteLine("This propriety is owned by " + prop.pl.name + ". You have to pay a rent !");
                        int money = 0;
                        if (prop is Company)
                        {
                            Company comp = (Company)prop;
                            int n_of_comp = comp.pl.list_pr.number_of_companies();
                            if (n_of_comp == 1)
                            {
                                money = 4 * dices;
                            }
                            else
                            {
                                money = 10 * dices;
                            }
                        }
                        if (prop is Station)
                        {
                            Station stat = (Station)prop;
                            int n_of_stat = stat.pl.list_pr.number_of_stations();
                            if (n_of_stat == 1)
                            {
                                money = stat.rent;
                            }
                            if (n_of_stat == 2)
                            {
                                money = stat.rent2stations;
                            }
                            if (n_of_stat == 3)
                            {
                                money = stat.rent3stations;
                            }
                            if (n_of_stat == 4)
                            {
                                money = stat.rent4stations;
                            }
                        }
                        if (prop is Street)
                        {
                            Street st = (Street)prop;
                            if (st.pl.list_pr.allColors(st.color))
                            {
                                money = st.rent * 2;
                                if (st.house == 1)
                                {
                                    money = st.rent1house;
                                }
                                if (st.house == 2)
                                {
                                    money = st.rent2house;
                                }
                                if (st.house == 3)
                                {
                                    money = st.rent3house;
                                }
                                if (st.house == 4)
                                {
                                    money = st.rent4house;
                                    if (st.hotel == 1)
                                    {
                                        money = st.renthotel;
                                    }
                                }
                            }
                            else
                            {
                                money = st.rent;
                            }
                        }
                        Console.WriteLine("You have to pay " + money + " euros to " + prop.pl.name);
                        account = account - money;
                        prop.pl.account += money;
                    }
                }
            }
        }
        public void goToPrison()
        {
            position = 10;
            in_prison = true;
        }
        public void outOfPrison(ListSpaces B, ListPlayer P)
        {
            Console.WriteLine("You're on prison !");
            Console.WriteLine("Press enter to roll dices");
            Console.ReadLine();
            Random rd = new Random();
            int first_dice = rd.Next(1, 7);
            int second_dice = rd.Next(1, 7);
            Console.WriteLine("The value of the first dice is : " + first_dice);
            Console.WriteLine("The value of the second dice is : " + second_dice);
            if (first_dice == second_dice)
            {
                Console.WriteLine("You're out of prison !");
                position = position + 2 * first_dice;
                recalculatePosition();
                Console.WriteLine("You are now in the space number : " + position);
                Console.WriteLine(B.getSpace(position));
                Console.WriteLine(" You've turned the dices, make an action.");
                in_prison = false;
                time_in_prison = 0;
                makeAction(B, P);
            }
            else
            {
                time_in_prison++;
                if (time_in_prison >= 3)
                {
                    Console.WriteLine("You're out of prison !");
                    in_prison = false;
                    time_in_prison = 0;
                    Console.WriteLine("You will play on your next turn.");
                }
                else
                {
                    Console.WriteLine("Sorry, you will still in prison for " + (3 - time_in_prison) + "turns ...");
                }
            }
            Console.ReadLine();
        }
        public void makeAction(ListSpaces B, ListPlayer P)
        {

            bool fin = false;
            bool valid = true;
            string lecture = "";
            do
            {
                Console.WriteLine("\nPlayer Action");
                Console.WriteLine("\n Turn of " + name);
                Console.WriteLine("-----------------\n\n");
                fin = false;
                Console.WriteLine();
                Console.WriteLine("1 : Buy this propiety");
                Console.WriteLine("2 : Buy a house or an hotel");
                Console.WriteLine("3 : Get informations");
                Console.WriteLine("4 : Pass your turn");

                do
                {
                    lecture = "";
                    valid = true;

                    Console.Write("\n Choose an action : ");
                    lecture = Console.ReadLine();
                    Console.WriteLine(lecture);
                    if (lecture == "" || !"1234".Contains(lecture[0]))
                    {
                        Console.WriteLine("Your choice <" + lecture + "> is not valid = > Please choose a correct action. ");
                        valid = false;
                    }
                } while (!valid);


                switch (lecture[0])
                {
                    case '1':
                        Console.Clear();
                        buySpace(B, P);
                        break;
                    case '2':
                        Console.Clear();
                        buyH(B);
                        break;
                    case '3':
                        Console.Clear();
                        getInformation(B, P);
                        break;
                    case '4':
                        Console.Clear();
                        Console.WriteLine("You pass your turn");
                        if (B.getSpace(position) is Propriety)
                        {
                            Propriety prop = (Propriety)B.getSpace(position);
                            if (!(prop.possessed))
                            {
                                Console.WriteLine("This propriety will be auctionned");
                                Player pla = prop.auction(P);
                                prop.possessed = true;
                                prop.pl = pla;
                                B.changeSpace(position, prop);
                                if (pla.list_pr == null)
                                {
                                    prop = (Propriety)B.getSpace(position).Shallowcopy();
                                    prop.next = null;
                                    pla.list_pr = new ListSpaces(prop);
                                }
                                else
                                {
                                    prop = (Propriety)B.getSpace(position).Shallowcopy();
                                    prop.next = null;
                                    pla.list_pr.AddTail(prop);
                                }
                            }
                        }
                        Console.ReadKey();
                        fin = true;
                        break;
                    default:
                        Console.WriteLine("\n Please choose a correct action.");
                        break;
                }
            } while (!fin);
        }
        public bool passByStart(int final)
        {
            bool res = false;
            if (final >= 40)
            {
                res = true;
            }
            return res;
        }
        public void getInformation(ListSpaces B, ListPlayer P)
        {
            Console.WriteLine("\nInformations");
            Console.WriteLine("-----------------\n\n");
            bool end = false;
            bool valid = true;
            string lecture = "";
            do
            {
                end = false;
                Console.WriteLine();
                Console.WriteLine("1 : Players informations");
                Console.WriteLine("2 : Spaces informations");
                Console.WriteLine("3 : Leave the information option");

                do
                {
                    lecture = "";
                    valid = true;

                    Console.Write("\n Choose an action : ");
                    lecture = Console.ReadLine();
                    Console.WriteLine(lecture);
                    if (lecture == "" || !"123".Contains(lecture[0]))
                    {
                        Console.WriteLine("Your choice <" + lecture + "> is not valid = > Please choose a correct action. ");
                        valid = false;
                    }
                } while (!valid);


                switch (lecture[0])
                {
                    case '1':
                        Console.Clear();
                        getInformationP(P);
                        break;
                    case '2':
                        Console.Clear();
                        getInformationS(B);
                        break;
                    case '3':
                        Console.Clear();
                        Console.WriteLine("You leave this optionnality");
                        Console.ReadKey();
                        end = true;
                        break;
                    default:
                        Console.WriteLine("\n Please choose a correct action.");
                        break;
                }
            } while (!end);
        }
        public void getInformationS(ListSpaces B)
        {
            Console.WriteLine("\nSpaces Informations");
            Console.WriteLine("-----------------\n\n");
            bool end = false;
            bool valid = true;
            string lecture = "";
            do
            {
                end = false;
                Console.WriteLine();
                Console.WriteLine("1 : Spaces informations by name");
                Console.WriteLine("2 : Spaces informations by number");
                Console.WriteLine("3 : Leave the spaces information option");

                do
                {
                    lecture = "";
                    valid = true;

                    Console.Write("\n Choose an action : ");
                    lecture = Console.ReadLine();
                    Console.WriteLine(lecture);
                    if (lecture == "" || !"123".Contains(lecture[0]))
                    {
                        Console.WriteLine("Your choice <" + lecture + "> is not valid = > Please choose a correct action. ");
                        valid = false;
                    }
                } while (!valid);


                switch (lecture[0])
                {
                    case '1':
                        Console.Clear();
                        Console.WriteLine(getInformationSname(B));
                        break;
                    case '2':
                        Console.Clear();
                        Console.WriteLine(getInformationSnumber(B));
                        break;
                    case '3':
                        Console.Clear();
                        Console.WriteLine("You leave this optionnality");
                        Console.ReadKey();
                        end = true;
                        break;
                    default:
                        Console.WriteLine("\n Please choose a correct action.");
                        break;
                }
            } while (!end);
        }
        public string getInformationSnumber(ListSpaces B)
        {
            Console.WriteLine("Enter the number of the space whom you want to have information: ");
            int pos_sp = Convert.ToInt32(Console.ReadLine());
            return ("This is the space n°" + pos_sp + "\n" + B.getSpace(pos_sp).ToString());
        }
        public string getInformationSname(ListSpaces B)
        {
            Console.WriteLine("Enter the name of the space whom you want to have information: ");
            string n = Console.ReadLine();
            int pos = 0;
            while (!(B.onList(n, 40)))
            {
                Console.WriteLine("This propriety is not in the game board !");
                Console.WriteLine("Please write the space name correctly.");
                Console.WriteLine("If you want to leave, press 'z'");
                n = Console.ReadLine();
                if (n == "z")
                {
                    return null;
                }
            }
            pos = B.getSpacePosition(n);
            return ("This is the space n°" + pos + "\n" + B.getSpace(pos).ToString());
        }
        public void getInformationP(ListPlayer P)
        {
            Console.WriteLine("\nSpaces Informations");
            Console.WriteLine("-----------------\n\n");
            bool end = false;
            bool valid = true;
            string lecture = "";
            do
            {
                end = false;
                Console.WriteLine();
                Console.WriteLine("1 : Player informations by name");
                Console.WriteLine("2 : Player informations by number");
                Console.WriteLine("3 : Leave the player information option");

                do
                {
                    lecture = "";
                    valid = true;

                    Console.Write("\n Choose an action : ");
                    lecture = Console.ReadLine();
                    Console.WriteLine(lecture);
                    if (lecture == "" || !"123".Contains(lecture[0]))
                    {
                        Console.WriteLine("Your choice <" + lecture + "> is not valid = > Please choose a correct action. ");
                        valid = false;
                    }
                } while (!valid);


                switch (lecture[0])
                {
                    case '1':
                        Console.Clear();
                        Console.WriteLine(getInformationPname(P));
                        break;
                    case '2':
                        Console.Clear();
                        Console.WriteLine(getInformationPnumber(P));
                        break;
                    case '3':
                        Console.Clear();
                        Console.WriteLine("You leave this optionnality");
                        Console.ReadKey();
                        end = true;
                        break;
                    default:
                        Console.WriteLine("\n Please choose a correct action.");
                        break;
                }
            } while (!end);
        }
        public string getInformationPnumber(ListPlayer P)
        {
            Console.WriteLine("Enter the number of the player whom you want to have information : ");
            int pos_sp = Convert.ToInt32(Console.ReadLine());
            return (P.getPlayer(pos_sp).ToString());
        }
        public string getInformationPname(ListPlayer P)
        {
            Console.WriteLine("Enter the name of the player whom you want to have information: ");
            string n = Console.ReadLine();
            int pos = 0;
            while (!(P.onList(n)))
            {
                Console.WriteLine("This player is not playing in this game !");
                Console.WriteLine("Please write a correct name.");
                Console.WriteLine("If you want to leave, press 'z'");
                n = Console.ReadLine();
                if (n == "z")
                {
                    return null;
                }
            }
            return ("Here is all the informations about this player \n" + P.getPlayerByName(n));
        }
        public void buySpace(ListSpaces B, ListPlayer P)
        {
            if (B.getSpace(position) is Propriety)
            {
                Propriety prop = (Propriety)B.getSpace(position);
                if (prop.possessed)
                {
                    Console.WriteLine("This propriety is already possessed by " + prop.pl.name + ", you can't buy it.");
                }
                else
                {

                    Console.WriteLine("Do you want to buy this propriety ? ");
                    Console.WriteLine("Press 1 for yes or 0 for no. ");
                    int resp = Convert.ToInt32(Console.ReadLine());
                    while (resp != 0 && resp != 1)
                    {
                        Console.WriteLine("Please enter a correct awnser.");
                        Console.WriteLine("Press 1 for yes or 0 for no. ");
                        resp = Convert.ToInt32(Console.ReadLine());
                    }
                    if (resp == 1)
                    {
                        int pri = prop.price;
                        if (account > prop.price)
                        {
                            prop.possessed = true;
                            prop.pl = this;
                            account = account - prop.price;
                            B.changeSpace(position, prop);
                            if (list_pr == null)
                            {
                                prop = (Propriety)B.getSpace(position).Shallowcopy();
                                prop.next = null;
                                list_pr = new ListSpaces(prop);
                            }
                            else
                            {
                                prop = (Propriety)B.getSpace(position).Shallowcopy();
                                prop.next = null;
                                list_pr.AddTail(prop);
                            }
                            Console.WriteLine("You have acquired " + prop.name);
                        }
                        else
                        {
                            Console.WriteLine("You don't have enough money in your bank account to buy this propriety ! ");
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("You can't buy this space, this is not a propriety.");
            }
        }
        public void buyH(ListSpaces B)
        {
            Console.WriteLine("\nBuy House/Hotel");
            Console.WriteLine("-----------------\n\n");
            bool end = false;
            bool valid = true;
            string lecture = "";
            do
            {
                end = false;
                Console.WriteLine();
                Console.WriteLine("1 : Buy a house");
                Console.WriteLine("2 : Buy a hotel");
                Console.WriteLine("3 : Leave the buy house/hotel option");

                do
                {
                    lecture = "";
                    valid = true;

                    Console.Write("\n Choose an action : ");
                    lecture = Console.ReadLine();
                    Console.WriteLine(lecture);
                    if (lecture == "" || !"123".Contains(lecture[0]))
                    {
                        Console.WriteLine("Your choice <" + lecture + "> is not valid = > Please choose a correct action. ");
                        valid = false;
                    }
                } while (!valid);


                switch (lecture[0])
                {
                    case '1':
                        Console.Clear();
                        buyHouse(B);
                        break;
                    case '2':
                        Console.Clear();
                        buyHotel(B);
                        break;
                    case '3':
                        Console.Clear();
                        Console.WriteLine("You leave this optionnality");
                        Console.ReadKey();
                        end = true;
                        break;
                    default:
                        Console.WriteLine("\n Please choose a correct action.");
                        break;
                }
            } while (!end);
        }
        public void buyHouse(ListSpaces B)
        {
            if (list_pr == null)
            {
                Console.WriteLine("You don't owe any propriety, you can't buy a house !");
            }
            else
            {
                Console.WriteLine("Enter the name of the street where you want to buy a house :");
                string prop_name = Console.ReadLine();
                while (!(B.onList(prop_name, 40)))
                {
                    Console.WriteLine("This propriety is not in the game board !");
                    Console.WriteLine("Please write the space name correctly.");
                    Console.WriteLine("If you want to leave, press 'z'");
                    prop_name = Console.ReadLine();
                    if (prop_name == "z")
                    {
                        break;
                    }
                }
                if ((B.onList(prop_name, 40)))
                {
                    while (!(list_pr.onList(prop_name, list_pr.Size)))
                    {
                        Console.WriteLine("You don't possess this propriety !");
                        Console.WriteLine("Your proprieties are " + list_pr);
                        Console.WriteLine("Please write the propriety name correctly.");
                        Console.WriteLine("If you want to leave, press 'z'");
                        prop_name = Console.ReadLine();
                        if (prop_name == "z")
                        {
                            break;
                        }
                        while (!(B.onList(prop_name, 40)))
                        {
                            Console.WriteLine("This propriety is not in the game board !");
                            Console.WriteLine("Please write the space name correctly.");
                            Console.WriteLine("If you want to leave, press 'z'");
                            prop_name = Console.ReadLine();
                            if (prop_name == "z")
                            {
                                break;
                            }
                        }
                    }
                    if ((B.onList(prop_name, 40)) && (list_pr.onList(prop_name, list_pr.Size)))
                    {
                        Propriety test = (Propriety)B.getSpace(B.getSpacePosition(prop_name));
                        if (test is Street)
                        {
                            Street test1 = (Street)B.getSpace(B.getSpacePosition(prop_name));
                            if (test1.house == 4)
                            {
                                Console.WriteLine("This street already have 4 houses, you can't buy another one");
                            }
                            else
                            {
                                if (list_pr.allColors(test1.color))
                                {
                                    Console.WriteLine("Do you want to buy a house in this propriety : " + test1.name + " ?");
                                    Console.WriteLine("The price of a house is " + test1.hprice());
                                    Console.WriteLine("Press 1 for yes 0 for no");
                                    int resp = Convert.ToInt32(Console.ReadLine());
                                    while (resp != 0 && resp != 1)
                                    {
                                        Console.WriteLine("Please enter a correct awnser.");
                                        Console.WriteLine("Press 1 for yes or 0 for no. ");
                                        resp = Convert.ToInt32(Console.ReadLine());
                                    }
                                    if (resp == 1)
                                    {
                                        if (account >= test1.hprice())
                                        {
                                            account = account - test1.hprice();
                                            test1.house++;
                                            B.changeSpace(B.getSpacePosition(prop_name), test1);
                                        }
                                        else
                                        {
                                            Console.WriteLine("You don't have enough money to buy this house !");
                                        }

                                    }

                                }
                                else
                                {
                                    Console.WriteLine("You don't have all the streets of the same color, you can't buy a house.");
                                }
                            }

                        }
                        else
                        {
                            Console.WriteLine("You can't put a house in this propriety, this isn't a street !");
                        }
                    }
                }
            }
        }
        public void buyHotel(ListSpaces B)
        {
            if (list_pr == null)
            {
                Console.WriteLine("You don't owe any propriety, you can't buy a hotel !");
            }
            else
            {
                Console.WriteLine("Enter the name of the street where you want to buy a hotel :");
                string prop_name = Console.ReadLine();
                while (!(B.onList(prop_name, 40)))
                {
                    Console.WriteLine("This propriety is not in the game board !");
                    Console.WriteLine("Please write the space name correctly.");
                    Console.WriteLine("If you want to leave, press 'z'");
                    prop_name = Console.ReadLine();
                    if (prop_name == "z")
                    {
                        break;
                    }
                }
                if ((B.onList(prop_name, 40)))
                {
                    while (!(list_pr.onList(prop_name, list_pr.Size)))
                    {
                        Console.WriteLine("You don't possess this propriety !");
                        Console.WriteLine("Your proprieties are " + list_pr);
                        Console.WriteLine("Please write the propriety name correctly.");
                        Console.WriteLine("If you want to leave, press 'z'");
                        prop_name = Console.ReadLine();
                        if (prop_name == "z")
                        {
                            break;
                        }
                        while (!(B.onList(prop_name, 40)))
                        {
                            Console.WriteLine("This propriety is not in the game board !");
                            Console.WriteLine("Please write the space name correctly.");
                            Console.WriteLine("If you want to leave, press 'z'");
                            prop_name = Console.ReadLine();
                            if (prop_name == "z")
                            {
                                break;
                            }
                        }
                    }
                    if ((B.onList(prop_name, 40)) && (list_pr.onList(prop_name, list_pr.Size)))
                    {
                        Propriety test = (Propriety)B.getSpace(B.getSpacePosition(prop_name));
                        if (test is Street)
                        {
                            Street test1 = (Street)B.getSpace(B.getSpacePosition(prop_name));
                            if (test1.house != 4)
                            {
                                Console.WriteLine("This street already doesn't have 4 houses, you can't buy a hotel !");
                            }
                            else
                            {
                                if (test1.hotel == 1)
                                {
                                    Console.WriteLine("This street already have 1 hotel, you can't buy another one !");
                                }
                                else
                                {
                                    Console.WriteLine("Do you want to buy a hotel in this propriety : " + test1.name + " ?");
                                    Console.WriteLine("The price of a hotel is " + test1.hprice());
                                    Console.WriteLine("Press 1 for yes 0 for no");
                                    int resp = Convert.ToInt32(Console.ReadLine());
                                    while (resp != 0 && resp != 1)
                                    {
                                        Console.WriteLine("Please enter a correct awnser.");
                                        Console.WriteLine("Press 1 for yes or 0 for no. ");
                                        resp = Convert.ToInt32(Console.ReadLine());
                                    }
                                    if (resp == 1)
                                    {
                                        if (account >= test1.hprice())
                                        {
                                            account = account - test1.hprice();
                                            test1.hotel++;
                                            B.changeSpace(B.getSpacePosition(prop_name), test1);
                                        }
                                        else
                                        {
                                            Console.WriteLine("You don't have enough money to buy this hotel !");
                                        }
                                    }
                                }
                            }

                        }
                        else
                        {
                            Console.WriteLine("You can't put a hotel in this propriety, this isn't a street !");
                        }
                    }
                }
            }

        }
        public bool bankruptcy()
        {
            bool bkr = false;
            if (account < 0)
            {
                bkr = true;
            }
            return bkr;
        }
        public int housesOwend()
        {
            int res = 0;
            if (list_pr != null)
            {
                foreach (Propriety P in list_pr)
                {
                    if (P is Street)
                    {
                        Street st = (Street)P;
                        res += st.house;
                    }
                }
            }
            return res;
        }
        public int hotelsOwend()
        {
            int res = 0;
            if (list_pr != null)
            {
                foreach (Propriety P in list_pr)
                {
                    if (P is Street)
                    {
                        Street st = (Street)P;
                        res += st.hotel;
                    }
                }
            }
            return res;
        }
        public override string ToString()
        {
            return ("\n -----------------------------------------------" +
                    "\n Name : " + name +
                    "\n Bank account : " + account +
                    "\n Current position : " + position +
                    "\n List of owned proprieties : " + list_pr +
                    "\n In prison : " + in_prison +
                    "\n Banker : " + banker +
                    "\n -----------------------------------------------");
        }
    }

}
