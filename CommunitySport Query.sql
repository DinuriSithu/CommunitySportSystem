CREATE DATABASE CommunitySportsDB;

USE CommunitySportsDB;

CREATE TABLE MEMBER(
    Member_ID INT IDENTITY(1,1) PRIMARY KEY,
    FirstName VARCHAR(50) NOT NULL,
    LastName VARCHAR(50) NOT NULL,
    Email VARCHAR(100) NOT NULL UNIQUE,
    Password VARCHAR(255) NOT NULL,
    Phone VARCHAR(20) NOT NULL,
    Address VARCHAR(200) NOT NULL,
    RegistrationDate DATE NOT NULL DEFAULT GETDATE());


CREATE TABLE SPORT(
    Sport_ID INT IDENTITY(1,1) PRIMARY KEY,
    SportName VARCHAR(50) NOT NULL UNIQUE
);

CREATE TABLE MEMBER_SPORT(
    Member_ID INT NOT NULL,
    Sport_ID INT NOT NULL,

    CONSTRAINT PK_Member_sport PRIMARY KEY (Member_ID, Sport_ID),

    CONSTRAINT FK_Member_sport_Member
        FOREIGN KEY (Member_ID)
        REFERENCES MEMBER(Member_ID),

    CONSTRAINT FK_Member_sport_Sport
        FOREIGN KEY (Sport_ID)
        REFERENCES SPORT(Sport_ID)
);

CREATE TABLE FACILITY_TYPE(
    FacilityTypeID INT IDENTITY(1,1) PRIMARY KEY,
    TypeName VARCHAR(50) NOT NULL UNIQUE
);

CREATE TABLE FACILITY(
    Facility_ID INT IDENTITY(1,1) PRIMARY KEY,
    FacilityName VARCHAR(100) NOT NULL,
    FacilityTypeID INT NOT NULL,
    Location VARCHAR(100) NOT NULL,
    Description VARCHAR(500) NULL,
    Capacity INT NOT NULL,
    HourlyRate DECIMAL(10,2) NOT NULL,
    Status VARCHAR(20) NOT NULL,

    CONSTRAINT FK_Facility_Type FOREIGN KEY (FacilityTypeID)
    REFERENCES FACILITY_TYPE(FacilityTypeID));


CREATE TABLE BOOKING(
    Booking_ID INT IDENTITY(1,1) PRIMARY KEY,
    Member_ID INT NOT NULL,
    Facility_ID INT NOT NULL,
    BookingDate DATE NOT NULL,
    StartTime TIME NOT NULL,
    EndTime TIME NOT NULL,
    BookingStatus VARCHAR(20) NOT NULL,
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),

    CONSTRAINT FK_Booking_Member FOREIGN KEY (Member_ID) REFERENCES MEMBER(Member_ID),

    CONSTRAINT FK_Booking_Facility FOREIGN KEY (Facility_ID) REFERENCES FACILITY(Facility_ID),

    CONSTRAINT CK_Booking_Time CHECK (EndTime > StartTime)
);

CREATE TABLE REVIEW(
    Review_ID INT IDENTITY(1,1) PRIMARY KEY,
    Member_ID INT NOT NULL,
    Facility_ID INT NOT NULL,
    Rating INT NOT NULL,
    Comment VARCHAR(1000) NULL,
    ReviewDate DATE NOT NULL DEFAULT GETDATE(),

    CONSTRAINT FK_Review_Member FOREIGN KEY (Member_ID) REFERENCES MEMBER(Member_ID),

    CONSTRAINT FK_Review_Facility FOREIGN KEY (Facility_ID) REFERENCES FACILITY(Facility_ID),

    CONSTRAINT CK_Review_Rating CHECK (Rating BETWEEN 1 AND 5)
);

CREATE TABLE INQUIRY(
    Inquiry_ID INT IDENTITY(1,1) PRIMARY KEY,
    Member_ID INT NULL,
    Name VARCHAR(100) NOT NULL,
    Email VARCHAR(100) NOT NULL,
    Subject VARCHAR(150) NOT NULL,
    Message VARCHAR(1000) NOT NULL,
    Inq_Date DATE NOT NULL DEFAULT GETDATE(),
    Inq_Status VARCHAR(20) NOT NULL,

    CONSTRAINT FK_Inquiry_Member FOREIGN KEY (Member_ID) REFERENCES MEMBER(Member_ID)
);

