CREATE DATABASE IF NOT EXISTS DevsuDB;
USE DevsuDB;
CREATE TABLE Persons (
    PersonID INT NOT NULL AUTO_INCREMENT UNIQUE,
    NAME VARCHAR (255) NOT NULL,
    Gender VARCHAR (255),
    Age INT,
    Identification VARCHAR (255),
    Address VARCHAR (255),
    Phone VARCHAR (255),
    PRIMARY KEY (PersonID)
);
CREATE TABLE Clients (
    ClientID INT NOT NULL AUTO_INCREMENT UNIQUE,
    PASSWORD VARCHAR (255) NOT NULL,
    State TINYINT (1) DEFAULT 1,
    PersonID INT UNIQUE,
    PRIMARY KEY (ClientID),
    FOREIGN KEY (PersonID) REFERENCES Persons (PersonID)
);
CREATE TABLE Accounts (
    AccountID INT NOT NULL AUTO_INCREMENT UNIQUE,
    Number INT NOT NULL,
    Type VARCHAR(255) NOT NULL,
    Balance BIGINT DEFAULT 0,
    State TINYINT (1) DEFAULT 1,
    ClientID int,
    PRIMARY KEY (AccountID),
    FOREIGN KEY (ClientID) REFERENCES Clients(ClientID)
);
CREATE TABLE Movements(
    MovementID INT NOT NULL AUTO_INCREMENT UNIQUE,
    Date DATETIME NOT NULL,
    Type varchar(255),
    Amount BIGINT NOT NULL,
    Balance BIGINT NOT NULL,
    Description varchar(255),
    AccountID int,
    PRIMARY KEY (MovementID),
    FOREIGN KEY (AccountID) REFERENCES Accounts(AccountID)
);