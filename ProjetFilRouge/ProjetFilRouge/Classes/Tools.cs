using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ProjetFilRouge.Classes
{
    public class Tools
    {

        public static bool IsName(string name)
        {
            string pattern = @"^[a-zA-Z\s]+$";
            return Regex.IsMatch(name, pattern);
        }

        // contrairement à un nom/prenom, j'autorise les chiffres dans un pseudo.

        public static bool IsPseudo(string pseudo)
        {
            string pattern = @"^([a-zA-Z\s]+|[1-9]+)$";
            return Regex.IsMatch(pseudo, pattern);
        }
        public static bool IsMdp(string mdp)
        {
            string pattern = @"^([a-zA-Z\s]+|[1-9]+)$";
            return Regex.IsMatch(mdp, pattern);
        }
        // peut preciser mdp OBLIGATOIRE avec deux chiffres avec ça : @"^([a-zA-Z\s]+|[1-9]{2})$"

        public static bool IsEmail(string email)
        {
            string pattern = @"^([\w0-9\._-]+)@([a-z0-9\._-]{2,})\.([a-z]{2,5})$";
            return Regex.IsMatch(email, pattern);
        }

        //// ALERTE : on defini le nombre max de caractère dans un message ? Si oui ==> ? 

        //public static bool IsMessage(string message)
        //{

        //    string pattern = @"^([a-zA-Z\s]{200})$";
        //    return Regex.IsMatch(message, pattern);
        //}


    }
}
