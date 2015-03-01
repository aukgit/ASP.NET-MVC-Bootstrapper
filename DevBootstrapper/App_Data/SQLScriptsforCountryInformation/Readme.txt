Developed By 
Developers Organism
Lead Programmer 'Alim Ul Karim'
Google for 'Alim Ul Karim'
alim@developers-organism.com


Informal Instructions
First run execute 
Build
ReStore nugut
execute this:
Update-Package -Reinstall  

then 
Rename the database inside the webconfig
run updates on nuget console:

Update-Database -Force -ConfigurationTypeName DevBootstrapper.Models.Migrations.DevIdentity.Configuration
Update-Database -Force -ConfigurationTypeName DevBootstrapper.Models.Migrations.Indentity.Configuration


Then Copy the sql from scripts folder or 7zip then run it inside the accounts database with SQLExpressTools
Before running the sql please rename the database name as your webconfig.

After that we are good to go.

Create Stored Procedures from given SQL in  the directory