USE Charity

CREATE TABLE Donor
(
  DonorID INT IDENTITY(1,1),
  Name VARCHAR(100),
  Number CHAR(11),
  Address VARCHAR(255),
  Bio VARCHAR(1000),
  Birthday DATE,
  Avatar VARBINARY(MAX),
  CONSTRAINT PK_Donor PRIMARY KEY (DonorID)
);

CREATE TABLE Account
(
  Email VARCHAR(255),
  AccountID INT IDENTITY(1,1),
  Password VARCHAR(255),
  Role INT,
  DonorID INT,
  CONSTRAINT PK_Account PRIMARY KEY (AccountID),
  CONSTRAINT FK_Account_Donor FOREIGN KEY (DonorID) REFERENCES Donor(DonorID)
);

CREATE TABLE Volunteer
(
  VolunteerID INT IDENTITY(1,1),
  Job VARCHAR(1000),
  Education VARCHAR(MAX),
  Experience VARCHAR(MAX),
  DonorID INT,
  CONSTRAINT PK_Volunteer PRIMARY KEY (VolunteerID),
  CONSTRAINT FK_Volunteer_Donor FOREIGN KEY (DonorID) REFERENCES Donor(DonorID)
);

CREATE TABLE PaymentMethod
(
  PaymentMethodID INT IDENTITY(1,1),
  Providder VARCHAR(100),
  AccountNumber CHAR(50),
  Name VARCHAR(1000),
  DonorID INT,
  CONSTRAINT PK_PaymentMethod PRIMARY KEY (PaymentMethodID),
  CONSTRAINT FK_PaymentMethod_Donor FOREIGN KEY (DonorID) REFERENCES Donor(DonorID)
);

CREATE TABLE Charities
(
  CharitiesID INT IDENTITY(1,1),
  Name VARCHAR(1000),
  Number CHAR(11),
  Address VARCHAR(MAX),
  Bio VARCHAR(MAX),
  Birthday DATE,
  Experience VARCHAR(MAX),
  Avatar VARBINARY(MAX),
  AccountID INT,
  CONSTRAINT PK_Charities PRIMARY KEY (CharitiesID),
  CONSTRAINT FK_Charities_Account FOREIGN KEY (AccountID) REFERENCES Account(AccountID)
);

CREATE TABLE Campaign
(
  CampainID INT IDENTITY(1,1),
  Name VARCHAR(MAX),
  DateBegin DATE,
  DateEnd DATE,
  Goals FLOAT,
  Describe VARCHAR(MAX),
  TagLine VARCHAR(2000),
  Location VARCHAR(MAX),
  CONSTRAINT PK_Campaign PRIMARY KEY (CampainID)
);

CREATE TABLE Comment
(
  CommentID INT IDENTITY(1,1),
  Value FLOAT,
  DonorID INT,
  CampainID INT,
  CONSTRAINT PK_Comment PRIMARY KEY (CommentID),
  CONSTRAINT FK_Comment_Donor FOREIGN KEY (DonorID) REFERENCES Donor(DonorID),
  CONSTRAINT FK_Comment_Campain FOREIGN KEY (CampainID) REFERENCES Campaign(CampainID)
);

CREATE TABLE Image
(
  ImageID INT IDENTITY(1,1),
  ImageData VARBINARY(MAX),
  CampainID INT,
  CONSTRAINT PK_Image PRIMARY KEY (ImageID),
  CONSTRAINT FK_Image_Campaign FOREIGN KEY (CampainID) REFERENCES Campaign(CampainID) --- ngu--
);

CREATE TABLE Tag
(
  TagID INT IDENTITY(1,1),
  Name CHAR(100),
  CONSTRAINT PK_Tag PRIMARY KEY (TagID)
);

CREATE TABLE Organize
(
  CharitiesID INT,
  CampainID INT,
  CONSTRAINT PK_Organize PRIMARY KEY (CharitiesID, CampainID),
  CONSTRAINT FK_Organize_Charities FOREIGN KEY (CharitiesID) REFERENCES Charities(CharitiesID),
  CONSTRAINT FK_Organize_Campaign FOREIGN KEY (CampainID) REFERENCES Campaign(CampainID)
);

CREATE TABLE Donate
(
  Value FLOAT,
  PaymentMethod VARCHAR(1000),
  Date DATE,
  DonorID INT,
  CampainID INT,
  CONSTRAINT PK_Donate PRIMARY KEY (DonorID, CampainID),
  CONSTRAINT FK_Donate_Donor FOREIGN KEY (DonorID) REFERENCES Donor(DonorID),
  CONSTRAINT FK_Donate_Campaign FOREIGN KEY (CampainID) REFERENCES Campaign(CampainID)
);

CREATE TABLE Participate
(
  VolunteerID INT,
  CampainID INT,
  CONSTRAINT PK_Participate PRIMARY KEY (VolunteerID, CampainID),
  CONSTRAINT FK_Participate_Volunteer FOREIGN KEY (VolunteerID) REFERENCES Volunteer(VolunteerID),
  CONSTRAINT FK_Participate_Campaign FOREIGN KEY (CampainID) REFERENCES Campaign(CampainID)
);

CREATE TABLE CampainTag
(
  CampainID INT,
  TagID INT IDENTITY(1,1),
  CONSTRAINT PK_CampainTag PRIMARY KEY (CampainID, TagID),
  CONSTRAINT FK_CampainTag_Campaign FOREIGN KEY (CampainID) REFERENCES Campaign(CampainID),
  CONSTRAINT FK_CampainTag_Tag FOREIGN KEY (TagID) REFERENCES Tag(TagID)
);