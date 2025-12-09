CREATE TABLE Users (
    UserID INT PRIMARY KEY IDENTITY(1,1),
    Username VARCHAR(50) UNIQUE NOT NULL,
    Password VARCHAR(50) NOT NULL,
    Email VARCHAR(100)
);

CREATE TABLE Profiles (
    ProfileID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT FOREIGN KEY REFERENCES Users(UserID),
    Elementary VARCHAR(200),
    JuniorHigh VARCHAR(200),
    SeniorHigh VARCHAR(200),
    College VARCHAR(200),
    Skills VARCHAR(MAX),
    Hobbies VARCHAR(MAX),
    Message VARCHAR(MAX)
);

-- Insert Users
INSERT INTO Users (Username, Password, Email)
VALUES 
('Clark', '1234', 'quijadaclark@email.com'),
('Gold ni Wally', 'araymopakak', 'egulssayosahparakangkalaban@gmail.com'),
('Chuss', 'Chusama06', 'chuajared285@gmail.com'),
('jirombalgbag', 'pogiako123', 'jirommapagmahal@gmail.com'),
('Person5', 'test', 'p5@email.com');

-- Insert Profiles
INSERT INTO Profiles (UserID, Elementary, JuniorHigh, SeniorHigh, College, Skills, Hobbies, Message)
VALUES
(1, 'Sto. Rosario Montessori School', 'Sto. Rosario Montessori School', 'Our Lady Of Fatima Valenzuela', 'Pamantasan ng Lungsod ng Valenzuela',
 'Design, Singing, Dancing', 'Travel, Outdoors', 'Message to Sir Bill'),

(2, 'Marilao Central school', ' AFGBMTS', 'VPTNHS', 'Pamantasan ng Lungsod ng Valenzuela',
 'Drawing, Playing Instrument, Masaktan kahit walang karapatan', 'Playing sports, Online gaming, Listening to music, Workout, Magpasarap', 'Sarap mo sir Bill pakiss, solid mo maging Tc sana dika magbago sana ako parin hanggang dulo.'),

(3, 'Colegio De Sta Cecilia', 'Gen. T. De Leon National High School', 'Gen. T. De Leon National High School', 'Pamantasan ng Lungsod ng Valenzuela',
 'Singing, Playing Guitar', 'Travelling, Playing Games, Motorcycle Rides', 'Matsalab Sir dahil sayo lumalakas nako maging Data Analyst'),

(4, 'Dona Ata Elementary School', 'Disiplina Village Bignal National High School', 'Bignay National High School', 'Pamantasan ng Lungsod ng Valenzuela',
 'Hair Cutting, Drawing', 'Listening to music, Basketball', 'stay humble gang!.'),

(5, 'Pasolo Elementary School', 'Valenzuela City School of Mathematics and Science', 'Our Lady Of Fatima Valenzuela', 'Pamantasan ng Lungsod ng Valenzuela',
 'Music, Photography', 'Singing, Exploring', 'Capture every moment.');

 SELECT * FROM Users
 SELECT * FROM Profiles

DROP TABLE Profiles