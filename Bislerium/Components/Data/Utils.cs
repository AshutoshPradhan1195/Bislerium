using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography;
using System.Text;
using System.IO;

using System.Threading.Tasks;
using iTextSharp.text.pdf;
using iTextSharp.text;

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
            Document pdfDocument = new Document( );
            PdfWriter.GetInstance(pdfDocument, new FileStream(Path.Combine( GetAppDirectoryPath(), $"{DateTime.Now.Ticks} report.pdf"), FileMode.Create)
            );
            pdfDocument.Open();


            Paragraph p = new Paragraph();
            Paragraph coffees = new Paragraph();
            Paragraph addons = new Paragraph();
            double total = 0;

            PdfPTable dataTable = new PdfPTable(5);



            dataTable.AddCell("Member Name");
            dataTable.AddCell("Coffees");
            dataTable.AddCell("AddOns");
            dataTable.AddCell("Price");
            dataTable.AddCell("Date");

            foreach(Orders order in orders)
            {


                dataTable.AddCell($"{order.MemberName}");
                coffees.Clear();

                for (int i = 0; i < order.CoffeeName.Count(); i++)
                {
                    coffees.Add($"{order.CoffeeName[i]}\n");
                }

                addons.Clear();

                for (int i = 0; i < order.AddOnName.Count(); i++)
                {
                    addons.Add($"{order.AddOnName[i]}\n");
                }
                dataTable.AddCell(coffees);
                dataTable.AddCell(addons);
                dataTable.AddCell($"{order.Price}");
                dataTable.AddCell($"{order.OrderDate}");

            }

            p.Add("Transaction Report ");
            p.Add($"{startDate} to {endDate}");
            

            foreach(Orders order1 in orders)
            {
                total = total + order1.Price;
            }

            p.Add($"Revenue: {total}");
            pdfDocument.Add(p);
            pdfDocument.Add(dataTable);

            pdfDocument.Close();
        }

        public static string GetAppDirectoryPath()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
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
