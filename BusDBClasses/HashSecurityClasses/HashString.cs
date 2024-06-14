using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

/******************************************************************************
     * File:        HashString.cs
     * Author:      Nils Hollenstein
     * Created:     2024-06-13
	 * Version:     1.0
     * Description: This file contains the HashString class, which hashes and verifys strings
     * 
     * History:
     * Date        Author             Changes
     * ----------  ----------------   ----------------------------------------------------
     * 2024-06-13  Nils Hollenstein   Initial creation.
     * 
     * License:
     * This software is provided 'as-is', without any express or implied
     * warranty. In no event will the authors be held liable for any damages
     * arising from the use of this software.
     * 
     * This file is part of the SeatReserve-Pro project.
     * 
     ******************************************************************************/

namespace BusDBClasses.HashSecurityClasses
{
    public class HashString
    {
        // https://www.claudiobernasconi.ch/2023/06/23/how-to-hash-passwords-with-bcrypt-in-csharp/
        // Method to hash a string with BCrypt
        public string HashBCrypt(string toHashText)
        {
            string passwordHash = BCrypt.Net.BCrypt.EnhancedHashPassword(toHashText);
            return passwordHash;
        }

        // Method to check if two BCrypt hashes are the same
        public bool VerifyBCrypt(string passwordHash, string inputToVerify)
        {
            bool correctinput = BCrypt.Net.BCrypt.EnhancedVerify(inputToVerify, passwordHash);
            return correctinput;
        }

        // Code from https://gist.github.com/grandsilence/e74d0fca294d0cbeda6baf581c32a306
        // Method to hash a string with SHA-512
        public string Hash512(string text)
        {
            var sb = new StringBuilder();
            using (var hash = SHA512.Create())
            {
                var result = hash.ComputeHash(Encoding.UTF8.GetBytes(text));
                for (int i = 0; i < result.Length; i++)
                sb.Append(result[i].ToString("x2"));
            }
            Console.WriteLine(sb.ToString());
            return sb.ToString();
        }
    }
}
