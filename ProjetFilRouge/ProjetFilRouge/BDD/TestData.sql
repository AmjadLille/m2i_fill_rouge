--insert into users (nom, prenom, pseudo, mdp, email, isstatut, isadmin) values
----('heutte','marion','xylaise','xylaise','xylaise@gmail.com','1',0),
----('kassir','yohan','wolraj','wolraj','wolraj@gmail.com','2',1)
--('TEST','TEST','TestUser','test','test@gmail.com',2,0),
--('TEST2','TEST2','TestUser2','test2','test2@gmail.com',2,0),
--('TEST3','TEST3','TestUser3','test3','test3@gmail.com',2,0)

--INSERT INTO Canal values
--('c#',3,null,2),
--('JAVA',3,null,3),
--('HTML',4,null,2);

--INSERT INTO UserInCanal (idCanal, idUser) values
--(1,3),
--(2,3),
--(3,4)

--INSERT INTO Commentaire (idUser,isStatut) values
--(5,2);

--INSERT INTO Commentaire values
--(5,null,'Test commentaire utilisateur 5',2);

INSERT INTO Contenu Values
('image',1,null,1,null,'canard',1),
('Lien',4,null,null,'www.google.fr',null,3),
('PhNeutre',2,1,2,null,null,2),
('JavaBien',3,2,3,'www.yahoo.fr',null,1),
('C#PourLesNuls',1,3,1,null,'DesChats',3)

--INSERT INTO Commentaire Values
--(1,1,'J''ai trouvé de quoi revolutionner le C#, je suis un génie, acclamez moi',1),
--(2,2,'J''ai trouvé de quoi revolutionner le JAVA, je suis UN FOU',3),
--(2,3,'J''ai trouvé de quoi revolutionner le PHP, Appelez moi sensei à partir d''aujourd''hui',2),
--(3,4,'J''ai trouvé de quoi revolutionner le MONDE du dev',2),
--(2,5,'J''ai trouvé ... ',1)