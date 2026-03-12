-- docker/mysql/init.sql
CREATE TABLE IF NOT EXISTS Users (
    -- Primary Key (CHAR(36) matches C# Guid)
    Id CHAR(36) NOT NULL PRIMARY KEY,

    -- User Details
    Username VARCHAR(50) NOT NULL,
    Email VARCHAR(255) NOT NULL,
    FirstName VARCHAR(100) NOT NULL,
    LastName VARCHAR(100) NOT NULL,
    PasswordHash VARCHAR(255) NOT NULL,

    -- Authentication Tokens
    RefreshToken VARCHAR(255) NULL,
    RefreshTokenExpiryTime DATETIME NULL,

    -- Audit & Status
    CreatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt DATETIME NULL,
    IsDeleted BOOLEAN NOT NULL DEFAULT FALSE,
    DeletedAt DATETIME NULL,
    
    -- Status Flag
    IsActive BOOLEAN NOT NULL DEFAULT TRUE,
    
    -- Indexes for performance
    INDEX IX_Users_Email (Email)
);