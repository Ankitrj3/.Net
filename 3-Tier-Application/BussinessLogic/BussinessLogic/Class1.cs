using ClassLibrary1;
namespace BussinessLogic
{
    public class BussinessLogicClass
    {
        ReverseDal rev = new ReverseDal();
        public string ReverseStringBussinessLogic()
        {
            char[] val = rev.ReverseDll().ToCharArray();
            Array.Reverse(val);
            return new string(val);
        }
    }
}
