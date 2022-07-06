class Program
{
    static void Main(string[] args)
    {
        var applicaiton = new Application();
       // applicaiton.PrintFromLocalVariable();
        applicaiton.PrintDateFromLocalStruct();
    }

    public class Application
    {
        private int number = 42;
        public void SomeMethod(int number)
        {
            
        }

        public void PrintFromLocalVariable()
        {
            int day = 12;
            int month = 10;
            int year = 2022;
            Console.WriteLine($"The date is: {day}/{month}/{year}");
        }

        public void PrintDateFromLocalStruct()
        {
            DateStruct ds = new DateStruct()
            {
                day = 12,
                month = 10,
                year = 2022
            };
            Console.WriteLine($"The date is: {ds.day}/{ds.month}/{ds.year}");
        }
    }
    
    public struct  DateStruct
    {
        public int day;
        public int month;
        public int year;
    }
}