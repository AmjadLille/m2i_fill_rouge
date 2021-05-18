Select * from users;
--select * from canal;
--select * from Commentaire;
--select * from UserInCanal;
--select * From Contenu

---- Récupére le theme du canal avec le pseudo du 
-- créateur
--select canal.theme, users.pseudo
--from userincanal
--join users
--on userincanal.iduser = users.id 
--join canal
--on userincanal.idcanal = canal.id 

----Récupération des inscriptions en cours
--Select * from Users where Users.isStatut = 1

---- Modification du statut de l'inscription
--UPDATE Users 
--set isStatut = 1
--where isStatut = 2
--and Users.id = 1;

-- si on veut modifier son email son mdp (modif profil utilisateur)

--Update Users
--set email = 'blablabla', mdp = 'toto'
--where Users.id = 1 

--requête de connexion 
--select * from users 
--where upper(pseudo) like upper('xylaise')  
--and mdp = 'xylaise' 

-- requête pour valider du contenu

--select Contenu.id,
--	   Contenu.titre,
--	   Users.pseudo as ContenuOwner,
--	   Commentaire.msg,
--	   Users.pseudo as CanalOwner,
--	   Contenu.link,
--	   Contenu.img,
--	   Contenu.isStatut
--from Contenu
--left join Users
--on Users.id = Contenu.idUser
--left join Commentaire
--on Commentaire.id = Contenu.idComment
--where Contenu.link like '%%';

Select id,
	   nom,
	   prenom,
	   pseudo,
	   mdp,
	   email,
	   isStatut,
	    REPLACE(REPLACE(REPLACE(isStatut, '1', 'En cours de validation'), 
		'2', 'Actif')
		, '3', 'Inactif') as isStatutReplace,
	   isAdmin,
	   REPLACE(Replace(isAdmin, 1,'Oui'),0,'Non') as isAdminReplace
from Users



