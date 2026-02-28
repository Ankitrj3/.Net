-- =============================================
-- Flight Search Engine - Database Setup Script
-- Author: Thiluck
-- Create Date: 2026
-- Description: Complete database creation script
-- =============================================

CREATE DATABASE FlightSearchDB;
GO
USE FlightSearchDB;

CREATE TABLE Flights(
    FlightId INT PRIMARY KEY IDENTITY(1,1),
    FlightName NVARCHAR(100) NOT NULL,
    FlightType NVARCHAR(50) NOT NULL,
    Source NVARCHAR(100) NOT NULL,
    Destination NVARCHAR(100) NOT NULL,
    PricePerSeat DECIMAL(18,2) NOT NULL
);

CREATE TABLE Hotels(
    HotelId INT PRIMARY KEY IDENTITY(1,1),
    HotelName NVARCHAR(100) NOT NULL,
    HotelType NVARCHAR(50) NOT NULL,
    Location NVARCHAR(100) NOT NULL,
    PricePerDay DECIMAL(18,2) NOT NULL
);


INSERT INTO Flights VALUES
('Indigo 101','Domestic','Hyderabad','Delhi',5000),
('Air India 201','Domestic','Hyderabad','Mumbai',4000),
('Emirates 301','International','Delhi','Dubai',25000);

INSERT INTO Hotels VALUES
('Taj Delhi','5 Star','Delhi',8000),
('Trident Mumbai','4 Star','Mumbai',6000),
('Dubai Grand','5 Star','Dubai',12000);


-- =============================================
-- Author:		<Author,,Thiluck>
-- Create date: <Create Date,29-02-2026,>
-- Description:	<Description,Stored Procedures,>
-- =============================================
CREATE PROCEDURE sp_GetSources
AS
BEGIN
    SELECT DISTINCT Source FROM Flights;
END


CREATE PROCEDURE sp_GetDestinations
AS
BEGIN
    SELECT DISTINCT Destination FROM Flights;
END


CREATE PROCEDURE sp_SearchFlights
    @Source NVARCHAR(100),
    @Destination NVARCHAR(100),
    @Persons INT
AS
BEGIN
    SELECT 
        FlightId,
        FlightName,
        FlightType,
        Source,
        Destination,
        PricePerSeat * @Persons AS TotalCost
    FROM Flights
    WHERE Source = @Source AND Destination = @Destination;
END

CREATE PROCEDURE sp_SearchFlightsWithHotels
    @Source NVARCHAR(100),
    @Destination NVARCHAR(100),
    @Persons INT
AS
BEGIN
    SELECT 
        f.FlightId,
        f.FlightName,
        f.Source,
        f.Destination,
        h.HotelName,
        (f.PricePerSeat * @Persons) + h.PricePerDay AS TotalCost
    FROM Flights f
    INNER JOIN Hotels h ON f.Destination = h.Location
    WHERE f.Source = @Source AND f.Destination = @Destination;
END




ALTER PROCEDURE sp_SearchFlightsWithHotels
    @Source NVARCHAR(100),
    @Destination NVARCHAR(100),
    @Persons INT
AS
BEGIN
    SELECT 
        f.FlightId,
        f.FlightName,
        f.Source,
        f.Destination,
        h.HotelName AS HotelName,
        (f.PricePerSeat * @Persons) + h.PricePerDay AS TotalCost
    FROM Flights f
    INNER JOIN Hotels h 
        ON f.Destination = h.Location
    WHERE 
        LTRIM(RTRIM(f.Source)) = LTRIM(RTRIM(@Source))
        AND 
        LTRIM(RTRIM(f.Destination)) = LTRIM(RTRIM(@Destination));
END
