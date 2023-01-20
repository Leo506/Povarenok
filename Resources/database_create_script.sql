create table PickupPoint
(
    PointId   int identity
        constraint PickupPoint_pk
            primary key,
    PostIndex varchar(6)   not null,
    City      nvarchar(50) not null,
    Street    nvarchar(50) not null
)
go

create table [Order]
(
    OrderID           int identity
        primary key,
    OrderStatus       nvarchar(max) not null,
    OrderDeliveryDate datetime      not null,
    OrderPickupPoint  int           not null
        constraint Order_PickupPoint_null_fk
            references PickupPoint,
    ClientName        nvarchar(100),
    GetCode           int,
    OrderData         datetime      not null
)
go

create table Product
(
    ProductArticleNumber   nvarchar(100)  not null
        primary key,
    ProductName            nvarchar(max)  not null,
    ProductDescription     nvarchar(max)  not null,
    ProductCategory        nvarchar(max)  not null,
    ProductPhoto           image,
    ProductManufacturer    nvarchar(max)  not null,
    ProductCost            decimal(19, 4) not null,
    ProductDiscountAmount  tinyint,
    ProductQuantityInStock int            not null,
    CurrentDiscount        tinyint        not null,
    Supplier               nvarchar(50)   not null
)
go

create table OrderList
(
    OrderId   int           not null
        constraint OrderList_Order_null_fk
            references [Order],
    ProductId nvarchar(100) not null
        constraint OrderList_Product_null_fk
            references Product,
    Amount    int           not null,
    Id        int identity
        constraint OrderList_pk
            primary key
)
go

create table Role
(
    RoleID   int identity
        primary key,
    RoleName nvarchar(100) not null
)
go

create table [User]
(
    UserID         int identity
        primary key,
    UserSurname    nvarchar(100) not null,
    UserName       nvarchar(100) not null,
    UserPatronymic nvarchar(100) not null,
    UserLogin      nvarchar(max) not null,
    UserPassword   nvarchar(max) not null,
    UserRole       int           not null
        references Role
)
go

