
namespace Company.LOB.LoanManagement.Entities
{
    public class LoanType
    {
        public string Name;

        public LoanType(string name)
        {
            this.Name = name;
        }

        public static LoanType A = new LoanType("A");
        public static LoanType B = new LoanType("B");
        public static LoanType C = new LoanType("C");
    }
}
