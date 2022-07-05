namespace CounterLib.Delegates
{
    public class Delegates
    {
        public delegate void PrintMesDel(string str);

        public delegate void ClearAndPrintMesDel(object obj1, object obj2 = null);
    }
}
