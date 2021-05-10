Select * from users;
--select * from canal;
--select * from Commentaire;
--select * from UserInCanal;

-- Select * users avec affichage des statuts
--Select nom, prenom, pseudo, mdp, email, 
--REPLACE(isStatut, 1, 'En cours de validation'),
--REPLACE(isStatut, 2, 'Actif')
--from Users

---- Récupére le theme du canal avec le pseudo du 
-- créateur
--Select Canal.theme, Users.pseudo as Owner
--From Canal
--join Users
--on Canal.idUser = Users.id

---- Récupérer le theme et le pseudo 
--Select Canal.theme, Users.pseudo
--from UserInCanal
--join Users
--on UserInCanal.idUser = Users.id 
--join Canal
--on UserInCanal.idCanal = Canal.id 

---- Modification du statut de l'inscription
--UPDATE Users set isStatut = 1
--where isStatut = 3 and Users.id = 1;

