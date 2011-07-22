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
INSERT INTO ProjectTypes (Id, Name, IsActive, [Order]) Values('SH', 'Sharepoint', 0, 4)

/*
	Insert into Requirement Types
*/
INSERT INTO RequirementTypes (Id, Name, IsActive) Values ('FC', 'Functional', 1)
INSERT INTO RequirementTypes (Id, Name, IsActive) Values ('NF', 'Non-Functional', 1)

/*
	Insert Status Codes
*/
INSERT INTO StatusCodes (Id, Name, Display) Values ('B', 'Beta', 1)
INSERT INTO StatusCodes (Id, Name, Display) Values ('C', 'Cancelled', 0)
INSERT INTO StatusCodes (Id, Name, Display) Values ('D', 'Developing', 1)
INSERT INTO StatusCodes (Id, Name, Display) Values ('P', 'Production', 0)
INSERT INTO StatusCodes (Id, Name, Display) Values ('W', 'Waiting', 1)
INSERT INTO StatusCodes (Id, Name, Display) Values ('H', 'On Hold', 1)

/*
	Insert into Section Types
*/
INSERT INTO SectionTypes (Id, Name, IsActive, [Order], IsSpecial)   VALUES ('AP', 'Appendix', 1, 7, 0)    
INSERT INTO SectionTypes (Id, Name, IsActive, [Order], IsSpecial)   VALUES ('DC', 'Description', 1, 0, 0)    
INSERT INTO SectionTypes (Id, Name, IsActive, [Order], IsSpecial)   VALUES ('DG', 'Diagrams', 1, 999, 0)    
INSERT INTO SectionTypes (Id, Name, IsActive, [Order], IsSpecial)   VALUES ('DT', 'Development Timeline', 1, 1, 0)    
INSERT INTO SectionTypes (Id, Name, IsActive, [Order], IsSpecial)   VALUES ('EL', 'Expected Load', 1, 2, 0)    
INSERT INTO SectionTypes (Id, Name, IsActive, [Order], IsSpecial)   VALUES ('GL', 'Glossary', 1, 8, 1)    
INSERT INTO SectionTypes (Id, Name, IsActive, [Order], IsSpecial)   VALUES ('PR', 'Purpose', 1, 3, 0)    
INSERT INTO SectionTypes (Id, Name, IsActive, [Order], IsSpecial)   VALUES ('RQ', 'Requirements', 1, 9, 1)    
INSERT INTO SectionTypes (Id, Name, IsActive, [Order], IsSpecial)   VALUES ('SC', 'System Context', 1, 4, 0)    
INSERT INTO SectionTypes (Id, Name, IsActive, [Order], IsSpecial)   VALUES ('SF', 'Screen Flow', 1, 5, 0)    
INSERT INTO SectionTypes (Id, Name, IsActive, [Order], IsSpecial)   VALUES ('SM', 'System Main Features', 1, 6, 0)    
INSERT INTO SectionTypes (Id, Name, IsActive, [Order], IsSpecial)   VALUES ('UC', 'Use Cases', 1, 10, 1)    

INSERT INTO SectionTypes (Id, Name, IsActive, [Order], IsSpecial)   VALUES ('IN', 'Introduction', 0, 900, 0)    
INSERT INTO SectionTypes (Id, Name, IsActive, [Order], IsSpecial)   VALUES ('SP', 'Scope', 0, 901, 0)    
INSERT INTO SectionTypes (Id, Name, IsActive, [Order], IsSpecial)   VALUES ('OE', 'Operating Environment', 0, 903, 0)    
INSERT INTO SectionTypes (Id, Name, IsActive, [Order], IsSpecial)   VALUES ('RL', 'Roles', 0, 904, 0)    
INSERT INTO SectionTypes (Id, Name, IsActive, [Order], IsSpecial)   VALUES ('A1', 'Appendix A', 0, 905, 0)    
INSERT INTO SectionTypes (Id, Name, IsActive, [Order], IsSpecial)   VALUES ('A2', 'Appendix B', 0, 906, 0)    
INSERT INTO SectionTypes (Id, Name, IsActive, [Order], IsSpecial)   VALUES ('A3', 'Appendix C', 0, 907, 0)    
INSERT INTO SectionTypes (Id, Name, IsActive, [Order], IsSpecial)   VALUES ('A4', 'Appendix D', 0, 908, 0)    
INSERT INTO SectionTypes (Id, Name, IsActive, [Order], IsSpecial)   VALUES ('SD', 'System Context Diagram', 0, 909, 0)    
INSERT INTO SectionTypes (Id, Name, IsActive, [Order], IsSpecial)   VALUES ('PF', 'Proposed Screen Flow', 0, 910, 0)    



/* Speicalized section types */
INSERT INTO SectionTypes (Id, Name, Description, IsActive, [Order], IsSpecial) Values ('GL', 'Glossary', '', 1, 7, 1)
INSERT INTO SectionTypes (Id, Name, Description, IsActive, [Order], IsSpecial) Values ('RQ', 'Requirements', '', 1, 8, 1)
INSERT INTO SectionTypes (Id, Name, Description, IsActive, [Order], IsSpecial) Values ('UC', 'Use Cases', '', 1, 9, 1)