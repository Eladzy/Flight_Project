USE [master]
GO
/****** Object:  Database [FlightCom]    Script Date: 2/11/2021 7:49:26 PM ******/
CREATE DATABASE [FlightCom]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'FlightCom', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\FlightCom.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'FlightCom_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\FlightCom_log.ldf' , SIZE = 466944KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [FlightCom] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [FlightCom].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [FlightCom] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [FlightCom] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [FlightCom] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [FlightCom] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [FlightCom] SET ARITHABORT OFF 
GO
ALTER DATABASE [FlightCom] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [FlightCom] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [FlightCom] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [FlightCom] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [FlightCom] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [FlightCom] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [FlightCom] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [FlightCom] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [FlightCom] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [FlightCom] SET  DISABLE_BROKER 
GO
ALTER DATABASE [FlightCom] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [FlightCom] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [FlightCom] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [FlightCom] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [FlightCom] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [FlightCom] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [FlightCom] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [FlightCom] SET RECOVERY FULL 
GO
ALTER DATABASE [FlightCom] SET  MULTI_USER 
GO
ALTER DATABASE [FlightCom] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [FlightCom] SET DB_CHAINING OFF 
GO
ALTER DATABASE [FlightCom] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [FlightCom] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [FlightCom] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'FlightCom', N'ON'
GO
ALTER DATABASE [FlightCom] SET QUERY_STORE = OFF
GO
USE [FlightCom]
GO
/****** Object:  Table [dbo].[ADMIN]    Script Date: 2/11/2021 7:49:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ADMIN](
	[ADMIN_USER_NAME] [nvarchar](50) NULL,
	[ADMIN_PASSWORD] [nvarchar](50) NULL,
	[ADMIN_ID] [bigint] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_ADMIN] PRIMARY KEY CLUSTERED 
(
	[ADMIN_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AIRLINE]    Script Date: 2/11/2021 7:49:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AIRLINE](
	[AL_ID] [bigint] IDENTITY(1,1) NOT NULL,
	[AL_NAME] [nvarchar](50) NULL,
	[AL_USERNAME] [nvarchar](50) NULL,
	[AL_PASSWORD] [nvarchar](50) NULL,
	[AL_COUNTRYCODE] [bigint] NOT NULL,
 CONSTRAINT [PK_AIRLINE] PRIMARY KEY CLUSTERED 
(
	[AL_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[COUNTRIES]    Script Date: 2/11/2021 7:49:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[COUNTRIES](
	[COU_ID] [bigint] NOT NULL,
	[COU_COUNTRY_NAME] [nvarchar](50) NULL,
 CONSTRAINT [PK_COUNTRIES] PRIMARY KEY CLUSTERED 
(
	[COU_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CUSTOMERS]    Script Date: 2/11/2021 7:49:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CUSTOMERS](
	[C_ID] [bigint] IDENTITY(1,1) NOT NULL,
	[C_FIRST_NAME] [nvarchar](50) NOT NULL,
	[C_LAST_NAME] [nvarchar](50) NOT NULL,
	[C_USER_NAME] [nvarchar](50) NOT NULL,
	[C_PASSWORD] [nvarchar](50) NOT NULL,
	[C_ADDRESS] [nvarchar](50) NOT NULL,
	[C_PHONE_NUMBER] [nvarchar](50) NOT NULL,
	[CREDIT_CARD_NUMBER] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_CUSTOMERS] PRIMARY KEY CLUSTERED 
(
	[C_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FLIGHT_HISTORY]    Script Date: 2/11/2021 7:49:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FLIGHT_HISTORY](
	[H_FLIGHT_ID] [bigint] NOT NULL,
	[H_FLIGHT_AIRLINE_ID] [bigint] NULL,
	[H_FLIGHT_ORIGIN_COUNTRY] [bigint] NULL,
	[H_FLIGHT_DESTINATION_COUNTRY] [bigint] NULL,
	[H__FLIGHT_DEPARTURE_TIME] [datetime] NULL,
	[H_FLIGHT_LAND_TIME] [datetime] NULL,
	[H_FLIGHT_TICKETS_LEFT] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FLIGHTS]    Script Date: 2/11/2021 7:49:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FLIGHTS](
	[F_ID] [bigint] IDENTITY(1,1) NOT NULL,
	[F_AIRLINE_ID] [bigint] NULL,
	[F_ORIGIN_COUNTRYCODE] [bigint] NULL,
	[F_DESTINATION_COUNTRYCODE] [bigint] NULL,
	[F_DEPARTURE_TIME] [datetime] NULL,
	[F_LANDING_TIME] [datetime] NULL,
	[F_REMAINING_TICKETS] [int] NULL,
 CONSTRAINT [PK_FLIGHTS] PRIMARY KEY CLUSTERED 
(
	[F_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TICKET_HISTORY]    Script Date: 2/11/2021 7:49:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TICKET_HISTORY](
	[H_TICKET_ID] [bigint] NOT NULL,
	[H_TICKET_FLIGHT_ID] [bigint] NOT NULL,
	[H_TICKET_CUSTOMER_ID] [bigint] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TICKETS]    Script Date: 2/11/2021 7:49:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TICKETS](
	[T_ID] [bigint] IDENTITY(1,1) NOT NULL,
	[T_FLIGHT_ID] [bigint] NOT NULL,
	[T_CUSTOMER_ID] [bigint] NOT NULL,
 CONSTRAINT [PK_TICKETS] PRIMARY KEY CLUSTERED 
(
	[T_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AIRLINE]  WITH CHECK ADD  CONSTRAINT [FK_AIRLINE_COUNTRIES] FOREIGN KEY([AL_COUNTRYCODE])
REFERENCES [dbo].[COUNTRIES] ([COU_ID])
GO
ALTER TABLE [dbo].[AIRLINE] CHECK CONSTRAINT [FK_AIRLINE_COUNTRIES]
GO
ALTER TABLE [dbo].[FLIGHTS]  WITH CHECK ADD  CONSTRAINT [FK_FLIGHTS_AIRLINE] FOREIGN KEY([F_AIRLINE_ID])
REFERENCES [dbo].[AIRLINE] ([AL_ID])
GO
ALTER TABLE [dbo].[FLIGHTS] CHECK CONSTRAINT [FK_FLIGHTS_AIRLINE]
GO
ALTER TABLE [dbo].[FLIGHTS]  WITH CHECK ADD  CONSTRAINT [FK_FLIGHTS_COUNTRIES] FOREIGN KEY([F_ORIGIN_COUNTRYCODE])
REFERENCES [dbo].[COUNTRIES] ([COU_ID])
GO
ALTER TABLE [dbo].[FLIGHTS] CHECK CONSTRAINT [FK_FLIGHTS_COUNTRIES]
GO
ALTER TABLE [dbo].[FLIGHTS]  WITH CHECK ADD  CONSTRAINT [FK_FLIGHTS_COUNTRIES1] FOREIGN KEY([F_DESTINATION_COUNTRYCODE])
REFERENCES [dbo].[COUNTRIES] ([COU_ID])
GO
ALTER TABLE [dbo].[FLIGHTS] CHECK CONSTRAINT [FK_FLIGHTS_COUNTRIES1]
GO
ALTER TABLE [dbo].[TICKETS]  WITH CHECK ADD  CONSTRAINT [FK_TICKETS_CUSTOMERS] FOREIGN KEY([T_CUSTOMER_ID])
REFERENCES [dbo].[CUSTOMERS] ([C_ID])
GO
ALTER TABLE [dbo].[TICKETS] CHECK CONSTRAINT [FK_TICKETS_CUSTOMERS]
GO
ALTER TABLE [dbo].[TICKETS]  WITH CHECK ADD  CONSTRAINT [FK_TICKETS_FLIGHTS] FOREIGN KEY([T_FLIGHT_ID])
REFERENCES [dbo].[FLIGHTS] ([F_ID])
GO
ALTER TABLE [dbo].[TICKETS] CHECK CONSTRAINT [FK_TICKETS_FLIGHTS]
GO
/****** Object:  StoredProcedure [dbo].[ADD_AIRLINE]    Script Date: 2/11/2021 7:49:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  procedure [dbo].[ADD_AIRLINE]
@name nvarchar(50),
@userName nvarchar(50),
@password nvarchar(50),
@countryCode bigint
as
insert into AIRLINE(AL_NAME,AL_USERNAME,AL_PASSWORD,AL_COUNTRYCODE) values(@name,@userName,@password,@countryCode);
GO
/****** Object:  StoredProcedure [dbo].[ADD_COUNTRY]    Script Date: 2/11/2021 7:49:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[ADD_COUNTRY]
@id bigint,
@name nvarchar(50)
as
insert into COUNTRIES(COU_ID,COU_COUNTRY_NAME) values(@id,@name);
GO
/****** Object:  StoredProcedure [dbo].[ADD_CUSTOMER]    Script Date: 2/11/2021 7:49:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[ADD_CUSTOMER]
@firstName nvarchar(50),
@lastName nvarchar(50),
@userName nvarchar(50),
@password nvarchar(50),
@address nvarchar(50),
@phoneNumber nvarchar(50),
@creditCard nvarchar(50)

as
insert into CUSTOMERS(C_FIRST_NAME,C_LAST_NAME,C_USER_NAME,C_PASSWORD,C_ADDRESS,C_PHONE_NUMBER,CREDIT_CARD_NUMBER)
values(@firstName,@lastName,@userName,@password,@address,@phoneNumber,@creditCard);
GO
/****** Object:  StoredProcedure [dbo].[ADD_FLIGHT]    Script Date: 2/11/2021 7:49:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[ADD_FLIGHT]
/*@id bigint,*/
@airline bigint,
@origin bigint,
@destination bigint,
@departure datetime,
@landing datetime,
@remaining int
as
insert into FLIGHTS(F_AIRLINE_ID,F_ORIGIN_COUNTRYCODE,F_DESTINATION_COUNTRYCODE,F_DEPARTURE_TIME,F_LANDING_TIME,F_REMAINING_TICKETS)
values(@airline,@origin,@destination,@departure,@landing,@remaining);
GO
/****** Object:  StoredProcedure [dbo].[ADD_TICKET]    Script Date: 2/11/2021 7:49:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[ADD_TICKET]
@flight bigint,
@customer bigint
as
insert into TICKETS(T_FLIGHT_ID,T_CUSTOMER_ID) values(@flight,@customer);

GO
/****** Object:  StoredProcedure [dbo].[CLEAN_UP]    Script Date: 2/11/2021 7:49:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[CLEAN_UP]
as
/*insert into TICKET_HISTORY(H_TICKET_ID,H_TICKET_FLIGHT_ID,H_TICKET_CUSTOMER_ID)
select T_ID,T_FLIGHT_ID,T_CUSTOMER_ID from TICKETS join FLIGHTS on F_LANDING_TIME<=DATEADD(hour,-3,GETDATE());
delete TICKETS from TICKETS inner join TICKET_HISTORY on T_ID=H_TICKET_ID;
insert into FLIGHT_HISTORY
select F_ID,F_AIRLINE_ID,F_ORIGIN_COUNTRYCODE,F_DESTINATION_COUNTRYCODE,F_DEPARTURE_TIME,F_LANDING_TIME,F_REMAINING_TICKETS
  from FLIGHTS inner join  TICKET_HISTORY on F_ID=H_TICKET_FLIGHT_ID;
  delete FLIGHTS from FLIGHTS  join FLIGHT_HISTORY on F_ID=H_FLIGHT_ID;*/
  /*INSERT INTO FLIGHT_HISTORY SELECT * FROM FLIGHTS WHERE  F_LANDING_TIME<=DATEADD(hour,-3,GETDATE());
DELETE FLIGHTS FROM FLIGHTS JOIN FLIGHT_HISTORY on F_ID=H_FLIGHT_ID;
INSERT INTO TICKET_HISTORY SELECT  T_ID,T_FLIGHT_ID,T_CUSTOMER_ID FROM TICKETS JOIN FLIGHT_HISTORY ON H_FLIGHT_ID=T_FLIGHT_ID;
DELETE TICKETS FROM TICKETS JOIN FLIGHT_HISTORY ON H_FLIGHT_ID=T_FLIGHT_ID;*/
insert into TICKET_HISTORY(H_TICKET_ID,H_TICKET_FLIGHT_ID,H_TICKET_CUSTOMER_ID)
select T_ID,T_FLIGHT_ID,T_CUSTOMER_ID from TICKETS join FLIGHTS on F_LANDING_TIME<=DATEADD(hour,-3,GETDATE());
delete TICKETS from TICKETS inner join TICKET_HISTORY on T_ID=H_TICKET_ID;
INSERT INTO FLIGHT_HISTORY SELECT * FROM FLIGHTS WHERE  F_LANDING_TIME<=DATEADD(hour,-3,GETDATE());
DELETE FLIGHTS FROM FLIGHTS JOIN FLIGHT_HISTORY on F_ID=H_FLIGHT_ID;
GO
/****** Object:  StoredProcedure [dbo].[DELETE_BY_ORDER]    Script Date: 2/11/2021 7:49:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[DELETE_BY_ORDER]
AS
BEGIN
DELETE FROM TICKET_HISTORY;
DELETE FROM FLIGHT_HISTORY;
DELETE FROM TICKETS;
DELETE FROM FLIGHTS;
DELETE FROM CUSTOMERS;
DELETE FROM AIRLINE;
DELETE FROM COUNTRIES;
END
GO
/****** Object:  StoredProcedure [dbo].[FLIGHTS_TO_HISTORY]    Script Date: 2/11/2021 7:49:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[FLIGHTS_TO_HISTORY]
as
insert into FLIGHT_HISTORY
select F_ID,F_AIRLINE_ID,F_ORIGIN_COUNTRYCODE,F_DESTINATION_COUNTRYCODE,F_DEPARTURE_TIME,F_LANDING_TIME,F_REMAINING_TICKETS
  from FLIGHTS  where F_LANDING_TIME<=DATEADD(hour,-3,GETDATE());
delete FLIGHTS 
from FLIGHTS inner join FLIGHT_HISTORY on F_ID=FLIGHT_HISTORY.H_FLIGHT_ID;
GO
/****** Object:  StoredProcedure [dbo].[GET_ADMIN_BY_ID]    Script Date: 2/11/2021 7:49:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[GET_ADMIN_BY_ID]
@id bigint
as
begin
select * from ADMIN where ADMIN_ID=@id;
end
GO
/****** Object:  StoredProcedure [dbo].[GET_ADMIN_BY_USERNAME]    Script Date: 2/11/2021 7:49:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[GET_ADMIN_BY_USERNAME]
@username nvarchar(50)
as
begin
select * from ADMIN where ADMIN_USER_NAME=@username
end
GO
/****** Object:  StoredProcedure [dbo].[GET_AIRLINE]    Script Date: 2/11/2021 7:49:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[GET_AIRLINE]
@id bigint
as
select * from AIRLINE where AL_ID=@id;
GO
/****** Object:  StoredProcedure [dbo].[GET_AIRLINE_BYUSER]    Script Date: 2/11/2021 7:49:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[GET_AIRLINE_BYUSER]
@user nvarchar(50)
as
select * from AIRLINE where AL_USERNAME=@user;
GO
/****** Object:  StoredProcedure [dbo].[GET_AIRLINES_BY_COUNTRY]    Script Date: 2/11/2021 7:49:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[GET_AIRLINES_BY_COUNTRY]
@countryCode bigint
as
select * from AIRLINE where AL_COUNTRYCODE=@countryCode;
GO
/****** Object:  StoredProcedure [dbo].[GET_ALL_AIRLINES]    Script Date: 2/11/2021 7:49:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[GET_ALL_AIRLINES]
as
select * from AIRLINE;
GO
/****** Object:  StoredProcedure [dbo].[GET_ALL_AVAILABLE_FLIGHTS]    Script Date: 2/11/2021 7:49:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[GET_ALL_AVAILABLE_FLIGHTS]
AS
BEGIN
SELECT FLIGHTS.F_ID,AIRLINE.AL_NAME,ORIGINS.COU_COUNTRY_NAME AS ORIGIN,DESTINATIONS.COU_COUNTRY_NAME AS DESTINATION,FLIGHTS.F_DEPARTURE_TIME,FLIGHTS.F_LANDING_TIME,FLIGHTS.F_REMAINING_TICKETS
FROM FLIGHTS JOIN AIRLINE ON FLIGHTS.F_AIRLINE_ID=AIRLINE.AL_ID
JOIN COUNTRIES AS ORIGINS ON F_ORIGIN_COUNTRYCODE=ORIGINS.COU_ID
JOIN COUNTRIES AS DESTINATIONS ON F_DESTINATION_COUNTRYCODE=DESTINATIONS.COU_ID
WHERE F_DEPARTURE_TIME >=DATEADD(HOUR,4,GETDATE()) AND F_REMAINING_TICKETS>0;

END
GO
/****** Object:  StoredProcedure [dbo].[GET_ALL_COUNTRIES]    Script Date: 2/11/2021 7:49:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[GET_ALL_COUNTRIES]
as
select * from COUNTRIES;
GO
/****** Object:  StoredProcedure [dbo].[GET_ALL_CUSTOMERS]    Script Date: 2/11/2021 7:49:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[GET_ALL_CUSTOMERS]
as
select * from CUSTOMERS
GO
/****** Object:  StoredProcedure [dbo].[GET_ALL_FLIGHTS]    Script Date: 2/11/2021 7:49:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[GET_ALL_FLIGHTS]
as
select * from FLIGHTS;
GO
/****** Object:  StoredProcedure [dbo].[GET_ALL_TICKETS]    Script Date: 2/11/2021 7:49:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[GET_ALL_TICKETS]
as
select * from TICKETS;
GO
/****** Object:  StoredProcedure [dbo].[GET_COUNTRY]    Script Date: 2/11/2021 7:49:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[GET_COUNTRY]
@id bigint
as
select * from COUNTRIES where COU_ID=@id;
GO
/****** Object:  StoredProcedure [dbo].[GET_CUSTOMER_BY_ID]    Script Date: 2/11/2021 7:49:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[GET_CUSTOMER_BY_ID]
@id bigint
as
select * from CUSTOMERS where C_ID=@id;
GO
/****** Object:  StoredProcedure [dbo].[GET_CUSTOMER_BY_USERNAME]    Script Date: 2/11/2021 7:49:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[GET_CUSTOMER_BY_USERNAME]
@user nvarchar(50)
as
select * from CUSTOMERS where C_USER_NAME=@user;
GO
/****** Object:  StoredProcedure [dbo].[GET_CUSTOMER_TICKET]    Script Date: 2/11/2021 7:49:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[GET_CUSTOMER_TICKET]
@id bigint,
@flightId bigint
as
begin
select top 1 *  from TICKETS where T_FLIGHT_ID=@flightId and T_CUSTOMER_ID=@id  
end
GO
/****** Object:  StoredProcedure [dbo].[GET_FLIGHT]    Script Date: 2/11/2021 7:49:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[GET_FLIGHT]
@id bigint
as
select * from FLIGHTS where F_ID=@id;
GO
/****** Object:  StoredProcedure [dbo].[GET_FLIGHTS_BY_AIRLINE]    Script Date: 2/11/2021 7:49:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[GET_FLIGHTS_BY_AIRLINE] 
@id int
as 
SELECT * FROM FLIGHTS WHERE F_AIRLINE_ID=@id;
GO
/****** Object:  StoredProcedure [dbo].[GET_FLIGHTS_BY_CUSTOMER]    Script Date: 2/11/2021 7:49:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[GET_FLIGHTS_BY_CUSTOMER]
@id bigint
as
select * from FLIGHTS join TICKETS on F_ID=T_FLIGHT_ID where T_CUSTOMER_ID=@id;
GO
/****** Object:  StoredProcedure [dbo].[GET_FLIGHTS_BY_DESTINATION_COUNTRY]    Script Date: 2/11/2021 7:49:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[GET_FLIGHTS_BY_DESTINATION_COUNTRY]
@country bigint
as
select * from FLIGHTS where F_DESTINATION_COUNTRYCODE=@country;
GO
/****** Object:  StoredProcedure [dbo].[GET_FLIGHTS_BY_ORIGIN_COUNTRY]    Script Date: 2/11/2021 7:49:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[GET_FLIGHTS_BY_ORIGIN_COUNTRY]
@country bigint
as
select * from FLIGHTS where F_ORIGIN_COUNTRYCODE=@country;
GO
/****** Object:  StoredProcedure [dbo].[GET_FLIGHTS_BY_TIMESPAN]    Script Date: 2/11/2021 7:49:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[GET_FLIGHTS_BY_TIMESPAN]
@flightId int=NULL,
@airlineId int=NULL,
@origin int=NULL,
@destination int=NULL,
@departure1 datetime=NULL,
@departure2 datetime=NULL,
@landing1 datetime=NULL,
@landing2 datetime=NULL
as
Begin
SELECT * FROM FLIGHTS WHERE(F_ID=@flightId OR @flightId IS null) AND(F_AIRLINE_ID=@airlineId OR @airlineId IS NULL) 
AND(F_ORIGIN_COUNTRYCODE=@origin OR @origin IS NULL) AND(F_DESTINATION_COUNTRYCODE=@destination OR @destination IS NULL)
AND(F_DEPARTURE_TIME>=@departure1 OR @departure1 IS NULL)AND(F_DEPARTURE_TIME<=@departure2 OR @departure2 IS NULL)
 AND(F_LANDING_TIME>=@landing1 OR @landing1 IS NULL)AND(F_LANDING_TIME<=@landing2 OR @landing2 IS NULL)

End
GO
/****** Object:  StoredProcedure [dbo].[GET_FLIGHTS_BYDEPARTURE]    Script Date: 2/11/2021 7:49:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[GET_FLIGHTS_BYDEPARTURE]
@departure datetime
as
select * from FLIGHTS where F_DEPARTURE_TIME=@departure;
GO
/****** Object:  StoredProcedure [dbo].[GET_FLIGHTS_BYLAND]    Script Date: 2/11/2021 7:49:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[GET_FLIGHTS_BYLAND]
@land datetime
as
select * from FLIGHTS where F_LANDING_TIME=@land;
GO
/****** Object:  StoredProcedure [dbo].[GET_PRESENTABLE_AIRLINE_BY_NAME]    Script Date: 2/11/2021 7:49:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GET_PRESENTABLE_AIRLINE_BY_NAME]
@name NVARCHAR(50)
AS
BEGIN
SELECT AIRLINE.AL_ID,AIRLINE.AL_NAME,COUNTRIES.COU_COUNTRY_NAME FROM AIRLINE
JOIN COUNTRIES ON AL_COUNTRYCODE=COU_ID where AL_NAME=@name;
END
GO
/****** Object:  StoredProcedure [dbo].[GET_PRESENTABLE_AIRLINES]    Script Date: 2/11/2021 7:49:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GET_PRESENTABLE_AIRLINES]
AS
BEGIN
SELECT AIRLINE.AL_ID,AIRLINE.AL_NAME,COUNTRIES.COU_COUNTRY_NAME FROM AIRLINE
JOIN COUNTRIES ON AL_COUNTRYCODE=COU_ID
END
GO
/****** Object:  StoredProcedure [dbo].[GET_PRESENTABLE_FLIGHTS_BY_CUSTOMER]    Script Date: 2/11/2021 7:49:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GET_PRESENTABLE_FLIGHTS_BY_CUSTOMER]
@id bigint
as
SELECT F_ID AS id, AIRLINE.AL_NAME AS airline,ORIGINS.COU_COUNTRY_NAME AS origin,
DESTINATIONS.COU_COUNTRY_NAME AS destination,F_DEPARTURE_TIME,F_LANDING_TIME
 from FLIGHTS JOIN AIRLINE ON F_AIRLINE_ID=AIRLINE.AL_ID
 JOIN COUNTRIES AS ORIGINS ON F_ORIGIN_COUNTRYCODE=ORIGINS.COU_ID 
 JOIN COUNTRIES AS DESTINATIONS ON F_DESTINATION_COUNTRYCODE=DESTINATIONS.COU_ID 
 JOIN TICKETS on F_ID=T_FLIGHT_ID where T_CUSTOMER_ID=@id;
GO
/****** Object:  StoredProcedure [dbo].[GET_TICKET]    Script Date: 2/11/2021 7:49:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[GET_TICKET]
@id bigint

as

select * from TICKETS where T_ID=@id;
GO
/****** Object:  StoredProcedure [dbo].[GET_TICKET_BY_INFO]    Script Date: 2/11/2021 7:49:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[GET_TICKET_BY_INFO]
@customerId bigint,
@flightId bigint
AS
SELECT TOP 1 * FROM TICKETS WHERE T_FLIGHT_ID=@flightId AND T_CUSTOMER_ID=@customerId;
GO
/****** Object:  StoredProcedure [dbo].[REMOVE_AIRLINE]    Script Date: 2/11/2021 7:49:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[REMOVE_AIRLINE]
@id bigint
as
delete from AIRLINE where AL_ID=@id;
GO
/****** Object:  StoredProcedure [dbo].[REMOVE_COUNTRY]    Script Date: 2/11/2021 7:49:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[REMOVE_COUNTRY]
@id bigint
as
delete  from COUNTRIES where COU_ID=@id
GO
/****** Object:  StoredProcedure [dbo].[REMOVE_CUSTOMER]    Script Date: 2/11/2021 7:49:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[REMOVE_CUSTOMER]
@id bigint
as
delete from CUSTOMERS where C_ID=@id;
GO
/****** Object:  StoredProcedure [dbo].[REMOVE_FLIGHT]    Script Date: 2/11/2021 7:49:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[REMOVE_FLIGHT]
@id bigint
as
delete  FLIGHTS where F_ID=@id;
GO
/****** Object:  StoredProcedure [dbo].[REMOVE_TICKET]    Script Date: 2/11/2021 7:49:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[REMOVE_TICKET]
@id bigint
as
delete from TICKETS where T_ID=@id;
GO
/****** Object:  StoredProcedure [dbo].[SEARCH_FLIGHT]    Script Date: 2/11/2021 7:49:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[SEARCH_FLIGHT]

@id bigint=NULL,
@airlineId bigint=NULL,
@origin int=NULL,
@destination int=NULL,
@departureTime datetime=NULL,
@landingTime datetime=NULL
as
Begin
SELECT F_ID,AIRLINE.AL_NAME,ORIGINS.COU_COUNTRY_NAME AS ORIGIN_NAME,
DESTINATIONS.COU_COUNTRY_NAME AS DESTINATION_NAME,F_DEPARTURE_TIME,F_LANDING_TIME,F_REMAINING_TICKETS
 FROM FLIGHTS
 JOIN AIRLINE ON F_AIRLINE_ID=AL_ID
 JOIN COUNTRIES AS ORIGINS ON F_ORIGIN_COUNTRYCODE=ORIGINS.COU_ID
 JOIN COUNTRIES AS DESTINATIONS ON F_DESTINATION_COUNTRYCODE=DESTINATIONS.COU_ID
WHERE(F_ID=@id OR @id IS NULL) AND (F_AIRLINE_ID=@airlineId OR @airlineId IS NULL) 
AND (F_ORIGIN_COUNTRYCODE=@origin OR @origin IS NULL) AND (F_DESTINATION_COUNTRYCODE=@destination OR @destination IS NULL)
AND (convert(date, F_DEPARTURE_TIME)=Convert(date,@departureTime) AND F_DEPARTURE_TIME >=DATEADD(HOUR,4,GETDATE()) or @departureTime IS NULL) AND (F_LANDING_TIME=@landingTime OR @landingTime IS NULL) AND F_REMAINING_TICKETS>0;
End

GO
/****** Object:  StoredProcedure [dbo].[TICKETS_TO_HISTORY]    Script Date: 2/11/2021 7:49:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[TICKETS_TO_HISTORY]
as
 insert into TICKET_HISTORY
  select T_ID,T_FLIGHT_ID,T_CUSTOMER_ID from TICKETS join FLIGHT_HISTORY on T_FLIGHT_ID=FLIGHT_HISTORY.H_FLIGHT_ID;
 delete TICKETS from TICKETS inner join TICKET_HISTORY on T_ID=H_TICKET_ID;
GO
/****** Object:  StoredProcedure [dbo].[UPDATE_AIRLINE]    Script Date: 2/11/2021 7:49:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[UPDATE_AIRLINE]
@id bigint,
@name nvarchar(50),
@userName nvarchar(50),
@password nvarchar(50),
@countryCode bigint
as
update AIRLINE set AL_NAME=@name,AL_USERNAME=@userName,AL_PASSWORD=@password,AL_COUNTRYCODE=@countryCode where AL_ID=@id;
GO
/****** Object:  StoredProcedure [dbo].[UPDATE_COUNTRY]    Script Date: 2/11/2021 7:49:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[UPDATE_COUNTRY]
@id bigint,
@name nvarchar(50)
as
update COUNTRIES set COU_COUNTRY_NAME=@name,COU_ID=@id where COU_ID=@id;
GO
/****** Object:  StoredProcedure [dbo].[UPDATE_CUSTOMER]    Script Date: 2/11/2021 7:49:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[UPDATE_CUSTOMER]
@id bigint,
@firstName nvarchar(50),
@lastName nvarchar(50),
@userName nvarchar(50),
@password nvarchar(50),
@address nvarchar(50),
@phoneNumber nvarchar(50),
@CreditCard nvarchar(50)
as
update CUSTOMERS set C_FIRST_NAME=@firstName, C_LAST_NAME=@lastName, C_USER_NAME=@userName,C_PASSWORD=@password,
C_ADDRESS=@address,C_PHONE_NUMBER=@phoneNumber,CREDIT_CARD_NUMBER=@CreditCard where C_ID=@id;
GO
/****** Object:  StoredProcedure [dbo].[UPDATE_FLIGHT]    Script Date: 2/11/2021 7:49:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[UPDATE_FLIGHT]
@id bigint,
@airline bigint,
@origin bigint,
@destination bigint,
@departure datetime,
@landing datetime,
@tickets int
as

update FLIGHTS set F_AIRLINE_ID=@airline, F_ORIGIN_COUNTRYCODE=@origin,F_DESTINATION_COUNTRYCODE=@destination,F_DEPARTURE_TIME=@departure,F_LANDING_TIME=@landing,F_REMAINING_TICKETS=@tickets where F_ID=@id;
GO
/****** Object:  StoredProcedure [dbo].[UPDATE_TICKET]    Script Date: 2/11/2021 7:49:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[UPDATE_TICKET]
@id bigint,
@flight bigint,
@customer bigint
as
update TICKETS set T_FLIGHT_ID=@flight,T_CUSTOMER_ID=@customer where T_ID=@id;
GO
USE [master]
GO
ALTER DATABASE [FlightCom] SET  READ_WRITE 
GO
