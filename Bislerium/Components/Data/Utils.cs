using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;


using System.Threading.Tasks;

namespace Bislerium.Components.Data
{
    public static class Utils
    {
        private const char _segmentDelimiter = ':';

        public static string HashSecret(string input)
        {
            var saltSize = 7;
            var iterations = 100_000;
            var keySize = 32;
            HashAlgorithmName algorithm = HashAlgorithmName.SHA256;
            byte[] salt = RandomNumberGenerator.GetBytes(saltSize);
            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(input, salt, iterations, algorithm, keySize);

            return string.Join(
                _segmentDelimiter,
                Convert.ToHexString(hash),
                Convert.ToHexString(salt),
                iterations,
                algorithm
            );
        }

        public static bool VerifyHash(string input, string hashString)
        {
            string[] segments = hashString.Split(_segmentDelimiter);
            byte[] hash = Convert.FromHexString(segments[0]);
            byte[] salt = Convert.FromHexString(segments[1]);
            int iterations = int.Parse(segments[2]);
            HashAlgorithmName algorithm = new(segments[3]);
            byte[] inputHash = Rfc2898DeriveBytes.Pbkdf2(
                input,
                salt,
                iterations,
                algorithm,
                hash.Length
            );

            return CryptographicOperations.FixedTimeEquals(inputHash, hash);
        }

        public static void GeneratePdf(List<Orders> orders, DateTime startDate, DateTime endDate)
        {
            PdfDocument pdfDocument = new PdfDocument(new PdfWriter(new FileStream("C:/Users/Acer/source/repos/Bislerium/Bislerium/wwwroot/hello.pdf", FileMode.Create, FileAccess.Write)));
            Document document = new Document(pdfDocument);

            Paragraph p = new Paragraph();

            String line = "Hello! Welcome to iTextPdf";
            document.Add(p.Add("Transaction Report"));
            document.Close();
            Console.WriteLine("Awesome PDF just got created.");
        }

        public static string GetAppDirectoryPath()
        {
            return Path.Combine("C:/Users/Acer/source/repos/Bislerium/Bislerium/wwwroot",
                "Bislerium"
            );
        }

        public static string GetAppUsersFilePath()
        {
            return Path.Combine(GetAppDirectoryPath(), "users.json");
        }

        public static string GetAppMembersFilePath()
        {
            return Path.Combine(GetAppDirectoryPath(), "members.json");
        }


        public static string GetAppCoffeeFilePath()
        {
            return Path.Combine(GetAppDirectoryPath(), "coffee.json");
        }

        public static string GetPurchasesDirectoryPath()
        {
            return Path.Combine(GetAppDirectoryPath(), "Purchases.json");
        }

        public static string GetAppAddOnDirectoryPath()
        {
            return Path.Combine(GetAppDirectoryPath(), "AddOns.json");
        }

    }
}
