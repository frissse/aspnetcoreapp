using Microsoft.EntityFrameworkCore;
using PM.BL;
using PM.DAL;
using PM.DAL.EF;
using PM.UI.CA;

DbContextOptionsBuilder options = new DbContextOptionsBuilder<PMdBContext>();
options.UseSqlite("Data Source=PM.db");
PMdBContext context = new PMdBContext(options.Options);
IRepository repository = new Repository(context);
IManager manager = new Manager(repository);
ConsoleUi consoleUi = new ConsoleUi(manager);

// InMemoryRepository.Seed();
// reeds aangepast, semicolon stond verkeerd waardoor Seed functie niet deel van de if code block
if (context.CreateDatabase(true)) 
    DataSeeder.Seed(context);

consoleUi.Run();



