# Sensor Simuator Console Application

This repository contains the working code for a console application that simulates temperature readings of a sensor. 

## Set up
This application runs using SQLite. If there are errors with the database, please follow the steps below to ensure SQLite is installed correctly.
1. Open the terminal using ctrl + `
2. Change directory to SensorSim `cd SensorSim`
3. Enter `dotnet add package Microsoft.Data.Sqlite`


### How to run
The application runs on a number system on start up. Choosing an option will invoke a method. The options are:
1. Start Sensor - This will start the sensor and produce readings. Readings will be logged on screen and into a database. The 'Q' key can be pressed at any time to stop the simulation. Data will be saved
2. View History - This will display all of the sensors past readings
3. View Average Temperatures - This will display the smoothed average from the last 5 readings, the recent average from the last 15 readings and the average of all readings in the database
4. Reset Sensor - This will remove all data stored about the sensor. You will be asked to confirm the reset

### Reconfiguration
To reconfigure the sensor, open the **SensorConfig.json** file in the **Configuration folder**.
Here you can change the Id, Name, Location, Minimum Temperature and Maximum Temperature.
The application will check if all attributes are valid.

### Project structure
<img width="299" height="280" alt="image" src="https://github.com/user-attachments/assets/63124f9f-d39c-403e-8afb-d3c21025bb46" />
