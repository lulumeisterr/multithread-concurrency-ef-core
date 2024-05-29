using System.Data;
using ConcorrenciaEF.Application.Data;
using ConcorrenciaEF.Application.infra;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

const string connection = @"Server=tcp:localhost,1433;Database=infinita_db;User ID=sa;Password=@Teste123;Encrypt=true;TrustServerCertificate=True;";

var tasks = new Task[3];
tasks[0] = Task.Run(async () => await UpdateOrCreateDataAsync(connection, new Negocio
{
    NumeroNegocio = 1,
    NaturezaOperacao = "BUYI",
    StatusNegociacao = "1",
    DataHoraMensagem = new DateTime(2024, 05, 19, 19, 10, 0).AddMilliseconds(100)
}));
tasks[1] = Task.Run(async () => await UpdateOrCreateDataAsync(connection, new Negocio
{
    NumeroNegocio = 1,
    NaturezaOperacao = "BUYI",
    StatusNegociacao = "2",
    DataHoraMensagem = new DateTime(2024, 05, 19, 19, 10, 0).AddMilliseconds(122)
}));
tasks[2] = Task.Run(async () => await UpdateOrCreateDataAsync(connection, new Negocio
{
    NumeroNegocio = 1,
    NaturezaOperacao = "BUYI",
    StatusNegociacao = "7",
    DataHoraMensagem = new DateTime(2024, 05, 19, 19, 10, 0).AddMilliseconds(123)
}));

await Task.WhenAll(tasks);

async Task UpdateOrCreateDataAsync(string connection, Negocio negocioRequest) {
    NegocioDbContext context = null;
    try
    {
        Console.WriteLine(negocioRequest.DataHoraMensagem.Millisecond);
        context = new NegocioDbContext(connection);
        /**
         *  garante que os dados lidos permaneçam consistentes durante a transação, mesmo que outras transações estejam ocorrendo simultaneamente.
         */
        var transaction = context.Database.BeginTransaction(IsolationLevel.RepeatableRead);
        Console.WriteLine("");
        Console.WriteLine($"Thread: {Thread.CurrentThread.ManagedThreadId}");
        Console.WriteLine("");
        Console.WriteLine("Buscando dados...");
        Negocio? dbNegocio = await context.Negocios
                           .FromSqlInterpolated($"SELECT * FROM Negocio WHERE NUMERO_NEGOCIO = 1") //coloca um tipo específico de bloqueio nas linhas afetadas pela instrução.
                           .AsNoTracking()
                           .FirstOrDefaultAsync();
        if (dbNegocio == null)
        {
            context.Add(new Negocio
            {
                NumeroNegocio = 1,
                NaturezaOperacao = "BUYI",
                StatusNegociacao = "1",
                DataHoraMensagem = DateTime.Now
            });
            await context.SaveChangesAsync();
            await transaction.CommitAsync();
            return;
        }
        Console.WriteLine($"Status {negocioRequest.StatusNegociacao}");

        if ( negocioRequest.DataHoraMensagem.Millisecond > dbNegocio.DataHoraMensagem.Millisecond)
        {
            dbNegocio.StatusNegociacao = negocioRequest.StatusNegociacao;
            dbNegocio.DataHoraMensagem = negocioRequest.DataHoraMensagem;
            Thread.Sleep(5000);
            context.Update(dbNegocio);
        }
        await context.SaveChangesAsync();
        await transaction.CommitAsync();
    }
    catch (SqlException ex)
    {
        Console.WriteLine(ex.StackTrace);

    }
    finally
    {
        context.Dispose();
        Console.WriteLine("Diposed e fechamento de conexao");
    }
}




