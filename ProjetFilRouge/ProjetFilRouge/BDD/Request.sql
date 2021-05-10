--Select * from users;
--select * from canal;
--select * from Commentaire;
--select * from UserInCanal;

-- Select * users avec affichage des statuts
--Select nom, prenom, pseudo, mdp, email, 
--REPLACE(isStatut, 1, 'En cours de validation'),
--REPLACE(isStatut, 2, 'Actif')
--from Users

-- Récupére le theme du canal avec le pseudo du 
-- créateur
--Select Canal.theme, Users.pseudo as Owner
--From Canal
--join Users
--on Canal.idUser = Users.id

--Select Canal.theme, Users.pseudo
--from UserInCanal
--join Users
--on UserInCanal.idUser = Users.id 
--join Canal
--on UserInCanal.idCanal = Canal.id 

--select * from Users;
--UPDATE Users 
--set isStatut = 3
--where isStatut = 1
--and Users.id = 1;
--select * from Users

-- Valider l'inscription d'un utilisateur
-- (changer son IsStatus)

-- si on veut modifier son email son mdp (modif profil utilisateur)

--Update Users
--set email = 'blablabla', mdp = 'toto'
--where Users.id = 1 
--select email, mdp 
--from Users

-- requête pour valider du contenu
select * From Contenu


