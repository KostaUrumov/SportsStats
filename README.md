# My_Transfermarkt is a small project for accessing and uploading data for football players(for now) and teams.

Application is orginzed to work with three types of roles: Admin, User and Agent

Main responsibilities for admin is access and edit data for entities Countries, Stadiums and Teams.
Data for Countries and Teams has been seeded by reading Json files, but Administartors can also update it if needed.

Agents are responsible to add and Edit players detail. They are king of owners of the Footballers. Also they are authorized to sign players with different teams or retire them.
Players can sign contract, be released or update any data only by their agents.

Users can not enter or edit any data. They can see what teams and footballers are listed filter them by country and view some details for them.

Unregistered user also can only see home page with random 10 teams display.
