using System;
using System.Collections.Generic;

namespace LegislativeAssemblyApp
{
    class Program
    {
        static LegislativeAssembly assembly = new LegislativeAssembly("Goa Legislative Assembly", "Panaji");

        static void Main(string[] args)
        {
            int choice;
            do
            {
                Console.WriteLine("\nGoa Legislative Assembly Management System");
                Console.WriteLine("1. Add Member");
                Console.WriteLine("2. Schedule Session");
                Console.WriteLine("3. Propose Bill");
                Console.WriteLine("4. Vote on Bill");
                Console.WriteLine("5. Display Members");
                Console.WriteLine("0. Exit");
                Console.Write("Enter your choice: ");
                choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        AddMember();
                        break;
                    case 2:
                        ScheduleSession();
                        break;
                    case 3:
                        ProposeBill();
                        break;
                    case 4:
                        VoteOnBill();
                        break;
                    case 5:
                        DisplayMembers();
                        break;
                    case 0:
                        Console.WriteLine("Exiting...");
                        break;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            } while (choice != 0);
        }

        static void AddMember()
        {
            Console.Write("Enter Member Name: ");
            string name = Console.ReadLine();
            Console.Write("Enter Constituency Name: ");
            string constituencyName = Console.ReadLine();
            Console.Write("Enter Population: ");
            int population = int.Parse(Console.ReadLine());
            Constituency constituency = new Constituency(constituencyName, population);
            Console.Write("Enter Party: ");
            string party = Console.ReadLine();
            Console.Write("Enter Contact Info: ");
            string contactInfo = Console.ReadLine();
            Member member = new Member(name, constituency, party, contactInfo);
            assembly.AddMember(member);
            Console.WriteLine("Member added successfully.");
        }

        static void ScheduleSession()
        {
            Console.Write("Enter Session Date: ");
            string date = Console.ReadLine();
            Console.Write("Enter Agenda: ");
            string agenda = Console.ReadLine();
            Session session = new Session(date, agenda);
            assembly.ScheduleSession(session);
            Console.WriteLine("Session scheduled successfully.");
        }

        static void ProposeBill()
        {
            Console.Write("Enter Member Name: ");
            string name = Console.ReadLine();
            Member member = FindMemberByName(name);
            if (member != null)
            {
                Console.Write("Enter Bill Title: ");
                string title = Console.ReadLine();
                Console.Write("Enter Bill Description: ");
                string description = Console.ReadLine();
                Bill bill = new Bill(title, description);
                member.ProposeBill(bill);
                Console.WriteLine("Bill proposed successfully.");
            }
            else
            {
                Console.WriteLine("Member not found.");
            }
        }

        static void VoteOnBill()
        {
            Console.Write("Enter Member Name: ");
            string name = Console.ReadLine();
            Member member = FindMemberByName(name);
            if (member != null)
            {
                Console.Write("Enter Bill Title: ");
                string title = Console.ReadLine();
                Console.Write("Enter Vote (yes/no): ");
                string vote = Console.ReadLine();
                Bill bill = new Bill(title, "");
                member.VoteOnBill(bill, vote);
                Console.WriteLine("Vote recorded successfully.");
            }
            else
            {
                Console.WriteLine("Member not found.");
            }
        }

        static void DisplayMembers()
        {
            List<Member> members = assembly.GetMembers();
            if (members.Count == 0)
            {
                Console.WriteLine("No members found.");
            }
            else
            {
                Console.WriteLine("Members of the Goa Legislative Assembly:");
                foreach (Member member in members)
                {
                    Console.WriteLine($"Name: {member.GetName()}, {member.GetConstituency().GetDetails()}, Party: {member.GetParty()}, Contact Info: {member.GetContactInfo()}");
                }
            }
        }

        static Member FindMemberByName(string name)
        {
            foreach (Member member in assembly.GetMembers())
            {
                if (member.GetName().Equals(name, StringComparison.OrdinalIgnoreCase))
                {
                    return member;
                }
            }
            return null;
        }
    }

    class Bill
    {
        public string Title { get; }
        public string Description { get; }
        public string Status { get; private set; }

        public Bill(string title, string description)
        {
            Title = title;
            Description = description;
            Status = "proposed";
        }

        public void UpdateStatus(string status)
        {
            Status = status;
        }

        public string GetDetails()
        {
            return $"Title: {Title}, Description: {Description}, Status: {Status}";
        }
    }

    class Constituency
    {
        public string Name { get; }
        public int Population { get; }

        public Constituency(string name, int population)
        {
            Name = name;
            Population = population;
        }

        public string GetDetails()
        {
            return $"Constituency: {Name}, Population: {Population}";
        }
    }

    class LegislativeAssembly
    {
        public string Name { get; }
        public string Location { get; }
        private List<Member> Members { get; }
        private List<Session> Sessions { get; }

        public LegislativeAssembly(string name, string location)
        {
            Name = name;
            Location = location;
            Members = new List<Member>();
            Sessions = new List<Session>();
        }

        public void AddMember(Member member)
        {
            Members.Add(member);
        }

        public void ScheduleSession(Session session)
        {
            Sessions.Add(session);
        }

        public List<Member> GetMembers()
        {
            return Members;
        }

        public List<Session> GetSessions()
        {
            return Sessions;
        }
    }

    class Member
    {
        public string Name { get; }
        public Constituency Constituency { get; }
        public string Party { get; }
        public string ContactInfo { get; }

        public Member(string name, Constituency constituency, string party, string contactInfo)
        {
            Name = name;
            Constituency = constituency;
            Party = party;
            ContactInfo = contactInfo;
        }

        public string GetName()
        {
            return Name;
        }

        public Constituency GetConstituency()
        {
            return Constituency;
        }

        public string GetParty()
        {
            return Party;
        }

        public string GetContactInfo()
        {
            return ContactInfo;
        }

        public void ProposeBill(Bill bill)
        {
            Console.WriteLine($"{Name} has proposed the bill: {bill.Title}");
        }

        public void VoteOnBill(Bill bill, string vote)
        {
            Console.WriteLine($"{Name} has voted {vote} on the bill: {bill.Title}");
        }
    }

    class Session
    {
        public string Date { get; }
        public string Agenda { get; }

        public Session(string date, string agenda)
        {
            Date = date;
            Agenda = agenda;
        }

        public string GetAgenda()
        {
            return Agenda;
        }

        public string GetDate()
        {
            return Date;
        }
    }
}
