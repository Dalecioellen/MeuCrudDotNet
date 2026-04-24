using CRUD_CI_CD;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Runtime.InteropServices.Marshalling;

//-- configuração do banco 

var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
optionsBuilder.UseSqlServer("Server=DESKTOP-63LD54P\\SQLEXPRESS;Database=CRUD_CI_CD;Trusted_Connection=True;TrustServerCertificate=True;");

using  var db = new AppDbContext(optionsBuilder.Options);
db.Database.EnsureCreated();


bool rodando = true;

while (rodando)
{
    Console.WriteLine("Escolha uma opção:");
    Console.WriteLine("1 - Criar produto");
    Console.WriteLine("2 - Listar produtos");
    Console.WriteLine("3 - Atualizar produto");
    Console.WriteLine("4 - Deletar produto");
    Console.WriteLine("5 - Sair");
    var opcao = Console.ReadLine();
    switch (opcao)
    {
        case "1":
            CriarProduto(db);
            break;
        case "2":
            ListarProdutos(db);
            break;
        case "3":
            AtualizarProduto(db);
            break;
        case "4":
            DeletarProduto(db);
            break;
        case "5":
            rodando = false;
            break;
        default:
            Console.WriteLine("Opção inválida.");
            break;
    }
}

   void CriarProduto(AppDbContext db)
{
    Console.WriteLine("Digite o nome do produto:");
    var nome = Console.ReadLine();
    Console.WriteLine("Digite o preço do produto:");
    var preco = decimal.Parse(Console.ReadLine());
    var produto = new Produto { Nome = nome, Preco = preco };
    db.Produtos.Add(produto);
    db.SaveChanges();
    Console.WriteLine("Produto criado com sucesso!");
};

    void ListarProdutos(AppDbContext db)
    {
        var produtos = db.Produtos.ToList();
        foreach (var produto in produtos)
        {
            Console.WriteLine($"ID: {produto.Id}, Nome: {produto.Nome}, Preço: {produto.Preco}");
        }
    };
    
    void AtualizarProduto(AppDbContext db)
    {
        Console.WriteLine("Digite o ID do produto que deseja atualizar:");
        var id = int.Parse(Console.ReadLine());
        var produto = db.Produtos.Find(id);
        if (produto != null)
        {
            Console.WriteLine("Digite o novo nome do produto:");
            produto.Nome = Console.ReadLine();
            Console.WriteLine("Digite o novo preço do produto:");
            produto.Preco = decimal.Parse(Console.ReadLine());
            db.SaveChanges();
            Console.WriteLine("Produto atualizado com sucesso!");
        }
        else
        {
            Console.WriteLine("Produto não encontrado.");
        }
    };
    
    void DeletarProduto(AppDbContext db)
    {
        Console.WriteLine("Digite o ID do produto que deseja deletar:");
        var id = int.Parse(Console.ReadLine());
        var produto = db.Produtos.Find(id);
        if (produto != null)
        {
            db.Produtos.Remove(produto);
            db.SaveChanges();
            Console.WriteLine("Produto deletado com sucesso!");
        }
        else
        {
            Console.WriteLine("Produto não encontrado.");
        }
    };


