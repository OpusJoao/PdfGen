# Singulare

## Requirements
To run the services you need:
 * Docker and docker-compose
	 * **Needed host.docker.internal inside your hosts file.**
 * Sql server management studio

## Before run
1. Before run the services you need run only the database and create the db to import the backup data.
	To run only the database run:
	```bash
		docker-compose up -d sql
	```
2. After the database is up, use the **Sql server management studio** to connect with the database:
	* username = sa
	* password = A&VeryComplex123Password
	* host = localhost

3. Restore the database using the file quote_db.bak to import all the data necessary

## How to run
To run the command bellow:
```bash
	docker-compose up -d
```

## How to use
You need to access the url [Singulare app - localhost](http://localhost:8080/swagger/index.html)

With the swagger you have all the routes to work with.

## How it works
When you send a request to processReport route, the singulare project will create a data to generate the report and will send this data to EngineReport using rabbitmq. When the EngineReport consume that data on queue, the service will get all the data from database and create an pdf using ironpdf. After done the process, will update with the path of report pdf. 
To see the progress just search in swagger processReport by id, were this route will return all about the process.  
