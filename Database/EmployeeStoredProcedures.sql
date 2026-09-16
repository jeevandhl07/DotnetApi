IF OBJECT_ID(N'dbo.Employees', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Employees
    (
        Id INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Employees PRIMARY KEY,
        Name NVARCHAR(150) NOT NULL,
        Email NVARCHAR(255) NOT NULL,
        Salary DECIMAL(18,2) NOT NULL
    );
END
GO

CREATE OR ALTER PROCEDURE dbo.Employee_GetAll
AS
BEGIN
    SET NOCOUNT ON;

    SELECT Id, Name, Email, Salary
    FROM dbo.Employees
    ORDER BY Id;
END
GO

CREATE OR ALTER PROCEDURE dbo.Employee_GetById
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT Id, Name, Email, Salary
    FROM dbo.Employees
    WHERE Id = @Id;
END
GO

CREATE OR ALTER PROCEDURE dbo.Employee_Create
    @Name NVARCHAR(150),
    @Email NVARCHAR(255),
    @Salary DECIMAL(18,2)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO dbo.Employees (Name, Email, Salary)
    VALUES (@Name, @Email, @Salary);

    SELECT Id, Name, Email, Salary
    FROM dbo.Employees
    WHERE Id = CONVERT(INT, SCOPE_IDENTITY());
END
GO

CREATE OR ALTER PROCEDURE dbo.Employee_Update
    @Id INT,
    @Name NVARCHAR(150),
    @Email NVARCHAR(255),
    @Salary DECIMAL(18,2)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE dbo.Employees
    SET Name = @Name,
        Email = @Email,
        Salary = @Salary
    WHERE Id = @Id;

    SELECT Id, Name, Email, Salary
    FROM dbo.Employees
    WHERE Id = @Id;
END
GO

CREATE OR ALTER PROCEDURE dbo.Employee_Delete
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM dbo.Employees
    WHERE Id = @Id;

    SELECT @@ROWCOUNT;
END
GO
