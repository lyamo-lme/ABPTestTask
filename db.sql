create database ABPTT
go
use ABPTT

create table Device
(
    Id          bigint identity
        constraint PK_Device
            primary key,
    DeviceToken nvarchar(15) not null
)
go

create table ColorExperiment
(
    Id       bigint identity
        constraint PK_ColorExperiment
            primary key,
    Color    nvarchar(15) not null,
    DeviceId bigint       not null
        constraint FK_ColorExperiment_DeviceId_Device_Id
            references Device
)
go

create table PriceExperiment
(
    Id       bigint identity
        constraint PK_PriceExperiment
            primary key,
    Price    decimal(19, 5) not null,
    DeviceId bigint         not null
        constraint FK_PriceExperiment_DeviceId_Device_Id
            references Device
)
go





