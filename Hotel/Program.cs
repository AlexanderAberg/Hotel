using EFCore_Code_First_Tutorial.Data;

namespace Hotel
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DataInitializer.Build();
        }
    }
}
