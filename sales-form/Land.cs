using System.Text;

namespace sales_form
{
    public class Land
    {
        public int X { get; set; }

        public int Y { get; set; }

        public bool ForSale { get; set; }

        public decimal SalesPrice { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.Append("Pavia ");
            sb.Append(X);
            sb.Append(" ");
            sb.Append(Y);
            sb.Append(" ");

            if (ForSale)
            {
                sb.Append(" sales price: ");
                sb.Append(SalesPrice / 1000000); // cnft api price is in lovelace
                sb.Append(" ADA");
            }

            return sb.ToString();
        }
    }
}
