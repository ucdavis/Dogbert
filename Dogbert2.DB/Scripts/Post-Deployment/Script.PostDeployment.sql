/*
	Insert the departments
*/
INSERT INTO Departments (Id, Name) Values ('ADNO', 'CA&ES Dean''s Office')

/*
	Insert Priority Types
*/
INSERT INTO PriorityTypes (Id, Name, IsActive, [Order]) Values('H', 'High', 1, 3)
INSERT INTO PriorityTypes (Id, Name, IsActive, [Order]) Values('M', 'Medium', 1, 1)
INSERT INTO PriorityTypes (Id, Name, IsActive, [Order]) Values('L', 'Low', 1, 2)

/*
	Insert in the Project Types
*/
INSERT INTO ProjectTypes (Id, Name, IsActive, [Order]) Values('IN', 'Infrastructure', 1, 3)
INSERT INTO ProjectTypes (Id, Name, IsActive, [Order]) Values('WA', 'Web Application', 1, 1)
INSERT INTO ProjectTypes (Id, Name, IsActive, [Order]) Values('WS', 'Web Site', 1, 2)

/*
	Insert into Requirement Types
*/
INSERT INTO RequirementTypes (Id, Name, IsActive) Values ('FC', 'Functional', 1)
INSERT INTO RequirementTypes (Id, Name, IsActive) Values ('NF', 'Non-Functional', 1)

/*
	Insert Status Codes
*/
INSERT INTO StatusCodes (Id, Name) Values ('B', 'Beta')
INSERT INTO StatusCodes (Id, Name) Values ('C', 'Cancelled')
INSERT INTO StatusCodes (Id, Name) Values ('D', 'Developing')
INSERT INTO StatusCodes (Id, Name) Values ('P', 'Production')
INSERT INTO StatusCodes (Id, Name) Values ('W', 'Waiting')

/*
	Insert into Text Types
*/
INSERT INTO TextTypes (Id, Name, Description, IsActive, [Order]) Values ('DC', 'Description', '', 1, 0)
INSERT INTO TextTypes (Id, Name, Description, IsActive, [Order]) Values ('DT', 'Development Timeline', '', 1, 1)
INSERT INTO TextTypes (Id, Name, Description, IsActive, [Order]) Values ('EL', 'Expected Load', '', 1, 2)
INSERT INTO TextTypes (Id, Name, Description, IsActive, [Order]) Values ('PR', 'Purpose', '', 1, 3)
INSERT INTO TextTypes (Id, Name, Description, IsActive, [Order]) Values ('SC', 'System Context', '', 1, 4)
INSERT INTO TextTypes (Id, Name, Description, IsActive, [Order]) Values ('SF', 'Screen Flow', '', 1, 5)
INSERT INTO TextTypes (Id, Name, Description, IsActive, [Order]) Values ('SM', 'System Main Features', '', 1, 6)