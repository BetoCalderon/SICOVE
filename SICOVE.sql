use master;
go
drop  database SICOVE; 
Set nocount on
--creando base de datos
create database SICOVE;
go
use SICOVE;
go
--creando las tablas 
go
create table tb_Clientes(
	IdCliente int identity primary key not null,
	Nombre varchar(50) not null,
	Direccion varchar(50) not null,
	Celular varchar(20) not null,
	DUI varchar(20) not null,
	FechaRegistro date not null
);
go
create table tb_Roles(
	IdRole int identity primary key not null,
	Nombre varchar(50) not null
);
go
create table tb_FormaPago(
	IdFormaPago int identity primary key not null,
	Nombre varchar(30) not null
);
go
create table tb_Usuarios
(
	IdUsuario int identity primary key not null,
	Usuario varchar(50) not null,
	Clave varchar(50) not null,
	IdRoles int references tb_Roles
);
go
create table tb_Empleados(
	IdEmpleado int identity primary key not null,
	Nombre varchar(50) not null,
	Direccion varchar(50) not null,
	Celular varchar(20) not null,
	DUI varchar(20) not null,
	IdUsuario int references tb_Usuarios
);
go
create table tb_Proveedores(
	IdProveedor int identity primary key not null,
	Nombre varchar(50) not null,
	Direccion varchar(50) not null,
	Celular varchar(20) not null,
	DUI varchar(20) not null,
	FechaRegistro date not null

);
go
create table tb_Productos(
	IdProducto int identity primary key not null,
	NomProducto varchar(50) not null,
);
go
create table tb_Compras(
	IdCompra int identity primary key not null,
	NumFac int not null,	
	Detalle varchar(100),
	Cantidad int not null,
	SubTotal money not null,
	Descuento money,
	IVA money not null,
	Total money not null,
	Fecha date not null,
	IdProducto int references tb_Productos,
	IdProveedor int references tb_Proveedores,
	IdFormaPago int references tb_FormaPago,
	IdEmpleado int references tb_Empleados,
);
go
create table tb_Ventas
(
	IdVenta int identity primary key not null,
	NumFac int not null,	
	Detalle varchar(100),
	Cantidad int not null,
	Precio money not null,
	SubTotal money not null,
	Descuento money,
	IVA money not null,
	Total money not null,
	Fecha date not null,
	IdProducto int references tb_Productos,
	IdFormaPago int references tb_FormaPago,
	IdCliente int references tb_Clientes,
	IdEmpleado int references tb_Empleados,
);
go
create table tb_Inventarios
(
	IdInventario int identity primary key not null,
	Existencia int not null,
	Detalle varchar(100) not null,
	Precio money not null,
	Fecha date not null,
	IdProducto int references tb_Productos,
);
go

select * from tb_Clientes	