CREATE DATABASE modulesdb use modulesdb; --this creates the database and than ussed it for the next queries

CREATE TABLE modules ( --this is the table for the users modules
    id INT NOT NULL PRIMARY KEY IDENTITY,
    code VARCHAR (100) NOT NULL,
	name VARCHAR (100) NOT NULL,
    credits int NOT NULL,
    weeks int NULL,
    hours_per_week int NULL,
    start_date VARCHAR(100) NOT NULL,
	current_hours int NOT NULL,
	Selfstudy_hours AS credits*10/ weeks - hours_per_week, --this calculates the self study hours that a student needs to do for a module 
	Self_study_hours_left AS (credits*10/ weeks - hours_per_week) - current_hours --this substracts what self studies the user has already done
);

CREATE TABLE users ( --table for users to save when they rigster and log in agian
	name VARCHAR (100) NOT NULL,
	password VARCHAR (100) NOT NULL 
);

CREATE TABLE setaday ( --table for the day they set to work on a module
    id INT NOT NULL PRIMARY KEY IDENTITY,
	day VARCHAR (100) NOT NULL,
	mcode VARCHAR (100) NOT NULL,
	study_hours int NOT NULL,
	created_on DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP --this gives the user the time they entered the data
);

--the following is used to insert data into the tables 
INSERT INTO modules (code, name, credits, weeks, hours_per_week, start_date, current_hours)
VALUES
('PROG6212', 'Programming 2B', '15', '8', '5', '2 September 2022', '5');

INSERT INTO modules (code, name, credits, weeks, hours_per_week, start_date, current_hours)
VALUES
('PROG6212', 'Programming 2a', '15', '8', '5', '2 September 2022', '5');

INSERT INTO modules (code, name, credits, weeks, hours_per_week, start_date, current_hours)
VALUES
('ADDB7311', 'Advanced database', '10', '6', '3', '7 September 2022', '5');

INSERT INTO users (name, password)
VALUES
('Mike', '5248');

INSERT INTO setaday (day, mcode, study_hours)
VALUES
('Wednesday', 'PROG6212', '2');
INSERT INTO setaday (day, mcode, study_hours)
VALUES
('Friday', 'ADDB7311', '1');

--Select stamements used to make sure the data works inside the tables 
SELECT * FROM modules;

SELECT * FROM users;

SELECT * FROM setaday;