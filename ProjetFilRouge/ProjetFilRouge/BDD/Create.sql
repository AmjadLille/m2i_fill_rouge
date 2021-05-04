CREATE TABLE Users(
id int identity(1,1) PRIMARY KEY,
nom varchar(50) not null,
prenom varchar(50) not null,
pseudo varchar(50) not null,
mdp varchar(50) not null,
email varchar(100) not null,
isStatut int not null, 
isAdmin bit not null
)
-- isStatus 
-- 1 -> En cours de validation
-- 2 -> Actif
-- 3 -> Inactif

CREATE TABLE UserInCanal(
id int identity(1,1) PRIMARY KEY,
idUser int not null,
idCanal int not null,
)

-- Quel utilisateur utilise quels canals et quel canal est utilisé par quels utilisateurs

CREATE TABLE Canal(
id int identity(1,1) PRIMARY KEY,
theme varchar(100) not null,
idUser int not null, 
idComment int,
isStatut int not null  
)

-- theme -> Le langage de programmation dont parlera de canal
-- isStatus
--	1 -> Actif
--	2 -> Inactif

CREATE TABLE Contenu(
id int identity(1,1) PRIMARY KEY,
titre varchar(100) not null,
idUser int not null,
idComment int,
idCanal int,
link varchar(500),
img varchar(500), 
isStatut int not null  
)
-- isStatus
--	1 -> Actif
--	2 -> Inactif

CREATE TABLE Commentaire(
id int identity(1,1) PRIMARY KEY,
idUser int not null,
idContenu int,
msg text,
isStatut int not null  
)