SELECT
    TABLE_NAME,
    COLUMN_NAME,
    CONSTRAINT_NAME
FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE
ORDER BY TABLE_NAME;

INSERT INTO MEMBER (FirstName, LastName, Email, Password, Phone, Address) 
VALUES ('John', 'Perera', 'john@gmail.com', 'John123', '0712345678', 'Colombo');

INSERT INTO MEMBER (FirstName, LastName, Email, Password, Phone, Address)
VALUES ('Sarah', 'Fernando', 'sarah@gmail.com', 'Sarah123', '0723456789', 'Kandy');

INSERT INTO MEMBER (FirstName, LastName, Email, Password, Phone, Address)
VALUES ('Nimal', 'Silva', 'nimal@gmail.com', 'Nimal123', '0774567890', 'Galle');

INSERT INTO MEMBER (FirstName, LastName, Email, Password, Phone, Address)
VALUES ('Amaya', 'Perera', 'amaya@gmail.com', 'Amaya123', '0755678901', 'Negombo');

INSERT INTO MEMBER (FirstName, LastName, Email, Password, Phone, Address)
VALUES ('Kasun', 'Fernando', 'kasun@gmail.com', 'Kasun123', '0766789012', 'Colombo');

INSERT INTO SPORT (SportName)
VALUES
('Tennis'),
('Football'),
('Basketball'),
('Badminton'),
('Swimming'),
('Volleyball');

INSERT INTO MEMBER_SPORT (Member_ID, Sport_ID)
VALUES
(1, 1), 
(1, 4), 
(2, 2), 
(2, 5), 
(3, 1), 
(3, 3), 
(4, 4),
(4, 6), 
(5, 2), 
(5, 3); 

INSERT INTO FACILITY_TYPE (TypeName)
VALUES
('Indoor'),
('Outdoor'),
('Swimming Pool'),
('Sports Hall'),
('Tennis Court');

INSERT INTO FACILITY (FacilityName, FacilityTypeID, Location, Description, Capacity, HourlyRate, Status)
VALUES ('Central Tennis Court', 5, 'Colombo', 'Professional outdoor tennis court', 4, 2500.00, 'Available');

INSERT INTO FACILITY (FacilityName, FacilityTypeID, Location, Description, Capacity, HourlyRate, Status)
VALUES ('Community Football Ground', 2, 'Colombo', 'Large outdoor football ground', 22, 5000.00, 'Available');

INSERT INTO FACILITY (FacilityName, FacilityTypeID, Location, Description, Capacity, HourlyRate, Status)
VALUES ('Indoor Basketball Court', 1, 'Kandy', 'Indoor basketball court with seating', 20, 3500.00, 'Available');

INSERT INTO FACILITY (FacilityName, FacilityTypeID, Location, Description, Capacity, HourlyRate, Status)
VALUES ('Community Swimming Pool', 3, 'Colombo', 'Community swimming pool', 30, 3000.00, 'Available');

INSERT INTO FACILITY (FacilityName, FacilityTypeID, Location, Description, Capacity, HourlyRate, Status)
VALUES ('Badminton Sports Hall', 4, 'Galle', 'Indoor badminton facility', 12, 2000.00, 'Available');

INSERT INTO FACILITY (FacilityName, FacilityTypeID, Location, Description, Capacity, HourlyRate, Status)
VALUES ('Outdoor Volleyball Court', 2, 'Negombo', 'Outdoor volleyball court', 14, 1800.00, 'Available');


INSERT INTO BOOKING (Member_ID, Facility_ID, BookingDate, StartTime, EndTime, BookingStatus)
VALUES (1, 1, '2026-09-10', '09:00', '10:00', 'Confirmed');

INSERT INTO BOOKING (Member_ID, Facility_ID, BookingDate, StartTime, EndTime, BookingStatus)
VALUES (2, 2, '2026-09-11', '15:00', '17:00', 'Confirmed');

INSERT INTO BOOKING (Member_ID, Facility_ID, BookingDate, StartTime, EndTime, BookingStatus)
VALUES (3, 3, '2026-09-12', '10:00', '11:00', 'Not Confirmed');

INSERT INTO BOOKING (Member_ID, Facility_ID, BookingDate, StartTime, EndTime, BookingStatus)
VALUES (4, 5, '2026-09-13', '14:00', '15:00', 'Confirmed');

