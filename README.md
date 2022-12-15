# Ship Battle

## MVP Design Doc / Vision

Ship battle will be a simple, local multi player ship fighting game. Two players will start from bases on each side 
of the map and compete to aquire gold from the map and return it to their base. After a player collectes a determined
large amount of gold, a countdown will start, and if that player's gold is not reduced below the threshhold, that 
player wins the game.

Players will be able to move their ships, either with a keyboard or a gamepad, through the water on the game
map. The players can fire cannon balls from either the right or left side of their ships.

Gold can be quired by the players from the middle of the map. The gold will be on land, so the player's ships
will not be able to get the gold directly. Rather, as an action, the players can disembark a small crewed boat
with crewmen that go can asore to retrieve the gold. The crewmen are non-combatents and will not fight each other.
However, the crewmen will be vulnerable to enemy ships as well as the fortifications protecting the gold.

After retrieving all the gold they can carry, the crewmen will attempt to row their boats back to the player ships.
After re-boarding the player's ship, the crewmen will have to be delivered back to the player's base where they will
disembark and stash the gold. Each player can only have one crew in play at a time.

The players can use their crew to steal gold from the other player instead of the central location on the map, but 
will make themselves vulnerable to the other player's base defenses.

Each player can use gold to build/rebuild base defenses, build AI controlled attack and defense ships and upgrade their
own ship.

Each player will have to avoid AI controlled pirate ships that will occasionally spawn and attempt to steal gold.

If a player dies, they have to wait 5 seconds and re-spawn.

### Goals
**February 2023** Playable alpha

**March 2023** Playable beta