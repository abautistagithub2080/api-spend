-- Con ACCESS.
CREATE TABLE CatUsuario(
    IDUsr AUTOINCREMENT PRIMARY KEY,
    Nombre CHAR(255), 
    Paterno CHAR(255),
    Materno CHAR(255),
    Usuario CHAR(255),
    Password CHAR(255),
	CONSTRAINT [Unique Index] UNIQUE ([IDUsr])
);


PARAMETERS [@Usuario] Text ( 255 ), [@Contra] Text ( 255 );
SELECT CatUsuario.Nombre
FROM CatUsuario
WHERE (((CatUsuario.Usuario)=[@Usuario]) AND ((CatUsuario.Password)=[@Contra]));

spGetUsuario
PARAMETERS [@Usuario] Text ( 255 ), [@Contra] Text ( 255 );
SELECT IDUsr
FROM CatUsuario
WHERE (((CatUsuario.Usuario)=[@Usuario]) AND ((CatUsuario.Password)=[@Contra]));

SP_User
PARAMETERS [@IDUsr] Short;
SELECT IDUsr, (TRIM(Nombre) + ' ' + TRIM(Paterno) + ' ' + TRIM(Materno)) AS NomCom FROM CatUsuario   



CREATE TABLE Menu(
	IDMenu AUTOINCREMENT PRIMARY KEY,
	Menu CHAR(30) NOT NULL,
	Ruta CHAR(255) NOT NULL,
	Accion CHAR(30) NOT NULL,
	Img CHAR(255) NOT NULL,
	IDOrden int NULL,
	CONSTRAINT [Unique Index] UNIQUE ([IDMenu])
);



INSERT INTO Menu(Menu, Ruta, Accion, Img, IDOrden) VALUES('Inicio', 'Main', '', 'img/Home-I.png', 1); 
INSERT INTO Menu(Menu, Ruta, Accion, Img, IDOrden) VALUES('Transacciones', 'Transaccion', '', 'img/Build.png', 2);
INSERT INTO Menu(Menu, Ruta, Accion, Img, IDOrden) VALUES('Catalogo', 'CatGasto', '', 'img/Docs.png', 3); 
INSERT INTO Menu(Menu, Ruta, Accion, Img, IDOrden) VALUES('Administración', 'Admon', '', 'img/Foco.png', 4); 
INSERT INTO Menu(Menu, Ruta, Accion, Img, IDOrden) VALUES('Asignación', 'Asign', '', 'img/Docs.png', 5); 
INSERT INTO Menu(Menu, Ruta, Accion, Img, IDOrden) VALUES('Salir', 'OUT', '', 'img/Direc-I.png', 99999); 


CREATE PROCEDURE SP_MenuXUserID @IDUsr INT
AS
SELECT IDMenu,Ruta, Img FROM Menu WHERE IDMenu IN(SELECT IDMenu FROM Accesos WHERE IDUsr=@IDUsr) ORDER BY IDOrden ASC


CREATE PROCEDURE SP_Menu @IDMenu INT AS 
SELECT IDMenu, Menu FROM Menu



CREATE TABLE Accesos(
	IDAcceso AUTOINCREMENT PRIMARY KEY,
    IDUsr INT,
	IDMenu INT,
	CONSTRAINT [Unique Index] UNIQUE ([IDAcceso])
);


INSERT INTO Accesos(IDUsr,IDMenu) VALUES(1,1)
INSERT INTO Accesos(IDUsr,IDMenu) VALUES(1,2)
INSERT INTO Accesos(IDUsr,IDMenu) VALUES(1,3)
INSERT INTO Accesos(IDUsr,IDMenu) VALUES(1,4)
INSERT INTO Accesos(IDUsr,IDMenu) VALUES(1,6)
GO
INSERT INTO Accesos(IDUsr,IDMenu) VALUES(2,1)
INSERT INTO Accesos(IDUsr,IDMenu) VALUES(2,2)
INSERT INTO Accesos(IDUsr,IDMenu) VALUES(2,3)
INSERT INTO Accesos(IDUsr,IDMenu) VALUES(2,4)
INSERT INTO Accesos(IDUsr,IDMenu) VALUES(2,6)
INSERT INTO Accesos(IDUsr,IDMenu) VALUES(2,5)
GO

CREATE TABLE CatUsuario(
    IDUsr AUTOINCREMENT PRIMARY KEY,
    Nombre CHAR(255), 
    Paterno CHAR(255),
    Materno CHAR(255),
    Usuario CHAR(255),
    Password CHAR(255),
	CONSTRAINT [Unique Index] UNIQUE ([IDUsr])
);


CREATE TABLE CatGastos(
    IDGastos AUTOINCREMENT PRIMARY KEY,
    Gastos VARCHAR(255),
	IDUsr INT NULL,	
	CONSTRAINT [Unique Index] UNIQUE ([IDGastos])
);

PARAMETERS [@IDUsr] Short;
SELECT IDGastos, Gastos FROM CatGastos WHERE IDUsr = @IDUsr 

spInsertCatGasto 
PARAMETERS [@WGas] Text(255), [@IDUsr] Short;
INSERT INTO CatGastos (Gastos, IDUsr) VALUES([@WGas], [@IDUsr]);  

spUpdateCatGasto
PARAMETERS [@WGas] Text(255), [@IDUsr] Short,  [@ID] Short ;
UPDATE CatGastos SET Gastos=[@WGas], IDUsr=[@IDUsr] WHERE IDGastos=[@ID]  


SP_CatGastoDELID
PARAMETERS [@IDUsr] Short, [@ID] Short;
DELETE FROM CatGastos WHERE IDGastos=[@ID] AND IDUsr=[@IDUsr]




