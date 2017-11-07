# SynthenticFinancialManager

Open the script "Database.sql" in MS SQL server 2014 that is present in the database folder
Adjust te path for the data and log files
Proceed to execute the script
Once the database has been created,Copy the "SynthenticFinancialManager" to c:\inetpub\wwwroot
Open the web.config file, and adjust the connection string settings as were set in the database
Open IIS manager and search for the folder copied in the previous step
Convert it to an Application
in a web browser open the next URL http://localhost/SynthenticFinancialManager/Account/SetupRoles. in order to setup the roles and users. Four roles are created: Assistant, Manager, Superintendent, Administrator. One user is created for each role: AssistantUser, ManagerUser, SuperintendentUser, AdministratorUser. Todos los ususarios con el password "123456" Download the dataset https://www.kaggle.com/ntnu-testimon/paysim1 posted on Kaggle.
Goto the "SynthenticFinancialManagerETL" folder and search in bin/debug and open and configure the file "SynthenticFinancialManagerETL.exe.config" with the same connection string for the Web Application. 9.Open a command window and go to the path of the exe file "SynthenticFinancialManagerETL.exe" that performs the ETL in the database 10.Execute the exe file with one argument, that is the path to the csv file, pe: "SynthenticFinancialManagerETL.exe c:\path\file.csv"
Open the website http://localhost/SynthenticFinancialManager/
