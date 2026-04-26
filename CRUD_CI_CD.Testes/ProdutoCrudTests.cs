
using Microsoft.EntityFrameworkCore;

namespace CRUD_CI_CD.Testes;

public class ProdutoCrudTests
{
    private AppDbContext GetAppContex()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        return new AppDbContext(options);
    }

    [Fact]
    public void Deve_Salvar_Produto()
    {
        var produto = new Produto { Nome = "Teste", Preco = 10.0m };
        using var context = GetAppContex();
        context.Produtos.Add(produto);
        context.SaveChanges();

        var produtoSalvo = context.Produtos.FirstOrDefault(p => p.Id == produto.Id);

        Assert.NotNull(produtoSalvo);
        Assert.Equal("Teste", produtoSalvo.Nome);
        Assert.Equal(10.0m, produtoSalvo.Preco);

    }

    [Fact]

    public void Deve_Atualizar_Produto()
    {
        var produto = new Produto { Nome = "Teste", Preco = 10.0m };
        using var context = GetAppContex();
        context.Produtos.Add(produto);
        context.SaveChanges();
        produto.Nome = "Teste Atualizado";
        produto.Preco = 20.0m;
        context.Produtos.Update(produto);
        context.SaveChanges();
        var produtoAtualizado = context.Produtos.FirstOrDefault(p => p.Id == produto.Id);
        Assert.NotNull(produtoAtualizado);
        Assert.Equal("Teste Atualizado", produtoAtualizado.Nome);
        Assert.Equal(20.0m, produtoAtualizado.Preco);
    }

    [Fact]
    public void Deve_Excluir_Produto()
    {
        var produto = new Produto { Nome = "Teste", Preco = 10.0m };
        using var context = GetAppContex();
        context.Produtos.Add(produto);
        context.SaveChanges();
        context.Produtos.Remove(produto);
        context.SaveChanges();
        var produtoExcluido = context.Produtos.FirstOrDefault(p => p.Id == produto.Id);
        Assert.Null(produtoExcluido);
    }

    [Fact]
    public void Deve_Listar_Produtos()
    {
        var produto1 = new Produto { Nome = "teste1", Preco = 10.0m };
        var produto2 = new Produto { Nome = "teste2", Preco = 20.0m };
        using var context = GetAppContex();
        context.Produtos.AddRange(produto1, produto2);
        context.SaveChanges();
        var produtos = context.Produtos.ToList();
        Assert.Equal(2, produtos.Count);
    }
}