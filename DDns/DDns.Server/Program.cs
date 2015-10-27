namespace DDns.Server {
    class Program {
        static void Main(string[] args) {
            DDnsServer server = new DDnsServer("0.0.0.0", 9876);
            server.Start();
        }
    }
}
