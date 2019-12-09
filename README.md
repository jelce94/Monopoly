# Monopoly
# Design Pattern & Software Development

The purpose of our project is to modelize a monopoly's game in C#.
In order to do that, we implemented a board by a  circular linked list of a specific class named spaces (Propriety, special spaces). These classes inherit from the abstract class 'spaces'. We separated into these two classes because propriety can be bought by a player whereas special spaces can't be bought by a player.
We create several classes that inherit the class propriety : station, company, street. 

For the special spaces, we use a boolean attribut to identify the type of special spaces as Chance, start, Luxe Tax... when we create an instance for each special space.

We judged more useful to use a boolean parameter for each type of special spaces than the factory's pattern because it is more easier to instanciate the spaces with the name of the special cases directly. The boolean parameter associated to the special space name will be changed to true whereas the factory's pattern impose us to create these several special space class, and we don't have any methods and attributs to put in. That's why, it is not very relevant. 

When we instancied the spaces and add them to the linked list, we created the original board of the French Monopoly. All the game instructions will be in english but the name of the streets will correspond to Paris's streets.

After creating the board game, we had to create the players who will play the game. We ask to the user how many players will play and to introduce their names. We also ask to the user the player who will have the banker role. This is an unique role, this player will be the one who input all the informations. We use a singleton desing pattern to make sure that there can only be one banker per game. As for the spaces, we created a circular single linked list for players, which will allow us to pass to the next player easily. We use an iterator design pattern to simplify this structure.
To create a player we only need his name, all the others parameters will be filled automatically. All players will start at position 0, with no possessed proprieties and with 1500 euros. We created them like the real Monopoly game.

The last step of creating a game, is to implement 2 queues who will serve as decks for chance and community chance cards. These queues will be filled randomly, so in to differents games the decks will be different.

Now that we have all the preriquisites we can use them together to start a game correctly. The first player will roll dices by calling an random method. Then he will advance to the position determinded with the dices. After that this player will have an Action menu where he can buy a propriety, buy a house or a hotel, get informations of players and spaces or simply pass his turn. We respect the Monopoly rules, so a space can only be purchased if the player is in the correct position, if he have enough money, if the space is a propriety and if the propriety is unowned. If he decide to not buy a unowned propriety, the propriety will be auctionned and all players who are not in prison can participate to the auction. 

When the player's turn finished, the next player will be able to play his turn. At the end of each player's turn, we will check if his bank account is positive. If it is not, the player will be declared in bankruptcy and he will be removed of the player linked list. When there is only a single player in the linked list, the game is over and the player is declared the winner. 

