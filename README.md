# My_Transfermarkt is a small project for accessing and uploading data for football players(for now), teams and tournaments.

Application is orginzed to work with three types of roles: Admin, User and Agent.

Main responsibilities for admin is access and edit data for entities Countries, Stadiums and Teams.

Agents are responsible to add and Edit players detail. They are king of owners of the Footballers. Also they are authorized to sign players with different teams or retire them.
Players can sign contract, be released or update any data only by their agents.

Users can not enter or edit any data. They can see what teams and footballers are listed filter them by country and view some details for them.

Unregistered user also can see home page with random 8 teams display, also can search by name for footballers, teams, country or stadiums. Of course they have access to register and log in pages

Tournaments can be filled with different teams.

Unregistered User:
- Can see home page.
- Can Register or Login.

- Can Search any entities by name(accept tournament for now).

Registered User(not Agent):
- See all teams as home page. There is a button where can filter teams by country.
- Info for all (stadiums, tournaments, players, retired players only and search option) is available.
- Players assigned for specific team and see their details.
- Teams in Specific tournament
- Can logout and change password.

Registered User(Agent):
- See his footballers as home page.
- Can add pucture, edit footballer data, release from a club or retire
- Can see All teams and all footballers. If a footballer is without an agent can be added to the logged agent list. 
- Can logout and change password.

Admin:
- Is redirected to area home page where can access different type of data. 
- Admin is responsible to add and remove teams to specific tournament. Also can edit footballer data if neede but cant assign them to a club.
- All the pages outside admin area should be unreachable.
- Can logout and change password.
