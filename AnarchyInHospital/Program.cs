using System;
using System.Collections.Generic;
using System.Linq;

namespace AnarchyInHospital
{
    class Progam
    {
        static void Main(string[] args)
        {
            Hospital hospital = new Hospital();
            bool isOpen = true;

            while (isOpen)
            {
                Console.WriteLine($"{(int)MenuCommands.SortPatientsByName}. {MenuCommands.SortPatientsByName}" +
                                  $"\n{(int)MenuCommands.SortPatientsByAge}. {MenuCommands.SortPatientsByAge}" +
                                  $"\n{(int)MenuCommands.FindPatientsByDisease}. {MenuCommands.FindPatientsByDisease}" +
                                  $"\n{(int)MenuCommands.Exit}. {MenuCommands.Exit}");

                MenuCommands userInput = (MenuCommands)GetNumber(Console.ReadLine());

                switch (userInput)
                {
                    case MenuCommands.SortPatientsByName:
                        hospital.SortPatientsByName();
                        break;
                    case MenuCommands.SortPatientsByAge:
                        hospital.SortPatientsByAge();
                        break;
                    case MenuCommands.FindPatientsByDisease:
                        hospital.FindPatientsByDisease();
                        break;
                    case MenuCommands.Exit:
                        isOpen = false;
                        break;
                }

                Console.ReadKey(true);
                Console.Clear();
            }

            Console.WriteLine("Выход");
        }

        public static int GetNumber(string numberText)
        {
            int number;

            while (int.TryParse(numberText, out number) == false)
            {
                Console.WriteLine("Повторите попытку:");
                numberText = Console.ReadLine();
            }

            return number;
        }
    }

    enum MenuCommands
    {
        SortPatientsByName = 1,
        SortPatientsByAge,
        FindPatientsByDisease,
        Exit
    }

    enum Diseases
    {
        AIDS = 1,
        LungsСancer,
        PancreasCancer,
        Hypertension,
        Death
    }

    class Hospital
    {
        private Random _random = new Random();
        private List<Patients> _patients = new List<Patients>();

        public Hospital(List<Patients> patients = null)
        {
            if (patients == null)
                SetDefaultListPatients();
        }

        public void SortPatientsByName()
        {
            Console.Clear();
            IOrderedEnumerable<Patients> filteredPatients = _patients.OrderBy(patient => patient.Name);
            ShowInfoOrderPatients(filteredPatients);
        }

        public void SortPatientsByAge()
        {
            Console.Clear();
            IOrderedEnumerable<Patients> filteredPatients = _patients.OrderBy(patient => patient.Age);
            ShowInfoOrderPatients(filteredPatients);
        }

        public void FindPatientsByDisease()
        {
            Console.Clear();
            Console.WriteLine($"Выберите заболевание:" +
                              $"\n{(int)Diseases.AIDS}. {Diseases.AIDS}" +
                              $"\n{(int)Diseases.LungsСancer}. {Diseases.LungsСancer}" +
                              $"\n{(int)Diseases.PancreasCancer}. {Diseases.PancreasCancer}" +
                              $"\n{(int)Diseases.Hypertension}. {Diseases.Hypertension}" +
                              $"\n{(int)Diseases.Death}. {Diseases.Death}");

            Diseases disease = (Diseases)Progam.GetNumber(Console.ReadLine());
            ShowPatientByDisease(disease);
        }

        private void ShowPatientByDisease(Diseases disease)
        {
            Console.Clear();
            var filteredPatients = _patients.Where(patient => patient.Disease == disease);

            foreach (var patient in filteredPatients)
            {
                patient.ShowInfo();
            }
        }

        private void ShowInfoOrderPatients(IOrderedEnumerable<Patients> patients)
        {
            foreach (var patient in patients)
            {
                patient.ShowInfo();
            }
        }

        private void SetDefaultListPatients()
        {
            int maximumDiseases = Enum.GetNames(typeof(Diseases)).Length + 1;
            int minimumDiseases = 1;
            int maximumSick = 21;
            int minimumSick = 10;
            int maximumAge = 91;
            int minimumAge = 70;

            for (int i = 0; i < _random.Next(minimumSick, maximumSick); i++)
            {
                _patients.Add(new Patients($"Больной_{i + 1}", _random.Next(minimumAge, maximumAge), (Diseases)_random.Next(minimumDiseases, maximumDiseases)));
            }
        }
    }

    class Patients
    {
        public string Name { get; private set; }
        public int Age { get; private set; }
        public Diseases Disease { get; private set; }

        public Patients(string name, int age, Diseases disease)
        {
            Name = name;
            Age = age;
            Disease = disease;
        }

        public void ShowInfo()
        {
            Console.WriteLine($"Больной - {Name} | Возраст - {Age} | Заболевание - {Disease}");
        }
    }
}