using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Bislerium.Components.Data
{
    public class MemberService
    {
        private static void SaveAll(List<Member> members)
        {
            string appDataDirectoryPath = Utils.GetAppDirectoryPath();
            string appMembersFilePath = Utils.GetAppMembersFilePath();

            if (!Directory.Exists(appDataDirectoryPath))
            {
                Directory.CreateDirectory(appDataDirectoryPath);
            }

            var json = JsonSerializer.Serialize(members);
            File.WriteAllText(appMembersFilePath, json);
        }

        public static List<Member> GetAll()
        {
            string appMembersFilePath = Utils.GetAppMembersFilePath();
            if (!File.Exists(appMembersFilePath))
            {
                return new List<Member>();
            }

            var json = File.ReadAllText(appMembersFilePath);

            return JsonSerializer.Deserialize<List<Member>>(json);
        }

        public static List<Member> Create(Guid userId, string fullName, string phoneNo )
        {
            List<Member> members = GetAll();
            bool memberExists = members.Any(x => x.PhoneNo == phoneNo);

            if (memberExists)
            {
                throw new Exception("Member already exists.");
            }

            members.Add(
                new Member
                {
                    FullName = fullName,
                    PhoneNo = phoneNo
                }
            );;;
            SaveAll(members);
            return members;
        }

        public static Member GetByPhoneNo(String phoneNo)
        {
            List<Member> members = GetAll();
            return members.FirstOrDefault(x => x.PhoneNo == phoneNo);
        }

        public static List<Member> Delete(String phoneNo)
        {
            List<Member> members = GetAll();
            Member member = members.FirstOrDefault(x => x.PhoneNo == phoneNo);

            if (member == null)
            {
                throw new Exception("Member not found.");
            }

            members.Remove(member);
            SaveAll(members);

            return members;
        }

    }
}
