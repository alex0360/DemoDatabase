namespace DemoDatabase.Local
{
    public class Distributor
    { 
        private readonly System.Random random = new System.Random();
        public int Movil => random.Next(10000000, 99999999);
    }
}
