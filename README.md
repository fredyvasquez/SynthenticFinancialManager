# SynthenticFinancialManager

1. Open the script "Database.sql" in MS SQL server 2014 that is present in the database folder, adjust te path for the data and log files
2. Proceed to execute the script
3. Once the database has been created,Copy the "SynthenticFinancialManager" to c:\inetpub\wwwroot
4. Open the web.config file, and adjust the connection string settings as were set in the database
5. Open IIS manager and search for the folder copied in the previous step
6. Convert it to an Application
7. In a web browser open the next URL http://localhost/SynthenticFinancialManager/Account/SetupRoles. in order to setup the roles and users. Four roles are created: Assistant, Manager, Superintendent, Administrator. One user is created for each role: AssistantUser, ManagerUser, SuperintendentUser, AdministratorUser. For test purposes the password of all users has been set to "123456" 
8. Download the dataset https://www.kaggle.com/ntnu-testimon/paysim1 posted on Kaggle or use the file "PS_log.csv" with 3000 test transactions located in the "SynthenticFinancialManagerETL" folder
9. Goto the "SynthenticFinancialManagerETL" folder and search in bin/release and open and configure the file "SynthenticFinancialManagerETL.exe.config" with the same connection string used for Web Application. 
10. Open a command window and go to the path of the exe file "SynthenticFinancialManagerETL.exe" that performs the ETL in the database
11. Execute the exe file with one argument, that is the path to the csv file, pe: "SynthenticFinancialManagerETL.exe PS_log.csv"
12. Open the website http://localhost/SynthenticFinancialManager/
13. Login with the different users: AssistantUser, ManagerUser, SuperintendentUser, AdministratorUser (password: 123456)
