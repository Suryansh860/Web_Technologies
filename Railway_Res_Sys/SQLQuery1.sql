--Creating database 
create database Train_Reservation
use Train_Reservation

--1.Creating table which consists of train details
Create table Trains(
Train_No numeric(5) not null primary key, 
Train_Name varchar(50), 
Source_Station varchar(30), 
Destination_Station varchar(30), 
Departure_Time time, 
Arrival_Time time,
isActiven Bit Not null Default 1
)

--Inserting train details into Train table.
Insert into Trains values(22411,'ARUNACHAL EXP','NAHARLAGUN','ANAND VIHAR TRM','12:05:00','23:30:00',1),
(12555,'GORAKHDHAM EXP','GORAKHPUR JN','BHATINDA JN','12:40:00 ','18:15:00',1),
(12553,'VAISHALI EXP','GORAKHPUR JN','NEW DELHI','13:30:00','06:25:00',1),
(15909,'AVADH ASSAM EXP','GORAKHPUR JN','LUCKNOW NR','05:25:00','11:20:00',1)

Select * from Trains
drop table Trains 


--2.Creating Table for Available seats in Train birth
Create table Berth_Availability(
Sr_No int identity,Train_No numeric(5) foreign key references Trains(Train_No),
[1-AC] numeric(3), 
[2-AC] numeric(3),
[SL-Class] numeric(3),
Total_Available_Berths numeric(4)
)

--Inserting Details of seat availability in Berth_Availability Tables.
Insert into Berth_Availability (Train_No, [1-AC] ,[2-AC], [SL-Class], Total_Available_Berths)
values(22411,200,200,500,900),(12555,200,200,500,900),(12553,200,200,500,900),(15909,200,200,500,900)

Select * from Berth_Availability
drop table Berth_Availability


--3.Creating Admin table.
Create table Admin(
Admin_Id numeric(10) unique, 
Admin_Name varchar(25),
Admin_Password varchar(20) not null)

--Inserting few Admin Details 
Insert into Admin values(123456,'Suryansh','Surya@1234')

Select * from Admin
drop table Admin
 

--4.Creating User table
Create table User_LogIn(
[User_id] numeric(10) primary key,
[User_Name] varchar(25),
User_Age numeric(2),
User_Password varchar(20) not null
)

Select * from User_LogIn
drop table User_LogIn


--5.Creating table for Fare/cost of berth seats.
Create table Seat_Fare(
Sr_No int identity,
Train_No numeric(5) foreign key references Trains(Train_No),
[1-AC Fare] numeric(5),
[2-AC Fare] numeric(5),
[SL-Class Fare] numeric(5)
)

--Inserting Fare of all classes seats.
Insert into Seat_Fare values(22411,2500,2000,1200),(12555,3000,2400,1600),(12553,2500,2000,1200),
(15909,4000,3000,1800)

Select * from Seat_Fare
drop table Seat_Fare




--6.Creating table for seat confirmation.
Create table Ticket_Confirmation(
Train_No numeric(5) foreign key references Trains(Train_No),
Passenger_Name varchar(25),
Age numeric(2),
[User_Id] numeric(10) foreign key references User_LogIn([User_Id]),
Source_Station varchar(30),
Destination_Station varchar(30),
Berth varchar(10),
Paid_Amount numeric(5),
Booking_Date date,
Travelling_Date date,
PNR_No numeric(20) primary key
)

Select * from Ticket_Confirmation
drop table Ticket_Confirmation

delete from Ticket_confirmation

--7.Creating table for ticket cancellation.
Create table Ticket_Cancellation(
Cancel_Id numeric(10) primary key,
Train_No numeric(5) foreign key references Trains(Train_No),
Passenger_Name varchar(25),
Age numeric(2),
[User_Id] numeric(10) foreign key references User_LogIn([User_Id]),
Berth varchar(10),
Paid_Amount numeric(5),
PNR_No numeric(20) foreign key references Ticket_Confirmation(PNR_No),
Cancel_Date date,
Cancel_Status varchar(20)
)

Select * from Ticket_Cancellation
drop table Ticket_Cancellation

---------------------------------------------------------------------------------------------------------------

--Procedure which subtracts the No. of Seats from Berth Availability table after the ticket booking done by User.

Create or Alter proc UpdateBookedTicket( @TrainNo NUMERIC(5),
    @Class NVARCHAR(20),
    @SeatsBooked INT)
as
Begin
 
    IF @Class = '1-AC'
        UPDATE Berth_Availability
        SET [1-AC] = [1-AC] - @SeatsBooked
        WHERE Train_No = @TrainNo;
    ELSE IF @Class = '2-AC'
        UPDATE Berth_Availability
        SET [2-AC] = [2-AC] - @SeatsBooked
        WHERE Train_No = @TrainNo;
    ELSE IF @Class = 'SL-Class'
        UPDATE Berth_Availability
        SET [SL-Class] = [SL-Class] - @SeatsBooked
        WHERE Train_No = @TrainNo;
end


--Procedure which adds the No. of Seats to Berth Availability table after the ticket cancellation done by User.

Create or Alter proc UpdateCancelTickets( @TrainNo NUMERIC(5),
    @Class NVARCHAR(20),
    @SeatsBooked INT) 
as
begin
 
    IF @Class = '1-AC'
        UPDATE Berth_Availability
        SET [1-AC] = [1-AC] + @SeatsBooked
        WHERE Train_No = @TrainNo;
    ELSE IF @Class = '2-AC'
        UPDATE Berth_Availability
        SET [2-AC] = [2-AC] + @SeatsBooked
        WHERE Train_No = @TrainNo;
    ELSE IF @Class = 'SL-Class'
        UPDATE Berth_Availability
        SET [SL-Class] = [SL-Class] + @SeatsBooked
        WHERE Train_No = @TrainNo;
end



--Procedure which adds the NO. of available seats for each birth when admin add a new train.

Create or Alter proc Add_Seat_In_Berth_Availability
(@tn_no numeric(5), @AC1_Seats numeric(5), @AC2_Seats numeric(5),@SL_Seats numeric(5),@Tot_Seats numeric(5))
as
Begin
Insert into Berth_Availability values(@tn_no, @AC1_Seats, @AC2_Seats, @SL_Seats, @Tot_Seats)
end


--Procedure which adds the Fair of a seats of a particular birth when admin adds a new train.
Create or Alter proc Add_Fair(@tn_no numeric(5), @F_Af numeric(5), @S_Af numeric(5),@Slf numeric(5))
as
Begin
Insert into Seat_Fare values(@tn_no, @F_Af, @S_Af,@Slf)
end


