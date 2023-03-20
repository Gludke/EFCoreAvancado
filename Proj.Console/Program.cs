using Proj.Console.Data;

//Cria/Apaga o DB de forma agilizada, sem migrations, de acordo com os Modelos
static void EnsureCreatedAndDeleted()
{
    using var db = new AplicationContext();
    //Cria o DB se ele não existir
    db.Database.EnsureCreated();
    //Apaga todo o DB se ele existir
    db.Database.EnsureDeleted();
}

static void GapDoEnsureCreatedAndDeleted()
{
    //Em caso de 2+ contextos no mesmo DB, deve-se forçar a criação dos outros

    using var db1 = new AplicationContext();
    using var db2 = new OutroAplicationContext();


    var dbCreator =
}

