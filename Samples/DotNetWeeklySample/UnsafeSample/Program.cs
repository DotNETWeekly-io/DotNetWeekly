namespace UnsafeSample
{
    internal class Program
    {
        static unsafe void Main(string[] args)
        {
            var number = 42;
            int* pointer = &number;

            Console.WriteLine("value: {0}", number);
            Console.WriteLine("pointer: {0}", pointer->ToString());
            Console.WriteLine("address: {0}", (int)pointer);

            GetTriple(pointer);
            Console.WriteLine("value: {0}", number);
            Console.WriteLine("pointer: {0}", pointer->ToString());
            Console.WriteLine("address: {0}", (int)pointer);

            var text = "happy";
            fixed(char* textPrt = text)
            {
                for (int i = 0; i < text.Length; i++)
                {
                    Console.WriteLine("text[{0}]: {1}", i, *(textPrt + i));
                }
            }

            Console.ReadKey();
        }

        static unsafe void GetTriple(int* num)
        {
            *num *= 3;
        }
    }
}