INSERT INTO BOOKING (Member_ID, Facility_ID, BookingDate, StartTime, EndTime, BookingStatus)
VALUES (5, 6, '2026-09-14', '16:00', '17:00', 'Not Confirmed');


INSERT INTO REVIEW (Member_ID, Facility_ID, Rating, Comment)
VALUES (1, 1, 5, 'The tennis court was in excellent condition.');

INSERT INTO REVIEW (Member_ID, Facility_ID, Rating, Comment)
VALUES (2, 2, 4, 'Good football ground with plenty of space.');

INSERT INTO REVIEW (Member_ID, Facility_ID, Rating, Comment)
VALUES (3, 3, 5, 'Very clean basketball court and good facilities.');

INSERT INTO REVIEW (Member_ID, Facility_ID, Rating, Comment)
VALUES (4, 5, 4, 'Good badminton facility.');

INSERT INTO REVIEW (Member_ID, Facility_ID, Rating, Comment)
VALUES (5, 6, 5, 'Excellent volleyball court.');


INSERT INTO INQUIRY (Member_ID, Name, Email, Subject, Message, Inq_Status)
VALUES (1, 'John Perera', 'john@gmail.com', 'Booking Question', 'Can I change my booking time?', 'Pending');


INSERT INTO INQUIRY (Member_ID, Name, Email, Subject, Message, Inq_Status)
VALUES (NULL, 'David Smith', 'david@gmail.com', 'Facility Information',
'I would like more information about the sports facilities.', 'Pending');


SELECT * FROM MEMBER;

SELECT * FROM SPORT;

SELECT * FROM FACILITY;

SELECT * FROM BOOKING;

SELECT * FROM REVIEW;

SELECT * FROM INQUIRY;

SELECT
    B.Booking_ID,
    M.FirstName + ' ' + M.LastName AS MemberName,
    F.FacilityName,
    B.BookingDate,
    B.StartTime,
    B.EndTime,
    B.BookingStatus
FROM BOOKING B
INNER JOIN MEMBER M
    ON B.Member_ID = M.Member_ID
INNER JOIN FACILITY F
    ON B.Facility_ID = F.Facility_ID;


SELECT
    F.Facility_ID,
    F.FacilityName,
    FT.TypeName,
    F.Location,
    F.Capacity,
    F.HourlyRate,
    F.Status
FROM FACILITY F
INNER JOIN FACILITY_TYPE FT
    ON F.FacilityTypeID = FT.FacilityTypeID;


SELECT
    B.Booking_ID,
    M.FirstName + ' ' + M.LastName AS MemberName,
    F.FacilityName,
    B.BookingDate,
    B.StartTime,
    B.EndTime,
    B.BookingStatus
FROM BOOKING B
INNER JOIN MEMBER M
    ON B.Member_ID = M.Member_ID
INNER JOIN FACILITY F
    ON B.Facility_ID = F.Facility_ID;


SELECT *
FROM BOOKING
WHERE Facility_ID = 1
  AND BookingDate = '2026-09-10'
  AND StartTime < '12:00'
  AND EndTime > '11:00';


SELECT
    M.FirstName + ' ' + M.LastName AS MemberName,
    S.SportName
FROM MEMBER_SPORT MS
INNER JOIN MEMBER M
    ON MS.Member_ID = M.Member_ID
INNER JOIN SPORT S
    ON MS.Sport_ID = S.Sport_ID
ORDER BY M.Member_ID;


SELECT 
    COLUMN_NAME,
    DATA_TYPE,
    IS_NULLABLE
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'INQUIRY'
AND COLUMN_NAME = 'Member_ID';

SELECT 
    Inquiry_ID,
    Member_ID,
    Name,
    Email,
    Subject,
    Inq_Date,
    Inq_Status
FROM INQUIRY
ORDER BY Inquiry_ID DESC;

UPDATE FACILITY
SET HourlyRate = 2500.00
WHERE Facility_ID = 2;

UPDATE BOOKING
SET BookingStatus = 'Cancelled'
WHERE Booking_ID = 1;

UPDATE FACILITY
SET Status = 'Unavailable'
WHERE Facility_ID = 1;

DELETE FROM REVIEW
WHERE Review_ID = 5;

DELETE FROM INQUIRY
WHERE Inquiry_ID = 2;

DELETE FROM BOOKING
WHERE Booking_ID = 5;

DELETE FROM MEMBER_SPORT
WHERE Member_ID = 1
AND Sport_ID = 2;