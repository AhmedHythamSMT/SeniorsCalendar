An Windows Program using C# by Visual Studio and Xampp"Local Host". The calendar has registertion and login methods.
It helps seniors to save their events, its description and date.
Also, It retrieves The saved events recently.

- Note: The program supports Both languages: - English Language (By default as windows language)  - Arabic Language

I'm using Local database "MySQL Database".
Database Informations:
database name: db_calendar
has 2 tables:
1-tbl_calendar
2-tbl_users

1-tbl_calendar structure:
id(primary key) INT, AUTO INCERMENT
name, TEXT
description, TEXT
date, TEXT

2-tbl_users structure:
id(primary key) INT, AUTO INCERMENT
username, TEXT
email, TEXT
password, TEXT
